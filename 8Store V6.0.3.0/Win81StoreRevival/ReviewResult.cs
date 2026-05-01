using System;

namespace Win81StoreRevival
{
	// Token: 0x02000011 RID: 17
	public class ReviewResult
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000081 RID: 129 RVA: 0x000037F5 File Offset: 0x000019F5
		// (set) Token: 0x06000082 RID: 130 RVA: 0x000037FD File Offset: 0x000019FD
		public bool Success { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003806 File Offset: 0x00001A06
		// (set) Token: 0x06000084 RID: 132 RVA: 0x0000380E File Offset: 0x00001A0E
		public string Error { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003817 File Offset: 0x00001A17
		// (set) Token: 0x06000086 RID: 134 RVA: 0x0000381F File Offset: 0x00001A1F
		public Review Review { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003828 File Offset: 0x00001A28
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00003830 File Offset: 0x00001A30
		public RatingSummary Ratings { get; set; }
	}
}
