using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000058 RID: 88
	internal interface IWrappedDictionary : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060004B7 RID: 1207
		[Nullable(1)]
		object UnderlyingDictionary { [NullableContext(1)] get; }
	}
}
