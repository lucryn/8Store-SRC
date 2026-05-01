using System;

namespace Newtonsoft.Json
{
	/// <summary>
	/// Specifies type name handling options for the <see cref="T:Newtonsoft.Json.JsonSerializer" />.
	/// </summary>
	// Token: 0x020000AD RID: 173
	[Flags]
	public enum TypeNameHandling
	{
		/// <summary>
		/// Do not include the .NET type name when serializing types.
		/// </summary>
		// Token: 0x04000322 RID: 802
		None = 0,
		/// <summary>
		/// Include the .NET type name when serializing into a JSON object structure.
		/// </summary>
		// Token: 0x04000323 RID: 803
		Objects = 1,
		/// <summary>
		/// Include the .NET type name when serializing into a JSON array structure.
		/// </summary>
		// Token: 0x04000324 RID: 804
		Arrays = 2,
		/// <summary>
		/// Always include the .NET type name when serializing.
		/// </summary>
		// Token: 0x04000325 RID: 805
		All = 3,
		/// <summary>
		/// Include the .NET type name when the type of the object being serialized is not the same as its declared type.
		/// </summary>
		// Token: 0x04000326 RID: 806
		Auto = 4
	}
}
