using System;
using Newtonsoft.Json;

namespace Win81StoreRevival
{
	// Token: 0x0200000F RID: 15
	public class UserAccount
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000064 RID: 100 RVA: 0x0000328B File Offset: 0x0000148B
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00003293 File Offset: 0x00001493
		[JsonProperty("id")]
		public string Id { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000066 RID: 102 RVA: 0x0000329C File Offset: 0x0000149C
		// (set) Token: 0x06000067 RID: 103 RVA: 0x000032A4 File Offset: 0x000014A4
		[JsonProperty("email")]
		public string Email { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000032AD File Offset: 0x000014AD
		// (set) Token: 0x06000069 RID: 105 RVA: 0x000032B5 File Offset: 0x000014B5
		[JsonProperty("username")]
		public string Username { get; set; }
	}
}
