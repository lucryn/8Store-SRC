using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	// Token: 0x0200002F RID: 47
	[NullableContext(2)]
	[Nullable(0)]
	public abstract class JsonReader : IDisposable
	{
		// Token: 0x060000E5 RID: 229 RVA: 0x000034EF File Offset: 0x000016EF
		[NullableContext(1)]
		public virtual Task<bool> ReadAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return cancellationToken.CancelIfRequestedAsync<bool>() ?? this.Read().ToAsync();
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00003508 File Offset: 0x00001708
		[NullableContext(1)]
		public Task SkipAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			JsonReader.<SkipAsync>d__1 <SkipAsync>d__;
			<SkipAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SkipAsync>d__.<>4__this = this;
			<SkipAsync>d__.cancellationToken = cancellationToken;
			<SkipAsync>d__.<>1__state = -1;
			<SkipAsync>d__.<>t__builder.Start<JsonReader.<SkipAsync>d__1>(ref <SkipAsync>d__);
			return <SkipAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00003554 File Offset: 0x00001754
		[NullableContext(1)]
		internal Task ReaderReadAndAssertAsync(CancellationToken cancellationToken)
		{
			JsonReader.<ReaderReadAndAssertAsync>d__2 <ReaderReadAndAssertAsync>d__;
			<ReaderReadAndAssertAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ReaderReadAndAssertAsync>d__.<>4__this = this;
			<ReaderReadAndAssertAsync>d__.cancellationToken = cancellationToken;
			<ReaderReadAndAssertAsync>d__.<>1__state = -1;
			<ReaderReadAndAssertAsync>d__.<>t__builder.Start<JsonReader.<ReaderReadAndAssertAsync>d__2>(ref <ReaderReadAndAssertAsync>d__);
			return <ReaderReadAndAssertAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000359F File Offset: 0x0000179F
		[NullableContext(1)]
		public virtual Task<bool?> ReadAsBooleanAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return cancellationToken.CancelIfRequestedAsync<bool?>() ?? Task.FromResult<bool?>(this.ReadAsBoolean());
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000035B6 File Offset: 0x000017B6
		[return: Nullable(new byte[]
		{
			1,
			2
		})]
		public virtual Task<byte[]> ReadAsBytesAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return cancellationToken.CancelIfRequestedAsync<byte[]>() ?? Task.FromResult<byte[]>(this.ReadAsBytes());
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000035D0 File Offset: 0x000017D0
		[return: Nullable(new byte[]
		{
			1,
			2
		})]
		internal Task<byte[]> ReadArrayIntoByteArrayAsync(CancellationToken cancellationToken)
		{
			JsonReader.<ReadArrayIntoByteArrayAsync>d__5 <ReadArrayIntoByteArrayAsync>d__;
			<ReadArrayIntoByteArrayAsync>d__.<>t__builder = AsyncTaskMethodBuilder<byte[]>.Create();
			<ReadArrayIntoByteArrayAsync>d__.<>4__this = this;
			<ReadArrayIntoByteArrayAsync>d__.cancellationToken = cancellationToken;
			<ReadArrayIntoByteArrayAsync>d__.<>1__state = -1;
			<ReadArrayIntoByteArrayAsync>d__.<>t__builder.Start<JsonReader.<ReadArrayIntoByteArrayAsync>d__5>(ref <ReadArrayIntoByteArrayAsync>d__);
			return <ReadArrayIntoByteArrayAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000361B File Offset: 0x0000181B
		[NullableContext(1)]
		public virtual Task<DateTime?> ReadAsDateTimeAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return cancellationToken.CancelIfRequestedAsync<DateTime?>() ?? Task.FromResult<DateTime?>(this.ReadAsDateTime());
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00003632 File Offset: 0x00001832
		[NullableContext(1)]
		public virtual Task<DateTimeOffset?> ReadAsDateTimeOffsetAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return cancellationToken.CancelIfRequestedAsync<DateTimeOffset?>() ?? Task.FromResult<DateTimeOffset?>(this.ReadAsDateTimeOffset());
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00003649 File Offset: 0x00001849
		[NullableContext(1)]
		public virtual Task<decimal?> ReadAsDecimalAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return cancellationToken.CancelIfRequestedAsync<decimal?>() ?? Task.FromResult<decimal?>(this.ReadAsDecimal());
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00003660 File Offset: 0x00001860
		[NullableContext(1)]
		public virtual Task<double?> ReadAsDoubleAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return Task.FromResult<double?>(this.ReadAsDouble());
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000366D File Offset: 0x0000186D
		[NullableContext(1)]
		public virtual Task<int?> ReadAsInt32Async(CancellationToken cancellationToken = default(CancellationToken))
		{
			return cancellationToken.CancelIfRequestedAsync<int?>() ?? Task.FromResult<int?>(this.ReadAsInt32());
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00003684 File Offset: 0x00001884
		[return: Nullable(new byte[]
		{
			1,
			2
		})]
		public virtual Task<string> ReadAsStringAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return cancellationToken.CancelIfRequestedAsync<string>() ?? Task.FromResult<string>(this.ReadAsString());
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000369C File Offset: 0x0000189C
		[NullableContext(1)]
		internal Task<bool> ReadAndMoveToContentAsync(CancellationToken cancellationToken)
		{
			JsonReader.<ReadAndMoveToContentAsync>d__12 <ReadAndMoveToContentAsync>d__;
			<ReadAndMoveToContentAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<ReadAndMoveToContentAsync>d__.<>4__this = this;
			<ReadAndMoveToContentAsync>d__.cancellationToken = cancellationToken;
			<ReadAndMoveToContentAsync>d__.<>1__state = -1;
			<ReadAndMoveToContentAsync>d__.<>t__builder.Start<JsonReader.<ReadAndMoveToContentAsync>d__12>(ref <ReadAndMoveToContentAsync>d__);
			return <ReadAndMoveToContentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000036E8 File Offset: 0x000018E8
		[NullableContext(1)]
		internal Task<bool> MoveToContentAsync(CancellationToken cancellationToken)
		{
			JsonToken tokenType = this.TokenType;
			if (tokenType == JsonToken.None || tokenType == JsonToken.Comment)
			{
				return this.MoveToContentFromNonContentAsync(cancellationToken);
			}
			return AsyncUtils.True;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00003710 File Offset: 0x00001910
		[NullableContext(1)]
		private Task<bool> MoveToContentFromNonContentAsync(CancellationToken cancellationToken)
		{
			JsonReader.<MoveToContentFromNonContentAsync>d__14 <MoveToContentFromNonContentAsync>d__;
			<MoveToContentFromNonContentAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<MoveToContentFromNonContentAsync>d__.<>4__this = this;
			<MoveToContentFromNonContentAsync>d__.cancellationToken = cancellationToken;
			<MoveToContentFromNonContentAsync>d__.<>1__state = -1;
			<MoveToContentFromNonContentAsync>d__.<>t__builder.Start<JsonReader.<MoveToContentFromNonContentAsync>d__14>(ref <MoveToContentFromNonContentAsync>d__);
			return <MoveToContentFromNonContentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x0000375B File Offset: 0x0000195B
		protected JsonReader.State CurrentState
		{
			get
			{
				return this._currentState;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00003763 File Offset: 0x00001963
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x0000376B File Offset: 0x0000196B
		public bool CloseInput { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00003774 File Offset: 0x00001974
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x0000377C File Offset: 0x0000197C
		public bool SupportMultipleContent { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00003785 File Offset: 0x00001985
		// (set) Token: 0x060000FA RID: 250 RVA: 0x0000378D File Offset: 0x0000198D
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

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00003796 File Offset: 0x00001996
		// (set) Token: 0x060000FC RID: 252 RVA: 0x0000379E File Offset: 0x0000199E
		public DateTimeZoneHandling DateTimeZoneHandling
		{
			get
			{
				return this._dateTimeZoneHandling;
			}
			set
			{
				if (value < DateTimeZoneHandling.Local || value > DateTimeZoneHandling.RoundtripKind)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._dateTimeZoneHandling = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000FD RID: 253 RVA: 0x000037BA File Offset: 0x000019BA
		// (set) Token: 0x060000FE RID: 254 RVA: 0x000037C2 File Offset: 0x000019C2
		public DateParseHandling DateParseHandling
		{
			get
			{
				return this._dateParseHandling;
			}
			set
			{
				if (value < DateParseHandling.None || value > DateParseHandling.DateTimeOffset)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._dateParseHandling = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000FF RID: 255 RVA: 0x000037DE File Offset: 0x000019DE
		// (set) Token: 0x06000100 RID: 256 RVA: 0x000037E6 File Offset: 0x000019E6
		public FloatParseHandling FloatParseHandling
		{
			get
			{
				return this._floatParseHandling;
			}
			set
			{
				if (value < FloatParseHandling.Double || value > FloatParseHandling.Decimal)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._floatParseHandling = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00003802 File Offset: 0x00001A02
		// (set) Token: 0x06000102 RID: 258 RVA: 0x0000380A File Offset: 0x00001A0A
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

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00003813 File Offset: 0x00001A13
		// (set) Token: 0x06000104 RID: 260 RVA: 0x0000381C File Offset: 0x00001A1C
		public int? MaxDepth
		{
			get
			{
				return this._maxDepth;
			}
			set
			{
				int? num = value;
				int num2 = 0;
				if (num.GetValueOrDefault() <= num2 & num != null)
				{
					throw new ArgumentException("Value must be positive.", "value");
				}
				this._maxDepth = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000105 RID: 261 RVA: 0x0000385B File Offset: 0x00001A5B
		public virtual JsonToken TokenType
		{
			get
			{
				return this._tokenType;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00003863 File Offset: 0x00001A63
		public virtual object Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000107 RID: 263 RVA: 0x0000386B File Offset: 0x00001A6B
		public virtual Type ValueType
		{
			get
			{
				object value = this._value;
				if (value == null)
				{
					return null;
				}
				return value.GetType();
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00003880 File Offset: 0x00001A80
		public virtual int Depth
		{
			get
			{
				List<JsonPosition> stack = this._stack;
				int num = (stack != null) ? stack.Count : 0;
				if (JsonTokenUtils.IsStartToken(this.TokenType) || this._currentPosition.Type == JsonContainerType.None)
				{
					return num;
				}
				return num + 1;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000109 RID: 265 RVA: 0x000038C0 File Offset: 0x00001AC0
		[Nullable(1)]
		public virtual string Path
		{
			[NullableContext(1)]
			get
			{
				if (this._currentPosition.Type == JsonContainerType.None)
				{
					return string.Empty;
				}
				JsonPosition? currentPosition = (this._currentState != JsonReader.State.ArrayStart && this._currentState != JsonReader.State.ConstructorStart && this._currentState != JsonReader.State.ObjectStart) ? new JsonPosition?(this._currentPosition) : default(JsonPosition?);
				return JsonPosition.BuildPath(this._stack, currentPosition);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00003927 File Offset: 0x00001B27
		// (set) Token: 0x0600010B RID: 267 RVA: 0x00003938 File Offset: 0x00001B38
		[Nullable(1)]
		public CultureInfo Culture
		{
			[NullableContext(1)]
			get
			{
				return this._culture ?? CultureInfo.InvariantCulture;
			}
			[NullableContext(1)]
			set
			{
				this._culture = value;
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00003941 File Offset: 0x00001B41
		internal JsonPosition GetPosition(int depth)
		{
			if (this._stack != null && depth < this._stack.Count)
			{
				return this._stack[depth];
			}
			return this._currentPosition;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000396C File Offset: 0x00001B6C
		protected JsonReader()
		{
			this._currentState = JsonReader.State.Start;
			this._dateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind;
			this._dateParseHandling = DateParseHandling.DateTime;
			this._floatParseHandling = FloatParseHandling.Double;
			this._maxDepth = new int?(64);
			this.CloseInput = true;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000039A4 File Offset: 0x00001BA4
		private void Push(JsonContainerType value)
		{
			this.UpdateScopeWithFinishedValue();
			if (this._currentPosition.Type == JsonContainerType.None)
			{
				this._currentPosition = new JsonPosition(value);
				return;
			}
			if (this._stack == null)
			{
				this._stack = new List<JsonPosition>();
			}
			this._stack.Add(this._currentPosition);
			this._currentPosition = new JsonPosition(value);
			if (this._maxDepth != null)
			{
				int num = this.Depth + 1;
				int? maxDepth = this._maxDepth;
				if ((num > maxDepth.GetValueOrDefault() & maxDepth != null) && !this._hasExceededMaxDepth)
				{
					this._hasExceededMaxDepth = true;
					throw JsonReaderException.Create(this, "The reader's MaxDepth of {0} has been exceeded.".FormatWith(CultureInfo.InvariantCulture, this._maxDepth));
				}
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00003A60 File Offset: 0x00001C60
		private JsonContainerType Pop()
		{
			JsonPosition currentPosition;
			if (this._stack != null && this._stack.Count > 0)
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
			if (this._maxDepth != null)
			{
				int depth = this.Depth;
				int? maxDepth = this._maxDepth;
				if (depth <= maxDepth.GetValueOrDefault() & maxDepth != null)
				{
					this._hasExceededMaxDepth = false;
				}
			}
			return currentPosition.Type;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00003B12 File Offset: 0x00001D12
		private JsonContainerType Peek()
		{
			return this._currentPosition.Type;
		}

		// Token: 0x06000111 RID: 273
		public abstract bool Read();

		// Token: 0x06000112 RID: 274 RVA: 0x00003B20 File Offset: 0x00001D20
		public virtual int? ReadAsInt32()
		{
			JsonToken contentToken = this.GetContentToken();
			if (contentToken != JsonToken.None)
			{
				switch (contentToken)
				{
				case JsonToken.Integer:
				case JsonToken.Float:
				{
					object value = this.Value;
					int num;
					if (value is int)
					{
						num = (int)value;
						return new int?(num);
					}
					try
					{
						num = Convert.ToInt32(value, CultureInfo.InvariantCulture);
					}
					catch (Exception ex)
					{
						throw JsonReaderException.Create(this, "Could not convert to integer: {0}.".FormatWith(CultureInfo.InvariantCulture, value), ex);
					}
					this.SetToken(JsonToken.Integer, num, false);
					return new int?(num);
				}
				case JsonToken.String:
				{
					string s = (string)this.Value;
					return this.ReadInt32String(s);
				}
				case JsonToken.Null:
				case JsonToken.EndArray:
					goto IL_34;
				}
				throw JsonReaderException.Create(this, "Error reading integer. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, contentToken));
			}
			IL_34:
			return default(int?);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00003C08 File Offset: 0x00001E08
		internal int? ReadInt32String(string s)
		{
			if (StringUtils.IsNullOrEmpty(s))
			{
				this.SetToken(JsonToken.Null, null, false);
				return default(int?);
			}
			int num;
			if (int.TryParse(s, 7, this.Culture, ref num))
			{
				this.SetToken(JsonToken.Integer, num, false);
				return new int?(num);
			}
			this.SetToken(JsonToken.String, s, false);
			throw JsonReaderException.Create(this, "Could not convert string to integer: {0}.".FormatWith(CultureInfo.InvariantCulture, s));
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00003C78 File Offset: 0x00001E78
		public virtual string ReadAsString()
		{
			JsonToken contentToken = this.GetContentToken();
			if (contentToken <= JsonToken.String)
			{
				if (contentToken != JsonToken.None)
				{
					if (contentToken != JsonToken.String)
					{
						goto IL_2E;
					}
					return (string)this.Value;
				}
			}
			else if (contentToken != JsonToken.Null && contentToken != JsonToken.EndArray)
			{
				goto IL_2E;
			}
			return null;
			IL_2E:
			if (JsonTokenUtils.IsPrimitiveToken(contentToken))
			{
				object value = this.Value;
				if (value != null)
				{
					IFormattable formattable = value as IFormattable;
					string text;
					if (formattable != null)
					{
						text = formattable.ToString(null, this.Culture);
					}
					else
					{
						Uri uri = value as Uri;
						text = ((uri != null) ? uri.OriginalString : value.ToString());
					}
					this.SetToken(JsonToken.String, text, false);
					return text;
				}
			}
			throw JsonReaderException.Create(this, "Error reading string. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, contentToken));
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00003D24 File Offset: 0x00001F24
		public virtual byte[] ReadAsBytes()
		{
			JsonToken contentToken = this.GetContentToken();
			if (contentToken <= JsonToken.String)
			{
				switch (contentToken)
				{
				case JsonToken.None:
					break;
				case JsonToken.StartObject:
				{
					this.ReadIntoWrappedTypeObject();
					byte[] array = this.ReadAsBytes();
					this.ReaderReadAndAssert();
					if (this.TokenType != JsonToken.EndObject)
					{
						throw JsonReaderException.Create(this, "Error reading bytes. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, this.TokenType));
					}
					this.SetToken(JsonToken.Bytes, array, false);
					return array;
				}
				case JsonToken.StartArray:
					return this.ReadArrayIntoByteArray();
				default:
				{
					if (contentToken != JsonToken.String)
					{
						goto IL_11C;
					}
					string text = (string)this.Value;
					byte[] array2;
					Guid guid;
					if (text.Length == 0)
					{
						array2 = CollectionUtils.ArrayEmpty<byte>();
					}
					else if (ConvertUtils.TryConvertGuid(text, out guid))
					{
						array2 = guid.ToByteArray();
					}
					else
					{
						array2 = Convert.FromBase64String(text);
					}
					this.SetToken(JsonToken.Bytes, array2, false);
					return array2;
				}
				}
			}
			else if (contentToken != JsonToken.Null && contentToken != JsonToken.EndArray)
			{
				if (contentToken != JsonToken.Bytes)
				{
					goto IL_11C;
				}
				object value = this.Value;
				if (value is Guid)
				{
					byte[] array3 = ((Guid)value).ToByteArray();
					this.SetToken(JsonToken.Bytes, array3, false);
					return array3;
				}
				return (byte[])this.Value;
			}
			return null;
			IL_11C:
			throw JsonReaderException.Create(this, "Error reading bytes. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, contentToken));
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00003E68 File Offset: 0x00002068
		[NullableContext(1)]
		internal byte[] ReadArrayIntoByteArray()
		{
			List<byte> list = new List<byte>();
			do
			{
				if (!this.Read())
				{
					this.SetToken(JsonToken.None);
				}
			}
			while (!this.ReadArrayElementIntoByteArrayReportDone(list));
			byte[] array = list.ToArray();
			this.SetToken(JsonToken.Bytes, array, false);
			return array;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00003EA8 File Offset: 0x000020A8
		[NullableContext(1)]
		private bool ReadArrayElementIntoByteArrayReportDone(List<byte> buffer)
		{
			JsonToken tokenType = this.TokenType;
			if (tokenType <= JsonToken.Comment)
			{
				if (tokenType == JsonToken.None)
				{
					throw JsonReaderException.Create(this, "Unexpected end when reading bytes.");
				}
				if (tokenType == JsonToken.Comment)
				{
					return false;
				}
			}
			else
			{
				if (tokenType == JsonToken.Integer)
				{
					buffer.Add(Convert.ToByte(this.Value, CultureInfo.InvariantCulture));
					return false;
				}
				if (tokenType == JsonToken.EndArray)
				{
					return true;
				}
			}
			throw JsonReaderException.Create(this, "Unexpected token when reading bytes: {0}.".FormatWith(CultureInfo.InvariantCulture, this.TokenType));
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00003F1C File Offset: 0x0000211C
		public virtual double? ReadAsDouble()
		{
			JsonToken contentToken = this.GetContentToken();
			if (contentToken != JsonToken.None)
			{
				switch (contentToken)
				{
				case JsonToken.Integer:
				case JsonToken.Float:
				{
					object value = this.Value;
					double num;
					if (value is double)
					{
						num = (double)value;
						return new double?(num);
					}
					num = Convert.ToDouble(value, CultureInfo.InvariantCulture);
					this.SetToken(JsonToken.Float, num, false);
					return new double?(num);
				}
				case JsonToken.String:
					return this.ReadDoubleString((string)this.Value);
				case JsonToken.Null:
				case JsonToken.EndArray:
					goto IL_34;
				}
				throw JsonReaderException.Create(this, "Error reading double. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, contentToken));
			}
			IL_34:
			return default(double?);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00003FD4 File Offset: 0x000021D4
		internal double? ReadDoubleString(string s)
		{
			if (StringUtils.IsNullOrEmpty(s))
			{
				this.SetToken(JsonToken.Null, null, false);
				return default(double?);
			}
			double num;
			if (double.TryParse(s, 231, this.Culture, ref num))
			{
				this.SetToken(JsonToken.Float, num, false);
				return new double?(num);
			}
			this.SetToken(JsonToken.String, s, false);
			throw JsonReaderException.Create(this, "Could not convert string to double: {0}.".FormatWith(CultureInfo.InvariantCulture, s));
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004048 File Offset: 0x00002248
		public virtual bool? ReadAsBoolean()
		{
			JsonToken contentToken = this.GetContentToken();
			if (contentToken != JsonToken.None)
			{
				switch (contentToken)
				{
				case JsonToken.Integer:
				case JsonToken.Float:
				{
					bool flag = Convert.ToBoolean(this.Value, CultureInfo.InvariantCulture);
					this.SetToken(JsonToken.Boolean, flag, false);
					return new bool?(flag);
				}
				case JsonToken.String:
					return this.ReadBooleanString((string)this.Value);
				case JsonToken.Boolean:
					return new bool?((bool)this.Value);
				case JsonToken.Null:
				case JsonToken.EndArray:
					goto IL_34;
				}
				throw JsonReaderException.Create(this, "Error reading boolean. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, contentToken));
			}
			IL_34:
			return default(bool?);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000040F8 File Offset: 0x000022F8
		internal bool? ReadBooleanString(string s)
		{
			if (StringUtils.IsNullOrEmpty(s))
			{
				this.SetToken(JsonToken.Null, null, false);
				return default(bool?);
			}
			bool flag;
			if (bool.TryParse(s, ref flag))
			{
				this.SetToken(JsonToken.Boolean, flag, false);
				return new bool?(flag);
			}
			this.SetToken(JsonToken.String, s, false);
			throw JsonReaderException.Create(this, "Could not convert string to boolean: {0}.".FormatWith(CultureInfo.InvariantCulture, s));
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00004164 File Offset: 0x00002364
		public virtual decimal? ReadAsDecimal()
		{
			JsonToken contentToken = this.GetContentToken();
			if (contentToken != JsonToken.None)
			{
				switch (contentToken)
				{
				case JsonToken.Integer:
				case JsonToken.Float:
				{
					object value = this.Value;
					decimal num;
					if (value is decimal)
					{
						num = (decimal)value;
						return new decimal?(num);
					}
					try
					{
						num = Convert.ToDecimal(value, CultureInfo.InvariantCulture);
					}
					catch (Exception ex)
					{
						throw JsonReaderException.Create(this, "Could not convert to decimal: {0}.".FormatWith(CultureInfo.InvariantCulture, value), ex);
					}
					this.SetToken(JsonToken.Float, num, false);
					return new decimal?(num);
				}
				case JsonToken.String:
					return this.ReadDecimalString((string)this.Value);
				case JsonToken.Null:
				case JsonToken.EndArray:
					goto IL_34;
				}
				throw JsonReaderException.Create(this, "Error reading decimal. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, contentToken));
			}
			IL_34:
			return default(decimal?);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00004248 File Offset: 0x00002448
		internal decimal? ReadDecimalString(string s)
		{
			if (StringUtils.IsNullOrEmpty(s))
			{
				this.SetToken(JsonToken.Null, null, false);
				return default(decimal?);
			}
			decimal num;
			if (decimal.TryParse(s, 111, this.Culture, ref num))
			{
				this.SetToken(JsonToken.Float, num, false);
				return new decimal?(num);
			}
			if (ConvertUtils.DecimalTryParse(s.ToCharArray(), 0, s.Length, out num) == ParseResult.Success)
			{
				this.SetToken(JsonToken.Float, num, false);
				return new decimal?(num);
			}
			this.SetToken(JsonToken.String, s, false);
			throw JsonReaderException.Create(this, "Could not convert string to decimal: {0}.".FormatWith(CultureInfo.InvariantCulture, s));
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000042E4 File Offset: 0x000024E4
		public virtual DateTime? ReadAsDateTime()
		{
			JsonToken contentToken = this.GetContentToken();
			if (contentToken <= JsonToken.String)
			{
				if (contentToken != JsonToken.None)
				{
					if (contentToken != JsonToken.String)
					{
						goto IL_7F;
					}
					return this.ReadDateTimeString((string)this.Value);
				}
			}
			else if (contentToken != JsonToken.Null && contentToken != JsonToken.EndArray)
			{
				if (contentToken != JsonToken.Date)
				{
					goto IL_7F;
				}
				object value = this.Value;
				if (value is DateTimeOffset)
				{
					this.SetToken(JsonToken.Date, ((DateTimeOffset)value).DateTime, false);
				}
				return new DateTime?((DateTime)this.Value);
			}
			return default(DateTime?);
			IL_7F:
			throw JsonReaderException.Create(this, "Error reading date. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, this.TokenType));
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00004390 File Offset: 0x00002590
		internal DateTime? ReadDateTimeString(string s)
		{
			if (StringUtils.IsNullOrEmpty(s))
			{
				this.SetToken(JsonToken.Null, null, false);
				return default(DateTime?);
			}
			DateTime dateTime;
			if (DateTimeUtils.TryParseDateTime(s, this.DateTimeZoneHandling, this._dateFormatString, this.Culture, out dateTime))
			{
				dateTime = DateTimeUtils.EnsureDateTime(dateTime, this.DateTimeZoneHandling);
				this.SetToken(JsonToken.Date, dateTime, false);
				return new DateTime?(dateTime);
			}
			if (DateTime.TryParse(s, this.Culture, 128, ref dateTime))
			{
				dateTime = DateTimeUtils.EnsureDateTime(dateTime, this.DateTimeZoneHandling);
				this.SetToken(JsonToken.Date, dateTime, false);
				return new DateTime?(dateTime);
			}
			throw JsonReaderException.Create(this, "Could not convert string to DateTime: {0}.".FormatWith(CultureInfo.InvariantCulture, s));
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004448 File Offset: 0x00002648
		public virtual DateTimeOffset? ReadAsDateTimeOffset()
		{
			JsonToken contentToken = this.GetContentToken();
			if (contentToken <= JsonToken.String)
			{
				if (contentToken != JsonToken.None)
				{
					if (contentToken != JsonToken.String)
					{
						goto IL_83;
					}
					string s = (string)this.Value;
					return this.ReadDateTimeOffsetString(s);
				}
			}
			else if (contentToken != JsonToken.Null && contentToken != JsonToken.EndArray)
			{
				if (contentToken != JsonToken.Date)
				{
					goto IL_83;
				}
				object value = this.Value;
				if (value is DateTime)
				{
					DateTime dateTime = (DateTime)value;
					this.SetToken(JsonToken.Date, new DateTimeOffset(dateTime), false);
				}
				return new DateTimeOffset?((DateTimeOffset)this.Value);
			}
			return default(DateTimeOffset?);
			IL_83:
			throw JsonReaderException.Create(this, "Error reading date. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, contentToken));
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000044F4 File Offset: 0x000026F4
		internal DateTimeOffset? ReadDateTimeOffsetString(string s)
		{
			if (StringUtils.IsNullOrEmpty(s))
			{
				this.SetToken(JsonToken.Null, null, false);
				return default(DateTimeOffset?);
			}
			DateTimeOffset dateTimeOffset;
			if (DateTimeUtils.TryParseDateTimeOffset(s, this._dateFormatString, this.Culture, out dateTimeOffset))
			{
				this.SetToken(JsonToken.Date, dateTimeOffset, false);
				return new DateTimeOffset?(dateTimeOffset);
			}
			if (DateTimeOffset.TryParse(s, this.Culture, 128, ref dateTimeOffset))
			{
				this.SetToken(JsonToken.Date, dateTimeOffset, false);
				return new DateTimeOffset?(dateTimeOffset);
			}
			this.SetToken(JsonToken.String, s, false);
			throw JsonReaderException.Create(this, "Could not convert string to DateTimeOffset: {0}.".FormatWith(CultureInfo.InvariantCulture, s));
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004594 File Offset: 0x00002794
		internal void ReaderReadAndAssert()
		{
			if (!this.Read())
			{
				throw this.CreateUnexpectedEndException();
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000045A5 File Offset: 0x000027A5
		[NullableContext(1)]
		internal JsonReaderException CreateUnexpectedEndException()
		{
			return JsonReaderException.Create(this, "Unexpected end when reading JSON.");
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000045B4 File Offset: 0x000027B4
		internal void ReadIntoWrappedTypeObject()
		{
			this.ReaderReadAndAssert();
			if (this.Value != null && this.Value.ToString() == "$type")
			{
				this.ReaderReadAndAssert();
				if (this.Value != null && this.Value.ToString().StartsWith("System.Byte[]", 4))
				{
					this.ReaderReadAndAssert();
					if (this.Value.ToString() == "$value")
					{
						return;
					}
				}
			}
			throw JsonReaderException.Create(this, "Error reading bytes. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, JsonToken.StartObject));
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00004648 File Offset: 0x00002848
		public void Skip()
		{
			if (this.TokenType == JsonToken.PropertyName)
			{
				this.Read();
			}
			if (JsonTokenUtils.IsStartToken(this.TokenType))
			{
				int depth = this.Depth;
				while (this.Read() && depth < this.Depth)
				{
				}
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000468A File Offset: 0x0000288A
		protected void SetToken(JsonToken newToken)
		{
			this.SetToken(newToken, null, true);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00004695 File Offset: 0x00002895
		protected void SetToken(JsonToken newToken, object value)
		{
			this.SetToken(newToken, value, true);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000046A0 File Offset: 0x000028A0
		protected void SetToken(JsonToken newToken, object value, bool updateIndex)
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
				this.SetPostValueState(updateIndex);
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

		// Token: 0x06000129 RID: 297 RVA: 0x00004771 File Offset: 0x00002971
		internal void SetPostValueState(bool updateIndex)
		{
			if (this.Peek() != JsonContainerType.None || this.SupportMultipleContent)
			{
				this._currentState = JsonReader.State.PostValue;
			}
			else
			{
				this.SetFinished();
			}
			if (updateIndex)
			{
				this.UpdateScopeWithFinishedValue();
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000479B File Offset: 0x0000299B
		private void UpdateScopeWithFinishedValue()
		{
			if (this._currentPosition.HasIndex)
			{
				this._currentPosition.Position = this._currentPosition.Position + 1;
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000047BC File Offset: 0x000029BC
		private void ValidateEnd(JsonToken endToken)
		{
			JsonContainerType jsonContainerType = this.Pop();
			if (this.GetTypeForCloseToken(endToken) != jsonContainerType)
			{
				throw JsonReaderException.Create(this, "JsonToken {0} is not valid for closing JsonType {1}.".FormatWith(CultureInfo.InvariantCulture, endToken, jsonContainerType));
			}
			if (this.Peek() != JsonContainerType.None || this.SupportMultipleContent)
			{
				this._currentState = JsonReader.State.PostValue;
				return;
			}
			this.SetFinished();
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000481C File Offset: 0x00002A1C
		protected void SetStateBasedOnCurrent()
		{
			JsonContainerType jsonContainerType = this.Peek();
			switch (jsonContainerType)
			{
			case JsonContainerType.None:
				this.SetFinished();
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

		// Token: 0x0600012D RID: 301 RVA: 0x00004883 File Offset: 0x00002A83
		private void SetFinished()
		{
			this._currentState = (this.SupportMultipleContent ? JsonReader.State.Start : JsonReader.State.Finished);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00004898 File Offset: 0x00002A98
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

		// Token: 0x0600012F RID: 303 RVA: 0x000048D2 File Offset: 0x00002AD2
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000048E1 File Offset: 0x00002AE1
		protected virtual void Dispose(bool disposing)
		{
			if (this._currentState != JsonReader.State.Closed && disposing)
			{
				this.Close();
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000048F9 File Offset: 0x00002AF9
		public virtual void Close()
		{
			this._currentState = JsonReader.State.Closed;
			this._tokenType = JsonToken.None;
			this._value = null;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00004910 File Offset: 0x00002B10
		internal void ReadAndAssert()
		{
			if (!this.Read())
			{
				throw JsonSerializationException.Create(this, "Unexpected end when reading JSON.");
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00004926 File Offset: 0x00002B26
		internal void ReadForTypeAndAssert(JsonContract contract, bool hasConverter)
		{
			if (!this.ReadForType(contract, hasConverter))
			{
				throw JsonSerializationException.Create(this, "Unexpected end when reading JSON.");
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00004940 File Offset: 0x00002B40
		internal bool ReadForType(JsonContract contract, bool hasConverter)
		{
			if (hasConverter)
			{
				return this.Read();
			}
			switch ((contract != null) ? contract.InternalReadType : ReadType.Read)
			{
			case ReadType.Read:
				return this.ReadAndMoveToContent();
			case ReadType.ReadAsInt32:
				this.ReadAsInt32();
				break;
			case ReadType.ReadAsInt64:
			{
				bool result = this.ReadAndMoveToContent();
				if (this.TokenType == JsonToken.Undefined)
				{
					throw JsonReaderException.Create(this, "An undefined token is not a valid {0}.".FormatWith(CultureInfo.InvariantCulture, ((contract != null) ? contract.UnderlyingType : null) ?? typeof(long)));
				}
				return result;
			}
			case ReadType.ReadAsBytes:
				this.ReadAsBytes();
				break;
			case ReadType.ReadAsString:
				this.ReadAsString();
				break;
			case ReadType.ReadAsDecimal:
				this.ReadAsDecimal();
				break;
			case ReadType.ReadAsDateTime:
				this.ReadAsDateTime();
				break;
			case ReadType.ReadAsDateTimeOffset:
				this.ReadAsDateTimeOffset();
				break;
			case ReadType.ReadAsDouble:
				this.ReadAsDouble();
				break;
			case ReadType.ReadAsBoolean:
				this.ReadAsBoolean();
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			return this.TokenType > JsonToken.None;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004A39 File Offset: 0x00002C39
		internal bool ReadAndMoveToContent()
		{
			return this.Read() && this.MoveToContent();
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00004A4C File Offset: 0x00002C4C
		internal bool MoveToContent()
		{
			JsonToken tokenType = this.TokenType;
			while (tokenType == JsonToken.None || tokenType == JsonToken.Comment)
			{
				if (!this.Read())
				{
					return false;
				}
				tokenType = this.TokenType;
			}
			return true;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00004A7C File Offset: 0x00002C7C
		private JsonToken GetContentToken()
		{
			while (this.Read())
			{
				JsonToken tokenType = this.TokenType;
				if (tokenType != JsonToken.Comment)
				{
					return tokenType;
				}
			}
			this.SetToken(JsonToken.None);
			return JsonToken.None;
		}

		// Token: 0x0400007F RID: 127
		private JsonToken _tokenType;

		// Token: 0x04000080 RID: 128
		private object _value;

		// Token: 0x04000081 RID: 129
		internal char _quoteChar;

		// Token: 0x04000082 RID: 130
		internal JsonReader.State _currentState;

		// Token: 0x04000083 RID: 131
		private JsonPosition _currentPosition;

		// Token: 0x04000084 RID: 132
		private CultureInfo _culture;

		// Token: 0x04000085 RID: 133
		private DateTimeZoneHandling _dateTimeZoneHandling;

		// Token: 0x04000086 RID: 134
		private int? _maxDepth;

		// Token: 0x04000087 RID: 135
		private bool _hasExceededMaxDepth;

		// Token: 0x04000088 RID: 136
		internal DateParseHandling _dateParseHandling;

		// Token: 0x04000089 RID: 137
		internal FloatParseHandling _floatParseHandling;

		// Token: 0x0400008A RID: 138
		private string _dateFormatString;

		// Token: 0x0400008B RID: 139
		private List<JsonPosition> _stack;

		// Token: 0x02000115 RID: 277
		[NullableContext(0)]
		protected internal enum State
		{
			// Token: 0x040004A6 RID: 1190
			Start,
			// Token: 0x040004A7 RID: 1191
			Complete,
			// Token: 0x040004A8 RID: 1192
			Property,
			// Token: 0x040004A9 RID: 1193
			ObjectStart,
			// Token: 0x040004AA RID: 1194
			Object,
			// Token: 0x040004AB RID: 1195
			ArrayStart,
			// Token: 0x040004AC RID: 1196
			Array,
			// Token: 0x040004AD RID: 1197
			Closed,
			// Token: 0x040004AE RID: 1198
			PostValue,
			// Token: 0x040004AF RID: 1199
			ConstructorStart,
			// Token: 0x040004B0 RID: 1200
			Constructor,
			// Token: 0x040004B1 RID: 1201
			Error,
			// Token: 0x040004B2 RID: 1202
			Finished
		}
	}
}
