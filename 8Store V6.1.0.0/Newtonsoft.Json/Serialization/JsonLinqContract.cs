using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000097 RID: 151
	public class JsonLinqContract : JsonContract
	{
		// Token: 0x060006FF RID: 1791 RVA: 0x0001C795 File Offset: 0x0001A995
		[NullableContext(1)]
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		public JsonLinqContract(Type underlyingType) : base(underlyingType)
		{
			this.ContractType = JsonContractType.Linq;
		}
	}
}
