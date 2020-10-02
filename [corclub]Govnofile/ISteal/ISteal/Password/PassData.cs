namespace ISteal.Password
{
    public class PassData
    {
        public string Url { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Program{ get; set; }

        public override string ToString()
        {
            return string.Format("SiteUrl : {0}\r\nLogin : {1}\r\nPassword : {2}\r\nProgram : {3}\r\n——————————————————————————————————", 
            new object[]
            {
               Url, Login, Password, Program
            });
        }
    }
}