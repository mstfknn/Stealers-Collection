using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using BrowserPasswords.MozillaPasswords;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace BrowserPasswords
{
	// Token: 0x0200000B RID: 11
	[DesignerGenerated]
	internal partial class frmMain : Form
	{
		// Token: 0x0600002E RID: 46 RVA: 0x00009370 File Offset: 0x00007570
		[DebuggerNonUserCode]
		public frmMain()
		{
			base.Load += this.frmMain_Load;
			base.FormClosed += this.frmMain_FormClosed;
			this.PST = new CProtectStore();
			this.PIE = new CIEPasswords();
			this.PHTTPFTP = new CHttpFtpAuths();
			this.PIESEVEN = new CIE7Passwords();
			this.InitializeComponent();
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000093FC File Offset: 0x000075FC
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00009414 File Offset: 0x00007614
		public virtual Button cmdRefresh
		{
			get
			{
				return this._cmdRefresh;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.cmdRefresh_Click);
				if (this._cmdRefresh != null)
				{
					this._cmdRefresh.Click -= value2;
				}
				this._cmdRefresh = value;
				if (this._cmdRefresh != null)
				{
					this._cmdRefresh.Click += value2;
				}
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00009460 File Offset: 0x00007660
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00009478 File Offset: 0x00007678
		public virtual ColumnHeader _lvIE_ColumnHeader_1
		{
			get
			{
				return this.__lvIE_ColumnHeader_1;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this.__lvIE_ColumnHeader_1 = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00009484 File Offset: 0x00007684
		// (set) Token: 0x06000035 RID: 53 RVA: 0x0000949C File Offset: 0x0000769C
		public virtual ColumnHeader _lvIE_ColumnHeader_2
		{
			get
			{
				return this.__lvIE_ColumnHeader_2;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this.__lvIE_ColumnHeader_2 = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000094A8 File Offset: 0x000076A8
		// (set) Token: 0x06000037 RID: 55 RVA: 0x000094C0 File Offset: 0x000076C0
		public virtual ColumnHeader _lvIE_ColumnHeader_3
		{
			get
			{
				return this.__lvIE_ColumnHeader_3;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this.__lvIE_ColumnHeader_3 = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000094CC File Offset: 0x000076CC
		// (set) Token: 0x06000039 RID: 57 RVA: 0x000094E4 File Offset: 0x000076E4
		public virtual ColumnHeader _lvIE_ColumnHeader_4
		{
			get
			{
				return this.__lvIE_ColumnHeader_4;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this.__lvIE_ColumnHeader_4 = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000094F0 File Offset: 0x000076F0
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00009508 File Offset: 0x00007708
		public virtual ColumnHeader _lvIE_ColumnHeader_5
		{
			get
			{
				return this.__lvIE_ColumnHeader_5;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this.__lvIE_ColumnHeader_5 = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00009514 File Offset: 0x00007714
		// (set) Token: 0x0600003D RID: 61 RVA: 0x0000952C File Offset: 0x0000772C
		public virtual ListView lvIE
		{
			get
			{
				return this._lvIE;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._lvIE = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00009538 File Offset: 0x00007738
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00009550 File Offset: 0x00007750
		public virtual Button cmdAllIEPass
		{
			get
			{
				return this._cmdAllIEPass;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.cmdAllIEPass_Click);
				if (this._cmdAllIEPass != null)
				{
					this._cmdAllIEPass.Click -= value2;
				}
				this._cmdAllIEPass = value;
				if (this._cmdAllIEPass != null)
				{
					this._cmdAllIEPass.Click += value2;
				}
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000040 RID: 64 RVA: 0x0000959C File Offset: 0x0000779C
		// (set) Token: 0x06000041 RID: 65 RVA: 0x000095B4 File Offset: 0x000077B4
		public virtual Button cmdAllIEForms
		{
			get
			{
				return this._cmdAllIEForms;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.cmdAllIEForms_Click);
				if (this._cmdAllIEForms != null)
				{
					this._cmdAllIEForms.Click -= value2;
				}
				this._cmdAllIEForms = value;
				if (this._cmdAllIEForms != null)
				{
					this._cmdAllIEForms.Click += value2;
				}
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00009600 File Offset: 0x00007800
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00009618 File Offset: 0x00007818
		public virtual Button cmdIEDel
		{
			get
			{
				return this._cmdIEDel;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.cmdIEDel_Click);
				if (this._cmdIEDel != null)
				{
					this._cmdIEDel.Click -= value2;
				}
				this._cmdIEDel = value;
				if (this._cmdIEDel != null)
				{
					this._cmdIEDel.Click += value2;
				}
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00009664 File Offset: 0x00007864
		// (set) Token: 0x06000045 RID: 69 RVA: 0x0000967C File Offset: 0x0000787C
		public virtual Button cmdIELine
		{
			get
			{
				return this._cmdIELine;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.cmdIELine_Click);
				if (this._cmdIELine != null)
				{
					this._cmdIELine.Click -= value2;
				}
				this._cmdIELine = value;
				if (this._cmdIELine != null)
				{
					this._cmdIELine.Click += value2;
				}
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000096C8 File Offset: 0x000078C8
		// (set) Token: 0x06000047 RID: 71 RVA: 0x000096E0 File Offset: 0x000078E0
		public virtual TabPage _SSTab1_TabPage2
		{
			get
			{
				return this.__SSTab1_TabPage2;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this.__SSTab1_TabPage2 = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000096EC File Offset: 0x000078EC
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00009704 File Offset: 0x00007904
		public virtual ColumnHeader _lvHttpFtp_ColumnHeader_1
		{
			get
			{
				return this.__lvHttpFtp_ColumnHeader_1;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this.__lvHttpFtp_ColumnHeader_1 = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00009710 File Offset: 0x00007910
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00009728 File Offset: 0x00007928
		public virtual ColumnHeader _lvHttpFtp_ColumnHeader_2
		{
			get
			{
				return this.__lvHttpFtp_ColumnHeader_2;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this.__lvHttpFtp_ColumnHeader_2 = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00009734 File Offset: 0x00007934
		// (set) Token: 0x0600004D RID: 77 RVA: 0x0000974C File Offset: 0x0000794C
		public virtual ColumnHeader _lvHttpFtp_ColumnHeader_3
		{
			get
			{
				return this.__lvHttpFtp_ColumnHeader_3;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this.__lvHttpFtp_ColumnHeader_3 = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00009758 File Offset: 0x00007958
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00009770 File Offset: 0x00007970
		public virtual ColumnHeader _lvHttpFtp_ColumnHeader_4
		{
			get
			{
				return this.__lvHttpFtp_ColumnHeader_4;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this.__lvHttpFtp_ColumnHeader_4 = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000050 RID: 80 RVA: 0x0000977C File Offset: 0x0000797C
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00009794 File Offset: 0x00007994
		public virtual ColumnHeader _lvHttpFtp_ColumnHeader_5
		{
			get
			{
				return this.__lvHttpFtp_ColumnHeader_5;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this.__lvHttpFtp_ColumnHeader_5 = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000052 RID: 82 RVA: 0x000097A0 File Offset: 0x000079A0
		// (set) Token: 0x06000053 RID: 83 RVA: 0x000097B8 File Offset: 0x000079B8
		public virtual ListView lvHttpFtp
		{
			get
			{
				return this._lvHttpFtp;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._lvHttpFtp = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000097C4 File Offset: 0x000079C4
		// (set) Token: 0x06000055 RID: 85 RVA: 0x000097DC File Offset: 0x000079DC
		public virtual Button cmdDelAuthLine
		{
			get
			{
				return this._cmdDelAuthLine;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.cmdDelAuthLine_Click);
				if (this._cmdDelAuthLine != null)
				{
					this._cmdDelAuthLine.Click -= value2;
				}
				this._cmdDelAuthLine = value;
				if (this._cmdDelAuthLine != null)
				{
					this._cmdDelAuthLine.Click += value2;
				}
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00009828 File Offset: 0x00007A28
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00009840 File Offset: 0x00007A40
		public virtual Button cmdDelAllAuth
		{
			get
			{
				return this._cmdDelAllAuth;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.cmdDelAllAuth_Click);
				if (this._cmdDelAllAuth != null)
				{
					this._cmdDelAllAuth.Click -= value2;
				}
				this._cmdDelAllAuth = value;
				if (this._cmdDelAllAuth != null)
				{
					this._cmdDelAllAuth.Click += value2;
				}
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000058 RID: 88 RVA: 0x0000988C File Offset: 0x00007A8C
		// (set) Token: 0x06000059 RID: 89 RVA: 0x000098A4 File Offset: 0x00007AA4
		public virtual TabPage _SSTab1_TabPage5
		{
			get
			{
				return this.__SSTab1_TabPage5;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this.__SSTab1_TabPage5 = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600005A RID: 90 RVA: 0x000098B0 File Offset: 0x00007AB0
		// (set) Token: 0x0600005B RID: 91 RVA: 0x000098C8 File Offset: 0x00007AC8
		public virtual ColumnHeader _lvIE7_ColumnHeader_1
		{
			get
			{
				return this.__lvIE7_ColumnHeader_1;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this.__lvIE7_ColumnHeader_1 = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600005C RID: 92 RVA: 0x000098D4 File Offset: 0x00007AD4
		// (set) Token: 0x0600005D RID: 93 RVA: 0x000098EC File Offset: 0x00007AEC
		public virtual ColumnHeader _lvIE7_ColumnHeader_2
		{
			get
			{
				return this.__lvIE7_ColumnHeader_2;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this.__lvIE7_ColumnHeader_2 = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600005E RID: 94 RVA: 0x000098F8 File Offset: 0x00007AF8
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00009910 File Offset: 0x00007B10
		public virtual ColumnHeader _lvIE7_ColumnHeader_3
		{
			get
			{
				return this.__lvIE7_ColumnHeader_3;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this.__lvIE7_ColumnHeader_3 = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000060 RID: 96 RVA: 0x0000991C File Offset: 0x00007B1C
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00009934 File Offset: 0x00007B34
		public virtual ColumnHeader _lvIE7_ColumnHeader_4
		{
			get
			{
				return this.__lvIE7_ColumnHeader_4;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this.__lvIE7_ColumnHeader_4 = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00009940 File Offset: 0x00007B40
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00009958 File Offset: 0x00007B58
		public virtual ColumnHeader _lvIE7_ColumnHeader_5
		{
			get
			{
				return this.__lvIE7_ColumnHeader_5;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this.__lvIE7_ColumnHeader_5 = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00009964 File Offset: 0x00007B64
		// (set) Token: 0x06000065 RID: 101 RVA: 0x0000997C File Offset: 0x00007B7C
		public virtual ColumnHeader _lvIE7_ColumnHeader_6
		{
			get
			{
				return this.__lvIE7_ColumnHeader_6;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this.__lvIE7_ColumnHeader_6 = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00009988 File Offset: 0x00007B88
		// (set) Token: 0x06000067 RID: 103 RVA: 0x000099A0 File Offset: 0x00007BA0
		public virtual ListView lvIE7
		{
			get
			{
				return this._lvIE7;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._lvIE7 = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000099AC File Offset: 0x00007BAC
		// (set) Token: 0x06000069 RID: 105 RVA: 0x000099C4 File Offset: 0x00007BC4
		public virtual Button cmdIE7Del
		{
			get
			{
				return this._cmdIE7Del;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.cmdIE7Del_Click);
				if (this._cmdIE7Del != null)
				{
					this._cmdIE7Del.Click -= value2;
				}
				this._cmdIE7Del = value;
				if (this._cmdIE7Del != null)
				{
					this._cmdIE7Del.Click += value2;
				}
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00009A10 File Offset: 0x00007C10
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00009A28 File Offset: 0x00007C28
		public virtual TabPage _SSTab1_TabPage6
		{
			get
			{
				return this.__SSTab1_TabPage6;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this.__SSTab1_TabPage6 = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00009A34 File Offset: 0x00007C34
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00009A4C File Offset: 0x00007C4C
		public virtual TabControl SSTab1
		{
			get
			{
				return this._SSTab1;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._SSTab1 = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00009A58 File Offset: 0x00007C58
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00009A70 File Offset: 0x00007C70
		public virtual Label Label2
		{
			get
			{
				return this._Label2;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._Label2 = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000071 RID: 113 RVA: 0x0000B7C8 File Offset: 0x000099C8
		// (set) Token: 0x06000072 RID: 114 RVA: 0x0000B7E0 File Offset: 0x000099E0
		internal virtual TabPage TabPage2
		{
			get
			{
				return this._TabPage2;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._TabPage2 = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000073 RID: 115 RVA: 0x0000B7EC File Offset: 0x000099EC
		// (set) Token: 0x06000074 RID: 116 RVA: 0x0000B804 File Offset: 0x00009A04
		private virtual ListView lvNoPassFF
		{
			get
			{
				return this._lvNoPassFF;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._lvNoPassFF = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000075 RID: 117 RVA: 0x0000B810 File Offset: 0x00009A10
		// (set) Token: 0x06000076 RID: 118 RVA: 0x0000B828 File Offset: 0x00009A28
		private virtual ColumnHeader columnHeader1
		{
			get
			{
				return this._columnHeader1;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._columnHeader1 = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000077 RID: 119 RVA: 0x0000B834 File Offset: 0x00009A34
		// (set) Token: 0x06000078 RID: 120 RVA: 0x0000B84C File Offset: 0x00009A4C
		private virtual ColumnHeader columnHeader2
		{
			get
			{
				return this._columnHeader2;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._columnHeader2 = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000079 RID: 121 RVA: 0x0000B858 File Offset: 0x00009A58
		// (set) Token: 0x0600007A RID: 122 RVA: 0x0000B870 File Offset: 0x00009A70
		internal virtual ColumnHeader ColumnHeader19
		{
			get
			{
				return this._ColumnHeader19;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._ColumnHeader19 = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600007B RID: 123 RVA: 0x0000B87C File Offset: 0x00009A7C
		// (set) Token: 0x0600007C RID: 124 RVA: 0x0000B894 File Offset: 0x00009A94
		internal virtual TabPage TabPage3
		{
			get
			{
				return this._TabPage3;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._TabPage3 = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600007D RID: 125 RVA: 0x0000B8A0 File Offset: 0x00009AA0
		// (set) Token: 0x0600007E RID: 126 RVA: 0x0000B8B8 File Offset: 0x00009AB8
		private virtual ListView lvFireFox
		{
			get
			{
				return this._lvFireFox;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._lvFireFox = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600007F RID: 127 RVA: 0x0000B8C4 File Offset: 0x00009AC4
		// (set) Token: 0x06000080 RID: 128 RVA: 0x0000B8DC File Offset: 0x00009ADC
		private virtual ColumnHeader columnHeader3
		{
			get
			{
				return this._columnHeader3;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._columnHeader3 = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000081 RID: 129 RVA: 0x0000B8E8 File Offset: 0x00009AE8
		// (set) Token: 0x06000082 RID: 130 RVA: 0x0000B900 File Offset: 0x00009B00
		private virtual ColumnHeader columnHeader4
		{
			get
			{
				return this._columnHeader4;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._columnHeader4 = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000083 RID: 131 RVA: 0x0000B90C File Offset: 0x00009B0C
		// (set) Token: 0x06000084 RID: 132 RVA: 0x0000B924 File Offset: 0x00009B24
		private virtual ColumnHeader ColumnHeader5
		{
			get
			{
				return this._ColumnHeader5;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._ColumnHeader5 = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000085 RID: 133 RVA: 0x0000B930 File Offset: 0x00009B30
		// (set) Token: 0x06000086 RID: 134 RVA: 0x0000B948 File Offset: 0x00009B48
		private virtual ColumnHeader ColumnHeader13
		{
			get
			{
				return this._ColumnHeader13;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._ColumnHeader13 = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000087 RID: 135 RVA: 0x0000B954 File Offset: 0x00009B54
		// (set) Token: 0x06000088 RID: 136 RVA: 0x0000B96C File Offset: 0x00009B6C
		private virtual ColumnHeader ColumnHeader14
		{
			get
			{
				return this._ColumnHeader14;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._ColumnHeader14 = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000089 RID: 137 RVA: 0x0000B978 File Offset: 0x00009B78
		// (set) Token: 0x0600008A RID: 138 RVA: 0x0000B990 File Offset: 0x00009B90
		private virtual ColumnHeader ColumnHeader15
		{
			get
			{
				return this._ColumnHeader15;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._ColumnHeader15 = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600008B RID: 139 RVA: 0x0000B99C File Offset: 0x00009B9C
		// (set) Token: 0x0600008C RID: 140 RVA: 0x0000B9B4 File Offset: 0x00009BB4
		private virtual ColumnHeader ColumnHeader16
		{
			get
			{
				return this._ColumnHeader16;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._ColumnHeader16 = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600008D RID: 141 RVA: 0x0000B9C0 File Offset: 0x00009BC0
		// (set) Token: 0x0600008E RID: 142 RVA: 0x0000B9D8 File Offset: 0x00009BD8
		internal virtual ColumnHeader ColumnHeader17
		{
			get
			{
				return this._ColumnHeader17;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._ColumnHeader17 = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600008F RID: 143 RVA: 0x0000B9E4 File Offset: 0x00009BE4
		// (set) Token: 0x06000090 RID: 144 RVA: 0x0000B9FC File Offset: 0x00009BFC
		internal virtual TabPage TabPage5
		{
			get
			{
				return this._TabPage5;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._TabPage5 = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000091 RID: 145 RVA: 0x0000BA08 File Offset: 0x00009C08
		// (set) Token: 0x06000092 RID: 146 RVA: 0x0000BA20 File Offset: 0x00009C20
		private virtual ListView lvChrome
		{
			get
			{
				return this._lvChrome;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._lvChrome = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000093 RID: 147 RVA: 0x0000BA2C File Offset: 0x00009C2C
		// (set) Token: 0x06000094 RID: 148 RVA: 0x0000BA44 File Offset: 0x00009C44
		private virtual ColumnHeader ColumnHeader28
		{
			get
			{
				return this._ColumnHeader28;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._ColumnHeader28 = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000095 RID: 149 RVA: 0x0000BA50 File Offset: 0x00009C50
		// (set) Token: 0x06000096 RID: 150 RVA: 0x0000BA68 File Offset: 0x00009C68
		private virtual ColumnHeader ColumnHeader29
		{
			get
			{
				return this._ColumnHeader29;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._ColumnHeader29 = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000097 RID: 151 RVA: 0x0000BA74 File Offset: 0x00009C74
		// (set) Token: 0x06000098 RID: 152 RVA: 0x0000BA8C File Offset: 0x00009C8C
		private virtual ColumnHeader ColumnHeader30
		{
			get
			{
				return this._ColumnHeader30;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._ColumnHeader30 = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000099 RID: 153 RVA: 0x0000BA98 File Offset: 0x00009C98
		// (set) Token: 0x0600009A RID: 154 RVA: 0x0000BAB0 File Offset: 0x00009CB0
		private virtual ColumnHeader ColumnHeader31
		{
			get
			{
				return this._ColumnHeader31;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._ColumnHeader31 = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600009B RID: 155 RVA: 0x0000BABC File Offset: 0x00009CBC
		// (set) Token: 0x0600009C RID: 156 RVA: 0x0000BAD4 File Offset: 0x00009CD4
		private virtual ColumnHeader ColumnHeader32
		{
			get
			{
				return this._ColumnHeader32;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._ColumnHeader32 = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600009D RID: 157 RVA: 0x0000BAE0 File Offset: 0x00009CE0
		// (set) Token: 0x0600009E RID: 158 RVA: 0x0000BAF8 File Offset: 0x00009CF8
		private virtual ColumnHeader ColumnHeader33
		{
			get
			{
				return this._ColumnHeader33;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._ColumnHeader33 = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600009F RID: 159 RVA: 0x0000BB04 File Offset: 0x00009D04
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x0000BB1C File Offset: 0x00009D1C
		internal virtual ColumnHeader ColumnHeader27
		{
			get
			{
				return this._ColumnHeader27;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._ColumnHeader27 = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x0000BB28 File Offset: 0x00009D28
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x0000BB40 File Offset: 0x00009D40
		internal virtual ColumnHeader ColumnHeader34
		{
			get
			{
				return this._ColumnHeader34;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._ColumnHeader34 = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x0000BB4C File Offset: 0x00009D4C
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x0000BB64 File Offset: 0x00009D64
		internal virtual ColumnHeader ColumnHeader35
		{
			get
			{
				return this._ColumnHeader35;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._ColumnHeader35 = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x0000BB70 File Offset: 0x00009D70
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x0000BB88 File Offset: 0x00009D88
		internal virtual ColumnHeader ColumnHeader36
		{
			get
			{
				return this._ColumnHeader36;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._ColumnHeader36 = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x0000BB94 File Offset: 0x00009D94
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x0000BBAC File Offset: 0x00009DAC
		internal virtual TabPage TabPage6
		{
			get
			{
				return this._TabPage6;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._TabPage6 = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x0000BBB8 File Offset: 0x00009DB8
		// (set) Token: 0x060000AA RID: 170 RVA: 0x0000BBD0 File Offset: 0x00009DD0
		private virtual ListView lvOpera
		{
			get
			{
				return this._lvOpera;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._lvOpera = value;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000AB RID: 171 RVA: 0x0000BBDC File Offset: 0x00009DDC
		// (set) Token: 0x060000AC RID: 172 RVA: 0x0000BBF4 File Offset: 0x00009DF4
		private virtual ColumnHeader ColumnHeader38
		{
			get
			{
				return this._ColumnHeader38;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._ColumnHeader38 = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000AD RID: 173 RVA: 0x0000BC00 File Offset: 0x00009E00
		// (set) Token: 0x060000AE RID: 174 RVA: 0x0000BC18 File Offset: 0x00009E18
		private virtual ColumnHeader ColumnHeader39
		{
			get
			{
				return this._ColumnHeader39;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._ColumnHeader39 = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000AF RID: 175 RVA: 0x0000BC24 File Offset: 0x00009E24
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x0000BC3C File Offset: 0x00009E3C
		private virtual ColumnHeader ColumnHeader40
		{
			get
			{
				return this._ColumnHeader40;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._ColumnHeader40 = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x0000BC48 File Offset: 0x00009E48
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x0000BC60 File Offset: 0x00009E60
		private virtual ColumnHeader ColumnHeader41
		{
			get
			{
				return this._ColumnHeader41;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._ColumnHeader41 = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x0000BC6C File Offset: 0x00009E6C
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x0000BC84 File Offset: 0x00009E84
		private virtual ColumnHeader ColumnHeader42
		{
			get
			{
				return this._ColumnHeader42;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._ColumnHeader42 = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x0000BC90 File Offset: 0x00009E90
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x0000BCA8 File Offset: 0x00009EA8
		internal virtual ColumnHeader ColumnHeader47
		{
			get
			{
				return this._ColumnHeader47;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._ColumnHeader47 = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x0000BCB4 File Offset: 0x00009EB4
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x0000BCCC File Offset: 0x00009ECC
		internal virtual TabPage TabPage7
		{
			get
			{
				return this._TabPage7;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._TabPage7 = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x0000BCD8 File Offset: 0x00009ED8
		// (set) Token: 0x060000BA RID: 186 RVA: 0x0000BCF0 File Offset: 0x00009EF0
		private virtual ListView lvSafari
		{
			get
			{
				return this._lvSafari;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._lvSafari = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000BB RID: 187 RVA: 0x0000BCFC File Offset: 0x00009EFC
		// (set) Token: 0x060000BC RID: 188 RVA: 0x0000BD14 File Offset: 0x00009F14
		private virtual ColumnHeader ColumnHeader37
		{
			get
			{
				return this._ColumnHeader37;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._ColumnHeader37 = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000BD RID: 189 RVA: 0x0000BD20 File Offset: 0x00009F20
		// (set) Token: 0x060000BE RID: 190 RVA: 0x0000BD38 File Offset: 0x00009F38
		private virtual ColumnHeader ColumnHeader43
		{
			get
			{
				return this._ColumnHeader43;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._ColumnHeader43 = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000BF RID: 191 RVA: 0x0000BD44 File Offset: 0x00009F44
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x0000BD5C File Offset: 0x00009F5C
		private virtual ColumnHeader ColumnHeader44
		{
			get
			{
				return this._ColumnHeader44;
			}
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				this._ColumnHeader44 = value;
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000BD68 File Offset: 0x00009F68
		private void AddSignons(MozillaProfile profile, MozillaSignons signons, ListView lvNoPass, ListView lvLog)
		{
			int i = 0;
			while (i < signons.NoPasswordSiteCount)
			{
				ListViewItem listViewItem = lvNoPass.Items.Add(profile.Name);
				listViewItem.SubItems.Add(signons.GetNoPasswordSite(i));
				listViewItem.SubItems.Add(signons.Version.ToString());
				Math.Max(Interlocked.Increment(ref i), checked(i - 1));
			}
			try
			{
				foreach (object obj in signons)
				{
					MozillaPassword mozillaPassword = (MozillaPassword)obj;
					ListViewItem listViewItem2 = lvLog.Items.Add(profile.Name);
					listViewItem2.SubItems.Add(mozillaPassword.SiteName);
					listViewItem2.SubItems.Add(mozillaPassword.Username);
					listViewItem2.SubItems.Add(mozillaPassword.Password);
					listViewItem2.SubItems.Add(mozillaPassword.UsernameField);
					listViewItem2.SubItems.Add(mozillaPassword.PasswordField);
					listViewItem2.SubItems.Add(mozillaPassword.Domain);
					listViewItem2.SubItems.Add(signons.Version.ToString());
				}
			}
			finally
			{
				IEnumerator enumerator;
				if (enumerator is IDisposable)
				{
					(enumerator as IDisposable).Dispose();
				}
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000BEBC File Offset: 0x0000A0BC
		public void RefreshSafariList()
		{
			this.lvSafari.Items.Clear();
			try
			{
				foreach (SafariPassword safariPassword in SafariPasswords.EnumeratePasswords())
				{
					ListViewItem listViewItem = this.lvSafari.Items.Add(safariPassword.Server);
					listViewItem.SubItems.Add(safariPassword.UserNameValue);
					listViewItem.SubItems.Add(safariPassword.PasswordValue);
				}
			}
			finally
			{
				IEnumerator<SafariPassword> enumerator;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000BF4C File Offset: 0x0000A14C
		public void RefreshOperaList()
		{
			this.lvOpera.Items.Clear();
			try
			{
				foreach (OperaPassword operaPassword in OperaPasswords.EnumeratePasswords())
				{
					ListViewItem listViewItem = this.lvOpera.Items.Add(string.Format("{0} ({1})", operaPassword.URL, operaPassword.ActionURL));
					listViewItem.SubItems.Add(operaPassword.UsernameValue);
					listViewItem.SubItems.Add(operaPassword.PasswordValue);
					listViewItem.SubItems.Add(operaPassword.UserElementName);
					listViewItem.SubItems.Add(operaPassword.PasswordElementName);
					listViewItem.SubItems.Add(operaPassword.CreationDate.ToString());
				}
			}
			finally
			{
				IEnumerator<OperaPassword> enumerator;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000C030 File Offset: 0x0000A230
		public void RefreshChromeList()
		{
			this.lvChrome.Items.Clear();
			try
			{
				foreach (ChromePassword chromePassword in ChromePasswords.EnumeratePasswords())
				{
					ListViewItem listViewItem = this.lvChrome.Items.Add(chromePassword.Blacklisted ? "Yes" : "");
					listViewItem.SubItems.Add(string.Format("{0} ({1})", chromePassword.OriginURL, chromePassword.ActionURL));
					listViewItem.SubItems.Add(chromePassword.UsernameValue);
					listViewItem.SubItems.Add(chromePassword.PasswordValue);
					listViewItem.SubItems.Add(chromePassword.UsernameElement);
					listViewItem.SubItems.Add(chromePassword.PasswordElement);
					listViewItem.SubItems.Add(chromePassword.SignonRealm);
					listViewItem.SubItems.Add(chromePassword.SubmitElement);
					listViewItem.SubItems.Add(chromePassword.SSLValid ? "Yes" : "No");
					listViewItem.SubItems.Add(chromePassword.CreationDate.ToString());
				}
			}
			finally
			{
				IEnumerator<ChromePassword> enumerator;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000C184 File Offset: 0x0000A384
		public void RefreshMozillaLists()
		{
			this.lvFireFox.Items.Clear();
			this.lvNoPassFF.Items.Clear();
			MozillaProfiles mozillaProfiles = new MozillaProfiles(MozillaSoft.FireFox);
			try
			{
				foreach (MozillaProfile mozillaProfile in mozillaProfiles)
				{
					this.AddSignons(mozillaProfile, mozillaProfile.GetSignons(MozillaProfile.MozillaVersion.Sqlite), this.lvNoPassFF, this.lvFireFox);
					this.AddSignons(mozillaProfile, mozillaProfile.GetSignons(MozillaProfile.MozillaVersion.Txt2e), this.lvNoPassFF, this.lvFireFox);
					this.AddSignons(mozillaProfile, mozillaProfile.GetSignons(MozillaProfile.MozillaVersion.Txt2d), this.lvNoPassFF, this.lvFireFox);
					this.AddSignons(mozillaProfile, mozillaProfile.GetSignons(MozillaProfile.MozillaVersion.Txt2c), this.lvNoPassFF, this.lvFireFox);
				}
			}
			finally
			{
				IEnumerator<MozillaProfile> enumerator;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000C254 File Offset: 0x0000A454
		private void cmdAllIEForms_Click(object sender, EventArgs e)
		{
			int num;
			int num4;
			object obj2;
			try
			{
				IL_00:
				ProjectData.ClearProjectError();
				num = 1;
				IL_07:
				int num2 = 2;
				IEnumerator enumerator = this.lvIE.Items.GetEnumerator();
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					ListViewItem listViewItem = (ListViewItem)obj;
					IL_29:
					num2 = 3;
					if (Operators.CompareString(listViewItem.Text, "Formulaire", false) == 0)
					{
						IL_40:
						num2 = 4;
						this.PIE.Delete(listViewItem.SubItems[1].Text);
					}
					IL_5F:
					num2 = 6;
				}
				if (enumerator is IDisposable)
				{
					(enumerator as IDisposable).Dispose();
				}
				IL_7D:
				num2 = 7;
				this.RefreshLists();
				IL_86:
				goto IL_F9;
				IL_8B:
				int num3 = num4 + 1;
				num4 = 0;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num3);
				IL_B9:
				goto IL_EE;
				IL_BB:
				num4 = num2;
				@switch(ICSharpCode.Decompiler.ILAst.ILLabel[], num);
				IL_CC:;
			}
			catch when (endfilter(obj2 is Exception & num != 0 & num4 == 0))
			{
				Exception ex = (Exception)obj3;
				goto IL_BB;
			}
			IL_EE:
			throw ProjectData.CreateProjectError(-2146828237);
			IL_F9:
			if (num4 != 0)
			{
				ProjectData.ClearProjectError();
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000C374 File Offset: 0x0000A574
		private void cmdAllIEPass_Click(object sender, EventArgs e)
		{
			try
			{
				foreach (object obj in this.lvIE.Items)
				{
					ListViewItem listViewItem = (ListViewItem)obj;
					if (Operators.CompareString(listViewItem.Text, "Utilisateur/Mot de passe", false) == 0)
					{
						this.PIE.Delete(listViewItem.SubItems[1].Text);
					}
				}
			}
			finally
			{
				IEnumerator enumerator;
				if (enumerator is IDisposable)
				{
					(enumerator as IDisposable).Dispose();
				}
			}
			this.RefreshLists();
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000C404 File Offset: 0x0000A604
		private void cmdDelAllAuth_Click(object sender, EventArgs e)
		{
			try
			{
				foreach (object obj in this.lvHttpFtp.Items)
				{
					ListViewItem listViewItem = (ListViewItem)obj;
					this.PHTTPFTP.Delete(Conversions.ToString(listViewItem.Tag));
				}
			}
			finally
			{
				IEnumerator enumerator;
				if (enumerator is IDisposable)
				{
					(enumerator as IDisposable).Dispose();
				}
			}
			this.RefreshLists();
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000C47C File Offset: 0x0000A67C
		private void cmdDelAuthLine_Click(object sender, EventArgs e)
		{
			ListViewItem focusedItem = this.lvHttpFtp.FocusedItem;
			if (focusedItem == null)
			{
				Interaction.MsgBox("Pas d'entrée sélectionnée !", MsgBoxStyle.Exclamation, null);
			}
			else
			{
				this.PHTTPFTP.Delete(Conversions.ToString(focusedItem.Tag));
				this.RefreshLists();
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000C4C4 File Offset: 0x0000A6C4
		private void cmdIE7Del_Click(object sender, EventArgs e)
		{
			ListViewItem focusedItem = this.lvIE7.FocusedItem;
			if (focusedItem == null)
			{
				Interaction.MsgBox("Pas d'entrée sélectionnée !", MsgBoxStyle.Exclamation, null);
			}
			else
			{
				this.PIESEVEN.Delete(Conversions.ToString(focusedItem.Tag));
				this.RefreshLists();
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000C50C File Offset: 0x0000A70C
		private void cmdIEDel_Click(object sender, EventArgs e)
		{
			ListViewItem focusedItem = this.lvIE.FocusedItem;
			if (focusedItem == null)
			{
				Interaction.MsgBox("Pas d'entrée sélectionnée !", MsgBoxStyle.Exclamation, null);
			}
			else
			{
				this.PIE.Delete(focusedItem.SubItems[1].Text);
				this.RefreshLists();
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000C55C File Offset: 0x0000A75C
		private void cmdIELine_Click(object sender, EventArgs e)
		{
			ListViewItem focusedItem = this.lvIE.FocusedItem;
			if (focusedItem == null)
			{
				Interaction.MsgBox("Pas d'entrée sélectionnée !", MsgBoxStyle.Exclamation, null);
			}
			else
			{
				this.PIE.DeletePart(focusedItem.SubItems[1].Text, focusedItem.SubItems[2].Text);
				this.RefreshLists();
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000C5BC File Offset: 0x0000A7BC
		private void cmdRefresh_Click(object sender, EventArgs e)
		{
			this.RefreshLists();
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000C5C4 File Offset: 0x0000A7C4
		private void RefreshLists()
		{
			ListViewItem listViewItem = null;
			this.lvIE.Items.Clear();
			this.lvHttpFtp.Items.Clear();
			this.lvIE7.Items.Clear();
			try
			{
				foreach (CIEPassword ciepassword in this.PIE)
				{
					if (ciepassword.PasswdType == 0)
					{
						listViewItem = this.lvIE.Items.Add("Formulaire");
					}
					else
					{
						listViewItem = this.lvIE.Items.Add("Utilisateur/Mot de passe");
					}
					listViewItem.SubItems.Add(ciepassword.ResourceName);
					listViewItem.SubItems.Add(ciepassword.UserName);
					listViewItem.SubItems.Add(ciepassword.Password);
					listViewItem.SubItems.Add(Conversions.ToString(ciepassword.AddDate));
				}
			}
			finally
			{
				IEnumerator<CIEPassword> enumerator;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}
			try
			{
				foreach (CHttpFtpAuth chttpFtpAuth in this.PHTTPFTP)
				{
					if (chttpFtpAuth.PasswdType == 0)
					{
						listViewItem = this.lvHttpFtp.Items.Add("FTP");
					}
					else
					{
						listViewItem = this.lvHttpFtp.Items.Add("HTTPAuth");
					}
					listViewItem.SubItems.Add(chttpFtpAuth.URL);
					listViewItem.SubItems.Add(chttpFtpAuth.UserName);
					listViewItem.SubItems.Add(chttpFtpAuth.Password);
					listViewItem.SubItems.Add(chttpFtpAuth.Description);
					listViewItem.Tag = chttpFtpAuth.ResourceName;
				}
			}
			finally
			{
				IEnumerator<CHttpFtpAuth> enumerator2;
				if (enumerator2 != null)
				{
					enumerator2.Dispose();
				}
			}
			try
			{
				foreach (CIE7Password cie7Password in this.PIESEVEN)
				{
					switch (cie7Password.PasswdType)
					{
					case 0:
						listViewItem = this.lvIE7.Items.Add("Formulaire");
						break;
					case 1:
						listViewItem = this.lvIE7.Items.Add("Mot de passe");
						break;
					case 2:
						listViewItem = this.lvIE7.Items.Add("HTTPAuth");
						break;
					case 3:
						listViewItem = this.lvIE7.Items.Add("FTP Account");
						break;
					}
					listViewItem.Tag = cie7Password.ResourceName;
					listViewItem.SubItems.Add(cie7Password.URL);
					listViewItem.SubItems.Add(cie7Password.UserName);
					listViewItem.SubItems.Add(cie7Password.Password);
					listViewItem.SubItems.Add(Conversions.ToString(cie7Password.AddDate));
					listViewItem.SubItems.Add(cie7Password.Description);
				}
			}
			finally
			{
				IEnumerator<CIE7Password> enumerator3;
				if (enumerator3 != null)
				{
					enumerator3.Dispose();
				}
			}
			this.RefreshMozillaLists();
			this.RefreshChromeList();
			this.RefreshOperaList();
			this.RefreshSafariList();
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000C910 File Offset: 0x0000AB10
		private void frmMain_Load(object sender, EventArgs e)
		{
			try
			{
				this.RefreshLists();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000C960 File Offset: 0x0000AB60
		private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.PST = null;
		}

		// Token: 0x04000018 RID: 24
		[AccessedThroughProperty("cmdRefresh")]
		private Button _cmdRefresh;

		// Token: 0x04000019 RID: 25
		[AccessedThroughProperty("_lvIE_ColumnHeader_1")]
		private ColumnHeader __lvIE_ColumnHeader_1;

		// Token: 0x0400001A RID: 26
		[AccessedThroughProperty("_lvIE_ColumnHeader_2")]
		private ColumnHeader __lvIE_ColumnHeader_2;

		// Token: 0x0400001B RID: 27
		[AccessedThroughProperty("_lvIE_ColumnHeader_3")]
		private ColumnHeader __lvIE_ColumnHeader_3;

		// Token: 0x0400001C RID: 28
		[AccessedThroughProperty("_lvIE_ColumnHeader_4")]
		private ColumnHeader __lvIE_ColumnHeader_4;

		// Token: 0x0400001D RID: 29
		[AccessedThroughProperty("_lvIE_ColumnHeader_5")]
		private ColumnHeader __lvIE_ColumnHeader_5;

		// Token: 0x0400001E RID: 30
		[AccessedThroughProperty("lvIE")]
		private ListView _lvIE;

		// Token: 0x0400001F RID: 31
		[AccessedThroughProperty("cmdAllIEPass")]
		private Button _cmdAllIEPass;

		// Token: 0x04000020 RID: 32
		[AccessedThroughProperty("cmdAllIEForms")]
		private Button _cmdAllIEForms;

		// Token: 0x04000021 RID: 33
		[AccessedThroughProperty("cmdIEDel")]
		private Button _cmdIEDel;

		// Token: 0x04000022 RID: 34
		[AccessedThroughProperty("cmdIELine")]
		private Button _cmdIELine;

		// Token: 0x04000023 RID: 35
		[AccessedThroughProperty("_SSTab1_TabPage2")]
		private TabPage __SSTab1_TabPage2;

		// Token: 0x04000024 RID: 36
		[AccessedThroughProperty("_lvHttpFtp_ColumnHeader_1")]
		private ColumnHeader __lvHttpFtp_ColumnHeader_1;

		// Token: 0x04000025 RID: 37
		[AccessedThroughProperty("_lvHttpFtp_ColumnHeader_2")]
		private ColumnHeader __lvHttpFtp_ColumnHeader_2;

		// Token: 0x04000026 RID: 38
		[AccessedThroughProperty("_lvHttpFtp_ColumnHeader_3")]
		private ColumnHeader __lvHttpFtp_ColumnHeader_3;

		// Token: 0x04000027 RID: 39
		[AccessedThroughProperty("_lvHttpFtp_ColumnHeader_4")]
		private ColumnHeader __lvHttpFtp_ColumnHeader_4;

		// Token: 0x04000028 RID: 40
		[AccessedThroughProperty("_lvHttpFtp_ColumnHeader_5")]
		private ColumnHeader __lvHttpFtp_ColumnHeader_5;

		// Token: 0x04000029 RID: 41
		[AccessedThroughProperty("lvHttpFtp")]
		private ListView _lvHttpFtp;

		// Token: 0x0400002A RID: 42
		[AccessedThroughProperty("cmdDelAuthLine")]
		private Button _cmdDelAuthLine;

		// Token: 0x0400002B RID: 43
		[AccessedThroughProperty("cmdDelAllAuth")]
		private Button _cmdDelAllAuth;

		// Token: 0x0400002C RID: 44
		[AccessedThroughProperty("_SSTab1_TabPage5")]
		private TabPage __SSTab1_TabPage5;

		// Token: 0x0400002D RID: 45
		[AccessedThroughProperty("_lvIE7_ColumnHeader_1")]
		private ColumnHeader __lvIE7_ColumnHeader_1;

		// Token: 0x0400002E RID: 46
		[AccessedThroughProperty("_lvIE7_ColumnHeader_2")]
		private ColumnHeader __lvIE7_ColumnHeader_2;

		// Token: 0x0400002F RID: 47
		[AccessedThroughProperty("_lvIE7_ColumnHeader_3")]
		private ColumnHeader __lvIE7_ColumnHeader_3;

		// Token: 0x04000030 RID: 48
		[AccessedThroughProperty("_lvIE7_ColumnHeader_4")]
		private ColumnHeader __lvIE7_ColumnHeader_4;

		// Token: 0x04000031 RID: 49
		[AccessedThroughProperty("_lvIE7_ColumnHeader_5")]
		private ColumnHeader __lvIE7_ColumnHeader_5;

		// Token: 0x04000032 RID: 50
		[AccessedThroughProperty("_lvIE7_ColumnHeader_6")]
		private ColumnHeader __lvIE7_ColumnHeader_6;

		// Token: 0x04000033 RID: 51
		[AccessedThroughProperty("lvIE7")]
		private ListView _lvIE7;

		// Token: 0x04000034 RID: 52
		[AccessedThroughProperty("cmdIE7Del")]
		private Button _cmdIE7Del;

		// Token: 0x04000035 RID: 53
		[AccessedThroughProperty("_SSTab1_TabPage6")]
		private TabPage __SSTab1_TabPage6;

		// Token: 0x04000036 RID: 54
		[AccessedThroughProperty("SSTab1")]
		private TabControl _SSTab1;

		// Token: 0x04000037 RID: 55
		[AccessedThroughProperty("Label2")]
		private Label _Label2;

		// Token: 0x04000038 RID: 56
		[AccessedThroughProperty("TabPage2")]
		private TabPage _TabPage2;

		// Token: 0x04000039 RID: 57
		[AccessedThroughProperty("lvNoPassFF")]
		private ListView _lvNoPassFF;

		// Token: 0x0400003A RID: 58
		[AccessedThroughProperty("columnHeader1")]
		private ColumnHeader _columnHeader1;

		// Token: 0x0400003B RID: 59
		[AccessedThroughProperty("columnHeader2")]
		private ColumnHeader _columnHeader2;

		// Token: 0x0400003C RID: 60
		[AccessedThroughProperty("ColumnHeader19")]
		private ColumnHeader _ColumnHeader19;

		// Token: 0x0400003D RID: 61
		[AccessedThroughProperty("TabPage3")]
		private TabPage _TabPage3;

		// Token: 0x0400003E RID: 62
		[AccessedThroughProperty("lvFireFox")]
		private ListView _lvFireFox;

		// Token: 0x0400003F RID: 63
		[AccessedThroughProperty("columnHeader3")]
		private ColumnHeader _columnHeader3;

		// Token: 0x04000040 RID: 64
		[AccessedThroughProperty("columnHeader4")]
		private ColumnHeader _columnHeader4;

		// Token: 0x04000041 RID: 65
		[AccessedThroughProperty("ColumnHeader5")]
		private ColumnHeader _ColumnHeader5;

		// Token: 0x04000042 RID: 66
		[AccessedThroughProperty("ColumnHeader13")]
		private ColumnHeader _ColumnHeader13;

		// Token: 0x04000043 RID: 67
		[AccessedThroughProperty("ColumnHeader14")]
		private ColumnHeader _ColumnHeader14;

		// Token: 0x04000044 RID: 68
		[AccessedThroughProperty("ColumnHeader15")]
		private ColumnHeader _ColumnHeader15;

		// Token: 0x04000045 RID: 69
		[AccessedThroughProperty("ColumnHeader16")]
		private ColumnHeader _ColumnHeader16;

		// Token: 0x04000046 RID: 70
		[AccessedThroughProperty("ColumnHeader17")]
		private ColumnHeader _ColumnHeader17;

		// Token: 0x04000047 RID: 71
		[AccessedThroughProperty("TabPage5")]
		private TabPage _TabPage5;

		// Token: 0x04000048 RID: 72
		[AccessedThroughProperty("lvChrome")]
		private ListView _lvChrome;

		// Token: 0x04000049 RID: 73
		[AccessedThroughProperty("ColumnHeader28")]
		private ColumnHeader _ColumnHeader28;

		// Token: 0x0400004A RID: 74
		[AccessedThroughProperty("ColumnHeader29")]
		private ColumnHeader _ColumnHeader29;

		// Token: 0x0400004B RID: 75
		[AccessedThroughProperty("ColumnHeader30")]
		private ColumnHeader _ColumnHeader30;

		// Token: 0x0400004C RID: 76
		[AccessedThroughProperty("ColumnHeader31")]
		private ColumnHeader _ColumnHeader31;

		// Token: 0x0400004D RID: 77
		[AccessedThroughProperty("ColumnHeader32")]
		private ColumnHeader _ColumnHeader32;

		// Token: 0x0400004E RID: 78
		[AccessedThroughProperty("ColumnHeader33")]
		private ColumnHeader _ColumnHeader33;

		// Token: 0x0400004F RID: 79
		[AccessedThroughProperty("ColumnHeader27")]
		private ColumnHeader _ColumnHeader27;

		// Token: 0x04000050 RID: 80
		[AccessedThroughProperty("ColumnHeader34")]
		private ColumnHeader _ColumnHeader34;

		// Token: 0x04000051 RID: 81
		[AccessedThroughProperty("ColumnHeader35")]
		private ColumnHeader _ColumnHeader35;

		// Token: 0x04000052 RID: 82
		[AccessedThroughProperty("ColumnHeader36")]
		private ColumnHeader _ColumnHeader36;

		// Token: 0x04000053 RID: 83
		[AccessedThroughProperty("TabPage6")]
		private TabPage _TabPage6;

		// Token: 0x04000054 RID: 84
		[AccessedThroughProperty("lvOpera")]
		private ListView _lvOpera;

		// Token: 0x04000055 RID: 85
		[AccessedThroughProperty("ColumnHeader38")]
		private ColumnHeader _ColumnHeader38;

		// Token: 0x04000056 RID: 86
		[AccessedThroughProperty("ColumnHeader39")]
		private ColumnHeader _ColumnHeader39;

		// Token: 0x04000057 RID: 87
		[AccessedThroughProperty("ColumnHeader40")]
		private ColumnHeader _ColumnHeader40;

		// Token: 0x04000058 RID: 88
		[AccessedThroughProperty("ColumnHeader41")]
		private ColumnHeader _ColumnHeader41;

		// Token: 0x04000059 RID: 89
		[AccessedThroughProperty("ColumnHeader42")]
		private ColumnHeader _ColumnHeader42;

		// Token: 0x0400005A RID: 90
		[AccessedThroughProperty("ColumnHeader47")]
		private ColumnHeader _ColumnHeader47;

		// Token: 0x0400005B RID: 91
		[AccessedThroughProperty("TabPage7")]
		private TabPage _TabPage7;

		// Token: 0x0400005C RID: 92
		[AccessedThroughProperty("lvSafari")]
		private ListView _lvSafari;

		// Token: 0x0400005D RID: 93
		[AccessedThroughProperty("ColumnHeader37")]
		private ColumnHeader _ColumnHeader37;

		// Token: 0x0400005E RID: 94
		[AccessedThroughProperty("ColumnHeader43")]
		private ColumnHeader _ColumnHeader43;

		// Token: 0x0400005F RID: 95
		[AccessedThroughProperty("ColumnHeader44")]
		private ColumnHeader _ColumnHeader44;

		// Token: 0x04000060 RID: 96
		private CProtectStore PST;

		// Token: 0x04000061 RID: 97
		private CIEPasswords PIE;

		// Token: 0x04000062 RID: 98
		private CHttpFtpAuths PHTTPFTP;

		// Token: 0x04000063 RID: 99
		private CIE7Passwords PIESEVEN;
	}
}
