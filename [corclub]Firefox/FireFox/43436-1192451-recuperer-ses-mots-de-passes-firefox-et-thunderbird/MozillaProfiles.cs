using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MozillaPasswords
{
    /// <summary>
    /// Soft Mozilla
    /// </summary>
    public enum MozillaSoft
    {
        FireFox,
        Thunderbird
    }
    /// <summary>
    /// Stocke la liste des profiles Mozilla d'un logiciel de la suite Mozilla
    /// Normalement, il n'y a qu'un seul profile par défaut par logiciel sans mot de passe maître
    /// </summary>
    public class MozillaProfiles : IEnumerable<MozillaProfile>
    {
        private List<MozillaProfile> profiles = new List<MozillaProfile>();

        public MozillaProfiles(MozillaSoft soft)
        {
            string profiles_ini_dir = null;

            //dossier des profiles suivant le sauf
            switch (soft)
            {
                case MozillaSoft.FireFox:
                    profiles_ini_dir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Mozilla\Firefox";
                    break;
                case MozillaSoft.Thunderbird:
                    profiles_ini_dir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Thunderbird";
                    break;
            }

            string profile_ini = profiles_ini_dir + @"\profiles.ini";
            if (!File.Exists(profile_ini)) return;

            //ouverture du fichier INI
            StreamReader p_ini = new StreamReader(profile_ini);

            string line = string.Empty;
            string name=string.Empty,path=string.Empty;
            bool relative=false;

            line = p_ini.ReadLine();
            //tant que l'on n'est pas à la fin du fichier
            while (line != null)
            {
                //si la ligne contient le début de la section d'un profile
                if (line.ToLower().StartsWith("[profile"))
                {
                    line = p_ini.ReadLine();
                    //tant que l'on n'est pas à la fin du profile en cours
                    while (line != null && !line.StartsWith("["))
                    {
                        //récupère les options de la section
                        if (line.StartsWith("IsRelative="))
                            relative = (int.Parse(line.Substring(11)) == 1 ? true : false);
                        else if (line.StartsWith("Name="))
                            name = line.Substring(5);
                        else if (line.StartsWith("Path="))
                            path = line.Substring(5);

                        line = p_ini.ReadLine();
                    }

                    //ajoute le profile récupéré
                    profiles.Add(new MozillaProfile(soft,profiles_ini_dir, name,path,relative));
                }
                //sinon passer la ligne
                else
                    line = p_ini.ReadLine();
            }
        }

        /// <summary>
        /// Récupère un profile de la liste par son index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public MozillaProfile this[int index]
        {
            get {
                if (index >= 0 && index < profiles.Count)
                    return profiles[index];
                else
                    return null;
            }
        }



        #region IEnumerable<MozillaProfile> Members

        public IEnumerator<MozillaProfile> GetEnumerator()
        {
            return profiles.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return profiles.GetEnumerator();
        }

        #endregion
    }
}
