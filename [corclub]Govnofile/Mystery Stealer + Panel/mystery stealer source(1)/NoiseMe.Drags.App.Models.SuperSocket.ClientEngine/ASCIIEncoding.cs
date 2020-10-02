using System.Collections.Generic;
using System.Text;

namespace NoiseMe.Drags.App.Models.SuperSocket.ClientEngine
{
	public class ASCIIEncoding : Encoding
	{
		private static ASCIIEncoding m_Instance;

		private char? fallbackCharacter;

		private static char[] byteToChar;

		private static Dictionary<char, byte> charToByte;

		public static ASCIIEncoding Instance => m_Instance;

		public override string WebName => "us-ascii";

		public char? FallbackCharacter
		{
			get
			{
				return fallbackCharacter;
			}
			set
			{
				fallbackCharacter = value;
				if (value.HasValue && !charToByte.ContainsKey(value.Value))
				{
					throw new EncoderFallbackException($"Cannot use the character [{value.Value}] (int value {(int)value.Value}) as fallback value - the fallback character itself is not supported by the encoding.");
				}
				FallbackByte = (value.HasValue ? new byte?(charToByte[value.Value]) : null);
			}
		}

		public byte? FallbackByte
		{
			get;
			private set;
		}

		public static int CharacterCount => byteToChar.Length;

		static ASCIIEncoding()
		{
			m_Instance = null;
			byteToChar = new char[128]
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
			charToByte = new Dictionary<char, byte>
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
			m_Instance = new ASCIIEncoding();
		}

		public override int GetHashCode()
		{
			return WebName.GetHashCode();
		}

		public ASCIIEncoding()
		{
			FallbackCharacter = '?';
		}

		public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			if (!FallbackByte.HasValue)
			{
				return GetBytesWithoutFallback(chars, charIndex, charCount, bytes, byteIndex);
			}
			return GetBytesWithFallBack(chars, charIndex, charCount, bytes, byteIndex);
		}

		private int GetBytesWithFallBack(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			for (int i = 0; i < charCount; i++)
			{
				char key = chars[i + charIndex];
				byte value;
				bool flag = charToByte.TryGetValue(key, out value);
				bytes[byteIndex + i] = (flag ? value : FallbackByte.Value);
			}
			return charCount;
		}

		private int GetBytesWithoutFallback(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			for (int i = 0; i < charCount; i++)
			{
				char c = chars[i + charIndex];
				if (!charToByte.TryGetValue(c, out byte value))
				{
					throw new EncoderFallbackException($"The encoding [{WebName}] cannot encode the character [{c}] (int value {(int)c}). Set the FallbackCharacter property in order to suppress this exception and encode a default character instead.");
				}
				bytes[byteIndex + i] = value;
			}
			return charCount;
		}

		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			if (!FallbackCharacter.HasValue)
			{
				return GetCharsWithoutFallback(bytes, byteIndex, byteCount, chars, charIndex);
			}
			return GetCharsWithFallback(bytes, byteIndex, byteCount, chars, charIndex);
		}

		private int GetCharsWithFallback(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			for (int i = 0; i < byteCount; i++)
			{
				byte b = bytes[i + byteIndex];
				char c = chars[charIndex + i] = ((b >= byteToChar.Length) ? FallbackCharacter.Value : byteToChar[b]);
			}
			return byteCount;
		}

		private int GetCharsWithoutFallback(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			for (int i = 0; i < byteCount; i++)
			{
				byte b = bytes[i + byteIndex];
				if (b >= byteToChar.Length)
				{
					throw new EncoderFallbackException($"The encoding [{WebName}] cannot decode byte value [{b}]. Set the FallbackCharacter property in order to suppress this exception and decode the value as a default character instead.");
				}
				chars[charIndex + i] = byteToChar[b];
			}
			return byteCount;
		}

		public override int GetByteCount(char[] chars, int index, int count)
		{
			return count;
		}

		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			return count;
		}

		public override int GetMaxByteCount(int charCount)
		{
			return charCount;
		}

		public override int GetMaxCharCount(int byteCount)
		{
			return byteCount;
		}
	}
}
