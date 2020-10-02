using System;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000ED RID: 237
	public interface IPipelineProcessor
	{
		// Token: 0x06000742 RID: 1858
		ProcessResult Process(ArraySegment<byte> segment);

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000743 RID: 1859
		BufferList Cache { get; }

		// Token: 0x06000744 RID: 1860
		void Reset();
	}
}
