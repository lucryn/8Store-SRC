using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace W80StoreRevival
{
	// Token: 0x02000012 RID: 18
	public class UpdateManager
	{
		// Token: 0x060000BB RID: 187 RVA: 0x0000C800 File Offset: 0x0000AA00
		[DebuggerStepThrough]
		public Task<bool> CheckForUpdatesAsync()
		{
			UpdateManager.<CheckForUpdatesAsync>d__0 <CheckForUpdatesAsync>d__;
			<CheckForUpdatesAsync>d__.<>4__this = this;
			<CheckForUpdatesAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<CheckForUpdatesAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<bool> <>t__builder = <CheckForUpdatesAsync>d__.<>t__builder;
			<>t__builder.Start<UpdateManager.<CheckForUpdatesAsync>d__0>(ref <CheckForUpdatesAsync>d__);
			return <CheckForUpdatesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000C84C File Offset: 0x0000AA4C
		private bool IsVersionGreater(string version1, string version2)
		{
			bool result;
			try
			{
				Debug.WriteLine(string.Concat(new string[]
				{
					"[UPDATE] Comparing versions: ",
					version1,
					" > ",
					version2,
					"?"
				}));
				string[] array = version1.Split(new char[]
				{
					'.'
				});
				string[] array2 = version2.Split(new char[]
				{
					'.'
				});
				for (int i = 0; i < Math.Min(array.Length, array2.Length); i++)
				{
					int num = int.Parse(array[i]);
					int num2 = int.Parse(array2[i]);
					if (num > num2)
					{
						Debug.WriteLine("[UPDATE] Version " + version1 + " is greater than " + version2);
						return true;
					}
					if (num < num2)
					{
						Debug.WriteLine("[UPDATE] Version " + version1 + " is less than " + version2);
						return false;
					}
				}
				bool flag = array.Length > array2.Length;
				Debug.WriteLine("[UPDATE] Version comparison result: " + flag);
				result = flag;
			}
			catch (Exception ex)
			{
				Debug.WriteLine("[UPDATE] Error comparing versions: " + ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x04000076 RID: 118
		private const string CurrentVersion = "0.8.1.1";

		// Token: 0x04000077 RID: 119
		private const string UpdateServerUrl = "http://8store.dankassassin368.com/updates/version1.json";
	}
}
