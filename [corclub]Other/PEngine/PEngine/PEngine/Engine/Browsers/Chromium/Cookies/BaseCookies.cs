namespace PEngine.Engine.Browsers.Chromium.Cookies
{
    using System;
    using System.Text;

    public class BaseCookies
    {
        public string Application { get; set; }
        public string HostKey { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public long Expires_utc { get; set; }
        public long Last_access_utc { get; set; }
        public string EncryptedValue { get; set; }
        public int Value { get; set; }

        public override string ToString()
        {
            var SB = new StringBuilder();
            SB.AppendLine($"Browser: {this.Application}");
            SB.AppendLine($"HostKey: {this.HostKey}");
            SB.AppendLine($"Name: {this.Name}");
            SB.AppendLine($"Path: {this.Path}");
            SB.AppendLine($"Expires_utc: {this.Expires_utc}");
            SB.AppendLine($"Last_access_utc: {this.Last_access_utc}");
            SB.AppendLine($"EncryptedValue: {this.EncryptedValue}");
            SB.AppendLine($"Value: {this.Value}{Environment.NewLine}");
            return SB?.ToString();
        }
    }
}