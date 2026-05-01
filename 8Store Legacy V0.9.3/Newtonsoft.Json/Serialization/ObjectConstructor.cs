using System;

namespace Newtonsoft.Json.Serialization
{
	/// <summary>
	/// Represents a method that constructs an object.
	/// </summary>
	/// <typeparam name="T">The object type to create.</typeparam>
	// Token: 0x020000A5 RID: 165
	// (Invoke) Token: 0x0600088D RID: 2189
	public delegate object ObjectConstructor<T>(params object[] args);
}
