using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x0200010F RID: 271
	internal class BsonRegex : BsonToken
	{
		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000D2F RID: 3375 RVA: 0x00034357 File Offset: 0x00032557
		// (set) Token: 0x06000D30 RID: 3376 RVA: 0x0003435F File Offset: 0x0003255F
		public BsonString Pattern { get; set; }

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000D31 RID: 3377 RVA: 0x00034368 File Offset: 0x00032568
		// (set) Token: 0x06000D32 RID: 3378 RVA: 0x00034370 File Offset: 0x00032570
		public BsonString Options { get; set; }

		// Token: 0x06000D33 RID: 3379 RVA: 0x00034379 File Offset: 0x00032579
		public BsonRegex(string pattern, string options)
		{
			this.Pattern = new BsonString(pattern, false);
			this.Options = new BsonString(options, false);
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000D34 RID: 3380 RVA: 0x0003439B File Offset: 0x0003259B
		public override BsonType Type
		{
			get
			{
				return BsonType.Regex;
			}
		}
	}
}
