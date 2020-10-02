using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NoiseMe.Drags.App.Models.SuperSocket.ProtoBase
{
	public interface IBufferStream
	{
		long Length
		{
			get;
		}

		IList<ArraySegment<byte>> Buffers
		{
			get;
		}

		int Read(byte[] buffer, int offset, int count);

		int ReadByte();

		short ReadInt16();

		short ReadInt16(bool littleEndian);

		ushort ReadUInt16();

		ushort ReadUInt16(bool littleEndian);

		int ReadInt32();

		int ReadInt32(bool littleEndian);

		uint ReadUInt32();

		uint ReadUInt32(bool littleEndian);

		long ReadInt64();

		long ReadInt64(bool littleEndian);

		ulong ReadUInt64();

		ulong ReadUInt64(bool littleEndian);

		IList<ArraySegment<byte>> Take(int length);

		string ReadString(int length, Encoding encoding);

		IBufferStream Skip(int count);

		Stream GetCurrentStream();

		void Clear();
	}
}
