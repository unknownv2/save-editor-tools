using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICE
{
    internal class  IceKey
    {

        internal class IceSubkey
        {
            internal uint[] val = new uint[03];
            internal IceSubkey()
            {
            }
        };

        int _size;
        int _rounds;
        IceSubkey[] _keysched;

        internal uint[] val = new uint[03];

        /* The S-boxes */
        static uint[,] ice_sbox = new uint[4, 1024];
        static int ice_sboxes_initialised = 0;


        /* Modulo values for the S-boxes */
        static readonly int[,] ice_smod =
        {
		    {333, 313, 505, 369},
			{379, 375, 319, 391},
			{361, 445, 451, 397},
			{397, 425, 395, 505}
        };

        /* XOR values for the S-boxes */
        static readonly int[,] ice_sxor =
        {
		    {0x83, 0x85, 0x9b, 0xcd},
			{0xcc, 0xa7, 0xad, 0x41},
			{0x4b, 0x2e, 0xd4, 0x33},
			{0xea, 0xcb, 0x2e, 0x04}
        };

        /* Permutation values for the P-box */
        static readonly uint[] ice_pbox =
        {
		    0x00000001, 0x00000080, 0x00000400, 0x00002000,
		    0x00080000, 0x00200000, 0x01000000, 0x40000000,
		    0x00000008, 0x00000020, 0x00000100, 0x00004000,
		    0x00010000, 0x00800000, 0x04000000, 0x20000000,
		    0x00000004, 0x00000010, 0x00000200, 0x00008000,
		    0x00020000, 0x00400000, 0x08000000, 0x10000000,
		    0x00000002, 0x00000040, 0x00000800, 0x00001000,
		    0x00040000, 0x00100000, 0x02000000, 0x80000000
        };

        /* The key rotation schedule */
        static readonly int[] ice_keyrot = { 0, 1, 2, 3, 2, 1, 3, 0 };
        static readonly int[] ice_keyrot2 = { 1, 3, 2, 0, 3, 1, 0, 2 };


        /*
         * 8-bit Galois Field multiplication of a by b, modulo m.
         * Just like arithmetic multiplication, except that additions and
         * subtractions are replaced by XOR.
         */

        internal static uint gf_mult(uint a, uint b, uint m)
        {
            uint res = 0;

            while (b != 0)
            {
                if ((b & 1) != 0)
                    res ^= a;

                a <<= 1;
                b >>= 1;

                if (a >= 256)
                    a ^= m;
            }

            return (res);
        }


        /*
         * Galois Field exponentiation.
         * Raise the base to the power of 7, modulo m.
         */

        internal static uint gf_exp7(uint b, uint m)
        {
            uint x;

            if (b == 0)
                return (0);

            x = gf_mult(b, b, m);
            x = gf_mult(b, x, m);
            x = gf_mult(x, x, m);
            return (gf_mult(b, x, m));
        }


        /*
         * Carry out the ICE 32-bit P-box permutation.
         */

        internal static uint ice_perm32(uint x)
        {
            uint res = 0;
            uint[] pbox = ice_pbox;
            int i = 0;
            while (x != 0)
            {
                if ((x & 1) != 0)
                    res |= pbox[i];
                i++;
                x >>= 1;
            }

            return (res);
        }


        /*
         * Initialise the ICE S-boxes.
         * This only has to be done once.
         */

        internal static void
        ice_sboxes_init()
        {
            int i;

            for (i = 0; i < 1024; i++)
            {
                int col = (i >> 1) & 0xff;
                int row = (i & 0x1) | ((i & 0x200) >> 8);
                uint x;

                x = gf_exp7((uint)(col ^ ice_sxor[0, row]), (uint)(ice_smod[0, row])) << 24;
                ice_sbox[0, i] = ice_perm32(x);

                x = gf_exp7((uint)(col ^ ice_sxor[1, row]), (uint)ice_smod[1, row]) << 16;
                ice_sbox[1, i] = ice_perm32(x);

                x = gf_exp7((uint)(col ^ ice_sxor[2, row]), (uint)ice_smod[2, row]) << 8;
                ice_sbox[2, i] = ice_perm32(x);

                x = gf_exp7((uint)(col ^ ice_sxor[3, row]), (uint)ice_smod[3, row]);
                ice_sbox[3, i] = ice_perm32(x);
            }
        }


        /*
         * Create a new ICE key.
         */

        internal IceKey(int n)
        {
            if (ice_sboxes_initialised == 0)
            {
                ice_sboxes_init();
                ice_sboxes_initialised = 1;
            }

            if (n < 1)
            {
                _size = 1;
                _rounds = 8;
            }
            else
            {
                _size = n;
                _rounds = n * 16;
            }

            _keysched = new IceSubkey[_rounds];
            for (int x = new int(); x < _rounds; x++)
                _keysched[x] = new IceSubkey();

        }



        /*
         * The single round ICE f function.
         */

        internal static uint ice_f(uint p, ref IceSubkey sk)
        {
            uint tl, tr;		/* Expanded 40-bit values */
            uint al, ar;		/* Salted expanded 40-bit values */

            /* Left half expansion */
            tl = ((p >> 16) & 0x3ff) | (((p >> 14) | (p << 18)) & 0xffc00);

            /* Right half expansion */
            tr = (p & 0x3ff) | ((p << 2) & 0xffc00);

            /* Perform the salt permutation */
            // al = (tr & sk->val[2]) | (tl & ~sk->val[2]);
            // ar = (tl & sk->val[2]) | (tr & ~sk->val[2]);
            al = sk.val[2] & (tl ^ tr);
            ar = al ^ tr;
            al ^= tl;

            al ^= sk.val[0];		/* XOR with the subkey */
            ar ^= sk.val[1];

            /* S-box lookup and permutation */
            return (ice_sbox[0, al >> 10] | ice_sbox[1, al & 0x3ff]
                | ice_sbox[2, ar >> 10] | ice_sbox[3, ar & 0x3ff]);
        }


        /*
         * Encrypt a block of 8 bytes of data with the given ICE key.
         */

        internal void encrypt(byte[] ptext, byte[] ctext)
        {
            int i;
            uint l, r;

            l = (uint)(((ptext[0]) << 24)
                        | ((ptext[1]) << 16)
                        | ((ptext[2]) << 8) | ptext[3]);
            r = (uint)(((ptext[4]) << 24)
                        | ((ptext[5]) << 16)
                        | ((ptext[6]) << 8) | ptext[7]);

            for (i = 0; i < _rounds; i += 2)
            {
                l ^= ice_f(r, ref _keysched[i]);
                r ^= ice_f(l, ref _keysched[i + 1]);
            }

            for (i = 0; i < 4; i++)
            {
                ctext[3 - i] = (byte)(r & 0xff);
                ctext[7 - i] = (byte)(l & 0xff);

                r >>= 8;
                l >>= 8;
            }
        }


        /*
         * Decrypt a block of 8 bytes of data with the given ICE key.
         */

        internal void decrypt(byte[] ctext, byte[] ptext)
        {
            int i;
            uint l, r;

            l = (uint)(((ctext[0]) << 24)
                        | ((ctext[1]) << 16)
                        | ((ctext[2]) << 8) | ctext[3]);
            r = (uint)(((ctext[4]) << 24)
                        | ((ctext[5]) << 16)
                        | ((ctext[6]) << 8) | ctext[7]);

            for (i = _rounds - 1; i > 0; i -= 2)
            {
                l ^= ice_f(r, ref _keysched[i]);
                r ^= ice_f(l, ref _keysched[i - 1]);
            }

            for (i = 0; i < 4; i++)
            {
                ptext[3 - i] = (byte)(r & 0xff);
                ptext[7 - i] = (byte)(l & 0xff);

                r >>= 8;
                l >>= 8;
            }
        }


        /*
         * Set 8 rounds [n, n+7] of the key schedule of an ICE key.
         */

        internal void scheduleBuild(ushort[] kb, int n, int[] keyrot)
        {
            int i;

            for (i = 0; i < 8; i++)
            {
                int j;
                int kr = keyrot[i];
                IceSubkey isk = _keysched[n + i];

                for (j = 0; j < 3; j++)
                    isk.val[j] = 0;

                for (j = 0; j < 15; j++)
                {
                    int k;
                    uint curr_sk = isk.val[j % 3];

                    for (k = 0; k < 4; k++)
                    {
                        ushort curr_kb = kb[(kr + k) & 3];
                        int bit = curr_kb & 1;
                        uint bit2 = (uint)curr_kb & 1;
                        curr_sk = (uint)((curr_sk << 1) | bit2);
                        curr_kb = (ushort)((curr_kb >> 1) | ((bit ^ 1) << 15));
                        kb[(kr + k) & 3] = curr_kb;
                        isk.val[j % 3] = curr_sk;
                    }
                }
            }
        }


        /*
         * Set the key schedule of an ICE key.
         */

        internal void set(byte[] key)
        {
            int i;

            if (_rounds == 8)
            {
                ushort[] kb = new ushort[4];

                for (i = 0; i < 4; i++)
                    kb[3 - i] = (ushort)((key[i * 2] << 8) | key[i * 2 + 1]);

                scheduleBuild(kb, 0, ice_keyrot);
                return;
            }

            for (i = 0; i < _size; i++)
            {
                int j;
                ushort[] kb = new ushort[4];

                for (j = 0; j < 4; j++)
                    kb[3 - j] = (ushort)((key[i * 8 + j * 2] << 8) | key[i * 8 + j * 2 + 1]);

                scheduleBuild(kb, i * 8, ice_keyrot);
                scheduleBuild(kb, _rounds - 8 - i * 8, ice_keyrot2);
            }
        }


        /*
         * Return the key size, in bytes.
         */

        internal int keySize()
        {
            return (_size * 8);
        }


        /*
         * Return the block size, in bytes.
         */

        internal int blockSize()
        {
            return (8);
        }

    }
}