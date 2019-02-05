using GameServer.Data;
using GameServer.Communication;
using GameServer.Network.PacketList;
using Elysium.Logs;

namespace GameServer.Server {
    public sealed class JoinGame {
        public Player Player { get; set; }

        public void Join() {
            var loginOk = new SpLoginOk(Player.Index, Authentication.HighIndex);
            loginOk.Send(Player.Connection);

            Animation animation;
            var animations = new SpUpdateAnimation();
            for (var i = 1; i <= Configuration.MaxAnimations; i++) {
                if (DataManagement.AnimationDatabase[i].Name.Length > 0) {
                    animation = DataManagement.AnimationDatabase[i];

                    animations.Build(animation);
                    animations.Send(Player.Connection);
                }
            }

            Npc npc;
            var npcs = new SpUpdateNpc();
            for (var i = 1; i <= Configuration.MaxNpcs; i++) {
                if (DataManagement.NpcDatabase[i].Name.Length > 0) {
                    npc = DataManagement.NpcDatabase[i];

                    npcs.Build(npc);
                    npcs.Send(Player.Connection);
                }
            }

            Item item;
            var items = new SpUpdateItem();
            for (var i = 1; i <= Configuration.MaxItems; i++) {
                if (DataManagement.ItemDatabase[i].Name.Length > 0) {
                    item = DataManagement.ItemDatabase[i];

                    items.Build(item);
                    items.Send(Player.Connection);
                }
            }

            Conversation conversation;
            var updateConversations = new SpUpdateConversation();
            for (var i = 1; i <= Configuration.MaxConversations; i++) {
                if (DataManagement.ConversationDatabase[i].Name.Length > 0) {
                    conversation = DataManagement.ConversationDatabase[i];

                    updateConversations.Build(conversation);
                    updateConversations.Send(Player.Connection);
                }
            }

            var playerData = new SpPlayerData(Player);
            playerData.Send(Player.Connection);

            var hp = new SpPlayerHP(Player);
            hp.Send(Player.Connection);

            var mp = new SpPlayerMP(Player);
            mp.Send(Player.Connection);

            var inventories = new SpInventory(Player);
            inventories.Send(Player.Connection);

            var movements = new PlayerMovement() {
                Player = Player
            };

            movements.Warp(Player.GetMap(), Player.X, Player.Y);

            var inGame = new SpInGame();
            inGame.Send(Player.Connection);

            Global.WriteLog(LogType.Game, $"{Player.Character} has joined", LogColor.Green);
        }
    }
}