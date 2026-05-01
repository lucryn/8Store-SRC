using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	/// <summary>
	/// Represents a writer that provides a fast, non-cached, forward-only way of generating Json data.
	/// </summary>
	// Token: 0x02000013 RID: 19
	public abstract class JsonWriter : IDisposable
	{
		// Token: 0x06000095 RID: 149 RVA: 0x00004464 File Offset: 0x00002664
		internal static JsonWriter.State[][] BuildStateArray()
		{
			List<JsonWriter.State[]> list = Enumerable.ToList<JsonWriter.State[]>(JsonWriter.StateArrayTempate);
			JsonWriter.State[] array = JsonWriter.StateArrayTempate[0];
			JsonWriter.State[] array2 = JsonWriter.StateArrayTempate[7];
			foreach (object obj in EnumUtils.GetValues(typeof(JsonToken)))
			{
				JsonToken jsonToken = (JsonToken)obj;
				if (list.Count <= (int)jsonToken)
				{
					switch (jsonToken)
					{
					case JsonToken.Integer:
					case JsonToken.Float:
					case JsonToken.String:
					case JsonToken.Boolean:
					case JsonToken.Null:
					case JsonToken.Undefined:
					case JsonToken.Date:
					case JsonToken.Bytes:
						list.Add(array2);
						continue;
					}
					list.Add(array);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004534 File Offset: 0x00002734
		static JsonWriter()
		{
			JsonWriter.StateArray = JsonWriter.BuildStateArray();
		}

		/// <summary>
		/// Gets or sets a value indicating whether the underlying stream or
		/// <see cref="T:System.IO.TextReader" /> should be closed when the writer is closed.
		/// </summary>
		/// <value>
		/// true to close the underlying stream or <see cref="T:System.IO.TextReader" /> when
		/// the writer is closed; otherwise false. The default is true.
		/// </value>
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00004752 File Offset: 0x00002952
		// (set) Token: 0x06000098 RID: 152 RVA: 0x0000475A File Offset: 0x0000295A
		public bool CloseOutput { get; set; }

		/// <summary>
		/// Gets the top.
		/// </summary>
		/// <value>The top.</value>
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00004764 File Offset: 0x00002964
		protected internal int Top
		{
			get
			{
				int num = this._stack.Count;
				if (this.Peek() != JsonContainerType.None)
				{
					num++;
				}
				return num;
			}
		}

		/// <summary>
		/// Gets the state of the writer.
		/// </summary>
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600009A RID: 154 RVA: 0x0000478C File Offset: 0x0000298C
		public WriteState WriteState
		{
			get
			{
				switch (this._currentState)
				{
				case JsonWriter.State.Start:
					return WriteState.Start;
				case JsonWriter.State.Property:
					return WriteState.Property;
				case JsonWriter.State.ObjectStart:
				case JsonWriter.State.Object:
					return WriteState.Object;
				case JsonWriter.State.ArrayStart:
				case JsonWriter.State.Array:
					return WriteState.Array;
				case JsonWriter.State.ConstructorStart:
				case JsonWriter.State.Constructor:
					return WriteState.Constructor;
				case JsonWriter.State.Closed:
					return WriteState.Closed;
				case JsonWriter.State.Error:
					return WriteState.Error;
				default:
					throw JsonWriterException.Create(this, "Invalid state: " + this._currentState, null);
				}
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000047FA File Offset: 0x000029FA
		internal string ContainerPath
		{
			get
			{
				if (this._currentPosition.Type == JsonContainerType.None)
				{
					return string.Empty;
				}
				return JsonPosition.BuildPath(this._stack);
			}
		}

		/// <summary>
		/// Gets the path of the writer. 
		/// </summary>
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600009C RID: 156 RVA: 0x0000481C File Offset: 0x00002A1C
		public string Path
		{
			get
			{
				if (this._currentPosition.Type == JsonContainerType.None)
				{
					return string.Empty;
				}
				bool flag = this._currentState != JsonWriter.State.ArrayStart && this._currentState != JsonWriter.State.ConstructorStart && this._currentState != JsonWriter.State.ObjectStart;
				IEnumerable<JsonPosition> positions = (!flag) ? this._stack : Enumerable.Concat<JsonPosition>(this._stack, new JsonPosition[]
				{
					this._currentPosition
				});
				return JsonPosition.BuildPath(positions);
			}
		}

		/// <summary>
		/// Indicates how JSON text output is formatted.
		/// </summary>
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00004895 File Offset: 0x00002A95
		// (set) Token: 0x0600009E RID: 158 RVA: 0x0000489D File Offset: 0x00002A9D
		public Formatting Formatting
		{
			get
			{
				return this._formatting;
			}
			set
			{
				this._formatting = value;
			}
		}

		/// <summary>
		/// Get or set how dates are written to JSON text.
		/// </summary>
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000048A6 File Offset: 0x00002AA6
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x000048AE File Offset: 0x00002AAE
		public DateFormatHandling DateFormatHandling
		{
			get
			{
				return this._dateFormatHandling;
			}
			set
			{
				this._dateFormatHandling = value;
			}
		}

		/// <summary>
		/// Get or set how <see cref="T:System.DateTime" /> time zones are handling when writing JSON text.
		/// </summary>
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x000048B7 File Offset: 0x00002AB7
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x000048BF File Offset: 0x00002ABF
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
		/// Get or set how strings are escaped when writing JSON text.
		/// </summary>
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x000048C8 File Offset: 0x00002AC8
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x000048D0 File Offset: 0x00002AD0
		public StringEscapeHandling StringEscapeHandling
		{
			get
			{
				return this._stringEscapeHandling;
			}
			set
			{
				this._stringEscapeHandling = value;
				this.OnStringEscapeHandlingChanged();
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000048DF File Offset: 0x00002ADF
		internal virtual void OnStringEscapeHandlingChanged()
		{
		}

		/// <summary>
		/// Get or set how special floating point numbers, e.g. <see cref="F:System.Double.NaN" />,
		/// <see cref="F:System.Double.PositiveInfinity" /> and <see cref="F:System.Double.NegativeInfinity" />,
		/// are written to JSON text.
		/// </summary>
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000048E1 File Offset: 0x00002AE1
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x000048E9 File Offset: 0x00002AE9
		public FloatFormatHandling FloatFormatHandling
		{
			get
			{
				return this._floatFormatHandling;
			}
			set
			{
				this._floatFormatHandling = value;
			}
		}

		/// <summary>
		/// Get or set how <see cref="T:System.DateTime" /> and <see cref="T:System.DateTimeOffset" /> values are formatting when writing JSON text.
		/// </summary>
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x000048F2 File Offset: 0x00002AF2
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x000048FA File Offset: 0x00002AFA
		public string DateFormatString
		{
			get
			{
				return this._dateFormatString;
			}
			set
			{
				this._dateFormatString = value;
			}
		}

		/// <summary>
		/// Gets or sets the culture used when writing JSON. Defaults to <see cref="P:System.Globalization.CultureInfo.InvariantCulture" />.
		/// </summary>
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00004903 File Offset: 0x00002B03
		// (set) Token: 0x060000AB RID: 171 RVA: 0x00004914 File Offset: 0x00002B14
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

		/// <summary>
		/// Creates an instance of the <c>JsonWriter</c> class. 
		/// </summary>
		// Token: 0x060000AC RID: 172 RVA: 0x0000491D File Offset: 0x00002B1D
		protected JsonWriter()
		{
			this._stack = new List<JsonPosition>(4);
			this._currentState = JsonWriter.State.Start;
			this._formatting = Formatting.None;
			this._dateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind;
			this.CloseOutput = true;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000494D File Offset: 0x00002B4D
		internal void UpdateScopeWithFinishedValue()
		{
			if (this._currentPosition.HasIndex)
			{
				this._currentPosition.Position = this._currentPosition.Position + 1;
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000496F File Offset: 0x00002B6F
		private void Push(JsonContainerType value)
		{
			if (this._currentPosition.Type != JsonContainerType.None)
			{
				this._stack.Add(this._currentPosition);
			}
			this._currentPosition = new JsonPosition(value);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000499C File Offset: 0x00002B9C
		private JsonContainerType Pop()
		{
			JsonPosition currentPosition = this._currentPosition;
			if (this._stack.Count > 0)
			{
				this._currentPosition = this._stack[this._stack.Count - 1];
				this._stack.RemoveAt(this._stack.Count - 1);
			}
			else
			{
				this._currentPosition = default(JsonPosition);
			}
			return currentPosition.Type;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004A09 File Offset: 0x00002C09
		private JsonContainerType Peek()
		{
			return this._currentPosition.Type;
		}

		/// <summary>
		/// Flushes whatever is in the buffer to the underlying streams and also flushes the underlying stream.
		/// </summary>
		// Token: 0x060000B1 RID: 177
		public abstract void Flush();

		/// <summary>
		/// Closes this stream and the underlying stream.
		/// </summary>
		// Token: 0x060000B2 RID: 178 RVA: 0x00004A16 File Offset: 0x00002C16
		public virtual void Close()
		{
			this.AutoCompleteAll();
		}

		/// <summary>
		/// Writes the beginning of a Json object.
		/// </summary>
		// Token: 0x060000B3 RID: 179 RVA: 0x00004A1E File Offset: 0x00002C1E
		public virtual void WriteStartObject()
		{
			this.InternalWriteStart(JsonToken.StartObject, JsonContainerType.Object);
		}

		/// <summary>
		/// Writes the end of a Json object.
		/// </summary>
		// Token: 0x060000B4 RID: 180 RVA: 0x00004A28 File Offset: 0x00002C28
		public virtual void WriteEndObject()
		{
			this.InternalWriteEnd(JsonContainerType.Object);
		}

		/// <summary>
		/// Writes the beginning of a Json array.
		/// </summary>
		// Token: 0x060000B5 RID: 181 RVA: 0x00004A31 File Offset: 0x00002C31
		public virtual void WriteStartArray()
		{
			this.InternalWriteStart(JsonToken.StartArray, JsonContainerType.Array);
		}

		/// <summary>
		/// Writes the end of an array.
		/// </summary>
		// Token: 0x060000B6 RID: 182 RVA: 0x00004A3B File Offset: 0x00002C3B
		public virtual void WriteEndArray()
		{
			this.InternalWriteEnd(JsonContainerType.Array);
		}

		/// <summary>
		/// Writes the start of a constructor with the given name.
		/// </summary>
		/// <param name="name">The name of the constructor.</param>
		// Token: 0x060000B7 RID: 183 RVA: 0x00004A44 File Offset: 0x00002C44
		public virtual void WriteStartConstructor(string name)
		{
			this.InternalWriteStart(JsonToken.StartConstructor, JsonContainerType.Constructor);
		}

		/// <summary>
		/// Writes the end constructor.
		/// </summary>
		// Token: 0x060000B8 RID: 184 RVA: 0x00004A4E File Offset: 0x00002C4E
		public virtual void WriteEndConstructor()
		{
			this.InternalWriteEnd(JsonContainerType.Constructor);
		}

		/// <summary>
		/// Writes the property name of a name/value pair on a JSON object.
		/// </summary>
		/// <param name="name">The name of the property.</param>
		// Token: 0x060000B9 RID: 185 RVA: 0x00004A57 File Offset: 0x00002C57
		public virtual void WritePropertyName(string name)
		{
			this.InternalWritePropertyName(name);
		}

		/// <summary>
		/// Writes the property name of a name/value pair on a JSON object.
		/// </summary>
		/// <param name="name">The name of the property.</param>
		/// <param name="escape">A flag to indicate whether the text should be escaped when it is written as a JSON property name.</param>
		// Token: 0x060000BA RID: 186 RVA: 0x00004A60 File Offset: 0x00002C60
		public virtual void WritePropertyName(string name, bool escape)
		{
			this.WritePropertyName(name);
		}

		/// <summary>
		/// Writes the end of the current Json object or array.
		/// </summary>
		// Token: 0x060000BB RID: 187 RVA: 0x00004A69 File Offset: 0x00002C69
		public virtual void WriteEnd()
		{
			this.WriteEnd(this.Peek());
		}

		/// <summary>
		/// Writes the current <see cref="T:Newtonsoft.Json.JsonReader" /> token and its children.
		/// </summary>
		/// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read the token from.</param>
		// Token: 0x060000BC RID: 188 RVA: 0x00004A77 File Offset: 0x00002C77
		public void WriteToken(JsonReader reader)
		{
			this.WriteToken(reader, true, true);
		}

		/// <summary>
		/// Writes the current <see cref="T:Newtonsoft.Json.JsonReader" /> token.
		/// </summary>
		/// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read the token from.</param>
		/// <param name="writeChildren">A flag indicating whether the current token's children should be written.</param>
		// Token: 0x060000BD RID: 189 RVA: 0x00004A82 File Offset: 0x00002C82
		public void WriteToken(JsonReader reader, bool writeChildren)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			this.WriteToken(reader, writeChildren, true);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004A98 File Offset: 0x00002C98
		internal void WriteToken(JsonReader reader, bool writeChildren, bool writeDateConstructorAsDate)
		{
			int initialDepth;
			if (reader.TokenType == JsonToken.None)
			{
				initialDepth = -1;
			}
			else if (!JsonWriter.IsStartToken(reader.TokenType))
			{
				initialDepth = reader.Depth + 1;
			}
			else
			{
				initialDepth = reader.Depth;
			}
			this.WriteToken(reader, initialDepth, writeChildren, writeDateConstructorAsDate);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004ADC File Offset: 0x00002CDC
		internal void WriteToken(JsonReader reader, int initialDepth, bool writeChildren, bool writeDateConstructorAsDate)
		{
			for (;;)
			{
				switch (reader.TokenType)
				{
				case JsonToken.None:
					goto IL_27E;
				case JsonToken.StartObject:
					this.WriteStartObject();
					goto IL_27E;
				case JsonToken.StartArray:
					this.WriteStartArray();
					goto IL_27E;
				case JsonToken.StartConstructor:
				{
					string text = reader.Value.ToString();
					if (writeDateConstructorAsDate && string.Equals(text, "Date", 4))
					{
						this.WriteConstructorDate(reader);
						goto IL_27E;
					}
					this.WriteStartConstructor(reader.Value.ToString());
					goto IL_27E;
				}
				case JsonToken.PropertyName:
					this.WritePropertyName(reader.Value.ToString());
					goto IL_27E;
				case JsonToken.Comment:
					this.WriteComment((reader.Value != null) ? reader.Value.ToString() : null);
					goto IL_27E;
				case JsonToken.Raw:
					this.WriteRawValue((reader.Value != null) ? reader.Value.ToString() : null);
					goto IL_27E;
				case JsonToken.Integer:
					if (reader.Value is BigInteger)
					{
						this.WriteValue((BigInteger)reader.Value);
						goto IL_27E;
					}
					this.WriteValue(Convert.ToInt64(reader.Value, CultureInfo.InvariantCulture));
					goto IL_27E;
				case JsonToken.Float:
				{
					object value = reader.Value;
					if (value is decimal)
					{
						this.WriteValue((decimal)value);
						goto IL_27E;
					}
					if (value is double)
					{
						this.WriteValue((double)value);
						goto IL_27E;
					}
					if (value is float)
					{
						this.WriteValue((float)value);
						goto IL_27E;
					}
					this.WriteValue(Convert.ToDouble(value, CultureInfo.InvariantCulture));
					goto IL_27E;
				}
				case JsonToken.String:
					this.WriteValue(reader.Value.ToString());
					goto IL_27E;
				case JsonToken.Boolean:
					this.WriteValue(Convert.ToBoolean(reader.Value, CultureInfo.InvariantCulture));
					goto IL_27E;
				case JsonToken.Null:
					this.WriteNull();
					goto IL_27E;
				case JsonToken.Undefined:
					this.WriteUndefined();
					goto IL_27E;
				case JsonToken.EndObject:
					this.WriteEndObject();
					goto IL_27E;
				case JsonToken.EndArray:
					this.WriteEndArray();
					goto IL_27E;
				case JsonToken.EndConstructor:
					this.WriteEndConstructor();
					goto IL_27E;
				case JsonToken.Date:
					if (reader.Value is DateTimeOffset)
					{
						this.WriteValue((DateTimeOffset)reader.Value);
						goto IL_27E;
					}
					this.WriteValue(Convert.ToDateTime(reader.Value, CultureInfo.InvariantCulture));
					goto IL_27E;
				case JsonToken.Bytes:
					this.WriteValue((byte[])reader.Value);
					goto IL_27E;
				}
				break;
				IL_27E:
				if (initialDepth - 1 >= reader.Depth - (JsonWriter.IsEndToken(reader.TokenType) ? 1 : 0) || !writeChildren || !reader.Read())
				{
					return;
				}
			}
			throw MiscellaneousUtils.CreateArgumentOutOfRangeException("TokenType", reader.TokenType, "Unexpected token type.");
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004D94 File Offset: 0x00002F94
		private void WriteConstructorDate(JsonReader reader)
		{
			if (!reader.Read())
			{
				throw JsonWriterException.Create(this, "Unexpected end when reading date constructor.", null);
			}
			if (reader.TokenType != JsonToken.Integer)
			{
				throw JsonWriterException.Create(this, "Unexpected token when reading date constructor. Expected Integer, got " + reader.TokenType, null);
			}
			long javaScriptTicks = (long)reader.Value;
			DateTime value = DateTimeUtils.ConvertJavaScriptTicksToDateTime(javaScriptTicks);
			if (!reader.Read())
			{
				throw JsonWriterException.Create(this, "Unexpected end when reading date constructor.", null);
			}
			if (reader.TokenType != JsonToken.EndConstructor)
			{
				throw JsonWriterException.Create(this, "Unexpected token when reading date constructor. Expected EndConstructor, got " + reader.TokenType, null);
			}
			this.WriteValue(value);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004E34 File Offset: 0x00003034
		internal static bool IsEndToken(JsonToken token)
		{
			switch (token)
			{
			case JsonToken.EndObject:
			case JsonToken.EndArray:
			case JsonToken.EndConstructor:
				return true;
			default:
				return false;
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004E60 File Offset: 0x00003060
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

		// Token: 0x060000C3 RID: 195 RVA: 0x00004E88 File Offset: 0x00003088
		private void WriteEnd(JsonContainerType type)
		{
			switch (type)
			{
			case JsonContainerType.Object:
				this.WriteEndObject();
				return;
			case JsonContainerType.Array:
				this.WriteEndArray();
				return;
			case JsonContainerType.Constructor:
				this.WriteEndConstructor();
				return;
			default:
				throw JsonWriterException.Create(this, "Unexpected type when writing end: " + type, null);
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004ED9 File Offset: 0x000030D9
		private void AutoCompleteAll()
		{
			while (this.Top > 0)
			{
				this.WriteEnd();
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004EEC File Offset: 0x000030EC
		private JsonToken GetCloseTokenForType(JsonContainerType type)
		{
			switch (type)
			{
			case JsonContainerType.Object:
				return JsonToken.EndObject;
			case JsonContainerType.Array:
				return JsonToken.EndArray;
			case JsonContainerType.Constructor:
				return JsonToken.EndConstructor;
			default:
				throw JsonWriterException.Create(this, "No close token for type: " + type, null);
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004F34 File Offset: 0x00003134
		private void AutoCompleteClose(JsonContainerType type)
		{
			int num = 0;
			if (this._currentPosition.Type == type)
			{
				num = 1;
			}
			else
			{
				int num2 = this.Top - 2;
				for (int i = num2; i >= 0; i--)
				{
					int num3 = num2 - i;
					if (this._stack[num3].Type == type)
					{
						num = i + 2;
						break;
					}
				}
			}
			if (num == 0)
			{
				throw JsonWriterException.Create(this, "No token to close.", null);
			}
			for (int j = 0; j < num; j++)
			{
				JsonToken closeTokenForType = this.GetCloseTokenForType(this.Pop());
				if (this._currentState == JsonWriter.State.Property)
				{
					this.WriteNull();
				}
				if (this._formatting == Formatting.Indented && this._currentState != JsonWriter.State.ObjectStart && this._currentState != JsonWriter.State.ArrayStart)
				{
					this.WriteIndent();
				}
				this.WriteEnd(closeTokenForType);
				JsonContainerType jsonContainerType = this.Peek();
				switch (jsonContainerType)
				{
				case JsonContainerType.None:
					this._currentState = JsonWriter.State.Start;
					break;
				case JsonContainerType.Object:
					this._currentState = JsonWriter.State.Object;
					break;
				case JsonContainerType.Array:
					this._currentState = JsonWriter.State.Array;
					break;
				case JsonContainerType.Constructor:
					this._currentState = JsonWriter.State.Array;
					break;
				default:
					throw JsonWriterException.Create(this, "Unknown JsonType: " + jsonContainerType, null);
				}
			}
		}

		/// <summary>
		/// Writes the specified end token.
		/// </summary>
		/// <param name="token">The end token to write.</param>
		// Token: 0x060000C7 RID: 199 RVA: 0x00005056 File Offset: 0x00003256
		protected virtual void WriteEnd(JsonToken token)
		{
		}

		/// <summary>
		/// Writes indent characters.
		/// </summary>
		// Token: 0x060000C8 RID: 200 RVA: 0x00005058 File Offset: 0x00003258
		protected virtual void WriteIndent()
		{
		}

		/// <summary>
		/// Writes the JSON value delimiter.
		/// </summary>
		// Token: 0x060000C9 RID: 201 RVA: 0x0000505A File Offset: 0x0000325A
		protected virtual void WriteValueDelimiter()
		{
		}

		/// <summary>
		/// Writes an indent space.
		/// </summary>
		// Token: 0x060000CA RID: 202 RVA: 0x0000505C File Offset: 0x0000325C
		protected virtual void WriteIndentSpace()
		{
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00005060 File Offset: 0x00003260
		internal void AutoComplete(JsonToken tokenBeingWritten)
		{
			JsonWriter.State state = JsonWriter.StateArray[(int)tokenBeingWritten][(int)this._currentState];
			if (state == JsonWriter.State.Error)
			{
				throw JsonWriterException.Create(this, "Token {0} in state {1} would result in an invalid JSON object.".FormatWith(CultureInfo.InvariantCulture, tokenBeingWritten.ToString(), this._currentState.ToString()), null);
			}
			if ((this._currentState == JsonWriter.State.Object || this._currentState == JsonWriter.State.Array || this._currentState == JsonWriter.State.Constructor) && tokenBeingWritten != JsonToken.Comment)
			{
				this.WriteValueDelimiter();
			}
			if (this._formatting == Formatting.Indented)
			{
				if (this._currentState == JsonWriter.State.Property)
				{
					this.WriteIndentSpace();
				}
				if (this._currentState == JsonWriter.State.Array || this._currentState == JsonWriter.State.ArrayStart || this._currentState == JsonWriter.State.Constructor || this._currentState == JsonWriter.State.ConstructorStart || (tokenBeingWritten == JsonToken.PropertyName && this._currentState != JsonWriter.State.Start))
				{
					this.WriteIndent();
				}
			}
			this._currentState = state;
		}

		/// <summary>
		/// Writes a null value.
		/// </summary>
		// Token: 0x060000CC RID: 204 RVA: 0x0000512D File Offset: 0x0000332D
		public virtual void WriteNull()
		{
			this.InternalWriteValue(JsonToken.Null);
		}

		/// <summary>
		/// Writes an undefined value.
		/// </summary>
		// Token: 0x060000CD RID: 205 RVA: 0x00005137 File Offset: 0x00003337
		public virtual void WriteUndefined()
		{
			this.InternalWriteValue(JsonToken.Undefined);
		}

		/// <summary>
		/// Writes raw JSON without changing the writer's state.
		/// </summary>
		/// <param name="json">The raw JSON to write.</param>
		// Token: 0x060000CE RID: 206 RVA: 0x00005141 File Offset: 0x00003341
		public virtual void WriteRaw(string json)
		{
			this.InternalWriteRaw();
		}

		/// <summary>
		/// Writes raw JSON where a value is expected and updates the writer's state.
		/// </summary>
		/// <param name="json">The raw JSON to write.</param>
		// Token: 0x060000CF RID: 207 RVA: 0x00005149 File Offset: 0x00003349
		public virtual void WriteRawValue(string json)
		{
			this.UpdateScopeWithFinishedValue();
			this.AutoComplete(JsonToken.Undefined);
			this.WriteRaw(json);
		}

		/// <summary>
		/// Writes a <see cref="T:System.String" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.String" /> value to write.</param>
		// Token: 0x060000D0 RID: 208 RVA: 0x00005160 File Offset: 0x00003360
		public virtual void WriteValue(string value)
		{
			this.InternalWriteValue(JsonToken.String);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Int32" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Int32" /> value to write.</param>
		// Token: 0x060000D1 RID: 209 RVA: 0x0000516A File Offset: 0x0000336A
		public virtual void WriteValue(int value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		/// <summary>
		/// Writes a <see cref="T:System.UInt32" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.UInt32" /> value to write.</param>
		// Token: 0x060000D2 RID: 210 RVA: 0x00005173 File Offset: 0x00003373
		[CLSCompliant(false)]
		public virtual void WriteValue(uint value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Int64" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Int64" /> value to write.</param>
		// Token: 0x060000D3 RID: 211 RVA: 0x0000517C File Offset: 0x0000337C
		public virtual void WriteValue(long value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		/// <summary>
		/// Writes a <see cref="T:System.UInt64" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.UInt64" /> value to write.</param>
		// Token: 0x060000D4 RID: 212 RVA: 0x00005185 File Offset: 0x00003385
		[CLSCompliant(false)]
		public virtual void WriteValue(ulong value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Single" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Single" /> value to write.</param>
		// Token: 0x060000D5 RID: 213 RVA: 0x0000518E File Offset: 0x0000338E
		public virtual void WriteValue(float value)
		{
			this.InternalWriteValue(JsonToken.Float);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Double" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Double" /> value to write.</param>
		// Token: 0x060000D6 RID: 214 RVA: 0x00005197 File Offset: 0x00003397
		public virtual void WriteValue(double value)
		{
			this.InternalWriteValue(JsonToken.Float);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Boolean" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Boolean" /> value to write.</param>
		// Token: 0x060000D7 RID: 215 RVA: 0x000051A0 File Offset: 0x000033A0
		public virtual void WriteValue(bool value)
		{
			this.InternalWriteValue(JsonToken.Boolean);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Int16" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Int16" /> value to write.</param>
		// Token: 0x060000D8 RID: 216 RVA: 0x000051AA File Offset: 0x000033AA
		public virtual void WriteValue(short value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		/// <summary>
		/// Writes a <see cref="T:System.UInt16" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.UInt16" /> value to write.</param>
		// Token: 0x060000D9 RID: 217 RVA: 0x000051B3 File Offset: 0x000033B3
		[CLSCompliant(false)]
		public virtual void WriteValue(ushort value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Char" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Char" /> value to write.</param>
		// Token: 0x060000DA RID: 218 RVA: 0x000051BC File Offset: 0x000033BC
		public virtual void WriteValue(char value)
		{
			this.InternalWriteValue(JsonToken.String);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Byte" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Byte" /> value to write.</param>
		// Token: 0x060000DB RID: 219 RVA: 0x000051C6 File Offset: 0x000033C6
		public virtual void WriteValue(byte value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		/// <summary>
		/// Writes a <see cref="T:System.SByte" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.SByte" /> value to write.</param>
		// Token: 0x060000DC RID: 220 RVA: 0x000051CF File Offset: 0x000033CF
		[CLSCompliant(false)]
		public virtual void WriteValue(sbyte value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Decimal" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Decimal" /> value to write.</param>
		// Token: 0x060000DD RID: 221 RVA: 0x000051D8 File Offset: 0x000033D8
		public virtual void WriteValue(decimal value)
		{
			this.InternalWriteValue(JsonToken.Float);
		}

		/// <summary>
		/// Writes a <see cref="T:System.DateTime" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.DateTime" /> value to write.</param>
		// Token: 0x060000DE RID: 222 RVA: 0x000051E1 File Offset: 0x000033E1
		public virtual void WriteValue(DateTime value)
		{
			this.InternalWriteValue(JsonToken.Date);
		}

		/// <summary>
		/// Writes a <see cref="T:System.DateTimeOffset" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.DateTimeOffset" /> value to write.</param>
		// Token: 0x060000DF RID: 223 RVA: 0x000051EB File Offset: 0x000033EB
		public virtual void WriteValue(DateTimeOffset value)
		{
			this.InternalWriteValue(JsonToken.Date);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Guid" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Guid" /> value to write.</param>
		// Token: 0x060000E0 RID: 224 RVA: 0x000051F5 File Offset: 0x000033F5
		public virtual void WriteValue(Guid value)
		{
			this.InternalWriteValue(JsonToken.String);
		}

		/// <summary>
		/// Writes a <see cref="T:System.TimeSpan" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.TimeSpan" /> value to write.</param>
		// Token: 0x060000E1 RID: 225 RVA: 0x000051FF File Offset: 0x000033FF
		public virtual void WriteValue(TimeSpan value)
		{
			this.InternalWriteValue(JsonToken.String);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Nullable`1" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Nullable`1" /> value to write.</param>
		// Token: 0x060000E2 RID: 226 RVA: 0x00005209 File Offset: 0x00003409
		public virtual void WriteValue(int? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.Value);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Nullable`1" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Nullable`1" /> value to write.</param>
		// Token: 0x060000E3 RID: 227 RVA: 0x00005228 File Offset: 0x00003428
		[CLSCompliant(false)]
		public virtual void WriteValue(uint? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.Value);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Nullable`1" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Nullable`1" /> value to write.</param>
		// Token: 0x060000E4 RID: 228 RVA: 0x00005247 File Offset: 0x00003447
		public virtual void WriteValue(long? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.Value);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Nullable`1" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Nullable`1" /> value to write.</param>
		// Token: 0x060000E5 RID: 229 RVA: 0x00005266 File Offset: 0x00003466
		[CLSCompliant(false)]
		public virtual void WriteValue(ulong? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.Value);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Nullable`1" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Nullable`1" /> value to write.</param>
		// Token: 0x060000E6 RID: 230 RVA: 0x00005285 File Offset: 0x00003485
		public virtual void WriteValue(float? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.Value);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Nullable`1" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Nullable`1" /> value to write.</param>
		// Token: 0x060000E7 RID: 231 RVA: 0x000052A4 File Offset: 0x000034A4
		public virtual void WriteValue(double? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.Value);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Nullable`1" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Nullable`1" /> value to write.</param>
		// Token: 0x060000E8 RID: 232 RVA: 0x000052C3 File Offset: 0x000034C3
		public virtual void WriteValue(bool? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.Value);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Nullable`1" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Nullable`1" /> value to write.</param>
		// Token: 0x060000E9 RID: 233 RVA: 0x000052E4 File Offset: 0x000034E4
		public virtual void WriteValue(short? value)
		{
			short? num = value;
			int? num2 = (num != null) ? new int?((int)num.GetValueOrDefault()) : default(int?);
			if (num2 == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.Value);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Nullable`1" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Nullable`1" /> value to write.</param>
		// Token: 0x060000EA RID: 234 RVA: 0x00005334 File Offset: 0x00003534
		[CLSCompliant(false)]
		public virtual void WriteValue(ushort? value)
		{
			ushort? num = value;
			int? num2 = (num != null) ? new int?((int)num.GetValueOrDefault()) : default(int?);
			if (num2 == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.Value);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Nullable`1" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Nullable`1" /> value to write.</param>
		// Token: 0x060000EB RID: 235 RVA: 0x00005384 File Offset: 0x00003584
		public virtual void WriteValue(char? value)
		{
			char? c = value;
			int? num = (c != null) ? new int?((int)c.GetValueOrDefault()) : default(int?);
			if (num == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.Value);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Nullable`1" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Nullable`1" /> value to write.</param>
		// Token: 0x060000EC RID: 236 RVA: 0x000053D4 File Offset: 0x000035D4
		public virtual void WriteValue(byte? value)
		{
			byte? b = value;
			int? num = (b != null) ? new int?((int)b.GetValueOrDefault()) : default(int?);
			if (num == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.Value);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Nullable`1" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Nullable`1" /> value to write.</param>
		// Token: 0x060000ED RID: 237 RVA: 0x00005424 File Offset: 0x00003624
		[CLSCompliant(false)]
		public virtual void WriteValue(sbyte? value)
		{
			sbyte? b = value;
			int? num = (b != null) ? new int?((int)b.GetValueOrDefault()) : default(int?);
			if (num == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.Value);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Nullable`1" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Nullable`1" /> value to write.</param>
		// Token: 0x060000EE RID: 238 RVA: 0x00005471 File Offset: 0x00003671
		public virtual void WriteValue(decimal? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.Value);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Nullable`1" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Nullable`1" /> value to write.</param>
		// Token: 0x060000EF RID: 239 RVA: 0x00005490 File Offset: 0x00003690
		public virtual void WriteValue(DateTime? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.Value);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Nullable`1" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Nullable`1" /> value to write.</param>
		// Token: 0x060000F0 RID: 240 RVA: 0x000054AF File Offset: 0x000036AF
		public virtual void WriteValue(DateTimeOffset? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.Value);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Nullable`1" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Nullable`1" /> value to write.</param>
		// Token: 0x060000F1 RID: 241 RVA: 0x000054CE File Offset: 0x000036CE
		public virtual void WriteValue(Guid? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.Value);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Nullable`1" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Nullable`1" /> value to write.</param>
		// Token: 0x060000F2 RID: 242 RVA: 0x000054ED File Offset: 0x000036ED
		public virtual void WriteValue(TimeSpan? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.Value);
		}

		/// <summary>
		/// Writes a <see cref="T:Byte[]" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:Byte[]" /> value to write.</param>
		// Token: 0x060000F3 RID: 243 RVA: 0x0000550C File Offset: 0x0000370C
		public virtual void WriteValue(byte[] value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.InternalWriteValue(JsonToken.Bytes);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Uri" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Uri" /> value to write.</param>
		// Token: 0x060000F4 RID: 244 RVA: 0x00005520 File Offset: 0x00003720
		public virtual void WriteValue(Uri value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.InternalWriteValue(JsonToken.String);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Object" /> value.
		/// An error will raised if the value cannot be written as a single JSON token.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Object" /> value to write.</param>
		// Token: 0x060000F5 RID: 245 RVA: 0x0000553A File Offset: 0x0000373A
		public virtual void WriteValue(object value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			if (value is BigInteger)
			{
				throw JsonWriter.CreateUnsupportedTypeException(this, value);
			}
			JsonWriter.WriteValue(this, ConvertUtils.GetTypeCode(value), value);
		}

		/// <summary>
		/// Writes out a comment <code>/*...*/</code> containing the specified text. 
		/// </summary>
		/// <param name="text">Text to place inside the comment.</param>
		// Token: 0x060000F6 RID: 246 RVA: 0x00005563 File Offset: 0x00003763
		public virtual void WriteComment(string text)
		{
			this.InternalWriteComment();
		}

		/// <summary>
		/// Writes out the given white space.
		/// </summary>
		/// <param name="ws">The string of white space characters.</param>
		// Token: 0x060000F7 RID: 247 RVA: 0x0000556B File Offset: 0x0000376B
		public virtual void WriteWhitespace(string ws)
		{
			this.InternalWriteWhitespace(ws);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005574 File Offset: 0x00003774
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000557D File Offset: 0x0000377D
		private void Dispose(bool disposing)
		{
			if (this._currentState != JsonWriter.State.Closed)
			{
				this.Close();
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005590 File Offset: 0x00003790
		internal static void WriteValue(JsonWriter writer, PrimitiveTypeCode typeCode, object value)
		{
			switch (typeCode)
			{
			case PrimitiveTypeCode.Char:
				writer.WriteValue((char)value);
				return;
			case PrimitiveTypeCode.CharNullable:
				writer.WriteValue((value == null) ? default(char?) : new char?((char)value));
				return;
			case PrimitiveTypeCode.Boolean:
				writer.WriteValue((bool)value);
				return;
			case PrimitiveTypeCode.BooleanNullable:
				writer.WriteValue((value == null) ? default(bool?) : new bool?((bool)value));
				return;
			case PrimitiveTypeCode.SByte:
				writer.WriteValue((sbyte)value);
				return;
			case PrimitiveTypeCode.SByteNullable:
				writer.WriteValue((value == null) ? default(sbyte?) : new sbyte?((sbyte)value));
				return;
			case PrimitiveTypeCode.Int16:
				writer.WriteValue((short)value);
				return;
			case PrimitiveTypeCode.Int16Nullable:
				writer.WriteValue((value == null) ? default(short?) : new short?((short)value));
				return;
			case PrimitiveTypeCode.UInt16:
				writer.WriteValue((ushort)value);
				return;
			case PrimitiveTypeCode.UInt16Nullable:
				writer.WriteValue((value == null) ? default(ushort?) : new ushort?((ushort)value));
				return;
			case PrimitiveTypeCode.Int32:
				writer.WriteValue((int)value);
				return;
			case PrimitiveTypeCode.Int32Nullable:
				writer.WriteValue((value == null) ? default(int?) : new int?((int)value));
				return;
			case PrimitiveTypeCode.Byte:
				writer.WriteValue((byte)value);
				return;
			case PrimitiveTypeCode.ByteNullable:
				writer.WriteValue((value == null) ? default(byte?) : new byte?((byte)value));
				return;
			case PrimitiveTypeCode.UInt32:
				writer.WriteValue((uint)value);
				return;
			case PrimitiveTypeCode.UInt32Nullable:
				writer.WriteValue((value == null) ? default(uint?) : new uint?((uint)value));
				return;
			case PrimitiveTypeCode.Int64:
				writer.WriteValue((long)value);
				return;
			case PrimitiveTypeCode.Int64Nullable:
				writer.WriteValue((value == null) ? default(long?) : new long?((long)value));
				return;
			case PrimitiveTypeCode.UInt64:
				writer.WriteValue((ulong)value);
				return;
			case PrimitiveTypeCode.UInt64Nullable:
				writer.WriteValue((value == null) ? default(ulong?) : new ulong?((ulong)value));
				return;
			case PrimitiveTypeCode.Single:
				writer.WriteValue((float)value);
				return;
			case PrimitiveTypeCode.SingleNullable:
				writer.WriteValue((value == null) ? default(float?) : new float?((float)value));
				return;
			case PrimitiveTypeCode.Double:
				writer.WriteValue((double)value);
				return;
			case PrimitiveTypeCode.DoubleNullable:
				writer.WriteValue((value == null) ? default(double?) : new double?((double)value));
				return;
			case PrimitiveTypeCode.DateTime:
				writer.WriteValue((DateTime)value);
				return;
			case PrimitiveTypeCode.DateTimeNullable:
				writer.WriteValue((value == null) ? default(DateTime?) : new DateTime?((DateTime)value));
				return;
			case PrimitiveTypeCode.DateTimeOffset:
				writer.WriteValue((DateTimeOffset)value);
				return;
			case PrimitiveTypeCode.DateTimeOffsetNullable:
				writer.WriteValue((value == null) ? default(DateTimeOffset?) : new DateTimeOffset?((DateTimeOffset)value));
				return;
			case PrimitiveTypeCode.Decimal:
				writer.WriteValue((decimal)value);
				return;
			case PrimitiveTypeCode.DecimalNullable:
				writer.WriteValue((value == null) ? default(decimal?) : new decimal?((decimal)value));
				return;
			case PrimitiveTypeCode.Guid:
				writer.WriteValue((Guid)value);
				return;
			case PrimitiveTypeCode.GuidNullable:
				writer.WriteValue((value == null) ? default(Guid?) : new Guid?((Guid)value));
				return;
			case PrimitiveTypeCode.TimeSpan:
				writer.WriteValue((TimeSpan)value);
				return;
			case PrimitiveTypeCode.TimeSpanNullable:
				writer.WriteValue((value == null) ? default(TimeSpan?) : new TimeSpan?((TimeSpan)value));
				return;
			case PrimitiveTypeCode.BigInteger:
				writer.WriteValue((BigInteger)value);
				return;
			case PrimitiveTypeCode.BigIntegerNullable:
				writer.WriteValue((value == null) ? default(BigInteger?) : new BigInteger?((BigInteger)value));
				return;
			case PrimitiveTypeCode.Uri:
				writer.WriteValue((Uri)value);
				return;
			case PrimitiveTypeCode.String:
				writer.WriteValue((string)value);
				return;
			case PrimitiveTypeCode.Bytes:
				writer.WriteValue((byte[])value);
				return;
			default:
				throw JsonWriter.CreateUnsupportedTypeException(writer, value);
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000059B9 File Offset: 0x00003BB9
		private static JsonWriterException CreateUnsupportedTypeException(JsonWriter writer, object value)
		{
			return JsonWriterException.Create(writer, "Unsupported type: {0}. Use the JsonSerializer class to get the object's JSON representation.".FormatWith(CultureInfo.InvariantCulture, value.GetType()), null);
		}

		/// <summary>
		/// Sets the state of the JsonWriter,
		/// </summary>
		/// <param name="token">The JsonToken being written.</param>
		/// <param name="value">The value being written.</param>
		// Token: 0x060000FC RID: 252 RVA: 0x000059D8 File Offset: 0x00003BD8
		protected void SetWriteState(JsonToken token, object value)
		{
			switch (token)
			{
			case JsonToken.StartObject:
				this.InternalWriteStart(token, JsonContainerType.Object);
				return;
			case JsonToken.StartArray:
				this.InternalWriteStart(token, JsonContainerType.Array);
				return;
			case JsonToken.StartConstructor:
				this.InternalWriteStart(token, JsonContainerType.Constructor);
				return;
			case JsonToken.PropertyName:
				if (!(value is string))
				{
					throw new ArgumentException("A name is required when setting property name state.", "value");
				}
				this.InternalWritePropertyName((string)value);
				return;
			case JsonToken.Comment:
				this.InternalWriteComment();
				return;
			case JsonToken.Raw:
				this.InternalWriteRaw();
				return;
			case JsonToken.Integer:
			case JsonToken.Float:
			case JsonToken.String:
			case JsonToken.Boolean:
			case JsonToken.Null:
			case JsonToken.Undefined:
			case JsonToken.Date:
			case JsonToken.Bytes:
				this.InternalWriteValue(token);
				return;
			case JsonToken.EndObject:
				this.InternalWriteEnd(JsonContainerType.Object);
				return;
			case JsonToken.EndArray:
				this.InternalWriteEnd(JsonContainerType.Array);
				return;
			case JsonToken.EndConstructor:
				this.InternalWriteEnd(JsonContainerType.Constructor);
				return;
			default:
				throw new ArgumentOutOfRangeException("token");
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005AAD File Offset: 0x00003CAD
		internal void InternalWriteEnd(JsonContainerType container)
		{
			this.AutoCompleteClose(container);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005AB6 File Offset: 0x00003CB6
		internal void InternalWritePropertyName(string name)
		{
			this._currentPosition.PropertyName = name;
			this.AutoComplete(JsonToken.PropertyName);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005ACB File Offset: 0x00003CCB
		internal void InternalWriteRaw()
		{
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005ACD File Offset: 0x00003CCD
		internal void InternalWriteStart(JsonToken token, JsonContainerType container)
		{
			this.UpdateScopeWithFinishedValue();
			this.AutoComplete(token);
			this.Push(container);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005AE3 File Offset: 0x00003CE3
		internal void InternalWriteValue(JsonToken token)
		{
			this.UpdateScopeWithFinishedValue();
			this.AutoComplete(token);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005AF2 File Offset: 0x00003CF2
		internal void InternalWriteWhitespace(string ws)
		{
			if (ws != null && !StringUtils.IsWhiteSpace(ws))
			{
				throw JsonWriterException.Create(this, "Only white space characters should be used.", null);
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005B0C File Offset: 0x00003D0C
		internal void InternalWriteComment()
		{
			this.AutoComplete(JsonToken.Comment);
		}

		// Token: 0x04000068 RID: 104
		private static readonly JsonWriter.State[][] StateArray;

		// Token: 0x04000069 RID: 105
		internal static readonly JsonWriter.State[][] StateArrayTempate = new JsonWriter.State[][]
		{
			new JsonWriter.State[]
			{
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.ObjectStart,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.ArrayStart,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.ConstructorStart,
				JsonWriter.State.ConstructorStart,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.ConstructorStart,
				JsonWriter.State.ConstructorStart,
				JsonWriter.State.ConstructorStart,
				JsonWriter.State.ConstructorStart,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.Property,
				JsonWriter.State.Error,
				JsonWriter.State.Property,
				JsonWriter.State.Property,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.Start,
				JsonWriter.State.Property,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.Object,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.Array,
				JsonWriter.State.Constructor,
				JsonWriter.State.Constructor,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.Start,
				JsonWriter.State.Property,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.Object,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.Array,
				JsonWriter.State.Constructor,
				JsonWriter.State.Constructor,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.Start,
				JsonWriter.State.Object,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Array,
				JsonWriter.State.Array,
				JsonWriter.State.Constructor,
				JsonWriter.State.Constructor,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			}
		};

		// Token: 0x0400006A RID: 106
		private readonly List<JsonPosition> _stack;

		// Token: 0x0400006B RID: 107
		private JsonPosition _currentPosition;

		// Token: 0x0400006C RID: 108
		private JsonWriter.State _currentState;

		// Token: 0x0400006D RID: 109
		private Formatting _formatting;

		// Token: 0x0400006E RID: 110
		private DateFormatHandling _dateFormatHandling;

		// Token: 0x0400006F RID: 111
		private DateTimeZoneHandling _dateTimeZoneHandling;

		// Token: 0x04000070 RID: 112
		private StringEscapeHandling _stringEscapeHandling;

		// Token: 0x04000071 RID: 113
		private FloatFormatHandling _floatFormatHandling;

		// Token: 0x04000072 RID: 114
		private string _dateFormatString;

		// Token: 0x04000073 RID: 115
		private CultureInfo _culture;

		// Token: 0x02000014 RID: 20
		internal enum State
		{
			// Token: 0x04000076 RID: 118
			Start,
			// Token: 0x04000077 RID: 119
			Property,
			// Token: 0x04000078 RID: 120
			ObjectStart,
			// Token: 0x04000079 RID: 121
			Object,
			// Token: 0x0400007A RID: 122
			ArrayStart,
			// Token: 0x0400007B RID: 123
			Array,
			// Token: 0x0400007C RID: 124
			ConstructorStart,
			// Token: 0x0400007D RID: 125
			Constructor,
			// Token: 0x0400007E RID: 126
			Closed,
			// Token: 0x0400007F RID: 127
			Error
		}
	}
}
