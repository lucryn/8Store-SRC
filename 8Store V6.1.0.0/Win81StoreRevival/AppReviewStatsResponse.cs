using System;
using Newtonsoft.Json;

namespace Win81StoreRevival
{
	// Token: 0x02000013 RID: 19
	public class AppReviewStatsResponse
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000090 RID: 144 RVA: 0x000033DF File Offset: 0x000015DF
		// (set) Token: 0x06000091 RID: 145 RVA: 0x000033E7 File Offset: 0x000015E7
		[JsonProperty("app_id")]
		public string AppId { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000092 RID: 146 RVA: 0x000033F0 File Offset: 0x000015F0
		// (set) Token: 0x06000093 RID: 147 RVA: 0x000033F8 File Offset: 0x000015F8
		[JsonProperty("average_rating")]
		public double AverageRating { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00003401 File Offset: 0x00001601
		// (set) Token: 0x06000095 RID: 149 RVA: 0x00003409 File Offset: 0x00001609
		[JsonProperty("review_count")]
		public int ReviewCount { get; set; }
	}
}
