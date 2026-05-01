using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using Callisto.OAuth;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
using Windows.UI;

namespace Callisto
{
	// Token: 0x0200002D RID: 45
	internal static class Extensions
	{
		// Token: 0x0600020A RID: 522 RVA: 0x0000B20E File Offset: 0x0000940E
		public static bool Contains(this string s, string value, StringComparison comparison)
		{
			return s.IndexOf(value, comparison) >= 0;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000B220 File Offset: 0x00009420
		internal static IDictionary<string, object> ParseUrlQueryString(string query)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (string.IsNullOrEmpty(query) || query.Trim().Length == 0)
			{
				return dictionary;
			}
			int length = query.Length;
			int i = 0;
			bool flag = true;
			while (i <= length)
			{
				int num = -1;
				int num2 = -1;
				for (int j = i; j < length; j++)
				{
					if (num == -1 && query.get_Chars(j) == '=')
					{
						num = j + 1;
					}
					else if (query.get_Chars(j) == '&' || query.get_Chars(j) == '#')
					{
						num2 = j;
						break;
					}
				}
				if (flag)
				{
					flag = false;
					if (query.get_Chars(i) == '#')
					{
						i++;
					}
				}
				string text;
				if (num == -1)
				{
					text = null;
					num = i;
				}
				else
				{
					text = query.Substring(i, num - i - 1);
				}
				if (num2 < 0)
				{
					i = -1;
					num2 = query.Length;
				}
				else
				{
					i = num2 + 1;
				}
				string text2 = query.Substring(num, num2 - num);
				if (!string.IsNullOrEmpty(text))
				{
					dictionary[text] = text2;
				}
				if (i == -1)
				{
					break;
				}
			}
			return dictionary;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000B320 File Offset: 0x00009520
		public static string QueryString(this string query, string param)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			int length = query.Length;
			int i = 0;
			bool flag = true;
			while (i <= length)
			{
				int num = -1;
				int num2 = -1;
				for (int j = i; j < length; j++)
				{
					if (num == -1 && query.get_Chars(j) == '=')
					{
						num = j + 1;
					}
					else if (query.get_Chars(j) == '&' || query.get_Chars(j) == '#')
					{
						num2 = j;
						break;
					}
				}
				if (flag)
				{
					flag = false;
					if (query.get_Chars(i) == '#')
					{
						i++;
					}
				}
				string text;
				if (num == -1)
				{
					text = null;
					num = i;
				}
				else
				{
					text = query.Substring(i, num - i - 1);
				}
				if (num2 < 0)
				{
					i = -1;
					num2 = query.Length;
				}
				else
				{
					i = num2 + 1;
				}
				string text2 = query.Substring(num, num2 - num);
				if (!string.IsNullOrEmpty(text))
				{
					dictionary[text] = text2;
				}
				if (i == -1)
				{
					break;
				}
			}
			return dictionary[param].ToString();
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000B414 File Offset: 0x00009614
		public static string Then(this string input, string value)
		{
			return input + value;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000B41D File Offset: 0x0000961D
		public static string ToLower(this Enum type)
		{
			return type.ToString().ToLower();
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000B42A File Offset: 0x0000962A
		public static string ToUpper(this Enum type)
		{
			return type.ToString().ToUpper();
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000B438 File Offset: 0x00009638
		public static DateTime FromNow(this TimeSpan value)
		{
			return new DateTime((DateTime.Now + value).Ticks);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000B460 File Offset: 0x00009660
		public static DateTime FromUnixTime(this long seconds)
		{
			DateTime dateTime;
			dateTime..ctor(1970, 1, 1);
			dateTime = dateTime.AddSeconds((double)seconds);
			return dateTime.ToLocalTime();
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000B48C File Offset: 0x0000968C
		public static long ToUnixTime(this DateTime dateTime)
		{
			return (long)(dateTime - new DateTime(1970, 1, 1)).TotalSeconds;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000B4B6 File Offset: 0x000096B6
		public static byte[] GetBytes(this string input)
		{
			return Encoding.UTF8.GetBytes(input);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000B4C4 File Offset: 0x000096C4
		public static string PercentEncode(this string s)
		{
			byte[] bytes = s.GetBytes();
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in bytes)
			{
				if ((b > 7 && b < 11) || b == 13)
				{
					stringBuilder.Append(string.Format("%0{0:X}", new object[]
					{
						b
					}));
				}
				else
				{
					stringBuilder.Append(string.Format("%{0:X}", new object[]
					{
						b
					}));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000B557 File Offset: 0x00009757
		public static string FormatWith(this string format, params object[] args)
		{
			return string.Format(format, args);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000B560 File Offset: 0x00009760
		public static string FormatWithInvariantCulture(this string format, params object[] args)
		{
			return string.Format(CultureInfo.InvariantCulture, format, args);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000B56E File Offset: 0x0000976E
		public static bool IsNullOrBlank(this string value)
		{
			return string.IsNullOrEmpty(value) || (!string.IsNullOrEmpty(value) && value.Trim() == string.Empty);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000B594 File Offset: 0x00009794
		public static bool EqualsIgnoreCase(this string left, string right)
		{
			return string.Compare(left, right, 5) == 0;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000B5BC File Offset: 0x000097BC
		public static bool EqualsAny(this string input, params string[] args)
		{
			return Enumerable.Aggregate<string, bool>(args, false, (bool current, string arg) => current | input.Equals(arg));
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000B5EC File Offset: 0x000097EC
		public static IEnumerable<T> AsEnumerable<T>(this T item)
		{
			return new T[]
			{
				item
			};
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000B60C File Offset: 0x0000980C
		public static IEnumerable<T> And<T>(this T item, T other)
		{
			return new T[]
			{
				item,
				other
			};
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000B7EC File Offset: 0x000099EC
		public static IEnumerable<T> And<T>(this IEnumerable<T> items, T item)
		{
			foreach (T i in items)
			{
				yield return i;
			}
			yield return item;
			yield break;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000B810 File Offset: 0x00009A10
		public static K TryWithKey<T, K>(this IDictionary<T, K> dictionary, T key)
		{
			if (!dictionary.ContainsKey(key))
			{
				return default(K);
			}
			return dictionary[key];
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000B9C8 File Offset: 0x00009BC8
		public static IEnumerable<T> ToEnumerable<T>(this object[] items) where T : class
		{
			foreach (object item in items)
			{
				T record = item as T;
				yield return record;
			}
			yield break;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000B9E8 File Offset: 0x00009BE8
		public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
		{
			foreach (T t in items)
			{
				action.Invoke(t);
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000BA30 File Offset: 0x00009C30
		public static void AddRange(this IDictionary<string, string> collection, NameValueCollection range)
		{
			foreach (string text in range.AllKeys)
			{
				collection.Add(text, range[text]);
			}
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000BA84 File Offset: 0x00009C84
		public static string ToQueryString(this NameValueCollection collection)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (collection.Count > 0)
			{
				stringBuilder.Append("?");
			}
			int num = 0;
			foreach (string text in collection.AllKeys)
			{
				stringBuilder.AppendFormat("{0}={1}", new object[]
				{
					text,
					collection[text].UrlEncode()
				});
				num++;
				if (num < collection.Count)
				{
					stringBuilder.Append("&");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000BB34 File Offset: 0x00009D34
		public static string Concatenate(this WebParameterCollection collection, string separator, string spacer)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int count = collection.Count;
			int num = 0;
			foreach (WebPair webPair in collection)
			{
				stringBuilder.Append(webPair.Name);
				stringBuilder.Append(separator);
				stringBuilder.Append(webPair.Value);
				num++;
				if (num < count)
				{
					stringBuilder.Append(spacer);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000BBC4 File Offset: 0x00009DC4
		public static string UrlEncode(this string value)
		{
			return Uri.EscapeDataString(value);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000BBCC File Offset: 0x00009DCC
		public static string UrlDecode(this string value)
		{
			return Uri.UnescapeDataString(value);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000BBD4 File Offset: 0x00009DD4
		public static Uri AsUri(this string value)
		{
			return new Uri(value);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000BBDC File Offset: 0x00009DDC
		public static string ToRequestValue(this OAuthSignatureMethod signatureMethod)
		{
			string text = signatureMethod.ToString().ToUpper();
			int num = text.IndexOf("SHA1");
			if (num <= -1)
			{
				return text;
			}
			return text.Insert(num, "-");
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000BC18 File Offset: 0x00009E18
		public static OAuthSignatureMethod FromRequestValue(this string signatureMethod)
		{
			if (signatureMethod != null)
			{
				if (signatureMethod == "HMAC-SHA1")
				{
					return OAuthSignatureMethod.HmacSha1;
				}
				if (signatureMethod == "RSA-SHA1")
				{
					return OAuthSignatureMethod.RsaSha1;
				}
			}
			return OAuthSignatureMethod.PlainText;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000BC4C File Offset: 0x00009E4C
		public static string HashWith(this string input, MacAlgorithmProvider hashProvider, string key)
		{
			IBuffer buffer = CryptographicBuffer.ConvertStringToBinary(key, 0);
			CryptographicKey cryptographicKey = hashProvider.CreateKey(buffer);
			IBuffer buffer2 = CryptographicEngine.Sign(cryptographicKey, CryptographicBuffer.ConvertStringToBinary(input, 0));
			return CryptographicBuffer.EncodeToBase64String(buffer2);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000BC80 File Offset: 0x00009E80
		public static Color FromName(string colorName)
		{
			PropertyInfo runtimeProperty = RuntimeReflectionExtensions.GetRuntimeProperty(typeof(Colors), colorName.UpperFirst());
			if (runtimeProperty == null)
			{
				throw new ArgumentException("This is not a known color name.  Use a proper hex color number.");
			}
			return (Color)runtimeProperty.GetValue(null);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000BCC0 File Offset: 0x00009EC0
		public static string UpperFirst(this string nameValue)
		{
			if (string.IsNullOrEmpty(nameValue))
			{
				return string.Empty;
			}
			char[] array = nameValue.ToCharArray();
			array[0] = char.ToUpperInvariant(array[0]);
			return new string(array);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000BCF4 File Offset: 0x00009EF4
		public static Color ToColor(this string hexValue)
		{
			if (!hexValue.Contains("#"))
			{
				return Extensions.FromName(hexValue);
			}
			hexValue = hexValue.Replace("#", string.Empty);
			if (hexValue.Length < 6)
			{
				throw new ArgumentException("This does not appear to be a proper hex color number");
			}
			byte b = byte.MaxValue;
			int num = 0;
			if (hexValue.Length == 8)
			{
				b = byte.Parse(hexValue.Substring(0, 2), 515);
				num = 2;
			}
			byte b2 = byte.Parse(hexValue.Substring(num, 2), 515);
			byte b3 = byte.Parse(hexValue.Substring(num + 2, 2), 515);
			byte b4 = byte.Parse(hexValue.Substring(num + 4, 2), 515);
			return Color.FromArgb(b, b2, b3, b4);
		}
	}
}
