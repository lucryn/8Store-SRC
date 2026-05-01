using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	/// <summary>
	/// Represents a reader that provides fast, non-cached, forward-only access to serialized Json data.
	/// </summary>
	// Token: 0x02000005 RID: 5
	public abstract class JsonReader : IDisposable
	{
		/// <summary>
		/// Gets the current reader state.
		/// </summary>
		/// <value>The current reader state.</value>
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002883 File Offset: 0x00000A83
		protected JsonReader.State CurrentState
		{
			get
			{
				return this._currentState;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the underlying stream or
		/// <see cref="T:System.IO.TextReader" /> should be closed when the reader is closed.
		/// </summary>
		/// <value>
		/// true to close the underlying stream or <see cref="T:System.IO.TextReader" /> when
		/// the reader is closed; otherwise false. The default is true.
		/// </value>
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000288B File Offset: 0x00000A8B
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002893 File Offset: 0x00000A93
		public bool CloseInput { get; set; }

		/// <summary>
		/// Gets the quotation mark character used to enclose the value of a string.
		/// </summary>
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000289C File Offset: 0x00000A9C
		// (set) Token: 0x06000015 RID: 21 RVA: 0x000028A4 File Offset: 0x00000AA4
		public virtual char QuoteChar
		{
			get
			{
				return this._quoteChar;
			}
			protected internal set
			{
				this._quoteChar = value;
			}
		}

		/// <summary>
		/// Get or set how <see cref="T:System.DateTime" /> time zones are handling when reading JSON.
		/// </summary>
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000028AD File Offset: 0x00000AAD
		// (set) Token: 0x06000017 RID: 23 RVA: 0x000028B5 File Offset: 0x00000AB5
		public DateTimeZoneHandling DateTimeZoneHandling
		{
			get
			{
				return this._dateTimeZoneHandling;
			}
			set
			{
				this._dateTimeZoneHandling = value;
			}
		}

		/// <summary>
		/// Get or set how date formatted strings, e.g. "\/Date(1198908717056)\/" and "2012-03-21T05:40Z", are parsed when reading JSON.
		/// </summary>
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000028BE File Offset: 0x00000ABE
		// (set) Token: 0x06000019 RID: 25 RVA: 0x000028C6 File Offset: 0x00000AC6
		public DateParseHandling DateParseHandling
		{
			get
			{
				return this._dateParseHandling;
			}
			set
			{
				this._dateParseHandling = value;
			}
		}

		/// <summary>
		/// Get or set how floating point numbers, e.g. 1.0 and 9.9, are parsed when reading JSON text.
		/// </summary>
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000028CF File Offset: 0x00000ACF
		// (set) Token: 0x0600001B RID: 27 RVA: 0x000028D7 File Offset: 0x00000AD7
		public FloatParseHandling FloatParseHandling
		{
			get
			{
				return this._floatParseHandling;
			}
			set
			{
				this._floatParseHandling = value;
			}
		}

		/// <summary>
		/// Gets or sets the maximum depth allowed when reading JSON. Reading past this depth will throw a <see cref="T:Newtonsoft.Json.JsonReaderException" />.
		/// </summary>
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000028E0 File Offset: 0x00000AE0
		// (set) Token: 0x0600001D RID: 29 RVA: 0x000028E8 File Offset: 0x00000AE8
		public int? MaxDepth
		{
			get
			{
				return this._maxDepth;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentException("Value must be positive.", "value");
				}
				this._maxDepth = value;
			}
		}

		/// <summary>
		/// Gets the type of the current JSON token. 
		/// </summary>
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002924 File Offset: 0x00000B24
		public virtual JsonToken TokenType
		{
			get
			{
				return this._tokenType;
			}
		}

		/// <summary>
		/// Gets the text value of the current JSON token.
		/// </summary>
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001F RID: 31 RVA: 0x0000292C File Offset: 0x00000B2C
		public virtual object Value
		{
			get
			{
				return this._value;
			}
		}

		/// <summary>
		/// Gets The Common Language Runtime (CLR) type for the current JSON token.
		/// </summary>
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002934 File Offset: 0x00000B34
		public virtual Type ValueType
		{
			get
			{
				if (this._value == null)
				{
					return null;
				}
				return this._value.GetType();
			}
		}

		/// <summary>
		/// Gets the depth of the current token in the JSON document.
		/// </summary>
		/// <value>The depth of the current token in the JSON document.</value>
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000021 RID: 33 RVA: 0x0000294C File Offset: 0x00000B4C
		public virtual int Depth
		{
			get
			{
				int count = this._stack.Count;
				if (JsonReader.IsStartToken(this.TokenType) || this._currentPosition.Type == JsonContainerType.None)
				{
					return count;
				}
				return count + 1;
			}
		}

		/// <summary>
		/// Gets the path of the current JSON token. 
		/// </summary>
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002984 File Offset: 0x00000B84
		public virtual string Path
		{
			get
			{
				if (this._currentPosition.Type == JsonContainerType.None)
				{
					return string.Empty;
				}
				bool flag = this._currentState != JsonReader.State.ArrayStart && this._currentState != JsonReader.State.ConstructorStart && this._currentState != JsonReader.State.ObjectStart;
				IEnumerable<JsonPosition> positions = (!flag) ? this._stack : Enumerable.Concat<JsonPosition>(this._stack, new JsonPosition[]
				{
					this._currentPosition
				});
				return JsonPosition.BuildPath(positions);
			}
		}

		/// <summary>
		/// Gets or sets the culture used when reading JSON. Defaults to <see cref="P:System.Globalization.CultureInfo.InvariantCulture" />.
		/// </summary>
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000029FE File Offset: 0x00000BFE
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002A0F File Offset: 0x00000C0F
		public CultureInfo Culture
		{
			get
			{
				return this._culture ?? CultureInfo.InvariantCulture;
			}
			set
			{
				this._culture = value;
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002A18 File Offset: 0x00000C18
		internal JsonPosition GetPosition(int depth)
		{
			if (depth < this._stack.Count)
			{
				return this._stack[depth];
			}
			return this._currentPosition;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.JsonReader" /> class with the specified <see cref="T:System.IO.TextReader" />.
		/// </summary>
		// Token: 0x06000026 RID: 38 RVA: 0x00002A3B File Offset: 0x00000C3B
		protected JsonReader()
		{
			this._currentState = JsonReader.State.Start;
			this._stack = new List<JsonPosition>(4);
			this._dateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind;
			this._dateParseHandling = DateParseHandling.DateTime;
			this._floatParseHandling = FloatParseHandling.Double;
			this.CloseInput = true;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002A74 File Offset: 0x00000C74
		private void Push(JsonContainerType value)
		{
			this.UpdateScopeWithFinishedValue();
			if (this._currentPosition.Type == JsonContainerType.None)
			{
				this._currentPosition = new JsonPosition(value);
				return;
			}
			this._stack.Add(this._currentPosition);
			this._currentPosition = new JsonPosition(value);
			if (this._maxDepth != null && this.Depth + 1 > this._maxDepth && !this._hasExceededMaxDepth)
			{
				this._hasExceededMaxDepth = true;
				throw JsonReaderException.Create(this, "The reader's MaxDepth of {0} has been exceeded.".FormatWith(CultureInfo.InvariantCulture, this._maxDepth));
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002B24 File Offset: 0x00000D24
		private JsonContainerType Pop()
		{
			JsonPosition currentPosition;
			if (this._stack.Count > 0)
			{
				currentPosition = this._currentPosition;
				this._currentPosition = this._stack[this._stack.Count - 1];
				this._stack.RemoveAt(this._stack.Count - 1);
			}
			else
			{
				currentPosition = this._currentPosition;
				this._currentPosition = default(JsonPosition);
			}
			if (this._maxDepth != null && this.Depth <= this._maxDepth)
			{
				this._hasExceededMaxDepth = false;
			}
			return currentPosition.Type;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002BD0 File Offset: 0x00000DD0
		private JsonContainerType Peek()
		{
			return this._currentPosition.Type;
		}

		/// <summary>
		/// Reads the next JSON token from the stream.
		/// </summary>
		/// <returns>true if the next token was read successfully; false if there are no more tokens to read.</returns>
		// Token: 0x0600002A RID: 42
		public abstract bool Read();

		/// <summary>
		/// Reads the next JSON token from the stream as a <see cref="T:System.Nullable`1" />.
		/// </summary>
		/// <returns>A <see cref="T:System.Nullable`1" />. This method will return <c>null</c> at the end of an array.</returns>
		// Token: 0x0600002B RID: 43
		public abstract int? ReadAsInt32();

		/// <summary>
		/// Reads the next JSON token from the stream as a <see cref="T:System.String" />.
		/// </summary>
		/// <returns>A <see cref="T:System.String" />. This method will return <c>null</c> at the end of an array.</returns>
		// Token: 0x0600002C RID: 44
		public abstract string ReadAsString();

		/// <summary>
		/// Reads the next JSON token from the stream as a <see cref="T:Byte[]" />.
		/// </summary>
		/// <returns>A <see cref="T:Byte[]" /> or a null reference if the next JSON token is null. This method will return <c>null</c> at the end of an array.</returns>
		// Token: 0x0600002D RID: 45
		public abstract byte[] ReadAsBytes();

		/// <summary>
		/// Reads the next JSON token from the stream as a <see cref="T:System.Nullable`1" />.
		/// </summary>
		/// <returns>A <see cref="T:System.Nullable`1" />. This method will return <c>null</c> at the end of an array.</returns>
		// Token: 0x0600002E RID: 46
		public abstract decimal? ReadAsDecimal();

		/// <summary>
		/// Reads the next JSON token from the stream as a <see cref="T:System.Nullable`1" />.
		/// </summary>
		/// <returns>A <see cref="T:System.String" />. This method will return <c>null</c> at the end of an array.</returns>
		// Token: 0x0600002F RID: 47
		public abstract DateTime? ReadAsDateTime();

		/// <summary>
		/// Reads the next JSON token from the stream as a <see cref="T:System.Nullable`1" />.
		/// </summary>
		/// <returns>A <see cref="T:System.Nullable`1" />. This method will return <c>null</c> at the end of an array.</returns>
		// Token: 0x06000030 RID: 48
		public abstract DateTimeOffset? ReadAsDateTimeOffset();

		// Token: 0x06000031 RID: 49 RVA: 0x00002BDD File Offset: 0x00000DDD
		internal virtual bool ReadInternal()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002BE4 File Offset: 0x00000DE4
		internal DateTimeOffset? ReadAsDateTimeOffsetInternal()
		{
			this._readType = ReadType.ReadAsDateTimeOffset;
			while (this.ReadInternal())
			{
				JsonToken tokenType = this.TokenType;
				if (tokenType != JsonToken.Comment)
				{
					if (tokenType == JsonToken.Date)
					{
						if (this.Value is DateTime)
						{
							this.SetToken(JsonToken.Date, new DateTimeOffset((DateTime)this.Value));
						}
						return new DateTimeOffset?((DateTimeOffset)this.Value);
					}
					if (tokenType == JsonToken.Null)
					{
						return default(DateTimeOffset?);
					}
					if (tokenType == JsonToken.String)
					{
						string text = (string)this.Value;
						if (string.IsNullOrEmpty(text))
						{
							this.SetToken(JsonToken.Null);
							return default(DateTimeOffset?);
						}
						DateTimeOffset dateTimeOffset;
						if (DateTimeOffset.TryParse(text, this.Culture, 128, ref dateTimeOffset))
						{
							this.SetToken(JsonToken.Date, dateTimeOffset);
							return new DateTimeOffset?(dateTimeOffset);
						}
						throw JsonReaderException.Create(this, "Could not convert string to DateTimeOffset: {0}.".FormatWith(CultureInfo.InvariantCulture, this.Value));
					}
					else
					{
						if (tokenType == JsonToken.EndArray)
						{
							return default(DateTimeOffset?);
						}
						throw JsonReaderException.Create(this, "Error reading date. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, tokenType));
					}
				}
			}
			this.SetToken(JsonToken.None);
			return default(DateTimeOffset?);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002D0C File Offset: 0x00000F0C
		internal byte[] ReadAsBytesInternal()
		{
			this._readType = ReadType.ReadAsBytes;
			while (this.ReadInternal())
			{
				JsonToken tokenType = this.TokenType;
				if (tokenType != JsonToken.Comment)
				{
					if (this.IsWrappedInTypeObject())
					{
						byte[] array = this.ReadAsBytes();
						this.ReadInternal();
						this.SetToken(JsonToken.Bytes, array);
						return array;
					}
					if (tokenType == JsonToken.String)
					{
						string text = (string)this.Value;
						byte[] array2 = (text.Length == 0) ? new byte[0] : Convert.FromBase64String(text);
						this.SetToken(JsonToken.Bytes, array2);
						return array2;
					}
					if (tokenType == JsonToken.Null)
					{
						return null;
					}
					if (tokenType == JsonToken.Bytes)
					{
						return (byte[])this.Value;
					}
					if (tokenType == JsonToken.StartArray)
					{
						List<byte> list = new List<byte>();
						while (this.ReadInternal())
						{
							tokenType = this.TokenType;
							JsonToken jsonToken = tokenType;
							switch (jsonToken)
							{
							case JsonToken.Comment:
								continue;
							case JsonToken.Raw:
								break;
							case JsonToken.Integer:
								list.Add(Convert.ToByte(this.Value, CultureInfo.InvariantCulture));
								continue;
							default:
								if (jsonToken == JsonToken.EndArray)
								{
									byte[] array3 = list.ToArray();
									this.SetToken(JsonToken.Bytes, array3);
									return array3;
								}
								break;
							}
							throw JsonReaderException.Create(this, "Unexpected token when reading bytes: {0}.".FormatWith(CultureInfo.InvariantCulture, tokenType));
						}
						throw JsonReaderException.Create(this, "Unexpected end when reading bytes.");
					}
					if (tokenType == JsonToken.EndArray)
					{
						return null;
					}
					throw JsonReaderException.Create(this, "Error reading bytes. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, tokenType));
				}
			}
			this.SetToken(JsonToken.None);
			return null;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002E60 File Offset: 0x00001060
		internal decimal? ReadAsDecimalInternal()
		{
			this._readType = ReadType.ReadAsDecimal;
			while (this.ReadInternal())
			{
				JsonToken tokenType = this.TokenType;
				if (tokenType != JsonToken.Comment)
				{
					if (tokenType == JsonToken.Integer || tokenType == JsonToken.Float)
					{
						if (!(this.Value is decimal))
						{
							this.SetToken(JsonToken.Float, Convert.ToDecimal(this.Value, CultureInfo.InvariantCulture));
						}
						return new decimal?((decimal)this.Value);
					}
					if (tokenType == JsonToken.Null)
					{
						return default(decimal?);
					}
					if (tokenType == JsonToken.String)
					{
						string text = (string)this.Value;
						if (string.IsNullOrEmpty(text))
						{
							this.SetToken(JsonToken.Null);
							return default(decimal?);
						}
						decimal num;
						if (decimal.TryParse(text, 111, this.Culture, ref num))
						{
							this.SetToken(JsonToken.Float, num);
							return new decimal?(num);
						}
						throw JsonReaderException.Create(this, "Could not convert string to decimal: {0}.".FormatWith(CultureInfo.InvariantCulture, this.Value));
					}
					else
					{
						if (tokenType == JsonToken.EndArray)
						{
							return default(decimal?);
						}
						throw JsonReaderException.Create(this, "Error reading decimal. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, tokenType));
					}
				}
			}
			this.SetToken(JsonToken.None);
			return default(decimal?);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002F84 File Offset: 0x00001184
		internal int? ReadAsInt32Internal()
		{
			this._readType = ReadType.ReadAsInt32;
			while (this.ReadInternal())
			{
				JsonToken tokenType = this.TokenType;
				if (tokenType != JsonToken.Comment)
				{
					if (tokenType == JsonToken.Integer || tokenType == JsonToken.Float)
					{
						if (!(this.Value is int))
						{
							this.SetToken(JsonToken.Integer, Convert.ToInt32(this.Value, CultureInfo.InvariantCulture));
						}
						return new int?((int)this.Value);
					}
					if (tokenType == JsonToken.Null)
					{
						return default(int?);
					}
					if (tokenType == JsonToken.String)
					{
						string text = (string)this.Value;
						if (string.IsNullOrEmpty(text))
						{
							this.SetToken(JsonToken.Null);
							return default(int?);
						}
						int num;
						if (int.TryParse(text, 7, this.Culture, ref num))
						{
							this.SetToken(JsonToken.Integer, num);
							return new int?(num);
						}
						throw JsonReaderException.Create(this, "Could not convert string to integer: {0}.".FormatWith(CultureInfo.InvariantCulture, this.Value));
					}
					else
					{
						if (tokenType == JsonToken.EndArray)
						{
							return default(int?);
						}
						throw JsonReaderException.Create(this, "Error reading integer. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, this.TokenType));
					}
				}
			}
			this.SetToken(JsonToken.None);
			return default(int?);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000030AC File Offset: 0x000012AC
		internal string ReadAsStringInternal()
		{
			this._readType = ReadType.ReadAsString;
			while (this.ReadInternal())
			{
				JsonToken tokenType = this.TokenType;
				if (tokenType != JsonToken.Comment)
				{
					if (tokenType == JsonToken.String)
					{
						return (string)this.Value;
					}
					if (tokenType == JsonToken.Null)
					{
						return null;
					}
					if (JsonReader.IsPrimitiveToken(tokenType) && this.Value != null)
					{
						string text;
						if (this.Value is IFormattable)
						{
							text = ((IFormattable)this.Value).ToString(null, this.Culture);
						}
						else
						{
							text = this.Value.ToString();
						}
						this.SetToken(JsonToken.String, text);
						return text;
					}
					if (tokenType == JsonToken.EndArray)
					{
						return null;
					}
					throw JsonReaderException.Create(this, "Error reading string. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, tokenType));
				}
			}
			this.SetToken(JsonToken.None);
			return null;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003164 File Offset: 0x00001364
		internal DateTime? ReadAsDateTimeInternal()
		{
			this._readType = ReadType.ReadAsDateTime;
			while (this.ReadInternal())
			{
				if (this.TokenType != JsonToken.Comment)
				{
					if (this.TokenType == JsonToken.Date)
					{
						return new DateTime?((DateTime)this.Value);
					}
					if (this.TokenType == JsonToken.Null)
					{
						return default(DateTime?);
					}
					if (this.TokenType == JsonToken.String)
					{
						string text = (string)this.Value;
						if (string.IsNullOrEmpty(text))
						{
							this.SetToken(JsonToken.Null);
							return default(DateTime?);
						}
						DateTime dateTime;
						if (DateTime.TryParse(text, this.Culture, 128, ref dateTime))
						{
							dateTime = DateTimeUtils.EnsureDateTime(dateTime, this.DateTimeZoneHandling);
							this.SetToken(JsonToken.Date, dateTime);
							return new DateTime?(dateTime);
						}
						throw JsonReaderException.Create(this, "Could not convert string to DateTime: {0}.".FormatWith(CultureInfo.InvariantCulture, this.Value));
					}
					else
					{
						if (this.TokenType == JsonToken.EndArray)
						{
							return default(DateTime?);
						}
						throw JsonReaderException.Create(this, "Error reading date. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, this.TokenType));
					}
				}
			}
			this.SetToken(JsonToken.None);
			return default(DateTime?);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003284 File Offset: 0x00001484
		private bool IsWrappedInTypeObject()
		{
			this._readType = ReadType.Read;
			if (this.TokenType != JsonToken.StartObject)
			{
				return false;
			}
			if (!this.ReadInternal())
			{
				throw JsonReaderException.Create(this, "Unexpected end when reading bytes.");
			}
			if (this.Value.ToString() == "$type")
			{
				this.ReadInternal();
				if (this.Value != null && this.Value.ToString().StartsWith("System.Byte[]"))
				{
					this.ReadInternal();
					if (this.Value.ToString() == "$value")
					{
						return true;
					}
				}
			}
			throw JsonReaderException.Create(this, "Error reading bytes. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, JsonToken.StartObject));
		}

		/// <summary>
		/// Skips the children of the current token.
		/// </summary>
		// Token: 0x06000039 RID: 57 RVA: 0x00003334 File Offset: 0x00001534
		public void Skip()
		{
			if (this.TokenType == JsonToken.PropertyName)
			{
				this.Read();
			}
			if (JsonReader.IsStartToken(this.TokenType))
			{
				int depth = this.Depth;
				while (this.Read() && depth < this.Depth)
				{
				}
			}
		}

		/// <summary>
		/// Sets the current token.
		/// </summary>
		/// <param name="newToken">The new token.</param>
		// Token: 0x0600003A RID: 58 RVA: 0x00003376 File Offset: 0x00001576
		protected void SetToken(JsonToken newToken)
		{
			this.SetToken(newToken, null);
		}

		/// <summary>
		/// Sets the current token and value.
		/// </summary>
		/// <param name="newToken">The new token.</param>
		/// <param name="value">The value.</param>
		// Token: 0x0600003B RID: 59 RVA: 0x00003380 File Offset: 0x00001580
		protected void SetToken(JsonToken newToken, object value)
		{
			this._tokenType = newToken;
			this._value = value;
			switch (newToken)
			{
			case JsonToken.StartObject:
				this._currentState = JsonReader.State.ObjectStart;
				this.Push(JsonContainerType.Object);
				return;
			case JsonToken.StartArray:
				this._currentState = JsonReader.State.ArrayStart;
				this.Push(JsonContainerType.Array);
				return;
			case JsonToken.StartConstructor:
				this._currentState = JsonReader.State.ConstructorStart;
				this.Push(JsonContainerType.Constructor);
				return;
			case JsonToken.PropertyName:
				this._currentState = JsonReader.State.Property;
				this._currentPosition.PropertyName = (string)value;
				return;
			case JsonToken.Comment:
				break;
			case JsonToken.Raw:
			case JsonToken.Integer:
			case JsonToken.Float:
			case JsonToken.String:
			case JsonToken.Boolean:
			case JsonToken.Null:
			case JsonToken.Undefined:
			case JsonToken.Date:
			case JsonToken.Bytes:
				this._currentState = ((this.Peek() != JsonContainerType.None) ? JsonReader.State.PostValue : JsonReader.State.Finished);
				this.UpdateScopeWithFinishedValue();
				break;
			case JsonToken.EndObject:
				this.ValidateEnd(JsonToken.EndObject);
				return;
			case JsonToken.EndArray:
				this.ValidateEnd(JsonToken.EndArray);
				return;
			case JsonToken.EndConstructor:
				this.ValidateEnd(JsonToken.EndConstructor);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003465 File Offset: 0x00001665
		private void UpdateScopeWithFinishedValue()
		{
			if (this._currentPosition.HasIndex)
			{
				this._currentPosition.Position = this._currentPosition.Position + 1;
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003488 File Offset: 0x00001688
		private void ValidateEnd(JsonToken endToken)
		{
			JsonContainerType jsonContainerType = this.Pop();
			if (this.GetTypeForCloseToken(endToken) != jsonContainerType)
			{
				throw JsonReaderException.Create(this, "JsonToken {0} is not valid for closing JsonType {1}.".FormatWith(CultureInfo.InvariantCulture, endToken, jsonContainerType));
			}
			this._currentState = ((this.Peek() != JsonContainerType.None) ? JsonReader.State.PostValue : JsonReader.State.Finished);
		}

		/// <summary>
		/// Sets the state based on current token type.
		/// </summary>
		// Token: 0x0600003E RID: 62 RVA: 0x000034DC File Offset: 0x000016DC
		protected void SetStateBasedOnCurrent()
		{
			JsonContainerType jsonContainerType = this.Peek();
			switch (jsonContainerType)
			{
			case JsonContainerType.None:
				this._currentState = JsonReader.State.Finished;
				return;
			case JsonContainerType.Object:
				this._currentState = JsonReader.State.Object;
				return;
			case JsonContainerType.Array:
				this._currentState = JsonReader.State.Array;
				return;
			case JsonContainerType.Constructor:
				this._currentState = JsonReader.State.Constructor;
				return;
			default:
				throw JsonReaderException.Create(this, "While setting the reader state back to current object an unexpected JsonType was encountered: {0}".FormatWith(CultureInfo.InvariantCulture, jsonContainerType));
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003548 File Offset: 0x00001748
		internal static bool IsPrimitiveToken(JsonToken token)
		{
			switch (token)
			{
			case JsonToken.Integer:
			case JsonToken.Float:
			case JsonToken.String:
			case JsonToken.Boolean:
			case JsonToken.Null:
			case JsonToken.Undefined:
			case JsonToken.Date:
			case JsonToken.Bytes:
				return true;
			}
			return false;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003590 File Offset: 0x00001790
		internal static bool IsStartToken(JsonToken token)
		{
			switch (token)
			{
			case JsonToken.StartObject:
			case JsonToken.StartArray:
			case JsonToken.StartConstructor:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000035B8 File Offset: 0x000017B8
		private JsonContainerType GetTypeForCloseToken(JsonToken token)
		{
			switch (token)
			{
			case JsonToken.EndObject:
				return JsonContainerType.Object;
			case JsonToken.EndArray:
				return JsonContainerType.Array;
			case JsonToken.EndConstructor:
				return JsonContainerType.Constructor;
			default:
				throw JsonReaderException.Create(this, "Not a valid close JsonToken: {0}".FormatWith(CultureInfo.InvariantCulture, token));
			}
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		// Token: 0x06000042 RID: 66 RVA: 0x000035FF File Offset: 0x000017FF
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources
		/// </summary>
		/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
		// Token: 0x06000043 RID: 67 RVA: 0x00003608 File Offset: 0x00001808
		protected virtual void Dispose(bool disposing)
		{
			if (this._currentState != JsonReader.State.Closed && disposing)
			{
				this.Close();
			}
		}

		/// <summary>
		/// Changes the <see cref="T:Newtonsoft.Json.JsonReader.State" /> to Closed. 
		/// </summary>
		// Token: 0x06000044 RID: 68 RVA: 0x0000361C File Offset: 0x0000181C
		public virtual void Close()
		{
			this._currentState = JsonReader.State.Closed;
			this._tokenType = JsonToken.None;
			this._value = null;
		}

		// Token: 0x0400000E RID: 14
		private JsonToken _tokenType;

		// Token: 0x0400000F RID: 15
		private object _value;

		// Token: 0x04000010 RID: 16
		internal char _quoteChar;

		// Token: 0x04000011 RID: 17
		internal JsonReader.State _currentState;

		// Token: 0x04000012 RID: 18
		internal ReadType _readType;

		// Token: 0x04000013 RID: 19
		private JsonPosition _currentPosition;

		// Token: 0x04000014 RID: 20
		private CultureInfo _culture;

		// Token: 0x04000015 RID: 21
		private DateTimeZoneHandling _dateTimeZoneHandling;

		// Token: 0x04000016 RID: 22
		private int? _maxDepth;

		// Token: 0x04000017 RID: 23
		private bool _hasExceededMaxDepth;

		// Token: 0x04000018 RID: 24
		internal DateParseHandling _dateParseHandling;

		// Token: 0x04000019 RID: 25
		internal FloatParseHandling _floatParseHandling;

		// Token: 0x0400001A RID: 26
		private readonly List<JsonPosition> _stack;

		/// <summary>
		/// Specifies the state of the reader.
		/// </summary>
		// Token: 0x02000006 RID: 6
		protected internal enum State
		{
			/// <summary>
			/// The Read method has not been called.
			/// </summary>
			// Token: 0x0400001D RID: 29
			Start,
			/// <summary>
			/// The end of the file has been reached successfully.
			/// </summary>
			// Token: 0x0400001E RID: 30
			Complete,
			/// <summary>
			/// Reader is at a property.
			/// </summary>
			// Token: 0x0400001F RID: 31
			Property,
			/// <summary>
			/// Reader is at the start of an object.
			/// </summary>
			// Token: 0x04000020 RID: 32
			ObjectStart,
			/// <summary>
			/// Reader is in an object.
			/// </summary>
			// Token: 0x04000021 RID: 33
			Object,
			/// <summary>
			/// Reader is at the start of an array.
			/// </summary>
			// Token: 0x04000022 RID: 34
			ArrayStart,
			/// <summary>
			/// Reader is in an array.
			/// </summary>
			// Token: 0x04000023 RID: 35
			Array,
			/// <summary>
			/// The Close method has been called.
			/// </summary>
			// Token: 0x04000024 RID: 36
			Closed,
			/// <summary>
			/// Reader has just read a value.
			/// </summary>
			// Token: 0x04000025 RID: 37
			PostValue,
			/// <summary>
			/// Reader is at the start of a constructor.
			/// </summary>
			// Token: 0x04000026 RID: 38
			ConstructorStart,
			/// <summary>
			/// Reader in a constructor.
			/// </summary>
			// Token: 0x04000027 RID: 39
			Constructor,
			/// <summary>
			/// An error occurred that prevents the read operation from continuing.
			/// </summary>
			// Token: 0x04000028 RID: 40
			Error,
			/// <summary>
			/// The end of the file has been reached successfully.
			/// </summary>
			// Token: 0x04000029 RID: 41
			Finished
		}
	}
}
