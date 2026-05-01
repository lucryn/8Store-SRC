using System;
using Newtonsoft.Json;

namespace Win81StoreRevival
{
	// Token: 0x0200000E RID: 14
	public class UserAccount
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00003707 File Offset: 0x00001907
		// (set) Token: 0x06000063 RID: 99 RVA: 0x0000370F File Offset: 0x0000190F
		[JsonProperty("id")]
		public string Id { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003718 File Offset: 0x00001918
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00003720 File Offset: 0x00001920
		[JsonProperty("email")]
		public string Email { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00003729 File Offset: 0x00001929
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00003731 File Offset: 0x00001931
		[JsonProperty("username")]
		public string Username { get; set; }
	}
}
