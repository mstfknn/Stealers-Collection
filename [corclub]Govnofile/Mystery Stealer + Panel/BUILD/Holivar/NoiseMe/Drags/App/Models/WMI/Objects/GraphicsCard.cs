using System;

namespace NoiseMe.Drags.App.Models.WMI.Objects
{
	// Token: 0x02000089 RID: 137
	public class GraphicsCard
	{
		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x00004E13 File Offset: 0x00003013
		public uint? MemoryCapacity { get; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x00004E1B File Offset: 0x0000301B
		public string Caption { get; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x00004E23 File Offset: 0x00003023
		public string Description { get; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x00004E2B File Offset: 0x0000302B
		public string Name { get; }

		// Token: 0x060004B9 RID: 1209 RVA: 0x00004E33 File Offset: 0x00003033
		public GraphicsCard(uint? memory, string caption, string description, string name)
		{
			this.MemoryCapacity = memory;
			this.Caption = caption;
			this.Description = description;
			this.Name = name;
		}
	}
}
