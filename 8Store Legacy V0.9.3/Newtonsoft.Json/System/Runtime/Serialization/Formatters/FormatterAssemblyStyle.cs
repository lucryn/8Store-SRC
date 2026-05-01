using System;

namespace System.Runtime.Serialization.Formatters
{
	/// <summary>
	/// Indicates the method that will be used during deserialization for locating and loading assemblies.
	/// </summary>
	// Token: 0x02000037 RID: 55
	public enum FormatterAssemblyStyle
	{
		/// <summary>
		/// In simple mode, the assembly used during deserialization need not match exactly the assembly used during serialization. Specifically, the version numbers need not match as the LoadWithPartialName method is used to load the assembly.
		/// </summary>
		// Token: 0x040000BB RID: 187
		Simple,
		/// <summary>
		/// In full mode, the assembly used during deserialization must match exactly the assembly used during serialization. The Load method of the Assembly class is used to load the assembly.
		/// </summary>
		// Token: 0x040000BC RID: 188
		Full
	}
}
