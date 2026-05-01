using System;

namespace Win81StoreRevival
{
	// Token: 0x02000012 RID: 18
	public class ReviewResult
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000087 RID: 135 RVA: 0x0000339B File Offset: 0x0000159B
		// (set) Token: 0x06000088 RID: 136 RVA: 0x000033A3 File Offset: 0x000015A3
		public bool Success { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000089 RID: 137 RVA: 0x000033AC File Offset: 0x000015AC
		// (set) Token: 0x0600008A RID: 138 RVA: 0x000033B4 File Offset: 0x000015B4
		public string Error { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000033BD File Offset: 0x000015BD
		// (set) Token: 0x0600008C RID: 140 RVA: 0x000033C5 File Offset: 0x000015C5
		public Review Review { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000033CE File Offset: 0x000015CE
		// (set) Token: 0x0600008E RID: 142 RVA: 0x000033D6 File Offset: 0x000015D6
		public RatingSummary Ratings { get; set; }
	}
}
