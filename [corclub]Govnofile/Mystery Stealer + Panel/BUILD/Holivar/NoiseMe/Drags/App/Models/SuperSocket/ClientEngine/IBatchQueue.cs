using System;
using System.Collections.Generic;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine
{
	// Token: 0x0200010B RID: 267
	public interface IBatchQueue<T>
	{
		// Token: 0x0600081D RID: 2077
		bool Enqueue(T item);

		// Token: 0x0600081E RID: 2078
		bool Enqueue(IList<T> items);

		// Token: 0x0600081F RID: 2079
		bool TryDequeue(IList<T> outputItems);

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000820 RID: 2080
		bool IsEmpty { get; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000821 RID: 2081
		int Count { get; }
	}
}
