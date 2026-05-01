using System;

namespace W80StoreRevival
{
	// Token: 0x02000005 RID: 5
	public class AppDetailsNavigationData
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00005B8C File Offset: 0x00003D8C
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00005BA3 File Offset: 0x00003DA3
		public StoreApp App { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00005BAC File Offset: 0x00003DAC
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00005BC3 File Offset: 0x00003DC3
		public bool ShowReviews { get; set; }
	}
}
