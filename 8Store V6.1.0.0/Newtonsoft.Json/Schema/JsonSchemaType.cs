using System;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x020000B8 RID: 184
	[Flags]
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public enum JsonSchemaType
	{
		// Token: 0x04000399 RID: 921
		None = 0,
		// Token: 0x0400039A RID: 922
		String = 1,
		// Token: 0x0400039B RID: 923
		Float = 2,
		// Token: 0x0400039C RID: 924
		Integer = 4,
		// Token: 0x0400039D RID: 925
		Boolean = 8,
		// Token: 0x0400039E RID: 926
		Object = 16,
		// Token: 0x0400039F RID: 927
		Array = 32,
		// Token: 0x040003A0 RID: 928
		Null = 64,
		// Token: 0x040003A1 RID: 929
		Any = 127
	}
}
