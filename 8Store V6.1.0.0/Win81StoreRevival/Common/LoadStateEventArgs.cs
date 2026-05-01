using System;
using System.Collections.Generic;

namespace Win81StoreRevival.Common
{
	// Token: 0x0200005F RID: 95
	public class LoadStateEventArgs : EventArgs
	{
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x0001D53C File Offset: 0x0001B73C
		// (set) Token: 0x06000628 RID: 1576 RVA: 0x0001D544 File Offset: 0x0001B744
		public object NavigationParameter { get; private set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x0001D54D File Offset: 0x0001B74D
		// (set) Token: 0x0600062A RID: 1578 RVA: 0x0001D555 File Offset: 0x0001B755
		public Dictionary<string, object> PageState { get; private set; }

		// Token: 0x0600062B RID: 1579 RVA: 0x0001D55E File Offset: 0x0001B75E
		public LoadStateEventArgs(object navigationParameter, Dictionary<string, object> pageState)
		{
			this.NavigationParameter = navigationParameter;
			this.PageState = pageState;
		}
	}
}
