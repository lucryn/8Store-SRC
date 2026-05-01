using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200004A RID: 74
	[NullableContext(1)]
	[Nullable(0)]
	internal static class AsyncUtils
	{
		// Token: 0x0600042B RID: 1067 RVA: 0x0000FF52 File Offset: 0x0000E152
		internal static Task<bool> ToAsync(this bool value)
		{
			if (!value)
			{
				return AsyncUtils.False;
			}
			return AsyncUtils.True;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000FF62 File Offset: 0x0000E162
		[NullableContext(2)]
		public static Task CancelIfRequestedAsync(this CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return null;
			}
			return cancellationToken.FromCanceled();
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000FF75 File Offset: 0x0000E175
		[NullableContext(2)]
		[return: Nullable(new byte[]
		{
			2,
			1
		})]
		public static Task<T> CancelIfRequestedAsync<T>(this CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return null;
			}
			return cancellationToken.FromCanceled<T>();
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000FF88 File Offset: 0x0000E188
		public static Task FromCanceled(this CancellationToken cancellationToken)
		{
			return new Task(delegate()
			{
			}, cancellationToken);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000FFB0 File Offset: 0x0000E1B0
		public static Task<T> FromCanceled<[Nullable(2)] T>(this CancellationToken cancellationToken)
		{
			Func<T> func;
			if ((func = AsyncUtils.<>c__6<T>.<>9__6_0) == null)
			{
				Func<T> func2 = AsyncUtils.<>c__6<T>.<>9__6_0 = (() => default(T));
				func = func2;
			}
			return new Task<T>(func, cancellationToken);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000FFE4 File Offset: 0x0000E1E4
		public static Task WriteAsync(this TextWriter writer, char value, CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return writer.WriteAsync(value);
			}
			return cancellationToken.FromCanceled();
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000FFFD File Offset: 0x0000E1FD
		public static Task WriteAsync(this TextWriter writer, [Nullable(2)] string value, CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return writer.WriteAsync(value);
			}
			return cancellationToken.FromCanceled();
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00010016 File Offset: 0x0000E216
		public static Task WriteAsync(this TextWriter writer, char[] value, int start, int count, CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return writer.WriteAsync(value, start, count);
			}
			return cancellationToken.FromCanceled();
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00010032 File Offset: 0x0000E232
		public static Task<int> ReadAsync(this TextReader reader, char[] buffer, int index, int count, CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return reader.ReadAsync(buffer, index, count);
			}
			return cancellationToken.FromCanceled<int>();
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0001004E File Offset: 0x0000E24E
		public static bool IsCompletedSuccessfully(this Task task)
		{
			return task.Status == 5;
		}

		// Token: 0x04000171 RID: 369
		public static readonly Task<bool> False = Task.FromResult<bool>(false);

		// Token: 0x04000172 RID: 370
		public static readonly Task<bool> True = Task.FromResult<bool>(true);

		// Token: 0x04000173 RID: 371
		internal static readonly Task CompletedTask = Task.Delay(0);
	}
}
