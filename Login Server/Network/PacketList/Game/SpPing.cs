
namespace LoginServer.Network.PacketList.Game {
    public sealed class SpPing {
        public ByteBuffer Build() {
            var msg = new ByteBuffer();
            msg.Write(OpCode.SendPacket[GetType()]);

            return msg;
        }
    }
}