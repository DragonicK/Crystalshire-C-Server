using GameServer.Data;

namespace GameServer.Network.PacketList {
    public sealed class SpUpdateNpc : SendPacket{
        public void Build(Npc npc) {
            msg.Flush();
            msg.Write((int)OpCode.SendPacket[GetType()]);
            msg.Write(npc.Id);
            msg.Write(npc.Name);
            msg.Write(npc.Sound);
            msg.Write(npc.Sprite);
            msg.Write(npc.SpawnSeconds);
            msg.Write(npc.Behaviour);
            msg.Write(npc.HP);
            msg.Write(npc.Animation);
            msg.Write(npc.Level);
            msg.Write(npc.Conversation);
        }
    }
}