using System;
using System.Text;
using GameServer.Data;
using GameServer.Server;
using GameServer.Communication;
using GameServer.Database.MySql;

namespace GameServer.Database {
    public sealed class CharacterDB : DBTemplate {
        public CharacterDB() : base(new DBFactory()) {

        }

        public void LoadCharacter(ref Player pData) {
            var query = "SELECT * FROM CharacterData WHERE CharacterId=@CharacterId";
            var sqlCommand = factory.GetCommand(sqlConnection);

            sqlCommand.SetCommand(query);
            sqlCommand.AddParameter("@CharacterId", pData.CharacterId);

            var sqlReader = sqlCommand.ExecuteReader();

            if (sqlReader.Read()) {
                pData.Character = (string)sqlReader.GetData("Name");
                pData.ClassId = (int)sqlReader.GetData("Classe");
                pData.Gender = Convert.ToByte(sqlReader.GetData("Sex"));
                pData.Sprite = (int)sqlReader.GetData("Sprite");
                pData.Level = (int)sqlReader.GetData("Level");
                pData.Experience = (int)sqlReader.GetData("Experience");
                pData.MapId = (int)sqlReader.GetData("Map");
                pData.X = (int)sqlReader.GetData("X");
                pData.Y = (int)sqlReader.GetData("Y");
                pData.Direction = (Direction)((int)sqlReader.GetData("Dir"));

                LoadInventoryFromString(ref pData, (string)sqlReader.GetData("Inventory"));
            }

            sqlReader.Close();
        }

        public bool ExistCharacter(string name) {
            var query = "SELECT CharacterId FROM CharacterData WHERE Name=@Name";
            var sqlCommand = factory.GetCommand(sqlConnection);
            sqlCommand.SetCommand(query);
            sqlCommand.AddParameter("@Name", name);

            var sqlReader = sqlCommand.ExecuteReader();
            var result = sqlReader.Read();

            sqlReader.Close();
            return result;
        }

        public void CreateCharacter(int accountId, int characterIndex, GenderType genderType, int sprite, string name, Class _class) {
            var query = new StringBuilder();
            query.Append("INSERT INTO CharacterData (AccountID, CharacterIndex, Name, Classe, Sex, Sprite, ");
            query.Append("Level, Experience, Inventory, ");
            query.Append("Map, X, Y) ");
            query.Append("VALUES (@AccountID, @CharacterIndex, @Name, @Classe, @Sex, @Sprite, ");
            query.Append("@Level, @Experience, @Inventory, ");
            query.Append("@Map, @X, @Y)");

            var sqlCommand = factory.GetCommand(sqlConnection);
            sqlCommand.SetCommand(query.ToString());
            sqlCommand.AddParameter("@AccountID", accountId);
            sqlCommand.AddParameter("@CharacterIndex", characterIndex);
            sqlCommand.AddParameter("@Name", name);
            sqlCommand.AddParameter("@Classe", _class.Id);
            sqlCommand.AddParameter("@Sex", (int)genderType);
            sqlCommand.AddParameter("@Sprite", sprite);
            sqlCommand.AddParameter("@Level", _class.Level);
            sqlCommand.AddParameter("@Experience", _class.Experience);
            sqlCommand.AddParameter("@Inventory", GetEmptyInventoryString());
            sqlCommand.AddParameter("@Map", _class.MapId);
            sqlCommand.AddParameter("@X", _class.X);
            sqlCommand.AddParameter("@Y", _class.Y);

            sqlCommand.ExecuteNonQuery();
        }

        public void DeleteCharacter(int characterId) {
            var query = "DeleteCharacter";
            var sqlCommand = factory.GetCommand(sqlConnection);

            sqlCommand.SetCommand(query);
            sqlCommand.SetCommandType(DBCommandType.StoredProcedure);

            sqlCommand.AddParameter("@UserId", characterId);

            sqlCommand.ExecuteNonQuery();
        }

        public void UpdateRequestDelete(int characterId, byte pending, DateTime date) {
            var query = "UPDATE CharacterData SET PendingExclusion=@Pending, ExclusionDate=@Date WHERE CharacterId=@CharacterId";
            var sqlCommand = factory.GetCommand(sqlConnection);
            sqlCommand.SetCommand(query);
            sqlCommand.AddParameter("@Pending", pending);
            sqlCommand.AddParameter("@Date", date);
            sqlCommand.AddParameter("@CharacterId", characterId);
            sqlCommand.ExecuteNonQuery();
        }

        public void UpdateCharacter(Player pData) {
            var query = new StringBuilder();
            query.Append("UPDATE CharacterData SET Name=@Name, Classe=@Classe, Sex=@Sex, Sprite=@Sprite, Level=@Level, Experience=@Experience, ");
            query.Append("Inventory=@Inventory, Map=@Map, X=@X, Y=@y, Dir=@Dir, LastLogoutDate=@LastLogoutDate WHERE CharacterId=@CharacterId");

            var sqlCommand = factory.GetCommand(sqlConnection);

            sqlCommand.SetCommand(query.ToString());
            sqlCommand.AddParameter("@Name", pData.Character);
            sqlCommand.AddParameter("@Classe", pData.ClassId);
            sqlCommand.AddParameter("@Sex", pData.Gender);
            sqlCommand.AddParameter("@Sprite", pData.Sprite);
            sqlCommand.AddParameter("@Level", pData.Level);
            sqlCommand.AddParameter("@Experience", pData.Experience);
            sqlCommand.AddParameter("@Inventory", GetInventoryString(pData));
            sqlCommand.AddParameter("@Map", pData.MapId);
            sqlCommand.AddParameter("@X", pData.X);
            sqlCommand.AddParameter("@Y", pData.Y);
            sqlCommand.AddParameter("@Dir", pData.Direction);
            sqlCommand.AddParameter("@LastLogoutDate", DateTime.Now);
            sqlCommand.AddParameter("@CharacterId", pData.CharacterId);
            sqlCommand.ExecuteNonQuery();  
        }

        public void UpdateLastLoginDate(int characterId) {
            var query = "UPDATE CharacterData SET LastLoginDate=@LastLoginDate WHERE CharacterId=@CharacterId";
            var sqlCommand = factory.GetCommand(sqlConnection);
            sqlCommand.SetCommand(query);
            sqlCommand.AddParameter("@LastLoginDate", DateTime.Now);
            sqlCommand.AddParameter("@CharacterId", characterId);
            sqlCommand.ExecuteNonQuery();    
        }

        public void LoadCharacterSelection(ref Player pData) {
            var query = "SELECT CharacterId, CharacterIndex, Name, Classe, Sprite, Level, PendingExclusion, ExclusionDate FROM CharacterData WHERE AccountId=@AccountId";
            var sqlCommand = factory.GetCommand(sqlConnection);

            sqlCommand.SetCommand(query);
            sqlCommand.AddParameter("@AccountId", pData.AccountId);

            var sqlReader = sqlCommand.ExecuteReader();
  
            while (sqlReader.Read()) {
                var index = Convert.ToInt16(sqlReader.GetData("CharacterIndex"));

                if (index >= 1 && index <= Configuration.MaxCharacters) {
                    pData.CharacterSelection[index].CharacterId = (int)sqlReader.GetData("CharacterId");
                    pData.CharacterSelection[index].Name = (string)sqlReader.GetData("Name");
                    pData.CharacterSelection[index].Classe = Convert.ToInt16(sqlReader.GetData("Classe"));
                    pData.CharacterSelection[index].Sprite = (int)sqlReader.GetData("Sprite");
                    pData.CharacterSelection[index].Level = (int)sqlReader.GetData("Level");

                    if (!DBNull.Value.Equals(sqlReader.GetData("ExclusionDate"))) {
                        pData.CharacterSelection[index].ExclusionDate = Convert.ToDateTime(sqlReader.GetData("ExclusionDate"));
                    }

                    pData.CharacterSelection[index].PendingExclusion = Convert.ToBoolean(sqlReader.GetData("PendingExclusion"));
                }
            }

            sqlReader.Close();
        }

        private string GetInventoryString(Player pData) {
            var inventory = new StringBuilder();

            for (var i = 1; i < Configuration.MaxInventories; i++) {
                inventory.Append($"{pData.Inventory[i].Id},{pData.Inventory[i].Value},");
            }

            // Remove a última vírgula.
            return inventory.ToString().Remove(inventory.Length - 1, 1);
        }

        private string GetEmptyInventoryString() {
            var inventory = new StringBuilder();

            for (var i = 1; i < Configuration.MaxInventories; i++) {
                inventory.Append($"0,0,");
            }

            // Remove a última vírgula.
            return inventory.ToString().Remove(inventory.Length - 1, 1);
        }

        private void LoadInventoryFromString(ref Player pData, string inventory) {
            var values = inventory.Split(',');

            var itemIndex = 0;
            var valueIndex = 1;

            if (inventory.Length > 0) { 

                for (var i = 1; i < Configuration.MaxInventories; i++) {
                    pData.Inventory[i] = new Inventory() {
                        Id = Convert.ToInt32(values[itemIndex]),
                        Value = Convert.ToInt32(values[valueIndex])
                    };

                    itemIndex += 2;
                    valueIndex += 2;
                }
            }
        }
    }
}