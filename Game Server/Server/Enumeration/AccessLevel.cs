namespace GameServer.Server {
    /// <summary>
    /// Nível de acesso do usuário ao sistema.
    /// </summary>
    public enum AccessLevel {
        Restrict,
        Normal,
        Monitor,
        GameMaster,
        Administrator
    }
}