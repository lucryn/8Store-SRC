using System;
using System.Collections.Generic;

namespace Win81StoreRevival
{
	// Token: 0x02000022 RID: 34
	public class PublisherPageData
	{
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00009F4D File Offset: 0x0000814D
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x00009F55 File Offset: 0x00008155
		public string PublisherName { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00009F5E File Offset: 0x0000815E
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x00009F66 File Offset: 0x00008166
		public List<StoreApp> PublisherApps { get; set; }
	}
}
