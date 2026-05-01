using System;
using System.Collections.ObjectModel;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x020000B6 RID: 182
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal class JsonSchemaNodeCollection : KeyedCollection<string, JsonSchemaNode>
	{
		// Token: 0x06000958 RID: 2392 RVA: 0x0002645D File Offset: 0x0002465D
		protected override string GetKeyForItem(JsonSchemaNode item)
		{
			return item.Id;
		}
	}
}
