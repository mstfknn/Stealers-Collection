using System;
using System.ComponentModel;
using ProtoBuf;

namespace NoiseMe.Drags.App.Models.Communication
{
	// Token: 0x02000157 RID: 343
	[ProtoContract(Name = "CommunicationObject")]
	public class CommunicationObject : INotifyPropertyChanged
	{
		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x000086CF File Offset: 0x000068CF
		// (set) Token: 0x06000AF8 RID: 2808 RVA: 0x000086D7 File Offset: 0x000068D7
		[ProtoMember(1, Name = "Version")]
		public string Version { get; set; }

		// Token: 0x06000AF9 RID: 2809 RVA: 0x000086E0 File Offset: 0x000068E0
		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged == null)
			{
				return;
			}
			propertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000AFA RID: 2810 RVA: 0x00022538 File Offset: 0x00020738
		// (remove) Token: 0x06000AFB RID: 2811 RVA: 0x00022570 File Offset: 0x00020770
		public event PropertyChangedEventHandler PropertyChanged;
	}
}
