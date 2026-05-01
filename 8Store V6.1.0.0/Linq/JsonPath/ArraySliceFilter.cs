using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020000D8 RID: 216
	internal class ArraySliceFilter : PathFilter
	{
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x0002D922 File Offset: 0x0002BB22
		// (set) Token: 0x06000BAB RID: 2987 RVA: 0x0002D92A File Offset: 0x0002BB2A
		public int? Start { get; set; }

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x0002D933 File Offset: 0x0002BB33
		// (set) Token: 0x06000BAD RID: 2989 RVA: 0x0002D93B File Offset: 0x0002BB3B
		public int? End { get; set; }

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x0002D944 File Offset: 0x0002BB44
		// (set) Token: 0x06000BAF RID: 2991 RVA: 0x0002D94C File Offset: 0x0002BB4C
		public int? Step { get; set; }

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0002D955 File Offset: 0x0002BB55
		[NullableContext(1)]
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, [Nullable(2)] JsonSelectSettings settings)
		{
			ArraySliceFilter.<ExecuteFilter>d__12 <ExecuteFilter>d__ = new ArraySliceFilter.<ExecuteFilter>d__12(-2);
			<ExecuteFilter>d__.<>4__this = this;
			<ExecuteFilter>d__.<>3__current = current;
			<ExecuteFilter>d__.<>3__settings = settings;
			return <ExecuteFilter>d__;
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0002D973 File Offset: 0x0002BB73
		private bool IsValid(int index, int stopIndex, bool positiveStep)
		{
			if (positiveStep)
			{
				return index < stopIndex;
			}
			return index > stopIndex;
		}
	}
}
