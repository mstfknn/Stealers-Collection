// Decompiled with JetBrains decompiler
// Type: AMV
// Assembly: dangunronpa, Version=1.2.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 3958FDFD-FC81-45EF-BFDA-37B7EDF3E7D7
// Assembly location: C:\Users\лёха\Desktop\dangunronpa.exe

using System.IO;
using System.Net;

public static class AMV
{
  public static string DontKillMe(string efwrgfwrgfrwg848)
  {
    HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(efwrgfwrgfrwg848);
    httpWebRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
    return new StreamReader(httpWebRequest.GetResponse().GetResponseStream()).ReadToEnd();
  }
}
