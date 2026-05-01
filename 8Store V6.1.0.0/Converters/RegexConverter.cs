using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000EF RID: 239
	[NullableContext(1)]
	[Nullable(0)]
	[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
	[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
	public class RegexConverter : JsonConverter
	{
		// Token: 0x06000C1F RID: 3103 RVA: 0x00030364 File Offset: 0x0002E564
		public override void WriteJson(JsonWriter writer, [Nullable(2)] object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			Regex regex = (Regex)value;
			BsonWriter bsonWriter = writer as BsonWriter;
			if (bsonWriter != null)
			{
				this.WriteBson(bsonWriter, regex);
				return;
			}
			this.WriteJson(writer, regex, serializer);
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x0003039E File Offset: 0x0002E59E
		private bool HasFlag(RegexOptions options, RegexOptions flag)
		{
			return (options & flag) == flag;
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x000303A8 File Offset: 0x0002E5A8
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

		// Token: 0x06000C22 RID: 3106 RVA: 0x00030440 File Offset: 0x0002E640
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

		// Token: 0x06000C23 RID: 3107 RVA: 0x000304BC File Offset: 0x0002E6BC
		[return: Nullable(2)]
		public override object ReadJson(JsonReader reader, Type objectType, [Nullable(2)] object existingValue, JsonSerializer serializer)
		{
			JsonToken tokenType = reader.TokenType;
			if (tokenType == JsonToken.StartObject)
			{
				return this.ReadRegexObject(reader, serializer);
			}
			if (tokenType == JsonToken.String)
			{
				return this.ReadRegexString(reader);
			}
			if (tokenType != JsonToken.Null)
			{
				throw JsonSerializationException.Create(reader, "Unexpected token when reading Regex.");
			}
			return null;
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x00030500 File Offset: 0x0002E700
		private object ReadRegexString(JsonReader reader)
		{
			string text = (string)reader.Value;
			if (text.Length > 0 && text.get_Chars(0) == '/')
			{
				int num = text.LastIndexOf('/');
				if (num > 0)
				{
					string text2 = text.Substring(1, num - 1);
					RegexOptions regexOptions = MiscellaneousUtils.GetRegexOptions(text.Substring(num + 1));
					return new Regex(text2, regexOptions);
				}
			}
			throw JsonSerializationException.Create(reader, "Regex pattern must be enclosed by slashes.");
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x00030568 File Offset: 0x0002E768
		private Regex ReadRegexObject(JsonReader reader, JsonSerializer serializer)
		{
			string text = null;
			RegexOptions? regexOptions = default(RegexOptions?);
			while (reader.Read())
			{
				JsonToken tokenType = reader.TokenType;
				if (tokenType != JsonToken.PropertyName)
				{
					if (tokenType != JsonToken.Comment)
					{
						if (tokenType == JsonToken.EndObject)
						{
							if (text == null)
							{
								throw JsonSerializationException.Create(reader, "Error deserializing Regex. No pattern found.");
							}
							return new Regex(text, regexOptions.GetValueOrDefault());
						}
					}
				}
				else
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
				}
			}
			throw JsonSerializationException.Create(reader, "Unexpected end when reading Regex.");
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x00030632 File Offset: 0x0002E832
		public override bool CanConvert(Type objectType)
		{
			return objectType.Name == "Regex" && this.IsRegex(objectType);
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x0003064F File Offset: 0x0002E84F
		[MethodImpl(8)]
		private bool IsRegex(Type objectType)
		{
			return objectType == typeof(Regex);
		}

		// Token: 0x0400042E RID: 1070
		private const string PatternName = "Pattern";

		// Token: 0x0400042F RID: 1071
		private const string OptionsName = "Options";
	}
}
