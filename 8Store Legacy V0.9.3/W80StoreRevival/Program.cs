using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Windows.UI.Xaml;

namespace W80StoreRevival
{
	// Token: 0x02000016 RID: 22
	public static class Program
	{
		// Token: 0x060000ED RID: 237 RVA: 0x00010CC7 File Offset: 0x0000EEC7
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
