using System;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase.Encoder
{
	// Token: 0x02000101 RID: 257
	public interface IProtoObjectEncoder
	{
		// Token: 0x060007C5 RID: 1989
		void EncodeObject(IOutputBuffer output, object target);
	}
}
