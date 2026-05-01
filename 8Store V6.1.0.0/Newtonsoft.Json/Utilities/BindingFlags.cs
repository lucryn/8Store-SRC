using System;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000073 RID: 115
	[Flags]
	internal enum BindingFlags
	{
		// Token: 0x04000248 RID: 584
		Default = 0,
		// Token: 0x04000249 RID: 585
		IgnoreCase = 1,
		// Token: 0x0400024A RID: 586
		DeclaredOnly = 2,
		// Token: 0x0400024B RID: 587
		Instance = 4,
		// Token: 0x0400024C RID: 588
		Static = 8,
		// Token: 0x0400024D RID: 589
		Public = 16,
		// Token: 0x0400024E RID: 590
		NonPublic = 32,
		// Token: 0x0400024F RID: 591
		FlattenHierarchy = 64,
		// Token: 0x04000250 RID: 592
		InvokeMethod = 256,
		// Token: 0x04000251 RID: 593
		CreateInstance = 512,
		// Token: 0x04000252 RID: 594
		GetField = 1024,
		// Token: 0x04000253 RID: 595
		SetField = 2048,
		// Token: 0x04000254 RID: 596
		GetProperty = 4096,
		// Token: 0x04000255 RID: 597
		SetProperty = 8192,
		// Token: 0x04000256 RID: 598
		PutDispProperty = 16384,
		// Token: 0x04000257 RID: 599
		ExactBinding = 65536,
		// Token: 0x04000258 RID: 600
		PutRefDispProperty = 32768,
		// Token: 0x04000259 RID: 601
		SuppressChangeType = 131072,
		// Token: 0x0400025A RID: 602
		OptionalParamBinding = 262144,
		// Token: 0x0400025B RID: 603
		IgnoreReturn = 16777216
	}
}
