using System;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000B4 RID: 180
	internal class TypeInformation
	{
		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x00022303 File Offset: 0x00020503
		// (set) Token: 0x0600090A RID: 2314 RVA: 0x0002230B File Offset: 0x0002050B
		public Type Type { get; set; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x00022314 File Offset: 0x00020514
		// (set) Token: 0x0600090C RID: 2316 RVA: 0x0002231C File Offset: 0x0002051C
		public PrimitiveTypeCode TypeCode { get; set; }
	}
}
