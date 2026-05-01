using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	/// <summary>
	/// Converts an <see cref="T:System.Enum" /> to and from its name string value.
	/// </summary>
	// Token: 0x02000021 RID: 33
	public class StringEnumConverter : JsonConverter
	{
		/// <summary>
		/// Gets or sets a value indicating whether the written enum text should be camel case.
		/// </summary>
		/// <value><c>true</c> if the written enum text will be camel case; otherwise, <c>false</c>.</value>
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00006F78 File Offset: 0x00005178
		// (set) Token: 0x0600016B RID: 363 RVA: 0x00006F80 File Offset: 0x00005180
		public bool CamelCaseText { get; set; }

		/// <summary>
		/// Writes the JSON representation of the object.
		/// </summary>
		/// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
		/// <param name="value">The value.</param>
		/// <param name="serializer">The calling serializer.</param>
		// Token: 0x0600016C RID: 364 RVA: 0x00006F8C File Offset: 0x0000518C
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			Enum @enum = (Enum)value;
			string text = @enum.ToString("G");
			if (char.IsNumber(text.get_Chars(0)) || text.get_Chars(0) == '-')
			{
				writer.WriteValue(value);
				return;
			}
			BidirectionalDictionary<string, string> enumNameMap = this.GetEnumNameMap(@enum.GetType());
			string[] array = text.Split(new char[]
			{
				','
			});
			for (int i = 0; i < array.Length; i++)
			{
				string text2 = array[i].Trim();
				string text3;
				enumNameMap.TryGetByFirst(text2, out text3);
				text3 = (text3 ?? text2);
				if (this.CamelCaseText)
				{
					text3 = StringUtils.ToCamelCase(text3);
				}
				array[i] = text3;
			}
			string value2 = string.Join(", ", array);
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
		// Token: 0x0600016D RID: 365 RVA: 0x0000705C File Offset: 0x0000525C
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			bool flag = ReflectionUtils.IsNullableType(objectType);
			Type type = flag ? Nullable.GetUnderlyingType(objectType) : objectType;
			if (reader.TokenType != JsonToken.Null)
			{
				try
				{
					if (reader.TokenType == JsonToken.String)
					{
						string text = reader.Value.ToString();
						if (text == string.Empty && flag)
						{
							return null;
						}
						BidirectionalDictionary<string, string> enumNameMap = this.GetEnumNameMap(type);
						string text2;
						if (text.IndexOf(',') != -1)
						{
							string[] array = text.Split(new char[]
							{
								','
							});
							for (int i = 0; i < array.Length; i++)
							{
								string enumText = array[i].Trim();
								array[i] = StringEnumConverter.ResolvedEnumName(enumNameMap, enumText);
							}
							text2 = string.Join(", ", array);
						}
						else
						{
							text2 = StringEnumConverter.ResolvedEnumName(enumNameMap, text);
						}
						return Enum.Parse(type, text2, true);
					}
					else if (reader.TokenType == JsonToken.Integer)
					{
						return ConvertUtils.ConvertOrCast(reader.Value, CultureInfo.InvariantCulture, type);
					}
				}
				catch (Exception ex)
				{
					throw JsonSerializationException.Create(reader, "Error converting value {0} to type '{1}'.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.FormatValueForPrint(reader.Value), objectType), ex);
				}
				throw JsonSerializationException.Create(reader, "Unexpected token when parsing enum. Expected String or Integer, got {0}.".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			if (!ReflectionUtils.IsNullableType(objectType))
			{
				throw JsonSerializationException.Create(reader, "Cannot convert null value to {0}.".FormatWith(CultureInfo.InvariantCulture, objectType));
			}
			return null;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000071D0 File Offset: 0x000053D0
		private static string ResolvedEnumName(BidirectionalDictionary<string, string> map, string enumText)
		{
			string text;
			map.TryGetBySecond(enumText, out text);
			text = (text ?? enumText);
			return text;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000071F8 File Offset: 0x000053F8
		private BidirectionalDictionary<string, string> GetEnumNameMap(Type t)
		{
			BidirectionalDictionary<string, string> bidirectionalDictionary;
			if (!this._enumMemberNamesPerType.TryGetValue(t, ref bidirectionalDictionary))
			{
				lock (this._enumMemberNamesPerType)
				{
					if (this._enumMemberNamesPerType.TryGetValue(t, ref bidirectionalDictionary))
					{
						return bidirectionalDictionary;
					}
					bidirectionalDictionary = new BidirectionalDictionary<string, string>(StringComparer.OrdinalIgnoreCase, StringComparer.OrdinalIgnoreCase);
					foreach (FieldInfo fieldInfo in t.GetFields())
					{
						string name = fieldInfo.Name;
						string text = Enumerable.SingleOrDefault<string>(Enumerable.Select<EnumMemberAttribute, string>(Enumerable.Cast<EnumMemberAttribute>(CustomAttributeExtensions.GetCustomAttributes(fieldInfo, typeof(EnumMemberAttribute), true)), (EnumMemberAttribute a) => a.Value)) ?? fieldInfo.Name;
						string text2;
						if (bidirectionalDictionary.TryGetBySecond(text, out text2))
						{
							throw new InvalidOperationException("Enum name '{0}' already exists on enum '{1}'.".FormatWith(CultureInfo.InvariantCulture, text, t.Name));
						}
						bidirectionalDictionary.Set(name, text);
					}
					this._enumMemberNamesPerType[t] = bidirectionalDictionary;
				}
				return bidirectionalDictionary;
			}
			return bidirectionalDictionary;
		}

		/// <summary>
		/// Determines whether this instance can convert the specified object type.
		/// </summary>
		/// <param name="objectType">Type of the object.</param>
		/// <returns>
		/// <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
		/// </returns>
		// Token: 0x06000170 RID: 368 RVA: 0x00007340 File Offset: 0x00005540
		public override bool CanConvert(Type objectType)
		{
			Type type = ReflectionUtils.IsNullableType(objectType) ? Nullable.GetUnderlyingType(objectType) : objectType;
			return type.IsEnum();
		}

		// Token: 0x0400008F RID: 143
		private readonly Dictionary<Type, BidirectionalDictionary<string, string>> _enumMemberNamesPerType = new Dictionary<Type, BidirectionalDictionary<string, string>>();
	}
}
