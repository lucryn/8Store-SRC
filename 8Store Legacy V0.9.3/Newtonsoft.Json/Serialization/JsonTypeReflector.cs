using System;
using System.Reflection;
using System.Runtime.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020000A4 RID: 164
	internal static class JsonTypeReflector
	{
		// Token: 0x0600087B RID: 2171 RVA: 0x00020B35 File Offset: 0x0001ED35
		public static JsonContainerAttribute GetJsonContainerAttribute(Type type)
		{
			return CachedAttributeGetter<JsonContainerAttribute>.GetAttribute(type);
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00020B3D File Offset: 0x0001ED3D
		public static JsonObjectAttribute GetJsonObjectAttribute(Type type)
		{
			return JsonTypeReflector.GetJsonContainerAttribute(type) as JsonObjectAttribute;
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00020B4A File Offset: 0x0001ED4A
		public static JsonArrayAttribute GetJsonArrayAttribute(Type type)
		{
			return JsonTypeReflector.GetJsonContainerAttribute(type) as JsonArrayAttribute;
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00020B57 File Offset: 0x0001ED57
		public static JsonDictionaryAttribute GetJsonDictionaryAttribute(Type type)
		{
			return JsonTypeReflector.GetJsonContainerAttribute(type) as JsonDictionaryAttribute;
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00020B64 File Offset: 0x0001ED64
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

		// Token: 0x06000880 RID: 2176 RVA: 0x00020B8C File Offset: 0x0001ED8C
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

		// Token: 0x06000881 RID: 2177 RVA: 0x00020BF8 File Offset: 0x0001EDF8
		public static MemberSerialization GetObjectMemberSerialization(Type objectType, bool ignoreSerializableAttribute)
		{
			JsonObjectAttribute jsonObjectAttribute = JsonTypeReflector.GetJsonObjectAttribute(objectType);
			if (jsonObjectAttribute != null)
			{
				return jsonObjectAttribute.MemberSerialization;
			}
			DataContractAttribute dataContractAttribute = JsonTypeReflector.GetDataContractAttribute(objectType);
			if (dataContractAttribute != null)
			{
				return MemberSerialization.OptIn;
			}
			return MemberSerialization.OptOut;
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00020C23 File Offset: 0x0001EE23
		private static Type GetJsonConverterType(object attributeProvider)
		{
			return JsonTypeReflector.JsonConverterTypeCache.Get(attributeProvider);
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00020C30 File Offset: 0x0001EE30
		private static Type GetJsonConverterTypeFromAttribute(object attributeProvider)
		{
			JsonConverterAttribute attribute = JsonTypeReflector.GetAttribute<JsonConverterAttribute>(attributeProvider);
			if (attribute == null)
			{
				return null;
			}
			return attribute.ConverterType;
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x00020C50 File Offset: 0x0001EE50
		public static JsonConverter GetJsonConverter(object attributeProvider, Type targetConvertedType)
		{
			Type jsonConverterType = JsonTypeReflector.GetJsonConverterType(attributeProvider);
			if (jsonConverterType != null)
			{
				return JsonConverterAttribute.CreateJsonConverterInstance(jsonConverterType);
			}
			return null;
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00020C74 File Offset: 0x0001EE74
		private static T GetAttribute<T>(Type type) where T : Attribute
		{
			T attribute = ReflectionUtils.GetAttribute<T>(type, true);
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

		// Token: 0x06000886 RID: 2182 RVA: 0x00020CEC File Offset: 0x0001EEEC
		private static T GetAttribute<T>(MemberInfo memberInfo) where T : Attribute
		{
			T attribute = ReflectionUtils.GetAttribute<T>(memberInfo, true);
			if (attribute != null)
			{
				return attribute;
			}
			if (memberInfo.DeclaringType != null)
			{
				foreach (Type targetType in memberInfo.DeclaringType.GetInterfaces())
				{
					MemberInfo memberInfoFromType = ReflectionUtils.GetMemberInfoFromType(targetType, memberInfo);
					if (memberInfoFromType != null)
					{
						attribute = ReflectionUtils.GetAttribute<T>(memberInfoFromType, true);
						if (attribute != null)
						{
							return attribute;
						}
					}
				}
			}
			return default(T);
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00020D84 File Offset: 0x0001EF84
		public static T GetAttribute<T>(object provider) where T : Attribute
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

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000888 RID: 2184 RVA: 0x00020DBA File Offset: 0x0001EFBA
		public static bool DynamicCodeGeneration
		{
			get
			{
				if (JsonTypeReflector._dynamicCodeGeneration == null)
				{
					JsonTypeReflector._dynamicCodeGeneration = new bool?(false);
				}
				return JsonTypeReflector._dynamicCodeGeneration.Value;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x00020DDD File Offset: 0x0001EFDD
		public static bool FullyTrusted
		{
			get
			{
				if (JsonTypeReflector._fullyTrusted == null)
				{
					JsonTypeReflector._fullyTrusted = new bool?(false);
				}
				return JsonTypeReflector._fullyTrusted.Value;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x00020E00 File Offset: 0x0001F000
		public static ReflectionDelegateFactory ReflectionDelegateFactory
		{
			get
			{
				return ExpressionReflectionDelegateFactory.Instance;
			}
		}

		// Token: 0x04000304 RID: 772
		public const string IdPropertyName = "$id";

		// Token: 0x04000305 RID: 773
		public const string RefPropertyName = "$ref";

		// Token: 0x04000306 RID: 774
		public const string TypePropertyName = "$type";

		// Token: 0x04000307 RID: 775
		public const string ValuePropertyName = "$value";

		// Token: 0x04000308 RID: 776
		public const string ArrayValuesPropertyName = "$values";

		// Token: 0x04000309 RID: 777
		public const string ShouldSerializePrefix = "ShouldSerialize";

		// Token: 0x0400030A RID: 778
		public const string SpecifiedPostfix = "Specified";

		// Token: 0x0400030B RID: 779
		private static bool? _dynamicCodeGeneration;

		// Token: 0x0400030C RID: 780
		private static bool? _fullyTrusted;

		// Token: 0x0400030D RID: 781
		private static readonly ThreadSafeStore<object, Type> JsonConverterTypeCache = new ThreadSafeStore<object, Type>(new Func<object, Type>(JsonTypeReflector.GetJsonConverterTypeFromAttribute));
	}
}
