using System;

namespace Win81StoreRevival
{
	// Token: 0x0200001F RID: 31
	public class AppDetailsNavigationData
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00009EE9 File Offset: 0x000080E9
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x00009EF1 File Offset: 0x000080F1
		public StoreApp App { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00009EFA File Offset: 0x000080FA
		// (set) Token: 0x060001EA RID: 490 RVA: 0x00009F02 File Offset: 0x00008102
		public bool ShowReviews { get; set; } = true;

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00009F0B File Offset: 0x0000810B
		// (set) Token: 0x060001EC RID: 492 RVA: 0x00009F13 File Offset: 0x00008113
		public bool AutoInstall { get; set; }
	}
}
