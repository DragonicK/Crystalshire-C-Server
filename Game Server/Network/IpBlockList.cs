using System;
using System.Collections.Generic;

namespace GameServer.Network {
    /// <summary>
    /// Lista de endereços bloqueados pelo servidor.
    /// </summary>
    public static class IpBlockList {
        /// <summary>
        /// Tempo de vida de um endereço temporariamente bloqueado.
        /// </summary>
        public static int IpBlockTime { get; set; }

        public static bool Enabled { get; set; }

        /// <summary>
        /// Lista de endereços bloqueados.
        /// </summary>
        public static Dictionary<string, IpAddress> IpBlocked { get; set; }

        private static int tick;

        private const int FifteenSeconds = 15000;

        public static void Initialize() {
            const int OneMinute = 60000;
            const int FiveMinutes = 5;

            IpBlocked = new Dictionary<string, IpAddress>();

            if (IpBlockTime <= 0) {
                IpBlockTime = OneMinute * FiveMinutes;
            }
        }

        /// <summary>
        /// Adiciona um endereço à lista de bloqueio.
        /// </summary>
        /// <param name="ipAddress"></param>
        public static void AddIpAddress(string ipAddress, bool permanent) {
            int time = Environment.TickCount;
            IpBlocked.Add(ipAddress, new IpAddress(ipAddress, (permanent) ? 0 : time, permanent));
        }

        /// <summary>
        /// Verifica se um endereço está na lista de bloqueio.
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public static bool IsIpBlocked(string ipAddress) {
            return IpBlocked.ContainsKey(ipAddress);
        }

        /// <summary>
        /// Limpa todos os endereços bloqueados.
        /// </summary>
        public static void Clear() {
            IpBlocked.Clear();
        }

        /// <summary>
        /// Remove todos os endereços em que o tempo foi expirado.
        /// </summary>
        public static void RemoveExpiredIpAddress() {
            if (Environment.TickCount >= tick + FifteenSeconds) {

                var list = new List<IpAddress>();

                foreach (var blockedIpAddress in IpBlocked.Values) {
                    if (!blockedIpAddress.Permanent) {
                        if (Environment.TickCount >= blockedIpAddress.Time + IpBlockTime) {
                            list.Add(blockedIpAddress);
                        }
                    }
                }

                tick = Environment.TickCount;

                RemoveExpiredIpAddress(list);
            }
        }

        private static void RemoveExpiredIpAddress(List<IpAddress> list) {
            if (list.Count > 0) {

                foreach (var ipAddress in list) {
                    IpBlocked.Remove(ipAddress.Ip);
                }

                list.Clear();
            }
        }
    }
}