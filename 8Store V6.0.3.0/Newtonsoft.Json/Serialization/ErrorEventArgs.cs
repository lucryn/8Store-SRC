using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000085 RID: 133
	[NullableContext(1)]
	[Nullable(0)]
	public class ErrorEventArgs : EventArgs
	{
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x0001B566 File Offset: 0x00019766
		[Nullable(2)]
		public object CurrentObject { [NullableContext(2)] get; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x0001B56E File Offset: 0x0001976E
		public ErrorContext ErrorContext { get; }

		// Token: 0x06000690 RID: 1680 RVA: 0x0001B576 File Offset: 0x00019776
		public ErrorEventArgs([Nullable(2)] object currentObject, ErrorContext errorContext)
		{
			this.CurrentObject = currentObject;
			this.ErrorContext = errorContext;
		}
	}
}
