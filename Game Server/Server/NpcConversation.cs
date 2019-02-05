using GameServer.Data;
using GameServer.Communication;
using GameServer.Network.PacketList;

namespace GameServer.Server {
    public sealed class NpcConversation {
        public Player Player { get; set; }

        public void Start() {
            var target = Player.Target;

            if (target < 1 || target > Configuration.MaxMapNpcs) {
                return;
            }

            var npcs = Player.GetMap().Npcs;
            if (npcs[target].Id < 1 || npcs[target].Id > Configuration.MaxNpcs) {
                return;
            }

            int npcX = 0;
            int npcY = 0;

            switch (Player.Direction) {
                case (byte)Direction.Up:
                    npcX = npcs[target].X;
                    npcY = npcs[target].Y + 1;
                    break;
                case Direction.Down:
                    npcX = npcs[target].X;
                    npcY = npcs[target].Y - 1;
                    break;
                case Direction.Left:
                    npcX = npcs[target].X + 1;
                    npcY = npcs[target].Y;
                    break;
                case Direction.Right:
                    npcX = npcs[target].X - 1;
                    npcY = npcs[target].Y;
                    break;
            }

            // Se não estiver na posição correta.
            if (Player.X != npcX && Player.Y != npcY) {
                return;
            }

            // Obtem o id da conversação do npc.
            var conversationId = DataManagement.NpcDatabase[npcs[target].Id].Conversation;
            if (conversationId <= 0) {
                return;
            }

            var conversation = DataManagement.ConversationDatabase[conversationId];
            if (conversation.Name.Length <= 0) {
                return;
            }

            npcs[target].InChatWith = Player.Index;
            npcs[target].LastDir = npcs[target].Direction;

            if (Player.Y == npcs[target].Y - 1) {
                npcs[target].Direction = Direction.Up;
            }
            else if (Player.Y == npcs[target].Y + 1) {
                npcs[target].Direction = Direction.Down;
            }
            else if (Player.X == npcs[target].X - 1) {
                npcs[target].Direction = Direction.Left;
            }
            else if (Player.X == npcs[target].X + 1) {
                npcs[target].Direction = Direction.Right;
            }

            var npcDirection = new SpMapNpcDirection(target, npcs[target].Direction);
            npcDirection.SendToMap(Player.MapId);

            Player.ChatNpcId = npcs[target].Id;
            Player.ChatMapNpcIndex = target;
            Player.CurrentChat = 1;

            Send();
        }

        public void SelectOption(int option) {
            if (option < 0 || option >= Configuration.MaxConversationOptions) {
                return;
            }

            var npcId = Player.ChatNpcId;
            var exitChat = false;

            if (npcId == 0) {
                return;
            }

            var npc = DataManagement.NpcDatabase[npcId];
            var conversation = DataManagement.ConversationDatabase[npc.Conversation];

            if (conversation.Chats[Player.CurrentChat].RTarget[option] == 0) {
                exitChat = true;
            }
            else {
                Player.CurrentChat = conversation.Chats[Player.CurrentChat].RTarget[option];
            }

            if (exitChat) {
                Player.ChatNpcId = 0;
                Player.CurrentChat = 0;

                Send();

                var mapNpc = Player.GetMap().Npcs[Player.ChatMapNpcIndex];
                if (mapNpc.InChatWith == Player.Index) {
                    mapNpc.InChatWith = 0;
                    mapNpc.Direction = mapNpc.LastDir;

                    var npcDirection = new SpMapNpcDirection(Player.ChatMapNpcIndex, mapNpc.Direction);
                    npcDirection.SendToMap(Player.MapId);
                }

                Player.ChatMapNpcIndex = 0;
            }
            else {
                Send();
            }
        }

        public void Send() {
            var conversation = new Conversation();

            if (Player.ChatNpcId > 0) {
                var npc = DataManagement.NpcDatabase[Player.ChatNpcId];
                conversation = DataManagement.ConversationDatabase[npc.Conversation];

                if (conversation.Chats[Player.CurrentChat].Event > 0) {
                    switch (conversation.Chats[Player.CurrentChat].Event) {
                        case 1:
                            Close();
                            return;
                        case 2:
                            Close(); 
                            return;
                        case 3:
                            Close();
                            return;
                    }
                }
            }

            var chatUpdate = new SpChatUpdate(Player.ChatNpcId, Player.CurrentChat, conversation);
            chatUpdate.Send(Player.Connection);
        }

        public void Close() {
            Player.ChatNpcId = 0;
            Player.CurrentChat = 0;

            Send();

            var mapNpc = Player.GetMap().Npcs[Player.ChatMapNpcIndex];
            if (mapNpc.InChatWith == Player.Index) {
                mapNpc.InChatWith = 0;
                mapNpc.Direction = mapNpc.LastDir;

                var npcDirection = new SpMapNpcDirection(Player.ChatMapNpcIndex, mapNpc.Direction);
                npcDirection.SendToMap(Player.MapId);
            }

            Player.ChatMapNpcIndex = 0;
        }
    }
}