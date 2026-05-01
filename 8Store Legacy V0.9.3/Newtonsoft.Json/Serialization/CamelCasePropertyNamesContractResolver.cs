using System;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	/// <summary>
	/// Resolves member mappings for a type, camel casing property names.
	/// </summary>
	// Token: 0x02000084 RID: 132
	public class CamelCasePropertyNamesContractResolver : DefaultContractResolver
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver" /> class.
		/// </summary>
		// Token: 0x060006F9 RID: 1785 RVA: 0x0001A7F4 File Offset: 0x000189F4
		public CamelCasePropertyNamesContractResolver() : base(true)
		{
		}

		/// <summary>
		/// Resolves the name of the property.
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
		/// <returns>The property name camel cased.</returns>
		// Token: 0x060006FA RID: 1786 RVA: 0x0001A7FD File Offset: 0x000189FD
		protected internal override string ResolvePropertyName(string propertyName)
		{
			return StringUtils.ToCamelCase(propertyName);
		}
	}
}
