using System;
using System.IO;
using System.Drawing;

namespace Elysium.Logs {
    public sealed class Log {
        /// <summary>
        /// Quando chamado o write, exporta o texto e a cor.
        /// </summary>
        public EventHandler<LogEventArgs> LogEvent;

        /// <summary>
        /// Ativa ou desativa os logs.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Indice do controle a user usado.
        /// </summary>
        public int Index { get; set; }

        string file = string.Empty;
        FileStream stream;
        StreamWriter writer;

        public Log(string name) {
            file = $"{name} {DateTime.Today.Year} - {DateTime.Today.Month} - {DateTime.Today.Day}.txt";
        }

        /// <summary>
        /// Abre o arquivo no modo de escrita.
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool OpenFile(out string msg) {
            try {
                if (!Directory.Exists("./Logs")) {
                    Directory.CreateDirectory("./Logs");
                }

                stream = new FileStream($"./Logs/{file}", FileMode.Append, FileAccess.Write);
                writer = new StreamWriter(stream);
            }
            catch (Exception ex) {
                msg = ex.Message;
                return false;
            }

            msg = string.Empty;
            return true;
        }

        /// <summary>
        /// Fecha o arquivo e libera os recursos.
        /// </summary>
        public void CloseFile() {
            if (stream != null) {
                writer.Close();
                stream.Close();

                writer.Dispose();
                stream.Dispose();
            }
        }

        /// <summary>
        /// Escreve no arquivo de logs.
        /// </summary>
        /// <param name="text"></param>
        private void Write(string text) {
            if (Enabled) {
                writer.WriteLine($"{DateTime.Now}: {text}");
                writer.Flush();
            }
        }

        /// <summary>
        /// Escreve a mensagem na tela e no arquivo.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="color"></param>
        public void Write(string text, LogColor color, bool saveinfile = true) {
            LogEvent?.Invoke(null, new LogEventArgs(text, GetColor(color), Index));

            if (saveinfile) {
                Write(text);
            }
        }

        private Color GetColor(LogColor color) {
            return Color.FromName(Enum.GetName(typeof(LogColor), color));
        }
    }
}