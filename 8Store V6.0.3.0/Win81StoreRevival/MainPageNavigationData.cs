using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Win81StoreRevival
{
	// Token: 0x02000029 RID: 41
	public class MainPageNavigationData
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000DDD0 File Offset: 0x0000BFD0
		// (set) Token: 0x06000254 RID: 596 RVA: 0x0000DDD8 File Offset: 0x0000BFD8
		public ObservableCollection<StoreApp> LoadedApps { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000DDE1 File Offset: 0x0000BFE1
		// (set) Token: 0x06000256 RID: 598 RVA: 0x0000DDE9 File Offset: 0x0000BFE9
		public List<StoreApp> AllAppsForStats { get; set; }
	}
}
