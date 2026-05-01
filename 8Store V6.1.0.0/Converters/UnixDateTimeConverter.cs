using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000F1 RID: 241
	[NullableContext(1)]
	[Nullable(0)]
	public class UnixDateTimeConverter : DateTimeConverterBase
	{
		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000C38 RID: 3128 RVA: 0x00030954 File Offset: 0x0002EB54
		// (set) Token: 0x06000C39 RID: 3129 RVA: 0x0003095C File Offset: 0x0002EB5C
		public bool AllowPreEpoch { get; set; }

		// Token: 0x06000C3A RID: 3130 RVA: 0x00030965 File Offset: 0x0002EB65
		public UnixDateTimeConverter() : this(false)
		{
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x0003096E File Offset: 0x0002EB6E
		public UnixDateTimeConverter(bool allowPreEpoch)
		{
			this.AllowPreEpoch = allowPreEpoch;
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x00030980 File Offset: 0x0002EB80
		public override void WriteJson(JsonWriter writer, [Nullable(2)] object value, JsonSerializer serializer)
		{
			long num;
			if (value is DateTime)
			{
				num = (long)(((DateTime)value).ToUniversalTime() - UnixDateTimeConverter.UnixEpoch).TotalSeconds;
			}
			else
			{
				if (!(value is DateTimeOffset))
				{
					throw new JsonSerializationException("Expected date object value.");
				}
				num = (long)(((DateTimeOffset)value).ToUniversalTime() - UnixDateTimeConverter.UnixEpoch).TotalSeconds;
			}
			if (!this.AllowPreEpoch && num < 0L)
			{
				throw new JsonSerializationException("Cannot convert date value that is before Unix epoch of 00:00:00 UTC on 1 January 1970.");
			}
			writer.WriteValue(num);
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x00030A14 File Offset: 0x0002EC14
		[return: Nullable(2)]
		public override object ReadJson(JsonReader reader, Type objectType, [Nullable(2)] object existingValue, JsonSerializer serializer)
		{
			bool flag = ReflectionUtils.IsNullable(objectType);
			if (reader.TokenType == JsonToken.Null)
			{
				if (!flag)
				{
					throw JsonSerializationException.Create(reader, "Cannot convert null value to {0}.".FormatWith(CultureInfo.InvariantCulture, objectType));
				}
				return null;
			}
			else
			{
				long num;
				if (reader.TokenType == JsonToken.Integer)
				{
					num = (long)reader.Value;
				}
				else
				{
					if (reader.TokenType != JsonToken.String)
					{
						throw JsonSerializationException.Create(reader, "Unexpected token parsing date. Expected Integer or String, got {0}.".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
					}
					if (!long.TryParse((string)reader.Value, ref num))
					{
						throw JsonSerializationException.Create(reader, "Cannot convert invalid value to {0}.".FormatWith(CultureInfo.InvariantCulture, objectType));
					}
				}
				if (!this.AllowPreEpoch && num < 0L)
				{
					throw JsonSerializationException.Create(reader, "Cannot convert value that is before Unix epoch of 00:00:00 UTC on 1 January 1970 to {0}.".FormatWith(CultureInfo.InvariantCulture, objectType));
				}
				DateTime dateTime = UnixDateTimeConverter.UnixEpoch.AddSeconds((double)num);
				if ((flag ? Nullable.GetUnderlyingType(objectType) : objectType) == typeof(DateTimeOffset))
				{
					return new DateTimeOffset(dateTime, TimeSpan.Zero);
				}
				return dateTime;
			}
		}

		// Token: 0x04000432 RID: 1074
		internal static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 1);
	}
}
