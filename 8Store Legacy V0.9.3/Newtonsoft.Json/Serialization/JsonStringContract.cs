using System;

namespace Newtonsoft.Json.Serialization
{
	/// <summary>
	/// Contract details for a <see cref="T:System.Type" /> used by the <see cref="T:Newtonsoft.Json.JsonSerializer" />.
	/// </summary>
	// Token: 0x020000A3 RID: 163
	public class JsonStringContract : JsonPrimitiveContract
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Serialization.JsonStringContract" /> class.
		/// </summary>
		/// <param name="underlyingType">The underlying type for the contract.</param>
		// Token: 0x0600087A RID: 2170 RVA: 0x00020B25 File Offset: 0x0001ED25
		public JsonStringContract(Type underlyingType) : base(underlyingType)
		{
			this.ContractType = JsonContractType.String;
		}
	}
}
