using System;
using System.Collections.Generic;

namespace Win81StoreRevival.Common
{
	// Token: 0x02000054 RID: 84
	public class SaveStateEventArgs : EventArgs
	{
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x0001B8F9 File Offset: 0x00019AF9
		// (set) Token: 0x060004E0 RID: 1248 RVA: 0x0001B901 File Offset: 0x00019B01
		public Dictionary<string, object> PageState { get; private set; }

		// Token: 0x060004E1 RID: 1249 RVA: 0x0001B90A File Offset: 0x00019B0A
		public SaveStateEventArgs(Dictionary<string, object> pageState)
		{
			this.PageState = pageState;
		}
	}
}
