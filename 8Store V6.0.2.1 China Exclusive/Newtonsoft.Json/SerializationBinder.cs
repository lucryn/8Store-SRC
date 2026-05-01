using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x02000044 RID: 68
	[Obsolete("SerializationBinder is obsolete. Use ISerializationBinder instead.")]
	public abstract class SerializationBinder
	{
		// Token: 0x06000428 RID: 1064
		[NullableContext(1)]
		public abstract Type BindToType([Nullable(2)] string assemblyName, string typeName);

		// Token: 0x06000429 RID: 1065 RVA: 0x0000FF42 File Offset: 0x0000E142
		[NullableContext(2)]
		public virtual void BindToName([Nullable(1)] Type serializedType, out string assemblyName, out string typeName)
		{
			assemblyName = null;
			typeName = null;
		}
	}
}
