using System;
using System.Collections;
using System.Collections.Generic;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x0200000B RID: 11
	internal class BsonObject : BsonToken, IEnumerable<BsonProperty>, IEnumerable
	{
		// Token: 0x06000075 RID: 117 RVA: 0x000042C8 File Offset: 0x000024C8
		public void Add(string name, BsonToken token)
		{
			this._children.Add(new BsonProperty
			{
				Name = new BsonString(name, false),
				Value = token
			});
			token.Parent = this;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00004302 File Offset: 0x00002502
		public override BsonType Type
		{
			get
			{
				return BsonType.Object;
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004305 File Offset: 0x00002505
		public IEnumerator<BsonProperty> GetEnumerator()
		{
			return this._children.GetEnumerator();
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004317 File Offset: 0x00002517
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000048 RID: 72
		private readonly List<BsonProperty> _children = new List<BsonProperty>();
	}
}
