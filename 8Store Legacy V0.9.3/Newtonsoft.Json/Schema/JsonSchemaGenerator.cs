using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	/// <summary>
	/// Generates a <see cref="T:Newtonsoft.Json.Schema.JsonSchema" /> from a specified <see cref="T:System.Type" />.
	/// </summary>
	// Token: 0x02000074 RID: 116
	public class JsonSchemaGenerator
	{
		/// <summary>
		/// Gets or sets how undefined schemas are handled by the serializer.
		/// </summary>
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x000172EC File Offset: 0x000154EC
		// (set) Token: 0x06000647 RID: 1607 RVA: 0x000172F4 File Offset: 0x000154F4
		public UndefinedSchemaIdHandling UndefinedSchemaIdHandling { get; set; }

		/// <summary>
		/// Gets or sets the contract resolver.
		/// </summary>
		/// <value>The contract resolver.</value>
		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x000172FD File Offset: 0x000154FD
		// (set) Token: 0x06000649 RID: 1609 RVA: 0x00017313 File Offset: 0x00015513
		public IContractResolver ContractResolver
		{
			get
			{
				if (this._contractResolver == null)
				{
					return DefaultContractResolver.Instance;
				}
				return this._contractResolver;
			}
			set
			{
				this._contractResolver = value;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x0001731C File Offset: 0x0001551C
		private JsonSchema CurrentSchema
		{
			get
			{
				return this._currentSchema;
			}
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00017324 File Offset: 0x00015524
		private void Push(JsonSchemaGenerator.TypeSchema typeSchema)
		{
			this._currentSchema = typeSchema.Schema;
			this._stack.Add(typeSchema);
			this._resolver.LoadedSchemas.Add(typeSchema.Schema);
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00017354 File Offset: 0x00015554
		private JsonSchemaGenerator.TypeSchema Pop()
		{
			JsonSchemaGenerator.TypeSchema result = this._stack[this._stack.Count - 1];
			this._stack.RemoveAt(this._stack.Count - 1);
			JsonSchemaGenerator.TypeSchema typeSchema = Enumerable.LastOrDefault<JsonSchemaGenerator.TypeSchema>(this._stack);
			if (typeSchema != null)
			{
				this._currentSchema = typeSchema.Schema;
			}
			else
			{
				this._currentSchema = null;
			}
			return result;
		}

		/// <summary>
		/// Generate a <see cref="T:Newtonsoft.Json.Schema.JsonSchema" /> from the specified type.
		/// </summary>
		/// <param name="type">The type to generate a <see cref="T:Newtonsoft.Json.Schema.JsonSchema" /> from.</param>
		/// <returns>A <see cref="T:Newtonsoft.Json.Schema.JsonSchema" /> generated from the specified type.</returns>
		// Token: 0x0600064D RID: 1613 RVA: 0x000173B7 File Offset: 0x000155B7
		public JsonSchema Generate(Type type)
		{
			return this.Generate(type, new JsonSchemaResolver(), false);
		}

		/// <summary>
		/// Generate a <see cref="T:Newtonsoft.Json.Schema.JsonSchema" /> from the specified type.
		/// </summary>
		/// <param name="type">The type to generate a <see cref="T:Newtonsoft.Json.Schema.JsonSchema" /> from.</param>
		/// <param name="resolver">The <see cref="T:Newtonsoft.Json.Schema.JsonSchemaResolver" /> used to resolve schema references.</param>
		/// <returns>A <see cref="T:Newtonsoft.Json.Schema.JsonSchema" /> generated from the specified type.</returns>
		// Token: 0x0600064E RID: 1614 RVA: 0x000173C6 File Offset: 0x000155C6
		public JsonSchema Generate(Type type, JsonSchemaResolver resolver)
		{
			return this.Generate(type, resolver, false);
		}

		/// <summary>
		/// Generate a <see cref="T:Newtonsoft.Json.Schema.JsonSchema" /> from the specified type.
		/// </summary>
		/// <param name="type">The type to generate a <see cref="T:Newtonsoft.Json.Schema.JsonSchema" /> from.</param>
		/// <param name="rootSchemaNullable">Specify whether the generated root <see cref="T:Newtonsoft.Json.Schema.JsonSchema" /> will be nullable.</param>
		/// <returns>A <see cref="T:Newtonsoft.Json.Schema.JsonSchema" /> generated from the specified type.</returns>
		// Token: 0x0600064F RID: 1615 RVA: 0x000173D1 File Offset: 0x000155D1
		public JsonSchema Generate(Type type, bool rootSchemaNullable)
		{
			return this.Generate(type, new JsonSchemaResolver(), rootSchemaNullable);
		}

		/// <summary>
		/// Generate a <see cref="T:Newtonsoft.Json.Schema.JsonSchema" /> from the specified type.
		/// </summary>
		/// <param name="type">The type to generate a <see cref="T:Newtonsoft.Json.Schema.JsonSchema" /> from.</param>
		/// <param name="resolver">The <see cref="T:Newtonsoft.Json.Schema.JsonSchemaResolver" /> used to resolve schema references.</param>
		/// <param name="rootSchemaNullable">Specify whether the generated root <see cref="T:Newtonsoft.Json.Schema.JsonSchema" /> will be nullable.</param>
		/// <returns>A <see cref="T:Newtonsoft.Json.Schema.JsonSchema" /> generated from the specified type.</returns>
		// Token: 0x06000650 RID: 1616 RVA: 0x000173E0 File Offset: 0x000155E0
		public JsonSchema Generate(Type type, JsonSchemaResolver resolver, bool rootSchemaNullable)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			ValidationUtils.ArgumentNotNull(resolver, "resolver");
			this._resolver = resolver;
			return this.GenerateInternal(type, (!rootSchemaNullable) ? Required.Always : Required.Default, false);
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00017410 File Offset: 0x00015610
		private string GetTitle(Type type)
		{
			JsonContainerAttribute jsonContainerAttribute = JsonTypeReflector.GetJsonContainerAttribute(type);
			if (jsonContainerAttribute != null && !string.IsNullOrEmpty(jsonContainerAttribute.Title))
			{
				return jsonContainerAttribute.Title;
			}
			return null;
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0001743C File Offset: 0x0001563C
		private string GetDescription(Type type)
		{
			JsonContainerAttribute jsonContainerAttribute = JsonTypeReflector.GetJsonContainerAttribute(type);
			if (jsonContainerAttribute != null && !string.IsNullOrEmpty(jsonContainerAttribute.Description))
			{
				return jsonContainerAttribute.Description;
			}
			return null;
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00017468 File Offset: 0x00015668
		private string GetTypeId(Type type, bool explicitOnly)
		{
			JsonContainerAttribute jsonContainerAttribute = JsonTypeReflector.GetJsonContainerAttribute(type);
			if (jsonContainerAttribute != null && !string.IsNullOrEmpty(jsonContainerAttribute.Id))
			{
				return jsonContainerAttribute.Id;
			}
			if (explicitOnly)
			{
				return null;
			}
			switch (this.UndefinedSchemaIdHandling)
			{
			case UndefinedSchemaIdHandling.UseTypeName:
				return type.FullName;
			case UndefinedSchemaIdHandling.UseAssemblyQualifiedName:
				return type.AssemblyQualifiedName;
			default:
				return null;
			}
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x000174D8 File Offset: 0x000156D8
		private JsonSchema GenerateInternal(Type type, Required valueRequired, bool required)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			string typeId = this.GetTypeId(type, false);
			string typeId2 = this.GetTypeId(type, true);
			if (!string.IsNullOrEmpty(typeId))
			{
				JsonSchema schema = this._resolver.GetSchema(typeId);
				if (schema != null)
				{
					if (valueRequired != Required.Always && !JsonSchemaGenerator.HasFlag(schema.Type, JsonSchemaType.Null))
					{
						schema.Type |= JsonSchemaType.Null;
					}
					if (required && schema.Required != true)
					{
						schema.Required = new bool?(true);
					}
					return schema;
				}
			}
			if (Enumerable.Any<JsonSchemaGenerator.TypeSchema>(this._stack, (JsonSchemaGenerator.TypeSchema tc) => tc.Type == type))
			{
				throw new JsonException("Unresolved circular reference for type '{0}'. Explicitly define an Id for the type using a JsonObject/JsonArray attribute or automatically generate a type Id using the UndefinedSchemaIdHandling property.".FormatWith(CultureInfo.InvariantCulture, type));
			}
			JsonContract jsonContract = this.ContractResolver.ResolveContract(type);
			JsonConverter jsonConverter;
			if ((jsonConverter = jsonContract.Converter) != null || (jsonConverter = jsonContract.InternalConverter) != null)
			{
				JsonSchema schema2 = jsonConverter.GetSchema();
				if (schema2 != null)
				{
					return schema2;
				}
			}
			this.Push(new JsonSchemaGenerator.TypeSchema(type, new JsonSchema()));
			if (typeId2 != null)
			{
				this.CurrentSchema.Id = typeId2;
			}
			if (required)
			{
				this.CurrentSchema.Required = new bool?(true);
			}
			this.CurrentSchema.Title = this.GetTitle(type);
			this.CurrentSchema.Description = this.GetDescription(type);
			if (jsonConverter != null)
			{
				this.CurrentSchema.Type = new JsonSchemaType?(JsonSchemaType.Any);
			}
			else
			{
				switch (jsonContract.ContractType)
				{
				case JsonContractType.Object:
					this.CurrentSchema.Type = new JsonSchemaType?(this.AddNullType(JsonSchemaType.Object, valueRequired));
					this.CurrentSchema.Id = this.GetTypeId(type, false);
					this.GenerateObjectSchema(type, (JsonObjectContract)jsonContract);
					goto IL_479;
				case JsonContractType.Array:
				{
					this.CurrentSchema.Type = new JsonSchemaType?(this.AddNullType(JsonSchemaType.Array, valueRequired));
					this.CurrentSchema.Id = this.GetTypeId(type, false);
					JsonArrayAttribute jsonArrayAttribute = JsonTypeReflector.GetJsonContainerAttribute(type) as JsonArrayAttribute;
					bool flag = jsonArrayAttribute == null || jsonArrayAttribute.AllowNullItems;
					Type collectionItemType = ReflectionUtils.GetCollectionItemType(type);
					if (collectionItemType != null)
					{
						this.CurrentSchema.Items = new List<JsonSchema>();
						this.CurrentSchema.Items.Add(this.GenerateInternal(collectionItemType, (!flag) ? Required.Always : Required.Default, false));
						goto IL_479;
					}
					goto IL_479;
				}
				case JsonContractType.Primitive:
				{
					this.CurrentSchema.Type = new JsonSchemaType?(this.GetJsonSchemaType(type, valueRequired));
					if (!(this.CurrentSchema.Type == JsonSchemaType.Integer) || !type.IsEnum() || type.IsDefined(typeof(FlagsAttribute), true))
					{
						goto IL_479;
					}
					this.CurrentSchema.Enum = new List<JToken>();
					EnumValues<long> namesAndValues = EnumUtils.GetNamesAndValues<long>(type);
					using (IEnumerator<EnumValue<long>> enumerator = namesAndValues.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							EnumValue<long> enumValue = enumerator.Current;
							JToken jtoken = JToken.FromObject(enumValue.Value);
							this.CurrentSchema.Enum.Add(jtoken);
						}
						goto IL_479;
					}
					break;
				}
				case JsonContractType.String:
					break;
				case JsonContractType.Dictionary:
				{
					this.CurrentSchema.Type = new JsonSchemaType?(this.AddNullType(JsonSchemaType.Object, valueRequired));
					Type type2;
					Type type3;
					ReflectionUtils.GetDictionaryKeyValueTypes(type, out type2, out type3);
					if (type2 == null)
					{
						goto IL_479;
					}
					JsonContract jsonContract2 = this.ContractResolver.ResolveContract(type2);
					if (jsonContract2.ContractType == JsonContractType.Primitive)
					{
						this.CurrentSchema.AdditionalProperties = this.GenerateInternal(type3, Required.Default, false);
						goto IL_479;
					}
					goto IL_479;
				}
				case JsonContractType.Dynamic:
				case JsonContractType.Linq:
					this.CurrentSchema.Type = new JsonSchemaType?(JsonSchemaType.Any);
					goto IL_479;
				default:
					throw new JsonException("Unexpected contract type: {0}".FormatWith(CultureInfo.InvariantCulture, jsonContract));
				}
				JsonSchemaType jsonSchemaType = (!ReflectionUtils.IsNullable(jsonContract.UnderlyingType)) ? JsonSchemaType.String : this.AddNullType(JsonSchemaType.String, valueRequired);
				this.CurrentSchema.Type = new JsonSchemaType?(jsonSchemaType);
			}
			IL_479:
			return this.Pop().Schema;
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0001797C File Offset: 0x00015B7C
		private JsonSchemaType AddNullType(JsonSchemaType type, Required valueRequired)
		{
			if (valueRequired != Required.Always)
			{
				return type | JsonSchemaType.Null;
			}
			return type;
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00017988 File Offset: 0x00015B88
		private bool HasFlag(DefaultValueHandling value, DefaultValueHandling flag)
		{
			return (value & flag) == flag;
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00017990 File Offset: 0x00015B90
		private void GenerateObjectSchema(Type type, JsonObjectContract contract)
		{
			this.CurrentSchema.Properties = new Dictionary<string, JsonSchema>();
			foreach (JsonProperty jsonProperty in contract.Properties)
			{
				if (!jsonProperty.Ignored)
				{
					bool flag = jsonProperty.NullValueHandling == NullValueHandling.Ignore || this.HasFlag(jsonProperty.DefaultValueHandling.GetValueOrDefault(), DefaultValueHandling.Ignore) || jsonProperty.ShouldSerialize != null || jsonProperty.GetIsSpecified != null;
					JsonSchema jsonSchema = this.GenerateInternal(jsonProperty.PropertyType, jsonProperty.Required, !flag);
					if (jsonProperty.DefaultValue != null)
					{
						jsonSchema.Default = JToken.FromObject(jsonProperty.DefaultValue);
					}
					this.CurrentSchema.Properties.Add(jsonProperty.PropertyName, jsonSchema);
				}
			}
			if (type.IsSealed())
			{
				this.CurrentSchema.AllowAdditionalProperties = false;
			}
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00017AA0 File Offset: 0x00015CA0
		internal static bool HasFlag(JsonSchemaType? value, JsonSchemaType flag)
		{
			if (value == null)
			{
				return true;
			}
			bool flag2 = (value & flag) == flag;
			return flag2 || (value == JsonSchemaType.Float && flag == JsonSchemaType.Integer);
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00017B20 File Offset: 0x00015D20
		private JsonSchemaType GetJsonSchemaType(Type type, Required valueRequired)
		{
			JsonSchemaType jsonSchemaType = JsonSchemaType.None;
			if (valueRequired != Required.Always && ReflectionUtils.IsNullable(type))
			{
				jsonSchemaType = JsonSchemaType.Null;
				if (ReflectionUtils.IsNullableType(type))
				{
					type = Nullable.GetUnderlyingType(type);
				}
			}
			PrimitiveTypeCode typeCode = ConvertUtils.GetTypeCode(type);
			switch (typeCode)
			{
			case PrimitiveTypeCode.Empty:
			case PrimitiveTypeCode.Object:
				return jsonSchemaType | JsonSchemaType.String;
			case PrimitiveTypeCode.Char:
				return jsonSchemaType | JsonSchemaType.String;
			case PrimitiveTypeCode.Boolean:
				return jsonSchemaType | JsonSchemaType.Boolean;
			case PrimitiveTypeCode.SByte:
			case PrimitiveTypeCode.Int16:
			case PrimitiveTypeCode.UInt16:
			case PrimitiveTypeCode.Int32:
			case PrimitiveTypeCode.Byte:
			case PrimitiveTypeCode.UInt32:
			case PrimitiveTypeCode.Int64:
			case PrimitiveTypeCode.UInt64:
			case PrimitiveTypeCode.BigInteger:
				return jsonSchemaType | JsonSchemaType.Integer;
			case PrimitiveTypeCode.Single:
			case PrimitiveTypeCode.Double:
			case PrimitiveTypeCode.Decimal:
				return jsonSchemaType | JsonSchemaType.Float;
			case PrimitiveTypeCode.DateTime:
			case PrimitiveTypeCode.DateTimeOffset:
				return jsonSchemaType | JsonSchemaType.String;
			case PrimitiveTypeCode.Guid:
			case PrimitiveTypeCode.TimeSpan:
			case PrimitiveTypeCode.Uri:
			case PrimitiveTypeCode.String:
			case PrimitiveTypeCode.Bytes:
				return jsonSchemaType | JsonSchemaType.String;
			}
			throw new JsonException("Unexpected type code '{0}' for type '{1}'.".FormatWith(CultureInfo.InvariantCulture, typeCode, type));
		}

		// Token: 0x0400022E RID: 558
		private IContractResolver _contractResolver;

		// Token: 0x0400022F RID: 559
		private JsonSchemaResolver _resolver;

		// Token: 0x04000230 RID: 560
		private readonly IList<JsonSchemaGenerator.TypeSchema> _stack = new List<JsonSchemaGenerator.TypeSchema>();

		// Token: 0x04000231 RID: 561
		private JsonSchema _currentSchema;

		// Token: 0x02000075 RID: 117
		private class TypeSchema
		{
			// Token: 0x17000144 RID: 324
			// (get) Token: 0x0600065B RID: 1627 RVA: 0x00017C4D File Offset: 0x00015E4D
			// (set) Token: 0x0600065C RID: 1628 RVA: 0x00017C55 File Offset: 0x00015E55
			public Type Type { get; private set; }

			// Token: 0x17000145 RID: 325
			// (get) Token: 0x0600065D RID: 1629 RVA: 0x00017C5E File Offset: 0x00015E5E
			// (set) Token: 0x0600065E RID: 1630 RVA: 0x00017C66 File Offset: 0x00015E66
			public JsonSchema Schema { get; private set; }

			// Token: 0x0600065F RID: 1631 RVA: 0x00017C6F File Offset: 0x00015E6F
			public TypeSchema(Type type, JsonSchema schema)
			{
				ValidationUtils.ArgumentNotNull(type, "type");
				ValidationUtils.ArgumentNotNull(schema, "schema");
				this.Type = type;
				this.Schema = schema;
			}
		}
	}
}
