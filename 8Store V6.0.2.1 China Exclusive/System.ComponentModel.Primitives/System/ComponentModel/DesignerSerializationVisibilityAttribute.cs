using System;

namespace System.ComponentModel
{
	// Token: 0x0200000A RID: 10
	[AttributeUsage(960)]
	public sealed class DesignerSerializationVisibilityAttribute : Attribute
	{
		// Token: 0x06000046 RID: 70 RVA: 0x000026E2 File Offset: 0x000008E2
		public DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility visibility)
		{
			this._visibility = visibility;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000026F1 File Offset: 0x000008F1
		public DesignerSerializationVisibility Visibility
		{
			get
			{
				return this._visibility;
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000026FC File Offset: 0x000008FC
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DesignerSerializationVisibilityAttribute designerSerializationVisibilityAttribute = obj as DesignerSerializationVisibilityAttribute;
			return designerSerializationVisibilityAttribute != null && designerSerializationVisibilityAttribute.Visibility == this._visibility;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002729 File Offset: 0x00000929
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04000022 RID: 34
		public static readonly DesignerSerializationVisibilityAttribute Content = new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content);

		// Token: 0x04000023 RID: 35
		public static readonly DesignerSerializationVisibilityAttribute Hidden = new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden);

		// Token: 0x04000024 RID: 36
		public static readonly DesignerSerializationVisibilityAttribute Visible = new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible);

		// Token: 0x04000025 RID: 37
		public static readonly DesignerSerializationVisibilityAttribute Default = DesignerSerializationVisibilityAttribute.Visible;

		// Token: 0x04000026 RID: 38
		private readonly DesignerSerializationVisibility _visibility;
	}
}
