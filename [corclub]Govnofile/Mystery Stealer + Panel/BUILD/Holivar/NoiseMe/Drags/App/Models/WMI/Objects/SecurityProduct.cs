using System;

namespace NoiseMe.Drags.App.Models.WMI.Objects
{
	// Token: 0x0200008E RID: 142
	public class SecurityProduct
	{
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x00004F9C File Offset: 0x0000319C
		public string Name { get; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x00004FA4 File Offset: 0x000031A4
		public string PathToExe { get; }

		// Token: 0x060004D3 RID: 1235 RVA: 0x00004FAC File Offset: 0x000031AC
		public SecurityProduct(string name, string path)
		{
			this.Name = name;
			this.PathToExe = path;
		}
	}
}
