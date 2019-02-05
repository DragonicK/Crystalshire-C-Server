namespace GameServer.Database {
    public interface IDBDataReader {
        void Close();
        bool Read();
        object GetData(string column);
    }
}