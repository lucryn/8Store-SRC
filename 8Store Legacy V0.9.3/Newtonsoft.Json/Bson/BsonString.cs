using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x0200000E RID: 14
	internal class BsonString : BsonValue
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000082 RID: 130 RVA: 0x0000439D File Offset: 0x0000259D
		// (set) Token: 0x06000083 RID: 131 RVA: 0x000043A5 File Offset: 0x000025A5
		public int ByteCount { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000084 RID: 132 RVA: 0x000043AE File Offset: 0x000025AE
		// (set) Token: 0x06000085 RID: 133 RVA: 0x000043B6 File Offset: 0x000025B6
		public bool IncludeLength { get; set; }

		// Token: 0x06000086 RID: 134 RVA: 0x000043BF File Offset: 0x000025BF
		public BsonString(object value, bool includeLength) : base(value, BsonType.String)
		{
			this.IncludeLength = includeLength;
		}
	}
}
