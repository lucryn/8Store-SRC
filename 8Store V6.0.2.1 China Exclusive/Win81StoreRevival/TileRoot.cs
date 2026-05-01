using System;
using System.Collections.Generic;

namespace Win81StoreRevival
{
	// Token: 0x02000035 RID: 53
	public class TileRoot
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600034F RID: 847 RVA: 0x00014EFC File Offset: 0x000130FC
		// (set) Token: 0x06000350 RID: 848 RVA: 0x00014F04 File Offset: 0x00013104
		public Dictionary<string, TileApp> squareTile { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00014F0D File Offset: 0x0001310D
		// (set) Token: 0x06000352 RID: 850 RVA: 0x00014F15 File Offset: 0x00013115
		public Dictionary<string, TileApp> wideTile { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000353 RID: 851 RVA: 0x00014F1E File Offset: 0x0001311E
		// (set) Token: 0x06000354 RID: 852 RVA: 0x00014F26 File Offset: 0x00013126
		public Dictionary<string, TileApp> hugeTile { get; set; }
	}
}
