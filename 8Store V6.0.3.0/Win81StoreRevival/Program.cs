using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Windows.UI.Xaml;

namespace Win81StoreRevival
{
	// Token: 0x02000044 RID: 68
	public static class Program
	{
		// Token: 0x060003DC RID: 988 RVA: 0x00017768 File Offset: 0x00015968
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		private static void Main(string[] args)
		{
			Application.Start(delegate(ApplicationInitializationCallbackParams p)
			{
				new App();
			});
		}
	}
}
