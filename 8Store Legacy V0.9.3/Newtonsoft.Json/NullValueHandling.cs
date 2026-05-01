using System;

namespace Newtonsoft.Json
{
	/// <summary>
	/// Specifies null value handling options for the <see cref="T:Newtonsoft.Json.JsonSerializer" />.
	/// </summary>
	/// <example>
	///   <code lang="cs" source="..\Src\Newtonsoft.Json.Tests\Documentation\SerializationTests.cs" region="ReducingSerializedJsonSizeNullValueHandlingObject" title="NullValueHandling Class" />
	///   <code lang="cs" source="..\Src\Newtonsoft.Json.Tests\Documentation\SerializationTests.cs" region="ReducingSerializedJsonSizeNullValueHandlingExample" title="NullValueHandling Ignore Example" />
	/// </example>
	// Token: 0x0200006A RID: 106
	public enum NullValueHandling
	{
		/// <summary>
		/// Include null values when serializing and deserializing objects.
		/// </summary>
		// Token: 0x040001CC RID: 460
		Include,
		/// <summary>
		/// Ignore null values when serializing and deserializing objects.
		/// </summary>
		// Token: 0x040001CD RID: 461
		Ignore
	}
}
