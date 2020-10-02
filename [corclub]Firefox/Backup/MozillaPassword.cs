using System;
using System.Collections.Generic;
using System.Text;

namespace MozillaPasswords
{
    /// <summary>
    /// Stocke un couple login/mot de passe d'un logiciel Mozilla
    /// </summary>
    public class MozillaPassword
    {
        private string sitename;
        //les noms des champs HTML dans lequel ont été saisis les login/mot de passe
        private string username_field,password_field;
        private string username, password;
        //domain du site contenant le formulaire HTML
        private string domain;
        //chemin du profile Mozilla stockant ce mot de passe
        private string profile_dir;
        //logiciel Mozilla
        private MozillaSoft soft;
        //mot de passe maitre
        private string sdr_password;

        /// <summary>
        /// Renvoie ou définit le mot de passe mettre du profile contenant le mot de passe
        /// null par défaut
        /// </summary>
        public string SDRPassword
        {
            get { return sdr_password; }
            set { sdr_password = value; }
        }
	
        public string SiteName
        {
            get { return sitename; }
        }
        public string UsernameField
        {
            get { return username_field; }
        }
        public string PasswordField
        {
            get { return password_field; }
        }

        /// <summary>
        /// Nom d'utilisateur décrypté
        /// </summary>
        public string Username
        {
            get {
                if (soft == MozillaSoft.Thunderbird)
                {
                    //dans thunderbird, l'username est dans l'URI de la boite mail
                    return new Uri(this.sitename).UserInfo;
                }
                else
                {
                    //dans les autres logiciels (FireFox), il est crypté
                    MozillaSDR sdr = new MozillaSDR(profile_dir, sdr_password);
                    string ret = sdr.DecryptPassword(this.username);
                    sdr.Dispose();
                    return ret;
                }
            }
        }
        public string Password
        {
            get {
                MozillaSDR sdr = new MozillaSDR(profile_dir, sdr_password);
                if (soft == MozillaSoft.Thunderbird)
                {
                    //dans thunderbird, le password est simplement encodé en base64
                    string ret = sdr.DecodePassword(this.password);
                    sdr.Dispose();
                    return ret;
                }
                else
                {
                    //dans les autres logiciels (FireFox), il est crypté
                    string ret = sdr.DecryptPassword(this.password);
                    sdr.Dispose();
                    return ret;
                }
            }
        }
        public string Domain
        {
            get { return domain; }
        }

        public MozillaPassword(MozillaSoft soft,string profile_dir,string sitename,string username_field,string password_field,string username,string password,string domain)
        {
            this.sitename = sitename;
            this.username_field = username_field;
            this.password_field = password_field;
            this.username = username;
            this.password = password;
            this.domain = domain;
            this.profile_dir = profile_dir;
            this.soft = soft;
        }

        /// <summary>
        /// Renvoie le password brut crypté
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string GetPasswordText(string password)
        {
            return this.password;
        }
        /// <summary>
        /// Renvoie l'username brut crypté
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string GetUsernameText(string password)
        {
            return this.username;
        }
    }
}
