using System;
using System.Collections.Generic;

namespace Win81StoreRevival.Common
{
	// Token: 0x02000053 RID: 83
	public class LoadStateEventArgs : EventArgs
	{
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x0001B8BD File Offset: 0x00019ABD
		// (set) Token: 0x060004DB RID: 1243 RVA: 0x0001B8C5 File Offset: 0x00019AC5
		public object NavigationParameter { get; private set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x0001B8CE File Offset: 0x00019ACE
		// (set) Token: 0x060004DD RID: 1245 RVA: 0x0001B8D6 File Offset: 0x00019AD6
		public Dictionary<string, object> PageState { get; private set; }

		// Token: 0x060004DE RID: 1246 RVA: 0x0001B8DF File Offset: 0x00019ADF
		public LoadStateEventArgs(object navigationParameter, Dictionary<string, object> pageState)
		{
			this.NavigationParameter = navigationParameter;
			this.PageState = pageState;
		}
	}
}
