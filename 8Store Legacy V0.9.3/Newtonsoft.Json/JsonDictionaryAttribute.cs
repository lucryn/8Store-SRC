using System;

namespace Newtonsoft.Json
{
	/// <summary>
	/// Instructs the <see cref="T:Newtonsoft.Json.JsonSerializer" /> how to serialize the collection.
	/// </summary>
	// Token: 0x02000040 RID: 64
	[AttributeUsage(1028, AllowMultiple = false)]
	public sealed class JsonDictionaryAttribute : JsonContainerAttribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.JsonDictionaryAttribute" /> class.
		/// </summary>
		// Token: 0x06000261 RID: 609 RVA: 0x00009950 File Offset: 0x00007B50
		public JsonDictionaryAttribute()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.JsonDictionaryAttribute" /> class with the specified container Id.
		/// </summary>
		/// <param name="id">The container Id.</param>
		// Token: 0x06000262 RID: 610 RVA: 0x00009958 File Offset: 0x00007B58
		public JsonDictionaryAttribute(string id) : base(id)
		{
		}
	}
}
