using System;
using Newtonsoft.Json;

namespace Win81StoreRevival
{
	// Token: 0x02000011 RID: 17
	public class SessionData
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00003379 File Offset: 0x00001579
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00003381 File Offset: 0x00001581
		[JsonProperty("access_token")]
		public string AccessToken { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000084 RID: 132 RVA: 0x0000338A File Offset: 0x0000158A
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00003392 File Offset: 0x00001592
		[JsonProperty("refresh_token")]
		public string RefreshToken { get; set; }
	}
}
