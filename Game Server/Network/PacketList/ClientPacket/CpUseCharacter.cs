using GameServer.Server;
using GameServer.Server.Character;

namespace GameServer.Network.PacketList {
    public sealed class CpUseCharacter : IRecvPacket {
        public void Process(byte[] buffer, IConnection connection) {
            var msg = new ByteBuffer(buffer);
            var characterIndex = msg.ReadInt32();

            if (characterIndex > 0) {
                var characters = new CharacterStart() {
                    Player = Authentication.Players[connection.Index]
                };

                characters.Use(characterIndex);
            }
        }
    }
}