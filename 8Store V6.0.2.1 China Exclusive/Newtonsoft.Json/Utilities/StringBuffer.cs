using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000075 RID: 117
	[NullableContext(2)]
	[Nullable(0)]
	internal struct StringBuffer
	{
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x00018268 File Offset: 0x00016468
		// (set) Token: 0x060005C3 RID: 1475 RVA: 0x00018270 File Offset: 0x00016470
		public int Position
		{
			get
			{
				return this._position;
			}
			set
			{
				this._position = value;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x00018279 File Offset: 0x00016479
		public bool IsEmpty
		{
			get
			{
				return this._buffer == null;
			}
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00018284 File Offset: 0x00016484
		public StringBuffer(IArrayPool<char> bufferPool, int initalSize)
		{
			this = new StringBuffer(BufferUtils.RentBuffer(bufferPool, initalSize));
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00018293 File Offset: 0x00016493
		[NullableContext(1)]
		private StringBuffer(char[] buffer)
		{
			this._buffer = buffer;
			this._position = 0;
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x000182A4 File Offset: 0x000164A4
		public void Append(IArrayPool<char> bufferPool, char value)
		{
			if (this._position == this._buffer.Length)
			{
				this.EnsureSize(bufferPool, 1);
			}
			char[] buffer = this._buffer;
			int position = this._position;
			this._position = position + 1;
			buffer[position] = value;
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x000182E4 File Offset: 0x000164E4
		[NullableContext(1)]
		public void Append([Nullable(2)] IArrayPool<char> bufferPool, char[] buffer, int startIndex, int count)
		{
			if (this._position + count >= this._buffer.Length)
			{
				this.EnsureSize(bufferPool, count);
			}
			Array.Copy(buffer, startIndex, this._buffer, this._position, count);
			this._position += count;
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00018331 File Offset: 0x00016531
		public void Clear(IArrayPool<char> bufferPool)
		{
			if (this._buffer != null)
			{
				BufferUtils.ReturnBuffer(bufferPool, this._buffer);
				this._buffer = null;
			}
			this._position = 0;
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00018358 File Offset: 0x00016558
		private void EnsureSize(IArrayPool<char> bufferPool, int appendLength)
		{
			char[] array = BufferUtils.RentBuffer(bufferPool, (this._position + appendLength) * 2);
			if (this._buffer != null)
			{
				Array.Copy(this._buffer, array, this._position);
				BufferUtils.ReturnBuffer(bufferPool, this._buffer);
			}
			this._buffer = array;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x000183A3 File Offset: 0x000165A3
		[NullableContext(1)]
		public override string ToString()
		{
			return this.ToString(0, this._position);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x000183B2 File Offset: 0x000165B2
		[NullableContext(1)]
		public string ToString(int start, int length)
		{
			return new string(this._buffer, start, length);
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x000183C1 File Offset: 0x000165C1
		public char[] InternalBuffer
		{
			get
			{
				return this._buffer;
			}
		}

		// Token: 0x0400025D RID: 605
		private char[] _buffer;

		// Token: 0x0400025E RID: 606
		private int _position;
	}
}
