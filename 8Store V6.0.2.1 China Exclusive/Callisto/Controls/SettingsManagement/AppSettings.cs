using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Callisto.Controls.Common;
using Windows.UI.ApplicationSettings;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Callisto.Controls.SettingsManagement
{
	// Token: 0x0200003B RID: 59
	public sealed class AppSettings : INotifySettingChanged
	{
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600027B RID: 635 RVA: 0x0000C8B0 File Offset: 0x0000AAB0
		// (remove) Token: 0x0600027C RID: 636 RVA: 0x0000C8E8 File Offset: 0x0000AAE8
		public event EventHandler<SettingChangedEventArgs> SettingChanged;

		// Token: 0x0600027D RID: 637 RVA: 0x0000C91D File Offset: 0x0000AB1D
		private AppSettings()
		{
			this.Configure();
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000C936 File Offset: 0x0000AB36
		// (set) Token: 0x0600027F RID: 639 RVA: 0x0000C93E File Offset: 0x0000AB3E
		[Obsolete("This is provided for backwards compatability until a proper light theme can be created so settings content will display corectly on a white background.", false)]
		public SolidColorBrush ContentBackgroundBrush { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000C948 File Offset: 0x0000AB48
		public static AppSettings Current
		{
			get
			{
				if (AppSettings._instance == null)
				{
					lock (AppSettings.SyncRoot)
					{
						if (AppSettings._instance == null)
						{
							AppSettings._instance = new AppSettings();
						}
					}
				}
				return AppSettings._instance;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000C9A8 File Offset: 0x0000ABA8
		public VisualElement VisualElements
		{
			get
			{
				return this._visualElement;
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000C9B0 File Offset: 0x0000ABB0
		[Obsolete("Use other overloads for Windows 8.1 that don't use FlyoutWidth")]
		public void AddCommand<T>(string headerText, SettingsFlyout.SettingsFlyoutWidth width = SettingsFlyout.SettingsFlyoutWidth.Narrow) where T : UserControl, new()
		{
			string text = headerText.Trim().Replace(" ", "");
			if (!this._commands.ContainsKey(text))
			{
				this._commands.Add(text, new SettingsCommandInfo<T>(headerText, width));
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000C9F4 File Offset: 0x0000ABF4
		public void AddCommand<T>(string headerText, double width = 500.0) where T : UserControl, new()
		{
			string text = headerText.Trim().Replace(" ", "");
			if (!this._commands.ContainsKey(text))
			{
				this._commands.Add(text, new SettingsCommandInfo<T>(headerText, width));
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000CA38 File Offset: 0x0000AC38
		[Obsolete("Use other overloads for Windows 8.1 that don't use FlyoutWidth")]
		public void AddCommand<T>(string headerText, SolidColorBrush headerBrush, SettingsFlyout.SettingsFlyoutWidth width = SettingsFlyout.SettingsFlyoutWidth.Narrow) where T : UserControl, new()
		{
			string text = headerText.Trim().Replace(" ", "");
			this._headerBrush = headerBrush;
			if (!this._commands.ContainsKey(text))
			{
				this._commands.Add(text, new SettingsCommandInfo<T>(headerText, width));
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000CA84 File Offset: 0x0000AC84
		public void AddCommand<T>(string headerText, SolidColorBrush headerBrush, double width = 500.0) where T : UserControl, new()
		{
			string text = headerText.Trim().Replace(" ", "");
			this._headerBrush = headerBrush;
			if (!this._commands.ContainsKey(text))
			{
				this._commands.Add(text, new SettingsCommandInfo<T>(headerText, width));
			}
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000CAD0 File Offset: 0x0000ACD0
		private void CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
		{
			if (this._commands != null)
			{
				foreach (KeyValuePair<object, ISettingsCommandInfo> keyValuePair in this._commands)
				{
					SettingsCommand settingsCommand = new SettingsCommand(keyValuePair.Key, keyValuePair.Value.HeaderText, new UICommandInvokedHandler(this.SettingsCommandInvokedHandler));
					args.Request.ApplicationCommands.Add(settingsCommand);
				}
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000CCD4 File Offset: 0x0000AED4
		[DebuggerStepThrough]
		private void Configure()
		{
			AppSettings.<Configure>d__1 <Configure>d__;
			<Configure>d__.<>4__this = this;
			<Configure>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<Configure>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <Configure>d__.<>t__builder;
			<>t__builder.Start<AppSettings.<Configure>d__1>(ref <Configure>d__);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000CD10 File Offset: 0x0000AF10
		private void SettingsCommandInvokedHandler(IUICommand command)
		{
			ISettingsCommandInfo settingsCommandInfo = this._commands[command.Id];
			this._settingsFlyout = new SettingsFlyout();
			this._settingsFlyout.put_HeaderBackground(this._headerBrush);
			this._settingsFlyout.put_IconSource(this._smallLogoImageSource);
			this._settingsFlyout.put_Title(command.Label);
			if (this.ContentBackgroundBrush != null)
			{
				this._settingsFlyout.put_Background(this.ContentBackgroundBrush);
			}
			this._settingsFlyout.put_Content(settingsCommandInfo.Instance);
			this._settingsFlyout.put_Width(settingsCommandInfo.LiteralWidth);
			this._settingsFlyout.Show();
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000CDB4 File Offset: 0x0000AFB4
		public void SignalSettingChanged([CallerMemberName] string settingName = "")
		{
			EventHandler<SettingChangedEventArgs> settingChanged = this.SettingChanged;
			if (settingChanged != null)
			{
				SettingChangedEventArgs settingChangedEventArgs = new SettingChangedEventArgs(settingName);
				settingChanged.Invoke(this, settingChangedEventArgs);
			}
		}

		// Token: 0x04000129 RID: 297
		private readonly Dictionary<object, ISettingsCommandInfo> _commands = new Dictionary<object, ISettingsCommandInfo>();

		// Token: 0x0400012A RID: 298
		private SolidColorBrush _headerBrush;

		// Token: 0x0400012B RID: 299
		private static volatile AppSettings _instance;

		// Token: 0x0400012C RID: 300
		private SettingsFlyout _settingsFlyout;

		// Token: 0x0400012D RID: 301
		private BitmapImage _smallLogoImageSource;

		// Token: 0x0400012E RID: 302
		private static readonly object SyncRoot = new object();

		// Token: 0x0400012F RID: 303
		private VisualElement _visualElement;
	}
}
