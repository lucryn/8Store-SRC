using System;
using System.Collections;
using System.Collections.Generic;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000109 RID: 265
	internal class BsonArray : BsonToken, IEnumerable<BsonToken>, IEnumerable
	{
		// Token: 0x06000D1B RID: 3355 RVA: 0x00034249 File Offset: 0x00032449
		public void Add(BsonToken token)
		{
			this._children.Add(token);
			token.Parent = this;
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x0003425E File Offset: 0x0003245E
		public override BsonType Type
		{
			get
			{
				return BsonType.Array;
			}
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x00034261 File Offset: 0x00032461
		public IEnumerator<BsonToken> GetEnumerator()
		{
			return this._children.GetEnumerator();
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x00034273 File Offset: 0x00032473
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000464 RID: 1124
		private readonly List<BsonToken> _children = new List<BsonToken>();
	}
}
