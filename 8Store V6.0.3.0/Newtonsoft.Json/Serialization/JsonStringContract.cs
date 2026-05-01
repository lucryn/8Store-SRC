using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020000A0 RID: 160
	public class JsonStringContract : JsonPrimitiveContract
	{
		// Token: 0x06000801 RID: 2049 RVA: 0x000226E5 File Offset: 0x000208E5
		[NullableContext(1)]
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		public JsonStringContract(Type underlyingType) : base(underlyingType)
		{
			this.ContractType = JsonContractType.String;
		}
	}
}
