using System;

namespace NoiseMe.Drags.App.Models.WebSocket4Net.Common
{
	// Token: 0x020000C2 RID: 194
	public interface ICommandInfo<TCommandData> : ICommandInfo
	{
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x0600067F RID: 1663
		TCommandData Data { get; }
	}
}
