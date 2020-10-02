using System;

namespace NoiseMe.Drags.App.Models.WMI.Objects
{
	// Token: 0x0200008B RID: 139
	public class NetworkAdapter
	{
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x00004E9D File Offset: 0x0000309D
		public string Caption { get; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x00004EA5 File Offset: 0x000030A5
		public string Description { get; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x00004EAD File Offset: 0x000030AD
		public bool? IsIpEnabled { get; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x00004EB5 File Offset: 0x000030B5
		public string MacAddress { get; }

		// Token: 0x060004C3 RID: 1219 RVA: 0x00004EBD File Offset: 0x000030BD
		public NetworkAdapter(string caption, string description, bool? enabled, string mac)
		{
			this.Caption = caption;
			this.Description = description;
			this.IsIpEnabled = enabled;
			this.MacAddress = mac;
		}
	}
}
