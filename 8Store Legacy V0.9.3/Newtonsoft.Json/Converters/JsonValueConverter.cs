using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json.Utilities;
using Windows.Data.Json;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x0200001E RID: 30
	public class JsonValueConverter : JsonConverter
	{
		// Token: 0x06000156 RID: 342 RVA: 0x000066FF File Offset: 0x000048FF
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			this.WriteJsonValue(writer, (IJsonValue)value);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00006710 File Offset: 0x00004910
		private void WriteJsonValue(JsonWriter writer, IJsonValue value)
		{
			switch (value.ValueType)
			{
			case 0:
				writer.WriteNull();
				return;
			case 1:
				writer.WriteValue(value.GetBoolean());
				return;
			case 2:
			{
				double number = value.GetNumber();
				bool flag = number % 1.0 == 0.0;
				if (flag && number <= 9.223372036854776E+18 && number >= -9.223372036854776E+18)
				{
					writer.WriteValue(Convert.ToInt64(number));
					return;
				}
				writer.WriteValue(number);
				return;
			}
			case 3:
				writer.WriteValue(value.GetString());
				return;
			case 4:
			{
				JsonArray array = value.GetArray();
				writer.WriteStartArray();
				for (int i = 0; i < array.Count; i++)
				{
					this.WriteJsonValue(writer, array[i]);
				}
				writer.WriteEndArray();
				return;
			}
			case 5:
			{
				JsonObject @object = value.GetObject();
				writer.WriteStartObject();
				foreach (KeyValuePair<string, IJsonValue> keyValuePair in @object)
				{
					writer.WritePropertyName(keyValuePair.Key);
					this.WriteJsonValue(writer, keyValuePair.Value);
				}
				writer.WriteEndObject();
				return;
			}
			default:
				throw new ArgumentOutOfRangeException("ValueType");
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00006860 File Offset: 0x00004A60
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.None)
			{
				reader.Read();
			}
			IJsonValue jsonValue = this.CreateJsonValue(reader);
			if (!objectType.IsAssignableFrom(jsonValue.GetType()))
			{
				throw JsonSerializationException.Create(reader, "Could not convert '{0}' to '{1}'.".FormatWith(CultureInfo.InvariantCulture, jsonValue.GetType(), objectType));
			}
			return jsonValue;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000068B0 File Offset: 0x00004AB0
		private IJsonValue CreateJsonValue(JsonReader reader)
		{
			while (reader.TokenType == JsonToken.Comment)
			{
				if (!reader.Read())
				{
					throw JsonSerializationException.Create(reader, "Unexpected end.");
				}
			}
			switch (reader.TokenType)
			{
			case JsonToken.StartObject:
				return this.CreateJsonObject(reader);
			case JsonToken.StartArray:
			{
				JsonArray jsonArray = new JsonArray();
				while (reader.Read())
				{
					JsonToken tokenType = reader.TokenType;
					if (tokenType == JsonToken.EndArray)
					{
						return jsonArray;
					}
					IJsonValue jsonValue = this.CreateJsonValue(reader);
					jsonArray.Add(jsonValue);
				}
				throw JsonSerializationException.Create(reader, "Unexpected end.");
			}
			case JsonToken.Integer:
			case JsonToken.Float:
				return JsonValue.CreateNumberValue(Convert.ToDouble(reader.Value, CultureInfo.InvariantCulture));
			case JsonToken.String:
				return JsonValue.CreateStringValue(reader.Value.ToString());
			case JsonToken.Boolean:
				return JsonValue.CreateBooleanValue(Convert.ToBoolean(reader.Value, CultureInfo.InvariantCulture));
			case JsonToken.Null:
				return JsonValue.Parse("null");
			case JsonToken.Date:
				return JsonValue.CreateStringValue(reader.Value.ToString());
			case JsonToken.Bytes:
				return JsonValue.CreateStringValue(reader.Value.ToString());
			}
			throw JsonSerializationException.Create(reader, "Unexpected or unsupported token: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00006A04 File Offset: 0x00004C04
		private JsonObject CreateJsonObject(JsonReader reader)
		{
			JsonObject jsonObject = new JsonObject();
			string text = null;
			while (reader.Read())
			{
				JsonToken tokenType = reader.TokenType;
				switch (tokenType)
				{
				case JsonToken.PropertyName:
					text = (string)reader.Value;
					break;
				case JsonToken.Comment:
					break;
				default:
				{
					if (tokenType == JsonToken.EndObject)
					{
						return jsonObject;
					}
					IJsonValue jsonValue = this.CreateJsonValue(reader);
					jsonObject.Add(text, jsonValue);
					break;
				}
				}
			}
			throw JsonSerializationException.Create(reader, "Unexpected end.");
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00006A6C File Offset: 0x00004C6C
		public override bool CanConvert(Type objectType)
		{
			return typeof(IJsonValue).IsAssignableFrom(objectType);
		}
	}
}
