using System;
using System.Collections.Generic;
using System.Text;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine
{
	// Token: 0x02000102 RID: 258
	public class ASCIIEncoding : Encoding
	{
		// Token: 0x060007C6 RID: 1990 RVA: 0x0001C2CC File Offset: 0x0001A4CC
		static ASCIIEncoding()
		{
			ASCIIEncoding.m_Instance = new ASCIIEncoding();
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060007C7 RID: 1991 RVA: 0x000066B8 File Offset: 0x000048B8
		public static ASCIIEncoding Instance
		{
			get
			{
				return ASCIIEncoding.m_Instance;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x000066BF File Offset: 0x000048BF
		public override string WebName
		{
			get
			{
				return "us-ascii";
			}
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x000066C6 File Offset: 0x000048C6
		public override int GetHashCode()
		{
			return this.WebName.GetHashCode();
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x000066D3 File Offset: 0x000048D3
		// (set) Token: 0x060007CB RID: 1995 RVA: 0x0001C7FC File Offset: 0x0001A9FC
		public char? FallbackCharacter
		{
			get
			{
				return this.fallbackCharacter;
			}
			set
			{
				this.fallbackCharacter = value;
				if (value != null && !ASCIIEncoding.charToByte.ContainsKey(value.Value))
				{
					throw new EncoderFallbackException(string.Format("Cannot use the character [{0}] (int value {1}) as fallback value - the fallback character itself is not supported by the encoding.", value.Value, (int)value.Value));
				}
				this.FallbackByte = ((value != null) ? new byte?(ASCIIEncoding.charToByte[value.Value]) : null);
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x000066DB File Offset: 0x000048DB
		// (set) Token: 0x060007CD RID: 1997 RVA: 0x000066E3 File Offset: 0x000048E3
		public byte? FallbackByte { get; private set; }

		// Token: 0x060007CE RID: 1998 RVA: 0x000066EC File Offset: 0x000048EC
		public ASCIIEncoding()
		{
			this.FallbackCharacter = new char?('?');
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0001C884 File Offset: 0x0001AA84
		public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			if (this.FallbackByte == null)
			{
				return this.GetBytesWithoutFallback(chars, charIndex, charCount, bytes, byteIndex);
			}
			return this.GetBytesWithFallBack(chars, charIndex, charCount, bytes, byteIndex);
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0001C8BC File Offset: 0x0001AABC
		private int GetBytesWithFallBack(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			for (int i = 0; i < charCount; i++)
			{
				char key = chars[i + charIndex];
				byte b;
				bool flag = ASCIIEncoding.charToByte.TryGetValue(key, out b);
				bytes[byteIndex + i] = (flag ? b : this.FallbackByte.Value);
			}
			return charCount;
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0001C908 File Offset: 0x0001AB08
		private int GetBytesWithoutFallback(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			for (int i = 0; i < charCount; i++)
			{
				char c = chars[i + charIndex];
				byte b;
				if (!ASCIIEncoding.charToByte.TryGetValue(c, out b))
				{
					throw new EncoderFallbackException(string.Format("The encoding [{0}] cannot encode the character [{1}] (int value {2}). Set the FallbackCharacter property in order to suppress this exception and encode a default character instead.", this.WebName, c, (int)c));
				}
				bytes[byteIndex + i] = b;
			}
			return charCount;
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0001C964 File Offset: 0x0001AB64
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			if (this.FallbackCharacter == null)
			{
				return this.GetCharsWithoutFallback(bytes, byteIndex, byteCount, chars, charIndex);
			}
			return this.GetCharsWithFallback(bytes, byteIndex, byteCount, chars, charIndex);
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0001C99C File Offset: 0x0001AB9C
		private int GetCharsWithFallback(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			for (int i = 0; i < byteCount; i++)
			{
				byte b = bytes[i + byteIndex];
				char c = ((int)b >= ASCIIEncoding.byteToChar.Length) ? this.FallbackCharacter.Value : ASCIIEncoding.byteToChar[(int)b];
				chars[charIndex + i] = c;
			}
			return byteCount;
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x0001C9E8 File Offset: 0x0001ABE8
		private int GetCharsWithoutFallback(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			for (int i = 0; i < byteCount; i++)
			{
				byte b = bytes[i + byteIndex];
				if ((int)b >= ASCIIEncoding.byteToChar.Length)
				{
					throw new EncoderFallbackException(string.Format("The encoding [{0}] cannot decode byte value [{1}]. Set the FallbackCharacter property in order to suppress this exception and decode the value as a default character instead.", this.WebName, b));
				}
				chars[charIndex + i] = ASCIIEncoding.byteToChar[(int)b];
			}
			return byteCount;
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00006701 File Offset: 0x00004901
		public override int GetByteCount(char[] chars, int index, int count)
		{
			return count;
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x00006701 File Offset: 0x00004901
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			return count;
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0000450E File Offset: 0x0000270E
		public override int GetMaxByteCount(int charCount)
		{
			return charCount;
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0000450E File Offset: 0x0000270E
		public override int GetMaxCharCount(int byteCount)
		{
			return byteCount;
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060007D9 RID: 2009 RVA: 0x00006704 File Offset: 0x00004904
		public static int CharacterCount
		{
			get
			{
				return ASCIIEncoding.byteToChar.Length;
			}
		}

		// Token: 0x04000321 RID: 801
		private static ASCIIEncoding m_Instance = null;

		// Token: 0x04000322 RID: 802
		private char? fallbackCharacter;

		// Token: 0x04000324 RID: 804
		private static char[] byteToChar = new char[]
		{
			'\0',
			'\u0001',
			'\u0002',
			'\u0003',
			'\u0004',
			'\u0005',
			'\u0006',
			'\a',
			'\b',
			'\t',
			'\n',
			'\v',
			'\f',
			'\r',
			'\u000e',
			'\u000f',
			'\u0010',
			'\u0011',
			'\u0012',
			'\u0013',
			'\u0014',
			'\u0015',
			'\u0016',
			'\u0017',
			'\u0018',
			'\u0019',
			'\u001a',
			'\u001b',
			'\u001c',
			'\u001d',
			'\u001e',
			'\u001f',
			' ',
			'!',
			'"',
			'#',
			'$',
			'%',
			'&',
			'\'',
			'(',
			')',
			'*',
			'+',
			',',
			'-',
			'.',
			'/',
			'0',
			'1',
			'2',
			'3',
			'4',
			'5',
			'6',
			'7',
			'8',
			'9',
			':',
			';',
			'<',
			'=',
			'>',
			'?',
			'@',
			'A',
			'B',
			'C',
			'D',
			'E',
			'F',
			'G',
			'H',
			'I',
			'J',
			'K',
			'L',
			'M',
			'N',
			'O',
			'P',
			'Q',
			'R',
			'S',
			'T',
			'U',
			'V',
			'W',
			'X',
			'Y',
			'Z',
			'[',
			'\\',
			']',
			'^',
			'_',
			'`',
			'a',
			'b',
			'c',
			'd',
			'e',
			'f',
			'g',
			'h',
			'i',
			'j',
			'k',
			'l',
			'm',
			'n',
			'o',
			'p',
			'q',
			'r',
			's',
			't',
			'u',
			'v',
			'w',
			'x',
			'y',
			'z',
			'{',
			'|',
			'}',
			'~',
			'\u007f'
		};

		// Token: 0x04000325 RID: 805
		private static Dictionary<char, byte> charToByte = new Dictionary<char, byte>
		{
			{
				'\0',
				0
			},
			{
				'\u0001',
				1
			},
			{
				'\u0002',
				2
			},
			{
				'\u0003',
				3
			},
			{
				'\u0004',
				4
			},
			{
				'\u0005',
				5
			},
			{
				'\u0006',
				6
			},
			{
				'\a',
				7
			},
			{
				'\b',
				8
			},
			{
				'\t',
				9
			},
			{
				'\n',
				10
			},
			{
				'\v',
				11
			},
			{
				'\f',
				12
			},
			{
				'\r',
				13
			},
			{
				'\u000e',
				14
			},
			{
				'\u000f',
				15
			},
			{
				'\u0010',
				16
			},
			{
				'\u0011',
				17
			},
			{
				'\u0012',
				18
			},
			{
				'\u0013',
				19
			},
			{
				'\u0014',
				20
			},
			{
				'\u0015',
				21
			},
			{
				'\u0016',
				22
			},
			{
				'\u0017',
				23
			},
			{
				'\u0018',
				24
			},
			{
				'\u0019',
				25
			},
			{
				'\u001a',
				26
			},
			{
				'\u001b',
				27
			},
			{
				'\u001c',
				28
			},
			{
				'\u001d',
				29
			},
			{
				'\u001e',
				30
			},
			{
				'\u001f',
				31
			},
			{
				' ',
				32
			},
			{
				'!',
				33
			},
			{
				'"',
				34
			},
			{
				'#',
				35
			},
			{
				'$',
				36
			},
			{
				'%',
				37
			},
			{
				'&',
				38
			},
			{
				'\'',
				39
			},
			{
				'(',
				40
			},
			{
				')',
				41
			},
			{
				'*',
				42
			},
			{
				'+',
				43
			},
			{
				',',
				44
			},
			{
				'-',
				45
			},
			{
				'.',
				46
			},
			{
				'/',
				47
			},
			{
				'0',
				48
			},
			{
				'1',
				49
			},
			{
				'2',
				50
			},
			{
				'3',
				51
			},
			{
				'4',
				52
			},
			{
				'5',
				53
			},
			{
				'6',
				54
			},
			{
				'7',
				55
			},
			{
				'8',
				56
			},
			{
				'9',
				57
			},
			{
				':',
				58
			},
			{
				';',
				59
			},
			{
				'<',
				60
			},
			{
				'=',
				61
			},
			{
				'>',
				62
			},
			{
				'?',
				63
			},
			{
				'@',
				64
			},
			{
				'A',
				65
			},
			{
				'B',
				66
			},
			{
				'C',
				67
			},
			{
				'D',
				68
			},
			{
				'E',
				69
			},
			{
				'F',
				70
			},
			{
				'G',
				71
			},
			{
				'H',
				72
			},
			{
				'I',
				73
			},
			{
				'J',
				74
			},
			{
				'K',
				75
			},
			{
				'L',
				76
			},
			{
				'M',
				77
			},
			{
				'N',
				78
			},
			{
				'O',
				79
			},
			{
				'P',
				80
			},
			{
				'Q',
				81
			},
			{
				'R',
				82
			},
			{
				'S',
				83
			},
			{
				'T',
				84
			},
			{
				'U',
				85
			},
			{
				'V',
				86
			},
			{
				'W',
				87
			},
			{
				'X',
				88
			},
			{
				'Y',
				89
			},
			{
				'Z',
				90
			},
			{
				'[',
				91
			},
			{
				'\\',
				92
			},
			{
				']',
				93
			},
			{
				'^',
				94
			},
			{
				'_',
				95
			},
			{
				'`',
				96
			},
			{
				'a',
				97
			},
			{
				'b',
				98
			},
			{
				'c',
				99
			},
			{
				'd',
				100
			},
			{
				'e',
				101
			},
			{
				'f',
				102
			},
			{
				'g',
				103
			},
			{
				'h',
				104
			},
			{
				'i',
				105
			},
			{
				'j',
				106
			},
			{
				'k',
				107
			},
			{
				'l',
				108
			},
			{
				'm',
				109
			},
			{
				'n',
				110
			},
			{
				'o',
				111
			},
			{
				'p',
				112
			},
			{
				'q',
				113
			},
			{
				'r',
				114
			},
			{
				's',
				115
			},
			{
				't',
				116
			},
			{
				'u',
				117
			},
			{
				'v',
				118
			},
			{
				'w',
				119
			},
			{
				'x',
				120
			},
			{
				'y',
				121
			},
			{
				'z',
				122
			},
			{
				'{',
				123
			},
			{
				'|',
				124
			},
			{
				'}',
				125
			},
			{
				'~',
				126
			},
			{
				'\u007f',
				127
			}
		};
	}
}
