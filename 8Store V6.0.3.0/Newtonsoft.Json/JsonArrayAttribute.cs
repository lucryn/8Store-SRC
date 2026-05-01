using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x0200001E RID: 30
	[AttributeUsage(1028, AllowMultiple = false)]
	public sealed class JsonArrayAttribute : JsonContainerAttribute
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002472 File Offset: 0x00000672
		// (set) Token: 0x06000033 RID: 51 RVA: 0x0000247A File Offset: 0x0000067A
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

		// Token: 0x06000034 RID: 52 RVA: 0x00002483 File Offset: 0x00000683
		public JsonArrayAttribute()
		{
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000248B File Offset: 0x0000068B
		public JsonArrayAttribute(bool allowNullItems)
		{
			this._allowNullItems = allowNullItems;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000249A File Offset: 0x0000069A
		[NullableContext(1)]
		public JsonArrayAttribute(string id) : base(id)
		{
		}

		// Token: 0x04000048 RID: 72
		private bool _allowNullItems;
	}
}
