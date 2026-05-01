using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Win81StoreRevival
{
	// Token: 0x0200003C RID: 60
	public class MainPageNavigationData
	{
		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x00015491 File Offset: 0x00013691
		// (set) Token: 0x0600043C RID: 1084 RVA: 0x00015499 File Offset: 0x00013699
		public ObservableCollection<StoreApp> LoadedApps { get; set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x000154A2 File Offset: 0x000136A2
		// (set) Token: 0x0600043E RID: 1086 RVA: 0x000154AA File Offset: 0x000136AA
		public List<StoreApp> AllAppsForStats { get; set; }
	}
}
