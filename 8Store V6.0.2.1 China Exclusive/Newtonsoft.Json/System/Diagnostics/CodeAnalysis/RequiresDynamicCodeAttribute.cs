using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200000F RID: 15
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(100, Inherited = false)]
	internal sealed class RequiresDynamicCodeAttribute : Attribute
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002121 File Offset: 0x00000321
		public RequiresDynamicCodeAttribute(string message)
		{
			this.Message = message;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002130 File Offset: 0x00000330
		public string Message { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002138 File Offset: 0x00000338
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002140 File Offset: 0x00000340
		[Nullable(2)]
		public string Url { [NullableContext(2)] get; [NullableContext(2)] set; }
	}
}
