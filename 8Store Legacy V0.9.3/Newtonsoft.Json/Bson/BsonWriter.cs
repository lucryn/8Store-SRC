using System;
using System.Globalization;
using System.IO;
using System.Numerics;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Bson
{
	/// <summary>
	/// Represents a writer that provides a fast, non-cached, forward-only way of generating JSON data.
	/// </summary>
	// Token: 0x02000015 RID: 21
	public class BsonWriter : JsonWriter
	{
		/// <summary>
		/// Gets or sets the <see cref="T:System.DateTimeKind" /> used when writing <see cref="T:System.DateTime" /> values to BSON.
		/// When set to <see cref="F:System.DateTimeKind.Unspecified" /> no conversion will occur.
		/// </summary>
		/// <value>The <see cref="T:System.DateTimeKind" /> used when writing <see cref="T:System.DateTime" /> values to BSON.</value>
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00005B15 File Offset: 0x00003D15
		// (set) Token: 0x06000105 RID: 261 RVA: 0x00005B22 File Offset: 0x00003D22
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

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Bson.BsonWriter" /> class.
		/// </summary>
		/// <param name="stream">The stream.</param>
		// Token: 0x06000106 RID: 262 RVA: 0x00005B30 File Offset: 0x00003D30
		public BsonWriter(Stream stream)
		{
			ValidationUtils.ArgumentNotNull(stream, "stream");
			this._writer = new BsonBinaryWriter(new BinaryWriter(stream));
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Bson.BsonWriter" /> class.
		/// </summary>
		/// <param name="writer">The writer.</param>
		// Token: 0x06000107 RID: 263 RVA: 0x00005B54 File Offset: 0x00003D54
		public BsonWriter(BinaryWriter writer)
		{
			ValidationUtils.ArgumentNotNull(writer, "writer");
			this._writer = new BsonBinaryWriter(writer);
		}

		/// <summary>
		/// Flushes whatever is in the buffer to the underlying streams and also flushes the underlying stream.
		/// </summary>
		// Token: 0x06000108 RID: 264 RVA: 0x00005B73 File Offset: 0x00003D73
		public override void Flush()
		{
			this._writer.Flush();
		}

		/// <summary>
		/// Writes the end.
		/// </summary>
		/// <param name="token">The token.</param>
		// Token: 0x06000109 RID: 265 RVA: 0x00005B80 File Offset: 0x00003D80
		protected override void WriteEnd(JsonToken token)
		{
			base.WriteEnd(token);
			this.RemoveParent();
			if (base.Top == 0)
			{
				this._writer.WriteToken(this._root);
			}
		}

		/// <summary>
		/// Writes out a comment <code>/*...*/</code> containing the specified text.
		/// </summary>
		/// <param name="text">Text to place inside the comment.</param>
		// Token: 0x0600010A RID: 266 RVA: 0x00005BA8 File Offset: 0x00003DA8
		public override void WriteComment(string text)
		{
			throw JsonWriterException.Create(this, "Cannot write JSON comment as BSON.", null);
		}

		/// <summary>
		/// Writes the start of a constructor with the given name.
		/// </summary>
		/// <param name="name">The name of the constructor.</param>
		// Token: 0x0600010B RID: 267 RVA: 0x00005BB6 File Offset: 0x00003DB6
		public override void WriteStartConstructor(string name)
		{
			throw JsonWriterException.Create(this, "Cannot write JSON constructor as BSON.", null);
		}

		/// <summary>
		/// Writes raw JSON.
		/// </summary>
		/// <param name="json">The raw JSON to write.</param>
		// Token: 0x0600010C RID: 268 RVA: 0x00005BC4 File Offset: 0x00003DC4
		public override void WriteRaw(string json)
		{
			throw JsonWriterException.Create(this, "Cannot write raw JSON as BSON.", null);
		}

		/// <summary>
		/// Writes raw JSON where a value is expected and updates the writer's state.
		/// </summary>
		/// <param name="json">The raw JSON to write.</param>
		// Token: 0x0600010D RID: 269 RVA: 0x00005BD2 File Offset: 0x00003DD2
		public override void WriteRawValue(string json)
		{
			throw JsonWriterException.Create(this, "Cannot write raw JSON as BSON.", null);
		}

		/// <summary>
		/// Writes the beginning of a Json array.
		/// </summary>
		// Token: 0x0600010E RID: 270 RVA: 0x00005BE0 File Offset: 0x00003DE0
		public override void WriteStartArray()
		{
			base.WriteStartArray();
			this.AddParent(new BsonArray());
		}

		/// <summary>
		/// Writes the beginning of a Json object.
		/// </summary>
		// Token: 0x0600010F RID: 271 RVA: 0x00005BF3 File Offset: 0x00003DF3
		public override void WriteStartObject()
		{
			base.WriteStartObject();
			this.AddParent(new BsonObject());
		}

		/// <summary>
		/// Writes the property name of a name/value pair on a Json object.
		/// </summary>
		/// <param name="name">The name of the property.</param>
		// Token: 0x06000110 RID: 272 RVA: 0x00005C06 File Offset: 0x00003E06
		public override void WritePropertyName(string name)
		{
			base.WritePropertyName(name);
			this._propertyName = name;
		}

		/// <summary>
		/// Closes this stream and the underlying stream.
		/// </summary>
		// Token: 0x06000111 RID: 273 RVA: 0x00005C16 File Offset: 0x00003E16
		public override void Close()
		{
			base.Close();
			if (base.CloseOutput && this._writer != null)
			{
				this._writer.Close();
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00005C39 File Offset: 0x00003E39
		private void AddParent(BsonToken container)
		{
			this.AddToken(container);
			this._parent = container;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00005C49 File Offset: 0x00003E49
		private void RemoveParent()
		{
			this._parent = this._parent.Parent;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00005C5C File Offset: 0x00003E5C
		private void AddValue(object value, BsonType type)
		{
			this.AddToken(new BsonValue(value, type));
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00005C6C File Offset: 0x00003E6C
		internal void AddToken(BsonToken token)
		{
			if (this._parent != null)
			{
				if (this._parent is BsonObject)
				{
					((BsonObject)this._parent).Add(this._propertyName, token);
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

		/// <summary>
		/// Writes a <see cref="T:System.Object" /> value.
		/// An error will raised if the value cannot be written as a single JSON token.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Object" /> value to write.</param>
		// Token: 0x06000116 RID: 278 RVA: 0x00005D04 File Offset: 0x00003F04
		public override void WriteValue(object value)
		{
			if (value is BigInteger)
			{
				base.InternalWriteValue(JsonToken.Integer);
				this.AddToken(new BsonBinary(((BigInteger)value).ToByteArray(), BsonBinaryType.Binary));
				return;
			}
			base.WriteValue(value);
		}

		/// <summary>
		/// Writes a null value.
		/// </summary>
		// Token: 0x06000117 RID: 279 RVA: 0x00005D42 File Offset: 0x00003F42
		public override void WriteNull()
		{
			base.WriteNull();
			this.AddValue(null, BsonType.Null);
		}

		/// <summary>
		/// Writes an undefined value.
		/// </summary>
		// Token: 0x06000118 RID: 280 RVA: 0x00005D53 File Offset: 0x00003F53
		public override void WriteUndefined()
		{
			base.WriteUndefined();
			this.AddValue(null, BsonType.Undefined);
		}

		/// <summary>
		/// Writes a <see cref="T:System.String" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.String" /> value to write.</param>
		// Token: 0x06000119 RID: 281 RVA: 0x00005D63 File Offset: 0x00003F63
		public override void WriteValue(string value)
		{
			base.WriteValue(value);
			if (value == null)
			{
				this.AddValue(null, BsonType.Null);
				return;
			}
			this.AddToken(new BsonString(value, true));
		}

		/// <summary>
		/// Writes a <see cref="T:System.Int32" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Int32" /> value to write.</param>
		// Token: 0x0600011A RID: 282 RVA: 0x00005D86 File Offset: 0x00003F86
		public override void WriteValue(int value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		/// <summary>
		/// Writes a <see cref="T:System.UInt32" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.UInt32" /> value to write.</param>
		// Token: 0x0600011B RID: 283 RVA: 0x00005D9D File Offset: 0x00003F9D
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

		/// <summary>
		/// Writes a <see cref="T:System.Int64" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Int64" /> value to write.</param>
		// Token: 0x0600011C RID: 284 RVA: 0x00005DC9 File Offset: 0x00003FC9
		public override void WriteValue(long value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Long);
		}

		/// <summary>
		/// Writes a <see cref="T:System.UInt64" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.UInt64" /> value to write.</param>
		// Token: 0x0600011D RID: 285 RVA: 0x00005DE0 File Offset: 0x00003FE0
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

		/// <summary>
		/// Writes a <see cref="T:System.Single" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Single" /> value to write.</param>
		// Token: 0x0600011E RID: 286 RVA: 0x00005E10 File Offset: 0x00004010
		public override void WriteValue(float value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Number);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Double" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Double" /> value to write.</param>
		// Token: 0x0600011F RID: 287 RVA: 0x00005E26 File Offset: 0x00004026
		public override void WriteValue(double value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Number);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Boolean" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Boolean" /> value to write.</param>
		// Token: 0x06000120 RID: 288 RVA: 0x00005E3C File Offset: 0x0000403C
		public override void WriteValue(bool value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Boolean);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Int16" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Int16" /> value to write.</param>
		// Token: 0x06000121 RID: 289 RVA: 0x00005E52 File Offset: 0x00004052
		public override void WriteValue(short value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		/// <summary>
		/// Writes a <see cref="T:System.UInt16" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.UInt16" /> value to write.</param>
		// Token: 0x06000122 RID: 290 RVA: 0x00005E69 File Offset: 0x00004069
		[CLSCompliant(false)]
		public override void WriteValue(ushort value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Char" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Char" /> value to write.</param>
		// Token: 0x06000123 RID: 291 RVA: 0x00005E80 File Offset: 0x00004080
		public override void WriteValue(char value)
		{
			base.WriteValue(value);
			string value2 = value.ToString();
			this.AddToken(new BsonString(value2, true));
		}

		/// <summary>
		/// Writes a <see cref="T:System.Byte" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Byte" /> value to write.</param>
		// Token: 0x06000124 RID: 292 RVA: 0x00005EAB File Offset: 0x000040AB
		public override void WriteValue(byte value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		/// <summary>
		/// Writes a <see cref="T:System.SByte" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.SByte" /> value to write.</param>
		// Token: 0x06000125 RID: 293 RVA: 0x00005EC2 File Offset: 0x000040C2
		[CLSCompliant(false)]
		public override void WriteValue(sbyte value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Integer);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Decimal" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Decimal" /> value to write.</param>
		// Token: 0x06000126 RID: 294 RVA: 0x00005ED9 File Offset: 0x000040D9
		public override void WriteValue(decimal value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Number);
		}

		/// <summary>
		/// Writes a <see cref="T:System.DateTime" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.DateTime" /> value to write.</param>
		// Token: 0x06000127 RID: 295 RVA: 0x00005EEF File Offset: 0x000040EF
		public override void WriteValue(DateTime value)
		{
			base.WriteValue(value);
			value = DateTimeUtils.EnsureDateTime(value, base.DateTimeZoneHandling);
			this.AddValue(value, BsonType.Date);
		}

		/// <summary>
		/// Writes a <see cref="T:System.DateTimeOffset" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.DateTimeOffset" /> value to write.</param>
		// Token: 0x06000128 RID: 296 RVA: 0x00005F14 File Offset: 0x00004114
		public override void WriteValue(DateTimeOffset value)
		{
			base.WriteValue(value);
			this.AddValue(value, BsonType.Date);
		}

		/// <summary>
		/// Writes a <see cref="T:Byte[]" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:Byte[]" /> value to write.</param>
		// Token: 0x06000129 RID: 297 RVA: 0x00005F2B File Offset: 0x0000412B
		public override void WriteValue(byte[] value)
		{
			base.WriteValue(value);
			this.AddToken(new BsonBinary(value, BsonBinaryType.Binary));
		}

		/// <summary>
		/// Writes a <see cref="T:System.Guid" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Guid" /> value to write.</param>
		// Token: 0x0600012A RID: 298 RVA: 0x00005F41 File Offset: 0x00004141
		public override void WriteValue(Guid value)
		{
			base.WriteValue(value);
			this.AddToken(new BsonBinary(value.ToByteArray(), BsonBinaryType.Uuid));
		}

		/// <summary>
		/// Writes a <see cref="T:System.TimeSpan" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.TimeSpan" /> value to write.</param>
		// Token: 0x0600012B RID: 299 RVA: 0x00005F5D File Offset: 0x0000415D
		public override void WriteValue(TimeSpan value)
		{
			base.WriteValue(value);
			this.AddToken(new BsonString(value.ToString(), true));
		}

		/// <summary>
		/// Writes a <see cref="T:System.Uri" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Uri" /> value to write.</param>
		// Token: 0x0600012C RID: 300 RVA: 0x00005F7F File Offset: 0x0000417F
		public override void WriteValue(Uri value)
		{
			base.WriteValue(value);
			this.AddToken(new BsonString(value.ToString(), true));
		}

		/// <summary>
		/// Writes a <see cref="T:Byte[]" /> value that represents a BSON object id.
		/// </summary>
		/// <param name="value">The Object ID value to write.</param>
		// Token: 0x0600012D RID: 301 RVA: 0x00005F9A File Offset: 0x0000419A
		public void WriteObjectId(byte[] value)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			if (value.Length != 12)
			{
				throw JsonWriterException.Create(this, "An object id must be 12 bytes", null);
			}
			base.UpdateScopeWithFinishedValue();
			base.AutoComplete(JsonToken.Undefined);
			this.AddValue(value, BsonType.Oid);
		}

		/// <summary>
		/// Writes a BSON regex.
		/// </summary>
		/// <param name="pattern">The regex pattern.</param>
		/// <param name="options">The regex options.</param>
		// Token: 0x0600012E RID: 302 RVA: 0x00005FD1 File Offset: 0x000041D1
		public void WriteRegex(string pattern, string options)
		{
			ValidationUtils.ArgumentNotNull(pattern, "pattern");
			base.UpdateScopeWithFinishedValue();
			base.AutoComplete(JsonToken.Undefined);
			this.AddToken(new BsonRegex(pattern, options));
		}

		// Token: 0x04000080 RID: 128
		private readonly BsonBinaryWriter _writer;

		// Token: 0x04000081 RID: 129
		private BsonToken _root;

		// Token: 0x04000082 RID: 130
		private BsonToken _parent;

		// Token: 0x04000083 RID: 131
		private string _propertyName;
	}
}
