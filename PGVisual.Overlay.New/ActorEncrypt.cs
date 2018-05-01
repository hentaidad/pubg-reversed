// Decompiled with JetBrains decompiler
// Type: PGVisual.Overlay.ActorEncrypt
// Assembly: PGVisual.Overlay, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36E310DD-04E8-450E-8620-AD5F611898A4
// Assembly location: C:\Users\TylerW10\AppData\Local\Temp\xpg\PGVisual.Overlay.exe

using System;

namespace PGVisual.Overlay
{
  internal class ActorEncrypt
  {
    private byte[] byte_table = new byte[256]
    {
      (byte) 15,
      (byte) 14,
      (byte) 13,
      (byte) 12,
      (byte) 11,
      (byte) 10,
      (byte) 9,
      (byte) 8,
      (byte) 7,
      (byte) 6,
      (byte) 5,
      (byte) 4,
      (byte) 3,
      (byte) 2,
      (byte) 1,
      (byte) 0,
      (byte) 31,
      (byte) 30,
      (byte) 29,
      (byte) 28,
      (byte) 27,
      (byte) 26,
      (byte) 25,
      (byte) 24,
      (byte) 23,
      (byte) 22,
      (byte) 21,
      (byte) 20,
      (byte) 19,
      (byte) 18,
      (byte) 17,
      (byte) 16,
      (byte) 47,
      (byte) 46,
      (byte) 45,
      (byte) 44,
      (byte) 43,
      (byte) 42,
      (byte) 41,
      (byte) 40,
      (byte) 39,
      (byte) 38,
      (byte) 37,
      (byte) 36,
      (byte) 35,
      (byte) 34,
      (byte) 33,
      (byte) 32,
      (byte) 63,
      (byte) 62,
      (byte) 61,
      (byte) 60,
      (byte) 59,
      (byte) 58,
      (byte) 57,
      (byte) 56,
      (byte) 55,
      (byte) 54,
      (byte) 53,
      (byte) 52,
      (byte) 51,
      (byte) 50,
      (byte) 49,
      (byte) 48,
      (byte) 79,
      (byte) 78,
      (byte) 77,
      (byte) 76,
      (byte) 75,
      (byte) 74,
      (byte) 73,
      (byte) 72,
      (byte) 71,
      (byte) 70,
      (byte) 69,
      (byte) 68,
      (byte) 67,
      (byte) 66,
      (byte) 65,
      (byte) 64,
      (byte) 95,
      (byte) 94,
      (byte) 93,
      (byte) 92,
      (byte) 91,
      (byte) 90,
      (byte) 89,
      (byte) 88,
      (byte) 87,
      (byte) 86,
      (byte) 85,
      (byte) 84,
      (byte) 83,
      (byte) 82,
      (byte) 81,
      (byte) 80,
      (byte) 111,
      (byte) 110,
      (byte) 109,
      (byte) 108,
      (byte) 107,
      (byte) 106,
      (byte) 105,
      (byte) 104,
      (byte) 103,
      (byte) 102,
      (byte) 101,
      (byte) 100,
      (byte) 99,
      (byte) 98,
      (byte) 97,
      (byte) 96,
      (byte) 127,
      (byte) 126,
      (byte) 125,
      (byte) 124,
      (byte) 123,
      (byte) 122,
      (byte) 121,
      (byte) 120,
      (byte) 119,
      (byte) 118,
      (byte) 117,
      (byte) 116,
      (byte) 115,
      (byte) 114,
      (byte) 113,
      (byte) 112,
      (byte) 143,
      (byte) 142,
      (byte) 141,
      (byte) 140,
      (byte) 139,
      (byte) 138,
      (byte) 137,
      (byte) 136,
      (byte) 135,
      (byte) 134,
      (byte) 133,
      (byte) 132,
      (byte) 131,
      (byte) 130,
      (byte) 129,
      (byte) 128,
      (byte) 159,
      (byte) 158,
      (byte) 157,
      (byte) 156,
      (byte) 155,
      (byte) 154,
      (byte) 153,
      (byte) 152,
      (byte) 151,
      (byte) 150,
      (byte) 149,
      (byte) 148,
      (byte) 147,
      (byte) 146,
      (byte) 145,
      (byte) 144,
      (byte) 175,
      (byte) 174,
      (byte) 173,
      (byte) 172,
      (byte) 171,
      (byte) 170,
      (byte) 169,
      (byte) 168,
      (byte) 167,
      (byte) 166,
      (byte) 165,
      (byte) 164,
      (byte) 163,
      (byte) 162,
      (byte) 161,
      (byte) 160,
      (byte) 191,
      (byte) 190,
      (byte) 189,
      (byte) 188,
      (byte) 187,
      (byte) 186,
      (byte) 185,
      (byte) 184,
      (byte) 183,
      (byte) 182,
      (byte) 181,
      (byte) 180,
      (byte) 179,
      (byte) 178,
      (byte) 177,
      (byte) 176,
      (byte) 207,
      (byte) 206,
      (byte) 205,
      (byte) 204,
      (byte) 203,
      (byte) 202,
      (byte) 201,
      (byte) 200,
      (byte) 199,
      (byte) 198,
      (byte) 197,
      (byte) 196,
      (byte) 195,
      (byte) 194,
      (byte) 193,
      (byte) 192,
      (byte) 223,
      (byte) 222,
      (byte) 221,
      (byte) 220,
      (byte) 219,
      (byte) 218,
      (byte) 217,
      (byte) 216,
      (byte) 215,
      (byte) 214,
      (byte) 213,
      (byte) 212,
      (byte) 211,
      (byte) 210,
      (byte) 209,
      (byte) 208,
      (byte) 239,
      (byte) 238,
      (byte) 237,
      (byte) 236,
      (byte) 235,
      (byte) 234,
      (byte) 233,
      (byte) 232,
      (byte) 231,
      (byte) 230,
      (byte) 229,
      (byte) 228,
      (byte) 227,
      (byte) 226,
      (byte) 225,
      (byte) 224,
      byte.MaxValue,
      (byte) 254,
      (byte) 253,
      (byte) 252,
      (byte) 251,
      (byte) 250,
      (byte) 249,
      (byte) 248,
      (byte) 247,
      (byte) 246,
      (byte) 245,
      (byte) 244,
      (byte) 243,
      (byte) 242,
      (byte) 241,
      (byte) 240
    };
    private int decrypt_rotator = 866889406;
    private int[] decrypt_offsets = new int[7]
    {
      -257,
      -1,
      (int) byte.MaxValue,
      511,
      767,
      1023,
      1279
    };
    private DynTable dyn_table_r = new DynTable();

        public unsafe ulong DecryptPtr(ulong encryptedPtr)
        {
            EncryptedBlock encryptedBlock = Program.Client.ReadMemory<EncryptedBlock>(encryptedPtr);
            long num = encryptedBlock.data[this.decrypt_p21(encryptedBlock.data[44], encryptedBlock.data[45])];
            ulong num2 = this.decrypt_p22(encryptedBlock.data[46], encryptedBlock.data[47]);
            return (ulong)(num ^ (long)num2);
        }

        private int ubyte0(int i)
    {
      return i & (int) byte.MaxValue;
    }

    private int ubyte1(int i)
    {
      return i >> 8 & (int) byte.MaxValue;
    }

    private int ubyte2(int i)
    {
      return i >> 16 & (int) byte.MaxValue;
    }

    private int ubyte3(int i)
    {
      return i >> 24 & (int) byte.MaxValue;
    }

    private int word0(long l)
    {
      return (int) (l & (long) ushort.MaxValue);
    }

    public void init_decryption()
    {
      this.dyn_table_r.generate(this.decrypt_rotator, this.decrypt_offsets);
    }

    private ushort decrypt_p1(long left, long right)
    {
      ushort num = (ushort) ((ulong) (right ^ ~left) ^ 3365UL);
      for (int index = 0; index < 5; ++index)
        num = (ushort) ((uint) this.byte_table[(int) this.byte_table[(int) (((uint) num ^ 17408U) >> 8)]] | (uint) this.byte_table[(int) this.byte_table[((int) num ^ 85) & (int) byte.MaxValue]] << 8);
      return num;
    }

    private ulong decrypt_p20(long left, long right, ulong a, ulong b, ulong c, ulong d_idx)
    {
      ushort num1 = this.decrypt_p1(left, right);
      int num2 = this.ubyte3((int) ((long) num1 ^ (long) b));
      ulong num3 = (ulong) this.ubyte1((int) num1) ^ c + 512UL;
      int num4 = this.ubyte0((int) ((long) num1 ^ (long) a)) + 768;
      ulong num5 = d_idx + 256UL;
      int num6 = (int) this.dyn_table_r.get((uint) num4);
      uint num7 = this.dyn_table_r.get((uint) num2);
      uint num8 = this.dyn_table_r.get((uint) num3);
      uint num9 = this.dyn_table_r.get((uint) num5);
      int num10 = (int) num7;
      return (ulong) ((uint) ~(num6 ^ num10 ^ (int) num8 ^ (int) num9) % 43U);
    }

    private ulong decrypt_p21(long left, long right)
    {
      return this.decrypt_p20(left, right, 188UL, 3618593468UL, 90UL, 175UL);
    }

    private ulong decrypt_p22(long left, long right)
    {
      return this.decrypt_p20(left, right, 12UL, 1558700812UL, 227UL, 231UL);
    }
  }
}
