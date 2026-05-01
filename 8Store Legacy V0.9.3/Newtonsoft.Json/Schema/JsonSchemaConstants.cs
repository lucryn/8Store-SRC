using System;
using System.Collections.Generic;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x02000072 RID: 114
	internal static class JsonSchemaConstants
	{
		// Token: 0x0600063B RID: 1595 RVA: 0x00017200 File Offset: 0x00015400
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

		// Token: 0x04000209 RID: 521
		public const string TypePropertyName = "type";

		// Token: 0x0400020A RID: 522
		public const string PropertiesPropertyName = "properties";

		// Token: 0x0400020B RID: 523
		public const string ItemsPropertyName = "items";

		// Token: 0x0400020C RID: 524
		public const string AdditionalItemsPropertyName = "additionalItems";

		// Token: 0x0400020D RID: 525
		public const string RequiredPropertyName = "required";

		// Token: 0x0400020E RID: 526
		public const string PatternPropertiesPropertyName = "patternProperties";

		// Token: 0x0400020F RID: 527
		public const string AdditionalPropertiesPropertyName = "additionalProperties";

		// Token: 0x04000210 RID: 528
		public const string RequiresPropertyName = "requires";

		// Token: 0x04000211 RID: 529
		public const string MinimumPropertyName = "minimum";

		// Token: 0x04000212 RID: 530
		public const string MaximumPropertyName = "maximum";

		// Token: 0x04000213 RID: 531
		public const string ExclusiveMinimumPropertyName = "exclusiveMinimum";

		// Token: 0x04000214 RID: 532
		public const string ExclusiveMaximumPropertyName = "exclusiveMaximum";

		// Token: 0x04000215 RID: 533
		public const string MinimumItemsPropertyName = "minItems";

		// Token: 0x04000216 RID: 534
		public const string MaximumItemsPropertyName = "maxItems";

		// Token: 0x04000217 RID: 535
		public const string PatternPropertyName = "pattern";

		// Token: 0x04000218 RID: 536
		public const string MaximumLengthPropertyName = "maxLength";

		// Token: 0x04000219 RID: 537
		public const string MinimumLengthPropertyName = "minLength";

		// Token: 0x0400021A RID: 538
		public const string EnumPropertyName = "enum";

		// Token: 0x0400021B RID: 539
		public const string ReadOnlyPropertyName = "readonly";

		// Token: 0x0400021C RID: 540
		public const string TitlePropertyName = "title";

		// Token: 0x0400021D RID: 541
		public const string DescriptionPropertyName = "description";

		// Token: 0x0400021E RID: 542
		public const string FormatPropertyName = "format";

		// Token: 0x0400021F RID: 543
		public const string DefaultPropertyName = "default";

		// Token: 0x04000220 RID: 544
		public const string TransientPropertyName = "transient";

		// Token: 0x04000221 RID: 545
		public const string DivisibleByPropertyName = "divisibleBy";

		// Token: 0x04000222 RID: 546
		public const string HiddenPropertyName = "hidden";

		// Token: 0x04000223 RID: 547
		public const string DisallowPropertyName = "disallow";

		// Token: 0x04000224 RID: 548
		public const string ExtendsPropertyName = "extends";

		// Token: 0x04000225 RID: 549
		public const string IdPropertyName = "id";

		// Token: 0x04000226 RID: 550
		public const string UniqueItemsPropertyName = "uniqueItems";

		// Token: 0x04000227 RID: 551
		public const string OptionValuePropertyName = "value";

		// Token: 0x04000228 RID: 552
		public const string OptionLabelPropertyName = "label";

		// Token: 0x04000229 RID: 553
		public const string ReferencePropertyName = "$ref";

		// Token: 0x0400022A RID: 554
		public static readonly IDictionary<string, JsonSchemaType> JsonSchemaTypeMapping;
	}
}
