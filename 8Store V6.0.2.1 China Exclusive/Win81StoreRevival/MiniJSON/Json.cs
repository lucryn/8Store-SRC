using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace MiniJSON
{
	// Token: 0x0200000A RID: 10
	public static class Json
	{
		// Token: 0x06000035 RID: 53 RVA: 0x00002C44 File Offset: 0x00000E44
		public static object Deserialize(string json)
		{
			bool flag = json == null;
			object result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = Json.Parser.Parse(json);
			}
			return result;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002C6C File Offset: 0x00000E6C
		public static string Serialize(object obj)
		{
			return Json.Serializer.Serialize(obj);
		}

		// Token: 0x0200005A RID: 90
		private sealed class Parser : IDisposable
		{
			// Token: 0x0600050C RID: 1292 RVA: 0x0001C124 File Offset: 0x0001A324
			public static bool IsWordBreak(char c)
			{
				return char.IsWhiteSpace(c) || "{}[],:\"".IndexOf(c) != -1;
			}

			// Token: 0x0600050D RID: 1293 RVA: 0x0001C154 File Offset: 0x0001A354
			public static bool IsHexDigit(char c)
			{
				return "0123456789ABCDEFabcdef".IndexOf(c) != -1;
			}

			// Token: 0x0600050E RID: 1294 RVA: 0x0001C177 File Offset: 0x0001A377
			private Parser(string jsonString)
			{
				this.json = new StringReader(jsonString);
			}

			// Token: 0x0600050F RID: 1295 RVA: 0x0001C190 File Offset: 0x0001A390
			public static object Parse(string jsonString)
			{
				object result;
				using (Json.Parser parser = new Json.Parser(jsonString))
				{
					result = parser.ParseValue();
				}
				return result;
			}

			// Token: 0x06000510 RID: 1296 RVA: 0x0001C1CC File Offset: 0x0001A3CC
			public void Dispose()
			{
				this.json.Dispose();
				this.json = null;
			}

			// Token: 0x06000511 RID: 1297 RVA: 0x0001C1E4 File Offset: 0x0001A3E4
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
						bool flag = text == null;
						if (flag)
						{
							goto Block_6;
						}
						bool flag2 = this.NextToken != Json.Parser.TOKEN.COLON;
						if (flag2)
						{
							goto Block_7;
						}
						this.json.Read();
						Json.Parser.TOKEN nextToken2 = this.NextToken;
						object obj = this.ParseByToken(nextToken2);
						bool flag3 = obj == null && nextToken2 != Json.Parser.TOKEN.NULL;
						if (flag3)
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
				goto IL_C1;
				Block_6:
				return null;
				Block_7:
				return null;
				Block_9:
				return null;
				IL_C1:
				return null;
			}

			// Token: 0x06000512 RID: 1298 RVA: 0x0001C2C4 File Offset: 0x0001A4C4
			private List<object> ParseArray()
			{
				List<object> list = new List<object>();
				this.json.Read();
				bool flag = true;
				while (flag)
				{
					Json.Parser.TOKEN nextToken = this.NextToken;
					Json.Parser.TOKEN token = nextToken;
					if (token != Json.Parser.TOKEN.NONE)
					{
						if (token != Json.Parser.TOKEN.SQUARED_CLOSE)
						{
							if (token != Json.Parser.TOKEN.COMMA)
							{
								object obj = this.ParseByToken(nextToken);
								bool flag2 = obj == null && nextToken != Json.Parser.TOKEN.NULL;
								if (flag2)
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
						continue;
					}
					return null;
				}
				return list;
			}

			// Token: 0x06000513 RID: 1299 RVA: 0x0001C34C File Offset: 0x0001A54C
			private object ParseValue()
			{
				Json.Parser.TOKEN nextToken = this.NextToken;
				return this.ParseByToken(nextToken);
			}

			// Token: 0x06000514 RID: 1300 RVA: 0x0001C36C File Offset: 0x0001A56C
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

			// Token: 0x06000515 RID: 1301 RVA: 0x0001C3F4 File Offset: 0x0001A5F4
			private string ParseString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				this.json.Read();
				bool flag = true;
				while (flag)
				{
					bool flag2 = this.json.Peek() == -1;
					if (flag2)
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
						else
						{
							bool flag3 = this.json.Peek() == -1;
							if (flag3)
							{
								flag = false;
							}
							else
							{
								nextChar = this.NextChar;
								char c2 = nextChar;
								if (c2 <= '\\')
								{
									if (c2 == '"' || c2 == '/' || c2 == '\\')
									{
										stringBuilder.Append(nextChar);
									}
								}
								else if (c2 <= 'f')
								{
									if (c2 != 'b')
									{
										if (c2 == 'f')
										{
											stringBuilder.Append('\f');
										}
									}
									else
									{
										stringBuilder.Append('\b');
									}
								}
								else if (c2 != 'n')
								{
									switch (c2)
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
											bool flag4 = !Json.Parser.IsHexDigit(array[i]);
											if (flag4)
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
					}
					else
					{
						flag = false;
					}
				}
				return stringBuilder.ToString();
			}

			// Token: 0x06000516 RID: 1302 RVA: 0x0001C5A8 File Offset: 0x0001A7A8
			private object ParseNumber()
			{
				string nextWord = this.NextWord;
				bool flag = nextWord.IndexOf('.') == -1 && nextWord.IndexOf('E') == -1 && nextWord.IndexOf('e') == -1;
				object result;
				if (flag)
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

			// Token: 0x06000517 RID: 1303 RVA: 0x0001C624 File Offset: 0x0001A824
			private void EatWhitespace()
			{
				while (char.IsWhiteSpace(this.PeekChar))
				{
					this.json.Read();
					bool flag = this.json.Peek() == -1;
					if (flag)
					{
						break;
					}
				}
			}

			// Token: 0x170000F9 RID: 249
			// (get) Token: 0x06000518 RID: 1304 RVA: 0x0001C668 File Offset: 0x0001A868
			private char PeekChar
			{
				get
				{
					return Convert.ToChar(this.json.Peek());
				}
			}

			// Token: 0x170000FA RID: 250
			// (get) Token: 0x06000519 RID: 1305 RVA: 0x0001C68C File Offset: 0x0001A88C
			private char NextChar
			{
				get
				{
					return Convert.ToChar(this.json.Read());
				}
			}

			// Token: 0x170000FB RID: 251
			// (get) Token: 0x0600051A RID: 1306 RVA: 0x0001C6B0 File Offset: 0x0001A8B0
			private string NextWord
			{
				get
				{
					StringBuilder stringBuilder = new StringBuilder();
					while (!Json.Parser.IsWordBreak(this.PeekChar))
					{
						stringBuilder.Append(this.NextChar);
						bool flag = this.json.Peek() == -1;
						if (flag)
						{
							break;
						}
					}
					return stringBuilder.ToString();
				}
			}

			// Token: 0x170000FC RID: 252
			// (get) Token: 0x0600051B RID: 1307 RVA: 0x0001C708 File Offset: 0x0001A908
			private Json.Parser.TOKEN NextToken
			{
				get
				{
					this.EatWhitespace();
					bool flag = this.json.Peek() == -1;
					Json.Parser.TOKEN result;
					if (flag)
					{
						result = Json.Parser.TOKEN.NONE;
					}
					else
					{
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
						if (!(nextWord == "false"))
						{
							if (!(nextWord == "true"))
							{
								if (!(nextWord == "null"))
								{
									result = Json.Parser.TOKEN.NONE;
								}
								else
								{
									result = Json.Parser.TOKEN.NULL;
								}
							}
							else
							{
								result = Json.Parser.TOKEN.TRUE;
							}
						}
						else
						{
							result = Json.Parser.TOKEN.FALSE;
						}
					}
					return result;
				}
			}

			// Token: 0x04000236 RID: 566
			private const string WORD_BREAK = "{}[],:\"";

			// Token: 0x04000237 RID: 567
			private const string HEX_DIGIT = "0123456789ABCDEFabcdef";

			// Token: 0x04000238 RID: 568
			private StringReader json;

			// Token: 0x02000159 RID: 345
			private enum TOKEN
			{
				// Token: 0x040009F6 RID: 2550
				NONE,
				// Token: 0x040009F7 RID: 2551
				CURLY_OPEN,
				// Token: 0x040009F8 RID: 2552
				CURLY_CLOSE,
				// Token: 0x040009F9 RID: 2553
				SQUARED_OPEN,
				// Token: 0x040009FA RID: 2554
				SQUARED_CLOSE,
				// Token: 0x040009FB RID: 2555
				COLON,
				// Token: 0x040009FC RID: 2556
				COMMA,
				// Token: 0x040009FD RID: 2557
				STRING,
				// Token: 0x040009FE RID: 2558
				NUMBER,
				// Token: 0x040009FF RID: 2559
				TRUE,
				// Token: 0x04000A00 RID: 2560
				FALSE,
				// Token: 0x04000A01 RID: 2561
				NULL
			}
		}

		// Token: 0x0200005B RID: 91
		private sealed class Serializer
		{
			// Token: 0x0600051C RID: 1308 RVA: 0x0001C859 File Offset: 0x0001AA59
			private Serializer()
			{
				this.builder = new StringBuilder();
			}

			// Token: 0x0600051D RID: 1309 RVA: 0x0001C870 File Offset: 0x0001AA70
			public static string Serialize(object obj)
			{
				Json.Serializer serializer = new Json.Serializer();
				serializer.SerializeValue(obj);
				return serializer.builder.ToString();
			}

			// Token: 0x0600051E RID: 1310 RVA: 0x0001C89C File Offset: 0x0001AA9C
			private void SerializeValue(object value)
			{
				bool flag = value == null;
				if (flag)
				{
					this.builder.Append("null");
				}
				else
				{
					string str;
					bool flag2 = (str = (value as string)) != null;
					if (flag2)
					{
						this.SerializeString(str);
					}
					else
					{
						bool flag3 = value is bool;
						if (flag3)
						{
							this.builder.Append(((bool)value) ? "true" : "false");
						}
						else
						{
							IList anArray;
							bool flag4 = (anArray = (value as IList)) != null;
							if (flag4)
							{
								this.SerializeArray(anArray);
							}
							else
							{
								IDictionary obj;
								bool flag5 = (obj = (value as IDictionary)) != null;
								if (flag5)
								{
									this.SerializeObject(obj);
								}
								else
								{
									bool flag6 = value is char;
									if (flag6)
									{
										this.SerializeString(new string((char)value, 1));
									}
									else
									{
										this.SerializeOther(value);
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x0600051F RID: 1311 RVA: 0x0001C988 File Offset: 0x0001AB88
			private void SerializeObject(IDictionary obj)
			{
				bool flag = true;
				this.builder.Append('{');
				foreach (object obj2 in obj.Keys)
				{
					bool flag2 = !flag;
					if (flag2)
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

			// Token: 0x06000520 RID: 1312 RVA: 0x0001CA40 File Offset: 0x0001AC40
			private void SerializeArray(IList anArray)
			{
				this.builder.Append('[');
				bool flag = true;
				for (int i = 0; i < anArray.Count; i++)
				{
					object value = anArray[i];
					bool flag2 = !flag;
					if (flag2)
					{
						this.builder.Append(',');
					}
					this.SerializeValue(value);
					flag = false;
				}
				this.builder.Append(']');
			}

			// Token: 0x06000521 RID: 1313 RVA: 0x0001CAB0 File Offset: 0x0001ACB0
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
						goto IL_EB;
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
								goto IL_EB;
							}
							this.builder.Append("\\\\");
						}
						else
						{
							this.builder.Append("\\\"");
						}
						break;
					}
					IL_149:
					i++;
					continue;
					IL_EB:
					int num = Convert.ToInt32(c);
					bool flag = num >= 32 && num <= 126;
					if (flag)
					{
						this.builder.Append(c);
					}
					else
					{
						this.builder.Append("\\u");
						this.builder.Append(num.ToString("x4"));
					}
					goto IL_149;
				}
				this.builder.Append('"');
			}

			// Token: 0x06000522 RID: 1314 RVA: 0x0001CC28 File Offset: 0x0001AE28
			private void SerializeOther(object value)
			{
				bool flag = value is float;
				if (flag)
				{
					this.builder.Append(((float)value).ToString("R", CultureInfo.InvariantCulture));
				}
				else
				{
					bool flag2 = value is int || value is uint || value is long || value is sbyte || value is byte || value is short || value is ushort || value is ulong;
					if (flag2)
					{
						this.builder.Append(value);
					}
					else
					{
						bool flag3 = value is double || value is decimal;
						if (flag3)
						{
							this.builder.Append(Convert.ToDouble(value).ToString("R", CultureInfo.InvariantCulture));
						}
						else
						{
							this.SerializeString(value.ToString());
						}
					}
				}
			}

			// Token: 0x04000239 RID: 569
			private StringBuilder builder;
		}
	}
}
