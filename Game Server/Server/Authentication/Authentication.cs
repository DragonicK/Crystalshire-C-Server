using System.Linq;
using System.Collections.Generic;
using GameServer.Network;
using GameServer.Database;
using GameServer.Communication;
using GameServer.Network.PacketList;
using Elysium.Logs;

namespace GameServer.Server {
    public static class Authentication {
        public static Dictionary<int, Player> Players { get; set; }

        public static int HighIndex { get; set; }

        static Authentication() {
            Players = new Dictionary<int, Player>();
        }

        public static void Add(WaitingUserData user, IConnection connection) {
            var index = connection.Index;
            var pData = new Player(connection, user);

            ((Connection)connection).OnDisconnect += OnDisconnect;
            ((Connection)connection).Authenticated = true;

            Players.Add(index, pData);

            var highIndex = new SpHighIndex(HighIndex);
            highIndex.SendToAll();
        }

        public static void OnDisconnect(int index) {
            if (Players.ContainsKey(index)) {

                SavePlayer(index);

                var msg = new SpPlayerLeft(index);
                msg.SendToMapBut(index, Players[index].MapId);
            }

            Players.Remove(index);
        }

        public static void Clear() {
            Players.Clear();
        }

        public static Player FindByAccountId(int accountId) {
            var pData = from player in Players.Values
                        where player.AccountId == accountId
                        select player;

            return pData.FirstOrDefault();
        }

        public static Player FindByUsername(string username) {
            var pData = from player in Players.Values
                        where (string.CompareOrdinal(player.Username, username) == 0)
                        select player;

            return pData.FirstOrDefault();
        }

        public static Player FindByCharacter(string character) {
            var pData = from player in Players.Values
                        where (string.CompareOrdinal(player.Character, character) == 0)
                        select player;

            return pData.FirstOrDefault();
        }

        public static void Quit(int index) {
            if (Players.ContainsKey(index)) {
                Players[index].Connection.Disconnect();
            }
        }

        private static void SavePlayer(int index) {
            var pData = Players[index];
            var db = new CharacterDB();
            var dbError = db.Open();

            if (dbError.Number != 0) {
                Global.WriteLog(LogType.System, $"Cannot connect to database", LogColor.Red);
                Global.WriteLog(LogType.System, $"Error Number: {dbError.Number}", LogColor.Red);
                Global.WriteLog(LogType.System, $"Error Message: {dbError.Message}", LogColor.Red);
            }
            else {
                db.UpdateCharacter(pData);
                db.Close();
            }
        }
    }
}