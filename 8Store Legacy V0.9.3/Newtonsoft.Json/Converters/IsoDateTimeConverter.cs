using System;
using System.Globalization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	/// <summary>
	/// Converts a <see cref="T:System.DateTime" /> to and from the ISO 8601 date format (e.g. 2008-04-12T12:53Z).
	/// </summary>
	// Token: 0x0200001C RID: 28
	public class IsoDateTimeConverter : DateTimeConverterBase
	{
		/// <summary>
		/// Gets or sets the date time styles used when converting a date to and from JSON.
		/// </summary>
		/// <value>The date time styles used when converting a date to and from JSON.</value>
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600014A RID: 330 RVA: 0x000062D0 File Offset: 0x000044D0
		// (set) Token: 0x0600014B RID: 331 RVA: 0x000062D8 File Offset: 0x000044D8
		public DateTimeStyles DateTimeStyles
		{
			get
			{
				return this._dateTimeStyles;
			}
			set
			{
				this._dateTimeStyles = value;
			}
		}

		/// <summary>
		/// Gets or sets the date time format used when converting a date to and from JSON.
		/// </summary>
		/// <value>The date time format used when converting a date to and from JSON.</value>
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600014C RID: 332 RVA: 0x000062E1 File Offset: 0x000044E1
		// (set) Token: 0x0600014D RID: 333 RVA: 0x000062F2 File Offset: 0x000044F2
		public string DateTimeFormat
		{
			get
			{
				return this._dateTimeFormat ?? string.Empty;
			}
			set
			{
				this._dateTimeFormat = StringUtils.NullEmptyString(value);
			}
		}

		/// <summary>
		/// Gets or sets the culture used when converting a date to and from JSON.
		/// </summary>
		/// <value>The culture used when converting a date to and from JSON.</value>
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00006300 File Offset: 0x00004500
		// (set) Token: 0x0600014F RID: 335 RVA: 0x00006311 File Offset: 0x00004511
		public CultureInfo Culture
		{
			get
			{
				return this._culture ?? CultureInfo.CurrentCulture;
			}
			set
			{
				this._culture = value;
			}
		}

		/// <summary>
		/// Writes the JSON representation of the object.
		/// </summary>
		/// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
		/// <param name="value">The value.</param>
		/// <param name="serializer">The calling serializer.</param>
		// Token: 0x06000150 RID: 336 RVA: 0x0000631C File Offset: 0x0000451C
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			string value2;
			if (value is DateTime)
			{
				DateTime dateTime = (DateTime)value;
				if ((this._dateTimeStyles & 16) == 16 || (this._dateTimeStyles & 64) == 64)
				{
					dateTime = dateTime.ToUniversalTime();
				}
				value2 = dateTime.ToString(this._dateTimeFormat ?? "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK", this.Culture);
			}
			else
			{
				if (!(value is DateTimeOffset))
				{
					throw new JsonSerializationException("Unexpected value when converting date. Expected DateTime or DateTimeOffset, got {0}.".FormatWith(CultureInfo.InvariantCulture, ReflectionUtils.GetObjectType(value)));
				}
				DateTimeOffset dateTimeOffset = (DateTimeOffset)value;
				if ((this._dateTimeStyles & 16) == 16 || (this._dateTimeStyles & 64) == 64)
				{
					dateTimeOffset = dateTimeOffset.ToUniversalTime();
				}
				value2 = dateTimeOffset.ToString(this._dateTimeFormat ?? "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK", this.Culture);
			}
			writer.WriteValue(value2);
		}

		/// <summary>
		/// Reads the JSON representation of the object.
		/// </summary>
		/// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
		/// <param name="objectType">Type of the object.</param>
		/// <param name="existingValue">The existing value of object being read.</param>
		/// <param name="serializer">The calling serializer.</param>
		/// <returns>The object value.</returns>
		// Token: 0x06000151 RID: 337 RVA: 0x000063EC File Offset: 0x000045EC
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			bool flag = ReflectionUtils.IsNullableType(objectType);
			Type type = flag ? Nullable.GetUnderlyingType(objectType) : objectType;
			if (reader.TokenType == JsonToken.Null)
			{
				if (!ReflectionUtils.IsNullableType(objectType))
				{
					throw JsonSerializationException.Create(reader, "Cannot convert null value to {0}.".FormatWith(CultureInfo.InvariantCulture, objectType));
				}
				return null;
			}
			else if (reader.TokenType == JsonToken.Date)
			{
				if (type == typeof(DateTimeOffset))
				{
					return new DateTimeOffset((DateTime)reader.Value);
				}
				return reader.Value;
			}
			else
			{
				if (reader.TokenType != JsonToken.String)
				{
					throw JsonSerializationException.Create(reader, "Unexpected token parsing date. Expected String, got {0}.".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
				}
				string text = reader.Value.ToString();
				if (string.IsNullOrEmpty(text) && flag)
				{
					return null;
				}
				if (type == typeof(DateTimeOffset))
				{
					if (!string.IsNullOrEmpty(this._dateTimeFormat))
					{
						return DateTimeOffset.ParseExact(text, this._dateTimeFormat, this.Culture, this._dateTimeStyles);
					}
					return DateTimeOffset.Parse(text, this.Culture, this._dateTimeStyles);
				}
				else
				{
					if (!string.IsNullOrEmpty(this._dateTimeFormat))
					{
						return DateTime.ParseExact(text, this._dateTimeFormat, this.Culture, this._dateTimeStyles);
					}
					return DateTime.Parse(text, this.Culture, this._dateTimeStyles);
				}
			}
		}

		// Token: 0x04000087 RID: 135
		private const string DefaultDateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";

		// Token: 0x04000088 RID: 136
		private DateTimeStyles _dateTimeStyles = 128;

		// Token: 0x04000089 RID: 137
		private string _dateTimeFormat;

		// Token: 0x0400008A RID: 138
		private CultureInfo _culture;
	}
}
