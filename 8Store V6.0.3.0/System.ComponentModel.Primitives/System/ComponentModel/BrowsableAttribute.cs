using System;

namespace System.ComponentModel
{
	// Token: 0x02000004 RID: 4
	[AttributeUsage(32767)]
	public sealed class BrowsableAttribute : Attribute
	{
		// Token: 0x0600001E RID: 30 RVA: 0x000022AE File Offset: 0x000004AE
		public BrowsableAttribute(bool browsable)
		{
			this._browsable = browsable;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000022BD File Offset: 0x000004BD
		public bool Browsable
		{
			get
			{
				return this._browsable;
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000022C8 File Offset: 0x000004C8
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			BrowsableAttribute browsableAttribute = obj as BrowsableAttribute;
			return browsableAttribute != null && browsableAttribute.Browsable == this._browsable;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000022F5 File Offset: 0x000004F5
		public override int GetHashCode()
		{
			return this._browsable.GetHashCode();
		}

		// Token: 0x04000003 RID: 3
		public static readonly BrowsableAttribute Yes = new BrowsableAttribute(true);

		// Token: 0x04000004 RID: 4
		public static readonly BrowsableAttribute No = new BrowsableAttribute(false);

		// Token: 0x04000005 RID: 5
		public static readonly BrowsableAttribute Default = BrowsableAttribute.Yes;

		// Token: 0x04000006 RID: 6
		private bool _browsable;
	}
}
