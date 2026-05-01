using System;
using System.Collections.Generic;
using System.Linq;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x020000B7 RID: 183
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public class JsonSchemaResolver
	{
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x0002646D File Offset: 0x0002466D
		// (set) Token: 0x0600095B RID: 2395 RVA: 0x00026475 File Offset: 0x00024675
		public IList<JsonSchema> LoadedSchemas { get; protected set; }

		// Token: 0x0600095C RID: 2396 RVA: 0x0002647E File Offset: 0x0002467E
		public JsonSchemaResolver()
		{
			this.LoadedSchemas = new List<JsonSchema>();
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x00026494 File Offset: 0x00024694
		public virtual JsonSchema GetSchema(string reference)
		{
			JsonSchema jsonSchema = Enumerable.SingleOrDefault<JsonSchema>(this.LoadedSchemas, (JsonSchema s) => string.Equals(s.Id, reference, 4));
			if (jsonSchema == null)
			{
				jsonSchema = Enumerable.SingleOrDefault<JsonSchema>(this.LoadedSchemas, (JsonSchema s) => string.Equals(s.Location, reference, 4));
			}
			return jsonSchema;
		}
	}
}
