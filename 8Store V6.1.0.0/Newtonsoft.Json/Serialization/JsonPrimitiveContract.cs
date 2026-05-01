using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000099 RID: 153
	[NullableContext(1)]
	[Nullable(0)]
	public class JsonPrimitiveContract : JsonContract
	{
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x0001C98D File Offset: 0x0001AB8D
		// (set) Token: 0x06000719 RID: 1817 RVA: 0x0001C995 File Offset: 0x0001AB95
		internal PrimitiveTypeCode TypeCode { get; set; }

		// Token: 0x0600071A RID: 1818 RVA: 0x0001C9A0 File Offset: 0x0001ABA0
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		public JsonPrimitiveContract(Type underlyingType) : base(underlyingType)
		{
			this.ContractType = JsonContractType.Primitive;
			this.TypeCode = ConvertUtils.GetTypeCode(underlyingType);
			this.IsReadOnlyOrFixedSize = true;
			ReadType internalReadType;
			if (JsonPrimitiveContract.ReadTypeMap.TryGetValue(this.NonNullableUnderlyingType, ref internalReadType))
			{
				this.InternalReadType = internalReadType;
			}
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0001C9EC File Offset: 0x0001ABEC
		// Note: this type is marked as 'beforefieldinit'.
		static JsonPrimitiveContract()
		{
			Dictionary<Type, ReadType> dictionary = new Dictionary<Type, ReadType>();
			Type typeFromHandle = typeof(byte[]);
			dictionary[typeFromHandle] = ReadType.ReadAsBytes;
			Type typeFromHandle2 = typeof(byte);
			dictionary[typeFromHandle2] = ReadType.ReadAsInt32;
			Type typeFromHandle3 = typeof(short);
			dictionary[typeFromHandle3] = ReadType.ReadAsInt32;
			Type typeFromHandle4 = typeof(int);
			dictionary[typeFromHandle4] = ReadType.ReadAsInt32;
			Type typeFromHandle5 = typeof(decimal);
			dictionary[typeFromHandle5] = ReadType.ReadAsDecimal;
			Type typeFromHandle6 = typeof(bool);
			dictionary[typeFromHandle6] = ReadType.ReadAsBoolean;
			Type typeFromHandle7 = typeof(string);
			dictionary[typeFromHandle7] = ReadType.ReadAsString;
			Type typeFromHandle8 = typeof(DateTime);
			dictionary[typeFromHandle8] = ReadType.ReadAsDateTime;
			Type typeFromHandle9 = typeof(DateTimeOffset);
			dictionary[typeFromHandle9] = ReadType.ReadAsDateTimeOffset;
			Type typeFromHandle10 = typeof(float);
			dictionary[typeFromHandle10] = ReadType.ReadAsDouble;
			Type typeFromHandle11 = typeof(double);
			dictionary[typeFromHandle11] = ReadType.ReadAsDouble;
			Type typeFromHandle12 = typeof(long);
			dictionary[typeFromHandle12] = ReadType.ReadAsInt64;
			JsonPrimitiveContract.ReadTypeMap = dictionary;
		}

		// Token: 0x040002DB RID: 731
		private static readonly Dictionary<Type, ReadType> ReadTypeMap;
	}
}
