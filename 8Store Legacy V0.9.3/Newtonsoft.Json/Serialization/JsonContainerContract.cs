using System;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	/// <summary>
	/// Contract details for a <see cref="T:System.Type" /> used by the <see cref="T:Newtonsoft.Json.JsonSerializer" />.
	/// </summary>
	// Token: 0x02000090 RID: 144
	public class JsonContainerContract : JsonContract
	{
		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x0001B266 File Offset: 0x00019466
		// (set) Token: 0x06000751 RID: 1873 RVA: 0x0001B26E File Offset: 0x0001946E
		internal JsonContract ItemContract
		{
			get
			{
				return this._itemContract;
			}
			set
			{
				this._itemContract = value;
				if (this._itemContract != null)
				{
					this._finalItemContract = (this._itemContract.UnderlyingType.IsSealed() ? this._itemContract : null);
					return;
				}
				this._finalItemContract = null;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x0001B2A8 File Offset: 0x000194A8
		internal JsonContract FinalItemContract
		{
			get
			{
				return this._finalItemContract;
			}
		}

		/// <summary>
		/// Gets or sets the default collection items <see cref="T:Newtonsoft.Json.JsonConverter" />.
		/// </summary>
		/// <value>The converter.</value>
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x0001B2B0 File Offset: 0x000194B0
		// (set) Token: 0x06000754 RID: 1876 RVA: 0x0001B2B8 File Offset: 0x000194B8
		public JsonConverter ItemConverter { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the collection items preserve object references.
		/// </summary>
		/// <value><c>true</c> if collection items preserve object references; otherwise, <c>false</c>.</value>
		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x0001B2C1 File Offset: 0x000194C1
		// (set) Token: 0x06000756 RID: 1878 RVA: 0x0001B2C9 File Offset: 0x000194C9
		public bool? ItemIsReference { get; set; }

		/// <summary>
		/// Gets or sets the collection item reference loop handling.
		/// </summary>
		/// <value>The reference loop handling.</value>
		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x0001B2D2 File Offset: 0x000194D2
		// (set) Token: 0x06000758 RID: 1880 RVA: 0x0001B2DA File Offset: 0x000194DA
		public ReferenceLoopHandling? ItemReferenceLoopHandling { get; set; }

		/// <summary>
		/// Gets or sets the collection item type name handling.
		/// </summary>
		/// <value>The type name handling.</value>
		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x0001B2E3 File Offset: 0x000194E3
		// (set) Token: 0x0600075A RID: 1882 RVA: 0x0001B2EB File Offset: 0x000194EB
		public TypeNameHandling? ItemTypeNameHandling { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Serialization.JsonContainerContract" /> class.
		/// </summary>
		/// <param name="underlyingType">The underlying type for the contract.</param>
		// Token: 0x0600075B RID: 1883 RVA: 0x0001B2F4 File Offset: 0x000194F4
		internal JsonContainerContract(Type underlyingType) : base(underlyingType)
		{
			JsonContainerAttribute jsonContainerAttribute = JsonTypeReflector.GetJsonContainerAttribute(underlyingType);
			if (jsonContainerAttribute != null)
			{
				if (jsonContainerAttribute.ItemConverterType != null)
				{
					this.ItemConverter = JsonConverterAttribute.CreateJsonConverterInstance(jsonContainerAttribute.ItemConverterType);
				}
				this.ItemIsReference = jsonContainerAttribute._itemIsReference;
				this.ItemReferenceLoopHandling = jsonContainerAttribute._itemReferenceLoopHandling;
				this.ItemTypeNameHandling = jsonContainerAttribute._itemTypeNameHandling;
			}
		}

		// Token: 0x0400029F RID: 671
		private JsonContract _itemContract;

		// Token: 0x040002A0 RID: 672
		private JsonContract _finalItemContract;
	}
}
