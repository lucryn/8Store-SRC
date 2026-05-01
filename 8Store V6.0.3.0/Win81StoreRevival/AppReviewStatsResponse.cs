using System;
using Newtonsoft.Json;

namespace Win81StoreRevival
{
	// Token: 0x02000012 RID: 18
	public class AppReviewStatsResponse
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00003839 File Offset: 0x00001A39
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00003841 File Offset: 0x00001A41
		[JsonProperty("app_id")]
		public string AppId { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600008C RID: 140 RVA: 0x0000384A File Offset: 0x00001A4A
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00003852 File Offset: 0x00001A52
		[JsonProperty("average_rating")]
		public double AverageRating { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600008E RID: 142 RVA: 0x0000385B File Offset: 0x00001A5B
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00003863 File Offset: 0x00001A63
		[JsonProperty("review_count")]
		public int ReviewCount { get; set; }
	}
}
