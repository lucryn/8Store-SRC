using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Numerics;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	/// <summary>
	/// Represents a reader that provides fast, non-cached, forward-only access to JSON text data.
	/// </summary>
	// Token: 0x0200004D RID: 77
	public class JsonTextReader : JsonReader, IJsonLineInfo
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.JsonReader" /> class with the specified <see cref="T:System.IO.TextReader" />.
		/// </summary>
		/// <param name="reader">The <c>TextReader</c> containing the XML data to read.</param>
		// Token: 0x06000329 RID: 809 RVA: 0x0000B33A File Offset: 0x0000953A
		public JsonTextReader(TextReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this._reader = reader;
			this._lineNumber = 1;
			this._chars = new char[1025];
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000B36E File Offset: 0x0000956E
		private StringBuffer GetBuffer()
		{
			if (this._buffer == null)
			{
				this._buffer = new StringBuffer(1025);
			}
			else
			{
				this._buffer.Position = 0;
			}
			return this._buffer;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000B39C File Offset: 0x0000959C
		private void OnNewLine(int pos)
		{
			this._lineNumber++;
			this._lineStartPos = pos - 1;
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000B3B8 File Offset: 0x000095B8
		private void ParseString(char quote)
		{
			this._charPos++;
			this.ShiftBufferIfNeeded();
			this.ReadStringIntoBuffer(quote);
			if (this._readType == ReadType.ReadAsBytes)
			{
				byte[] value;
				if (this._stringReference.Length == 0)
				{
					value = new byte[0];
				}
				else
				{
					value = Convert.FromBase64CharArray(this._stringReference.Chars, this._stringReference.StartIndex, this._stringReference.Length);
				}
				base.SetToken(JsonToken.Bytes, value);
				return;
			}
			if (this._readType == ReadType.ReadAsString)
			{
				string value2 = this._stringReference.ToString();
				base.SetToken(JsonToken.String, value2);
				this._quoteChar = quote;
				return;
			}
			string text = this._stringReference.ToString();
			if (this._dateParseHandling != DateParseHandling.None)
			{
				DateParseHandling dateParseHandling;
				if (this._readType == ReadType.ReadAsDateTime)
				{
					dateParseHandling = DateParseHandling.DateTime;
				}
				else if (this._readType == ReadType.ReadAsDateTimeOffset)
				{
					dateParseHandling = DateParseHandling.DateTimeOffset;
				}
				else
				{
					dateParseHandling = this._dateParseHandling;
				}
				object value3;
				if (DateTimeUtils.TryParseDateTime(text, dateParseHandling, base.DateTimeZoneHandling, out value3))
				{
					base.SetToken(JsonToken.Date, value3);
					return;
				}
			}
			base.SetToken(JsonToken.String, text);
			this._quoteChar = quote;
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000B4C3 File Offset: 0x000096C3
		private static void BlockCopyChars(char[] src, int srcOffset, char[] dst, int dstOffset, int count)
		{
			Buffer.BlockCopy(src, srcOffset * 2, dst, dstOffset * 2, count * 2);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000B4D8 File Offset: 0x000096D8
		private void ShiftBufferIfNeeded()
		{
			int num = this._chars.Length;
			if ((double)(num - this._charPos) <= (double)num * 0.1)
			{
				int num2 = this._charsUsed - this._charPos;
				if (num2 > 0)
				{
					JsonTextReader.BlockCopyChars(this._chars, this._charPos, this._chars, 0, num2);
				}
				this._lineStartPos -= this._charPos;
				this._charPos = 0;
				this._charsUsed = num2;
				this._chars[this._charsUsed] = '\0';
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000B55F File Offset: 0x0000975F
		private int ReadData(bool append)
		{
			return this.ReadData(append, 0);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000B56C File Offset: 0x0000976C
		private int ReadData(bool append, int charsRequired)
		{
			if (this._isEndOfFile)
			{
				return 0;
			}
			if (this._charsUsed + charsRequired >= this._chars.Length - 1)
			{
				if (append)
				{
					int num = Math.Max(this._chars.Length * 2, this._charsUsed + charsRequired + 1);
					char[] array = new char[num];
					JsonTextReader.BlockCopyChars(this._chars, 0, array, 0, this._chars.Length);
					this._chars = array;
				}
				else
				{
					int num2 = this._charsUsed - this._charPos;
					if (num2 + charsRequired + 1 >= this._chars.Length)
					{
						char[] array2 = new char[num2 + charsRequired + 1];
						if (num2 > 0)
						{
							JsonTextReader.BlockCopyChars(this._chars, this._charPos, array2, 0, num2);
						}
						this._chars = array2;
					}
					else if (num2 > 0)
					{
						JsonTextReader.BlockCopyChars(this._chars, this._charPos, this._chars, 0, num2);
					}
					this._lineStartPos -= this._charPos;
					this._charPos = 0;
					this._charsUsed = num2;
				}
			}
			int num3 = this._chars.Length - this._charsUsed - 1;
			int num4 = this._reader.Read(this._chars, this._charsUsed, num3);
			this._charsUsed += num4;
			if (num4 == 0)
			{
				this._isEndOfFile = true;
			}
			this._chars[this._charsUsed] = '\0';
			return num4;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000B6BF File Offset: 0x000098BF
		private bool EnsureChars(int relativePosition, bool append)
		{
			return this._charPos + relativePosition < this._charsUsed || this.ReadChars(relativePosition, append);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000B6DC File Offset: 0x000098DC
		private bool ReadChars(int relativePosition, bool append)
		{
			if (this._isEndOfFile)
			{
				return false;
			}
			int num = this._charPos + relativePosition - this._charsUsed + 1;
			int num2 = 0;
			do
			{
				int num3 = this.ReadData(append, num - num2);
				if (num3 == 0)
				{
					break;
				}
				num2 += num3;
			}
			while (num2 < num);
			return num2 >= num;
		}

		/// <summary>
		/// Reads the next JSON token from the stream.
		/// </summary>
		/// <returns>
		/// true if the next token was read successfully; false if there are no more tokens to read.
		/// </returns>
		// Token: 0x06000333 RID: 819 RVA: 0x0000B724 File Offset: 0x00009924
		[DebuggerStepThrough]
		public override bool Read()
		{
			this._readType = ReadType.Read;
			if (!this.ReadInternal())
			{
				base.SetToken(JsonToken.None);
				return false;
			}
			return true;
		}

		/// <summary>
		/// Reads the next JSON token from the stream as a <see cref="T:Byte[]" />.
		/// </summary>
		/// <returns>
		/// A <see cref="T:Byte[]" /> or a null reference if the next JSON token is null. This method will return <c>null</c> at the end of an array.
		/// </returns>
		// Token: 0x06000334 RID: 820 RVA: 0x0000B73F File Offset: 0x0000993F
		public override byte[] ReadAsBytes()
		{
			return base.ReadAsBytesInternal();
		}

		/// <summary>
		/// Reads the next JSON token from the stream as a <see cref="T:System.Nullable`1" />.
		/// </summary>
		/// <returns>A <see cref="T:System.Nullable`1" />. This method will return <c>null</c> at the end of an array.</returns>
		// Token: 0x06000335 RID: 821 RVA: 0x0000B747 File Offset: 0x00009947
		public override decimal? ReadAsDecimal()
		{
			return base.ReadAsDecimalInternal();
		}

		/// <summary>
		/// Reads the next JSON token from the stream as a <see cref="T:System.Nullable`1" />.
		/// </summary>
		/// <returns>A <see cref="T:System.Nullable`1" />. This method will return <c>null</c> at the end of an array.</returns>
		// Token: 0x06000336 RID: 822 RVA: 0x0000B74F File Offset: 0x0000994F
		public override int? ReadAsInt32()
		{
			return base.ReadAsInt32Internal();
		}

		/// <summary>
		/// Reads the next JSON token from the stream as a <see cref="T:System.String" />.
		/// </summary>
		/// <returns>A <see cref="T:System.String" />. This method will return <c>null</c> at the end of an array.</returns>
		// Token: 0x06000337 RID: 823 RVA: 0x0000B757 File Offset: 0x00009957
		public override string ReadAsString()
		{
			return base.ReadAsStringInternal();
		}

		/// <summary>
		/// Reads the next JSON token from the stream as a <see cref="T:System.Nullable`1" />.
		/// </summary>
		/// <returns>A <see cref="T:System.String" />. This method will return <c>null</c> at the end of an array.</returns>
		// Token: 0x06000338 RID: 824 RVA: 0x0000B75F File Offset: 0x0000995F
		public override DateTime? ReadAsDateTime()
		{
			return base.ReadAsDateTimeInternal();
		}

		/// <summary>
		/// Reads the next JSON token from the stream as a <see cref="T:System.Nullable`1" />.
		/// </summary>
		/// <returns>A <see cref="T:System.DateTimeOffset" />. This method will return <c>null</c> at the end of an array.</returns>
		// Token: 0x06000339 RID: 825 RVA: 0x0000B767 File Offset: 0x00009967
		public override DateTimeOffset? ReadAsDateTimeOffset()
		{
			return base.ReadAsDateTimeOffsetInternal();
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000B770 File Offset: 0x00009970
		internal override bool ReadInternal()
		{
			for (;;)
			{
				switch (this._currentState)
				{
				case JsonReader.State.Start:
				case JsonReader.State.Property:
				case JsonReader.State.ArrayStart:
				case JsonReader.State.Array:
				case JsonReader.State.ConstructorStart:
				case JsonReader.State.Constructor:
					goto IL_43;
				case JsonReader.State.Complete:
				case JsonReader.State.Closed:
				case JsonReader.State.Error:
					continue;
				case JsonReader.State.ObjectStart:
				case JsonReader.State.Object:
					goto IL_4A;
				case JsonReader.State.PostValue:
					if (this.ParsePostValue())
					{
						return true;
					}
					continue;
				case JsonReader.State.Finished:
					goto IL_5B;
				}
				break;
			}
			throw JsonReaderException.Create(this, "Unexpected state: {0}.".FormatWith(CultureInfo.InvariantCulture, base.CurrentState));
			IL_43:
			return this.ParseValue();
			IL_4A:
			return this.ParseObject();
			IL_5B:
			if (!this.EnsureChars(0, false))
			{
				return false;
			}
			this.EatWhitespace(false);
			if (this._isEndOfFile)
			{
				return false;
			}
			if (this._chars[this._charPos] == '/')
			{
				this.ParseComment();
				return true;
			}
			throw JsonReaderException.Create(this, "Additional text encountered after finished reading JSON content: {0}.".FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000B858 File Offset: 0x00009A58
		private void ReadStringIntoBuffer(char quote)
		{
			int num = this._charPos;
			int charPos = this._charPos;
			int num2 = this._charPos;
			StringBuffer stringBuffer = null;
			char c2;
			for (;;)
			{
				char c = this._chars[num++];
				if (c <= '\r')
				{
					if (c != '\0')
					{
						if (c != '\n')
						{
							if (c == '\r')
							{
								this._charPos = num - 1;
								this.ProcessCarriageReturn(true);
								num = this._charPos;
							}
						}
						else
						{
							this._charPos = num - 1;
							this.ProcessLineFeed();
							num = this._charPos;
						}
					}
					else if (this._charsUsed == num - 1)
					{
						num--;
						if (this.ReadData(true) == 0)
						{
							break;
						}
					}
				}
				else if (c != '"' && c != '\'')
				{
					if (c == '\\')
					{
						this._charPos = num;
						if (!this.EnsureChars(0, true))
						{
							goto Block_10;
						}
						int writeToPosition = num - 1;
						c2 = this._chars[num];
						char c3 = c2;
						char c4;
						if (c3 <= '\\')
						{
							if (c3 <= '\'')
							{
								if (c3 != '"' && c3 != '\'')
								{
									goto Block_14;
								}
							}
							else if (c3 != '/')
							{
								if (c3 != '\\')
								{
									goto Block_16;
								}
								num++;
								c4 = '\\';
								goto IL_2BF;
							}
							c4 = c2;
							num++;
						}
						else if (c3 <= 'f')
						{
							if (c3 != 'b')
							{
								if (c3 != 'f')
								{
									goto Block_19;
								}
								num++;
								c4 = '\f';
							}
							else
							{
								num++;
								c4 = '\b';
							}
						}
						else
						{
							if (c3 != 'n')
							{
								switch (c3)
								{
								case 'r':
									num++;
									c4 = '\r';
									goto IL_2BF;
								case 't':
									num++;
									c4 = '\t';
									goto IL_2BF;
								case 'u':
									num++;
									this._charPos = num;
									c4 = this.ParseUnicode();
									if (StringUtils.IsLowSurrogate(c4))
									{
										c4 = '�';
									}
									else if (StringUtils.IsHighSurrogate(c4))
									{
										bool flag;
										do
										{
											flag = false;
											if (this.EnsureChars(2, true) && this._chars[this._charPos] == '\\' && this._chars[this._charPos + 1] == 'u')
											{
												char writeChar = c4;
												this._charPos += 2;
												c4 = this.ParseUnicode();
												if (!StringUtils.IsLowSurrogate(c4))
												{
													if (StringUtils.IsHighSurrogate(c4))
													{
														writeChar = '�';
														flag = true;
													}
													else
													{
														writeChar = '�';
													}
												}
												if (stringBuffer == null)
												{
													stringBuffer = this.GetBuffer();
												}
												this.WriteCharToBuffer(stringBuffer, writeChar, num2, writeToPosition);
												num2 = this._charPos;
											}
											else
											{
												c4 = '�';
											}
										}
										while (flag);
									}
									num = this._charPos;
									goto IL_2BF;
								}
								goto Block_21;
							}
							num++;
							c4 = '\n';
						}
						IL_2BF:
						if (stringBuffer == null)
						{
							stringBuffer = this.GetBuffer();
						}
						this.WriteCharToBuffer(stringBuffer, c4, num2, writeToPosition);
						num2 = num;
					}
				}
				else if (this._chars[num - 1] == quote)
				{
					goto Block_30;
				}
			}
			this._charPos = num;
			throw JsonReaderException.Create(this, "Unterminated string. Expected delimiter: {0}.".FormatWith(CultureInfo.InvariantCulture, quote));
			Block_10:
			this._charPos = num;
			throw JsonReaderException.Create(this, "Unterminated string. Expected delimiter: {0}.".FormatWith(CultureInfo.InvariantCulture, quote));
			Block_14:
			Block_16:
			Block_19:
			Block_21:
			num++;
			this._charPos = num;
			throw JsonReaderException.Create(this, "Bad JSON escape sequence: {0}.".FormatWith(CultureInfo.InvariantCulture, "\\" + c2));
			Block_30:
			num--;
			if (charPos == num2)
			{
				this._stringReference = new StringReference(this._chars, charPos, num - charPos);
			}
			else
			{
				if (stringBuffer == null)
				{
					stringBuffer = this.GetBuffer();
				}
				if (num > num2)
				{
					stringBuffer.Append(this._chars, num2, num - num2);
				}
				this._stringReference = new StringReference(stringBuffer.GetInternalBuffer(), 0, stringBuffer.Position);
			}
			num++;
			this._charPos = num;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000BBE8 File Offset: 0x00009DE8
		private void WriteCharToBuffer(StringBuffer buffer, char writeChar, int lastWritePosition, int writeToPosition)
		{
			if (writeToPosition > lastWritePosition)
			{
				buffer.Append(this._chars, lastWritePosition, writeToPosition - lastWritePosition);
			}
			buffer.Append(writeChar);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000BC08 File Offset: 0x00009E08
		private char ParseUnicode()
		{
			if (this.EnsureChars(4, true))
			{
				string text = new string(this._chars, this._charPos, 4);
				char c = Convert.ToChar(int.Parse(text, 515, NumberFormatInfo.InvariantInfo));
				char result = c;
				this._charPos += 4;
				return result;
			}
			throw JsonReaderException.Create(this, "Unexpected end while parsing unicode character.");
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000BC68 File Offset: 0x00009E68
		private void ReadNumberIntoBuffer()
		{
			int num = this._charPos;
			for (;;)
			{
				char c = this._chars[num++];
				if (c <= 'F')
				{
					if (c != '\0')
					{
						switch (c)
						{
						case '+':
						case '-':
						case '.':
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
						case 'A':
						case 'B':
						case 'C':
						case 'D':
						case 'E':
						case 'F':
							continue;
						}
						break;
					}
					if (this._charsUsed != num - 1)
					{
						goto IL_F4;
					}
					num--;
					this._charPos = num;
					if (this.ReadData(true) == 0)
					{
						return;
					}
				}
				else if (c != 'X')
				{
					switch (c)
					{
					case 'a':
					case 'b':
					case 'c':
					case 'd':
					case 'e':
					case 'f':
						break;
					default:
						if (c != 'x')
						{
							goto Block_6;
						}
						break;
					}
				}
			}
			Block_6:
			goto IL_FE;
			IL_F4:
			this._charPos = num - 1;
			return;
			IL_FE:
			this._charPos = num - 1;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000BD7C File Offset: 0x00009F7C
		private void ClearRecentString()
		{
			if (this._buffer != null)
			{
				this._buffer.Position = 0;
			}
			this._stringReference = default(StringReference);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000BDA0 File Offset: 0x00009FA0
		private bool ParsePostValue()
		{
			char c;
			for (;;)
			{
				c = this._chars[this._charPos];
				char c2 = c;
				if (c2 <= ')')
				{
					if (c2 <= '\r')
					{
						if (c2 != '\0')
						{
							switch (c2)
							{
							case '\t':
								break;
							case '\n':
								this.ProcessLineFeed();
								continue;
							case '\v':
							case '\f':
								goto IL_145;
							case '\r':
								this.ProcessCarriageReturn(false);
								continue;
							default:
								goto IL_145;
							}
						}
						else
						{
							if (this._charsUsed != this._charPos)
							{
								this._charPos++;
								continue;
							}
							if (this.ReadData(false) == 0)
							{
								break;
							}
							continue;
						}
					}
					else if (c2 != ' ')
					{
						if (c2 != ')')
						{
							goto IL_145;
						}
						goto IL_E5;
					}
					this._charPos++;
					continue;
				}
				if (c2 <= '/')
				{
					if (c2 == ',')
					{
						goto IL_105;
					}
					if (c2 == '/')
					{
						goto IL_FD;
					}
				}
				else
				{
					if (c2 == ']')
					{
						goto IL_CD;
					}
					if (c2 == '}')
					{
						goto IL_B5;
					}
				}
				IL_145:
				if (!char.IsWhiteSpace(c))
				{
					goto IL_160;
				}
				this._charPos++;
			}
			this._currentState = JsonReader.State.Finished;
			return false;
			IL_B5:
			this._charPos++;
			base.SetToken(JsonToken.EndObject);
			return true;
			IL_CD:
			this._charPos++;
			base.SetToken(JsonToken.EndArray);
			return true;
			IL_E5:
			this._charPos++;
			base.SetToken(JsonToken.EndConstructor);
			return true;
			IL_FD:
			this.ParseComment();
			return true;
			IL_105:
			this._charPos++;
			base.SetStateBasedOnCurrent();
			return false;
			IL_160:
			throw JsonReaderException.Create(this, "After parsing a value an unexpected character was encountered: {0}.".FormatWith(CultureInfo.InvariantCulture, c));
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000BF28 File Offset: 0x0000A128
		private bool ParseObject()
		{
			for (;;)
			{
				char c = this._chars[this._charPos];
				char c2 = c;
				if (c2 <= '\r')
				{
					if (c2 != '\0')
					{
						switch (c2)
						{
						case '\t':
							break;
						case '\n':
							this.ProcessLineFeed();
							continue;
						case '\v':
						case '\f':
							goto IL_BF;
						case '\r':
							this.ProcessCarriageReturn(false);
							continue;
						default:
							goto IL_BF;
						}
					}
					else
					{
						if (this._charsUsed != this._charPos)
						{
							this._charPos++;
							continue;
						}
						if (this.ReadData(false) == 0)
						{
							break;
						}
						continue;
					}
				}
				else if (c2 != ' ')
				{
					if (c2 == '/')
					{
						goto IL_8D;
					}
					if (c2 != '}')
					{
						goto IL_BF;
					}
					goto IL_75;
				}
				this._charPos++;
				continue;
				IL_BF:
				if (!char.IsWhiteSpace(c))
				{
					goto IL_DA;
				}
				this._charPos++;
			}
			return false;
			IL_75:
			base.SetToken(JsonToken.EndObject);
			this._charPos++;
			return true;
			IL_8D:
			this.ParseComment();
			return true;
			IL_DA:
			return this.ParseProperty();
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000C018 File Offset: 0x0000A218
		private bool ParseProperty()
		{
			char c = this._chars[this._charPos];
			char c2;
			if (c == '"' || c == '\'')
			{
				this._charPos++;
				c2 = c;
				this.ShiftBufferIfNeeded();
				this.ReadStringIntoBuffer(c2);
			}
			else
			{
				if (!this.ValidIdentifierChar(c))
				{
					throw JsonReaderException.Create(this, "Invalid property identifier character: {0}.".FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
				}
				c2 = '\0';
				this.ShiftBufferIfNeeded();
				this.ParseUnquotedProperty();
			}
			string value = this._stringReference.ToString();
			this.EatWhitespace(false);
			if (this._chars[this._charPos] != ':')
			{
				throw JsonReaderException.Create(this, "Invalid character after parsing property name. Expected ':' but got: {0}.".FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
			}
			this._charPos++;
			base.SetToken(JsonToken.PropertyName, value);
			this._quoteChar = c2;
			this.ClearRecentString();
			return true;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000C114 File Offset: 0x0000A314
		private bool ValidIdentifierChar(char value)
		{
			return char.IsLetterOrDigit(value) || value == '_' || value == '$';
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000C12C File Offset: 0x0000A32C
		private void ParseUnquotedProperty()
		{
			int charPos = this._charPos;
			char c2;
			for (;;)
			{
				char c = this._chars[this._charPos];
				if (c == '\0')
				{
					if (this._charsUsed != this._charPos)
					{
						goto IL_3C;
					}
					if (this.ReadData(true) == 0)
					{
						break;
					}
				}
				else
				{
					c2 = this._chars[this._charPos];
					if (!this.ValidIdentifierChar(c2))
					{
						goto IL_7E;
					}
					this._charPos++;
				}
			}
			throw JsonReaderException.Create(this, "Unexpected end while parsing unquoted property name.");
			IL_3C:
			this._stringReference = new StringReference(this._chars, charPos, this._charPos - charPos);
			return;
			IL_7E:
			if (char.IsWhiteSpace(c2) || c2 == ':')
			{
				this._stringReference = new StringReference(this._chars, charPos, this._charPos - charPos);
				return;
			}
			throw JsonReaderException.Create(this, "Invalid JavaScript property identifier character: {0}.".FormatWith(CultureInfo.InvariantCulture, c2));
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000C1FC File Offset: 0x0000A3FC
		private bool ParseValue()
		{
			char c;
			for (;;)
			{
				c = this._chars[this._charPos];
				char c2 = c;
				if (c2 <= 'I')
				{
					if (c2 <= '\r')
					{
						if (c2 != '\0')
						{
							switch (c2)
							{
							case '\t':
								break;
							case '\n':
								this.ProcessLineFeed();
								continue;
							case '\v':
							case '\f':
								goto IL_272;
							case '\r':
								this.ProcessCarriageReturn(false);
								continue;
							default:
								goto IL_272;
							}
						}
						else
						{
							if (this._charsUsed != this._charPos)
							{
								this._charPos++;
								continue;
							}
							if (this.ReadData(false) == 0)
							{
								break;
							}
							continue;
						}
					}
					else
					{
						switch (c2)
						{
						case ' ':
							break;
						case '!':
							goto IL_272;
						case '"':
							goto IL_110;
						default:
							switch (c2)
							{
							case '\'':
								goto IL_110;
							case '(':
							case '*':
							case '+':
							case '.':
								goto IL_272;
							case ')':
								goto IL_230;
							case ',':
								goto IL_226;
							case '-':
								goto IL_1A3;
							case '/':
								goto IL_1D0;
							default:
								if (c2 != 'I')
								{
									goto IL_272;
								}
								goto IL_19B;
							}
							break;
						}
					}
					this._charPos++;
					continue;
				}
				if (c2 <= 'f')
				{
					if (c2 == 'N')
					{
						goto IL_193;
					}
					switch (c2)
					{
					case '[':
						goto IL_1F7;
					case '\\':
						break;
					case ']':
						goto IL_20E;
					default:
						if (c2 == 'f')
						{
							goto IL_121;
						}
						break;
					}
				}
				else
				{
					if (c2 == 'n')
					{
						goto IL_129;
					}
					switch (c2)
					{
					case 't':
						goto IL_119;
					case 'u':
						goto IL_1D8;
					default:
						if (c2 == '{')
						{
							goto IL_1E0;
						}
						break;
					}
				}
				IL_272:
				if (!char.IsWhiteSpace(c))
				{
					goto IL_28D;
				}
				this._charPos++;
			}
			return false;
			IL_110:
			this.ParseString(c);
			return true;
			IL_119:
			this.ParseTrue();
			return true;
			IL_121:
			this.ParseFalse();
			return true;
			IL_129:
			if (this.EnsureChars(1, true))
			{
				char c3 = this._chars[this._charPos + 1];
				if (c3 == 'u')
				{
					this.ParseNull();
				}
				else
				{
					if (c3 != 'e')
					{
						throw JsonReaderException.Create(this, "Unexpected character encountered while parsing value: {0}.".FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
					}
					this.ParseConstructor();
				}
				return true;
			}
			throw JsonReaderException.Create(this, "Unexpected end.");
			IL_193:
			this.ParseNumberNaN();
			return true;
			IL_19B:
			this.ParseNumberPositiveInfinity();
			return true;
			IL_1A3:
			if (this.EnsureChars(1, true) && this._chars[this._charPos + 1] == 'I')
			{
				this.ParseNumberNegativeInfinity();
			}
			else
			{
				this.ParseNumber();
			}
			return true;
			IL_1D0:
			this.ParseComment();
			return true;
			IL_1D8:
			this.ParseUndefined();
			return true;
			IL_1E0:
			this._charPos++;
			base.SetToken(JsonToken.StartObject);
			return true;
			IL_1F7:
			this._charPos++;
			base.SetToken(JsonToken.StartArray);
			return true;
			IL_20E:
			this._charPos++;
			base.SetToken(JsonToken.EndArray);
			return true;
			IL_226:
			base.SetToken(JsonToken.Undefined);
			return true;
			IL_230:
			this._charPos++;
			base.SetToken(JsonToken.EndConstructor);
			return true;
			IL_28D:
			if (char.IsNumber(c) || c == '-' || c == '.')
			{
				this.ParseNumber();
				return true;
			}
			throw JsonReaderException.Create(this, "Unexpected character encountered while parsing value: {0}.".FormatWith(CultureInfo.InvariantCulture, c));
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000C4CB File Offset: 0x0000A6CB
		private void ProcessLineFeed()
		{
			this._charPos++;
			this.OnNewLine(this._charPos);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000C4E8 File Offset: 0x0000A6E8
		private void ProcessCarriageReturn(bool append)
		{
			this._charPos++;
			if (this.EnsureChars(1, append) && this._chars[this._charPos] == '\n')
			{
				this._charPos++;
			}
			this.OnNewLine(this._charPos);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000C538 File Offset: 0x0000A738
		private bool EatWhitespace(bool oneOrMore)
		{
			bool flag = false;
			bool flag2 = false;
			while (!flag)
			{
				char c = this._chars[this._charPos];
				char c2 = c;
				if (c2 != '\0')
				{
					if (c2 != '\n')
					{
						if (c2 != '\r')
						{
							if (c == ' ' || char.IsWhiteSpace(c))
							{
								flag2 = true;
								this._charPos++;
							}
							else
							{
								flag = true;
							}
						}
						else
						{
							this.ProcessCarriageReturn(false);
						}
					}
					else
					{
						this.ProcessLineFeed();
					}
				}
				else if (this._charsUsed == this._charPos)
				{
					if (this.ReadData(false) == 0)
					{
						flag = true;
					}
				}
				else
				{
					this._charPos++;
				}
			}
			return !oneOrMore || flag2;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000C5D4 File Offset: 0x0000A7D4
		private void ParseConstructor()
		{
			if (!this.MatchValueWithTrailingSeperator("new"))
			{
				throw JsonReaderException.Create(this, "Unexpected content while parsing JSON.");
			}
			this.EatWhitespace(false);
			int charPos = this._charPos;
			char c;
			for (;;)
			{
				c = this._chars[this._charPos];
				if (c == '\0')
				{
					if (this._charsUsed != this._charPos)
					{
						goto IL_53;
					}
					if (this.ReadData(true) == 0)
					{
						break;
					}
				}
				else
				{
					if (!char.IsLetterOrDigit(c))
					{
						goto IL_85;
					}
					this._charPos++;
				}
			}
			throw JsonReaderException.Create(this, "Unexpected end while parsing constructor.");
			IL_53:
			int charPos2 = this._charPos;
			this._charPos++;
			goto IL_F7;
			IL_85:
			if (c == '\r')
			{
				charPos2 = this._charPos;
				this.ProcessCarriageReturn(true);
			}
			else if (c == '\n')
			{
				charPos2 = this._charPos;
				this.ProcessLineFeed();
			}
			else if (char.IsWhiteSpace(c))
			{
				charPos2 = this._charPos;
				this._charPos++;
			}
			else
			{
				if (c != '(')
				{
					throw JsonReaderException.Create(this, "Unexpected character while parsing constructor: {0}.".FormatWith(CultureInfo.InvariantCulture, c));
				}
				charPos2 = this._charPos;
			}
			IL_F7:
			this._stringReference = new StringReference(this._chars, charPos, charPos2 - charPos);
			string value = this._stringReference.ToString();
			this.EatWhitespace(false);
			if (this._chars[this._charPos] != '(')
			{
				throw JsonReaderException.Create(this, "Unexpected character while parsing constructor: {0}.".FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
			}
			this._charPos++;
			this.ClearRecentString();
			base.SetToken(JsonToken.StartConstructor, value);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000C768 File Offset: 0x0000A968
		private void ParseNumber()
		{
			this.ShiftBufferIfNeeded();
			char c = this._chars[this._charPos];
			int charPos = this._charPos;
			this.ReadNumberIntoBuffer();
			this._stringReference = new StringReference(this._chars, charPos, this._charPos - charPos);
			bool flag = char.IsDigit(c) && this._stringReference.Length == 1;
			bool flag2 = c == '0' && this._stringReference.Length > 1 && this._stringReference.Chars[this._stringReference.StartIndex + 1] != '.' && this._stringReference.Chars[this._stringReference.StartIndex + 1] != 'e' && this._stringReference.Chars[this._stringReference.StartIndex + 1] != 'E';
			object value;
			JsonToken newToken;
			if (this._readType == ReadType.ReadAsInt32)
			{
				if (flag)
				{
					value = (int)(c - '0');
				}
				else if (flag2)
				{
					string text = this._stringReference.ToString();
					int num = text.StartsWith("0x", 5) ? Convert.ToInt32(text, 16) : Convert.ToInt32(text, 8);
					value = num;
				}
				else
				{
					value = ConvertUtils.Int32Parse(this._stringReference.Chars, this._stringReference.StartIndex, this._stringReference.Length);
				}
				newToken = JsonToken.Integer;
			}
			else if (this._readType == ReadType.ReadAsDecimal)
			{
				if (flag)
				{
					value = c - 48m;
				}
				else if (flag2)
				{
					string text2 = this._stringReference.ToString();
					long num2 = text2.StartsWith("0x", 5) ? Convert.ToInt64(text2, 16) : Convert.ToInt64(text2, 8);
					value = Convert.ToDecimal(num2);
				}
				else
				{
					string text3 = this._stringReference.ToString();
					value = decimal.Parse(text3, 239, CultureInfo.InvariantCulture);
				}
				newToken = JsonToken.Float;
			}
			else if (flag)
			{
				value = (long)((ulong)c - 48UL);
				newToken = JsonToken.Integer;
			}
			else if (flag2)
			{
				string text4 = this._stringReference.ToString();
				value = (text4.StartsWith("0x", 5) ? Convert.ToInt64(text4, 16) : Convert.ToInt64(text4, 8));
				newToken = JsonToken.Integer;
			}
			else
			{
				long num3;
				ParseResult parseResult = ConvertUtils.Int64TryParse(this._stringReference.Chars, this._stringReference.StartIndex, this._stringReference.Length, out num3);
				if (parseResult == ParseResult.Success)
				{
					value = num3;
					newToken = JsonToken.Integer;
				}
				else if (parseResult == ParseResult.Invalid)
				{
					string text5 = this._stringReference.ToString();
					if (this._floatParseHandling == FloatParseHandling.Decimal)
					{
						value = decimal.Parse(text5, 239, CultureInfo.InvariantCulture);
					}
					else
					{
						value = Convert.ToDouble(text5, CultureInfo.InvariantCulture);
					}
					newToken = JsonToken.Float;
				}
				else
				{
					if (parseResult != ParseResult.Overflow)
					{
						throw JsonReaderException.Create(this, "Unknown error parsing integer.");
					}
					string text6 = this._stringReference.ToString();
					value = BigInteger.Parse(text6, CultureInfo.InvariantCulture);
					newToken = JsonToken.Integer;
				}
			}
			this.ClearRecentString();
			base.SetToken(newToken, value);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000CAAC File Offset: 0x0000ACAC
		private void ParseComment()
		{
			this._charPos++;
			if (!this.EnsureChars(1, false) || this._chars[this._charPos] != '*')
			{
				throw JsonReaderException.Create(this, "Error parsing comment. Expected: *, got {0}.".FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
			}
			this._charPos++;
			int charPos = this._charPos;
			bool flag = false;
			while (!flag)
			{
				char c = this._chars[this._charPos];
				if (c <= '\n')
				{
					if (c != '\0')
					{
						if (c == '\n')
						{
							this.ProcessLineFeed();
							continue;
						}
					}
					else
					{
						if (this._charsUsed != this._charPos)
						{
							this._charPos++;
							continue;
						}
						if (this.ReadData(true) == 0)
						{
							throw JsonReaderException.Create(this, "Unexpected end while parsing comment.");
						}
						continue;
					}
				}
				else
				{
					if (c == '\r')
					{
						this.ProcessCarriageReturn(true);
						continue;
					}
					if (c == '*')
					{
						this._charPos++;
						if (this.EnsureChars(0, true) && this._chars[this._charPos] == '/')
						{
							this._stringReference = new StringReference(this._chars, charPos, this._charPos - charPos - 1);
							this._charPos++;
							flag = true;
							continue;
						}
						continue;
					}
				}
				this._charPos++;
			}
			base.SetToken(JsonToken.Comment, this._stringReference.ToString());
			this.ClearRecentString();
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000CC2C File Offset: 0x0000AE2C
		private bool MatchValue(string value)
		{
			if (!this.EnsureChars(value.Length - 1, true))
			{
				return false;
			}
			for (int i = 0; i < value.Length; i++)
			{
				if (this._chars[this._charPos + i] != value.get_Chars(i))
				{
					return false;
				}
			}
			this._charPos += value.Length;
			return true;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000CC8C File Offset: 0x0000AE8C
		private bool MatchValueWithTrailingSeperator(string value)
		{
			return this.MatchValue(value) && (!this.EnsureChars(0, false) || this.IsSeperator(this._chars[this._charPos]) || this._chars[this._charPos] == '\0');
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000CCDC File Offset: 0x0000AEDC
		private bool IsSeperator(char c)
		{
			if (c <= ')')
			{
				switch (c)
				{
				case '\t':
				case '\n':
				case '\r':
					break;
				case '\v':
				case '\f':
					goto IL_85;
				default:
					if (c != ' ')
					{
						if (c != ')')
						{
							goto IL_85;
						}
						if (base.CurrentState == JsonReader.State.Constructor || base.CurrentState == JsonReader.State.ConstructorStart)
						{
							return true;
						}
						return false;
					}
					break;
				}
				return true;
			}
			if (c <= '/')
			{
				if (c != ',')
				{
					if (c != '/')
					{
						goto IL_85;
					}
					return this.EnsureChars(1, false) && this._chars[this._charPos + 1] == '*';
				}
			}
			else if (c != ']' && c != '}')
			{
				goto IL_85;
			}
			return true;
			IL_85:
			if (char.IsWhiteSpace(c))
			{
				return true;
			}
			return false;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000CD79 File Offset: 0x0000AF79
		private void ParseTrue()
		{
			if (this.MatchValueWithTrailingSeperator(JsonConvert.True))
			{
				base.SetToken(JsonToken.Boolean, true);
				return;
			}
			throw JsonReaderException.Create(this, "Error parsing boolean value.");
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000CDA2 File Offset: 0x0000AFA2
		private void ParseNull()
		{
			if (this.MatchValueWithTrailingSeperator(JsonConvert.Null))
			{
				base.SetToken(JsonToken.Null);
				return;
			}
			throw JsonReaderException.Create(this, "Error parsing null value.");
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000CDC5 File Offset: 0x0000AFC5
		private void ParseUndefined()
		{
			if (this.MatchValueWithTrailingSeperator(JsonConvert.Undefined))
			{
				base.SetToken(JsonToken.Undefined);
				return;
			}
			throw JsonReaderException.Create(this, "Error parsing undefined value.");
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000CDE8 File Offset: 0x0000AFE8
		private void ParseFalse()
		{
			if (this.MatchValueWithTrailingSeperator(JsonConvert.False))
			{
				base.SetToken(JsonToken.Boolean, false);
				return;
			}
			throw JsonReaderException.Create(this, "Error parsing boolean value.");
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000CE14 File Offset: 0x0000B014
		private void ParseNumberNegativeInfinity()
		{
			if (!this.MatchValueWithTrailingSeperator(JsonConvert.NegativeInfinity))
			{
				throw JsonReaderException.Create(this, "Error parsing negative infinity value.");
			}
			if (this._floatParseHandling == FloatParseHandling.Decimal)
			{
				throw new JsonReaderException("Cannot read -Infinity as a decimal.");
			}
			base.SetToken(JsonToken.Float, double.NegativeInfinity);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000CE64 File Offset: 0x0000B064
		private void ParseNumberPositiveInfinity()
		{
			if (!this.MatchValueWithTrailingSeperator(JsonConvert.PositiveInfinity))
			{
				throw JsonReaderException.Create(this, "Error parsing positive infinity value.");
			}
			if (this._floatParseHandling == FloatParseHandling.Decimal)
			{
				throw new JsonReaderException("Cannot read Infinity as a decimal.");
			}
			base.SetToken(JsonToken.Float, double.PositiveInfinity);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000CEB4 File Offset: 0x0000B0B4
		private void ParseNumberNaN()
		{
			if (!this.MatchValueWithTrailingSeperator(JsonConvert.NaN))
			{
				throw JsonReaderException.Create(this, "Error parsing NaN value.");
			}
			if (this._floatParseHandling == FloatParseHandling.Decimal)
			{
				throw new JsonReaderException("Cannot read NaN as a decimal.");
			}
			base.SetToken(JsonToken.Float, double.NaN);
		}

		/// <summary>
		/// Changes the state to closed. 
		/// </summary>
		// Token: 0x06000356 RID: 854 RVA: 0x0000CF03 File Offset: 0x0000B103
		public override void Close()
		{
			base.Close();
			if (base.CloseInput && this._reader != null)
			{
				this._reader.Dispose();
			}
			if (this._buffer != null)
			{
				this._buffer.Clear();
			}
		}

		/// <summary>
		/// Gets a value indicating whether the class can return line information.
		/// </summary>
		/// <returns>
		/// 	<c>true</c> if LineNumber and LinePosition can be provided; otherwise, <c>false</c>.
		/// </returns>
		// Token: 0x06000357 RID: 855 RVA: 0x0000CF39 File Offset: 0x0000B139
		public bool HasLineInfo()
		{
			return true;
		}

		/// <summary>
		/// Gets the current line number.
		/// </summary>
		/// <value>
		/// The current line number or 0 if no line information is available (for example, HasLineInfo returns false).
		/// </value>
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0000CF3C File Offset: 0x0000B13C
		public int LineNumber
		{
			get
			{
				if (base.CurrentState == JsonReader.State.Start && this.LinePosition == 0)
				{
					return 0;
				}
				return this._lineNumber;
			}
		}

		/// <summary>
		/// Gets the current line position.
		/// </summary>
		/// <value>
		/// The current line position or 0 if no line information is available (for example, HasLineInfo returns false).
		/// </value>
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0000CF56 File Offset: 0x0000B156
		public int LinePosition
		{
			get
			{
				return this._charPos - this._lineStartPos;
			}
		}

		// Token: 0x04000144 RID: 324
		private const char UnicodeReplacementChar = '�';

		// Token: 0x04000145 RID: 325
		private readonly TextReader _reader;

		// Token: 0x04000146 RID: 326
		private char[] _chars;

		// Token: 0x04000147 RID: 327
		private int _charsUsed;

		// Token: 0x04000148 RID: 328
		private int _charPos;

		// Token: 0x04000149 RID: 329
		private int _lineStartPos;

		// Token: 0x0400014A RID: 330
		private int _lineNumber;

		// Token: 0x0400014B RID: 331
		private bool _isEndOfFile;

		// Token: 0x0400014C RID: 332
		private StringBuffer _buffer;

		// Token: 0x0400014D RID: 333
		private StringReference _stringReference;
	}
}
