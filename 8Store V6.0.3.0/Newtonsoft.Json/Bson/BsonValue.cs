using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x0200010B RID: 267
	internal class BsonValue : BsonToken
	{
		// Token: 0x06000D23 RID: 3363 RVA: 0x000342BE File Offset: 0x000324BE
		public BsonValue(object value, BsonType type)
		{
			this._value = value;
			this._type = type;
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000D24 RID: 3364 RVA: 0x000342D4 File Offset: 0x000324D4
		public object Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000D25 RID: 3365 RVA: 0x000342DC File Offset: 0x000324DC
		public override BsonType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x04000468 RID: 1128
		private readonly object _value;

		// Token: 0x04000469 RID: 1129
		private readonly BsonType _type;
	}
}
