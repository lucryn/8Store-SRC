using System;
using System.Reflection;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000CC RID: 204
	internal class LateBoundReflectionDelegateFactory : ReflectionDelegateFactory
	{
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x000268C8 File Offset: 0x00024AC8
		internal static ReflectionDelegateFactory Instance
		{
			get
			{
				return LateBoundReflectionDelegateFactory._instance;
			}
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x000268FC File Offset: 0x00024AFC
		public override MethodCall<T, object> CreateMethodCall<T>(MethodBase method)
		{
			ValidationUtils.ArgumentNotNull(method, "method");
			ConstructorInfo c = method as ConstructorInfo;
			if (c != null)
			{
				return (T o, object[] a) => c.Invoke(a);
			}
			return (T o, object[] a) => method.Invoke(o, a);
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x00026988 File Offset: 0x00024B88
		public override Func<T> CreateDefaultConstructor<T>(Type type)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			if (type.IsValueType())
			{
				return () => (T)((object)Activator.CreateInstance(type));
			}
			ConstructorInfo constructorInfo = ReflectionUtils.GetDefaultConstructor(type, true);
			return () => (T)((object)constructorInfo.Invoke(null));
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x00026A08 File Offset: 0x00024C08
		public override Func<T, object> CreateGet<T>(PropertyInfo propertyInfo)
		{
			ValidationUtils.ArgumentNotNull(propertyInfo, "propertyInfo");
			return (T o) => propertyInfo.GetValue(o, null);
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x00026A5C File Offset: 0x00024C5C
		public override Func<T, object> CreateGet<T>(FieldInfo fieldInfo)
		{
			ValidationUtils.ArgumentNotNull(fieldInfo, "fieldInfo");
			return (T o) => fieldInfo.GetValue(o);
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x00026AB0 File Offset: 0x00024CB0
		public override Action<T, object> CreateSet<T>(FieldInfo fieldInfo)
		{
			ValidationUtils.ArgumentNotNull(fieldInfo, "fieldInfo");
			return delegate(T o, object v)
			{
				fieldInfo.SetValue(o, v);
			};
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x00026B04 File Offset: 0x00024D04
		public override Action<T, object> CreateSet<T>(PropertyInfo propertyInfo)
		{
			ValidationUtils.ArgumentNotNull(propertyInfo, "propertyInfo");
			return delegate(T o, object v)
			{
				propertyInfo.SetValue(o, v, null);
			};
		}

		// Token: 0x040003B5 RID: 949
		private static readonly LateBoundReflectionDelegateFactory _instance = new LateBoundReflectionDelegateFactory();
	}
}
