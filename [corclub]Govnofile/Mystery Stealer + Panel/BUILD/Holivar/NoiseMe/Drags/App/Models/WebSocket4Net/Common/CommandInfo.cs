using System;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Common
{
	// Token: 0x020000BB RID: 187
	public abstract class CommandInfo<TCommandData> : ICommandInfo<TCommandData>, ICommandInfo
	{
		// Token: 0x0600066D RID: 1645 RVA: 0x00005DD3 File Offset: 0x00003FD3
		public CommandInfo(string key, TCommandData data)
		{
			this.Key = key;
			this.Data = data;
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x00005DE9 File Offset: 0x00003FE9
		// (set) Token: 0x0600066F RID: 1647 RVA: 0x00005DF1 File Offset: 0x00003FF1
		public TCommandData Data { get; private set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x00005DFA File Offset: 0x00003FFA
		// (set) Token: 0x06000671 RID: 1649 RVA: 0x00005E02 File Offset: 0x00004002
		public string Key { get; private set; }
	}
}
