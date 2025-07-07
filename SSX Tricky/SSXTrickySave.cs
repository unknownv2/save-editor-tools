using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ElectronicArts;

namespace SSX
{
    public class SSXGameSave
    {
        public struct SaveHeader
        {
            public string SaveName;
            public uint Checksum1;
            public uint Checksum2;
        }
        private EndianIO IO;

        private SaveHeader Header;

        private long PlayerDbStart;

        public int Credits
        {
            get;
            set;
        }

        private EndianIO SaveIO;
        private SSXSaveStream SaveStream;
        private byte[] PlayerData;

        public SSXGameSave(EndianIO IO)
        {
            if (IO != null)
            {
                this.IO = IO;
            }
            this.VerifySave();
            this.Read();
        }

        public void Save()
        {
            this.WriteMainPlayerDb();

            this.IO.SeekTo(PlayerDbStart);
            this.IO.Out.Write(SaveStream.ToArray());

            this.FixChecksums();
        }

        private void FixChecksums()
        {
            this.IO.In.SeekTo(0x1C);
            byte[] SaveData = IO.In.ReadBytes(IO.Stream.Length - 0x1C);            
            uint sum2 = EACRC32.Calculate_Alt(SaveData, 0x04, SaveData.Length - 4, 0x00);

            this.IO.Out.SeekTo(0x1C);
            this.IO.Out.Write(sum2);

            SaveData.WriteInt32(0x00, (int)sum2);

            uint sum1 = EACRC32.Calculate_Alt2(SaveData, SaveData.Length, 0x00);
            this.IO.Out.SeekTo(0x10);
            this.IO.Out.Write(sum1); 
        }

        private void VerifySave()
        {
            Header.SaveName = IO.In.ReadAsciiString(0x10);
            Header.Checksum1 = IO.In.ReadUInt32();
            IO.In.BaseStream.Position += 0x08;
            Header.Checksum2 = IO.In.ReadUInt32();

            this.IO.In.SeekTo(0x1C);
            byte[] SaveData = IO.In.ReadBytes(IO.Stream.Length - 0x1C);

            uint sum1 = EACRC32.Calculate_Alt2(SaveData, SaveData.Length, 0x00);
            uint sum2 = EACRC32.Calculate_Alt(SaveData, 0x04, SaveData.Length - 4, 0x00);

            if ((sum1 != Header.Checksum1) || (sum2 != Header.Checksum2))
                throw new Exception("corrupt save data detected.");
        }

        private void Read()
        {
            this.IO.In.SeekTo(0x20);
            int GlobalEntryCount = IO.In.ReadInt32();
            for (int x = 0; x < GlobalEntryCount; x++)
            {
                int MaxSectionLen = IO.In.ReadInt32();
                int SectionLen = IO.In.ReadInt32();

                switch(x)
                {
                    case 0:
                        this.ReadMainPlayerDb(SectionLen);
                    break;
                }
            }
        }

        private void ReadMainPlayerDb(int Length)
        {
            PlayerDbStart = IO.Stream.Position;

            SaveStream = new SSXSaveStream(IO.In.ReadBytes(Length));

            EndianIO io = new EndianIO(SaveStream, EndianType.BigEndian, true);

            // header = 0x10 bytes
            byte[] Header = io.In.ReadBytes(0x10);
            if (Header[0x00] != 0x44 || Header[0x01] != 0x42 || Header[0x02] != 0x00
                || Header[0x03] != 0x08 || Header[0x04] != 0x01)
            {
                throw new Exception("invalid player database buffer detected.");
            }

            int len = Header.ReadInt32(0x08);
            int start = Header.ReadInt32(0x0C);

            int subEntryCount = io.In.ReadInt32();
            SaveStream.VerifyBuffer();

            byte[] EntryTable = io.In.ReadBytes((subEntryCount << 3));
            SaveStream.VerifyBuffer();

            byte[] test1 = io.In.ReadBytes(0x20);
            SaveStream.VerifyBuffer();

            byte[] test2 = io.In.ReadBytes(Horizon.Functions.Global.ROTL32(test1[0x18], 0x4));

            PlayerData = io.In.ReadBytes(0x64 * 0x0C);
            SaveStream.VerifyBuffer();

            Credits = PlayerData.ReadInt32(0x00) ^ (-2147483648);
            SaveIO = io;
        }

        private void WriteMainPlayerDb()
        {
            PlayerData.WriteInt32(0x00, (this.Credits | -2147483648));

            SaveIO.SeekTo(0x00);
            SaveIO.In.ReadBytes(0x10);
            int subEntryCount = SaveIO.In.ReadInt32();
            SaveStream.VerifyBuffer();

            byte[] EntryTable = SaveIO.In.ReadBytes((subEntryCount << 3));
            SaveStream.VerifyBuffer();

            byte[] test1 = SaveIO.In.ReadBytes(0x20);
            SaveStream.VerifyBuffer();

            byte[] test2 = SaveIO.In.ReadBytes(Horizon.Functions.Global.ROTL32(test1[0x18], 0x4));
            SaveIO.Out.Write(PlayerData);

            SaveStream.FlushSecBuffer();
        }
    }

    public class SSXSaveStream : Stream
    {
        private bool StreamError { get { return (IO.Stream != null && IO.Stream.Length > 0x00); } }
        public override bool CanSeek { get { return IO.Stream.CanSeek && StreamError; } }
        public override bool CanRead { get { return IO.Stream.CanWrite && StreamError; } }
        public override bool CanWrite { get { return IO.Stream.CanWrite & StreamError; } }

        private long _Length;
        private long _Position;

        private long ChecksumBufLen = 0x00;

        public override long Length { get { return _Length; } }
        public override long  Position
        {
	        get 
	        {
                return _Position;
	        }
	          set 
	        {
                _Position = value;
                IO.Stream.Position = _Position;
	        }
        }

        private EndianIO IO;

        public uint Checksum1;
        public uint Checksum2;

        public SSXSaveStream(byte[] savedata)
        {
            IO = new EndianIO(savedata, EndianType.BigEndian, true);

            _Length = IO.Length;

            _Position = 0x00;

            Checksum1 = 0xFFFFFFFF;
            Checksum2 = 0xFFFFFFFF;

            ChecksumBufLen = 0x00;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return (_Position = IO.Stream.Seek(offset, origin));
        }

        public override void SetLength(long value)
        {
            IO.Stream.SetLength(value);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int readlen = IO.Stream.Read(buffer, offset, count);

            Checksum1 = EACRC32.Calculate_AltNoXor(buffer, readlen, Checksum1);
            Checksum2 = EACRC32.Calculate_AltNoXor(buffer, readlen, Checksum2);

            _Position += readlen;

            return readlen;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            IO.Stream.Write(buffer, offset, count);

            Checksum1 = EACRC32.Calculate_AltNoXor(buffer, count, Checksum1);
            Checksum2 = EACRC32.Calculate_AltNoXor(buffer, count, Checksum2);

            _Position += count;
        }

        public override void Flush()
        {
            IO.Stream.Flush();
        }

        public override void Close()
        {
            IO.Close();
        }

        public void VerifyBuffer()
        {
            if (IO.In.ReadUInt32() != Checksum1)
                throw new Exception("SSX: invalid save data detected from disk stream.");

            Checksum1 = 0xFFFFFFFF;
            ChecksumBufLen += 0x04;
            _Position += 0x04;
        }

        public void FlushSecBuffer()
        {
            IO.Out.Write(Checksum1);

            Checksum1 = 0xFFFFFFFF;

            _Position = + 0x04;
        }
    }
}