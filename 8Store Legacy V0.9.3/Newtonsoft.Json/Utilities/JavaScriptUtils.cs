using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000CB RID: 203
	internal static class JavaScriptUtils
	{
		// Token: 0x060009BB RID: 2491 RVA: 0x0002649C File Offset: 0x0002469C
		static JavaScriptUtils()
		{
			List<char> list = new List<char>();
			list.Add('\n');
			list.Add('\r');
			list.Add('\t');
			list.Add('\\');
			list.Add('\f');
			list.Add('\b');
			IList<char> list2 = list;
			for (int i = 0; i < 32; i++)
			{
				list2.Add((char)i);
			}
			foreach (char c in Enumerable.Union<char>(list2, new char[]
			{
				'\''
			}))
			{
				JavaScriptUtils.SingleQuoteCharEscapeFlags[(int)c] = true;
			}
			foreach (char c2 in Enumerable.Union<char>(list2, new char[]
			{
				'"'
			}))
			{
				JavaScriptUtils.DoubleQuoteCharEscapeFlags[(int)c2] = true;
			}
			foreach (char c3 in Enumerable.Union<char>(list2, new char[]
			{
				'"',
				'\'',
				'<',
				'>',
				'&'
			}))
			{
				JavaScriptUtils.HtmlCharEscapeFlags[(int)c3] = true;
			}
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x00026628 File Offset: 0x00024828
		public static void WriteEscapedJavaScriptString(TextWriter writer, string s, char delimiter, bool appendDelimiters, bool[] charEscapeFlags, StringEscapeHandling stringEscapeHandling, ref char[] writeBuffer)
		{
			if (appendDelimiters)
			{
				writer.Write(delimiter);
			}
			if (s != null)
			{
				int num = 0;
				for (int i = 0; i < s.Length; i++)
				{
					char c = s.get_Chars(i);
					if ((int)c >= charEscapeFlags.Length || charEscapeFlags[(int)c])
					{
						char c2 = c;
						string text;
						if (c2 <= '\\')
						{
							switch (c2)
							{
							case '\b':
								text = "\\b";
								break;
							case '\t':
								text = "\\t";
								break;
							case '\n':
								text = "\\n";
								break;
							case '\v':
								goto IL_D4;
							case '\f':
								text = "\\f";
								break;
							case '\r':
								text = "\\r";
								break;
							default:
								if (c2 != '\\')
								{
									goto IL_D4;
								}
								text = "\\\\";
								break;
							}
						}
						else if (c2 != '\u0085')
						{
							switch (c2)
							{
							case '\u2028':
								text = "\\u2028";
								break;
							case '\u2029':
								text = "\\u2029";
								break;
							default:
								goto IL_D4;
							}
						}
						else
						{
							text = "\\u0085";
						}
						IL_125:
						if (text == null)
						{
							goto IL_1BC;
						}
						bool flag = string.Equals(text, "!");
						if (i > num)
						{
							int num2 = i - num + (flag ? 6 : 0);
							int num3 = flag ? 6 : 0;
							if (writeBuffer == null || writeBuffer.Length < num2)
							{
								char[] array = new char[num2];
								if (flag)
								{
									Array.Copy(writeBuffer, array, 6);
								}
								writeBuffer = array;
							}
							s.CopyTo(num, writeBuffer, num3, num2 - num3);
							writer.Write(writeBuffer, num3, num2 - num3);
						}
						num = i + 1;
						if (!flag)
						{
							writer.Write(text);
							goto IL_1BC;
						}
						writer.Write(writeBuffer, 0, 6);
						goto IL_1BC;
						IL_D4:
						if ((int)c >= charEscapeFlags.Length && stringEscapeHandling != StringEscapeHandling.EscapeNonAscii)
						{
							text = null;
							goto IL_125;
						}
						if (c == '\'' && stringEscapeHandling != StringEscapeHandling.EscapeHtml)
						{
							text = "\\'";
							goto IL_125;
						}
						if (c == '"' && stringEscapeHandling != StringEscapeHandling.EscapeHtml)
						{
							text = "\\\"";
							goto IL_125;
						}
						if (writeBuffer == null)
						{
							writeBuffer = new char[6];
						}
						StringUtils.ToCharAsUnicode(c, writeBuffer);
						text = "!";
						goto IL_125;
					}
					IL_1BC:;
				}
				if (num == 0)
				{
					writer.Write(s);
				}
				else
				{
					int num4 = s.Length - num;
					if (writeBuffer == null || writeBuffer.Length < num4)
					{
						writeBuffer = new char[num4];
					}
					s.CopyTo(num, writeBuffer, 0, num4);
					writer.Write(writeBuffer, 0, num4);
				}
			}
			if (appendDelimiters)
			{
				writer.Write(delimiter);
			}
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x00026854 File Offset: 0x00024A54
		public static string ToEscapedJavaScriptString(string value, char delimiter, bool appendDelimiters)
		{
			string result;
			using (StringWriter stringWriter = StringUtils.CreateStringWriter(StringUtils.GetLength(value) ?? 16))
			{
				char[] array = null;
				JavaScriptUtils.WriteEscapedJavaScriptString(stringWriter, value, delimiter, appendDelimiters, (delimiter == '"') ? JavaScriptUtils.DoubleQuoteCharEscapeFlags : JavaScriptUtils.SingleQuoteCharEscapeFlags, StringEscapeHandling.Default, ref array);
				result = stringWriter.ToString();
			}
			return result;
		}

		// Token: 0x040003B1 RID: 945
		private const string EscapedUnicodeText = "!";

		// Token: 0x040003B2 RID: 946
		internal static readonly bool[] SingleQuoteCharEscapeFlags = new bool[128];

		// Token: 0x040003B3 RID: 947
		internal static readonly bool[] DoubleQuoteCharEscapeFlags = new bool[128];

		// Token: 0x040003B4 RID: 948
		internal static readonly bool[] HtmlCharEscapeFlags = new bool[128];
	}
}
