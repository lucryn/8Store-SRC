using System;
using System.Globalization;
using System.Reflection;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000C9 RID: 201
	internal abstract class ReflectionDelegateFactory
	{
		// Token: 0x060009A8 RID: 2472 RVA: 0x00025E5C File Offset: 0x0002405C
		public Func<T, object> CreateGet<T>(MemberInfo memberInfo)
		{
			PropertyInfo propertyInfo = memberInfo as PropertyInfo;
			if (propertyInfo != null)
			{
				return this.CreateGet<T>(propertyInfo);
			}
			FieldInfo fieldInfo = memberInfo as FieldInfo;
			if (fieldInfo != null)
			{
				return this.CreateGet<T>(fieldInfo);
			}
			throw new Exception("Could not create getter for {0}.".FormatWith(CultureInfo.InvariantCulture, memberInfo));
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x00025EA4 File Offset: 0x000240A4
		public Action<T, object> CreateSet<T>(MemberInfo memberInfo)
		{
			PropertyInfo propertyInfo = memberInfo as PropertyInfo;
			if (propertyInfo != null)
			{
				return this.CreateSet<T>(propertyInfo);
			}
			FieldInfo fieldInfo = memberInfo as FieldInfo;
			if (fieldInfo != null)
			{
				return this.CreateSet<T>(fieldInfo);
			}
			throw new Exception("Could not create setter for {0}.".FormatWith(CultureInfo.InvariantCulture, memberInfo));
		}

		// Token: 0x060009AA RID: 2474
		public abstract MethodCall<T, object> CreateMethodCall<T>(MethodBase method);

		// Token: 0x060009AB RID: 2475
		public abstract Func<T> CreateDefaultConstructor<T>(Type type);

		// Token: 0x060009AC RID: 2476
		public abstract Func<T, object> CreateGet<T>(PropertyInfo propertyInfo);

		// Token: 0x060009AD RID: 2477
		public abstract Func<T, object> CreateGet<T>(FieldInfo fieldInfo);

		// Token: 0x060009AE RID: 2478
		public abstract Action<T, object> CreateSet<T>(FieldInfo fieldInfo);

		// Token: 0x060009AF RID: 2479
		public abstract Action<T, object> CreateSet<T>(PropertyInfo propertyInfo);
	}
}
