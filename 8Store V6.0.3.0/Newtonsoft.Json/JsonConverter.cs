using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x02000022 RID: 34
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class JsonConverter
	{
		// Token: 0x06000092 RID: 146
		public abstract void WriteJson(JsonWriter writer, [Nullable(2)] object value, JsonSerializer serializer);

		// Token: 0x06000093 RID: 147
		[return: Nullable(2)]
		public abstract object ReadJson(JsonReader reader, Type objectType, [Nullable(2)] object existingValue, JsonSerializer serializer);

		// Token: 0x06000094 RID: 148
		public abstract bool CanConvert(Type objectType);

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00002E6F File Offset: 0x0000106F
		public virtual bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00002E72 File Offset: 0x00001072
		public virtual bool CanWrite
		{
			get
			{
				return true;
			}
		}
	}
}
