using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020000D9 RID: 217
	[NullableContext(2)]
	[Nullable(0)]
	internal class FieldFilter : PathFilter
	{
		// Token: 0x06000BB3 RID: 2995 RVA: 0x0002D989 File Offset: 0x0002BB89
		public FieldFilter(string name)
		{
			this.Name = name;
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0002D998 File Offset: 0x0002BB98
		[NullableContext(1)]
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, [Nullable(2)] JsonSelectSettings settings)
		{
			FieldFilter.<ExecuteFilter>d__2 <ExecuteFilter>d__ = new FieldFilter.<ExecuteFilter>d__2(-2);
			<ExecuteFilter>d__.<>4__this = this;
			<ExecuteFilter>d__.<>3__current = current;
			<ExecuteFilter>d__.<>3__settings = settings;
			return <ExecuteFilter>d__;
		}

		// Token: 0x04000403 RID: 1027
		internal string Name;
	}
}
