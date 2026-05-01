using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Win81StoreRevival
{
	// Token: 0x0200003E RID: 62
	public static class SuspensionManager
	{
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x000164E0 File Offset: 0x000146E0
		public static Dictionary<string, object> SessionState
		{
			get
			{
				return SuspensionManager._sessionState;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x000164F8 File Offset: 0x000146F8
		public static List<Type> KnownTypes
		{
			get
			{
				return SuspensionManager._knownTypes;
			}
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00016510 File Offset: 0x00014710
		[DebuggerStepThrough]
		public static Task SaveAsync()
		{
			SuspensionManager.<SaveAsync>d__7 <SaveAsync>d__ = new SuspensionManager.<SaveAsync>d__7();
			<SaveAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SaveAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <SaveAsync>d__.<>t__builder;
			<>t__builder.Start<SuspensionManager.<SaveAsync>d__7>(ref <SaveAsync>d__);
			return <SaveAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00016550 File Offset: 0x00014750
		[DebuggerStepThrough]
		public static Task RestoreAsync()
		{
			SuspensionManager.<RestoreAsync>d__8 <RestoreAsync>d__ = new SuspensionManager.<RestoreAsync>d__8();
			<RestoreAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<RestoreAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <RestoreAsync>d__.<>t__builder;
			<>t__builder.Start<SuspensionManager.<RestoreAsync>d__8>(ref <RestoreAsync>d__);
			return <RestoreAsync>d__.<>t__builder.Task;
		}

		// Token: 0x040001BE RID: 446
		private static Dictionary<string, object> _sessionState = new Dictionary<string, object>();

		// Token: 0x040001BF RID: 447
		private static List<Type> _knownTypes = new List<Type>();

		// Token: 0x040001C0 RID: 448
		private const string SessionStateFilename = "_sessionState.dat";
	}
}
