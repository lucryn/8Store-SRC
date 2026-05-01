using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020000DE RID: 222
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class QueryExpression
	{
		// Token: 0x06000BD2 RID: 3026 RVA: 0x0002EC89 File Offset: 0x0002CE89
		public QueryExpression(QueryOperator @operator)
		{
			this.Operator = @operator;
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x0002EC98 File Offset: 0x0002CE98
		public bool IsMatch(JToken root, JToken t)
		{
			return this.IsMatch(root, t, null);
		}

		// Token: 0x06000BD4 RID: 3028
		public abstract bool IsMatch(JToken root, JToken t, [Nullable(2)] JsonSelectSettings settings);

		// Token: 0x04000417 RID: 1047
		internal QueryOperator Operator;
	}
}
