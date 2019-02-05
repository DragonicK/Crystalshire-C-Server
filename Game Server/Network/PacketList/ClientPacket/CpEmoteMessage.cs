using GameServer.Server;

namespace GameServer.Network.PacketList {
    public sealed class CpEmoteMessage : IRecvPacket {
        public void Process(byte[] buffer, IConnection connection) {
            var msg = new ByteBuffer(buffer);
            var text = msg.ReadString().Trim();

            if (text.Length > 0) {
                var message = new Message() {
                    Player = Authentication.Players[connection.Index]
                };

                message.Emote(text);
            }
        }
    }
}