using System;

namespace Win81StoreRevival.Common
{
	// Token: 0x02000064 RID: 100
	public class SuspensionManagerException : Exception
	{
		// Token: 0x06000656 RID: 1622 RVA: 0x0001DB83 File Offset: 0x0001BD83
		public SuspensionManagerException()
		{
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0001DB8B File Offset: 0x0001BD8B
		public SuspensionManagerException(Exception e) : base("SuspensionManager failed", e)
		{
		}
	}
}
