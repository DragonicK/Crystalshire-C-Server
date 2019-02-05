using System;
using System.Collections;

namespace Elysium.Service {
    public class PlayerService {
        Hashtable service = new Hashtable();

        const int EXPIRED = 1;

        /// <summary>
        /// Indice de serviço.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public DictionaryEntry this[int serviceID] {
            get {
                return (DictionaryEntry)service[serviceID];
            }
        }

        /// <summary>
        /// Adiciona um novo serviço de usuário.
        /// </summary>
        /// <param name="serviceID"></param>
        /// <param name="dateTime"></param>
        public void Add(int serviceID, DateTime dateTime) {
            service.Add(serviceID, dateTime);
        }

        /// <summary>
        /// Adiciona um novo serviço de usuário.
        /// </summary>
        /// <param name="data"></param>
        public void Add(string data) {
            const int ID = 0, DATE = 1;
            string[] result = data.Split('-');

            service.Add(int.Parse(result[ID]), DateTime.Parse(result[DATE]));
        }

        /// <summary>
        /// Remove um serviço de usuário.
        /// </summary>
        /// <param name="serviceID"></param>
        public void Remove(int serviceID) {
            service.Remove(serviceID);
        }

        /// <summary>
        /// Quantidade de serviços.
        /// </summary>
        /// <returns></returns>
        public int Count() {
            return service.Count;
        }

        /// <summary>
        /// Limpa todos os dados.
        /// </summary>
        public void Clear() {
            service.Clear();
        }

        /// <summary>
        /// Verifica se o serviço já expirou.
        /// </summary>
        /// <param name="serviceID"></param>
        /// <returns></returns>
        public bool IsServiceExpired(int serviceID) {
            return DateTime.Now.CompareTo(Convert.ToDateTime(service[serviceID])) == EXPIRED ? true : false;
        }

        /// <summary>
        /// Verifica a lista de serviços e atualiza no DB.
        /// </summary>
        public void VerifyServices() {
            var services = GetServicesID();

            foreach (var serviceID in services) {
                if (IsServiceExpired(serviceID)) {
                    service.Remove(serviceID);
                }
            }
        }

        /// <summary>
        /// Lista todos os serviços do usuário.
        /// </summary>
        /// <returns></returns>
        public int[] GetServicesID() {
            int[] service = new int[this.service.Count];
            var index = 0;

            foreach (DictionaryEntry item in this.service) {
                service[index++] = Convert.ToInt32(item.Key);
                //index++;
            }

            return service;
        }

        /// <summary>
        /// Retorna o ID com o tempo do serviço.
        /// </summary>
        /// <param name="serviceID"></param>
        /// <returns></returns>
        public string GetService(int serviceID) {
            var date = Convert.ToDateTime(service[serviceID]);
            return $"{serviceID}-{date.ToString()}";
        }
    }
}