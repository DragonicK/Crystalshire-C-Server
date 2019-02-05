using LoginServer.Communication;

namespace LoginServer.Network.PacketList.Game{
    public sealed class SpSendUserData {
        public int AccountId { get; set; }
        public string Username { get; set; }
        public string UniqueKey { get; set; }
        public int AccessLevel { get; set; }
        public int Cash { get; set; }

        public void Send() {
            var msg = new ByteBuffer();

            msg.Write(OpCode.SendPacket[GetType()]);
            msg.Write(AccountId);
            msg.Write(Username);
            msg.Write(UniqueKey);
            msg.Write(Cash);
            msg.Write(AccessLevel);

            Global.SendGameServerPacket?.Invoke(msg);
        }
    }
}