using GameServer.Data;
using GameServer.Communication;

namespace GameServer.Network.PacketList {
    public sealed class CpRequestAnimation : IRecvPacket {
        public void Process(byte[] buffer, IConnection connection) {
            Animation animation;

            var animations = new SpUpdateAnimation();
            for (var i = 1; i <= Configuration.MaxItems; i++) {
                if (DataManagement.AnimationDatabase[i].Name.Length > 0) {
                    animation = DataManagement.AnimationDatabase[i];

                    animations.Build(animation);
                    animations.Send(connection);
                }
            }
        }
    }
}