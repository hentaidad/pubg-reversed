// Decompiled with JetBrains decompiler
// Type: PGVisual.Overlay.Vector3
// Assembly: PGVisual.Overlay, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36E310DD-04E8-450E-8620-AD5F611898A4
// Assembly location: C:\Users\TylerW10\AppData\Local\Temp\xpg\PGVisual.Overlay.exe

using System;

namespace PGVisual.Overlay
{
  internal struct Vector3
  {
    public float X;
    public float Y;
    public float Z;

    public Vector3(float xVal, float yVal, float zVal)
    {
      this.X = xVal;
      this.Y = yVal;
      this.Z = zVal;
    }

    public static Vector3 operator +(Vector3 mv1, Vector3 mv2)
    {
      return new Vector3(mv1.X + mv2.X, mv1.Y + mv2.Y, mv1.Z + mv2.Z);
    }

    public static Vector3 operator -(Vector3 mv1, Vector3 mv2)
    {
      return new Vector3(mv1.X - mv2.X, mv1.Y - mv2.Y, mv1.Z - mv2.Z);
    }

    public static Vector3 operator -(Vector3 mv1, float var)
    {
      return new Vector3(mv1.X - var, mv1.Y - var, mv1.Z - var);
    }

    public static Vector3 operator *(Vector3 mv1, Vector3 mv2)
    {
      return new Vector3(mv1.X * mv2.X, mv1.Y * mv2.Y, mv1.Z * mv2.Z);
    }

    public static Vector3 operator *(Vector3 mv, float var)
    {
      return new Vector3(mv.X * var, mv.Y * var, mv.Z * var);
    }

    public static Vector3 operator %(Vector3 mv1, Vector3 mv2)
    {
      return new Vector3((float) ((double) mv1.Y * (double) mv2.Z - (double) mv1.Z * (double) mv2.Y), (float) ((double) mv1.Z * (double) mv2.X - (double) mv1.X * (double) mv2.Z), (float) ((double) mv1.X * (double) mv2.Y - (double) mv1.Y * (double) mv2.X));
    }

    public float this[int key]
    {
      get
      {
        return this.GetValueByIndex(key);
      }
      set
      {
        this.SetValueByIndex(key, value);
      }
    }

    private void SetValueByIndex(int key, float value)
    {
      switch (key)
      {
        case 0:
          this.X = value;
          break;
        case 1:
          this.Y = value;
          break;
        case 2:
          this.Z = value;
          break;
      }
    }

    private float GetValueByIndex(int key)
    {
      if (key == 0)
        return this.X;
      if (key == 1)
        return this.Y;
      return this.Z;
    }

    public float DotProduct(Vector3 mv)
    {
      return (float) ((double) this.X * (double) mv.X + (double) this.Y * (double) mv.Y + (double) this.Z * (double) mv.Z);
    }

    public Vector3 ScaleBy(float value)
    {
      return new Vector3(this.X * value, this.Y * value, this.Z * value);
    }

    public Vector3 ComponentProduct(Vector3 mv)
    {
      return new Vector3(this.X * mv.X, this.Y * mv.Y, this.Z * mv.Z);
    }

    public void ComponentProductUpdate(Vector3 mv)
    {
      this.X *= mv.X;
      this.Y *= mv.Y;
      this.Z *= mv.Z;
    }

    public Vector3 VectorProduct(Vector3 mv)
    {
      return new Vector3((float) ((double) this.Y * (double) mv.Z - (double) this.Z * (double) mv.Y), (float) ((double) this.Z * (double) mv.X - (double) this.X * (double) mv.Z), (float) ((double) this.X * (double) mv.Y - (double) this.Y * (double) mv.X));
    }

    public float ScalarProduct(Vector3 mv)
    {
      return (float) ((double) this.X * (double) mv.X + (double) this.Y * (double) mv.Y + (double) this.Z * (double) mv.Z);
    }

    public float Distance(Vector3 v)
    {
      return (float) Math.Sqrt(Math.Pow((double) v.X - (double) this.X, 2.0) + Math.Pow((double) v.Y - (double) this.Y, 2.0) + Math.Pow((double) v.Z - (double) this.Z, 2.0));
    }

    public void AddScaledVector(Vector3 mv, float scale)
    {
      this.X += mv.X * scale;
      this.Y += mv.Y * scale;
      this.Z += mv.Z * scale;
    }

    public float Magnitude()
    {
      return (float) Math.Sqrt((double) this.X * (double) this.X + (double) this.Y * (double) this.Y + (double) this.Z * (double) this.Z);
    }

    public float SquareMagnitude()
    {
      return (float) ((double) this.X * (double) this.X + (double) this.Y * (double) this.Y + (double) this.Z * (double) this.Z);
    }

    public void Trim(float size)
    {
      if ((double) this.SquareMagnitude() <= (double) size * (double) size)
        return;
      this.Normalize();
      this.X *= size;
      this.Y *= size;
      this.Z *= size;
    }

    public void Normalize()
    {
      float num = this.Magnitude();
      if ((double) num > 0.0)
      {
        this.X /= num;
        this.Y /= num;
        this.Z /= num;
      }
      else
      {
        this.X = 0.0f;
        this.Y = 0.0f;
        this.Z = 0.0f;
      }
    }

    public Vector3 Inverted()
    {
      return new Vector3(-this.X, -this.Y, -this.Z);
    }

    public Vector3 Unit()
    {
      Vector3 vector3 = this;
      vector3.Normalize();
      return vector3;
    }

    public void Clear()
    {
      this.X = 0.0f;
      this.Y = 0.0f;
      this.Z = 0.0f;
    }

    public static float Distance(Vector3 mv1, Vector3 mv2)
    {
      return (mv1 - mv2).Magnitude();
    }

    public static Vector3 Zero()
    {
      return new Vector3(0.0f, 0.0f, 0.0f);
    }
  }
}
