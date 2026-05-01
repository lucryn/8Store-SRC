using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Callisto.Controls.Common
{
	// Token: 0x02000002 RID: 2
	public class AppManifestHelper
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000022E4 File Offset: 0x000004E4
		[DebuggerStepThrough]
		public static Task<VisualElement> GetManifestVisualElementsAsync()
		{
			AppManifestHelper.<GetManifestVisualElementsAsync>d__3 <GetManifestVisualElementsAsync>d__;
			<GetManifestVisualElementsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<VisualElement>.Create();
			<GetManifestVisualElementsAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<VisualElement> <>t__builder = <GetManifestVisualElementsAsync>d__.<>t__builder;
			<>t__builder.Start<AppManifestHelper.<GetManifestVisualElementsAsync>d__3>(ref <GetManifestVisualElementsAsync>d__);
			return <GetManifestVisualElementsAsync>d__.<>t__builder.Task;
		}
	}
}
