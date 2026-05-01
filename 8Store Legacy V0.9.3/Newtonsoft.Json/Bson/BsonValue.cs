using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x0200000D RID: 13
	internal class BsonValue : BsonToken
	{
		// Token: 0x0600007F RID: 127 RVA: 0x00004377 File Offset: 0x00002577
		public BsonValue(object value, BsonType type)
		{
			this._value = value;
			this._type = type;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000080 RID: 128 RVA: 0x0000438D File Offset: 0x0000258D
		public object Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00004395 File Offset: 0x00002595
		public override BsonType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x0400004A RID: 74
		private readonly object _value;

		// Token: 0x0400004B RID: 75
		private readonly BsonType _type;
	}
}
