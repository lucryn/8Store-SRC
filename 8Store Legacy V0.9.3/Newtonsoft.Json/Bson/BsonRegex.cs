using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000010 RID: 16
	internal class BsonRegex : BsonToken
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000043F2 File Offset: 0x000025F2
		// (set) Token: 0x0600008B RID: 139 RVA: 0x000043FA File Offset: 0x000025FA
		public BsonString Pattern { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00004403 File Offset: 0x00002603
		// (set) Token: 0x0600008D RID: 141 RVA: 0x0000440B File Offset: 0x0000260B
		public BsonString Options { get; set; }

		// Token: 0x0600008E RID: 142 RVA: 0x00004414 File Offset: 0x00002614
		public BsonRegex(string pattern, string options)
		{
			this.Pattern = new BsonString(pattern, false);
			this.Options = new BsonString(options, false);
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00004436 File Offset: 0x00002636
		public override BsonType Type
		{
			get
			{
				return BsonType.Regex;
			}
		}
	}
}
