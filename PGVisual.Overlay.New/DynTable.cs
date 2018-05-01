// Decompiled with JetBrains decompiler
// Type: PGVisual.Overlay.DynTable
// Assembly: PGVisual.Overlay, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36E310DD-04E8-450E-8620-AD5F611898A4
// Assembly location: C:\Users\TylerW10\AppData\Local\Temp\xpg\PGVisual.Overlay.exe

namespace PGVisual.Overlay
{
  internal class DynTable
  {
    private uint[] table = new uint[2048];

    public void generate(int rotator, int[] decrypt_offsets)
    {
      for (uint index = 0; index < 256U; ++index)
      {
        uint num1 = index;
        if (((int) num1 & 1) != 0)
          num1 ^= (uint) rotator;
        uint num2 = num1 >> 1;
        if (((int) num2 & 1) != 0)
          num2 ^= (uint) rotator;
        uint num3 = num2 >> 1;
        if (((int) num3 & 1) != 0)
          num3 ^= (uint) rotator;
        uint num4 = num3 >> 1;
        if (((int) num4 & 1) != 0)
          num4 ^= (uint) rotator;
        uint num5 = num4 >> 1;
        if (((int) num5 & 1) != 0)
          num5 ^= (uint) rotator;
        uint num6 = num5 >> 1;
        if (((int) num6 & 1) != 0)
          num6 ^= (uint) rotator;
        uint num7 = num6 >> 1;
        if (((int) num7 & 1) != 0)
          num7 ^= (uint) rotator;
        uint num8 = num7 >> 1;
        if (((int) num8 & 1) != 0)
          num8 ^= (uint) rotator;
        this.table[(int) index] = num8 >> 1;
      }
      uint num9 = 512;
      for (int index = 0; index < 256; ++index)
      {
        uint num1 = this.table[(int) num9 - 512];
        ++num9;
        uint num2 = num1 >> 8 ^ this.table[(int) num1 & (int) byte.MaxValue];
        this.table[(long) num9 + (long) decrypt_offsets[0]] = num2;
        uint num3 = num2 >> 8 ^ this.table[(int) num2 & (int) byte.MaxValue];
        this.table[(long) num9 + (long) decrypt_offsets[1]] = num3;
        uint num4 = num3 >> 8 ^ this.table[(int) num3 & (int) byte.MaxValue];
        this.table[(long) num9 + (long) decrypt_offsets[2]] = num4;
        uint num5 = num4 >> 8 ^ this.table[(int) num4 & (int) byte.MaxValue];
        this.table[(long) num9 + (long) decrypt_offsets[3]] = num5;
        uint num6 = num5 >> 8 ^ this.table[(int) num5 & (int) byte.MaxValue];
        this.table[(long) num9 + (long) decrypt_offsets[4]] = num6;
        uint num7 = num6 >> 8 ^ this.table[(int) num6 & (int) byte.MaxValue];
        this.table[(long) num9 + (long) decrypt_offsets[5]] = num7;
        uint num8 = num7 >> 8 ^ this.table[(int) num7 & (int) byte.MaxValue];
        this.table[(long) num9 + (long) decrypt_offsets[6]] = num8;
      }
    }

    public uint get(uint idx)
    {
      return this.table[(int) idx];
    }
  }
}
