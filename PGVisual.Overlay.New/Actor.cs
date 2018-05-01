// Decompiled with JetBrains decompiler
// Type: PGVisual.Overlay.Actor
// Assembly: PGVisual.Overlay, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36E310DD-04E8-450E-8620-AD5F611898A4
// Assembly location: C:\Users\TylerW10\AppData\Local\Temp\xpg\PGVisual.Overlay.exe

namespace PGVisual.Overlay
{
  internal class Actor : BaseActor
  {
    public ulong mesh;
    public ulong boneArray;
    public Vector3 position3D;
    public ulong rootComp;
    public Vector3 position2D;
    public Vector3 positionHead2D;
    public float health;
    public Actor(ulong actorPtr)
    {
      this.ptr = actorPtr;
      this.rootComp = Program.Client.ReadPtr(actorPtr + 392UL);
      if ((long) this.rootComp == 0L)
        return;
      this.position3D = Program.Client.ReadMemory<Vector3>(this.rootComp + 596UL);
      this.health = Program.Client.ReadMemory<float>(actorPtr + 4444UL);
      this.mesh = Program.Client.ReadPtr(actorPtr + 1040UL);
      if ((long) this.mesh != 0L)
      {
        this.boneArray = Program.Client.ReadPtr(this.mesh + 2400UL);
        Vector3 bone = this.GetBone(6);
        Program.Pubg.worldToScreen(bone, out this.positionHead2D);
      }
      Program.Pubg.worldToScreen(this.position3D, out this.position2D);
    }

    public Vector3 GetBone(int boneIndex)
    {
      if ((long) this.boneArray == 0L)
        return new Vector3(0.0f, 0.0f, 0.0f);
      D3DMatrix d3Dmatrix = D3DMatrix.Multiplicate(new D3DMatrix(Program.Client.ReadMemory<FTransform>(this.boneArray + (ulong) (boneIndex * 48))), new D3DMatrix(Program.Client.ReadMemory<FTransform>(this.mesh + 640UL)));
      return new Vector3(d3Dmatrix._41, d3Dmatrix._42, d3Dmatrix._43);
    }

    public bool IsValid()
    {
      if ((long) this.rootComp != 0L)
        return this.mesh > 0UL;
      return false;
    }
  }
}
