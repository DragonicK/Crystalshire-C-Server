using GameServer.Data;
using GameServer.Server;
using GameServer.Communication;
using Elysium.Logs;

namespace GameServer.Network.PacketList {
    public sealed class CpSaveAnimation : IRecvPacket {
        public void Process(byte[] buffer, IConnection connection) {
            var pData = Authentication.Players[connection.Index];

            if (pData.AccessLevel < AccessLevel.Administrator) {
                return;
            }

            var msg = new ByteBuffer(buffer);
            var animationId = msg.ReadInt32();

            if (animationId <= 0 || animationId > Configuration.MaxAnimations) {
                msg.Clear();
                return;
            }

            var animation = new Animation() {
                Id = animationId,
                Name = msg.ReadString(),
                Sound = msg.ReadString(), 
            };

            for (var i = 0; i < Configuration.MaxAnimationLayer; i++) {
                animation.Sprite[i] = msg.ReadInt32();
                animation.Frames[i] = msg.ReadInt32();
                animation.LoopCount[i] = msg.ReadInt32();
                animation.LoopTime[i] = msg.ReadInt32();
            }

            DataManagement.AnimationDatabase[animationId] = animation;
            DataManagement.AnimationDatabase.SaveFile(animationId);

            var updateAnimations = new SpUpdateAnimation();
            updateAnimations.Build(animation);
            updateAnimations.SendToAllBut(pData.Index);

            Global.WriteLog(LogType.Game, $"Character: {pData.Character} {pData.AccessLevel.ToString()} saved animationId {animationId}", LogColor.Green);
        }
    }
}