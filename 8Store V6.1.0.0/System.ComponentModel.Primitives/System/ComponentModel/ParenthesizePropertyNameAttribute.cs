using System;

namespace System.ComponentModel
{
	// Token: 0x02000016 RID: 22
	[AttributeUsage(32767)]
	public sealed class ParenthesizePropertyNameAttribute : Attribute
	{
		// Token: 0x06000083 RID: 131 RVA: 0x00002B47 File Offset: 0x00000D47
		public ParenthesizePropertyNameAttribute() : this(false)
		{
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002B50 File Offset: 0x00000D50
		public ParenthesizePropertyNameAttribute(bool needParenthesis)
		{
			this.NeedParenthesis = needParenthesis;
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00002B5F File Offset: 0x00000D5F
		public bool NeedParenthesis { get; }

		// Token: 0x06000086 RID: 134 RVA: 0x00002B68 File Offset: 0x00000D68
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ParenthesizePropertyNameAttribute parenthesizePropertyNameAttribute = obj as ParenthesizePropertyNameAttribute;
			return parenthesizePropertyNameAttribute != null && parenthesizePropertyNameAttribute.NeedParenthesis == this.NeedParenthesis;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00002729 File Offset: 0x00000929
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0400003F RID: 63
		public static readonly ParenthesizePropertyNameAttribute Default = new ParenthesizePropertyNameAttribute();
	}
}
