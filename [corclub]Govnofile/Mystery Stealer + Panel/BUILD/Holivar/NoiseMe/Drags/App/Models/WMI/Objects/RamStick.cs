using System;

namespace NoiseMe.Drags.App.Models.WMI.Objects
{
	// Token: 0x0200008D RID: 141
	public class RamStick
	{
		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x00004F47 File Offset: 0x00003147
		public ulong? Capacity { get; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x00004F4F File Offset: 0x0000314F
		public uint? ClockSpeed { get; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x00004F57 File Offset: 0x00003157
		public string Manufacturer { get; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x00004F5F File Offset: 0x0000315F
		public string SerialNumber { get; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x00004F67 File Offset: 0x00003167
		public uint? PositionInRow { get; }

		// Token: 0x060004D0 RID: 1232 RVA: 0x00004F6F File Offset: 0x0000316F
		public RamStick(ulong? capacity, uint? speed, string manufacturer, string serial, uint? pos)
		{
			this.Capacity = capacity;
			this.ClockSpeed = speed;
			this.Manufacturer = manufacturer;
			this.SerialNumber = serial;
			this.PositionInRow = pos;
		}
	}
}
