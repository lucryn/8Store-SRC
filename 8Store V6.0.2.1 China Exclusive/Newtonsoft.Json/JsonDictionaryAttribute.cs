using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x02000026 RID: 38
	[AttributeUsage(1028, AllowMultiple = false)]
	public sealed class JsonDictionaryAttribute : JsonContainerAttribute
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x00002F9E File Offset: 0x0000119E
		public JsonDictionaryAttribute()
		{
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00002FA6 File Offset: 0x000011A6
		[NullableContext(1)]
		public JsonDictionaryAttribute(string id) : base(id)
		{
		}
	}
}
