using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x0200000A RID: 10
	internal abstract class BsonToken
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600006F RID: 111
		public abstract BsonType Type { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000070 RID: 112 RVA: 0x0000429D File Offset: 0x0000249D
		// (set) Token: 0x06000071 RID: 113 RVA: 0x000042A5 File Offset: 0x000024A5
		public BsonToken Parent { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000072 RID: 114 RVA: 0x000042AE File Offset: 0x000024AE
		// (set) Token: 0x06000073 RID: 115 RVA: 0x000042B6 File Offset: 0x000024B6
		public int CalculatedSize { get; set; }
	}
}
