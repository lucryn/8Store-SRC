using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020000E4 RID: 228
	[NullableContext(2)]
	[Nullable(0)]
	internal class ScanFilter : PathFilter
	{
		// Token: 0x06000BE7 RID: 3047 RVA: 0x0002F25F File Offset: 0x0002D45F
		public ScanFilter(string name)
		{
			this.Name = name;
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x0002F26E File Offset: 0x0002D46E
		[NullableContext(1)]
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, [Nullable(2)] JsonSelectSettings settings)
		{
			ScanFilter.<ExecuteFilter>d__2 <ExecuteFilter>d__ = new ScanFilter.<ExecuteFilter>d__2(-2);
			<ExecuteFilter>d__.<>4__this = this;
			<ExecuteFilter>d__.<>3__current = current;
			return <ExecuteFilter>d__;
		}

		// Token: 0x0400041E RID: 1054
		internal string Name;
	}
}
