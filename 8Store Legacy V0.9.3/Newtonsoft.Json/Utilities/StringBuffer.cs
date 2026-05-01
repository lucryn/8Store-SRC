using System;

namespace Newtonsoft.Json.Utilities
{
	/// <summary>
	/// Builds a string. Unlike StringBuilder this class lets you reuse it's internal buffer.
	/// </summary>
	// Token: 0x020000D4 RID: 212
	internal class StringBuffer
	{
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x00027F9A File Offset: 0x0002619A
		// (set) Token: 0x06000A0D RID: 2573 RVA: 0x00027FA2 File Offset: 0x000261A2
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

		// Token: 0x06000A0E RID: 2574 RVA: 0x00027FAB File Offset: 0x000261AB
		public StringBuffer()
		{
			this._buffer = StringBuffer.EmptyBuffer;
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x00027FBE File Offset: 0x000261BE
		public StringBuffer(int initalSize)
		{
			this._buffer = new char[initalSize];
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x00027FD4 File Offset: 0x000261D4
		public void Append(char value)
		{
			if (this._position == this._buffer.Length)
			{
				this.EnsureSize(1);
			}
			this._buffer[this._position++] = value;
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x00028011 File Offset: 0x00026211
		public void Append(char[] buffer, int startIndex, int count)
		{
			if (this._position + count >= this._buffer.Length)
			{
				this.EnsureSize(count);
			}
			Array.Copy(buffer, startIndex, this._buffer, this._position, count);
			this._position += count;
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0002804E File Offset: 0x0002624E
		public void Clear()
		{
			this._buffer = StringBuffer.EmptyBuffer;
			this._position = 0;
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00028064 File Offset: 0x00026264
		private void EnsureSize(int appendLength)
		{
			char[] array = new char[(this._position + appendLength) * 2];
			Array.Copy(this._buffer, array, this._position);
			this._buffer = array;
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0002809A File Offset: 0x0002629A
		public override string ToString()
		{
			return this.ToString(0, this._position);
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x000280A9 File Offset: 0x000262A9
		public string ToString(int start, int length)
		{
			return new string(this._buffer, start, length);
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x000280B8 File Offset: 0x000262B8
		public char[] GetInternalBuffer()
		{
			return this._buffer;
		}

		// Token: 0x040003D6 RID: 982
		private char[] _buffer;

		// Token: 0x040003D7 RID: 983
		private int _position;

		// Token: 0x040003D8 RID: 984
		private static readonly char[] EmptyBuffer = new char[0];
	}
}
