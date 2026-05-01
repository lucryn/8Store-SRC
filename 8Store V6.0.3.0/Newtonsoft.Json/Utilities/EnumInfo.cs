using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000060 RID: 96
	[NullableContext(1)]
	[Nullable(0)]
	internal class EnumInfo
	{
		// Token: 0x06000509 RID: 1289 RVA: 0x00014713 File Offset: 0x00012913
		public EnumInfo(bool isFlags, ulong[] values, string[] names, string[] resolvedNames)
		{
			this.IsFlags = isFlags;
			this.Values = values;
			this.Names = names;
			this.ResolvedNames = resolvedNames;
		}

		// Token: 0x04000200 RID: 512
		public readonly bool IsFlags;

		// Token: 0x04000201 RID: 513
		public readonly ulong[] Values;

		// Token: 0x04000202 RID: 514
		public readonly string[] Names;

		// Token: 0x04000203 RID: 515
		public readonly string[] ResolvedNames;
	}
}
