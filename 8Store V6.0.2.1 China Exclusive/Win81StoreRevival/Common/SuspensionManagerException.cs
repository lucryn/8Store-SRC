using System;

namespace Win81StoreRevival.Common
{
	// Token: 0x02000058 RID: 88
	public class SuspensionManagerException : Exception
	{
		// Token: 0x06000509 RID: 1289 RVA: 0x0001C0CB File Offset: 0x0001A2CB
		public SuspensionManagerException()
		{
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0001C0D5 File Offset: 0x0001A2D5
		public SuspensionManagerException(Exception e) : base("SuspensionManager failed", e)
		{
		}
	}
}
