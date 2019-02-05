using System.IO;
using System.Collections.Generic;
using GameServer.Communication;

namespace GameServer.Data {
    public sealed class AnimationDatabase : Database<Animation> {
        public AnimationDatabase() {
            MaxValues = Configuration.MaxAnimations;
            values = new Dictionary<int, Animation>();

            for (var i = 1; i <= MaxValues; i++) {
                values.Add(i, new Animation());
            }
        }

        public override void LoadFile(int objectId) {
            var name = $"{FileName}{objectId}.dat";
            var animation = values[objectId];

            var file = new FileStream($"./Data/{Folder}/{name}", FileMode.Open, FileAccess.Read);
            var reader = new BinaryReader(file);

            animation.Id = reader.ReadInt32();
            animation.Name = reader.ReadString();
            animation.Sound = reader.ReadString();

            for (var i = 0; i < Configuration.MaxAnimationLayer; i++) {
                animation.Sprite[i] = reader.ReadInt32();
                animation.Frames[i] = reader.ReadInt32();
                animation.LoopCount[i] = reader.ReadInt32();
                animation.LoopTime[i] = reader.ReadInt32();
            }

            reader.Close();
            reader.Dispose();

            file.Close();
            file.Dispose();
        }

        public override void SaveFile(int objectId) {
            var name = $"{FileName}{objectId}.dat";
            var animation = values[objectId];

            var file = new FileStream($"./Data/{Folder}/{name}", FileMode.Create, FileAccess.Write);
            var writer = new BinaryWriter(file);

            animation.Id = objectId;
            writer.Write(animation.Id);
            writer.Write(animation.Name);
            writer.Write(animation.Sound);

            for (var i = 0; i < Configuration.MaxAnimationLayer; i++) {
                writer.Write(animation.Sprite[i]);
                writer.Write(animation.Frames[i]);
                writer.Write(animation.LoopCount[i]);
                writer.Write(animation.LoopTime[i]);
            }

            writer.Close();
            writer.Dispose();

            file.Close();
            file.Dispose();
        }
    }
}