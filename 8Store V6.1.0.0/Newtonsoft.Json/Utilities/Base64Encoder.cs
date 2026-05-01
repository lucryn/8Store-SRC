using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200004B RID: 75
	[NullableContext(1)]
	[Nullable(0)]
	internal class Base64Encoder
	{
		// Token: 0x06000436 RID: 1078 RVA: 0x0001007C File Offset: 0x0000E27C
		public Base64Encoder(TextWriter writer)
		{
			ValidationUtils.ArgumentNotNull(writer, "writer");
			this._writer = writer;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x000100A4 File Offset: 0x0000E2A4
		private void ValidateEncode(byte[] buffer, int index, int count)
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
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x000100F0 File Offset: 0x0000E2F0
		public void Encode(byte[] buffer, int index, int count)
		{
			this.ValidateEncode(buffer, index, count);
			if (this._leftOverBytesCount > 0)
			{
				if (this.FulfillFromLeftover(buffer, index, ref count))
				{
					return;
				}
				int count2 = Convert.ToBase64CharArray(this._leftOverBytes, 0, 3, this._charsLine, 0);
				this.WriteChars(this._charsLine, 0, count2);
			}
			this.StoreLeftOverBytes(buffer, index, ref count);
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

		// Token: 0x06000439 RID: 1081 RVA: 0x00010184 File Offset: 0x0000E384
		private void StoreLeftOverBytes(byte[] buffer, int index, ref int count)
		{
			int num = count % 3;
			if (num > 0)
			{
				count -= num;
				if (this._leftOverBytes == null)
				{
					this._leftOverBytes = new byte[3];
				}
				for (int i = 0; i < num; i++)
				{
					this._leftOverBytes[i] = buffer[index + count + i];
				}
			}
			this._leftOverBytesCount = num;
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x000101D8 File Offset: 0x0000E3D8
		private bool FulfillFromLeftover(byte[] buffer, int index, ref int count)
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
				return true;
			}
			return false;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00010224 File Offset: 0x0000E424
		public void Flush()
		{
			if (this._leftOverBytesCount > 0)
			{
				int count = Convert.ToBase64CharArray(this._leftOverBytes, 0, this._leftOverBytesCount, this._charsLine, 0);
				this.WriteChars(this._charsLine, 0, count);
				this._leftOverBytesCount = 0;
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00010269 File Offset: 0x0000E469
		private void WriteChars(char[] chars, int index, int count)
		{
			this._writer.Write(chars, index, count);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0001027C File Offset: 0x0000E47C
		public Task EncodeAsync(byte[] buffer, int index, int count, CancellationToken cancellationToken)
		{
			Base64Encoder.<EncodeAsync>d__13 <EncodeAsync>d__;
			<EncodeAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<EncodeAsync>d__.<>4__this = this;
			<EncodeAsync>d__.buffer = buffer;
			<EncodeAsync>d__.index = index;
			<EncodeAsync>d__.count = count;
			<EncodeAsync>d__.cancellationToken = cancellationToken;
			<EncodeAsync>d__.<>1__state = -1;
			<EncodeAsync>d__.<>t__builder.Start<Base64Encoder.<EncodeAsync>d__13>(ref <EncodeAsync>d__);
			return <EncodeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x000102E0 File Offset: 0x0000E4E0
		private Task WriteCharsAsync(char[] chars, int index, int count, CancellationToken cancellationToken)
		{
			return this._writer.WriteAsync(chars, index, count, cancellationToken);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x000102F4 File Offset: 0x0000E4F4
		public Task FlushAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			if (this._leftOverBytesCount > 0)
			{
				int count = Convert.ToBase64CharArray(this._leftOverBytes, 0, this._leftOverBytesCount, this._charsLine, 0);
				this._leftOverBytesCount = 0;
				return this.WriteCharsAsync(this._charsLine, 0, count, cancellationToken);
			}
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x04000174 RID: 372
		private const int Base64LineSize = 76;

		// Token: 0x04000175 RID: 373
		private const int LineSizeInBytes = 57;

		// Token: 0x04000176 RID: 374
		private readonly char[] _charsLine = new char[76];

		// Token: 0x04000177 RID: 375
		private readonly TextWriter _writer;

		// Token: 0x04000178 RID: 376
		[Nullable(2)]
		private byte[] _leftOverBytes;

		// Token: 0x04000179 RID: 377
		private int _leftOverBytesCount;
	}
}
