using GameServer.Network.PacketList;

namespace GameServer.Server {
    public sealed class PlayerTarget {
        public Player Player { get; set; }

        public void ChangeTarget(int target, TargetType targetType) {
            Player.Target = target;
            Player.TargetType = targetType;

            if (Player.TargetType == TargetType.Npc) {
                StartConversation();
            }
        }

        public void ClearTarget() {
            Player.Target = 0;
            Player.TargetType = TargetType.None;

            SendTarget();
        }

        public void SendTarget() {
            var target = new SpTarget(Player.Target, TargetType.None);
            target.Send(Player.Connection);

            if (Player.ChatNpcId > 0) {
                var chat = new NpcConversation() {
                    Player = Player
                };

                chat.Close();
            }
        }

        private void StartConversation() {
            var chat = new NpcConversation() {
                Player = Player
            };

            chat.Start();
        }
    }
}