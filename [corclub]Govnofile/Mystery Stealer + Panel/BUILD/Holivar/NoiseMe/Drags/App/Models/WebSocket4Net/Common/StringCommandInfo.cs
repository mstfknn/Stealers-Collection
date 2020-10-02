using System;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Common
{
	// Token: 0x020000C3 RID: 195
	public class StringCommandInfo : CommandInfo<string>
	{
		// Token: 0x06000680 RID: 1664 RVA: 0x00005E41 File Offset: 0x00004041
		public StringCommandInfo(string key, string data, string[] parameters) : base(key, data)
		{
			this.Parameters = parameters;
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x00005E52 File Offset: 0x00004052
		// (set) Token: 0x06000682 RID: 1666 RVA: 0x00005E5A File Offset: 0x0000405A
		public string[] Parameters { get; private set; }

		// Token: 0x06000683 RID: 1667 RVA: 0x00005E63 File Offset: 0x00004063
		public string GetFirstParam()
		{
			if (this.Parameters.Length != 0)
			{
				return this.Parameters[0];
			}
			return string.Empty;
		}

		// Token: 0x170001C5 RID: 453
		public string this[int index]
		{
			get
			{
				return this.Parameters[index];
			}
		}
	}
}
