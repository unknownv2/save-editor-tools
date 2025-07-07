using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using ForzaMotorsport;

namespace ForzaHorizon
{
    public class ForzaHorizonProfile
    {
        private EndianIO IO;
        public EndianIO SaveIO;

        private byte[] _aesKey, _hmacShaKey, _creator;

        public ForzaProfile Profile;
        private GlobalForzaSecurity _forzaSecurity;
        public ForzaHorizonProfile(EndianIO io, ulong profileId, byte[] baseAesKey, byte[] baseHmacShaKey)
        {
            if (io != null)
                IO = io;

            _creator = Horizon.Functions.Global.convertToBigEndian(BitConverter.GetBytes(profileId));

            _aesKey = GlobalForzaSecurity.TransformHorizonSessionKey(baseAesKey, _creator, -2);
            _hmacShaKey = GlobalForzaSecurity.TransformHorizonSessionKey(baseHmacShaKey, _creator, -4);

            _forzaSecurity = new GlobalForzaSecurity(ForzaVersion.ForzaHorizon, _aesKey, _hmacShaKey);
            Profile = new ForzaProfile((SaveIO = _forzaSecurity.DecryptData(IO.ToArray(), true)));
        }

        public ForzaHorizonProfile(EndianIO io, ulong profileId, byte[] baseAesKey, byte[][] baseHmacShaKey, int version)
        {
            if(version > 0x02)
                throw new ForzaException("invalid forza game version detected. Please report to a Horizon developer.");
            if (io != null)
                IO = io;
            else
                throw new ForzaException("invalid forza profile I/O detected. Please report to a Horizon developer."); 
            
            _creator = Horizon.Functions.Global.convertToBigEndian(BitConverter.GetBytes(profileId));


            _aesKey = GlobalForzaSecurity.TransformHorizonSessionKey(baseAesKey, _creator, -2);
           _hmacShaKey = GlobalForzaSecurity.TransformHorizonSessionKey(baseHmacShaKey[version], _creator, -4);
            

            _forzaSecurity = new GlobalForzaSecurity(ForzaVersion.ForzaHorizon, _aesKey, _hmacShaKey);
            Profile = new ForzaProfile((SaveIO = _forzaSecurity.DecryptData(IO.ToArray(), true)));
        }
        public void Save()
        {
            SaveIO.Stream.Flush();
            _forzaSecurity.EncryptProfileData(IO, SaveIO.ToArray());
        }
    }
}
