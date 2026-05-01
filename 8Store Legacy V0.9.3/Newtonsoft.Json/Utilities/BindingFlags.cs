using System;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000D2 RID: 210
	[Flags]
	internal enum BindingFlags
	{
		// Token: 0x040003BD RID: 957
		Default = 0,
		// Token: 0x040003BE RID: 958
		IgnoreCase = 1,
		// Token: 0x040003BF RID: 959
		DeclaredOnly = 2,
		// Token: 0x040003C0 RID: 960
		Instance = 4,
		// Token: 0x040003C1 RID: 961
		Static = 8,
		// Token: 0x040003C2 RID: 962
		Public = 16,
		// Token: 0x040003C3 RID: 963
		NonPublic = 32,
		// Token: 0x040003C4 RID: 964
		FlattenHierarchy = 64,
		// Token: 0x040003C5 RID: 965
		InvokeMethod = 256,
		// Token: 0x040003C6 RID: 966
		CreateInstance = 512,
		// Token: 0x040003C7 RID: 967
		GetField = 1024,
		// Token: 0x040003C8 RID: 968
		SetField = 2048,
		// Token: 0x040003C9 RID: 969
		GetProperty = 4096,
		// Token: 0x040003CA RID: 970
		SetProperty = 8192,
		// Token: 0x040003CB RID: 971
		PutDispProperty = 16384,
		// Token: 0x040003CC RID: 972
		ExactBinding = 65536,
		// Token: 0x040003CD RID: 973
		PutRefDispProperty = 32768,
		// Token: 0x040003CE RID: 974
		SuppressChangeType = 131072,
		// Token: 0x040003CF RID: 975
		OptionalParamBinding = 262144,
		// Token: 0x040003D0 RID: 976
		IgnoreReturn = 16777216
	}
}
