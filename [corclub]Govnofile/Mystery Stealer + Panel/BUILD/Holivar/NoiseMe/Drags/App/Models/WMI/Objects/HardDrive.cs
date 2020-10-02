using System;

namespace NoiseMe.Drags.App.Models.WMI.Objects
{
	// Token: 0x0200008A RID: 138
	public class HardDrive
	{
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x00004E58 File Offset: 0x00003058
		public string Caption { get; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x00004E60 File Offset: 0x00003060
		public string Model { get; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x00004E68 File Offset: 0x00003068
		public uint? Signature { get; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x00004E70 File Offset: 0x00003070
		public ulong? Capacity { get; }

		// Token: 0x060004BE RID: 1214 RVA: 0x00004E78 File Offset: 0x00003078
		public HardDrive(string caption, string model, uint? signature, ulong? capacity)
		{
			this.Caption = caption;
			this.Model = model;
			this.Signature = signature;
			this.Capacity = capacity;
		}
	}
}
