using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	/// <summary>
	/// Converts a <see cref="T:System.Collections.Generic.KeyValuePair`2" /> to and from JSON.
	/// </summary>
	// Token: 0x0200001F RID: 31
	public class KeyValuePairConverter : JsonConverter
	{
		/// <summary>
		/// Writes the JSON representation of the object.
		/// </summary>
		/// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
		/// <param name="value">The value.</param>
		/// <param name="serializer">The calling serializer.</param>
		// Token: 0x0600015D RID: 349 RVA: 0x00006A88 File Offset: 0x00004C88
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			Type type = value.GetType();
			PropertyInfo property = type.GetProperty("Key");
			PropertyInfo property2 = type.GetProperty("Value");
			DefaultContractResolver defaultContractResolver = serializer.ContractResolver as DefaultContractResolver;
			writer.WriteStartObject();
			writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.GetResolvedPropertyName("Key") : "Key");
			serializer.Serialize(writer, ReflectionUtils.GetMemberValue(property, value));
			writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.GetResolvedPropertyName("Value") : "Value");
			serializer.Serialize(writer, ReflectionUtils.GetMemberValue(property2, value));
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
		// Token: 0x0600015E RID: 350 RVA: 0x00006B20 File Offset: 0x00004D20
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			bool flag = ReflectionUtils.IsNullableType(objectType);
			if (reader.TokenType != JsonToken.Null)
			{
				Type type = flag ? Nullable.GetUnderlyingType(objectType) : objectType;
				IList<Type> genericArguments = type.GetGenericArguments();
				Type objectType2 = genericArguments[0];
				Type objectType3 = genericArguments[1];
				object obj = null;
				object obj2 = null;
				reader.Read();
				while (reader.TokenType == JsonToken.PropertyName)
				{
					string text = reader.Value.ToString();
					if (string.Equals(text, "Key", 5))
					{
						reader.Read();
						obj = serializer.Deserialize(reader, objectType2);
					}
					else if (string.Equals(text, "Value", 5))
					{
						reader.Read();
						obj2 = serializer.Deserialize(reader, objectType3);
					}
					else
					{
						reader.Skip();
					}
					reader.Read();
				}
				return Activator.CreateInstance(type, new object[]
				{
					obj,
					obj2
				});
			}
			if (!flag)
			{
				throw JsonSerializationException.Create(reader, "Cannot convert null value to KeyValuePair.");
			}
			return null;
		}

		/// <summary>
		/// Determines whether this instance can convert the specified object type.
		/// </summary>
		/// <param name="objectType">Type of the object.</param>
		/// <returns>
		/// 	<c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
		/// </returns>
		// Token: 0x0600015F RID: 351 RVA: 0x00006C0C File Offset: 0x00004E0C
		public override bool CanConvert(Type objectType)
		{
			Type type = ReflectionUtils.IsNullableType(objectType) ? Nullable.GetUnderlyingType(objectType) : objectType;
			return type.IsValueType() && type.IsGenericType() && type.GetGenericTypeDefinition() == typeof(KeyValuePair);
		}

		// Token: 0x0400008B RID: 139
		private const string KeyName = "Key";

		// Token: 0x0400008C RID: 140
		private const string ValueName = "Value";
	}
}
