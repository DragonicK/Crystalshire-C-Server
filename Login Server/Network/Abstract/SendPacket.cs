namespace LoginServer.Network {
    public abstract class SendPacket {
        protected ByteBuffer msg;

        public SendPacket() {
            msg = new ByteBuffer();
        }

        ~SendPacket() {
            msg.Flush();
            msg = null;
        }

        public void Send(IConnection connection) {
            ((Connection)connection).Send(msg, GetType().Name);
        }
    }
}