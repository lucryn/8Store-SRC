using System;

namespace Newtonsoft.Json
{
	/// <summary>
	/// Instructs the <see cref="T:Newtonsoft.Json.JsonSerializer" /> to use the specified constructor when deserializing that object.
	/// </summary>
	// Token: 0x0200003C RID: 60
	[AttributeUsage(32, AllowMultiple = false)]
	public sealed class JsonConstructorAttribute : Attribute
	{
	}
}
