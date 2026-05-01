using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;

namespace Win81StoreRevival
{
	// Token: 0x02000022 RID: 34
	public sealed class CustomSettingsPage : Page, IComponentConnector
	{
		// Token: 0x060001D3 RID: 467 RVA: 0x0000BBA4 File Offset: 0x00009DA4
		public CustomSettingsPage()
		{
			this.InitializeComponent();
			this.localSettings = ApplicationData.Current.LocalSettings;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000BBC5 File Offset: 0x00009DC5
		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			this.PlaySound();
			base.Frame.GoBack();
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000BBDC File Offset: 0x00009DDC
		private void FinishButton_Click(object sender, RoutedEventArgs e)
		{
			this.PlaySound();
			this.localSettings.Values["AutoUpdateNotify"] = this.AutoUpdateToggle.IsOn;
			this.localSettings.Values["AutoOpen"] = this.AutoOpenToggle.IsOn;
			this.localSettings.Values["LowPerformanceMode"] = this.LowPerformanceToggle.IsOn;
			this.localSettings.Values["DarkMode"] = this.DarkModeToggle.IsOn;
			this.localSettings.Values["WelcomeCompleted"] = true;
			Debug.WriteLine("Custom settings saved:");
			Debug.WriteLine(string.Format("Auto-update: {0}", new object[]
			{
				this.AutoUpdateToggle.IsOn ? "Enabled" : "Disabled"
			}));
			Debug.WriteLine(string.Format("Auto-open: {0}", new object[]
			{
				this.AutoOpenToggle.IsOn ? "Enabled" : "Disabled"
			}));
			Debug.WriteLine(string.Format("Low-performance mode: {0}", new object[]
			{
				this.LowPerformanceToggle.IsOn ? "Enabled" : "Disabled"
			}));
			Debug.WriteLine(string.Format("Dark mode: {0}", new object[]
			{
				this.DarkModeToggle.IsOn ? "Enabled" : "Disabled"
			}));
			base.Frame.Navigate(typeof(MainPage));
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000BD90 File Offset: 0x00009F90
		private void PlaySound()
		{
			try
			{
				this.SoundPlayer.Stop();
				this.SoundPlayer.Play();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("Error playing sound: {0}", new object[]
				{
					ex.Message
				}));
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000BDF0 File Offset: 0x00009FF0
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			bool contentLoaded = this._contentLoaded;
			if (!contentLoaded)
			{
				this._contentLoaded = true;
				Application.LoadComponent(this, new Uri("ms-appx:///CustomSettingsPage.xaml"), 0);
				this.SoundPlayer = (MediaElement)base.FindName("SoundPlayer");
				this.FinishButton = (Button)base.FindName("FinishButton");
				this.DarkModeToggle = (ToggleSwitch)base.FindName("DarkModeToggle");
				this.LowPerformanceToggle = (ToggleSwitch)base.FindName("LowPerformanceToggle");
				this.AutoOpenToggle = (ToggleSwitch)base.FindName("AutoOpenToggle");
				this.AutoUpdateToggle = (ToggleSwitch)base.FindName("AutoUpdateToggle");
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000BEAC File Offset: 0x0000A0AC
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.FinishButton_Click));
			}
			this._contentLoaded = true;
		}

		// Token: 0x040000EA RID: 234
		private ApplicationDataContainer localSettings;

		// Token: 0x040000EB RID: 235
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private MediaElement SoundPlayer;

		// Token: 0x040000EC RID: 236
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button FinishButton;

		// Token: 0x040000ED RID: 237
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ToggleSwitch DarkModeToggle;

		// Token: 0x040000EE RID: 238
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ToggleSwitch LowPerformanceToggle;

		// Token: 0x040000EF RID: 239
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ToggleSwitch AutoOpenToggle;

		// Token: 0x040000F0 RID: 240
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ToggleSwitch AutoUpdateToggle;

		// Token: 0x040000F1 RID: 241
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
