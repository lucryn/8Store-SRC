using System;

namespace Newtonsoft.Json
{
	/// <summary>
	/// Specifies the member serialization options for the <see cref="T:Newtonsoft.Json.JsonSerializer" />.
	/// </summary>
	// Token: 0x02000068 RID: 104
	public enum MemberSerialization
	{
		/// <summary>
		/// All public members are serialized by default. Members can be excluded using <see cref="T:Newtonsoft.Json.JsonIgnoreAttribute" /> or <see cref="!:NonSerializedAttribute" />.
		/// This is the default member serialization mode.
		/// </summary>
		// Token: 0x040001C5 RID: 453
		OptOut,
		/// <summary>
		/// Only members must be marked with <see cref="T:Newtonsoft.Json.JsonPropertyAttribute" /> or <see cref="T:System.Runtime.Serialization.DataMemberAttribute" /> are serialized.
		/// This member serialization mode can also be set by marking the class with <see cref="T:System.Runtime.Serialization.DataContractAttribute" />.
		/// </summary>
		// Token: 0x040001C6 RID: 454
		OptIn,
		/// <summary>
		/// All public and private fields are serialized. Members can be excluded using <see cref="T:Newtonsoft.Json.JsonIgnoreAttribute" /> or <see cref="!:NonSerializedAttribute" />.
		/// This member serialization mode can also be set by marking the class with <see cref="!:SerializableAttribute" />
		/// and setting IgnoreSerializableAttribute on <see cref="T:Newtonsoft.Json.Serialization.DefaultContractResolver" /> to false.
		/// </summary>
		// Token: 0x040001C7 RID: 455
		Fields
	}
}
