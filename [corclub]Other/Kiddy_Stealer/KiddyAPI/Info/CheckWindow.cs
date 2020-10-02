using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KiddyAPI.Injector.NativeAPI;

namespace KiddyAPI.Info
{
    /// <summary>
    /// Доступ к методу получения активного окна
    /// </summary>
    public class CheckWindow
    {
        /// <summary>
        /// Получаем активное окно пользователя
        /// </summary>
        /// <param name="name">Имя программы для отслеживания</param>
        /// <returns>Вернет true, если окно совпадает с name</returns>
        public static bool GetWindow(string name)
        {
            IntPtr hWnd = GetForegroundWindow();
            int pId = 0;
            GetWindowThreadProcessId(hWnd, ref pId);
            var p = Process.GetProcessById(pId);
            if (p.MainWindowTitle.Contains(name))
                return true;
            else
            {
                return false;
            }

        }
    }
}
