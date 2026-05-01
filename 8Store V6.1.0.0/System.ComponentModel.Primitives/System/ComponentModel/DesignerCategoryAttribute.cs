using System;

namespace System.ComponentModel
{
	// Token: 0x02000008 RID: 8
	[AttributeUsage(4, AllowMultiple = false, Inherited = true)]
	public sealed class DesignerCategoryAttribute : Attribute
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00002641 File Offset: 0x00000841
		public DesignerCategoryAttribute()
		{
			this.Category = string.Empty;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002654 File Offset: 0x00000854
		public DesignerCategoryAttribute(string category)
		{
			this.Category = category;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002663 File Offset: 0x00000863
		public string Category { get; }

		// Token: 0x06000043 RID: 67 RVA: 0x0000266C File Offset: 0x0000086C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DesignerCategoryAttribute designerCategoryAttribute = obj as DesignerCategoryAttribute;
			return designerCategoryAttribute != null && designerCategoryAttribute.Category == this.Category;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000269C File Offset: 0x0000089C
		public override int GetHashCode()
		{
			return this.Category.GetHashCode();
		}

		// Token: 0x04000019 RID: 25
		public static readonly DesignerCategoryAttribute Component = new DesignerCategoryAttribute("Component");

		// Token: 0x0400001A RID: 26
		public static readonly DesignerCategoryAttribute Default = new DesignerCategoryAttribute();

		// Token: 0x0400001B RID: 27
		public static readonly DesignerCategoryAttribute Form = new DesignerCategoryAttribute("Form");

		// Token: 0x0400001C RID: 28
		public static readonly DesignerCategoryAttribute Generic = new DesignerCategoryAttribute("Designer");
	}
}
