using System;

namespace NoiseMe.Drags.App.Models.WMI.Objects
{
	// Token: 0x02000088 RID: 136
	public class BaseBoard
	{
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x00004DBE File Offset: 0x00002FBE
		public string Model { get; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x00004DC6 File Offset: 0x00002FC6
		public string Product { get; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x00004DCE File Offset: 0x00002FCE
		public string Name { get; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00004DD6 File Offset: 0x00002FD6
		public string Manufacturer { get; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00004DDE File Offset: 0x00002FDE
		public string SerialNumber { get; }

		// Token: 0x060004B4 RID: 1204 RVA: 0x00004DE6 File Offset: 0x00002FE6
		public BaseBoard(string model, string product, string name, string manufacturer, string serial)
		{
			this.Model = model;
			this.Product = product;
			this.Name = name;
			this.Manufacturer = manufacturer;
			this.SerialNumber = serial;
		}
	}
}
