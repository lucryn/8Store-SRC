using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace Win81StoreRevival
{
	// Token: 0x0200002E RID: 46
	public sealed class InstalledAppsPage : Page, IComponentConnector
	{
		// Token: 0x06000270 RID: 624 RVA: 0x0000E9EC File Offset: 0x0000CBEC
		public InstalledAppsPage()
		{
			this.InitializeComponent();
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(this.add_Loaded), new Action<EventRegistrationToken>(this.remove_Loaded), new RoutedEventHandler(this.InstalledAppsPage_Loaded));
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000EA40 File Offset: 0x0000CC40
		[DebuggerStepThrough]
		private void InstalledAppsPage_Loaded(object sender, RoutedEventArgs e)
		{
			InstalledAppsPage.<InstalledAppsPage_Loaded>d__3 <InstalledAppsPage_Loaded>d__ = new InstalledAppsPage.<InstalledAppsPage_Loaded>d__3();
			<InstalledAppsPage_Loaded>d__.<>4__this = this;
			<InstalledAppsPage_Loaded>d__.sender = sender;
			<InstalledAppsPage_Loaded>d__.e = e;
			<InstalledAppsPage_Loaded>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<InstalledAppsPage_Loaded>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <InstalledAppsPage_Loaded>d__.<>t__builder;
			<>t__builder.Start<InstalledAppsPage.<InstalledAppsPage_Loaded>d__3>(ref <InstalledAppsPage_Loaded>d__);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000EA8C File Offset: 0x0000CC8C
		[DebuggerStepThrough]
		private Task LoadDownloadedApps()
		{
			InstalledAppsPage.<LoadDownloadedApps>d__4 <LoadDownloadedApps>d__ = new InstalledAppsPage.<LoadDownloadedApps>d__4();
			<LoadDownloadedApps>d__.<>4__this = this;
			<LoadDownloadedApps>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadDownloadedApps>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadDownloadedApps>d__.<>t__builder;
			<>t__builder.Start<InstalledAppsPage.<LoadDownloadedApps>d__4>(ref <LoadDownloadedApps>d__);
			return <LoadDownloadedApps>d__.<>t__builder.Task;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000EAD4 File Offset: 0x0000CCD4
		[DebuggerStepThrough]
		private void CheckUpdatesButton_Click(object sender, RoutedEventArgs e)
		{
			InstalledAppsPage.<CheckUpdatesButton_Click>d__5 <CheckUpdatesButton_Click>d__ = new InstalledAppsPage.<CheckUpdatesButton_Click>d__5();
			<CheckUpdatesButton_Click>d__.<>4__this = this;
			<CheckUpdatesButton_Click>d__.sender = sender;
			<CheckUpdatesButton_Click>d__.e = e;
			<CheckUpdatesButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<CheckUpdatesButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <CheckUpdatesButton_Click>d__.<>t__builder;
			<>t__builder.Start<InstalledAppsPage.<CheckUpdatesButton_Click>d__5>(ref <CheckUpdatesButton_Click>d__);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000EB20 File Offset: 0x0000CD20
		[DebuggerStepThrough]
		private Task<bool> CheckForAppUpdate(DownloadedAppInfo app)
		{
			InstalledAppsPage.<CheckForAppUpdate>d__6 <CheckForAppUpdate>d__ = new InstalledAppsPage.<CheckForAppUpdate>d__6();
			<CheckForAppUpdate>d__.<>4__this = this;
			<CheckForAppUpdate>d__.app = app;
			<CheckForAppUpdate>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<CheckForAppUpdate>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<bool> <>t__builder = <CheckForAppUpdate>d__.<>t__builder;
			<>t__builder.Start<InstalledAppsPage.<CheckForAppUpdate>d__6>(ref <CheckForAppUpdate>d__);
			return <CheckForAppUpdate>d__.<>t__builder.Task;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000EB70 File Offset: 0x0000CD70
		private void InstalledApp_Click(object sender, ItemClickEventArgs e)
		{
			DownloadedAppInfo downloadedAppInfo = e.ClickedItem as DownloadedAppInfo;
			bool flag = downloadedAppInfo != null;
			if (flag)
			{
				StoreApp storeApp = new StoreApp
				{
					Id = downloadedAppInfo.AppId,
					Name = downloadedAppInfo.AppName,
					Publisher = downloadedAppInfo.Publisher,
					Version = downloadedAppInfo.Version,
					IconUrl = downloadedAppInfo.IconUrl
				};
				base.Frame.Navigate(typeof(AppDetailsPage), storeApp);
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000EBF4 File Offset: 0x0000CDF4
		private void Icon_ImageFailed(object sender, ExceptionRoutedEventArgs e)
		{
			Image image = sender as Image;
			bool flag = image != null;
			if (flag)
			{
				image.put_Source(new BitmapImage(new Uri("ms-appx:///Assets/StoreLogo.png")));
			}
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00005867 File Offset: 0x00003A67
		private void Border_Tapped_1(object sender, TappedRoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(AllAppsPage));
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000EC2C File Offset: 0x0000CE2C
		[DebuggerStepThrough]
		private Task ShowMessageDialog(string title, string message)
		{
			InstalledAppsPage.<ShowMessageDialog>d__10 <ShowMessageDialog>d__ = new InstalledAppsPage.<ShowMessageDialog>d__10();
			<ShowMessageDialog>d__.<>4__this = this;
			<ShowMessageDialog>d__.title = title;
			<ShowMessageDialog>d__.message = message;
			<ShowMessageDialog>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ShowMessageDialog>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <ShowMessageDialog>d__.<>t__builder;
			<>t__builder.Start<InstalledAppsPage.<ShowMessageDialog>d__10>(ref <ShowMessageDialog>d__);
			return <ShowMessageDialog>d__.<>t__builder.Task;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000EC84 File Offset: 0x0000CE84
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);
			Task task = this.LoadDownloadedApps();
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000ECA4 File Offset: 0x0000CEA4
		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				bool flag = base.Frame != null && base.Frame.CanGoBack;
				if (flag)
				{
					base.Frame.GoBack();
				}
				else
				{
					base.Frame.Navigate(typeof(MainPage));
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("Error in BackButton_Click: {0}", new object[]
				{
					ex.Message
				}));
				try
				{
					base.Frame.Navigate(typeof(MainPage));
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000ED58 File Offset: 0x0000CF58
		private void button_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				base.Frame.Navigate(typeof(MainPage));
			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("Error in button_Click: {0}", new object[]
				{
					ex.Message
				}));
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000EDB4 File Offset: 0x0000CFB4
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			bool contentLoaded = this._contentLoaded;
			if (!contentLoaded)
			{
				this._contentLoaded = true;
				Application.LoadComponent(this, new Uri("ms-appx:///InstalledAppsPage.xaml"), 0);
				this.UpdateProgressBar = (ProgressBar)base.FindName("UpdateProgressBar");
				this.LoadingPanel = (Grid)base.FindName("LoadingPanel");
				this.EmptyStatePanel = (Grid)base.FindName("EmptyStatePanel");
				this.button = (Button)base.FindName("button");
				this.LoadingRing = (ProgressRing)base.FindName("LoadingRing");
				this.InstalledAppsListView = (ListView)base.FindName("InstalledAppsListView");
				this.backButton = (Button)base.FindName("backButton");
				this.CheckUpdatesButton = (Button)base.FindName("CheckUpdatesButton");
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000EE9C File Offset: 0x0000D09C
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.button_Click));
				break;
			}
			case 2:
			{
				ListViewBase listViewBase = (ListViewBase)target;
				WindowsRuntimeMarshal.AddEventHandler<ItemClickEventHandler>(new Func<ItemClickEventHandler, EventRegistrationToken>(listViewBase.add_ItemClick), new Action<EventRegistrationToken>(listViewBase.remove_ItemClick), new ItemClickEventHandler(this.InstalledApp_Click));
				break;
			}
			case 3:
			{
				Image image = (Image)target;
				WindowsRuntimeMarshal.AddEventHandler<ExceptionRoutedEventHandler>(new Func<ExceptionRoutedEventHandler, EventRegistrationToken>(image.add_ImageFailed), new Action<EventRegistrationToken>(image.remove_ImageFailed), new ExceptionRoutedEventHandler(this.Icon_ImageFailed));
				break;
			}
			case 4:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.BackButton_Click));
				break;
			}
			case 5:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.CheckUpdatesButton_Click));
				break;
			}
			}
			this._contentLoaded = true;
		}

		// Token: 0x0400013C RID: 316
		private ObservableCollection<DownloadedAppInfo> downloadedApps = new ObservableCollection<DownloadedAppInfo>();

		// Token: 0x0400013D RID: 317
		private const string DOWNLOADED_APPS_FILE = "downloaded_apps.json";

		// Token: 0x0400013E RID: 318
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ProgressBar UpdateProgressBar;

		// Token: 0x0400013F RID: 319
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Grid LoadingPanel;

		// Token: 0x04000140 RID: 320
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Grid EmptyStatePanel;

		// Token: 0x04000141 RID: 321
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button button;

		// Token: 0x04000142 RID: 322
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ProgressRing LoadingRing;

		// Token: 0x04000143 RID: 323
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ListView InstalledAppsListView;

		// Token: 0x04000144 RID: 324
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button backButton;

		// Token: 0x04000145 RID: 325
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button CheckUpdatesButton;

		// Token: 0x04000146 RID: 326
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
