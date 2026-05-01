using System;
using System.Collections.Generic;

namespace Win81StoreRevival
{
	// Token: 0x02000039 RID: 57
	public class TileRoot
	{
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0001532C File Offset: 0x0001352C
		// (set) Token: 0x0600040F RID: 1039 RVA: 0x00015334 File Offset: 0x00013534
		public Dictionary<string, TileApp> squareTile { get; set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x0001533D File Offset: 0x0001353D
		// (set) Token: 0x06000411 RID: 1041 RVA: 0x00015345 File Offset: 0x00013545
		public Dictionary<string, TileApp> wideTile { get; set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0001534E File Offset: 0x0001354E
		// (set) Token: 0x06000413 RID: 1043 RVA: 0x00015356 File Offset: 0x00013556
		public Dictionary<string, TileApp> hugeTile { get; set; }
	}
}
