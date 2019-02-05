using System;
using System.IO;
using System.Collections.Generic;
using GameServer.Communication;

namespace GameServer.Data {
    public sealed class MapDatabase : Database<Map> {
        private readonly int layerCount;

        public MapDatabase() {
            MaxValues = Configuration.MaxMaps;
            values = new Dictionary<int, Map>();

            for (var i = 1; i <= MaxValues; i++) {
                values.Add(i, new Map());
            }

            layerCount = Enum.GetValues(typeof(LayerType)).Length;
        }

        public override void LoadFile(int objectId) {
            var name = $"{FileName}{objectId}.dat";
            var map = values[objectId];

            var file = new FileStream($"./Data/{Folder}/{name}", FileMode.Open, FileAccess.Read);
            var reader = new BinaryReader(file);

            ReadMapData(ref reader, ref map);

            ReadMapEvents(ref reader, ref map);

            map.Tile = new Tile[map.MaxX + 1, map.MaxY + 1];

            for (var x = 0; x <= map.MaxX; x++) {
                for (var y = 0; y <= map.MaxY; y++) {

                    map.Tile[x, y] = new Tile {
                        Type = (TileType)reader.ReadByte(),
                        Data1 = reader.ReadInt32(),
                        Data2 = reader.ReadInt32(),
                        Data3 = reader.ReadInt32(),
                        Data4 = reader.ReadInt32(),
                        Data5 = reader.ReadInt32(),
                        DirBlock = reader.ReadByte()
                    };

                    for (var i = 0; i < layerCount; i++) {
                        map.Tile[x, y].Layer[i].TileSet = reader.ReadInt32();
                        map.Tile[x, y].Layer[i].X = reader.ReadInt32();
                        map.Tile[x, y].Layer[i].Y = reader.ReadInt32();
                        map.Tile[x, y].AutoTile[i] = reader.ReadByte();
                    }
                }
            }

            reader.Close();
            reader.Dispose();

            file.Close();
            file.Dispose();
        }

        public override void SaveFile(int objectId) {
            var name = $"{FileName}{objectId}.dat";
            var map = values[objectId];

            var file = new FileStream($"./Data/{Folder}/{name}", FileMode.Create, FileAccess.Write);
            var writer = new BinaryWriter(file);

            map.Id = objectId;

            WriteMapData(ref writer, ref map);

            WriteMapEvents(ref writer, ref map);

            for (var x = 0; x <= map.MaxX; x++) {
                for (var y = 0; y <= map.MaxY; y++) {
                    writer.Write((byte)map.Tile[x, y].Type);
                    writer.Write(map.Tile[x, y].Data1);
                    writer.Write(map.Tile[x, y].Data2);
                    writer.Write(map.Tile[x, y].Data3);
                    writer.Write(map.Tile[x, y].Data4);
                    writer.Write(map.Tile[x, y].Data5);
                    writer.Write(map.Tile[x, y].DirBlock);

                    for (var i = 0; i < layerCount; i++) {
                        writer.Write(map.Tile[x, y].Layer[i].TileSet);
                        writer.Write(map.Tile[x, y].Layer[i].X);
                        writer.Write(map.Tile[x, y].Layer[i].Y);
                        writer.Write(map.Tile[x, y].AutoTile[i]);
                    }
                }
            }

            writer.Close();
            writer.Dispose();

            file.Close();
            file.Dispose();
        }

        private void WriteMapData(ref BinaryWriter writer, ref Map map) {
            writer.Write(map.Id);
            writer.Write(map.Name); // writer.Write(GetBufferText(map.Name, Constants.MaxNameLength));
            writer.Write(map.Music); // writer.Write(GetBufferText(map.Music, Constants.MaxNameLength));
            writer.Write(map.Moral);
            writer.Write(map.Up);
            writer.Write(map.Down);
            writer.Write(map.Left);
            writer.Write(map.Right);

            writer.Write(map.BootMap);
            writer.Write(map.BootX);
            writer.Write(map.BootY);

            writer.Write(map.MaxX);
            writer.Write(map.MaxY);

            writer.Write(map.BossNpc);

            for (var i = 1; i <= Configuration.MaxMapNpcs; i++) {
                writer.Write(map.Npc[i]);
            }
        }

        private void WriteMapEvents(ref BinaryWriter writer, ref Map map) {
            //Events
            writer.Write(map.EventCount);

            if (map.EventCount > 0) {
                for (var i = 0; i < map.EventCount; i++) {
                    writer.Write(map.Events[i].Name); //GetBufferText(map.Events[i].Name, Constants.MaxNameLength));
                    writer.Write(map.Events[i].X);
                    writer.Write(map.Events[i].Y);
                    writer.Write(map.Events[i].PageCount);

                    if (map.Events[i].PageCount > 0) {
                        for (var z = 0; z < map.Events[i].PageCount; z++) {
                            writer.Write(map.Events[i].EventPages[z].CheckPlayerVariable);
                            writer.Write(map.Events[i].EventPages[z].CheckSelfSwitch);
                            writer.Write(map.Events[i].EventPages[z].CheckHasItem);

                            writer.Write(map.Events[i].EventPages[z].PlayerVariableNum);
                            writer.Write(map.Events[i].EventPages[z].SelfSwitchNum);
                            writer.Write(map.Events[i].EventPages[z].HasItemNum);

                            writer.Write(map.Events[i].EventPages[z].PlayerVariable);

                            writer.Write(map.Events[i].EventPages[z].GraphicType);
                            writer.Write(map.Events[i].EventPages[z].Graphic);
                            writer.Write(map.Events[i].EventPages[z].GraphicX);
                            writer.Write(map.Events[i].EventPages[z].GraphicY);

                            writer.Write(map.Events[i].EventPages[z].MoveType);
                            writer.Write(map.Events[i].EventPages[z].MoveSpeed);
                            writer.Write(map.Events[i].EventPages[z].MoveFrequency);
                            writer.Write(map.Events[i].EventPages[z].WalkAnim);

                            writer.Write(map.Events[i].EventPages[z].StepAnim);
                            writer.Write(map.Events[i].EventPages[z].DirFix);
                            writer.Write(map.Events[i].EventPages[z].WalkThrough);

                            writer.Write(map.Events[i].EventPages[z].Priority);
                            writer.Write(map.Events[i].EventPages[z].Trigger);
                            writer.Write(map.Events[i].EventPages[z].CommandCount);

                            if (map.Events[i].EventPages[z].CommandCount > 0) {
                                for (var x = 0; x < map.Events[i].EventPages[z].CommandCount; x++) {
                                    writer.Write(map.Events[i].EventPages[z].Commands[x].Type);
                                    writer.Write(map.Events[i].EventPages[z].Commands[x].Text); //GetBufferText(map.Events[i].EventPages[z].Commands[x].Text, Constants.MaxNameLength));
                                    writer.Write(map.Events[i].EventPages[z].Commands[x].Colour);
                                    writer.Write(map.Events[i].EventPages[z].Commands[x].Channel);
                                    writer.Write(map.Events[i].EventPages[z].Commands[x].TargetType);
                                    writer.Write(map.Events[i].EventPages[z].Commands[x].Target);

                                }
                            }
                        }
                    }
                }
            }
        }

        private void ReadMapData(ref BinaryReader reader, ref Map map) {
            map.Id = reader.ReadInt32();
            map.Name = reader.ReadString(); //GetTextFromBuffer(ref reader, Constants.MaxNameLength);
            map.Music = reader.ReadString(); //GetTextFromBuffer(ref reader, Constants.MaxNameLength);
 
            map.Moral = reader.ReadByte();
            map.Up = reader.ReadInt32();
            map.Down = reader.ReadInt32();
            map.Left = reader.ReadInt32();
            map.Right = reader.ReadInt32();

            map.BootMap = reader.ReadInt32();
            map.BootX = reader.ReadByte();
            map.BootY = reader.ReadByte();

            map.MaxX = reader.ReadByte();
            map.MaxY = reader.ReadByte();

            map.BossNpc = reader.ReadInt32();

            for (var i = 1; i <= Configuration.MaxMapNpcs; i++) {
                map.Npc[i] = reader.ReadInt32();
            }
        }

        private void ReadMapEvents(ref BinaryReader reader, ref Map map) {
            map.EventCount = reader.ReadInt32();

            if (map.EventCount > 0) {
                for (var x = 0; x < map.EventCount; x++) {
                    map.Events[x] = new Event {
                        Name = reader.ReadString(), //GetTextFromBuffer(ref reader, Constants.MaxNameLength),
                        X = reader.ReadInt32(),
                        Y = reader.ReadInt32(),
                        PageCount = reader.ReadInt32()
                    };

                    if (map.Events[x].PageCount > 0) {
                        map.Events[x].EventPages = new EventPage[map.Events[x].PageCount];

                        for (var y = 0; y < map.Events[x].PageCount; y++) {
                            map.Events[x].EventPages[y] = new EventPage {
                                CheckPlayerVariable = reader.ReadByte(),
                                CheckSelfSwitch = reader.ReadByte(),
                                CheckHasItem = reader.ReadByte(),

                                PlayerVariableNum = reader.ReadInt32(),
                                SelfSwitchNum = reader.ReadInt32(),
                                HasItemNum = reader.ReadInt32(),

                                PlayerVariable = reader.ReadInt32(),

                                GraphicType = reader.ReadByte(),
                                Graphic = reader.ReadInt32(),
                                GraphicX = reader.ReadInt32(),
                                GraphicY = reader.ReadInt32(),

                                MoveType = reader.ReadByte(),
                                MoveSpeed = reader.ReadByte(),
                                MoveFrequency = reader.ReadByte(),
                                WalkAnim = reader.ReadByte(),

                                StepAnim = reader.ReadByte(),
                                DirFix = reader.ReadByte(),
                                WalkThrough = reader.ReadByte(),

                                Priority = reader.ReadByte(),
                                Trigger = reader.ReadByte(),
                                CommandCount = reader.ReadInt32()
                            };

                            if (map.Events[x].EventPages[y].CommandCount > 0) {
                                map.Events[x].EventPages[y].Commands = new EventCommand[map.Events[x].EventPages[y].CommandCount];

                                for (var z = 0; z < map.Events[x].EventPages[y].CommandCount; z++) {
             
                                    map.Events[x].EventPages[y].Commands[z] = new EventCommand {
                                        Type = reader.ReadByte(),
                                        Text = reader.ReadString(), // GetTextFromBuffer(ref reader, Constants.MaxNameLength),
                                        Colour = reader.ReadInt32(),
                                        Channel = reader.ReadByte(),
                                        TargetType = reader.ReadByte(),
                                        Target = reader.ReadInt32()
                                    };

                                }
                            }
                        }
                    }
                }//
            }
        }
    }
}