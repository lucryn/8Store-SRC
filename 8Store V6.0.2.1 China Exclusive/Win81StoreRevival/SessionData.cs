using System;
using Newtonsoft.Json;

namespace Win81StoreRevival
{
	// Token: 0x02000010 RID: 16
	public class SessionData
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600007C RID: 124 RVA: 0x000037D3 File Offset: 0x000019D3
		// (set) Token: 0x0600007D RID: 125 RVA: 0x000037DB File Offset: 0x000019DB
		[JsonProperty("access_token")]
		public string AccessToken { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600007E RID: 126 RVA: 0x000037E4 File Offset: 0x000019E4
		// (set) Token: 0x0600007F RID: 127 RVA: 0x000037EC File Offset: 0x000019EC
		[JsonProperty("refresh_token")]
		public string RefreshToken { get; set; }
	}
}
