#region

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Security.Cryptography;
using KoiVM.AST.IL;

#endregion

namespace KoiVM.RT
{
    [Obfuscation(Exclude = false, Feature = "+koi;-ref proxy")]
    internal class DbgWriter
    {
        private byte[] dbgInfo;
        private readonly HashSet<string> documents = new HashSet<string>();

        private readonly Dictionary<ILBlock, List<DbgEntry>> entries = new Dictionary<ILBlock, List<DbgEntry>>();

        public void AddSequencePoint(ILBlock block, uint offset, uint len, string document, uint lineNum)
        {
            if (!this.entries.TryGetValue(block, out List<DbgEntry> entryList))
                entryList = this.entries[block] = new List<DbgEntry>();

            entryList.Add(new DbgEntry
            {
                offset = offset,
                len = len,
                document = document,
                lineNum = lineNum
            });
            this.documents.Add(document);
        }

        public DbgSerializer GetSerializer() => new DbgSerializer(this);

        public byte[] GetDbgInfo() => this.dbgInfo;

        private struct DbgEntry
        {
            public uint offset;
            public uint len;

            public string document;
            public uint lineNum;
        }

        internal class DbgSerializer : IDisposable
        {
            private readonly DbgWriter dbg;
            private Dictionary<string, uint> docMap;
            private readonly MemoryStream stream;
            private readonly BinaryWriter writer;

            internal DbgSerializer(DbgWriter dbg)
            {
                this.dbg = dbg;
                this.stream = new MemoryStream();
                var aes = new AesManaged();
                aes.IV = aes.Key = Convert.FromBase64String("UkVwAyrARLAy4GmQLL860w==");
                this.writer = new BinaryWriter(
                    new DeflateStream(
                        new CryptoStream(this.stream, aes.CreateEncryptor(), CryptoStreamMode.Write),
                        CompressionMode.Compress
                    )
                );

                this.InitStream();
            }

            public void Dispose()
            {
                this.writer.Dispose();
                this.dbg.dbgInfo = this.stream.ToArray();
            }

            private void InitStream()
            {
                this.docMap = new Dictionary<string, uint>();
                this.writer.Write(this.dbg.documents.Count);
                uint docId = 0;
                foreach(string doc in this.dbg.documents)
                {
                    this.writer.Write(doc);
                    this.docMap[doc] = docId++;
                }
            }

            public void WriteBlock(BasicBlockChunk chunk)
            {
                if (chunk == null || !this.dbg.entries.TryGetValue(chunk.Block, out List<DbgEntry> entryList) ||
                   chunk.Block.Content.Count == 0)
                    return;

                uint offset = chunk.Block.Content[0].Offset;
                foreach(DbgEntry entry in entryList)
                {
                    this.writer.Write(entry.offset + chunk.Block.Content[0].Offset);
                    this.writer.Write(entry.len);
                    this.writer.Write(this.docMap[entry.document]);
                    this.writer.Write(entry.lineNum);
                }
            }
        }
    }
}