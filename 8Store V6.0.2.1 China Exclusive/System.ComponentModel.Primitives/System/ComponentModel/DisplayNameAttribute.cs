using System;

namespace System.ComponentModel
{
	// Token: 0x0200000C RID: 12
	[AttributeUsage(708)]
	public class DisplayNameAttribute : Attribute
	{
		// Token: 0x06000050 RID: 80 RVA: 0x000027E5 File Offset: 0x000009E5
		public DisplayNameAttribute() : this(string.Empty)
		{
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000027F2 File Offset: 0x000009F2
		public DisplayNameAttribute(string displayName)
		{
			this.DisplayNameValue = displayName;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002801 File Offset: 0x00000A01
		public virtual string DisplayName
		{
			get
			{
				return this.DisplayNameValue;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002809 File Offset: 0x00000A09
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002811 File Offset: 0x00000A11
		protected string DisplayNameValue { get; set; }

		// Token: 0x06000055 RID: 85 RVA: 0x0000281C File Offset: 0x00000A1C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DisplayNameAttribute displayNameAttribute = obj as DisplayNameAttribute;
			return displayNameAttribute != null && displayNameAttribute.DisplayName == this.DisplayName;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000284C File Offset: 0x00000A4C
		public override int GetHashCode()
		{
			return this.DisplayName.GetHashCode();
		}

		// Token: 0x0400002B RID: 43
		public static readonly DisplayNameAttribute Default = new DisplayNameAttribute();
	}
}
