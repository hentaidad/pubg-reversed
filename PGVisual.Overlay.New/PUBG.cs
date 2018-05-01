// Decompiled with JetBrains decompiler
// Type: PGVisual.Overlay.PUBG
// Assembly: PGVisual.Overlay, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36E310DD-04E8-450E-8620-AD5F611898A4
// Assembly location: C:\Users\TylerW10\AppData\Local\Temp\xpg\PGVisual.Overlay.exe

using l00l;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace PGVisual.Overlay
{
    internal unsafe class PUBG
    {
        private ActorEncrypt encrypt = new ActorEncrypt();
        private const ulong _ofsUWorld = 67454544;
        private const ulong _ofsGNames = 66283840;
        //private ulong GNamesPtr;
        //private ulong UWorldPtr;
        private ulong _processId;
        private ConcurrentBag<Actor> _actors;
        private ConcurrentBag<Item> _items;
        private ConcurrentBag<Car> _cars;
        private Thread _updateThread;
        private bool _running;
        private int _width;
        private int _heigth;
        private byte[] readItem;
        private HSClient _client;
        private ulong _baseAddress;
       // private ulong UWorld;

        public bool farDistance = false;
        private int[] _playerClasses;
        private int[] _itemClasses;
        private FCameraCacheEntry _cameraCache;
        private LocalActor _localActor;
        public long RetF8 = 0;
        long RetF10 = 0;

        [DllImport("User32.dll")]
        public static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey); // Keys enumeration
        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(System.Int32 vKey);

        public short getKeyF8()
        {
            return GetAsyncKeyState(Module1.VK_F8);
        }

        public short getKeyF9()
        {
            return GetAsyncKeyState(Module1.VK_F9);
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        public ConcurrentBag<Actor> Players
        {
            get
            {
                return this._actors;
            }
        }

        public ConcurrentBag<Item> Items
        {
            get
            {
                return this._items;
            }
        }

        public LocalActor LocalActor
        {
            get
            {
                return this._localActor;
            }
        }

        public ulong BaseAddress
        {
            get
            {
                return this._baseAddress;
            }
            set
            {
                this._baseAddress = value;
            }
        }

        public PUBG(ulong processId, HSClient client)
        {
            this._processId = processId;
            this._actors = new ConcurrentBag<Actor>();
            this._items = new ConcurrentBag<Item>();
            //this._cars = new ConcurrentBag<Car>();

            this._running = true;
            this._client = client;
            this._updateThread = new Thread(new ThreadStart(this.UpdateThread));
            this._playerClasses = new int[2];
            this._itemClasses = new int[2];
        }

        public void SetDimension(int width, int heigth)
        {
            this._width = width;
            this._heigth = heigth;
        }



        public Vector3 GetActorPos(ulong entity)
        {
            Vector3 position3D;
            ulong rootComp;
            rootComp = Program.Client.ReadPtr(entity + 392UL);
            position3D = Program.Client.ReadMemory<Vector3>(rootComp + 596UL);

            return position3D;
        }

        public bool worldToScreen(Vector3 world, out Vector3 screen)
        {
            screen.Z = 0.0f;
            FMinimalViewInfo pov = this._cameraCache.POV;
            D3DMatrix d3Dmatrix = new D3DMatrix(pov.Rotation, new Vector3(0.0f, 0.0f, 0.0f));
            Vector3 mv1 = new Vector3(d3Dmatrix._11, d3Dmatrix._12, d3Dmatrix._13);
            Vector3 mv2 = new Vector3(d3Dmatrix._21, d3Dmatrix._22, d3Dmatrix._23);
            Vector3 mv3 = new Vector3(d3Dmatrix._31, d3Dmatrix._32, d3Dmatrix._33);
            Vector3 vector3_1 = world - pov.Location;
            Vector3 vector3_2 = new Vector3(vector3_1.DotProduct(mv2), vector3_1.DotProduct(mv3), vector3_1.DotProduct(mv1));
            if ((double)vector3_2.Z < 1.0)
                vector3_2.Z = 1f;
            float fov = pov.FOV;
            float num1 = (float)this._width / 2f;
            float num2 = (float)this._heigth / 2f;
            screen.X = num1 + vector3_2.X * (num1 / (float)Math.Tan((double)fov * 3.14159274101257 / 360.0)) / vector3_2.Z;
            screen.Y = num2 - vector3_2.Y * (num1 / (float)Math.Tan((double)fov * 3.14159274101257 / 360.0)) / vector3_2.Z;
            if ((double)screen.X >= 0.0 && (double)screen.Y <= (double)this._heigth && (double)screen.Y >= 0.0)
                return (double)screen.Y <= (double)this._heigth;
            return false;
        }

        private bool InitClassIds()
        {
            for (int Index = 0; Index < 200000; ++Index)
            {

                string globalName = this.getGlobalName(Index);
                if (globalName.GetHashCode() == -1818364965)
                {
                    this._playerClasses[0] = Index;
                }
                else if (globalName.GetHashCode() == 736643556)
                {
                    this._playerClasses[1] = Index;
                }
                else if (globalName.GetHashCode() == -767281852) //Component
                {
                    this._itemClasses[0] = Index;
                    Console.WriteLine(globalName);
                }
                else if (globalName.GetHashCode() == 1078012530) //Group
                {
                    this._itemClasses[1] = Index;
                    Console.WriteLine(globalName);
                }

                if (this._playerClasses[0] != 0 && this._playerClasses[1] != 0 && this._itemClasses[0] != 0 && this._itemClasses[1] != 0)
                {
                    globalName = string.Empty;
                    return true;
                }





            }
            return false;
        }







        private string getGlobalName(int Index)
        {
            int num1 = 3145728;
            if (Index > num1)
                return "NULL";
            int num2 = 16384;
            int num3 = Index / num2;
            int num4 = Index % num2;
            ulong num5 = this._client.ReadPtr(this._baseAddress + 0x3DF2F48);
            if ((long)num5 != 0L)
            {
                ulong num6 = this._client.ReadPtr(num5 + (ulong)(8 * num3));
                if ((long)num6 != 0L)
                {
                    ulong num7 = this._client.ReadPtr(num6 + (ulong)(8 * num4));
                    if ((long)num7 != 0L)
                    {
                        byte[] array = new byte[1000];
                        this._client.ReadMemory(num7 + 16UL, array);
                        int newSize = Array.IndexOf<byte>(array, (byte)0);
                        if (newSize < 1)
                            return "NULL";
                        Array.Resize<byte>(ref array, newSize);
                        return Encoding.ASCII.GetString(array);
                    }
                }
            }
            return "NULL";
        }


        public bool Start()
        {
            Console.WriteLine("Initializing Motor...");

            this.BaseAddress = this._client.GetModuleBase("TslGame.exe");

            if (this.BaseAddress <= 0UL)
                return false;
            //this.encrypt.init_decryption();
            //this.GNamesPtr = this.encrypt.DecryptPtr(this.BaseAddress + 66283840UL);



            if (!this.InitClassIds())
                return false;
            this._updateThread.Start();
            return true;
        }

        private ulong getActor(int index, ulong entityList)
        {
            //return this.encrypt.DecryptPtr(entityList + 384UL * (ulong) index);
            return this._client.ReadPtr(entityList + 0x8 * (ulong)index);
        }

        bool IsBitSet(byte b, int pos)
        {
            return (b & (1 << pos)) != 0;
        }

        private void UpdateThread()
        {

            while (PUBG.FindWindow("UnrealWindow", (string)null) != IntPtr.Zero)
            {
                //if (Convert.ToBoolean(GetAsyncKeyState(Module1.VK_F10) & 0x8000))
                //{
                //    this.DroppedItemGroupArray = 0L;
                //    this.pADroppedItemGroup = 0L;
                //    this.pUItem = 0L;
                //    this.Itemid = 0;


                //    Console.ForegroundColor = ConsoleColor.DarkBlue;
                //    Console.WriteLine("Cleared");
                //    Console.ForegroundColor = ConsoleColor.Black;
                //}

                //if (Convert.ToBoolean(GetAsyncKeyState(Module1.VK_F9) & 0x8000))
                //{
                //    this._items = new ConcurrentBag<Item>();

                //    Console.ForegroundColor = ConsoleColor.DarkBlue;
                //    Console.WriteLine("Cleared2");
                //    Console.ForegroundColor = ConsoleColor.Black;
                //}

                if (Convert.ToBoolean(GetAsyncKeyState(Module1.VK_F7) & 0x8000))
                {
                    if (farDistance)
                    {
                        farDistance = false;
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Switched to short Distance Pointers");
                        Console.ForegroundColor = ConsoleColor.White;
                    } else
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Switched to long Distance Pointers");
                        Console.ForegroundColor = ConsoleColor.White;
                        farDistance = true;
                    }
                }


                //if (Convert.ToBoolean(GetAsyncKeyState(Module1.VK_F6) & 0x8000))
                //{
                //    ulong pointer = this._client.ReadPtr(this.BaseAddress + 0x3D88F10);
                //    ulong UWorld = this._client.ReadPtr(pointer + 0x0);


                //}





                try
                {
                    // ulong UWorld = this._client.ReadPtr(this._client.ReadPtr(this.BaseAddress + 0x3d87f10));
                    //ulong UWorld = this._client.ReadPtr(this._client.ReadPtr(this.BaseAddress + 0x262D30));

                    ulong UWorld = this._client.ReadPtr(this._client.ReadPtr(this.BaseAddress + 0x3D88F10));

                    if ((long)UWorld != 0L)
                    {
                        ulong GameInstance = this._client.ReadPtr(UWorld + 320UL);
                        if ((long)GameInstance != 0L)
                        {
                            ulong LocalPlayerArray = this._client.ReadPtr(GameInstance + 56UL);
                            if ((long)LocalPlayerArray != 0L)
                            {
                                ulong localPlayer = this._client.ReadPtr(LocalPlayerArray);
                                if ((long)localPlayer != 0L)
                                {
                                    ulong PlayerController = this._client.ReadPtr(localPlayer + 48UL);
                                    if ((long)PlayerController != 0L)
                                    {
                                        ulong PlayerCameraManager = this._client.ReadPtr(PlayerController + 1096UL);
                                        if ((long)PlayerCameraManager != 0L)
                                        {
                                            this._cameraCache = this._client.ReadMemory<FCameraCacheEntry>(PlayerCameraManager + 1056UL);
                                            ulong num5 = this._client.ReadPtr(localPlayer + 88UL);
                                            if ((long)num5 != 0L)
                                            {
                                                ulong num6 = this._client.ReadPtr(num5 + 128UL);
                                                if ((long)num6 != 0L)
                                                {
                                                    ulong num7 = this._client.ReadPtr(num6 + 48UL);
                                                    if ((long)num7 != 0L)
                                                    {
                                                        ulong actorPtr = this._client.ReadPtr(PlayerController + 1064UL);
                                                        if ((long)actorPtr != 0L)
                                                        {
                                                            this._localActor = new LocalActor(actorPtr, localPlayer);
                                                            ulong entityList = this._client.ReadPtr(num7 + 0xA0); //entity list
                                                            int num8 = this._client.ReadMemory<int>(num7 + 0xA8); //entity
                                                        
                                                            if (Program.Pubg.farDistance == true)
                                                            {
                                                                entityList = this._client.ReadPtr(num7 + 0xB0); //entity list
                                                                num8 = this._client.ReadMemory<int>(num7 + 0xB8); //entity
                                                            }

                                                            if (Program.Pubg.farDistance == false)
                                                            {
                                                                entityList = this._client.ReadPtr(num7 + 0xA0); //entity list
                                                                num8 = this._client.ReadMemory<int>(num7 + 0xA8); //entity
                                                            }


                                                            if ((long)entityList != 0L)
                                                            {
                                                                if (num8 > 0)
                                                                {
                                                                    ConcurrentBag<Actor> concurrentBag = new ConcurrentBag<Actor>();
                                                                    ConcurrentBag<Item> concurrentBagI = new ConcurrentBag<Item>();
                                                                    //ConcurrentBag<Car> concurrentBagCar = new ConcurrentBag<Car>();

                                                                    for (int index = 0; index < num8; ++index)
                                                                    {
                                                                        ulong entity = this.getActor(index, entityList);
                                                                        if ((long)entity != 0L)
                                                                        {
                                                                            int id = this._client.ReadMemory<int>(entity + 24UL);
                                                                            if (id == this._playerClasses[0] || id == this._playerClasses[1])
                                                                            {
                                                                                Actor actor2 = new Actor(entity);
                                                                                if (actor2 != null && actor2.IsValid())
                                                                                    concurrentBag.Add(actor2);
                                                                            }


                                                                            if (id == this._itemClasses[1] && (true == false))
                                                                            {


                                                                                ulong DroppedItemGroupArray = this._client.ReadPtr(entity + 0x2E8);

                                                                                if ((long)DroppedItemGroupArray != 0L)
                                                                                {
                                                                                    int itemCount = this._client.ReadMemory<int>(entity + 0x2F0);




                                                                                   // TBitArray test = this._client.ReadMemory<TBitArray>(entity + 0x300);

                                                                                    for (int j = 0; j < itemCount; j++)
                                                                                    {

                                                                                        Console.WriteLine("funny");

                                                                                            ulong pADroppedItemGroup = this._client.ReadPtr(DroppedItemGroupArray + j * 0x10);
                                                                                            int Itemid = this._client.ReadMemory<int>(pADroppedItemGroup + 0x18);
                                                                                            if (Itemid == this._itemClasses[0])
                                                                                            {

                                                                                                ulong pUItem = this._client.ReadPtr(pADroppedItemGroup + 0x4e0);
                                                                                                //Console.WriteLine("item");
                                                                                                ulong pUItemFString = this._client.ReadPtr(pUItem + 0x40);
                                                                                                //Console.WriteLine("fstring");
                                                                                                ulong pItemName = this._client.ReadPtr(pUItemFString + 0x28);
                                                                                                //Console.WriteLine("itemname");
                                                                                                this.readItem = null;
                                                                                                this.readItem = this._client.read(pItemName);
                                                                                                var result = System.Text.Encoding.UTF8.GetString(readItem);
                                                                                                var str = Regex.Replace(result, @"\s", "");
                                                                                                str = Regex.Replace(str, @"[^\x0d\x0a\x20-\x7e\t]", "");


                                                                                                if (str != string.Empty && (long)pUItem != 0L && (long)pUItemFString != 0L && (long)pItemName != 0L)
                                                                                                {
                                                                                                    ulong rootComp = Program.Client.ReadPtr(entity + 392UL);
                                                                                                    this._cameraCache = this._client.ReadMemory<FCameraCacheEntry>(PlayerCameraManager + 1056UL);
                                                                                                    var position = this._client.ReadMemory<Vector3>(pADroppedItemGroup + 0x254);
                                                                                                var relative = this._client.ReadMemory<Vector3>(pADroppedItemGroup + 0x2D0);
                                                                                                    Console.WriteLine(str + " " + position.X + " " + position.Y + " " + position.Z + " " + relative.X + " " + relative.Y + " " + relative.Z);

                                                                                                    Vector3 screenloc;
                                                                                                    bool abc = worldToScreen(position + relative, out screenloc);

                                                                                                    if (abc)
                                                                                                    {
                                                                                                        Item item = new Item(id, str, screenloc, entity);
                                                                                                        item.position3D = position + relative;
                                                                                                        concurrentBagI.Add(item);
                                                                                                        Console.WriteLine("abc");
                                                                                                    }

                                                                                                }
                                                                                            }
                                                                                        }

                                                                                    }
                                                                                    DroppedItemGroupArray = 0;
                                                                                }

                                                                        }
                                                                    }
                                  this._actors = concurrentBag;
                                  this._items = concurrentBagI;
                                  //this._cars = concurrentBagCar;

                                                                }
                                                            }
                            }
                          }
                        }
                      }
                    }
                  }
                }
              }
            }
          }
        }
        catch (Exception ex)
        {
        }
        Thread.Sleep(1000/60);
      }
    }

        private bool IsBitSet(uint v, object p)
        {
            throw new NotImplementedException();
        }
    }
}
