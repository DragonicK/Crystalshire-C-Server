using GameServer.Data;
using GameServer.Server;
using GameServer.Communication;
using Elysium.Logs;

namespace GameServer.Network.PacketList {
    public sealed class CpSaveNpcData : IRecvPacket {
        public void Process(byte[] buffer, IConnection connection) {
            var pData = Authentication.Players[connection.Index];

            if (pData.AccessLevel < AccessLevel.Administrator) {
                return;
            }

            var msg = new ByteBuffer(buffer);
            var npcId = msg.ReadInt32();

            if (npcId <= 0 || npcId > Configuration.MaxNpcs) {
                msg.Flush();
                return;
            }

            var npc = new Npc() {
                Name = msg.ReadString(),
                Sound = msg.ReadString(),
                Sprite = msg.ReadInt32(),
                SpawnSeconds = msg.ReadInt32(),
                Behaviour  = msg.ReadByte(),
                HP = msg.ReadInt32(),
                Animation = msg.ReadInt32(),
                Level = msg.ReadInt32(),
                Conversation = msg.ReadInt32()
            };

            DataManagement.NpcDatabase[npcId] = npc;
            DataManagement.NpcDatabase.SaveFile(npcId);

            Global.WriteLog(LogType.Game, $"Character: {pData.Character} {pData.AccessLevel.ToString()} saved npcId {npcId}", LogColor.Green);

            var updateNpc = new SpUpdateNpc();
            updateNpc.Build(npc);
            updateNpc.SendToAllBut(pData.Index);
        }
    }
}