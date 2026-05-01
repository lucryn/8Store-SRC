using System;

namespace Newtonsoft.Json
{
	/// <summary>
	/// Specifies how constructors are used when initializing objects during deserialization by the <see cref="T:Newtonsoft.Json.JsonSerializer" />.
	/// </summary>
	// Token: 0x02000016 RID: 22
	public enum ConstructorHandling
	{
		/// <summary>
		/// First attempt to use the public default constructor, then fall back to single paramatized constructor, then the non-public default constructor.
		/// </summary>
		// Token: 0x04000085 RID: 133
		Default,
		/// <summary>
		/// Json.NET will use a non-public default constructor before falling back to a paramatized constructor.
		/// </summary>
		// Token: 0x04000086 RID: 134
		AllowNonPublicDefaultConstructor
	}
}
