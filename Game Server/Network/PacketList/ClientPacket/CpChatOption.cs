using GameServer.Server;

namespace GameServer.Network.PacketList {
    public sealed class CpChatOption : IRecvPacket {
        public void Process(byte[] buffer, IConnection connection) {
            var msg = new ByteBuffer(buffer);
            var option = msg.ReadInt32();

            if (option > 0) {
                var chat = new NpcConversation() {
                    Player = Authentication.Players[connection.Index]
                };

                chat.SelectOption(option);
            }
        }
    }
}