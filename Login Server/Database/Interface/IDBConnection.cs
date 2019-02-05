namespace LoginServer.Database {
    public interface IDBConnection {
        DBError Open();
        void Close();
        bool IsOpen();
    }
}