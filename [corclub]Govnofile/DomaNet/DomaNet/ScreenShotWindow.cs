namespace DomaNet
{
    using System.Drawing;
    using System.Windows.Forms;

    public class ScreenShotWindow
    {
        public static void Take(string SaveFile)
        {
            using (var bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height))
            {
                using (var g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);
                    bmp.Save(SaveFile);
                }
            }
        }
    }
}