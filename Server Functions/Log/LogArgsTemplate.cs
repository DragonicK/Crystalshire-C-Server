using System;

namespace Elysium.Logs {
    public abstract class LogArgsTemplate {
        /// <summary>
        /// Quando chamado o write, exporta o texto e a cor.
        /// </summary>
        public EventHandler<LogEventArgs> Event;

        bool Enabled { get; set; }

        /// <summary>
        /// Indice do controle a user usado.
        /// </summary>
        int Index { get; set; }
    }
}