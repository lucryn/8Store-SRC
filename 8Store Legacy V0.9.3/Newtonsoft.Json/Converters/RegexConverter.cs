using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json.Converters
{
	/// <summary>
	/// Converts a <see cref="T:System.Text.RegularExpressions.Regex" /> to and from JSON and BSON.
	/// </summary>
	// Token: 0x02000020 RID: 32
	public class RegexConverter : JsonConverter
	{
		/// <summary>
		/// Writes the JSON representation of the object.
		/// </summary>
		/// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
		/// <param name="value">The value.</param>
		/// <param name="serializer">The calling serializer.</param>
		// Token: 0x06000161 RID: 353 RVA: 0x00006C58 File Offset: 0x00004E58
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			Regex regex = (Regex)value;
			BsonWriter bsonWriter = writer as BsonWriter;
			if (bsonWriter != null)
			{
				this.WriteBson(bsonWriter, regex);
				return;
			}
			this.WriteJson(writer, regex, serializer);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00006C88 File Offset: 0x00004E88
		private bool HasFlag(RegexOptions options, RegexOptions flag)
		{
			return (options & flag) == flag;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00006C90 File Offset: 0x00004E90
		private void WriteBson(BsonWriter writer, Regex regex)
		{
			string text = null;
			if (this.HasFlag(regex.Options, 1))
			{
				text += "i";
			}
			if (this.HasFlag(regex.Options, 2))
			{
				text += "m";
			}
			if (this.HasFlag(regex.Options, 16))
			{
				text += "s";
			}
			text += "u";
			if (this.HasFlag(regex.Options, 4))
			{
				text += "x";
			}
			writer.WriteRegex(regex.ToString(), text);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00006D28 File Offset: 0x00004F28
		private void WriteJson(JsonWriter writer, Regex regex, JsonSerializer serializer)
		{
			DefaultContractResolver defaultContractResolver = serializer.ContractResolver as DefaultContractResolver;
			writer.WriteStartObject();
			writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.GetResolvedPropertyName("Pattern") : "Pattern");
			writer.WriteValue(regex.ToString());
			writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.GetResolvedPropertyName("Options") : "Options");
			serializer.Serialize(writer, regex.Options);
			writer.WriteEndObject();
		}

		/// <summary>
		/// Reads the JSON representation of the object.
		/// </summary>
		/// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
		/// <param name="objectType">Type of the object.</param>
		/// <param name="existingValue">The existing value of object being read.</param>
		/// <param name="serializer">The calling serializer.</param>
		/// <returns>The object value.</returns>
		// Token: 0x06000165 RID: 357 RVA: 0x00006DA1 File Offset: 0x00004FA1
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.StartObject)
			{
				return this.ReadRegexObject(reader, serializer);
			}
			if (reader.TokenType == JsonToken.String)
			{
				return this.ReadRegexString(reader);
			}
			throw JsonSerializationException.Create(reader, "Unexpected token when reading Regex.");
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00006DD4 File Offset: 0x00004FD4
		private object ReadRegexString(JsonReader reader)
		{
			string text = (string)reader.Value;
			int num = text.LastIndexOf('/');
			string text2 = text.Substring(1, num - 1);
			string text3 = text.Substring(num + 1);
			RegexOptions regexOptions = 0;
			string text4 = text3;
			for (int i = 0; i < text4.Length; i++)
			{
				char c = text4.get_Chars(i);
				char c2 = c;
				if (c2 <= 'm')
				{
					if (c2 != 'i')
					{
						if (c2 == 'm')
						{
							regexOptions |= 2;
						}
					}
					else
					{
						regexOptions |= 1;
					}
				}
				else if (c2 != 's')
				{
					if (c2 == 'x')
					{
						regexOptions |= 4;
					}
				}
				else
				{
					regexOptions |= 16;
				}
			}
			return new Regex(text2, regexOptions);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00006E80 File Offset: 0x00005080
		private Regex ReadRegexObject(JsonReader reader, JsonSerializer serializer)
		{
			string text = null;
			RegexOptions? regexOptions = default(RegexOptions?);
			while (reader.Read())
			{
				JsonToken tokenType = reader.TokenType;
				switch (tokenType)
				{
				case JsonToken.PropertyName:
				{
					string text2 = reader.Value.ToString();
					if (!reader.Read())
					{
						throw JsonSerializationException.Create(reader, "Unexpected end when reading Regex.");
					}
					if (string.Equals(text2, "Pattern", 5))
					{
						text = (string)reader.Value;
					}
					else if (string.Equals(text2, "Options", 5))
					{
						regexOptions = new RegexOptions?(serializer.Deserialize<RegexOptions>(reader));
					}
					else
					{
						reader.Skip();
					}
					break;
				}
				case JsonToken.Comment:
					break;
				default:
					if (tokenType == JsonToken.EndObject)
					{
						if (text == null)
						{
							throw JsonSerializationException.Create(reader, "Error deserializing Regex. No pattern found.");
						}
						return new Regex(text, regexOptions ?? 0);
					}
					break;
				}
			}
			throw JsonSerializationException.Create(reader, "Unexpected end when reading Regex.");
		}

		/// <summary>
		/// Determines whether this instance can convert the specified object type.
		/// </summary>
		/// <param name="objectType">Type of the object.</param>
		/// <returns>
		/// 	<c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
		/// </returns>
		// Token: 0x06000168 RID: 360 RVA: 0x00006F61 File Offset: 0x00005161
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(Regex);
		}

		// Token: 0x0400008D RID: 141
		private const string PatternName = "Pattern";

		// Token: 0x0400008E RID: 142
		private const string OptionsName = "Options";
	}
}
