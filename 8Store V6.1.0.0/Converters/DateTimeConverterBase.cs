using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000E9 RID: 233
	public abstract class DateTimeConverterBase : JsonConverter
	{
		// Token: 0x06000BFC RID: 3068 RVA: 0x0002F5F1 File Offset: 0x0002D7F1
		[NullableContext(1)]
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(DateTime) || objectType == typeof(DateTime?) || (objectType == typeof(DateTimeOffset) || objectType == typeof(DateTimeOffset?));
		}
	}
}
