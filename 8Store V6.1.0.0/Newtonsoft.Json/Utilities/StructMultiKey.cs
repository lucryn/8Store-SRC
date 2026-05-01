using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000079 RID: 121
	[NullableContext(1)]
	[Nullable(0)]
	internal readonly struct StructMultiKey<[Nullable(2)] T1, [Nullable(2)] T2> : IEquatable<StructMultiKey<T1, T2>>
	{
		// Token: 0x060005ED RID: 1517 RVA: 0x00018941 File Offset: 0x00016B41
		public StructMultiKey(T1 v1, T2 v2)
		{
			this.Value1 = v1;
			this.Value2 = v2;
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00018954 File Offset: 0x00016B54
		public override int GetHashCode()
		{
			T1 value = this.Value1;
			int num = (value != null) ? value.GetHashCode() : 0;
			T2 value2 = this.Value2;
			return num ^ ((value2 != null) ? value2.GetHashCode() : 0);
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x000189AC File Offset: 0x00016BAC
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (obj is StructMultiKey<T1, T2>)
			{
				StructMultiKey<T1, T2> other = (StructMultiKey<T1, T2>)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x000189D3 File Offset: 0x00016BD3
		public bool Equals([Nullable(new byte[]
		{
			0,
			1,
			1
		})] StructMultiKey<T1, T2> other)
		{
			return object.Equals(this.Value1, other.Value1) && object.Equals(this.Value2, other.Value2);
		}

		// Token: 0x04000267 RID: 615
		public readonly T1 Value1;

		// Token: 0x04000268 RID: 616
		public readonly T2 Value2;
	}
}
