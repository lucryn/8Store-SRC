using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x02000076 RID: 118
	internal class JsonSchemaModel
	{
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x00017C9B File Offset: 0x00015E9B
		// (set) Token: 0x06000661 RID: 1633 RVA: 0x00017CA3 File Offset: 0x00015EA3
		public bool Required { get; set; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x00017CAC File Offset: 0x00015EAC
		// (set) Token: 0x06000663 RID: 1635 RVA: 0x00017CB4 File Offset: 0x00015EB4
		public JsonSchemaType Type { get; set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x00017CBD File Offset: 0x00015EBD
		// (set) Token: 0x06000665 RID: 1637 RVA: 0x00017CC5 File Offset: 0x00015EC5
		public int? MinimumLength { get; set; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x00017CCE File Offset: 0x00015ECE
		// (set) Token: 0x06000667 RID: 1639 RVA: 0x00017CD6 File Offset: 0x00015ED6
		public int? MaximumLength { get; set; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x00017CDF File Offset: 0x00015EDF
		// (set) Token: 0x06000669 RID: 1641 RVA: 0x00017CE7 File Offset: 0x00015EE7
		public double? DivisibleBy { get; set; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x00017CF0 File Offset: 0x00015EF0
		// (set) Token: 0x0600066B RID: 1643 RVA: 0x00017CF8 File Offset: 0x00015EF8
		public double? Minimum { get; set; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x00017D01 File Offset: 0x00015F01
		// (set) Token: 0x0600066D RID: 1645 RVA: 0x00017D09 File Offset: 0x00015F09
		public double? Maximum { get; set; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x00017D12 File Offset: 0x00015F12
		// (set) Token: 0x0600066F RID: 1647 RVA: 0x00017D1A File Offset: 0x00015F1A
		public bool ExclusiveMinimum { get; set; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x00017D23 File Offset: 0x00015F23
		// (set) Token: 0x06000671 RID: 1649 RVA: 0x00017D2B File Offset: 0x00015F2B
		public bool ExclusiveMaximum { get; set; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x00017D34 File Offset: 0x00015F34
		// (set) Token: 0x06000673 RID: 1651 RVA: 0x00017D3C File Offset: 0x00015F3C
		public int? MinimumItems { get; set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x00017D45 File Offset: 0x00015F45
		// (set) Token: 0x06000675 RID: 1653 RVA: 0x00017D4D File Offset: 0x00015F4D
		public int? MaximumItems { get; set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x00017D56 File Offset: 0x00015F56
		// (set) Token: 0x06000677 RID: 1655 RVA: 0x00017D5E File Offset: 0x00015F5E
		public IList<string> Patterns { get; set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x00017D67 File Offset: 0x00015F67
		// (set) Token: 0x06000679 RID: 1657 RVA: 0x00017D6F File Offset: 0x00015F6F
		public IList<JsonSchemaModel> Items { get; set; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x00017D78 File Offset: 0x00015F78
		// (set) Token: 0x0600067B RID: 1659 RVA: 0x00017D80 File Offset: 0x00015F80
		public IDictionary<string, JsonSchemaModel> Properties { get; set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600067C RID: 1660 RVA: 0x00017D89 File Offset: 0x00015F89
		// (set) Token: 0x0600067D RID: 1661 RVA: 0x00017D91 File Offset: 0x00015F91
		public IDictionary<string, JsonSchemaModel> PatternProperties { get; set; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600067E RID: 1662 RVA: 0x00017D9A File Offset: 0x00015F9A
		// (set) Token: 0x0600067F RID: 1663 RVA: 0x00017DA2 File Offset: 0x00015FA2
		public JsonSchemaModel AdditionalProperties { get; set; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x00017DAB File Offset: 0x00015FAB
		// (set) Token: 0x06000681 RID: 1665 RVA: 0x00017DB3 File Offset: 0x00015FB3
		public JsonSchemaModel AdditionalItems { get; set; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x00017DBC File Offset: 0x00015FBC
		// (set) Token: 0x06000683 RID: 1667 RVA: 0x00017DC4 File Offset: 0x00015FC4
		public bool PositionalItemsValidation { get; set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x00017DCD File Offset: 0x00015FCD
		// (set) Token: 0x06000685 RID: 1669 RVA: 0x00017DD5 File Offset: 0x00015FD5
		public bool AllowAdditionalProperties { get; set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x00017DDE File Offset: 0x00015FDE
		// (set) Token: 0x06000687 RID: 1671 RVA: 0x00017DE6 File Offset: 0x00015FE6
		public bool AllowAdditionalItems { get; set; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x00017DEF File Offset: 0x00015FEF
		// (set) Token: 0x06000689 RID: 1673 RVA: 0x00017DF7 File Offset: 0x00015FF7
		public bool UniqueItems { get; set; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x00017E00 File Offset: 0x00016000
		// (set) Token: 0x0600068B RID: 1675 RVA: 0x00017E08 File Offset: 0x00016008
		public IList<JToken> Enum { get; set; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x00017E11 File Offset: 0x00016011
		// (set) Token: 0x0600068D RID: 1677 RVA: 0x00017E19 File Offset: 0x00016019
		public JsonSchemaType Disallow { get; set; }

		// Token: 0x0600068E RID: 1678 RVA: 0x00017E22 File Offset: 0x00016022
		public JsonSchemaModel()
		{
			this.Type = JsonSchemaType.Any;
			this.AllowAdditionalProperties = true;
			this.AllowAdditionalItems = true;
			this.Required = false;
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x00017E48 File Offset: 0x00016048
		public static JsonSchemaModel Create(IList<JsonSchema> schemata)
		{
			JsonSchemaModel jsonSchemaModel = new JsonSchemaModel();
			foreach (JsonSchema schema in schemata)
			{
				JsonSchemaModel.Combine(jsonSchemaModel, schema);
			}
			return jsonSchemaModel;
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00017E98 File Offset: 0x00016098
		private static void Combine(JsonSchemaModel model, JsonSchema schema)
		{
			model.Required = (model.Required || (schema.Required ?? false));
			model.Type &= (schema.Type ?? JsonSchemaType.Any);
			model.MinimumLength = MathUtils.Max(model.MinimumLength, schema.MinimumLength);
			model.MaximumLength = MathUtils.Min(model.MaximumLength, schema.MaximumLength);
			model.DivisibleBy = MathUtils.Max(model.DivisibleBy, schema.DivisibleBy);
			model.Minimum = MathUtils.Max(model.Minimum, schema.Minimum);
			model.Maximum = MathUtils.Max(model.Maximum, schema.Maximum);
			model.ExclusiveMinimum = (model.ExclusiveMinimum || (schema.ExclusiveMinimum ?? false));
			model.ExclusiveMaximum = (model.ExclusiveMaximum || (schema.ExclusiveMaximum ?? false));
			model.MinimumItems = MathUtils.Max(model.MinimumItems, schema.MinimumItems);
			model.MaximumItems = MathUtils.Min(model.MaximumItems, schema.MaximumItems);
			model.PositionalItemsValidation = (model.PositionalItemsValidation || schema.PositionalItemsValidation);
			model.AllowAdditionalProperties = (model.AllowAdditionalProperties && schema.AllowAdditionalProperties);
			model.AllowAdditionalItems = (model.AllowAdditionalItems && schema.AllowAdditionalItems);
			model.UniqueItems = (model.UniqueItems || schema.UniqueItems);
			if (schema.Enum != null)
			{
				if (model.Enum == null)
				{
					model.Enum = new List<JToken>();
				}
				model.Enum.AddRangeDistinct(schema.Enum, JToken.EqualityComparer);
			}
			model.Disallow |= (schema.Disallow ?? JsonSchemaType.None);
			if (schema.Pattern != null)
			{
				if (model.Patterns == null)
				{
					model.Patterns = new List<string>();
				}
				model.Patterns.AddDistinct(schema.Pattern);
			}
		}
	}
}
