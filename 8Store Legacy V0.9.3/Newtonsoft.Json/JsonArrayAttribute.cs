using System;

namespace Newtonsoft.Json
{
	/// <summary>
	/// Instructs the <see cref="T:Newtonsoft.Json.JsonSerializer" /> how to serialize the collection.
	/// </summary>
	// Token: 0x0200003B RID: 59
	[AttributeUsage(1028, AllowMultiple = false)]
	public sealed class JsonArrayAttribute : JsonContainerAttribute
	{
		/// <summary>
		/// Gets or sets a value indicating whether null items are allowed in the collection.
		/// </summary>
		/// <value><c>true</c> if null items are allowed in the collection; otherwise, <c>false</c>.</value>
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00008ECF File Offset: 0x000070CF
		// (set) Token: 0x06000213 RID: 531 RVA: 0x00008ED7 File Offset: 0x000070D7
		public bool AllowNullItems
		{
			get
			{
				return this._allowNullItems;
			}
			set
			{
				this._allowNullItems = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.JsonArrayAttribute" /> class.
		/// </summary>
		// Token: 0x06000214 RID: 532 RVA: 0x00008EE0 File Offset: 0x000070E0
		public JsonArrayAttribute()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.JsonObjectAttribute" /> class with a flag indicating whether the array can contain null items
		/// </summary>
		/// <param name="allowNullItems">A flag indicating whether the array can contain null items.</param>
		// Token: 0x06000215 RID: 533 RVA: 0x00008EE8 File Offset: 0x000070E8
		public JsonArrayAttribute(bool allowNullItems)
		{
			this._allowNullItems = allowNullItems;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.JsonArrayAttribute" /> class with the specified container Id.
		/// </summary>
		/// <param name="id">The container Id.</param>
		// Token: 0x06000216 RID: 534 RVA: 0x00008EF7 File Offset: 0x000070F7
		public JsonArrayAttribute(string id) : base(id)
		{
		}

		// Token: 0x040000C8 RID: 200
		private bool _allowNullItems;
	}
}
