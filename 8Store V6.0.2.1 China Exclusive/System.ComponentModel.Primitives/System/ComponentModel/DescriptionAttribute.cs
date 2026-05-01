using System;

namespace System.ComponentModel
{
	// Token: 0x02000007 RID: 7
	[AttributeUsage(32767)]
	public class DescriptionAttribute : Attribute
	{
		// Token: 0x06000038 RID: 56 RVA: 0x000025C0 File Offset: 0x000007C0
		public DescriptionAttribute() : this(string.Empty)
		{
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000025CD File Offset: 0x000007CD
		public DescriptionAttribute(string description)
		{
			this.DescriptionValue = description;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000025DC File Offset: 0x000007DC
		public virtual string Description
		{
			get
			{
				return this.DescriptionValue;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000025E4 File Offset: 0x000007E4
		// (set) Token: 0x0600003C RID: 60 RVA: 0x000025EC File Offset: 0x000007EC
		protected string DescriptionValue { get; set; }

		// Token: 0x0600003D RID: 61 RVA: 0x000025F8 File Offset: 0x000007F8
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DescriptionAttribute descriptionAttribute = obj as DescriptionAttribute;
			return descriptionAttribute != null && descriptionAttribute.Description == this.Description;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002628 File Offset: 0x00000828
		public override int GetHashCode()
		{
			return this.Description.GetHashCode();
		}

		// Token: 0x04000017 RID: 23
		public static readonly DescriptionAttribute Default = new DescriptionAttribute();
	}
}
