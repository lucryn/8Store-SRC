using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000111 RID: 273
	internal enum BsonType : sbyte
	{
		// Token: 0x04000474 RID: 1140
		Number = 1,
		// Token: 0x04000475 RID: 1141
		String,
		// Token: 0x04000476 RID: 1142
		Object,
		// Token: 0x04000477 RID: 1143
		Array,
		// Token: 0x04000478 RID: 1144
		Binary,
		// Token: 0x04000479 RID: 1145
		Undefined,
		// Token: 0x0400047A RID: 1146
		Oid,
		// Token: 0x0400047B RID: 1147
		Boolean,
		// Token: 0x0400047C RID: 1148
		Date,
		// Token: 0x0400047D RID: 1149
		Null,
		// Token: 0x0400047E RID: 1150
		Regex,
		// Token: 0x0400047F RID: 1151
		Reference,
		// Token: 0x04000480 RID: 1152
		Code,
		// Token: 0x04000481 RID: 1153
		Symbol,
		// Token: 0x04000482 RID: 1154
		CodeWScope,
		// Token: 0x04000483 RID: 1155
		Integer,
		// Token: 0x04000484 RID: 1156
		TimeStamp,
		// Token: 0x04000485 RID: 1157
		Long,
		// Token: 0x04000486 RID: 1158
		MinKey = -1,
		// Token: 0x04000487 RID: 1159
		MaxKey = 127
	}
}
