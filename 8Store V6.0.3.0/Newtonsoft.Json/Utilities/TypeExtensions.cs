using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200007B RID: 123
	[NullableContext(1)]
	[Nullable(0)]
	internal static class TypeExtensions
	{
		// Token: 0x060005F4 RID: 1524 RVA: 0x00018B0C File Offset: 0x00016D0C
		[return: Nullable(2)]
		public static MethodInfo GetGetMethod(this PropertyInfo propertyInfo)
		{
			return propertyInfo.GetGetMethod(false);
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00018B18 File Offset: 0x00016D18
		[return: Nullable(2)]
		public static MethodInfo GetGetMethod(this PropertyInfo propertyInfo, bool nonPublic)
		{
			MethodInfo getMethod = propertyInfo.GetMethod;
			if (getMethod != null && (getMethod.IsPublic || nonPublic))
			{
				return getMethod;
			}
			return null;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00018B3C File Offset: 0x00016D3C
		[return: Nullable(2)]
		public static MethodInfo GetSetMethod(this PropertyInfo propertyInfo)
		{
			return propertyInfo.GetSetMethod(false);
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00018B48 File Offset: 0x00016D48
		[return: Nullable(2)]
		public static MethodInfo GetSetMethod(this PropertyInfo propertyInfo, bool nonPublic)
		{
			MethodInfo setMethod = propertyInfo.SetMethod;
			if (setMethod != null && (setMethod.IsPublic || nonPublic))
			{
				return setMethod;
			}
			return null;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00018B6C File Offset: 0x00016D6C
		public static bool IsSubclassOf(this Type type, Type c)
		{
			return IntrospectionExtensions.GetTypeInfo(type).IsSubclassOf(c);
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00018B7A File Offset: 0x00016D7A
		public static bool IsAssignableFrom(this Type type, Type c)
		{
			return IntrospectionExtensions.GetTypeInfo(type).IsAssignableFrom(IntrospectionExtensions.GetTypeInfo(c));
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00018B8D File Offset: 0x00016D8D
		public static bool IsInstanceOfType(this Type type, [Nullable(2)] object o)
		{
			return o != null && type.IsAssignableFrom(o.GetType());
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00018BA0 File Offset: 0x00016DA0
		public static MethodInfo Method(this Delegate d)
		{
			return RuntimeReflectionExtensions.GetMethodInfo(d);
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00018BA8 File Offset: 0x00016DA8
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
			return (MemberTypes)0;
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00018BD4 File Offset: 0x00016DD4
		public static bool ContainsGenericParameters(this Type type)
		{
			return IntrospectionExtensions.GetTypeInfo(type).ContainsGenericParameters;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00018BE1 File Offset: 0x00016DE1
		public static bool IsInterface(this Type type)
		{
			return IntrospectionExtensions.GetTypeInfo(type).IsInterface;
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00018BEE File Offset: 0x00016DEE
		public static bool IsGenericType(this Type type)
		{
			return IntrospectionExtensions.GetTypeInfo(type).IsGenericType;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00018BFB File Offset: 0x00016DFB
		public static bool IsGenericTypeDefinition(this Type type)
		{
			return IntrospectionExtensions.GetTypeInfo(type).IsGenericTypeDefinition;
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00018C08 File Offset: 0x00016E08
		[return: Nullable(2)]
		public static Type BaseType(this Type type)
		{
			return IntrospectionExtensions.GetTypeInfo(type).BaseType;
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00018C15 File Offset: 0x00016E15
		public static Assembly Assembly(this Type type)
		{
			return IntrospectionExtensions.GetTypeInfo(type).Assembly;
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00018C22 File Offset: 0x00016E22
		public static bool IsEnum(this Type type)
		{
			return IntrospectionExtensions.GetTypeInfo(type).IsEnum;
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00018C2F File Offset: 0x00016E2F
		public static bool IsClass(this Type type)
		{
			return IntrospectionExtensions.GetTypeInfo(type).IsClass;
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00018C3C File Offset: 0x00016E3C
		public static bool IsSealed(this Type type)
		{
			return IntrospectionExtensions.GetTypeInfo(type).IsSealed;
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00018C4C File Offset: 0x00016E4C
		public static PropertyInfo GetProperty(this Type type, string name, BindingFlags bindingFlags, [Nullable(2)] object placeholder1, Type propertyType, IList<Type> indexParameters, [Nullable(2)] object placeholder2)
		{
			return Enumerable.SingleOrDefault<PropertyInfo>(Enumerable.Where<PropertyInfo>(type.GetProperties(bindingFlags), delegate(PropertyInfo p)
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

		// Token: 0x06000607 RID: 1543 RVA: 0x00018C93 File Offset: 0x00016E93
		public static IEnumerable<MemberInfo> GetMember(this Type type, string name, MemberTypes memberType, BindingFlags bindingFlags)
		{
			return type.GetMemberInternal(name, new MemberTypes?(memberType), bindingFlags);
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00018CA3 File Offset: 0x00016EA3
		public static MethodInfo GetBaseDefinition(this MethodInfo method)
		{
			return RuntimeReflectionExtensions.GetRuntimeBaseDefinition(method);
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00018CAC File Offset: 0x00016EAC
		public static bool IsDefined(this Type type, Type attributeType, bool inherit)
		{
			return Enumerable.Any<CustomAttributeData>(IntrospectionExtensions.GetTypeInfo(type).CustomAttributes, (CustomAttributeData a) => a.AttributeType == attributeType);
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00018CE2 File Offset: 0x00016EE2
		public static MethodInfo GetMethod(this Type type, string name)
		{
			return type.GetMethod(name, TypeExtensions.DefaultFlags);
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00018CF0 File Offset: 0x00016EF0
		public static MethodInfo GetMethod(this Type type, string name, BindingFlags bindingFlags)
		{
			return IntrospectionExtensions.GetTypeInfo(type).GetDeclaredMethod(name);
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00018CFE File Offset: 0x00016EFE
		public static MethodInfo GetMethod(this Type type, IList<Type> parameterTypes)
		{
			return type.GetMethod(null, parameterTypes);
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00018D08 File Offset: 0x00016F08
		public static MethodInfo GetMethod(this Type type, [Nullable(2)] string name, IList<Type> parameterTypes)
		{
			return type.GetMethod(name, TypeExtensions.DefaultFlags, null, parameterTypes, null);
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00018D1C File Offset: 0x00016F1C
		public static MethodInfo GetMethod(this Type type, [Nullable(2)] string name, BindingFlags bindingFlags, [Nullable(2)] object placeHolder1, IList<Type> parameterTypes, [Nullable(2)] object placeHolder2)
		{
			return MethodBinder.SelectMethod<MethodInfo>(Enumerable.Where<MethodInfo>(IntrospectionExtensions.GetTypeInfo(type).DeclaredMethods, (MethodInfo m) => (name == null || m.Name == name) && TypeExtensions.TestAccessibility(m, bindingFlags)), parameterTypes);
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00018D60 File Offset: 0x00016F60
		public static IEnumerable<ConstructorInfo> GetConstructors(this Type type)
		{
			return type.GetConstructors(TypeExtensions.DefaultFlags);
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00018D70 File Offset: 0x00016F70
		public static IEnumerable<ConstructorInfo> GetConstructors(this Type type, BindingFlags bindingFlags)
		{
			return Enumerable.Where<ConstructorInfo>(IntrospectionExtensions.GetTypeInfo(type).DeclaredConstructors, (ConstructorInfo c) => TypeExtensions.TestAccessibility(c, bindingFlags));
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00018DA6 File Offset: 0x00016FA6
		public static ConstructorInfo GetConstructor(this Type type, IList<Type> parameterTypes)
		{
			return type.GetConstructor(TypeExtensions.DefaultFlags, null, parameterTypes, null);
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00018DB6 File Offset: 0x00016FB6
		public static ConstructorInfo GetConstructor(this Type type, BindingFlags bindingFlags, [Nullable(2)] object placeholder1, IList<Type> parameterTypes, [Nullable(2)] object placeholder2)
		{
			return MethodBinder.SelectMethod<ConstructorInfo>(type.GetConstructors(bindingFlags), parameterTypes);
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00018DC8 File Offset: 0x00016FC8
		public static MemberInfo[] GetMember(this Type type, string member)
		{
			return type.GetMemberInternal(member, default(MemberTypes?), TypeExtensions.DefaultFlags);
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00018DEC File Offset: 0x00016FEC
		public static MemberInfo[] GetMember(this Type type, string member, BindingFlags bindingFlags)
		{
			return type.GetMemberInternal(member, default(MemberTypes?), bindingFlags);
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00018E0C File Offset: 0x0001700C
		public static MemberInfo[] GetMemberInternal(this Type type, string member, MemberTypes? memberType, BindingFlags bindingFlags)
		{
			return Enumerable.ToArray<MemberInfo>(Enumerable.Where<MemberInfo>(IntrospectionExtensions.GetTypeInfo(type).GetMembersRecursive(), delegate(MemberInfo m)
			{
				if (m.Name == member)
				{
					if (memberType != null)
					{
						MemberTypes? memberTypes = m.MemberType() | memberType;
						MemberTypes? memberType2 = memberType;
						if (!(memberTypes.GetValueOrDefault() == memberType2.GetValueOrDefault() & memberTypes != null == (memberType2 != null)))
						{
							return false;
						}
					}
					return TypeExtensions.TestAccessibility(m, bindingFlags);
				}
				return false;
			}));
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00018E55 File Offset: 0x00017055
		[return: Nullable(2)]
		public static FieldInfo GetField(this Type type, string member)
		{
			return type.GetField(member, TypeExtensions.DefaultFlags);
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00018E64 File Offset: 0x00017064
		[return: Nullable(2)]
		public static FieldInfo GetField(this Type type, string member, BindingFlags bindingFlags)
		{
			FieldInfo declaredField = IntrospectionExtensions.GetTypeInfo(type).GetDeclaredField(member);
			if (declaredField == null || !TypeExtensions.TestAccessibility(declaredField, bindingFlags))
			{
				return null;
			}
			return declaredField;
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00018E90 File Offset: 0x00017090
		public static IEnumerable<PropertyInfo> GetProperties(this Type type, BindingFlags bindingFlags)
		{
			IEnumerable<PropertyInfo> enumerable;
			if (!bindingFlags.HasFlag(BindingFlags.DeclaredOnly))
			{
				enumerable = IntrospectionExtensions.GetTypeInfo(type).GetPropertiesRecursive();
			}
			else
			{
				IList<PropertyInfo> list = Enumerable.ToList<PropertyInfo>(IntrospectionExtensions.GetTypeInfo(type).DeclaredProperties);
				enumerable = list;
			}
			return Enumerable.Where<PropertyInfo>(enumerable, (PropertyInfo p) => TypeExtensions.TestAccessibility(p, bindingFlags));
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00018EF4 File Offset: 0x000170F4
		private static bool ContainsMemberName(IEnumerable<MemberInfo> members, string name)
		{
			using (IEnumerator<MemberInfo> enumerator = members.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Name == name)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00018F48 File Offset: 0x00017148
		private static IList<MemberInfo> GetMembersRecursive(this TypeInfo type)
		{
			TypeInfo typeInfo = type;
			List<MemberInfo> list = new List<MemberInfo>();
			while (typeInfo != null)
			{
				foreach (MemberInfo memberInfo in typeInfo.DeclaredMembers)
				{
					if (!TypeExtensions.ContainsMemberName(list, memberInfo.Name))
					{
						list.Add(memberInfo);
					}
				}
				Type baseType = typeInfo.BaseType;
				typeInfo = ((baseType != null) ? IntrospectionExtensions.GetTypeInfo(baseType) : null);
			}
			return list;
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00018FC4 File Offset: 0x000171C4
		private static IList<PropertyInfo> GetPropertiesRecursive(this TypeInfo type)
		{
			TypeInfo typeInfo = type;
			List<PropertyInfo> list = new List<PropertyInfo>();
			while (typeInfo != null)
			{
				foreach (PropertyInfo propertyInfo in typeInfo.DeclaredProperties)
				{
					if (!TypeExtensions.ContainsMemberName(list, propertyInfo.Name))
					{
						list.Add(propertyInfo);
					}
				}
				Type baseType = typeInfo.BaseType;
				typeInfo = ((baseType != null) ? IntrospectionExtensions.GetTypeInfo(baseType) : null);
			}
			return list;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00019040 File Offset: 0x00017240
		private static IList<FieldInfo> GetFieldsRecursive(this TypeInfo type)
		{
			TypeInfo typeInfo = type;
			List<FieldInfo> list = new List<FieldInfo>();
			while (typeInfo != null)
			{
				foreach (FieldInfo fieldInfo in typeInfo.DeclaredFields)
				{
					if (!TypeExtensions.ContainsMemberName(list, fieldInfo.Name))
					{
						list.Add(fieldInfo);
					}
				}
				Type baseType = typeInfo.BaseType;
				typeInfo = ((baseType != null) ? IntrospectionExtensions.GetTypeInfo(baseType) : null);
			}
			return list;
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x000190BC File Offset: 0x000172BC
		public static IEnumerable<MethodInfo> GetMethods(this Type type, BindingFlags bindingFlags)
		{
			return IntrospectionExtensions.GetTypeInfo(type).DeclaredMethods;
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x000190C9 File Offset: 0x000172C9
		[return: Nullable(2)]
		public static PropertyInfo GetProperty(this Type type, string name)
		{
			return type.GetProperty(name, TypeExtensions.DefaultFlags);
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x000190D8 File Offset: 0x000172D8
		[return: Nullable(2)]
		public static PropertyInfo GetProperty(this Type type, string name, BindingFlags bindingFlags)
		{
			PropertyInfo declaredProperty = IntrospectionExtensions.GetTypeInfo(type).GetDeclaredProperty(name);
			if (declaredProperty == null || !TypeExtensions.TestAccessibility(declaredProperty, bindingFlags))
			{
				return null;
			}
			return declaredProperty;
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00019101 File Offset: 0x00017301
		public static IEnumerable<FieldInfo> GetFields(this Type type)
		{
			return type.GetFields(TypeExtensions.DefaultFlags);
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00019110 File Offset: 0x00017310
		public static IEnumerable<FieldInfo> GetFields(this Type type, BindingFlags bindingFlags)
		{
			IEnumerable<FieldInfo> enumerable;
			if (!bindingFlags.HasFlag(BindingFlags.DeclaredOnly))
			{
				enumerable = IntrospectionExtensions.GetTypeInfo(type).GetFieldsRecursive();
			}
			else
			{
				IList<FieldInfo> list = Enumerable.ToList<FieldInfo>(IntrospectionExtensions.GetTypeInfo(type).DeclaredFields);
				enumerable = list;
			}
			return Enumerable.ToList<FieldInfo>(Enumerable.Where<FieldInfo>(enumerable, (FieldInfo f) => TypeExtensions.TestAccessibility(f, bindingFlags)));
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00019177 File Offset: 0x00017377
		private static bool TestAccessibility(PropertyInfo member, BindingFlags bindingFlags)
		{
			return (member.GetMethod != null && TypeExtensions.TestAccessibility(member.GetMethod, bindingFlags)) || (member.SetMethod != null && TypeExtensions.TestAccessibility(member.SetMethod, bindingFlags));
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x000191AC File Offset: 0x000173AC
		private static bool TestAccessibility(MemberInfo member, BindingFlags bindingFlags)
		{
			FieldInfo fieldInfo = member as FieldInfo;
			if (fieldInfo != null)
			{
				return TypeExtensions.TestAccessibility(fieldInfo, bindingFlags);
			}
			MethodBase methodBase = member as MethodBase;
			if (methodBase != null)
			{
				return TypeExtensions.TestAccessibility(methodBase, bindingFlags);
			}
			PropertyInfo propertyInfo = member as PropertyInfo;
			if (propertyInfo != null)
			{
				return TypeExtensions.TestAccessibility(propertyInfo, bindingFlags);
			}
			throw new ArgumentOutOfRangeException("Unexpected member type.");
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x000191FC File Offset: 0x000173FC
		private static bool TestAccessibility(FieldInfo member, BindingFlags bindingFlags)
		{
			bool flag = (member.IsPublic && bindingFlags.HasFlag(BindingFlags.Public)) || (!member.IsPublic && bindingFlags.HasFlag(BindingFlags.NonPublic));
			bool flag2 = (member.IsStatic && bindingFlags.HasFlag(BindingFlags.Static)) || (!member.IsStatic && bindingFlags.HasFlag(BindingFlags.Instance));
			return flag && flag2;
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00019284 File Offset: 0x00017484
		private static bool TestAccessibility(MethodBase member, BindingFlags bindingFlags)
		{
			bool flag = (member.IsPublic && bindingFlags.HasFlag(BindingFlags.Public)) || (!member.IsPublic && bindingFlags.HasFlag(BindingFlags.NonPublic));
			bool flag2 = (member.IsStatic && bindingFlags.HasFlag(BindingFlags.Static)) || (!member.IsStatic && bindingFlags.HasFlag(BindingFlags.Instance));
			return flag && flag2;
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0001930A File Offset: 0x0001750A
		public static Type[] GetGenericArguments(this Type type)
		{
			return IntrospectionExtensions.GetTypeInfo(type).GenericTypeArguments;
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00019317 File Offset: 0x00017517
		public static IEnumerable<Type> GetInterfaces(this Type type)
		{
			return IntrospectionExtensions.GetTypeInfo(type).ImplementedInterfaces;
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00019324 File Offset: 0x00017524
		public static IEnumerable<MethodInfo> GetMethods(this Type type)
		{
			return IntrospectionExtensions.GetTypeInfo(type).DeclaredMethods;
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00019331 File Offset: 0x00017531
		public static bool IsAbstract(this Type type)
		{
			return IntrospectionExtensions.GetTypeInfo(type).IsAbstract;
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0001933E File Offset: 0x0001753E
		public static bool IsVisible(this Type type)
		{
			return IntrospectionExtensions.GetTypeInfo(type).IsVisible;
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0001934B File Offset: 0x0001754B
		public static bool IsValueType(this Type type)
		{
			return IntrospectionExtensions.GetTypeInfo(type).IsValueType;
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00019358 File Offset: 0x00017558
		public static bool IsPrimitive(this Type type)
		{
			return IntrospectionExtensions.GetTypeInfo(type).IsPrimitive;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00019368 File Offset: 0x00017568
		public static bool AssignableToTypeNameIncludingInterfaces([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] this Type type, string fullTypeName, [Nullable(2)] [NotNullWhen(true)] out Type match)
		{
			if (type.AssignableToTypeName(fullTypeName, out match))
			{
				return true;
			}
			using (IEnumerator<Type> enumerator = type.GetInterfaces().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (string.Equals(enumerator.Current.Name, fullTypeName, 4))
					{
						match = type;
						return true;
					}
				}
			}
			match = null;
			return false;
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x000193D4 File Offset: 0x000175D4
		public static bool AssignableToTypeName(this Type type, string fullTypeName, [Nullable(2)] [NotNullWhen(true)] out Type match)
		{
			for (Type type2 = type; type2 != null; type2 = type2.BaseType())
			{
				if (string.Equals(type2.FullName, fullTypeName, 4))
				{
					match = type2;
					return true;
				}
			}
			match = null;
			return false;
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00019408 File Offset: 0x00017608
		public static bool AssignableToTypeName(this Type type, string fullTypeName)
		{
			Type type2;
			return type.AssignableToTypeName(fullTypeName, out type2);
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00019420 File Offset: 0x00017620
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		public static bool ImplementInterface(this Type type, Type interfaceType)
		{
			for (Type type2 = type; type2 != null; type2 = type2.BaseType())
			{
				foreach (Type type3 in type2.GetInterfaces())
				{
					if (type3 == interfaceType || (type3 != null && type3.ImplementInterface(interfaceType)))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0400026C RID: 620
		private static readonly BindingFlags DefaultFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;
	}
}
