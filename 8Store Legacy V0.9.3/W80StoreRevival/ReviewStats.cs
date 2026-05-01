using System;

namespace W80StoreRevival
{
	// Token: 0x02000011 RID: 17
	public class ReviewStats
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x0000C054 File Offset: 0x0000A254
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x0000C06B File Offset: 0x0000A26B
		public string StarRatingDisplay { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x0000C074 File Offset: 0x0000A274
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x0000C08B File Offset: 0x0000A28B
		public int TotalReviews { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x0000C094 File Offset: 0x0000A294
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x0000C0AB File Offset: 0x0000A2AB
		public double AverageRating { get; set; }
	}
}
