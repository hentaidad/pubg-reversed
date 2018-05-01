// Decompiled with JetBrains decompiler
// Type: PGVisual.Overlay.Actor
// Assembly: PGVisual.Overlay, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36E310DD-04E8-450E-8620-AD5F611898A4
// Assembly location: C:\Users\TylerW10\AppData\Local\Temp\xpg\PGVisual.Overlay.exe

namespace PGVisual.Overlay
{
    internal class Item : BaseActor
    {
        public Vector3 position3D;
        public Vector3 position2D;
        public int id;
        public string name;
       

        public Item(int Iid,string Iname,Vector3 Ilocation2d,ulong pointer)
        {
            this.ptr = pointer;
            this.id = Iid;
            this.name = Iname;
            this.position2D = Ilocation2d;


        }
    }
}
