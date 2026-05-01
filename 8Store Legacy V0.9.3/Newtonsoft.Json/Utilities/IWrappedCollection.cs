using System;
using System.Collections;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000B1 RID: 177
	internal interface IWrappedCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060008EE RID: 2286
		object UnderlyingCollection { get; }
	}
}
