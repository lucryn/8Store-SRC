using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020000E3 RID: 227
	[NullableContext(1)]
	[Nullable(0)]
	internal class RootFilter : PathFilter
	{
		// Token: 0x06000BE4 RID: 3044 RVA: 0x0002F23F File Offset: 0x0002D43F
		private RootFilter()
		{
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x0002F247 File Offset: 0x0002D447
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, [Nullable(2)] JsonSelectSettings settings)
		{
			return new JToken[]
			{
				root
			};
		}

		// Token: 0x0400041D RID: 1053
		public static readonly RootFilter Instance = new RootFilter();
	}
}
