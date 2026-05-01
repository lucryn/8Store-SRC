using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x020000B3 RID: 179
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal class JsonSchemaModel
	{
		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x00025A57 File Offset: 0x00023C57
		// (set) Token: 0x06000912 RID: 2322 RVA: 0x00025A5F File Offset: 0x00023C5F
		public bool Required { get; set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x00025A68 File Offset: 0x00023C68
		// (set) Token: 0x06000914 RID: 2324 RVA: 0x00025A70 File Offset: 0x00023C70
		public JsonSchemaType Type { get; set; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x00025A79 File Offset: 0x00023C79
		// (set) Token: 0x06000916 RID: 2326 RVA: 0x00025A81 File Offset: 0x00023C81
		public int? MinimumLength { get; set; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x00025A8A File Offset: 0x00023C8A
		// (set) Token: 0x06000918 RID: 2328 RVA: 0x00025A92 File Offset: 0x00023C92
		public int? MaximumLength { get; set; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x00025A9B File Offset: 0x00023C9B
		// (set) Token: 0x0600091A RID: 2330 RVA: 0x00025AA3 File Offset: 0x00023CA3
		public double? DivisibleBy { get; set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x00025AAC File Offset: 0x00023CAC
		// (set) Token: 0x0600091C RID: 2332 RVA: 0x00025AB4 File Offset: 0x00023CB4
		public double? Minimum { get; set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x00025ABD File Offset: 0x00023CBD
		// (set) Token: 0x0600091E RID: 2334 RVA: 0x00025AC5 File Offset: 0x00023CC5
		public double? Maximum { get; set; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x00025ACE File Offset: 0x00023CCE
		// (set) Token: 0x06000920 RID: 2336 RVA: 0x00025AD6 File Offset: 0x00023CD6
		public bool ExclusiveMinimum { get; set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x00025ADF File Offset: 0x00023CDF
		// (set) Token: 0x06000922 RID: 2338 RVA: 0x00025AE7 File Offset: 0x00023CE7
		public bool ExclusiveMaximum { get; set; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x00025AF0 File Offset: 0x00023CF0
		// (set) Token: 0x06000924 RID: 2340 RVA: 0x00025AF8 File Offset: 0x00023CF8
		public int? MinimumItems { get; set; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000925 RID: 2341 RVA: 0x00025B01 File Offset: 0x00023D01
		// (set) Token: 0x06000926 RID: 2342 RVA: 0x00025B09 File Offset: 0x00023D09
		public int? MaximumItems { get; set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x00025B12 File Offset: 0x00023D12
		// (set) Token: 0x06000928 RID: 2344 RVA: 0x00025B1A File Offset: 0x00023D1A
		public IList<string> Patterns { get; set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x00025B23 File Offset: 0x00023D23
		// (set) Token: 0x0600092A RID: 2346 RVA: 0x00025B2B File Offset: 0x00023D2B
		public IList<JsonSchemaModel> Items { get; set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x00025B34 File Offset: 0x00023D34
		// (set) Token: 0x0600092C RID: 2348 RVA: 0x00025B3C File Offset: 0x00023D3C
		public IDictionary<string, JsonSchemaModel> Properties { get; set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x00025B45 File Offset: 0x00023D45
		// (set) Token: 0x0600092E RID: 2350 RVA: 0x00025B4D File Offset: 0x00023D4D
		public IDictionary<string, JsonSchemaModel> PatternProperties { get; set; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x00025B56 File Offset: 0x00023D56
		// (set) Token: 0x06000930 RID: 2352 RVA: 0x00025B5E File Offset: 0x00023D5E
		public JsonSchemaModel AdditionalProperties { get; set; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x00025B67 File Offset: 0x00023D67
		// (set) Token: 0x06000932 RID: 2354 RVA: 0x00025B6F File Offset: 0x00023D6F
		public JsonSchemaModel AdditionalItems { get; set; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x00025B78 File Offset: 0x00023D78
		// (set) Token: 0x06000934 RID: 2356 RVA: 0x00025B80 File Offset: 0x00023D80
		public bool PositionalItemsValidation { get; set; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x00025B89 File Offset: 0x00023D89
		// (set) Token: 0x06000936 RID: 2358 RVA: 0x00025B91 File Offset: 0x00023D91
		public bool AllowAdditionalProperties { get; set; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x00025B9A File Offset: 0x00023D9A
		// (set) Token: 0x06000938 RID: 2360 RVA: 0x00025BA2 File Offset: 0x00023DA2
		public bool AllowAdditionalItems { get; set; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x00025BAB File Offset: 0x00023DAB
		// (set) Token: 0x0600093A RID: 2362 RVA: 0x00025BB3 File Offset: 0x00023DB3
		public bool UniqueItems { get; set; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x00025BBC File Offset: 0x00023DBC
		// (set) Token: 0x0600093C RID: 2364 RVA: 0x00025BC4 File Offset: 0x00023DC4
		public IList<JToken> Enum { get; set; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x00025BCD File Offset: 0x00023DCD
		// (set) Token: 0x0600093E RID: 2366 RVA: 0x00025BD5 File Offset: 0x00023DD5
		public JsonSchemaType Disallow { get; set; }

		// Token: 0x0600093F RID: 2367 RVA: 0x00025BDE File Offset: 0x00023DDE
		public JsonSchemaModel()
		{
			this.Type = JsonSchemaType.Any;
			this.AllowAdditionalProperties = true;
			this.AllowAdditionalItems = true;
			this.Required = false;
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x00025C04 File Offset: 0x00023E04
		public static JsonSchemaModel Create(IList<JsonSchema> schemata)
		{
			JsonSchemaModel jsonSchemaModel = new JsonSchemaModel();
			foreach (JsonSchema schema in schemata)
			{
				JsonSchemaModel.Combine(jsonSchemaModel, schema);
			}
			return jsonSchemaModel;
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x00025C54 File Offset: 0x00023E54
		private static void Combine(JsonSchemaModel model, JsonSchema schema)
		{
			model.Required = (model.Required || schema.Required.GetValueOrDefault());
			model.Type &= schema.Type.GetValueOrDefault(JsonSchemaType.Any);
			model.MinimumLength = MathUtils.Max(model.MinimumLength, schema.MinimumLength);
			model.MaximumLength = MathUtils.Min(model.MaximumLength, schema.MaximumLength);
			model.DivisibleBy = MathUtils.Max(model.DivisibleBy, schema.DivisibleBy);
			model.Minimum = MathUtils.Max(model.Minimum, schema.Minimum);
			model.Maximum = MathUtils.Max(model.Maximum, schema.Maximum);
			model.ExclusiveMinimum = (model.ExclusiveMinimum || schema.ExclusiveMinimum.GetValueOrDefault());
			model.ExclusiveMaximum = (model.ExclusiveMaximum || schema.ExclusiveMaximum.GetValueOrDefault());
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
			model.Disallow |= schema.Disallow.GetValueOrDefault();
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
