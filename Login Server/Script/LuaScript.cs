using NLua;
using System;
using LoginServer.Communication;
using LoginServer.Network;

namespace LoginServer.Script {
    public sealed class LuaScript {

        public void InitializeScript() {
            using (var lua = new Lua()) {

                lua.LoadCLRPackage();
                lua.RegisterFunction("AddChecksum", null, typeof(Checksum).GetMethod("Add"));
                lua.RegisterFunction("AddCountry", null, typeof(GeoIpBlock).GetMethod("AddCountry"));
                lua.RegisterFunction("AddIpAddress", null, typeof(IpBlockList).GetMethod("AddIpAddress"));

                Checksum.Clear();
                lua.DoFile("Checksum.lua");

                GeoIpBlock.Clear();
                lua.DoFile("GeoIP.lua");

                IpBlockList.Clear();
                lua.DoFile("BlockList.lua");
            }
        }

        public void ReloadChecksum() {
            using (var lua = new Lua()) {
                Checksum.Clear();

                lua.LoadCLRPackage();
                lua.RegisterFunction("AddChecksum", null, typeof(Checksum).GetMethod("Add"));

                lua.DoFile("Checksum.lua");
            }
        }

        public void ReloadGeoBlockList() {
            using (var lua = new Lua()) {
                GeoIpBlock.Clear();

                lua.LoadCLRPackage();
                lua.RegisterFunction("AddCountry", null, typeof(GeoIpBlock).GetMethod("AddCountry"));

                lua.DoFile("GeoIP.lua");
            }
        }

        public void ReloadBlockList() {
            using (var lua = new Lua()) {
                IpBlockList.Clear();

                lua.LoadCLRPackage();
                lua.RegisterFunction("AddIpAddress", null, typeof(IpBlockList).GetMethod("AddIpAddress"));

                lua.DoFile("BlockList.lua");
            }
        }
    }
}