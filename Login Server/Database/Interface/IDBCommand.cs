namespace LoginServer.Database {
    public interface IDBCommand {
        void AddParameter(string parameter, object value);
        void SetCommand(string commandText);
        int ExecuteNonQuery();
        IDBDataReader ExecuteReader();
    }
}