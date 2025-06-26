using System;
using System.Collections.Generic;
using System.IO;

namespace UnrealEngine
{
    class UnrealData
    {
        public UnrealData(byte[] data)
            : this(new EndianIO(new MemoryStream(data), EndianType.BigEndian, true))
        {

        }

        public UnrealData(EndianIO IO)
        {
            this.Segments = new List<byte[]>();

            while (IO.Stream.Position != IO.Stream.Length && IO.In.ReadUInt32() == Magic)
                Segments.Add(ReadSegment(IO));
        }

        private readonly List<byte[]> Segments;
        private const uint Magic = 0x9E2A83C1;
        private const int SegmentSize = 0x20000;

        public EndianIO GetSingleSegmentIO(int segmentIndex)
        {
            EndianIO IO = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
            IO.Out.Write(Segments[segmentIndex]);
            IO.Stream.Position = 0x00;
            return IO;
        }

        public EndianIO GetSegmentIO()
        {
            var io = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
            foreach (var seg in Segments)
                io.Out.Write(seg);
            return io;
        }

        public EndianIO GetSegmentIO(int startingSegmentIndex)
        {
            EndianIO IO = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
            for (int x = startingSegmentIndex; x < Segments.Count; x++)
                IO.Out.Write(Segments[x]);
            IO.Stream.Position = 0x00;
            return IO;
        }

        public void WriteSingleSegment(int segmentIndex, byte[] segmentData)
        {
            if (segmentData.Length > SegmentSize)
                throw new Exception(string.Format("Unreal: Segment overflow [{0x0:X2}]", segmentIndex));

            this.Segments[segmentIndex] = segmentData;
        }

        public void WriteSplitSegment(int startingIndex, byte[] segmentData)
        {
            if (startingIndex >= Segments.Count)
                throw new Exception(string.Format("Unreal: Segment index out of bounds [0x{0:X4}]", startingIndex));

            MemoryStream ms = new MemoryStream(segmentData, false);
            byte[] buffer = new byte[SegmentSize];
            int read;
            while ((read = ms.Read(buffer, 0, SegmentSize)) != 0)
            {
                if (startingIndex == Segments.Count)
                    Segments.Add(buffer.ReadBytes(0, read));
                else
                    Segments[startingIndex] = buffer.ReadBytes(0, read);
                startingIndex++;
            }

            if (Segments.Count <= startingIndex)
                return;

            Segments.RemoveRange(startingIndex, Segments.Count - startingIndex);
        }

        public byte[] ToArray(out int[] dataSizes)
        {
            dataSizes = new int[Segments.Count];
            EndianIO IO = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);
            byte[] segData;
            for (int x = 0; x < Segments.Count; x++)
            {
                IO.Out.Write(Magic);
                segData = CreateSegment(Segments[x]);
                IO.Out.Write(segData);
                dataSizes[x] = segData.Length + 4;
            }
            byte[] ret = IO.ToArray();
            IO.Close();
            return ret;
        }

        public byte[] ToArray()
        {
            int[] dataSizes;
            return this.ToArray(out dataSizes);
        }

        private static byte[] ReadSegment(EndianIO IO)
        {
            // Skip the max segment size
            IO.Stream.Position += 0x04;

            int compressedSize = IO.In.ReadInt32();
            int decompressedSize = IO.In.ReadInt32();

            if (compressedSize != IO.In.ReadInt32() || decompressedSize != IO.In.ReadInt32())
                throw new Exception("Unreal: Segment header corrupted");

            MemoryStream ms = new MemoryStream();

            byte[] compressedData = IO.In.ReadBytes(compressedSize);

            int out_len = LZO.LZO1X.Decompress(compressedData, ms);
            if (out_len != decompressedSize)
                throw new Exception("Unreal: Invalid segment detected");

            byte[] ret = ms.ToArray();

            ms.Close();

            return ret;
        }

        public static byte[] CreateSegment(byte[] decompressedData)
        {
            EndianIO IO = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);

            IO.Out.Write(SegmentSize);

            MemoryStream compressionStream = new MemoryStream();

            int out_len = LZO.LZO1X.Compress(decompressedData, (uint)decompressedData.Length, compressionStream);

            IO.Out.Write(out_len);
            IO.Out.Write(decompressedData.Length);
            IO.Out.Write(out_len);
            IO.Out.Write(decompressedData.Length);

            IO.Out.Write(compressionStream.ToArray());
            compressionStream.Close();

            byte[] segData = IO.ToArray();
            IO.Close();
            return segData;
        }
    }
}
