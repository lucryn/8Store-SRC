using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Win81StoreRevival.Common;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace Win81StoreRevival
{
	// Token: 0x0200001E RID: 30
	public sealed class AppDetailsPage : Page, IComponentConnector
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00008511 File Offset: 0x00006711
		public ObservableCollection<Review> FeaturedReviews { get; } = new ObservableCollection<Review>();

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00008519 File Offset: 0x00006719
		public NavigationHelper NavigationHelper
		{
			get
			{
				return this.navigationHelper;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00008521 File Offset: 0x00006721
		public ObservableDictionary DefaultViewModel
		{
			get
			{
				return this.defaultViewModel;
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000852C File Offset: 0x0000672C
		public AppDetailsPage()
		{
			try
			{
				this.InitializeComponent();
				this.navigationHelper = new NavigationHelper(this);
				this.Trap.Focus(3);
				this.UpdateTitleNB();
				this.httpClient = new HttpClient();
				this.httpClient.Timeout = TimeSpan.FromSeconds(30.0);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000085D0 File Offset: 0x000067D0
		public void UpdateTitleNB()
		{
			object obj = ApplicationData.Current.LocalSettings.Values["UseClassicTitleNavbar"];
			if (obj != null && (bool)obj)
			{
				ResourceLoader forCurrentView = ResourceLoader.GetForCurrentView();
				this.nbLogoText.put_Text(forCurrentView.GetString("HomeWord"));
				return;
			}
			this.nbLogoText.put_Text("8Store");
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00008634 File Offset: 0x00006834
		private void ShowLoadingOverlay()
		{
			try
			{
				Grid grid = base.FindName("ScreenshotLO") as Grid;
				ProgressRing progressRing = base.FindName("ScreenshoRingi") as ProgressRing;
				if (grid != null)
				{
					grid.put_Visibility(0);
					if (progressRing != null)
					{
						progressRing.put_IsActive(true);
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000868C File Offset: 0x0000688C
		private void HideLoadingOverlay()
		{
			try
			{
				Grid grid = base.FindName("ScreenshotLO") as Grid;
				ProgressRing progressRing = base.FindName("ScreenshoRingi") as ProgressRing;
				if (grid != null)
				{
					if (progressRing != null)
					{
						progressRing.put_IsActive(false);
					}
					grid.put_Visibility(1);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x000086E4 File Offset: 0x000068E4
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			AppDetailsPage.<OnNavigatedTo>d__25 <OnNavigatedTo>d__;
			<OnNavigatedTo>d__.<>4__this = this;
			<OnNavigatedTo>d__.e = e;
			<OnNavigatedTo>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<OnNavigatedTo>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <OnNavigatedTo>d__.<>t__builder;
			<>t__builder.Start<AppDetailsPage.<OnNavigatedTo>d__25>(ref <OnNavigatedTo>d__);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00008728 File Offset: 0x00006928
		private Task LoadAppById(string appId)
		{
			AppDetailsPage.<LoadAppById>d__26 <LoadAppById>d__;
			<LoadAppById>d__.<>4__this = this;
			<LoadAppById>d__.appId = appId;
			<LoadAppById>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadAppById>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadAppById>d__.<>t__builder;
			<>t__builder.Start<AppDetailsPage.<LoadAppById>d__26>(ref <LoadAppById>d__);
			return <LoadAppById>d__.<>t__builder.Task;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00008778 File Offset: 0x00006978
		private void PublisherText_PointerPressed(object sender, PointerRoutedEventArgs e)
		{
			try
			{
				if (this.currentApp != null && !string.IsNullOrEmpty(this.currentApp.Publisher))
				{
					TextBlock textBlock = sender as TextBlock;
					if (textBlock != null)
					{
						try
						{
							textBlock.put_Foreground(new SolidColorBrush(Color.FromArgb(byte.MaxValue, 0, 170, 79)));
						}
						catch (Exception)
						{
						}
					}
					if (base.Frame != null)
					{
						base.Frame.Navigate(typeof(PublisherAppsPage), this.currentApp.Publisher);
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00008818 File Offset: 0x00006A18
		private void PublisherText_PointerEntered(object sender, PointerRoutedEventArgs e)
		{
			try
			{
				if (this.currentApp != null && !string.IsNullOrEmpty(this.currentApp.Publisher))
				{
					TextBlock textBlock = sender as TextBlock;
					if (textBlock != null)
					{
						textBlock.put_Foreground(new SolidColorBrush(Color.FromArgb(byte.MaxValue, 0, 170, 79)));
					}
					Window window = Window.Current;
					if (((window != null) ? window.CoreWindow : null) != null)
					{
						Window.Current.CoreWindow.put_PointerCursor(new CoreCursor(3, 0U));
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000088A4 File Offset: 0x00006AA4
		private void PublisherText_PointerExited(object sender, PointerRoutedEventArgs e)
		{
			try
			{
				TextBlock textBlock = sender as TextBlock;
				if (textBlock != null)
				{
					textBlock.put_Foreground(new SolidColorBrush(Color.FromArgb(byte.MaxValue, 102, 102, 102)));
				}
				Window window = Window.Current;
				if (((window != null) ? window.CoreWindow : null) != null)
				{
					Window.Current.CoreWindow.put_PointerCursor(new CoreCursor(0, 0U));
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00008914 File Offset: 0x00006B14
		private Task LoadAllApps()
		{
			AppDetailsPage.<LoadAllApps>d__30 <LoadAllApps>d__;
			<LoadAllApps>d__.<>4__this = this;
			<LoadAllApps>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadAllApps>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadAllApps>d__.<>t__builder;
			<>t__builder.Start<AppDetailsPage.<LoadAllApps>d__30>(ref <LoadAllApps>d__);
			return <LoadAllApps>d__.<>t__builder.Task;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000895C File Offset: 0x00006B5C
		private Task LoadSuggestedApps()
		{
			AppDetailsPage.<LoadSuggestedApps>d__31 <LoadSuggestedApps>d__;
			<LoadSuggestedApps>d__.<>4__this = this;
			<LoadSuggestedApps>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadSuggestedApps>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadSuggestedApps>d__.<>t__builder;
			<>t__builder.Start<AppDetailsPage.<LoadSuggestedApps>d__31>(ref <LoadSuggestedApps>d__);
			return <LoadSuggestedApps>d__.<>t__builder.Task;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x000089A4 File Offset: 0x00006BA4
		private List<StoreApp> BuildRelatedApps(int count)
		{
			if (this.allApps == null || this.currentApp == null)
			{
				return new List<StoreApp>();
			}
			return Enumerable.ToList<StoreApp>(Enumerable.Select(Enumerable.Take(Enumerable.ThenBy(Enumerable.OrderByDescending(Enumerable.Select(Enumerable.Where<StoreApp>(this.allApps, (StoreApp app) => app != null && !string.IsNullOrWhiteSpace(app.Id) && app.Id != this.currentApp.Id && !app.Id.StartsWith("settings")), (StoreApp app) => new
			{
				App = app,
				Score = this.GetRelatedAppScore(app)
			}), x => x.Score), x => x.App.Name), count), x => x.App));
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00008A68 File Offset: 0x00006C68
		private int GetRelatedAppScore(StoreApp app)
		{
			int num = 0;
			if (app == null || this.currentApp == null)
			{
				return num;
			}
			if (!string.IsNullOrWhiteSpace(app.Publisher) && !string.IsNullOrWhiteSpace(this.currentApp.Publisher) && app.Publisher.Equals(this.currentApp.Publisher, 5))
			{
				num += 4;
			}
			if (!string.IsNullOrWhiteSpace(app.Category) && !string.IsNullOrWhiteSpace(this.currentApp.Category) && app.Category.Equals(this.currentApp.Category, 5))
			{
				num += 3;
			}
			if (!string.IsNullOrWhiteSpace(app.Type) && !string.IsNullOrWhiteSpace(this.currentApp.Type) && app.Type.Equals(this.currentApp.Type, 5))
			{
				num += 2;
			}
			if (app.Featured)
			{
				num++;
			}
			return num;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00008B48 File Offset: 0x00006D48
		private void FindAndSetRelatedAppsSource()
		{
			try
			{
				GridView gridView = this.FindVisualChild<GridView>(this.MainHub, "RelatedAppsGridView");
				if (gridView != null)
				{
					this.relatedAppsGridView = gridView;
					gridView.put_ItemsSource(this.suggestedApps);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00008B94 File Offset: 0x00006D94
		private void RelatedAppsGridView_Loaded(object sender, RoutedEventArgs e)
		{
			this.relatedAppsGridView = (sender as GridView);
			if (this.relatedAppsGridView != null)
			{
				this.relatedAppsGridView.put_ItemsSource(this.suggestedApps);
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00008BBC File Offset: 0x00006DBC
		private T FindVisualChild<T>(DependencyObject parent, string name) where T : FrameworkElement
		{
			if (parent == null)
			{
				return default(T);
			}
			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
			{
				DependencyObject child = VisualTreeHelper.GetChild(parent, i);
				if (child is T && ((T)((object)child)).Name == name)
				{
					return (T)((object)child);
				}
				T t = this.FindVisualChild<T>(child, name);
				if (t != null)
				{
					return t;
				}
			}
			return default(T);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00008C34 File Offset: 0x00006E34
		protected override void OnNavigatedFrom(NavigationEventArgs e)
		{
			base.OnNavigatedFrom(e);
			if (this.navigationHelper != null)
			{
				this.navigationHelper.OnNavigatedFrom(e);
			}
			DownloadManager.DownloadProgress -= new EventHandler<DownloadManager.DownloadProgressEventArgs>(this.OnDownloadProgress);
			DownloadManager.DownloadCompleted -= new EventHandler<DownloadManager.DownloadCompletedEventArgs>(this.OnDownloadCompleted);
			if (this.httpClient != null)
			{
				this.httpClient.Dispose();
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00008C94 File Offset: 0x00006E94
		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (base.Frame != null && base.Frame.CanGoBack)
				{
					base.Frame.GoBack();
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00004871 File Offset: 0x00002A71
		private void StoreLogo_Click(object sender, RoutedEventArgs e)
		{
			if (base.Frame != null)
			{
				base.Frame.Navigate(typeof(MainPage));
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00008CD8 File Offset: 0x00006ED8
		private void nbSearch_QuerySubmitted(object sender, SearchBoxQuerySubmittedEventArgs e)
		{
			string text = (e != null && e.QueryText != null) ? e.QueryText.Trim() : null;
			if (!string.IsNullOrEmpty(text))
			{
				base.Frame.Navigate(typeof(SearchResultsPage), text);
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00008D20 File Offset: 0x00006F20
		private void PlaySound()
		{
			try
			{
				SoundManager.PlayDownloadSound();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00008D48 File Offset: 0x00006F48
		private void PlaySoundLoaded()
		{
			try
			{
				SoundManager.PlayUpdateSound();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00008D70 File Offset: 0x00006F70
		private void UpdateActionButtonLabel(string text, bool enabled)
		{
			Button button = base.FindName("DownloadButton") as Button;
			if (button == null)
			{
				return;
			}
			button.put_Content(text);
			button.put_IsEnabled(enabled);
			button.put_Opacity(enabled ? 1.0 : 0.85);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00008DBD File Offset: 0x00006FBD
		private string GetActionButtonText()
		{
			if (this.autoInstallRequested)
			{
				return "Install";
			}
			if (this.currentApp != null && !string.IsNullOrWhiteSpace(this.currentApp.PackageFileName))
			{
				return "Install";
			}
			return "Download";
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00008DF4 File Offset: 0x00006FF4
		private void DownloadButton_Click(object sender, RoutedEventArgs e)
		{
			AppDetailsPage.<DownloadButton_Click>d__47 <DownloadButton_Click>d__;
			<DownloadButton_Click>d__.<>4__this = this;
			<DownloadButton_Click>d__.sender = sender;
			<DownloadButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<DownloadButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <DownloadButton_Click>d__.<>t__builder;
			<>t__builder.Start<AppDetailsPage.<DownloadButton_Click>d__47>(ref <DownloadButton_Click>d__);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00008E38 File Offset: 0x00007038
		private Task StartDownloadAsync(Button downloadButton)
		{
			AppDetailsPage.<StartDownloadAsync>d__48 <StartDownloadAsync>d__;
			<StartDownloadAsync>d__.<>4__this = this;
			<StartDownloadAsync>d__.downloadButton = downloadButton;
			<StartDownloadAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<StartDownloadAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <StartDownloadAsync>d__.<>t__builder;
			<>t__builder.Start<AppDetailsPage.<StartDownloadAsync>d__48>(ref <StartDownloadAsync>d__);
			return <StartDownloadAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00008E88 File Offset: 0x00007088
		private Task<StoreApp> ResolveAppByIdAsync(string appId)
		{
			AppDetailsPage.<ResolveAppByIdAsync>d__49 <ResolveAppByIdAsync>d__;
			<ResolveAppByIdAsync>d__.appId = appId;
			<ResolveAppByIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<StoreApp>.Create();
			<ResolveAppByIdAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<StoreApp> <>t__builder = <ResolveAppByIdAsync>d__.<>t__builder;
			<>t__builder.Start<AppDetailsPage.<ResolveAppByIdAsync>d__49>(ref <ResolveAppByIdAsync>d__);
			return <ResolveAppByIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00008ED0 File Offset: 0x000070D0
		private string GetDownloadUrl()
		{
			if (this.currentApp == null)
			{
				return null;
			}
			string text = this.currentApp.DownloadUrl;
			if (!string.IsNullOrEmpty(text) && !text.StartsWith("http", 5))
			{
				text = "https://8store.modyleprojects.ru/data/downloads/" + text;
			}
			if (string.IsNullOrWhiteSpace(text) && !string.IsNullOrWhiteSpace(this.currentApp.PackageFileName))
			{
				text = "https://8store.modyleprojects.ru/files/" + this.currentApp.Id + "/" + this.currentApp.PackageFileName;
			}
			return text;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00008F58 File Offset: 0x00007158
		private void OnDownloadProgress(object sender, DownloadManager.DownloadProgressEventArgs e)
		{
			if (e.DownloadItem != null)
			{
				string appName = e.DownloadItem.AppName;
				StoreApp storeApp = this.currentApp;
				if (appName == ((storeApp != null) ? storeApp.Name : null) && this.isDownloadInProgress)
				{
					base.Dispatcher.RunAsync(0, delegate()
					{
						this.UpdateProgressFromDownloadItem(e.DownloadItem);
					});
				}
			}
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00008FD0 File Offset: 0x000071D0
		private void OnDownloadCompleted(object sender, DownloadManager.DownloadCompletedEventArgs e)
		{
			AppDetailsPage.<>c__DisplayClass52_0 CS$<>8__locals1 = new AppDetailsPage.<>c__DisplayClass52_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.e = e;
			if (CS$<>8__locals1.e.DownloadItem != null)
			{
				string appName = CS$<>8__locals1.e.DownloadItem.AppName;
				StoreApp storeApp = this.currentApp;
				if (appName == ((storeApp != null) ? storeApp.Name : null))
				{
					base.Dispatcher.RunAsync(0, delegate()
					{
						AppDetailsPage.<>c__DisplayClass52_0.<<OnDownloadCompleted>b__0>d <<OnDownloadCompleted>b__0>d;
						<<OnDownloadCompleted>b__0>d.<>4__this = CS$<>8__locals1;
						<<OnDownloadCompleted>b__0>d.<>t__builder = AsyncVoidMethodBuilder.Create();
						<<OnDownloadCompleted>b__0>d.<>1__state = -1;
						AsyncVoidMethodBuilder <>t__builder = <<OnDownloadCompleted>b__0>d.<>t__builder;
						<>t__builder.Start<AppDetailsPage.<>c__DisplayClass52_0.<<OnDownloadCompleted>b__0>d>(ref <<OnDownloadCompleted>b__0>d);
					});
				}
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00009040 File Offset: 0x00007240
		private void UpdateProgressFromDownloadItem(DownloadItem item)
		{
			if (item == null || this.TopProgressBar == null)
			{
				return;
			}
			base.Dispatcher.RunAsync(0, delegate()
			{
				this.TopProgressBar.put_Visibility(0);
				this.TopProgressBar.put_IsIndeterminate(false);
				this.TopProgressBar.put_Maximum(100.0);
				this.TopProgressBar.put_Value(item.Progress);
				if (item.Progress >= 100.0)
				{
					this.TopProgressBar.put_Foreground(new SolidColorBrush(Color.FromArgb(byte.MaxValue, 0, 170, 79)));
				}
			});
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000908C File Offset: 0x0000728C
		private void ShowDownloadProgress(string fileName, double progress)
		{
			try
			{
				if (this.TopProgressBar != null)
				{
					this.TopProgressBar.put_Visibility(0);
					if (progress >= 0.0)
					{
						this.TopProgressBar.put_Value(progress);
						this.TopProgressBar.put_IsIndeterminate(false);
					}
					else
					{
						this.TopProgressBar.put_IsIndeterminate(true);
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000090F4 File Offset: 0x000072F4
		private void HideDownloadProgress()
		{
			try
			{
				if (this.TopProgressBar != null)
				{
					this.TopProgressBar.put_Visibility(1);
					this.TopProgressBar.put_Value(0.0);
					this.TopProgressBar.put_Foreground(new SolidColorBrush(Color.FromArgb(byte.MaxValue, 0, 170, 79)));
				}
				DownloadManager.DownloadProgress -= new EventHandler<DownloadManager.DownloadProgressEventArgs>(this.OnDownloadProgress);
				DownloadManager.DownloadCompleted -= new EventHandler<DownloadManager.DownloadCompletedEventArgs>(this.OnDownloadCompleted);
				this.isDownloadInProgress = false;
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000918C File Offset: 0x0000738C
		private void ReportButton_Click(object sender, RoutedEventArgs e)
		{
			AppDetailsPage.<ReportButton_Click>d__56 <ReportButton_Click>d__;
			<ReportButton_Click>d__.<>4__this = this;
			<ReportButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<ReportButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <ReportButton_Click>d__.<>t__builder;
			<>t__builder.Start<AppDetailsPage.<ReportButton_Click>d__56>(ref <ReportButton_Click>d__);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000091C8 File Offset: 0x000073C8
		private void RelatedAppsListView_ItemClick(object sender, ItemClickEventArgs e)
		{
			StoreApp storeApp = e.ClickedItem as StoreApp;
			if (storeApp != null)
			{
				base.Frame.Navigate(typeof(AppDetailsPage), storeApp);
			}
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x000091FC File Offset: 0x000073FC
		private void ScreenshotList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			GridView gridView = sender as GridView;
			if (gridView == null)
			{
				return;
			}
			string text = gridView.SelectedItem as string;
			if (string.IsNullOrWhiteSpace(text) && this.currentApp != null && this.currentApp.Screenshots != null && gridView.SelectedIndex >= 0 && gridView.SelectedIndex < this.currentApp.Screenshots.Count)
			{
				text = this.currentApp.Screenshots[gridView.SelectedIndex];
			}
			this.UpdateMainScreenshot(text);
			FlipView flipView = this.FindVisualChild<FlipView>(this.MainHub, "ScreenshotViewerList");
			if (flipView != null && gridView.SelectedIndex >= 0 && flipView.SelectedIndex != gridView.SelectedIndex)
			{
				flipView.put_SelectedIndex(gridView.SelectedIndex);
			}
		}

		// Token: 0x060001CA RID: 458 RVA: 0x000092B4 File Offset: 0x000074B4
		private void ScreenshotList_ItemClick(object sender, ItemClickEventArgs e)
		{
			GridView gridView = sender as GridView;
			if (gridView == null)
			{
				return;
			}
			if (!object.Equals(gridView.SelectedItem, e.ClickedItem))
			{
				gridView.put_SelectedItem(e.ClickedItem);
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000092EB File Offset: 0x000074EB
		private void SeeAllReviews_Click(object sender, RoutedEventArgs e)
		{
			if (this.currentApp == null)
			{
				return;
			}
			base.Frame.Navigate(typeof(AppReviewsPage), this.currentApp);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00009314 File Offset: 0x00007514
		private void FullscreenButton_Click(object sender, RoutedEventArgs e)
		{
			if (this.currentApp == null)
			{
				return;
			}
			List<string> screenshots = this.currentApp.Screenshots;
			if (screenshots == null || screenshots.Count == 0)
			{
				return;
			}
			int selectedIndex = 0;
			GridView gridView = this.FindVisualChild<GridView>(this.MainHub, "ScreenshotList");
			if (gridView != null && gridView.SelectedIndex >= 0)
			{
				selectedIndex = gridView.SelectedIndex;
			}
			FullScreenViewPage.FullscreenViewPayload fullscreenViewPayload = new FullScreenViewPage.FullscreenViewPayload
			{
				Screenshots = screenshots,
				SelectedIndex = selectedIndex
			};
			base.Frame.Navigate(typeof(FullScreenViewPage), fullscreenViewPayload);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00009394 File Offset: 0x00007594
		private void UpdateScreenshotPreviewVisibility(bool hasScreenshots)
		{
			Image image = this.FindVisualChild<Image>(this.MainHub, "ScreenshotPlaceholderImage");
			FlipView flipView = this.FindVisualChild<FlipView>(this.MainHub, "ScreenshotViewerList");
			if (image != null)
			{
				image.put_Visibility(hasScreenshots ? 1 : 0);
			}
			if (flipView != null)
			{
				flipView.put_Visibility(hasScreenshots ? 0 : 1);
			}
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000093E8 File Offset: 0x000075E8
		private void ScreenshotViewerList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			FlipView flipView = sender as FlipView;
			if (flipView == null)
			{
				return;
			}
			GridView gridView = this.FindVisualChild<GridView>(this.MainHub, "ScreenshotList");
			if (gridView != null && flipView.SelectedIndex >= 0 && gridView.SelectedIndex != flipView.SelectedIndex)
			{
				gridView.put_SelectedIndex(flipView.SelectedIndex);
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00006009 File Offset: 0x00004209
		private void Border_Tapped_1(object sender, TappedRoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(AllAppsPage));
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00009438 File Offset: 0x00007638
		private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
		{
			Image image = sender as Image;
			if (image == null)
			{
				return;
			}
			try
			{
				image.put_Source(new BitmapImage(new Uri("ms-appx:///Assets/icon-error.png")));
			}
			catch
			{
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000947C File Offset: 0x0000767C
		private Task ShowMessageDialog(string title, string message)
		{
			AppDetailsPage.<ShowMessageDialog>d__66 <ShowMessageDialog>d__;
			<ShowMessageDialog>d__.title = title;
			<ShowMessageDialog>d__.message = message;
			<ShowMessageDialog>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ShowMessageDialog>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <ShowMessageDialog>d__.<>t__builder;
			<>t__builder.Start<AppDetailsPage.<ShowMessageDialog>d__66>(ref <ShowMessageDialog>d__);
			return <ShowMessageDialog>d__.<>t__builder.Task;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x000094CC File Offset: 0x000076CC
		private Task EnsureFirstScreenshotSelected()
		{
			AppDetailsPage.<EnsureFirstScreenshotSelected>d__67 <EnsureFirstScreenshotSelected>d__;
			<EnsureFirstScreenshotSelected>d__.<>4__this = this;
			<EnsureFirstScreenshotSelected>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<EnsureFirstScreenshotSelected>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <EnsureFirstScreenshotSelected>d__.<>t__builder;
			<>t__builder.Start<AppDetailsPage.<EnsureFirstScreenshotSelected>d__67>(ref <EnsureFirstScreenshotSelected>d__);
			return <EnsureFirstScreenshotSelected>d__.<>t__builder.Task;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00009514 File Offset: 0x00007714
		private void UpdateMainScreenshot(string screenshotUri)
		{
			FlipView flipView = this.FindVisualChild<FlipView>(this.MainHub, "ScreenshotViewerList");
			if (flipView == null)
			{
				return;
			}
			if (string.IsNullOrWhiteSpace(screenshotUri))
			{
				flipView.put_SelectedIndex(-1);
				return;
			}
			try
			{
				StoreApp storeApp = this.currentApp;
				List<string> list = (storeApp != null) ? storeApp.Screenshots : null;
				if (list == null)
				{
					this.UpdateScreenshotPreviewVisibility(false);
					flipView.put_SelectedIndex(-1);
				}
				else
				{
					int num = list.FindIndex((string s) => string.Equals(s, screenshotUri, 5));
					if (num >= 0 && flipView.SelectedIndex != num)
					{
						this.UpdateScreenshotPreviewVisibility(true);
						flipView.put_SelectedIndex(num);
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x000095C4 File Offset: 0x000077C4
		private Task LoadReviewStatsForCurrentApp()
		{
			AppDetailsPage.<LoadReviewStatsForCurrentApp>d__69 <LoadReviewStatsForCurrentApp>d__;
			<LoadReviewStatsForCurrentApp>d__.<>4__this = this;
			<LoadReviewStatsForCurrentApp>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadReviewStatsForCurrentApp>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadReviewStatsForCurrentApp>d__.<>t__builder;
			<>t__builder.Start<AppDetailsPage.<LoadReviewStatsForCurrentApp>d__69>(ref <LoadReviewStatsForCurrentApp>d__);
			return <LoadReviewStatsForCurrentApp>d__.<>t__builder.Task;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000960C File Offset: 0x0000780C
		private Task LoadApproximateSizeForCurrentApp()
		{
			AppDetailsPage.<LoadApproximateSizeForCurrentApp>d__70 <LoadApproximateSizeForCurrentApp>d__;
			<LoadApproximateSizeForCurrentApp>d__.<>4__this = this;
			<LoadApproximateSizeForCurrentApp>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadApproximateSizeForCurrentApp>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadApproximateSizeForCurrentApp>d__.<>t__builder;
			<>t__builder.Start<AppDetailsPage.<LoadApproximateSizeForCurrentApp>d__70>(ref <LoadApproximateSizeForCurrentApp>d__);
			return <LoadApproximateSizeForCurrentApp>d__.<>t__builder.Task;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00009654 File Offset: 0x00007854
		private static string FormatByteSize(long bytes)
		{
			string[] array = new string[]
			{
				"B",
				"KB",
				"MB",
				"GB",
				"TB"
			};
			double num = (double)bytes;
			int num2 = 0;
			while (num >= 1024.0 && num2 < array.Length - 1)
			{
				num2++;
				num /= 1024.0;
			}
			return string.Format("{0:0.##} {1}", new object[]
			{
				num,
				array[num2]
			});
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x000096DC File Offset: 0x000078DC
		private void UpdateFeaturedReviews()
		{
			this.FeaturedReviews.Clear();
			if (this.appReviews == null || this.appReviews.Count == 0)
			{
				return;
			}
			List<Review> list = Enumerable.ToList<Review>(Enumerable.Take<Review>(Enumerable.ThenByDescending<Review, DateTime>(Enumerable.OrderByDescending<Review, int>(this.appReviews, (Review r) => r.Rating), (Review r) => r.CreatedAt), Math.Min(8, this.appReviews.Count)));
			if (list.Count == 0)
			{
				return;
			}
			Random random = new Random();
			int num = Math.Min(4, list.Count);
			for (int i = 0; i < num; i++)
			{
				int num2 = random.Next(list.Count);
				this.FeaturedReviews.Add(list[num2]);
				list.RemoveAt(num2);
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x000097C8 File Offset: 0x000079C8
		private void DeleteReview_Click(object sender, RoutedEventArgs e)
		{
			AppDetailsPage.<DeleteReview_Click>d__73 <DeleteReview_Click>d__;
			<DeleteReview_Click>d__.<>4__this = this;
			<DeleteReview_Click>d__.sender = sender;
			<DeleteReview_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<DeleteReview_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <DeleteReview_Click>d__.<>t__builder;
			<>t__builder.Start<AppDetailsPage.<DeleteReview_Click>d__73>(ref <DeleteReview_Click>d__);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000980C File Offset: 0x00007A0C
		private void ToggleReviewExpand_Click(object sender, RoutedEventArgs e)
		{
			HyperlinkButton hyperlinkButton = sender as HyperlinkButton;
			Review review = ((hyperlinkButton != null) ? hyperlinkButton.Tag : null) as Review;
			if (review != null)
			{
				review.IsExpanded = !review.IsExpanded;
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00009844 File Offset: 0x00007A44
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Application.LoadComponent(this, new Uri("ms-appx:///AppDetailsPage.xaml"), 0);
			this.pageRoot = (Page)base.FindName("pageRoot");
			this.Trap = (Button)base.FindName("Trap");
			this.TopProgressBar = (ProgressBar)base.FindName("TopProgressBar");
			this.MainHub = (Hub)base.FindName("MainHub");
			this.backButton = (Button)base.FindName("backButton");
			this.nbSearch = (SearchBox)base.FindName("nbSearch");
			this.nbLogoText = (TextBlock)base.FindName("nbLogoText");
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000990C File Offset: 0x00007B0C
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
			{
				Image image = (Image)target;
				WindowsRuntimeMarshal.AddEventHandler<ExceptionRoutedEventHandler>(new Func<ExceptionRoutedEventHandler, EventRegistrationToken>(image.add_ImageFailed), new Action<EventRegistrationToken>(image.remove_ImageFailed), new ExceptionRoutedEventHandler(this.Image_ImageFailed));
				break;
			}
			case 2:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.BackButton_Click));
				break;
			}
			case 3:
			{
				ListViewBase listViewBase = (ListViewBase)target;
				WindowsRuntimeMarshal.AddEventHandler<ItemClickEventHandler>(new Func<ItemClickEventHandler, EventRegistrationToken>(listViewBase.add_ItemClick), new Action<EventRegistrationToken>(listViewBase.remove_ItemClick), new ItemClickEventHandler(this.RelatedAppsListView_ItemClick));
				FrameworkElement frameworkElement = (FrameworkElement)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(frameworkElement.add_Loaded), new Action<EventRegistrationToken>(frameworkElement.remove_Loaded), new RoutedEventHandler(this.RelatedAppsGridView_Loaded));
				break;
			}
			case 4:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.DeleteReview_Click));
				break;
			}
			case 5:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.ReportButton_Click));
				break;
			}
			case 6:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.ToggleReviewExpand_Click));
				break;
			}
			case 7:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.SeeAllReviews_Click));
				break;
			}
			case 8:
			{
				ListViewBase listViewBase = (ListViewBase)target;
				WindowsRuntimeMarshal.AddEventHandler<ItemClickEventHandler>(new Func<ItemClickEventHandler, EventRegistrationToken>(listViewBase.add_ItemClick), new Action<EventRegistrationToken>(listViewBase.remove_ItemClick), new ItemClickEventHandler(this.ScreenshotList_ItemClick));
				Selector selector = (Selector)target;
				WindowsRuntimeMarshal.AddEventHandler<SelectionChangedEventHandler>(new Func<SelectionChangedEventHandler, EventRegistrationToken>(selector.add_SelectionChanged), new Action<EventRegistrationToken>(selector.remove_SelectionChanged), new SelectionChangedEventHandler(this.ScreenshotList_SelectionChanged));
				break;
			}
			case 9:
			{
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<PointerEventHandler>(new Func<PointerEventHandler, EventRegistrationToken>(uielement.add_PointerPressed), new Action<EventRegistrationToken>(uielement.remove_PointerPressed), new PointerEventHandler(this.PublisherText_PointerPressed));
				break;
			}
			case 10:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.FullscreenButton_Click));
				break;
			}
			case 11:
			{
				Selector selector = (Selector)target;
				WindowsRuntimeMarshal.AddEventHandler<SelectionChangedEventHandler>(new Func<SelectionChangedEventHandler, EventRegistrationToken>(selector.add_SelectionChanged), new Action<EventRegistrationToken>(selector.remove_SelectionChanged), new SelectionChangedEventHandler(this.ScreenshotViewerList_SelectionChanged));
				break;
			}
			case 12:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.DownloadButton_Click));
				break;
			}
			case 13:
			{
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uielement.add_Tapped), new Action<EventRegistrationToken>(uielement.remove_Tapped), new TappedEventHandler(this.StoreLogo_Click));
				break;
			}
			case 14:
			{
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uielement.add_Tapped), new Action<EventRegistrationToken>(uielement.remove_Tapped), new TappedEventHandler(this.Border_Tapped_1));
				break;
			}
			case 15:
			{
				SearchBox searchBox = (SearchBox)target;
				WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<SearchBox, SearchBoxQuerySubmittedEventArgs>>(new Func<TypedEventHandler<SearchBox, SearchBoxQuerySubmittedEventArgs>, EventRegistrationToken>(searchBox.add_QuerySubmitted), new Action<EventRegistrationToken>(searchBox.remove_QuerySubmitted), new TypedEventHandler<SearchBox, SearchBoxQuerySubmittedEventArgs>(this.nbSearch_QuerySubmitted));
				break;
			}
			}
			this._contentLoaded = true;
		}

		// Token: 0x040000A7 RID: 167
		private StoreApp currentApp;

		// Token: 0x040000A8 RID: 168
		private HttpClient httpClient;

		// Token: 0x040000A9 RID: 169
		private bool isDownloading;

		// Token: 0x040000AA RID: 170
		private bool isLoadingFromDeepLink;

		// Token: 0x040000AB RID: 171
		private List<StoreApp> allApps;

		// Token: 0x040000AC RID: 172
		private List<StoreApp> suggestedApps = new List<StoreApp>();

		// Token: 0x040000AD RID: 173
		private GridView relatedAppsGridView;

		// Token: 0x040000AE RID: 174
		private List<Review> appReviews = new List<Review>();

		// Token: 0x040000AF RID: 175
		private UserAccount currentUser;

		// Token: 0x040000B0 RID: 176
		private bool showReviews = true;

		// Token: 0x040000B1 RID: 177
		private bool autoInstallRequested;

		// Token: 0x040000B2 RID: 178
		private bool autoInstallStarted;

		// Token: 0x040000B3 RID: 179
		private NavigationHelper navigationHelper;

		// Token: 0x040000B4 RID: 180
		private ObservableDictionary defaultViewModel = new ObservableDictionary();

		// Token: 0x040000B6 RID: 182
		private DownloadItem currentDownloadItem;

		// Token: 0x040000B7 RID: 183
		private bool isDownloadInProgress;

		// Token: 0x040000B8 RID: 184
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Page pageRoot;

		// Token: 0x040000B9 RID: 185
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button Trap;

		// Token: 0x040000BA RID: 186
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ProgressBar TopProgressBar;

		// Token: 0x040000BB RID: 187
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Hub MainHub;

		// Token: 0x040000BC RID: 188
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button backButton;

		// Token: 0x040000BD RID: 189
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private SearchBox nbSearch;

		// Token: 0x040000BE RID: 190
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock nbLogoText;

		// Token: 0x040000BF RID: 191
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
