using System;

namespace GrandSteal.Client.Data.Firefox
{
	// Token: 0x02000015 RID: 21
	public class RootLogin
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000023D7 File Offset: 0x000005D7
		// (set) Token: 0x06000071 RID: 113 RVA: 0x000023DF File Offset: 0x000005DF
		public int nextId { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000072 RID: 114 RVA: 0x000023E8 File Offset: 0x000005E8
		// (set) Token: 0x06000073 RID: 115 RVA: 0x000023F0 File Offset: 0x000005F0
		public LoginJson[] logins { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000023F9 File Offset: 0x000005F9
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00002401 File Offset: 0x00000601
		public object[] disabledHosts { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000076 RID: 118 RVA: 0x0000240A File Offset: 0x0000060A
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00002412 File Offset: 0x00000612
		public int version { get; set; }
	}
}
