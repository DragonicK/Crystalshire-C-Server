using System;
using GameServer.Server;
using GameServer.Communication;

namespace GameServer.Network.PacketList {
    public sealed class SpCharacterSelection : SendPacket {
        public SpCharacterSelection(Player player) {
            TimeSpan date;

            msg.Write((int)OpCode.SendPacket[GetType()]);

            for (var n = 1; n <= Configuration.MaxCharacters; n++) {
                msg.Write(player.CharacterSelection[n].Name);
                msg.Write(player.CharacterSelection[n].Sprite);
                msg.Write(player.CharacterSelection[n].Classe);
                msg.Write(Convert.ToByte(player.CharacterSelection[n].PendingExclusion));

                // Escreve o tempo restante.
                if (player.CharacterSelection[n].PendingExclusion) {
                    date = DateTime.Now.Subtract(player.CharacterSelection[n].ExclusionDate);

                    // Múltiplica por -1 para transformar em positivo.
                    msg.Write((byte)(date.Hours * -1));
                    msg.Write((byte)(date.Minutes * -1));
                    msg.Write((byte)(date.Seconds * -1));
                }
            }
        }
    }
}