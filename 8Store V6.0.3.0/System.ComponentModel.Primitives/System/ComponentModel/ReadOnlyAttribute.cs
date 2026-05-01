using System;

namespace System.ComponentModel
{
	// Token: 0x02000017 RID: 23
	[AttributeUsage(32767)]
	public sealed class ReadOnlyAttribute : Attribute
	{
		// Token: 0x06000089 RID: 137 RVA: 0x00002BA1 File Offset: 0x00000DA1
		public ReadOnlyAttribute(bool isReadOnly)
		{
			this.IsReadOnly = isReadOnly;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00002BB0 File Offset: 0x00000DB0
		public bool IsReadOnly { get; }

		// Token: 0x0600008B RID: 139 RVA: 0x00002BB8 File Offset: 0x00000DB8
		public override bool Equals(object value)
		{
			if (this == value)
			{
				return true;
			}
			ReadOnlyAttribute readOnlyAttribute = value as ReadOnlyAttribute;
			return readOnlyAttribute != null && readOnlyAttribute.IsReadOnly == this.IsReadOnly;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00002729 File Offset: 0x00000929
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04000041 RID: 65
		public static readonly ReadOnlyAttribute Yes = new ReadOnlyAttribute(true);

		// Token: 0x04000042 RID: 66
		public static readonly ReadOnlyAttribute No = new ReadOnlyAttribute(false);

		// Token: 0x04000043 RID: 67
		public static readonly ReadOnlyAttribute Default = ReadOnlyAttribute.No;
	}
}
