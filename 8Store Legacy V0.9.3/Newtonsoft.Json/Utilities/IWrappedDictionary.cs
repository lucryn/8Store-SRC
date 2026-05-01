using System;
using System.Collections;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000BB RID: 187
	internal interface IWrappedDictionary : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000943 RID: 2371
		object UnderlyingDictionary { get; }
	}
}
