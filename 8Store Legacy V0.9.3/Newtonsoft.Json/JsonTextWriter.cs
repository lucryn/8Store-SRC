using System;
using System.Globalization;
using System.IO;
using System.Numerics;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	/// <summary>
	/// Represents a writer that provides a fast, non-cached, forward-only way of generating Json data.
	/// </summary>
	// Token: 0x0200004E RID: 78
	public class JsonTextWriter : JsonWriter
	{
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0000CF65 File Offset: 0x0000B165
		private Base64Encoder Base64Encoder
		{
			get
			{
				if (this._base64Encoder == null)
				{
					this._base64Encoder = new Base64Encoder(this._writer);
				}
				return this._base64Encoder;
			}
		}

		/// <summary>
		/// Gets or sets how many IndentChars to write for each level in the hierarchy when <see cref="T:Newtonsoft.Json.Formatting" /> is set to <c>Formatting.Indented</c>.
		/// </summary>
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0000CF86 File Offset: 0x0000B186
		// (set) Token: 0x0600035C RID: 860 RVA: 0x0000CF8E File Offset: 0x0000B18E
		public int Indentation
		{
			get
			{
				return this._indentation;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("Indentation value must be greater than 0.");
				}
				this._indentation = value;
			}
		}

		/// <summary>
		/// Gets or sets which character to use to quote attribute values.
		/// </summary>
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0000CFA6 File Offset: 0x0000B1A6
		// (set) Token: 0x0600035E RID: 862 RVA: 0x0000CFAE File Offset: 0x0000B1AE
		public char QuoteChar
		{
			get
			{
				return this._quoteChar;
			}
			set
			{
				if (value != '"' && value != '\'')
				{
					throw new ArgumentException("Invalid JavaScript string quote character. Valid quote characters are ' and \".");
				}
				this._quoteChar = value;
				this.UpdateCharEscapeFlags();
			}
		}

		/// <summary>
		/// Gets or sets which character to use for indenting when <see cref="T:Newtonsoft.Json.Formatting" /> is set to <c>Formatting.Indented</c>.
		/// </summary>
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000CFD2 File Offset: 0x0000B1D2
		// (set) Token: 0x06000360 RID: 864 RVA: 0x0000CFDA File Offset: 0x0000B1DA
		public char IndentChar
		{
			get
			{
				return this._indentChar;
			}
			set
			{
				this._indentChar = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether object names will be surrounded with quotes.
		/// </summary>
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0000CFE3 File Offset: 0x0000B1E3
		// (set) Token: 0x06000362 RID: 866 RVA: 0x0000CFEB File Offset: 0x0000B1EB
		public bool QuoteName
		{
			get
			{
				return this._quoteName;
			}
			set
			{
				this._quoteName = value;
			}
		}

		/// <summary>
		/// Creates an instance of the <c>JsonWriter</c> class using the specified <see cref="T:System.IO.TextWriter" />. 
		/// </summary>
		/// <param name="textWriter">The <c>TextWriter</c> to write to.</param>
		// Token: 0x06000363 RID: 867 RVA: 0x0000CFF4 File Offset: 0x0000B1F4
		public JsonTextWriter(TextWriter textWriter)
		{
			if (textWriter == null)
			{
				throw new ArgumentNullException("textWriter");
			}
			this._writer = textWriter;
			this._quoteChar = '"';
			this._quoteName = true;
			this._indentChar = ' ';
			this._indentation = 2;
			this.UpdateCharEscapeFlags();
		}

		/// <summary>
		/// Flushes whatever is in the buffer to the underlying streams and also flushes the underlying stream.
		/// </summary>
		// Token: 0x06000364 RID: 868 RVA: 0x0000D040 File Offset: 0x0000B240
		public override void Flush()
		{
			this._writer.Flush();
		}

		/// <summary>
		/// Closes this stream and the underlying stream.
		/// </summary>
		// Token: 0x06000365 RID: 869 RVA: 0x0000D04D File Offset: 0x0000B24D
		public override void Close()
		{
			base.Close();
			if (base.CloseOutput && this._writer != null)
			{
				this._writer.Dispose();
			}
		}

		/// <summary>
		/// Writes the beginning of a Json object.
		/// </summary>
		// Token: 0x06000366 RID: 870 RVA: 0x0000D070 File Offset: 0x0000B270
		public override void WriteStartObject()
		{
			base.InternalWriteStart(JsonToken.StartObject, JsonContainerType.Object);
			this._writer.Write("{");
		}

		/// <summary>
		/// Writes the beginning of a Json array.
		/// </summary>
		// Token: 0x06000367 RID: 871 RVA: 0x0000D08A File Offset: 0x0000B28A
		public override void WriteStartArray()
		{
			base.InternalWriteStart(JsonToken.StartArray, JsonContainerType.Array);
			this._writer.Write("[");
		}

		/// <summary>
		/// Writes the start of a constructor with the given name.
		/// </summary>
		/// <param name="name">The name of the constructor.</param>
		// Token: 0x06000368 RID: 872 RVA: 0x0000D0A4 File Offset: 0x0000B2A4
		public override void WriteStartConstructor(string name)
		{
			base.InternalWriteStart(JsonToken.StartConstructor, JsonContainerType.Constructor);
			this._writer.Write("new ");
			this._writer.Write(name);
			this._writer.Write("(");
		}

		/// <summary>
		/// Writes the specified end token.
		/// </summary>
		/// <param name="token">The end token to write.</param>
		// Token: 0x06000369 RID: 873 RVA: 0x0000D0DC File Offset: 0x0000B2DC
		protected override void WriteEnd(JsonToken token)
		{
			switch (token)
			{
			case JsonToken.EndObject:
				this._writer.Write("}");
				return;
			case JsonToken.EndArray:
				this._writer.Write("]");
				return;
			case JsonToken.EndConstructor:
				this._writer.Write(")");
				return;
			default:
				throw JsonWriterException.Create(this, "Invalid JsonToken: " + token, null);
			}
		}

		/// <summary>
		/// Writes the property name of a name/value pair on a Json object.
		/// </summary>
		/// <param name="name">The name of the property.</param>
		// Token: 0x0600036A RID: 874 RVA: 0x0000D14C File Offset: 0x0000B34C
		public override void WritePropertyName(string name)
		{
			base.InternalWritePropertyName(name);
			this.WriteEscapedString(name);
			this._writer.Write(':');
		}

		/// <summary>
		/// Writes the property name of a name/value pair on a JSON object.
		/// </summary>
		/// <param name="name">The name of the property.</param>
		/// <param name="escape">A flag to indicate whether the text should be escaped when it is written as a JSON property name.</param>
		// Token: 0x0600036B RID: 875 RVA: 0x0000D16C File Offset: 0x0000B36C
		public override void WritePropertyName(string name, bool escape)
		{
			base.InternalWritePropertyName(name);
			if (escape)
			{
				this.WriteEscapedString(name);
			}
			else
			{
				if (this._quoteName)
				{
					this._writer.Write(this._quoteChar);
				}
				this._writer.Write(name);
				if (this._quoteName)
				{
					this._writer.Write(this._quoteChar);
				}
			}
			this._writer.Write(':');
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000D1D7 File Offset: 0x0000B3D7
		internal override void OnStringEscapeHandlingChanged()
		{
			this.UpdateCharEscapeFlags();
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000D1DF File Offset: 0x0000B3DF
		private void UpdateCharEscapeFlags()
		{
			if (base.StringEscapeHandling == StringEscapeHandling.EscapeHtml)
			{
				this._charEscapeFlags = JavaScriptUtils.HtmlCharEscapeFlags;
				return;
			}
			if (this._quoteChar == '"')
			{
				this._charEscapeFlags = JavaScriptUtils.DoubleQuoteCharEscapeFlags;
				return;
			}
			this._charEscapeFlags = JavaScriptUtils.SingleQuoteCharEscapeFlags;
		}

		/// <summary>
		/// Writes indent characters.
		/// </summary>
		// Token: 0x0600036E RID: 878 RVA: 0x0000D218 File Offset: 0x0000B418
		protected override void WriteIndent()
		{
			this._writer.Write(Environment.NewLine);
			int num;
			for (int i = base.Top * this._indentation; i > 0; i -= num)
			{
				num = Math.Min(i, 10);
				this._writer.Write(new string(this._indentChar, num));
			}
		}

		/// <summary>
		/// Writes the JSON value delimiter.
		/// </summary>
		// Token: 0x0600036F RID: 879 RVA: 0x0000D26D File Offset: 0x0000B46D
		protected override void WriteValueDelimiter()
		{
			this._writer.Write(',');
		}

		/// <summary>
		/// Writes an indent space.
		/// </summary>
		// Token: 0x06000370 RID: 880 RVA: 0x0000D27C File Offset: 0x0000B47C
		protected override void WriteIndentSpace()
		{
			this._writer.Write(' ');
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000D28B File Offset: 0x0000B48B
		private void WriteValueInternal(string value, JsonToken token)
		{
			this._writer.Write(value);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Object" /> value.
		/// An error will raised if the value cannot be written as a single JSON token.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Object" /> value to write.</param>
		// Token: 0x06000372 RID: 882 RVA: 0x0000D29C File Offset: 0x0000B49C
		public override void WriteValue(object value)
		{
			if (value is BigInteger)
			{
				base.InternalWriteValue(JsonToken.Integer);
				this.WriteValueInternal(((BigInteger)value).ToString(CultureInfo.InvariantCulture), JsonToken.String);
				return;
			}
			base.WriteValue(value);
		}

		/// <summary>
		/// Writes a null value.
		/// </summary>
		// Token: 0x06000373 RID: 883 RVA: 0x0000D2DB File Offset: 0x0000B4DB
		public override void WriteNull()
		{
			base.InternalWriteValue(JsonToken.Null);
			this.WriteValueInternal(JsonConvert.Null, JsonToken.Null);
		}

		/// <summary>
		/// Writes an undefined value.
		/// </summary>
		// Token: 0x06000374 RID: 884 RVA: 0x0000D2F2 File Offset: 0x0000B4F2
		public override void WriteUndefined()
		{
			base.InternalWriteValue(JsonToken.Undefined);
			this.WriteValueInternal(JsonConvert.Undefined, JsonToken.Undefined);
		}

		/// <summary>
		/// Writes raw JSON.
		/// </summary>
		/// <param name="json">The raw JSON to write.</param>
		// Token: 0x06000375 RID: 885 RVA: 0x0000D309 File Offset: 0x0000B509
		public override void WriteRaw(string json)
		{
			base.InternalWriteRaw();
			this._writer.Write(json);
		}

		/// <summary>
		/// Writes a <see cref="T:System.String" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.String" /> value to write.</param>
		// Token: 0x06000376 RID: 886 RVA: 0x0000D31D File Offset: 0x0000B51D
		public override void WriteValue(string value)
		{
			base.InternalWriteValue(JsonToken.String);
			if (value == null)
			{
				this.WriteValueInternal(JsonConvert.Null, JsonToken.Null);
				return;
			}
			this.WriteEscapedString(value);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000D33F File Offset: 0x0000B53F
		private void WriteEscapedString(string value)
		{
			this.EnsureWriteBuffer();
			JavaScriptUtils.WriteEscapedJavaScriptString(this._writer, value, this._quoteChar, true, this._charEscapeFlags, base.StringEscapeHandling, ref this._writeBuffer);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Int32" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Int32" /> value to write.</param>
		// Token: 0x06000378 RID: 888 RVA: 0x0000D36C File Offset: 0x0000B56C
		public override void WriteValue(int value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue((long)value);
		}

		/// <summary>
		/// Writes a <see cref="T:System.UInt32" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.UInt32" /> value to write.</param>
		// Token: 0x06000379 RID: 889 RVA: 0x0000D37D File Offset: 0x0000B57D
		[CLSCompliant(false)]
		public override void WriteValue(uint value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue((long)((ulong)value));
		}

		/// <summary>
		/// Writes a <see cref="T:System.Int64" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Int64" /> value to write.</param>
		// Token: 0x0600037A RID: 890 RVA: 0x0000D38E File Offset: 0x0000B58E
		public override void WriteValue(long value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue(value);
		}

		/// <summary>
		/// Writes a <see cref="T:System.UInt64" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.UInt64" /> value to write.</param>
		// Token: 0x0600037B RID: 891 RVA: 0x0000D39E File Offset: 0x0000B59E
		[CLSCompliant(false)]
		public override void WriteValue(ulong value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue(value);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Single" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Single" /> value to write.</param>
		// Token: 0x0600037C RID: 892 RVA: 0x0000D3AE File Offset: 0x0000B5AE
		public override void WriteValue(float value)
		{
			base.InternalWriteValue(JsonToken.Float);
			this.WriteValueInternal(JsonConvert.ToString(value, base.FloatFormatHandling, this.QuoteChar, false), JsonToken.Float);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Nullable`1" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Nullable`1" /> value to write.</param>
		// Token: 0x0600037D RID: 893 RVA: 0x0000D3D1 File Offset: 0x0000B5D1
		public override void WriteValue(float? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			base.InternalWriteValue(JsonToken.Float);
			this.WriteValueInternal(JsonConvert.ToString(value.Value, base.FloatFormatHandling, this.QuoteChar, true), JsonToken.Float);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Double" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Double" /> value to write.</param>
		// Token: 0x0600037E RID: 894 RVA: 0x0000D40A File Offset: 0x0000B60A
		public override void WriteValue(double value)
		{
			base.InternalWriteValue(JsonToken.Float);
			this.WriteValueInternal(JsonConvert.ToString(value, base.FloatFormatHandling, this.QuoteChar, false), JsonToken.Float);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Nullable`1" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Nullable`1" /> value to write.</param>
		// Token: 0x0600037F RID: 895 RVA: 0x0000D42D File Offset: 0x0000B62D
		public override void WriteValue(double? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			base.InternalWriteValue(JsonToken.Float);
			this.WriteValueInternal(JsonConvert.ToString(value.Value, base.FloatFormatHandling, this.QuoteChar, true), JsonToken.Float);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Boolean" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Boolean" /> value to write.</param>
		// Token: 0x06000380 RID: 896 RVA: 0x0000D466 File Offset: 0x0000B666
		public override void WriteValue(bool value)
		{
			base.InternalWriteValue(JsonToken.Boolean);
			this.WriteValueInternal(JsonConvert.ToString(value), JsonToken.Boolean);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Int16" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Int16" /> value to write.</param>
		// Token: 0x06000381 RID: 897 RVA: 0x0000D47E File Offset: 0x0000B67E
		public override void WriteValue(short value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue((long)value);
		}

		/// <summary>
		/// Writes a <see cref="T:System.UInt16" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.UInt16" /> value to write.</param>
		// Token: 0x06000382 RID: 898 RVA: 0x0000D48F File Offset: 0x0000B68F
		[CLSCompliant(false)]
		public override void WriteValue(ushort value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue((long)((ulong)value));
		}

		/// <summary>
		/// Writes a <see cref="T:System.Char" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Char" /> value to write.</param>
		// Token: 0x06000383 RID: 899 RVA: 0x0000D4A0 File Offset: 0x0000B6A0
		public override void WriteValue(char value)
		{
			base.InternalWriteValue(JsonToken.String);
			this.WriteValueInternal(JsonConvert.ToString(value), JsonToken.String);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Byte" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Byte" /> value to write.</param>
		// Token: 0x06000384 RID: 900 RVA: 0x0000D4B8 File Offset: 0x0000B6B8
		public override void WriteValue(byte value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue((long)((ulong)value));
		}

		/// <summary>
		/// Writes a <see cref="T:System.SByte" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.SByte" /> value to write.</param>
		// Token: 0x06000385 RID: 901 RVA: 0x0000D4C9 File Offset: 0x0000B6C9
		[CLSCompliant(false)]
		public override void WriteValue(sbyte value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue((long)value);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Decimal" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Decimal" /> value to write.</param>
		// Token: 0x06000386 RID: 902 RVA: 0x0000D4DA File Offset: 0x0000B6DA
		public override void WriteValue(decimal value)
		{
			base.InternalWriteValue(JsonToken.Float);
			this.WriteValueInternal(JsonConvert.ToString(value), JsonToken.Float);
		}

		/// <summary>
		/// Writes a <see cref="T:System.DateTime" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.DateTime" /> value to write.</param>
		// Token: 0x06000387 RID: 903 RVA: 0x0000D4F0 File Offset: 0x0000B6F0
		public override void WriteValue(DateTime value)
		{
			base.InternalWriteValue(JsonToken.Date);
			value = DateTimeUtils.EnsureDateTime(value, base.DateTimeZoneHandling);
			if (string.IsNullOrEmpty(base.DateFormatString))
			{
				this.EnsureWriteBuffer();
				int num = 0;
				this._writeBuffer[num++] = this._quoteChar;
				num = DateTimeUtils.WriteDateTimeString(this._writeBuffer, num, value, default(TimeSpan?), value.Kind, base.DateFormatHandling);
				this._writeBuffer[num++] = this._quoteChar;
				this._writer.Write(this._writeBuffer, 0, num);
				return;
			}
			this._writer.Write(this._quoteChar);
			this._writer.Write(value.ToString(base.DateFormatString, base.Culture));
			this._writer.Write(this._quoteChar);
		}

		/// <summary>
		/// Writes a <see cref="T:Byte[]" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:Byte[]" /> value to write.</param>
		// Token: 0x06000388 RID: 904 RVA: 0x0000D5C4 File Offset: 0x0000B7C4
		public override void WriteValue(byte[] value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			base.InternalWriteValue(JsonToken.Bytes);
			this._writer.Write(this._quoteChar);
			this.Base64Encoder.Encode(value, 0, value.Length);
			this.Base64Encoder.Flush();
			this._writer.Write(this._quoteChar);
		}

		/// <summary>
		/// Writes a <see cref="T:System.DateTimeOffset" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.DateTimeOffset" /> value to write.</param>
		// Token: 0x06000389 RID: 905 RVA: 0x0000D620 File Offset: 0x0000B820
		public override void WriteValue(DateTimeOffset value)
		{
			base.InternalWriteValue(JsonToken.Date);
			if (string.IsNullOrEmpty(base.DateFormatString))
			{
				this.EnsureWriteBuffer();
				int num = 0;
				this._writeBuffer[num++] = this._quoteChar;
				num = DateTimeUtils.WriteDateTimeString(this._writeBuffer, num, (base.DateFormatHandling == DateFormatHandling.IsoDateFormat) ? value.DateTime : value.UtcDateTime, new TimeSpan?(value.Offset), 2, base.DateFormatHandling);
				this._writeBuffer[num++] = this._quoteChar;
				this._writer.Write(this._writeBuffer, 0, num);
				return;
			}
			this._writer.Write(this._quoteChar);
			this._writer.Write(value.ToString(base.DateFormatString, base.Culture));
			this._writer.Write(this._quoteChar);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Guid" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Guid" /> value to write.</param>
		// Token: 0x0600038A RID: 906 RVA: 0x0000D6FA File Offset: 0x0000B8FA
		public override void WriteValue(Guid value)
		{
			base.InternalWriteValue(JsonToken.String);
			this.WriteValueInternal(JsonConvert.ToString(value, this._quoteChar), JsonToken.String);
		}

		/// <summary>
		/// Writes a <see cref="T:System.TimeSpan" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.TimeSpan" /> value to write.</param>
		// Token: 0x0600038B RID: 907 RVA: 0x0000D718 File Offset: 0x0000B918
		public override void WriteValue(TimeSpan value)
		{
			base.InternalWriteValue(JsonToken.String);
			this.WriteValueInternal(JsonConvert.ToString(value, this._quoteChar), JsonToken.String);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Uri" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Uri" /> value to write.</param>
		// Token: 0x0600038C RID: 908 RVA: 0x0000D736 File Offset: 0x0000B936
		public override void WriteValue(Uri value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			base.InternalWriteValue(JsonToken.String);
			this.WriteValueInternal(JsonConvert.ToString(value, this._quoteChar), JsonToken.String);
		}

		/// <summary>
		/// Writes out a comment <code>/*...*/</code> containing the specified text. 
		/// </summary>
		/// <param name="text">Text to place inside the comment.</param>
		// Token: 0x0600038D RID: 909 RVA: 0x0000D764 File Offset: 0x0000B964
		public override void WriteComment(string text)
		{
			base.InternalWriteComment();
			this._writer.Write("/*");
			this._writer.Write(text);
			this._writer.Write("*/");
		}

		/// <summary>
		/// Writes out the given white space.
		/// </summary>
		/// <param name="ws">The string of white space characters.</param>
		// Token: 0x0600038E RID: 910 RVA: 0x0000D798 File Offset: 0x0000B998
		public override void WriteWhitespace(string ws)
		{
			base.InternalWriteWhitespace(ws);
			this._writer.Write(ws);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000D7AD File Offset: 0x0000B9AD
		private void EnsureWriteBuffer()
		{
			if (this._writeBuffer == null)
			{
				this._writeBuffer = new char[64];
			}
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000D7C4 File Offset: 0x0000B9C4
		private void WriteIntegerValue(long value)
		{
			this.EnsureWriteBuffer();
			if (value >= 0L && value <= 9L)
			{
				this._writer.Write((char)(48L + value));
				return;
			}
			ulong uvalue = (ulong)((value < 0L) ? (-(ulong)value) : value);
			if (value < 0L)
			{
				this._writer.Write('-');
			}
			this.WriteIntegerValue(uvalue);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000D818 File Offset: 0x0000BA18
		private void WriteIntegerValue(ulong uvalue)
		{
			this.EnsureWriteBuffer();
			if (uvalue <= 9UL)
			{
				this._writer.Write((char)(48UL + uvalue));
				return;
			}
			int num = MathUtils.IntLength(uvalue);
			int num2 = 0;
			do
			{
				this._writeBuffer[num - ++num2] = (char)(48UL + uvalue % 10UL);
				uvalue /= 10UL;
			}
			while (uvalue != 0UL);
			this._writer.Write(this._writeBuffer, 0, num2);
		}

		// Token: 0x0400014E RID: 334
		private readonly TextWriter _writer;

		// Token: 0x0400014F RID: 335
		private Base64Encoder _base64Encoder;

		// Token: 0x04000150 RID: 336
		private char _indentChar;

		// Token: 0x04000151 RID: 337
		private int _indentation;

		// Token: 0x04000152 RID: 338
		private char _quoteChar;

		// Token: 0x04000153 RID: 339
		private bool _quoteName;

		// Token: 0x04000154 RID: 340
		private bool[] _charEscapeFlags;

		// Token: 0x04000155 RID: 341
		private char[] _writeBuffer;
	}
}
