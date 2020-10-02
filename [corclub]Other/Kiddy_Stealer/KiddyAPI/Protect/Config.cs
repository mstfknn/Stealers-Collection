using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace KiddyAPI.Protect
{
    /// <summary>
    /// Конфигурация различных модулей
    /// </summary>
    public class Config
    {
        /// <summary>
        /// Поле, которое вернет true, если программа запущена под администратором, иначе false
        /// </summary>
        public static bool IsAdmin
        {
            get { return WindowsIdentity.GetCurrent().Owner.IsWellKnown(WellKnownSidType.AccountAdministratorSid); }
        }
        
    }
}
