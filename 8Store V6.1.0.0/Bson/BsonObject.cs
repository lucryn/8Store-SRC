using System;
using System.Collections;
using System.Collections.Generic;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000108 RID: 264
	internal class BsonObject : BsonToken, IEnumerable<BsonProperty>, IEnumerable
	{
		// Token: 0x06000D16 RID: 3350 RVA: 0x000341EC File Offset: 0x000323EC
		public void Add(string name, BsonToken token)
		{
			this._children.Add(new BsonProperty
			{
				Name = new BsonString(name, false),
				Value = token
			});
			token.Parent = this;
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000D17 RID: 3351 RVA: 0x00034219 File Offset: 0x00032419
		public override BsonType Type
		{
			get
			{
				return BsonType.Object;
			}
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x0003421C File Offset: 0x0003241C
		public IEnumerator<BsonProperty> GetEnumerator()
		{
			return this._children.GetEnumerator();
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x0003422E File Offset: 0x0003242E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000463 RID: 1123
		private readonly List<BsonProperty> _children = new List<BsonProperty>();
	}
}
