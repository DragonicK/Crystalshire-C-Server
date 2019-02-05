namespace GameServer.Network {
    public enum ServerPacket {
        None,
        /// <summary>
        /// Cabeçalho para ping.
        /// </summary>
        Ping,

        /// <summary>
        /// Exibe mensagem de dialogo no cliente.
        /// </summary>
        AlertMessage,

        /// <summary>
        /// Envia a chave unica para o cliente.
        /// </summary>
        LoginToken,

        /// <summary>
        /// Envia os personagens para o menu de seleção.
        /// </summary>
        Characters,

        /// <summary>
        /// Envia as classes para a tela de login.
        /// </summary>
        Classes,

        /// <summary>
        /// Indica que o login foi feito e envia o índice (index) do jogador e o maior indice.
        /// </summary>
        LoginOk,

        /// <summary>
        /// Indica que o personagem já está no jogo e inicia a renderização dos gráficos.
        /// </summary>
        InGame,

        /// <summary>
        /// Envia os dados do usuário.
        /// </summary>
        PlayerData,
        PlayerHP,
        PlayerMP,
        MapEditor,
        PlayerMove,
        PlayerXY,
        PlayerDirection,
        PlayerLeft,
        HighIndex,
        NpcEditor,
        UpdateNpc,
        MapNpcData,
        MapNpcDirection,
        MapNpcMove,
        MapMessage,
        PlayerMessage,
        BroadcastMessage,
        AdminMessage,
        ChatBubble,
        SayMessage,
        ActionMessage,
        LoadMap,
        ItemEditor,
        UpdateItem,
        Inventory,
        UpdateInventory,
        AnimationEditor,
        UpdateAnimation,
        Target,
        ConversationEditor,
        UpdateConversation,
        ChatUpdate,
        Sound,
        StartAnimation,
        CancelAnimation
    }
}