// Decompiled with JetBrains decompiler
// Type: PGVisual.Overlay.FQuat
// Assembly: PGVisual.Overlay, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36E310DD-04E8-450E-8620-AD5F611898A4
// Assembly location: C:\Users\TylerW10\AppData\Local\Temp\xpg\PGVisual.Overlay.exe

using System.Runtime.InteropServices;

namespace PGVisual.Overlay
{
  [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Auto)]
  internal struct FQuat
  {
    public float x;
    public float y;
    public float z;
    public float w;
  }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct FText
    {
        fixed byte pad0[0x28];
        public FString fstring;
    }
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct FString
    {
        fixed sbyte arrName[64];

        public override string ToString()
        {
            fixed (sbyte* pName = arrName)
                return new string(pName);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct TBitArray
    {
        public fixed uint AllocatorInstanceData[6];
        public uint NumBits;
        public uint MaxBits;

    }

}
