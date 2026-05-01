using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Win81StoreRevival
{
	// Token: 0x02000044 RID: 68
	public static class SuspensionManager
	{
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x00017089 File Offset: 0x00015289
		public static Dictionary<string, object> SessionState
		{
			get
			{
				return SuspensionManager._sessionState;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x00017090 File Offset: 0x00015290
		public static List<Type> KnownTypes
		{
			get
			{
				return SuspensionManager._knownTypes;
			}
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00017098 File Offset: 0x00015298
		public static Task SaveAsync()
		{
			SuspensionManager.<SaveAsync>d__7 <SaveAsync>d__;
			<SaveAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SaveAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <SaveAsync>d__.<>t__builder;
			<>t__builder.Start<SuspensionManager.<SaveAsync>d__7>(ref <SaveAsync>d__);
			return <SaveAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x000170D8 File Offset: 0x000152D8
		public static Task RestoreAsync()
		{
			SuspensionManager.<RestoreAsync>d__8 <RestoreAsync>d__;
			<RestoreAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<RestoreAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <RestoreAsync>d__.<>t__builder;
			<>t__builder.Start<SuspensionManager.<RestoreAsync>d__8>(ref <RestoreAsync>d__);
			return <RestoreAsync>d__.<>t__builder.Task;
		}

		// Token: 0x040001FD RID: 509
		private static Dictionary<string, object> _sessionState = new Dictionary<string, object>();

		// Token: 0x040001FE RID: 510
		private static List<Type> _knownTypes = new List<Type>();

		// Token: 0x040001FF RID: 511
		private const string SessionStateFilename = "_sessionState.dat";
	}
}
