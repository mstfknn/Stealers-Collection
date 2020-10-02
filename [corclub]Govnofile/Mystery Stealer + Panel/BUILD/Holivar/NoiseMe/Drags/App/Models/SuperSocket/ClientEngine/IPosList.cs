using System;
using System.Collections;
using System.Collections.Generic;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine
{
	// Token: 0x0200010D RID: 269
	public interface IPosList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000825 RID: 2085
		// (set) Token: 0x06000826 RID: 2086
		int Position { get; set; }
	}
}
