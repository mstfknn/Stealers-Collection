using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Globalization;

namespace MsnPasswordFinder
{
    class LanguageSupport
    {
        public static string Language = string.Empty;

        public static string GetNativeLanguage()
        {
            return CultureInfo.CurrentCulture.Parent.NativeName;
        }

        private string GetLanguageDependedString(string key)
        {
            string Key = "";

            switch (LanguageSupport.Language)
            {
                case "English":
                    Key = "EN_" + key;
                    break;
                case "Turkish":
                    Key = "TR_" + key;
                    break;
                default:
                    Key = "EN_" + key;
                    break;
            }

            return LangRes.ResourceManager.GetString(Key, LangRes.Culture);
        }

        public string this[string key]
        {
            get
            {
                return this.GetLanguageDependedString(key);
            }
        }
    }
}
