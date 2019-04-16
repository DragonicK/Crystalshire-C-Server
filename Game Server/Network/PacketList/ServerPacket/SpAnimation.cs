namespace GameServer.Network.PacketList {
    public sealed class SpAnimation : SendPacket {
        public void Build(int animationId, int x, int y, byte lockType = 0, int lockIndex = 0, byte isCasting = 0) {
            msg.Flush();
            msg.Write((int)OpCode.SendPacket[GetType()]);
            msg.Write(animationId);
            msg.Write(x);
            msg.Write(y);
            msg.Write(lockType);
            msg.Write(lockIndex);
            msg.Write(isCasting);
        }
    }
}