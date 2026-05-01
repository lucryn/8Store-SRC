using System;
using Windows.UI.Xaml.Controls;

namespace Callisto.Controls.SettingsManagement
{
	// Token: 0x0200003E RID: 62
	internal class SettingsCommandInfo<T> : ISettingsCommandInfo where T : UserControl, new()
	{
		// Token: 0x06000291 RID: 657 RVA: 0x0000CDFD File Offset: 0x0000AFFD
		public SettingsCommandInfo(string headerText, SettingsFlyout.SettingsFlyoutWidth width)
		{
			this.HeaderText = headerText;
			this.Width = width;
			if (width == SettingsFlyout.SettingsFlyoutWidth.Narrow)
			{
				this.LiteralWidth = 346.0;
				return;
			}
			this.LiteralWidth = 646.0;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000CE3A File Offset: 0x0000B03A
		public SettingsCommandInfo(string headerText, double width)
		{
			this.HeaderText = headerText;
			this.LiteralWidth = width;
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0000CE50 File Offset: 0x0000B050
		// (set) Token: 0x06000294 RID: 660 RVA: 0x0000CE58 File Offset: 0x0000B058
		public string HeaderText { get; private set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000CE61 File Offset: 0x0000B061
		public UserControl Instance
		{
			get
			{
				return Activator.CreateInstance<T>();
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000CE6D File Offset: 0x0000B06D
		// (set) Token: 0x06000297 RID: 663 RVA: 0x0000CE75 File Offset: 0x0000B075
		[Obsolete("Use LiteralWidth for Windows 8.1")]
		public SettingsFlyout.SettingsFlyoutWidth Width { get; private set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000CE7E File Offset: 0x0000B07E
		// (set) Token: 0x06000299 RID: 665 RVA: 0x0000CE86 File Offset: 0x0000B086
		public double LiteralWidth { get; private set; }
	}
}
