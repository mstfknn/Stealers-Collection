using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MozillaPasswords
{
    /// <summary>
    /// Parse les fichiers signons.txt et signons2.txt
    /// </summary>
    public class MozillaSignons : IEnumerable<MozillaPassword>
    {
        //liste des sites à ne pas conserver les mots de passe
        private List<string> no_save_sites = new List<string>();
        //liste des mots de passe
        private List<MozillaPassword> password_list = new List<MozillaPassword>();

        public MozillaSignons(MozillaSoft soft,string profile_path,bool bVersion2)
        {
            //fichier signons dans un profile
            string szSignons = profile_path + @"\signons" + (bVersion2 ? "2" : "") + ".txt";

            if (!File.Exists(szSignons)) return;

            //lecture de ce fichier txt
            StreamReader signons = new StreamReader(szSignons);

            //lire la ligne de signature (#2c ou #2d)
            signons.ReadLine();
            string line = string.Empty;
            //tant que l'on n'atteint pas une ligne contenant seulement un "."
            while ((line = signons.ReadLine()) != ".")
                //on est dans les sites à ne pas conserver les mots de passes
                no_save_sites.Add(line);

            string username, password, username_field, password_field, sitename, domain = string.Empty;
            //tant que l'on n'est pas à la fin du fichier
            while ((line = signons.ReadLine()) != null)
            {
                //normalement, la dernière ligne du fichier est vide
                if (line == string.Empty)
                    break;

                //un slot est constitué du noms du site suivi d'un ou plusieurs entregistrements 
                //contenant les infos pour présaisir le formulaire de login du site
                sitename = line;

                //pour chaque enregistrement de 4 ou 5 lignes
                while (true)
                {
                    //récupère le champs de l'username
                    username_field = signons.ReadLine();
                    //si la ligne est un "." alors enregistrement suivant
                    if (username_field == null || username_field == ".")
                        break;
                    username = signons.ReadLine();
                    //le nom du champs de mot de passe est précédé d'une "*"
                    password_field = signons.ReadLine().Substring(1);
                    password = signons.ReadLine();
                    //pour thunderbird, le mot de passe est précédé d'un ~
                    if (password.StartsWith("~")) password = password.Substring(1);
                    //dans la version 2, il y a le nom du domain en +
                    if (bVersion2) domain = signons.ReadLine();

                    //ajoute le mot de passe à la liste
                    password_list.Add(new MozillaPassword(soft,profile_path,sitename, username_field, password_field, username, password, domain));
                }
            }
        }

        /// <summary>
        /// Récupère un enregistrement de mot de passe par son index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public MozillaPassword this[int index]
        {
            get {
                if (index >= 0 && index < password_list.Count)
                    return password_list[index];
                else
                    return null;
            }
        }


        #region IEnumerable<MozillaPassword> Members

        public IEnumerator<MozillaPassword> GetEnumerator()
        {
            return password_list.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return password_list.GetEnumerator();
        }
        #endregion

        /// <summary>
        /// renvoie le nombre de site ne devant pas enregistrer le mot de passe
        /// </summary>
        public int NoPasswordSiteCount
        {
            get { return no_save_sites.Count; }
        }
        /// <summary>
        /// renvoie le nombre de mots de passe
        /// </summary>
        public int PasswordCount
        {
            get { return password_list.Count; }
        }

        /// <summary>
        /// renvoie le indexième site ne devant pas enregistrer le mot de passe
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetNoPasswordSite(int index)
        {
            if (index >= 0 && index < no_save_sites.Count)
                return no_save_sites[index];
            else
                return null;
        }
    }
}
