using System;
using System.Collections.Generic;

namespace Win81StoreRevival
{
	// Token: 0x02000018 RID: 24
	public class AppCollection
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00005D4F File Offset: 0x00003F4F
		// (set) Token: 0x0600010E RID: 270 RVA: 0x00005D57 File Offset: 0x00003F57
		public string Name { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00005D60 File Offset: 0x00003F60
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00005D68 File Offset: 0x00003F68
		public List<string> AppIcons { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00005D71 File Offset: 0x00003F71
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00005D79 File Offset: 0x00003F79
		public string AppCount { get; set; }

		// Token: 0x06000113 RID: 275 RVA: 0x00005D82 File Offset: 0x00003F82
		public AppCollection()
		{
			this.AppIcons = new List<string>();
		}
	}
}
