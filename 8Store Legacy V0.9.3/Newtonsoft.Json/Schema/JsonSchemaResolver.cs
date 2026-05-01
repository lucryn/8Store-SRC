using System;
using System.Collections.Generic;
using System.Linq;

namespace Newtonsoft.Json.Schema
{
	/// <summary>
	/// Resolves <see cref="T:Newtonsoft.Json.Schema.JsonSchema" /> from an id.
	/// </summary>
	// Token: 0x0200007A RID: 122
	public class JsonSchemaResolver
	{
		/// <summary>
		/// Gets or sets the loaded schemas.
		/// </summary>
		/// <value>The loaded schemas.</value>
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x0001874A File Offset: 0x0001694A
		// (set) Token: 0x060006B1 RID: 1713 RVA: 0x00018752 File Offset: 0x00016952
		public IList<JsonSchema> LoadedSchemas { get; protected set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Schema.JsonSchemaResolver" /> class.
		/// </summary>
		// Token: 0x060006B2 RID: 1714 RVA: 0x0001875B File Offset: 0x0001695B
		public JsonSchemaResolver()
		{
			this.LoadedSchemas = new List<JsonSchema>();
		}

		/// <summary>
		/// Gets a <see cref="T:Newtonsoft.Json.Schema.JsonSchema" /> for the specified reference.
		/// </summary>
		/// <param name="reference">The id.</param>
		/// <returns>A <see cref="T:Newtonsoft.Json.Schema.JsonSchema" /> for the specified reference.</returns>
		// Token: 0x060006B3 RID: 1715 RVA: 0x000187A0 File Offset: 0x000169A0
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
