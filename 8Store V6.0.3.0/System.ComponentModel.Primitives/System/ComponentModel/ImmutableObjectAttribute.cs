using System;

namespace System.ComponentModel
{
	// Token: 0x02000010 RID: 16
	[AttributeUsage(32767)]
	public sealed class ImmutableObjectAttribute : Attribute
	{
		// Token: 0x06000068 RID: 104 RVA: 0x0000298E File Offset: 0x00000B8E
		public ImmutableObjectAttribute(bool immutable)
		{
			this.Immutable = immutable;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000069 RID: 105 RVA: 0x0000299D File Offset: 0x00000B9D
		public bool Immutable { get; }

		// Token: 0x0600006A RID: 106 RVA: 0x000029A8 File Offset: 0x00000BA8
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ImmutableObjectAttribute immutableObjectAttribute = obj as ImmutableObjectAttribute;
			return immutableObjectAttribute != null && immutableObjectAttribute.Immutable == this.Immutable;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002729 File Offset: 0x00000929
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0400002E RID: 46
		public static readonly ImmutableObjectAttribute Yes = new ImmutableObjectAttribute(true);

		// Token: 0x0400002F RID: 47
		public static readonly ImmutableObjectAttribute No = new ImmutableObjectAttribute(false);

		// Token: 0x04000030 RID: 48
		public static readonly ImmutableObjectAttribute Default = ImmutableObjectAttribute.No;
	}
}
