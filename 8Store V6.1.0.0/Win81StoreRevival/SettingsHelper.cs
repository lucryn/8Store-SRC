using System;
using Windows.Storage;

namespace Win81StoreRevival
{
	// Token: 0x02000041 RID: 65
	public static class SettingsHelper
	{
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x0001688C File Offset: 0x00014A8C
		// (set) Token: 0x0600047B RID: 1147 RVA: 0x000168E2 File Offset: 0x00014AE2
		public static bool AutoOpen
		{
			get
			{
				object obj;
				if (SettingsHelper.localSettings.Values.TryGetValue("AutoOpenAppx", ref obj))
				{
					return obj != null && Convert.ToBoolean(obj);
				}
				return SettingsHelper.localSettings.Values.TryGetValue("AutoOpen", ref obj) && obj != null && Convert.ToBoolean(obj);
			}
			set
			{
				SettingsHelper.localSettings.Values["AutoOpenAppx"] = value;
				SettingsHelper.localSettings.Values["AutoOpen"] = value;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x00016918 File Offset: 0x00014B18
		// (set) Token: 0x0600047D RID: 1149 RVA: 0x00016945 File Offset: 0x00014B45
		public static bool AutoSideload
		{
			get
			{
				object obj = SettingsHelper.localSettings.Values["AutoSideload"];
				return obj == null || Convert.ToBoolean(obj);
			}
			set
			{
				SettingsHelper.localSettings.Values["AutoSideload"] = value;
			}
		}

		// Token: 0x040001F0 RID: 496
		private static ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
	}
}
