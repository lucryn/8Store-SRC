using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	/// <summary>
	/// Used by <see cref="T:Newtonsoft.Json.JsonSerializer" /> to resolves a <see cref="T:Newtonsoft.Json.Serialization.JsonContract" /> for a given <see cref="T:System.Type" />.
	/// </summary>
	// Token: 0x02000083 RID: 131
	public class DefaultContractResolver : IContractResolver
	{
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x00018FE0 File Offset: 0x000171E0
		internal static IContractResolver Instance
		{
			get
			{
				return DefaultContractResolver._instance;
			}
		}

		/// <summary>
		/// Gets a value indicating whether members are being get and set using dynamic code generation.
		/// This value is determined by the runtime permissions available.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if using dynamic code generation; otherwise, <c>false</c>.
		/// </value>
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x00018FE7 File Offset: 0x000171E7
		public bool DynamicCodeGeneration
		{
			get
			{
				return JsonTypeReflector.DynamicCodeGeneration;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether compiler generated members should be serialized.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if serialized compiler generated members; otherwise, <c>false</c>.
		/// </value>
		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x00018FEE File Offset: 0x000171EE
		// (set) Token: 0x060006CD RID: 1741 RVA: 0x00018FF6 File Offset: 0x000171F6
		public bool SerializeCompilerGeneratedMembers { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Serialization.DefaultContractResolver" /> class.
		/// </summary>
		// Token: 0x060006CE RID: 1742 RVA: 0x00018FFF File Offset: 0x000171FF
		public DefaultContractResolver() : this(false)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Serialization.DefaultContractResolver" /> class.
		/// </summary>
		/// <param name="shareCache">
		/// If set to <c>true</c> the <see cref="T:Newtonsoft.Json.Serialization.DefaultContractResolver" /> will use a cached shared with other resolvers of the same type.
		/// Sharing the cache will significantly performance because expensive reflection will only happen once but could cause unexpected
		/// behavior if different instances of the resolver are suppose to produce different results. When set to false it is highly
		/// recommended to reuse <see cref="T:Newtonsoft.Json.Serialization.DefaultContractResolver" /> instances with the <see cref="T:Newtonsoft.Json.JsonSerializer" />.
		/// </param>
		// Token: 0x060006CF RID: 1743 RVA: 0x00019008 File Offset: 0x00017208
		public DefaultContractResolver(bool shareCache)
		{
			this._sharedCache = shareCache;
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0001901F File Offset: 0x0001721F
		private Dictionary<ResolverContractKey, JsonContract> GetCache()
		{
			if (this._sharedCache)
			{
				return DefaultContractResolver._sharedContractCache;
			}
			return this._instanceContractCache;
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00019035 File Offset: 0x00017235
		private void UpdateCache(Dictionary<ResolverContractKey, JsonContract> cache)
		{
			if (this._sharedCache)
			{
				DefaultContractResolver._sharedContractCache = cache;
				return;
			}
			this._instanceContractCache = cache;
		}

		/// <summary>
		/// Resolves the contract for a given type.
		/// </summary>
		/// <param name="type">The type to resolve a contract for.</param>
		/// <returns>The contract for a given type.</returns>
		// Token: 0x060006D2 RID: 1746 RVA: 0x00019050 File Offset: 0x00017250
		public virtual JsonContract ResolveContract(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			ResolverContractKey resolverContractKey = new ResolverContractKey(base.GetType(), type);
			Dictionary<ResolverContractKey, JsonContract> cache = this.GetCache();
			JsonContract jsonContract;
			if (cache == null || !cache.TryGetValue(resolverContractKey, ref jsonContract))
			{
				jsonContract = this.CreateContract(type);
				lock (DefaultContractResolver._typeContractCacheLock)
				{
					cache = this.GetCache();
					Dictionary<ResolverContractKey, JsonContract> dictionary = (cache != null) ? new Dictionary<ResolverContractKey, JsonContract>(cache) : new Dictionary<ResolverContractKey, JsonContract>();
					dictionary[resolverContractKey] = jsonContract;
					this.UpdateCache(dictionary);
				}
			}
			return jsonContract;
		}

		/// <summary>
		/// Gets the serializable members for the type.
		/// </summary>
		/// <param name="objectType">The type to get serializable members for.</param>
		/// <returns>The serializable members for the type.</returns>
		// Token: 0x060006D3 RID: 1747 RVA: 0x00019108 File Offset: 0x00017308
		protected virtual List<MemberInfo> GetSerializableMembers(Type objectType)
		{
			bool ignoreSerializableAttribute = true;
			MemberSerialization objectMemberSerialization = JsonTypeReflector.GetObjectMemberSerialization(objectType, ignoreSerializableAttribute);
			List<MemberInfo> list = Enumerable.ToList<MemberInfo>(Enumerable.Where<MemberInfo>(ReflectionUtils.GetFieldsAndProperties(objectType, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic), (MemberInfo m) => !ReflectionUtils.IsIndexedProperty(m)));
			List<MemberInfo> list2 = new List<MemberInfo>();
			if (objectMemberSerialization != MemberSerialization.Fields)
			{
				DataContractAttribute dataContractAttribute = JsonTypeReflector.GetDataContractAttribute(objectType);
				List<MemberInfo> list3 = Enumerable.ToList<MemberInfo>(Enumerable.Where<MemberInfo>(ReflectionUtils.GetFieldsAndProperties(objectType, this.DefaultMembersSearchFlags), (MemberInfo m) => !ReflectionUtils.IsIndexedProperty(m)));
				foreach (MemberInfo memberInfo in list)
				{
					if (this.SerializeCompilerGeneratedMembers || !CustomAttributeExtensions.IsDefined(memberInfo, typeof(CompilerGeneratedAttribute), true))
					{
						if (list3.Contains(memberInfo))
						{
							list2.Add(memberInfo);
						}
						else if (JsonTypeReflector.GetAttribute<JsonPropertyAttribute>(memberInfo) != null)
						{
							list2.Add(memberInfo);
						}
						else if (dataContractAttribute != null && JsonTypeReflector.GetAttribute<DataMemberAttribute>(memberInfo) != null)
						{
							list2.Add(memberInfo);
						}
						else if (objectMemberSerialization == MemberSerialization.Fields && memberInfo.MemberType() == MemberTypes.Field)
						{
							list2.Add(memberInfo);
						}
					}
				}
				Type type;
				if (objectType.AssignableToTypeName("System.Data.Objects.DataClasses.EntityObject", out type))
				{
					list2 = Enumerable.ToList<MemberInfo>(Enumerable.Where<MemberInfo>(list2, new Func<MemberInfo, bool>(this.ShouldSerializeEntityMember)));
				}
			}
			else
			{
				foreach (MemberInfo memberInfo2 in list)
				{
					FieldInfo fieldInfo = memberInfo2 as FieldInfo;
					if (fieldInfo != null && !fieldInfo.IsStatic)
					{
						list2.Add(memberInfo2);
					}
				}
			}
			return list2;
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x000192CC File Offset: 0x000174CC
		private bool ShouldSerializeEntityMember(MemberInfo memberInfo)
		{
			PropertyInfo propertyInfo = memberInfo as PropertyInfo;
			return propertyInfo == null || !propertyInfo.PropertyType.IsGenericType() || !(propertyInfo.PropertyType.GetGenericTypeDefinition().FullName == "System.Data.Objects.DataClasses.EntityReference`1");
		}

		/// <summary>
		/// Creates a <see cref="T:Newtonsoft.Json.Serialization.JsonObjectContract" /> for the given type.
		/// </summary>
		/// <param name="objectType">Type of the object.</param>
		/// <returns>A <see cref="T:Newtonsoft.Json.Serialization.JsonObjectContract" /> for the given type.</returns>
		// Token: 0x060006D5 RID: 1749 RVA: 0x00019310 File Offset: 0x00017510
		protected virtual JsonObjectContract CreateObjectContract(Type objectType)
		{
			JsonObjectContract jsonObjectContract = new JsonObjectContract(objectType);
			this.InitializeContract(jsonObjectContract);
			bool ignoreSerializableAttribute = true;
			jsonObjectContract.MemberSerialization = JsonTypeReflector.GetObjectMemberSerialization(jsonObjectContract.NonNullableUnderlyingType, ignoreSerializableAttribute);
			jsonObjectContract.Properties.AddRange(this.CreateProperties(jsonObjectContract.NonNullableUnderlyingType, jsonObjectContract.MemberSerialization));
			JsonObjectAttribute jsonObjectAttribute = JsonTypeReflector.GetJsonObjectAttribute(jsonObjectContract.NonNullableUnderlyingType);
			if (jsonObjectAttribute != null)
			{
				jsonObjectContract.ItemRequired = jsonObjectAttribute._itemRequired;
			}
			ConstructorInfo attributeConstructor = this.GetAttributeConstructor(jsonObjectContract.NonNullableUnderlyingType);
			if (attributeConstructor != null)
			{
				jsonObjectContract.OverrideConstructor = attributeConstructor;
				jsonObjectContract.ConstructorParameters.AddRange(this.CreateConstructorParameters(attributeConstructor, jsonObjectContract.Properties));
			}
			else if (jsonObjectContract.MemberSerialization != MemberSerialization.Fields && (jsonObjectContract.DefaultCreator == null || jsonObjectContract.DefaultCreatorNonPublic))
			{
				ConstructorInfo parametrizedConstructor = this.GetParametrizedConstructor(jsonObjectContract.NonNullableUnderlyingType);
				if (parametrizedConstructor != null)
				{
					jsonObjectContract.ParametrizedConstructor = parametrizedConstructor;
					jsonObjectContract.ConstructorParameters.AddRange(this.CreateConstructorParameters(parametrizedConstructor, jsonObjectContract.Properties));
				}
			}
			jsonObjectContract.ExtensionDataSetter = this.GetExtensionDataForType(jsonObjectContract.NonNullableUnderlyingType);
			return jsonObjectContract;
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00019464 File Offset: 0x00017664
		private ExtensionDataSetter GetExtensionDataForType(Type type)
		{
			ExtensionDataSetter result = null;
			foreach (Type type2 in this.GetClassHierarchyForType(type))
			{
				IList<MemberInfo> list = new List<MemberInfo>();
				list.AddRange(type2.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic));
				list.AddRange(type2.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic));
				foreach (MemberInfo memberInfo in list)
				{
					MemberTypes memberTypes = memberInfo.MemberType();
					if ((memberTypes == MemberTypes.Property || memberTypes == MemberTypes.Field) && CustomAttributeExtensions.IsDefined(memberInfo, typeof(JsonExtensionDataAttribute), false))
					{
						Type type3 = ReflectionUtils.GetMemberUnderlyingType(memberInfo);
						Type type4;
						if (ReflectionUtils.ImplementsGenericDefinition(type3, typeof(IDictionary), out type4))
						{
							Type type5 = type4.GetGenericArguments()[0];
							Type type6 = type4.GetGenericArguments()[1];
							if (ReflectionUtils.IsGenericDefinition(type3, typeof(IDictionary)))
							{
								type3 = typeof(Dictionary).MakeGenericType(new Type[]
								{
									type5,
									type6
								});
							}
							if (type5.IsAssignableFrom(typeof(string)) && type6.IsAssignableFrom(typeof(JToken)))
							{
								MethodInfo method = type3.GetMethod("Add", new Type[]
								{
									type5,
									type6
								});
								Func<object, object> getExtensionDataDictionary = JsonTypeReflector.ReflectionDelegateFactory.CreateGet<object>(memberInfo);
								Action<object, object> setExtensionDataDictionary = JsonTypeReflector.ReflectionDelegateFactory.CreateSet<object>(memberInfo);
								Func<object> createExtensionDataDictionary = JsonTypeReflector.ReflectionDelegateFactory.CreateDefaultConstructor<object>(type3);
								MethodCall<object, object> setExtensionDataDictionaryValue = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(method);
								result = delegate(object o, string key, JToken value)
								{
									object obj = getExtensionDataDictionary.Invoke(o);
									if (obj == null)
									{
										obj = createExtensionDataDictionary.Invoke();
										setExtensionDataDictionary.Invoke(o, obj);
									}
									setExtensionDataDictionaryValue(obj, new object[]
									{
										key,
										value
									});
								};
								continue;
							}
						}
						throw new JsonException("Invalid extension data attribute on '{0}'. Member '{1}' type must implement IDictionary<string, JToken>.".FormatWith(CultureInfo.InvariantCulture, DefaultContractResolver.GetClrTypeFullName(memberInfo.DeclaringType), memberInfo.Name));
					}
				}
			}
			return result;
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x000196B8 File Offset: 0x000178B8
		private ConstructorInfo GetAttributeConstructor(Type objectType)
		{
			IList<ConstructorInfo> list = Enumerable.ToList<ConstructorInfo>(Enumerable.Where<ConstructorInfo>(objectType.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic), (ConstructorInfo c) => CustomAttributeExtensions.IsDefined(c, typeof(JsonConstructorAttribute), true)));
			if (list.Count > 1)
			{
				throw new JsonException("Multiple constructors with the JsonConstructorAttribute.");
			}
			if (list.Count == 1)
			{
				return list[0];
			}
			if (objectType == typeof(Version))
			{
				return objectType.GetConstructor(new Type[]
				{
					typeof(int),
					typeof(int),
					typeof(int),
					typeof(int)
				});
			}
			return null;
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0001976C File Offset: 0x0001796C
		private ConstructorInfo GetParametrizedConstructor(Type objectType)
		{
			IList<ConstructorInfo> list = Enumerable.ToList<ConstructorInfo>(objectType.GetConstructors(BindingFlags.Instance | BindingFlags.Public));
			if (list.Count == 1)
			{
				return list[0];
			}
			return null;
		}

		/// <summary>
		/// Creates the constructor parameters.
		/// </summary>
		/// <param name="constructor">The constructor to create properties for.</param>
		/// <param name="memberProperties">The type's member properties.</param>
		/// <returns>Properties for the given <see cref="T:System.Reflection.ConstructorInfo" />.</returns>
		// Token: 0x060006D9 RID: 1753 RVA: 0x0001979C File Offset: 0x0001799C
		protected virtual IList<JsonProperty> CreateConstructorParameters(ConstructorInfo constructor, JsonPropertyCollection memberProperties)
		{
			ParameterInfo[] parameters = constructor.GetParameters();
			JsonPropertyCollection jsonPropertyCollection = new JsonPropertyCollection(constructor.DeclaringType);
			foreach (ParameterInfo parameterInfo in parameters)
			{
				JsonProperty jsonProperty = (parameterInfo.Name != null) ? memberProperties.GetClosestMatchProperty(parameterInfo.Name) : null;
				if (jsonProperty != null && jsonProperty.PropertyType != parameterInfo.ParameterType)
				{
					jsonProperty = null;
				}
				JsonProperty jsonProperty2 = this.CreatePropertyFromConstructorParameter(jsonProperty, parameterInfo);
				if (jsonProperty2 != null)
				{
					jsonPropertyCollection.AddProperty(jsonProperty2);
				}
			}
			return jsonPropertyCollection;
		}

		/// <summary>
		/// Creates a <see cref="T:Newtonsoft.Json.Serialization.JsonProperty" /> for the given <see cref="T:System.Reflection.ParameterInfo" />.
		/// </summary>
		/// <param name="matchingMemberProperty">The matching member property.</param>
		/// <param name="parameterInfo">The constructor parameter.</param>
		/// <returns>A created <see cref="T:Newtonsoft.Json.Serialization.JsonProperty" /> for the given <see cref="T:System.Reflection.ParameterInfo" />.</returns>
		// Token: 0x060006DA RID: 1754 RVA: 0x0001981C File Offset: 0x00017A1C
		protected virtual JsonProperty CreatePropertyFromConstructorParameter(JsonProperty matchingMemberProperty, ParameterInfo parameterInfo)
		{
			JsonProperty jsonProperty = new JsonProperty();
			jsonProperty.PropertyType = parameterInfo.ParameterType;
			bool flag;
			this.SetPropertySettingsFromAttributes(jsonProperty, parameterInfo, parameterInfo.Name, parameterInfo.Member.DeclaringType, MemberSerialization.OptOut, out flag);
			jsonProperty.Readable = false;
			jsonProperty.Writable = true;
			if (matchingMemberProperty != null)
			{
				jsonProperty.PropertyName = ((jsonProperty.PropertyName != parameterInfo.Name) ? jsonProperty.PropertyName : matchingMemberProperty.PropertyName);
				jsonProperty.Converter = (jsonProperty.Converter ?? matchingMemberProperty.Converter);
				jsonProperty.MemberConverter = (jsonProperty.MemberConverter ?? matchingMemberProperty.MemberConverter);
				if (!jsonProperty._hasExplicitDefaultValue && matchingMemberProperty._hasExplicitDefaultValue)
				{
					jsonProperty.DefaultValue = matchingMemberProperty.DefaultValue;
				}
				JsonProperty jsonProperty2 = jsonProperty;
				Required? required = jsonProperty._required;
				jsonProperty2._required = ((required != null) ? new Required?(required.GetValueOrDefault()) : matchingMemberProperty._required);
				JsonProperty jsonProperty3 = jsonProperty;
				bool? isReference = jsonProperty.IsReference;
				jsonProperty3.IsReference = ((isReference != null) ? new bool?(isReference.GetValueOrDefault()) : matchingMemberProperty.IsReference);
				JsonProperty jsonProperty4 = jsonProperty;
				NullValueHandling? nullValueHandling = jsonProperty.NullValueHandling;
				jsonProperty4.NullValueHandling = ((nullValueHandling != null) ? new NullValueHandling?(nullValueHandling.GetValueOrDefault()) : matchingMemberProperty.NullValueHandling);
				JsonProperty jsonProperty5 = jsonProperty;
				DefaultValueHandling? defaultValueHandling = jsonProperty.DefaultValueHandling;
				jsonProperty5.DefaultValueHandling = ((defaultValueHandling != null) ? new DefaultValueHandling?(defaultValueHandling.GetValueOrDefault()) : matchingMemberProperty.DefaultValueHandling);
				JsonProperty jsonProperty6 = jsonProperty;
				ReferenceLoopHandling? referenceLoopHandling = jsonProperty.ReferenceLoopHandling;
				jsonProperty6.ReferenceLoopHandling = ((referenceLoopHandling != null) ? new ReferenceLoopHandling?(referenceLoopHandling.GetValueOrDefault()) : matchingMemberProperty.ReferenceLoopHandling);
				JsonProperty jsonProperty7 = jsonProperty;
				ObjectCreationHandling? objectCreationHandling = jsonProperty.ObjectCreationHandling;
				jsonProperty7.ObjectCreationHandling = ((objectCreationHandling != null) ? new ObjectCreationHandling?(objectCreationHandling.GetValueOrDefault()) : matchingMemberProperty.ObjectCreationHandling);
				JsonProperty jsonProperty8 = jsonProperty;
				TypeNameHandling? typeNameHandling = jsonProperty.TypeNameHandling;
				jsonProperty8.TypeNameHandling = ((typeNameHandling != null) ? new TypeNameHandling?(typeNameHandling.GetValueOrDefault()) : matchingMemberProperty.TypeNameHandling);
			}
			return jsonProperty;
		}

		/// <summary>
		/// Resolves the default <see cref="T:Newtonsoft.Json.JsonConverter" /> for the contract.
		/// </summary>
		/// <param name="objectType">Type of the object.</param>
		/// <returns>The contract's default <see cref="T:Newtonsoft.Json.JsonConverter" />.</returns>
		// Token: 0x060006DB RID: 1755 RVA: 0x00019A06 File Offset: 0x00017C06
		protected virtual JsonConverter ResolveContractConverter(Type objectType)
		{
			return JsonTypeReflector.GetJsonConverter(objectType, objectType);
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00019A0F File Offset: 0x00017C0F
		private Func<object> GetDefaultCreator(Type createdType)
		{
			return JsonTypeReflector.ReflectionDelegateFactory.CreateDefaultConstructor<object>(createdType);
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00019A1C File Offset: 0x00017C1C
		[SuppressMessage("Microsoft.Portability", "CA1903:UseOnlyApiFromTargetedFramework", MessageId = "System.Runtime.Serialization.DataContractAttribute.#get_IsReference()")]
		private void InitializeContract(JsonContract contract)
		{
			JsonContainerAttribute jsonContainerAttribute = JsonTypeReflector.GetJsonContainerAttribute(contract.NonNullableUnderlyingType);
			if (jsonContainerAttribute != null)
			{
				contract.IsReference = jsonContainerAttribute._isReference;
			}
			else
			{
				DataContractAttribute dataContractAttribute = JsonTypeReflector.GetDataContractAttribute(contract.NonNullableUnderlyingType);
				if (dataContractAttribute != null && dataContractAttribute.IsReference)
				{
					contract.IsReference = new bool?(true);
				}
			}
			contract.Converter = this.ResolveContractConverter(contract.NonNullableUnderlyingType);
			contract.InternalConverter = JsonSerializer.GetMatchingConverter(DefaultContractResolver.BuiltInConverters, contract.NonNullableUnderlyingType);
			if (ReflectionUtils.HasDefaultConstructor(contract.CreatedType, true) || contract.CreatedType.IsValueType())
			{
				contract.DefaultCreator = this.GetDefaultCreator(contract.CreatedType);
				contract.DefaultCreatorNonPublic = (!contract.CreatedType.IsValueType() && ReflectionUtils.GetDefaultConstructor(contract.CreatedType) == null);
			}
			this.ResolveCallbackMethods(contract, contract.NonNullableUnderlyingType);
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00019AF0 File Offset: 0x00017CF0
		private void ResolveCallbackMethods(JsonContract contract, Type t)
		{
			List<SerializationCallback> list;
			List<SerializationCallback> list2;
			List<SerializationCallback> list3;
			List<SerializationCallback> list4;
			List<SerializationErrorCallback> list5;
			this.GetCallbackMethodsForType(t, out list, out list2, out list3, out list4, out list5);
			if (list != null && (!t.IsGenericType() || t.GetGenericTypeDefinition() != typeof(ConcurrentDictionary)))
			{
				contract.OnSerializingCallbacks.AddRange(list);
			}
			if (list2 != null)
			{
				contract.OnSerializedCallbacks.AddRange(list2);
			}
			if (list3 != null)
			{
				contract.OnDeserializingCallbacks.AddRange(list3);
			}
			if (list4 != null && (!t.IsGenericType() || t.GetGenericTypeDefinition() != typeof(ConcurrentDictionary)))
			{
				contract.OnDeserializedCallbacks.AddRange(list4);
			}
			if (list5 != null)
			{
				contract.OnErrorCallbacks.AddRange(list5);
			}
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00019B90 File Offset: 0x00017D90
		private void GetCallbackMethodsForType(Type type, out List<SerializationCallback> onSerializing, out List<SerializationCallback> onSerialized, out List<SerializationCallback> onDeserializing, out List<SerializationCallback> onDeserialized, out List<SerializationErrorCallback> onError)
		{
			onSerializing = null;
			onSerialized = null;
			onDeserializing = null;
			onDeserialized = null;
			onError = null;
			foreach (Type type2 in this.GetClassHierarchyForType(type))
			{
				MethodInfo currentCallback = null;
				MethodInfo currentCallback2 = null;
				MethodInfo currentCallback3 = null;
				MethodInfo currentCallback4 = null;
				MethodInfo currentCallback5 = null;
				foreach (MethodInfo methodInfo in type2.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
				{
					if (!methodInfo.ContainsGenericParameters)
					{
						Type type3 = null;
						ParameterInfo[] parameters = methodInfo.GetParameters();
						if (DefaultContractResolver.IsValidCallback(methodInfo, parameters, typeof(OnSerializingAttribute), currentCallback, ref type3))
						{
							onSerializing = (onSerializing ?? new List<SerializationCallback>());
							onSerializing.Add(JsonContract.CreateSerializationCallback(methodInfo));
							currentCallback = methodInfo;
						}
						if (DefaultContractResolver.IsValidCallback(methodInfo, parameters, typeof(OnSerializedAttribute), currentCallback2, ref type3))
						{
							onSerialized = (onSerialized ?? new List<SerializationCallback>());
							onSerialized.Add(JsonContract.CreateSerializationCallback(methodInfo));
							currentCallback2 = methodInfo;
						}
						if (DefaultContractResolver.IsValidCallback(methodInfo, parameters, typeof(OnDeserializingAttribute), currentCallback3, ref type3))
						{
							onDeserializing = (onDeserializing ?? new List<SerializationCallback>());
							onDeserializing.Add(JsonContract.CreateSerializationCallback(methodInfo));
							currentCallback3 = methodInfo;
						}
						if (DefaultContractResolver.IsValidCallback(methodInfo, parameters, typeof(OnDeserializedAttribute), currentCallback4, ref type3))
						{
							onDeserialized = (onDeserialized ?? new List<SerializationCallback>());
							onDeserialized.Add(JsonContract.CreateSerializationCallback(methodInfo));
							currentCallback4 = methodInfo;
						}
						if (DefaultContractResolver.IsValidCallback(methodInfo, parameters, typeof(OnErrorAttribute), currentCallback5, ref type3))
						{
							onError = (onError ?? new List<SerializationErrorCallback>());
							onError.Add(JsonContract.CreateSerializationErrorCallback(methodInfo));
							currentCallback5 = methodInfo;
						}
					}
				}
			}
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00019D90 File Offset: 0x00017F90
		private List<Type> GetClassHierarchyForType(Type type)
		{
			List<Type> list = new List<Type>();
			Type type2 = type;
			while (type2 != null && type2 != typeof(object))
			{
				list.Add(type2);
				type2 = type2.BaseType();
			}
			list.Reverse();
			return list;
		}

		/// <summary>
		/// Creates a <see cref="T:Newtonsoft.Json.Serialization.JsonDictionaryContract" /> for the given type.
		/// </summary>
		/// <param name="objectType">Type of the object.</param>
		/// <returns>A <see cref="T:Newtonsoft.Json.Serialization.JsonDictionaryContract" /> for the given type.</returns>
		// Token: 0x060006E1 RID: 1761 RVA: 0x00019DCC File Offset: 0x00017FCC
		protected virtual JsonDictionaryContract CreateDictionaryContract(Type objectType)
		{
			JsonDictionaryContract jsonDictionaryContract = new JsonDictionaryContract(objectType);
			this.InitializeContract(jsonDictionaryContract);
			jsonDictionaryContract.PropertyNameResolver = new Func<string, string>(this.ResolvePropertyName);
			return jsonDictionaryContract;
		}

		/// <summary>
		/// Creates a <see cref="T:Newtonsoft.Json.Serialization.JsonArrayContract" /> for the given type.
		/// </summary>
		/// <param name="objectType">Type of the object.</param>
		/// <returns>A <see cref="T:Newtonsoft.Json.Serialization.JsonArrayContract" /> for the given type.</returns>
		// Token: 0x060006E2 RID: 1762 RVA: 0x00019DFC File Offset: 0x00017FFC
		protected virtual JsonArrayContract CreateArrayContract(Type objectType)
		{
			JsonArrayContract jsonArrayContract = new JsonArrayContract(objectType);
			this.InitializeContract(jsonArrayContract);
			return jsonArrayContract;
		}

		/// <summary>
		/// Creates a <see cref="T:Newtonsoft.Json.Serialization.JsonPrimitiveContract" /> for the given type.
		/// </summary>
		/// <param name="objectType">Type of the object.</param>
		/// <returns>A <see cref="T:Newtonsoft.Json.Serialization.JsonPrimitiveContract" /> for the given type.</returns>
		// Token: 0x060006E3 RID: 1763 RVA: 0x00019E18 File Offset: 0x00018018
		protected virtual JsonPrimitiveContract CreatePrimitiveContract(Type objectType)
		{
			JsonPrimitiveContract jsonPrimitiveContract = new JsonPrimitiveContract(objectType);
			this.InitializeContract(jsonPrimitiveContract);
			return jsonPrimitiveContract;
		}

		/// <summary>
		/// Creates a <see cref="T:Newtonsoft.Json.Serialization.JsonLinqContract" /> for the given type.
		/// </summary>
		/// <param name="objectType">Type of the object.</param>
		/// <returns>A <see cref="T:Newtonsoft.Json.Serialization.JsonLinqContract" /> for the given type.</returns>
		// Token: 0x060006E4 RID: 1764 RVA: 0x00019E34 File Offset: 0x00018034
		protected virtual JsonLinqContract CreateLinqContract(Type objectType)
		{
			JsonLinqContract jsonLinqContract = new JsonLinqContract(objectType);
			this.InitializeContract(jsonLinqContract);
			return jsonLinqContract;
		}

		/// <summary>
		/// Creates a <see cref="T:Newtonsoft.Json.Serialization.JsonDynamicContract" /> for the given type.
		/// </summary>
		/// <param name="objectType">Type of the object.</param>
		/// <returns>A <see cref="T:Newtonsoft.Json.Serialization.JsonDynamicContract" /> for the given type.</returns>
		// Token: 0x060006E5 RID: 1765 RVA: 0x00019E50 File Offset: 0x00018050
		protected virtual JsonDynamicContract CreateDynamicContract(Type objectType)
		{
			JsonDynamicContract jsonDynamicContract = new JsonDynamicContract(objectType);
			this.InitializeContract(jsonDynamicContract);
			jsonDynamicContract.PropertyNameResolver = new Func<string, string>(this.ResolvePropertyName);
			jsonDynamicContract.Properties.AddRange(this.CreateProperties(objectType, MemberSerialization.OptOut));
			return jsonDynamicContract;
		}

		/// <summary>
		/// Creates a <see cref="T:Newtonsoft.Json.Serialization.JsonStringContract" /> for the given type.
		/// </summary>
		/// <param name="objectType">Type of the object.</param>
		/// <returns>A <see cref="T:Newtonsoft.Json.Serialization.JsonStringContract" /> for the given type.</returns>
		// Token: 0x060006E6 RID: 1766 RVA: 0x00019E94 File Offset: 0x00018094
		protected virtual JsonStringContract CreateStringContract(Type objectType)
		{
			JsonStringContract jsonStringContract = new JsonStringContract(objectType);
			this.InitializeContract(jsonStringContract);
			return jsonStringContract;
		}

		/// <summary>
		/// Determines which contract type is created for the given type.
		/// </summary>
		/// <param name="objectType">Type of the object.</param>
		/// <returns>A <see cref="T:Newtonsoft.Json.Serialization.JsonContract" /> for the given type.</returns>
		// Token: 0x060006E7 RID: 1767 RVA: 0x00019EB0 File Offset: 0x000180B0
		protected virtual JsonContract CreateContract(Type objectType)
		{
			Type type = ReflectionUtils.EnsureNotNullableType(objectType);
			if (DefaultContractResolver.IsJsonPrimitiveType(objectType))
			{
				return this.CreatePrimitiveContract(objectType);
			}
			if (JsonTypeReflector.GetJsonObjectAttribute(type) != null)
			{
				return this.CreateObjectContract(objectType);
			}
			if (JsonTypeReflector.GetJsonArrayAttribute(type) != null)
			{
				return this.CreateArrayContract(objectType);
			}
			if (JsonTypeReflector.GetJsonDictionaryAttribute(type) != null)
			{
				return this.CreateDictionaryContract(objectType);
			}
			if (type == typeof(JToken) || type.IsSubclassOf(typeof(JToken)))
			{
				return this.CreateLinqContract(objectType);
			}
			if (CollectionUtils.IsDictionaryType(type))
			{
				return this.CreateDictionaryContract(objectType);
			}
			if (typeof(IEnumerable).IsAssignableFrom(type))
			{
				return this.CreateArrayContract(objectType);
			}
			if (DefaultContractResolver.CanConvertToString(type))
			{
				return this.CreateStringContract(objectType);
			}
			if (typeof(IDynamicMetaObjectProvider).IsAssignableFrom(type))
			{
				return this.CreateDynamicContract(objectType);
			}
			return this.CreateObjectContract(objectType);
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00019F88 File Offset: 0x00018188
		internal static bool IsJsonPrimitiveType(Type t)
		{
			PrimitiveTypeCode typeCode = ConvertUtils.GetTypeCode(t);
			return typeCode != PrimitiveTypeCode.Empty && typeCode != PrimitiveTypeCode.Object;
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00019FA8 File Offset: 0x000181A8
		internal static bool CanConvertToString(Type type)
		{
			return type == typeof(Type) || type.IsSubclassOf(typeof(Type));
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00019FCC File Offset: 0x000181CC
		private static bool IsValidCallback(MethodInfo method, ParameterInfo[] parameters, Type attributeType, MethodInfo currentCallback, ref Type prevAttributeType)
		{
			if (!CustomAttributeExtensions.IsDefined(method, attributeType, false))
			{
				return false;
			}
			if (currentCallback != null)
			{
				throw new JsonException("Invalid attribute. Both '{0}' and '{1}' in type '{2}' have '{3}'.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					method,
					currentCallback,
					DefaultContractResolver.GetClrTypeFullName(method.DeclaringType),
					attributeType
				}));
			}
			if (prevAttributeType != null)
			{
				throw new JsonException("Invalid Callback. Method '{3}' in type '{2}' has both '{0}' and '{1}'.".FormatWith(CultureInfo.InvariantCulture, new object[]
				{
					prevAttributeType,
					attributeType,
					DefaultContractResolver.GetClrTypeFullName(method.DeclaringType),
					method
				}));
			}
			if (method.IsVirtual)
			{
				throw new JsonException("Virtual Method '{0}' of type '{1}' cannot be marked with '{2}' attribute.".FormatWith(CultureInfo.InvariantCulture, method, DefaultContractResolver.GetClrTypeFullName(method.DeclaringType), attributeType));
			}
			if (method.ReturnType != typeof(void))
			{
				throw new JsonException("Serialization Callback '{1}' in type '{0}' must return void.".FormatWith(CultureInfo.InvariantCulture, DefaultContractResolver.GetClrTypeFullName(method.DeclaringType), method));
			}
			if (attributeType == typeof(OnErrorAttribute))
			{
				if (parameters == null || parameters.Length != 2 || parameters[0].ParameterType != typeof(StreamingContext) || parameters[1].ParameterType != typeof(ErrorContext))
				{
					throw new JsonException("Serialization Error Callback '{1}' in type '{0}' must have two parameters of type '{2}' and '{3}'.".FormatWith(CultureInfo.InvariantCulture, new object[]
					{
						DefaultContractResolver.GetClrTypeFullName(method.DeclaringType),
						method,
						typeof(StreamingContext),
						typeof(ErrorContext)
					}));
				}
			}
			else if (parameters == null || parameters.Length != 1 || parameters[0].ParameterType != typeof(StreamingContext))
			{
				throw new JsonException("Serialization Callback '{1}' in type '{0}' must have a single parameter of type '{2}'.".FormatWith(CultureInfo.InvariantCulture, DefaultContractResolver.GetClrTypeFullName(method.DeclaringType), method, typeof(StreamingContext)));
			}
			prevAttributeType = attributeType;
			return true;
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0001A194 File Offset: 0x00018394
		internal static string GetClrTypeFullName(Type type)
		{
			if (type.IsGenericTypeDefinition() || !type.ContainsGenericParameters())
			{
				return type.FullName;
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[]
			{
				type.Namespace,
				type.Name
			});
		}

		/// <summary>
		/// Creates properties for the given <see cref="T:Newtonsoft.Json.Serialization.JsonContract" />.
		/// </summary>
		/// <param name="type">The type to create properties for.</param>
		/// /// <param name="memberSerialization">The member serialization mode for the type.</param>
		/// <returns>Properties for the given <see cref="T:Newtonsoft.Json.Serialization.JsonContract" />.</returns>
		// Token: 0x060006EC RID: 1772 RVA: 0x0001A20C File Offset: 0x0001840C
		protected virtual IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
		{
			List<MemberInfo> serializableMembers = this.GetSerializableMembers(type);
			if (serializableMembers == null)
			{
				throw new JsonSerializationException("Null collection of seralizable members returned.");
			}
			JsonPropertyCollection jsonPropertyCollection = new JsonPropertyCollection(type);
			foreach (MemberInfo member in serializableMembers)
			{
				JsonProperty jsonProperty = this.CreateProperty(member, memberSerialization);
				if (jsonProperty != null)
				{
					jsonPropertyCollection.AddProperty(jsonProperty);
				}
			}
			return Enumerable.ToList<JsonProperty>(Enumerable.OrderBy<JsonProperty, int>(jsonPropertyCollection, delegate(JsonProperty p)
			{
				int? order = p.Order;
				if (order == null)
				{
					return -1;
				}
				return order.GetValueOrDefault();
			}));
		}

		/// <summary>
		/// Creates the <see cref="T:Newtonsoft.Json.Serialization.IValueProvider" /> used by the serializer to get and set values from a member.
		/// </summary>
		/// <param name="member">The member.</param>
		/// <returns>The <see cref="T:Newtonsoft.Json.Serialization.IValueProvider" /> used by the serializer to get and set values from a member.</returns>
		// Token: 0x060006ED RID: 1773 RVA: 0x0001A2B0 File Offset: 0x000184B0
		protected virtual IValueProvider CreateMemberValueProvider(MemberInfo member)
		{
			return new ExpressionValueProvider(member);
		}

		/// <summary>
		/// Creates a <see cref="T:Newtonsoft.Json.Serialization.JsonProperty" /> for the given <see cref="T:System.Reflection.MemberInfo" />.
		/// </summary>
		/// <param name="memberSerialization">The member's parent <see cref="T:Newtonsoft.Json.MemberSerialization" />.</param>
		/// <param name="member">The member to create a <see cref="T:Newtonsoft.Json.Serialization.JsonProperty" /> for.</param>
		/// <returns>A created <see cref="T:Newtonsoft.Json.Serialization.JsonProperty" /> for the given <see cref="T:System.Reflection.MemberInfo" />.</returns>
		// Token: 0x060006EE RID: 1774 RVA: 0x0001A2C8 File Offset: 0x000184C8
		protected virtual JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			JsonProperty jsonProperty = new JsonProperty();
			jsonProperty.PropertyType = ReflectionUtils.GetMemberUnderlyingType(member);
			jsonProperty.DeclaringType = member.DeclaringType;
			jsonProperty.ValueProvider = this.CreateMemberValueProvider(member);
			bool flag;
			this.SetPropertySettingsFromAttributes(jsonProperty, member, member.Name, member.DeclaringType, memberSerialization, out flag);
			if (memberSerialization != MemberSerialization.Fields)
			{
				jsonProperty.Readable = ReflectionUtils.CanReadMemberValue(member, flag);
				jsonProperty.Writable = ReflectionUtils.CanSetMemberValue(member, flag, jsonProperty.HasMemberAttribute);
			}
			else
			{
				jsonProperty.Readable = true;
				jsonProperty.Writable = true;
			}
			jsonProperty.ShouldSerialize = this.CreateShouldSerializeTest(member);
			this.SetIsSpecifiedActions(jsonProperty, member, flag);
			return jsonProperty;
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0001A364 File Offset: 0x00018564
		private void SetPropertySettingsFromAttributes(JsonProperty property, object attributeProvider, string name, Type declaringType, MemberSerialization memberSerialization, out bool allowNonPublicAccess)
		{
			DataContractAttribute dataContractAttribute = JsonTypeReflector.GetDataContractAttribute(declaringType);
			MemberInfo memberInfo = attributeProvider as MemberInfo;
			DataMemberAttribute dataMemberAttribute;
			if (dataContractAttribute != null && memberInfo != null)
			{
				dataMemberAttribute = JsonTypeReflector.GetDataMemberAttribute(memberInfo);
			}
			else
			{
				dataMemberAttribute = null;
			}
			JsonPropertyAttribute attribute = JsonTypeReflector.GetAttribute<JsonPropertyAttribute>(attributeProvider);
			if (attribute != null)
			{
				property.HasMemberAttribute = true;
			}
			string propertyName;
			if (attribute != null && attribute.PropertyName != null)
			{
				propertyName = attribute.PropertyName;
			}
			else if (dataMemberAttribute != null && dataMemberAttribute.Name != null)
			{
				propertyName = dataMemberAttribute.Name;
			}
			else
			{
				propertyName = name;
			}
			property.PropertyName = this.ResolvePropertyName(propertyName);
			property.UnderlyingName = name;
			bool flag = false;
			if (attribute != null)
			{
				property._required = attribute._required;
				property.Order = attribute._order;
				property.DefaultValueHandling = attribute._defaultValueHandling;
				flag = true;
			}
			else if (dataMemberAttribute != null)
			{
				property._required = new Required?(dataMemberAttribute.IsRequired ? Required.AllowNull : Required.Default);
				property.Order = ((dataMemberAttribute.Order != -1) ? new int?(dataMemberAttribute.Order) : default(int?));
				property.DefaultValueHandling = ((!dataMemberAttribute.EmitDefaultValue) ? new DefaultValueHandling?(DefaultValueHandling.Ignore) : default(DefaultValueHandling?));
				flag = true;
			}
			bool flag2 = JsonTypeReflector.GetAttribute<JsonIgnoreAttribute>(attributeProvider) != null || JsonTypeReflector.GetAttribute<JsonExtensionDataAttribute>(attributeProvider) != null;
			if (memberSerialization != MemberSerialization.OptIn)
			{
				bool flag3 = JsonTypeReflector.GetAttribute<IgnoreDataMemberAttribute>(attributeProvider) != null;
				property.Ignored = (flag2 || flag3);
			}
			else
			{
				property.Ignored = (flag2 || !flag);
			}
			property.Converter = JsonTypeReflector.GetJsonConverter(attributeProvider, property.PropertyType);
			property.MemberConverter = JsonTypeReflector.GetJsonConverter(attributeProvider, property.PropertyType);
			DefaultValueAttribute attribute2 = JsonTypeReflector.GetAttribute<DefaultValueAttribute>(attributeProvider);
			if (attribute2 != null)
			{
				property.DefaultValue = attribute2.Value;
			}
			property.NullValueHandling = ((attribute != null) ? attribute._nullValueHandling : default(NullValueHandling?));
			property.ReferenceLoopHandling = ((attribute != null) ? attribute._referenceLoopHandling : default(ReferenceLoopHandling?));
			property.ObjectCreationHandling = ((attribute != null) ? attribute._objectCreationHandling : default(ObjectCreationHandling?));
			property.TypeNameHandling = ((attribute != null) ? attribute._typeNameHandling : default(TypeNameHandling?));
			property.IsReference = ((attribute != null) ? attribute._isReference : default(bool?));
			property.ItemIsReference = ((attribute != null) ? attribute._itemIsReference : default(bool?));
			property.ItemConverter = ((attribute != null && attribute.ItemConverterType != null) ? JsonConverterAttribute.CreateJsonConverterInstance(attribute.ItemConverterType) : null);
			property.ItemReferenceLoopHandling = ((attribute != null) ? attribute._itemReferenceLoopHandling : default(ReferenceLoopHandling?));
			property.ItemTypeNameHandling = ((attribute != null) ? attribute._itemTypeNameHandling : default(TypeNameHandling?));
			allowNonPublicAccess = false;
			if ((this.DefaultMembersSearchFlags & BindingFlags.NonPublic) == BindingFlags.NonPublic)
			{
				allowNonPublicAccess = true;
			}
			if (attribute != null)
			{
				allowNonPublicAccess = true;
			}
			if (memberSerialization == MemberSerialization.Fields)
			{
				allowNonPublicAccess = true;
			}
			if (dataMemberAttribute != null)
			{
				allowNonPublicAccess = true;
				property.HasMemberAttribute = true;
			}
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0001A650 File Offset: 0x00018850
		private Predicate<object> CreateShouldSerializeTest(MemberInfo member)
		{
			MethodInfo method = member.DeclaringType.GetMethod("ShouldSerialize" + member.Name, ReflectionUtils.EmptyTypes);
			if (method == null || method.ReturnType != typeof(bool))
			{
				return null;
			}
			MethodCall<object, object> shouldSerializeCall = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(method);
			return (object o) => (bool)shouldSerializeCall(o, new object[0]);
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0001A6D4 File Offset: 0x000188D4
		private void SetIsSpecifiedActions(JsonProperty property, MemberInfo member, bool allowNonPublicAccess)
		{
			MemberInfo memberInfo = member.DeclaringType.GetProperty(member.Name + "Specified");
			if (memberInfo == null)
			{
				memberInfo = member.DeclaringType.GetField(member.Name + "Specified");
			}
			if (memberInfo == null || ReflectionUtils.GetMemberUnderlyingType(memberInfo) != typeof(bool))
			{
				return;
			}
			Func<object, object> specifiedPropertyGet = JsonTypeReflector.ReflectionDelegateFactory.CreateGet<object>(memberInfo);
			property.GetIsSpecified = ((object o) => (bool)specifiedPropertyGet.Invoke(o));
			if (ReflectionUtils.CanSetMemberValue(memberInfo, allowNonPublicAccess, false))
			{
				property.SetIsSpecified = JsonTypeReflector.ReflectionDelegateFactory.CreateSet<object>(memberInfo);
			}
		}

		/// <summary>
		/// Resolves the name of the property.
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
		/// <returns>Name of the property.</returns>
		// Token: 0x060006F2 RID: 1778 RVA: 0x0001A776 File Offset: 0x00018976
		protected internal virtual string ResolvePropertyName(string propertyName)
		{
			return propertyName;
		}

		/// <summary>
		/// Gets the resolved name of the property.
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
		/// <returns>Name of the property.</returns>
		// Token: 0x060006F3 RID: 1779 RVA: 0x0001A779 File Offset: 0x00018979
		public string GetResolvedPropertyName(string propertyName)
		{
			return this.ResolvePropertyName(propertyName);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0001A784 File Offset: 0x00018984
		// Note: this type is marked as 'beforefieldinit'.
		static DefaultContractResolver()
		{
			List<JsonConverter> list = new List<JsonConverter>();
			list.Add(new ExpandoObjectConverter());
			list.Add(new XmlNodeConverter());
			list.Add(new JsonValueConverter());
			list.Add(new KeyValuePairConverter());
			list.Add(new BsonObjectIdConverter());
			list.Add(new RegexConverter());
			DefaultContractResolver.BuiltInConverters = list;
			DefaultContractResolver._typeContractCacheLock = new object();
		}

		// Token: 0x0400026C RID: 620
		private static readonly IContractResolver _instance = new DefaultContractResolver(true);

		// Token: 0x0400026D RID: 621
		private static readonly IList<JsonConverter> BuiltInConverters;

		// Token: 0x0400026E RID: 622
		private static Dictionary<ResolverContractKey, JsonContract> _sharedContractCache;

		// Token: 0x0400026F RID: 623
		private static readonly object _typeContractCacheLock;

		// Token: 0x04000270 RID: 624
		private Dictionary<ResolverContractKey, JsonContract> _instanceContractCache;

		// Token: 0x04000271 RID: 625
		private readonly bool _sharedCache;

		// Token: 0x04000272 RID: 626
		private BindingFlags DefaultMembersSearchFlags = BindingFlags.Instance | BindingFlags.Public;
	}
}
