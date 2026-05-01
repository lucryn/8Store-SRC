using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000D6 RID: 214
	internal static class StringUtils
	{
		// Token: 0x06000A1D RID: 2589 RVA: 0x00028118 File Offset: 0x00026318
		public static string FormatWith(this string format, IFormatProvider provider, object arg0)
		{
			return format.FormatWith(provider, new object[]
			{
				arg0
			});
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x00028138 File Offset: 0x00026338
		public static string FormatWith(this string format, IFormatProvider provider, object arg0, object arg1)
		{
			return format.FormatWith(provider, new object[]
			{
				arg0,
				arg1
			});
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0002815C File Offset: 0x0002635C
		public static string FormatWith(this string format, IFormatProvider provider, object arg0, object arg1, object arg2)
		{
			return format.FormatWith(provider, new object[]
			{
				arg0,
				arg1,
				arg2
			});
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x00028185 File Offset: 0x00026385
		public static string FormatWith(this string format, IFormatProvider provider, params object[] args)
		{
			ValidationUtils.ArgumentNotNull(format, "format");
			return string.Format(provider, format, args);
		}

		/// <summary>
		/// Determines whether the string is all white space. Empty string will return false.
		/// </summary>
		/// <param name="s">The string to test whether it is all white space.</param>
		/// <returns>
		/// 	<c>true</c> if the string is all white space; otherwise, <c>false</c>.
		/// </returns>
		// Token: 0x06000A21 RID: 2593 RVA: 0x0002819C File Offset: 0x0002639C
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

		/// <summary>
		/// Nulls an empty string.
		/// </summary>
		/// <param name="s">The string.</param>
		/// <returns>Null if the string was null, otherwise the string unchanged.</returns>
		// Token: 0x06000A22 RID: 2594 RVA: 0x000281E3 File Offset: 0x000263E3
		public static string NullEmptyString(string s)
		{
			if (!string.IsNullOrEmpty(s))
			{
				return s;
			}
			return null;
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x000281F0 File Offset: 0x000263F0
		public static StringWriter CreateStringWriter(int capacity)
		{
			StringBuilder stringBuilder = new StringBuilder(capacity);
			return new StringWriter(stringBuilder, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x00028214 File Offset: 0x00026414
		public static int? GetLength(string value)
		{
			if (value == null)
			{
				return default(int?);
			}
			return new int?(value.Length);
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0002823C File Offset: 0x0002643C
		public static void ToCharAsUnicode(char c, char[] buffer)
		{
			buffer[0] = '\\';
			buffer[1] = 'u';
			buffer[2] = MathUtils.IntToHex((int)(c >> 12 & '\u000f'));
			buffer[3] = MathUtils.IntToHex((int)(c >> 8 & '\u000f'));
			buffer[4] = MathUtils.IntToHex((int)(c >> 4 & '\u000f'));
			buffer[5] = MathUtils.IntToHex((int)(c & '\u000f'));
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x000282C8 File Offset: 0x000264C8
		public static TSource ForgivingCaseSensitiveFind<TSource>(this IEnumerable<TSource> source, Func<TSource, string> valueSelector, string testValue)
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
			IEnumerable<TSource> enumerable2 = Enumerable.Where<TSource>(source, (TSource s) => string.Equals(valueSelector.Invoke(s), testValue, 4));
			return Enumerable.SingleOrDefault<TSource>(enumerable2);
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x00028350 File Offset: 0x00026550
		public static string ToCamelCase(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return s;
			}
			if (!char.IsUpper(s.get_Chars(0)))
			{
				return s;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < s.Length; i++)
			{
				bool flag = i + 1 < s.Length;
				if (i != 0 && flag && !char.IsUpper(s.get_Chars(i + 1)))
				{
					stringBuilder.Append(s.Substring(i));
					break;
				}
				char c = char.ToLower(s.get_Chars(i));
				stringBuilder.Append(c);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x000283DD File Offset: 0x000265DD
		public static bool IsHighSurrogate(char c)
		{
			return char.IsHighSurrogate(c);
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x000283E5 File Offset: 0x000265E5
		public static bool IsLowSurrogate(char c)
		{
			return char.IsLowSurrogate(c);
		}

		// Token: 0x040003DC RID: 988
		public const string CarriageReturnLineFeed = "\r\n";

		// Token: 0x040003DD RID: 989
		public const string Empty = "";

		// Token: 0x040003DE RID: 990
		public const char CarriageReturn = '\r';

		// Token: 0x040003DF RID: 991
		public const char LineFeed = '\n';

		// Token: 0x040003E0 RID: 992
		public const char Tab = '\t';
	}
}
