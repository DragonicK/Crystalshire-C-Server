using System;
using System.Collections.Generic;

namespace GameServer.Network {
    /// <summary>
    /// Bloqueia um número alto de requisições feitos pelo mesmo endereço IP.
    /// </summary>
    public sealed class IpFiltering {
        /// <summary>
        /// Tempo de verificação de cada novo acesso de ip.
        /// </summary>
        public int CheckAccessTime { get; set; }

        /// <summary>
        /// Quantidade máxima de tentativas de acesso de cada ip a cada intervalo de tempo.
        /// </summary>
        public int IpMaxAttempt { get; set; }

        /// <summary>
        /// Quantidade máxima de acesso pelo mesmo ip.
        /// </summary>
        public int IpMaxAccessCount { get; set; }

        /// <summary>
        /// Tempo de vida do ip bloqueado no sistema para consulta.
        /// </summary>
        public int IpLifetime { get; set; }

        public bool Enabled { get; set; }

        // Tempo para executar cada remoção.
        private int tick;

        private const int OneMinute = 60000;

        /// <summary>
        /// Bloqueia a lista de ip quando a o método de remoção está em execução.
        /// Evita que a lista seja percorrida enquanto algum ip está sendo removido.
        /// </summary>
        private bool blocked = false;

        private Dictionary<string, IpAddress> ipList;

        public IpFiltering() {
            ipList = new Dictionary<string, IpAddress>();

            if (CheckAccessTime <= 0) {
                CheckAccessTime = 5000;
            }

            if (IpMaxAttempt <= 0) {
                IpMaxAttempt = 15;
            }

            if (IpMaxAccessCount <= 0) {
                IpMaxAccessCount = 50;
            }

            if (IpLifetime <= 0) {
                IpLifetime = (1000 * 60) * 2;
            }
        }

        /// <summary>
        /// Limpa todos os endereços bloqueados.
        /// </summary>
        public void Clear() {
            ipList.Clear();
        }

        /// <summary>
        /// Verifica o tempo de cada ip no sistema e adiciona à lista de exclusão.
        /// </summary>
        public void RemoveExpiredIpAddress() {
            // Faz com o que a verificação ocorra a cada 1 minuto. 
            if (Environment.TickCount >= tick + OneMinute) {

                var list = new List<IpAddress>();

                // Se ultrapassou o tempo de permanência no sistema, adiciona à lista de remoção.
                foreach (var ipAddress in ipList.Values) {
                    if (!ipAddress.Permanent) {
                        if (Environment.TickCount >= ipAddress.Time + IpLifetime) {
                            list.Add(ipAddress);
                        }
                    }
                }

                tick = Environment.TickCount;

                RemoveExpiredIpAddress(list);
            }
        }

        /// <summary>
        /// Realiza a verificação se um ip pode ser bloqueado.
        /// </summary>
        /// <param name="ipAddress"></param>
        public bool CanBlockIpAddress(string ipAddress) {
            // Somente executa quando não está no modo bloqueado para exclusão.
            if (blocked) {
                return false;
            }

            // Procura o IP na lista.          
            var result = ipList.ContainsKey(ipAddress);

            // Se não houver nada na lista, adiciona um novo ip.
            if (!result) {
                AddIpAddress(ipAddress, false);
            }
            else {
                // Analisa a quantidade de tentativas e acesso do ip encontrado.
                CheckIpAddressAttempt(ipAddress);

                // Verifica se ultrapassou o límite de tentativas e conexões, caso verdadeiro.
                if (IsMaxAttemptPassed(ipAddress) || IsMaxAccessPassed(ipAddress)) {
                    // Remove da lista pois será adicionado à lista de ip bloqueados. 
                    ipList.Remove(ipAddress);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Verifica a quantidade de acessos e o tempo do ip.
        /// </summary>
        /// <param name="ipAddress"></param>
        private void CheckIpAddressAttempt(string ipAddress) {
            // Obtêm o valor na lista para manipulação dos dados.
            var _ipAddress = ipList[ipAddress];

            // Se acontecer um novo acesso, verifica o tempo do último acesso do IP.

            // Se o acesso aconteceu depois do tempo estipulado, atualiza o tempo do último acesso mas não incrementa.
            if (IsTimePassed(ipAddress)) {
                _ipAddress.Time = Environment.TickCount;
            }
            else {
                // Se o tempo estipulado não passou, incrementa o número de tentativas.
                _ipAddress.Time = Environment.TickCount;
                _ipAddress.AttemptCount++;
            }

            // Incrementa a quantidade de conexões pelo mesmo ip.
            _ipAddress.AccessCount++;

            // Devolve o valor para a lista.
            ipList[ipAddress] = _ipAddress;
        }

        /// <summary>
        /// Verifica se o tempo mínimo de acesso de um ip foi ultrapassado.
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        private bool IsTimePassed(string ipAddress) {
            // Se o tempo do último acesso foi ultrapassado. Retorna verdadeiro.
            if (Environment.TickCount >= (ipList[ipAddress].Time + CheckAccessTime)) {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Verifica se o ip ultrapassou a quantidade máxima de acessos.
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        private bool IsMaxAccessPassed(string ipAddress) {
            return (ipList[ipAddress].AccessCount >= IpMaxAccessCount) ? true : false;
        }

        /// <summary>
        /// Verifica se o ip ultrapassou a quantidade máxima de tentativas.
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        private bool IsMaxAttemptPassed(string ipAddress) {
            return (ipList[ipAddress].AttemptCount >= IpMaxAttempt) ? true : false;
        }

        // Adiciona um novo ip à lista.
        private void AddIpAddress(string ipAddress, bool permanent) {
            ipList.Add(ipAddress, new IpAddress(ipAddress, Environment.TickCount, permanent));
        }

        private void RemoveExpiredIpAddress(List<IpAddress> list) {
            if (list.Count > 0) {
                // Bloqueia o acesso à lista para a verificação de IP.
                blocked = true;

                foreach (var ipAddress in list) {
                    ipList.Remove(ipAddress.Ip);
                }

                // Desativa o bloqueio para a que lista possa ser verificada novamente.
                blocked = false;

                list.Clear();
            }
        }
    }
}