using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000106 RID: 262
	[Obsolete("BSON reading and writing has been moved to its own package. See https://www.nuget.org/packages/Newtonsoft.Json.Bson for more details.")]
	public class BsonReader : JsonReader
	{
		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x0003359E File Offset: 0x0003179E
		// (set) Token: 0x06000CEF RID: 3311 RVA: 0x000335A6 File Offset: 0x000317A6
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

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x000335AF File Offset: 0x000317AF
		// (set) Token: 0x06000CF1 RID: 3313 RVA: 0x000335B7 File Offset: 0x000317B7
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

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x000335C0 File Offset: 0x000317C0
		// (set) Token: 0x06000CF3 RID: 3315 RVA: 0x000335C8 File Offset: 0x000317C8
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

		// Token: 0x06000CF4 RID: 3316 RVA: 0x000335D1 File Offset: 0x000317D1
		public BsonReader(Stream stream) : this(stream, false, 2)
		{
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x000335DC File Offset: 0x000317DC
		public BsonReader(BinaryReader reader) : this(reader, false, 2)
		{
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x000335E7 File Offset: 0x000317E7
		public BsonReader(Stream stream, bool readRootValueAsArray, DateTimeKind dateTimeKindHandling)
		{
			ValidationUtils.ArgumentNotNull(stream, "stream");
			this._reader = new BinaryReader(stream);
			this._stack = new List<BsonReader.ContainerContext>();
			this._readRootValueAsArray = readRootValueAsArray;
			this._dateTimeKindHandling = dateTimeKindHandling;
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x0003361F File Offset: 0x0003181F
		public BsonReader(BinaryReader reader, bool readRootValueAsArray, DateTimeKind dateTimeKindHandling)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			this._reader = reader;
			this._stack = new List<BsonReader.ContainerContext>();
			this._readRootValueAsArray = readRootValueAsArray;
			this._dateTimeKindHandling = dateTimeKindHandling;
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x00033652 File Offset: 0x00031852
		private string ReadElement()
		{
			this._currentElementType = this.ReadType();
			return this.ReadString();
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x00033668 File Offset: 0x00031868
		public override bool Read()
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

		// Token: 0x06000CFA RID: 3322 RVA: 0x00033714 File Offset: 0x00031914
		public override void Close()
		{
			base.Close();
			if (base.CloseInput)
			{
				BinaryReader reader = this._reader;
				if (reader == null)
				{
					return;
				}
				reader.Dispose();
			}
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x00033734 File Offset: 0x00031934
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

		// Token: 0x06000CFC RID: 3324 RVA: 0x00033810 File Offset: 0x00031A10
		private bool ReadReference()
		{
			JsonReader.State currentState = base.CurrentState;
			if (currentState != JsonReader.State.Property)
			{
				if (currentState == JsonReader.State.ObjectStart)
				{
					base.SetToken(JsonToken.PropertyName, "$ref");
					this._bsonReaderState = BsonReader.BsonReaderState.ReferenceRef;
					return true;
				}
				if (currentState != JsonReader.State.PostValue)
				{
					throw JsonReaderException.Create(this, "Unexpected state when reading BSON reference: " + base.CurrentState.ToString());
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
				throw JsonReaderException.Create(this, "Unexpected state when reading BSON reference: " + this._bsonReaderState.ToString());
			}
			else
			{
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
				throw JsonReaderException.Create(this, "Unexpected state when reading BSON reference: " + this._bsonReaderState.ToString());
			}
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x00033920 File Offset: 0x00031B20
		private bool ReadNormal()
		{
			switch (base.CurrentState)
			{
			case JsonReader.State.Start:
				break;
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
					if (!base.SupportMultipleContent)
					{
						return false;
					}
				}
				else
				{
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
						JsonToken token = (currentContext.Type == BsonType.Object) ? JsonToken.EndObject : JsonToken.EndArray;
						base.SetToken(token);
						return true;
					}
				}
				break;
			}
			case JsonReader.State.Object:
			case JsonReader.State.Array:
				goto IL_145;
			case JsonReader.State.ConstructorStart:
			case JsonReader.State.Constructor:
			case JsonReader.State.Error:
			case JsonReader.State.Finished:
				return false;
			default:
				goto IL_145;
			}
			JsonToken token2 = (!this._readRootValueAsArray) ? JsonToken.StartObject : JsonToken.StartArray;
			BsonType type = (!this._readRootValueAsArray) ? BsonType.Object : BsonType.Array;
			base.SetToken(token2);
			BsonReader.ContainerContext containerContext = new BsonReader.ContainerContext(type);
			this.PushContext(containerContext);
			containerContext.Length = this.ReadInt32();
			return true;
			IL_145:
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x00033A7C File Offset: 0x00031C7C
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

		// Token: 0x06000CFF RID: 3327 RVA: 0x00033AD4 File Offset: 0x00031CD4
		private void PushContext(BsonReader.ContainerContext newContext)
		{
			this._stack.Add(newContext);
			this._currentContext = newContext;
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x00033AE9 File Offset: 0x00031CE9
		private byte ReadByte()
		{
			this.MovePosition(1);
			return this._reader.ReadByte();
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x00033B00 File Offset: 0x00031D00
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
			{
				BsonBinaryType bsonBinaryType;
				byte[] array = this.ReadBinary(out bsonBinaryType);
				object value = (bsonBinaryType != BsonBinaryType.Uuid) ? array : new Guid(array);
				base.SetToken(JsonToken.Bytes, value);
				return;
			}
			case BsonType.Undefined:
				base.SetToken(JsonToken.Undefined);
				return;
			case BsonType.Oid:
			{
				byte[] value2 = this.ReadBytes(12);
				base.SetToken(JsonToken.Bytes, value2);
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
				DateTime dateTime = DateTimeUtils.ConvertJavaScriptTicksToDateTime(this.ReadInt64());
				DateTimeKind dateTimeKindHandling = this.DateTimeKindHandling;
				DateTime dateTime2;
				if (dateTimeKindHandling != null)
				{
					if (dateTimeKindHandling != 2)
					{
						dateTime2 = dateTime;
					}
					else
					{
						dateTime2 = dateTime.ToLocalTime();
					}
				}
				else
				{
					dateTime2 = DateTime.SpecifyKind(dateTime, 0);
				}
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
				string value3 = "/" + text + "/" + text2;
				base.SetToken(JsonToken.String, value3);
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
				throw new ArgumentOutOfRangeException("type", "Unexpected BsonType value: " + type.ToString());
			}
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x00033D50 File Offset: 0x00031F50
		private byte[] ReadBinary(out BsonBinaryType binaryType)
		{
			int count = this.ReadInt32();
			binaryType = (BsonBinaryType)this.ReadByte();
			if (binaryType == BsonBinaryType.BinaryOld && !this._jsonNet35BinaryCompatibility)
			{
				count = this.ReadInt32();
			}
			return this.ReadBytes(count);
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x00033D88 File Offset: 0x00031F88
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

		// Token: 0x06000D04 RID: 3332 RVA: 0x00033EA8 File Offset: 0x000320A8
		private string ReadLengthString()
		{
			int num = this.ReadInt32();
			this.MovePosition(num);
			string @string = this.GetString(num - 1);
			this._reader.ReadByte();
			return @string;
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x00033ED8 File Offset: 0x000320D8
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

		// Token: 0x06000D06 RID: 3334 RVA: 0x00033FF0 File Offset: 0x000321F0
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

		// Token: 0x06000D07 RID: 3335 RVA: 0x00034034 File Offset: 0x00032234
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

		// Token: 0x06000D08 RID: 3336 RVA: 0x00034090 File Offset: 0x00032290
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

		// Token: 0x06000D09 RID: 3337 RVA: 0x000340D9 File Offset: 0x000322D9
		private double ReadDouble()
		{
			this.MovePosition(8);
			return this._reader.ReadDouble();
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x000340ED File Offset: 0x000322ED
		private int ReadInt32()
		{
			this.MovePosition(4);
			return this._reader.ReadInt32();
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x00034101 File Offset: 0x00032301
		private long ReadInt64()
		{
			this.MovePosition(8);
			return this._reader.ReadInt64();
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x00034115 File Offset: 0x00032315
		private BsonType ReadType()
		{
			this.MovePosition(1);
			return (BsonType)this._reader.ReadSByte();
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x00034129 File Offset: 0x00032329
		private void MovePosition(int count)
		{
			this._currentContext.Position += count;
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x0003413E File Offset: 0x0003233E
		private byte[] ReadBytes(int count)
		{
			this.MovePosition(count);
			return this._reader.ReadBytes(count);
		}

		// Token: 0x04000452 RID: 1106
		private const int MaxCharBytesSize = 128;

		// Token: 0x04000453 RID: 1107
		private static readonly byte[] SeqRange1 = new byte[]
		{
			default(byte),
			127
		};

		// Token: 0x04000454 RID: 1108
		private static readonly byte[] SeqRange2 = new byte[]
		{
			194,
			223
		};

		// Token: 0x04000455 RID: 1109
		private static readonly byte[] SeqRange3 = new byte[]
		{
			224,
			239
		};

		// Token: 0x04000456 RID: 1110
		private static readonly byte[] SeqRange4 = new byte[]
		{
			240,
			244
		};

		// Token: 0x04000457 RID: 1111
		private readonly BinaryReader _reader;

		// Token: 0x04000458 RID: 1112
		private readonly List<BsonReader.ContainerContext> _stack;

		// Token: 0x04000459 RID: 1113
		private byte[] _byteBuffer;

		// Token: 0x0400045A RID: 1114
		private char[] _charBuffer;

		// Token: 0x0400045B RID: 1115
		private BsonType _currentElementType;

		// Token: 0x0400045C RID: 1116
		private BsonReader.BsonReaderState _bsonReaderState;

		// Token: 0x0400045D RID: 1117
		private BsonReader.ContainerContext _currentContext;

		// Token: 0x0400045E RID: 1118
		private bool _readRootValueAsArray;

		// Token: 0x0400045F RID: 1119
		private bool _jsonNet35BinaryCompatibility;

		// Token: 0x04000460 RID: 1120
		private DateTimeKind _dateTimeKindHandling;

		// Token: 0x020001F1 RID: 497
		private enum BsonReaderState
		{
			// Token: 0x040008B1 RID: 2225
			Normal,
			// Token: 0x040008B2 RID: 2226
			ReferenceStart,
			// Token: 0x040008B3 RID: 2227
			ReferenceRef,
			// Token: 0x040008B4 RID: 2228
			ReferenceId,
			// Token: 0x040008B5 RID: 2229
			CodeWScopeStart,
			// Token: 0x040008B6 RID: 2230
			CodeWScopeCode,
			// Token: 0x040008B7 RID: 2231
			CodeWScopeScope,
			// Token: 0x040008B8 RID: 2232
			CodeWScopeScopeObject,
			// Token: 0x040008B9 RID: 2233
			CodeWScopeScopeEnd
		}

		// Token: 0x020001F2 RID: 498
		private class ContainerContext
		{
			// Token: 0x06000FE9 RID: 4073 RVA: 0x0004582F File Offset: 0x00043A2F
			public ContainerContext(BsonType type)
			{
				this.Type = type;
			}

			// Token: 0x040008BA RID: 2234
			public readonly BsonType Type;

			// Token: 0x040008BB RID: 2235
			public int Length;

			// Token: 0x040008BC RID: 2236
			public int Position;
		}
	}
}
