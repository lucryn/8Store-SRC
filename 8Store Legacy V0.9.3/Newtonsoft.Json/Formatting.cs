using System;

namespace Newtonsoft.Json
{
	/// <summary>
	/// Specifies formatting options for the <see cref="T:Newtonsoft.Json.JsonTextWriter" />.
	/// </summary>
	// Token: 0x02000038 RID: 56
	public enum Formatting
	{
		/// <summary>
		/// No special formatting is applied. This is the default.
		/// </summary>
		// Token: 0x040000BE RID: 190
		None,
		/// <summary>
		/// Causes child objects to be indented according to the <see cref="P:Newtonsoft.Json.JsonTextWriter.Indentation" /> and <see cref="P:Newtonsoft.Json.JsonTextWriter.IndentChar" /> settings.
		/// </summary>
		// Token: 0x040000BF RID: 191
		Indented
	}
}
