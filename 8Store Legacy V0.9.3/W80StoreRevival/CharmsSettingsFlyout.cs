using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

namespace W80StoreRevival
{
	// Token: 0x02000007 RID: 7
	public sealed class CharmsSettingsFlyout : UserControl, IComponentConnector
	{
		// Token: 0x06000047 RID: 71 RVA: 0x0000764F File Offset: 0x0000584F
		public CharmsSettingsFlyout()
		{
			this.InitializeComponent();
			this.LoadSettings();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00007668 File Offset: 0x00005868
		private void LoadSettings()
		{
			try
			{
				ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
				if (localSettings.Values.ContainsKey("AutoUpdateEnabled"))
				{
					this.AutoUpdateToggle.put_IsOn((bool)localSettings.Values["AutoUpdateEnabled"]);
				}
				else
				{
					this.AutoUpdateToggle.put_IsOn(true);
					localSettings.Values["AutoUpdateEnabled"] = true;
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("[SETTINGS] Error loading settings: {ex.Message}");
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00007708 File Offset: 0x00005908
		private void AutoUpdateToggle_Toggled(object sender, RoutedEventArgs e)
		{
			try
			{
				ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
				localSettings.Values["AutoUpdateEnabled"] = this.AutoUpdateToggle.IsOn;
				Debug.WriteLine("[SETTINGS] Auto update set to: {AutoUpdateToggle.IsOn}");
				if (this.AutoUpdateToggle.IsOn)
				{
					this.CheckForUpdates();
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("[SETTINGS] Error saving auto update setting: {ex.Message}");
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00007940 File Offset: 0x00005B40
		[DebuggerStepThrough]
		private void CheckForUpdates()
		{
			CharmsSettingsFlyout.<CheckForUpdates>d__0 <CheckForUpdates>d__;
			<CheckForUpdates>d__.<>4__this = this;
			<CheckForUpdates>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<CheckForUpdates>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <CheckForUpdates>d__.<>t__builder;
			<>t__builder.Start<CharmsSettingsFlyout.<CheckForUpdates>d__0>(ref <CheckForUpdates>d__);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000797C File Offset: 0x00005B7C
		[DebuggerNonUserCode]
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this._contentLoaded)
			{
				this._contentLoaded = true;
				Application.LoadComponent(this, new Uri("ms-appx:///CharmsSettingsFlyout.xaml"), 0);
				this.AutoUpdateToggle = (ToggleSwitch)base.FindName("AutoUpdateToggle");
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000079C8 File Offset: 0x00005BC8
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				ToggleSwitch toggleSwitch = (ToggleSwitch)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(toggleSwitch.add_Toggled), new Action<EventRegistrationToken>(toggleSwitch.remove_Toggled), new RoutedEventHandler(this.AutoUpdateToggle_Toggled));
			}
			this._contentLoaded = true;
		}

		// Token: 0x04000035 RID: 53
		private const string AutoUpdateKey = "AutoUpdateEnabled";

		// Token: 0x04000036 RID: 54
		private const string LastUpdateCheckKey = "LastUpdateCheckTime";

		// Token: 0x04000037 RID: 55
		private const string UpdateServerUrlKey = "UpdateServerUrl";

		// Token: 0x04000038 RID: 56
		private const string LegacyUpdateKey = "LegacyUpdateEnabled";

		// Token: 0x04000039 RID: 57
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ToggleSwitch AutoUpdateToggle;

		// Token: 0x0400003A RID: 58
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
