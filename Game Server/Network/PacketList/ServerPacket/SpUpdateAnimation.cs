using GameServer.Data;
using GameServer.Communication;

namespace GameServer.Network.PacketList {
    public sealed class SpUpdateAnimation : SendPacket {
        public void Build(Animation animation) {
            msg.Clear();
            msg.Write((int)OpCode.SendPacket[GetType()]);
            msg.Write(animation.Id);
            msg.Write(animation.Name);
            msg.Write(animation.Sound);

            for (var i = 0; i < Configuration.MaxAnimationLayer; i++) {
                msg.Write(animation.Sprite[i]);
                msg.Write(animation.Frames[i]);
                msg.Write(animation.LoopCount[i]);
                msg.Write(animation.LoopTime[i]);
            }
        }
    }
}