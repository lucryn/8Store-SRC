using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020000E5 RID: 229
	[NullableContext(1)]
	[Nullable(0)]
	internal class ScanMultipleFilter : PathFilter
	{
		// Token: 0x06000BE9 RID: 3049 RVA: 0x0002F285 File Offset: 0x0002D485
		public ScanMultipleFilter(List<string> names)
		{
			this._names = names;
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x0002F294 File Offset: 0x0002D494
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, [Nullable(2)] JsonSelectSettings settings)
		{
			ScanMultipleFilter.<ExecuteFilter>d__2 <ExecuteFilter>d__ = new ScanMultipleFilter.<ExecuteFilter>d__2(-2);
			<ExecuteFilter>d__.<>4__this = this;
			<ExecuteFilter>d__.<>3__current = current;
			return <ExecuteFilter>d__;
		}

		// Token: 0x0400041F RID: 1055
		private List<string> _names;
	}
}
