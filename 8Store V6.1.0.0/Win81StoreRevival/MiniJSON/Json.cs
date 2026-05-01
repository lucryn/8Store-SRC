using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace MiniJSON
{
	// Token: 0x0200000C RID: 12
	public static class Json
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00002E24 File Offset: 0x00001024
		public static object Deserialize(string json)
		{
			if (json == null)
			{
				return null;
			}
			return Json.Parser.Parse(json);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002E31 File Offset: 0x00001031
		public static string Serialize(object obj)
		{
			return Json.Serializer.Serialize(obj);
		}

		// Token: 0x02000069 RID: 105
		private sealed class Parser : IDisposable
		{
			// Token: 0x06000693 RID: 1683 RVA: 0x0001F0D8 File Offset: 0x0001D2D8
			public static bool IsWordBreak(char c)
			{
				return char.IsWhiteSpace(c) || "{}[],:\"".IndexOf(c) != -1;
			}

			// Token: 0x06000694 RID: 1684 RVA: 0x0001F0F5 File Offset: 0x0001D2F5
			public static bool IsHexDigit(char c)
			{
				return "0123456789ABCDEFabcdef".IndexOf(c) != -1;
			}

			// Token: 0x06000695 RID: 1685 RVA: 0x0001F108 File Offset: 0x0001D308
			private Parser(string jsonString)
			{
				this.json = new StringReader(jsonString);
			}

			// Token: 0x06000696 RID: 1686 RVA: 0x0001F11C File Offset: 0x0001D31C
			public static object Parse(string jsonString)
			{
				object result;
				using (Json.Parser parser = new Json.Parser(jsonString))
				{
					result = parser.ParseValue();
				}
				return result;
			}

			// Token: 0x06000697 RID: 1687 RVA: 0x0001F154 File Offset: 0x0001D354
			public void Dispose()
			{
				this.json.Dispose();
				this.json = null;
			}

			// Token: 0x06000698 RID: 1688 RVA: 0x0001F168 File Offset: 0x0001D368
			private Dictionary<string, object> ParseObject()
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				this.json.Read();
				Json.Parser.TOKEN nextToken;
				for (;;)
				{
					nextToken = this.NextToken;
					if (nextToken <= Json.Parser.TOKEN.CURLY_CLOSE)
					{
						break;
					}
					if (nextToken != Json.Parser.TOKEN.COMMA)
					{
						if (nextToken != Json.Parser.TOKEN.STRING)
						{
							goto Block_5;
						}
						string text = this.ParseString();
						if (text == null)
						{
							goto Block_6;
						}
						if (this.NextToken != Json.Parser.TOKEN.COLON)
						{
							goto Block_7;
						}
						this.json.Read();
						Json.Parser.TOKEN nextToken2 = this.NextToken;
						object obj = this.ParseByToken(nextToken2);
						if (obj == null && nextToken2 != Json.Parser.TOKEN.NULL)
						{
							goto Block_9;
						}
						dictionary[text] = obj;
					}
				}
				if (nextToken == Json.Parser.TOKEN.NONE)
				{
					return null;
				}
				if (nextToken == Json.Parser.TOKEN.CURLY_CLOSE)
				{
					return dictionary;
				}
				Block_5:
				goto IL_7D;
				Block_6:
				return null;
				Block_7:
				return null;
				Block_9:
				return null;
				IL_7D:
				return null;
			}

			// Token: 0x06000699 RID: 1689 RVA: 0x0001F1F4 File Offset: 0x0001D3F4
			private List<object> ParseArray()
			{
				List<object> list = new List<object>();
				this.json.Read();
				bool flag = true;
				while (flag)
				{
					Json.Parser.TOKEN nextToken = this.NextToken;
					if (nextToken == Json.Parser.TOKEN.NONE)
					{
						return null;
					}
					if (nextToken != Json.Parser.TOKEN.SQUARED_CLOSE)
					{
						if (nextToken != Json.Parser.TOKEN.COMMA)
						{
							object obj = this.ParseByToken(nextToken);
							if (obj == null && nextToken != Json.Parser.TOKEN.NULL)
							{
								return null;
							}
							list.Add(obj);
						}
					}
					else
					{
						flag = false;
					}
				}
				return list;
			}

			// Token: 0x0600069A RID: 1690 RVA: 0x0001F250 File Offset: 0x0001D450
			private object ParseValue()
			{
				Json.Parser.TOKEN nextToken = this.NextToken;
				return this.ParseByToken(nextToken);
			}

			// Token: 0x0600069B RID: 1691 RVA: 0x0001F26C File Offset: 0x0001D46C
			private object ParseByToken(Json.Parser.TOKEN token)
			{
				switch (token)
				{
				case Json.Parser.TOKEN.CURLY_OPEN:
					return this.ParseObject();
				case Json.Parser.TOKEN.SQUARED_OPEN:
					return this.ParseArray();
				case Json.Parser.TOKEN.STRING:
					return this.ParseString();
				case Json.Parser.TOKEN.NUMBER:
					return this.ParseNumber();
				case Json.Parser.TOKEN.TRUE:
					return true;
				case Json.Parser.TOKEN.FALSE:
					return false;
				case Json.Parser.TOKEN.NULL:
					return null;
				}
				return null;
			}

			// Token: 0x0600069C RID: 1692 RVA: 0x0001F2DC File Offset: 0x0001D4DC
			private string ParseString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				this.json.Read();
				bool flag = true;
				while (flag)
				{
					if (this.json.Peek() == -1)
					{
						break;
					}
					char nextChar = this.NextChar;
					if (nextChar != '"')
					{
						if (nextChar != '\\')
						{
							stringBuilder.Append(nextChar);
						}
						else if (this.json.Peek() == -1)
						{
							flag = false;
						}
						else
						{
							nextChar = this.NextChar;
							if (nextChar <= '\\')
							{
								if (nextChar == '"' || nextChar == '/' || nextChar == '\\')
								{
									stringBuilder.Append(nextChar);
								}
							}
							else if (nextChar <= 'f')
							{
								if (nextChar != 'b')
								{
									if (nextChar == 'f')
									{
										stringBuilder.Append('\f');
									}
								}
								else
								{
									stringBuilder.Append('\b');
								}
							}
							else if (nextChar != 'n')
							{
								switch (nextChar)
								{
								case 'r':
									stringBuilder.Append('\r');
									break;
								case 't':
									stringBuilder.Append('\t');
									break;
								case 'u':
								{
									char[] array = new char[4];
									for (int i = 0; i < 4; i++)
									{
										array[i] = this.NextChar;
										if (!Json.Parser.IsHexDigit(array[i]))
										{
											return null;
										}
									}
									stringBuilder.Append((char)Convert.ToInt32(new string(array), 16));
									break;
								}
								}
							}
							else
							{
								stringBuilder.Append('\n');
							}
						}
					}
					else
					{
						flag = false;
					}
				}
				return stringBuilder.ToString();
			}

			// Token: 0x0600069D RID: 1693 RVA: 0x0001F43C File Offset: 0x0001D63C
			private object ParseNumber()
			{
				string nextWord = this.NextWord;
				if (nextWord.IndexOf('.') == -1 && nextWord.IndexOf('E') == -1 && nextWord.IndexOf('e') == -1)
				{
					long num;
					long.TryParse(nextWord, 511, CultureInfo.InvariantCulture, ref num);
					return num;
				}
				double num2;
				double.TryParse(nextWord, 511, CultureInfo.InvariantCulture, ref num2);
				return num2;
			}

			// Token: 0x0600069E RID: 1694 RVA: 0x0001F4A4 File Offset: 0x0001D6A4
			private void EatWhitespace()
			{
				while (char.IsWhiteSpace(this.PeekChar))
				{
					this.json.Read();
					if (this.json.Peek() == -1)
					{
						break;
					}
				}
			}

			// Token: 0x1700013B RID: 315
			// (get) Token: 0x0600069F RID: 1695 RVA: 0x0001F4CF File Offset: 0x0001D6CF
			private char PeekChar
			{
				get
				{
					return Convert.ToChar(this.json.Peek());
				}
			}

			// Token: 0x1700013C RID: 316
			// (get) Token: 0x060006A0 RID: 1696 RVA: 0x0001F4E1 File Offset: 0x0001D6E1
			private char NextChar
			{
				get
				{
					return Convert.ToChar(this.json.Read());
				}
			}

			// Token: 0x1700013D RID: 317
			// (get) Token: 0x060006A1 RID: 1697 RVA: 0x0001F4F4 File Offset: 0x0001D6F4
			private string NextWord
			{
				get
				{
					StringBuilder stringBuilder = new StringBuilder();
					while (!Json.Parser.IsWordBreak(this.PeekChar))
					{
						stringBuilder.Append(this.NextChar);
						if (this.json.Peek() == -1)
						{
							break;
						}
					}
					return stringBuilder.ToString();
				}
			}

			// Token: 0x1700013E RID: 318
			// (get) Token: 0x060006A2 RID: 1698 RVA: 0x0001F538 File Offset: 0x0001D738
			private Json.Parser.TOKEN NextToken
			{
				get
				{
					this.EatWhitespace();
					if (this.json.Peek() == -1)
					{
						return Json.Parser.TOKEN.NONE;
					}
					char peekChar = this.PeekChar;
					if (peekChar <= '[')
					{
						switch (peekChar)
						{
						case '"':
							return Json.Parser.TOKEN.STRING;
						case '#':
						case '$':
						case '%':
						case '&':
						case '\'':
						case '(':
						case ')':
						case '*':
						case '+':
						case '.':
						case '/':
							break;
						case ',':
							this.json.Read();
							return Json.Parser.TOKEN.COMMA;
						case '-':
						case '0':
						case '1':
						case '2':
						case '3':
						case '4':
						case '5':
						case '6':
						case '7':
						case '8':
						case '9':
							return Json.Parser.TOKEN.NUMBER;
						case ':':
							return Json.Parser.TOKEN.COLON;
						default:
							if (peekChar == '[')
							{
								return Json.Parser.TOKEN.SQUARED_OPEN;
							}
							break;
						}
					}
					else
					{
						if (peekChar == ']')
						{
							this.json.Read();
							return Json.Parser.TOKEN.SQUARED_CLOSE;
						}
						if (peekChar == '{')
						{
							return Json.Parser.TOKEN.CURLY_OPEN;
						}
						if (peekChar == '}')
						{
							this.json.Read();
							return Json.Parser.TOKEN.CURLY_CLOSE;
						}
					}
					string nextWord = this.NextWord;
					if (nextWord == "false")
					{
						return Json.Parser.TOKEN.FALSE;
					}
					if (nextWord == "true")
					{
						return Json.Parser.TOKEN.TRUE;
					}
					if (!(nextWord == "null"))
					{
						return Json.Parser.TOKEN.NONE;
					}
					return Json.Parser.TOKEN.NULL;
				}
			}

			// Token: 0x040002A0 RID: 672
			private const string WORD_BREAK = "{}[],:\"";

			// Token: 0x040002A1 RID: 673
			private const string HEX_DIGIT = "0123456789ABCDEFabcdef";

			// Token: 0x040002A2 RID: 674
			private StringReader json;

			// Token: 0x02000179 RID: 377
			private enum TOKEN
			{
				// Token: 0x040007B7 RID: 1975
				NONE,
				// Token: 0x040007B8 RID: 1976
				CURLY_OPEN,
				// Token: 0x040007B9 RID: 1977
				CURLY_CLOSE,
				// Token: 0x040007BA RID: 1978
				SQUARED_OPEN,
				// Token: 0x040007BB RID: 1979
				SQUARED_CLOSE,
				// Token: 0x040007BC RID: 1980
				COLON,
				// Token: 0x040007BD RID: 1981
				COMMA,
				// Token: 0x040007BE RID: 1982
				STRING,
				// Token: 0x040007BF RID: 1983
				NUMBER,
				// Token: 0x040007C0 RID: 1984
				TRUE,
				// Token: 0x040007C1 RID: 1985
				FALSE,
				// Token: 0x040007C2 RID: 1986
				NULL
			}
		}

		// Token: 0x0200006A RID: 106
		private sealed class Serializer
		{
			// Token: 0x060006A3 RID: 1699 RVA: 0x0001F65A File Offset: 0x0001D85A
			private Serializer()
			{
				this.builder = new StringBuilder();
			}

			// Token: 0x060006A4 RID: 1700 RVA: 0x0001F66D File Offset: 0x0001D86D
			public static string Serialize(object obj)
			{
				Json.Serializer serializer = new Json.Serializer();
				serializer.SerializeValue(obj);
				return serializer.builder.ToString();
			}

			// Token: 0x060006A5 RID: 1701 RVA: 0x0001F688 File Offset: 0x0001D888
			private void SerializeValue(object value)
			{
				if (value == null)
				{
					this.builder.Append("null");
					return;
				}
				string str;
				if ((str = (value as string)) != null)
				{
					this.SerializeString(str);
					return;
				}
				if (value is bool)
				{
					this.builder.Append(((bool)value) ? "true" : "false");
					return;
				}
				IList anArray;
				if ((anArray = (value as IList)) != null)
				{
					this.SerializeArray(anArray);
					return;
				}
				IDictionary obj;
				if ((obj = (value as IDictionary)) != null)
				{
					this.SerializeObject(obj);
					return;
				}
				if (value is char)
				{
					this.SerializeString(new string((char)value, 1));
					return;
				}
				this.SerializeOther(value);
			}

			// Token: 0x060006A6 RID: 1702 RVA: 0x0001F72C File Offset: 0x0001D92C
			private void SerializeObject(IDictionary obj)
			{
				bool flag = true;
				this.builder.Append('{');
				foreach (object obj2 in obj.Keys)
				{
					if (!flag)
					{
						this.builder.Append(',');
					}
					this.SerializeString(obj2.ToString());
					this.builder.Append(':');
					this.SerializeValue(obj[obj2]);
					flag = false;
				}
				this.builder.Append('}');
			}

			// Token: 0x060006A7 RID: 1703 RVA: 0x0001F7D4 File Offset: 0x0001D9D4
			private void SerializeArray(IList anArray)
			{
				this.builder.Append('[');
				bool flag = true;
				for (int i = 0; i < anArray.Count; i++)
				{
					object value = anArray[i];
					if (!flag)
					{
						this.builder.Append(',');
					}
					this.SerializeValue(value);
					flag = false;
				}
				this.builder.Append(']');
			}

			// Token: 0x060006A8 RID: 1704 RVA: 0x0001F834 File Offset: 0x0001DA34
			private void SerializeString(string str)
			{
				this.builder.Append('"');
				char[] array = str.ToCharArray();
				int i = 0;
				while (i < array.Length)
				{
					char c = array[i];
					switch (c)
					{
					case '\b':
						this.builder.Append("\\b");
						break;
					case '\t':
						this.builder.Append("\\t");
						break;
					case '\n':
						this.builder.Append("\\n");
						break;
					case '\v':
						goto IL_E0;
					case '\f':
						this.builder.Append("\\f");
						break;
					case '\r':
						this.builder.Append("\\r");
						break;
					default:
						if (c != '"')
						{
							if (c != '\\')
							{
								goto IL_E0;
							}
							this.builder.Append("\\\\");
						}
						else
						{
							this.builder.Append("\\\"");
						}
						break;
					}
					IL_129:
					i++;
					continue;
					IL_E0:
					int num = Convert.ToInt32(c);
					if (num >= 32 && num <= 126)
					{
						this.builder.Append(c);
						goto IL_129;
					}
					this.builder.Append("\\u");
					this.builder.Append(num.ToString("x4"));
					goto IL_129;
				}
				this.builder.Append('"');
			}

			// Token: 0x060006A9 RID: 1705 RVA: 0x0001F988 File Offset: 0x0001DB88
			private void SerializeOther(object value)
			{
				if (value is float)
				{
					this.builder.Append(((float)value).ToString("R", CultureInfo.InvariantCulture));
					return;
				}
				if (value is int || value is uint || value is long || value is sbyte || value is byte || value is short || value is ushort || value is ulong)
				{
					this.builder.Append(value);
					return;
				}
				if (value is double || value is decimal)
				{
					this.builder.Append(Convert.ToDouble(value).ToString("R", CultureInfo.InvariantCulture));
					return;
				}
				this.SerializeString(value.ToString());
			}

			// Token: 0x040002A3 RID: 675
			private StringBuilder builder;
		}
	}
}
