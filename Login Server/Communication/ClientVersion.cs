namespace LoginServer.Communication {
    public struct ClientVersion {
        public byte ClientMajor { get; set; }
        public byte ClientMinor { get; set; }
        public byte ClientRevision { get; set; }

        public bool Compare(ClientVersion version) {
            if (ClientMajor != version.ClientMajor) {
                return false;
            }

            if (ClientMinor != version.ClientMinor) {
                return false;
            }

            if (ClientRevision != version.ClientRevision) {
                return false;
            }

            return true;    
        }
    }
}