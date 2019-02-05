using GameServer.Server;

namespace GameServer.Network.PacketList {
    public sealed class CpPlayerMessage : IRecvPacket {
        public void Process(byte[] buffer, IConnection connection) {
            var msg = new ByteBuffer(buffer);
            var player = msg.ReadString();
            var text = msg.ReadString();

            if (text.Length > 0) {
                var message = new Message() {                  
                    Player = Authentication.Players[connection.Index]
                };

                message.Private(Authentication.FindByCharacter(player), text);
            }
        }
    }
}