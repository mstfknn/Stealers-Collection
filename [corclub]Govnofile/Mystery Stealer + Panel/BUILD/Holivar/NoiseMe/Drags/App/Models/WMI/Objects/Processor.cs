using System;

namespace NoiseMe.Drags.App.Models.WMI.Objects
{
	// Token: 0x0200008C RID: 140
	public class Processor
	{
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x00004EE2 File Offset: 0x000030E2
		public uint? ClockSpeed { get; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00004EEA File Offset: 0x000030EA
		public ushort? Voltage { get; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x00004EF2 File Offset: 0x000030F2
		public string Name { get; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00004EFA File Offset: 0x000030FA
		public string Manufacturer { get; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00004F02 File Offset: 0x00003102
		public uint? NumberOfCores { get; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x00004F0A File Offset: 0x0000310A
		public string Id { get; }

		// Token: 0x060004CA RID: 1226 RVA: 0x00004F12 File Offset: 0x00003112
		public Processor(uint? speed, ushort? volt, string name, string manufacturer, uint? cores, string id)
		{
			this.ClockSpeed = speed;
			this.Voltage = volt;
			this.Name = name;
			this.Manufacturer = manufacturer;
			this.NumberOfCores = cores;
			this.Id = id;
		}
	}
}
