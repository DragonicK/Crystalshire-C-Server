using GameServer.Data;

namespace GameServer.Network.PacketList {
    public sealed class SpUpdateItem : SendPacket {
        public void Build(Item item) {
            msg.Clear();
            msg.Write((int)OpCode.SendPacket[GetType()]);
            msg.Write(item.Id);
            msg.Write(item.Name);
            msg.Write(item.Description);
            msg.Write(item.Sound);
            msg.Write(item.Icon);
            msg.Write((byte)item.Type);
            msg.Write(item.Data1);
            msg.Write(item.Data2);
            msg.Write(item.Data3);
            msg.Write(item.ClassRequired);
            msg.Write(item.AccessLevelRequired);
            msg.Write(item.LevelRequired);
            msg.Write(item.Price);
            msg.Write(item.Rarity);
            msg.Write(item.BindType);
            msg.Write(item.Animation);
        }
    }
}