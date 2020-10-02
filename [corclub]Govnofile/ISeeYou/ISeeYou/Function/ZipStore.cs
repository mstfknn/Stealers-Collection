using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace ISeeYou
{
    class ZipStore
    {
        public static void PackedZip(string pathFolder, string zipName)
        {
            ZipStorer zip = ZipStorer.Create(zipName + ".zip", "");
            zip.EncodeUTF8 = true;

            string path = pathFolder;
            string[] arrFiles = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            foreach (var item in arrFiles)
            {
                string newPath = item.Replace(path, "");
                zip.AddFile(ZipStorer.Compression.Deflate,
                           item, newPath, "");
            }
            zip.Close();
        }
        private class ZipStorer : IDisposable
        {
            static ZipStorer()
            {
                for (int i = 0; i < CrcTable.Length; i++)
                {
                    uint num = (uint)i;
                    for (int j = 0; j < 8; j++)
                    {
                        if ((num & 1u) != 0u)
                        {
                            num = (3988292384u ^ num >> 1);
                        }
                        else
                        {
                            num >>= 1;
                        }
                    }
                    CrcTable[i] = num;
                }
            }

            public void AddFile(Compression _method, string _pathname, string _filenameInZip, string _comment)
            {
                if (this.Access == FileAccess.Read)
                {
                    throw new InvalidOperationException("Writing is not alowed");
                }
                FileStream fileStream = new FileStream(_pathname, FileMode.Open, FileAccess.Read);
                this.AddStream(_method, _filenameInZip, fileStream, File.GetLastWriteTime(_pathname), _comment);
                fileStream.Close();
            }
            public void AddStream(Compression _method, string _filenameInZip, Stream _source, DateTime _modTime, string _comment)
            {
                if (this.Access == FileAccess.Read)
                {
                    throw new InvalidOperationException("Writing is not alowed");
                }
                if (this.Files.Count != 0)
                {
                    ZipFileEntry zipFileEntry = this.Files[this.Files.Count - 1];
                }
                ZipFileEntry item = new ZipFileEntry
                {
                    Method = _method,
                    EncodeUTF8 = this.EncodeUTF8,
                    FilenameInZip = this.NormalizedFilename(_filenameInZip),
                    Comment = ((_comment == null) ? "" : _comment),
                    Crc32 = 0u,
                    HeaderOffset = (uint)this.ZipFileStream.Position,
                    ModifyTime = _modTime
                };
                this.WriteLocalHeader(ref item);
                item.FileOffset = (uint)this.ZipFileStream.Position;
                this.Store(ref item, _source);
                _source.Close();
                this.UpdateCrcAndSizes(ref item);
                this.Files.Add(item);
            }

            public void Close()
            {
                if (this.Access != FileAccess.Read)
                {
                    uint offset = (uint)this.ZipFileStream.Position;
                    uint num = 0u;
                    if (this.CentralDirImage != null)
                    {
                        this.ZipFileStream.Write(this.CentralDirImage, 0, this.CentralDirImage.Length);
                    }
                    for (int i = 0; i < this.Files.Count; i++)
                    {
                        long position = this.ZipFileStream.Position;
                        this.WriteCentralDirRecord(this.Files[i]);
                        num += (uint)(this.ZipFileStream.Position - position);
                    }
                    if (this.CentralDirImage != null)
                    {
                        this.WriteEndRecord(num + (uint)this.CentralDirImage.Length, offset);
                    }
                    else
                    {
                        this.WriteEndRecord(num, offset);
                    }
                }
                if (this.ZipFileStream != null)
                {
                    this.ZipFileStream.Flush();
                    this.ZipFileStream.Dispose();
                    this.ZipFileStream = null;
                }
            }
            public static ZipStorer Create(Stream _stream, string _comment)
            {
                return new ZipStorer
                {
                    Comment = _comment,
                    ZipFileStream = _stream,
                    Access = FileAccess.Write
                };
            }
            public static ZipStorer Create(string _filename, string _comment)
            {
                Stream stream = new FileStream(_filename, FileMode.Create, FileAccess.ReadWrite);
                ZipStorer zipStorer = Create(stream, _comment);
                zipStorer.Comment = _comment;
                zipStorer.FileName = _filename;
                return zipStorer;
            }
            private uint DateTimeToDosTime(DateTime _dt)
            {
                return (uint)(_dt.Second / 2 | _dt.Minute << 5 | _dt.Hour << 11 | _dt.Day << 16 | _dt.Month << 21 | _dt.Year - 1980 << 25);
            }
            public void Dispose()
            {
                this.Close();
            }
            private DateTime DosTimeToDateTime(uint _dt)
            {
                return new DateTime((int)((_dt >> 25) + 1980u), (int)(_dt >> 21 & 15u), (int)(_dt >> 16 & 31u), (int)(_dt >> 11 & 31u), (int)(_dt >> 5 & 63u), (int)((_dt & 31u) * 2u));
            }
            public bool ExtractFile(ZipFileEntry _zfe, Stream _stream)
            {
                if (!_stream.CanWrite)
                {
                    throw new InvalidOperationException("Stream cannot be written");
                }
                byte[] array = new byte[4];
                this.ZipFileStream.Seek((long)((ulong)_zfe.HeaderOffset), SeekOrigin.Begin);
                this.ZipFileStream.Read(array, 0, 4);
                bool result;
                if (BitConverter.ToUInt32(array, 0) == 67324752u)
                {
                    Stream stream;
                    if (_zfe.Method == Compression.Store)
                    {
                        stream = this.ZipFileStream;
                    }
                    else
                    {
                        if (_zfe.Method != Compression.Deflate)
                        {
                            goto IL_9E;
                        }
                        stream = new DeflateStream(this.ZipFileStream, CompressionMode.Decompress, true);
                    }
                    byte[] array2 = new byte[16384];
                    this.ZipFileStream.Seek((long)((ulong)_zfe.FileOffset), SeekOrigin.Begin);
                    int num2;
                    for (uint num = _zfe.FileSize; num > 0u; num -= (uint)num2)
                    {
                        num2 = stream.Read(array2, 0, (int)Math.Min((long)((ulong)num), (long)array2.Length));
                        _stream.Write(array2, 0, num2);
                    }
                    _stream.Flush();
                    if (_zfe.Method == Compression.Deflate)
                    {
                        stream.Dispose();
                    }
                    result = true;
                    return result;
                }
            IL_9E:
                result = false;
                return result;
            }
            public bool ExtractFile(ZipFileEntry _zfe, string _filename)
            {
                string directoryName = Path.GetDirectoryName(_filename);
                if (!Directory.Exists(directoryName))
                {
                    Directory.CreateDirectory(directoryName);
                }
                bool result;
                if (Directory.Exists(_filename))
                {
                    result = true;
                }
                else
                {
                    Stream stream = new FileStream(_filename, FileMode.Create, FileAccess.Write);
                    bool flag = this.ExtractFile(_zfe, stream);
                    if (flag)
                    {
                        stream.Close();
                    }
                    File.SetCreationTime(_filename, _zfe.ModifyTime);
                    File.SetLastWriteTime(_filename, _zfe.ModifyTime);
                    result = flag;
                }
                return result;
            }
            private uint GetFileOffset(uint _headerOffset)
            {
                byte[] array = new byte[2];
                this.ZipFileStream.Seek((long)((ulong)(_headerOffset + 26u)), SeekOrigin.Begin);
                this.ZipFileStream.Read(array, 0, 2);
                ushort num = BitConverter.ToUInt16(array, 0);
                this.ZipFileStream.Read(array, 0, 2);
                ushort num2 = BitConverter.ToUInt16(array, 0);
                return (uint)(30 + num + num2) + _headerOffset;
            }
            private string NormalizedFilename(string _filename)
            {
                string text = _filename.Replace('\\', '/');
                int num = text.IndexOf(':');
                if (num >= 0)
                {
                    text = text.Remove(0, num + 1);
                }
                return text.Trim(new char[]
			{
				'/'
			});
            }
            public static ZipStorer Open(Stream _stream, FileAccess _access)
            {
                if (!_stream.CanSeek && _access != FileAccess.Read)
                {
                    throw new InvalidOperationException("Stream cannot seek");
                }
                ZipStorer zipStorer = new ZipStorer
                {
                    ZipFileStream = _stream,
                    Access = _access
                };
                if (!zipStorer.ReadFileInfo())
                {
                    throw new InvalidDataException();
                }
                return zipStorer;
            }
            public static ZipStorer Open(string _filename, FileAccess _access)
            {
                Stream stream = new FileStream(_filename, FileMode.Open, (_access == FileAccess.Read) ? FileAccess.Read : FileAccess.ReadWrite);
                ZipStorer zipStorer = Open(stream, _access);
                zipStorer.FileName = _filename;
                return zipStorer;
            }
            public List<ZipFileEntry> ReadCentralDir()
            {
                if (this.CentralDirImage == null)
                {
                    throw new InvalidOperationException("Central directory currently does not exist");
                }
                List<ZipFileEntry> list = new List<ZipFileEntry>();
                List<ZipFileEntry> result;
                ushort num;
                ushort num2;
                ushort num3;
                for (int i = 0; i < this.CentralDirImage.Length; i += (int)(46 + num + num2 + num3))
                {
                    if (BitConverter.ToUInt32(this.CentralDirImage, i) != 33639248u)
                    {
                        result = list;
                        return result;
                    }
                    bool flag = (BitConverter.ToUInt16(this.CentralDirImage, i + 8) & 2048) != 0;
                    ushort method = BitConverter.ToUInt16(this.CentralDirImage, i + 10);
                    uint dt = BitConverter.ToUInt32(this.CentralDirImage, i + 12);
                    uint crc = BitConverter.ToUInt32(this.CentralDirImage, i + 16);
                    uint compressedSize = BitConverter.ToUInt32(this.CentralDirImage, i + 20);
                    uint fileSize = BitConverter.ToUInt32(this.CentralDirImage, i + 24);
                    num = BitConverter.ToUInt16(this.CentralDirImage, i + 28);
                    num2 = BitConverter.ToUInt16(this.CentralDirImage, i + 30);
                    num3 = BitConverter.ToUInt16(this.CentralDirImage, i + 32);
                    uint headerOffset = BitConverter.ToUInt32(this.CentralDirImage, i + 42);
                    uint headerSize = (uint)(46 + num + num2 + num3);
                    Encoding encoding = flag ? Encoding.UTF8 : DefaultEncoding;
                    ZipFileEntry item = new ZipFileEntry
                    {
                        Method = (Compression)method,
                        FilenameInZip = encoding.GetString(this.CentralDirImage, i + 46, (int)num),
                        FileOffset = this.GetFileOffset(headerOffset),
                        FileSize = fileSize,
                        CompressedSize = compressedSize,
                        HeaderOffset = headerOffset,
                        HeaderSize = headerSize,
                        Crc32 = crc,
                        ModifyTime = this.DosTimeToDateTime(dt)
                    };
                    if (num3 > 0)
                    {
                        item.Comment = encoding.GetString(this.CentralDirImage, i + 46 + (int)num + (int)num2, (int)num3);
                    }
                    list.Add(item);
                }
                result = list;
                return result;
            }
            private bool ReadFileInfo()
            {
                bool result;
                if (this.ZipFileStream.Length >= 22L)
                {
                    try
                    {
                        this.ZipFileStream.Seek(-17L, SeekOrigin.End);
                        BinaryReader binaryReader = new BinaryReader(this.ZipFileStream);
                        while (true)
                        {
                            this.ZipFileStream.Seek(-5L, SeekOrigin.Current);
                            if (binaryReader.ReadUInt32() == 101010256u)
                            {
                                break;
                            }
                            if (this.ZipFileStream.Position <= 0L)
                            {
                                goto Block_5;
                            }
                        }
                        this.ZipFileStream.Seek(6L, SeekOrigin.Current);
                        ushort existingFiles = binaryReader.ReadUInt16();
                        int num = binaryReader.ReadInt32();
                        uint num2 = binaryReader.ReadUInt32();
                        ushort num3 = binaryReader.ReadUInt16();
                        if (this.ZipFileStream.Position + (long)((ulong)num3) != this.ZipFileStream.Length)
                        {
                            result = false;
                            return result;
                        }
                        this.ExistingFiles = existingFiles;
                        this.CentralDirImage = new byte[num];
                        this.ZipFileStream.Seek((long)((ulong)num2), SeekOrigin.Begin);
                        this.ZipFileStream.Read(this.CentralDirImage, 0, num);
                        this.ZipFileStream.Seek((long)((ulong)num2), SeekOrigin.Begin);
                        result = true;
                        return result;
                    Block_5: ;
                    }
                    catch
                    {
                    }
                }
                result = false;
                return result;
            }
            public static bool RemoveEntries(ref ZipStorer _zip, List<ZipFileEntry> _zfes)
            {
                if (!(_zip.ZipFileStream is FileStream))
                {
                    throw new InvalidOperationException("RemoveEntries is allowed just over streams of type FileStream");
                }
                List<ZipFileEntry> list = _zip.ReadCentralDir();
                string tempFileName = Path.GetTempFileName();
                string tempFileName2 = Path.GetTempFileName();
                bool result;
                try
                {
                    ZipStorer zipStorer = Create(tempFileName, string.Empty);
                    foreach (ZipFileEntry current in list)
                    {
                        if (!_zfes.Contains(current) && _zip.ExtractFile(current, tempFileName2))
                        {
                            zipStorer.AddFile(current.Method, tempFileName2, current.FilenameInZip, current.Comment);
                        }
                    }
                    _zip.Close();
                    zipStorer.Close();
                    File.Delete(_zip.FileName);
                    File.Move(tempFileName, _zip.FileName);
                    _zip = Open(_zip.FileName, _zip.Access);
                }
                catch
                {
                    result = false;
                    return result;
                }
                finally
                {
                    if (File.Exists(tempFileName))
                    {
                        File.Delete(tempFileName);
                    }
                    if (File.Exists(tempFileName2))
                    {
                        File.Delete(tempFileName2);
                    }
                }
                result = true;
                return result;
            }
            private void Store(ref ZipFileEntry _zfe, Stream _source)
            {
                byte[] array = new byte[16384];
                uint num = 0u;
                long position = this.ZipFileStream.Position;
                long position2 = _source.Position;
                Stream stream;
                if (_zfe.Method == Compression.Store)
                {
                    stream = this.ZipFileStream;
                }
                else
                {
                    stream = new DeflateStream(this.ZipFileStream, CompressionMode.Compress, true);
                }
                _zfe.Crc32 = 4294967295u;
                int num2;
                do
                {
                    num2 = _source.Read(array, 0, array.Length);
                    num += (uint)num2;
                    if (num2 > 0)
                    {
                        stream.Write(array, 0, num2);
                        uint num3 = 0u;
                        while ((ulong)num3 < (ulong)((long)num2))
                        {
                            _zfe.Crc32 = (CrcTable[(int)((IntPtr)((long)((ulong)((_zfe.Crc32 ^ (uint)array[(int)((UIntPtr)num3)]) & 255u))))] ^ _zfe.Crc32 >> 8);
                            num3 += 1u;
                        }
                    }
                }
                while (num2 == array.Length);
                stream.Flush();
                if (_zfe.Method == Compression.Deflate)
                {
                    stream.Dispose();
                }
                _zfe.Crc32 ^= 4294967295u;
                _zfe.FileSize = num;
                _zfe.CompressedSize = (uint)(this.ZipFileStream.Position - position);
                if (_zfe.Method == Compression.Deflate && !this.ForceDeflating && _source.CanSeek && _zfe.CompressedSize > _zfe.FileSize)
                {
                    _zfe.Method = Compression.Store;
                    this.ZipFileStream.Position = position;
                    this.ZipFileStream.SetLength(position);
                    _source.Position = position2;
                    this.Store(ref _zfe, _source);
                }
            }
            private void UpdateCrcAndSizes(ref ZipFileEntry _zfe)
            {
                long position = this.ZipFileStream.Position;
                this.ZipFileStream.Position = (long)((ulong)(_zfe.HeaderOffset + 8u));
                this.ZipFileStream.Write(BitConverter.GetBytes((ushort)_zfe.Method), 0, 2);
                this.ZipFileStream.Position = (long)((ulong)(_zfe.HeaderOffset + 14u));
                this.ZipFileStream.Write(BitConverter.GetBytes(_zfe.Crc32), 0, 4);
                this.ZipFileStream.Write(BitConverter.GetBytes(_zfe.CompressedSize), 0, 4);
                this.ZipFileStream.Write(BitConverter.GetBytes(_zfe.FileSize), 0, 4);
                this.ZipFileStream.Position = position;
            }
            private void WriteCentralDirRecord(ZipFileEntry _zfe)
            {
                Encoding encoding = _zfe.EncodeUTF8 ? Encoding.UTF8 : DefaultEncoding;
                byte[] bytes = encoding.GetBytes(_zfe.FilenameInZip);
                byte[] bytes2 = encoding.GetBytes(_zfe.Comment);
                this.ZipFileStream.Write(new byte[]
			{
				80,
				75,
				1,
				2,
				23,
				11,
				20,
				0
			}, 0, 8);
                this.ZipFileStream.Write(BitConverter.GetBytes(_zfe.EncodeUTF8 ? 2048 : 0), 0, 2);
                this.ZipFileStream.Write(BitConverter.GetBytes((ushort)_zfe.Method), 0, 2);
                this.ZipFileStream.Write(BitConverter.GetBytes(this.DateTimeToDosTime(_zfe.ModifyTime)), 0, 4);
                this.ZipFileStream.Write(BitConverter.GetBytes(_zfe.Crc32), 0, 4);
                this.ZipFileStream.Write(BitConverter.GetBytes(_zfe.CompressedSize), 0, 4);
                this.ZipFileStream.Write(BitConverter.GetBytes(_zfe.FileSize), 0, 4);
                this.ZipFileStream.Write(BitConverter.GetBytes((ushort)bytes.Length), 0, 2);
                this.ZipFileStream.Write(BitConverter.GetBytes(0), 0, 2);
                this.ZipFileStream.Write(BitConverter.GetBytes((ushort)bytes2.Length), 0, 2);
                this.ZipFileStream.Write(BitConverter.GetBytes(0), 0, 2);
                this.ZipFileStream.Write(BitConverter.GetBytes(0), 0, 2);
                this.ZipFileStream.Write(BitConverter.GetBytes(0), 0, 2);
                this.ZipFileStream.Write(BitConverter.GetBytes(33024), 0, 2);
                this.ZipFileStream.Write(BitConverter.GetBytes(_zfe.HeaderOffset), 0, 4);
                this.ZipFileStream.Write(bytes, 0, bytes.Length);
                this.ZipFileStream.Write(bytes2, 0, bytes2.Length);
            }
            private void WriteEndRecord(uint _size, uint _offset)
            {
                byte[] bytes = (this.EncodeUTF8 ? Encoding.UTF8 : DefaultEncoding).GetBytes(this.Comment);
                this.ZipFileStream.Write(new byte[]
			{
				80,
				75,
				5,
				6,
				0,
				0,
				0,
				0
			}, 0, 8);
                this.ZipFileStream.Write(BitConverter.GetBytes((int)((ushort)this.Files.Count + this.ExistingFiles)), 0, 2);
                this.ZipFileStream.Write(BitConverter.GetBytes((int)((ushort)this.Files.Count + this.ExistingFiles)), 0, 2);
                this.ZipFileStream.Write(BitConverter.GetBytes(_size), 0, 4);
                this.ZipFileStream.Write(BitConverter.GetBytes(_offset), 0, 4);
                this.ZipFileStream.Write(BitConverter.GetBytes((ushort)bytes.Length), 0, 2);
                this.ZipFileStream.Write(bytes, 0, bytes.Length);
            }
            private void WriteLocalHeader(ref ZipFileEntry _zfe)
            {
                long position = this.ZipFileStream.Position;
                byte[] bytes = (_zfe.EncodeUTF8 ? Encoding.UTF8 : DefaultEncoding).GetBytes(_zfe.FilenameInZip);
                this.ZipFileStream.Write(new byte[]
			{
				80,
				75,
				3,
				4,
				20,
				0
			}, 0, 6);
                this.ZipFileStream.Write(BitConverter.GetBytes(_zfe.EncodeUTF8 ? 2048 : 0), 0, 2);
                this.ZipFileStream.Write(BitConverter.GetBytes((ushort)_zfe.Method), 0, 2);
                this.ZipFileStream.Write(BitConverter.GetBytes(this.DateTimeToDosTime(_zfe.ModifyTime)), 0, 4);
                byte[] buffer = new byte[12];
                this.ZipFileStream.Write(buffer, 0, 12);
                this.ZipFileStream.Write(BitConverter.GetBytes((ushort)bytes.Length), 0, 2);
                this.ZipFileStream.Write(BitConverter.GetBytes(0), 0, 2);
                this.ZipFileStream.Write(bytes, 0, bytes.Length);
                _zfe.HeaderSize = (uint)(this.ZipFileStream.Position - position);
            }
            private FileAccess Access;
            private byte[] CentralDirImage;
            private string Comment = "";
            private static uint[] CrcTable = new uint[256];
            private static Encoding DefaultEncoding = Encoding.GetEncoding(437);
            public bool EncodeUTF8;
            private ushort ExistingFiles;
            private string FileName;
            private List<ZipFileEntry> Files = new List<ZipFileEntry>();
            public bool ForceDeflating;
            private Stream ZipFileStream;
            public enum Compression : ushort
            {
                Deflate = 8,
                Store = 0
            }

            public struct ZipFileEntry
            {
                public override string ToString()
                {
                    return this.FilenameInZip;
                }

                public Compression Method;
                public string FilenameInZip;
                public uint FileSize;
                public uint CompressedSize;
                public uint HeaderOffset;
                public uint FileOffset;
                public uint HeaderSize;
                public uint Crc32;
                public DateTime ModifyTime;
                public string Comment;
                public bool EncodeUTF8;
            }
        }
    }
}
