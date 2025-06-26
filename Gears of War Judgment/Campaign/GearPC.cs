using System;
using System.Collections.Generic;
using System.IO;

namespace Horizon.PackageEditors.Gears_of_War_Judgment.Campaign
{
    internal class GearPC : ActorRecord
    {
        internal byte[] SavedGuid;
        internal Name CheckpointName;
        internal float PawnHealthPct;
        internal string PawnClassName;
        internal string RidableClassName;
        internal byte[] RidableSavedGuid;
        internal string MutatedClassName;
        internal string PawnPathName;
        internal Vector Location;
        internal Rotator Rotation;
        internal string BasePathName;
        internal bool WeaponHolstered;
        internal byte TeamIndex;
        internal string SquadFormationName;
        internal string SlotPathName;
        internal int SlotID;
        internal List<InventoryRecord> InventoryRecords;
        internal List<InventoryRecord> KismetInventoryRecords;
        internal List<ObjectiveInfo> Objectives;
        internal string CratePathName;
        internal bool SilverbackSpotlightEnabled;
        internal CheckpointMusicRecord Music;
        internal string[] MountedFaceFX; //3
        internal List<StoredKismetVariable> StoredKismetVariables; //read int32
        internal string[] KismetAnimSets; //4
        internal string KismetAnimTree;
        internal float GUDSFrequencyMultiplier;
        internal int Score;
        internal int TeamScore;
        internal int Kills;
        internal int Deaths;
        internal int Revives;
        internal int PlayerSlotIndex;
        internal byte[] CarriedPawnGuid;
        internal string[] AttachmentClassNames; //4
        internal string GameOverReason;
        internal List<int> MoraleBoostsStarted;
        internal List<int> MoraleBoostsCompleted;
        internal int ValorPoints;
        internal StarScoreData TotalStarScoreDataWhenJIP;
        internal int[] GatingStats; //16

        protected override void Deserialize(EndianIO io)
        {
            SavedGuid = io.In.ReadBytes(16);
            CheckpointName = new Name(io);
            PawnHealthPct = io.In.ReadSingle();
            PawnClassName = ReadString(io);
            RidableClassName = ReadString(io);
            RidableSavedGuid = io.In.ReadBytes(16);
            MutatedClassName = ReadString(io);
            PawnPathName = ReadString(io);
            Location = new Vector(io);
            Rotation = new Rotator(io);
            BasePathName = ReadString(io);
            WeaponHolstered = io.In.ReadBoolean();
            TeamIndex = io.In.ReadByte();
            SquadFormationName = ReadString(io);
            SlotPathName = ReadString(io);
            SlotID = io.In.ReadInt32();

            int numInventory = io.In.ReadInt32();
            InventoryRecords = new List<InventoryRecord>(numInventory);
            while (numInventory-- != 0)
                InventoryRecords.Add(new InventoryRecord(io));

            numInventory = io.In.ReadInt32();
            KismetInventoryRecords = new List<InventoryRecord>(numInventory);
            while (numInventory-- != 0)
                KismetInventoryRecords.Add(new InventoryRecord(io));

            int numObjectives = io.In.ReadInt32();
            Objectives = new List<ObjectiveInfo>(numObjectives);
            while (numObjectives-- != 0)
                Objectives.Add(new ObjectiveInfo(io));

            CratePathName = ReadString(io);
            SilverbackSpotlightEnabled = io.In.ReadBoolean();
            Music = new CheckpointMusicRecord(io);

            MountedFaceFX = new string[3];
            for (int x = 0; x < 3; x++)
                MountedFaceFX[x] = ReadString(io);

            int numKismetVars = io.In.ReadInt32();

            if (numKismetVars != 0)
                throw new Exception("GearPC: Unsupported Kismet variable. Please send this file to a developer.");

            StoredKismetVariables = new List<StoredKismetVariable>(numKismetVars);
            while (numKismetVars-- != 0)
                StoredKismetVariables.Add(new StoredKismetVariable(io));

            KismetAnimSets = new string[4];
            for (int x = 0; x < 4; x++)
                KismetAnimSets[x] = ReadString(io);

            KismetAnimTree = ReadString(io);
            GUDSFrequencyMultiplier = io.In.ReadSingle();
            Score = io.In.ReadInt32();
            TeamScore = io.In.ReadInt32();
            Kills = io.In.ReadInt32();
            Deaths = io.In.ReadInt32();
            Revives = io.In.ReadInt32();
            PlayerSlotIndex = io.In.ReadInt32();
            CarriedPawnGuid = io.In.ReadBytes(16);

            AttachmentClassNames = new string[4];
            for (int x = 0; x < 4; x++)
                AttachmentClassNames[x] = ReadString(io);

            GameOverReason = ReadString(io);

            int numBoosts = io.In.ReadInt32();
            MoraleBoostsStarted = new List<int>(numBoosts);
            while (numBoosts-- != 0)
                MoraleBoostsStarted.Add(io.In.ReadInt32());

            numBoosts = io.In.ReadInt32();
            MoraleBoostsCompleted = new List<int>(numBoosts);
            while (numBoosts-- != 0)
                MoraleBoostsCompleted.Add(io.In.ReadInt32());

            ValorPoints = io.In.ReadInt32();
            TotalStarScoreDataWhenJIP = new StarScoreData(io);

            GatingStats = new int[16];
            for (int x = 0; x < 16; x++)
                GatingStats[x] = io.In.ReadInt32();
        }

        protected override void Serialize(EndianIO io)
        {
            if (SavedGuid.Length != 16)
                throw new Exception("GearPC: Invalid GUID length.");

            io.Out.Write(SavedGuid);
            CheckpointName.Write(io);
            io.Out.Write(PawnHealthPct);
            WriteString(io, PawnClassName);
            WriteString(io, RidableClassName);

            if (RidableSavedGuid.Length != 16)
                throw new Exception("GearPC: Invalid GUID length.");

            io.Out.Write(RidableSavedGuid);
            WriteString(io, MutatedClassName);
            WriteString(io, PawnPathName);
            Location.Write(io);
            Rotation.Write(io);
            WriteString(io, BasePathName);
            io.Out.Write(WeaponHolstered);
            io.Out.Write(TeamIndex);
            WriteString(io, SquadFormationName);
            WriteString(io, SlotPathName);
            io.Out.Write(SlotID);

            io.Out.Write(InventoryRecords.Count);
            foreach (var i in InventoryRecords)
                i.Write(io);

            io.Out.Write(KismetInventoryRecords.Count);
            foreach (var i in KismetInventoryRecords)
                i.Write(io);

            io.Out.Write(Objectives.Count);
            foreach (var o in Objectives)
                o.Write(io);

            WriteString(io, CratePathName);
            io.Out.Write(SilverbackSpotlightEnabled);
            Music.Write(io);

            foreach (var s in MountedFaceFX)
                WriteString(io, s);

            io.Out.Write(StoredKismetVariables.Count);
            foreach (var v in StoredKismetVariables)
                v.Write(io);

            foreach (var s in KismetAnimSets)
                WriteString(io, s);

            WriteString(io, KismetAnimTree);
            io.Out.Write(GUDSFrequencyMultiplier);
            io.Out.Write(Score);
            io.Out.Write(TeamScore);
            io.Out.Write(Kills);
            io.Out.Write(Deaths);
            io.Out.Write(Revives);
            io.Out.Write(PlayerSlotIndex);

            if (CarriedPawnGuid.Length != 16)
                throw new Exception("GearPC: Invalid GUID length.");

            io.Out.Write(CarriedPawnGuid);

            foreach (var s in AttachmentClassNames)
                WriteString(io, s);

            WriteString(io, GameOverReason);

            io.Out.Write(MoraleBoostsStarted.Count);
            foreach (var m in MoraleBoostsStarted)
                io.Out.Write(m);

            io.Out.Write(MoraleBoostsCompleted.Count);
            foreach (var m in MoraleBoostsCompleted)
                io.Out.Write(m);

            io.Out.Write(ValorPoints);
            TotalStarScoreDataWhenJIP.Write(io);

            foreach (var i in GatingStats)
                io.Out.Write(i);
        }
    }

    internal class StoredKismetVariable
    {
        internal Name VariableName;
        //internal string StringValue;
        //internal float FloatValue;
        //internal Object ObjectValue;
        //internal int IntValue;
        //internal bool BoolValue;

        internal StoredKismetVariable(EndianIO io)
        {
            VariableName = new Name(io);

        }

        internal void Write(EndianIO io)
        {
            VariableName.Write(io);

        }
    }

    internal struct ObjectiveInfo
    {
        internal Name ObjectiveName;
        internal string ObjectiveDesc;
        internal string ObjectiveDescVars;
        internal string NonLocalizedDesc;
        internal bool ShowInList;
        internal bool Completed;
        internal bool Failed;
        internal float UpdatedTime;
        internal bool NotifyPlayer;
        internal bool LocatorWasSent;
        internal bool AlwaysDraw;
        //internal GearObjectiveLocator[] Locators; //10
        internal string[] LocatorPathNames; //10

        internal ObjectiveInfo(EndianIO io)
        {
            ObjectiveName = new Name(io);
            ObjectiveDesc = io.In.ReadString(io.In.ReadInt32());
            ObjectiveDescVars = io.In.ReadString(io.In.ReadInt32());
            NonLocalizedDesc = io.In.ReadString(io.In.ReadInt32());
            ShowInList = io.In.ReadBoolean();
            Completed = io.In.ReadBoolean();
            Failed = io.In.ReadBoolean();
            UpdatedTime = io.In.ReadSingle();
            NotifyPlayer = io.In.ReadBoolean();
            LocatorWasSent = io.In.ReadBoolean();
            AlwaysDraw = io.In.ReadBoolean();

            LocatorPathNames = new string[10];
            for (int x = 0; x < 10; x++)
                LocatorPathNames[x] = io.In.ReadString(io.In.ReadInt32());
        }

        internal void Write(EndianIO io)
        {
            ObjectiveName.Write(io);

            int t;

            if (ObjectiveDesc.Length == 0)
                io.Out.Write(0);
            else
            {
                t = ObjectiveDesc.Length + 1;
                io.Out.Write(t);
                io.Out.WriteAsciiString(ObjectiveDesc, t);
            }

            if (ObjectiveDescVars.Length == 0)
                io.Out.Write(0);
            else
            {
                t = ObjectiveDescVars.Length + 1;
                io.Out.Write(t);
                io.Out.WriteAsciiString(ObjectiveDescVars, t);
            }

            if (NonLocalizedDesc.Length == 0)
                io.Out.Write(0);
            else
            {
                t = NonLocalizedDesc.Length + 1;
                io.Out.Write(t);
                io.Out.WriteAsciiString(NonLocalizedDesc, t);
            }

            io.Out.Write(ShowInList);
            io.Out.Write(Completed);
            io.Out.Write(Failed);
            io.Out.Write(UpdatedTime);
            io.Out.Write(NotifyPlayer);
            io.Out.Write(LocatorWasSent);
            io.Out.Write(AlwaysDraw);

            foreach (var s in LocatorPathNames)
            {
                if (s.Length == 0)
                    io.Out.Write(0);
                else
                {
                    t = s.Length + 1;
                    io.Out.Write(t);
                    io.Out.WriteAsciiString(s, t);
                }
            }
        }
    }

    internal struct CheckpointMusicRecord //extends MusicTrackStruct
    {
        internal Name TrackType;
        //internal string TheSoundCue;
        internal bool AutoPlay;
        internal bool PersistentAcrossLevels;
        internal float FadeInTime;
        internal float FadeInVolumeLevel;
        internal float FadeOutTime;
        internal float FadeOutVolumeLevel;
        //internal string TheSoundCuePath;

        internal CheckpointMusicRecord(EndianIO io)
        {
            TrackType = new Name(io);
            //TheSoundCue = io.In.ReadString(io.In.ReadInt32());
            AutoPlay = io.In.ReadBoolean();
            PersistentAcrossLevels = io.In.ReadBoolean();
            FadeInTime = io.In.ReadSingle();
            FadeInVolumeLevel = io.In.ReadSingle();
            FadeOutTime = io.In.ReadSingle();
            FadeOutVolumeLevel = io.In.ReadSingle();
            //TheSoundCuePath = io.In.ReadString(io.In.ReadInt32());
        }

        internal void Write(EndianIO io)
        {
            TrackType.Write(io);

            //var t = TheSoundCue.Length + 1;
            //io.Out.Write(t);
            //io.Out.WriteAsciiString(TheSoundCue, t);

            io.Out.Write(AutoPlay);
            io.Out.Write(PersistentAcrossLevels);
            io.Out.Write(FadeInTime);
            io.Out.Write(FadeInVolumeLevel);
            io.Out.Write(FadeOutTime);
            io.Out.Write(FadeOutVolumeLevel);

            //var t = TheSoundCuePath.Length + 1;
            //io.Out.Write(t);
            //io.Out.WriteAsciiString(TheSoundCuePath, t);
        }
    }

    internal struct StarScoreData
    {
        internal float TotalStarScore;
        internal float[] TotalStarScoreDetails; //8

        internal StarScoreData(EndianIO io)
        {
            TotalStarScore = io.In.ReadSingle();

            TotalStarScoreDetails = new float[8];
            for (int x = 0; x < 8; x++)
                TotalStarScoreDetails[x] = io.In.ReadSingle();
        }

        internal void Write(EndianIO io)
        {
            io.Out.Write(TotalStarScore);
            foreach (var t in TotalStarScoreDetails)
                io.Out.Write(t);
        }
    }

    /*internal struct SoundCue
    {
        internal string FaceFXAnimName;
        internal int FaceFXAnimSetRef;
        internal string FaceFXGroupName;
        internal int MaxConcurrentPlayCount;
        internal float PitchMultiplier;
        internal Name SoundClass;
        internal float VolumeMultiplier;
    }*/
}
