using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x0200002B RID: 43
	[AttributeUsage(1036, AllowMultiple = false)]
	public sealed class JsonObjectAttribute : JsonContainerAttribute
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00003024 File Offset: 0x00001224
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x0000302C File Offset: 0x0000122C
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

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00003035 File Offset: 0x00001235
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00003042 File Offset: 0x00001242
		public MissingMemberHandling MissingMemberHandling
		{
			get
			{
				return this._missingMemberHandling.GetValueOrDefault();
			}
			set
			{
				this._missingMemberHandling = new MissingMemberHandling?(value);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00003050 File Offset: 0x00001250
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x0000305D File Offset: 0x0000125D
		public NullValueHandling ItemNullValueHandling
		{
			get
			{
				return this._itemNullValueHandling.GetValueOrDefault();
			}
			set
			{
				this._itemNullValueHandling = new NullValueHandling?(value);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x0000306B File Offset: 0x0000126B
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00003078 File Offset: 0x00001278
		public Required ItemRequired
		{
			get
			{
				return this._itemRequired.GetValueOrDefault();
			}
			set
			{
				this._itemRequired = new Required?(value);
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003086 File Offset: 0x00001286
		public JsonObjectAttribute()
		{
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000308E File Offset: 0x0000128E
		public JsonObjectAttribute(MemberSerialization memberSerialization)
		{
			this.MemberSerialization = memberSerialization;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000309D File Offset: 0x0000129D
		[NullableContext(1)]
		public JsonObjectAttribute(string id) : base(id)
		{
		}

		// Token: 0x04000061 RID: 97
		private MemberSerialization _memberSerialization;

		// Token: 0x04000062 RID: 98
		internal MissingMemberHandling? _missingMemberHandling;

		// Token: 0x04000063 RID: 99
		internal Required? _itemRequired;

		// Token: 0x04000064 RID: 100
		internal NullValueHandling? _itemNullValueHandling;
	}
}
