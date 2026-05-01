using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200008E RID: 142
	[NullableContext(2)]
	[Nullable(0)]
	public class JsonContainerContract : JsonContract
	{
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x0001BD4C File Offset: 0x00019F4C
		// (set) Token: 0x060006B2 RID: 1714 RVA: 0x0001BD54 File Offset: 0x00019F54
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

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x0001BD8E File Offset: 0x00019F8E
		internal JsonContract FinalItemContract
		{
			get
			{
				return this._finalItemContract;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x0001BD96 File Offset: 0x00019F96
		// (set) Token: 0x060006B5 RID: 1717 RVA: 0x0001BD9E File Offset: 0x00019F9E
		public JsonConverter ItemConverter { get; set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x0001BDA7 File Offset: 0x00019FA7
		// (set) Token: 0x060006B7 RID: 1719 RVA: 0x0001BDAF File Offset: 0x00019FAF
		public bool? ItemIsReference { get; set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x0001BDB8 File Offset: 0x00019FB8
		// (set) Token: 0x060006B9 RID: 1721 RVA: 0x0001BDC0 File Offset: 0x00019FC0
		public ReferenceLoopHandling? ItemReferenceLoopHandling { get; set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x0001BDC9 File Offset: 0x00019FC9
		// (set) Token: 0x060006BB RID: 1723 RVA: 0x0001BDD1 File Offset: 0x00019FD1
		public TypeNameHandling? ItemTypeNameHandling { get; set; }

		// Token: 0x060006BC RID: 1724 RVA: 0x0001BDDC File Offset: 0x00019FDC
		[NullableContext(1)]
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		internal JsonContainerContract(Type underlyingType) : base(underlyingType)
		{
			JsonContainerAttribute cachedAttribute = JsonTypeReflector.GetCachedAttribute<JsonContainerAttribute>(underlyingType);
			if (cachedAttribute != null)
			{
				if (cachedAttribute.ItemConverterType != null)
				{
					this.ItemConverter = JsonTypeReflector.CreateJsonConverterInstance(cachedAttribute.ItemConverterType, cachedAttribute.ItemConverterParameters);
				}
				this.ItemIsReference = cachedAttribute._itemIsReference;
				this.ItemReferenceLoopHandling = cachedAttribute._itemReferenceLoopHandling;
				this.ItemTypeNameHandling = cachedAttribute._itemTypeNameHandling;
			}
		}

		// Token: 0x04000296 RID: 662
		private JsonContract _itemContract;

		// Token: 0x04000297 RID: 663
		private JsonContract _finalItemContract;
	}
}
