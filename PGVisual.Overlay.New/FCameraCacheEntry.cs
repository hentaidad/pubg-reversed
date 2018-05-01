// Decompiled with JetBrains decompiler
// Type: PGVisual.Overlay.FCameraCacheEntry
// Assembly: PGVisual.Overlay, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36E310DD-04E8-450E-8620-AD5F611898A4
// Assembly location: C:\Users\TylerW10\AppData\Local\Temp\xpg\PGVisual.Overlay.exe

using System.Runtime.InteropServices;

namespace PGVisual.Overlay
{
  [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Auto)]
  internal struct FCameraCacheEntry
  {
    public float TimeStamp;
    private unsafe fixed byte UnknownData00[12];
    public FMinimalViewInfo POV;
  }
}
