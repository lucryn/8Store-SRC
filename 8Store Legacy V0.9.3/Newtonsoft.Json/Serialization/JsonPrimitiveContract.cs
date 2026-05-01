using System;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	/// <summary>
	/// Contract details for a <see cref="T:System.Type" /> used by the <see cref="T:Newtonsoft.Json.JsonSerializer" />.
	/// </summary>
	// Token: 0x0200009A RID: 154
	public class JsonPrimitiveContract : JsonContract
	{
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x0001BF22 File Offset: 0x0001A122
		// (set) Token: 0x0600079F RID: 1951 RVA: 0x0001BF2A File Offset: 0x0001A12A
		internal PrimitiveTypeCode TypeCode { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Serialization.JsonPrimitiveContract" /> class.
		/// </summary>
		/// <param name="underlyingType">The underlying type for the contract.</param>
		// Token: 0x060007A0 RID: 1952 RVA: 0x0001BF33 File Offset: 0x0001A133
		public JsonPrimitiveContract(Type underlyingType) : base(underlyingType)
		{
			this.ContractType = JsonContractType.Primitive;
			this.TypeCode = ConvertUtils.GetTypeCode(underlyingType);
			this.IsReadOnlyOrFixedSize = true;
		}
	}
}
