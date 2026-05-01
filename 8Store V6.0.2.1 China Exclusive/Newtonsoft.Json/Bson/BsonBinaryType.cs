using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000103 RID: 259
	internal enum BsonBinaryType : byte
	{
		// Token: 0x04000446 RID: 1094
		Binary,
		// Token: 0x04000447 RID: 1095
		Function,
		// Token: 0x04000448 RID: 1096
		[Obsolete("This type has been deprecated in the BSON specification. Use Binary instead.")]
		BinaryOld,
		// Token: 0x04000449 RID: 1097
		[Obsolete("This type has been deprecated in the BSON specification. Use Uuid instead.")]
		UuidOld,
		// Token: 0x0400044A RID: 1098
		Uuid,
		// Token: 0x0400044B RID: 1099
		Md5,
		// Token: 0x0400044C RID: 1100
		UserDefined = 128
	}
}
