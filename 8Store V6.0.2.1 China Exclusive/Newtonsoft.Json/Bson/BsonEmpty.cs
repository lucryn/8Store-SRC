using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x0200010A RID: 266
	internal class BsonEmpty : BsonToken
	{
		// Token: 0x06000D20 RID: 3360 RVA: 0x0003428E File Offset: 0x0003248E
		private BsonEmpty(BsonType type)
		{
			this.Type = type;
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000D21 RID: 3361 RVA: 0x0003429D File Offset: 0x0003249D
		public override BsonType Type { get; }

		// Token: 0x04000465 RID: 1125
		public static readonly BsonToken Null = new BsonEmpty(BsonType.Null);

		// Token: 0x04000466 RID: 1126
		public static readonly BsonToken Undefined = new BsonEmpty(BsonType.Undefined);
	}
}
