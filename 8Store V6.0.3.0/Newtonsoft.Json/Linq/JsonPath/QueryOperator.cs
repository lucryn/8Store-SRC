using System;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020000DD RID: 221
	internal enum QueryOperator
	{
		// Token: 0x0400040A RID: 1034
		None,
		// Token: 0x0400040B RID: 1035
		Equals,
		// Token: 0x0400040C RID: 1036
		NotEquals,
		// Token: 0x0400040D RID: 1037
		Exists,
		// Token: 0x0400040E RID: 1038
		LessThan,
		// Token: 0x0400040F RID: 1039
		LessThanOrEquals,
		// Token: 0x04000410 RID: 1040
		GreaterThan,
		// Token: 0x04000411 RID: 1041
		GreaterThanOrEquals,
		// Token: 0x04000412 RID: 1042
		And,
		// Token: 0x04000413 RID: 1043
		Or,
		// Token: 0x04000414 RID: 1044
		RegexEquals,
		// Token: 0x04000415 RID: 1045
		StrictEquals,
		// Token: 0x04000416 RID: 1046
		StrictNotEquals
	}
}
