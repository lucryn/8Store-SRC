using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000052 RID: 82
	[NullableContext(1)]
	[Nullable(0)]
	internal class TypeInformation
	{
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x00010FDE File Offset: 0x0000F1DE
		public Type Type { get; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x00010FE6 File Offset: 0x0000F1E6
		public PrimitiveTypeCode TypeCode { get; }

		// Token: 0x0600047A RID: 1146 RVA: 0x00010FEE File Offset: 0x0000F1EE
		public TypeInformation(Type type, PrimitiveTypeCode typeCode)
		{
			this.Type = type;
			this.TypeCode = typeCode;
		}
	}
}
