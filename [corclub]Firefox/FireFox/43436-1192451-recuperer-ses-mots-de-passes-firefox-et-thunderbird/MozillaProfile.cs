using System;
using System.Collections.Generic;
using System.Text;

namespace MozillaPasswords
{
    /// <summary>
    /// Stocke les infos sur un profile Mozilla
    /// </summary>
    public class MozillaProfile
    {
        private bool relative;
        private string profile_path;
        private string name;
        private string profiles_ini_dir;
        private MozillaSoft soft;

        public string Name
        {
            get { return name; }
        }

        public string Path
        {
            get { return profile_path; }
        }

        /// <summary>
        /// Indique si Path est un chemin absolu ou relatif au dossier des profiles
        /// </summary>
        public bool IsRelative
        {
            get { return relative; }
        }

        public MozillaProfile(MozillaSoft soft,string profiles_ini_dir,string name,string path,bool relative)
        {
            this.name = name;
            this.relative = relative;
            this.profile_path = path;
            this.profiles_ini_dir = profiles_ini_dir;
            this.soft = soft;
        }

        /// <summary>
        /// Renvoie un décrypteur de mot de passe (signons.txt ou sigons2.txt)
        /// </summary>
        /// <param name="bVersion2">Version de signonsX.txt (true version 2)</param>
        /// <returns></returns>
        public MozillaSignons GetSignons(bool bVersion2)
        {
            string profile_full_path;
            if (this.relative)
                profile_full_path = profiles_ini_dir +"\\"+ this.profile_path;
            else
                profile_full_path = this.profile_path;

            return new MozillaSignons(soft,profile_full_path,bVersion2);
        }
    }
}
