using System;
using LoginServer.Network;
using Elysium.Logs;

namespace LoginServer.Communication {
    public class Global {
        public static Action<ByteBuffer> SendGameServerPacket { get; set; }

        private static Log logSystem;
        private static Log logConnection;
        private static Log logUser;

        public static void OpenLog(EventHandler<LogEventArgs> eventHandler) {
            logSystem = new Log("System") {
                Enabled = true,
                Index = (int)LogType.System
            };

            logConnection = new Log("Connection") {
                Enabled = true,
                Index = (int)LogType.Connection
            };

            logUser = new Log("User") {
                Enabled = true,
                Index = (int)LogType.User
            };

            logSystem.LogEvent += eventHandler;
            logConnection.LogEvent += eventHandler;
            logUser.LogEvent += eventHandler;

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

            success = logUser.OpenFile(out msg);

            if (!success) {
                WriteLog(LogType.System, $"User Log: {msg}", LogColor.Red);
                logUser.Enabled = false;
            }
        }

        public static void CloseLog() {
            logSystem.CloseFile();
            logConnection.CloseFile();
            logUser.CloseFile();

            logSystem = null;
            logConnection = null;
            logUser = null;
        }

        public static void WriteLog(LogType index, string text, LogColor color) {
            switch (index) {
                case LogType.System:
                    logSystem.Write(text, color);
                    break;
                case LogType.Connection:
                    logConnection.Write(text, color);
                    break;
                case LogType.User:
                    logUser.Write(text, color);
                    break;
            }
        }
    }
}