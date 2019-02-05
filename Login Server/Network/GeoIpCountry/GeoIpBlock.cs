using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace LoginServer.Network {
    public static class GeoIpBlock {
        public static bool Enabled { get; set; }
        /// <summary>
        /// Lista de endereços e dados de países.
        /// </summary>
        private static HashSet<IpCountry> geoip = new HashSet<IpCountry>();

        /// <summary>
        /// Lista de países bloqueados.
        /// </summary>
        private static List<string> countryBlock = new List<string>();

        /// <summary>
        /// Adiciona um país à lista de bloqueio.
        /// </summary>
        /// <param name="code"></param>
        public static void AddCountry(string code) {
            countryBlock.Add(code);
        }

        /// <summary>
        /// Limpa a lista de países bloqueados.
        /// </summary>
        public static void Clear() {
            countryBlock.Clear();
        }

        /// <summary>
        /// Limpa todos os dados.
        /// </summary>
        public static void ClearAll() {
            geoip.Clear();
            countryBlock.Clear();
        }

        /// <summary>
        /// Verifica se um endereço está bloqueado.
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public static bool IsCountryIpBlocked(string ipAddress) {
            var country = FindCountryByIp(ipAddress);

            if (country == null) {
                return false;
            }

            return (IsCountryCodeBlocked(country.Code)) ? true : false;
        }

        /// <summary>
        /// Verifica se o código do país está na lista de bloqueio.
        /// </summary>
        /// <param name="country_code"></param>
        /// <returns></returns>
        private static bool IsCountryCodeBlocked(string country_code) {
            var count = countryBlock.Count;

            for (var index = 0; index < count; index++) {
                if (countryBlock[index].CompareTo(country_code) == 0) {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Realiza uma pesquisa pelo endereço.
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public static IpCountry FindCountryByIp(string ipAddress) {
            var ipNumber = GetIpNumber(ipAddress);

            var find = from country in geoip
                       where (country.NumberMin <= ipNumber && country.NumberMax >= ipNumber)
                       select country;

            return find.FirstOrDefault();
        }

        /// <summary>
        /// Faz a soma dos valores e retorna o número do endereço.
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        private static long GetIpNumber(string ipAddress) {
            if (ipAddress == "::1") {
                ipAddress = "127.0.0.1";
            }

            string[] ips = ipAddress.Split('.');

            long w = long.Parse(ips[0]) * 16777216;
            long x = long.Parse(ips[1]) * 65536;
            long y = long.Parse(ips[2]) * 256;
            long z = long.Parse(ips[3]);

            long ipnumber = w + x + y + z;
            return ipnumber;
        }

        /// <summary>
        /// Abre o arquivo e obtem todos os dados.
        /// </summary>
        public static void LoadData() {
            using (var fs = File.OpenRead(".\\GeoIPCountryWhois.csv")) {
                using (var sqlReader = new StreamReader(fs)) {

                    while (!sqlReader.EndOfStream) {
                        var values = sqlReader.ReadLine().Split(',');

                        var ipCountry = new IpCountry() {
                            IpFrom = values[0].Replace('"', ' ').Trim(),
                            IpTo = values[1].Replace('"', ' ').Trim(),
                            NumberMin = Convert.ToInt64(values[2].Replace('"', ' ').Trim()),
                            NumberMax = Convert.ToInt64(values[3].Replace('"', ' ').Trim()),
                            Code = values[4].Replace('"', ' ').Trim(),
                            Country = values[5].Replace('"', ' ').Trim()
                        };

                        geoip.Add(ipCountry);
                    }
                }
            }
        }
    }
}