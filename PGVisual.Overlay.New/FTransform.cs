// Decompiled with JetBrains decompiler
// Type: PGVisual.Overlay.FTransform
// Assembly: PGVisual.Overlay, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36E310DD-04E8-450E-8620-AD5F611898A4
// Assembly location: C:\Users\TylerW10\AppData\Local\Temp\xpg\PGVisual.Overlay.exe

using System.Runtime.InteropServices;

namespace PGVisual.Overlay
{
  [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Auto)]
  internal struct FTransform
  {
    public FQuat rot;
    public Vector3 translation;
    private unsafe fixed char pad[4];
    public Vector3 scale;
    private unsafe fixed char pad1[4];
  }
}
