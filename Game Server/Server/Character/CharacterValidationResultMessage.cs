using GameServer.Network.PacketList;

namespace GameServer.Server.Character {
    public abstract class CharacterValidationResultMessage {
        public Player Player { get; set; }

        protected bool CanValidateNext(CharacterValidationResult validationResult) {
            if (validationResult.AlertMessageType != AlertMessageType.None) {
                SendMessage(validationResult);

                if (validationResult.Disconnect) {
                    Player.Connection.Disconnect();
                }

                return false;
            }

            return true;
        }

        protected void SendMessage(CharacterValidationResult validationResult) {
            var msg = new SpAlertMessage(validationResult.AlertMessageType, validationResult.MenuResetType);
            msg.Send(Player.Connection);
        }
    }
}