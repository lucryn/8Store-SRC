using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

namespace Win81StoreRevival
{
	// Token: 0x0200002C RID: 44
	public sealed class GeneralSettingsFlyout : SettingsFlyout, IComponentConnector
	{
		// Token: 0x06000265 RID: 613 RVA: 0x0000E20E File Offset: 0x0000C40E
		public GeneralSettingsFlyout()
		{
			this.InitializeComponent();
			this.localSettings = ApplicationData.Current.LocalSettings;
			this.LoadSettings();
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000E238 File Offset: 0x0000C438
		private void LoadSettings()
		{
			bool flag = this.localSettings.Values.ContainsKey("SoundEffectsEnabled");
			if (flag)
			{
				this.SoundEffectsSwitch.put_IsOn((bool)this.localSettings.Values["SoundEffectsEnabled"]);
			}
			else
			{
				this.localSettings.Values["SoundEffectsEnabled"] = true;
				this.SoundEffectsSwitch.put_IsOn(true);
			}
			bool flag2 = this.localSettings.Values.ContainsKey("DarkModeEnabled");
			if (flag2)
			{
				this.DarkModeSwitch.put_IsOn((bool)this.localSettings.Values["DarkModeEnabled"]);
			}
			bool flag3 = this.localSettings.Values.ContainsKey("LowPerformanceMode");
			if (flag3)
			{
				this.LowPerformanceModeSwitch.put_IsOn((bool)this.localSettings.Values["LowPerformanceMode"]);
			}
			bool flag4 = this.localSettings.Values.ContainsKey("AutoUpdateNotify");
			if (flag4)
			{
				this.AutoUpdateNotifySwitch.put_IsOn((bool)this.localSettings.Values["AutoUpdateNotify"]);
			}
			bool flag5 = this.localSettings.Values.ContainsKey("AutoOpenAppx");
			if (flag5)
			{
				this.AutoOpenSwitch.put_IsOn((bool)this.localSettings.Values["AutoOpenAppx"]);
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000E3BC File Offset: 0x0000C5BC
		private void SoundEffectsSwitch_Toggled(object sender, RoutedEventArgs e)
		{
			try
			{
				bool isOn = this.SoundEffectsSwitch.IsOn;
				this.localSettings.Values["SoundEffectsEnabled"] = isOn;
				Debug.WriteLine(string.Format("Sound effects {0}", new object[]
				{
					isOn ? "enabled" : "disabled"
				}));
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error saving sound setting: " + ex.Message);
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000E44C File Offset: 0x0000C64C
		private void DarkModeSwitch_Toggled(object sender, RoutedEventArgs e)
		{
			try
			{
				this.localSettings.Values["DarkModeEnabled"] = this.DarkModeSwitch.IsOn;
				Debug.WriteLine("Dark mode toggled: " + this.DarkModeSwitch.IsOn.ToString());
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error saving dark mode setting: " + ex.Message);
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000E4D4 File Offset: 0x0000C6D4
		private void LowPerformanceModeSwitch_Toggled(object sender, RoutedEventArgs e)
		{
			try
			{
				this.localSettings.Values["LowPerformanceMode"] = this.LowPerformanceModeSwitch.IsOn;
				Debug.WriteLine("Low performance mode toggled: " + this.LowPerformanceModeSwitch.IsOn.ToString());
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error saving low performance mode setting: " + ex.Message);
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000E55C File Offset: 0x0000C75C
		private void AutoUpdateNotifySwitch_Toggled(object sender, RoutedEventArgs e)
		{
			try
			{
				this.localSettings.Values["AutoUpdateNotify"] = this.AutoUpdateNotifySwitch.IsOn;
				Debug.WriteLine("Auto update notify toggled: " + this.AutoUpdateNotifySwitch.IsOn.ToString());
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error saving auto update notify setting: " + ex.Message);
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000E5E4 File Offset: 0x0000C7E4
		private void AutoOpenSwitch_Toggled(object sender, RoutedEventArgs e)
		{
			try
			{
				this.localSettings.Values["AutoOpenAppx"] = this.AutoOpenSwitch.IsOn;
				Debug.WriteLine("Auto open appx toggled: " + this.AutoOpenSwitch.IsOn.ToString());
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error saving auto open appx setting: " + ex.Message);
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000E66C File Offset: 0x0000C86C
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			bool contentLoaded = this._contentLoaded;
			if (!contentLoaded)
			{
				this._contentLoaded = true;
				Application.LoadComponent(this, new Uri("ms-appx:///GeneralSettingsFlyout.xaml"), 0);
				this.SoundEffectsSwitch = (ToggleSwitch)base.FindName("SoundEffectsSwitch");
				this.DarkModeSwitch = (ToggleSwitch)base.FindName("DarkModeSwitch");
				this.LowPerformanceModeSwitch = (ToggleSwitch)base.FindName("LowPerformanceModeSwitch");
				this.AutoUpdateNotifySwitch = (ToggleSwitch)base.FindName("AutoUpdateNotifySwitch");
				this.AutoOpenSwitch = (ToggleSwitch)base.FindName("AutoOpenSwitch");
				this.LoadingPanel = (StackPanel)base.FindName("LoadingPanel");
				this.StatusMessage = (TextBlock)base.FindName("StatusMessage");
				this.LoadingRing = (ProgressRing)base.FindName("LoadingRing");
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000E754 File Offset: 0x0000C954
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
			{
				ToggleSwitch toggleSwitch = (ToggleSwitch)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(toggleSwitch.add_Toggled), new Action<EventRegistrationToken>(toggleSwitch.remove_Toggled), new RoutedEventHandler(this.SoundEffectsSwitch_Toggled));
				break;
			}
			case 2:
			{
				ToggleSwitch toggleSwitch = (ToggleSwitch)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(toggleSwitch.add_Toggled), new Action<EventRegistrationToken>(toggleSwitch.remove_Toggled), new RoutedEventHandler(this.DarkModeSwitch_Toggled));
				break;
			}
			case 3:
			{
				ToggleSwitch toggleSwitch = (ToggleSwitch)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(toggleSwitch.add_Toggled), new Action<EventRegistrationToken>(toggleSwitch.remove_Toggled), new RoutedEventHandler(this.LowPerformanceModeSwitch_Toggled));
				break;
			}
			case 4:
			{
				ToggleSwitch toggleSwitch = (ToggleSwitch)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(toggleSwitch.add_Toggled), new Action<EventRegistrationToken>(toggleSwitch.remove_Toggled), new RoutedEventHandler(this.AutoUpdateNotifySwitch_Toggled));
				break;
			}
			case 5:
			{
				ToggleSwitch toggleSwitch = (ToggleSwitch)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(toggleSwitch.add_Toggled), new Action<EventRegistrationToken>(toggleSwitch.remove_Toggled), new RoutedEventHandler(this.AutoOpenSwitch_Toggled));
				break;
			}
			}
			this._contentLoaded = true;
		}

		// Token: 0x04000131 RID: 305
		private ApplicationDataContainer localSettings;

		// Token: 0x04000132 RID: 306
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ToggleSwitch SoundEffectsSwitch;

		// Token: 0x04000133 RID: 307
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ToggleSwitch DarkModeSwitch;

		// Token: 0x04000134 RID: 308
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ToggleSwitch LowPerformanceModeSwitch;

		// Token: 0x04000135 RID: 309
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ToggleSwitch AutoUpdateNotifySwitch;

		// Token: 0x04000136 RID: 310
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ToggleSwitch AutoOpenSwitch;

		// Token: 0x04000137 RID: 311
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel LoadingPanel;

		// Token: 0x04000138 RID: 312
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock StatusMessage;

		// Token: 0x04000139 RID: 313
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ProgressRing LoadingRing;

		// Token: 0x0400013A RID: 314
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
