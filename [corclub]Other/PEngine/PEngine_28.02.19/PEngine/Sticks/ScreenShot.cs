namespace PEngine.Sticks
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class ScreenShot
    {
        public static void Shoot(string SaveFile)
        {
            try
            {
                using (var bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height))
                {
                    using (var g = Graphics.FromImage(bmp))
                    {
                        try
                        {
                            g.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);
                            bmp.Save(SaveFile);
                        }
                        catch (Win32Exception) { }
                        catch (ArgumentNullException) { }
                        catch (ExternalException) { }
                    }
                }
            }
            catch (Exception) { }
        }
    }
}