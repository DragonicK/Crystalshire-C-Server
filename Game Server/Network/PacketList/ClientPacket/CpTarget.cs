using System;
using GameServer.Server;

namespace GameServer.Network.PacketList {
    public sealed class CpTarget : IRecvPacket {
        public void Process(byte[] buffer, IConnection connection) {
            var pData = Authentication.Players[connection.Index];
            var msg = new ByteBuffer(buffer);
            var target = msg.ReadInt32();
            var targetType = msg.ReadInt32();

            if (Enum.IsDefined(typeof(TargetType), targetType)) {
                var changeTarget = new PlayerTarget() {
                    Player = Authentication.Players[connection.Index]
                };

                changeTarget.ChangeTarget(target, (TargetType)targetType);
            }
        }
    }
}