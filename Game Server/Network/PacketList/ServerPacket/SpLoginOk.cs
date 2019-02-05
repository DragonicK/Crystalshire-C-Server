namespace GameServer.Network.PacketList {
    public sealed class SpLoginOk : SendPacket {
        public SpLoginOk(int index, int highIndex) {
            msg.Write((int)OpCode.SendPacket[GetType()]);
            msg.Write(index);
            msg.Write(highIndex);
        }
    }
}