using System;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Common
{
	// Token: 0x020000BE RID: 190
	public interface IClientCommandReader<TCommandInfo> where TCommandInfo : ICommandInfo
	{
		// Token: 0x0600067A RID: 1658
		TCommandInfo GetCommandInfo(byte[] readBuffer, int offset, int length, out int left);

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600067B RID: 1659
		IClientCommandReader<TCommandInfo> NextCommandReader { get; }
	}
}
