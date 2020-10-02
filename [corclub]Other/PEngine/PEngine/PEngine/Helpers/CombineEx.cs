namespace PEngine.Helpers
{
    using System;
    using System.IO;

    public class CombineEx
    {
        public static string CombinationEx(string pathdir, string filename)
        {
            try
            {
                return Directory.Exists(pathdir) || File.Exists(filename) ? Path.Combine(pathdir, filename) : string.Empty;
            }
            catch (Exception) { return string.Empty; }
        }

        public static string Combination(string pathdir, string filename)
        {
            try
            {
                return Directory.Exists(pathdir) ? Path.Combine(pathdir, filename) : Path.Combine(pathdir, filename);
            }
            catch { return string.Empty; }
        }

        public static void MoveExeFile(string from, string to)
        {
            try
            {
                File.Move(from, to);
            }
            catch (IOException) { }
            catch (ArgumentException) { }
            catch (UnauthorizedAccessException) { }
        }

        public static void DeleteFile(string filename)
        {
            if (File.Exists(filename))
            {
                try
                {
                    File.Delete(filename);
                }
                catch (IOException) { }
                catch (UnauthorizedAccessException) { }
                catch (ArgumentException) { }
            }
        }

        public static void FileCopy(string from, string to, bool status)
        {
            try
            {
                File.Copy(from, to, status);
            }
            catch (UnauthorizedAccessException) { }
            catch (IOException) { }
            catch (ArgumentException) { }
        }

        public static void CreateDir(string dir)
        {
            if (!Directory.Exists(dir))
            {
                try
                {
                    Directory.CreateDirectory(dir);
                }
                catch (IOException) { }
                catch (ArgumentException) { }
                catch (UnauthorizedAccessException) { }
                catch (NotSupportedException) { }
            }
        }

        public static void DeleteDir(string dirname, bool recursive = true)
        {
            if (Directory.Exists(dirname))
            {
                try
                {
                    Directory.Delete(dirname, recursive);
                }
                catch (IOException) { }
                catch (ArgumentException) { }
                catch (UnauthorizedAccessException) { }
                catch (NotSupportedException) { }
            }
        }
    }
}
