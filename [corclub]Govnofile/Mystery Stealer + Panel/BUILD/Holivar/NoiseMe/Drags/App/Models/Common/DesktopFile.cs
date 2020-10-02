using System;
using System.ComponentModel;
using ProtoBuf;

namespace NoiseMe.Drags.App.Models.Common
{
	// Token: 0x02000164 RID: 356
	[ProtoContract(Name = "DesktopFile")]
	public class DesktopFile : INotifyPropertyChanged
	{
		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x00008D1B File Offset: 0x00006F1B
		// (set) Token: 0x06000B6B RID: 2923 RVA: 0x00008D23 File Offset: 0x00006F23
		[ProtoMember(1, Name = "Filename")]
		public string Filename
		{
			get
			{
				return this._filename;
			}
			set
			{
				this._filename = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("Filename"));
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x00008D47 File Offset: 0x00006F47
		// (set) Token: 0x06000B6D RID: 2925 RVA: 0x00008D4F File Offset: 0x00006F4F
		[ProtoMember(2, Name = "FileData")]
		public byte[] FileData
		{
			get
			{
				return this._fileData;
			}
			set
			{
				this._fileData = value;
				PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
				if (propertyChanged == null)
				{
					return;
				}
				propertyChanged(this, new PropertyChangedEventArgs("FileData"));
			}
		}

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06000B6E RID: 2926 RVA: 0x00022FB0 File Offset: 0x000211B0
		// (remove) Token: 0x06000B6F RID: 2927 RVA: 0x00022FE8 File Offset: 0x000211E8
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x04000462 RID: 1122
		private string _filename;

		// Token: 0x04000463 RID: 1123
		private byte[] _fileData;
	}
}
