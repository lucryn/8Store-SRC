using System;
using Windows.UI.Xaml.Controls;

namespace Callisto.Controls.SettingsManagement
{
	// Token: 0x0200003C RID: 60
	internal interface ISettingsCommandInfo
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600028B RID: 651
		string HeaderText { get; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600028C RID: 652
		UserControl Instance { get; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600028D RID: 653
		[Obsolete("Use LiteralWidth for Windows 8.1")]
		SettingsFlyout.SettingsFlyoutWidth Width { get; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600028E RID: 654
		double LiteralWidth { get; }
	}
}
