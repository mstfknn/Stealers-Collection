using System;
using System.Runtime.InteropServices;

namespace Strange.Features
{
	// Token: 0x0200000A RID: 10
	internal class WebCam
	{
		// Token: 0x06000013 RID: 19
		[DllImport("avicap32.dll")]
		public static extern IntPtr capCreateCaptureWindowA(string lpszWindowName, int dwStyle, int X, int Y, int nWidth, int nHeight, int hwndParent, int nID);

		// Token: 0x06000014 RID: 20
		[DllImport("user32")]
		public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

		// Token: 0x06000015 RID: 21 RVA: 0x00002554 File Offset: 0x00000754
		internal static void GetWebCamPicture(string path)
		{
			IntPtr intPtr = Marshal.StringToHGlobalAnsi(path);
			IntPtr hWnd = WebCam.capCreateCaptureWindowA("VFW Capture", -1073741824, 0, 0, 640, 480, 0, 0);
			WebCam.SendMessage(hWnd, 1034u, 0, 0);
			WebCam.SendMessage(hWnd, 1034u, 0, 0);
			WebCam.SendMessage(hWnd, 1049u, 0, intPtr.ToInt32());
			WebCam.SendMessage(hWnd, 1035u, 0, 0);
			WebCam.SendMessage(hWnd, 16u, 0, 0);
		}

		// Token: 0x04000001 RID: 1
		private const int WM_CAP_DRIVER_CONNECT = 1034;

		// Token: 0x04000002 RID: 2
		private const int WM_CAP_DRIVER_DISCONNECT = 1035;

		// Token: 0x04000003 RID: 3
		private const int WS_CHILD = 1073741824;

		// Token: 0x04000004 RID: 4
		private const int WS_POPUP = -2147483648;

		// Token: 0x04000005 RID: 5
		private const int WM_CAP_SAVEDIB = 1049;

		// Token: 0x04000006 RID: 6
		private const int WM_CLOSE = 16;
	}
}
