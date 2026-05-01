using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200007D RID: 125
	[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
	[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
	internal static class CachedAttributeGetter<T> where T : Attribute
	{
		// Token: 0x06000633 RID: 1587 RVA: 0x000194A1 File Offset: 0x000176A1
		[NullableContext(1)]
		[return: Nullable(2)]
		public static T GetAttribute(object type)
		{
			return CachedAttributeGetter<T>.TypeAttributeCache.Get(type);
		}

		// Token: 0x0400026D RID: 621
		[Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		private static readonly ThreadSafeStore<object, T> TypeAttributeCache = new ThreadSafeStore<object, T>(new Func<object, T>(JsonTypeReflector.GetAttribute<T>));
	}
}
