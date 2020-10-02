using System;
using System.ComponentModel;
using ProtoBuf;

namespace NoiseMe.Drags.App.Models.Common
{
	// Token: 0x0200015D RID: 349
	[ProtoContract(Name = "RemoteTask")]
	public class RemoteTask : INotifyPropertyChanged
	{
		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x000087F8 File Offset: 0x000069F8
		// (set) Token: 0x06000B11 RID: 2833 RVA: 0x00008800 File Offset: 0x00006A00
		[ProtoMember(1, Name = "Id")]
		public int Id { get; set; }

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000B12 RID: 2834 RVA: 0x00008809 File Offset: 0x00006A09
		// (set) Token: 0x06000B13 RID: 2835 RVA: 0x00008811 File Offset: 0x00006A11
		[ProtoMember(2, Name = "Action")]
		public string Action { get; set; }

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x0000881A File Offset: 0x00006A1A
		// (set) Token: 0x06000B15 RID: 2837 RVA: 0x00008822 File Offset: 0x00006A22
		[ProtoMember(3, Name = "Target")]
		public string Target { get; set; }

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x0000882B File Offset: 0x00006A2B
		// (set) Token: 0x06000B17 RID: 2839 RVA: 0x00008833 File Offset: 0x00006A33
		[ProtoMember(4, Name = "ExecuteHidden")]
		public bool ExecuteHidden { get; set; }

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000B18 RID: 2840 RVA: 0x00022948 File Offset: 0x00020B48
		// (remove) Token: 0x06000B19 RID: 2841 RVA: 0x00022980 File Offset: 0x00020B80
		public event PropertyChangedEventHandler PropertyChanged;
	}
}
