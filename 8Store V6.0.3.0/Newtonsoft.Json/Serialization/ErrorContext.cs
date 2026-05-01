using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000084 RID: 132
	[NullableContext(1)]
	[Nullable(0)]
	public class ErrorContext
	{
		// Token: 0x06000685 RID: 1669 RVA: 0x0001B4FF File Offset: 0x000196FF
		internal ErrorContext([Nullable(2)] object originalObject, [Nullable(2)] object member, string path, Exception error)
		{
			this.OriginalObject = originalObject;
			this.Member = member;
			this.Error = error;
			this.Path = path;
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x0001B524 File Offset: 0x00019724
		// (set) Token: 0x06000687 RID: 1671 RVA: 0x0001B52C File Offset: 0x0001972C
		internal bool Traced { get; set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x0001B535 File Offset: 0x00019735
		public Exception Error { get; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x0001B53D File Offset: 0x0001973D
		[Nullable(2)]
		public object OriginalObject { [NullableContext(2)] get; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x0001B545 File Offset: 0x00019745
		[Nullable(2)]
		public object Member { [NullableContext(2)] get; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x0001B54D File Offset: 0x0001974D
		public string Path { get; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x0001B555 File Offset: 0x00019755
		// (set) Token: 0x0600068D RID: 1677 RVA: 0x0001B55D File Offset: 0x0001975D
		public bool Handled { get; set; }
	}
}
