using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000011 RID: 17
	[NullableContext(2)]
	[Nullable(0)]
	[AttributeUsage(32767, Inherited = false, AllowMultiple = true)]
	internal sealed class UnconditionalSuppressMessageAttribute : Attribute
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002171 File Offset: 0x00000371
		[NullableContext(1)]
		public UnconditionalSuppressMessageAttribute(string category, string checkId)
		{
			this.Category = category;
			this.CheckId = checkId;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002187 File Offset: 0x00000387
		[Nullable(1)]
		public string Category { [NullableContext(1)] get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000218F File Offset: 0x0000038F
		[Nullable(1)]
		public string CheckId { [NullableContext(1)] get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002197 File Offset: 0x00000397
		// (set) Token: 0x0600001F RID: 31 RVA: 0x0000219F File Offset: 0x0000039F
		public string Scope { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000021A8 File Offset: 0x000003A8
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000021B0 File Offset: 0x000003B0
		public string Target { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000021B9 File Offset: 0x000003B9
		// (set) Token: 0x06000023 RID: 35 RVA: 0x000021C1 File Offset: 0x000003C1
		public string MessageId { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000021CA File Offset: 0x000003CA
		// (set) Token: 0x06000025 RID: 37 RVA: 0x000021D2 File Offset: 0x000003D2
		public string Justification { get; set; }
	}
}
