using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000081 RID: 129
	public class DefaultNamingStrategy : NamingStrategy
	{
		// Token: 0x06000676 RID: 1654 RVA: 0x0001B231 File Offset: 0x00019431
		[NullableContext(1)]
		protected override string ResolvePropertyName(string name)
		{
			return name;
		}
	}
}
