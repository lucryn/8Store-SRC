using System;
using System.Numerics;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	/// <summary>
	/// Represents a writer that provides a fast, non-cached, forward-only way of generating Json data.
	/// </summary>
	// Token: 0x02000067 RID: 103
	public class JTokenWriter : JsonWriter
	{
		/// <summary>
		/// Gets the token being writen.
		/// </summary>
		/// <value>The token being writen.</value>
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x00015A9C File Offset: 0x00013C9C
		public JToken Token
		{
			get
			{
				if (this._token != null)
				{
					return this._token;
				}
				return this._value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Linq.JTokenWriter" /> class writing to the given <see cref="T:Newtonsoft.Json.Linq.JContainer" />.
		/// </summary>
		/// <param name="container">The container being written to.</param>
		// Token: 0x060005AE RID: 1454 RVA: 0x00015AB3 File Offset: 0x00013CB3
		public JTokenWriter(JContainer container)
		{
			ValidationUtils.ArgumentNotNull(container, "container");
			this._token = container;
			this._parent = container;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Linq.JTokenWriter" /> class.
		/// </summary>
		// Token: 0x060005AF RID: 1455 RVA: 0x00015AD4 File Offset: 0x00013CD4
		public JTokenWriter()
		{
		}

		/// <summary>
		/// Flushes whatever is in the buffer to the underlying streams and also flushes the underlying stream.
		/// </summary>
		// Token: 0x060005B0 RID: 1456 RVA: 0x00015ADC File Offset: 0x00013CDC
		public override void Flush()
		{
		}

		/// <summary>
		/// Closes this stream and the underlying stream.
		/// </summary>
		// Token: 0x060005B1 RID: 1457 RVA: 0x00015ADE File Offset: 0x00013CDE
		public override void Close()
		{
			base.Close();
		}

		/// <summary>
		/// Writes the beginning of a Json object.
		/// </summary>
		// Token: 0x060005B2 RID: 1458 RVA: 0x00015AE6 File Offset: 0x00013CE6
		public override void WriteStartObject()
		{
			base.WriteStartObject();
			this.AddParent(new JObject());
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x00015AF9 File Offset: 0x00013CF9
		private void AddParent(JContainer container)
		{
			if (this._parent == null)
			{
				this._token = container;
			}
			else
			{
				this._parent.AddAndSkipParentCheck(container);
			}
			this._parent = container;
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00015B1F File Offset: 0x00013D1F
		private void RemoveParent()
		{
			this._parent = this._parent.Parent;
			if (this._parent != null && this._parent.Type == JTokenType.Property)
			{
				this._parent = this._parent.Parent;
			}
		}

		/// <summary>
		/// Writes the beginning of a Json array.
		/// </summary>
		// Token: 0x060005B5 RID: 1461 RVA: 0x00015B59 File Offset: 0x00013D59
		public override void WriteStartArray()
		{
			base.WriteStartArray();
			this.AddParent(new JArray());
		}

		/// <summary>
		/// Writes the start of a constructor with the given name.
		/// </summary>
		/// <param name="name">The name of the constructor.</param>
		// Token: 0x060005B6 RID: 1462 RVA: 0x00015B6C File Offset: 0x00013D6C
		public override void WriteStartConstructor(string name)
		{
			base.WriteStartConstructor(name);
			this.AddParent(new JConstructor(name));
		}

		/// <summary>
		/// Writes the end.
		/// </summary>
		/// <param name="token">The token.</param>
		// Token: 0x060005B7 RID: 1463 RVA: 0x00015B81 File Offset: 0x00013D81
		protected override void WriteEnd(JsonToken token)
		{
			this.RemoveParent();
		}

		/// <summary>
		/// Writes the property name of a name/value pair on a Json object.
		/// </summary>
		/// <param name="name">The name of the property.</param>
		// Token: 0x060005B8 RID: 1464 RVA: 0x00015B89 File Offset: 0x00013D89
		public override void WritePropertyName(string name)
		{
			base.WritePropertyName(name);
			this.AddParent(new JProperty(name));
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00015B9E File Offset: 0x00013D9E
		private void AddValue(object value, JsonToken token)
		{
			this.AddValue(new JValue(value), token);
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00015BB0 File Offset: 0x00013DB0
		internal void AddValue(JValue value, JsonToken token)
		{
			if (this._parent != null)
			{
				this._parent.Add(value);
				if (this._parent.Type == JTokenType.Property)
				{
					this._parent = this._parent.Parent;
					return;
				}
			}
			else
			{
				this._value = (value ?? new JValue(null));
			}
		}

		/// <summary>
		/// Writes a <see cref="T:System.Object" /> value.
		/// An error will raised if the value cannot be written as a single JSON token.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Object" /> value to write.</param>
		// Token: 0x060005BB RID: 1467 RVA: 0x00015C02 File Offset: 0x00013E02
		public override void WriteValue(object value)
		{
			if (value is BigInteger)
			{
				base.InternalWriteValue(JsonToken.Integer);
				this.AddValue(value, JsonToken.Integer);
				return;
			}
			base.WriteValue(value);
		}

		/// <summary>
		/// Writes a null value.
		/// </summary>
		// Token: 0x060005BC RID: 1468 RVA: 0x00015C23 File Offset: 0x00013E23
		public override void WriteNull()
		{
			base.WriteNull();
			this.AddValue(null, JsonToken.Null);
		}

		/// <summary>
		/// Writes an undefined value.
		/// </summary>
		// Token: 0x060005BD RID: 1469 RVA: 0x00015C34 File Offset: 0x00013E34
		public override void WriteUndefined()
		{
			base.WriteUndefined();
			this.AddValue(null, JsonToken.Undefined);
		}

		/// <summary>
		/// Writes raw JSON.
		/// </summary>
		/// <param name="json">The raw JSON to write.</param>
		// Token: 0x060005BE RID: 1470 RVA: 0x00015C45 File Offset: 0x00013E45
		public override void WriteRaw(string json)
		{
			base.WriteRaw(json);
			this.AddValue(new JRaw(json), JsonToken.Raw);
		}

		/// <summary>
		/// Writes out a comment <code>/*...*/</code> containing the specified text.
		/// </summary>
		/// <param name="text">Text to place inside the comment.</param>
		// Token: 0x060005BF RID: 1471 RVA: 0x00015C5B File Offset: 0x00013E5B
		public override void WriteComment(string text)
		{
			base.WriteComment(text);
			this.AddValue(JValue.CreateComment(text), JsonToken.Comment);
		}

		/// <summary>
		/// Writes a <see cref="T:System.String" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.String" /> value to write.</param>
		// Token: 0x060005C0 RID: 1472 RVA: 0x00015C71 File Offset: 0x00013E71
		public override void WriteValue(string value)
		{
			base.WriteValue(value);
			this.AddValue(value ?? string.Empty, JsonToken.String);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Int32" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Int32" /> value to write.</param>
		// Token: 0x060005C1 RID: 1473 RVA: 0x00015C8C File Offset: 0x00013E8C
		public override void WriteValue(int value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		/// <summary>
		/// Writes a <see cref="T:System.UInt32" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.UInt32" /> value to write.</param>
		// Token: 0x060005C2 RID: 1474 RVA: 0x00015CA2 File Offset: 0x00013EA2
		[CLSCompliant(false)]
		public override void WriteValue(uint value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Int64" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Int64" /> value to write.</param>
		// Token: 0x060005C3 RID: 1475 RVA: 0x00015CB8 File Offset: 0x00013EB8
		public override void WriteValue(long value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		/// <summary>
		/// Writes a <see cref="T:System.UInt64" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.UInt64" /> value to write.</param>
		// Token: 0x060005C4 RID: 1476 RVA: 0x00015CCE File Offset: 0x00013ECE
		[CLSCompliant(false)]
		public override void WriteValue(ulong value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Single" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Single" /> value to write.</param>
		// Token: 0x060005C5 RID: 1477 RVA: 0x00015CE4 File Offset: 0x00013EE4
		public override void WriteValue(float value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Float);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Double" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Double" /> value to write.</param>
		// Token: 0x060005C6 RID: 1478 RVA: 0x00015CFA File Offset: 0x00013EFA
		public override void WriteValue(double value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Float);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Boolean" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Boolean" /> value to write.</param>
		// Token: 0x060005C7 RID: 1479 RVA: 0x00015D10 File Offset: 0x00013F10
		public override void WriteValue(bool value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Boolean);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Int16" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Int16" /> value to write.</param>
		// Token: 0x060005C8 RID: 1480 RVA: 0x00015D27 File Offset: 0x00013F27
		public override void WriteValue(short value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		/// <summary>
		/// Writes a <see cref="T:System.UInt16" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.UInt16" /> value to write.</param>
		// Token: 0x060005C9 RID: 1481 RVA: 0x00015D3D File Offset: 0x00013F3D
		[CLSCompliant(false)]
		public override void WriteValue(ushort value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Char" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Char" /> value to write.</param>
		// Token: 0x060005CA RID: 1482 RVA: 0x00015D54 File Offset: 0x00013F54
		public override void WriteValue(char value)
		{
			base.WriteValue(value);
			string value2 = value.ToString();
			this.AddValue(value2, JsonToken.String);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Byte" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Byte" /> value to write.</param>
		// Token: 0x060005CB RID: 1483 RVA: 0x00015D7B File Offset: 0x00013F7B
		public override void WriteValue(byte value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		/// <summary>
		/// Writes a <see cref="T:System.SByte" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.SByte" /> value to write.</param>
		// Token: 0x060005CC RID: 1484 RVA: 0x00015D91 File Offset: 0x00013F91
		[CLSCompliant(false)]
		public override void WriteValue(sbyte value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Integer);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Decimal" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Decimal" /> value to write.</param>
		// Token: 0x060005CD RID: 1485 RVA: 0x00015DA7 File Offset: 0x00013FA7
		public override void WriteValue(decimal value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Float);
		}

		/// <summary>
		/// Writes a <see cref="T:System.DateTime" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.DateTime" /> value to write.</param>
		// Token: 0x060005CE RID: 1486 RVA: 0x00015DBD File Offset: 0x00013FBD
		public override void WriteValue(DateTime value)
		{
			base.WriteValue(value);
			value = DateTimeUtils.EnsureDateTime(value, base.DateTimeZoneHandling);
			this.AddValue(value, JsonToken.Date);
		}

		/// <summary>
		/// Writes a <see cref="T:System.DateTimeOffset" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.DateTimeOffset" /> value to write.</param>
		// Token: 0x060005CF RID: 1487 RVA: 0x00015DE2 File Offset: 0x00013FE2
		public override void WriteValue(DateTimeOffset value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Date);
		}

		/// <summary>
		/// Writes a <see cref="T:Byte[]" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:Byte[]" /> value to write.</param>
		// Token: 0x060005D0 RID: 1488 RVA: 0x00015DF9 File Offset: 0x00013FF9
		public override void WriteValue(byte[] value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.Bytes);
		}

		/// <summary>
		/// Writes a <see cref="T:System.TimeSpan" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.TimeSpan" /> value to write.</param>
		// Token: 0x060005D1 RID: 1489 RVA: 0x00015E0B File Offset: 0x0001400B
		public override void WriteValue(TimeSpan value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.String);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Guid" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Guid" /> value to write.</param>
		// Token: 0x060005D2 RID: 1490 RVA: 0x00015E22 File Offset: 0x00014022
		public override void WriteValue(Guid value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.String);
		}

		/// <summary>
		/// Writes a <see cref="T:System.Uri" /> value.
		/// </summary>
		/// <param name="value">The <see cref="T:System.Uri" /> value to write.</param>
		// Token: 0x060005D3 RID: 1491 RVA: 0x00015E39 File Offset: 0x00014039
		public override void WriteValue(Uri value)
		{
			base.WriteValue(value);
			this.AddValue(value, JsonToken.String);
		}

		// Token: 0x040001C1 RID: 449
		private JContainer _token;

		// Token: 0x040001C2 RID: 450
		private JContainer _parent;

		// Token: 0x040001C3 RID: 451
		private JValue _value;
	}
}
