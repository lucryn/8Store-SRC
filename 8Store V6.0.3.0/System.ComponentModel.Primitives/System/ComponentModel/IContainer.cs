using System;

namespace System.ComponentModel
{
	// Token: 0x0200000F RID: 15
	public interface IContainer : IDisposable
	{
		// Token: 0x06000064 RID: 100
		void Add(IComponent component);

		// Token: 0x06000065 RID: 101
		void Add(IComponent component, string name);

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000066 RID: 102
		ComponentCollection Components { get; }

		// Token: 0x06000067 RID: 103
		void Remove(IComponent component);
	}
}
