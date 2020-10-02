namespace PEngine.Helpers
{
    using System;
    using System.IO;

    public class SaveData
    {
        public static void SaveFile(string FileName, string Text)
        {
            try
            {
                File.AppendAllText(FileName, Text);
            }
            catch (IOException) { }
            catch (ArgumentException) { }
            catch (NotSupportedException) { }
            catch (UnauthorizedAccessException) { }
        }
    }
}