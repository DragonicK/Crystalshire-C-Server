using System;
using System.Collections.Generic;
using GameServer.Data;
using GameServer.Network;
using GameServer.Communication;
using GameServer.Server.Character;

namespace GameServer.Server {
    public sealed class Player {
        public IConnection Connection { get; set; }

        public bool Connected { 
            get {
                if (Connection != null) {
                    return Connection.Connected;
                }

                return false;
            }
         }

        /// <summary>
        /// Chave única de identificação no sistema.
        /// </summary>
        public string UniqueKey { get; set; }

        /// <summary>
        /// Id de indice de conexão.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Tempo de conexão em segundos.
        /// </summary>
        public int ConnectedTime { get; set; }

        /// <summary>
        /// Estado atual do usuário no sistema.
        /// </summary>
        public GameState GameState { get; set; }

        #region Account Data

        public int AccountId { get; set; }
        public string Username { get; set; }
        public int Cash { get; set; }
        public AccessLevel AccessLevel { get; set; }

        #endregion

        #region Character Data

        public int CharacterId { get; set; }
        public string Character { get; set; }
        public int ClassId { get; set; }
        public int Sprite { get; set; }
        public byte Gender { get; set; }

        public int Level { get; set; }
        public int Experience { get; set; }
        public int Points { get; set; }

        public Direction Direction { get; set; }
        public int RegionId { get; set; }
        public int MapId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public int[] Vitals { get; set; }
        
        public Dictionary<int, CharacterSelection> CharacterSelection { get; set; }

        #endregion

        public int Target { get; set; }
        public TargetType TargetType { get; set; }

        public int ChatNpcId { get; set; }
        public int ChatMapNpcIndex { get; set; }
        public int CurrentChat { get; set; }


        public Dictionary<int, Inventory> Inventory { get; set; }

        public Player(IConnection connection, WaitingUserData user) {
            Index = connection.Index;
            Connection = connection;
            AccountId = user.AccountId;
            Username = user.Username;
            UniqueKey = user.UniqueKey;
            AccessLevel = user.AccessLevel;
            Cash = user.Cash;
            GameState = GameState.Characters;

            Vitals = new int[Enum.GetValues(typeof(VitalType)).Length];

            Inventory = new Dictionary<int, Inventory>();

            for (var i = 1; i <= Configuration.MaxInventories; i++) {
                Inventory.Add(i, new Inventory());
            }

            CharacterSelection = new Dictionary<int, CharacterSelection>();

            for (var i = 1; i <= Configuration.MaxCharacters; i++) {
                CharacterSelection.Add(i, new CharacterSelection());
            }
        }

        public MapInstance GetMap() {
            return Global.Maps[MapId];
        }
    }
}