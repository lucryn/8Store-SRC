using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200009F RID: 159
	internal class JsonSerializerInternalReader : JsonSerializerInternalBase
	{
		// Token: 0x060007EC RID: 2028 RVA: 0x0001C555 File Offset: 0x0001A755
		public JsonSerializerInternalReader(JsonSerializer serializer) : base(serializer)
		{
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x0001C560 File Offset: 0x0001A760
		public void Populate(JsonReader reader, object target)
		{
			ValidationUtils.ArgumentNotNull(target, "target");
			Type type = target.GetType();
			JsonContract jsonContract = this.Serializer._contractResolver.ResolveContract(type);
			if (reader.TokenType == JsonToken.None)
			{
				reader.Read();
			}
			if (reader.TokenType == JsonToken.StartArray)
			{
				if (jsonContract.ContractType == JsonContractType.Array)
				{
					JsonArrayContract jsonArrayContract = (JsonArrayContract)jsonContract;
					this.PopulateList(jsonArrayContract.ShouldCreateWrapper ? jsonArrayContract.CreateWrapper(target) : ((IList)target), reader, jsonArrayContract, null, null);
					return;
				}
				throw JsonSerializationException.Create(reader, "Cannot populate JSON array onto type '{0}'.".FormatWith(CultureInfo.InvariantCulture, type));
			}
			else
			{
				if (reader.TokenType != JsonToken.StartObject)
				{
					throw JsonSerializationException.Create(reader, "Unexpected initial token '{0}' when populating object. Expected JSON object or array.".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
				}
				this.CheckedRead(reader);
				string id = null;
				if (reader.TokenType == JsonToken.PropertyName && string.Equals(reader.Value.ToString(), "$id", 4))
				{
					this.CheckedRead(reader);
					id = ((reader.Value != null) ? reader.Value.ToString() : null);
					this.CheckedRead(reader);
				}
				if (jsonContract.ContractType == JsonContractType.Dictionary)
				{
					JsonDictionaryContract jsonDictionaryContract = (JsonDictionaryContract)jsonContract;
					this.PopulateDictionary(jsonDictionaryContract.ShouldCreateWrapper ? jsonDictionaryContract.CreateWrapper(target) : ((IDictionary)target), reader, jsonDictionaryContract, null, id);
					return;
				}
				if (jsonContract.ContractType == JsonContractType.Object)
				{
					this.PopulateObject(target, reader, (JsonObjectContract)jsonContract, null, id);
					return;
				}
				throw JsonSerializationException.Create(reader, "Cannot populate JSON object onto type '{0}'.".FormatWith(CultureInfo.InvariantCulture, type));
			}
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x0001C6D7 File Offset: 0x0001A8D7
		private JsonContract GetContractSafe(Type type)
		{
			if (type == null)
			{
				return null;
			}
			return this.Serializer._contractResolver.ResolveContract(type);
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x0001C6F0 File Offset: 0x0001A8F0
		public object Deserialize(JsonReader reader, Type objectType, bool checkAdditionalContent)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			JsonContract contractSafe = this.GetContractSafe(objectType);
			object result;
			try
			{
				JsonConverter converter = this.GetConverter(contractSafe, null, null, null);
				if (reader.TokenType == JsonToken.None && !this.ReadForType(reader, contractSafe, converter != null))
				{
					if (contractSafe != null && !contractSafe.IsNullable)
					{
						throw JsonSerializationException.Create(reader, "No JSON content found and type '{0}' is not nullable.".FormatWith(CultureInfo.InvariantCulture, contractSafe.UnderlyingType));
					}
					result = null;
				}
				else
				{
					object obj;
					if (converter != null && converter.CanRead)
					{
						obj = this.DeserializeConvertable(converter, reader, objectType, null);
					}
					else
					{
						obj = this.CreateValueInternal(reader, objectType, contractSafe, null, null, null, null);
					}
					if (checkAdditionalContent && reader.Read() && reader.TokenType != JsonToken.Comment)
					{
						throw new JsonSerializationException("Additional text found in JSON string after finishing deserializing object.");
					}
					result = obj;
				}
			}
			catch (Exception ex)
			{
				if (!base.IsErrorHandled(null, contractSafe, null, reader as IJsonLineInfo, reader.Path, ex))
				{
					throw;
				}
				this.HandleError(reader, false, 0);
				result = null;
			}
			return result;
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x0001C7E8 File Offset: 0x0001A9E8
		private JsonSerializerProxy GetInternalSerializer()
		{
			if (this._internalSerializer == null)
			{
				this._internalSerializer = new JsonSerializerProxy(this);
			}
			return this._internalSerializer;
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x0001C804 File Offset: 0x0001AA04
		private JToken CreateJToken(JsonReader reader, JsonContract contract)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			if (contract != null && contract.UnderlyingType == typeof(JRaw))
			{
				return JRaw.Create(reader);
			}
			JToken token;
			using (JTokenWriter jtokenWriter = new JTokenWriter())
			{
				jtokenWriter.WriteToken(reader);
				token = jtokenWriter.Token;
			}
			return token;
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0001C86C File Offset: 0x0001AA6C
		private JToken CreateJObject(JsonReader reader)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			JToken token;
			using (JTokenWriter jtokenWriter = new JTokenWriter())
			{
				jtokenWriter.WriteStartObject();
				if (reader.TokenType == JsonToken.PropertyName)
				{
					jtokenWriter.WriteToken(reader, reader.Depth - 1, true, true);
				}
				else
				{
					jtokenWriter.WriteEndObject();
				}
				token = jtokenWriter.Token;
			}
			return token;
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0001C8D8 File Offset: 0x0001AAD8
		private object CreateValueInternal(JsonReader reader, Type objectType, JsonContract contract, JsonProperty member, JsonContainerContract containerContract, JsonProperty containerMember, object existingValue)
		{
			if (contract != null && contract.ContractType == JsonContractType.Linq)
			{
				return this.CreateJToken(reader, contract);
			}
			for (;;)
			{
				switch (reader.TokenType)
				{
				case JsonToken.StartObject:
					goto IL_6D;
				case JsonToken.StartArray:
					goto IL_7F;
				case JsonToken.StartConstructor:
					goto IL_102;
				case JsonToken.Comment:
					if (!reader.Read())
					{
						goto Block_10;
					}
					continue;
				case JsonToken.Raw:
					goto IL_133;
				case JsonToken.Integer:
				case JsonToken.Float:
				case JsonToken.Boolean:
				case JsonToken.Date:
				case JsonToken.Bytes:
					goto IL_8E;
				case JsonToken.String:
					goto IL_A3;
				case JsonToken.Null:
				case JsonToken.Undefined:
					goto IL_11E;
				}
				break;
			}
			goto IL_144;
			IL_6D:
			return this.CreateObject(reader, objectType, contract, member, containerContract, containerMember, existingValue);
			IL_7F:
			return this.CreateList(reader, objectType, contract, member, existingValue, null);
			IL_8E:
			return this.EnsureType(reader, reader.Value, CultureInfo.InvariantCulture, contract, objectType);
			IL_A3:
			string text = (string)reader.Value;
			if (string.IsNullOrEmpty(text) && objectType != typeof(string) && objectType != typeof(object) && contract != null && contract.IsNullable)
			{
				return null;
			}
			if (objectType == typeof(byte[]))
			{
				return Convert.FromBase64String(text);
			}
			return this.EnsureType(reader, text, CultureInfo.InvariantCulture, contract, objectType);
			IL_102:
			string value = reader.Value.ToString();
			return this.EnsureType(reader, value, CultureInfo.InvariantCulture, contract, objectType);
			IL_11E:
			return this.EnsureType(reader, reader.Value, CultureInfo.InvariantCulture, contract, objectType);
			IL_133:
			return new JRaw((string)reader.Value);
			IL_144:
			throw JsonSerializationException.Create(reader, "Unexpected token while deserializing object: " + reader.TokenType);
			Block_10:
			throw JsonSerializationException.Create(reader, "Unexpected end when deserializing object.");
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0001CA5C File Offset: 0x0001AC5C
		internal string GetExpectedDescription(JsonContract contract)
		{
			switch (contract.ContractType)
			{
			case JsonContractType.Object:
			case JsonContractType.Dictionary:
			case JsonContractType.Dynamic:
				return "JSON object (e.g. {\"name\":\"value\"})";
			case JsonContractType.Array:
				return "JSON array (e.g. [1,2,3])";
			case JsonContractType.Primitive:
				return "JSON primitive value (e.g. string, number, boolean, null)";
			case JsonContractType.String:
				return "JSON string value";
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0001CAB0 File Offset: 0x0001ACB0
		private JsonConverter GetConverter(JsonContract contract, JsonConverter memberConverter, JsonContainerContract containerContract, JsonProperty containerProperty)
		{
			JsonConverter result = null;
			if (memberConverter != null)
			{
				result = memberConverter;
			}
			else if (containerProperty != null && containerProperty.ItemConverter != null)
			{
				result = containerProperty.ItemConverter;
			}
			else if (containerContract != null && containerContract.ItemConverter != null)
			{
				result = containerContract.ItemConverter;
			}
			else if (contract != null)
			{
				JsonConverter matchingConverter;
				if (contract.Converter != null)
				{
					result = contract.Converter;
				}
				else if ((matchingConverter = this.Serializer.GetMatchingConverter(contract.UnderlyingType)) != null)
				{
					result = matchingConverter;
				}
				else if (contract.InternalConverter != null)
				{
					result = contract.InternalConverter;
				}
			}
			return result;
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0001CB30 File Offset: 0x0001AD30
		private object CreateObject(JsonReader reader, Type objectType, JsonContract contract, JsonProperty member, JsonContainerContract containerContract, JsonProperty containerMember, object existingValue)
		{
			this.CheckedRead(reader);
			object result;
			string text;
			if (this.ReadSpecialProperties(reader, ref objectType, ref contract, member, containerContract, containerMember, existingValue, out result, out text))
			{
				return result;
			}
			if (this.HasNoDefinedType(contract))
			{
				return this.CreateJObject(reader);
			}
			switch (contract.ContractType)
			{
			case JsonContractType.Object:
			{
				bool flag = false;
				JsonObjectContract jsonObjectContract = (JsonObjectContract)contract;
				object obj;
				if (existingValue != null)
				{
					obj = existingValue;
				}
				else
				{
					obj = this.CreateNewObject(reader, jsonObjectContract, member, containerMember, text, out flag);
				}
				if (flag)
				{
					return obj;
				}
				return this.PopulateObject(obj, reader, jsonObjectContract, member, text);
			}
			case JsonContractType.Primitive:
			{
				JsonPrimitiveContract contract2 = (JsonPrimitiveContract)contract;
				if (reader.TokenType == JsonToken.PropertyName && string.Equals(reader.Value.ToString(), "$value", 4))
				{
					this.CheckedRead(reader);
					if (reader.TokenType == JsonToken.StartObject)
					{
						throw JsonSerializationException.Create(reader, "Unexpected token when deserializing primitive value: " + reader.TokenType);
					}
					object result2 = this.CreateValueInternal(reader, objectType, contract2, member, null, null, existingValue);
					this.CheckedRead(reader);
					return result2;
				}
				break;
			}
			case JsonContractType.Dictionary:
			{
				JsonDictionaryContract jsonDictionaryContract = (JsonDictionaryContract)contract;
				object result3;
				if (existingValue == null)
				{
					bool flag2;
					IDictionary dictionary = this.CreateNewDictionary(reader, jsonDictionaryContract, out flag2);
					if (text != null && flag2)
					{
						throw JsonSerializationException.Create(reader, "Cannot preserve reference to readonly dictionary, or dictionary created from a non-default constructor: {0}.".FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
					}
					if (contract.OnSerializingCallbacks.Count > 0 && flag2)
					{
						throw JsonSerializationException.Create(reader, "Cannot call OnSerializing on readonly dictionary, or dictionary created from a non-default constructor: {0}.".FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
					}
					if (contract.OnErrorCallbacks.Count > 0 && flag2)
					{
						throw JsonSerializationException.Create(reader, "Cannot call OnError on readonly list, or dictionary created from a non-default constructor: {0}.".FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
					}
					this.PopulateDictionary(dictionary, reader, jsonDictionaryContract, member, text);
					if (flag2)
					{
						return jsonDictionaryContract.ParametrizedConstructor.Invoke(new object[]
						{
							dictionary
						});
					}
					if (dictionary is IWrappedDictionary)
					{
						return ((IWrappedDictionary)dictionary).UnderlyingDictionary;
					}
					result3 = dictionary;
				}
				else
				{
					result3 = this.PopulateDictionary(jsonDictionaryContract.ShouldCreateWrapper ? jsonDictionaryContract.CreateWrapper(existingValue) : ((IDictionary)existingValue), reader, jsonDictionaryContract, member, text);
				}
				return result3;
			}
			case JsonContractType.Dynamic:
			{
				JsonDynamicContract contract3 = (JsonDynamicContract)contract;
				return this.CreateDynamic(reader, contract3, member, text);
			}
			}
			throw JsonSerializationException.Create(reader, "Cannot deserialize the current JSON object (e.g. {{\"name\":\"value\"}}) into type '{0}' because the type requires a {1} to deserialize correctly.\r\nTo fix this error either change the JSON to a {1} or change the deserialized type so that it is a normal .NET type (e.g. not a primitive type like integer, not a collection type like an array or List<T>) that can be deserialized from a JSON object. JsonObjectAttribute can also be added to the type to force it to deserialize from a JSON object.\r\n".FormatWith(CultureInfo.InvariantCulture, objectType, this.GetExpectedDescription(contract)));
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0001CD94 File Offset: 0x0001AF94
		private bool ReadSpecialProperties(JsonReader reader, ref Type objectType, ref JsonContract contract, JsonProperty member, JsonContainerContract containerContract, JsonProperty containerMember, object existingValue, out object newValue, out string id)
		{
			id = null;
			newValue = null;
			if (reader.TokenType == JsonToken.PropertyName)
			{
				string text = reader.Value.ToString();
				if (text.Length > 0 && text.get_Chars(0) == '$')
				{
					string text2;
					string text3;
					Type type;
					for (;;)
					{
						text = reader.Value.ToString();
						bool flag;
						if (string.Equals(text, "$ref", 4))
						{
							this.CheckedRead(reader);
							if (reader.TokenType != JsonToken.String && reader.TokenType != JsonToken.Null)
							{
								break;
							}
							text2 = ((reader.Value != null) ? reader.Value.ToString() : null);
							this.CheckedRead(reader);
							if (text2 != null)
							{
								goto Block_7;
							}
							flag = true;
						}
						else if (string.Equals(text, "$type", 4))
						{
							this.CheckedRead(reader);
							text3 = reader.Value.ToString();
							TypeNameHandling typeNameHandling = ((member != null) ? member.TypeNameHandling : default(TypeNameHandling?)) ?? (((containerContract != null) ? containerContract.ItemTypeNameHandling : default(TypeNameHandling?)) ?? (((containerMember != null) ? containerMember.ItemTypeNameHandling : default(TypeNameHandling?)) ?? this.Serializer._typeNameHandling));
							if (typeNameHandling != TypeNameHandling.None)
							{
								string typeName;
								string assemblyName;
								ReflectionUtils.SplitFullyQualifiedTypeName(text3, out typeName, out assemblyName);
								try
								{
									type = this.Serializer._binder.BindToType(assemblyName, typeName);
								}
								catch (Exception ex)
								{
									throw JsonSerializationException.Create(reader, "Error resolving type specified in JSON '{0}'.".FormatWith(CultureInfo.InvariantCulture, text3), ex);
								}
								if (type == null)
								{
									goto Block_20;
								}
								if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Verbose)
								{
									this.TraceWriter.Trace(TraceLevel.Verbose, JsonPosition.FormatMessage(reader as IJsonLineInfo, reader.Path, "Resolved type '{0}' to {1}.".FormatWith(CultureInfo.InvariantCulture, text3, type)), null);
								}
								if (objectType != null && objectType != typeof(IDynamicMetaObjectProvider) && !objectType.IsAssignableFrom(type))
								{
									goto Block_25;
								}
								objectType = type;
								contract = this.GetContractSafe(type);
							}
							this.CheckedRead(reader);
							flag = true;
						}
						else if (string.Equals(text, "$id", 4))
						{
							this.CheckedRead(reader);
							id = ((reader.Value != null) ? reader.Value.ToString() : null);
							this.CheckedRead(reader);
							flag = true;
						}
						else
						{
							if (string.Equals(text, "$values", 4))
							{
								goto Block_28;
							}
							flag = false;
						}
						if (!flag || reader.TokenType != JsonToken.PropertyName)
						{
							return false;
						}
					}
					throw JsonSerializationException.Create(reader, "JSON reference {0} property must have a string or null value.".FormatWith(CultureInfo.InvariantCulture, "$ref"));
					Block_7:
					if (reader.TokenType == JsonToken.PropertyName)
					{
						throw JsonSerializationException.Create(reader, "Additional content found in JSON reference object. A JSON reference object should only have a {0} property.".FormatWith(CultureInfo.InvariantCulture, "$ref"));
					}
					newValue = this.Serializer.GetReferenceResolver().ResolveReference(this, text2);
					if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Info)
					{
						this.TraceWriter.Trace(TraceLevel.Info, JsonPosition.FormatMessage(reader as IJsonLineInfo, reader.Path, "Resolved object reference '{0}' to {1}.".FormatWith(CultureInfo.InvariantCulture, text2, newValue.GetType())), null);
					}
					return true;
					Block_20:
					throw JsonSerializationException.Create(reader, "Type specified in JSON '{0}' was not resolved.".FormatWith(CultureInfo.InvariantCulture, text3));
					Block_25:
					throw JsonSerializationException.Create(reader, "Type specified in JSON '{0}' is not compatible with '{1}'.".FormatWith(CultureInfo.InvariantCulture, type.AssemblyQualifiedName, objectType.AssemblyQualifiedName));
					Block_28:
					this.CheckedRead(reader);
					object obj = this.CreateList(reader, objectType, contract, member, existingValue, id);
					this.CheckedRead(reader);
					newValue = obj;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x0001D124 File Offset: 0x0001B324
		private JsonArrayContract EnsureArrayContract(JsonReader reader, Type objectType, JsonContract contract)
		{
			if (contract == null)
			{
				throw JsonSerializationException.Create(reader, "Could not resolve type '{0}' to a JsonContract.".FormatWith(CultureInfo.InvariantCulture, objectType));
			}
			JsonArrayContract jsonArrayContract = contract as JsonArrayContract;
			if (jsonArrayContract == null)
			{
				throw JsonSerializationException.Create(reader, "Cannot deserialize the current JSON array (e.g. [1,2,3]) into type '{0}' because the type requires a {1} to deserialize correctly.\r\nTo fix this error either change the JSON to a {1} or change the deserialized type to an array or a type that implements a collection interface (e.g. ICollection, IList) like List<T> that can be deserialized from a JSON array. JsonArrayAttribute can also be added to the type to force it to deserialize from a JSON array.\r\n".FormatWith(CultureInfo.InvariantCulture, objectType, this.GetExpectedDescription(contract)));
			}
			return jsonArrayContract;
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x0001D174 File Offset: 0x0001B374
		private void CheckedRead(JsonReader reader)
		{
			if (!reader.Read())
			{
				throw JsonSerializationException.Create(reader, "Unexpected end when deserializing object.");
			}
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x0001D18C File Offset: 0x0001B38C
		private object CreateList(JsonReader reader, Type objectType, JsonContract contract, JsonProperty member, object existingValue, string id)
		{
			if (this.HasNoDefinedType(contract))
			{
				return this.CreateJToken(reader, contract);
			}
			JsonArrayContract jsonArrayContract = this.EnsureArrayContract(reader, objectType, contract);
			object result;
			if (existingValue == null)
			{
				bool flag;
				IList list = this.CreateNewList(reader, jsonArrayContract, out flag);
				if (id != null && flag)
				{
					throw JsonSerializationException.Create(reader, "Cannot preserve reference to array or readonly list, or list created from a non-default constructor: {0}.".FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
				}
				if (contract.OnSerializingCallbacks.Count > 0 && flag)
				{
					throw JsonSerializationException.Create(reader, "Cannot call OnSerializing on an array or readonly list, or list created from a non-default constructor: {0}.".FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
				}
				if (contract.OnErrorCallbacks.Count > 0 && flag)
				{
					throw JsonSerializationException.Create(reader, "Cannot call OnError on an array or readonly list, or list created from a non-default constructor: {0}.".FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
				}
				if (!jsonArrayContract.IsMultidimensionalArray)
				{
					this.PopulateList(list, reader, jsonArrayContract, member, id);
				}
				else
				{
					this.PopulateMultidimensionalArray(list, reader, jsonArrayContract, member, id);
				}
				if (flag)
				{
					if (jsonArrayContract.IsMultidimensionalArray)
					{
						list = CollectionUtils.ToMultidimensionalArray(list, jsonArrayContract.CollectionItemType, contract.CreatedType.GetArrayRank());
					}
					else
					{
						if (!contract.CreatedType.IsArray)
						{
							return jsonArrayContract.ParametrizedConstructor.Invoke(new object[]
							{
								list
							});
						}
						Array array = Array.CreateInstance(jsonArrayContract.CollectionItemType, new int[]
						{
							list.Count
						});
						list.CopyTo(array, 0);
						list = array;
					}
				}
				else if (list is IWrappedCollection)
				{
					return ((IWrappedCollection)list).UnderlyingCollection;
				}
				result = list;
			}
			else
			{
				result = this.PopulateList(jsonArrayContract.ShouldCreateWrapper ? jsonArrayContract.CreateWrapper(existingValue) : ((IList)existingValue), reader, jsonArrayContract, member, id);
			}
			return result;
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x0001D325 File Offset: 0x0001B525
		private bool HasNoDefinedType(JsonContract contract)
		{
			return contract == null || contract.UnderlyingType == typeof(object) || contract.ContractType == JsonContractType.Linq || contract.UnderlyingType == typeof(IDynamicMetaObjectProvider);
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x0001D35C File Offset: 0x0001B55C
		private object EnsureType(JsonReader reader, object value, CultureInfo culture, JsonContract contract, Type targetType)
		{
			if (targetType == null)
			{
				return value;
			}
			Type objectType = ReflectionUtils.GetObjectType(value);
			if (objectType != targetType)
			{
				if (value == null && contract.IsNullable)
				{
					return null;
				}
				try
				{
					if (!contract.IsConvertable)
					{
						return ConvertUtils.ConvertOrCast(value, culture, contract.NonNullableUnderlyingType);
					}
					JsonPrimitiveContract jsonPrimitiveContract = (JsonPrimitiveContract)contract;
					if (contract.IsEnum)
					{
						if (value is string)
						{
							return Enum.Parse(contract.NonNullableUnderlyingType, value.ToString(), true);
						}
						if (ConvertUtils.IsInteger(jsonPrimitiveContract.TypeCode))
						{
							return Enum.ToObject(contract.NonNullableUnderlyingType, value);
						}
					}
					if (value is BigInteger)
					{
						return ConvertUtils.FromBigInteger((BigInteger)value, targetType);
					}
					return Convert.ChangeType(value, contract.NonNullableUnderlyingType, culture);
				}
				catch (Exception ex)
				{
					throw JsonSerializationException.Create(reader, "Error converting value {0} to type '{1}'.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.FormatValueForPrint(value), targetType), ex);
				}
				return value;
			}
			return value;
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0001D454 File Offset: 0x0001B654
		private bool SetPropertyValue(JsonProperty property, JsonConverter propertyConverter, JsonContainerContract containerContract, JsonProperty containerProperty, JsonReader reader, object target)
		{
			bool flag;
			object value;
			JsonContract contract;
			bool flag2;
			if (this.CalculatePropertyDetails(property, ref propertyConverter, containerContract, containerProperty, reader, target, out flag, out value, out contract, out flag2))
			{
				return false;
			}
			object obj;
			if (propertyConverter != null && propertyConverter.CanRead)
			{
				if (!flag2 && target != null && property.Readable)
				{
					value = property.ValueProvider.GetValue(target);
				}
				obj = this.DeserializeConvertable(propertyConverter, reader, property.PropertyType, value);
			}
			else
			{
				obj = this.CreateValueInternal(reader, property.PropertyType, contract, property, containerContract, containerProperty, flag ? value : null);
			}
			if ((!flag || obj != value) && this.ShouldSetPropertyValue(property, obj))
			{
				property.ValueProvider.SetValue(target, obj);
				if (property.SetIsSpecified != null)
				{
					if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Verbose)
					{
						this.TraceWriter.Trace(TraceLevel.Verbose, JsonPosition.FormatMessage(reader as IJsonLineInfo, reader.Path, "IsSpecified for property '{0}' on {1} set to true.".FormatWith(CultureInfo.InvariantCulture, property.PropertyName, property.DeclaringType)), null);
					}
					property.SetIsSpecified.Invoke(target, true);
				}
				return true;
			}
			return flag;
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0001D56C File Offset: 0x0001B76C
		private bool CalculatePropertyDetails(JsonProperty property, ref JsonConverter propertyConverter, JsonContainerContract containerContract, JsonProperty containerProperty, JsonReader reader, object target, out bool useExistingValue, out object currentValue, out JsonContract propertyContract, out bool gottenCurrentValue)
		{
			currentValue = null;
			useExistingValue = false;
			propertyContract = null;
			gottenCurrentValue = false;
			if (property.Ignored)
			{
				return true;
			}
			JsonToken tokenType = reader.TokenType;
			if (property.PropertyContract == null)
			{
				property.PropertyContract = this.GetContractSafe(property.PropertyType);
			}
			ObjectCreationHandling valueOrDefault = property.ObjectCreationHandling.GetValueOrDefault(this.Serializer._objectCreationHandling);
			if (valueOrDefault != ObjectCreationHandling.Replace && (tokenType == JsonToken.StartArray || tokenType == JsonToken.StartObject) && property.Readable)
			{
				currentValue = property.ValueProvider.GetValue(target);
				gottenCurrentValue = true;
				if (currentValue != null)
				{
					propertyContract = this.GetContractSafe(currentValue.GetType());
					useExistingValue = (!propertyContract.IsReadOnlyOrFixedSize && !propertyContract.UnderlyingType.IsValueType());
				}
			}
			if (!property.Writable && !useExistingValue)
			{
				return true;
			}
			if (property.NullValueHandling.GetValueOrDefault(this.Serializer._nullValueHandling) == NullValueHandling.Ignore && tokenType == JsonToken.Null)
			{
				return true;
			}
			if (this.HasFlag(property.DefaultValueHandling.GetValueOrDefault(this.Serializer._defaultValueHandling), DefaultValueHandling.Ignore) && !this.HasFlag(property.DefaultValueHandling.GetValueOrDefault(this.Serializer._defaultValueHandling), DefaultValueHandling.Populate) && JsonReader.IsPrimitiveToken(tokenType) && MiscellaneousUtils.ValueEquals(reader.Value, property.GetResolvedDefaultValue()))
			{
				return true;
			}
			if (currentValue == null)
			{
				propertyContract = property.PropertyContract;
			}
			else
			{
				propertyContract = this.GetContractSafe(currentValue.GetType());
				if (propertyContract != property.PropertyContract)
				{
					propertyConverter = this.GetConverter(propertyContract, property.MemberConverter, containerContract, containerProperty);
				}
			}
			return false;
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0001D704 File Offset: 0x0001B904
		private void AddReference(JsonReader reader, string id, object value)
		{
			try
			{
				if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Verbose)
				{
					this.TraceWriter.Trace(TraceLevel.Verbose, JsonPosition.FormatMessage(reader as IJsonLineInfo, reader.Path, "Read object reference Id '{0}' for {1}.".FormatWith(CultureInfo.InvariantCulture, id, value.GetType())), null);
				}
				this.Serializer.GetReferenceResolver().AddReference(this, id, value);
			}
			catch (Exception ex)
			{
				throw JsonSerializationException.Create(reader, "Error reading object reference '{0}'.".FormatWith(CultureInfo.InvariantCulture, id), ex);
			}
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0001D79C File Offset: 0x0001B99C
		private bool HasFlag(DefaultValueHandling value, DefaultValueHandling flag)
		{
			return (value & flag) == flag;
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0001D7A4 File Offset: 0x0001B9A4
		private bool ShouldSetPropertyValue(JsonProperty property, object value)
		{
			return (property.NullValueHandling.GetValueOrDefault(this.Serializer._nullValueHandling) != NullValueHandling.Ignore || value != null) && (!this.HasFlag(property.DefaultValueHandling.GetValueOrDefault(this.Serializer._defaultValueHandling), DefaultValueHandling.Ignore) || this.HasFlag(property.DefaultValueHandling.GetValueOrDefault(this.Serializer._defaultValueHandling), DefaultValueHandling.Populate) || !MiscellaneousUtils.ValueEquals(value, property.GetResolvedDefaultValue())) && property.Writable;
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0001D834 File Offset: 0x0001BA34
		private IList CreateNewList(JsonReader reader, JsonArrayContract contract, out bool createdFromNonDefaultConstructor)
		{
			if (!contract.CanDeserialize)
			{
				throw JsonSerializationException.Create(reader, "Cannot create and populate list type {0}.".FormatWith(CultureInfo.InvariantCulture, contract.CreatedType));
			}
			if (contract.IsReadOnlyOrFixedSize)
			{
				createdFromNonDefaultConstructor = true;
				IList list = contract.CreateTemporaryCollection();
				if (contract.ShouldCreateWrapper)
				{
					list = contract.CreateWrapper(list);
				}
				return list;
			}
			if (contract.DefaultCreator != null && (!contract.DefaultCreatorNonPublic || this.Serializer._constructorHandling == ConstructorHandling.AllowNonPublicDefaultConstructor))
			{
				object obj = contract.DefaultCreator.Invoke();
				if (contract.ShouldCreateWrapper)
				{
					obj = contract.CreateWrapper(obj);
				}
				createdFromNonDefaultConstructor = false;
				return (IList)obj;
			}
			if (contract.ParametrizedConstructor != null)
			{
				createdFromNonDefaultConstructor = true;
				return contract.CreateTemporaryCollection();
			}
			throw JsonSerializationException.Create(reader, "Unable to find a constructor to use for type {0}.".FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0001D8FC File Offset: 0x0001BAFC
		private IDictionary CreateNewDictionary(JsonReader reader, JsonDictionaryContract contract, out bool createdFromNonDefaultConstructor)
		{
			if (contract.IsReadOnlyOrFixedSize)
			{
				createdFromNonDefaultConstructor = true;
				return contract.CreateTemporaryDictionary();
			}
			if (contract.DefaultCreator != null && (!contract.DefaultCreatorNonPublic || this.Serializer._constructorHandling == ConstructorHandling.AllowNonPublicDefaultConstructor))
			{
				object obj = contract.DefaultCreator.Invoke();
				if (contract.ShouldCreateWrapper)
				{
					obj = contract.CreateWrapper(obj);
				}
				createdFromNonDefaultConstructor = false;
				return (IDictionary)obj;
			}
			if (contract.ParametrizedConstructor != null)
			{
				createdFromNonDefaultConstructor = true;
				return contract.CreateTemporaryDictionary();
			}
			throw JsonSerializationException.Create(reader, "Unable to find a default constructor to use for type {0}.".FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0001D98C File Offset: 0x0001BB8C
		private void OnDeserializing(JsonReader reader, JsonContract contract, object value)
		{
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Info)
			{
				this.TraceWriter.Trace(TraceLevel.Info, JsonPosition.FormatMessage(reader as IJsonLineInfo, reader.Path, "Started deserializing {0}".FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType)), null);
			}
			contract.InvokeOnDeserializing(value, this.Serializer._context);
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0001D9F4 File Offset: 0x0001BBF4
		private void OnDeserialized(JsonReader reader, JsonContract contract, object value)
		{
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Info)
			{
				this.TraceWriter.Trace(TraceLevel.Info, JsonPosition.FormatMessage(reader as IJsonLineInfo, reader.Path, "Finished deserializing {0}".FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType)), null);
			}
			contract.InvokeOnDeserialized(value, this.Serializer._context);
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0001DA5C File Offset: 0x0001BC5C
		private object PopulateDictionary(IDictionary dictionary, JsonReader reader, JsonDictionaryContract contract, JsonProperty containerProperty, string id)
		{
			IWrappedDictionary wrappedDictionary = dictionary as IWrappedDictionary;
			object obj = (wrappedDictionary != null) ? wrappedDictionary.UnderlyingDictionary : dictionary;
			if (id != null)
			{
				this.AddReference(reader, id, obj);
			}
			this.OnDeserializing(reader, contract, obj);
			int depth = reader.Depth;
			if (contract.KeyContract == null)
			{
				contract.KeyContract = this.GetContractSafe(contract.DictionaryKeyType);
			}
			if (contract.ItemContract == null)
			{
				contract.ItemContract = this.GetContractSafe(contract.DictionaryValueType);
			}
			JsonConverter jsonConverter = contract.ItemConverter ?? this.GetConverter(contract.ItemContract, null, contract, containerProperty);
			PrimitiveTypeCode primitiveTypeCode = (contract.KeyContract is JsonPrimitiveContract) ? ((JsonPrimitiveContract)contract.KeyContract).TypeCode : PrimitiveTypeCode.Empty;
			bool flag = false;
			for (;;)
			{
				JsonToken tokenType = reader.TokenType;
				switch (tokenType)
				{
				case JsonToken.PropertyName:
				{
					object obj2 = reader.Value;
					try
					{
						try
						{
							object obj3;
							if ((primitiveTypeCode == PrimitiveTypeCode.DateTime || primitiveTypeCode == PrimitiveTypeCode.DateTimeNullable) && DateTimeUtils.TryParseDateTime(obj2.ToString(), DateParseHandling.DateTime, reader.DateTimeZoneHandling, out obj3))
							{
								obj2 = obj3;
							}
							else if ((primitiveTypeCode == PrimitiveTypeCode.DateTimeOffset || primitiveTypeCode == PrimitiveTypeCode.DateTimeOffsetNullable) && DateTimeUtils.TryParseDateTime(obj2.ToString(), DateParseHandling.DateTimeOffset, reader.DateTimeZoneHandling, out obj3))
							{
								obj2 = obj3;
							}
							else
							{
								obj2 = this.EnsureType(reader, obj2, CultureInfo.InvariantCulture, contract.KeyContract, contract.DictionaryKeyType);
							}
						}
						catch (Exception ex)
						{
							throw JsonSerializationException.Create(reader, "Could not convert string '{0}' to dictionary key type '{1}'. Create a TypeConverter to convert from the string to the key type object.".FormatWith(CultureInfo.InvariantCulture, reader.Value, contract.DictionaryKeyType), ex);
						}
						if (!this.ReadForType(reader, contract.ItemContract, jsonConverter != null))
						{
							throw JsonSerializationException.Create(reader, "Unexpected end when deserializing object.");
						}
						object obj4;
						if (jsonConverter != null && jsonConverter.CanRead)
						{
							obj4 = this.DeserializeConvertable(jsonConverter, reader, contract.DictionaryValueType, null);
						}
						else
						{
							obj4 = this.CreateValueInternal(reader, contract.DictionaryValueType, contract.ItemContract, null, contract, containerProperty, null);
						}
						dictionary[obj2] = obj4;
						break;
					}
					catch (Exception ex2)
					{
						if (base.IsErrorHandled(obj, contract, obj2, reader as IJsonLineInfo, reader.Path, ex2))
						{
							this.HandleError(reader, true, depth);
							break;
						}
						throw;
					}
					goto IL_1FC;
				}
				case JsonToken.Comment:
					break;
				default:
					if (tokenType != JsonToken.EndObject)
					{
						goto Block_8;
					}
					goto IL_1FC;
				}
				IL_21D:
				if (flag || !reader.Read())
				{
					goto IL_22C;
				}
				continue;
				IL_1FC:
				flag = true;
				goto IL_21D;
			}
			Block_8:
			throw JsonSerializationException.Create(reader, "Unexpected token when deserializing object: " + reader.TokenType);
			IL_22C:
			if (!flag)
			{
				this.ThrowUnexpectedEndException(reader, contract, obj, "Unexpected end when deserializing object.");
			}
			this.OnDeserialized(reader, contract, obj);
			return obj;
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0001DCD0 File Offset: 0x0001BED0
		private object PopulateMultidimensionalArray(IList list, JsonReader reader, JsonArrayContract contract, JsonProperty containerProperty, string id)
		{
			int arrayRank = contract.UnderlyingType.GetArrayRank();
			if (id != null)
			{
				this.AddReference(reader, id, list);
			}
			this.OnDeserializing(reader, contract, list);
			JsonContract contractSafe = this.GetContractSafe(contract.CollectionItemType);
			JsonConverter converter = this.GetConverter(contractSafe, null, contract, containerProperty);
			int? num = default(int?);
			Stack<IList> stack = new Stack<IList>();
			stack.Push(list);
			IList list2 = list;
			bool flag = false;
			for (;;)
			{
				int depth = reader.Depth;
				if (stack.Count == arrayRank)
				{
					try
					{
						if (this.ReadForType(reader, contractSafe, converter != null))
						{
							JsonToken tokenType = reader.TokenType;
							if (tokenType != JsonToken.Comment)
							{
								if (tokenType == JsonToken.EndArray)
								{
									stack.Pop();
									list2 = stack.Peek();
									num = default(int?);
								}
								else
								{
									object obj;
									if (converter != null && converter.CanRead)
									{
										obj = this.DeserializeConvertable(converter, reader, contract.CollectionItemType, null);
									}
									else
									{
										obj = this.CreateValueInternal(reader, contract.CollectionItemType, contractSafe, null, contract, containerProperty, null);
									}
									list2.Add(obj);
								}
							}
							goto IL_201;
						}
						goto IL_208;
					}
					catch (Exception ex)
					{
						JsonPosition position = reader.GetPosition(depth);
						if (!base.IsErrorHandled(list, contract, position.Position, reader as IJsonLineInfo, reader.Path, ex))
						{
							throw;
						}
						this.HandleError(reader, true, depth);
						if (num != null && num == position.Position)
						{
							throw JsonSerializationException.Create(reader, "Infinite loop detected from error handling.", ex);
						}
						num = new int?(position.Position);
						goto IL_201;
					}
					goto IL_181;
				}
				goto IL_181;
				IL_201:
				if (flag)
				{
					goto IL_208;
				}
				continue;
				IL_181:
				if (!reader.Read())
				{
					goto IL_208;
				}
				JsonToken tokenType2 = reader.TokenType;
				if (tokenType2 == JsonToken.StartArray)
				{
					IList list3 = new List<object>();
					list2.Add(list3);
					stack.Push(list3);
					list2 = list3;
					goto IL_201;
				}
				if (tokenType2 == JsonToken.Comment)
				{
					goto IL_201;
				}
				if (tokenType2 != JsonToken.EndArray)
				{
					break;
				}
				stack.Pop();
				if (stack.Count > 0)
				{
					list2 = stack.Peek();
					goto IL_201;
				}
				flag = true;
				goto IL_201;
			}
			throw JsonSerializationException.Create(reader, "Unexpected token when deserializing multidimensional array: " + reader.TokenType);
			IL_208:
			if (!flag)
			{
				this.ThrowUnexpectedEndException(reader, contract, list, "Unexpected end when deserializing array.");
			}
			this.OnDeserialized(reader, contract, list);
			return list;
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0001DF14 File Offset: 0x0001C114
		private void ThrowUnexpectedEndException(JsonReader reader, JsonContract contract, object currentObject, string message)
		{
			try
			{
				throw JsonSerializationException.Create(reader, message);
			}
			catch (Exception ex)
			{
				if (!base.IsErrorHandled(currentObject, contract, null, reader as IJsonLineInfo, reader.Path, ex))
				{
					throw;
				}
				this.HandleError(reader, false, 0);
			}
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0001DF64 File Offset: 0x0001C164
		private object PopulateList(IList list, JsonReader reader, JsonArrayContract contract, JsonProperty containerProperty, string id)
		{
			IWrappedCollection wrappedCollection = list as IWrappedCollection;
			object obj = (wrappedCollection != null) ? wrappedCollection.UnderlyingCollection : list;
			if (id != null)
			{
				this.AddReference(reader, id, obj);
			}
			if (list.IsFixedSize)
			{
				reader.Skip();
				return obj;
			}
			this.OnDeserializing(reader, contract, obj);
			int depth = reader.Depth;
			JsonContract contractSafe = this.GetContractSafe(contract.CollectionItemType);
			JsonConverter converter = this.GetConverter(contractSafe, null, contract, containerProperty);
			int? num = default(int?);
			bool flag = false;
			do
			{
				try
				{
					if (!this.ReadForType(reader, contractSafe, converter != null))
					{
						break;
					}
					JsonToken tokenType = reader.TokenType;
					if (tokenType != JsonToken.Comment)
					{
						if (tokenType == JsonToken.EndArray)
						{
							flag = true;
						}
						else
						{
							object obj2;
							if (converter != null && converter.CanRead)
							{
								obj2 = this.DeserializeConvertable(converter, reader, contract.CollectionItemType, null);
							}
							else
							{
								obj2 = this.CreateValueInternal(reader, contract.CollectionItemType, contractSafe, null, contract, containerProperty, null);
							}
							list.Add(obj2);
						}
					}
				}
				catch (Exception ex)
				{
					JsonPosition position = reader.GetPosition(depth);
					if (!base.IsErrorHandled(obj, contract, position.Position, reader as IJsonLineInfo, reader.Path, ex))
					{
						throw;
					}
					this.HandleError(reader, true, depth);
					if (num != null && num == position.Position)
					{
						throw JsonSerializationException.Create(reader, "Infinite loop detected from error handling.", ex);
					}
					num = new int?(position.Position);
				}
			}
			while (!flag);
			if (!flag)
			{
				this.ThrowUnexpectedEndException(reader, contract, obj, "Unexpected end when deserializing array.");
			}
			this.OnDeserialized(reader, contract, obj);
			return obj;
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0001E108 File Offset: 0x0001C308
		private object CreateDynamic(JsonReader reader, JsonDynamicContract contract, JsonProperty member, string id)
		{
			if (!contract.IsInstantiable)
			{
				throw JsonSerializationException.Create(reader, "Could not create an instance of type {0}. Type is an interface or abstract class and cannot be instantiated.".FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
			}
			if (contract.DefaultCreator != null && (!contract.DefaultCreatorNonPublic || this.Serializer._constructorHandling == ConstructorHandling.AllowNonPublicDefaultConstructor))
			{
				IDynamicMetaObjectProvider dynamicMetaObjectProvider = (IDynamicMetaObjectProvider)contract.DefaultCreator.Invoke();
				if (id != null)
				{
					this.AddReference(reader, id, dynamicMetaObjectProvider);
				}
				this.OnDeserializing(reader, contract, dynamicMetaObjectProvider);
				int depth = reader.Depth;
				bool flag = false;
				for (;;)
				{
					JsonToken tokenType = reader.TokenType;
					if (tokenType == JsonToken.PropertyName)
					{
						string text = reader.Value.ToString();
						try
						{
							if (!reader.Read())
							{
								throw JsonSerializationException.Create(reader, "Unexpected end when setting {0}'s value.".FormatWith(CultureInfo.InvariantCulture, text));
							}
							JsonProperty closestMatchProperty = contract.Properties.GetClosestMatchProperty(text);
							if (closestMatchProperty != null && closestMatchProperty.Writable && !closestMatchProperty.Ignored)
							{
								if (closestMatchProperty.PropertyContract == null)
								{
									closestMatchProperty.PropertyContract = this.GetContractSafe(closestMatchProperty.PropertyType);
								}
								JsonConverter converter = this.GetConverter(closestMatchProperty.PropertyContract, closestMatchProperty.MemberConverter, null, null);
								if (!this.SetPropertyValue(closestMatchProperty, converter, null, member, reader, dynamicMetaObjectProvider))
								{
									reader.Skip();
								}
							}
							else
							{
								Type type = JsonReader.IsPrimitiveToken(reader.TokenType) ? reader.ValueType : typeof(IDynamicMetaObjectProvider);
								JsonContract contractSafe = this.GetContractSafe(type);
								JsonConverter converter2 = this.GetConverter(contractSafe, null, null, member);
								object value;
								if (converter2 != null && converter2.CanRead)
								{
									value = this.DeserializeConvertable(converter2, reader, type, null);
								}
								else
								{
									value = this.CreateValueInternal(reader, type, contractSafe, null, null, member, null);
								}
								contract.TrySetMember(dynamicMetaObjectProvider, text, value);
							}
							goto IL_205;
						}
						catch (Exception ex)
						{
							if (base.IsErrorHandled(dynamicMetaObjectProvider, contract, text, reader as IJsonLineInfo, reader.Path, ex))
							{
								this.HandleError(reader, true, depth);
								goto IL_205;
							}
							throw;
						}
						goto IL_1E5;
					}
					if (tokenType != JsonToken.EndObject)
					{
						break;
					}
					goto IL_1E5;
					IL_205:
					if (flag || !reader.Read())
					{
						goto IL_213;
					}
					continue;
					IL_1E5:
					flag = true;
					goto IL_205;
				}
				throw JsonSerializationException.Create(reader, "Unexpected token when deserializing object: " + reader.TokenType);
				IL_213:
				if (!flag)
				{
					this.ThrowUnexpectedEndException(reader, contract, dynamicMetaObjectProvider, "Unexpected end when deserializing object.");
				}
				this.OnDeserialized(reader, contract, dynamicMetaObjectProvider);
				return dynamicMetaObjectProvider;
			}
			throw JsonSerializationException.Create(reader, "Unable to find a default constructor to use for type {0}.".FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType));
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0001E374 File Offset: 0x0001C574
		private object CreateObjectFromNonDefaultConstructor(JsonReader reader, JsonObjectContract contract, JsonProperty containerProperty, ConstructorInfo constructorInfo, string id)
		{
			ValidationUtils.ArgumentNotNull(constructorInfo, "constructorInfo");
			Type underlyingType = contract.UnderlyingType;
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Info)
			{
				this.TraceWriter.Trace(TraceLevel.Info, JsonPosition.FormatMessage(reader as IJsonLineInfo, reader.Path, "Deserializing {0} using a non-default constructor '{1}'.".FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType, constructorInfo)), null);
			}
			IDictionary<JsonProperty, object> dictionary = this.ResolvePropertyAndConstructorValues(contract, containerProperty, reader, underlyingType);
			IDictionary<ParameterInfo, object> dictionary2 = Enumerable.ToDictionary<ParameterInfo, ParameterInfo, object>(constructorInfo.GetParameters(), (ParameterInfo p) => p, (ParameterInfo p) => null);
			IDictionary<JsonProperty, object> dictionary3 = new Dictionary<JsonProperty, object>();
			foreach (KeyValuePair<JsonProperty, object> keyValuePair in dictionary)
			{
				ParameterInfo key = dictionary2.ForgivingCaseSensitiveFind((KeyValuePair<ParameterInfo, object> kv) => kv.Key.Name, keyValuePair.Key.UnderlyingName).Key;
				if (key != null)
				{
					dictionary2[key] = keyValuePair.Value;
				}
				else
				{
					dictionary3.Add(keyValuePair);
				}
			}
			object obj = constructorInfo.Invoke(Enumerable.ToArray<object>(dictionary2.Values));
			if (id != null)
			{
				this.AddReference(reader, id, obj);
			}
			this.OnDeserializing(reader, contract, obj);
			foreach (KeyValuePair<JsonProperty, object> keyValuePair2 in dictionary3)
			{
				JsonProperty key2 = keyValuePair2.Key;
				object value = keyValuePair2.Value;
				if (this.ShouldSetPropertyValue(keyValuePair2.Key, keyValuePair2.Value))
				{
					key2.ValueProvider.SetValue(obj, value);
				}
				else if (!key2.Writable && value != null)
				{
					JsonContract jsonContract = this.Serializer._contractResolver.ResolveContract(key2.PropertyType);
					if (jsonContract.ContractType == JsonContractType.Array)
					{
						JsonArrayContract jsonArrayContract = (JsonArrayContract)jsonContract;
						object value2 = key2.ValueProvider.GetValue(obj);
						if (value2 == null)
						{
							continue;
						}
						IWrappedCollection wrappedCollection = jsonArrayContract.CreateWrapper(value2);
						IWrappedCollection wrappedCollection2 = jsonArrayContract.CreateWrapper(value);
						using (IEnumerator enumerator3 = wrappedCollection2.GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								object obj2 = enumerator3.Current;
								wrappedCollection.Add(obj2);
							}
							continue;
						}
					}
					if (jsonContract.ContractType == JsonContractType.Dictionary)
					{
						JsonDictionaryContract jsonDictionaryContract = (JsonDictionaryContract)jsonContract;
						object value3 = key2.ValueProvider.GetValue(obj);
						if (value3 != null)
						{
							IDictionary dictionary4 = jsonDictionaryContract.ShouldCreateWrapper ? jsonDictionaryContract.CreateWrapper(value3) : ((IDictionary)value3);
							IDictionary dictionary5 = jsonDictionaryContract.ShouldCreateWrapper ? jsonDictionaryContract.CreateWrapper(value) : ((IDictionary)value);
							foreach (object obj3 in dictionary5)
							{
								DictionaryEntry dictionaryEntry = (DictionaryEntry)obj3;
								dictionary4.Add(dictionaryEntry.Key, dictionaryEntry.Value);
							}
						}
					}
				}
			}
			this.OnDeserialized(reader, contract, obj);
			return obj;
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0001E724 File Offset: 0x0001C924
		private object DeserializeConvertable(JsonConverter converter, JsonReader reader, Type objectType, object existingValue)
		{
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Info)
			{
				this.TraceWriter.Trace(TraceLevel.Info, JsonPosition.FormatMessage(reader as IJsonLineInfo, reader.Path, "Started deserializing {0} with converter {1}.".FormatWith(CultureInfo.InvariantCulture, objectType, converter.GetType())), null);
			}
			object result = converter.ReadJson(reader, objectType, existingValue, this.GetInternalSerializer());
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Info)
			{
				this.TraceWriter.Trace(TraceLevel.Info, JsonPosition.FormatMessage(reader as IJsonLineInfo, reader.Path, "Finished deserializing {0} with converter {1}.".FormatWith(CultureInfo.InvariantCulture, objectType, converter.GetType())), null);
			}
			return result;
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0001E7D8 File Offset: 0x0001C9D8
		private IDictionary<JsonProperty, object> ResolvePropertyAndConstructorValues(JsonObjectContract contract, JsonProperty containerProperty, JsonReader reader, Type objectType)
		{
			IDictionary<JsonProperty, object> dictionary = new Dictionary<JsonProperty, object>();
			bool flag = false;
			string text;
			for (;;)
			{
				JsonToken tokenType = reader.TokenType;
				switch (tokenType)
				{
				case JsonToken.PropertyName:
				{
					text = reader.Value.ToString();
					JsonProperty jsonProperty = contract.ConstructorParameters.GetClosestMatchProperty(text) ?? contract.Properties.GetClosestMatchProperty(text);
					if (jsonProperty != null)
					{
						if (jsonProperty.PropertyContract == null)
						{
							jsonProperty.PropertyContract = this.GetContractSafe(jsonProperty.PropertyType);
						}
						JsonConverter converter = this.GetConverter(jsonProperty.PropertyContract, jsonProperty.MemberConverter, contract, containerProperty);
						if (!this.ReadForType(reader, jsonProperty.PropertyContract, converter != null))
						{
							goto Block_6;
						}
						if (!jsonProperty.Ignored)
						{
							if (jsonProperty.PropertyContract == null)
							{
								jsonProperty.PropertyContract = this.GetContractSafe(jsonProperty.PropertyType);
							}
							object obj;
							if (converter != null && converter.CanRead)
							{
								obj = this.DeserializeConvertable(converter, reader, jsonProperty.PropertyType, null);
							}
							else
							{
								obj = this.CreateValueInternal(reader, jsonProperty.PropertyType, jsonProperty.PropertyContract, jsonProperty, contract, containerProperty, null);
							}
							dictionary[jsonProperty] = obj;
						}
						else
						{
							reader.Skip();
						}
					}
					else
					{
						if (!reader.Read())
						{
							goto Block_11;
						}
						if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Verbose)
						{
							this.TraceWriter.Trace(TraceLevel.Verbose, JsonPosition.FormatMessage(reader as IJsonLineInfo, reader.Path, "Could not find member '{0}' on {1}.".FormatWith(CultureInfo.InvariantCulture, text, contract.UnderlyingType)), null);
						}
						if (this.Serializer._missingMemberHandling == MissingMemberHandling.Error)
						{
							goto Block_14;
						}
						reader.Skip();
					}
					break;
				}
				case JsonToken.Comment:
					break;
				default:
					if (tokenType != JsonToken.EndObject)
					{
						goto Block_2;
					}
					flag = true;
					break;
				}
				if (flag || !reader.Read())
				{
					return dictionary;
				}
			}
			Block_2:
			throw JsonSerializationException.Create(reader, "Unexpected token when deserializing object: " + reader.TokenType);
			Block_6:
			throw JsonSerializationException.Create(reader, "Unexpected end when setting {0}'s value.".FormatWith(CultureInfo.InvariantCulture, text));
			Block_11:
			throw JsonSerializationException.Create(reader, "Unexpected end when setting {0}'s value.".FormatWith(CultureInfo.InvariantCulture, text));
			Block_14:
			throw JsonSerializationException.Create(reader, "Could not find member '{0}' on object of type '{1}'".FormatWith(CultureInfo.InvariantCulture, text, objectType.Name));
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0001E9E4 File Offset: 0x0001CBE4
		private bool ReadForType(JsonReader reader, JsonContract contract, bool hasConverter)
		{
			if (hasConverter)
			{
				return reader.Read();
			}
			switch ((contract != null) ? contract.InternalReadType : ReadType.Read)
			{
			case ReadType.Read:
				while (reader.Read())
				{
					if (reader.TokenType != JsonToken.Comment)
					{
						return true;
					}
				}
				return false;
			case ReadType.ReadAsInt32:
				reader.ReadAsInt32();
				break;
			case ReadType.ReadAsBytes:
				reader.ReadAsBytes();
				break;
			case ReadType.ReadAsString:
				reader.ReadAsString();
				break;
			case ReadType.ReadAsDecimal:
				reader.ReadAsDecimal();
				break;
			case ReadType.ReadAsDateTime:
				reader.ReadAsDateTime();
				break;
			case ReadType.ReadAsDateTimeOffset:
				reader.ReadAsDateTimeOffset();
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			return reader.TokenType != JsonToken.None;
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0001EA8C File Offset: 0x0001CC8C
		public object CreateNewObject(JsonReader reader, JsonObjectContract objectContract, JsonProperty containerMember, JsonProperty containerProperty, string id, out bool createdFromNonDefaultConstructor)
		{
			object obj = null;
			if (!objectContract.IsInstantiable)
			{
				throw JsonSerializationException.Create(reader, "Could not create an instance of type {0}. Type is an interface or abstract class and cannot be instantiated.".FormatWith(CultureInfo.InvariantCulture, objectContract.UnderlyingType));
			}
			if (objectContract.OverrideConstructor != null)
			{
				if (objectContract.OverrideConstructor.GetParameters().Length > 0)
				{
					createdFromNonDefaultConstructor = true;
					return this.CreateObjectFromNonDefaultConstructor(reader, objectContract, containerMember, objectContract.OverrideConstructor, id);
				}
				obj = objectContract.OverrideConstructor.Invoke(null);
			}
			else if (objectContract.DefaultCreator != null && (!objectContract.DefaultCreatorNonPublic || this.Serializer._constructorHandling == ConstructorHandling.AllowNonPublicDefaultConstructor || objectContract.ParametrizedConstructor == null))
			{
				obj = objectContract.DefaultCreator.Invoke();
			}
			else if (objectContract.ParametrizedConstructor != null)
			{
				createdFromNonDefaultConstructor = true;
				return this.CreateObjectFromNonDefaultConstructor(reader, objectContract, containerMember, objectContract.ParametrizedConstructor, id);
			}
			if (obj == null)
			{
				throw JsonSerializationException.Create(reader, "Unable to find a constructor to use for type {0}. A class should either have a default constructor, one constructor with arguments or a constructor marked with the JsonConstructor attribute.".FormatWith(CultureInfo.InvariantCulture, objectContract.UnderlyingType));
			}
			createdFromNonDefaultConstructor = false;
			return obj;
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x0001EB78 File Offset: 0x0001CD78
		private object PopulateObject(object newObject, JsonReader reader, JsonObjectContract contract, JsonProperty member, string id)
		{
			this.OnDeserializing(reader, contract, newObject);
			Dictionary<JsonProperty, JsonSerializerInternalReader.PropertyPresence> dictionary;
			if (!contract.HasRequiredOrDefaultValueProperties && !this.HasFlag(this.Serializer._defaultValueHandling, DefaultValueHandling.Populate))
			{
				dictionary = null;
			}
			else
			{
				dictionary = Enumerable.ToDictionary<JsonProperty, JsonProperty, JsonSerializerInternalReader.PropertyPresence>(contract.Properties, (JsonProperty m) => m, (JsonProperty m) => JsonSerializerInternalReader.PropertyPresence.None);
			}
			Dictionary<JsonProperty, JsonSerializerInternalReader.PropertyPresence> dictionary2 = dictionary;
			if (id != null)
			{
				this.AddReference(reader, id, newObject);
			}
			int depth = reader.Depth;
			bool flag = false;
			for (;;)
			{
				JsonToken tokenType = reader.TokenType;
				switch (tokenType)
				{
				case JsonToken.PropertyName:
				{
					string text = reader.Value.ToString();
					try
					{
						JsonProperty closestMatchProperty = contract.Properties.GetClosestMatchProperty(text);
						if (closestMatchProperty == null)
						{
							if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Verbose)
							{
								this.TraceWriter.Trace(TraceLevel.Verbose, JsonPosition.FormatMessage(reader as IJsonLineInfo, reader.Path, "Could not find member '{0}' on {1}".FormatWith(CultureInfo.InvariantCulture, text, contract.UnderlyingType)), null);
							}
							if (this.Serializer._missingMemberHandling == MissingMemberHandling.Error)
							{
								throw JsonSerializationException.Create(reader, "Could not find member '{0}' on object of type '{1}'".FormatWith(CultureInfo.InvariantCulture, text, contract.UnderlyingType.Name));
							}
							if (!reader.Read())
							{
								break;
							}
							this.SetExtensionData(contract, reader, text, newObject);
							break;
						}
						else
						{
							if (closestMatchProperty.PropertyContract == null)
							{
								closestMatchProperty.PropertyContract = this.GetContractSafe(closestMatchProperty.PropertyType);
							}
							JsonConverter converter = this.GetConverter(closestMatchProperty.PropertyContract, closestMatchProperty.MemberConverter, contract, member);
							if (!this.ReadForType(reader, closestMatchProperty.PropertyContract, converter != null))
							{
								throw JsonSerializationException.Create(reader, "Unexpected end when setting {0}'s value.".FormatWith(CultureInfo.InvariantCulture, text));
							}
							this.SetPropertyPresence(reader, closestMatchProperty, dictionary2);
							if (!this.SetPropertyValue(closestMatchProperty, converter, contract, member, reader, newObject))
							{
								this.SetExtensionData(contract, reader, text, newObject);
							}
							break;
						}
					}
					catch (Exception ex)
					{
						if (base.IsErrorHandled(newObject, contract, text, reader as IJsonLineInfo, reader.Path, ex))
						{
							this.HandleError(reader, true, depth);
							break;
						}
						throw;
					}
					goto IL_219;
				}
				case JsonToken.Comment:
					break;
				default:
					if (tokenType != JsonToken.EndObject)
					{
						goto Block_7;
					}
					goto IL_219;
				}
				IL_239:
				if (flag || !reader.Read())
				{
					goto IL_247;
				}
				continue;
				IL_219:
				flag = true;
				goto IL_239;
			}
			Block_7:
			throw JsonSerializationException.Create(reader, "Unexpected token when deserializing object: " + reader.TokenType);
			IL_247:
			if (!flag)
			{
				this.ThrowUnexpectedEndException(reader, contract, newObject, "Unexpected end when deserializing object.");
			}
			this.EndObject(newObject, reader, contract, depth, dictionary2);
			this.OnDeserialized(reader, contract, newObject);
			return newObject;
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0001EE10 File Offset: 0x0001D010
		private void SetExtensionData(JsonObjectContract contract, JsonReader reader, string memberName, object o)
		{
			if (contract.ExtensionDataSetter != null)
			{
				try
				{
					JToken value = JToken.ReadFrom(reader);
					contract.ExtensionDataSetter(o, memberName, value);
					return;
				}
				catch (Exception ex)
				{
					throw JsonSerializationException.Create(reader, "Error setting value in extension data for type '{0}'.".FormatWith(CultureInfo.InvariantCulture, contract.UnderlyingType), ex);
				}
			}
			reader.Skip();
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0001EE74 File Offset: 0x0001D074
		private void EndObject(object newObject, JsonReader reader, JsonObjectContract contract, int initialDepth, Dictionary<JsonProperty, JsonSerializerInternalReader.PropertyPresence> propertiesPresence)
		{
			if (propertiesPresence != null)
			{
				foreach (KeyValuePair<JsonProperty, JsonSerializerInternalReader.PropertyPresence> keyValuePair in propertiesPresence)
				{
					JsonProperty key = keyValuePair.Key;
					JsonSerializerInternalReader.PropertyPresence value = keyValuePair.Value;
					if (value != JsonSerializerInternalReader.PropertyPresence.None)
					{
						if (value != JsonSerializerInternalReader.PropertyPresence.Null)
						{
							continue;
						}
					}
					try
					{
						Required required = key._required ?? (contract.ItemRequired ?? Required.Default);
						switch (value)
						{
						case JsonSerializerInternalReader.PropertyPresence.None:
							if (required == Required.AllowNull || required == Required.Always)
							{
								throw JsonSerializationException.Create(reader, "Required property '{0}' not found in JSON.".FormatWith(CultureInfo.InvariantCulture, key.PropertyName));
							}
							if (key.PropertyContract == null)
							{
								key.PropertyContract = this.GetContractSafe(key.PropertyType);
							}
							if (this.HasFlag(key.DefaultValueHandling.GetValueOrDefault(this.Serializer._defaultValueHandling), DefaultValueHandling.Populate) && key.Writable)
							{
								key.ValueProvider.SetValue(newObject, this.EnsureType(reader, key.GetResolvedDefaultValue(), CultureInfo.InvariantCulture, key.PropertyContract, key.PropertyType));
							}
							break;
						case JsonSerializerInternalReader.PropertyPresence.Null:
							if (required == Required.Always)
							{
								throw JsonSerializationException.Create(reader, "Required property '{0}' expects a value but got null.".FormatWith(CultureInfo.InvariantCulture, key.PropertyName));
							}
							break;
						}
					}
					catch (Exception ex)
					{
						if (!base.IsErrorHandled(newObject, contract, key.PropertyName, reader as IJsonLineInfo, reader.Path, ex))
						{
							throw;
						}
						this.HandleError(reader, true, initialDepth);
					}
				}
			}
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0001F03C File Offset: 0x0001D23C
		private void SetPropertyPresence(JsonReader reader, JsonProperty property, Dictionary<JsonProperty, JsonSerializerInternalReader.PropertyPresence> requiredProperties)
		{
			if (property != null && requiredProperties != null)
			{
				requiredProperties[property] = ((reader.TokenType == JsonToken.Null || reader.TokenType == JsonToken.Undefined) ? JsonSerializerInternalReader.PropertyPresence.Null : JsonSerializerInternalReader.PropertyPresence.Value);
			}
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0001F063 File Offset: 0x0001D263
		private void HandleError(JsonReader reader, bool readPastError, int initialDepth)
		{
			base.ClearErrorContext();
			if (readPastError)
			{
				reader.Skip();
				while (reader.Depth > initialDepth + 1)
				{
					if (!reader.Read())
					{
						return;
					}
				}
			}
		}

		// Token: 0x040002F4 RID: 756
		private JsonSerializerProxy _internalSerializer;

		// Token: 0x020000A0 RID: 160
		internal enum PropertyPresence
		{
			// Token: 0x040002FB RID: 763
			None,
			// Token: 0x040002FC RID: 764
			Null,
			// Token: 0x040002FD RID: 765
			Value
		}
	}
}
