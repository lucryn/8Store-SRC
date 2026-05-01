using System;

namespace Win81StoreRevival
{
	// Token: 0x0200001C RID: 28
	public class AppDetailsNavigationData
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00009617 File Offset: 0x00007817
		// (set) Token: 0x0600017E RID: 382 RVA: 0x0000961F File Offset: 0x0000781F
		public StoreApp App { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00009628 File Offset: 0x00007828
		// (set) Token: 0x06000180 RID: 384 RVA: 0x00009630 File Offset: 0x00007830
		public bool ShowReviews { get; set; } = true;
	}
}
