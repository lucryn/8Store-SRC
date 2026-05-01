using System;

namespace System.ComponentModel
{
	// Token: 0x02000013 RID: 19
	[AttributeUsage(32767)]
	public sealed class LocalizableAttribute : Attribute
	{
		// Token: 0x06000074 RID: 116 RVA: 0x00002A0E File Offset: 0x00000C0E
		public LocalizableAttribute(bool isLocalizable)
		{
			this.IsLocalizable = isLocalizable;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002A1D File Offset: 0x00000C1D
		public bool IsLocalizable { get; }

		// Token: 0x06000076 RID: 118 RVA: 0x00002A28 File Offset: 0x00000C28
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			LocalizableAttribute localizableAttribute = obj as LocalizableAttribute;
			return localizableAttribute != null && localizableAttribute.IsLocalizable == this.IsLocalizable;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002729 File Offset: 0x00000929
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04000034 RID: 52
		public static readonly LocalizableAttribute Yes = new LocalizableAttribute(true);

		// Token: 0x04000035 RID: 53
		public static readonly LocalizableAttribute No = new LocalizableAttribute(false);

		// Token: 0x04000036 RID: 54
		public static readonly LocalizableAttribute Default = LocalizableAttribute.No;
	}
}
