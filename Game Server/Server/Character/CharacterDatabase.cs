using System;
using System.Threading.Tasks;
using GameServer.Database;
using GameServer.Communication;
using GameServer.Network.PacketList;
using Elysium.Logs;

namespace GameServer.Server.Character {
    public sealed class CharacterDatabase {
        public int Result { get; set; }
 
        CharacterDB db;
        DBError dbError;
        Player pData;

        const int Failed = 0;
        const int Success = 1;

        ~CharacterDatabase() {
            if (db != null) {
                if (db.Connected) {
                    db.Close();
                }
            }
        }

        public CharacterDatabase(Player player) {
            pData = player;

            db = new CharacterDB();
            dbError = db.Open();

            if (dbError.Number != 0) {
                Global.WriteLog(LogType.System, $"Cannot connect to database", LogColor.Red);
                Global.WriteLog(LogType.System, $"Error Number: {dbError.Number}", LogColor.Red);
                Global.WriteLog(LogType.System, $"Error Message: {dbError.Message}", LogColor.Red);
            }
        }

        /// <summary>
        /// Fecha a conexão com o banco de dados.
        /// </summary>
        public void Close() {
            db.Close();
        }

        public bool Exist(string character) {
            var result = false;

            if (db.Connected) {
                result = db.ExistCharacter(character);
            }

            return result;
        }

        public void Create(CharacterValidation validation) {
            if (db.Connected) {

                db.CreateCharacter(
                    pData.AccountId,
                    validation.CharacterIndex,  
                    validation.GenderType,
                    validation.SpriteId, 
                    validation.CharacterName, 
                    validation.CharacterClass
                    );

                return;
            }
        }

        public void Delete(int characterId) {
            if (db.Connected) {
                db.DeleteCharacter(characterId);
            }
        } 

        /// <summary>
        /// Atualiza a requisição da exclusão do personagem.
        /// </summary>
        /// <param name="characterId"></param>
        /// <param name="pending"></param>
        /// <param name="date"></param>
        public void UpdateRequestDelete(int characterId, bool pending, DateTime date) {
            var value = Convert.ToByte(pending);

            if (db.Connected) {
                db.UpdateRequestDelete(characterId, value, date);
            }
        }

        public async void SendCharactersAsync() {
            var result = await Task.Run( () =>
               LoadCharacterSelection()
            );

            // Se obteve sucesso na leitura.
            // Cria o pacote e envia os personagens.
            if (result > 0) {
                var msg = new SpCharacterSelection(pData);
                msg.Send(pData.Connection);
            }

            // Salva o resultado em uma variavel da classe.
            Result = result;

            db.Close();
        }

        /// <summary>
        /// Carrega os dados dos personagens para exibição.
        /// </summary>
        /// <returns></returns>
        private int LoadCharacterSelection() {
            if (db.Connected) {
                db.LoadCharacterSelection(ref pData);

                // Deleta personagens que precisam de atenção.
                DeletePendingCharacters();
  
                return Success;
            }

            return Failed;
        }

        /// <summary>
        /// Carrega os dados de um personagem.
        /// </summary>
        public bool LoadCharacter(bool cancelDeleteRequest) {
            var result = false;

            if (pData != null) {
                if (db.Connected) {
                    db.LoadCharacter(ref pData);
                    db.UpdateLastLoginDate(pData.CharacterId);

                    if (cancelDeleteRequest) {
                        db.UpdateRequestDelete(pData.CharacterId, 0, DateTime.Now);
                    }

                    result = true;
                }        
            }
 
            return result;
        }

        /// <summary>
        /// Verifica se há personagens com exclusão pendente e realiza a exclusão.
        /// </summary>
        /// <returns></returns>
        private void DeletePendingCharacters() {
            CharacterSelection character;

            for(var n = 1; n <= Configuration.MaxCharacters; n++) {
                character = pData.CharacterSelection[n];

                if (character.PendingExclusion) {
                    if (IsExpired(character.ExclusionDate) == Expired.Yes) {
                        Delete(character.CharacterId);

                        // Limpa os dados do personagem.
                        character.Clear();
                    }
                    else {
                        Global.DeleteRequest.AddCharacter(pData.AccountId, character.CharacterId, character.ExclusionDate);
                    }
                }
            }
        }

        private Expired IsExpired(DateTime date) {
            return (DateTime.Now.CompareTo(date) == (int)Expired.Yes) ? Expired.Yes : Expired.No;
        }
    }
}