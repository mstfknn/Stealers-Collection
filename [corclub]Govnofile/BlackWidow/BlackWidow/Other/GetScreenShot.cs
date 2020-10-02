using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlackWidow.Other
{
    class GetScreenShot
    {
        private static Bitmap _bitmap;
        private static DateTime _timestamp;
        public static void TakeAndSave()
        {
            _bitmap = TakeScreenShot(Screen.PrimaryScreen);
            _timestamp = DateTime.Now;
            SaveScreenShot();
        }

        private static void SaveScreenShot()
        {
            string text = string.Format("{0:dd-MM-yyyy HH-mm-ss}", _timestamp) + ".jpg";
            _bitmap.Save(text, ImageFormat.Jpeg);
        }

        private static Bitmap TakeScreenShot(Screen currentScreen)
        {
            Bitmap bitmap = new Bitmap(currentScreen.Bounds.Width, currentScreen.Bounds.Height, PixelFormat.Format32bppArgb);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.CopyFromScreen(currentScreen.Bounds.X, currentScreen.Bounds.Y, 0, 0, currentScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
            return bitmap;
        }
    }
}
