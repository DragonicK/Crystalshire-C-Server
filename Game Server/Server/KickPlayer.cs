using GameServer.Network.PacketList;

namespace GameServer.Server {
    public sealed class KickPlayer {
        public Player Player { get; set; }
        private Player player;

        public void Kick(string character) {
            if (Player.AccessLevel >= AccessLevel.GameMaster) {
                player = Authentication.FindByCharacter(character);

                if (player != null) {
                    Disconnect();
                } 
                else {
                    var msg = new SpPlayerMessage();
                    msg.Build("Player is not online", TextColor.Pink);
                    msg.Send(Player.Connection);
                }
            }     
        }

        private void Disconnect() {
            var msg = new SpAlertMessage(AlertMessageType.Kicked, MenuResetType.Login, true);
            msg.Send(player.Connection);

            player.Connection.Disconnect();
        }
    }
}