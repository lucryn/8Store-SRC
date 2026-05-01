using System;

namespace System.ComponentModel
{
	// Token: 0x02000014 RID: 20
	[AttributeUsage(32767)]
	public sealed class MergablePropertyAttribute : Attribute
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00002A77 File Offset: 0x00000C77
		public MergablePropertyAttribute(bool allowMerge)
		{
			this.AllowMerge = allowMerge;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002A86 File Offset: 0x00000C86
		public bool AllowMerge { get; }

		// Token: 0x0600007B RID: 123 RVA: 0x00002A90 File Offset: 0x00000C90
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			MergablePropertyAttribute mergablePropertyAttribute = obj as MergablePropertyAttribute;
			return mergablePropertyAttribute != null && mergablePropertyAttribute.AllowMerge == this.AllowMerge;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002729 File Offset: 0x00000929
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04000037 RID: 55
		public static readonly MergablePropertyAttribute Yes = new MergablePropertyAttribute(true);

		// Token: 0x04000038 RID: 56
		public static readonly MergablePropertyAttribute No = new MergablePropertyAttribute(false);

		// Token: 0x04000039 RID: 57
		public static readonly MergablePropertyAttribute Default = MergablePropertyAttribute.Yes;
	}
}
