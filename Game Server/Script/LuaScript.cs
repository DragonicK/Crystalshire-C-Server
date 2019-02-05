using System;
using GameServer.Data;
using GameServer.Server;
using GameServer.Network;
using GameServer.Communication;
using GameServer.Server.Character;
using NLua;

namespace GameServer.Script {
    public sealed class LuaScript {
        public void InitializeScript() {
            using (var lua = new Lua()) {
                lua.RegisterFunction("AddIpAddress", null, typeof(IpBlockList).GetMethod("AddIpAddress"));

                IpBlockList.Clear();
                lua.DoFile("BlockList.lua");
            }
        }

        public void LoadClasses() {
            using (var lua = new Lua()) {
                lua.LoadCLRPackage();
                lua.RegisterFunction("AddClass", null, typeof(DataManagement).GetMethod("AddClass"));
                lua.DoFile("./Data/Classes/classes.lua");
            }
        }

        public void LoadCharacterConfiguration() {
            using (var lua = new Lua()) {
                lua.RegisterFunction("AddTime", null, typeof(CharacterDeleteRequest).GetMethod("AddTime"));
                lua.DoFile("./Data/Character.lua");

                Configuration.CharacterCreation = Convert.ToBoolean(lua["Character.Creation"]);
                Configuration.CharacterDelete = Convert.ToBoolean(lua["Character.Delete"]);
                Configuration.CharacterDeleteMinLevel = Convert.ToInt32(lua["Character.DeleteMinLevel"]);
                Configuration.CharacterDeleteMaxLevel = Convert.ToInt32(lua["Character.DeleteMaxLevel"]);
                Configuration.SpriteRangeMinimum = Convert.ToInt32(lua["Character.SpriteRangeMinimum"]);
                Configuration.SpriteRangeMaximum = Convert.ToInt32(lua["Character.SpriteRangeMaximum"]);
            }
        }

        public void LoadProhibitedNames() {
            using (var lua = new Lua()) {
                lua.RegisterFunction("AddRange", null, typeof(ProhibitedNames).GetMethod("AddRange"));
                lua.DoFile("./Data/ProhibitedNames.lua");
            }
        }

        public void LoadGameConfiguration() {
            using (var lua = new Lua()) {
                lua.DoFile("./Data/Configuration.lua");

                Configuration.MaxCharacters = Convert.ToInt32(lua["GameData.MaxCharacters"]);
                Configuration.MaxConversationOptions = Convert.ToInt32(lua["GameData.MaxConversationOptions"]);
                Configuration.MaxAnimationLayer = Convert.ToInt32(lua["GameData.MaxAnimationLayer"]);

                Configuration.MaxAnimations = Convert.ToInt32(lua["GameData.MaxAnimations"]);
                Configuration.MaxConversations = Convert.ToInt32(lua["GameData.MaxConversations"]);
                Configuration.MaxItems = Convert.ToInt32(lua["GameData.MaxItems"]);
                Configuration.MaxMaps = Convert.ToInt32(lua["GameData.MaxMaps"]);
                Configuration.MaxNpcs = Convert.ToInt32(lua["GameData.MaxNpcs"]);
                Configuration.MaxMapNpcs = Convert.ToInt32(lua["GameData.MaxMapNpcs"]);
                Configuration.MaxInventories = Convert.ToInt32(lua["GameData.MaxInventories"]);
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