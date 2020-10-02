using System;
using System.Collections;
using System.Collections.Generic;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine
{
	// Token: 0x0200010E RID: 270
	public class PosList<T> : List<T>, IPosList<T>, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x00006912 File Offset: 0x00004B12
		// (set) Token: 0x06000828 RID: 2088 RVA: 0x0000691A File Offset: 0x00004B1A
		public int Position { get; set; }
	}
}
