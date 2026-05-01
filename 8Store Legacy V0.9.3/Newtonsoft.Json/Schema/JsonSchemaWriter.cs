using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x0200007C RID: 124
	internal class JsonSchemaWriter
	{
		// Token: 0x060006B4 RID: 1716 RVA: 0x000187EE File Offset: 0x000169EE
		public JsonSchemaWriter(JsonWriter writer, JsonSchemaResolver resolver)
		{
			ValidationUtils.ArgumentNotNull(writer, "writer");
			this._writer = writer;
			this._resolver = resolver;
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00018810 File Offset: 0x00016A10
		private void ReferenceOrWriteSchema(JsonSchema schema)
		{
			if (schema.Id != null && this._resolver.GetSchema(schema.Id) != null)
			{
				this._writer.WriteStartObject();
				this._writer.WritePropertyName("$ref");
				this._writer.WriteValue(schema.Id);
				this._writer.WriteEndObject();
				return;
			}
			this.WriteSchema(schema);
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00018878 File Offset: 0x00016A78
		public void WriteSchema(JsonSchema schema)
		{
			ValidationUtils.ArgumentNotNull(schema, "schema");
			if (!this._resolver.LoadedSchemas.Contains(schema))
			{
				this._resolver.LoadedSchemas.Add(schema);
			}
			this._writer.WriteStartObject();
			this.WritePropertyIfNotNull(this._writer, "id", schema.Id);
			this.WritePropertyIfNotNull(this._writer, "title", schema.Title);
			this.WritePropertyIfNotNull(this._writer, "description", schema.Description);
			this.WritePropertyIfNotNull(this._writer, "required", schema.Required);
			this.WritePropertyIfNotNull(this._writer, "readonly", schema.ReadOnly);
			this.WritePropertyIfNotNull(this._writer, "hidden", schema.Hidden);
			this.WritePropertyIfNotNull(this._writer, "transient", schema.Transient);
			if (schema.Type != null)
			{
				this.WriteType("type", this._writer, schema.Type.Value);
			}
			if (!schema.AllowAdditionalProperties)
			{
				this._writer.WritePropertyName("additionalProperties");
				this._writer.WriteValue(schema.AllowAdditionalProperties);
			}
			else if (schema.AdditionalProperties != null)
			{
				this._writer.WritePropertyName("additionalProperties");
				this.ReferenceOrWriteSchema(schema.AdditionalProperties);
			}
			if (!schema.AllowAdditionalItems)
			{
				this._writer.WritePropertyName("additionalItems");
				this._writer.WriteValue(schema.AllowAdditionalItems);
			}
			else if (schema.AdditionalItems != null)
			{
				this._writer.WritePropertyName("additionalItems");
				this.ReferenceOrWriteSchema(schema.AdditionalItems);
			}
			this.WriteSchemaDictionaryIfNotNull(this._writer, "properties", schema.Properties);
			this.WriteSchemaDictionaryIfNotNull(this._writer, "patternProperties", schema.PatternProperties);
			this.WriteItems(schema);
			this.WritePropertyIfNotNull(this._writer, "minimum", schema.Minimum);
			this.WritePropertyIfNotNull(this._writer, "maximum", schema.Maximum);
			this.WritePropertyIfNotNull(this._writer, "exclusiveMinimum", schema.ExclusiveMinimum);
			this.WritePropertyIfNotNull(this._writer, "exclusiveMaximum", schema.ExclusiveMaximum);
			this.WritePropertyIfNotNull(this._writer, "minLength", schema.MinimumLength);
			this.WritePropertyIfNotNull(this._writer, "maxLength", schema.MaximumLength);
			this.WritePropertyIfNotNull(this._writer, "minItems", schema.MinimumItems);
			this.WritePropertyIfNotNull(this._writer, "maxItems", schema.MaximumItems);
			this.WritePropertyIfNotNull(this._writer, "divisibleBy", schema.DivisibleBy);
			this.WritePropertyIfNotNull(this._writer, "format", schema.Format);
			this.WritePropertyIfNotNull(this._writer, "pattern", schema.Pattern);
			if (schema.Enum != null)
			{
				this._writer.WritePropertyName("enum");
				this._writer.WriteStartArray();
				foreach (JToken jtoken in schema.Enum)
				{
					jtoken.WriteTo(this._writer, new JsonConverter[0]);
				}
				this._writer.WriteEndArray();
			}
			if (schema.Default != null)
			{
				this._writer.WritePropertyName("default");
				schema.Default.WriteTo(this._writer, new JsonConverter[0]);
			}
			if (schema.Disallow != null)
			{
				this.WriteType("disallow", this._writer, schema.Disallow.Value);
			}
			if (schema.Extends != null && schema.Extends.Count > 0)
			{
				this._writer.WritePropertyName("extends");
				if (schema.Extends.Count == 1)
				{
					this.ReferenceOrWriteSchema(schema.Extends[0]);
				}
				else
				{
					this._writer.WriteStartArray();
					foreach (JsonSchema schema2 in schema.Extends)
					{
						this.ReferenceOrWriteSchema(schema2);
					}
					this._writer.WriteEndArray();
				}
			}
			this._writer.WriteEndObject();
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00018D2C File Offset: 0x00016F2C
		private void WriteSchemaDictionaryIfNotNull(JsonWriter writer, string propertyName, IDictionary<string, JsonSchema> properties)
		{
			if (properties != null)
			{
				writer.WritePropertyName(propertyName);
				writer.WriteStartObject();
				foreach (KeyValuePair<string, JsonSchema> keyValuePair in properties)
				{
					writer.WritePropertyName(keyValuePair.Key);
					this.ReferenceOrWriteSchema(keyValuePair.Value);
				}
				writer.WriteEndObject();
			}
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00018DA0 File Offset: 0x00016FA0
		private void WriteItems(JsonSchema schema)
		{
			if (schema.Items == null && !schema.PositionalItemsValidation)
			{
				return;
			}
			this._writer.WritePropertyName("items");
			if (schema.PositionalItemsValidation)
			{
				this._writer.WriteStartArray();
				if (schema.Items != null)
				{
					foreach (JsonSchema schema2 in schema.Items)
					{
						this.ReferenceOrWriteSchema(schema2);
					}
				}
				this._writer.WriteEndArray();
				return;
			}
			if (schema.Items != null && schema.Items.Count > 0)
			{
				this.ReferenceOrWriteSchema(schema.Items[0]);
				return;
			}
			this._writer.WriteStartObject();
			this._writer.WriteEndObject();
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00018E80 File Offset: 0x00017080
		private void WriteType(string propertyName, JsonWriter writer, JsonSchemaType type)
		{
			IList<JsonSchemaType> list2;
			if (Enum.IsDefined(typeof(JsonSchemaType), type))
			{
				List<JsonSchemaType> list = new List<JsonSchemaType>();
				list.Add(type);
				list2 = list;
			}
			else
			{
				list2 = Enumerable.ToList<JsonSchemaType>(Enumerable.Where<JsonSchemaType>(EnumUtils.GetFlagsValues<JsonSchemaType>(type), (JsonSchemaType v) => v != JsonSchemaType.None));
			}
			if (list2.Count == 0)
			{
				return;
			}
			writer.WritePropertyName(propertyName);
			if (list2.Count == 1)
			{
				writer.WriteValue(JsonSchemaBuilder.MapType(list2[0]));
				return;
			}
			writer.WriteStartArray();
			foreach (JsonSchemaType type2 in list2)
			{
				writer.WriteValue(JsonSchemaBuilder.MapType(type2));
			}
			writer.WriteEndArray();
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00018F5C File Offset: 0x0001715C
		private void WritePropertyIfNotNull(JsonWriter writer, string propertyName, object value)
		{
			if (value != null)
			{
				writer.WritePropertyName(propertyName);
				writer.WriteValue(value);
			}
		}

		// Token: 0x04000263 RID: 611
		private readonly JsonWriter _writer;

		// Token: 0x04000264 RID: 612
		private readonly JsonSchemaResolver _resolver;
	}
}
