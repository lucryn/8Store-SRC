using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020000A9 RID: 169
	[NullableContext(1)]
	[Nullable(0)]
	internal class SerializationBinderAdapter : ISerializationBinder
	{
		// Token: 0x06000837 RID: 2103 RVA: 0x00022FC8 File Offset: 0x000211C8
		public SerializationBinderAdapter(SerializationBinder serializationBinder)
		{
			this.SerializationBinder = serializationBinder;
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00022FD7 File Offset: 0x000211D7
		public Type BindToType([Nullable(2)] string assemblyName, string typeName)
		{
			return this.SerializationBinder.BindToType(assemblyName, typeName);
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00022FE6 File Offset: 0x000211E6
		[NullableContext(2)]
		public void BindToName([Nullable(1)] Type serializedType, out string assemblyName, out string typeName)
		{
			this.SerializationBinder.BindToName(serializedType, out assemblyName, out typeName);
		}

		// Token: 0x0400031C RID: 796
		public readonly SerializationBinder SerializationBinder;
	}
}
