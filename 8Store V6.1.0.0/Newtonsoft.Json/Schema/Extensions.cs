using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x020000AD RID: 173
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public static class Extensions
	{
		// Token: 0x0600088D RID: 2189 RVA: 0x00023B6C File Offset: 0x00021D6C
		[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public static bool IsValid(this JToken source, JsonSchema schema)
		{
			bool valid = true;
			source.Validate(schema, delegate(object sender, ValidationEventArgs args)
			{
				valid = false;
			});
			return valid;
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00023BA0 File Offset: 0x00021DA0
		[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public static bool IsValid(this JToken source, JsonSchema schema, out IList<string> errorMessages)
		{
			IList<string> errors = new List<string>();
			source.Validate(schema, delegate(object sender, ValidationEventArgs args)
			{
				errors.Add(args.Message);
			});
			errorMessages = errors;
			return errorMessages.Count == 0;
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00023BE3 File Offset: 0x00021DE3
		[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public static void Validate(this JToken source, JsonSchema schema)
		{
			source.Validate(schema, null);
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x00023BF0 File Offset: 0x00021DF0
		[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public static void Validate(this JToken source, JsonSchema schema, ValidationEventHandler validationEventHandler)
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			ValidationUtils.ArgumentNotNull(schema, "schema");
			using (JsonValidatingReader jsonValidatingReader = new JsonValidatingReader(source.CreateReader()))
			{
				jsonValidatingReader.Schema = schema;
				if (validationEventHandler != null)
				{
					jsonValidatingReader.ValidationEventHandler += validationEventHandler;
				}
				while (jsonValidatingReader.Read())
				{
				}
			}
		}
	}
}
