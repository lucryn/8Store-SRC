using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x020000B5 RID: 181
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal class JsonSchemaNode
	{
		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x0600094B RID: 2379 RVA: 0x000262B2 File Offset: 0x000244B2
		public string Id { get; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x0600094C RID: 2380 RVA: 0x000262BA File Offset: 0x000244BA
		public ReadOnlyCollection<JsonSchema> Schemas { get; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600094D RID: 2381 RVA: 0x000262C2 File Offset: 0x000244C2
		public Dictionary<string, JsonSchemaNode> Properties { get; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x000262CA File Offset: 0x000244CA
		public Dictionary<string, JsonSchemaNode> PatternProperties { get; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x000262D2 File Offset: 0x000244D2
		public List<JsonSchemaNode> Items { get; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x000262DA File Offset: 0x000244DA
		// (set) Token: 0x06000951 RID: 2385 RVA: 0x000262E2 File Offset: 0x000244E2
		public JsonSchemaNode AdditionalProperties { get; set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000952 RID: 2386 RVA: 0x000262EB File Offset: 0x000244EB
		// (set) Token: 0x06000953 RID: 2387 RVA: 0x000262F3 File Offset: 0x000244F3
		public JsonSchemaNode AdditionalItems { get; set; }

		// Token: 0x06000954 RID: 2388 RVA: 0x000262FC File Offset: 0x000244FC
		public JsonSchemaNode(JsonSchema schema)
		{
			this.Schemas = new ReadOnlyCollection<JsonSchema>(new JsonSchema[]
			{
				schema
			});
			this.Properties = new Dictionary<string, JsonSchemaNode>();
			this.PatternProperties = new Dictionary<string, JsonSchemaNode>();
			this.Items = new List<JsonSchemaNode>();
			this.Id = JsonSchemaNode.GetId(this.Schemas);
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00026358 File Offset: 0x00024558
		private JsonSchemaNode(JsonSchemaNode source, JsonSchema schema)
		{
			this.Schemas = new ReadOnlyCollection<JsonSchema>(Enumerable.ToList<JsonSchema>(Enumerable.Union<JsonSchema>(source.Schemas, new JsonSchema[]
			{
				schema
			})));
			this.Properties = new Dictionary<string, JsonSchemaNode>(source.Properties);
			this.PatternProperties = new Dictionary<string, JsonSchemaNode>(source.PatternProperties);
			this.Items = new List<JsonSchemaNode>(source.Items);
			this.AdditionalProperties = source.AdditionalProperties;
			this.AdditionalItems = source.AdditionalItems;
			this.Id = JsonSchemaNode.GetId(this.Schemas);
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x000263EC File Offset: 0x000245EC
		public JsonSchemaNode Combine(JsonSchema schema)
		{
			return new JsonSchemaNode(this, schema);
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x000263F8 File Offset: 0x000245F8
		public static string GetId(IEnumerable<JsonSchema> schemata)
		{
			return string.Join("-", Enumerable.OrderBy<string, string>(Enumerable.Select<JsonSchema, string>(schemata, (JsonSchema s) => s.InternalId), (string id) => id, StringComparer.Ordinal));
		}
	}
}
