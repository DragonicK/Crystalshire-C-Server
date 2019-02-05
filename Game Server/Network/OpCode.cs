using System;
using System.Collections.Generic;
using GameServer.Network.PacketList;

namespace GameServer.Network {
    public static class OpCode {
        public static Dictionary<ClientPacket, Type> RecvPacket = new Dictionary<ClientPacket, Type>();
        public static Dictionary<Type, ServerPacket> SendPacket = new Dictionary<Type, ServerPacket>();

        public static void Initialize() {
            RecvPacket = new Dictionary<ClientPacket, Type> {
                { ClientPacket.Ping, typeof(CpPing) },
                // Recebe os dados do servidor de login.
                { ClientPacket.UserData, typeof(CpUserData) },
                { ClientPacket.AuthLogin, typeof(CpAuthenticateLogin) },
                { ClientPacket.DeleteCharacter, typeof(CpDeleteCharacter) },
                { ClientPacket.CreateCharacter, typeof(CpCreateCharacter) },
                { ClientPacket.UseCharacter, typeof(CpUseCharacter) },
                { ClientPacket.RequestEditMap, typeof(CpRequestEditMap) },
                { ClientPacket.SaveMapData, typeof(CpSaveMapData) },
                { ClientPacket.PlayerMove, typeof(CpPlayerMove) },
                { ClientPacket.RequestEditNpc, typeof(CpRequestEditNpc) },
                { ClientPacket.SaveNpcData, typeof(CpSaveNpcData) },
                { ClientPacket.RequestNpcs, typeof(CpRequestNpcs) },
                { ClientPacket.SayMessage, typeof(CpSayMessage) },
                { ClientPacket.PlayerMessage, typeof(CpPlayerMessage) },
                { ClientPacket.EmoteMessage, typeof(CpEmoteMessage) },
                { ClientPacket.BroadcastMessage, typeof(CpBroadcastMessage) },
                { ClientPacket.PlayerDirection, typeof(CpPlayerDirection) },
                { ClientPacket.AdminWarp, typeof(CpAdminWarp) },
                { ClientPacket.WarpMeTo, typeof(CpWarpMeTo) },
                { ClientPacket.WarpToMe, typeof(CpWarpToMe) },
                { ClientPacket.WarpTo, typeof(CpWarpTo) },
                { ClientPacket.QuitGame, typeof(CpQuitGame) },
                { ClientPacket.RequestNewMap, typeof(CpRequestNewMap) },
                { ClientPacket.Target, typeof(CpTarget) },
                { ClientPacket.RequestEditItem, typeof(CpRequestEditItem) },
                { ClientPacket.SaveItem, typeof(CpSaveItem) },
                { ClientPacket.RequestItems, typeof(CpRequestItems) },
                { ClientPacket.SwapInventory, typeof(CpSwapInventory) },
                { ClientPacket.UseItem, typeof(CpUseItem) },
                { ClientPacket.KickPlayer, typeof(CpKickPlayer) },
                { ClientPacket.MapRespawn, typeof(CpMapRespawn) },
                { ClientPacket.RequestEditAnimation, typeof(CpRequestEditAnimation) },
                { ClientPacket.SaveAnimation, typeof(CpSaveAnimation) },
                { ClientPacket.RequestAnimation, typeof(CpRequestAnimation) },
                { ClientPacket.RequestEditConversation, typeof(CpRequestEditConversation) },
                { ClientPacket.SaveConversation, typeof(CpSaveConversation) },
                { ClientPacket.RequestConversation, typeof(CpRequestConversation) },
                { ClientPacket.ChatOption, typeof(CpChatOption) }
            };

            SendPacket = new Dictionary<Type, ServerPacket>() {
                { typeof(SpPing), ServerPacket.Ping },
                { typeof(SpAlertMessage), ServerPacket.AlertMessage },
                { typeof(SpCharacterSelection), ServerPacket.Characters },
                { typeof(SpClasses), ServerPacket.Classes },
                { typeof(SpLoginOk), ServerPacket.LoginOk },
                { typeof(SpInGame), ServerPacket.InGame },
                { typeof(SpPlayerData), ServerPacket.PlayerData },
                { typeof(SpPlayerHP), ServerPacket.PlayerHP },
                { typeof(SpPlayerMP), ServerPacket.PlayerMP },
                { typeof(SpMapEditor), ServerPacket.MapEditor },
                { typeof(SpPlayerMove), ServerPacket.PlayerMove },
                { typeof(SpPlayerXY), ServerPacket.PlayerXY },
                { typeof(SpPlayerDirection), ServerPacket.PlayerDirection },
                { typeof(SpPlayerLeft), ServerPacket.PlayerLeft },
                { typeof(SpHighIndex), ServerPacket.HighIndex },
                { typeof(SpNpcEditor), ServerPacket.NpcEditor },
                { typeof(SpUpdateNpc), ServerPacket.UpdateNpc },
                { typeof(SpMapNpcData), ServerPacket.MapNpcData },
                { typeof(SpMapNpcDirection), ServerPacket.MapNpcDirection },
                { typeof(SpMapNpcMove), ServerPacket.MapNpcMove },
                { typeof(SpMapMessage), ServerPacket.MapMessage },
                { typeof(SpPlayerMessage), ServerPacket.PlayerMessage },
                { typeof(SpBroadcastMessage), ServerPacket.BroadcastMessage },
                { typeof(SpChatBubble), ServerPacket.ChatBubble },
                { typeof(SpSayMessage), ServerPacket.SayMessage },
                { typeof(SpActionMessage), ServerPacket.ActionMessage },
                { typeof(SpLoadMap), ServerPacket.LoadMap },
                { typeof(SpUpdateItem), ServerPacket.UpdateItem },
                { typeof(SpItemEditor), ServerPacket.ItemEditor },
                { typeof(SpInventory), ServerPacket.Inventory },
                { typeof(SpUpdateInventory), ServerPacket.UpdateInventory },
                { typeof(SpAnimationEditor), ServerPacket.AnimationEditor },
                { typeof(SpUpdateAnimation), ServerPacket.UpdateAnimation },
                { typeof(SpTarget), ServerPacket.Target },
                { typeof(SpConversationEditor), ServerPacket.ConversationEditor },
                { typeof(SpUpdateConversation), ServerPacket.UpdateConversation },
                { typeof(SpChatUpdate), ServerPacket.ChatUpdate },
                { typeof(SpSound), ServerPacket.Sound },
                { typeof(SpAnimation), ServerPacket.StartAnimation },
                { typeof(SpCancelAnimation), ServerPacket.CancelAnimation }
            };
        }
     }
}