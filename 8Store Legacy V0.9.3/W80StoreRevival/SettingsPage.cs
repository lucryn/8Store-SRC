using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;

namespace W80StoreRevival
{
	// Token: 0x0200000F RID: 15
	public sealed class SettingsPage : Page, IComponentConnector
	{
		// Token: 0x0600008B RID: 139 RVA: 0x0000B821 File Offset: 0x00009A21
		public SettingsPage()
		{
			this.InitializeComponent();
			this.LoadSettings();
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000B83C File Offset: 0x00009A3C
		private void LoadSettings()
		{
			try
			{
				Debug.WriteLine("[SETTINGS] Loading settings");
				object obj = ApplicationData.Current.LocalSettings.Values["AutoUpdateEnabled"];
				if (obj != null)
				{
					this.AutoUpdateToggle.put_IsOn((bool)obj);
				}
				else
				{
					this.AutoUpdateToggle.put_IsOn(true);
					ApplicationData.Current.LocalSettings.Values["AutoUpdateEnabled"] = true;
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("[SETTINGS] Error loading settings: " + ex.Message);
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000B8EC File Offset: 0x00009AEC
		private void AutoUpdateToggle_Toggled(object sender, RoutedEventArgs e)
		{
			try
			{
				ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
				localSettings.Values["AutoUpdateEnabled"] = this.AutoUpdateToggle.IsOn;
				Debug.WriteLine("[SETTINGS] Auto update set to: " + this.AutoUpdateToggle.IsOn);
			}
			catch (Exception ex)
			{
				Debug.WriteLine("[SETTINGS] Error saving auto update setting: " + ex.Message);
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000BC04 File Offset: 0x00009E04
		[DebuggerStepThrough]
		private void CheckUpdatesButton_Click(object sender, RoutedEventArgs e)
		{
			SettingsPage.<CheckUpdatesButton_Click>d__0 <CheckUpdatesButton_Click>d__;
			<CheckUpdatesButton_Click>d__.<>4__this = this;
			<CheckUpdatesButton_Click>d__.sender = sender;
			<CheckUpdatesButton_Click>d__.e = e;
			<CheckUpdatesButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<CheckUpdatesButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <CheckUpdatesButton_Click>d__.<>t__builder;
			<>t__builder.Start<SettingsPage.<CheckUpdatesButton_Click>d__0>(ref <CheckUpdatesButton_Click>d__);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000BC50 File Offset: 0x00009E50
		public void Show()
		{
			try
			{
				this._settingsPopup = new Popup();
				this._settingsPopup.put_Child(this);
				this._settingsPopup.put_IsLightDismissEnabled(true);
				Popup settingsPopup = this._settingsPopup;
				WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(settingsPopup.add_Closed), new Action<EventRegistrationToken>(settingsPopup.remove_Closed), new EventHandler<object>(this.OnPopupClosed));
				Rect bounds = Window.Current.Bounds;
				this._settingsPopup.put_HorizontalOffset(bounds.Width - 346.0);
				this._settingsPopup.put_VerticalOffset(0.0);
				this._settingsPopup.put_IsOpen(true);
				Debug.WriteLine("[SETTINGS] Settings flyout opened");
			}
			catch (Exception ex)
			{
				Debug.WriteLine("[SETTINGS] Error opening flyout: " + ex.Message);
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000BD3C File Offset: 0x00009F3C
		private void OnPopupClosed(object sender, object e)
		{
			this._settingsPopup = null;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000BD48 File Offset: 0x00009F48
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (!this._contentLoaded)
			{
				this._contentLoaded = true;
				Application.LoadComponent(this, new Uri("ms-appx:///SettingsPage.xaml"), 0);
				this.CheckUpdatesButton = (Button)base.FindName("CheckUpdatesButton");
				this.AutoUpdateToggle = (ToggleSwitch)base.FindName("AutoUpdateToggle");
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000BDAC File Offset: 0x00009FAC
		[DebuggerNonUserCode]
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		public void Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.CheckUpdatesButton_Click));
				break;
			}
			case 2:
			{
				ToggleSwitch toggleSwitch = (ToggleSwitch)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(toggleSwitch.add_Toggled), new Action<EventRegistrationToken>(toggleSwitch.remove_Toggled), new RoutedEventHandler(this.AutoUpdateToggle_Toggled));
				break;
			}
			}
			this._contentLoaded = true;
		}

		// Token: 0x0400005F RID: 95
		private Popup _settingsPopup;

		// Token: 0x04000060 RID: 96
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button CheckUpdatesButton;

		// Token: 0x04000061 RID: 97
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ToggleSwitch AutoUpdateToggle;

		// Token: 0x04000062 RID: 98
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
