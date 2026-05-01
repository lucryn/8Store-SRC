using System;
using System.Globalization;
using System.IO;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000112 RID: 274
	[Obsolete("BSON reading and writing has been moved to its own package. See https://www.nuget.org/packages/Newtonsoft.Json.Bson for more details.")]
	public class BsonWriter : JsonWriter
	{
		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000D3A RID: 3386 RVA: 0x000343C9 File Offset: 0x000325C9
		// (set) Token: 0x06000D3B RID: 3387 RVA: 0x000343D6 File Offset: 0x000325D6
		public DateTimeKind DateTimeKindHandling
		{
			get
			{
				return this._writer.DateTimeKindHandling;
			}
			set
			{
				this._writer.DateTimeKindHandling = value;
			}
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x000343E4 File Offset: 0x000325E4
		public BsonWriter(Stream stream)
		{
			ValidationUtils.ArgumentNotNull(stream, "stream");
			this._writer = new BsonBinaryWriter(new BinaryWriter(stream));
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x00034408 File Offset: 0x00032608
		public BsonWriter(BinaryWriter writer)
		{
			ValidationUtils.ArgumentNotNull(writer, "writer");
			this._writer = new BsonBinaryWriter(writer);
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x00034427 File Offset: 0x00032627
		public override void Flush()
		{
			this._writer.Flush();
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x00034434 File Offset: 0x00032634
		protected override void WriteEnd(JsonToken token)
		{
			base.WriteEnd(token);
			this.RemoveParent();
			if (base.Top == 0)
			{
				this._writer.WriteToken(this._root);
			}
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x0003445C File Offset: 0x0003265C
		public override void WriteComment(string text)
		{
			throw JsonWriterException.Create(this, "Cannot write JSON comment as BSON.", null);
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x0003446A File Offset: 0x0003266A
		public override void WriteStartConstructor(string name)
		{
			throw JsonWriterException.Create(this, "Cannot write JSON constructor as BSON.", null);
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x00034478 File Offset: 0x00032678
		public override void WriteRaw(string json)
		{
			throw JsonWriterException.Create(this, "Cannot write raw JSON as BSON.", null);
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x00034486 File Offset: 0x00032686
		public override void WriteRawValue(string json)
		{
			throw JsonWriterException.Create(this, "Cannot write raw JSON as BSON.", null);
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x00034494 File Offset: 0x00032694
		public override void WriteStartArray()
		{
			base.WriteStartArray();
			this.AddParent(new BsonArray());
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x000344A7 File Offset: 0x000326A7
		public override void WriteStartObject()
		{
			base.WriteStartObject();
			this.AddParent(new BsonObject());
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x000344BA File Offset: 0x000326BA
		public override void WritePropertyName(string name)
		{
			base.WritePropertyName(name);
			this._propertyName = name;
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x000344CA File Offset: 0x000326CA
		public override void Close()
		{
			base.Close();
			if (base.CloseOutput)
			{
				BsonBinaryWriter writer = this._writer;
				if (writer == null)
				{
					return;
				}
				writer.Close();
			}
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x000344EA File Offset: 0x000326EA
		private void AddParent(BsonToken container)
		{
			this.AddToken(container);
			this._parent = container;
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x000344FA File Offset: 0x000326FA
		private void RemoveParent()
		{
			this._parent = this._parent.Parent;
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x0003450D File Offset: 0x0003270D
		private void AddValue(object value, BsonType type)
		{
			this.AddToken(new BsonValue(value, type));
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x0003451C File Offset: 0x0003271C
		internal void AddToken(BsonToken token)
		{
			if (this._parent != null)
			{
				BsonObject bsonObject = this._parent as BsonObject;
				if (bsonObject != null)
				{
					bsonObject.Add(this._propertyName, token);
					this._propertyName = null;
					return;
				}
				((BsonArray)this._parent).Add(token);
				return;
			}
			else
			{
				if (token.Type != BsonType.Object && token.Type != BsonType.Array)
				{
					throw JsonWriterException.Create(this, "Error writing {0} value. BSON must start with an Object or Array.".FormatWith(CultureInfo.InvariantCulture, token.Type), null);
				}
				this._parent = token;
				this._root = token;
				return;
			}
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x000345A9 File Offset: 0x000327A9
		public override void WriteValue(object value)
		{
			base.WriteValue(value);
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x000345B2 File Offset: 0x000327B2
		public override void WriteNull()
		{
			base.WriteNull();
			this.AddToken(BsonEmpty.Null);
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x000345C5 File Offset: 0x000327C5
		public override void WriteUndefined()
		{
			base.WriteUndefined();
			this.AddToken(BsonEmpty.Undefined);
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x000345D8 File Offset: 0x000327D8
		public override void WriteValue(string value)
		{
			base.WriteValue(value);
			this.AddToken((value == null) ? BsonEmpty.Null : new BsonString(value, true));
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x000345F8 File Offset: 0x000327F8
		public override void WriteValue(int value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x0003460F File Offset: 0x0003280F
		[CLSCompliant(false)]
		public override void WriteValue(uint value)
		{
			if (value > 2147483647U)
			{
				throw JsonWriterException.Create(this, "Value is too large to fit in a signed 32 bit integer. BSON does not support unsigned values.", null);
			}
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x0003463B File Offset: 0x0003283B
		public override void WriteValue(long value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Long);
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x00034652 File Offset: 0x00032852
		[CLSCompliant(false)]
		public override void WriteValue(ulong value)
		{
			if (value > 9223372036854775807UL)
			{
				throw JsonWriterException.Create(this, "Value is too large to fit in a signed 64 bit integer. BSON does not support unsigned values.", null);
			}
			base.WriteValue(value);
			this.AddValue(value, BsonType.Long);
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x00034682 File Offset: 0x00032882
		public override void WriteValue(float value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Number);
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x00034698 File Offset: 0x00032898
		public override void WriteValue(double value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Number);
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x000346AE File Offset: 0x000328AE
		public override void WriteValue(bool value)
		{
			base.WriteValue(value);
			this.AddToken(value ? BsonBoolean.True : BsonBoolean.False);
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x000346CC File Offset: 0x000328CC
		public override void WriteValue(short value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x000346E3 File Offset: 0x000328E3
		[CLSCompliant(false)]
		public override void WriteValue(ushort value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x000346FC File Offset: 0x000328FC
		public override void WriteValue(char value)
		{
			base.WriteValue(value);
			string value2 = value.ToString();
			this.AddToken(new BsonString(value2, true));
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x00034727 File Offset: 0x00032927
		public override void WriteValue(byte value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x0003473E File Offset: 0x0003293E
		[CLSCompliant(false)]
		public override void WriteValue(sbyte value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x00034755 File Offset: 0x00032955
		public override void WriteValue(decimal value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Number);
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x0003476B File Offset: 0x0003296B
		public override void WriteValue(DateTime value)
		{
			base.WriteValue(value);
			value = DateTimeUtils.EnsureDateTime(value, base.DateTimeZoneHandling);
			this.AddValue(value, BsonType.Date);
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x00034790 File Offset: 0x00032990
		public override void WriteValue(DateTimeOffset value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Date);
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x000347A7 File Offset: 0x000329A7
		public override void WriteValue(byte[] value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			base.WriteValue(value);
			this.AddToken(new BsonBinary(value, BsonBinaryType.Binary));
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x000347C7 File Offset: 0x000329C7
		public override void WriteValue(Guid value)
		{
			base.WriteValue(value);
			this.AddToken(new BsonBinary(value.ToByteArray(), BsonBinaryType.Uuid));
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x000347E3 File Offset: 0x000329E3
		public override void WriteValue(TimeSpan value)
		{
			base.WriteValue(value);
			this.AddToken(new BsonString(value.ToString(), true));
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x00034805 File Offset: 0x00032A05
		public override void WriteValue(Uri value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			base.WriteValue(value);
			this.AddToken(new BsonString(value.ToString(), true));
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x00034830 File Offset: 0x00032A30
		public void WriteObjectId(byte[] value)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			if (value.Length != 12)
			{
				throw JsonWriterException.Create(this, "An object id must be 12 bytes", null);
			}
			base.SetWriteState(JsonToken.Undefined, null);
			this.AddValue(value, BsonType.Oid);
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x00034862 File Offset: 0x00032A62
		public void WriteRegex(string pattern, string options)
		{
			ValidationUtils.ArgumentNotNull(pattern, "pattern");
			base.SetWriteState(JsonToken.Undefined, null);
			this.AddToken(new BsonRegex(pattern, options));
		}

		// Token: 0x04000488 RID: 1160
		private readonly BsonBinaryWriter _writer;

		// Token: 0x04000489 RID: 1161
		private BsonToken _root;

		// Token: 0x0400048A RID: 1162
		private BsonToken _parent;

		// Token: 0x0400048B RID: 1163
		private string _propertyName;
	}
}
