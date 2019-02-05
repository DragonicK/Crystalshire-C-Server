using System.IO;
using System.Text;
using System.Collections.Generic;

namespace GameServer.Data {
    public abstract class Database<T> {
        public string Folder { get; set; }
        public string FileName { get; set; } = string.Empty;
        public int MaxValues { get; set; } = 1;

        protected Dictionary<int, T> values;

        public T this[int index] {
            get {
                return values[index];
            }

            set {
                values[index] = value;
            }
        }

        public void CheckFolder() {
            if (!Directory.Exists($"./Data/{Folder}")) {
                Directory.CreateDirectory($"./Data/{Folder}");
            }
        }

        public void Clear() {
            values.Clear();
        }

        public void LoadFiles() {
            CheckFiles();

            for (var i = 1; i <= MaxValues; i++) {
                LoadFile(i);
            }
        }

        public virtual void LoadFile(int objectId) {

        }

        public void SaveFiles() {
            for (var i = 1; i <= MaxValues; i++) {
                SaveFile(i);
            }
        }

        public virtual void SaveFile(int objectId) {

        }

        private void CheckFiles() {
            for (var i = 1; i <= MaxValues; i++) {
                if (!File.Exists($"./Data/{Folder}/{FileName}{i}.dat")) {
                    SaveFile(i);
                }
            }
        }

        protected byte[] GetBufferText(string text, int length) {
            var _buffer = Encoding.ASCII.GetBytes(text);
            var buffer = new byte[length];

            _buffer.CopyTo(buffer, 0);

            return buffer;
        }

        protected string GetTextFromBuffer(ref BinaryReader reader, int length) {
            var buffer = new byte[length];
            reader.Read(buffer, 0, length);
            return Encoding.ASCII.GetString(buffer);
        }
    }
}