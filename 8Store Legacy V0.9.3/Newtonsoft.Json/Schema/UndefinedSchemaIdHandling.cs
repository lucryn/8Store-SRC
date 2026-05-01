using System;

namespace Newtonsoft.Json.Schema
{
	/// <summary>
	/// Specifies undefined schema Id handling options for the <see cref="T:Newtonsoft.Json.Schema.JsonSchemaGenerator" />.
	/// </summary>
	// Token: 0x0200007D RID: 125
	public enum UndefinedSchemaIdHandling
	{
		/// <summary>
		/// Do not infer a schema Id.
		/// </summary>
		// Token: 0x04000267 RID: 615
		None,
		/// <summary>
		/// Use the .NET type name as the schema Id.
		/// </summary>
		// Token: 0x04000268 RID: 616
		UseTypeName,
		/// <summary>
		/// Use the assembly qualified .NET type name as the schema Id.
		/// </summary>
		// Token: 0x04000269 RID: 617
		UseAssemblyQualifiedName
	}
}
