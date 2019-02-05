using System;
using System.Collections.Generic;
using LoginServer.Network.PacketList;
using LoginServer.Network.PacketList.Login;
using LoginServer.Network.PacketList.Game;
using LoginServer.Network.PacketList.Client;

namespace LoginServer.Network {
    public static class OpCode {
        public static Dictionary<int, Type> RecvPacket;
        public static Dictionary<Type, int> SendPacket;

        public static void Initialize() {
            RecvPacket = new Dictionary<int, Type> {
                // O pacote de ping não fica registrado no servidor de login.
                { ClientPacket.AuthLogin, typeof(CpAccountLogin) },
            };

            SendPacket = new Dictionary<Type, int>() {
                // Ping enviado para o game server. Verificar a conexão.
                { typeof(SpPing), ServerPacket.Ping },
                { typeof(SpAlertMessage), ServerPacket.AlertMessage },
                { typeof(SpLoginToken), ServerPacket.LoginToken },
                { typeof(SpSendUserData), ServerPacket.SendUserData }
            };

        }
    }
}