using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000088 RID: 136
	[NullableContext(1)]
	public interface IContractResolver
	{
		// Token: 0x06000696 RID: 1686
		JsonContract ResolveContract(Type type);
	}
}
