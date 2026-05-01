using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200007F RID: 127
	[NullableContext(1)]
	[Nullable(0)]
	[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
	[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
	public class CamelCasePropertyNamesContractResolver : DefaultContractResolver
	{
		// Token: 0x06000639 RID: 1593 RVA: 0x000194FD File Offset: 0x000176FD
		public CamelCasePropertyNamesContractResolver()
		{
			base.NamingStrategy = new CamelCaseNamingStrategy
			{
				ProcessDictionaryKeys = true,
				OverrideSpecifiedNames = true
			};
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00019520 File Offset: 0x00017720
		public override JsonContract ResolveContract(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			StructMultiKey<Type, Type> structMultiKey = new StructMultiKey<Type, Type>(base.GetType(), type);
			Dictionary<StructMultiKey<Type, Type>, JsonContract> contractCache = CamelCasePropertyNamesContractResolver._contractCache;
			JsonContract jsonContract;
			if (contractCache == null || !contractCache.TryGetValue(structMultiKey, ref jsonContract))
			{
				jsonContract = this.CreateContract(type);
				object typeContractCacheLock = CamelCasePropertyNamesContractResolver.TypeContractCacheLock;
				lock (typeContractCacheLock)
				{
					contractCache = CamelCasePropertyNamesContractResolver._contractCache;
					Dictionary<StructMultiKey<Type, Type>, JsonContract> dictionary = (contractCache != null) ? new Dictionary<StructMultiKey<Type, Type>, JsonContract>(contractCache) : new Dictionary<StructMultiKey<Type, Type>, JsonContract>();
					dictionary[structMultiKey] = jsonContract;
					CamelCasePropertyNamesContractResolver._contractCache = dictionary;
				}
			}
			return jsonContract;
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x000195B8 File Offset: 0x000177B8
		internal override DefaultJsonNameTable GetNameTable()
		{
			return CamelCasePropertyNamesContractResolver.NameTable;
		}

		// Token: 0x0400026E RID: 622
		private static readonly object TypeContractCacheLock = new object();

		// Token: 0x0400026F RID: 623
		private static readonly DefaultJsonNameTable NameTable = new DefaultJsonNameTable();

		// Token: 0x04000270 RID: 624
		[Nullable(new byte[]
		{
			2,
			0,
			1,
			1,
			1
		})]
		private static Dictionary<StructMultiKey<Type, Type>, JsonContract> _contractCache;
	}
}
