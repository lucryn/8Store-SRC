using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000011 RID: 17
	internal class BsonProperty
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000090 RID: 144 RVA: 0x0000443A File Offset: 0x0000263A
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00004442 File Offset: 0x00002642
		public BsonString Name { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000092 RID: 146 RVA: 0x0000444B File Offset: 0x0000264B
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00004453 File Offset: 0x00002653
		public BsonToken Value { get; set; }
	}
}
