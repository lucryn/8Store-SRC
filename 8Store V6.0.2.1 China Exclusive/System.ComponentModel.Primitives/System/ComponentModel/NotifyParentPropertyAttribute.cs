using System;

namespace System.ComponentModel
{
	// Token: 0x02000015 RID: 21
	[AttributeUsage(128)]
	public sealed class NotifyParentPropertyAttribute : Attribute
	{
		// Token: 0x0600007E RID: 126 RVA: 0x00002ADF File Offset: 0x00000CDF
		public NotifyParentPropertyAttribute(bool notifyParent)
		{
			this.NotifyParent = notifyParent;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00002AEE File Offset: 0x00000CEE
		public bool NotifyParent { get; }

		// Token: 0x06000080 RID: 128 RVA: 0x00002AF8 File Offset: 0x00000CF8
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			NotifyParentPropertyAttribute notifyParentPropertyAttribute = obj as NotifyParentPropertyAttribute;
			return notifyParentPropertyAttribute != null && notifyParentPropertyAttribute.NotifyParent == this.NotifyParent;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002729 File Offset: 0x00000929
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0400003B RID: 59
		public static readonly NotifyParentPropertyAttribute Yes = new NotifyParentPropertyAttribute(true);

		// Token: 0x0400003C RID: 60
		public static readonly NotifyParentPropertyAttribute No = new NotifyParentPropertyAttribute(false);

		// Token: 0x0400003D RID: 61
		public static readonly NotifyParentPropertyAttribute Default = NotifyParentPropertyAttribute.No;
	}
}
