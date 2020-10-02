namespace DaddyRecovery.Browsers.Chromium
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Helpers;

    public static class GetApplication
    {
        /// <summary>
        /// Список полных имен браузеров  
        /// </summary>
        private static readonly KeyValuePair<string, string>[] List = new KeyValuePair<string, string>[]
        {
            new KeyValuePair<string, string>("Chrome", "Google Chrome"),
            new KeyValuePair<string, string>("Opera Stable" , "Opera"),
            new KeyValuePair<string, string>("Neon" , "Opera Neon"),
            new KeyValuePair<string, string>("Citrio" , "Citrio"),
            new KeyValuePair<string, string>("MapleStudio" , "CoolNovo"),
            new KeyValuePair<string, string>("Avant Profiles" , "Avant Webkit"),
            new KeyValuePair<string, string>("Iridium" , "Iridium"),
            new KeyValuePair<string, string>("Yandex" , "Yandex"),
            new KeyValuePair<string, string>("Orbitum" , "Orbitum"),
            new KeyValuePair<string, string>("Kinza" , "Kinza"),
            new KeyValuePair<string, string>("Brave-Browser" , "Brave"),
            new KeyValuePair<string, string>("Amigo" , "Amigo"),
            new KeyValuePair<string, string>("Torch" , "Torch"),
            new KeyValuePair<string, string>("Comodo" , "Comodo Dragon"),
            new KeyValuePair<string, string>("Kometa" , "Kometa"),
            new KeyValuePair<string, string>("Vivaldi" , "Vivaldi"),
            new KeyValuePair<string, string>("Nichrome" , "Nichrome Rambler"),
            new KeyValuePair<string, string>("Epic" , "Epic Privacy"),
            new KeyValuePair<string, string>("CocCoc" , "CocCoc"),
            new KeyValuePair<string, string>("360Browser" , "360Browser"),
            new KeyValuePair<string, string>("Sputnik" , "Sputnik"),
            new KeyValuePair<string, string>("Uran" , "Uran"),
            new KeyValuePair<string, string>("CentBrowser" , "CentBrowser"),
            new KeyValuePair<string, string>("7Star" , "7Star"),
            new KeyValuePair<string, string>("Elements" , "Elements"),
            new KeyValuePair<string, string>("Superbird" , "Superbird"),
            new KeyValuePair<string, string>("Chedot" , "Chedot"),
            new KeyValuePair<string, string>("Suhba" , "Suhba"),
            new KeyValuePair<string, string>("Mustang" , "Mustang"),
        };

        /// <summary>
        /// Получаем все имена браузеров из списка
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetNameCycle(string path)
        {
            foreach (KeyValuePair<string, string> v in List.Where(v => path.Contains(v.Key)).Select(v => v))
            {
                return v.Value;
            }
            return path;
        }

        /// <summary>
        /// Метод для получения имени браузера
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
        public static string GetTitle(List<string> Item)
        {
            foreach (string Br in Item)
            {
                string Name = CombineEx.GetDirName(Br);
                if (CombineEx.ExistsDir(Name)) return CombineEx.GetFileName(Name);
            }
            return "Неизвестный браузер";
        }
    }
}