using System;

namespace Win81StoreRevival
{
	// Token: 0x02000047 RID: 71
	public class UpdateNavigationData
	{
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x00017B6F File Offset: 0x00015D6F
		// (set) Token: 0x060004AF RID: 1199 RVA: 0x00017B77 File Offset: 0x00015D77
		public AppVersionInfo VersionInfo { get; set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x00017B80 File Offset: 0x00015D80
		// (set) Token: 0x060004B1 RID: 1201 RVA: 0x00017B88 File Offset: 0x00015D88
		public string CurrentVersion { get; set; }
	}
}
