using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020000D7 RID: 215
	[NullableContext(1)]
	[Nullable(0)]
	internal class ArrayMultipleIndexFilter : PathFilter
	{
		// Token: 0x06000BA8 RID: 2984 RVA: 0x0002D8F5 File Offset: 0x0002BAF5
		public ArrayMultipleIndexFilter(List<int> indexes)
		{
			this.Indexes = indexes;
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0002D904 File Offset: 0x0002BB04
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, [Nullable(2)] JsonSelectSettings settings)
		{
			ArrayMultipleIndexFilter.<ExecuteFilter>d__2 <ExecuteFilter>d__ = new ArrayMultipleIndexFilter.<ExecuteFilter>d__2(-2);
			<ExecuteFilter>d__.<>4__this = this;
			<ExecuteFilter>d__.<>3__current = current;
			<ExecuteFilter>d__.<>3__settings = settings;
			return <ExecuteFilter>d__;
		}

		// Token: 0x040003FF RID: 1023
		internal List<int> Indexes;
	}
}
