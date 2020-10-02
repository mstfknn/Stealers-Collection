using System;

namespace GrandSteal.Client.Data.Firefox
{
	// Token: 0x02000016 RID: 22
	public class LoginJson
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000079 RID: 121 RVA: 0x0000241B File Offset: 0x0000061B
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00002423 File Offset: 0x00000623
		public int id { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600007B RID: 123 RVA: 0x0000242C File Offset: 0x0000062C
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00002434 File Offset: 0x00000634
		public string hostname { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600007D RID: 125 RVA: 0x0000243D File Offset: 0x0000063D
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00002445 File Offset: 0x00000645
		public object httpRealm { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600007F RID: 127 RVA: 0x0000244E File Offset: 0x0000064E
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00002456 File Offset: 0x00000656
		public string formSubmitURL { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000081 RID: 129 RVA: 0x0000245F File Offset: 0x0000065F
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00002467 File Offset: 0x00000667
		public string usernameField { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00002470 File Offset: 0x00000670
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00002478 File Offset: 0x00000678
		public string passwordField { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00002481 File Offset: 0x00000681
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00002489 File Offset: 0x00000689
		public string encryptedUsername { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00002492 File Offset: 0x00000692
		// (set) Token: 0x06000088 RID: 136 RVA: 0x0000249A File Offset: 0x0000069A
		public string encryptedPassword { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000089 RID: 137 RVA: 0x000024A3 File Offset: 0x000006A3
		// (set) Token: 0x0600008A RID: 138 RVA: 0x000024AB File Offset: 0x000006AB
		public string guid { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000024B4 File Offset: 0x000006B4
		// (set) Token: 0x0600008C RID: 140 RVA: 0x000024BC File Offset: 0x000006BC
		public int encType { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000024C5 File Offset: 0x000006C5
		// (set) Token: 0x0600008E RID: 142 RVA: 0x000024CD File Offset: 0x000006CD
		public long timeCreated { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000024D6 File Offset: 0x000006D6
		// (set) Token: 0x06000090 RID: 144 RVA: 0x000024DE File Offset: 0x000006DE
		public long timeLastUsed { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000091 RID: 145 RVA: 0x000024E7 File Offset: 0x000006E7
		// (set) Token: 0x06000092 RID: 146 RVA: 0x000024EF File Offset: 0x000006EF
		public long timePasswordChanged { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000093 RID: 147 RVA: 0x000024F8 File Offset: 0x000006F8
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00002500 File Offset: 0x00000700
		public int timesUsed { get; set; }
	}
}
