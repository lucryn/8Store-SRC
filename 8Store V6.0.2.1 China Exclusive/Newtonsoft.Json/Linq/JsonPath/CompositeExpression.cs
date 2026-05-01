using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020000DF RID: 223
	[NullableContext(1)]
	[Nullable(0)]
	internal class CompositeExpression : QueryExpression
	{
		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x0002ECA3 File Offset: 0x0002CEA3
		// (set) Token: 0x06000BD6 RID: 3030 RVA: 0x0002ECAB File Offset: 0x0002CEAB
		public List<QueryExpression> Expressions { get; set; }

		// Token: 0x06000BD7 RID: 3031 RVA: 0x0002ECB4 File Offset: 0x0002CEB4
		public CompositeExpression(QueryOperator @operator) : base(@operator)
		{
			this.Expressions = new List<QueryExpression>();
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x0002ECC8 File Offset: 0x0002CEC8
		public override bool IsMatch(JToken root, JToken t, [Nullable(2)] JsonSelectSettings settings)
		{
			QueryOperator @operator = this.Operator;
			if (@operator == QueryOperator.And)
			{
				using (List<QueryExpression>.Enumerator enumerator = this.Expressions.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!enumerator.Current.IsMatch(root, t, settings))
						{
							return false;
						}
					}
				}
				return true;
			}
			if (@operator != QueryOperator.Or)
			{
				throw new ArgumentOutOfRangeException();
			}
			using (List<QueryExpression>.Enumerator enumerator = this.Expressions.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsMatch(root, t, settings))
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
