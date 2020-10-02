using System;
using System.IO;

namespace Confuser.Runtime
{
    internal static class Lzma {
		const uint kNumStates = 12;

		const int kNumPosSlotBits = 6;

		const uint kNumLenToPosStates = 4;

		const uint kMatchMinLen = 2;

		const int kNumAlignBits = 4;
		const uint kAlignTableSize = 1 << kNumAlignBits;

		const uint kStartPosModelIndex = 4;
		const uint kEndPosModelIndex = 14;

		const uint kNumFullDistances = 1 << ((int)kEndPosModelIndex / 2);

		const int kNumPosStatesBitsMax = 4;
		const uint kNumPosStatesMax = (1 << kNumPosStatesBitsMax);

		const int kNumLowLenBits = 3;
		const int kNumMidLenBits = 3;
		const int kNumHighLenBits = 8;
		const uint kNumLowLenSymbols = 1 << kNumLowLenBits;
		const uint kNumMidLenSymbols = 1 << kNumMidLenBits;

		public static byte[] Decompress(byte[] data) {
			var s = new MemoryStream(data);
			var decoder = new LzmaDecoder();
            byte[] prop = new byte[5];
			s.Read(prop, 0, 5);
			decoder.SetDecoderProperties(prop);
			long outSize = 0;
			for (int i = 0; i < 8; i++) {
				int v = s.ReadByte();
				outSize |= ((long)(byte)v) << (8 * i);
			}
            byte[] b = new byte[(int)outSize];
			var z = new MemoryStream(b, true);
			long compressedSize = s.Length - 13;
			decoder.Code(s, z, compressedSize, outSize);
			return b;
		}

		struct BitDecoder {
			public const int kNumBitModelTotalBits = 11;
			public const uint kBitModelTotal = (1 << kNumBitModelTotalBits);
			const int kNumMoveBits = 5;

			uint Prob;

            public void Init() => this.Prob = kBitModelTotal >> 1;

            public uint Decode(Decoder rangeDecoder) {
				uint newBound = (rangeDecoder.Range >> kNumBitModelTotalBits) * this.Prob;
				if (rangeDecoder.Code < newBound) {
					rangeDecoder.Range = newBound;
                    this.Prob += (kBitModelTotal - this.Prob) >> kNumMoveBits;
					if (rangeDecoder.Range < Decoder.kTopValue) {
						rangeDecoder.Code = (rangeDecoder.Code << 8) | (byte)rangeDecoder.Stream.ReadByte();
						rangeDecoder.Range <<= 8;
					}
					return 0;
				}
				rangeDecoder.Range -= newBound;
				rangeDecoder.Code -= newBound;
                this.Prob -= (this.Prob) >> kNumMoveBits;
				if (rangeDecoder.Range < Decoder.kTopValue) {
					rangeDecoder.Code = (rangeDecoder.Code << 8) | (byte)rangeDecoder.Stream.ReadByte();
					rangeDecoder.Range <<= 8;
				}
				return 1;
			}
		}

		struct BitTreeDecoder {
			readonly BitDecoder[] Models;
			readonly int NumBitLevels;

			public BitTreeDecoder(int numBitLevels) {
                this.NumBitLevels = numBitLevels;
                this.Models = new BitDecoder[1 << numBitLevels];
			}

			public void Init() {
				for (uint i = 1; i < (1 << this.NumBitLevels); i++)
                    this.Models[i].Init();
			}

			public uint Decode(Decoder rangeDecoder) {
				uint m = 1;
				for (int bitIndex = this.NumBitLevels; bitIndex > 0; bitIndex--)
					m = (m << 1) + this.Models[m].Decode(rangeDecoder);
				return m - ((uint)1 << this.NumBitLevels);
			}

			public uint ReverseDecode(Decoder rangeDecoder) {
				uint m = 1;
				uint symbol = 0;
				for (int bitIndex = 0; bitIndex < this.NumBitLevels; bitIndex++) {
					uint bit = this.Models[m].Decode(rangeDecoder);
					m <<= 1;
					m += bit;
					symbol |= (bit << bitIndex);
				}
				return symbol;
			}

			public static uint ReverseDecode(BitDecoder[] Models, UInt32 startIndex,
			                                 Decoder rangeDecoder, int NumBitLevels) {
				uint m = 1;
				uint symbol = 0;
				for (int bitIndex = 0; bitIndex < NumBitLevels; bitIndex++) {
					uint bit = Models[startIndex + m].Decode(rangeDecoder);
					m <<= 1;
					m += bit;
					symbol |= (bit << bitIndex);
				}
				return symbol;
			}
		}

		class Decoder {
			public const uint kTopValue = (1 << 24);
			public uint Code;
			public uint Range;
			public Stream Stream;

			public void Init(Stream stream) {
                // Stream.Init(stream);
                this.Stream = stream;

                this.Code = 0;
                this.Range = 0xFFFFFFFF;
				for (int i = 0; i < 5; i++)
                    this.Code = (this.Code << 8) | (byte)this.Stream.ReadByte();
			}

            public void ReleaseStream() => this.Stream = null;

            public void Normalize() {
				while (this.Range < kTopValue) {
                    this.Code = (this.Code << 8) | (byte)this.Stream.ReadByte();
                    this.Range <<= 8;
				}
			}

			public uint DecodeDirectBits(int numTotalBits) {
				uint range = this.Range;
				uint code = this.Code;
				uint result = 0;
				for (int i = numTotalBits; i > 0; i--) {
					range >>= 1;
					/*
                    result <<= 1;
                    if (code >= range)
                    {
                        code -= range;
                        result |= 1;
                    }
                    */
					uint t = (code - range) >> 31;
					code -= range & (t - 1);
					result = (result << 1) | (1 - t);

					if (range < kTopValue) {
						code = (code << 8) | (byte)this.Stream.ReadByte();
						range <<= 8;
					}
				}
                this.Range = range;
                this.Code = code;
				return result;
			}
		}

		class LzmaDecoder {
			readonly BitDecoder[] m_IsMatchDecoders = new BitDecoder[kNumStates << kNumPosStatesBitsMax];
			readonly BitDecoder[] m_IsRep0LongDecoders = new BitDecoder[kNumStates << kNumPosStatesBitsMax];
			readonly BitDecoder[] m_IsRepDecoders = new BitDecoder[kNumStates];
			readonly BitDecoder[] m_IsRepG0Decoders = new BitDecoder[kNumStates];
			readonly BitDecoder[] m_IsRepG1Decoders = new BitDecoder[kNumStates];
			readonly BitDecoder[] m_IsRepG2Decoders = new BitDecoder[kNumStates];

			readonly LenDecoder m_LenDecoder = new LenDecoder();

			readonly LiteralDecoder m_LiteralDecoder = new LiteralDecoder();
			readonly OutWindow m_OutWindow = new OutWindow();
			readonly BitDecoder[] m_PosDecoders = new BitDecoder[kNumFullDistances - kEndPosModelIndex];
			readonly BitTreeDecoder[] m_PosSlotDecoder = new BitTreeDecoder[kNumLenToPosStates];
			readonly Decoder m_RangeDecoder = new Decoder();
			readonly LenDecoder m_RepLenDecoder = new LenDecoder();
			bool _solid = false;

			uint m_DictionarySize;
			uint m_DictionarySizeCheck;
			BitTreeDecoder m_PosAlignDecoder = new BitTreeDecoder(kNumAlignBits);

			uint m_PosStateMask;

			public LzmaDecoder() {
                this.m_DictionarySize = 0xFFFFFFFF;
				for (int i = 0; i < kNumLenToPosStates; i++)
                    this.m_PosSlotDecoder[i] = new BitTreeDecoder(kNumPosSlotBits);
			}

			void SetDictionarySize(uint dictionarySize) {
				if (this.m_DictionarySize != dictionarySize) {
                    this.m_DictionarySize = dictionarySize;
                    this.m_DictionarySizeCheck = Math.Max(this.m_DictionarySize, 1);
					uint blockSize = Math.Max(this.m_DictionarySizeCheck, (1 << 12));
                    this.m_OutWindow.Create(blockSize);
				}
			}

            void SetLiteralProperties(int lp, int lc) => this.m_LiteralDecoder.Create(lp, lc);

            void SetPosBitsProperties(int pb) {
				uint numPosStates = (uint)1 << pb;
                this.m_LenDecoder.Create(numPosStates);
                this.m_RepLenDecoder.Create(numPosStates);
                this.m_PosStateMask = numPosStates - 1;
			}

			void Init(Stream inStream, Stream outStream) {
                this.m_RangeDecoder.Init(inStream);
                this.m_OutWindow.Init(outStream, this._solid);

				uint i;
				for (i = 0; i < kNumStates; i++) {
					for (uint j = 0; j <= this.m_PosStateMask; j++) {
						uint index = (i << kNumPosStatesBitsMax) + j;
                        this.m_IsMatchDecoders[index].Init();
                        this.m_IsRep0LongDecoders[index].Init();
					}
                    this.m_IsRepDecoders[i].Init();
                    this.m_IsRepG0Decoders[i].Init();
                    this.m_IsRepG1Decoders[i].Init();
                    this.m_IsRepG2Decoders[i].Init();
				}

                this.m_LiteralDecoder.Init();
				for (i = 0; i < kNumLenToPosStates; i++)
                    this.m_PosSlotDecoder[i].Init();
				// m_PosSpecDecoder.Init();
				for (i = 0; i < kNumFullDistances - kEndPosModelIndex; i++)
                    this.m_PosDecoders[i].Init();

                this.m_LenDecoder.Init();
                this.m_RepLenDecoder.Init();
                this.m_PosAlignDecoder.Init();
			}

			public void Code(Stream inStream, Stream outStream,
			                 Int64 inSize, Int64 outSize) {
                this.Init(inStream, outStream);

				var state = new State();
				state.Init();
				uint rep0 = 0, rep1 = 0, rep2 = 0, rep3 = 0;

				UInt64 nowPos64 = 0;
                ulong outSize64 = (UInt64)outSize;
				if (nowPos64 < outSize64) {
                    this.m_IsMatchDecoders[state.Index << kNumPosStatesBitsMax].Decode(this.m_RangeDecoder);
					state.UpdateChar();
					byte b = this.m_LiteralDecoder.DecodeNormal(this.m_RangeDecoder, 0, 0);
                    this.m_OutWindow.PutByte(b);
					nowPos64++;
				}
				while (nowPos64 < outSize64) {
					// UInt64 next = Math.Min(nowPos64 + (1 << 18), outSize64);
					// while(nowPos64 < next)
					{
						uint posState = (uint)nowPos64 & this.m_PosStateMask;
						if (this.m_IsMatchDecoders[(state.Index << kNumPosStatesBitsMax) + posState].Decode(this.m_RangeDecoder) == 0) {
							byte b;
							byte prevByte = this.m_OutWindow.GetByte(0);
							if (!state.IsCharState())
								b = this.m_LiteralDecoder.DecodeWithMatchByte(this.m_RangeDecoder,
								                                         (uint)nowPos64, prevByte, this.m_OutWindow.GetByte(rep0));
							else
								b = this.m_LiteralDecoder.DecodeNormal(this.m_RangeDecoder, (uint)nowPos64, prevByte);
                            this.m_OutWindow.PutByte(b);
							state.UpdateChar();
							nowPos64++;
						}
						else {
							uint len;
							if (this.m_IsRepDecoders[state.Index].Decode(this.m_RangeDecoder) == 1) {
								if (this.m_IsRepG0Decoders[state.Index].Decode(this.m_RangeDecoder) == 0) {
									if (this.m_IsRep0LongDecoders[(state.Index << kNumPosStatesBitsMax) + posState].Decode(this.m_RangeDecoder) == 0) {
										state.UpdateShortRep();
                                        this.m_OutWindow.PutByte(this.m_OutWindow.GetByte(rep0));
										nowPos64++;
										continue;
									}
								}
								else {
									UInt32 distance;
									if (this.m_IsRepG1Decoders[state.Index].Decode(this.m_RangeDecoder) == 0) {
										distance = rep1;
									}
									else {
										if (this.m_IsRepG2Decoders[state.Index].Decode(this.m_RangeDecoder) == 0)
											distance = rep2;
										else {
											distance = rep3;
											rep3 = rep2;
										}
										rep2 = rep1;
									}
									rep1 = rep0;
									rep0 = distance;
								}
								len = this.m_RepLenDecoder.Decode(this.m_RangeDecoder, posState) + kMatchMinLen;
								state.UpdateRep();
							}
							else {
								rep3 = rep2;
								rep2 = rep1;
								rep1 = rep0;
								len = kMatchMinLen + this.m_LenDecoder.Decode(this.m_RangeDecoder, posState);
								state.UpdateMatch();
								uint posSlot = this.m_PosSlotDecoder[GetLenToPosState(len)].Decode(this.m_RangeDecoder);
								if (posSlot >= kStartPosModelIndex) {
                                    int numDirectBits = (int)((posSlot >> 1) - 1);
									rep0 = ((2 | (posSlot & 1)) << numDirectBits);
									if (posSlot < kEndPosModelIndex)
										rep0 += BitTreeDecoder.ReverseDecode(this.m_PosDecoders,
										                                     rep0 - posSlot - 1, this.m_RangeDecoder, numDirectBits);
									else {
										rep0 += (this.m_RangeDecoder.DecodeDirectBits(
											numDirectBits - kNumAlignBits) << kNumAlignBits);
										rep0 += this.m_PosAlignDecoder.ReverseDecode(this.m_RangeDecoder);
									}
								}
								else
									rep0 = posSlot;
							}
							if (rep0 >= nowPos64 || rep0 >= this.m_DictionarySizeCheck) {
								if (rep0 == 0xFFFFFFFF)
									break;
							}
                            this.m_OutWindow.CopyBlock(rep0, len);
							nowPos64 += len;
						}
					}
				}
                this.m_OutWindow.Flush();
                this.m_OutWindow.ReleaseStream();
                this.m_RangeDecoder.ReleaseStream();
			}

			public void SetDecoderProperties(byte[] properties) {
				int lc = properties[0] % 9;
				int remainder = properties[0] / 9;
				int lp = remainder % 5;
				int pb = remainder / 5;
				UInt32 dictionarySize = 0;
				for (int i = 0; i < 4; i++)
					dictionarySize += ((UInt32)(properties[1 + i])) << (i * 8);
                this.SetDictionarySize(dictionarySize);
                this.SetLiteralProperties(lp, lc);
                this.SetPosBitsProperties(pb);
			}

			static uint GetLenToPosState(uint len) {
				len -= kMatchMinLen;
                return len < kNumLenToPosStates ? len : kNumLenToPosStates - 1;
            }

            class LenDecoder {
				readonly BitTreeDecoder[] m_LowCoder = new BitTreeDecoder[kNumPosStatesMax];
				readonly BitTreeDecoder[] m_MidCoder = new BitTreeDecoder[kNumPosStatesMax];
				BitDecoder m_Choice = new BitDecoder();
				BitDecoder m_Choice2 = new BitDecoder();
				BitTreeDecoder m_HighCoder = new BitTreeDecoder(kNumHighLenBits);
				uint m_NumPosStates;

				public void Create(uint numPosStates) {
					for (uint posState = this.m_NumPosStates; posState < numPosStates; posState++) {
                        this.m_LowCoder[posState] = new BitTreeDecoder(kNumLowLenBits);
                        this.m_MidCoder[posState] = new BitTreeDecoder(kNumMidLenBits);
					}
                    this.m_NumPosStates = numPosStates;
				}

				public void Init() {
                    this.m_Choice.Init();
					for (uint posState = 0; posState < this.m_NumPosStates; posState++) {
                        this.m_LowCoder[posState].Init();
                        this.m_MidCoder[posState].Init();
					}
                    this.m_Choice2.Init();
                    this.m_HighCoder.Init();
				}

				public uint Decode(Decoder rangeDecoder, uint posState) {
					if (this.m_Choice.Decode(rangeDecoder) == 0)
						return this.m_LowCoder[posState].Decode(rangeDecoder);
					uint symbol = kNumLowLenSymbols;
					if (this.m_Choice2.Decode(rangeDecoder) == 0)
						symbol += this.m_MidCoder[posState].Decode(rangeDecoder);
					else {
						symbol += kNumMidLenSymbols;
						symbol += this.m_HighCoder.Decode(rangeDecoder);
					}
					return symbol;
				}
			}

			class LiteralDecoder {
				Decoder2[] m_Coders;
				int m_NumPosBits;
				int m_NumPrevBits;
				uint m_PosMask;

				public void Create(int numPosBits, int numPrevBits) {
					if (this.m_Coders != null && this.m_NumPrevBits == numPrevBits &&
                        this.m_NumPosBits == numPosBits)
						return;
                    this.m_NumPosBits = numPosBits;
                    this.m_PosMask = ((uint)1 << numPosBits) - 1;
                    this.m_NumPrevBits = numPrevBits;
					uint numStates = (uint)1 << (this.m_NumPrevBits + this.m_NumPosBits);
                    this.m_Coders = new Decoder2[numStates];
					for (uint i = 0; i < numStates; i++)
                        this.m_Coders[i].Create();
				}

				public void Init() {
					uint numStates = (uint)1 << (this.m_NumPrevBits + this.m_NumPosBits);
					for (uint i = 0; i < numStates; i++)
                        this.m_Coders[i].Init();
				}

                uint GetState(uint pos, byte prevByte) => ((pos & this.m_PosMask) << this.m_NumPrevBits) + (uint)(prevByte >> (8 - this.m_NumPrevBits));

                public byte DecodeNormal(Decoder rangeDecoder, uint pos, byte prevByte) => this.m_Coders[this.GetState(pos, prevByte)].DecodeNormal(rangeDecoder);

                public byte DecodeWithMatchByte(Decoder rangeDecoder, uint pos, byte prevByte, byte matchByte) => this.m_Coders[this.GetState(pos, prevByte)].DecodeWithMatchByte(rangeDecoder, matchByte);

                struct Decoder2 {
					BitDecoder[] m_Decoders;

                    public void Create() => this.m_Decoders = new BitDecoder[0x300];

                    public void Init() {
						for (int i = 0; i < 0x300; i++) this.m_Decoders[i].Init();
					}

					public byte DecodeNormal(Decoder rangeDecoder) {
						uint symbol = 1;
						do
							symbol = (symbol << 1) | this.m_Decoders[symbol].Decode(rangeDecoder); while (symbol < 0x100);
						return (byte)symbol;
					}

					public byte DecodeWithMatchByte(Decoder rangeDecoder, byte matchByte) {
						uint symbol = 1;
						do {
							uint matchBit = (uint)(matchByte >> 7) & 1;
							matchByte <<= 1;
							uint bit = this.m_Decoders[((1 + matchBit) << 8) + symbol].Decode(rangeDecoder);
							symbol = (symbol << 1) | bit;
							if (matchBit != bit) {
								while (symbol < 0x100)
									symbol = (symbol << 1) | this.m_Decoders[symbol].Decode(rangeDecoder);
								break;
							}
						} while (symbol < 0x100);
						return (byte)symbol;
					}
				}
			};
		}

		class OutWindow {
			byte[] _buffer;
			uint _pos;
			Stream _stream;
			uint _streamPos;
			uint _windowSize;

			public void Create(uint windowSize) {
				if (this._windowSize != windowSize) {
                    this._buffer = new byte[windowSize];
				}
                this._windowSize = windowSize;
                this._pos = 0;
                this._streamPos = 0;
			}

			public void Init(Stream stream, bool solid) {
                this.ReleaseStream();
                this._stream = stream;
				if (!solid) {
                    this._streamPos = 0;
                    this._pos = 0;
				}
			}

			public void ReleaseStream() {
                this.Flush();
                this._stream = null;
				Buffer.BlockCopy(new byte[this._buffer.Length], 0, this._buffer, 0, this._buffer.Length);
			}

			public void Flush() {
				uint size = this._pos - this._streamPos;
				if (size == 0)
					return;
                this._stream.Write(this._buffer, (int)this._streamPos, (int)size);
				if (this._pos >= this._windowSize)
                    this._pos = 0;
                this._streamPos = this._pos;
			}

			public void CopyBlock(uint distance, uint len) {
				uint pos = this._pos - distance - 1;
				if (pos >= this._windowSize)
					pos += this._windowSize;
				for (; len > 0; len--) {
					if (pos >= this._windowSize)
						pos = 0;
                    this._buffer[this._pos++] = this._buffer[pos++];
					if (this._pos >= this._windowSize)
                        this.Flush();
				}
			}

			public void PutByte(byte b) {
                this._buffer[this._pos++] = b;
				if (this._pos >= this._windowSize)
                    this.Flush();
			}

			public byte GetByte(uint distance) {
				uint pos = this._pos - distance - 1;
				if (pos >= this._windowSize)
					pos += this._windowSize;
				return this._buffer[pos];
			}
		}

		struct State {
			public uint Index;

            public void Init() => this.Index = 0;

            public void UpdateChar() {
				if (this.Index < 4) this.Index = 0;
				else if (this.Index < 10) this.Index -= 3;
				else this.Index -= 6;
			}

            public void UpdateMatch() => this.Index = (uint)(this.Index < 7 ? 7 : 10);

            public void UpdateRep() => this.Index = (uint)(this.Index < 7 ? 8 : 11);

            public void UpdateShortRep() => this.Index = (uint)(this.Index < 7 ? 9 : 11);

            public bool IsCharState() => this.Index < 7;
        }
	}
}