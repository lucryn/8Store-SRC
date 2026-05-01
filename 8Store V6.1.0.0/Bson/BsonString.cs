using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x0200010D RID: 269
	internal class BsonString : BsonValue
	{
		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000D28 RID: 3368 RVA: 0x0003430B File Offset: 0x0003250B
		// (set) Token: 0x06000D29 RID: 3369 RVA: 0x00034313 File Offset: 0x00032513
		public int ByteCount { get; set; }

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000D2A RID: 3370 RVA: 0x0003431C File Offset: 0x0003251C
		public bool IncludeLength { get; }

		// Token: 0x06000D2B RID: 3371 RVA: 0x00034324 File Offset: 0x00032524
		public BsonString(object value, bool includeLength) : base(value, BsonType.String)
		{
			this.IncludeLength = includeLength;
		}
	}
}
