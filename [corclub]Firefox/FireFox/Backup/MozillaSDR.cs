using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Reflection;

namespace MozillaPasswords
{
    /*
     * SDR decoder in C#, based on Wejn <wejn@box.cz>, itself based on sdrtest.c
     * All conditions mentioned in sdrtest apply.
     * 
     * Nécessite les dlls de NSPR4 et de NSS3 dans le dossier de l'exécutable (par ex : bin\Debug et bin\Release)
     * Télécharger ces deux libraries à :
     * -> ftp://ftp.mozilla.org/pub/mozilla.org/nspr/releases/v4.6.7/src/nspr-4.6.7.tar.gz
     * -> https://ftp.mozilla.org/pub/mozilla.org/security/nss/releases/NSS_3_11_4_RTM/msvc6.0/WINNT5.0_OPT.OBJ/nss-3.11.4.zip
     * 
     * Extraire les dlls du dossier lib de ces archives et les placer dans le dossier de l'exécutable
     *
     * Author: ShareVB
     */
    public class MozillaSDR : IDisposable
    {
        //un item de sécurité de NSS
        [StructLayout(LayoutKind.Sequential)]
        private struct SECItem {
            public Int32 type;
            public IntPtr data;
            public Int32 len;
        };
        //définit la callback qui sera appelée pour récupérer le mot de passe maitre du profile qui stocke le mot de passe à décrypter
        [DllImport("nss3.dll",CallingConvention=CallingConvention.Cdecl)]
        private static extern int PK11_SetPasswordFunc(MulticastDelegate callback);
        //initialise NSS
        [DllImport("nss3.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int NSS_Init([MarshalAs(UnmanagedType.LPStr)]string profile_dir);
        //décode une chaine en base64 et renvoie un SECItem contenant la chaine décodée
        [DllImport("nss3.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr NSSBase64_DecodeBuffer(IntPtr p1, IntPtr p2, [MarshalAs(UnmanagedType.LPStr)]string encoded, int encoded_len);
        //décrypte un texte avec PK11SDR_Encrypt
        [DllImport("nss3.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int PK11SDR_Decrypt(IntPtr encrypted_item, ref SECItem text, int p1);
        //Libère les données pointées par un SECItem (bDestroy indique si l'on doit aussi libérer le SECItem)
        [DllImport("nss3.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int SECITEM_FreeItem(ref SECItem item, int bDestroy);
        [DllImport("nss3.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int SECITEM_FreeItem(IntPtr item,int bDestroy);
        //libère NSS
        [DllImport("nss3.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int NSS_Shutdown();
        //libère les ressources de NSS
        [DllImport("libnspr4.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int PR_Cleanup();

        //mot de passe maître
        private static string password = null;

        //permet à NSS de récupérer le mot de passe maitre
        private static IntPtr My_GetModulePassword(IntPtr slot, int retry, IntPtr arg){
                if(retry == 0 && password != null){
                        return Marshal.StringToHGlobalAnsi(password);
                }else{
                        return IntPtr.Zero;
                }
        }

        /// <summary>
        /// Initialise NSS avec un dossier de profile et son mot de passe maître (habituellement null)
        /// </summary>
        /// <param name="profile_dir">dossier du profile contenant le mot de passe à décrypter</param>
        /// <param name="master_passwd">mot de passe maitre du profile</param>
        /// <returns>0 si succès, != 0 si erreur</returns>
        private int nss_init(string profile_dir, string master_passwd) {
            int    rv;
            password = master_passwd;

            //Environment.CurrentDirectory = profile_dir;

            /*
             * Initialize the Security libraries.
             */
            //crée ou récupère un délégué CDECL pour fournir comme callback de récupération de mot de passe
            MulticastDelegate deleg = CallConvDelegateBuilder.CreateDelegateInstance(
                typeof(MozillaSDR), "My_GetModulePassword",CallingConvention.Cdecl, null);
            PK11_SetPasswordFunc(deleg);

            rv = NSS_Init(profile_dir);
            if (rv != 0)
                return -1;
            else
                return 0;
        }

        /// <summary>
        /// Décrypte un texte pour le profile en cours et le mot de passe maitre en cours
        /// </summary>
        /// <param name="b64_encrypted_data">texte encodé en base 64 à décrypter</param>
        /// <param name="buf">résultat décrypté</param>
        /// <returns>0 si succès, != 0 si erreur</returns>
        private int nss_decrypt(string b64_encrypted_data, out string buf)
        {
            //valeur de retour
            int retval = 0;
            //résultat du déchiffrement
            int rv;
            //texte déchiffré
            SECItem text = new SECItem();
            //texte décodé mais crypté
            IntPtr ok = IntPtr.Zero;
            buf = string.Empty;

            text.data = IntPtr.Zero; text.len = 0;

            /* input was base64 encoded.  Decode it. */
            ok = NSSBase64_DecodeBuffer(IntPtr.Zero, IntPtr.Zero,
                   b64_encrypted_data, b64_encrypted_data.Length);

            if (ok == IntPtr.Zero)
            {
                retval = -1;
            }
            else
            {
                /* Decrypt the value */
                rv = PK11SDR_Decrypt(ok, ref text, 0);
                if (rv != 0)
                {
                    retval = -2;
                }
                else
                {
                    //récupère la chaine ASCII
                    buf = Marshal.PtrToStringAnsi(text.data,text.len);
                    /* == cleanup and adjust pointers == */
                    SECITEM_FreeItem(ref text, 0);
                }
                SECITEM_FreeItem(ok, 1);
            }

            //libère le texte décodé alloué par la fonction de décodage
            if (text.data != IntPtr.Zero)
                Marshal.FreeHGlobal(text.data);

            return retval;
        }

        /// <summary>
        /// Décode une chaine en base64
        /// </summary>
        /// <param name="b64_encrypted_data">texte encodé</param>
        /// <param name="buf">résultat du décodage</param>
        /// <returns>0 si succès, != 0 si erreur</returns>
        private int nss_decode(string b64_encrypted_data, out string buf)
        {
            //valeur de retour
            int retval = 0;
            //texte déchiffré
            SECItem text = new SECItem();
            //texte décodé mais crypté
            IntPtr ok = IntPtr.Zero;
            buf = string.Empty;

            /* input was base64 encoded.  Decode it. */
            ok = NSSBase64_DecodeBuffer(IntPtr.Zero, IntPtr.Zero,
                   b64_encrypted_data, b64_encrypted_data.Length);

            if (ok == IntPtr.Zero)
            {
                retval = -1;
            }
            else
            {
                //récupère le SECItem pointé
                text = (SECItem)Marshal.PtrToStructure(ok, typeof(SECItem));
                //récupère la chaine contenue dans le SECItem
                buf = Marshal.PtrToStringAnsi(text.data, text.len);

                //libère le SECItem
                SECITEM_FreeItem(ok, 1);
            }

            return retval;
        }

        /// <summary>
        /// Libère NSS
        /// </summary>
        /// <returns>0 si succès, != 0 si erreur</returns>
        private int nss_free()
        {
            if (NSS_Shutdown() != 0)
                return -1;
            //peut bloquer dans certain cas, donc cleanup par windows à la fin du programme (solution suggérée par Mozilla)
            //PR_Cleanup();
            return 0;
        }

        /// <summary>
        /// Initialise NSS avec un dossier de profile et son mot de passe maître (habituellement null)
        /// </summary>
        /// <param name="profile_dir">dossier du profile contenant le mot de passe à décrypter</param>
        /// <param name="master_passwd">mot de passe maitre du profile</param>
        public MozillaSDR(string profile_dir, string password)
        {
            nss_init(profile_dir,password);
        }

        /// <summary>
        /// Décrypte un mot de passe crypté
        /// </summary>
        /// <param name="encrypted">texte encrypté et encodé en base 64</param>
        /// <returns>renvoie le texte décrypté ou string.Empty</returns>
        public string DecryptPassword(string encrypted)
        {
            string ret = null;
            if (nss_decrypt(encrypted, out ret) != 0)
                return string.Empty;
            else
                return ret;
        }
        /// <summary>
        /// Décode un texte encodé en base64
        /// </summary>
        /// <param name="encoded">texte encodé</param>
        /// <returns>renvoie le texte décodé ou string.Empty</returns>
        public string DecodePassword(string encoded)
        {
            string ret = null;
            if (nss_decode(encoded, out ret) != 0)
                return string.Empty;
            else
                return ret;
        }

        #region IDisposable Members

        public void Dispose()
        {
            //libère les ressources on dispose
            nss_free();
        }

        #endregion
    }
}
