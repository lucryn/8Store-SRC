using System;
using System.Collections;
using System.Collections.Generic;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x0200000C RID: 12
	internal class BsonArray : BsonToken, IEnumerable<BsonToken>, IEnumerable
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00004332 File Offset: 0x00002532
		public void Add(BsonToken token)
		{
			this._children.Add(token);
			token.Parent = this;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00004347 File Offset: 0x00002547
		public override BsonType Type
		{
			get
			{
				return BsonType.Array;
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000434A File Offset: 0x0000254A
		public IEnumerator<BsonToken> GetEnumerator()
		{
			return this._children.GetEnumerator();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000435C File Offset: 0x0000255C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000049 RID: 73
		private readonly List<BsonToken> _children = new List<BsonToken>();
	}
}
