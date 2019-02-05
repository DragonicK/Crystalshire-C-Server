namespace LoginServer.Server {
    public enum AlertMessageType {
        None,
        Connection,
        Banned,
        Kicked,
        Outdated,
        StringLength,
        IllegalName,
        Maintenance,
        NameTaken,
        NameLength,
        NameIllegal,
        Database,
        WrongPassword,
        UserNotActivated
    }
}