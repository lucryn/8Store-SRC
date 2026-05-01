using System;

namespace System.ComponentModel
{
	// Token: 0x0200000B RID: 11
	[AttributeUsage(32767)]
	public sealed class DesignOnlyAttribute : Attribute
	{
		// Token: 0x0600004B RID: 75 RVA: 0x0000275E File Offset: 0x0000095E
		public DesignOnlyAttribute(bool isDesignOnly)
		{
			this.IsDesignOnly = isDesignOnly;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600004C RID: 76 RVA: 0x0000276D File Offset: 0x0000096D
		public bool IsDesignOnly { get; }

		// Token: 0x0600004D RID: 77 RVA: 0x00002778 File Offset: 0x00000978
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DesignOnlyAttribute designOnlyAttribute = obj as DesignOnlyAttribute;
			return designOnlyAttribute != null && designOnlyAttribute.IsDesignOnly == this.IsDesignOnly;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000027A8 File Offset: 0x000009A8
		public override int GetHashCode()
		{
			return this.IsDesignOnly.GetHashCode();
		}

		// Token: 0x04000028 RID: 40
		public static readonly DesignOnlyAttribute Yes = new DesignOnlyAttribute(true);

		// Token: 0x04000029 RID: 41
		public static readonly DesignOnlyAttribute No = new DesignOnlyAttribute(false);

		// Token: 0x0400002A RID: 42
		public static readonly DesignOnlyAttribute Default = DesignOnlyAttribute.No;
	}
}
