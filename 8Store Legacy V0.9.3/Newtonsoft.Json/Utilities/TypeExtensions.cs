using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000D8 RID: 216
	internal static class TypeExtensions
	{
		// Token: 0x06000A2D RID: 2605 RVA: 0x000284E8 File Offset: 0x000266E8
		public static MethodInfo GetGetMethod(this PropertyInfo propertyInfo)
		{
			return propertyInfo.GetGetMethod(false);
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x000284F4 File Offset: 0x000266F4
		public static MethodInfo GetGetMethod(this PropertyInfo propertyInfo, bool nonPublic)
		{
			MethodInfo getMethod = propertyInfo.GetMethod;
			if (getMethod != null && (getMethod.IsPublic || nonPublic))
			{
				return getMethod;
			}
			return null;
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x00028519 File Offset: 0x00026719
		public static MethodInfo GetSetMethod(this PropertyInfo propertyInfo)
		{
			return propertyInfo.GetSetMethod(false);
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x00028524 File Offset: 0x00026724
		public static MethodInfo GetSetMethod(this PropertyInfo propertyInfo, bool nonPublic)
		{
			MethodInfo setMethod = propertyInfo.SetMethod;
			if (setMethod != null && (setMethod.IsPublic || nonPublic))
			{
				return setMethod;
			}
			return null;
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00028549 File Offset: 0x00026749
		public static bool IsSubclassOf(this Type type, Type c)
		{
			return IntrospectionExtensions.GetTypeInfo(type).IsSubclassOf(c);
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x00028557 File Offset: 0x00026757
		public static bool IsAssignableFrom(this Type type, Type c)
		{
			return IntrospectionExtensions.GetTypeInfo(type).IsAssignableFrom(IntrospectionExtensions.GetTypeInfo(c));
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0002856A File Offset: 0x0002676A
		public static MethodInfo Method(this Delegate d)
		{
			return RuntimeReflectionExtensions.GetMethodInfo(d);
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00028572 File Offset: 0x00026772
		public static MemberTypes MemberType(this MemberInfo memberInfo)
		{
			if (memberInfo is PropertyInfo)
			{
				return MemberTypes.Property;
			}
			if (memberInfo is FieldInfo)
			{
				return MemberTypes.Field;
			}
			if (memberInfo is EventInfo)
			{
				return MemberTypes.Event;
			}
			if (memberInfo is MethodInfo)
			{
				return MemberTypes.Method;
			}
			return MemberTypes.Other;
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0002859D File Offset: 0x0002679D
		public static bool ContainsGenericParameters(this Type type)
		{
			return IntrospectionExtensions.GetTypeInfo(type).ContainsGenericParameters;
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x000285AA File Offset: 0x000267AA
		public static bool IsInterface(this Type type)
		{
			return IntrospectionExtensions.GetTypeInfo(type).IsInterface;
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x000285B7 File Offset: 0x000267B7
		public static bool IsGenericType(this Type type)
		{
			return IntrospectionExtensions.GetTypeInfo(type).IsGenericType;
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x000285C4 File Offset: 0x000267C4
		public static bool IsGenericTypeDefinition(this Type type)
		{
			return IntrospectionExtensions.GetTypeInfo(type).IsGenericTypeDefinition;
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x000285D1 File Offset: 0x000267D1
		public static Type BaseType(this Type type)
		{
			return IntrospectionExtensions.GetTypeInfo(type).BaseType;
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x000285DE File Offset: 0x000267DE
		public static bool IsEnum(this Type type)
		{
			return IntrospectionExtensions.GetTypeInfo(type).IsEnum;
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x000285EB File Offset: 0x000267EB
		public static bool IsClass(this Type type)
		{
			return IntrospectionExtensions.GetTypeInfo(type).IsClass;
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x000285F8 File Offset: 0x000267F8
		public static bool IsSealed(this Type type)
		{
			return IntrospectionExtensions.GetTypeInfo(type).IsSealed;
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x00028605 File Offset: 0x00026805
		public static MethodInfo GetBaseDefinition(this MethodInfo method)
		{
			return RuntimeReflectionExtensions.GetRuntimeBaseDefinition(method);
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x00028628 File Offset: 0x00026828
		public static bool IsDefined(this Type type, Type attributeType, bool inherit)
		{
			return Enumerable.Any<CustomAttributeData>(IntrospectionExtensions.GetTypeInfo(type).CustomAttributes, (CustomAttributeData a) => a.AttributeType == attributeType);
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0002865E File Offset: 0x0002685E
		public static MethodInfo GetMethod(this Type type, string name)
		{
			return type.GetMethod(name, TypeExtensions.DefaultFlags);
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0002866C File Offset: 0x0002686C
		public static MethodInfo GetMethod(this Type type, string name, BindingFlags bindingFlags)
		{
			return IntrospectionExtensions.GetTypeInfo(type).GetDeclaredMethod(name);
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0002867A File Offset: 0x0002687A
		public static MethodInfo GetMethod(this Type type, IList<Type> parameterTypes)
		{
			return type.GetMethod(null, parameterTypes);
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x00028684 File Offset: 0x00026884
		public static MethodInfo GetMethod(this Type type, string name, IList<Type> parameterTypes)
		{
			return type.GetMethod(name, TypeExtensions.DefaultFlags, null, parameterTypes, null);
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x00028718 File Offset: 0x00026918
		public static MethodInfo GetMethod(this Type type, string name, BindingFlags bindingFlags, object placeHolder1, IList<Type> parameterTypes, object placeHolder2)
		{
			return Enumerable.SingleOrDefault<MethodInfo>(Enumerable.Where<MethodInfo>(IntrospectionExtensions.GetTypeInfo(type).DeclaredMethods, delegate(MethodInfo m)
			{
				if (name != null && m.Name != name)
				{
					return false;
				}
				if (!TypeExtensions.TestAccessibility(m, bindingFlags))
				{
					return false;
				}
				return Enumerable.SequenceEqual<Type>(Enumerable.Select<ParameterInfo, Type>(m.GetParameters(), (ParameterInfo p) => p.ParameterType), parameterTypes);
			}));
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x000287F8 File Offset: 0x000269F8
		public static PropertyInfo GetProperty(this Type type, string name, BindingFlags bindingFlags, object placeholder1, Type propertyType, IList<Type> indexParameters, object placeholder2)
		{
			return Enumerable.SingleOrDefault<PropertyInfo>(Enumerable.Where<PropertyInfo>(IntrospectionExtensions.GetTypeInfo(type).DeclaredProperties, delegate(PropertyInfo p)
			{
				if (name != null && name != p.Name)
				{
					return false;
				}
				if (propertyType != null && propertyType != p.PropertyType)
				{
					return false;
				}
				if (indexParameters != null)
				{
					if (!Enumerable.SequenceEqual<Type>(Enumerable.Select<ParameterInfo, Type>(p.GetIndexParameters(), (ParameterInfo ip) => ip.ParameterType), indexParameters))
					{
						return false;
					}
				}
				return true;
			}));
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0002888C File Offset: 0x00026A8C
		public static IEnumerable<MemberInfo> GetMember(this Type type, string name, MemberTypes memberType, BindingFlags bindingFlags)
		{
			return Enumerable.Where<MemberInfo>(IntrospectionExtensions.GetTypeInfo(type).GetMembersRecursive(), (MemberInfo m) => (name == null || !(name != m.Name)) && m.MemberType() == memberType && TypeExtensions.TestAccessibility(m, bindingFlags));
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x000288D0 File Offset: 0x00026AD0
		public static IEnumerable<ConstructorInfo> GetConstructors(this Type type)
		{
			return type.GetConstructors(TypeExtensions.DefaultFlags);
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x000288DD File Offset: 0x00026ADD
		public static IEnumerable<ConstructorInfo> GetConstructors(this Type type, BindingFlags bindingFlags)
		{
			return type.GetConstructors(bindingFlags, null);
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x00028958 File Offset: 0x00026B58
		private static IEnumerable<ConstructorInfo> GetConstructors(this Type type, BindingFlags bindingFlags, IList<Type> parameterTypes)
		{
			return Enumerable.Where<ConstructorInfo>(IntrospectionExtensions.GetTypeInfo(type).DeclaredConstructors, delegate(ConstructorInfo c)
			{
				if (!TypeExtensions.TestAccessibility(c, bindingFlags))
				{
					return false;
				}
				if (parameterTypes != null)
				{
					if (!Enumerable.SequenceEqual<Type>(Enumerable.Select<ParameterInfo, Type>(c.GetParameters(), (ParameterInfo p) => p.ParameterType), parameterTypes))
					{
						return false;
					}
				}
				return true;
			});
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x00028995 File Offset: 0x00026B95
		public static ConstructorInfo GetConstructor(this Type type, IList<Type> parameterTypes)
		{
			return type.GetConstructor(TypeExtensions.DefaultFlags, null, parameterTypes, null);
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x000289A5 File Offset: 0x00026BA5
		public static ConstructorInfo GetConstructor(this Type type, BindingFlags bindingFlags, object placeholder1, IList<Type> parameterTypes, object placeholder2)
		{
			return Enumerable.SingleOrDefault<ConstructorInfo>(type.GetConstructors(bindingFlags, parameterTypes));
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x000289B4 File Offset: 0x00026BB4
		public static MemberInfo[] GetMember(this Type type, string member)
		{
			return type.GetMember(member, TypeExtensions.DefaultFlags);
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x000289F0 File Offset: 0x00026BF0
		public static MemberInfo[] GetMember(this Type type, string member, BindingFlags bindingFlags)
		{
			return Enumerable.ToArray<MemberInfo>(Enumerable.Where<MemberInfo>(IntrospectionExtensions.GetTypeInfo(type).GetMembersRecursive(), (MemberInfo m) => m.Name == member && TypeExtensions.TestAccessibility(m, bindingFlags)));
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00028A32 File Offset: 0x00026C32
		public static MemberInfo GetField(this Type type, string member)
		{
			return type.GetField(member, TypeExtensions.DefaultFlags);
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x00028A40 File Offset: 0x00026C40
		public static MemberInfo GetField(this Type type, string member, BindingFlags bindingFlags)
		{
			return IntrospectionExtensions.GetTypeInfo(type).GetDeclaredField(member);
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x00028A64 File Offset: 0x00026C64
		public static IEnumerable<PropertyInfo> GetProperties(this Type type, BindingFlags bindingFlags)
		{
			IList<PropertyInfo> list = bindingFlags.HasFlag(BindingFlags.DeclaredOnly) ? Enumerable.ToList<PropertyInfo>(IntrospectionExtensions.GetTypeInfo(type).DeclaredProperties) : IntrospectionExtensions.GetTypeInfo(type).GetPropertiesRecursive();
			return Enumerable.Where<PropertyInfo>(list, (PropertyInfo p) => TypeExtensions.TestAccessibility(p, bindingFlags));
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x00028AE8 File Offset: 0x00026CE8
		private static IList<MemberInfo> GetMembersRecursive(this TypeInfo type)
		{
			TypeInfo typeInfo = type;
			IList<MemberInfo> list = new List<MemberInfo>();
			while (typeInfo != null)
			{
				using (IEnumerator<MemberInfo> enumerator = typeInfo.DeclaredMembers.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						MemberInfo member = enumerator.Current;
						if (!Enumerable.Any<MemberInfo>(list, (MemberInfo p) => p.Name == member.Name))
						{
							list.Add(member);
						}
					}
				}
				typeInfo = ((typeInfo.BaseType != null) ? IntrospectionExtensions.GetTypeInfo(typeInfo.BaseType) : null);
			}
			return list;
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x00028BAC File Offset: 0x00026DAC
		private static IList<PropertyInfo> GetPropertiesRecursive(this TypeInfo type)
		{
			TypeInfo typeInfo = type;
			IList<PropertyInfo> list = new List<PropertyInfo>();
			while (typeInfo != null)
			{
				using (IEnumerator<PropertyInfo> enumerator = typeInfo.DeclaredProperties.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						PropertyInfo member = enumerator.Current;
						if (!Enumerable.Any<PropertyInfo>(list, (PropertyInfo p) => p.Name == member.Name))
						{
							list.Add(member);
						}
					}
				}
				typeInfo = ((typeInfo.BaseType != null) ? IntrospectionExtensions.GetTypeInfo(typeInfo.BaseType) : null);
			}
			return list;
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x00028C70 File Offset: 0x00026E70
		private static IList<FieldInfo> GetFieldsRecursive(this TypeInfo type)
		{
			TypeInfo typeInfo = type;
			IList<FieldInfo> list = new List<FieldInfo>();
			while (typeInfo != null)
			{
				using (IEnumerator<FieldInfo> enumerator = typeInfo.DeclaredFields.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						FieldInfo member = enumerator.Current;
						if (!Enumerable.Any<FieldInfo>(list, (FieldInfo p) => p.Name == member.Name))
						{
							list.Add(member);
						}
					}
				}
				typeInfo = ((typeInfo.BaseType != null) ? IntrospectionExtensions.GetTypeInfo(typeInfo.BaseType) : null);
			}
			return list;
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x00028D14 File Offset: 0x00026F14
		public static IEnumerable<MethodInfo> GetMethods(this Type type, BindingFlags bindingFlags)
		{
			return IntrospectionExtensions.GetTypeInfo(type).DeclaredMethods;
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x00028D21 File Offset: 0x00026F21
		public static PropertyInfo GetProperty(this Type type, string name)
		{
			return type.GetProperty(name, TypeExtensions.DefaultFlags);
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x00028D2F File Offset: 0x00026F2F
		public static PropertyInfo GetProperty(this Type type, string name, BindingFlags bindingFlags)
		{
			return IntrospectionExtensions.GetTypeInfo(type).GetDeclaredProperty(name);
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x00028D3D File Offset: 0x00026F3D
		public static IEnumerable<FieldInfo> GetFields(this Type type)
		{
			return type.GetFields(TypeExtensions.DefaultFlags);
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x00028D60 File Offset: 0x00026F60
		public static IEnumerable<FieldInfo> GetFields(this Type type, BindingFlags bindingFlags)
		{
			IList<FieldInfo> list = bindingFlags.HasFlag(BindingFlags.DeclaredOnly) ? Enumerable.ToList<FieldInfo>(IntrospectionExtensions.GetTypeInfo(type).DeclaredFields) : IntrospectionExtensions.GetTypeInfo(type).GetFieldsRecursive();
			return Enumerable.ToList<FieldInfo>(Enumerable.Where<FieldInfo>(list, (FieldInfo f) => TypeExtensions.TestAccessibility(f, bindingFlags)));
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x00028DC7 File Offset: 0x00026FC7
		private static bool TestAccessibility(PropertyInfo member, BindingFlags bindingFlags)
		{
			return (member.GetMethod != null && TypeExtensions.TestAccessibility(member.GetMethod, bindingFlags)) || (member.SetMethod != null && TypeExtensions.TestAccessibility(member.SetMethod, bindingFlags));
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x00028DFC File Offset: 0x00026FFC
		private static bool TestAccessibility(MemberInfo member, BindingFlags bindingFlags)
		{
			if (member is FieldInfo)
			{
				return TypeExtensions.TestAccessibility((FieldInfo)member, bindingFlags);
			}
			if (member is MethodBase)
			{
				return TypeExtensions.TestAccessibility((MethodBase)member, bindingFlags);
			}
			if (member is PropertyInfo)
			{
				return TypeExtensions.TestAccessibility((PropertyInfo)member, bindingFlags);
			}
			throw new Exception("Unexpected member type.");
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00028E54 File Offset: 0x00027054
		private static bool TestAccessibility(FieldInfo member, BindingFlags bindingFlags)
		{
			bool flag = (member.IsPublic && bindingFlags.HasFlag(BindingFlags.Public)) || (!member.IsPublic && bindingFlags.HasFlag(BindingFlags.NonPublic));
			bool flag2 = (member.IsStatic && bindingFlags.HasFlag(BindingFlags.Static)) || (!member.IsStatic && bindingFlags.HasFlag(BindingFlags.Instance));
			return flag && flag2;
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x00028EE0 File Offset: 0x000270E0
		private static bool TestAccessibility(MethodBase member, BindingFlags bindingFlags)
		{
			bool flag = (member.IsPublic && bindingFlags.HasFlag(BindingFlags.Public)) || (!member.IsPublic && bindingFlags.HasFlag(BindingFlags.NonPublic));
			bool flag2 = (member.IsStatic && bindingFlags.HasFlag(BindingFlags.Static)) || (!member.IsStatic && bindingFlags.HasFlag(BindingFlags.Instance));
			return flag && flag2;
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x00028F6B File Offset: 0x0002716B
		public static Type[] GetGenericArguments(this Type type)
		{
			return IntrospectionExtensions.GetTypeInfo(type).GenericTypeArguments;
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x00028F78 File Offset: 0x00027178
		public static IEnumerable<Type> GetInterfaces(this Type type)
		{
			return IntrospectionExtensions.GetTypeInfo(type).ImplementedInterfaces;
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x00028F85 File Offset: 0x00027185
		public static IEnumerable<MethodInfo> GetMethods(this Type type)
		{
			return IntrospectionExtensions.GetTypeInfo(type).DeclaredMethods;
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00028F92 File Offset: 0x00027192
		public static bool IsAbstract(this Type type)
		{
			return IntrospectionExtensions.GetTypeInfo(type).IsAbstract;
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x00028F9F File Offset: 0x0002719F
		public static bool IsVisible(this Type type)
		{
			return IntrospectionExtensions.GetTypeInfo(type).IsVisible;
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x00028FAC File Offset: 0x000271AC
		public static bool IsValueType(this Type type)
		{
			return IntrospectionExtensions.GetTypeInfo(type).IsValueType;
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x00028FBC File Offset: 0x000271BC
		public static bool AssignableToTypeName(this Type type, string fullTypeName, out Type match)
		{
			for (Type type2 = type; type2 != null; type2 = type2.BaseType())
			{
				if (string.Equals(type2.FullName, fullTypeName, 4))
				{
					match = type2;
					return true;
				}
			}
			foreach (Type type3 in type.GetInterfaces())
			{
				if (string.Equals(type3.Name, fullTypeName, 4))
				{
					match = type;
					return true;
				}
			}
			match = null;
			return false;
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x00029040 File Offset: 0x00027240
		public static bool AssignableToTypeName(this Type type, string fullTypeName)
		{
			Type type2;
			return type.AssignableToTypeName(fullTypeName, out type2);
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x00029074 File Offset: 0x00027274
		public static MethodInfo GetGenericMethod(this Type type, string name, params Type[] parameterTypes)
		{
			IEnumerable<MethodInfo> enumerable = Enumerable.Where<MethodInfo>(type.GetMethods(), (MethodInfo method) => method.Name == name);
			foreach (MethodInfo methodInfo in enumerable)
			{
				if (methodInfo.HasParameters(parameterTypes))
				{
					return methodInfo;
				}
			}
			return null;
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x000290F8 File Offset: 0x000272F8
		public static bool HasParameters(this MethodInfo method, params Type[] parameterTypes)
		{
			Type[] array = Enumerable.ToArray<Type>(Enumerable.Select<ParameterInfo, Type>(method.GetParameters(), (ParameterInfo parameter) => parameter.ParameterType));
			if (array.Length != parameterTypes.Length)
			{
				return false;
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].ToString() != parameterTypes[i].ToString())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x000293B8 File Offset: 0x000275B8
		public static IEnumerable<Type> GetAllInterfaces(this Type target)
		{
			foreach (Type i in target.GetInterfaces())
			{
				yield return i;
				foreach (Type ci in i.GetInterfaces())
				{
					yield return ci;
				}
			}
			yield break;
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x000293E0 File Offset: 0x000275E0
		public static IEnumerable<MethodInfo> GetAllMethods(this Type target)
		{
			List<Type> list = Enumerable.ToList<Type>(target.GetAllInterfaces());
			list.Add(target);
			return Enumerable.SelectMany<Type, MethodInfo, MethodInfo>(list, (Type type) => type.GetMethods(), (Type type, MethodInfo method) => method);
		}

		// Token: 0x040003E4 RID: 996
		private static BindingFlags DefaultFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;
	}
}
