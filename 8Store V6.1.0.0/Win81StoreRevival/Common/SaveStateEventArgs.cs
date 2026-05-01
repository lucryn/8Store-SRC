using System;
using System.Collections.Generic;

namespace Win81StoreRevival.Common
{
	// Token: 0x02000060 RID: 96
	public class SaveStateEventArgs : EventArgs
	{
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x0001D574 File Offset: 0x0001B774
		// (set) Token: 0x0600062D RID: 1581 RVA: 0x0001D57C File Offset: 0x0001B77C
		public Dictionary<string, object> PageState { get; private set; }

		// Token: 0x0600062E RID: 1582 RVA: 0x0001D585 File Offset: 0x0001B785
		public SaveStateEventArgs(Dictionary<string, object> pageState)
		{
			this.PageState = pageState;
		}
	}
}
