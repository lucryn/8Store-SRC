using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x0200010C RID: 268
	internal class BsonBoolean : BsonValue
	{
		// Token: 0x06000D26 RID: 3366 RVA: 0x000342E4 File Offset: 0x000324E4
		private BsonBoolean(bool value) : base(value, BsonType.Boolean)
		{
		}

		// Token: 0x0400046A RID: 1130
		public static readonly BsonBoolean False = new BsonBoolean(false);

		// Token: 0x0400046B RID: 1131
		public static readonly BsonBoolean True = new BsonBoolean(true);
	}
}
