using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x0200002E RID: 46
	[NullableContext(2)]
	[Nullable(0)]
	[AttributeUsage(2432, AllowMultiple = false)]
	public sealed class JsonPropertyAttribute : Attribute
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x0000335A File Offset: 0x0000155A
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x00003362 File Offset: 0x00001562
		public Type ItemConverterType { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x0000336B File Offset: 0x0000156B
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x00003373 File Offset: 0x00001573
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

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x0000337C File Offset: 0x0000157C
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00003384 File Offset: 0x00001584
		public Type NamingStrategyType { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x0000338D File Offset: 0x0000158D
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00003395 File Offset: 0x00001595
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public object[] NamingStrategyParameters { [return: Nullable(new byte[]
		{
			2,
			1
		})] get; [param: Nullable(new byte[]
		{
			2,
			1
		})] set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000CB RID: 203 RVA: 0x0000339E File Offset: 0x0000159E
		// (set) Token: 0x060000CC RID: 204 RVA: 0x000033AB File Offset: 0x000015AB
		public NullValueHandling NullValueHandling
		{
			get
			{
				return this._nullValueHandling.GetValueOrDefault();
			}
			set
			{
				this._nullValueHandling = new NullValueHandling?(value);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000CD RID: 205 RVA: 0x000033B9 File Offset: 0x000015B9
		// (set) Token: 0x060000CE RID: 206 RVA: 0x000033C6 File Offset: 0x000015C6
		public DefaultValueHandling DefaultValueHandling
		{
			get
			{
				return this._defaultValueHandling.GetValueOrDefault();
			}
			set
			{
				this._defaultValueHandling = new DefaultValueHandling?(value);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000CF RID: 207 RVA: 0x000033D4 File Offset: 0x000015D4
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x000033E1 File Offset: 0x000015E1
		public ReferenceLoopHandling ReferenceLoopHandling
		{
			get
			{
				return this._referenceLoopHandling.GetValueOrDefault();
			}
			set
			{
				this._referenceLoopHandling = new ReferenceLoopHandling?(value);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x000033EF File Offset: 0x000015EF
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x000033FC File Offset: 0x000015FC
		public ObjectCreationHandling ObjectCreationHandling
		{
			get
			{
				return this._objectCreationHandling.GetValueOrDefault();
			}
			set
			{
				this._objectCreationHandling = new ObjectCreationHandling?(value);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x0000340A File Offset: 0x0000160A
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00003417 File Offset: 0x00001617
		public TypeNameHandling TypeNameHandling
		{
			get
			{
				return this._typeNameHandling.GetValueOrDefault();
			}
			set
			{
				this._typeNameHandling = new TypeNameHandling?(value);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00003425 File Offset: 0x00001625
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00003432 File Offset: 0x00001632
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

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00003440 File Offset: 0x00001640
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x0000344D File Offset: 0x0000164D
		public int Order
		{
			get
			{
				return this._order.GetValueOrDefault();
			}
			set
			{
				this._order = new int?(value);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x0000345B File Offset: 0x0000165B
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00003468 File Offset: 0x00001668
		public Required Required
		{
			get
			{
				return this._required.GetValueOrDefault();
			}
			set
			{
				this._required = new Required?(value);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00003476 File Offset: 0x00001676
		// (set) Token: 0x060000DC RID: 220 RVA: 0x0000347E File Offset: 0x0000167E
		public string PropertyName { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00003487 File Offset: 0x00001687
		// (set) Token: 0x060000DE RID: 222 RVA: 0x00003494 File Offset: 0x00001694
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

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000DF RID: 223 RVA: 0x000034A2 File Offset: 0x000016A2
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x000034AF File Offset: 0x000016AF
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

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x000034BD File Offset: 0x000016BD
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x000034CA File Offset: 0x000016CA
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

		// Token: 0x060000E3 RID: 227 RVA: 0x000034D8 File Offset: 0x000016D8
		public JsonPropertyAttribute()
		{
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000034E0 File Offset: 0x000016E0
		[NullableContext(1)]
		public JsonPropertyAttribute(string propertyName)
		{
			this.PropertyName = propertyName;
		}

		// Token: 0x0400006F RID: 111
		internal NullValueHandling? _nullValueHandling;

		// Token: 0x04000070 RID: 112
		internal DefaultValueHandling? _defaultValueHandling;

		// Token: 0x04000071 RID: 113
		internal ReferenceLoopHandling? _referenceLoopHandling;

		// Token: 0x04000072 RID: 114
		internal ObjectCreationHandling? _objectCreationHandling;

		// Token: 0x04000073 RID: 115
		internal TypeNameHandling? _typeNameHandling;

		// Token: 0x04000074 RID: 116
		internal bool? _isReference;

		// Token: 0x04000075 RID: 117
		internal int? _order;

		// Token: 0x04000076 RID: 118
		internal Required? _required;

		// Token: 0x04000077 RID: 119
		internal bool? _itemIsReference;

		// Token: 0x04000078 RID: 120
		internal ReferenceLoopHandling? _itemReferenceLoopHandling;

		// Token: 0x04000079 RID: 121
		internal TypeNameHandling? _itemTypeNameHandling;
	}
}
