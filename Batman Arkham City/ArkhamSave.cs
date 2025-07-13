using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnrealEngine;

namespace Horizon.PackageEditors.Batman_Arkham_City
{
    class ArkhamSave
    {
        private int UnrealEngineBuild;
        private const int BlockAmount = 4;
        private EndianIO IO;
        private int SaveNumber, ArkhamBuild;
        public UnrealData Data;

        public ArkhamSave(EndianIO IO)
        {
            this.IO = IO;

            UnrealEngineBuild = IO.In.ReadInt32();

            ArkhamBuild = IO.In.ReadInt32();
            SaveNumber = IO.In.ReadInt32();

            // Skip the block lengths
            IO.Stream.Position += 0x1C;

            Data = new UnrealData(IO);
        }

        public void Save()
        {
            IO.Stream.Position = 0x00;
            IO.Out.Write(UnrealEngineBuild);
            IO.Out.Write(ArkhamBuild);
            IO.Out.Write(++SaveNumber);

            int[] blockSizes;
            byte[] blockData = Data.ToArray(out blockSizes);

            IO.Out.Write(blockSizes[0]);
            IO.Out.Write(blockSizes[1]);
            IO.Out.Write(BlockAmount);
            for (int x = 2; x < blockSizes.Length; x++)
                IO.Out.Write(blockSizes[x]);
            IO.Stream.Position = 0x28;

            IO.Out.Write(blockData);
        }
    }
}
