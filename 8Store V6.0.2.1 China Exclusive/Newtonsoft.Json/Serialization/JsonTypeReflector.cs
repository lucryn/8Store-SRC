using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020000A1 RID: 161
	[NullableContext(1)]
	[Nullable(0)]
	internal static class JsonTypeReflector
	{
		// Token: 0x06000802 RID: 2050 RVA: 0x000226F5 File Offset: 0x000208F5
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		[return: Nullable(2)]
		public static T GetCachedAttribute<[Nullable(0)] T>(object attributeProvider) where T : Attribute
		{
			return CachedAttributeGetter<T>.GetAttribute(attributeProvider);
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x00022700 File Offset: 0x00020900
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		public static bool CanTypeDescriptorConvertString(Type type, out TypeConverter typeConverter)
		{
			typeConverter = TypeDescriptor.GetConverter(type);
			if (typeConverter != null)
			{
				Type type2 = typeConverter.GetType();
				if (!string.Equals(type2.FullName, "System.ComponentModel.ComponentConverter", 4) && !string.Equals(type2.FullName, "System.ComponentModel.ReferenceConverter", 4) && !string.Equals(type2.FullName, "System.Windows.Forms.Design.DataSourceConverter", 4) && type2 != typeof(TypeConverter))
				{
					return typeConverter.CanConvertTo(typeof(string));
				}
			}
			return false;
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0002277C File Offset: 0x0002097C
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		[return: Nullable(2)]
		public static DataContractAttribute GetDataContractAttribute(Type type)
		{
			for (Type type2 = type; type2 != null; type2 = type2.BaseType())
			{
				DataContractAttribute attribute = CachedAttributeGetter<DataContractAttribute>.GetAttribute(type2);
				if (attribute != null)
				{
					return attribute;
				}
			}
			return null;
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x000227A4 File Offset: 0x000209A4
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		[return: Nullable(2)]
		public static DataMemberAttribute GetDataMemberAttribute(MemberInfo memberInfo)
		{
			if (memberInfo.MemberType() == MemberTypes.Field)
			{
				return CachedAttributeGetter<DataMemberAttribute>.GetAttribute(memberInfo);
			}
			PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
			DataMemberAttribute attribute = CachedAttributeGetter<DataMemberAttribute>.GetAttribute(propertyInfo);
			if (attribute == null && propertyInfo.IsVirtual())
			{
				Type type = propertyInfo.DeclaringType;
				while (attribute == null && type != null)
				{
					PropertyInfo propertyInfo2 = (PropertyInfo)ReflectionUtils.GetMemberInfoFromType(type, propertyInfo);
					if (propertyInfo2 != null && propertyInfo2.IsVirtual())
					{
						attribute = CachedAttributeGetter<DataMemberAttribute>.GetAttribute(propertyInfo2);
					}
					type = type.BaseType();
				}
			}
			return attribute;
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00022810 File Offset: 0x00020A10
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public static MemberSerialization GetObjectMemberSerialization(Type objectType, bool ignoreSerializableAttribute)
		{
			JsonObjectAttribute cachedAttribute = JsonTypeReflector.GetCachedAttribute<JsonObjectAttribute>(objectType);
			if (cachedAttribute != null)
			{
				return cachedAttribute.MemberSerialization;
			}
			if (JsonTypeReflector.GetDataContractAttribute(objectType) != null)
			{
				return MemberSerialization.OptIn;
			}
			return MemberSerialization.OptOut;
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0002283C File Offset: 0x00020A3C
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		[return: Nullable(2)]
		public static JsonConverter GetJsonConverter(object attributeProvider)
		{
			JsonConverterAttribute cachedAttribute = JsonTypeReflector.GetCachedAttribute<JsonConverterAttribute>(attributeProvider);
			if (cachedAttribute != null)
			{
				Func<object[], object> func = JsonTypeReflector.CreatorCache.Instance.Get(cachedAttribute.ConverterType);
				if (func != null)
				{
					return (JsonConverter)func.Invoke(cachedAttribute.ConverterParameters);
				}
			}
			return null;
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0002287A File Offset: 0x00020A7A
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public static JsonConverter CreateJsonConverterInstance(Type converterType, [Nullable(new byte[]
		{
			2,
			1
		})] object[] args)
		{
			return (JsonConverter)JsonTypeReflector.CreatorCache.Instance.Get(converterType).Invoke(args);
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00022892 File Offset: 0x00020A92
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public static NamingStrategy CreateNamingStrategyInstance(Type namingStrategyType, [Nullable(new byte[]
		{
			2,
			1
		})] object[] args)
		{
			return (NamingStrategy)JsonTypeReflector.CreatorCache.Instance.Get(namingStrategyType).Invoke(args);
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x000228AA File Offset: 0x00020AAA
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		[return: Nullable(2)]
		public static NamingStrategy GetContainerNamingStrategy(JsonContainerAttribute containerAttribute)
		{
			if (containerAttribute.NamingStrategyInstance == null)
			{
				if (containerAttribute.NamingStrategyType == null)
				{
					return null;
				}
				containerAttribute.NamingStrategyInstance = JsonTypeReflector.CreateNamingStrategyInstance(containerAttribute.NamingStrategyType, containerAttribute.NamingStrategyParameters);
			}
			return containerAttribute.NamingStrategyInstance;
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x000228DC File Offset: 0x00020ADC
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		[return: Nullable(new byte[]
		{
			1,
			2,
			1,
			1
		})]
		private static Func<object[], object> GetCreator([DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)] Type type)
		{
			Func<object> defaultConstructor = ReflectionUtils.HasDefaultConstructor(type, false) ? JsonTypeReflector.ReflectionDelegateFactory.CreateDefaultConstructor<object>(type) : null;
			return delegate([Nullable(new byte[]
			{
				2,
				1
			})] object[] parameters)
			{
				object result;
				try
				{
					if (parameters != null)
					{
						Type[] parameterTypes = Enumerable.ToArray<Type>(Enumerable.Select<object, Type>(parameters, delegate(object param)
						{
							if (param == null)
							{
								throw new InvalidOperationException("Cannot pass a null parameter to the constructor.");
							}
							return param.GetType();
						}));
						ConstructorInfo constructor = type.GetConstructor(parameterTypes);
						if (constructor == null)
						{
							throw new JsonException("No matching parameterized constructor found for '{0}'.".FormatWith(CultureInfo.InvariantCulture, type));
						}
						result = JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(constructor)(parameters);
					}
					else
					{
						if (defaultConstructor == null)
						{
							throw new JsonException("No parameterless constructor defined for '{0}'.".FormatWith(CultureInfo.InvariantCulture, type));
						}
						result = defaultConstructor.Invoke();
					}
				}
				catch (Exception innerException)
				{
					throw new JsonException("Error creating '{0}'.".FormatWith(CultureInfo.InvariantCulture, type), innerException);
				}
				return result;
			};
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00022929 File Offset: 0x00020B29
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		[return: Nullable(2)]
		private static Type GetAssociatedMetadataType(Type type)
		{
			return JsonTypeReflector.AssociatedMetadataTypesCache.Instance.Get(type);
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00022938 File Offset: 0x00020B38
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		[return: Nullable(2)]
		private static Type GetAssociateMetadataTypeFromAttribute(Type type)
		{
			foreach (Attribute attribute in ReflectionUtils.GetAttributes(type, null, true))
			{
				Type type2 = attribute.GetType();
				if (string.Equals(type2.FullName, "System.ComponentModel.DataAnnotations.MetadataTypeAttribute", 4))
				{
					if (JsonTypeReflector._metadataTypeAttributeReflectionObject == null)
					{
						JsonTypeReflector._metadataTypeAttributeReflectionObject = ReflectionObject.Create(type2, new string[]
						{
							"MetadataClassType"
						});
					}
					return (Type)JsonTypeReflector._metadataTypeAttributeReflectionObject.GetValue(attribute, "MetadataClassType");
				}
			}
			return null;
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x000229B4 File Offset: 0x00020BB4
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		[return: Nullable(2)]
		private static T GetAttribute<[Nullable(0)] T>([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] Type type) where T : Attribute
		{
			Type associatedMetadataType = JsonTypeReflector.GetAssociatedMetadataType(type);
			T attribute;
			if (associatedMetadataType != null)
			{
				attribute = ReflectionUtils.GetAttribute<T>(associatedMetadataType, true);
				if (attribute != null)
				{
					return attribute;
				}
			}
			attribute = ReflectionUtils.GetAttribute<T>(type, true);
			if (attribute != null)
			{
				return attribute;
			}
			foreach (Type attributeProvider in type.GetInterfaces())
			{
				attribute = ReflectionUtils.GetAttribute<T>(attributeProvider, true);
				if (attribute != null)
				{
					return attribute;
				}
			}
			return default(T);
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x00022A48 File Offset: 0x00020C48
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		[return: Nullable(2)]
		private static T GetAttribute<[Nullable(0)] T>(MemberInfo memberInfo) where T : Attribute
		{
			Type associatedMetadataType = JsonTypeReflector.GetAssociatedMetadataType(memberInfo.DeclaringType);
			T attribute;
			if (associatedMetadataType != null)
			{
				MemberInfo memberInfoFromType = ReflectionUtils.GetMemberInfoFromType(associatedMetadataType, memberInfo);
				if (memberInfoFromType != null)
				{
					attribute = ReflectionUtils.GetAttribute<T>(memberInfoFromType, true);
					if (attribute != null)
					{
						return attribute;
					}
				}
			}
			attribute = ReflectionUtils.GetAttribute<T>(memberInfo, true);
			if (attribute != null)
			{
				return attribute;
			}
			if (memberInfo.DeclaringType != null)
			{
				foreach (Type targetType in memberInfo.DeclaringType.GetInterfaces())
				{
					MemberInfo memberInfoFromType2 = ReflectionUtils.GetMemberInfoFromType(targetType, memberInfo);
					if (memberInfoFromType2 != null)
					{
						attribute = ReflectionUtils.GetAttribute<T>(memberInfoFromType2, true);
						if (attribute != null)
						{
							return attribute;
						}
					}
				}
			}
			return default(T);
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00022B08 File Offset: 0x00020D08
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		[return: Nullable(2)]
		public static T GetAttribute<[Nullable(0)] T>(object provider) where T : Attribute
		{
			Type type = provider as Type;
			if (type != null)
			{
				return JsonTypeReflector.GetAttribute<T>(type);
			}
			MemberInfo memberInfo = provider as MemberInfo;
			if (memberInfo != null)
			{
				return JsonTypeReflector.GetAttribute<T>(memberInfo);
			}
			return ReflectionUtils.GetAttribute<T>(provider, true);
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x00022B3E File Offset: 0x00020D3E
		public static bool DynamicCodeGeneration
		{
			get
			{
				if (JsonTypeReflector._dynamicCodeGeneration == null)
				{
					JsonTypeReflector._dynamicCodeGeneration = new bool?(false);
				}
				return JsonTypeReflector._dynamicCodeGeneration.GetValueOrDefault();
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x00022B61 File Offset: 0x00020D61
		public static bool FullyTrusted
		{
			get
			{
				if (JsonTypeReflector._fullyTrusted == null)
				{
					JsonTypeReflector._fullyTrusted = new bool?(true);
				}
				return JsonTypeReflector._fullyTrusted.GetValueOrDefault();
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x00022B84 File Offset: 0x00020D84
		public static ReflectionDelegateFactory ReflectionDelegateFactory
		{
			get
			{
				return ExpressionReflectionDelegateFactory.Instance;
			}
		}

		// Token: 0x04000309 RID: 777
		private static bool? _dynamicCodeGeneration;

		// Token: 0x0400030A RID: 778
		private static bool? _fullyTrusted;

		// Token: 0x0400030B RID: 779
		public const string IdPropertyName = "$id";

		// Token: 0x0400030C RID: 780
		public const string RefPropertyName = "$ref";

		// Token: 0x0400030D RID: 781
		public const string TypePropertyName = "$type";

		// Token: 0x0400030E RID: 782
		public const string ValuePropertyName = "$value";

		// Token: 0x0400030F RID: 783
		public const string ArrayValuesPropertyName = "$values";

		// Token: 0x04000310 RID: 784
		public const string ShouldSerializePrefix = "ShouldSerialize";

		// Token: 0x04000311 RID: 785
		public const string SpecifiedPostfix = "Specified";

		// Token: 0x04000312 RID: 786
		public const string ConcurrentDictionaryTypeName = "System.Collections.Concurrent.ConcurrentDictionary`2";

		// Token: 0x04000313 RID: 787
		[Nullable(2)]
		private static ReflectionObject _metadataTypeAttributeReflectionObject;

		// Token: 0x020001B6 RID: 438
		[NullableContext(0)]
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		private static class CreatorCache
		{
			// Token: 0x0400077A RID: 1914
			[Nullable(new byte[]
			{
				1,
				1,
				1,
				2,
				1,
				1
			})]
			internal static readonly ThreadSafeStore<Type, Func<object[], object>> Instance = new ThreadSafeStore<Type, Func<object[], object>>(new Func<Type, Func<object[], object>>(JsonTypeReflector.GetCreator));
		}

		// Token: 0x020001B7 RID: 439
		[NullableContext(0)]
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		private static class AssociatedMetadataTypesCache
		{
			// Token: 0x0400077B RID: 1915
			[Nullable(new byte[]
			{
				1,
				1,
				2
			})]
			internal static readonly ThreadSafeStore<Type, Type> Instance = new ThreadSafeStore<Type, Type>(new Func<Type, Type>(JsonTypeReflector.GetAssociateMetadataTypeFromAttribute));
		}
	}
}
