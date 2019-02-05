using GameServer.Data;

namespace GameServer.Network.PacketList {
    public sealed class SpClasses : SendPacket {
        public SpClasses() {
            var classes = DataManagement.Classes;

            msg.Write((int)OpCode.SendPacket[GetType()]);
            msg.Write(classes.Count);

            foreach (var _class in classes.Values) {
                if (_class.Selectable) {
                    msg.Write(_class.Id);
                    msg.Write(_class.Name);

                    WriteMaleSprites(_class);
                    WriteFemaleSprites(_class);
                }
            }
        }

        private void WriteMaleSprites(Class _class) {
            msg.Write(_class.MaleSprite.Length);

            for (var i = 0; i < _class.MaleSprite.Length; i++) {
                msg.Write(_class.MaleSprite[i]);
            }
        }

        private void WriteFemaleSprites(Class _class) {
            msg.Write(_class.FemaleSprite.Length);

            for (var i = 0; i < _class.MaleSprite.Length; i++) {
                msg.Write(_class.FemaleSprite[i]);
            }
        }
    }
}