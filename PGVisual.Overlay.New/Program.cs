// Decompiled with JetBrains decompiler
// Type: PGVisual.Overlay.Program
// Assembly: PGVisual.Overlay, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 36E310DD-04E8-450E-8620-AD5F611898A4
// Assembly location: C:\Users\TylerW10\AppData\Local\Temp\xpg\PGVisual.Overlay.exe

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace PGVisual.Overlay
{
  internal static class Program
  {
    public static PUBG Pubg = (PUBG) null;
    public static Process PubgProcess = (Process) null;
    public static string HWID = "";
    public static string Username = "";
    public static string Password = "";
    public static HSClient Client;

    [DllImport("user32.dll", SetLastError = true)]
    private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    private static void CheckHWID()
    {
      int num = 0;
      while (true)
      {
        if (new WebClient() { Proxy = ((IWebProxy) null) }.DownloadString("http://5.196.102.147/xpg/xpg.php?username=" + Program.Username + "&password=" + Program.Password + "&hwid=" + Program.HWID + "&session=1336").Length != 32)
          ++num;
        if (num == 3)
          Environment.Exit(0);
        Thread.Sleep(60000);
      }
    }

    [STAThread]
    private static void Main(string[] args)
    {
      Console.ForegroundColor = ConsoleColor.White;
      string[] guid = Guid.NewGuid().ToString().Split('-');
      Console.Title = (guid[0] + guid[1] + "|VentrixCode#6897|" + guid[2] + guid[3]).Replace("-", "");
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      for (int i =0;i < 3;i++)
            {
                Console.WriteLine(Guid.NewGuid().ToString().Replace("-",""));

            }
            Console.WriteLine("Waiting for G");
      do
      {
        //Program.PubgProcess = ((IEnumerable<Process>) Process.GetProcessesByName("TslGame")).FirstOrDefault<Process>();
        Program.PubgProcess = Process.GetProcesses().FirstOrDefault(x => x.ProcessName.GetHashCode() == -219875547);
        Thread.Sleep(100);
      }
      while (Program.PubgProcess == null);
      Console.WriteLine("G Proc {0}", (object) Program.PubgProcess.Id);
      Console.WriteLine("Init PayPass");
      Program.Client = new HSClient();
      if (Program.Client.connect())
      {
          Console.ForegroundColor = ConsoleColor.Green;
          Console.WriteLine("PayPass step 1/2 OK");
          Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("Loading PayPass, wait few seconds...");
        Thread.Sleep(2000);
        if (Program.Client.Handshake(Program.PubgProcess.Id, 10))
        {
          Console.ForegroundColor = ConsoleColor.Green;
          Console.WriteLine("PayPass step 2/2 OK");
          Console.WriteLine("PayPass Completed");
           Console.ForegroundColor = ConsoleColor.White;
          IntPtr zero = IntPtr.Zero;
          Console.WriteLine("Looking for Fenster...");
          IntPtr window;
          do
          {
            window = Program.FindWindow("UnrealWindow", (string) null);
            Thread.Sleep(100);
          }
          while (window == IntPtr.Zero);
          Console.ForegroundColor = ConsoleColor.Green;
          Console.WriteLine("Fenster Found {0}", (object) window.ToInt32());
          Console.ForegroundColor = ConsoleColor.White;
          Thread.Sleep(1000);
          Program.Pubg = new PUBG((ulong) Program.PubgProcess.Id, Program.Client);
          if (!Program.Pubg.Start())
          {
            Console.WriteLine("Failed to load");
            Environment.Exit(0);
          }
          Application.Run((Form) new PGVisual.Overlay.Overlay(window));
        }
        else
          Console.ForegroundColor = ConsoleColor.Red;
          Console.WriteLine("PayPass step 2 fail");
      }
      else
       Console.ForegroundColor = ConsoleColor.Red;
       Console.WriteLine("PayPass step 1 fail");
    }
  }
}
