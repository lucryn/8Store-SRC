using System;
using Windows.Storage;

namespace Win81StoreRevival
{
	// Token: 0x0200003B RID: 59
	public static class SettingsHelper
	{
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000396 RID: 918 RVA: 0x00015B80 File Offset: 0x00013D80
		// (set) Token: 0x06000397 RID: 919 RVA: 0x00015BB3 File Offset: 0x00013DB3
		public static bool AutoOpen
		{
			get
			{
				object obj = SettingsHelper.localSettings.Values["AutoOpen"];
				return obj != null && Convert.ToBoolean(obj);
			}
			set
			{
				SettingsHelper.localSettings.Values["AutoOpen"] = value;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000398 RID: 920 RVA: 0x00015BD4 File Offset: 0x00013DD4
		// (set) Token: 0x06000399 RID: 921 RVA: 0x00015C07 File Offset: 0x00013E07
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

		// Token: 0x040001B1 RID: 433
		private static ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
	}
}
