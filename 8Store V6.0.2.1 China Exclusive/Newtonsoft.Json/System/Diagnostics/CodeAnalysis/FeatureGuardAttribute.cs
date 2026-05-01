using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000008 RID: 8
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(128, Inherited = false, AllowMultiple = true)]
	internal sealed class FeatureGuardAttribute : Attribute
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000020AD File Offset: 0x000002AD
		public FeatureGuardAttribute(Type featureType)
		{
			this.FeatureType = featureType;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020BC File Offset: 0x000002BC
		public Type FeatureType { get; }
	}
}
