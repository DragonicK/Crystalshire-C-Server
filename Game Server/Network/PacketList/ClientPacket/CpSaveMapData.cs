using System;
using GameServer.Data;
using GameServer.Server;
using GameServer.Communication;
using Elysium.Logs;

namespace GameServer.Network.PacketList {
    public sealed class CpSaveMapData : IRecvPacket {
        readonly int layerCount = 0;

        public CpSaveMapData() {
            layerCount = Enum.GetValues(typeof(LayerType)).Length;
        }

        public void Process(byte[] buffer, IConnection connection) {
            var pData = Authentication.Players[connection.Index];

            if (pData.AccessLevel < AccessLevel.Administrator) {
                return;
            }

            var mapId = pData.MapId;
            var map = new Map();
            var msg = new ByteBuffer(buffer);

            map.Id = mapId;
            map.Name = msg.ReadString();
            map.Music = msg.ReadString();
            map.Moral = msg.ReadByte();
            map.Up = msg.ReadInt32();
            map.Down = msg.ReadInt32();
            map.Left = msg.ReadInt32();
            map.Right = msg.ReadInt32();
            map.BootMap = msg.ReadInt32();
            map.BootX = msg.ReadByte();
            map.BootY = msg.ReadByte();
            map.MaxX = msg.ReadByte();
            map.MaxY = msg.ReadByte();
            map.BossNpc = msg.ReadInt32();
      
            for (var i = 1; i <= Configuration.MaxMapNpcs; i++) {
                map.Npc[i] = msg.ReadInt32();
            }

            ReadMapEventData(ref map, ref msg);

            map.Tile = new Tile[map.MaxX + 1, map.MaxY + 1];

            for (var x = 0; x <= map.MaxX; x++) {
                for (var y = 0; y <= map.MaxY; y++) {

                    map.Tile[x, y] = new Tile {
                        Type = (TileType)msg.ReadByte(),
                        Data1 = msg.ReadInt32(),
                        Data2 = msg.ReadInt32(),
                        Data3 = msg.ReadInt32(),
                        Data4 = msg.ReadInt32(),
                        Data5 = msg.ReadInt32(),

                        DirBlock = msg.ReadByte()
                    };

                    for (var i = 0; i < layerCount; i++) {
                        map.Tile[x, y].Layer[i] = new Square {
                            X = msg.ReadInt32(),
                            Y = msg.ReadInt32(),
                            TileSet = msg.ReadInt32()
                        };

                        map.Tile[x, y].AutoTile[i] = msg.ReadByte();
                    }

                }
            }

            // Adiciona o mapa.
            DataManagement.MapDatabase[mapId] = map;
            DataManagement.MapDatabase.SaveFile(mapId);
            Global.InitializeMap(mapId);

            var mapNpcs = new SpMapNpcData(Global.GetMap(mapId));
            mapNpcs.SendToMap(mapId);

            Global.WriteLog(LogType.Game, $"Character: {pData.Character} {pData.AccessLevel.ToString()} saved mapId {mapId}", LogColor.Green);
        }

        private void ReadMapEventData(ref Map map, ref ByteBuffer msg) {
            map.EventCount = msg.ReadInt32();

            if (map.EventCount > 0) {
                map.Events = new Event[map.EventCount];

                for (var x = 0; x < map.EventCount; x++) {
                    map.Events[x] = new Event {
                        Name = msg.ReadString(),
                        X = msg.ReadInt32(),
                        Y = msg.ReadInt32(),
                        PageCount = msg.ReadInt32()
                    };

                    if (map.Events[x].PageCount > 0) {
                        map.Events[x].EventPages = new EventPage[map.Events[x].PageCount];

                        for (var y = 0; y < map.Events[x].PageCount; y++) {
                            map.Events[x].EventPages[y] = new EventPage {
                                CheckPlayerVariable = msg.ReadByte(),
                                CheckSelfSwitch = msg.ReadByte(),
                                CheckHasItem = msg.ReadByte(),

                                PlayerVariableNum = msg.ReadInt32(),
                                SelfSwitchNum = msg.ReadInt32(),
                                HasItemNum = msg.ReadInt32(),

                                PlayerVariable = msg.ReadInt32(),

                                GraphicType = msg.ReadByte(),
                                Graphic = msg.ReadInt32(),
                                GraphicX = msg.ReadInt32(),
                                GraphicY = msg.ReadInt32(),

                                MoveType = msg.ReadByte(),
                                MoveSpeed = msg.ReadByte(),
                                MoveFrequency = msg.ReadByte(),
                                WalkAnim = msg.ReadByte(),

                                StepAnim = msg.ReadByte(),
                                DirFix = msg.ReadByte(),
                                WalkThrough = msg.ReadByte(),

                                Priority = msg.ReadByte(),
                                Trigger = msg.ReadByte(),
                                CommandCount = msg.ReadInt32()
                            };

                            if (map.Events[x].EventPages[y].CommandCount > 0) {
                                map.Events[x].EventPages[y].Commands = new EventCommand[map.Events[x].EventPages[y].CommandCount];

                                for (var z = 0; z < map.Events[x].EventPages[y].CommandCount; z++) {
                                    map.Events[x].EventPages[y].Commands[z] = new EventCommand {
                                        Type = msg.ReadByte(),
                                        Text = msg.ReadString(),
                                        Colour = msg.ReadInt32(),
                                        Channel = msg.ReadByte(),
                                        TargetType = msg.ReadByte(),
                                        Target = msg.ReadInt32()
                                    };

                                }
                            }
                        }
                    }
                }
            }
        }
    }
}