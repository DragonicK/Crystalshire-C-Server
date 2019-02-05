using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Network.PacketList {
    public sealed class SpHighIndex : SendPacket {
        public SpHighIndex(int highIndex) {
            msg.Write((int)OpCode.SendPacket[GetType()]);
            msg.Write(highIndex);
        }
    }
}