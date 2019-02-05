using System;
using System.Collections.Generic;
using GameServer.Data;
using GameServer.Server;
using GameServer.Server.Character;
using Elysium.Logs;

namespace GameServer.Communication {
    public class Global {
        public static Dictionary<int, MapInstance> Maps { get; set; }
        public static CharacterDeleteRequest DeleteRequest { get; set; }

        private static Log logConnection;
        private static Log logPlayer;
        private static Log logSystem;
        private static Log logGame;
        private static Log logChat;
           
        public static void OpenLog(EventHandler<LogEventArgs> eventHandler) {
            logSystem = new Log("System") {
                Enabled = true,
                Index = (int)LogType.System
            };

            logConnection = new Log("Connection") {
                Enabled = true,
                Index = (int)LogType.Connection
            };

            logPlayer = new Log("Player") {
                Enabled = true,
                Index = (int)LogType.Player
            };

            logChat = new Log("Chat") {
                Enabled = true,
                Index = (int)LogType.Chat
            };

            logGame = new Log("Game") {
                Enabled = true,
                Index = (int)LogType.Game
            };

            logSystem.LogEvent += eventHandler;
            logConnection.LogEvent += eventHandler;
            logPlayer.LogEvent += eventHandler;
            logChat.LogEvent += eventHandler;
            logGame.LogEvent += eventHandler;

            var msg = string.Empty;
            var success = logSystem.OpenFile(out msg);

            if (!success) {
                WriteLog(LogType.System, $"System Log: {msg}", LogColor.Red);
                logSystem.Enabled = false;
            }

            success = logConnection.OpenFile(out msg);

            if (!success) {
                WriteLog(LogType.System, $"Connection Log: {msg}", LogColor.Red);
                logConnection.Enabled = false;
            }

            success = logPlayer.OpenFile(out msg);

            if (!success) {
                WriteLog(LogType.System, $"Player Log: {msg}", LogColor.Red);
                logPlayer.Enabled = false;
            }

            success = logChat.OpenFile(out msg);

            if (!success) {
                WriteLog(LogType.System, $"Chat Log: {msg}", LogColor.Red);
                logChat.Enabled = false;
            }

            success = logGame.OpenFile(out msg);

            if (!success) {
                WriteLog(LogType.System, $"Chat Log: {msg}", LogColor.Red);
                logGame.Enabled = false;
            }
        }

        public static void CloseLog() {
            logSystem.CloseFile();
            logConnection.CloseFile();
            logPlayer.CloseFile();
            logGame.CloseFile();
            logChat.CloseFile();

            logSystem = null;
            logConnection = null;
            logPlayer = null;
            logGame = null;
            logChat = null;
        }

        public static void WriteLog(LogType type, string text, LogColor color) {
            switch (type) {
                case LogType.Player:
                    logPlayer.Write(text, color);
                    break;
                case LogType.System:
                    logSystem.Write(text, color);
                    break;
                case LogType.Game:
                    logGame.Write(text, color);
                    break;
                case LogType.Chat:
                    logChat.Write(text, color);
                    break;
                case LogType.Connection:
                    logConnection.Write(text, color);
                    break;
            }
        }
        
        public void DeleteCharacter(int accountId, int characterId) {
            var pData = Authentication.FindByAccountId(accountId);
            var close = false;

            // Somente é usado para enviar os personagens caso esteja online.
            var characters = new CharacterDatabase(pData);
            characters.Delete(characterId);

            if (pData != null) {
                // Carrega os personagens e envia somente se o jogador estiver conectado.
                if (pData.GameState == GameState.Characters && pData.Connected) {
                    // Por ser usado await, o próprio método deve fechar a conexão com o banco.                
                    characters.SendCharactersAsync();
                }
                else {
                    close = true;
                }
            }
            else {
                close = true;
            }

            // Fecha o banco quando os personagens não são enviados.
            if (close) {
                characters.Close();
            }
        }

        public static void InitializeMap(int mapId) {
            Maps[mapId] = new MapInstance(DataManagement.MapDatabase[mapId]);

            WriteLog(LogType.System, $"{Maps[mapId].MaxX} {Maps[mapId].MaxY}", LogColor.BlueViolet);

        }

        public void InitializeMaps() {
            Maps = new Dictionary<int, MapInstance>();

            for (var i = 1; i <= Configuration.MaxMaps; i++) {
                Maps.Add(i, new MapInstance(DataManagement.MapDatabase[i]));
            }
        }

        public static MapInstance GetMap(int mapId) {
            return Maps[mapId];
        }
    }
}