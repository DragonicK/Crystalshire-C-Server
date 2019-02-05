using GameServer.Server;

namespace GameServer.Network.PacketList {
    public sealed class SpPlayerData : SendPacket {
        public SpPlayerData(Player player) {
            msg.Write((int)OpCode.SendPacket[GetType()]);
            msg.Write(player.Index);
            msg.Write(player.Character);
            msg.Write((int)player.AccessLevel);
            msg.Write(player.ClassId);
            msg.Write(player.Sprite);
            msg.Write(player.Level);
            msg.Write(player.Points);
            msg.Write(player.MapId);
            msg.Write(player.X);
            msg.Write(player.Y);
            msg.Write((int)player.Direction);
        }
    }
}