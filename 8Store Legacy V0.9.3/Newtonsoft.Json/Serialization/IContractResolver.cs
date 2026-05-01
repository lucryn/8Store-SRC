using System;

namespace Newtonsoft.Json.Serialization
{
	/// <summary>
	/// Used by <see cref="T:Newtonsoft.Json.JsonSerializer" /> to resolves a <see cref="T:Newtonsoft.Json.Serialization.JsonContract" /> for a given <see cref="T:System.Type" />.
	/// </summary>
	/// <example>
	///   <code lang="cs" source="..\Src\Newtonsoft.Json.Tests\Documentation\SerializationTests.cs" region="ReducingSerializedJsonSizeContractResolverObject" title="IContractResolver Class" />
	///   <code lang="cs" source="..\Src\Newtonsoft.Json.Tests\Documentation\SerializationTests.cs" region="ReducingSerializedJsonSizeContractResolverExample" title="IContractResolver Example" />
	/// </example>
	// Token: 0x02000082 RID: 130
	public interface IContractResolver
	{
		/// <summary>
		/// Resolves the contract for a given type.
		/// </summary>
		/// <param name="type">The type to resolve a contract for.</param>
		/// <returns>The contract for a given type.</returns>
		// Token: 0x060006C9 RID: 1737
		JsonContract ResolveContract(Type type);
	}
}
