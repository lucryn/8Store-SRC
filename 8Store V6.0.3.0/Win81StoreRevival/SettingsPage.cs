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
	// Token: 0x0200003C RID: 60
	public sealed class SettingsPage : Page, IComponentConnector
	{
		// Token: 0x0600039B RID: 923 RVA: 0x00015C36 File Offset: 0x00013E36
		public SettingsPage()
		{
			this.InitializeComponent();
			this.localSettings = ApplicationData.Current.LocalSettings;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00015C57 File Offset: 0x00013E57
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);
			this.LoadSettings();
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00015C6C File Offset: 0x00013E6C
		private void LoadSettings()
		{
			bool flag = this.localSettings.Values.ContainsKey("LastRefreshTime");
			if (flag)
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

		// Token: 0x0600039E RID: 926 RVA: 0x0000595D File Offset: 0x00003B5D
		private void UpdateVersionDisplay()
		{
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000BF0F File Offset: 0x0000A10F
		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(MainPage));
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00015CE4 File Offset: 0x00013EE4
		[DebuggerStepThrough]
		private void RefreshButton_Click(object sender, RoutedEventArgs e)
		{
			SettingsPage.<RefreshButton_Click>d__8 <RefreshButton_Click>d__ = new SettingsPage.<RefreshButton_Click>d__8();
			<RefreshButton_Click>d__.<>4__this = this;
			<RefreshButton_Click>d__.sender = sender;
			<RefreshButton_Click>d__.e = e;
			<RefreshButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<RefreshButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <RefreshButton_Click>d__.<>t__builder;
			<>t__builder.Start<SettingsPage.<RefreshButton_Click>d__8>(ref <RefreshButton_Click>d__);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00015D30 File Offset: 0x00013F30
		private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
		{
			Image image = sender as Image;
			bool flag = image != null;
			if (flag)
			{
				Debug.WriteLine(string.Format("Image failed to load: {0}", new object[]
				{
					e.ErrorMessage
				}));
				try
				{
					Uri uri = new Uri("https://cdn-icons-png.flaticon.com/512/149/149452.png");
					BitmapImage bitmapImage = new BitmapImage(uri);
					image.put_Source(bitmapImage);
				}
				catch (Exception ex)
				{
					Debug.WriteLine(string.Format("Fallback image also failed: {0}", new object[]
					{
						ex.Message
					}));
				}
			}
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00015DC4 File Offset: 0x00013FC4
		[DebuggerStepThrough]
		private Task RefreshAppsAsync()
		{
			SettingsPage.<RefreshAppsAsync>d__10 <RefreshAppsAsync>d__ = new SettingsPage.<RefreshAppsAsync>d__10();
			<RefreshAppsAsync>d__.<>4__this = this;
			<RefreshAppsAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<RefreshAppsAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <RefreshAppsAsync>d__.<>t__builder;
			<>t__builder.Start<SettingsPage.<RefreshAppsAsync>d__10>(ref <RefreshAppsAsync>d__);
			return <RefreshAppsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000595D File Offset: 0x00003B5D
		private void SettingsItem_Tapped(object sender, TappedRoutedEventArgs e)
		{
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00015E0C File Offset: 0x0001400C
		[DebuggerStepThrough]
		private void CheckUpdatesButton_Click(object sender, RoutedEventArgs e)
		{
			SettingsPage.<CheckUpdatesButton_Click>d__12 <CheckUpdatesButton_Click>d__ = new SettingsPage.<CheckUpdatesButton_Click>d__12();
			<CheckUpdatesButton_Click>d__.<>4__this = this;
			<CheckUpdatesButton_Click>d__.sender = sender;
			<CheckUpdatesButton_Click>d__.e = e;
			<CheckUpdatesButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<CheckUpdatesButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <CheckUpdatesButton_Click>d__.<>t__builder;
			<>t__builder.Start<SettingsPage.<CheckUpdatesButton_Click>d__12>(ref <CheckUpdatesButton_Click>d__);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00015E58 File Offset: 0x00014058
		[DebuggerStepThrough]
		private Task CheckForUpdatesFromSettings()
		{
			SettingsPage.<CheckForUpdatesFromSettings>d__13 <CheckForUpdatesFromSettings>d__ = new SettingsPage.<CheckForUpdatesFromSettings>d__13();
			<CheckForUpdatesFromSettings>d__.<>4__this = this;
			<CheckForUpdatesFromSettings>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<CheckForUpdatesFromSettings>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <CheckForUpdatesFromSettings>d__.<>t__builder;
			<>t__builder.Start<SettingsPage.<CheckForUpdatesFromSettings>d__13>(ref <CheckForUpdatesFromSettings>d__);
			return <CheckForUpdatesFromSettings>d__.<>t__builder.Task;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00015EA0 File Offset: 0x000140A0
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
					bool flag = int.TryParse(array[i], ref num) && int.TryParse(array2[i], ref num2);
					if (flag)
					{
						bool flag2 = num2 > num;
						if (flag2)
						{
							return true;
						}
						bool flag3 = num2 < num;
						if (flag3)
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

		// Token: 0x060003A7 RID: 935 RVA: 0x00015F58 File Offset: 0x00014158
		[DebuggerStepThrough]
		private Task ShowMessage(string title, string message)
		{
			SettingsPage.<ShowMessage>d__15 <ShowMessage>d__ = new SettingsPage.<ShowMessage>d__15();
			<ShowMessage>d__.<>4__this = this;
			<ShowMessage>d__.title = title;
			<ShowMessage>d__.message = message;
			<ShowMessage>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ShowMessage>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <ShowMessage>d__.<>t__builder;
			<>t__builder.Start<SettingsPage.<ShowMessage>d__15>(ref <ShowMessage>d__);
			return <ShowMessage>d__.<>t__builder.Task;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00015FB0 File Offset: 0x000141B0
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			bool contentLoaded = this._contentLoaded;
			if (!contentLoaded)
			{
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
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00016080 File Offset: 0x00014280
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

		// Token: 0x040001B2 RID: 434
		private ApplicationDataContainer localSettings;

		// Token: 0x040001B3 RID: 435
		private const string CURRENT_VERSION = "5.3.1.0";

		// Token: 0x040001B4 RID: 436
		private const string VERSION_CHECK_URL = "https://8store.modyleprojects.ru/updates/version.json";

		// Token: 0x040001B5 RID: 437
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock LastUpdatedText;

		// Token: 0x040001B6 RID: 438
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button CheckUpdatesButton;

		// Token: 0x040001B7 RID: 439
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel LoadingPanel;

		// Token: 0x040001B8 RID: 440
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock StatusMessage;

		// Token: 0x040001B9 RID: 441
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ProgressRing LoadingRing;

		// Token: 0x040001BA RID: 442
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button RefreshButton;

		// Token: 0x040001BB RID: 443
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button TopAppsButton;

		// Token: 0x040001BC RID: 444
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
