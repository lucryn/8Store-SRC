using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000006 RID: 6
	[AttributeUsage(28108, Inherited = false)]
	internal sealed class DynamicallyAccessedMembersAttribute : Attribute
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002096 File Offset: 0x00000296
		public DynamicallyAccessedMembersAttribute(DynamicallyAccessedMemberTypes memberTypes)
		{
			this.MemberTypes = memberTypes;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020A5 File Offset: 0x000002A5
		public DynamicallyAccessedMemberTypes MemberTypes { get; }
	}
}
