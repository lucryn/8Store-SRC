using System;

namespace System.ComponentModel
{
	// Token: 0x02000016 RID: 22
	public interface ITypeDescriptorContext : IServiceProvider
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600008D RID: 141
		IContainer Container { get; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600008E RID: 142
		object Instance { get; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600008F RID: 143
		PropertyDescriptor PropertyDescriptor { get; }

		// Token: 0x06000090 RID: 144
		bool OnComponentChanging();

		// Token: 0x06000091 RID: 145
		void OnComponentChanged();
	}
}
