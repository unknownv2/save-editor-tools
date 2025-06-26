using System.IO;

namespace Horizon.PackageEditors.Gears_of_War_Judgment.Campaign
{
    internal class InventoryRecord
    {
        internal string InventoryClassPath;
        internal int AmmoCount;
        internal int SpareAmmoCount;
        internal WeaponSlot CharacterSlot;
        internal bool IsActiveWeapon;

        internal InventoryRecord(WeaponSlot characterSlot)
        {
            CharacterSlot = characterSlot;
        }

        internal InventoryRecord(EndianIO io)
        {
            InventoryClassPath = io.In.ReadString(io.In.ReadInt32());
            AmmoCount = io.In.ReadInt32();
            SpareAmmoCount = io.In.ReadInt32();
            CharacterSlot = (WeaponSlot)io.In.ReadByte();
            IsActiveWeapon = io.In.ReadBoolean();
        }

        internal void Write(EndianIO io)
        {
            var t = InventoryClassPath.Length + 1;

            if (t == 1)
                io.Out.Write(0);
            {
                io.Out.Write(t);
                io.Out.WriteAsciiString(InventoryClassPath, t);
            }

            io.Out.Write(AmmoCount);
            io.Out.Write(SpareAmmoCount);
            io.Out.Write((byte)CharacterSlot);
            io.Out.Write(IsActiveWeapon);
        }
    }

    internal struct UEDateTime
    {
        internal int Year;
        internal int Month;
        internal int Day;
        internal int Second;

        internal UEDateTime(EndianIO io)
        {
            Year = io.In.ReadInt32();
            Month = io.In.ReadInt32();
            Day = io.In.ReadInt32();
            Second = io.In.ReadInt32();
        }

        internal void Write(EndianIO io)
        {
            io.Out.Write(Year);
            io.Out.Write(Month);
            io.Out.Write(Day);
            io.Out.Write(Second);
        }
    }

    internal struct Name
    {
        internal string String;
        internal int Int;

        internal Name(EndianIO io)
        {
            String = io.In.ReadString(io.In.ReadInt32());
            Int = io.In.ReadInt32();
        }

        internal void Write(EndianIO io)
        {
            if (String.Length == 0)
                io.Out.Write(0);
            else
            {
                io.Out.Write(String.Length + 1);
                io.Out.WriteAsciiString(String, String.Length + 1);
            }
            io.Out.Write(Int);
        }
    }

    internal struct Vector
    {
        internal float X;
        internal float Y;
        internal float Z;

        internal Vector(EndianIO io)
        {
            X = io.In.ReadSingle();
            Y = io.In.ReadSingle();
            Z = io.In.ReadSingle();
        }

        internal void Write(EndianIO io)
        {
            io.Out.Write(X);
            io.Out.Write(Y);
            io.Out.Write(Z);
        }
    }

    internal struct Rotator
    {
        internal int Pitch;
        internal int Yaw;
        internal int Roll;

        internal Rotator(EndianIO io)
        {
            Pitch = io.In.ReadInt32();
            Yaw = io.In.ReadInt32();
            Roll = io.In.ReadInt32();
        }

        internal void Write(EndianIO io)
        {
            io.Out.Write(Pitch);
            io.Out.Write(Yaw);
            io.Out.Write(Roll);
        }
    }

    internal enum WeaponSlot : byte
    {
        Secondary,
        Grenade,
        Primary1,
        Primary2
    }

    internal enum DifficultyLevel : byte
    {
        Casual,
        Normal,
        Hardcore,
        Insane
    }

    internal enum JackSpotlightSetting : byte
    {
        Default,
        Intervention,
        Outpost
    }

    // Nothing fun to edit

    /*struct AIDirectorVolume
    {
        bool Enabled;
        bool Blocked;
    }

    struct DynamicBlockingVolume
    {
        Vector Location;
        Rotator Rotation;
        bool CollideActors;
        bool BlockActors;
        bool NeedsReplication;

        internal DynamicBlockingVolume(EndianIO io)
        {
            Location = new Vector(io);
            Rotation = new Rotator(io);
            CollideActors = io.In.ReadBoolean();
            BlockActors = io.In.ReadBoolean();
            NeedsReplication = io.In.ReadBoolean();
        }

        internal void Write(EndianIO io)
        {
            Location.Write(io);
            Rotation.Write(io);
            io.Out.Write(CollideActors);
            io.Out.Write(BlockActors);
            io.Out.Write(NeedsReplication);
        }
    }

    struct Emitter
    {
        bool IsActive;

        
    }

    struct Trigger
    {
        bool CollideActors;
        bool ValorUnlock;
    }*/
}
