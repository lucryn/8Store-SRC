using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000CE RID: 206
	public class JTokenEqualityComparer : IEqualityComparer<JToken>
	{
		// Token: 0x06000B3A RID: 2874 RVA: 0x0002BE00 File Offset: 0x0002A000
		[NullableContext(2)]
		public bool Equals(JToken x, JToken y)
		{
			return JToken.DeepEquals(x, y);
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x0002BE09 File Offset: 0x0002A009
		[NullableContext(1)]
		public int GetHashCode(JToken obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetDeepHashCode();
		}
	}
}
