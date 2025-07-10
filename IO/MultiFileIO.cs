using System.Collections.Generic;
using Horizon.Library.Systems.FATX;

namespace System.IO
{
    public sealed class MultiFileIO : EndianIO
    {
        private Stream[] IO;
        private long[] sizeUpTill;
        private int currentFileIndex = -1;
        private bool FragmentMode = false;

        public MultiFileIO(List<string> FilePaths, EndianType EndianType)
            : this(FilePaths, EndianType, false) { }
        public MultiFileIO(List<string> filePaths, EndianType endianType, bool openStream)
            : base(filePaths[0], endianType)
        {
            IO = new FileStream[filePaths.Count];
            sizeUpTill = new long[filePaths.Count + 1];
            for (int x = 0; x < filePaths.Count; x++)
            {
                IO[x] = new FileStream(
                    FatxDeviceControl.CreateFile(filePaths[x], FileAccess.ReadWrite, FileShare.ReadWrite,
                    IntPtr.Zero, FileMode.Open, 0xA0000040, IntPtr.Zero), FileAccess.ReadWrite);
                sizeUpTill[x + 1] = sizeUpTill[x] + IO[x].Length;
            }
            if (openStream)
                Open();
        }
        public MultiFileIO(List<string> filePaths, EndianType endianType, bool openStream, bool IsInFragmentMode)
            : base(filePaths[0], endianType)
        {
            this.FragmentMode = IsInFragmentMode;

            IO = new FileStream[filePaths.Count];
            sizeUpTill = new long[filePaths.Count + 1];
            for (int x = 0; x < filePaths.Count; x++)
            {
                IO[x] = new FileStream(
                    FatxDeviceControl.CreateFile(filePaths[x], FileAccess.ReadWrite, FileShare.ReadWrite,
                    IntPtr.Zero, FileMode.Open, 0xA0000040, IntPtr.Zero), FileAccess.ReadWrite);
                sizeUpTill[x + 1] = sizeUpTill[x] + IO[x].Length;
            }
            if (openStream)
                Open();
        }

        public void openFile(int fileIndex)
        {
            if (fileIndex != currentFileIndex)
            {
                currentFileIndex = fileIndex;
                base.Stream = IO[currentFileIndex];
                base.In = new MultiEndianReader(this);
                base.Out = new MultiEndianWriter(this);
                base.Stream.Position = 0;
                Opened = true;
            }
        }

        public override void Open()
        {
            openFile(0);
        }

        public override long Position
        {
            get
            {
                if (FragmentMode)
                {
                    return base.Stream.Position;
                }
                else
                {
                    return sizeUpTill[currentFileIndex] + base.Stream.Position;
                }
            }
            set
            {
                if (FragmentMode)
                {
                    base.Stream.Position = value;
                }
                else
                {
                    int newIndex = getFileIndexFromPosition(value);
                    if (newIndex != currentFileIndex)
                    {
                        openFile(newIndex);
                        currentFileIndex = newIndex;
                    }
                    base.Stream.Position = value - sizeUpTill[currentFileIndex];
                }
            }
        }

        private int getFileIndexFromPosition(long position)
        {
            for (int x = 0; x < IO.Length; x++)
                if (sizeUpTill[x + 1] > position)
                    return x;
            throw new IOException("Cannot seek passed the end of the stream.");
        }

        public override long Length
        {
            get
            {
                return sizeUpTill[sizeUpTill.Length - 1];
            }
        }

        public override void SeekTo(object position)
        {
            Position = Convert.ToInt64(position);
        }

        public override void SeekTo(long seekLength, SeekOrigin orgin)
        {
            switch (orgin)
            {
                case SeekOrigin.Begin:
                    Position = seekLength;
                    break;
                case SeekOrigin.Current:
                    Position += seekLength;
                    break;
                case SeekOrigin.End:
                    Position = Length - seekLength;
                    break;
            }
        }

        private int bytesLeftInCurrentFile
        {
            get
            {
                return (int)(base.Stream.Length - base.Stream.Position);
            }
        }

        public int Read(byte[] buffer, int index, int count)
        {
            int bytesToRead = count;
            while (bytesToRead != 0)
            {
                if (base.Stream.Length == base.Stream.Position)
                    openFile(currentFileIndex + 1);
                int readBytes = bytesToRead > bytesLeftInCurrentFile
                    ? bytesLeftInCurrentFile : bytesToRead;
                base.In.BaseStream.Read(buffer, index, readBytes);
                bytesToRead -= readBytes;
                index += readBytes;
            }
            return count;
        }

        public void Write(byte[] buffer, int index, int count)
        {
            while (count != 0)
            {
                if (base.Stream.Length == base.Stream.Position)
                    openFile(currentFileIndex + 1);
                int writeBytes = count > bytesLeftInCurrentFile
                    ? bytesLeftInCurrentFile : count;
                base.Out.BaseStream.Write(buffer, index, writeBytes);
                count -= writeBytes;
                index += writeBytes;
            }
        }

        public sealed class MultiEndianReader : EndianReader
        {
            private MultiFileIO IoRef;
            public MultiEndianReader(MultiFileIO multiIoRef)
                : base(multiIoRef.Stream, multiIoRef.EndianType)
            {
                IoRef = multiIoRef;
            }

            public override void SeekTo(object offset, SeekOrigin SeekOrigin)
            {
                IoRef.SeekTo(Convert.ToInt64(offset), SeekOrigin);
            }

            public override int Read(byte[] buffer, int index, int count)
            {
                return IoRef.Read(buffer, index, count);
            }
        }

        public sealed class MultiEndianWriter : EndianWriter
        {
            private MultiFileIO IoRef;
            public MultiEndianWriter(MultiFileIO multiIoRef)
                : base(multiIoRef.Stream, multiIoRef.EndianType)
            {
                IoRef = multiIoRef;
            }

            public override void SeekTo(object Position)
            {
                IoRef.SeekTo(Convert.ToInt64(Position), SeekOrigin.Begin);
            }

            public override void Write(byte[] buffer)
            {
                IoRef.Write(buffer, 0, buffer.Length);
            }

            public override void Write(byte[] Buffer, object BufferLength)
            {
                IoRef.Write(Buffer, 0, Convert.ToInt32(BufferLength));
            }

            public override void Write(byte[] Buffer, object offset, object BufferLength)
            {
                IoRef.Write(Buffer, Convert.ToInt32(offset), Convert.ToInt32(BufferLength));
            }

            public override void Write(byte[] buffer, int index, int count)
            {
                IoRef.Write(buffer, index, count);
            }

            public override void Write(byte[] Data, EndianType EndianType)
            {
                if (EndianType == EndianType.BigEndian)
                    Array.Reverse(Data);
                IoRef.Write(Data, 0, Data.Length);
            }
        }
    }
}
