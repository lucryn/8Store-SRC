using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Bson
{
	/// <summary>
	/// Represents a reader that provides fast, non-cached, forward-only access to serialized Json data.
	/// </summary>
	// Token: 0x02000007 RID: 7
	public class BsonReader : JsonReader
	{
		/// <summary>
		/// Gets or sets a value indicating whether binary data reading should compatible with incorrect Json.NET 3.5 written binary.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if binary data reading will be compatible with incorrect Json.NET 3.5 written binary; otherwise, <c>false</c>.
		/// </value>
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00003633 File Offset: 0x00001833
		// (set) Token: 0x06000046 RID: 70 RVA: 0x0000363B File Offset: 0x0000183B
		[Obsolete("JsonNet35BinaryCompatibility will be removed in a future version of Json.NET.")]
		public bool JsonNet35BinaryCompatibility
		{
			get
			{
				return this._jsonNet35BinaryCompatibility;
			}
			set
			{
				this._jsonNet35BinaryCompatibility = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the root object will be read as a JSON array.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if the root object will be read as a JSON array; otherwise, <c>false</c>.
		/// </value>
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00003644 File Offset: 0x00001844
		// (set) Token: 0x06000048 RID: 72 RVA: 0x0000364C File Offset: 0x0000184C
		public bool ReadRootValueAsArray
		{
			get
			{
				return this._readRootValueAsArray;
			}
			set
			{
				this._readRootValueAsArray = value;
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="T:System.DateTimeKind" /> used when reading <see cref="T:System.DateTime" /> values from BSON.
		/// </summary>
		/// <value>The <see cref="T:System.DateTimeKind" /> used when reading <see cref="T:System.DateTime" /> values from BSON.</value>
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00003655 File Offset: 0x00001855
		// (set) Token: 0x0600004A RID: 74 RVA: 0x0000365D File Offset: 0x0000185D
		public DateTimeKind DateTimeKindHandling
		{
			get
			{
				return this._dateTimeKindHandling;
			}
			set
			{
				this._dateTimeKindHandling = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Bson.BsonReader" /> class.
		/// </summary>
		/// <param name="stream">The stream.</param>
		// Token: 0x0600004B RID: 75 RVA: 0x00003666 File Offset: 0x00001866
		public BsonReader(Stream stream) : this(stream, false, 2)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Bson.BsonReader" /> class.
		/// </summary>
		/// <param name="reader">The reader.</param>
		// Token: 0x0600004C RID: 76 RVA: 0x00003671 File Offset: 0x00001871
		public BsonReader(BinaryReader reader) : this(reader, false, 2)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Bson.BsonReader" /> class.
		/// </summary>
		/// <param name="stream">The stream.</param>
		/// <param name="readRootValueAsArray">if set to <c>true</c> the root object will be read as a JSON array.</param>
		/// <param name="dateTimeKindHandling">The <see cref="T:System.DateTimeKind" /> used when reading <see cref="T:System.DateTime" /> values from BSON.</param>
		// Token: 0x0600004D RID: 77 RVA: 0x0000367C File Offset: 0x0000187C
		public BsonReader(Stream stream, bool readRootValueAsArray, DateTimeKind dateTimeKindHandling)
		{
			ValidationUtils.ArgumentNotNull(stream, "stream");
			this._reader = new BinaryReader(stream);
			this._stack = new List<BsonReader.ContainerContext>();
			this._readRootValueAsArray = readRootValueAsArray;
			this._dateTimeKindHandling = dateTimeKindHandling;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Bson.BsonReader" /> class.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <param name="readRootValueAsArray">if set to <c>true</c> the root object will be read as a JSON array.</param>
		/// <param name="dateTimeKindHandling">The <see cref="T:System.DateTimeKind" /> used when reading <see cref="T:System.DateTime" /> values from BSON.</param>
		// Token: 0x0600004E RID: 78 RVA: 0x000036B4 File Offset: 0x000018B4
		public BsonReader(BinaryReader reader, bool readRootValueAsArray, DateTimeKind dateTimeKindHandling)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			this._reader = reader;
			this._stack = new List<BsonReader.ContainerContext>();
			this._readRootValueAsArray = readRootValueAsArray;
			this._dateTimeKindHandling = dateTimeKindHandling;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000036E8 File Offset: 0x000018E8
		private string ReadElement()
		{
			this._currentElementType = this.ReadType();
			return this.ReadString();
		}

		/// <summary>
		/// Reads the next JSON token from the stream as a <see cref="T:Byte[]" />.
		/// </summary>
		/// <returns>
		/// A <see cref="T:Byte[]" /> or a null reference if the next JSON token is null. This method will return <c>null</c> at the end of an array.
		/// </returns>
		// Token: 0x06000050 RID: 80 RVA: 0x00003709 File Offset: 0x00001909
		public override byte[] ReadAsBytes()
		{
			return base.ReadAsBytesInternal();
		}

		/// <summary>
		/// Reads the next JSON token from the stream as a <see cref="T:System.Nullable`1" />.
		/// </summary>
		/// <returns>A <see cref="T:System.Nullable`1" />. This method will return <c>null</c> at the end of an array.</returns>
		// Token: 0x06000051 RID: 81 RVA: 0x00003711 File Offset: 0x00001911
		public override decimal? ReadAsDecimal()
		{
			return base.ReadAsDecimalInternal();
		}

		/// <summary>
		/// Reads the next JSON token from the stream as a <see cref="T:System.Nullable`1" />.
		/// </summary>
		/// <returns>A <see cref="T:System.Nullable`1" />. This method will return <c>null</c> at the end of an array.</returns>
		// Token: 0x06000052 RID: 82 RVA: 0x00003719 File Offset: 0x00001919
		public override int? ReadAsInt32()
		{
			return base.ReadAsInt32Internal();
		}

		/// <summary>
		/// Reads the next JSON token from the stream as a <see cref="T:System.String" />.
		/// </summary>
		/// <returns>A <see cref="T:System.String" />. This method will return <c>null</c> at the end of an array.</returns>
		// Token: 0x06000053 RID: 83 RVA: 0x00003721 File Offset: 0x00001921
		public override string ReadAsString()
		{
			return base.ReadAsStringInternal();
		}

		/// <summary>
		/// Reads the next JSON token from the stream as a <see cref="T:System.Nullable`1" />.
		/// </summary>
		/// <returns>A <see cref="T:System.String" />. This method will return <c>null</c> at the end of an array.</returns>
		// Token: 0x06000054 RID: 84 RVA: 0x00003729 File Offset: 0x00001929
		public override DateTime? ReadAsDateTime()
		{
			return base.ReadAsDateTimeInternal();
		}

		/// <summary>
		/// Reads the next JSON token from the stream as a <see cref="T:System.Nullable`1" />.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Nullable`1" />. This method will return <c>null</c> at the end of an array.
		/// </returns>
		// Token: 0x06000055 RID: 85 RVA: 0x00003731 File Offset: 0x00001931
		public override DateTimeOffset? ReadAsDateTimeOffset()
		{
			return base.ReadAsDateTimeOffsetInternal();
		}

		/// <summary>
		/// Reads the next JSON token from the stream.
		/// </summary>
		/// <returns>
		/// true if the next token was read successfully; false if there are no more tokens to read.
		/// </returns>
		// Token: 0x06000056 RID: 86 RVA: 0x00003739 File Offset: 0x00001939
		public override bool Read()
		{
			this._readType = Newtonsoft.Json.ReadType.Read;
			return this.ReadInternal();
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003748 File Offset: 0x00001948
		internal override bool ReadInternal()
		{
			bool result;
			try
			{
				bool flag;
				switch (this._bsonReaderState)
				{
				case BsonReader.BsonReaderState.Normal:
					flag = this.ReadNormal();
					break;
				case BsonReader.BsonReaderState.ReferenceStart:
				case BsonReader.BsonReaderState.ReferenceRef:
				case BsonReader.BsonReaderState.ReferenceId:
					flag = this.ReadReference();
					break;
				case BsonReader.BsonReaderState.CodeWScopeStart:
				case BsonReader.BsonReaderState.CodeWScopeCode:
				case BsonReader.BsonReaderState.CodeWScopeScope:
				case BsonReader.BsonReaderState.CodeWScopeScopeObject:
				case BsonReader.BsonReaderState.CodeWScopeScopeEnd:
					flag = this.ReadCodeWScope();
					break;
				default:
					throw JsonReaderException.Create(this, "Unexpected state: {0}".FormatWith(CultureInfo.InvariantCulture, this._bsonReaderState));
				}
				if (!flag)
				{
					base.SetToken(JsonToken.None);
					result = false;
				}
				else
				{
					result = true;
				}
			}
			catch (EndOfStreamException)
			{
				base.SetToken(JsonToken.None);
				result = false;
			}
			return result;
		}

		/// <summary>
		/// Changes the <see cref="T:Newtonsoft.Json.JsonReader.State" /> to Closed.
		/// </summary>
		// Token: 0x06000058 RID: 88 RVA: 0x000037F4 File Offset: 0x000019F4
		public override void Close()
		{
			base.Close();
			if (base.CloseInput && this._reader != null)
			{
				this._reader.Dispose();
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003818 File Offset: 0x00001A18
		private bool ReadCodeWScope()
		{
			switch (this._bsonReaderState)
			{
			case BsonReader.BsonReaderState.CodeWScopeStart:
				base.SetToken(JsonToken.PropertyName, "$code");
				this._bsonReaderState = BsonReader.BsonReaderState.CodeWScopeCode;
				return true;
			case BsonReader.BsonReaderState.CodeWScopeCode:
				this.ReadInt32();
				base.SetToken(JsonToken.String, this.ReadLengthString());
				this._bsonReaderState = BsonReader.BsonReaderState.CodeWScopeScope;
				return true;
			case BsonReader.BsonReaderState.CodeWScopeScope:
			{
				if (base.CurrentState == JsonReader.State.PostValue)
				{
					base.SetToken(JsonToken.PropertyName, "$scope");
					return true;
				}
				base.SetToken(JsonToken.StartObject);
				this._bsonReaderState = BsonReader.BsonReaderState.CodeWScopeScopeObject;
				BsonReader.ContainerContext containerContext = new BsonReader.ContainerContext(BsonType.Object);
				this.PushContext(containerContext);
				containerContext.Length = this.ReadInt32();
				return true;
			}
			case BsonReader.BsonReaderState.CodeWScopeScopeObject:
			{
				bool flag = this.ReadNormal();
				if (flag && this.TokenType == JsonToken.EndObject)
				{
					this._bsonReaderState = BsonReader.BsonReaderState.CodeWScopeScopeEnd;
				}
				return flag;
			}
			case BsonReader.BsonReaderState.CodeWScopeScopeEnd:
				base.SetToken(JsonToken.EndObject);
				this._bsonReaderState = BsonReader.BsonReaderState.Normal;
				return true;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000038F4 File Offset: 0x00001AF4
		private bool ReadReference()
		{
			JsonReader.State currentState = base.CurrentState;
			switch (currentState)
			{
			case JsonReader.State.Property:
				if (this._bsonReaderState == BsonReader.BsonReaderState.ReferenceRef)
				{
					base.SetToken(JsonToken.String, this.ReadLengthString());
					return true;
				}
				if (this._bsonReaderState == BsonReader.BsonReaderState.ReferenceId)
				{
					base.SetToken(JsonToken.Bytes, this.ReadBytes(12));
					return true;
				}
				throw JsonReaderException.Create(this, "Unexpected state when reading BSON reference: " + this._bsonReaderState);
			case JsonReader.State.ObjectStart:
				base.SetToken(JsonToken.PropertyName, "$ref");
				this._bsonReaderState = BsonReader.BsonReaderState.ReferenceRef;
				return true;
			default:
				if (currentState != JsonReader.State.PostValue)
				{
					throw JsonReaderException.Create(this, "Unexpected state when reading BSON reference: " + base.CurrentState);
				}
				if (this._bsonReaderState == BsonReader.BsonReaderState.ReferenceRef)
				{
					base.SetToken(JsonToken.PropertyName, "$id");
					this._bsonReaderState = BsonReader.BsonReaderState.ReferenceId;
					return true;
				}
				if (this._bsonReaderState == BsonReader.BsonReaderState.ReferenceId)
				{
					base.SetToken(JsonToken.EndObject);
					this._bsonReaderState = BsonReader.BsonReaderState.Normal;
					return true;
				}
				throw JsonReaderException.Create(this, "Unexpected state when reading BSON reference: " + this._bsonReaderState);
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000039F8 File Offset: 0x00001BF8
		private bool ReadNormal()
		{
			switch (base.CurrentState)
			{
			case JsonReader.State.Start:
			{
				JsonToken token = (!this._readRootValueAsArray) ? JsonToken.StartObject : JsonToken.StartArray;
				BsonType type = (!this._readRootValueAsArray) ? BsonType.Object : BsonType.Array;
				base.SetToken(token);
				BsonReader.ContainerContext containerContext = new BsonReader.ContainerContext(type);
				this.PushContext(containerContext);
				containerContext.Length = this.ReadInt32();
				return true;
			}
			case JsonReader.State.Complete:
			case JsonReader.State.Closed:
				return false;
			case JsonReader.State.Property:
				this.ReadType(this._currentElementType);
				return true;
			case JsonReader.State.ObjectStart:
			case JsonReader.State.ArrayStart:
			case JsonReader.State.PostValue:
			{
				BsonReader.ContainerContext currentContext = this._currentContext;
				if (currentContext == null)
				{
					return false;
				}
				int num = currentContext.Length - 1;
				if (currentContext.Position < num)
				{
					if (currentContext.Type == BsonType.Array)
					{
						this.ReadElement();
						this.ReadType(this._currentElementType);
						return true;
					}
					base.SetToken(JsonToken.PropertyName, this.ReadElement());
					return true;
				}
				else
				{
					if (currentContext.Position != num)
					{
						throw JsonReaderException.Create(this, "Read past end of current container context.");
					}
					if (this.ReadByte() != 0)
					{
						throw JsonReaderException.Create(this, "Unexpected end of object byte value.");
					}
					this.PopContext();
					if (this._currentContext != null)
					{
						this.MovePosition(currentContext.Length);
					}
					JsonToken token2 = (currentContext.Type == BsonType.Object) ? JsonToken.EndObject : JsonToken.EndArray;
					base.SetToken(token2);
					return true;
				}
				break;
			}
			case JsonReader.State.ConstructorStart:
			case JsonReader.State.Constructor:
			case JsonReader.State.Error:
			case JsonReader.State.Finished:
				return false;
			}
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003B50 File Offset: 0x00001D50
		private void PopContext()
		{
			this._stack.RemoveAt(this._stack.Count - 1);
			if (this._stack.Count == 0)
			{
				this._currentContext = null;
				return;
			}
			this._currentContext = this._stack[this._stack.Count - 1];
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003BA8 File Offset: 0x00001DA8
		private void PushContext(BsonReader.ContainerContext newContext)
		{
			this._stack.Add(newContext);
			this._currentContext = newContext;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003BBD File Offset: 0x00001DBD
		private byte ReadByte()
		{
			this.MovePosition(1);
			return this._reader.ReadByte();
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003BD4 File Offset: 0x00001DD4
		private void ReadType(BsonType type)
		{
			switch (type)
			{
			case BsonType.Number:
			{
				double num = this.ReadDouble();
				if (this._floatParseHandling == FloatParseHandling.Decimal)
				{
					base.SetToken(JsonToken.Float, Convert.ToDecimal(num, CultureInfo.InvariantCulture));
					return;
				}
				base.SetToken(JsonToken.Float, num);
				return;
			}
			case BsonType.String:
			case BsonType.Symbol:
				base.SetToken(JsonToken.String, this.ReadLengthString());
				return;
			case BsonType.Object:
			{
				base.SetToken(JsonToken.StartObject);
				BsonReader.ContainerContext containerContext = new BsonReader.ContainerContext(BsonType.Object);
				this.PushContext(containerContext);
				containerContext.Length = this.ReadInt32();
				return;
			}
			case BsonType.Array:
			{
				base.SetToken(JsonToken.StartArray);
				BsonReader.ContainerContext containerContext2 = new BsonReader.ContainerContext(BsonType.Array);
				this.PushContext(containerContext2);
				containerContext2.Length = this.ReadInt32();
				return;
			}
			case BsonType.Binary:
				base.SetToken(JsonToken.Bytes, this.ReadBinary());
				return;
			case BsonType.Undefined:
				base.SetToken(JsonToken.Undefined);
				return;
			case BsonType.Oid:
			{
				byte[] value = this.ReadBytes(12);
				base.SetToken(JsonToken.Bytes, value);
				return;
			}
			case BsonType.Boolean:
			{
				bool flag = Convert.ToBoolean(this.ReadByte());
				base.SetToken(JsonToken.Boolean, flag);
				return;
			}
			case BsonType.Date:
			{
				long javaScriptTicks = this.ReadInt64();
				DateTime dateTime = DateTimeUtils.ConvertJavaScriptTicksToDateTime(javaScriptTicks);
				DateTime dateTime2;
				switch (this.DateTimeKindHandling)
				{
				case 0:
					dateTime2 = DateTime.SpecifyKind(dateTime, 0);
					goto IL_178;
				case 2:
					dateTime2 = dateTime.ToLocalTime();
					goto IL_178;
				}
				dateTime2 = dateTime;
				IL_178:
				base.SetToken(JsonToken.Date, dateTime2);
				return;
			}
			case BsonType.Null:
				base.SetToken(JsonToken.Null);
				return;
			case BsonType.Regex:
			{
				string text = this.ReadString();
				string text2 = this.ReadString();
				string value2 = "/" + text + "/" + text2;
				base.SetToken(JsonToken.String, value2);
				return;
			}
			case BsonType.Reference:
				base.SetToken(JsonToken.StartObject);
				this._bsonReaderState = BsonReader.BsonReaderState.ReferenceStart;
				return;
			case BsonType.Code:
				base.SetToken(JsonToken.String, this.ReadLengthString());
				return;
			case BsonType.CodeWScope:
				base.SetToken(JsonToken.StartObject);
				this._bsonReaderState = BsonReader.BsonReaderState.CodeWScopeStart;
				return;
			case BsonType.Integer:
				base.SetToken(JsonToken.Integer, (long)this.ReadInt32());
				return;
			case BsonType.TimeStamp:
			case BsonType.Long:
				base.SetToken(JsonToken.Integer, this.ReadInt64());
				return;
			default:
				throw new ArgumentOutOfRangeException("type", "Unexpected BsonType value: " + type);
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003E10 File Offset: 0x00002010
		private byte[] ReadBinary()
		{
			int count = this.ReadInt32();
			BsonBinaryType bsonBinaryType = (BsonBinaryType)this.ReadByte();
			if (bsonBinaryType == BsonBinaryType.BinaryOld && !this._jsonNet35BinaryCompatibility)
			{
				count = this.ReadInt32();
			}
			return this.ReadBytes(count);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003E48 File Offset: 0x00002048
		private string ReadString()
		{
			this.EnsureBuffers();
			StringBuilder stringBuilder = null;
			int num = 0;
			int num2 = 0;
			int num4;
			for (;;)
			{
				int num3 = num2;
				byte b;
				while (num3 < 128 && (b = this._reader.ReadByte()) > 0)
				{
					this._byteBuffer[num3++] = b;
				}
				num4 = num3 - num2;
				num += num4;
				if (num3 < 128 && stringBuilder == null)
				{
					break;
				}
				int lastFullCharStop = this.GetLastFullCharStop(num3 - 1);
				int chars = Encoding.UTF8.GetChars(this._byteBuffer, 0, lastFullCharStop + 1, this._charBuffer, 0);
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder(256);
				}
				stringBuilder.Append(this._charBuffer, 0, chars);
				if (lastFullCharStop < num4 - 1)
				{
					num2 = num4 - lastFullCharStop - 1;
					Array.Copy(this._byteBuffer, lastFullCharStop + 1, this._byteBuffer, 0, num2);
				}
				else
				{
					if (num3 < 128)
					{
						goto Block_6;
					}
					num2 = 0;
				}
			}
			int chars2 = Encoding.UTF8.GetChars(this._byteBuffer, 0, num4, this._charBuffer, 0);
			this.MovePosition(num + 1);
			return new string(this._charBuffer, 0, chars2);
			Block_6:
			this.MovePosition(num + 1);
			return stringBuilder.ToString();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003F68 File Offset: 0x00002168
		private string ReadLengthString()
		{
			int num = this.ReadInt32();
			this.MovePosition(num);
			string @string = this.GetString(num - 1);
			this._reader.ReadByte();
			return @string;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003F9C File Offset: 0x0000219C
		private string GetString(int length)
		{
			if (length == 0)
			{
				return string.Empty;
			}
			this.EnsureBuffers();
			StringBuilder stringBuilder = null;
			int num = 0;
			int num2 = 0;
			int num4;
			for (;;)
			{
				int num3 = (length - num > 128 - num2) ? (128 - num2) : (length - num);
				num4 = this._reader.Read(this._byteBuffer, num2, num3);
				if (num4 == 0)
				{
					break;
				}
				num += num4;
				num4 += num2;
				if (num4 == length)
				{
					goto Block_4;
				}
				int lastFullCharStop = this.GetLastFullCharStop(num4 - 1);
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder(length);
				}
				int chars = Encoding.UTF8.GetChars(this._byteBuffer, 0, lastFullCharStop + 1, this._charBuffer, 0);
				stringBuilder.Append(this._charBuffer, 0, chars);
				if (lastFullCharStop < num4 - 1)
				{
					num2 = num4 - lastFullCharStop - 1;
					Array.Copy(this._byteBuffer, lastFullCharStop + 1, this._byteBuffer, 0, num2);
				}
				else
				{
					num2 = 0;
				}
				if (num >= length)
				{
					goto Block_7;
				}
			}
			throw new EndOfStreamException("Unable to read beyond the end of the stream.");
			Block_4:
			int chars2 = Encoding.UTF8.GetChars(this._byteBuffer, 0, num4, this._charBuffer, 0);
			return new string(this._charBuffer, 0, chars2);
			Block_7:
			return stringBuilder.ToString();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000040B4 File Offset: 0x000022B4
		private int GetLastFullCharStop(int start)
		{
			int i = start;
			int num = 0;
			while (i >= 0)
			{
				num = this.BytesInSequence(this._byteBuffer[i]);
				if (num == 0)
				{
					i--;
				}
				else
				{
					if (num != 1)
					{
						i--;
						break;
					}
					break;
				}
			}
			if (num == start - i)
			{
				return start;
			}
			return i;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000040F8 File Offset: 0x000022F8
		private int BytesInSequence(byte b)
		{
			if (b <= BsonReader.SeqRange1[1])
			{
				return 1;
			}
			if (b >= BsonReader.SeqRange2[0] && b <= BsonReader.SeqRange2[1])
			{
				return 2;
			}
			if (b >= BsonReader.SeqRange3[0] && b <= BsonReader.SeqRange3[1])
			{
				return 3;
			}
			if (b >= BsonReader.SeqRange4[0] && b <= BsonReader.SeqRange4[1])
			{
				return 4;
			}
			return 0;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00004154 File Offset: 0x00002354
		private void EnsureBuffers()
		{
			if (this._byteBuffer == null)
			{
				this._byteBuffer = new byte[128];
			}
			if (this._charBuffer == null)
			{
				int maxCharCount = Encoding.UTF8.GetMaxCharCount(128);
				this._charBuffer = new char[maxCharCount];
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000419D File Offset: 0x0000239D
		private double ReadDouble()
		{
			this.MovePosition(8);
			return this._reader.ReadDouble();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000041B1 File Offset: 0x000023B1
		private int ReadInt32()
		{
			this.MovePosition(4);
			return this._reader.ReadInt32();
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000041C5 File Offset: 0x000023C5
		private long ReadInt64()
		{
			this.MovePosition(8);
			return this._reader.ReadInt64();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000041D9 File Offset: 0x000023D9
		private BsonType ReadType()
		{
			this.MovePosition(1);
			return (BsonType)this._reader.ReadSByte();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000041ED File Offset: 0x000023ED
		private void MovePosition(int count)
		{
			this._currentContext.Position += count;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004202 File Offset: 0x00002402
		private byte[] ReadBytes(int count)
		{
			this.MovePosition(count);
			return this._reader.ReadBytes(count);
		}

		// Token: 0x0400002A RID: 42
		private const int MaxCharBytesSize = 128;

		// Token: 0x0400002B RID: 43
		private static readonly byte[] SeqRange1 = new byte[]
		{
			default(byte),
			127
		};

		// Token: 0x0400002C RID: 44
		private static readonly byte[] SeqRange2 = new byte[]
		{
			194,
			223
		};

		// Token: 0x0400002D RID: 45
		private static readonly byte[] SeqRange3 = new byte[]
		{
			224,
			239
		};

		// Token: 0x0400002E RID: 46
		private static readonly byte[] SeqRange4 = new byte[]
		{
			240,
			244
		};

		// Token: 0x0400002F RID: 47
		private readonly BinaryReader _reader;

		// Token: 0x04000030 RID: 48
		private readonly List<BsonReader.ContainerContext> _stack;

		// Token: 0x04000031 RID: 49
		private byte[] _byteBuffer;

		// Token: 0x04000032 RID: 50
		private char[] _charBuffer;

		// Token: 0x04000033 RID: 51
		private BsonType _currentElementType;

		// Token: 0x04000034 RID: 52
		private BsonReader.BsonReaderState _bsonReaderState;

		// Token: 0x04000035 RID: 53
		private BsonReader.ContainerContext _currentContext;

		// Token: 0x04000036 RID: 54
		private bool _readRootValueAsArray;

		// Token: 0x04000037 RID: 55
		private bool _jsonNet35BinaryCompatibility;

		// Token: 0x04000038 RID: 56
		private DateTimeKind _dateTimeKindHandling;

		// Token: 0x02000008 RID: 8
		private enum BsonReaderState
		{
			// Token: 0x0400003A RID: 58
			Normal,
			// Token: 0x0400003B RID: 59
			ReferenceStart,
			// Token: 0x0400003C RID: 60
			ReferenceRef,
			// Token: 0x0400003D RID: 61
			ReferenceId,
			// Token: 0x0400003E RID: 62
			CodeWScopeStart,
			// Token: 0x0400003F RID: 63
			CodeWScopeCode,
			// Token: 0x04000040 RID: 64
			CodeWScopeScope,
			// Token: 0x04000041 RID: 65
			CodeWScopeScopeObject,
			// Token: 0x04000042 RID: 66
			CodeWScopeScopeEnd
		}

		// Token: 0x02000009 RID: 9
		private class ContainerContext
		{
			// Token: 0x0600006E RID: 110 RVA: 0x0000428E File Offset: 0x0000248E
			public ContainerContext(BsonType type)
			{
				this.Type = type;
			}

			// Token: 0x04000043 RID: 67
			public readonly BsonType Type;

			// Token: 0x04000044 RID: 68
			public int Length;

			// Token: 0x04000045 RID: 69
			public int Position;
		}
	}
}
