using System;
using Newtonsoft.Json.Schema;

namespace Newtonsoft.Json
{
	/// <summary>
	/// Converts an object to and from JSON.
	/// </summary>
	// Token: 0x02000017 RID: 23
	public abstract class JsonConverter
	{
		/// <summary>
		/// Writes the JSON representation of the object.
		/// </summary>
		/// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
		/// <param name="value">The value.</param>
		/// <param name="serializer">The calling serializer.</param>
		// Token: 0x0600012F RID: 303
		public abstract void WriteJson(JsonWriter writer, object value, JsonSerializer serializer);

		/// <summary>
		/// Reads the JSON representation of the object.
		/// </summary>
		/// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
		/// <param name="objectType">Type of the object.</param>
		/// <param name="existingValue">The existing value of object being read.</param>
		/// <param name="serializer">The calling serializer.</param>
		/// <returns>The object value.</returns>
		// Token: 0x06000130 RID: 304
		public abstract object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer);

		/// <summary>
		/// Determines whether this instance can convert the specified object type.
		/// </summary>
		/// <param name="objectType">Type of the object.</param>
		/// <returns>
		/// 	<c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
		/// </returns>
		// Token: 0x06000131 RID: 305
		public abstract bool CanConvert(Type objectType);

		/// <summary>
		/// Gets the <see cref="T:Newtonsoft.Json.Schema.JsonSchema" /> of the JSON produced by the JsonConverter.
		/// </summary>
		/// <returns>The <see cref="T:Newtonsoft.Json.Schema.JsonSchema" /> of the JSON produced by the JsonConverter.</returns>
		// Token: 0x06000132 RID: 306 RVA: 0x00005FF9 File Offset: 0x000041F9
		public virtual JsonSchema GetSchema()
		{
			return null;
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="T:Newtonsoft.Json.JsonConverter" /> can read JSON.
		/// </summary>
		/// <value><c>true</c> if this <see cref="T:Newtonsoft.Json.JsonConverter" /> can read JSON; otherwise, <c>false</c>.</value>
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00005FFC File Offset: 0x000041FC
		public virtual bool CanRead
		{
			get
			{
				return true;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="T:Newtonsoft.Json.JsonConverter" /> can write JSON.
		/// </summary>
		/// <value><c>true</c> if this <see cref="T:Newtonsoft.Json.JsonConverter" /> can write JSON; otherwise, <c>false</c>.</value>
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00005FFF File Offset: 0x000041FF
		public virtual bool CanWrite
		{
			get
			{
				return true;
			}
		}
	}
}
