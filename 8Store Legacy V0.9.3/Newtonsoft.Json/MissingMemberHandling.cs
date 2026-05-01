using System;

namespace Newtonsoft.Json
{
	/// <summary>
	/// Specifies missing member handling options for the <see cref="T:Newtonsoft.Json.JsonSerializer" />.
	/// </summary>
	// Token: 0x02000069 RID: 105
	public enum MissingMemberHandling
	{
		/// <summary>
		/// Ignore a missing member and do not attempt to deserialize it.
		/// </summary>
		// Token: 0x040001C9 RID: 457
		Ignore,
		/// <summary>
		/// Throw a <see cref="T:Newtonsoft.Json.JsonSerializationException" /> when a missing member is encountered during deserialization.
		/// </summary>
		// Token: 0x040001CA RID: 458
		Error
	}
}
