using System;

namespace Win81StoreRevival
{
	// Token: 0x02000041 RID: 65
	public class UpdateNavigationData
	{
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060003CA RID: 970 RVA: 0x00016FE6 File Offset: 0x000151E6
		// (set) Token: 0x060003CB RID: 971 RVA: 0x00016FEE File Offset: 0x000151EE
		public AppVersionInfo VersionInfo { get; set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060003CC RID: 972 RVA: 0x00016FF7 File Offset: 0x000151F7
		// (set) Token: 0x060003CD RID: 973 RVA: 0x00016FFF File Offset: 0x000151FF
		public string CurrentVersion { get; set; }
	}
}
