using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	// Token: 0x020000D1 RID: 209
	public interface IBufferStream
	{
		// Token: 0x060006C2 RID: 1730
		int Read(byte[] buffer, int offset, int count);

		// Token: 0x060006C3 RID: 1731
		int ReadByte();

		// Token: 0x060006C4 RID: 1732
		short ReadInt16();

		// Token: 0x060006C5 RID: 1733
		short ReadInt16(bool littleEndian);

		// Token: 0x060006C6 RID: 1734
		ushort ReadUInt16();

		// Token: 0x060006C7 RID: 1735
		ushort ReadUInt16(bool littleEndian);

		// Token: 0x060006C8 RID: 1736
		int ReadInt32();

		// Token: 0x060006C9 RID: 1737
		int ReadInt32(bool littleEndian);

		// Token: 0x060006CA RID: 1738
		uint ReadUInt32();

		// Token: 0x060006CB RID: 1739
		uint ReadUInt32(bool littleEndian);

		// Token: 0x060006CC RID: 1740
		long ReadInt64();

		// Token: 0x060006CD RID: 1741
		long ReadInt64(bool littleEndian);

		// Token: 0x060006CE RID: 1742
		ulong ReadUInt64();

		// Token: 0x060006CF RID: 1743
		ulong ReadUInt64(bool littleEndian);

		// Token: 0x060006D0 RID: 1744
		IList<ArraySegment<byte>> Take(int length);

		// Token: 0x060006D1 RID: 1745
		string ReadString(int length, Encoding encoding);

		// Token: 0x060006D2 RID: 1746
		IBufferStream Skip(int count);

		// Token: 0x060006D3 RID: 1747
		Stream GetCurrentStream();

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060006D4 RID: 1748
		long Length { get; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060006D5 RID: 1749
		IList<ArraySegment<byte>> Buffers { get; }

		// Token: 0x060006D6 RID: 1750
		void Clear();
	}
}
