using System;
using System.Text;
using System.Collections.Generic;
using LoginServer.Communication;
using Elysium.Logs;

namespace LoginServer.Network {
    public sealed class ByteBuffer {
        private const int Byte = 1;
        private const int Int16 = 2;
        private const int Int32 = 4;

        List<byte> buffer;
        int readpos = 0;

        public ByteBuffer() {
            buffer = new List<byte>();
        }

        public ByteBuffer(byte[] arr) {
            buffer = new List<byte>();
            buffer.AddRange(arr);
        }

        private int ReadPosition() {
            return readpos;
        }

        public byte[] ToArray() {
            return buffer.ToArray();
        }

        public int Length() {
            return buffer.Count - readpos;
        }

        public int Count() {
            return buffer.Count;
        }

        public void Trim() {
            if (readpos >= buffer.Count) {
                Flush();
            }
        }

        public void Flush() {
            buffer.Clear();
            readpos = 0;
        }

        public void Write(byte[] value) {
            buffer.AddRange(value);
        }

        public void Write(byte value) {
            buffer.Add(value);
        }

        public void Write(short value) {
            buffer.AddRange(BitConverter.GetBytes(value));
        }

        public void Write(int value) {
            buffer.AddRange(BitConverter.GetBytes(value));
        }

        public void Write(long value) {
            buffer.AddRange(BitConverter.GetBytes(value));
        }

        public void Write(string value) {
            buffer.AddRange(BitConverter.GetBytes(value.Trim().Length));
            buffer.AddRange(Encoding.ASCII.GetBytes(value));
        }

        public byte[] ReadBytes(int length, bool peek = true) {
            if (readpos > buffer.Count - length) {
                return new byte[0];
            }

            var values = buffer.GetRange(readpos, length);

            if (peek) {
                readpos += length;
            }

            return values.ToArray();
        }

        public byte ReadByte(bool peek = true) {
            if (readpos > buffer.Count - Byte) {
                return 0;
            }

            var value = buffer[readpos];

            if (peek) {
                readpos += 1;
            }

            return value;
        }

        public short ReadInt16(bool peek = true) {
            if (readpos > buffer.Count - Int16) {
                return 0;
            }

            var value = BitConverter.ToInt16(ToArray(), readpos);

            if (peek) {
                readpos += 2;
            }

            return value;
        }

        public int ReadInt32(bool peek = true) {
            if (readpos > buffer.Count - Int32) {
                return 0;
            }

            var value = BitConverter.ToInt32(buffer.ToArray(), readpos);

            if (peek) {
                readpos += 4;
            }

            return value;
        }

        public string ReadString(bool peek = true) {
            try {

                var length = ReadInt32();
                if (readpos > buffer.Count - length) {
                    return string.Empty;
                }

                var text = Encoding.ASCII.GetString(ToArray(), readpos, length);

                if (peek) {
                    readpos += text.Length;
                }

                return text;
            }
            catch (ArgumentOutOfRangeException ex) {
                Global.WriteLog(LogType.System, $"Error when reading state: {GetType().Name}", LogColor.Red);
                Global.WriteLog(LogType.System, $"Data: {ex.Message}", LogColor.Red);
                return string.Empty;
            }
        }
    }
}