using System;
using System.IO;
using System.Security.Cryptography;
using Horizon.Functions;

namespace TheBehemoth
{
    internal class BattleBlockTheaterProfile
    {
        private readonly ulong Xuid;
        private readonly EndianIO IO;
        private EndianIO profileIO;

        internal int Gems { get; set; }
        internal int Yarn { get; set; }

        internal BattleBlockTheaterProfile(EndianIO io, ulong xuid)
        {
            IO = io;
            Xuid = xuid;
            Read();
        }

        private void Read()
        {
            IO.SeekTo(0);
            var keyObf = IO.In.ReadInt64();
            
            IO.SeekTo(8);
            // generate exisiting profile key and decrypt profile data
            profileIO = new EndianIO(ProcessData(IO.In.ReadBytes(IO.Length - 0x1C), GetExistingUserKey(keyObf, Xuid, 0x10), false), EndianType.BigEndian, true);

            // verify the data's hash
            IO.SeekTo(0);
            var body = IO.In.ReadBytes(IO.Length - 0x14);
            var hash = IO.In.ReadBytes(0x14);
            if(!HorizonCrypt.ArrayEquals(HorizonCrypt.SHA1(body), hash))
                throw new Exception("BattleBlock Theater: Invalid profile data detected!");

            profileIO.SeekTo(0x210);
            Gems = profileIO.In.ReadInt32();
            Yarn = profileIO.In.ReadInt32();
        }

        internal byte[] Save()
        {
            // write the stats
            profileIO.SeekTo(0x210);
            profileIO.Out.Write(Gems);
            profileIO.Out.Write(Yarn);
            
            // write back the encrypted data
            var confounder = ((long)Global.random.Next() << 32) | (uint)Global.random.Next();

            var profileData = new EndianIO(new MemoryStream(), EndianType.BigEndian, true);

            profileData.Out.Write(confounder);
            profileData.Out.Write(ProcessData(profileIO.ToArray(), GetExistingUserKey(confounder, Xuid, 0x10), true));
            var profileHash = HorizonCrypt.SHA1(profileData.ToArray());
            profileData.Out.Write(profileHash);

            return profileData.ToArray();
        }

        internal static int KeyRounds;

        private static byte[] GetExistingUserKey(long obfKey, ulong xuid, int keyLen)
        {
            var key = new byte[keyLen];
            int mask = 1, idx = 0, j = 0;
            for (var i = 0; i < KeyRounds; i++)
            {
                var a = (key[j] << 0x01) & 0xFE;
                int v;
                if ((idx & 1) == mask)
                {
                    v = (int) (xuid & 1);
                    xuid >>= 1;
                }
                else
                {
                    v = (int) (obfKey & 1);
                    obfKey >>= 1;
                }
                key[j] = (byte) (a + v);
                if (++j >= keyLen)
                {
                    j = 0;
                    mask = mask == 0 ? 1 : 0;
                }
                idx++;
            }

            return key;
        }

        private static byte[] ProcessData(byte[] input, byte[] userKey, bool isEncrypting)
        {
            var engine = new BlowfishEngine();
            var cipher = new BufferedBlockCipher(engine);
            cipher.Init(isEncrypting, new KeyParameter(userKey));

            var outBytes = new byte[input.Length];
            var len = cipher.ProcessBytes(input, 0, input.Length, outBytes, 0);
            cipher.DoFinal(outBytes, len);
            return outBytes;
        }

        internal byte[] ExportBuffer()
        {
            return profileIO.ToArray();
        }
    }
}
