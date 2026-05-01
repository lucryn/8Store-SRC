using System;

namespace Newtonsoft.Json.Serialization
{
	/// <summary>
	/// Contract details for a <see cref="T:System.Type" /> used by the <see cref="T:Newtonsoft.Json.JsonSerializer" />.
	/// </summary>
	// Token: 0x02000098 RID: 152
	public class JsonLinqContract : JsonContract
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Serialization.JsonLinqContract" /> class.
		/// </summary>
		/// <param name="underlyingType">The underlying type for the contract.</param>
		// Token: 0x0600078D RID: 1933 RVA: 0x0001BD76 File Offset: 0x00019F76
		public JsonLinqContract(Type underlyingType) : base(underlyingType)
		{
			this.ContractType = JsonContractType.Linq;
		}
	}
}
