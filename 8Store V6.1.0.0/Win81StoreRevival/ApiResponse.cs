using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Win81StoreRevival
{
	// Token: 0x02000010 RID: 16
	public class ApiResponse<T>
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000032BE File Offset: 0x000014BE
		// (set) Token: 0x0600006C RID: 108 RVA: 0x000032C6 File Offset: 0x000014C6
		[JsonProperty("success")]
		public bool Success { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000032CF File Offset: 0x000014CF
		// (set) Token: 0x0600006E RID: 110 RVA: 0x000032D7 File Offset: 0x000014D7
		[JsonProperty("error")]
		public string Error { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000032E0 File Offset: 0x000014E0
		// (set) Token: 0x06000070 RID: 112 RVA: 0x000032E8 File Offset: 0x000014E8
		[JsonProperty("user")]
		public T User { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000071 RID: 113 RVA: 0x000032F1 File Offset: 0x000014F1
		// (set) Token: 0x06000072 RID: 114 RVA: 0x000032F9 File Offset: 0x000014F9
		[JsonProperty("session")]
		public SessionData Session { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00003302 File Offset: 0x00001502
		// (set) Token: 0x06000074 RID: 116 RVA: 0x0000330A File Offset: 0x0000150A
		[JsonProperty("reviews")]
		public List<Review> Reviews { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00003313 File Offset: 0x00001513
		// (set) Token: 0x06000076 RID: 118 RVA: 0x0000331B File Offset: 0x0000151B
		[JsonProperty("review")]
		public Review Review { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003324 File Offset: 0x00001524
		// (set) Token: 0x06000078 RID: 120 RVA: 0x0000332C File Offset: 0x0000152C
		[JsonProperty("message")]
		public string Message { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003335 File Offset: 0x00001535
		// (set) Token: 0x0600007A RID: 122 RVA: 0x0000333D File Offset: 0x0000153D
		[JsonProperty("stats")]
		public List<AppReviewStatsResponse> Stats { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00003346 File Offset: 0x00001546
		// (set) Token: 0x0600007C RID: 124 RVA: 0x0000334E File Offset: 0x0000154E
		[JsonProperty("ratings")]
		public RatingSummary Ratings { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003357 File Offset: 0x00001557
		// (set) Token: 0x0600007E RID: 126 RVA: 0x0000335F File Offset: 0x0000155F
		[JsonProperty("applications")]
		public List<StoreApp> Applications { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00003368 File Offset: 0x00001568
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00003370 File Offset: 0x00001570
		[JsonProperty("submissions")]
		public List<StoreApp> Submissions { get; set; }
	}
}
