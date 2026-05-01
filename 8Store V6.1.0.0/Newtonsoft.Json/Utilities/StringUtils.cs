using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000078 RID: 120
	[NullableContext(1)]
	[Nullable(0)]
	internal static class StringUtils
	{
		// Token: 0x060005D7 RID: 1495 RVA: 0x000184FF File Offset: 0x000166FF
		[NullableContext(2)]
		public static bool IsNullOrEmpty([NotNullWhen(false)] string value)
		{
			return string.IsNullOrEmpty(value);
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00018507 File Offset: 0x00016707
		public static string FormatWith(this string format, IFormatProvider provider, [Nullable(2)] object arg0)
		{
			return format.FormatWith(provider, new object[]
			{
				arg0
			});
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0001851A File Offset: 0x0001671A
		public static string FormatWith(this string format, IFormatProvider provider, [Nullable(2)] object arg0, [Nullable(2)] object arg1)
		{
			return format.FormatWith(provider, new object[]
			{
				arg0,
				arg1
			});
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00018531 File Offset: 0x00016731
		public static string FormatWith(this string format, IFormatProvider provider, [Nullable(2)] object arg0, [Nullable(2)] object arg1, [Nullable(2)] object arg2)
		{
			return format.FormatWith(provider, new object[]
			{
				arg0,
				arg1,
				arg2
			});
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0001854D File Offset: 0x0001674D
		[NullableContext(2)]
		[return: Nullable(1)]
		public static string FormatWith([Nullable(1)] this string format, [Nullable(1)] IFormatProvider provider, object arg0, object arg1, object arg2, object arg3)
		{
			return format.FormatWith(provider, new object[]
			{
				arg0,
				arg1,
				arg2,
				arg3
			});
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x0001856E File Offset: 0x0001676E
		private static string FormatWith(this string format, IFormatProvider provider, [Nullable(new byte[]
		{
			1,
			2
		})] params object[] args)
		{
			ValidationUtils.ArgumentNotNull(format, "format");
			return string.Format(provider, format, args);
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00018584 File Offset: 0x00016784
		public static bool IsWhiteSpace(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (s.Length == 0)
			{
				return false;
			}
			for (int i = 0; i < s.Length; i++)
			{
				if (!char.IsWhiteSpace(s.get_Chars(i)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x000185CB File Offset: 0x000167CB
		public static StringWriter CreateStringWriter(int capacity)
		{
			return new StringWriter(new StringBuilder(capacity), CultureInfo.InvariantCulture);
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x000185E0 File Offset: 0x000167E0
		public static void ToCharAsUnicode(char c, char[] buffer)
		{
			buffer[0] = '\\';
			buffer[1] = 'u';
			buffer[2] = MathUtils.IntToHex((int)(c >> 12 & '\u000f'));
			buffer[3] = MathUtils.IntToHex((int)(c >> 8 & '\u000f'));
			buffer[4] = MathUtils.IntToHex((int)(c >> 4 & '\u000f'));
			buffer[5] = MathUtils.IntToHex((int)(c & '\u000f'));
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00018630 File Offset: 0x00016830
		[return: Nullable(2)]
		public static TSource ForgivingCaseSensitiveFind<[Nullable(2)] TSource>(this IEnumerable<TSource> source, Func<TSource, string> valueSelector, string testValue)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (valueSelector == null)
			{
				throw new ArgumentNullException("valueSelector");
			}
			IEnumerable<TSource> enumerable = Enumerable.Where<TSource>(source, (TSource s) => string.Equals(valueSelector.Invoke(s), testValue, 5));
			if (Enumerable.Count<TSource>(enumerable) <= 1)
			{
				return Enumerable.SingleOrDefault<TSource>(enumerable);
			}
			return Enumerable.SingleOrDefault<TSource>(Enumerable.Where<TSource>(source, (TSource s) => string.Equals(valueSelector.Invoke(s), testValue, 4)));
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x000186AC File Offset: 0x000168AC
		public static string ToCamelCase(string s)
		{
			if (StringUtils.IsNullOrEmpty(s) || !char.IsUpper(s.get_Chars(0)))
			{
				return s;
			}
			char[] array = s.ToCharArray();
			int num = 0;
			while (num < array.Length && (num != 1 || char.IsUpper(array[num])))
			{
				bool flag = num + 1 < array.Length;
				if (num > 0 && flag && !char.IsUpper(array[num + 1]))
				{
					if (char.IsSeparator(array[num + 1]))
					{
						array[num] = StringUtils.ToLower(array[num]);
						break;
					}
					break;
				}
				else
				{
					array[num] = StringUtils.ToLower(array[num]);
					num++;
				}
			}
			return new string(array);
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0001873B File Offset: 0x0001693B
		private static char ToLower(char c)
		{
			c = char.ToLowerInvariant(c);
			return c;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00018746 File Offset: 0x00016946
		public static string ToSnakeCase(string s)
		{
			return StringUtils.ToSeparatedCase(s, '_');
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00018750 File Offset: 0x00016950
		public static string ToKebabCase(string s)
		{
			return StringUtils.ToSeparatedCase(s, '-');
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x0001875C File Offset: 0x0001695C
		private static string ToSeparatedCase(string s, char separator)
		{
			if (StringUtils.IsNullOrEmpty(s))
			{
				return s;
			}
			StringBuilder stringBuilder = new StringBuilder();
			StringUtils.SeparatedCaseState separatedCaseState = StringUtils.SeparatedCaseState.Start;
			for (int i = 0; i < s.Length; i++)
			{
				if (s.get_Chars(i) == ' ')
				{
					if (separatedCaseState != StringUtils.SeparatedCaseState.Start)
					{
						separatedCaseState = StringUtils.SeparatedCaseState.NewWord;
					}
				}
				else if (char.IsUpper(s.get_Chars(i)))
				{
					switch (separatedCaseState)
					{
					case StringUtils.SeparatedCaseState.Lower:
					case StringUtils.SeparatedCaseState.NewWord:
						stringBuilder.Append(separator);
						break;
					case StringUtils.SeparatedCaseState.Upper:
					{
						bool flag = i + 1 < s.Length;
						if (i > 0 && flag)
						{
							char c = s.get_Chars(i + 1);
							if (!char.IsUpper(c) && c != separator)
							{
								stringBuilder.Append(separator);
							}
						}
						break;
					}
					}
					char c2 = char.ToLowerInvariant(s.get_Chars(i));
					stringBuilder.Append(c2);
					separatedCaseState = StringUtils.SeparatedCaseState.Upper;
				}
				else if (s.get_Chars(i) == separator)
				{
					stringBuilder.Append(separator);
					separatedCaseState = StringUtils.SeparatedCaseState.Start;
				}
				else
				{
					if (separatedCaseState == StringUtils.SeparatedCaseState.NewWord)
					{
						stringBuilder.Append(separator);
					}
					stringBuilder.Append(s.get_Chars(i));
					separatedCaseState = StringUtils.SeparatedCaseState.Lower;
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00018860 File Offset: 0x00016A60
		public static bool IsHighSurrogate(char c)
		{
			return char.IsHighSurrogate(c);
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00018868 File Offset: 0x00016A68
		public static bool IsLowSurrogate(char c)
		{
			return char.IsLowSurrogate(c);
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00018870 File Offset: 0x00016A70
		public static int IndexOf(string s, char c)
		{
			return s.IndexOf(c);
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00018879 File Offset: 0x00016A79
		public static string Replace(string s, string oldValue, string newValue)
		{
			return s.Replace(oldValue, newValue);
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00018883 File Offset: 0x00016A83
		public static bool StartsWith(this string source, char value)
		{
			return source.Length > 0 && source.get_Chars(0) == value;
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0001889A File Offset: 0x00016A9A
		public static bool EndsWith(this string source, char value)
		{
			return source.Length > 0 && source.get_Chars(source.Length - 1) == value;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x000188B8 File Offset: 0x00016AB8
		public static string Trim(this string s, int start, int length)
		{
			if (s == null)
			{
				throw new ArgumentNullException();
			}
			if (start < 0)
			{
				throw new ArgumentOutOfRangeException("start");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			int num = start + length - 1;
			if (num >= s.Length)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			while (start < num)
			{
				if (!char.IsWhiteSpace(s.get_Chars(start)))
				{
					IL_6C:
					while (num >= start && char.IsWhiteSpace(s.get_Chars(num)))
					{
						num--;
					}
					return s.Substring(start, num - start + 1);
				}
				start++;
			}
			goto IL_6C;
		}

		// Token: 0x04000262 RID: 610
		public const string CarriageReturnLineFeed = "\r\n";

		// Token: 0x04000263 RID: 611
		public const string Empty = "";

		// Token: 0x04000264 RID: 612
		public const char CarriageReturn = '\r';

		// Token: 0x04000265 RID: 613
		public const char LineFeed = '\n';

		// Token: 0x04000266 RID: 614
		public const char Tab = '\t';

		// Token: 0x0200019B RID: 411
		[NullableContext(0)]
		private enum SeparatedCaseState
		{
			// Token: 0x0400073F RID: 1855
			Start,
			// Token: 0x04000740 RID: 1856
			Lower,
			// Token: 0x04000741 RID: 1857
			Upper,
			// Token: 0x04000742 RID: 1858
			NewWord
		}
	}
}
