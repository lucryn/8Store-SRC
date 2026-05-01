using System;

namespace System.ComponentModel
{
	// Token: 0x02000011 RID: 17
	[AttributeUsage(4)]
	public sealed class InitializationEventAttribute : Attribute
	{
		// Token: 0x0600006D RID: 109 RVA: 0x000029F7 File Offset: 0x00000BF7
		public InitializationEventAttribute(string eventName)
		{
			this.EventName = eventName;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002A06 File Offset: 0x00000C06
		public string EventName { get; }
	}
}
