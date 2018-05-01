// Decompiled with JetBrains decompiler
// Type: PGVisual.Overlay.D3DMatrix
// Assembly: PGVisual.Overlay, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36E310DD-04E8-450E-8620-AD5F611898A4
// Assembly location: C:\Users\TylerW10\AppData\Local\Temp\xpg\PGVisual.Overlay.exe

using System;
using System.Runtime.InteropServices;

namespace PGVisual.Overlay
{
  [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Auto)]
  internal struct D3DMatrix
  {
    public float _11;
    public float _12;
    public float _13;
    public float _14;
    public float _21;
    public float _22;
    public float _23;
    public float _24;
    public float _31;
    public float _32;
    public float _33;
    public float _34;
    public float _41;
    public float _42;
    public float _43;
    public float _44;

    public static D3DMatrix Multiplicate(D3DMatrix pM1, D3DMatrix pM2)
    {
      return new D3DMatrix()
      {
        _11 = (float) ((double) pM1._11 * (double) pM2._11 + (double) pM1._12 * (double) pM2._21 + (double) pM1._13 * (double) pM2._31 + (double) pM1._14 * (double) pM2._41),
        _12 = (float) ((double) pM1._11 * (double) pM2._12 + (double) pM1._12 * (double) pM2._22 + (double) pM1._13 * (double) pM2._32 + (double) pM1._14 * (double) pM2._42),
        _13 = (float) ((double) pM1._11 * (double) pM2._13 + (double) pM1._12 * (double) pM2._23 + (double) pM1._13 * (double) pM2._33 + (double) pM1._14 * (double) pM2._43),
        _14 = (float) ((double) pM1._11 * (double) pM2._14 + (double) pM1._12 * (double) pM2._24 + (double) pM1._13 * (double) pM2._34 + (double) pM1._14 * (double) pM2._44),
        _21 = (float) ((double) pM1._21 * (double) pM2._11 + (double) pM1._22 * (double) pM2._21 + (double) pM1._23 * (double) pM2._31 + (double) pM1._24 * (double) pM2._41),
        _22 = (float) ((double) pM1._21 * (double) pM2._12 + (double) pM1._22 * (double) pM2._22 + (double) pM1._23 * (double) pM2._32 + (double) pM1._24 * (double) pM2._42),
        _23 = (float) ((double) pM1._21 * (double) pM2._13 + (double) pM1._22 * (double) pM2._23 + (double) pM1._23 * (double) pM2._33 + (double) pM1._24 * (double) pM2._43),
        _24 = (float) ((double) pM1._21 * (double) pM2._14 + (double) pM1._22 * (double) pM2._24 + (double) pM1._23 * (double) pM2._34 + (double) pM1._24 * (double) pM2._44),
        _31 = (float) ((double) pM1._31 * (double) pM2._11 + (double) pM1._32 * (double) pM2._21 + (double) pM1._33 * (double) pM2._31 + (double) pM1._34 * (double) pM2._41),
        _32 = (float) ((double) pM1._31 * (double) pM2._12 + (double) pM1._32 * (double) pM2._22 + (double) pM1._33 * (double) pM2._32 + (double) pM1._34 * (double) pM2._42),
        _33 = (float) ((double) pM1._31 * (double) pM2._13 + (double) pM1._32 * (double) pM2._23 + (double) pM1._33 * (double) pM2._33 + (double) pM1._34 * (double) pM2._43),
        _34 = (float) ((double) pM1._31 * (double) pM2._14 + (double) pM1._32 * (double) pM2._24 + (double) pM1._33 * (double) pM2._34 + (double) pM1._34 * (double) pM2._44),
        _41 = (float) ((double) pM1._41 * (double) pM2._11 + (double) pM1._42 * (double) pM2._21 + (double) pM1._43 * (double) pM2._31 + (double) pM1._44 * (double) pM2._41),
        _42 = (float) ((double) pM1._41 * (double) pM2._12 + (double) pM1._42 * (double) pM2._22 + (double) pM1._43 * (double) pM2._32 + (double) pM1._44 * (double) pM2._42),
        _43 = (float) ((double) pM1._41 * (double) pM2._13 + (double) pM1._42 * (double) pM2._23 + (double) pM1._43 * (double) pM2._33 + (double) pM1._44 * (double) pM2._43),
        _44 = (float) ((double) pM1._41 * (double) pM2._14 + (double) pM1._42 * (double) pM2._24 + (double) pM1._43 * (double) pM2._34 + (double) pM1._44 * (double) pM2._44)
      };
    }

    public D3DMatrix(FTransform transform)
    {
      this._41 = transform.translation.X;
      this._42 = transform.translation.Y;
      this._43 = transform.translation.Z;
      float num1 = transform.rot.x + transform.rot.x;
      float num2 = transform.rot.y + transform.rot.y;
      float num3 = transform.rot.z + transform.rot.z;
      float num4 = transform.rot.x * num1;
      float num5 = transform.rot.y * num2;
      float num6 = transform.rot.z * num3;
      this._11 = (float) (1.0 - ((double) num5 + (double) num6)) * transform.scale.X;
      this._22 = (float) (1.0 - ((double) num4 + (double) num6)) * transform.scale.Y;
      this._33 = (float) (1.0 - ((double) num4 + (double) num5)) * transform.scale.Z;
      float num7 = transform.rot.y * num3;
      float num8 = transform.rot.w * num1;
      this._32 = (num7 - num8) * transform.scale.Z;
      this._23 = (num7 + num8) * transform.scale.Y;
      float num9 = transform.rot.x * num2;
      float num10 = transform.rot.w * num3;
      this._21 = (num9 - num10) * transform.scale.Y;
      this._12 = (num9 + num10) * transform.scale.X;
      float num11 = transform.rot.x * num3;
      float num12 = transform.rot.w * num2;
      this._31 = (num11 + num12) * transform.scale.Z;
      this._13 = (num11 - num12) * transform.scale.X;
      this._14 = 0.0f;
      this._24 = 0.0f;
      this._34 = 0.0f;
      this._44 = 1f;
    }

    public D3DMatrix(Vector3 rot, Vector3 origin)
    {
      double num1 = (double) rot.X * 3.14159274101257 / 180.0;
      float num2 = (float) ((double) rot.Y * 3.14159274101257 / 180.0);
      float num3 = (float) ((double) rot.Z * 3.14159274101257 / 180.0);
      float num4 = (float) Math.Sin(num1);
      float num5 = (float) Math.Cos(num1);
      float num6 = (float) Math.Sin((double) num2);
      float num7 = (float) Math.Cos((double) num2);
      float num8 = (float) Math.Sin((double) num3);
      float num9 = (float) Math.Cos((double) num3);
      this._11 = num5 * num7;
      this._12 = num5 * num6;
      this._13 = num4;
      this._14 = 0.0f;
      this._21 = (float) ((double) num8 * (double) num4 * (double) num7 - (double) num9 * (double) num6);
      this._22 = (float) ((double) num8 * (double) num4 * (double) num6 + (double) num9 * (double) num7);
      this._23 = -num8 * num5;
      this._24 = 0.0f;
      this._31 = (float) -((double) num9 * (double) num4 * (double) num7 + (double) num8 * (double) num6);
      this._32 = (float) ((double) num7 * (double) num8 - (double) num9 * (double) num4 * (double) num6);
      this._33 = num9 * num5;
      this._34 = 0.0f;
      this._41 = origin.X;
      this._42 = origin.Y;
      this._43 = origin.Z;
      this._44 = 1f;
    }
  }
}
