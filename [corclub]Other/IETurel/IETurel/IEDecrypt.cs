using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

internal static class IEDecrypt
{
    public static string Decrypt(byte[] blob)
    {
        return Encoding.UTF8.GetString(ProtectedData.Unprotect(blob, null, DataProtectionScope.CurrentUser));
    }

    public static string DecryptStringFromBase64(string cipherText)
    {
        return Encoding.UTF8.GetString(Convert.FromBase64String(cipherText));
    }

    public static int GetHexVal(char hex)
    {
        int num = hex;
        return (num - ((num < 0x3a) ? 0x30 : 0x37));
    }

    public static byte[] HexStringToByteArray(string hex)
    {
        var buffer = new byte[hex.Length >> 0x1];
        for (var i = 0; i < (hex.Length >> 0x1); i++)
        {
            buffer[i] = (byte)((GetHexVal(hex[i << 0x1]) << 0x4) + GetHexVal(hex[(i << 0x1) + 0x1]));
        }
        return buffer;
    }

    public static string ANSIStringFromHex(string hexString)
    {
        var builder = new StringBuilder(hexString.Length / 0x2);
        for (var i = 0; i < hexString.Length; i += 0x2)
        {
            builder.Append(Convert.ToChar(Convert.ToUInt32(hexString.Substring(i, 0x2), 0x10)));
        }
        return builder.ToString();
    }

    private static T ByteArrayToStructure<T>(byte[] bytes) where T : struct
    {
        var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
        handle.Free();
        return (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
    }

    private static string GetURLHashString(string wstrURL)
    {
        var zero = IntPtr.Zero;
        var phHash = IntPtr.Zero;
        SafeNativeMethods.CryptAcquireContext(out zero, string.Empty, string.Empty, 1, 0xf0000000);
        if (!SafeNativeMethods.CryptCreateHash(zero, Enums.ALG_ID.CALG_SHA1, IntPtr.Zero, 0, ref phHash))
        {
            throw new Win32Exception(Marshal.GetLastWin32Error());
        }
        var builder = new StringBuilder(0x2a);
        if (SafeNativeMethods.CryptHashData(phHash, Encoding.Unicode.GetBytes(wstrURL), (wstrURL.Length + 1) * 2, 0))
        {
            uint pdwDataLen = 20;
            var pbData = new byte[pdwDataLen];
            if (!SafeNativeMethods.CryptGetHashParam(phHash, Enums.HashParameters.HP_HASHVAL, pbData, ref pdwDataLen, 0))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            byte num2 = 0;
            builder.Length = 0;
            for (var i = 0; i < pdwDataLen; i++)
            {
                num2 = (byte)(num2 + pbData[i]);
                builder.AppendFormat($"{0:X2}", pbData[i]);
            }
            builder.AppendFormat("{0:X2}", num2);
            SafeNativeMethods.CryptDestroyHash(phHash);
        }
        SafeNativeMethods.CryptReleaseContext(zero, 0);
        return builder.ToString();
    }

    private static bool DoesURLMatchWithHash(string urlHash)
    {
        using (var hklmHive_x64 = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, (Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32)))
        {
            using (var runKey = hklmHive_x64.OpenSubKey(@"Software\Microsoft\Internet Explorer\IntelliForms\Storage2", (Environment.Is64BitOperatingSystem ? true : false)))
            {
                if (runKey == null)
                {
                    return false;
                }
                foreach (var str in runKey.GetValueNames())
                {
                    if (str == urlHash)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    public static bool DecryptIePassword(string url, List<string[]> dataList)
    {
        if (!DoesURLMatchWithHash(GetURLHashString(url)))
        {
            return false;
        }
        using (var hklmHive_x64 = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, (Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32)))
        {
            using (var runKey = hklmHive_x64.OpenSubKey(@"Software\Microsoft\Internet Explorer\IntelliForms\Storage2", (Environment.Is64BitOperatingSystem ? true : false)))
            {
                if (runKey == null)
                {
                    return false;
                }
                var dst = new byte[2 * (url.Length + 1)];
                Buffer.BlockCopy(url.ToCharArray(), 0, dst, 0, url.Length * 2);
                var bytes = ProtectedData.Unprotect((byte[])runKey.GetValue(GetURLHashString(url)), dst, DataProtectionScope.CurrentUser);
                Structures.IEAutoComplteSecretHeader structure = ByteArrayToStructure<Structures.IEAutoComplteSecretHeader>(bytes);
                if (bytes.Length >= ((structure.dwSize + structure.dwSecretInfoSize) + structure.dwSecretSize))
                {
                    var num2 = Marshal.SizeOf(typeof(Structures.SecretEntry));
                    var buffer4 = new byte[structure.dwSecretSize];
                    var srcOffset = (int)(structure.dwSize + structure.dwSecretInfoSize);
                    Buffer.BlockCopy(bytes, srcOffset, buffer4, 0, buffer4.Length);

                    if (dataList == null)
                    {
                        dataList = new List<string[]>();
                    }
                    else
                    {
                        dataList.Clear();
                    }
                    srcOffset = Marshal.SizeOf(structure);
                    for (var i = 0; i < structure.IESecretHeader.dwTotalSecrets / 2; i++)
                    {
                        var buffer5 = new byte[num2];
                        Buffer.BlockCopy(bytes, srcOffset, buffer5, 0, buffer5.Length);
                        var entry = ByteArrayToStructure<Structures.SecretEntry>(buffer5);
                        var item = new string[3];
                        var buffer6 = new byte[entry.dwLength * 2];
                        Buffer.BlockCopy(buffer4, (int)entry.dwOffset, buffer6, 0, buffer6.Length);
                        item[0] = Encoding.Unicode.GetString(buffer6);
                        srcOffset += num2;
                        Buffer.BlockCopy(bytes, srcOffset, buffer5, 0, buffer5.Length);
                        entry = ByteArrayToStructure<Structures.SecretEntry>(buffer5);
                        var buffer7 = new byte[entry.dwLength * 2];
                        Buffer.BlockCopy(buffer4, (int)entry.dwOffset, buffer7, 0, buffer7.Length);
                        item[1] = Encoding.Unicode.GetString(buffer7);
                        item[2] = GetURLHashString(url);
                        dataList.Add(item);
                        srcOffset += num2;
                    }
                }
            }
        }
        return true;
    }

    private const int ALG_CLASS_HASH = 0x8000;
    private const int ALG_SID_SHA1 = 4;
    private const uint CRYPT_VERIFYCONTEXT = 0xf0000000;
    private const uint PROV_RSA_FULL = 1;
}