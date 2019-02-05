using System;
using System.IO;
using System.Collections;
using System.Linq;
using System.Text;

namespace Elysium.IO {
    public class Settings {
        /// <summary>
        /// cache de configuração.
        /// </summary>
        private Hashtable cache = new Hashtable();

        /// <summary>
        /// Lê o arquivo de configuração e armazena as informações.
        /// </summary>
        /// <param name="fileName"></param>
        public void ParseConfigFile(string fileName) {
            if (!File.Exists(fileName))
                throw new Exception("cannot find server configuration file");

            cache.Clear();

            using (StreamReader reader = new StreamReader(fileName, Encoding.Unicode)) {
                string[] validLines = reader.ReadToEnd().Split('\n').Where(l => !l.StartsWith("//")).ToArray();
                foreach (string line in validLines) {
                    if (line == "\r")
                        continue;

                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] parameters = line.Split('=');
                    cache.Add(parameters[0].Trim(), parameters[1].Trim());
                }
            }
        }

        /// <summary>
        /// Retorna o parâmetro em bool.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool GetBoolean(string key) {
            return Convert.ToBoolean(Convert.ToInt32(cache[key]));
        }

        /// <summary>
        /// Retorna o parâmetro em byte.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public byte GetByte(string key) {
            return Convert.ToByte(cache[key]);
        }

        // <summary>
        /// Retorna o parâmetro em inteiro de 32 bits.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public short GetInt16(string key) {
            return Convert.ToInt16(cache[key]);
        }

        /// <summary>
        /// Retorna o parâmetro em inteiro de 32 bits.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int GetInt32(string key) {
            return Convert.ToInt32(cache[key]);
        }

        /// <summary>
        /// Retorna o parâmetro em inteiro de 64 bits.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public long GetInt64(string key) {
            return Convert.ToInt64(cache[key]);
        }

        /// <summary>
        /// Retorna o parâmetro em string.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetString(string key) {
            if (cache[key] == null) return "command not found";

            return cache[key].ToString();
        }

        /// <summary>
        /// Limpa a lista de configurações.
        /// </summary>
        public void Clear() {
            cache.Clear();
        }
    }
}