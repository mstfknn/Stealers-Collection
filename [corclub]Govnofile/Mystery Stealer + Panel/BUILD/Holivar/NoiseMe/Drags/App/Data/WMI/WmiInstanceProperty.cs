using System;

namespace NoiseMe.Drags.App.Data.WMI
{
	// Token: 0x02000179 RID: 377
	public class WmiInstanceProperty
	{
		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x00009651 File Offset: 0x00007851
		public string Name { get; }

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x00009659 File Offset: 0x00007859
		public object Value { get; }

		// Token: 0x06000C33 RID: 3123 RVA: 0x00009661 File Offset: 0x00007861
		public WmiInstanceProperty(string name, object value)
		{
			this.Name = name;
			this.Value = value;
		}
	}
}
