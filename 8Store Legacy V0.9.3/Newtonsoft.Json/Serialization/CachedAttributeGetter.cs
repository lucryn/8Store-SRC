using System;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000081 RID: 129
	internal static class CachedAttributeGetter<T> where T : Attribute
	{
		// Token: 0x060006C7 RID: 1735 RVA: 0x00018FBB File Offset: 0x000171BB
		public static T GetAttribute(object type)
		{
			return CachedAttributeGetter<T>.TypeAttributeCache.Get(type);
		}

		// Token: 0x0400026B RID: 619
		private static readonly ThreadSafeStore<object, T> TypeAttributeCache = new ThreadSafeStore<object, T>(new Func<object, T>(JsonTypeReflector.GetAttribute<T>));
	}
}
