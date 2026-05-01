using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Win81StoreRevival
{
	// Token: 0x0200000F RID: 15
	public class ApiResponse<T>
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000069 RID: 105 RVA: 0x0000373A File Offset: 0x0000193A
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00003742 File Offset: 0x00001942
		[JsonProperty("success")]
		public bool Success { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600006B RID: 107 RVA: 0x0000374B File Offset: 0x0000194B
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00003753 File Offset: 0x00001953
		[JsonProperty("error")]
		public string Error { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600006D RID: 109 RVA: 0x0000375C File Offset: 0x0000195C
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00003764 File Offset: 0x00001964
		[JsonProperty("user")]
		public T User { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600006F RID: 111 RVA: 0x0000376D File Offset: 0x0000196D
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00003775 File Offset: 0x00001975
		[JsonProperty("session")]
		public SessionData Session { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000071 RID: 113 RVA: 0x0000377E File Offset: 0x0000197E
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00003786 File Offset: 0x00001986
		[JsonProperty("reviews")]
		public List<Review> Reviews { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000073 RID: 115 RVA: 0x0000378F File Offset: 0x0000198F
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00003797 File Offset: 0x00001997
		[JsonProperty("review")]
		public Review Review { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000075 RID: 117 RVA: 0x000037A0 File Offset: 0x000019A0
		// (set) Token: 0x06000076 RID: 118 RVA: 0x000037A8 File Offset: 0x000019A8
		[JsonProperty("message")]
		public string Message { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000077 RID: 119 RVA: 0x000037B1 File Offset: 0x000019B1
		// (set) Token: 0x06000078 RID: 120 RVA: 0x000037B9 File Offset: 0x000019B9
		[JsonProperty("stats")]
		public List<AppReviewStatsResponse> Stats { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000037C2 File Offset: 0x000019C2
		// (set) Token: 0x0600007A RID: 122 RVA: 0x000037CA File Offset: 0x000019CA
		[JsonProperty("ratings")]
		public RatingSummary Ratings { get; set; }
	}
}
