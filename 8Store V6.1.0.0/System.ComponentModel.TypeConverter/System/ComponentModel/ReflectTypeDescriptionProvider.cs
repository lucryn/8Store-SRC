using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace System.ComponentModel
{
	// Token: 0x02000005 RID: 5
	internal sealed class ReflectTypeDescriptionProvider
	{
		// Token: 0x06000027 RID: 39 RVA: 0x000023EA File Offset: 0x000005EA
		internal ReflectTypeDescriptionProvider()
		{
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000023F4 File Offset: 0x000005F4
		private static Dictionary<object, object> IntrinsicTypeConverters
		{
			get
			{
				if (ReflectTypeDescriptionProvider.s_intrinsicConverters == null)
				{
					Dictionary<object, object> dictionary = new Dictionary<object, object>();
					dictionary[typeof(bool)] = typeof(BooleanConverter);
					dictionary[typeof(byte)] = typeof(ByteConverter);
					dictionary[typeof(sbyte)] = typeof(SByteConverter);
					dictionary[typeof(char)] = typeof(CharConverter);
					dictionary[typeof(double)] = typeof(DoubleConverter);
					dictionary[typeof(string)] = typeof(StringConverter);
					dictionary[typeof(int)] = typeof(Int32Converter);
					dictionary[typeof(short)] = typeof(Int16Converter);
					dictionary[typeof(long)] = typeof(Int64Converter);
					dictionary[typeof(float)] = typeof(SingleConverter);
					dictionary[typeof(ushort)] = typeof(UInt16Converter);
					dictionary[typeof(uint)] = typeof(UInt32Converter);
					dictionary[typeof(ulong)] = typeof(UInt64Converter);
					dictionary[typeof(object)] = typeof(TypeConverter);
					dictionary[typeof(void)] = typeof(TypeConverter);
					dictionary[typeof(DateTime)] = typeof(DateTimeConverter);
					dictionary[typeof(DateTimeOffset)] = typeof(DateTimeOffsetConverter);
					dictionary[typeof(decimal)] = typeof(DecimalConverter);
					dictionary[typeof(TimeSpan)] = typeof(TimeSpanConverter);
					dictionary[typeof(Guid)] = typeof(GuidConverter);
					dictionary[typeof(Array)] = typeof(ArrayConverter);
					dictionary[typeof(ICollection)] = typeof(CollectionConverter);
					dictionary[typeof(Enum)] = typeof(EnumConverter);
					dictionary[ReflectTypeDescriptionProvider.s_intrinsicNullableKey] = typeof(NullableConverter);
					ReflectTypeDescriptionProvider.s_intrinsicConverters = dictionary;
				}
				return ReflectTypeDescriptionProvider.s_intrinsicConverters;
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002690 File Offset: 0x00000890
		private static object CreateInstance(Type objectType, Type parameterType, ref bool noTypeConstructor)
		{
			ConstructorInfo constructorInfo = null;
			noTypeConstructor = true;
			foreach (ConstructorInfo constructorInfo2 in IntrospectionExtensions.GetTypeInfo(objectType).DeclaredConstructors)
			{
				if (constructorInfo2.IsPublic)
				{
					ParameterInfo[] parameters = constructorInfo2.GetParameters();
					if (parameters.Length == 1 && parameters[0].ParameterType.Equals(typeof(Type)))
					{
						constructorInfo = constructorInfo2;
						break;
					}
				}
			}
			if (constructorInfo != null)
			{
				noTypeConstructor = false;
				return constructorInfo.Invoke(new object[]
				{
					parameterType
				});
			}
			return Activator.CreateInstance(objectType);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002730 File Offset: 0x00000930
		private static TypeConverterAttribute GetTypeConverterAttributeIfAny(Type type)
		{
			using (IEnumerator<TypeConverterAttribute> enumerator = CustomAttributeExtensions.GetCustomAttributes<TypeConverterAttribute>(IntrospectionExtensions.GetTypeInfo(type), false).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					return enumerator.Current;
				}
			}
			return null;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002784 File Offset: 0x00000984
		internal static TypeConverter GetConverter(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			object obj = ReflectTypeDescriptionProvider.SearchIntrinsicTable_ExactTypeMatch(type);
			if (obj != null)
			{
				return (TypeConverter)obj;
			}
			TypeConverterAttribute typeConverterAttributeIfAny = ReflectTypeDescriptionProvider.GetTypeConverterAttributeIfAny(type);
			if (typeConverterAttributeIfAny == null)
			{
				Type baseType = IntrospectionExtensions.GetTypeInfo(type).BaseType;
				while (baseType != null && baseType != typeof(object))
				{
					typeConverterAttributeIfAny = ReflectTypeDescriptionProvider.GetTypeConverterAttributeIfAny(baseType);
					if (typeConverterAttributeIfAny != null)
					{
						break;
					}
					baseType = IntrospectionExtensions.GetTypeInfo(baseType).BaseType;
				}
			}
			if (typeConverterAttributeIfAny == null)
			{
				IEnumerable<Type> implementedInterfaces = IntrospectionExtensions.GetTypeInfo(type).ImplementedInterfaces;
				foreach (Type type2 in implementedInterfaces)
				{
					if ((IntrospectionExtensions.GetTypeInfo(type2).Attributes & 3) != null)
					{
						typeConverterAttributeIfAny = ReflectTypeDescriptionProvider.GetTypeConverterAttributeIfAny(type2);
						if (typeConverterAttributeIfAny != null)
						{
							break;
						}
					}
				}
			}
			if (typeConverterAttributeIfAny != null)
			{
				Type typeFromName = ReflectTypeDescriptionProvider.GetTypeFromName(typeConverterAttributeIfAny.ConverterTypeName, type);
				if (typeFromName != null && IntrospectionExtensions.GetTypeInfo(typeof(TypeConverter)).IsAssignableFrom(IntrospectionExtensions.GetTypeInfo(typeFromName)))
				{
					bool flag = true;
					object obj2 = (TypeConverter)ReflectTypeDescriptionProvider.CreateInstance(typeFromName, type, ref flag);
					if (flag)
					{
						object obj3 = ReflectTypeDescriptionProvider.s_syncObject;
						lock (obj3)
						{
							ReflectTypeDescriptionProvider.IntrinsicTypeConverters[type] = obj2;
						}
					}
					return (TypeConverter)obj2;
				}
			}
			return (TypeConverter)ReflectTypeDescriptionProvider.SearchIntrinsicTable(type);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000028EC File Offset: 0x00000AEC
		private static Type GetTypeFromName(string typeName, Type type)
		{
			if (string.IsNullOrEmpty(typeName))
			{
				return null;
			}
			int num = typeName.IndexOf(',');
			Type type2 = null;
			if (num == -1)
			{
				type2 = IntrospectionExtensions.GetTypeInfo(type).Assembly.GetType(typeName);
			}
			if (type2 == null)
			{
				type2 = Type.GetType(typeName);
			}
			return type2;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002930 File Offset: 0x00000B30
		private static object SearchIntrinsicTable(Type callingType)
		{
			object obj = null;
			object obj2 = ReflectTypeDescriptionProvider.s_syncObject;
			lock (obj2)
			{
				Type type = callingType;
				while (type != null && type != typeof(object) && (!ReflectTypeDescriptionProvider.IntrinsicTypeConverters.TryGetValue(type, ref obj) || obj == null))
				{
					type = IntrospectionExtensions.GetTypeInfo(type).BaseType;
				}
				TypeInfo typeInfo = IntrospectionExtensions.GetTypeInfo(callingType);
				if (obj == null)
				{
					foreach (object obj3 in ReflectTypeDescriptionProvider.IntrinsicTypeConverters.Keys)
					{
						Type type2 = obj3 as Type;
						if (type2 != null)
						{
							TypeInfo typeInfo2 = IntrospectionExtensions.GetTypeInfo(type2);
							if (typeInfo2.IsInterface && typeInfo2.IsAssignableFrom(typeInfo))
							{
								ReflectTypeDescriptionProvider.IntrinsicTypeConverters.TryGetValue(obj3, ref obj);
								string text = obj as string;
								if (text != null)
								{
									obj = Type.GetType(text);
									if (obj != null)
									{
										ReflectTypeDescriptionProvider.IntrinsicTypeConverters[callingType] = obj;
									}
								}
								if (obj != null)
								{
									break;
								}
							}
						}
					}
				}
				if (obj == null && typeInfo.IsGenericType && typeInfo.GetGenericTypeDefinition() == typeof(Nullable))
				{
					ReflectTypeDescriptionProvider.IntrinsicTypeConverters.TryGetValue(ReflectTypeDescriptionProvider.s_intrinsicNullableKey, ref obj);
				}
				if (obj == null)
				{
					ReflectTypeDescriptionProvider.IntrinsicTypeConverters.TryGetValue(typeof(object), ref obj);
				}
				Type type3 = obj as Type;
				if (type3 != null)
				{
					bool flag2 = true;
					obj = ReflectTypeDescriptionProvider.CreateInstance(type3, callingType, ref flag2);
					if (flag2)
					{
						ReflectTypeDescriptionProvider.IntrinsicTypeConverters[callingType] = obj;
					}
				}
			}
			return obj;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002ADC File Offset: 0x00000CDC
		private static object SearchIntrinsicTable_ExactTypeMatch(Type callingType)
		{
			object obj = null;
			object obj2 = ReflectTypeDescriptionProvider.s_syncObject;
			lock (obj2)
			{
				if (callingType != null && !ReflectTypeDescriptionProvider.IntrinsicTypeConverters.TryGetValue(callingType, ref obj))
				{
					return null;
				}
				Type type = obj as Type;
				if (type != null)
				{
					bool flag2 = true;
					obj = ReflectTypeDescriptionProvider.CreateInstance(type, callingType, ref flag2);
					if (flag2)
					{
						ReflectTypeDescriptionProvider.IntrinsicTypeConverters[callingType] = obj;
					}
				}
			}
			return obj;
		}

		// Token: 0x04000003 RID: 3
		private static volatile Dictionary<object, object> s_intrinsicConverters;

		// Token: 0x04000004 RID: 4
		private static object s_intrinsicNullableKey = new object();

		// Token: 0x04000005 RID: 5
		private static object s_syncObject = new object();
	}
}
