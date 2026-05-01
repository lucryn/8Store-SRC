using System;

namespace Newtonsoft.Json.Converters
{
	/// <summary>
	/// Provides a base class for converting a <see cref="T:System.DateTime" /> to and from JSON.
	/// </summary>
	// Token: 0x0200001A RID: 26
	public abstract class DateTimeConverterBase : JsonConverter
	{
		/// <summary>
		/// Determines whether this instance can convert the specified object type.
		/// </summary>
		/// <param name="objectType">Type of the object.</param>
		/// <returns>
		/// 	<c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
		/// </returns>
		// Token: 0x06000140 RID: 320 RVA: 0x00006115 File Offset: 0x00004315
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(DateTime) || objectType == typeof(DateTime?) || (objectType == typeof(DateTimeOffset) || objectType == typeof(DateTimeOffset?));
		}
	}
}
