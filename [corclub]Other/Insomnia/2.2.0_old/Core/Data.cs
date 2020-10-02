using System;
using System.Collections.Generic;
using System.Text;

namespace insomnia
{
    internal class Data
    {
        public string decrypt(string text)
        {
            try
            {
                string newString = String.Empty;
                char[] c = text.ToCharArray();
                int R = c[0];
                int L = text.Length - 1;
                int len = text.Length;
                text = "";
                char temp;

                switch (R % 4)
                {
                    case 0:
                        for (int F = 1; F < L; F++, L--)
                        {
                            temp = c[F];
                            c[F] = c[L];
                            c[L] = temp;
                            F += 2;
                            if (F < L)
                            {
                                temp = c[F];
                                c[F] = c[F - 1];
                                c[F - 1] = temp;
                            }
                        }
                        for (int k = 1; k < len; k++)
                        {
                            text += c[k];
                        }
                        break;
                    case 1:
                        for (int F = 1; F < L; F++, L--)
                        {
                            temp = c[F];
                            c[F] = c[L];
                            c[L] = temp;
                            L -= 2;
                            if (F < L)
                            {
                                temp = c[L];
                                c[L] = c[L + 1];
                                c[L + 1] = temp;
                            }
                        }
                        for (int k = 1; k < len; k++)
                        {
                            text += c[k];
                        }
                        break;
                    case 2:

                        temp = c[L];
                        c[L] = c[(L / 2)];
                        c[(L / 2)] = temp;

                        temp = c[1];
                        c[1] = c[(L / 2) + 1];
                        c[(L / 2) + 1] = temp;

                        for (int k = 1; k < len; k++)
                        {
                            text += c[k];
                        }
                        break;
                    case 3:
                        for (int k = 1; k < len; k++)
                        {
                            text += c[k];
                        }
                        break;
                }

                c = text.ToCharArray();
                int avg = c[0];

                for (int k = 1; k < text.Length; k += 2)
                {
                    if (c[k] >= 80)
                    {
                        newString += (char)((int)c[k + 1] + avg - 48);
                    }
                    else
                    {
                        newString += (char)(avg - (int)c[k + 1] + 48);
                    }
                }
                return newString;
            }
            catch
            {
                Environment.Exit(0);
                return null;
            }
        }
    }
}
