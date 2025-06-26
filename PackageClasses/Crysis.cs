using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Crysis
{
    internal class Crysis3
    {

    }

    internal class CrysisCryptek
    {
        private byte[] S;
        private readonly byte[] P;
        private int K, N;
        private readonly int _seed;
        internal CrysisCryptek()
        {
            
        }
        internal CrysisCryptek(byte[] p, int seed)
        {
            if (p == null)
                return;

            P = new byte[p.Length * 2];

            for (int i = 0; i < 2; i++)
            {
                Array.Copy(p, 0, P, i * 0x100, p.Length);
            }
            _seed = seed;
            Init();
        }
        void Init()
        {
            if (S == null)
            {
                S = new byte[P.Length];
                Array.Copy(P, 0, S, 0, P.Length);
            }
            else
            {
                Array.Copy(S, 0, P, 0, S.Length);
            }

            K = 0;
            N = _seed;
        }

        public void Crypt(byte[] pInput, Stream pOutput, bool forEncryption)
        {
            if (pInput == null || pOutput == null) return;
            if (forEncryption)
            {
                // Re-initialize the table
                Init();
            }
            var writer = new EndianWriter(pOutput, EndianType.BigEndian);
            foreach (byte val in pInput)
            {
                int secondIndex = (K + 1) & 0xFF;

                K = secondIndex;

                N = (N + P[0x100 + secondIndex]) & 0xFF;

                byte a = P[0x100 + N];
                byte b = P[0x100 + secondIndex];

                P[0x100 + N] = b;
                P[0x100 + K] = a;

                writer.Write((byte)(val ^ (P[0x100 + ((P[0x100 + N] + P[0x100 + K]) & 0xFF)]) & 0xFF));
            }
        }
    }
}
