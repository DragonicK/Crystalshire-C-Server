namespace GameServer.Network {
    public enum ClientPacket {
        None,
        /// <summary>
        /// Cabeçalho para ping.
        /// </summary>
        Ping,

        /// <summary>
        /// Não usado.
        /// O pacote de número 2 pertence apenas ao servidor de login.
        /// </summary>
        NoOperation,

        /// <summary>
        /// Realiza o login do usuário no game server.
        /// </summary>
        AuthLogin,

        /// <summary>
        /// Realiza a exclusão de um personagem.
        /// </summary>
        DeleteCharacter,

        /// <summary>
        /// Adiciona um novo personagem.
        /// </summary>
        CreateCharacter,

        /// <summary>
        /// Entra com o personagem no jogo.
        /// </summary>
        UseCharacter,

        RequestEditMap,
        SaveMapData,
        PlayerMove,
        RequestEditNpc,
        SaveNpcData,
        RequestNpcs,
        SayMessage,
        PlayerMessage,
        EmoteMessage,
        BroadcastMessage,
        PlayerDirection,
        AdminWarp,
        WarpMeTo,
        WarpToMe,
        WarpTo,
        QuitGame,
        RequestNewMap,
        Target,
        RequestEditItem,
        SaveItem,
        RequestItems,
        SwapInventory,
        UseItem,
        KickPlayer,
        MapRespawn,
        RequestEditAnimation,
        SaveAnimation,
        RequestAnimation,
        RequestEditConversation,
        SaveConversation,
        RequestConversation,
        ChatOption,

        /// <summary>
        /// Recebe os dados do login do usuário a partir do login server.
        /// </summary>
        UserData = 255
    }
}