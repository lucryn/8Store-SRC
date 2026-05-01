using System;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	/// <summary>
	/// Represents a reader that provides fast, non-cached, forward-only access to serialized Json data.
	/// </summary>
	// Token: 0x02000065 RID: 101
	public class JTokenReader : JsonReader, IJsonLineInfo
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Linq.JTokenReader" /> class.
		/// </summary>
		/// <param name="token">The token to read from.</param>
		// Token: 0x06000599 RID: 1433 RVA: 0x00015616 File Offset: 0x00013816
		public JTokenReader(JToken token)
		{
			ValidationUtils.ArgumentNotNull(token, "token");
			this._root = token;
			this._current = token;
		}

		/// <summary>
		/// Reads the next JSON token from the stream as a <see cref="T:Byte[]" />.
		/// </summary>
		/// <returns>
		/// A <see cref="T:Byte[]" /> or a null reference if the next JSON token is null. This method will return <c>null</c> at the end of an array.
		/// </returns>
		// Token: 0x0600059A RID: 1434 RVA: 0x00015637 File Offset: 0x00013837
		public override byte[] ReadAsBytes()
		{
			return base.ReadAsBytesInternal();
		}

		/// <summary>
		/// Reads the next JSON token from the stream as a <see cref="T:System.Nullable`1" />.
		/// </summary>
		/// <returns>A <see cref="T:System.Nullable`1" />. This method will return <c>null</c> at the end of an array.</returns>
		// Token: 0x0600059B RID: 1435 RVA: 0x0001563F File Offset: 0x0001383F
		public override decimal? ReadAsDecimal()
		{
			return base.ReadAsDecimalInternal();
		}

		/// <summary>
		/// Reads the next JSON token from the stream as a <see cref="T:System.Nullable`1" />.
		/// </summary>
		/// <returns>A <see cref="T:System.Nullable`1" />. This method will return <c>null</c> at the end of an array.</returns>
		// Token: 0x0600059C RID: 1436 RVA: 0x00015647 File Offset: 0x00013847
		public override int? ReadAsInt32()
		{
			return base.ReadAsInt32Internal();
		}

		/// <summary>
		/// Reads the next JSON token from the stream as a <see cref="T:System.String" />.
		/// </summary>
		/// <returns>A <see cref="T:System.String" />. This method will return <c>null</c> at the end of an array.</returns>
		// Token: 0x0600059D RID: 1437 RVA: 0x0001564F File Offset: 0x0001384F
		public override string ReadAsString()
		{
			return base.ReadAsStringInternal();
		}

		/// <summary>
		/// Reads the next JSON token from the stream as a <see cref="T:System.Nullable`1" />.
		/// </summary>
		/// <returns>A <see cref="T:System.String" />. This method will return <c>null</c> at the end of an array.</returns>
		// Token: 0x0600059E RID: 1438 RVA: 0x00015657 File Offset: 0x00013857
		public override DateTime? ReadAsDateTime()
		{
			return base.ReadAsDateTimeInternal();
		}

		/// <summary>
		/// Reads the next JSON token from the stream as a <see cref="T:System.Nullable`1" />.
		/// </summary>
		/// <returns>A <see cref="T:System.Nullable`1" />. This method will return <c>null</c> at the end of an array.</returns>
		// Token: 0x0600059F RID: 1439 RVA: 0x0001565F File Offset: 0x0001385F
		public override DateTimeOffset? ReadAsDateTimeOffset()
		{
			return base.ReadAsDateTimeOffsetInternal();
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00015668 File Offset: 0x00013868
		internal override bool ReadInternal()
		{
			if (base.CurrentState == JsonReader.State.Start)
			{
				this.SetToken(this._current);
				return true;
			}
			JContainer jcontainer = this._current as JContainer;
			if (jcontainer != null && this._parent != jcontainer)
			{
				return this.ReadInto(jcontainer);
			}
			return this.ReadOver(this._current);
		}

		/// <summary>
		/// Reads the next JSON token from the stream.
		/// </summary>
		/// <returns>
		/// true if the next token was read successfully; false if there are no more tokens to read.
		/// </returns>
		// Token: 0x060005A1 RID: 1441 RVA: 0x000156B7 File Offset: 0x000138B7
		public override bool Read()
		{
			this._readType = ReadType.Read;
			return this.ReadInternal();
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x000156C8 File Offset: 0x000138C8
		private bool ReadOver(JToken t)
		{
			if (t == this._root)
			{
				return this.ReadToEnd();
			}
			JToken next = t.Next;
			if (next != null && next != t && t != t.Parent.Last)
			{
				this._current = next;
				this.SetToken(this._current);
				return true;
			}
			if (t.Parent == null)
			{
				return this.ReadToEnd();
			}
			return this.SetEnd(t.Parent);
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00015731 File Offset: 0x00013931
		private bool ReadToEnd()
		{
			base.SetToken(JsonToken.None);
			return false;
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x0001573B File Offset: 0x0001393B
		private bool IsEndElement
		{
			get
			{
				return this._current == this._parent;
			}
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0001574C File Offset: 0x0001394C
		private JsonToken? GetEndToken(JContainer c)
		{
			switch (c.Type)
			{
			case JTokenType.Object:
				return new JsonToken?(JsonToken.EndObject);
			case JTokenType.Array:
				return new JsonToken?(JsonToken.EndArray);
			case JTokenType.Constructor:
				return new JsonToken?(JsonToken.EndConstructor);
			case JTokenType.Property:
				return default(JsonToken?);
			default:
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException("Type", c.Type, "Unexpected JContainer type.");
			}
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x000157B8 File Offset: 0x000139B8
		private bool ReadInto(JContainer c)
		{
			JToken first = c.First;
			if (first == null)
			{
				return this.SetEnd(c);
			}
			this.SetToken(first);
			this._current = first;
			this._parent = c;
			return true;
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x000157F0 File Offset: 0x000139F0
		private bool SetEnd(JContainer c)
		{
			JsonToken? endToken = this.GetEndToken(c);
			if (endToken != null)
			{
				base.SetToken(endToken.Value);
				this._current = c;
				this._parent = c;
				return true;
			}
			return this.ReadOver(c);
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00015834 File Offset: 0x00013A34
		private void SetToken(JToken token)
		{
			switch (token.Type)
			{
			case JTokenType.Object:
				base.SetToken(JsonToken.StartObject);
				return;
			case JTokenType.Array:
				base.SetToken(JsonToken.StartArray);
				return;
			case JTokenType.Constructor:
				base.SetToken(JsonToken.StartConstructor);
				return;
			case JTokenType.Property:
				base.SetToken(JsonToken.PropertyName, ((JProperty)token).Name);
				return;
			case JTokenType.Comment:
				base.SetToken(JsonToken.Comment, ((JValue)token).Value);
				return;
			case JTokenType.Integer:
				base.SetToken(JsonToken.Integer, ((JValue)token).Value);
				return;
			case JTokenType.Float:
				base.SetToken(JsonToken.Float, ((JValue)token).Value);
				return;
			case JTokenType.String:
				base.SetToken(JsonToken.String, ((JValue)token).Value);
				return;
			case JTokenType.Boolean:
				base.SetToken(JsonToken.Boolean, ((JValue)token).Value);
				return;
			case JTokenType.Null:
				base.SetToken(JsonToken.Null, ((JValue)token).Value);
				return;
			case JTokenType.Undefined:
				base.SetToken(JsonToken.Undefined, ((JValue)token).Value);
				return;
			case JTokenType.Date:
				base.SetToken(JsonToken.Date, ((JValue)token).Value);
				return;
			case JTokenType.Raw:
				base.SetToken(JsonToken.Raw, ((JValue)token).Value);
				return;
			case JTokenType.Bytes:
				base.SetToken(JsonToken.Bytes, ((JValue)token).Value);
				return;
			case JTokenType.Guid:
				base.SetToken(JsonToken.String, this.SafeToString(((JValue)token).Value));
				return;
			case JTokenType.Uri:
				base.SetToken(JsonToken.String, this.SafeToString(((JValue)token).Value));
				return;
			case JTokenType.TimeSpan:
				base.SetToken(JsonToken.String, this.SafeToString(((JValue)token).Value));
				return;
			default:
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException("Type", token.Type, "Unexpected JTokenType.");
			}
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x000159F0 File Offset: 0x00013BF0
		private string SafeToString(object value)
		{
			if (value == null)
			{
				return null;
			}
			return value.ToString();
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00015A00 File Offset: 0x00013C00
		bool IJsonLineInfo.HasLineInfo()
		{
			if (base.CurrentState == JsonReader.State.Start)
			{
				return false;
			}
			IJsonLineInfo jsonLineInfo = this.IsEndElement ? null : this._current;
			return jsonLineInfo != null && jsonLineInfo.HasLineInfo();
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x00015A34 File Offset: 0x00013C34
		int IJsonLineInfo.LineNumber
		{
			get
			{
				if (base.CurrentState == JsonReader.State.Start)
				{
					return 0;
				}
				IJsonLineInfo jsonLineInfo = this.IsEndElement ? null : this._current;
				if (jsonLineInfo != null)
				{
					return jsonLineInfo.LineNumber;
				}
				return 0;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x00015A68 File Offset: 0x00013C68
		int IJsonLineInfo.LinePosition
		{
			get
			{
				if (base.CurrentState == JsonReader.State.Start)
				{
					return 0;
				}
				IJsonLineInfo jsonLineInfo = this.IsEndElement ? null : this._current;
				if (jsonLineInfo != null)
				{
					return jsonLineInfo.LinePosition;
				}
				return 0;
			}
		}

		// Token: 0x040001AB RID: 427
		private readonly JToken _root;

		// Token: 0x040001AC RID: 428
		private JToken _parent;

		// Token: 0x040001AD RID: 429
		private JToken _current;
	}
}
