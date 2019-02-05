using System;
using GameServer.Server;
using GameServer.Server.Character;

namespace GameServer.Network.PacketList {
    public sealed class CpDeleteCharacter : IRecvPacket {
        public void Process(byte[] buffer, IConnection connection) {
            var msg = new ByteBuffer(buffer);
            var characterIndex = msg.ReadInt32();

            var deletion = new CharacterDeletion(characterIndex) {
                Player = Authentication.Players[connection.Index]
            };

            deletion.Delete();
        }
    }
}