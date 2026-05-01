using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Windows.UI.Xaml;

namespace Win81StoreRevival
{
	// Token: 0x02000049 RID: 73
	public static class Program
	{
		// Token: 0x060004B6 RID: 1206 RVA: 0x00017C01 File Offset: 0x00015E01
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
