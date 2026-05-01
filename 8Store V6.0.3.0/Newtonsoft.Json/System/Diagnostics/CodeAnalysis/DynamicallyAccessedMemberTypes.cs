using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000007 RID: 7
	[Flags]
	internal enum DynamicallyAccessedMemberTypes
	{
		// Token: 0x04000005 RID: 5
		None = 0,
		// Token: 0x04000006 RID: 6
		PublicParameterlessConstructor = 1,
		// Token: 0x04000007 RID: 7
		PublicConstructors = 3,
		// Token: 0x04000008 RID: 8
		NonPublicConstructors = 4,
		// Token: 0x04000009 RID: 9
		PublicMethods = 8,
		// Token: 0x0400000A RID: 10
		NonPublicMethods = 16,
		// Token: 0x0400000B RID: 11
		PublicFields = 32,
		// Token: 0x0400000C RID: 12
		NonPublicFields = 64,
		// Token: 0x0400000D RID: 13
		PublicNestedTypes = 128,
		// Token: 0x0400000E RID: 14
		NonPublicNestedTypes = 256,
		// Token: 0x0400000F RID: 15
		PublicProperties = 512,
		// Token: 0x04000010 RID: 16
		NonPublicProperties = 1024,
		// Token: 0x04000011 RID: 17
		PublicEvents = 2048,
		// Token: 0x04000012 RID: 18
		NonPublicEvents = 4096,
		// Token: 0x04000013 RID: 19
		Interfaces = 8192,
		// Token: 0x04000014 RID: 20
		All = -1
	}
}
