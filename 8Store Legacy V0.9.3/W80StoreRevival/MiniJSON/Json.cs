using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace MiniJSON
{
	// Token: 0x0200000B RID: 11
	public static class Json
	{
		// Token: 0x06000072 RID: 114 RVA: 0x0000ABCC File Offset: 0x00008DCC
		public static object Deserialize(string json)
		{
			object result;
			if (json == null)
			{
				result = null;
			}
			else
			{
				result = Json.Parser.Parse(json);
			}
			return result;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000ABF4 File Offset: 0x00008DF4
		public static string Serialize(object obj)
		{
			return Json.Serializer.Serialize(obj);
		}

		// Token: 0x0200000C RID: 12
		private sealed class Parser : IDisposable
		{
			// Token: 0x06000074 RID: 116 RVA: 0x0000AC0C File Offset: 0x00008E0C
			public static bool IsWordBreak(char c)
			{
				return char.IsWhiteSpace(c) || "{}[],:\"".IndexOf(c) != -1;
			}

			// Token: 0x06000075 RID: 117 RVA: 0x0000AC3C File Offset: 0x00008E3C
			public static bool IsHexDigit(char c)
			{
				return "0123456789ABCDEFabcdef".IndexOf(c) != -1;
			}

			// Token: 0x06000076 RID: 118 RVA: 0x0000AC5F File Offset: 0x00008E5F
			private Parser(string jsonString)
			{
				this.json = new StringReader(jsonString);
			}

			// Token: 0x06000077 RID: 119 RVA: 0x0000AC78 File Offset: 0x00008E78
			public static object Parse(string jsonString)
			{
				object result;
				using (Json.Parser parser = new Json.Parser(jsonString))
				{
					result = parser.ParseValue();
				}
				return result;
			}

			// Token: 0x06000078 RID: 120 RVA: 0x0000ACBC File Offset: 0x00008EBC
			public void Dispose()
			{
				this.json.Dispose();
				this.json = null;
			}

			// Token: 0x06000079 RID: 121 RVA: 0x0000ACD4 File Offset: 0x00008ED4
			private Dictionary<string, object> ParseObject()
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				this.json.Read();
				for (;;)
				{
					Json.Parser.TOKEN nextToken = this.NextToken;
					switch (nextToken)
					{
					case Json.Parser.TOKEN.NONE:
						goto IL_47;
					case Json.Parser.TOKEN.CURLY_OPEN:
						goto IL_C2;
					case Json.Parser.TOKEN.CURLY_CLOSE:
						goto IL_51;
					default:
						switch (nextToken)
						{
						case Json.Parser.TOKEN.COMMA:
							continue;
						case Json.Parser.TOKEN.STRING:
						{
							string text = this.ParseString();
							if (text == null)
							{
								goto Block_3;
							}
							if (this.NextToken != Json.Parser.TOKEN.COLON)
							{
								goto Block_4;
							}
							this.json.Read();
							Json.Parser.TOKEN nextToken2 = this.NextToken;
							object obj = this.ParseByToken(nextToken2);
							if (obj == null && nextToken2 != Json.Parser.TOKEN.NULL)
							{
								goto Block_6;
							}
							dictionary[text] = obj;
							continue;
						}
						}
						goto Block_2;
					}
				}
				Block_2:
				goto IL_C2;
				IL_47:
				return null;
				IL_51:
				return dictionary;
				Block_3:
				return null;
				Block_4:
				return null;
				Block_6:
				return null;
				IL_C2:
				return null;
			}

			// Token: 0x0600007A RID: 122 RVA: 0x0000ADB4 File Offset: 0x00008FB4
			private List<object> ParseArray()
			{
				List<object> list = new List<object>();
				this.json.Read();
				bool flag = true;
				while (flag)
				{
					Json.Parser.TOKEN nextToken = this.NextToken;
					Json.Parser.TOKEN token = nextToken;
					List<object> result;
					if (token != Json.Parser.TOKEN.NONE)
					{
						switch (token)
						{
						case Json.Parser.TOKEN.SQUARED_CLOSE:
							flag = false;
							break;
						case Json.Parser.TOKEN.COLON:
							goto IL_49;
						case Json.Parser.TOKEN.COMMA:
							break;
						default:
							goto IL_49;
						}
						continue;
						IL_49:
						object obj = this.ParseByToken(nextToken);
						if (obj != null || nextToken == Json.Parser.TOKEN.NULL)
						{
							list.Add(obj);
							continue;
						}
						result = null;
					}
					else
					{
						result = null;
					}
					return result;
				}
				return list;
			}

			// Token: 0x0600007B RID: 123 RVA: 0x0000AE44 File Offset: 0x00009044
			private object ParseValue()
			{
				Json.Parser.TOKEN nextToken = this.NextToken;
				return this.ParseByToken(nextToken);
			}

			// Token: 0x0600007C RID: 124 RVA: 0x0000AE64 File Offset: 0x00009064
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

			// Token: 0x0600007D RID: 125 RVA: 0x0000AEEC File Offset: 0x000090EC
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
					char c = nextChar;
					if (c != '"')
					{
						if (c != '\\')
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
							c = nextChar;
							if (c <= '\\')
							{
								if (c == '"' || c == '/' || c == '\\')
								{
									stringBuilder.Append(nextChar);
								}
							}
							else if (c <= 'f')
							{
								if (c != 'b')
								{
									if (c == 'f')
									{
										stringBuilder.Append('\f');
									}
								}
								else
								{
									stringBuilder.Append('\b');
								}
							}
							else if (c != 'n')
							{
								switch (c)
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

			// Token: 0x0600007E RID: 126 RVA: 0x0000B094 File Offset: 0x00009294
			private object ParseNumber()
			{
				string nextWord = this.NextWord;
				object result;
				if (nextWord.IndexOf('.') == -1 && nextWord.IndexOf('E') == -1 && nextWord.IndexOf('e') == -1)
				{
					long num;
					long.TryParse(nextWord, 511, CultureInfo.InvariantCulture, ref num);
					result = num;
				}
				else
				{
					double num2;
					double.TryParse(nextWord, 511, CultureInfo.InvariantCulture, ref num2);
					result = num2;
				}
				return result;
			}

			// Token: 0x0600007F RID: 127 RVA: 0x0000B114 File Offset: 0x00009314
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

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x06000080 RID: 128 RVA: 0x0000B15C File Offset: 0x0000935C
			private char PeekChar
			{
				get
				{
					return Convert.ToChar(this.json.Peek());
				}
			}

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000081 RID: 129 RVA: 0x0000B180 File Offset: 0x00009380
			private char NextChar
			{
				get
				{
					return Convert.ToChar(this.json.Read());
				}
			}

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x06000082 RID: 130 RVA: 0x0000B1A4 File Offset: 0x000093A4
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

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x06000083 RID: 131 RVA: 0x0000B200 File Offset: 0x00009400
			private Json.Parser.TOKEN NextToken
			{
				get
				{
					this.EatWhitespace();
					Json.Parser.TOKEN result;
					if (this.json.Peek() == -1)
					{
						result = Json.Parser.TOKEN.NONE;
					}
					else
					{
						char peekChar = this.PeekChar;
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
							switch (peekChar)
							{
							case '[':
								return Json.Parser.TOKEN.SQUARED_OPEN;
							case '\\':
								break;
							case ']':
								this.json.Read();
								return Json.Parser.TOKEN.SQUARED_CLOSE;
							default:
								switch (peekChar)
								{
								case '{':
									return Json.Parser.TOKEN.CURLY_OPEN;
								case '}':
									this.json.Read();
									return Json.Parser.TOKEN.CURLY_CLOSE;
								}
								break;
							}
							break;
						}
						string nextWord = this.NextWord;
						if (nextWord != null)
						{
							if (nextWord == "false")
							{
								return Json.Parser.TOKEN.FALSE;
							}
							if (nextWord == "true")
							{
								return Json.Parser.TOKEN.TRUE;
							}
							if (nextWord == "null")
							{
								return Json.Parser.TOKEN.NULL;
							}
						}
						result = Json.Parser.TOKEN.NONE;
					}
					return result;
				}
			}

			// Token: 0x0400004E RID: 78
			private const string WORD_BREAK = "{}[],:\"";

			// Token: 0x0400004F RID: 79
			private const string HEX_DIGIT = "0123456789ABCDEFabcdef";

			// Token: 0x04000050 RID: 80
			private StringReader json;

			// Token: 0x0200000D RID: 13
			private enum TOKEN
			{
				// Token: 0x04000052 RID: 82
				NONE,
				// Token: 0x04000053 RID: 83
				CURLY_OPEN,
				// Token: 0x04000054 RID: 84
				CURLY_CLOSE,
				// Token: 0x04000055 RID: 85
				SQUARED_OPEN,
				// Token: 0x04000056 RID: 86
				SQUARED_CLOSE,
				// Token: 0x04000057 RID: 87
				COLON,
				// Token: 0x04000058 RID: 88
				COMMA,
				// Token: 0x04000059 RID: 89
				STRING,
				// Token: 0x0400005A RID: 90
				NUMBER,
				// Token: 0x0400005B RID: 91
				TRUE,
				// Token: 0x0400005C RID: 92
				FALSE,
				// Token: 0x0400005D RID: 93
				NULL
			}
		}

		// Token: 0x0200000E RID: 14
		private sealed class Serializer
		{
			// Token: 0x06000084 RID: 132 RVA: 0x0000B360 File Offset: 0x00009560
			private Serializer()
			{
				this.builder = new StringBuilder();
			}

			// Token: 0x06000085 RID: 133 RVA: 0x0000B378 File Offset: 0x00009578
			public static string Serialize(object obj)
			{
				Json.Serializer serializer = new Json.Serializer();
				serializer.SerializeValue(obj);
				return serializer.builder.ToString();
			}

			// Token: 0x06000086 RID: 134 RVA: 0x0000B3A4 File Offset: 0x000095A4
			private void SerializeValue(object value)
			{
				string str;
				IList anArray;
				IDictionary obj;
				if (value == null)
				{
					this.builder.Append("null");
				}
				else if ((str = (value as string)) != null)
				{
					this.SerializeString(str);
				}
				else if (value is bool)
				{
					this.builder.Append(((bool)value) ? "true" : "false");
				}
				else if ((anArray = (value as IList)) != null)
				{
					this.SerializeArray(anArray);
				}
				else if ((obj = (value as IDictionary)) != null)
				{
					this.SerializeObject(obj);
				}
				else if (value is char)
				{
					this.SerializeString(new string((char)value, 1));
				}
				else
				{
					this.SerializeOther(value);
				}
			}

			// Token: 0x06000087 RID: 135 RVA: 0x0000B490 File Offset: 0x00009690
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

			// Token: 0x06000088 RID: 136 RVA: 0x0000B54C File Offset: 0x0000974C
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

			// Token: 0x06000089 RID: 137 RVA: 0x0000B5B8 File Offset: 0x000097B8
			private void SerializeString(string str)
			{
				this.builder.Append('"');
				char[] array = str.ToCharArray();
				int i = 0;
				while (i < array.Length)
				{
					char c = array[i];
					char c2 = c;
					switch (c2)
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
						goto IL_E8;
					case '\f':
						this.builder.Append("\\f");
						break;
					case '\r':
						this.builder.Append("\\r");
						break;
					default:
						if (c2 != '"')
						{
							if (c2 != '\\')
							{
								goto IL_E8;
							}
							this.builder.Append("\\\\");
						}
						else
						{
							this.builder.Append("\\\"");
						}
						break;
					}
					IL_141:
					i++;
					continue;
					IL_E8:
					int num = Convert.ToInt32(c);
					if (num >= 32 && num <= 126)
					{
						this.builder.Append(c);
					}
					else
					{
						this.builder.Append("\\u");
						this.builder.Append(num.ToString("x4"));
					}
					goto IL_141;
				}
				this.builder.Append('"');
			}

			// Token: 0x0600008A RID: 138 RVA: 0x0000B728 File Offset: 0x00009928
			private void SerializeOther(object value)
			{
				if (value is float)
				{
					this.builder.Append(((float)value).ToString("R", CultureInfo.InvariantCulture));
				}
				else if (value is int || value is uint || value is long || value is sbyte || value is byte || value is short || value is ushort || value is ulong)
				{
					this.builder.Append(value);
				}
				else if (value is double || value is decimal)
				{
					this.builder.Append(Convert.ToDouble(value).ToString("R", CultureInfo.InvariantCulture));
				}
				else
				{
					this.SerializeString(value.ToString());
				}
			}

			// Token: 0x0400005E RID: 94
			private StringBuilder builder;
		}
	}
}
