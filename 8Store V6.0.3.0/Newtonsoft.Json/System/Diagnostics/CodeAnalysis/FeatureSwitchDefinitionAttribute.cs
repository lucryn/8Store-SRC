using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000009 RID: 9
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(128, Inherited = false)]
	internal sealed class FeatureSwitchDefinitionAttribute : Attribute
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000020C4 File Offset: 0x000002C4
		public FeatureSwitchDefinitionAttribute(string switchName)
		{
			this.SwitchName = switchName;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020D3 File Offset: 0x000002D3
		public string SwitchName { get; }
	}
}
