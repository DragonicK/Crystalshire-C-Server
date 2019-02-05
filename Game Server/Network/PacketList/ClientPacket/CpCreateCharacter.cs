using GameServer.Server;
using GameServer.Server.Character;

namespace GameServer.Network.PacketList {
    public sealed class CpCreateCharacter : IRecvPacket {
        public void Process(byte[] buffer, IConnection connection) {
            var msg = new ByteBuffer(buffer);

            string name = msg.ReadString();
            int genderType = msg.ReadInt32();
            int classIndex = msg.ReadInt32();
            int spriteIndex = msg.ReadInt32();
            int characterIndex = msg.ReadInt32();

            if (name.Length > 0) {
                var creation = new CharacterCreation(name, characterIndex, classIndex, genderType, spriteIndex) {
                    Player = Authentication.Players[connection.Index]
                };

                creation.Create();
            }
        }
    }
}