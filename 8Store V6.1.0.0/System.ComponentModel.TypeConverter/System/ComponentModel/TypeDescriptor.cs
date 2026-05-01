using System;

namespace System.ComponentModel
{
	// Token: 0x02000006 RID: 6
	public sealed class TypeDescriptor
	{
		// Token: 0x06000030 RID: 48 RVA: 0x000023EA File Offset: 0x000005EA
		private TypeDescriptor()
		{
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002B72 File Offset: 0x00000D72
		public static TypeConverter GetConverter(Type type)
		{
			return ReflectTypeDescriptionProvider.GetConverter(type);
		}
	}
}
