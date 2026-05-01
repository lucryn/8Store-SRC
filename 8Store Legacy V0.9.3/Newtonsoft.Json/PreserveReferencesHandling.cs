using System;

namespace Newtonsoft.Json
{
	/// <summary>
	/// Specifies reference handling options for the <see cref="T:Newtonsoft.Json.JsonSerializer" />.
	/// Note that references cannot be preserved when a value is set via a non-default constructor such as types that implement ISerializable.
	/// </summary>
	/// <example>
	///   <code lang="cs" source="..\Src\Newtonsoft.Json.Tests\Documentation\SerializationTests.cs" region="PreservingObjectReferencesOn" title="Preserve Object References" />       
	/// </example>
	// Token: 0x0200006C RID: 108
	[Flags]
	public enum PreserveReferencesHandling
	{
		/// <summary>
		/// Do not preserve references when serializing types.
		/// </summary>
		// Token: 0x040001D3 RID: 467
		None = 0,
		/// <summary>
		/// Preserve references when serializing into a JSON object structure.
		/// </summary>
		// Token: 0x040001D4 RID: 468
		Objects = 1,
		/// <summary>
		/// Preserve references when serializing into a JSON array structure.
		/// </summary>
		// Token: 0x040001D5 RID: 469
		Arrays = 2,
		/// <summary>
		/// Preserve references when serializing.
		/// </summary>
		// Token: 0x040001D6 RID: 470
		All = 3
	}
}
