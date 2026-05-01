using System;

namespace Newtonsoft.Json
{
	/// <summary>
	/// Instructs the <see cref="T:Newtonsoft.Json.JsonSerializer" /> how to serialize the object.
	/// </summary>
	// Token: 0x02000044 RID: 68
	[AttributeUsage(1036, AllowMultiple = false)]
	public sealed class JsonObjectAttribute : JsonContainerAttribute
	{
		/// <summary>
		/// Gets or sets the member serialization.
		/// </summary>
		/// <value>The member serialization.</value>
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000999E File Offset: 0x00007B9E
		// (set) Token: 0x0600026A RID: 618 RVA: 0x000099A6 File Offset: 0x00007BA6
		public MemberSerialization MemberSerialization
		{
			get
			{
				return this._memberSerialization;
			}
			set
			{
				this._memberSerialization = value;
			}
		}

		/// <summary>
		/// Gets or sets a value that indicates whether the object's properties are required.
		/// </summary>
		/// <value>
		/// 	A value indicating whether the object's properties are required.
		/// </value>
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600026B RID: 619 RVA: 0x000099B0 File Offset: 0x00007BB0
		// (set) Token: 0x0600026C RID: 620 RVA: 0x000099D6 File Offset: 0x00007BD6
		public Required ItemRequired
		{
			get
			{
				Required? itemRequired = this._itemRequired;
				if (itemRequired == null)
				{
					return Required.Default;
				}
				return itemRequired.GetValueOrDefault();
			}
			set
			{
				this._itemRequired = new Required?(value);
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.JsonObjectAttribute" /> class.
		/// </summary>
		// Token: 0x0600026D RID: 621 RVA: 0x000099E4 File Offset: 0x00007BE4
		public JsonObjectAttribute()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.JsonObjectAttribute" /> class with the specified member serialization.
		/// </summary>
		/// <param name="memberSerialization">The member serialization.</param>
		// Token: 0x0600026E RID: 622 RVA: 0x000099EC File Offset: 0x00007BEC
		public JsonObjectAttribute(MemberSerialization memberSerialization)
		{
			this.MemberSerialization = memberSerialization;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.JsonObjectAttribute" /> class with the specified container Id.
		/// </summary>
		/// <param name="id">The container Id.</param>
		// Token: 0x0600026F RID: 623 RVA: 0x000099FB File Offset: 0x00007BFB
		public JsonObjectAttribute(string id) : base(id)
		{
		}

		// Token: 0x040000D2 RID: 210
		private MemberSerialization _memberSerialization;

		// Token: 0x040000D3 RID: 211
		internal Required? _itemRequired;
	}
}
