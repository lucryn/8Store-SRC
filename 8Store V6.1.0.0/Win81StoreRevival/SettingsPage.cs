using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace Win81StoreRevival
{
	// Token: 0x02000042 RID: 66
	public sealed class SettingsPage : Page, IComponentConnector
	{
		// Token: 0x0600047F RID: 1151 RVA: 0x00016972 File Offset: 0x00014B72
		public SettingsPage()
		{
			this.InitializeComponent();
			this.localSettings = ApplicationData.Current.LocalSettings;
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00016990 File Offset: 0x00014B90
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);
			this.LoadSettings();
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x000169A0 File Offset: 0x00014BA0
		private void LoadSettings()
		{
			if (this.localSettings.Values.ContainsKey("LastRefreshTime"))
			{
				DateTimeOffset dateTimeOffset = (DateTimeOffset)this.localSettings.Values["LastRefreshTime"];
				this.LastUpdatedText.put_Text(dateTimeOffset.ToString("g"));
			}
			else
			{
				this.LastUpdatedText.put_Text("Never");
			}
			this.UpdateVersionDisplay();
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00004901 File Offset: 0x00002B01
		private void UpdateVersionDisplay()
		{
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00004859 File Offset: 0x00002A59
		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(MainPage));
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00016A10 File Offset: 0x00014C10
		private void RefreshButton_Click(object sender, RoutedEventArgs e)
		{
			SettingsPage.<RefreshButton_Click>d__8 <RefreshButton_Click>d__;
			<RefreshButton_Click>d__.<>4__this = this;
			<RefreshButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<RefreshButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <RefreshButton_Click>d__.<>t__builder;
			<>t__builder.Start<SettingsPage.<RefreshButton_Click>d__8>(ref <RefreshButton_Click>d__);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00016A4C File Offset: 0x00014C4C
		private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
		{
			Image image = sender as Image;
			if (image != null)
			{
				try
				{
					BitmapImage bitmapImage = new BitmapImage(new Uri("ms-appx:///Assets/icon-error.png"));
					image.put_Source(bitmapImage);
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00016A90 File Offset: 0x00014C90
		private Task RefreshAppsAsync()
		{
			SettingsPage.<RefreshAppsAsync>d__10 <RefreshAppsAsync>d__;
			<RefreshAppsAsync>d__.<>4__this = this;
			<RefreshAppsAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<RefreshAppsAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <RefreshAppsAsync>d__.<>t__builder;
			<>t__builder.Start<SettingsPage.<RefreshAppsAsync>d__10>(ref <RefreshAppsAsync>d__);
			return <RefreshAppsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00004901 File Offset: 0x00002B01
		private void SettingsItem_Tapped(object sender, TappedRoutedEventArgs e)
		{
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00016AD8 File Offset: 0x00014CD8
		private void CheckUpdatesButton_Click(object sender, RoutedEventArgs e)
		{
			SettingsPage.<CheckUpdatesButton_Click>d__12 <CheckUpdatesButton_Click>d__;
			<CheckUpdatesButton_Click>d__.<>4__this = this;
			<CheckUpdatesButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<CheckUpdatesButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <CheckUpdatesButton_Click>d__.<>t__builder;
			<>t__builder.Start<SettingsPage.<CheckUpdatesButton_Click>d__12>(ref <CheckUpdatesButton_Click>d__);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00016B14 File Offset: 0x00014D14
		private Task CheckForUpdatesFromSettings()
		{
			SettingsPage.<CheckForUpdatesFromSettings>d__13 <CheckForUpdatesFromSettings>d__;
			<CheckForUpdatesFromSettings>d__.<>4__this = this;
			<CheckForUpdatesFromSettings>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<CheckForUpdatesFromSettings>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <CheckForUpdatesFromSettings>d__.<>t__builder;
			<>t__builder.Start<SettingsPage.<CheckForUpdatesFromSettings>d__13>(ref <CheckForUpdatesFromSettings>d__);
			return <CheckForUpdatesFromSettings>d__.<>t__builder.Task;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00016B5C File Offset: 0x00014D5C
		private bool IsUpdateRequired(string currentVersion, string latestVersion)
		{
			bool result;
			try
			{
				string[] array = currentVersion.Split(new char[]
				{
					'.'
				});
				string[] array2 = latestVersion.Split(new char[]
				{
					'.'
				});
				for (int i = 0; i < Math.Min(array.Length, array2.Length); i++)
				{
					int num = 0;
					int num2 = 0;
					if (int.TryParse(array[i], ref num) && int.TryParse(array2[i], ref num2))
					{
						if (num2 > num)
						{
							return true;
						}
						if (num2 < num)
						{
							return false;
						}
					}
				}
				result = false;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00016BF4 File Offset: 0x00014DF4
		private Task ShowMessage(string title, string message)
		{
			SettingsPage.<ShowMessage>d__15 <ShowMessage>d__;
			<ShowMessage>d__.title = title;
			<ShowMessage>d__.message = message;
			<ShowMessage>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ShowMessage>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <ShowMessage>d__.<>t__builder;
			<>t__builder.Start<SettingsPage.<ShowMessage>d__15>(ref <ShowMessage>d__);
			return <ShowMessage>d__.<>t__builder.Task;
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00016C44 File Offset: 0x00014E44
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Application.LoadComponent(this, new Uri("ms-appx:///SettingsPage.xaml"), 0);
			this.LastUpdatedText = (TextBlock)base.FindName("LastUpdatedText");
			this.CheckUpdatesButton = (Button)base.FindName("CheckUpdatesButton");
			this.LoadingPanel = (StackPanel)base.FindName("LoadingPanel");
			this.StatusMessage = (TextBlock)base.FindName("StatusMessage");
			this.LoadingRing = (ProgressRing)base.FindName("LoadingRing");
			this.RefreshButton = (Button)base.FindName("RefreshButton");
			this.TopAppsButton = (Button)base.FindName("TopAppsButton");
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00016D0C File Offset: 0x00014F0C
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
			{
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uielement.add_Tapped), new Action<EventRegistrationToken>(uielement.remove_Tapped), new TappedEventHandler(this.SettingsItem_Tapped));
				break;
			}
			case 2:
			{
				Image image = (Image)target;
				WindowsRuntimeMarshal.AddEventHandler<ExceptionRoutedEventHandler>(new Func<ExceptionRoutedEventHandler, EventRegistrationToken>(image.add_ImageFailed), new Action<EventRegistrationToken>(image.remove_ImageFailed), new ExceptionRoutedEventHandler(this.Image_ImageFailed));
				break;
			}
			case 3:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.CheckUpdatesButton_Click));
				break;
			}
			case 4:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.RefreshButton_Click));
				break;
			}
			case 5:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.BackButton_Click));
				break;
			}
			}
			this._contentLoaded = true;
		}

		// Token: 0x040001F1 RID: 497
		private ApplicationDataContainer localSettings;

		// Token: 0x040001F2 RID: 498
		private const string CURRENT_VERSION = "5.3.1.0";

		// Token: 0x040001F3 RID: 499
		private const string VERSION_CHECK_URL = "https://8store.modyleprojects.ru/updates/version.json";

		// Token: 0x040001F4 RID: 500
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock LastUpdatedText;

		// Token: 0x040001F5 RID: 501
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button CheckUpdatesButton;

		// Token: 0x040001F6 RID: 502
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel LoadingPanel;

		// Token: 0x040001F7 RID: 503
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock StatusMessage;

		// Token: 0x040001F8 RID: 504
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ProgressRing LoadingRing;

		// Token: 0x040001F9 RID: 505
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button RefreshButton;

		// Token: 0x040001FA RID: 506
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button TopAppsButton;

		// Token: 0x040001FB RID: 507
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
