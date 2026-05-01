using System;
using Newtonsoft.Json.Linq;

namespace Newtonsoft.Json.Serialization
{
	/// <summary>
	/// Sets extension data for an object during deserialization.
	/// </summary>
	/// <param name="o">The object to set extension data on.</param>
	/// <param name="key">The extension data key.</param>
	/// <param name="value">The extension data value.</param>
	// Token: 0x02000095 RID: 149
	// (Invoke) Token: 0x06000772 RID: 1906
	public delegate void ExtensionDataSetter(object o, string key, JToken value);
}
