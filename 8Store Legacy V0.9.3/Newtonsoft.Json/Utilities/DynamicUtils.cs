using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000C1 RID: 193
	internal static class DynamicUtils
	{
		// Token: 0x0600098E RID: 2446 RVA: 0x000256E8 File Offset: 0x000238E8
		public static IEnumerable<string> GetDynamicMemberNames(this IDynamicMetaObjectProvider dynamicProvider)
		{
			DynamicMetaObject metaObject = dynamicProvider.GetMetaObject(Expression.Constant(dynamicProvider));
			return metaObject.GetDynamicMemberNames();
		}

		// Token: 0x020000C2 RID: 194
		internal static class BinderWrapper
		{
			// Token: 0x0600098F RID: 2447 RVA: 0x00025708 File Offset: 0x00023908
			private static void Init()
			{
				if (!DynamicUtils.BinderWrapper._init)
				{
					if (Type.GetType("Microsoft.CSharp.RuntimeBinder.Binder, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", false) == null)
					{
						throw new InvalidOperationException("Could not resolve type '{0}'. You may need to add a reference to Microsoft.CSharp.dll to work with dynamic types.".FormatWith(CultureInfo.InvariantCulture, "Microsoft.CSharp.RuntimeBinder.Binder, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"));
					}
					int[] values = new int[1];
					DynamicUtils.BinderWrapper._getCSharpArgumentInfoArray = DynamicUtils.BinderWrapper.CreateSharpArgumentInfoArray(values);
					DynamicUtils.BinderWrapper._setCSharpArgumentInfoArray = DynamicUtils.BinderWrapper.CreateSharpArgumentInfoArray(new int[]
					{
						default(int),
						3
					});
					DynamicUtils.BinderWrapper.CreateMemberCalls();
					DynamicUtils.BinderWrapper._init = true;
				}
			}

			// Token: 0x06000990 RID: 2448 RVA: 0x00025778 File Offset: 0x00023978
			private static object CreateSharpArgumentInfoArray(params int[] values)
			{
				Type type = Type.GetType("Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
				Type type2 = Type.GetType("Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfoFlags, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
				Array array = Array.CreateInstance(type, new int[]
				{
					values.Length
				});
				for (int i = 0; i < values.Length; i++)
				{
					MethodInfo method = type.GetMethod("Create", new Type[]
					{
						type2,
						typeof(string)
					});
					MethodBase methodBase = method;
					object obj = null;
					object[] array2 = new object[2];
					array2[0] = 0;
					object obj2 = methodBase.Invoke(obj, array2);
					array.SetValue(obj2, new int[]
					{
						i
					});
				}
				return array;
			}

			// Token: 0x06000991 RID: 2449 RVA: 0x00025824 File Offset: 0x00023A24
			private static void CreateMemberCalls()
			{
				Type type = Type.GetType("Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", true);
				Type type2 = Type.GetType("Microsoft.CSharp.RuntimeBinder.CSharpBinderFlags, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", true);
				Type type3 = Type.GetType("Microsoft.CSharp.RuntimeBinder.Binder, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", true);
				Type type4 = typeof(IEnumerable).MakeGenericType(new Type[]
				{
					type
				});
				MethodInfo method = type3.GetMethod("GetMember", new Type[]
				{
					type2,
					typeof(string),
					typeof(Type),
					type4
				});
				DynamicUtils.BinderWrapper._getMemberCall = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(method);
				MethodInfo method2 = type3.GetMethod("SetMember", new Type[]
				{
					type2,
					typeof(string),
					typeof(Type),
					type4
				});
				DynamicUtils.BinderWrapper._setMemberCall = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(method2);
			}

			// Token: 0x06000992 RID: 2450 RVA: 0x00025910 File Offset: 0x00023B10
			public static CallSiteBinder GetMember(string name, Type context)
			{
				DynamicUtils.BinderWrapper.Init();
				return (CallSiteBinder)DynamicUtils.BinderWrapper._getMemberCall(null, new object[]
				{
					0,
					name,
					context,
					DynamicUtils.BinderWrapper._getCSharpArgumentInfoArray
				});
			}

			// Token: 0x06000993 RID: 2451 RVA: 0x00025954 File Offset: 0x00023B54
			public static CallSiteBinder SetMember(string name, Type context)
			{
				DynamicUtils.BinderWrapper.Init();
				return (CallSiteBinder)DynamicUtils.BinderWrapper._setMemberCall(null, new object[]
				{
					0,
					name,
					context,
					DynamicUtils.BinderWrapper._setCSharpArgumentInfoArray
				});
			}

			// Token: 0x0400039F RID: 927
			public const string CSharpAssemblyName = "Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

			// Token: 0x040003A0 RID: 928
			private const string BinderTypeName = "Microsoft.CSharp.RuntimeBinder.Binder, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

			// Token: 0x040003A1 RID: 929
			private const string CSharpArgumentInfoTypeName = "Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

			// Token: 0x040003A2 RID: 930
			private const string CSharpArgumentInfoFlagsTypeName = "Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfoFlags, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

			// Token: 0x040003A3 RID: 931
			private const string CSharpBinderFlagsTypeName = "Microsoft.CSharp.RuntimeBinder.CSharpBinderFlags, Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";

			// Token: 0x040003A4 RID: 932
			private static object _getCSharpArgumentInfoArray;

			// Token: 0x040003A5 RID: 933
			private static object _setCSharpArgumentInfoArray;

			// Token: 0x040003A6 RID: 934
			private static MethodCall<object, object> _getMemberCall;

			// Token: 0x040003A7 RID: 935
			private static MethodCall<object, object> _setMemberCall;

			// Token: 0x040003A8 RID: 936
			private static bool _init;
		}
	}
}
