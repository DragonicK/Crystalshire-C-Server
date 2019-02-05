using GameServer.Server;

namespace GameServer.Network.PacketList {
    public sealed class CpUserData : IRecvPacket {
        public void Process(byte[] buffer, IConnection connection) {
            var msg = new ByteBuffer(buffer);

            WaitingUserData.Add(
                new WaitingUserData() {
                    AccountId = msg.ReadInt32(),
                    Username = msg.ReadString(),
                    UniqueKey = msg.ReadString(),
                    Cash = msg.ReadInt32(),
                    AccessLevel = (AccessLevel)msg.ReadInt32()
                }
            );
        }
    }
}