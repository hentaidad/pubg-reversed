// Decompiled with JetBrains decompiler
// Type: PGVisual.Overlay.LocalActor
// Assembly: PGVisual.Overlay, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36E310DD-04E8-450E-8620-AD5F611898A4
// Assembly location: C:\Users\TylerW10\AppData\Local\Temp\xpg\PGVisual.Overlay.exe

namespace PGVisual.Overlay
{
  internal class LocalActor : BaseActor
  {
    private ulong localPlayer;
    public Vector3 position3D;

    public LocalActor(ulong actorPtr, ulong localPlayer)
    {
      this.ptr = actorPtr;
      this.localPlayer = localPlayer;
      if ((long) this.localPlayer == 0L)
        return;
      this.position3D = Program.Client.ReadMemory<Vector3>(this.localPlayer + 112UL);
    }
  }
}
