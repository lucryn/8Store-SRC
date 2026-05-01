using System;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	/// <summary>
	/// Create a custom object
	/// </summary>
	/// <typeparam name="T">The object type to convert.</typeparam>
	// Token: 0x02000019 RID: 25
	public abstract class CustomCreationConverter<T> : JsonConverter
	{
		/// <summary>
		/// Writes the JSON representation of the object.
		/// </summary>
		/// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
		/// <param name="value">The value.</param>
		/// <param name="serializer">The calling serializer.</param>
		// Token: 0x0600013A RID: 314 RVA: 0x000060A4 File Offset: 0x000042A4
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotSupportedException("CustomCreationConverter should only be used while deserializing.");
		}

		/// <summary>
		/// Reads the JSON representation of the object.
		/// </summary>
		/// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
		/// <param name="objectType">Type of the object.</param>
		/// <param name="existingValue">The existing value of object being read.</param>
		/// <param name="serializer">The calling serializer.</param>
		/// <returns>The object value.</returns>
		// Token: 0x0600013B RID: 315 RVA: 0x000060B0 File Offset: 0x000042B0
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
			{
				return null;
			}
			T t = this.Create(objectType);
			if (t == null)
			{
				throw new JsonSerializationException("No object created.");
			}
			serializer.Populate(reader, t);
			return t;
		}

		/// <summary>
		/// Creates an object which will then be populated by the serializer.
		/// </summary>
		/// <param name="objectType">Type of the object.</param>
		/// <returns>The created object.</returns>
		// Token: 0x0600013C RID: 316
		public abstract T Create(Type objectType);

		/// <summary>
		/// Determines whether this instance can convert the specified object type.
		/// </summary>
		/// <param name="objectType">Type of the object.</param>
		/// <returns>
		/// 	<c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
		/// </returns>
		// Token: 0x0600013D RID: 317 RVA: 0x000060F8 File Offset: 0x000042F8
		public override bool CanConvert(Type objectType)
		{
			return typeof(T).IsAssignableFrom(objectType);
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="T:Newtonsoft.Json.JsonConverter" /> can write JSON.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this <see cref="T:Newtonsoft.Json.JsonConverter" /> can write JSON; otherwise, <c>false</c>.
		/// </value>
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600013E RID: 318 RVA: 0x0000610A File Offset: 0x0000430A
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}
	}
}
