using System.Collections.Generic;
using GameServer.Communication;
using Elysium.Logs;

namespace GameServer.Data {
    public static class DataManagement {
        public static MapDatabase MapDatabase { get; set; }
        public static NpcDatabase NpcDatabase { get; set; }
        public static ItemDatabase ItemDatabase { get; set; }
        public static AnimationDatabase AnimationDatabase { get; set; }
        public static ConversationDatabase ConversationDatabase { get; set; }
        public static Dictionary<int, Class> Classes { get; set; }

        static DataManagement() {
            Classes = new Dictionary<int, Class>();
        }

        public static void AddClass(Class _class) {
            var id = _class.Id;

            if (!IsValidClass(id)) {
                Classes.Add(id, _class);
                Global.WriteLog(LogType.System, $"Classe: {_class.Name} Id {_class.Id} adicionado", LogColor.Black);
            }
            else {
                Global.WriteLog(LogType.System, $"Classe: {_class.Name} Id {_class.Id} não adicionado, Id duplicado", LogColor.Crimson);
            }
        }

        public static bool IsValidClass(int classId) {
            if (Classes.ContainsKey(classId)) {
                return true;
            }

            return false;
        }

        public static void ClearDatabases() {
            MapDatabase.Clear();
            NpcDatabase.Clear();
            ItemDatabase.Clear();
            AnimationDatabase.Clear();
            ConversationDatabase.Clear();
            Classes.Clear();
        }

        public static void InitializeData() {
            MapDatabase = new MapDatabase() {
                Folder = "Maps",
                FileName = "Map"
            };

            Global.WriteLog(LogType.System, "Loading maps", LogColor.Coral);
            MapDatabase.CheckFolder();
            MapDatabase.LoadFiles();

            NpcDatabase = new NpcDatabase() {
                Folder = "Npcs",
                FileName = "Npc"
            };

            Global.WriteLog(LogType.System, "Loading npcs", LogColor.Coral);
            NpcDatabase.CheckFolder();
            NpcDatabase.LoadFiles();

            ItemDatabase = new ItemDatabase() {
                Folder = "Items",
                FileName = "Item"
            };

            Global.WriteLog(LogType.System, "Loading items", LogColor.Coral);
            ItemDatabase.CheckFolder();
            ItemDatabase.LoadFiles();

            AnimationDatabase = new AnimationDatabase() {
                Folder = "Animations",
                FileName = "Animation"
            };

            Global.WriteLog(LogType.System, "Loading animations", LogColor.Coral);
            AnimationDatabase.CheckFolder();
            AnimationDatabase.LoadFiles();

            ConversationDatabase = new ConversationDatabase() {
                Folder = "Conversations",
                FileName = "Conversation"
            };

            Global.WriteLog(LogType.System, "Loading conversations", LogColor.Coral);
            ConversationDatabase.CheckFolder();
            ConversationDatabase.LoadFiles();
        }
    }
}