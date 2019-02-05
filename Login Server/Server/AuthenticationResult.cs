namespace LoginServer.Server {
    public enum AuthenticationResult {
        None,
        Success,
        Error,
        Maintenance,
        WrongUserData,
        AccountIsNotActivated,
        AccountIsBanned,
        VersionOutdated,
        StringLength
    }
}