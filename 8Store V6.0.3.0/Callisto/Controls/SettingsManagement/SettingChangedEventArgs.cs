using System;

namespace Callisto.Controls.SettingsManagement
{
	// Token: 0x0200003D RID: 61
	public sealed class SettingChangedEventArgs : EventArgs
	{
		// Token: 0x0600028F RID: 655 RVA: 0x0000CDE6 File Offset: 0x0000AFE6
		public SettingChangedEventArgs(string settingName)
		{
			this._settingName = settingName;
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000290 RID: 656 RVA: 0x0000CDF5 File Offset: 0x0000AFF5
		public string SettingName
		{
			get
			{
				return this._settingName;
			}
		}

		// Token: 0x04000131 RID: 305
		private readonly string _settingName;
	}
}
