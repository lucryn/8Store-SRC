using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000010 RID: 16
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(100, Inherited = false)]
	internal sealed class RequiresUnreferencedCodeAttribute : Attribute
	{
		// Token: 0x06000017 RID: 23 RVA: 0x00002149 File Offset: 0x00000349
		public RequiresUnreferencedCodeAttribute(string message)
		{
			this.Message = message;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002158 File Offset: 0x00000358
		public string Message { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002160 File Offset: 0x00000360
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002168 File Offset: 0x00000368
		[Nullable(2)]
		public string Url { [NullableContext(2)] get; [NullableContext(2)] set; }
	}
}
