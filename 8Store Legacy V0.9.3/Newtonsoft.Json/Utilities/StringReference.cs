using System;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000D5 RID: 213
	internal struct StringReference
	{
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x000280CD File Offset: 0x000262CD
		public char[] Chars
		{
			get
			{
				return this._chars;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000A19 RID: 2585 RVA: 0x000280D5 File Offset: 0x000262D5
		public int StartIndex
		{
			get
			{
				return this._startIndex;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x000280DD File Offset: 0x000262DD
		public int Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x000280E5 File Offset: 0x000262E5
		public StringReference(char[] chars, int startIndex, int length)
		{
			this._chars = chars;
			this._startIndex = startIndex;
			this._length = length;
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x000280FC File Offset: 0x000262FC
		public override string ToString()
		{
			return new string(this._chars, this._startIndex, this._length);
		}

		// Token: 0x040003D9 RID: 985
		private readonly char[] _chars;

		// Token: 0x040003DA RID: 986
		private readonly int _startIndex;

		// Token: 0x040003DB RID: 987
		private readonly int _length;
	}
}
