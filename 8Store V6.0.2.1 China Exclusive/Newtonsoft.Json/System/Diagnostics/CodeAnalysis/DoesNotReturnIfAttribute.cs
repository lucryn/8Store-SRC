using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200000E RID: 14
	[AttributeUsage(2048, Inherited = false)]
	internal class DoesNotReturnIfAttribute : Attribute
	{
		// Token: 0x06000011 RID: 17 RVA: 0x0000210A File Offset: 0x0000030A
		public DoesNotReturnIfAttribute(bool parameterValue)
		{
			this.ParameterValue = parameterValue;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002119 File Offset: 0x00000319
		public bool ParameterValue { get; }
	}
}
