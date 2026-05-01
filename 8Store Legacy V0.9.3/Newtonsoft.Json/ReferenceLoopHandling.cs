using System;

namespace Newtonsoft.Json
{
	/// <summary>
	/// Specifies reference loop handling options for the <see cref="T:Newtonsoft.Json.JsonSerializer" />.
	/// </summary>
	// Token: 0x0200006D RID: 109
	public enum ReferenceLoopHandling
	{
		/// <summary>
		/// Throw a <see cref="T:Newtonsoft.Json.JsonSerializationException" /> when a loop is encountered.
		/// </summary>
		// Token: 0x040001D8 RID: 472
		Error,
		/// <summary>
		/// Ignore loop references and do not serialize.
		/// </summary>
		// Token: 0x040001D9 RID: 473
		Ignore,
		/// <summary>
		/// Serialize loop references.
		/// </summary>
		// Token: 0x040001DA RID: 474
		Serialize
	}
}
