using System;

namespace Newtonsoft.Json
{
	/// <summary>
	/// Indicating whether a property is required.
	/// </summary>
	// Token: 0x0200006E RID: 110
	public enum Required
	{
		/// <summary>
		/// The property is not required. The default state.
		/// </summary>
		// Token: 0x040001DC RID: 476
		Default,
		/// <summary>
		/// The property must be defined in JSON but can be a null value.
		/// </summary>
		// Token: 0x040001DD RID: 477
		AllowNull,
		/// <summary>
		/// The property must be defined in JSON and cannot be a null value.
		/// </summary>
		// Token: 0x040001DE RID: 478
		Always
	}
}
