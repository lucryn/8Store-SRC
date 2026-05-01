using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x02000078 RID: 120
	internal class JsonSchemaNode
	{
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x00018552 File Offset: 0x00016752
		// (set) Token: 0x0600069B RID: 1691 RVA: 0x0001855A File Offset: 0x0001675A
		public string Id { get; private set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x00018563 File Offset: 0x00016763
		// (set) Token: 0x0600069D RID: 1693 RVA: 0x0001856B File Offset: 0x0001676B
		public ReadOnlyCollection<JsonSchema> Schemas { get; private set; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x00018574 File Offset: 0x00016774
		// (set) Token: 0x0600069F RID: 1695 RVA: 0x0001857C File Offset: 0x0001677C
		public Dictionary<string, JsonSchemaNode> Properties { get; private set; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x00018585 File Offset: 0x00016785
		// (set) Token: 0x060006A1 RID: 1697 RVA: 0x0001858D File Offset: 0x0001678D
		public Dictionary<string, JsonSchemaNode> PatternProperties { get; private set; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x00018596 File Offset: 0x00016796
		// (set) Token: 0x060006A3 RID: 1699 RVA: 0x0001859E File Offset: 0x0001679E
		public List<JsonSchemaNode> Items { get; private set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x000185A7 File Offset: 0x000167A7
		// (set) Token: 0x060006A5 RID: 1701 RVA: 0x000185AF File Offset: 0x000167AF
		public JsonSchemaNode AdditionalProperties { get; set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x000185B8 File Offset: 0x000167B8
		// (set) Token: 0x060006A7 RID: 1703 RVA: 0x000185C0 File Offset: 0x000167C0
		public JsonSchemaNode AdditionalItems { get; set; }

		// Token: 0x060006A8 RID: 1704 RVA: 0x000185CC File Offset: 0x000167CC
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

		// Token: 0x060006A9 RID: 1705 RVA: 0x00018628 File Offset: 0x00016828
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

		// Token: 0x060006AA RID: 1706 RVA: 0x000186BE File Offset: 0x000168BE
		public JsonSchemaNode Combine(JsonSchema schema)
		{
			return new JsonSchemaNode(this, schema);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x000186D4 File Offset: 0x000168D4
		public static string GetId(IEnumerable<JsonSchema> schemata)
		{
			return string.Join("-", Enumerable.ToArray<string>(Enumerable.OrderBy<string, string>(Enumerable.Select<JsonSchema, string>(schemata, (JsonSchema s) => s.InternalId), (string id) => id, StringComparer.Ordinal)));
		}
	}
}
