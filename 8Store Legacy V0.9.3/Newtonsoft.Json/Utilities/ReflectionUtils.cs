using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.Serialization.Formatters;
using System.Text;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000D3 RID: 211
	internal static class ReflectionUtils
	{
		// Token: 0x060009DE RID: 2526 RVA: 0x00026F48 File Offset: 0x00025148
		public static bool IsVirtual(this PropertyInfo propertyInfo)
		{
			ValidationUtils.ArgumentNotNull(propertyInfo, "propertyInfo");
			MethodInfo methodInfo = propertyInfo.GetGetMethod();
			if (methodInfo != null && methodInfo.IsVirtual)
			{
				return true;
			}
			methodInfo = propertyInfo.GetSetMethod();
			return methodInfo != null && methodInfo.IsVirtual;
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x00026F8C File Offset: 0x0002518C
		public static MethodInfo GetBaseDefinition(this PropertyInfo propertyInfo)
		{
			ValidationUtils.ArgumentNotNull(propertyInfo, "propertyInfo");
			MethodInfo methodInfo = propertyInfo.GetGetMethod();
			if (methodInfo != null)
			{
				return methodInfo.GetBaseDefinition();
			}
			methodInfo = propertyInfo.GetSetMethod();
			if (methodInfo != null)
			{
				return methodInfo.GetBaseDefinition();
			}
			return null;
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x00026FC7 File Offset: 0x000251C7
		public static bool IsPublic(PropertyInfo property)
		{
			return (property.GetGetMethod() != null && property.GetGetMethod().IsPublic) || (property.GetSetMethod() != null && property.GetSetMethod().IsPublic);
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x00026FF8 File Offset: 0x000251F8
		public static Type GetObjectType(object v)
		{
			if (v == null)
			{
				return null;
			}
			return v.GetType();
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x00027008 File Offset: 0x00025208
		public static string GetTypeName(Type t, FormatterAssemblyStyle assemblyFormat, SerializationBinder binder)
		{
			string text3;
			if (binder != null)
			{
				string text;
				string text2;
				binder.BindToName(t, out text, out text2);
				text3 = text2 + ((text == null) ? "" : (", " + text));
			}
			else
			{
				text3 = t.AssemblyQualifiedName;
			}
			switch (assemblyFormat)
			{
			case FormatterAssemblyStyle.Simple:
				return ReflectionUtils.RemoveAssemblyDetails(text3);
			case FormatterAssemblyStyle.Full:
				return text3;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x00027068 File Offset: 0x00025268
		private static string RemoveAssemblyDetails(string fullyQualifiedTypeName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < fullyQualifiedTypeName.Length; i++)
			{
				char c = fullyQualifiedTypeName.get_Chars(i);
				char c2 = c;
				if (c2 != ',')
				{
					switch (c2)
					{
					case '[':
						flag = false;
						flag2 = false;
						stringBuilder.Append(c);
						goto IL_77;
					case ']':
						flag = false;
						flag2 = false;
						stringBuilder.Append(c);
						goto IL_77;
					}
					if (!flag2)
					{
						stringBuilder.Append(c);
					}
				}
				else if (!flag)
				{
					flag = true;
					stringBuilder.Append(c);
				}
				else
				{
					flag2 = true;
				}
				IL_77:;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x000270FF File Offset: 0x000252FF
		public static bool HasDefaultConstructor(Type t, bool nonPublic)
		{
			ValidationUtils.ArgumentNotNull(t, "t");
			return t.IsValueType() || ReflectionUtils.GetDefaultConstructor(t, nonPublic) != null;
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x00027123 File Offset: 0x00025323
		public static ConstructorInfo GetDefaultConstructor(Type t)
		{
			return ReflectionUtils.GetDefaultConstructor(t, false);
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x0002713C File Offset: 0x0002533C
		public static ConstructorInfo GetDefaultConstructor(Type t, bool nonPublic)
		{
			BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;
			if (nonPublic)
			{
				bindingFlags |= BindingFlags.NonPublic;
			}
			return Enumerable.SingleOrDefault<ConstructorInfo>(t.GetConstructors(bindingFlags), (ConstructorInfo c) => !Enumerable.Any<ParameterInfo>(c.GetParameters()));
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x0002717D File Offset: 0x0002537D
		public static bool IsNullable(Type t)
		{
			ValidationUtils.ArgumentNotNull(t, "t");
			return !t.IsValueType() || ReflectionUtils.IsNullableType(t);
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0002719A File Offset: 0x0002539A
		public static bool IsNullableType(Type t)
		{
			ValidationUtils.ArgumentNotNull(t, "t");
			return t.IsGenericType() && t.GetGenericTypeDefinition() == typeof(Nullable);
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x000271C3 File Offset: 0x000253C3
		public static Type EnsureNotNullableType(Type t)
		{
			if (!ReflectionUtils.IsNullableType(t))
			{
				return t;
			}
			return Nullable.GetUnderlyingType(t);
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x000271D8 File Offset: 0x000253D8
		public static bool IsGenericDefinition(Type type, Type genericInterfaceDefinition)
		{
			if (!type.IsGenericType())
			{
				return false;
			}
			Type genericTypeDefinition = type.GetGenericTypeDefinition();
			return genericTypeDefinition == genericInterfaceDefinition;
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x000271FC File Offset: 0x000253FC
		public static bool ImplementsGenericDefinition(Type type, Type genericInterfaceDefinition)
		{
			Type type2;
			return ReflectionUtils.ImplementsGenericDefinition(type, genericInterfaceDefinition, out type2);
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x00027214 File Offset: 0x00025414
		public static bool ImplementsGenericDefinition(Type type, Type genericInterfaceDefinition, out Type implementingType)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			ValidationUtils.ArgumentNotNull(genericInterfaceDefinition, "genericInterfaceDefinition");
			if (!genericInterfaceDefinition.IsInterface() || !genericInterfaceDefinition.IsGenericTypeDefinition())
			{
				throw new ArgumentNullException("'{0}' is not a generic interface definition.".FormatWith(CultureInfo.InvariantCulture, genericInterfaceDefinition));
			}
			if (type.IsInterface() && type.IsGenericType())
			{
				Type genericTypeDefinition = type.GetGenericTypeDefinition();
				if (genericInterfaceDefinition == genericTypeDefinition)
				{
					implementingType = type;
					return true;
				}
			}
			foreach (Type type2 in type.GetInterfaces())
			{
				if (type2.IsGenericType())
				{
					Type genericTypeDefinition2 = type2.GetGenericTypeDefinition();
					if (genericInterfaceDefinition == genericTypeDefinition2)
					{
						implementingType = type2;
						return true;
					}
				}
			}
			implementingType = null;
			return false;
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x000272DC File Offset: 0x000254DC
		public static bool InheritsGenericDefinition(Type type, Type genericClassDefinition)
		{
			Type type2;
			return ReflectionUtils.InheritsGenericDefinition(type, genericClassDefinition, out type2);
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x000272F4 File Offset: 0x000254F4
		public static bool InheritsGenericDefinition(Type type, Type genericClassDefinition, out Type implementingType)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			ValidationUtils.ArgumentNotNull(genericClassDefinition, "genericClassDefinition");
			if (!genericClassDefinition.IsClass() || !genericClassDefinition.IsGenericTypeDefinition())
			{
				throw new ArgumentNullException("'{0}' is not a generic class definition.".FormatWith(CultureInfo.InvariantCulture, genericClassDefinition));
			}
			return ReflectionUtils.InheritsGenericDefinitionInternal(type, genericClassDefinition, out implementingType);
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x00027348 File Offset: 0x00025548
		private static bool InheritsGenericDefinitionInternal(Type currentType, Type genericClassDefinition, out Type implementingType)
		{
			if (currentType.IsGenericType())
			{
				Type genericTypeDefinition = currentType.GetGenericTypeDefinition();
				if (genericClassDefinition == genericTypeDefinition)
				{
					implementingType = currentType;
					return true;
				}
			}
			if (currentType.BaseType() == null)
			{
				implementingType = null;
				return false;
			}
			return ReflectionUtils.InheritsGenericDefinitionInternal(currentType.BaseType(), genericClassDefinition, out implementingType);
		}

		/// <summary>
		/// Gets the type of the typed collection's items.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns>The type of the typed collection's items.</returns>
		// Token: 0x060009F0 RID: 2544 RVA: 0x00027388 File Offset: 0x00025588
		public static Type GetCollectionItemType(Type type)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			if (type.IsArray)
			{
				return type.GetElementType();
			}
			Type type2;
			if (ReflectionUtils.ImplementsGenericDefinition(type, typeof(IEnumerable), out type2))
			{
				if (type2.IsGenericTypeDefinition())
				{
					throw new Exception("Type {0} is not a collection.".FormatWith(CultureInfo.InvariantCulture, type));
				}
				return type2.GetGenericArguments()[0];
			}
			else
			{
				if (typeof(IEnumerable).IsAssignableFrom(type))
				{
					return null;
				}
				throw new Exception("Type {0} is not a collection.".FormatWith(CultureInfo.InvariantCulture, type));
			}
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x00027414 File Offset: 0x00025614
		public static void GetDictionaryKeyValueTypes(Type dictionaryType, out Type keyType, out Type valueType)
		{
			ValidationUtils.ArgumentNotNull(dictionaryType, "type");
			Type type;
			if (ReflectionUtils.ImplementsGenericDefinition(dictionaryType, typeof(IDictionary), out type))
			{
				if (type.IsGenericTypeDefinition())
				{
					throw new Exception("Type {0} is not a dictionary.".FormatWith(CultureInfo.InvariantCulture, dictionaryType));
				}
				Type[] genericArguments = type.GetGenericArguments();
				keyType = genericArguments[0];
				valueType = genericArguments[1];
				return;
			}
			else
			{
				if (typeof(IDictionary).IsAssignableFrom(dictionaryType))
				{
					keyType = null;
					valueType = null;
					return;
				}
				throw new Exception("Type {0} is not a dictionary.".FormatWith(CultureInfo.InvariantCulture, dictionaryType));
			}
		}

		/// <summary>
		/// Gets the member's underlying type.
		/// </summary>
		/// <param name="member">The member.</param>
		/// <returns>The underlying type of the member.</returns>
		// Token: 0x060009F2 RID: 2546 RVA: 0x000274A0 File Offset: 0x000256A0
		public static Type GetMemberUnderlyingType(MemberInfo member)
		{
			ValidationUtils.ArgumentNotNull(member, "member");
			switch (member.MemberType())
			{
			case MemberTypes.Property:
				return ((PropertyInfo)member).PropertyType;
			case MemberTypes.Field:
				return ((FieldInfo)member).FieldType;
			case MemberTypes.Event:
				return ((EventInfo)member).EventHandlerType;
			default:
				throw new ArgumentException("MemberInfo must be of type FieldInfo, PropertyInfo or EventInfo", "member");
			}
		}

		/// <summary>
		/// Determines whether the member is an indexed property.
		/// </summary>
		/// <param name="member">The member.</param>
		/// <returns>
		/// 	<c>true</c> if the member is an indexed property; otherwise, <c>false</c>.
		/// </returns>
		// Token: 0x060009F3 RID: 2547 RVA: 0x00027508 File Offset: 0x00025708
		public static bool IsIndexedProperty(MemberInfo member)
		{
			ValidationUtils.ArgumentNotNull(member, "member");
			PropertyInfo propertyInfo = member as PropertyInfo;
			return propertyInfo != null && ReflectionUtils.IsIndexedProperty(propertyInfo);
		}

		/// <summary>
		/// Determines whether the property is an indexed property.
		/// </summary>
		/// <param name="property">The property.</param>
		/// <returns>
		/// 	<c>true</c> if the property is an indexed property; otherwise, <c>false</c>.
		/// </returns>
		// Token: 0x060009F4 RID: 2548 RVA: 0x00027532 File Offset: 0x00025732
		public static bool IsIndexedProperty(PropertyInfo property)
		{
			ValidationUtils.ArgumentNotNull(property, "property");
			return property.GetIndexParameters().Length > 0;
		}

		/// <summary>
		/// Gets the member's value on the object.
		/// </summary>
		/// <param name="member">The member.</param>
		/// <param name="target">The target object.</param>
		/// <returns>The member's value on the object.</returns>
		// Token: 0x060009F5 RID: 2549 RVA: 0x0002754C File Offset: 0x0002574C
		public static object GetMemberValue(MemberInfo member, object target)
		{
			ValidationUtils.ArgumentNotNull(member, "member");
			ValidationUtils.ArgumentNotNull(target, "target");
			switch (member.MemberType())
			{
			case MemberTypes.Property:
				try
				{
					return ((PropertyInfo)member).GetValue(target, null);
				}
				catch (TargetParameterCountException ex)
				{
					throw new ArgumentException("MemberInfo '{0}' has index parameters".FormatWith(CultureInfo.InvariantCulture, member.Name), ex);
				}
				break;
			case MemberTypes.Field:
				return ((FieldInfo)member).GetValue(target);
			}
			throw new ArgumentException("MemberInfo '{0}' is not of type FieldInfo or PropertyInfo".FormatWith(CultureInfo.InvariantCulture, CultureInfo.InvariantCulture, member.Name), "member");
		}

		/// <summary>
		/// Sets the member's value on the target object.
		/// </summary>
		/// <param name="member">The member.</param>
		/// <param name="target">The target.</param>
		/// <param name="value">The value.</param>
		// Token: 0x060009F6 RID: 2550 RVA: 0x000275F8 File Offset: 0x000257F8
		public static void SetMemberValue(MemberInfo member, object target, object value)
		{
			ValidationUtils.ArgumentNotNull(member, "member");
			ValidationUtils.ArgumentNotNull(target, "target");
			switch (member.MemberType())
			{
			case MemberTypes.Property:
				((PropertyInfo)member).SetValue(target, value, null);
				return;
			case MemberTypes.Field:
				((FieldInfo)member).SetValue(target, value);
				return;
			default:
				throw new ArgumentException("MemberInfo '{0}' must be of type FieldInfo or PropertyInfo".FormatWith(CultureInfo.InvariantCulture, member.Name), "member");
			}
		}

		/// <summary>
		/// Determines whether the specified MemberInfo can be read.
		/// </summary>
		/// <param name="member">The MemberInfo to determine whether can be read.</param>
		/// /// <param name="nonPublic">if set to <c>true</c> then allow the member to be gotten non-publicly.</param>
		/// <returns>
		/// 	<c>true</c> if the specified MemberInfo can be read; otherwise, <c>false</c>.
		/// </returns>
		// Token: 0x060009F7 RID: 2551 RVA: 0x00027670 File Offset: 0x00025870
		public static bool CanReadMemberValue(MemberInfo member, bool nonPublic)
		{
			switch (member.MemberType())
			{
			case MemberTypes.Property:
			{
				PropertyInfo propertyInfo = (PropertyInfo)member;
				return propertyInfo.CanRead && (nonPublic || propertyInfo.GetGetMethod(nonPublic) != null);
			}
			case MemberTypes.Field:
			{
				FieldInfo fieldInfo = (FieldInfo)member;
				return nonPublic || fieldInfo.IsPublic;
			}
			default:
				return false;
			}
		}

		/// <summary>
		/// Determines whether the specified MemberInfo can be set.
		/// </summary>
		/// <param name="member">The MemberInfo to determine whether can be set.</param>
		/// <param name="nonPublic">if set to <c>true</c> then allow the member to be set non-publicly.</param>
		/// <param name="canSetReadOnly">if set to <c>true</c> then allow the member to be set if read-only.</param>
		/// <returns>
		/// 	<c>true</c> if the specified MemberInfo can be set; otherwise, <c>false</c>.
		/// </returns>
		// Token: 0x060009F8 RID: 2552 RVA: 0x000276D4 File Offset: 0x000258D4
		public static bool CanSetMemberValue(MemberInfo member, bool nonPublic, bool canSetReadOnly)
		{
			switch (member.MemberType())
			{
			case MemberTypes.Property:
			{
				PropertyInfo propertyInfo = (PropertyInfo)member;
				return propertyInfo.CanWrite && (nonPublic || propertyInfo.GetSetMethod(nonPublic) != null);
			}
			case MemberTypes.Field:
			{
				FieldInfo fieldInfo = (FieldInfo)member;
				return (!fieldInfo.IsInitOnly || canSetReadOnly) && (nonPublic || fieldInfo.IsPublic);
			}
			default:
				return false;
			}
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x00027774 File Offset: 0x00025974
		public static List<MemberInfo> GetFieldsAndProperties(Type type, BindingFlags bindingAttr)
		{
			List<MemberInfo> list = new List<MemberInfo>();
			list.AddRange(ReflectionUtils.GetFields(type, bindingAttr));
			list.AddRange(ReflectionUtils.GetProperties(type, bindingAttr));
			List<MemberInfo> list2 = new List<MemberInfo>(list.Count);
			foreach (IGrouping<string, MemberInfo> grouping in Enumerable.GroupBy<MemberInfo, string>(list, (MemberInfo m) => m.Name))
			{
				int num = Enumerable.Count<MemberInfo>(grouping);
				IList<MemberInfo> list3 = Enumerable.ToList<MemberInfo>(grouping);
				if (num == 1)
				{
					list2.Add(Enumerable.First<MemberInfo>(list3));
				}
				else
				{
					IEnumerable<MemberInfo> enumerable = Enumerable.Where<MemberInfo>(list3, (MemberInfo m) => !ReflectionUtils.IsOverridenGenericMember(m, bindingAttr) || m.Name == "Item");
					list2.AddRange(enumerable);
				}
			}
			return list2;
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x00027870 File Offset: 0x00025A70
		private static bool IsOverridenGenericMember(MemberInfo memberInfo, BindingFlags bindingAttr)
		{
			MemberTypes memberTypes = memberInfo.MemberType();
			if (memberTypes != MemberTypes.Field && memberTypes != MemberTypes.Property)
			{
				throw new ArgumentException("Member must be a field or property.");
			}
			Type declaringType = memberInfo.DeclaringType;
			if (!declaringType.IsGenericType())
			{
				return false;
			}
			Type genericTypeDefinition = declaringType.GetGenericTypeDefinition();
			if (genericTypeDefinition == null)
			{
				return false;
			}
			MemberInfo[] member = genericTypeDefinition.GetMember(memberInfo.Name, bindingAttr);
			if (member.Length == 0)
			{
				return false;
			}
			Type memberUnderlyingType = ReflectionUtils.GetMemberUnderlyingType(member[0]);
			return memberUnderlyingType.IsGenericParameter;
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x000278DE File Offset: 0x00025ADE
		public static T GetAttribute<T>(object attributeProvider) where T : Attribute
		{
			return ReflectionUtils.GetAttribute<T>(attributeProvider, true);
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x000278E8 File Offset: 0x00025AE8
		public static T GetAttribute<T>(object attributeProvider, bool inherit) where T : Attribute
		{
			T[] attributes = ReflectionUtils.GetAttributes<T>(attributeProvider, inherit);
			if (attributes == null)
			{
				return default(T);
			}
			return Enumerable.SingleOrDefault<T>(attributes);
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x00027910 File Offset: 0x00025B10
		public static T[] GetAttributes<T>(object provider, bool inherit) where T : Attribute
		{
			if (provider is Type)
			{
				return Enumerable.ToArray<T>(CustomAttributeExtensions.GetCustomAttributes<T>(IntrospectionExtensions.GetTypeInfo((Type)provider), inherit));
			}
			if (provider is Assembly)
			{
				return Enumerable.ToArray<T>(CustomAttributeExtensions.GetCustomAttributes<T>((Assembly)provider));
			}
			if (provider is MemberInfo)
			{
				return Enumerable.ToArray<T>(CustomAttributeExtensions.GetCustomAttributes<T>((MemberInfo)provider, inherit));
			}
			if (provider is Module)
			{
				return Enumerable.ToArray<T>(CustomAttributeExtensions.GetCustomAttributes<T>((Module)provider));
			}
			if (provider is ParameterInfo)
			{
				return Enumerable.ToArray<T>(CustomAttributeExtensions.GetCustomAttributes<T>((ParameterInfo)provider, inherit));
			}
			throw new Exception("Cannot get attributes from '{0}'.".FormatWith(CultureInfo.InvariantCulture, provider));
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x000279B8 File Offset: 0x00025BB8
		public static void SplitFullyQualifiedTypeName(string fullyQualifiedTypeName, out string typeName, out string assemblyName)
		{
			int? assemblyDelimiterIndex = ReflectionUtils.GetAssemblyDelimiterIndex(fullyQualifiedTypeName);
			if (assemblyDelimiterIndex != null)
			{
				typeName = fullyQualifiedTypeName.Substring(0, assemblyDelimiterIndex.Value).Trim();
				assemblyName = fullyQualifiedTypeName.Substring(assemblyDelimiterIndex.Value + 1, fullyQualifiedTypeName.Length - assemblyDelimiterIndex.Value - 1).Trim();
				return;
			}
			typeName = fullyQualifiedTypeName;
			assemblyName = null;
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x00027A18 File Offset: 0x00025C18
		private static int? GetAssemblyDelimiterIndex(string fullyQualifiedTypeName)
		{
			int num = 0;
			for (int i = 0; i < fullyQualifiedTypeName.Length; i++)
			{
				char c = fullyQualifiedTypeName.get_Chars(i);
				char c2 = c;
				if (c2 != ',')
				{
					switch (c2)
					{
					case '[':
						num++;
						break;
					case ']':
						num--;
						break;
					}
				}
				else if (num == 0)
				{
					return new int?(i);
				}
			}
			return default(int?);
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x00027A88 File Offset: 0x00025C88
		public static MemberInfo GetMemberInfoFromType(Type targetType, MemberInfo memberInfo)
		{
			MemberTypes memberTypes = memberInfo.MemberType();
			if (memberTypes == MemberTypes.Property)
			{
				PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
				Type[] indexParameters = Enumerable.ToArray<Type>(Enumerable.Select<ParameterInfo, Type>(propertyInfo.GetIndexParameters(), (ParameterInfo p) => p.ParameterType));
				return targetType.GetProperty(propertyInfo.Name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, propertyInfo.PropertyType, indexParameters, null);
			}
			return Enumerable.SingleOrDefault<MemberInfo>(targetType.GetMember(memberInfo.Name, memberInfo.MemberType(), BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic));
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x00027B08 File Offset: 0x00025D08
		public static IEnumerable<FieldInfo> GetFields(Type targetType, BindingFlags bindingAttr)
		{
			ValidationUtils.ArgumentNotNull(targetType, "targetType");
			List<MemberInfo> list = new List<MemberInfo>(targetType.GetFields(bindingAttr));
			return Enumerable.Cast<FieldInfo>(list);
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x00027B3C File Offset: 0x00025D3C
		private static void GetChildPrivateFields(IList<MemberInfo> initialFields, Type targetType, BindingFlags bindingAttr)
		{
			if ((bindingAttr & BindingFlags.NonPublic) != BindingFlags.Default)
			{
				BindingFlags bindingFlags = bindingAttr.RemoveFlag(BindingFlags.Public);
				while ((targetType = targetType.BaseType()) != null)
				{
					IEnumerable<MemberInfo> collection = Enumerable.Cast<MemberInfo>(Enumerable.Where<FieldInfo>(targetType.GetFields(bindingFlags), (FieldInfo f) => f.IsPrivate));
					initialFields.AddRange(collection);
				}
			}
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x00027B9C File Offset: 0x00025D9C
		public static IEnumerable<PropertyInfo> GetProperties(Type targetType, BindingFlags bindingAttr)
		{
			ValidationUtils.ArgumentNotNull(targetType, "targetType");
			List<PropertyInfo> list = new List<PropertyInfo>(targetType.GetProperties(bindingAttr));
			ReflectionUtils.GetChildPrivateProperties(list, targetType, bindingAttr);
			for (int i = 0; i < list.Count; i++)
			{
				PropertyInfo propertyInfo = list[i];
				if (propertyInfo.DeclaringType != targetType)
				{
					PropertyInfo propertyInfo2 = (PropertyInfo)ReflectionUtils.GetMemberInfoFromType(propertyInfo.DeclaringType, propertyInfo);
					list[i] = propertyInfo2;
				}
			}
			return list;
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x00027C06 File Offset: 0x00025E06
		public static BindingFlags RemoveFlag(this BindingFlags bindingAttr, BindingFlags flag)
		{
			if ((bindingAttr & flag) != flag)
			{
				return bindingAttr;
			}
			return bindingAttr ^ flag;
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x00027CB8 File Offset: 0x00025EB8
		private static void GetChildPrivateProperties(IList<PropertyInfo> initialProperties, Type targetType, BindingFlags bindingAttr)
		{
			while ((targetType = targetType.BaseType()) != null)
			{
				foreach (PropertyInfo subTypeProperty2 in targetType.GetProperties(bindingAttr))
				{
					PropertyInfo subTypeProperty = subTypeProperty2;
					if (!ReflectionUtils.IsPublic(subTypeProperty))
					{
						int num = initialProperties.IndexOf((PropertyInfo p) => p.Name == subTypeProperty.Name);
						if (num == -1)
						{
							initialProperties.Add(subTypeProperty);
						}
						else
						{
							initialProperties[num] = subTypeProperty;
						}
					}
					else if (!subTypeProperty.IsVirtual())
					{
						int num2 = initialProperties.IndexOf((PropertyInfo p) => p.Name == subTypeProperty.Name && p.DeclaringType == subTypeProperty.DeclaringType);
						if (num2 == -1)
						{
							initialProperties.Add(subTypeProperty);
						}
					}
					else
					{
						int num3 = initialProperties.IndexOf((PropertyInfo p) => p.Name == subTypeProperty.Name && p.IsVirtual() && p.GetBaseDefinition() != null && p.GetBaseDefinition().DeclaringType.IsAssignableFrom(subTypeProperty.DeclaringType));
						if (num3 == -1)
						{
							initialProperties.Add(subTypeProperty);
						}
					}
				}
			}
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x00027E2C File Offset: 0x0002602C
		public static bool IsMethodOverridden(Type currentType, Type methodDeclaringType, string method)
		{
			return Enumerable.Any<MethodInfo>(currentType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic), (MethodInfo info) => info.Name == method && info.DeclaringType != methodDeclaringType && info.GetBaseDefinition().DeclaringType == methodDeclaringType);
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00027E68 File Offset: 0x00026068
		public static object GetDefaultValue(Type type)
		{
			if (!type.IsValueType())
			{
				return null;
			}
			PrimitiveTypeCode typeCode = ConvertUtils.GetTypeCode(type);
			switch (typeCode)
			{
			case PrimitiveTypeCode.Char:
			case PrimitiveTypeCode.SByte:
			case PrimitiveTypeCode.Int16:
			case PrimitiveTypeCode.UInt16:
			case PrimitiveTypeCode.Int32:
			case PrimitiveTypeCode.Byte:
			case PrimitiveTypeCode.UInt32:
				return 0;
			case PrimitiveTypeCode.CharNullable:
			case PrimitiveTypeCode.BooleanNullable:
			case PrimitiveTypeCode.SByteNullable:
			case PrimitiveTypeCode.Int16Nullable:
			case PrimitiveTypeCode.UInt16Nullable:
			case PrimitiveTypeCode.Int32Nullable:
			case PrimitiveTypeCode.ByteNullable:
			case PrimitiveTypeCode.UInt32Nullable:
			case PrimitiveTypeCode.Int64Nullable:
			case PrimitiveTypeCode.UInt64Nullable:
			case PrimitiveTypeCode.SingleNullable:
			case PrimitiveTypeCode.DoubleNullable:
			case PrimitiveTypeCode.DateTimeNullable:
			case PrimitiveTypeCode.DateTimeOffsetNullable:
			case PrimitiveTypeCode.DecimalNullable:
				break;
			case PrimitiveTypeCode.Boolean:
				return false;
			case PrimitiveTypeCode.Int64:
			case PrimitiveTypeCode.UInt64:
				return 0L;
			case PrimitiveTypeCode.Single:
				return 0f;
			case PrimitiveTypeCode.Double:
				return 0.0;
			case PrimitiveTypeCode.DateTime:
				return default(DateTime);
			case PrimitiveTypeCode.DateTimeOffset:
				return default(DateTimeOffset);
			case PrimitiveTypeCode.Decimal:
				return 0m;
			case PrimitiveTypeCode.Guid:
				return default(Guid);
			default:
				if (typeCode == PrimitiveTypeCode.BigInteger)
				{
					return default(BigInteger);
				}
				break;
			}
			if (ReflectionUtils.IsNullable(type))
			{
				return null;
			}
			return Activator.CreateInstance(type);
		}

		// Token: 0x040003D1 RID: 977
		public static readonly Type[] EmptyTypes = new Type[0];
	}
}
