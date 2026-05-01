using System;

namespace System.ComponentModel
{
	// Token: 0x02000012 RID: 18
	public interface ISite : IServiceProvider
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600006F RID: 111
		IComponent Component { get; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000070 RID: 112
		IContainer Container { get; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000071 RID: 113
		bool DesignMode { get; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000072 RID: 114
		// (set) Token: 0x06000073 RID: 115
		string Name { get; set; }
	}
}
