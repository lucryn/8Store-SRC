using System;

namespace Newtonsoft.Json
{
	/// <summary>
	/// Instructs the <see cref="T:Newtonsoft.Json.JsonSerializer" /> to always serialize the member with the specified name.
	/// </summary>
	// Token: 0x02000047 RID: 71
	[AttributeUsage(2432, AllowMultiple = false)]
	public sealed class JsonPropertyAttribute : Attribute
	{
		/// <summary>
		/// Gets or sets the converter used when serializing the property's collection items.
		/// </summary>
		/// <value>The collection's items converter.</value>
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000275 RID: 629 RVA: 0x00009BA7 File Offset: 0x00007DA7
		// (set) Token: 0x06000276 RID: 630 RVA: 0x00009BAF File Offset: 0x00007DAF
		public Type ItemConverterType { get; set; }

		/// <summary>
		/// Gets or sets the null value handling used when serializing this property.
		/// </summary>
		/// <value>The null value handling.</value>
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000277 RID: 631 RVA: 0x00009BB8 File Offset: 0x00007DB8
		// (set) Token: 0x06000278 RID: 632 RVA: 0x00009BDE File Offset: 0x00007DDE
		public NullValueHandling NullValueHandling
		{
			get
			{
				NullValueHandling? nullValueHandling = this._nullValueHandling;
				if (nullValueHandling == null)
				{
					return NullValueHandling.Include;
				}
				return nullValueHandling.GetValueOrDefault();
			}
			set
			{
				this._nullValueHandling = new NullValueHandling?(value);
			}
		}

		/// <summary>
		/// Gets or sets the default value handling used when serializing this property.
		/// </summary>
		/// <value>The default value handling.</value>
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000279 RID: 633 RVA: 0x00009BEC File Offset: 0x00007DEC
		// (set) Token: 0x0600027A RID: 634 RVA: 0x00009C12 File Offset: 0x00007E12
		public DefaultValueHandling DefaultValueHandling
		{
			get
			{
				DefaultValueHandling? defaultValueHandling = this._defaultValueHandling;
				if (defaultValueHandling == null)
				{
					return DefaultValueHandling.Include;
				}
				return defaultValueHandling.GetValueOrDefault();
			}
			set
			{
				this._defaultValueHandling = new DefaultValueHandling?(value);
			}
		}

		/// <summary>
		/// Gets or sets the reference loop handling used when serializing this property.
		/// </summary>
		/// <value>The reference loop handling.</value>
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600027B RID: 635 RVA: 0x00009C20 File Offset: 0x00007E20
		// (set) Token: 0x0600027C RID: 636 RVA: 0x00009C46 File Offset: 0x00007E46
		public ReferenceLoopHandling ReferenceLoopHandling
		{
			get
			{
				ReferenceLoopHandling? referenceLoopHandling = this._referenceLoopHandling;
				if (referenceLoopHandling == null)
				{
					return ReferenceLoopHandling.Error;
				}
				return referenceLoopHandling.GetValueOrDefault();
			}
			set
			{
				this._referenceLoopHandling = new ReferenceLoopHandling?(value);
			}
		}

		/// <summary>
		/// Gets or sets the object creation handling used when deserializing this property.
		/// </summary>
		/// <value>The object creation handling.</value>
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600027D RID: 637 RVA: 0x00009C54 File Offset: 0x00007E54
		// (set) Token: 0x0600027E RID: 638 RVA: 0x00009C7A File Offset: 0x00007E7A
		public ObjectCreationHandling ObjectCreationHandling
		{
			get
			{
				ObjectCreationHandling? objectCreationHandling = this._objectCreationHandling;
				if (objectCreationHandling == null)
				{
					return ObjectCreationHandling.Auto;
				}
				return objectCreationHandling.GetValueOrDefault();
			}
			set
			{
				this._objectCreationHandling = new ObjectCreationHandling?(value);
			}
		}

		/// <summary>
		/// Gets or sets the type name handling used when serializing this property.
		/// </summary>
		/// <value>The type name handling.</value>
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600027F RID: 639 RVA: 0x00009C88 File Offset: 0x00007E88
		// (set) Token: 0x06000280 RID: 640 RVA: 0x00009CAE File Offset: 0x00007EAE
		public TypeNameHandling TypeNameHandling
		{
			get
			{
				TypeNameHandling? typeNameHandling = this._typeNameHandling;
				if (typeNameHandling == null)
				{
					return TypeNameHandling.None;
				}
				return typeNameHandling.GetValueOrDefault();
			}
			set
			{
				this._typeNameHandling = new TypeNameHandling?(value);
			}
		}

		/// <summary>
		/// Gets or sets whether this property's value is serialized as a reference.
		/// </summary>
		/// <value>Whether this property's value is serialized as a reference.</value>
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000281 RID: 641 RVA: 0x00009CBC File Offset: 0x00007EBC
		// (set) Token: 0x06000282 RID: 642 RVA: 0x00009CE2 File Offset: 0x00007EE2
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
		/// Gets or sets the order of serialization and deserialization of a member.
		/// </summary>
		/// <value>The numeric order of serialization or deserialization.</value>
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000283 RID: 643 RVA: 0x00009CF0 File Offset: 0x00007EF0
		// (set) Token: 0x06000284 RID: 644 RVA: 0x00009D16 File Offset: 0x00007F16
		public int Order
		{
			get
			{
				int? order = this._order;
				if (order == null)
				{
					return 0;
				}
				return order.GetValueOrDefault();
			}
			set
			{
				this._order = new int?(value);
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this property is required.
		/// </summary>
		/// <value>
		/// 	A value indicating whether this property is required.
		/// </value>
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000285 RID: 645 RVA: 0x00009D24 File Offset: 0x00007F24
		// (set) Token: 0x06000286 RID: 646 RVA: 0x00009D4A File Offset: 0x00007F4A
		public Required Required
		{
			get
			{
				Required? required = this._required;
				if (required == null)
				{
					return Required.Default;
				}
				return required.GetValueOrDefault();
			}
			set
			{
				this._required = new Required?(value);
			}
		}

		/// <summary>
		/// Gets or sets the name of the property.
		/// </summary>
		/// <value>The name of the property.</value>
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000287 RID: 647 RVA: 0x00009D58 File Offset: 0x00007F58
		// (set) Token: 0x06000288 RID: 648 RVA: 0x00009D60 File Offset: 0x00007F60
		public string PropertyName { get; set; }

		/// <summary>
		/// Gets or sets the the reference loop handling used when serializing the property's collection items.
		/// </summary>
		/// <value>The collection's items reference loop handling.</value>
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000289 RID: 649 RVA: 0x00009D6C File Offset: 0x00007F6C
		// (set) Token: 0x0600028A RID: 650 RVA: 0x00009D92 File Offset: 0x00007F92
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
		/// Gets or sets the the type name handling used when serializing the property's collection items.
		/// </summary>
		/// <value>The collection's items type name handling.</value>
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00009DA0 File Offset: 0x00007FA0
		// (set) Token: 0x0600028C RID: 652 RVA: 0x00009DC6 File Offset: 0x00007FC6
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
		/// Gets or sets whether this property's collection items are serialized as a reference.
		/// </summary>
		/// <value>Whether this property's collection items are serialized as a reference.</value>
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600028D RID: 653 RVA: 0x00009DD4 File Offset: 0x00007FD4
		// (set) Token: 0x0600028E RID: 654 RVA: 0x00009DFA File Offset: 0x00007FFA
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
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.JsonPropertyAttribute" /> class.
		/// </summary>
		// Token: 0x0600028F RID: 655 RVA: 0x00009E08 File Offset: 0x00008008
		public JsonPropertyAttribute()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.JsonPropertyAttribute" /> class with the specified name.
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
		// Token: 0x06000290 RID: 656 RVA: 0x00009E10 File Offset: 0x00008010
		public JsonPropertyAttribute(string propertyName)
		{
			this.PropertyName = propertyName;
		}

		// Token: 0x040000DD RID: 221
		internal NullValueHandling? _nullValueHandling;

		// Token: 0x040000DE RID: 222
		internal DefaultValueHandling? _defaultValueHandling;

		// Token: 0x040000DF RID: 223
		internal ReferenceLoopHandling? _referenceLoopHandling;

		// Token: 0x040000E0 RID: 224
		internal ObjectCreationHandling? _objectCreationHandling;

		// Token: 0x040000E1 RID: 225
		internal TypeNameHandling? _typeNameHandling;

		// Token: 0x040000E2 RID: 226
		internal bool? _isReference;

		// Token: 0x040000E3 RID: 227
		internal int? _order;

		// Token: 0x040000E4 RID: 228
		internal Required? _required;

		// Token: 0x040000E5 RID: 229
		internal bool? _itemIsReference;

		// Token: 0x040000E6 RID: 230
		internal ReferenceLoopHandling? _itemReferenceLoopHandling;

		// Token: 0x040000E7 RID: 231
		internal TypeNameHandling? _itemTypeNameHandling;
	}
}
