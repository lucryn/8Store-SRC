using System;

namespace Newtonsoft.Json
{
	/// <summary>
	/// Specifies the state of the <see cref="T:Newtonsoft.Json.JsonWriter" />.
	/// </summary>
	// Token: 0x020000DA RID: 218
	public enum WriteState
	{
		/// <summary>
		/// An exception has been thrown, which has left the <see cref="T:Newtonsoft.Json.JsonWriter" /> in an invalid state.
		/// You may call the <see cref="M:Newtonsoft.Json.JsonWriter.Close" /> method to put the <see cref="T:Newtonsoft.Json.JsonWriter" /> in the <c>Closed</c> state.
		/// Any other <see cref="T:Newtonsoft.Json.JsonWriter" /> method calls results in an <see cref="T:System.InvalidOperationException" /> being thrown. 
		/// </summary>
		// Token: 0x040003E9 RID: 1001
		Error,
		/// <summary>
		/// The <see cref="M:Newtonsoft.Json.JsonWriter.Close" /> method has been called. 
		/// </summary>
		// Token: 0x040003EA RID: 1002
		Closed,
		/// <summary>
		/// An object is being written. 
		/// </summary>
		// Token: 0x040003EB RID: 1003
		Object,
		/// <summary>
		/// A array is being written.
		/// </summary>
		// Token: 0x040003EC RID: 1004
		Array,
		/// <summary>
		/// A constructor is being written.
		/// </summary>
		// Token: 0x040003ED RID: 1005
		Constructor,
		/// <summary>
		/// A property is being written.
		/// </summary>
		// Token: 0x040003EE RID: 1006
		Property,
		/// <summary>
		/// A write method has not been called.
		/// </summary>
		// Token: 0x040003EF RID: 1007
		Start
	}
}
