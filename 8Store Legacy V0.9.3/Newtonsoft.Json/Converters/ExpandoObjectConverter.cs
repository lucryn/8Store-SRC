using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	/// <summary>
	/// Converts an ExpandoObject to and from JSON.
	/// </summary>
	// Token: 0x0200001B RID: 27
	public class ExpandoObjectConverter : JsonConverter
	{
		/// <summary>
		/// Writes the JSON representation of the object.
		/// </summary>
		/// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
		/// <param name="value">The value.</param>
		/// <param name="serializer">The calling serializer.</param>
		// Token: 0x06000142 RID: 322 RVA: 0x00006158 File Offset: 0x00004358
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
		}

		/// <summary>
		/// Reads the JSON representation of the object.
		/// </summary>
		/// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
		/// <param name="objectType">Type of the object.</param>
		/// <param name="existingValue">The existing value of object being read.</param>
		/// <param name="serializer">The calling serializer.</param>
		/// <returns>The object value.</returns>
		// Token: 0x06000143 RID: 323 RVA: 0x0000615A File Offset: 0x0000435A
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return this.ReadValue(reader);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00006164 File Offset: 0x00004364
		private object ReadValue(JsonReader reader)
		{
			while (reader.TokenType == JsonToken.Comment)
			{
				if (!reader.Read())
				{
					throw JsonSerializationException.Create(reader, "Unexpected end when reading ExpandoObject.");
				}
			}
			switch (reader.TokenType)
			{
			case JsonToken.StartObject:
				return this.ReadObject(reader);
			case JsonToken.StartArray:
				return this.ReadList(reader);
			default:
				if (JsonReader.IsPrimitiveToken(reader.TokenType))
				{
					return reader.Value;
				}
				throw JsonSerializationException.Create(reader, "Unexpected token when converting ExpandoObject: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000061F0 File Offset: 0x000043F0
		private object ReadList(JsonReader reader)
		{
			IList<object> list = new List<object>();
			while (reader.Read())
			{
				JsonToken tokenType = reader.TokenType;
				if (tokenType != JsonToken.Comment)
				{
					if (tokenType == JsonToken.EndArray)
					{
						return list;
					}
					object obj = this.ReadValue(reader);
					list.Add(obj);
				}
			}
			throw JsonSerializationException.Create(reader, "Unexpected end when reading ExpandoObject.");
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000623C File Offset: 0x0000443C
		private object ReadObject(JsonReader reader)
		{
			IDictionary<string, object> dictionary = new ExpandoObject();
			while (reader.Read())
			{
				JsonToken tokenType = reader.TokenType;
				switch (tokenType)
				{
				case JsonToken.PropertyName:
				{
					string text = reader.Value.ToString();
					if (!reader.Read())
					{
						throw JsonSerializationException.Create(reader, "Unexpected end when reading ExpandoObject.");
					}
					object obj = this.ReadValue(reader);
					dictionary[text] = obj;
					break;
				}
				case JsonToken.Comment:
					break;
				default:
					if (tokenType == JsonToken.EndObject)
					{
						return dictionary;
					}
					break;
				}
			}
			throw JsonSerializationException.Create(reader, "Unexpected end when reading ExpandoObject.");
		}

		/// <summary>
		/// Determines whether this instance can convert the specified object type.
		/// </summary>
		/// <param name="objectType">Type of the object.</param>
		/// <returns>
		/// 	<c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
		/// </returns>
		// Token: 0x06000147 RID: 327 RVA: 0x000062B6 File Offset: 0x000044B6
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(ExpandoObject);
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="T:Newtonsoft.Json.JsonConverter" /> can write JSON.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this <see cref="T:Newtonsoft.Json.JsonConverter" /> can write JSON; otherwise, <c>false</c>.
		/// </value>
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000148 RID: 328 RVA: 0x000062C5 File Offset: 0x000044C5
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}
	}
}
