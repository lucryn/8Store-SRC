using System;

namespace Newtonsoft.Json
{
	/// <summary>
	/// Specifies how object creation is handled by the <see cref="T:Newtonsoft.Json.JsonSerializer" />.
	/// </summary>
	// Token: 0x0200006B RID: 107
	public enum ObjectCreationHandling
	{
		/// <summary>
		/// Reuse existing objects, create new objects when needed.
		/// </summary>
		// Token: 0x040001CF RID: 463
		Auto,
		/// <summary>
		/// Only reuse existing objects.
		/// </summary>
		// Token: 0x040001D0 RID: 464
		Reuse,
		/// <summary>
		/// Always create new objects.
		/// </summary>
		// Token: 0x040001D1 RID: 465
		Replace
	}
}
