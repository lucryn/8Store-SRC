using System;
using System.Collections.Generic;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x020000B0 RID: 176
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal static class JsonSchemaConstants
	{
		// Token: 0x060008F4 RID: 2292 RVA: 0x00025094 File Offset: 0x00023294
		// Note: this type is marked as 'beforefieldinit'.
		static JsonSchemaConstants()
		{
			Dictionary<string, JsonSchemaType> dictionary = new Dictionary<string, JsonSchemaType>();
			dictionary.Add("string", JsonSchemaType.String);
			dictionary.Add("object", JsonSchemaType.Object);
			dictionary.Add("integer", JsonSchemaType.Integer);
			dictionary.Add("number", JsonSchemaType.Float);
			dictionary.Add("null", JsonSchemaType.Null);
			dictionary.Add("boolean", JsonSchemaType.Boolean);
			dictionary.Add("array", JsonSchemaType.Array);
			dictionary.Add("any", JsonSchemaType.Any);
			JsonSchemaConstants.JsonSchemaTypeMapping = dictionary;
		}

		// Token: 0x0400034D RID: 845
		public const string TypePropertyName = "type";

		// Token: 0x0400034E RID: 846
		public const string PropertiesPropertyName = "properties";

		// Token: 0x0400034F RID: 847
		public const string ItemsPropertyName = "items";

		// Token: 0x04000350 RID: 848
		public const string AdditionalItemsPropertyName = "additionalItems";

		// Token: 0x04000351 RID: 849
		public const string RequiredPropertyName = "required";

		// Token: 0x04000352 RID: 850
		public const string PatternPropertiesPropertyName = "patternProperties";

		// Token: 0x04000353 RID: 851
		public const string AdditionalPropertiesPropertyName = "additionalProperties";

		// Token: 0x04000354 RID: 852
		public const string RequiresPropertyName = "requires";

		// Token: 0x04000355 RID: 853
		public const string MinimumPropertyName = "minimum";

		// Token: 0x04000356 RID: 854
		public const string MaximumPropertyName = "maximum";

		// Token: 0x04000357 RID: 855
		public const string ExclusiveMinimumPropertyName = "exclusiveMinimum";

		// Token: 0x04000358 RID: 856
		public const string ExclusiveMaximumPropertyName = "exclusiveMaximum";

		// Token: 0x04000359 RID: 857
		public const string MinimumItemsPropertyName = "minItems";

		// Token: 0x0400035A RID: 858
		public const string MaximumItemsPropertyName = "maxItems";

		// Token: 0x0400035B RID: 859
		public const string PatternPropertyName = "pattern";

		// Token: 0x0400035C RID: 860
		public const string MaximumLengthPropertyName = "maxLength";

		// Token: 0x0400035D RID: 861
		public const string MinimumLengthPropertyName = "minLength";

		// Token: 0x0400035E RID: 862
		public const string EnumPropertyName = "enum";

		// Token: 0x0400035F RID: 863
		public const string ReadOnlyPropertyName = "readonly";

		// Token: 0x04000360 RID: 864
		public const string TitlePropertyName = "title";

		// Token: 0x04000361 RID: 865
		public const string DescriptionPropertyName = "description";

		// Token: 0x04000362 RID: 866
		public const string FormatPropertyName = "format";

		// Token: 0x04000363 RID: 867
		public const string DefaultPropertyName = "default";

		// Token: 0x04000364 RID: 868
		public const string TransientPropertyName = "transient";

		// Token: 0x04000365 RID: 869
		public const string DivisibleByPropertyName = "divisibleBy";

		// Token: 0x04000366 RID: 870
		public const string HiddenPropertyName = "hidden";

		// Token: 0x04000367 RID: 871
		public const string DisallowPropertyName = "disallow";

		// Token: 0x04000368 RID: 872
		public const string ExtendsPropertyName = "extends";

		// Token: 0x04000369 RID: 873
		public const string IdPropertyName = "id";

		// Token: 0x0400036A RID: 874
		public const string UniqueItemsPropertyName = "uniqueItems";

		// Token: 0x0400036B RID: 875
		public const string OptionValuePropertyName = "value";

		// Token: 0x0400036C RID: 876
		public const string OptionLabelPropertyName = "label";

		// Token: 0x0400036D RID: 877
		public static readonly IDictionary<string, JsonSchemaType> JsonSchemaTypeMapping;
	}
}
