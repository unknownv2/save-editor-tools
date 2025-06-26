using System;
using System.Collections.Generic;
using System.IO;

namespace Horizon.PackageEditors.Gears_of_War_Judgment.Campaign
{
    enum PerceptionMood : byte
    {
        None,
        Normal,
        Alert,
        Unaware,
        Oblivious
    }

    enum CombatMood : byte
    {
        None,
        Normal,
        Passive,
        Aggressive,
        Ambush
    }

    class GearAI : ActorRecord
    {
        internal byte[] SavedGuid;
        internal float PawnHealthPct;
        internal string PawnClassName;
        internal string RidableClassName;
        internal string PawnPathName;
        internal string MutatedClassName;
        internal Vector Location;
        internal Rotator Rotation;
        internal string BasePathName;
        internal byte TeamIndex;
        internal string SquadName;
        internal PerceptionMood PerceptionMood;
        internal CombatMood CombatMood;
        internal string SlotPathName;
        internal int SlotID;
        internal List<InventoryRecord> InventoryRecords;
        internal bool IsLeader;
        internal bool CanDBNO;
        internal bool ForceShowInTaccom;
        internal bool AllowCombatTransitions_Kismet;
        internal bool AllowCombatTransitions_Command;
        internal bool WeaponHolstered;
        internal string MoveActionPathName;
        internal string SquadRoutePathName;
        internal string[] MountedFaceFX; //3
        internal string[] KismetAnimSets; //4
        internal string KismetAnimTree;
        internal ReaverCheckpointData ReaverRecord;
        internal JackSpotlightSetting JackSpotlightSetting;
        internal bool JackSpotlightOn;
        internal bool JackHidden;
        internal string DrivenTurretPathName;
        internal int PlayerSlotIndex;
        internal string PlayerName;
        internal int CorpserEyeHealthLeft;
        internal int CorpserEyeHealthRight;
        internal int CorpserEyeHealthLeftMid;
        internal int CorpserEyeHealthRightMid;
        internal int LambentBerserkerPhase;

        protected override void Deserialize(EndianIO io)
        {
            SavedGuid = io.In.ReadBytes(16);
            PawnHealthPct = io.In.ReadSingle();
            PawnClassName = ReadString(io);
            RidableClassName = ReadString(io);
            PawnPathName = ReadString(io);
            MutatedClassName = ReadString(io);
            Location = new Vector(io);
            Rotation = new Rotator(io);
            BasePathName = ReadString(io);
            TeamIndex = io.In.ReadByte();
            SquadName = ReadString(io);
            PerceptionMood = (PerceptionMood)io.In.ReadByte();
            CombatMood = (CombatMood)io.In.ReadByte();
            SlotPathName = ReadString(io);
            SlotID = io.In.ReadInt32();

            int numInventory = io.In.ReadInt32();
            InventoryRecords = new List<InventoryRecord>(numInventory);
            while (numInventory-- != 0)
                InventoryRecords.Add(new InventoryRecord(io));

            IsLeader = io.In.ReadBoolean();
            CanDBNO = io.In.ReadBoolean();
            ForceShowInTaccom = io.In.ReadBoolean();
            AllowCombatTransitions_Kismet = io.In.ReadBoolean();
            AllowCombatTransitions_Command = io.In.ReadBoolean();
            WeaponHolstered = io.In.ReadBoolean();
            MoveActionPathName = ReadString(io);
            SquadRoutePathName = ReadString(io);

            MountedFaceFX = new string[3];
            for (int x = 0; x < 3; x++)
                MountedFaceFX[x] = ReadString(io);

            KismetAnimSets = new string[4];
            for (int x = 0; x < 4; x++)
                KismetAnimSets[x] = ReadString(io);

            KismetAnimTree = ReadString(io);
            ReaverRecord = new ReaverCheckpointData(io);
            JackSpotlightSetting = (JackSpotlightSetting)io.In.ReadByte();
            JackSpotlightOn = io.In.ReadBoolean();
            JackHidden = io.In.ReadBoolean();
            DrivenTurretPathName = ReadString(io);
            PlayerSlotIndex = io.In.ReadInt32();
            PlayerName = ReadString(io);
            CorpserEyeHealthLeft = io.In.ReadInt32();
            CorpserEyeHealthRight = io.In.ReadInt32();
            CorpserEyeHealthLeftMid = io.In.ReadInt32();
            CorpserEyeHealthRightMid = io.In.ReadInt32();
            LambentBerserkerPhase = io.In.ReadInt32();
        }

        protected override void Serialize(EndianIO io)
        {
            if (SavedGuid.Length != 16)
                throw new Exception("GearAI: Invalid GUID length.");

            io.Out.Write(SavedGuid);
            io.Out.Write(PawnHealthPct);
            WriteString(io, PawnClassName);
            WriteString(io, RidableClassName);
            WriteString(io, PawnPathName);
            WriteString(io, MutatedClassName);
            Location.Write(io);
            Rotation.Write(io);
            WriteString(io, BasePathName);
            io.Out.Write(TeamIndex);
            WriteString(io, SquadName);
            io.Out.Write((byte)PerceptionMood);
            io.Out.Write((byte)CombatMood);
            WriteString(io, SlotPathName);
            io.Out.Write(SlotID);

            io.Out.Write(InventoryRecords.Count);
            foreach (var i in InventoryRecords)
                i.Write(io);

            io.Out.Write(IsLeader);
            io.Out.Write(CanDBNO);
            io.Out.Write(ForceShowInTaccom);
            io.Out.Write(AllowCombatTransitions_Kismet);
            io.Out.Write(AllowCombatTransitions_Command);
            io.Out.Write(WeaponHolstered);
            WriteString(io, MoveActionPathName);
            WriteString(io, SquadRoutePathName);

            foreach (var s in MountedFaceFX)
                WriteString(io, s);

            foreach (var s in KismetAnimSets)
                WriteString(io, s);

            WriteString(io, KismetAnimTree);
            ReaverRecord.Write(io);
            io.Out.Write((byte)JackSpotlightSetting);
            io.Out.Write(JackSpotlightOn);
            io.Out.Write(JackHidden);
            WriteString(io, DrivenTurretPathName);
            io.Out.Write(PlayerSlotIndex);
            WriteString(io, PlayerName);
            io.Out.Write(CorpserEyeHealthLeft);
            io.Out.Write(CorpserEyeHealthRight);
            io.Out.Write(CorpserEyeHealthLeftMid);
            io.Out.Write(CorpserEyeHealthRightMid);
            io.Out.Write(LambentBerserkerPhase);
        }
    }

    struct ReaverCheckpointData
    {
        internal bool HasData;
        internal string[] FlightPaths; //16
        internal string InitialFlightPath;
        internal float CurrentInterpTime;
        internal int CurrentFlightIndex;
        internal bool AllowLanding;
        internal Name InitialFlightPathGroupName;

        internal ReaverCheckpointData(EndianIO io)
        {
            HasData = io.In.ReadBoolean();

            FlightPaths = new string[16];
            for (int x = 0; x < 16; x++)
                FlightPaths[x] = io.In.ReadString(io.In.ReadInt32());

            InitialFlightPath = io.In.ReadString(io.In.ReadInt32());
            CurrentInterpTime = io.In.ReadSingle();
            CurrentFlightIndex = io.In.ReadInt32();
            AllowLanding = io.In.ReadBoolean();
            InitialFlightPathGroupName = new Name(io);
        }

        internal void Write(EndianIO io)
        {
            io.Out.Write(HasData);

            foreach (var s in FlightPaths)
            {
                var t = s.Length + 1;

                if (t == 1)
                    io.Out.Write(0);
                else
                {
                    io.Out.Write(t);
                    io.Out.WriteAsciiString(s, t);
                }
            }

            var x = InitialFlightPath.Length + 1;

            if (x == 1)
                io.Out.Write(0);
            else
            {
                io.Out.Write(x);
                io.Out.WriteAsciiString(InitialFlightPath, x);
            }

            io.Out.Write(CurrentInterpTime);
            io.Out.Write(CurrentFlightIndex);
            io.Out.Write(AllowLanding);

            InitialFlightPathGroupName.Write(io);
        }
    }
}
