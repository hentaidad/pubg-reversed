// Decompiled with JetBrains decompiler
// Type: PGVisual.Overlay.HSClient
// Assembly: PGVisual.Overlay, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36E310DD-04E8-450E-8620-AD5F611898A4
// Assembly location: C:\Users\TylerW10\AppData\Local\Temp\xpg\PGVisual.Overlay.exe

using Microsoft.Win32;
using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;

namespace R6S_ESP
{
  internal class HSClient
  {
    private Socket _tcpSocket;

    public HSClient()
    {
      this._tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    }

    public bool SendInt(int value)
    {
      return this._tcpSocket.Send(BitConverter.GetBytes(value)) > 0;
    }

    public bool SendUInt(uint value)
    {
      return this._tcpSocket.Send(BitConverter.GetBytes(value)) > 0;
    }

    public bool SendUInt64(ulong value)
    {
      return this._tcpSocket.Send(BitConverter.GetBytes(value)) > 0;
    }

    public bool SendOperation(HSClient.HSProxyOperation operation)
    {
      return this.SendInt((int) operation);
    }

    public bool SendString(string str)
    {
      return this.SendInt(str.Length) && this._tcpSocket.Send(Encoding.ASCII.GetBytes(str)) > 0;
    }

    public int ReadInt()
    {
      byte[] buffer = new byte[4];
      if (this._tcpSocket.Receive(buffer) > 0)
        return BitConverter.ToInt32(buffer, 0);
      throw new Exception("read failure");
    }

    public ulong ReadUInt64()
    {
      byte[] buffer = new byte[8];
      if (this._tcpSocket.Receive(buffer) > 0)
        return BitConverter.ToUInt64(buffer, 0);
      throw new Exception("read failure");
    }

    public bool ReadBytes(byte[] buffer)
    {
      if (this._tcpSocket.Receive(buffer) > 0)
        return true;
      throw new Exception("read failure");
    }

    private static T ByteArrayToStructure<T>(byte[] bytes) where T : struct
    {
      GCHandle gcHandle = GCHandle.Alloc((object) bytes, GCHandleType.Pinned);
      try
      {
        return (T) Marshal.PtrToStructure(gcHandle.AddrOfPinnedObject(), typeof (T));
      }
      finally
      {
        gcHandle.Free();
      }
    }

    public T ReadMemory<T>(ulong addr) where T : struct
    {
      byte[] numArray = new byte[Marshal.SizeOf(typeof (T))];
      if (!this.ReadMemory(addr, numArray))
        throw new Exception("read memory failed");
      return HSClient.ByteArrayToStructure<T>(numArray);
    }
        
    public byte[] read(ulong addr)
        {
            byte[] numArray = new byte[64];
            if (!this.ReadMemory(addr, numArray))
                throw new Exception("read memory failed");
            return numArray;
        }



        public ulong ReadPtr(ulong addr)
    {
      return this.ReadMemory<ulong>(addr);
    }

    public bool ReadMemory(ulong addr, byte[] buffer)
    {
      return this.SendOperation(HSClient.HSProxyOperation.READ_BUFFER) && this.SendUInt64(addr) && (this.SendInt(buffer.Length) && this.ReadInt() == 1) && (this.ReadInt() > 0 && this.ReadBytes(buffer));
    }

    public ulong GetModuleBase(string module)
    {
      if (this.SendOperation(HSClient.HSProxyOperation.GET_MODULEBASE) && this.SendString(module))
        return this.ReadUInt64();
      return 0;
    }

    public bool Handshake(int processId, int version)
    {
      if (this.SendUInt(3735928559U) && this.SendInt(version) && this.SendInt(processId))
        return this.ReadInt() == 1;
      return false;
    }

    public bool connect()
    {
      using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\HSProxy"))
      {
        if (registryKey != null)
        {
          int int32 = Convert.ToInt32(registryKey.GetValue("Port"));
          try
          {
            this._tcpSocket.Connect("127.0.0.1", int32);
                        Console.WriteLine(this._tcpSocket.LocalEndPoint.ToString());
          }
          catch (Exception ex)
          {
            return false;
          }
          return true;
        }
      }
      return false;
    }

    public enum HSProxyOperation
    {
      WRITE_BUFFER,
      READ_BUFFER,
      CREATE_THREAD,
      ALLOCATE,
      GET_MODULEBASE,
    }
  }
}
