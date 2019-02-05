using GameServer.Communication;
using GameServer.Network.PacketList;
using Elysium.Logs;

namespace GameServer.Server {
    public sealed class Message {
        public Player Player { get; set; }

        public void Say(string text) {
            var msg = new SpSayMessage();
            msg.Build(Player.Character, Player.AccessLevel, text, "[Map] ", QBColor.White);
            msg.SendToMap(Player.MapId);

            var bubble = new SpChatBubble(Player.Index, TargetType.Player, text, TextColor.White);
            bubble.SendToMap(Player.MapId);

            Global.WriteLog(LogType.Chat, $"{Player.Character} says: {text}", LogColor.Black);
        }

        public void Private(Player player, string text) {
            var msg = new SpPlayerMessage();

            if (player != null) {
                if (player.Index == Player.Index) {
                    return;
                }

                msg.Build($"<< {Player.Character}: {text}", TextColor.Pink);
                msg.Send(player.Connection);

                msg.Build($">> {Player.Character}: {text}", TextColor.Pink);
                msg.Send(Player.Connection);

                Global.WriteLog(LogType.Chat, $"{Player.Character} tells {player.Character}: {text}", LogColor.HotPink);
            }
            else {
                msg.Build("Player is not online", TextColor.Pink);
                msg.Send(Player.Connection);
            }
        }

        public void Emote(string message) {
            var msg = new SpMapMessage(message, TextColor.BrigthCyan);
            msg.SendToMap(Player.MapId);

            Global.WriteLog(LogType.Chat, $"{Player.Character} emote: {message}", LogColor.Black);
        }  

        public void Broadcast(string message) {
            var text = $"[Global] {Player.Character}: {message}";

            var msg = new SpSayMessage();
            msg.Build(Player.Character, Player.AccessLevel, message, "[Global] ", QBColor.Blue);
            msg.SendToAll();

            Global.WriteLog(LogType.Chat, text, LogColor.Blue);
        }
    }
}