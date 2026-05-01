using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200004F RID: 79
	internal interface IWrappedCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600045D RID: 1117
		[Nullable(1)]
		object UnderlyingCollection { [NullableContext(1)] get; }
	}
}
