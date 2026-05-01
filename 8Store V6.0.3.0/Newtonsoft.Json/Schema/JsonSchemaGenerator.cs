using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x020000B2 RID: 178
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public class JsonSchemaGenerator
	{
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060008FC RID: 2300 RVA: 0x00025163 File Offset: 0x00023363
		// (set) Token: 0x060008FD RID: 2301 RVA: 0x0002516B File Offset: 0x0002336B
		public UndefinedSchemaIdHandling UndefinedSchemaIdHandling { get; set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x00025174 File Offset: 0x00023374
		// (set) Token: 0x060008FF RID: 2303 RVA: 0x0002518A File Offset: 0x0002338A
		public IContractResolver ContractResolver
		{
			[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
			[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
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

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x00025193 File Offset: 0x00023393
		private JsonSchema CurrentSchema
		{
			get
			{
				return this._currentSchema;
			}
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x0002519B File Offset: 0x0002339B
		private void Push(JsonSchemaGenerator.TypeSchema typeSchema)
		{
			this._currentSchema = typeSchema.Schema;
			this._stack.Add(typeSchema);
			this._resolver.LoadedSchemas.Add(typeSchema.Schema);
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x000251CC File Offset: 0x000233CC
		private JsonSchemaGenerator.TypeSchema Pop()
		{
			JsonSchemaGenerator.TypeSchema result = this._stack[this._stack.Count - 1];
			this._stack.RemoveAt(this._stack.Count - 1);
			JsonSchemaGenerator.TypeSchema typeSchema = Enumerable.LastOrDefault<JsonSchemaGenerator.TypeSchema>(this._stack);
			if (typeSchema != null)
			{
				this._currentSchema = typeSchema.Schema;
				return result;
			}
			this._currentSchema = null;
			return result;
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x0002522C File Offset: 0x0002342C
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public JsonSchema Generate(Type type)
		{
			return this.Generate(type, new JsonSchemaResolver(), false);
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x0002523B File Offset: 0x0002343B
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public JsonSchema Generate(Type type, JsonSchemaResolver resolver)
		{
			return this.Generate(type, resolver, false);
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x00025246 File Offset: 0x00023446
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public JsonSchema Generate(Type type, bool rootSchemaNullable)
		{
			return this.Generate(type, new JsonSchemaResolver(), rootSchemaNullable);
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x00025255 File Offset: 0x00023455
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public JsonSchema Generate(Type type, JsonSchemaResolver resolver, bool rootSchemaNullable)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			ValidationUtils.ArgumentNotNull(resolver, "resolver");
			this._resolver = resolver;
			return this.GenerateInternal(type, (!rootSchemaNullable) ? Required.Always : Required.Default, false);
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x00025284 File Offset: 0x00023484
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		private string GetTitle(Type type)
		{
			JsonContainerAttribute cachedAttribute = JsonTypeReflector.GetCachedAttribute<JsonContainerAttribute>(type);
			if (!StringUtils.IsNullOrEmpty((cachedAttribute != null) ? cachedAttribute.Title : null))
			{
				return cachedAttribute.Title;
			}
			return null;
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x000252B4 File Offset: 0x000234B4
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		private string GetDescription(Type type)
		{
			JsonContainerAttribute cachedAttribute = JsonTypeReflector.GetCachedAttribute<JsonContainerAttribute>(type);
			if (!StringUtils.IsNullOrEmpty((cachedAttribute != null) ? cachedAttribute.Description : null))
			{
				return cachedAttribute.Description;
			}
			return null;
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x000252E4 File Offset: 0x000234E4
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		private string GetTypeId(Type type, bool explicitOnly)
		{
			JsonContainerAttribute cachedAttribute = JsonTypeReflector.GetCachedAttribute<JsonContainerAttribute>(type);
			if (!StringUtils.IsNullOrEmpty((cachedAttribute != null) ? cachedAttribute.Id : null))
			{
				return cachedAttribute.Id;
			}
			if (explicitOnly)
			{
				return null;
			}
			UndefinedSchemaIdHandling undefinedSchemaIdHandling = this.UndefinedSchemaIdHandling;
			if (undefinedSchemaIdHandling == UndefinedSchemaIdHandling.UseTypeName)
			{
				return type.FullName;
			}
			if (undefinedSchemaIdHandling != UndefinedSchemaIdHandling.UseAssemblyQualifiedName)
			{
				return null;
			}
			return type.AssemblyQualifiedName;
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00025338 File Offset: 0x00023538
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		private JsonSchema GenerateInternal([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.Interfaces)] Type type, Required valueRequired, bool required)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			string typeId = this.GetTypeId(type, false);
			string typeId2 = this.GetTypeId(type, true);
			if (!StringUtils.IsNullOrEmpty(typeId))
			{
				JsonSchema schema = this._resolver.GetSchema(typeId);
				if (schema != null)
				{
					if (valueRequired != Required.Always && !JsonSchemaGenerator.HasFlag(schema.Type, JsonSchemaType.Null))
					{
						schema.Type |= JsonSchemaType.Null;
					}
					if (required && !schema.Required.GetValueOrDefault())
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
			bool flag = (jsonContract.Converter ?? jsonContract.InternalConverter) != null;
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
			if (!flag)
			{
				switch (jsonContract.ContractType)
				{
				case JsonContractType.Object:
					this.CurrentSchema.Type = new JsonSchemaType?(this.AddNullType(JsonSchemaType.Object, valueRequired));
					this.CurrentSchema.Id = this.GetTypeId(type, false);
					this.GenerateObjectSchema(type, (JsonObjectContract)jsonContract);
					goto IL_431;
				case JsonContractType.Array:
				{
					this.CurrentSchema.Type = new JsonSchemaType?(this.AddNullType(JsonSchemaType.Array, valueRequired));
					this.CurrentSchema.Id = this.GetTypeId(type, false);
					JsonArrayAttribute cachedAttribute = JsonTypeReflector.GetCachedAttribute<JsonArrayAttribute>(type);
					bool flag2 = cachedAttribute == null || cachedAttribute.AllowNullItems;
					Type collectionItemType = ReflectionUtils.GetCollectionItemType(type);
					if (collectionItemType != null)
					{
						this.CurrentSchema.Items = new List<JsonSchema>();
						this.CurrentSchema.Items.Add(this.GenerateInternal(collectionItemType, (!flag2) ? Required.Always : Required.Default, false));
						goto IL_431;
					}
					goto IL_431;
				}
				case JsonContractType.Primitive:
					this.CurrentSchema.Type = new JsonSchemaType?(this.GetJsonSchemaType(type, valueRequired));
					if (this.CurrentSchema.Type.GetValueOrDefault() == JsonSchemaType.Integer && type.IsEnum() && !type.IsDefined(typeof(FlagsAttribute), true))
					{
						this.CurrentSchema.Enum = new List<JToken>();
						EnumInfo enumValuesAndNames = EnumUtils.GetEnumValuesAndNames(type);
						for (int i = 0; i < enumValuesAndNames.Names.Length; i++)
						{
							ulong num = enumValuesAndNames.Values[i];
							JToken jtoken = JToken.FromObject(Enum.ToObject(type, num));
							this.CurrentSchema.Enum.Add(jtoken);
						}
						goto IL_431;
					}
					goto IL_431;
				case JsonContractType.String:
				{
					JsonSchemaType jsonSchemaType = (!ReflectionUtils.IsNullable(jsonContract.UnderlyingType)) ? JsonSchemaType.String : this.AddNullType(JsonSchemaType.String, valueRequired);
					this.CurrentSchema.Type = new JsonSchemaType?(jsonSchemaType);
					goto IL_431;
				}
				case JsonContractType.Dictionary:
				{
					this.CurrentSchema.Type = new JsonSchemaType?(this.AddNullType(JsonSchemaType.Object, valueRequired));
					Type type2;
					Type type3;
					ReflectionUtils.GetDictionaryKeyValueTypes(type, out type2, out type3);
					if (type2 != null && this.ContractResolver.ResolveContract(type2).ContractType == JsonContractType.Primitive)
					{
						this.CurrentSchema.AdditionalProperties = this.GenerateInternal(type3, Required.Default, false);
						goto IL_431;
					}
					goto IL_431;
				}
				case JsonContractType.Dynamic:
				case JsonContractType.Linq:
					this.CurrentSchema.Type = new JsonSchemaType?(JsonSchemaType.Any);
					goto IL_431;
				}
				throw new JsonException("Unexpected contract type: {0}".FormatWith(CultureInfo.InvariantCulture, jsonContract));
			}
			this.CurrentSchema.Type = new JsonSchemaType?(JsonSchemaType.Any);
			IL_431:
			return this.Pop().Schema;
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x00025781 File Offset: 0x00023981
		private JsonSchemaType AddNullType(JsonSchemaType type, Required valueRequired)
		{
			if (valueRequired != Required.Always)
			{
				return type | JsonSchemaType.Null;
			}
			return type;
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x0002578D File Offset: 0x0002398D
		private bool HasFlag(DefaultValueHandling value, DefaultValueHandling flag)
		{
			return (value & flag) == flag;
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x00025798 File Offset: 0x00023998
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		private void GenerateObjectSchema(Type type, JsonObjectContract contract)
		{
			this.CurrentSchema.Properties = new Dictionary<string, JsonSchema>();
			foreach (JsonProperty jsonProperty in contract.Properties)
			{
				if (!jsonProperty.Ignored)
				{
					bool flag = jsonProperty.NullValueHandling.GetValueOrDefault() == NullValueHandling.Ignore || this.HasFlag(jsonProperty.DefaultValueHandling.GetValueOrDefault(), DefaultValueHandling.Ignore) || jsonProperty.ShouldSerialize != null || jsonProperty.GetIsSpecified != null;
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

		// Token: 0x0600090E RID: 2318 RVA: 0x0002589C File Offset: 0x00023A9C
		internal static bool HasFlag(JsonSchemaType? value, JsonSchemaType flag)
		{
			if (value == null)
			{
				return true;
			}
			JsonSchemaType? jsonSchemaType = value & flag;
			return (jsonSchemaType.GetValueOrDefault() == flag & jsonSchemaType != null) || (flag == JsonSchemaType.Integer && (value & JsonSchemaType.Float).GetValueOrDefault() == JsonSchemaType.Float);
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x0002592C File Offset: 0x00023B2C
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

		// Token: 0x04000372 RID: 882
		private IContractResolver _contractResolver;

		// Token: 0x04000373 RID: 883
		private JsonSchemaResolver _resolver;

		// Token: 0x04000374 RID: 884
		private readonly IList<JsonSchemaGenerator.TypeSchema> _stack = new List<JsonSchemaGenerator.TypeSchema>();

		// Token: 0x04000375 RID: 885
		private JsonSchema _currentSchema;

		// Token: 0x020001BD RID: 445
		private class TypeSchema
		{
			// Token: 0x1700027D RID: 637
			// (get) Token: 0x06000EE5 RID: 3813 RVA: 0x00040AAD File Offset: 0x0003ECAD
			public Type Type { get; }

			// Token: 0x1700027E RID: 638
			// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x00040AB5 File Offset: 0x0003ECB5
			public JsonSchema Schema { get; }

			// Token: 0x06000EE7 RID: 3815 RVA: 0x00040ABD File Offset: 0x0003ECBD
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
