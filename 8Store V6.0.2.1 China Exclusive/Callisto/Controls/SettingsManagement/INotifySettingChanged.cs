using System;

namespace Callisto.Controls.SettingsManagement
{
	// Token: 0x0200003A RID: 58
	public interface INotifySettingChanged
	{
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000279 RID: 633
		// (remove) Token: 0x0600027A RID: 634
		event EventHandler<SettingChangedEventArgs> SettingChanged;
	}
}
