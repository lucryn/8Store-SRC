using System;
using System.IO;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000AE RID: 174
	internal class Base64Encoder
	{
		// Token: 0x060008D6 RID: 2262 RVA: 0x00021831 File Offset: 0x0001FA31
		public Base64Encoder(TextWriter writer)
		{
			ValidationUtils.ArgumentNotNull(writer, "writer");
			this._writer = writer;
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x00021858 File Offset: 0x0001FA58
		public void Encode(byte[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (count > buffer.Length - index)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this._leftOverBytesCount > 0)
			{
				int leftOverBytesCount = this._leftOverBytesCount;
				while (leftOverBytesCount < 3 && count > 0)
				{
					this._leftOverBytes[leftOverBytesCount++] = buffer[index++];
					count--;
				}
				if (count == 0 && leftOverBytesCount < 3)
				{
					this._leftOverBytesCount = leftOverBytesCount;
					return;
				}
				int count2 = Convert.ToBase64CharArray(this._leftOverBytes, 0, 3, this._charsLine, 0);
				this.WriteChars(this._charsLine, 0, count2);
			}
			this._leftOverBytesCount = count % 3;
			if (this._leftOverBytesCount > 0)
			{
				count -= this._leftOverBytesCount;
				if (this._leftOverBytes == null)
				{
					this._leftOverBytes = new byte[3];
				}
				for (int i = 0; i < this._leftOverBytesCount; i++)
				{
					this._leftOverBytes[i] = buffer[index + count + i];
				}
			}
			int num = index + count;
			int num2 = 57;
			while (index < num)
			{
				if (index + num2 > num)
				{
					num2 = num - index;
				}
				int count3 = Convert.ToBase64CharArray(buffer, index, num2, this._charsLine, 0);
				this.WriteChars(this._charsLine, 0, count3);
				index += num2;
			}
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0002199C File Offset: 0x0001FB9C
		public void Flush()
		{
			if (this._leftOverBytesCount > 0)
			{
				int count = Convert.ToBase64CharArray(this._leftOverBytes, 0, this._leftOverBytesCount, this._charsLine, 0);
				this.WriteChars(this._charsLine, 0, count);
				this._leftOverBytesCount = 0;
			}
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x000219E1 File Offset: 0x0001FBE1
		private void WriteChars(char[] chars, int index, int count)
		{
			this._writer.Write(chars, index, count);
		}

		// Token: 0x04000327 RID: 807
		private const int Base64LineSize = 76;

		// Token: 0x04000328 RID: 808
		private const int LineSizeInBytes = 57;

		// Token: 0x04000329 RID: 809
		private readonly char[] _charsLine = new char[76];

		// Token: 0x0400032A RID: 810
		private readonly TextWriter _writer;

		// Token: 0x0400032B RID: 811
		private byte[] _leftOverBytes;

		// Token: 0x0400032C RID: 812
		private int _leftOverBytesCount;
	}
}
