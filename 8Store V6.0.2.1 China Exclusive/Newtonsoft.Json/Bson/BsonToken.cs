using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000107 RID: 263
	internal abstract class BsonToken
	{
		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000D10 RID: 3344
		public abstract BsonType Type { get; }

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x000341C2 File Offset: 0x000323C2
		// (set) Token: 0x06000D12 RID: 3346 RVA: 0x000341CA File Offset: 0x000323CA
		public BsonToken Parent { get; set; }

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000D13 RID: 3347 RVA: 0x000341D3 File Offset: 0x000323D3
		// (set) Token: 0x06000D14 RID: 3348 RVA: 0x000341DB File Offset: 0x000323DB
		public int CalculatedSize { get; set; }
	}
}
