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
	// Token: 0x02000031 RID: 49
	public sealed class InstalledAppsPage : Page, IComponentConnector
	{
		// Token: 0x060002ED RID: 749 RVA: 0x0000F58C File Offset: 0x0000D78C
		public InstalledAppsPage()
		{
			this.InitializeComponent();
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(this.add_Loaded), new Action<EventRegistrationToken>(this.remove_Loaded), new RoutedEventHandler(this.InstalledAppsPage_Loaded));
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000F5DC File Offset: 0x0000D7DC
		private void InstalledAppsPage_Loaded(object sender, RoutedEventArgs e)
		{
			InstalledAppsPage.<InstalledAppsPage_Loaded>d__3 <InstalledAppsPage_Loaded>d__;
			<InstalledAppsPage_Loaded>d__.<>4__this = this;
			<InstalledAppsPage_Loaded>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<InstalledAppsPage_Loaded>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <InstalledAppsPage_Loaded>d__.<>t__builder;
			<>t__builder.Start<InstalledAppsPage.<InstalledAppsPage_Loaded>d__3>(ref <InstalledAppsPage_Loaded>d__);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000F618 File Offset: 0x0000D818
		private Task LoadDownloadedApps()
		{
			InstalledAppsPage.<LoadDownloadedApps>d__4 <LoadDownloadedApps>d__;
			<LoadDownloadedApps>d__.<>4__this = this;
			<LoadDownloadedApps>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadDownloadedApps>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadDownloadedApps>d__.<>t__builder;
			<>t__builder.Start<InstalledAppsPage.<LoadDownloadedApps>d__4>(ref <LoadDownloadedApps>d__);
			return <LoadDownloadedApps>d__.<>t__builder.Task;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000F660 File Offset: 0x0000D860
		private void CheckUpdatesButton_Click(object sender, RoutedEventArgs e)
		{
			InstalledAppsPage.<CheckUpdatesButton_Click>d__5 <CheckUpdatesButton_Click>d__;
			<CheckUpdatesButton_Click>d__.<>4__this = this;
			<CheckUpdatesButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<CheckUpdatesButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <CheckUpdatesButton_Click>d__.<>t__builder;
			<>t__builder.Start<InstalledAppsPage.<CheckUpdatesButton_Click>d__5>(ref <CheckUpdatesButton_Click>d__);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000F69C File Offset: 0x0000D89C
		private Task<bool> CheckForAppUpdate(DownloadedAppInfo app)
		{
			InstalledAppsPage.<CheckForAppUpdate>d__6 <CheckForAppUpdate>d__;
			<CheckForAppUpdate>d__.app = app;
			<CheckForAppUpdate>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<CheckForAppUpdate>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<bool> <>t__builder = <CheckForAppUpdate>d__.<>t__builder;
			<>t__builder.Start<InstalledAppsPage.<CheckForAppUpdate>d__6>(ref <CheckForAppUpdate>d__);
			return <CheckForAppUpdate>d__.<>t__builder.Task;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000F6E4 File Offset: 0x0000D8E4
		private void InstalledApp_Click(object sender, ItemClickEventArgs e)
		{
			DownloadedAppInfo downloadedAppInfo = e.ClickedItem as DownloadedAppInfo;
			if (downloadedAppInfo != null)
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

		// Token: 0x060002F3 RID: 755 RVA: 0x0000F75C File Offset: 0x0000D95C
		private void Icon_ImageFailed(object sender, ExceptionRoutedEventArgs e)
		{
			Image image = sender as Image;
			if (image != null)
			{
				image.put_Source(new BitmapImage(new Uri("ms-appx:///Assets/StoreLogo.png")));
			}
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00006009 File Offset: 0x00004209
		private void Border_Tapped_1(object sender, TappedRoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(AllAppsPage));
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000F788 File Offset: 0x0000D988
		private Task ShowMessageDialog(string title, string message)
		{
			InstalledAppsPage.<ShowMessageDialog>d__10 <ShowMessageDialog>d__;
			<ShowMessageDialog>d__.title = title;
			<ShowMessageDialog>d__.message = message;
			<ShowMessageDialog>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ShowMessageDialog>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <ShowMessageDialog>d__.<>t__builder;
			<>t__builder.Start<InstalledAppsPage.<ShowMessageDialog>d__10>(ref <ShowMessageDialog>d__);
			return <ShowMessageDialog>d__.<>t__builder.Task;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000F7D5 File Offset: 0x0000D9D5
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);
			this.LoadDownloadedApps();
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000F7E8 File Offset: 0x0000D9E8
		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (base.Frame != null && base.Frame.CanGoBack)
				{
					base.Frame.GoBack();
				}
				else
				{
					base.Frame.Navigate(typeof(MainPage));
				}
			}
			catch (Exception)
			{
				try
				{
					base.Frame.Navigate(typeof(MainPage));
				}
				catch
				{
				}
			}
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000F86C File Offset: 0x0000DA6C
		private void button_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				base.Frame.Navigate(typeof(MainPage));
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000F8A4 File Offset: 0x0000DAA4
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
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

		// Token: 0x060002FA RID: 762 RVA: 0x0000F984 File Offset: 0x0000DB84
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

		// Token: 0x04000142 RID: 322
		private ObservableCollection<DownloadedAppInfo> downloadedApps = new ObservableCollection<DownloadedAppInfo>();

		// Token: 0x04000143 RID: 323
		private const string DOWNLOADED_APPS_FILE = "downloaded_apps.json";

		// Token: 0x04000144 RID: 324
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ProgressBar UpdateProgressBar;

		// Token: 0x04000145 RID: 325
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Grid LoadingPanel;

		// Token: 0x04000146 RID: 326
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Grid EmptyStatePanel;

		// Token: 0x04000147 RID: 327
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button button;

		// Token: 0x04000148 RID: 328
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ProgressRing LoadingRing;

		// Token: 0x04000149 RID: 329
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ListView InstalledAppsListView;

		// Token: 0x0400014A RID: 330
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button backButton;

		// Token: 0x0400014B RID: 331
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button CheckUpdatesButton;

		// Token: 0x0400014C RID: 332
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
