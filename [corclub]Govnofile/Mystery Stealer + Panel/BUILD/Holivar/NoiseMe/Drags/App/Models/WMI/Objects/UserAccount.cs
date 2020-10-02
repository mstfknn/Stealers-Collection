using System;

namespace NoiseMe.Drags.App.Models.WMI.Objects
{
	// Token: 0x0200008F RID: 143
	public class UserAccount
	{
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x00004FC2 File Offset: 0x000031C2
		public string Name { get; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x00004FCA File Offset: 0x000031CA
		public string FullName { get; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00004FD2 File Offset: 0x000031D2
		public bool? IsDisabled { get; }

		// Token: 0x060004D7 RID: 1239 RVA: 0x00004FDA File Offset: 0x000031DA
		public UserAccount(string name, string fullName, bool? disabled)
		{
			this.Name = name;
			this.FullName = fullName;
			this.IsDisabled = disabled;
		}
	}
}
