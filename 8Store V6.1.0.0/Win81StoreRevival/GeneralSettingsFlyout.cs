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
	// Token: 0x0200002F RID: 47
	public sealed class GeneralSettingsFlyout : SettingsFlyout, IComponentConnector
	{
		// Token: 0x060002E3 RID: 739 RVA: 0x0000F086 File Offset: 0x0000D286
		public GeneralSettingsFlyout()
		{
			this.InitializeComponent();
			this.localSettings = ApplicationData.Current.LocalSettings;
			this.LoadSettings();
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000F0AC File Offset: 0x0000D2AC
		private void LoadSettings()
		{
			if (this.localSettings.Values.ContainsKey("SoundEffectsEnabled"))
			{
				this.SoundEffectsSwitch.put_IsOn((bool)this.localSettings.Values["SoundEffectsEnabled"]);
			}
			else
			{
				this.localSettings.Values["SoundEffectsEnabled"] = true;
				this.SoundEffectsSwitch.put_IsOn(true);
			}
			if (this.localSettings.Values.ContainsKey("LowPerformanceMode"))
			{
				this.LowPerformanceModeSwitch.put_IsOn((bool)this.localSettings.Values["LowPerformanceMode"]);
			}
			if (this.localSettings.Values.ContainsKey("AutoUpdateNotify"))
			{
				this.AutoUpdateNotifySwitch.put_IsOn((bool)this.localSettings.Values["AutoUpdateNotify"]);
			}
			if (this.localSettings.Values.ContainsKey("AutoOpenAppx"))
			{
				this.AutoOpenSwitch.put_IsOn((bool)this.localSettings.Values["AutoOpenAppx"]);
			}
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000F1D4 File Offset: 0x0000D3D4
		private void SoundEffectsSwitch_Toggled(object sender, RoutedEventArgs e)
		{
			try
			{
				bool isOn = this.SoundEffectsSwitch.IsOn;
				this.localSettings.Values["SoundEffectsEnabled"] = isOn;
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000F220 File Offset: 0x0000D420
		private void LowPerformanceModeSwitch_Toggled(object sender, RoutedEventArgs e)
		{
			try
			{
				this.localSettings.Values["LowPerformanceMode"] = this.LowPerformanceModeSwitch.IsOn;
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000F268 File Offset: 0x0000D468
		private void AutoUpdateNotifySwitch_Toggled(object sender, RoutedEventArgs e)
		{
			try
			{
				this.localSettings.Values["AutoUpdateNotify"] = this.AutoUpdateNotifySwitch.IsOn;
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000F2B0 File Offset: 0x0000D4B0
		private void AutoOpenSwitch_Toggled(object sender, RoutedEventArgs e)
		{
			try
			{
				this.localSettings.Values["AutoOpenAppx"] = this.AutoOpenSwitch.IsOn;
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000F2F8 File Offset: 0x0000D4F8
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Application.LoadComponent(this, new Uri("ms-appx:///GeneralSettingsFlyout.xaml"), 0);
			this.SoundEffectsSwitch = (ToggleSwitch)base.FindName("SoundEffectsSwitch");
			this.LowPerformanceModeSwitch = (ToggleSwitch)base.FindName("LowPerformanceModeSwitch");
			this.AutoUpdateNotifySwitch = (ToggleSwitch)base.FindName("AutoUpdateNotifySwitch");
			this.AutoOpenSwitch = (ToggleSwitch)base.FindName("AutoOpenSwitch");
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000F380 File Offset: 0x0000D580
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
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(toggleSwitch.add_Toggled), new Action<EventRegistrationToken>(toggleSwitch.remove_Toggled), new RoutedEventHandler(this.LowPerformanceModeSwitch_Toggled));
				break;
			}
			case 3:
			{
				ToggleSwitch toggleSwitch = (ToggleSwitch)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(toggleSwitch.add_Toggled), new Action<EventRegistrationToken>(toggleSwitch.remove_Toggled), new RoutedEventHandler(this.AutoUpdateNotifySwitch_Toggled));
				break;
			}
			case 4:
			{
				ToggleSwitch toggleSwitch = (ToggleSwitch)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(toggleSwitch.add_Toggled), new Action<EventRegistrationToken>(toggleSwitch.remove_Toggled), new RoutedEventHandler(this.AutoOpenSwitch_Toggled));
				break;
			}
			}
			this._contentLoaded = true;
		}

		// Token: 0x0400013B RID: 315
		private ApplicationDataContainer localSettings;

		// Token: 0x0400013C RID: 316
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ToggleSwitch SoundEffectsSwitch;

		// Token: 0x0400013D RID: 317
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ToggleSwitch LowPerformanceModeSwitch;

		// Token: 0x0400013E RID: 318
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ToggleSwitch AutoUpdateNotifySwitch;

		// Token: 0x0400013F RID: 319
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ToggleSwitch AutoOpenSwitch;

		// Token: 0x04000140 RID: 320
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
