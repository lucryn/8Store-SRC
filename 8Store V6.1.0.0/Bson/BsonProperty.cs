using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000110 RID: 272
	internal class BsonProperty
	{
		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000D35 RID: 3381 RVA: 0x0003439F File Offset: 0x0003259F
		// (set) Token: 0x06000D36 RID: 3382 RVA: 0x000343A7 File Offset: 0x000325A7
		public BsonString Name { get; set; }

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000D37 RID: 3383 RVA: 0x000343B0 File Offset: 0x000325B0
		// (set) Token: 0x06000D38 RID: 3384 RVA: 0x000343B8 File Offset: 0x000325B8
		public BsonToken Value { get; set; }
	}
}
