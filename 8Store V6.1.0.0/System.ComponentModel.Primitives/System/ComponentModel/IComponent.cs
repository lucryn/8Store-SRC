using System;

namespace System.ComponentModel
{
	// Token: 0x0200000E RID: 14
	public interface IComponent : IDisposable
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000060 RID: 96
		// (set) Token: 0x06000061 RID: 97
		ISite Site { get; set; }

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000062 RID: 98
		// (remove) Token: 0x06000063 RID: 99
		event EventHandler Disposed;
	}
}
