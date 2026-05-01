using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x0200001C RID: 28
	[NullableContext(1)]
	public interface IArrayPool<[Nullable(2)] T>
	{
		// Token: 0x0600002D RID: 45
		T[] Rent(int minimumLength);

		// Token: 0x0600002E RID: 46
		void Return([Nullable(new byte[]
		{
			2,
			1
		})] T[] array);
	}
}
