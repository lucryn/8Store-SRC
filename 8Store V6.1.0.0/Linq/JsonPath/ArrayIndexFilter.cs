using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020000D6 RID: 214
	internal class ArrayIndexFilter : PathFilter
	{
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x0002D8BE File Offset: 0x0002BABE
		// (set) Token: 0x06000BA5 RID: 2981 RVA: 0x0002D8C6 File Offset: 0x0002BAC6
		public int? Index { get; set; }

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0002D8CF File Offset: 0x0002BACF
		[NullableContext(1)]
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, [Nullable(2)] JsonSelectSettings settings)
		{
			ArrayIndexFilter.<ExecuteFilter>d__4 <ExecuteFilter>d__ = new ArrayIndexFilter.<ExecuteFilter>d__4(-2);
			<ExecuteFilter>d__.<>4__this = this;
			<ExecuteFilter>d__.<>3__current = current;
			<ExecuteFilter>d__.<>3__settings = settings;
			return <ExecuteFilter>d__;
		}
	}
}
