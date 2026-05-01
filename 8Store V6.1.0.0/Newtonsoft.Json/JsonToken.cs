using System;

namespace Newtonsoft.Json
{
	// Token: 0x02000038 RID: 56
	public enum JsonToken
	{
		// Token: 0x0400010E RID: 270
		None,
		// Token: 0x0400010F RID: 271
		StartObject,
		// Token: 0x04000110 RID: 272
		StartArray,
		// Token: 0x04000111 RID: 273
		StartConstructor,
		// Token: 0x04000112 RID: 274
		PropertyName,
		// Token: 0x04000113 RID: 275
		Comment,
		// Token: 0x04000114 RID: 276
		Raw,
		// Token: 0x04000115 RID: 277
		Integer,
		// Token: 0x04000116 RID: 278
		Float,
		// Token: 0x04000117 RID: 279
		String,
		// Token: 0x04000118 RID: 280
		Boolean,
		// Token: 0x04000119 RID: 281
		Null,
		// Token: 0x0400011A RID: 282
		Undefined,
		// Token: 0x0400011B RID: 283
		EndObject,
		// Token: 0x0400011C RID: 284
		EndArray,
		// Token: 0x0400011D RID: 285
		EndConstructor,
		// Token: 0x0400011E RID: 286
		Date,
		// Token: 0x0400011F RID: 287
		Bytes
	}
}
