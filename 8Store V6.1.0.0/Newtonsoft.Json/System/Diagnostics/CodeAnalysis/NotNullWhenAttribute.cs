using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200000B RID: 11
	[AttributeUsage(2048, AllowMultiple = false)]
	internal sealed class NotNullWhenAttribute : Attribute
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000020E3 File Offset: 0x000002E3
		public NotNullWhenAttribute(bool returnValue)
		{
			this.ReturnValue = returnValue;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000020F2 File Offset: 0x000002F2
		public bool ReturnValue { get; }
	}
}
