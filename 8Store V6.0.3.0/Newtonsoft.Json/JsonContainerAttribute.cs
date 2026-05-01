using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json
{
	// Token: 0x02000020 RID: 32
	[NullableContext(2)]
	[Nullable(0)]
	[AttributeUsage(1028, AllowMultiple = false)]
	public abstract class JsonContainerAttribute : Attribute
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000024AB File Offset: 0x000006AB
		// (set) Token: 0x06000039 RID: 57 RVA: 0x000024B3 File Offset: 0x000006B3
		public string Id { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000024BC File Offset: 0x000006BC
		// (set) Token: 0x0600003B RID: 59 RVA: 0x000024C4 File Offset: 0x000006C4
		public string Title { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000024CD File Offset: 0x000006CD
		// (set) Token: 0x0600003D RID: 61 RVA: 0x000024D5 File Offset: 0x000006D5
		public string Description { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000024DE File Offset: 0x000006DE
		// (set) Token: 0x0600003F RID: 63 RVA: 0x000024E6 File Offset: 0x000006E6
		public Type ItemConverterType { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000024EF File Offset: 0x000006EF
		// (set) Token: 0x06000041 RID: 65 RVA: 0x000024F7 File Offset: 0x000006F7
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public object[] ItemConverterParameters { [return: Nullable(new byte[]
		{
			2,
			1
		})] get; [param: Nullable(new byte[]
		{
			2,
			1
		})] set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002500 File Offset: 0x00000700
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002508 File Offset: 0x00000708
		public Type NamingStrategyType
		{
			get
			{
				return this._namingStrategyType;
			}
			set
			{
				this._namingStrategyType = value;
				this.NamingStrategyInstance = null;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002518 File Offset: 0x00000718
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00002520 File Offset: 0x00000720
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public object[] NamingStrategyParameters
		{
			[return: Nullable(new byte[]
			{
				2,
				1
			})]
			get
			{
				return this._namingStrategyParameters;
			}
			[param: Nullable(new byte[]
			{
				2,
				1
			})]
			set
			{
				this._namingStrategyParameters = value;
				this.NamingStrategyInstance = null;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002530 File Offset: 0x00000730
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002538 File Offset: 0x00000738
		internal NamingStrategy NamingStrategyInstance { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002541 File Offset: 0x00000741
		// (set) Token: 0x06000049 RID: 73 RVA: 0x0000254E File Offset: 0x0000074E
		public bool IsReference
		{
			get
			{
				return this._isReference.GetValueOrDefault();
			}
			set
			{
				this._isReference = new bool?(value);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004A RID: 74 RVA: 0x0000255C File Offset: 0x0000075C
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002569 File Offset: 0x00000769
		public bool ItemIsReference
		{
			get
			{
				return this._itemIsReference.GetValueOrDefault();
			}
			set
			{
				this._itemIsReference = new bool?(value);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002577 File Offset: 0x00000777
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002584 File Offset: 0x00000784
		public ReferenceLoopHandling ItemReferenceLoopHandling
		{
			get
			{
				return this._itemReferenceLoopHandling.GetValueOrDefault();
			}
			set
			{
				this._itemReferenceLoopHandling = new ReferenceLoopHandling?(value);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002592 File Offset: 0x00000792
		// (set) Token: 0x0600004F RID: 79 RVA: 0x0000259F File Offset: 0x0000079F
		public TypeNameHandling ItemTypeNameHandling
		{
			get
			{
				return this._itemTypeNameHandling.GetValueOrDefault();
			}
			set
			{
				this._itemTypeNameHandling = new TypeNameHandling?(value);
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000025AD File Offset: 0x000007AD
		protected JsonContainerAttribute()
		{
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000025B5 File Offset: 0x000007B5
		[NullableContext(1)]
		protected JsonContainerAttribute(string id)
		{
			this.Id = id;
		}

		// Token: 0x0400004F RID: 79
		internal bool? _isReference;

		// Token: 0x04000050 RID: 80
		internal bool? _itemIsReference;

		// Token: 0x04000051 RID: 81
		internal ReferenceLoopHandling? _itemReferenceLoopHandling;

		// Token: 0x04000052 RID: 82
		internal TypeNameHandling? _itemTypeNameHandling;

		// Token: 0x04000053 RID: 83
		private Type _namingStrategyType;

		// Token: 0x04000054 RID: 84
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private object[] _namingStrategyParameters;
	}
}
