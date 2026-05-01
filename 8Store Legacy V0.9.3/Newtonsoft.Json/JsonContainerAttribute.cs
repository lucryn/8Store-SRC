using System;

namespace Newtonsoft.Json
{
	/// <summary>
	/// Instructs the <see cref="T:Newtonsoft.Json.JsonSerializer" /> how to serialize the object.
	/// </summary>
	// Token: 0x0200003A RID: 58
	[AttributeUsage(1028, AllowMultiple = false)]
	public abstract class JsonContainerAttribute : Attribute
	{
		/// <summary>
		/// Gets or sets the id.
		/// </summary>
		/// <value>The id.</value>
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00008DA1 File Offset: 0x00006FA1
		// (set) Token: 0x06000201 RID: 513 RVA: 0x00008DA9 File Offset: 0x00006FA9
		public string Id { get; set; }

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00008DB2 File Offset: 0x00006FB2
		// (set) Token: 0x06000203 RID: 515 RVA: 0x00008DBA File Offset: 0x00006FBA
		public string Title { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>The description.</value>
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00008DC3 File Offset: 0x00006FC3
		// (set) Token: 0x06000205 RID: 517 RVA: 0x00008DCB File Offset: 0x00006FCB
		public string Description { get; set; }

		/// <summary>
		/// Gets the collection's items converter.
		/// </summary>
		/// <value>The collection's items converter.</value>
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00008DD4 File Offset: 0x00006FD4
		// (set) Token: 0x06000207 RID: 519 RVA: 0x00008DDC File Offset: 0x00006FDC
		public Type ItemConverterType { get; set; }

		/// <summary>
		/// Gets or sets a value that indicates whether to preserve object references.
		/// </summary>
		/// <value>
		/// 	<c>true</c> to keep object reference; otherwise, <c>false</c>. The default is <c>false</c>.
		/// </value>
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00008DE8 File Offset: 0x00006FE8
		// (set) Token: 0x06000209 RID: 521 RVA: 0x00008E0E File Offset: 0x0000700E
		public bool IsReference
		{
			get
			{
				return this._isReference ?? false;
			}
			set
			{
				this._isReference = new bool?(value);
			}
		}

		/// <summary>
		/// Gets or sets a value that indicates whether to preserve collection's items references.
		/// </summary>
		/// <value>
		/// 	<c>true</c> to keep collection's items object references; otherwise, <c>false</c>. The default is <c>false</c>.
		/// </value>
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00008E1C File Offset: 0x0000701C
		// (set) Token: 0x0600020B RID: 523 RVA: 0x00008E42 File Offset: 0x00007042
		public bool ItemIsReference
		{
			get
			{
				return this._itemIsReference ?? false;
			}
			set
			{
				this._itemIsReference = new bool?(value);
			}
		}

		/// <summary>
		/// Gets or sets the reference loop handling used when serializing the collection's items.
		/// </summary>
		/// <value>The reference loop handling.</value>
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00008E50 File Offset: 0x00007050
		// (set) Token: 0x0600020D RID: 525 RVA: 0x00008E76 File Offset: 0x00007076
		public ReferenceLoopHandling ItemReferenceLoopHandling
		{
			get
			{
				ReferenceLoopHandling? itemReferenceLoopHandling = this._itemReferenceLoopHandling;
				if (itemReferenceLoopHandling == null)
				{
					return ReferenceLoopHandling.Error;
				}
				return itemReferenceLoopHandling.GetValueOrDefault();
			}
			set
			{
				this._itemReferenceLoopHandling = new ReferenceLoopHandling?(value);
			}
		}

		/// <summary>
		/// Gets or sets the type name handling used when serializing the collection's items.
		/// </summary>
		/// <value>The type name handling.</value>
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600020E RID: 526 RVA: 0x00008E84 File Offset: 0x00007084
		// (set) Token: 0x0600020F RID: 527 RVA: 0x00008EAA File Offset: 0x000070AA
		public TypeNameHandling ItemTypeNameHandling
		{
			get
			{
				TypeNameHandling? itemTypeNameHandling = this._itemTypeNameHandling;
				if (itemTypeNameHandling == null)
				{
					return TypeNameHandling.None;
				}
				return itemTypeNameHandling.GetValueOrDefault();
			}
			set
			{
				this._itemTypeNameHandling = new TypeNameHandling?(value);
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.JsonContainerAttribute" /> class.
		/// </summary>
		// Token: 0x06000210 RID: 528 RVA: 0x00008EB8 File Offset: 0x000070B8
		protected JsonContainerAttribute()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.JsonContainerAttribute" /> class with the specified container Id.
		/// </summary>
		/// <param name="id">The container Id.</param>
		// Token: 0x06000211 RID: 529 RVA: 0x00008EC0 File Offset: 0x000070C0
		protected JsonContainerAttribute(string id)
		{
			this.Id = id;
		}

		// Token: 0x040000C0 RID: 192
		internal bool? _isReference;

		// Token: 0x040000C1 RID: 193
		internal bool? _itemIsReference;

		// Token: 0x040000C2 RID: 194
		internal ReferenceLoopHandling? _itemReferenceLoopHandling;

		// Token: 0x040000C3 RID: 195
		internal TypeNameHandling? _itemTypeNameHandling;
	}
}
