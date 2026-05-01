using System;
using System.Collections.Generic;

namespace Win81StoreRevival
{
	// Token: 0x0200001E RID: 30
	public class PublisherPageData
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000185 RID: 389 RVA: 0x0000965A File Offset: 0x0000785A
		// (set) Token: 0x06000186 RID: 390 RVA: 0x00009662 File Offset: 0x00007862
		public string PublisherName { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000187 RID: 391 RVA: 0x0000966B File Offset: 0x0000786B
		// (set) Token: 0x06000188 RID: 392 RVA: 0x00009673 File Offset: 0x00007873
		public List<StoreApp> PublisherApps { get; set; }
	}
}
