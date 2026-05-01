using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000076 RID: 118
	[NullableContext(1)]
	[Nullable(0)]
	internal readonly struct StringReference
	{
		// Token: 0x170000DF RID: 223
		public char this[int i]
		{
			get
			{
				return this._chars[i];
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x000183D3 File Offset: 0x000165D3
		public char[] Chars
		{
			get
			{
				return this._chars;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x000183DB File Offset: 0x000165DB
		public int StartIndex
		{
			get
			{
				return this._startIndex;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x000183E3 File Offset: 0x000165E3
		public int Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x000183EB File Offset: 0x000165EB
		public StringReference(char[] chars, int startIndex, int length)
		{
			this._chars = chars;
			this._startIndex = startIndex;
			this._length = length;
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00018402 File Offset: 0x00016602
		public override string ToString()
		{
			return new string(this._chars, this._startIndex, this._length);
		}

		// Token: 0x0400025F RID: 607
		private readonly char[] _chars;

		// Token: 0x04000260 RID: 608
		private readonly int _startIndex;

		// Token: 0x04000261 RID: 609
		private readonly int _length;
	}
}
