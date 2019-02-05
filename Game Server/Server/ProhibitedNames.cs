using System.Collections.Generic;
using System.Linq;

namespace GameServer.Server {
    public static class ProhibitedNames {
        /// <summary>
        /// Lista de nomes banidos.
        /// </summary>
        private static HashSet<string> names = new HashSet<string>();

        /// <summary>
        /// Adiciona um array de nomes.
        /// </summary>
        /// <param name="name"></param>
        public static void AddRange(params string[] name) {
            foreach (string item in name) {
                names.Add(item);
            }
        }

        /// <summary>
        /// Realiza a comparação entre dois items.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool Compare(string name) {
            if (string.IsNullOrEmpty(name)) return false;

            var find_name = from nData in names
                            where string.Compare(nData, name, true) == 0
                            select nData;

            //retorna falso, quando a string é nula ou vazia
            return (string.IsNullOrEmpty(find_name.FirstOrDefault())) ? false : true;
        }

        /// <summary>
        /// Remove todos os items do hashset.
        /// </summary>
        public static void Clear() {
            names.Clear();
            names = null;
        }
    }
}