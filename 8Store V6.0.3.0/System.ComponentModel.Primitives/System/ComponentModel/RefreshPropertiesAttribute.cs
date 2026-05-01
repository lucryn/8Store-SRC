using System;

namespace System.ComponentModel
{
	// Token: 0x02000019 RID: 25
	[AttributeUsage(32767)]
	public sealed class RefreshPropertiesAttribute : Attribute
	{
		// Token: 0x0600008E RID: 142 RVA: 0x00002C07 File Offset: 0x00000E07
		public RefreshPropertiesAttribute(RefreshProperties refresh)
		{
			this.RefreshProperties = refresh;
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00002C16 File Offset: 0x00000E16
		public RefreshProperties RefreshProperties { get; }

		// Token: 0x06000090 RID: 144 RVA: 0x00002C20 File Offset: 0x00000E20
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			RefreshPropertiesAttribute refreshPropertiesAttribute = obj as RefreshPropertiesAttribute;
			return refreshPropertiesAttribute != null && refreshPropertiesAttribute.RefreshProperties == this.RefreshProperties;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00002729 File Offset: 0x00000929
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04000049 RID: 73
		public static readonly RefreshPropertiesAttribute All = new RefreshPropertiesAttribute(RefreshProperties.All);

		// Token: 0x0400004A RID: 74
		public static readonly RefreshPropertiesAttribute Repaint = new RefreshPropertiesAttribute(RefreshProperties.Repaint);

		// Token: 0x0400004B RID: 75
		public static readonly RefreshPropertiesAttribute Default = new RefreshPropertiesAttribute(RefreshProperties.None);
	}
}
