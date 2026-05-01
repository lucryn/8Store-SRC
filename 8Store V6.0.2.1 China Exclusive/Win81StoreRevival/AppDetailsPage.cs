using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Win81StoreRevival.Common;
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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

namespace Win81StoreRevival
{
	// Token: 0x0200001B RID: 27
	public sealed class AppDetailsPage : Page, IComponentConnector
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00007840 File Offset: 0x00005A40
		public NavigationHelper NavigationHelper
		{
			get
			{
				return this.navigationHelper;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00007858 File Offset: 0x00005A58
		public ObservableDictionary DefaultViewModel
		{
			get
			{
				return this.defaultViewModel;
			}
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00007870 File Offset: 0x00005A70
		public AppDetailsPage()
		{
			try
			{
				Debug.WriteLine("AppDetailsPage constructor called");
				this.InitializeComponent();
				Debug.WriteLine("InitializeComponent completed");
				this.navigationHelper = new NavigationHelper(this);
				this.httpClient = new HttpClient();
				this.httpClient.Timeout = TimeSpan.FromSeconds(30.0);
				Debug.WriteLine("HttpClient created");
				this.CreateStatusUI();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("Error in constructor: {0}", new object[]
				{
					ex.Message
				}));
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00007964 File Offset: 0x00005B64
		private void CreateStatusUI()
		{
			try
			{
				Button button = base.FindName("DownloadButton") as Button;
				Grid grid = base.Content as Grid;
				bool flag = grid != null && button != null;
				if (flag)
				{
					GeneralTransform generalTransform = button.TransformToVisual(grid);
					Point point = generalTransform.TransformPoint(new Point(0.0, 0.0));
					Border border = new Border();
					SolidColorBrush solidColorBrush = new SolidColorBrush(Colors.Black);
					solidColorBrush.put_Opacity(0.7);
					border.put_Background(solidColorBrush);
					border.put_CornerRadius(new CornerRadius(5.0));
					border.put_Height(40.0);
					border.put_Width(180.0);
					border.put_VerticalAlignment(0);
					border.put_HorizontalAlignment(0);
					border.put_Margin(new Thickness(point.X + 120.0, point.Y, 0.0, 0.0));
					border.put_Visibility(1);
					this.downloadingBorder = border;
					this.downloadingGrid = new Grid();
					StackPanel stackPanel = new StackPanel();
					stackPanel.put_Orientation(0);
					stackPanel.put_HorizontalAlignment(1);
					stackPanel.put_VerticalAlignment(1);
					StackPanel stackPanel2 = stackPanel;
					ProgressRing progressRing = new ProgressRing();
					progressRing.put_IsActive(true);
					progressRing.put_Width(20.0);
					progressRing.put_Height(20.0);
					progressRing.put_Foreground(new SolidColorBrush(Colors.White));
					progressRing.put_Margin(new Thickness(0.0, 5.0, 0.0, 2.0));
					ProgressRing progressRing2 = progressRing;
					TextBlock textBlock = new TextBlock();
					textBlock.put_Text("0%");
					textBlock.put_Foreground(new SolidColorBrush(Colors.White));
					textBlock.put_FontSize(12.0);
					textBlock.put_HorizontalAlignment(1);
					textBlock.put_Margin(new Thickness(0.0, 0.0, 0.0, 5.0));
					TextBlock textBlock2 = textBlock;
					stackPanel2.Children.Add(progressRing2);
					stackPanel2.Children.Add(textBlock2);
					this.downloadingGrid.Children.Add(stackPanel2);
					this.downloadingBorder.put_Child(this.downloadingGrid);
					this.statusTextBlock = textBlock2;
					Border border2 = new Border();
					SolidColorBrush solidColorBrush2 = new SolidColorBrush(Colors.Black);
					solidColorBrush2.put_Opacity(0.7);
					border2.put_Background(solidColorBrush2);
					border2.put_CornerRadius(new CornerRadius(5.0));
					border2.put_Height(40.0);
					border2.put_Width(100.0);
					border2.put_VerticalAlignment(0);
					border2.put_HorizontalAlignment(0);
					border2.put_Margin(new Thickness(point.X + 120.0, point.Y, 0.0, 0.0));
					border2.put_Visibility(1);
					this.installingBorder = border2;
					this.installingGrid = new Grid();
					StackPanel stackPanel3 = new StackPanel();
					stackPanel3.put_Orientation(1);
					stackPanel3.put_HorizontalAlignment(1);
					stackPanel3.put_VerticalAlignment(1);
					StackPanel stackPanel4 = stackPanel3;
					StackPanel stackPanel5 = new StackPanel();
					stackPanel5.put_Orientation(1);
					stackPanel5.put_Margin(new Thickness(5.0, 0.0, 5.0, 0.0));
					StackPanel stackPanel6 = stackPanel5;
					for (int i = 0; i < 3; i++)
					{
						Rectangle rectangle = new Rectangle();
						rectangle.put_Width(8.0);
						rectangle.put_Height(8.0);
						rectangle.put_Fill(new SolidColorBrush(Colors.White));
						rectangle.put_Margin(new Thickness(2.0, 0.0, 2.0, 0.0));
						rectangle.put_RadiusX(1.0);
						rectangle.put_RadiusY(1.0);
						Rectangle rectangle2 = rectangle;
						Storyboard storyboard = new Storyboard();
						DoubleAnimation doubleAnimation = new DoubleAnimation();
						doubleAnimation.put_From(new double?(0.3));
						doubleAnimation.put_To(new double?(1.0));
						doubleAnimation.put_Duration(TimeSpan.FromMilliseconds(800.0));
						doubleAnimation.put_AutoReverse(true);
						doubleAnimation.put_RepeatBehavior(RepeatBehavior.Forever);
						DoubleAnimation doubleAnimation2 = doubleAnimation;
						Storyboard.SetTarget(doubleAnimation2, rectangle2);
						Storyboard.SetTargetProperty(doubleAnimation2, "Opacity");
						storyboard.Children.Add(doubleAnimation2);
						storyboard.put_BeginTime(new TimeSpan?(TimeSpan.FromMilliseconds((double)(i * 200))));
						storyboard.Begin();
						stackPanel6.Children.Add(rectangle2);
					}
					TextBlock textBlock3 = new TextBlock();
					textBlock3.put_Text("Installing");
					textBlock3.put_Foreground(new SolidColorBrush(Colors.White));
					textBlock3.put_FontSize(12.0);
					textBlock3.put_VerticalAlignment(1);
					TextBlock textBlock4 = textBlock3;
					stackPanel4.Children.Add(stackPanel6);
					stackPanel4.Children.Add(textBlock4);
					this.installingGrid.Children.Add(stackPanel4);
					this.installingBorder.put_Child(this.installingGrid);
					grid.Children.Add(this.downloadingBorder);
					grid.Children.Add(this.installingBorder);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("Error creating status UI: {0}", new object[]
				{
					ex.Message
				}));
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00007F30 File Offset: 0x00006130
		private void ShowLoadingOverlay()
		{
			try
			{
				Grid grid = base.FindName("ScreenshotLO") as Grid;
				ProgressRing progressRing = base.FindName("ScreenshoRingi") as ProgressRing;
				bool flag = grid != null;
				if (flag)
				{
					grid.put_Visibility(0);
					bool flag2 = progressRing != null;
					if (flag2)
					{
						progressRing.put_IsActive(true);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("[ShowLoadingOverlay] Error: {0}", new object[]
				{
					ex.Message
				}));
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00007FBC File Offset: 0x000061BC
		private void HideLoadingOverlay()
		{
			try
			{
				Grid grid = base.FindName("ScreenshotLO") as Grid;
				ProgressRing progressRing = base.FindName("ScreenshoRingi") as ProgressRing;
				bool flag = grid != null;
				if (flag)
				{
					bool flag2 = progressRing != null;
					if (flag2)
					{
						progressRing.put_IsActive(false);
					}
					grid.put_Visibility(1);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("[HideLoadingOverlay] Error: {0}", new object[]
				{
					ex.Message
				}));
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00008048 File Offset: 0x00006248
		[DebuggerStepThrough]
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			AppDetailsPage.<OnNavigatedTo>d__27 <OnNavigatedTo>d__ = new AppDetailsPage.<OnNavigatedTo>d__27();
			<OnNavigatedTo>d__.<>4__this = this;
			<OnNavigatedTo>d__.e = e;
			<OnNavigatedTo>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<OnNavigatedTo>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <OnNavigatedTo>d__.<>t__builder;
			<>t__builder.Start<AppDetailsPage.<OnNavigatedTo>d__27>(ref <OnNavigatedTo>d__);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000808C File Offset: 0x0000628C
		[DebuggerStepThrough]
		private Task LoadAppById(string appId)
		{
			AppDetailsPage.<LoadAppById>d__28 <LoadAppById>d__ = new AppDetailsPage.<LoadAppById>d__28();
			<LoadAppById>d__.<>4__this = this;
			<LoadAppById>d__.appId = appId;
			<LoadAppById>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadAppById>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadAppById>d__.<>t__builder;
			<>t__builder.Start<AppDetailsPage.<LoadAppById>d__28>(ref <LoadAppById>d__);
			return <LoadAppById>d__.<>t__builder.Task;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000080DC File Offset: 0x000062DC
		private void PublisherText_PointerPressed(object sender, PointerRoutedEventArgs e)
		{
			try
			{
				bool flag = this.currentApp != null && !string.IsNullOrEmpty(this.currentApp.Publisher);
				if (flag)
				{
					Debug.WriteLine(string.Format("Navigating to PublisherAppsPage for publisher: {0}", new object[]
					{
						this.currentApp.Publisher
					}));
					TextBlock textBlock = sender as TextBlock;
					bool flag2 = textBlock != null;
					if (flag2)
					{
						try
						{
							textBlock.put_Foreground(new SolidColorBrush(Color.FromArgb(byte.MaxValue, 0, 170, 79)));
						}
						catch (Exception ex)
						{
							Debug.WriteLine(string.Format("Error changing publisher text color: {0}", new object[]
							{
								ex.Message
							}));
						}
					}
					bool flag3 = base.Frame != null;
					if (flag3)
					{
						base.Frame.Navigate(typeof(PublisherAppsPage), this.currentApp.Publisher);
					}
				}
			}
			catch (Exception ex2)
			{
				Debug.WriteLine(string.Format("Error in PublisherText_PointerPressed: {0}", new object[]
				{
					ex2.Message
				}));
			}
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00008204 File Offset: 0x00006404
		private void PublisherText_PointerEntered(object sender, PointerRoutedEventArgs e)
		{
			try
			{
				bool flag = this.currentApp != null && !string.IsNullOrEmpty(this.currentApp.Publisher);
				if (flag)
				{
					TextBlock textBlock = sender as TextBlock;
					bool flag2 = textBlock != null;
					if (flag2)
					{
						textBlock.put_Foreground(new SolidColorBrush(Color.FromArgb(byte.MaxValue, 0, 170, 79)));
					}
					Window window = Window.Current;
					bool flag3 = ((window != null) ? window.CoreWindow : null) != null;
					if (flag3)
					{
						Window.Current.CoreWindow.put_PointerCursor(new CoreCursor(3, 0U));
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("Error in PublisherText_PointerEntered: {0}", new object[]
				{
					ex.Message
				}));
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000082D0 File Offset: 0x000064D0
		private void PublisherText_PointerExited(object sender, PointerRoutedEventArgs e)
		{
			try
			{
				TextBlock textBlock = sender as TextBlock;
				bool flag = textBlock != null;
				if (flag)
				{
					textBlock.put_Foreground(new SolidColorBrush(Color.FromArgb(byte.MaxValue, 102, 102, 102)));
				}
				Window window = Window.Current;
				bool flag2 = ((window != null) ? window.CoreWindow : null) != null;
				if (flag2)
				{
					Window.Current.CoreWindow.put_PointerCursor(new CoreCursor(0, 0U));
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("Error in PublisherText_PointerExited: {0}", new object[]
				{
					ex.Message
				}));
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00008374 File Offset: 0x00006574
		[DebuggerStepThrough]
		private Task LoadAllApps()
		{
			AppDetailsPage.<LoadAllApps>d__32 <LoadAllApps>d__ = new AppDetailsPage.<LoadAllApps>d__32();
			<LoadAllApps>d__.<>4__this = this;
			<LoadAllApps>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadAllApps>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadAllApps>d__.<>t__builder;
			<>t__builder.Start<AppDetailsPage.<LoadAllApps>d__32>(ref <LoadAllApps>d__);
			return <LoadAllApps>d__.<>t__builder.Task;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000083BC File Offset: 0x000065BC
		[DebuggerStepThrough]
		private Task LoadSuggestedApps()
		{
			AppDetailsPage.<LoadSuggestedApps>d__33 <LoadSuggestedApps>d__ = new AppDetailsPage.<LoadSuggestedApps>d__33();
			<LoadSuggestedApps>d__.<>4__this = this;
			<LoadSuggestedApps>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadSuggestedApps>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadSuggestedApps>d__.<>t__builder;
			<>t__builder.Start<AppDetailsPage.<LoadSuggestedApps>d__33>(ref <LoadSuggestedApps>d__);
			return <LoadSuggestedApps>d__.<>t__builder.Task;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00008404 File Offset: 0x00006604
		private void FindAndSetRelatedAppsSource()
		{
			try
			{
				ListView listView = this.FindVisualChild<ListView>(this.MainHub, "RelatedAppsListView");
				bool flag = listView != null;
				if (flag)
				{
					listView.put_ItemsSource(this.suggestedApps);
					Debug.WriteLine(string.Format("Set RelatedAppsListView with {0} items", new object[]
					{
						this.suggestedApps.Count
					}));
				}
				else
				{
					Debug.WriteLine("Could not find RelatedAppsListView");
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("Error finding RelatedAppsListView: {0}", new object[]
				{
					ex.Message
				}));
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000084AC File Offset: 0x000066AC
		private T FindVisualChild<T>(DependencyObject parent, string name) where T : FrameworkElement
		{
			bool flag = parent == null;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
				{
					DependencyObject child = VisualTreeHelper.GetChild(parent, i);
					bool flag2 = child is T && ((T)((object)child)).Name == name;
					if (flag2)
					{
						return (T)((object)child);
					}
					T t = this.FindVisualChild<T>(child, name);
					bool flag3 = t != null;
					if (flag3)
					{
						return t;
					}
				}
				result = default(T);
			}
			return result;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00008554 File Offset: 0x00006754
		protected override void OnNavigatedFrom(NavigationEventArgs e)
		{
			base.OnNavigatedFrom(e);
			bool flag = this.navigationHelper != null;
			if (flag)
			{
				this.navigationHelper.OnNavigatedFrom(e);
			}
			DownloadManager.DownloadProgress -= new EventHandler<DownloadManager.DownloadProgressEventArgs>(this.OnDownloadProgress);
			DownloadManager.DownloadCompleted -= new EventHandler<DownloadManager.DownloadCompletedEventArgs>(this.OnDownloadCompleted);
			bool flag2 = this.httpClient != null;
			if (flag2)
			{
				this.httpClient.Dispose();
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000085C4 File Offset: 0x000067C4
		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				bool flag = base.Frame != null && base.Frame.CanGoBack;
				if (flag)
				{
					base.Frame.GoBack();
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("Error in BackButton_Click: {0}", new object[]
				{
					ex.Message
				}));
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00008634 File Offset: 0x00006834
		private void PlaySound()
		{
			try
			{
				SoundManager.PlayDownloadSound();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("Error playing sound: {0}", new object[]
				{
					ex.Message
				}));
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00008680 File Offset: 0x00006880
		private void PlaySoundLoaded()
		{
			try
			{
				SoundManager.PlayUpdateSound();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("Error playing sound: {0}", new object[]
				{
					ex.Message
				}));
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000086CC File Offset: 0x000068CC
		[DebuggerStepThrough]
		private void DownloadButton_Click(object sender, RoutedEventArgs e)
		{
			AppDetailsPage.<DownloadButton_Click>d__40 <DownloadButton_Click>d__ = new AppDetailsPage.<DownloadButton_Click>d__40();
			<DownloadButton_Click>d__.<>4__this = this;
			<DownloadButton_Click>d__.sender = sender;
			<DownloadButton_Click>d__.e = e;
			<DownloadButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<DownloadButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <DownloadButton_Click>d__.<>t__builder;
			<>t__builder.Start<AppDetailsPage.<DownloadButton_Click>d__40>(ref <DownloadButton_Click>d__);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00008718 File Offset: 0x00006918
		[DebuggerStepThrough]
		private void OnDownloadCompleted(object sender, DownloadManager.DownloadCompletedEventArgs e)
		{
			AppDetailsPage.<OnDownloadCompleted>d__41 <OnDownloadCompleted>d__ = new AppDetailsPage.<OnDownloadCompleted>d__41();
			<OnDownloadCompleted>d__.<>4__this = this;
			<OnDownloadCompleted>d__.sender = sender;
			<OnDownloadCompleted>d__.e = e;
			<OnDownloadCompleted>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<OnDownloadCompleted>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <OnDownloadCompleted>d__.<>t__builder;
			<>t__builder.Start<AppDetailsPage.<OnDownloadCompleted>d__41>(ref <OnDownloadCompleted>d__);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00008764 File Offset: 0x00006964
		private void OnDownloadProgress(object sender, DownloadManager.DownloadProgressEventArgs e)
		{
			bool flag;
			if (e.DownloadItem != null)
			{
				string appName = e.DownloadItem.AppName;
				StoreApp storeApp = this.currentApp;
				if (appName == ((storeApp != null) ? storeApp.Name : null) && this.isDownloadInProgress)
				{
					flag = !this.isInstalling;
					goto IL_5F;
				}
			}
			flag = false;
			IL_5F:
			bool flag2 = flag;
			if (flag2)
			{
				IAsyncAction asyncAction = base.Dispatcher.RunAsync(0, delegate()
				{
					this.UpdateProgressFromDownloadItem(e.DownloadItem);
					bool flag3 = this.statusTextBlock != null;
					if (flag3)
					{
						this.statusTextBlock.put_Text(string.Format("{0:F0}%", new object[]
						{
							e.DownloadItem.Progress
						}));
					}
					bool flag4 = this.TopProgressBar != null;
					if (flag4)
					{
						this.TopProgressBar.put_Value(e.DownloadItem.Progress);
					}
				});
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000087F0 File Offset: 0x000069F0
		[DebuggerStepThrough]
		private Task<bool> InstallWithPackageManager(StorageFile appxFile)
		{
			AppDetailsPage.<InstallWithPackageManager>d__43 <InstallWithPackageManager>d__ = new AppDetailsPage.<InstallWithPackageManager>d__43();
			<InstallWithPackageManager>d__.<>4__this = this;
			<InstallWithPackageManager>d__.appxFile = appxFile;
			<InstallWithPackageManager>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<InstallWithPackageManager>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<bool> <>t__builder = <InstallWithPackageManager>d__.<>t__builder;
			<>t__builder.Start<AppDetailsPage.<InstallWithPackageManager>d__43>(ref <InstallWithPackageManager>d__);
			return <InstallWithPackageManager>d__.<>t__builder.Task;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00008840 File Offset: 0x00006A40
		private void ShowDownloadingStatus()
		{
			try
			{
				Button button = base.FindName("DownloadButton") as Button;
				Grid grid = base.Content as Grid;
				bool flag = this.downloadingBorder != null && button != null && grid != null;
				if (flag)
				{
					GeneralTransform generalTransform = button.TransformToVisual(grid);
					Point point = generalTransform.TransformPoint(new Point(0.0, 0.0));
					this.downloadingBorder.put_Margin(new Thickness(point.X + 120.0, point.Y, 0.0, 0.0));
					this.downloadingBorder.put_Visibility(0);
				}
				bool flag2 = this.installingBorder != null;
				if (flag2)
				{
					this.installingBorder.put_Visibility(1);
				}
				bool flag3 = this.TopProgressBar != null;
				if (flag3)
				{
					this.TopProgressBar.put_Visibility(0);
					this.TopProgressBar.put_Value(0.0);
					this.TopProgressBar.put_IsIndeterminate(false);
					this.TopProgressBar.put_Foreground(new SolidColorBrush(Color.FromArgb(byte.MaxValue, 0, 170, 79)));
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("[ShowDownloadingStatus] Error: {0}", new object[]
				{
					ex.Message
				}));
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x000089BC File Offset: 0x00006BBC
		private void ShowInstallingStatus()
		{
			try
			{
				Button button = base.FindName("DownloadButton") as Button;
				Grid grid = base.Content as Grid;
				bool flag = this.installingBorder != null && button != null && grid != null;
				if (flag)
				{
					GeneralTransform generalTransform = button.TransformToVisual(grid);
					Point point = generalTransform.TransformPoint(new Point(0.0, 0.0));
					this.installingBorder.put_Margin(new Thickness(point.X + 120.0, point.Y, 0.0, 0.0));
					this.installingBorder.put_Visibility(0);
				}
				bool flag2 = this.downloadingBorder != null;
				if (flag2)
				{
					this.downloadingBorder.put_Visibility(1);
				}
				bool flag3 = this.TopProgressBar != null;
				if (flag3)
				{
					this.TopProgressBar.put_Visibility(1);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("[ShowInstallingStatus] Error: {0}", new object[]
				{
					ex.Message
				}));
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00008AE4 File Offset: 0x00006CE4
		private void HideDownloadStatus()
		{
			try
			{
				bool flag = this.downloadingBorder != null;
				if (flag)
				{
					this.downloadingBorder.put_Visibility(1);
				}
				bool flag2 = this.TopProgressBar != null;
				if (flag2)
				{
					this.TopProgressBar.put_Visibility(1);
					this.TopProgressBar.put_Value(0.0);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("[HideDownloadStatus] Error: {0}", new object[]
				{
					ex.Message
				}));
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00008B78 File Offset: 0x00006D78
		private void HideInstallingStatus()
		{
			try
			{
				bool flag = this.installingBorder != null;
				if (flag)
				{
					this.installingBorder.put_Visibility(1);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("[HideInstallingStatus] Error: {0}", new object[]
				{
					ex.Message
				}));
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00008BDC File Offset: 0x00006DDC
		private void UpdateProgressFromDownloadItem(DownloadItem item)
		{
			bool flag = item == null || this.TopProgressBar == null;
			if (!flag)
			{
				this.TopProgressBar.put_Value(item.Progress);
				this.TopProgressBar.put_IsIndeterminate(false);
				this.TopProgressBar.put_Visibility(0);
				bool flag2 = item.Status.Contains("Error") || item.Status.Contains("Failed");
				if (flag2)
				{
					this.TopProgressBar.put_Foreground(new SolidColorBrush(Colors.Red));
				}
				else
				{
					this.TopProgressBar.put_Foreground(new SolidColorBrush(Color.FromArgb(byte.MaxValue, 0, 170, 79)));
				}
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00008C98 File Offset: 0x00006E98
		private void ShowDownloadProgress(string fileName, double progress)
		{
			try
			{
				bool flag = this.TopProgressBar != null;
				if (flag)
				{
					this.TopProgressBar.put_Visibility(0);
					bool flag2 = progress >= 0.0;
					if (flag2)
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
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("[ShowDownloadProgress] Error: {0}", new object[]
				{
					ex.Message
				}));
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00008D3C File Offset: 0x00006F3C
		private void HideDownloadProgress()
		{
			try
			{
				bool flag = this.TopProgressBar != null;
				if (flag)
				{
					this.TopProgressBar.put_Visibility(1);
					this.TopProgressBar.put_Value(0.0);
					this.TopProgressBar.put_Foreground(new SolidColorBrush(Color.FromArgb(byte.MaxValue, 0, 170, 79)));
				}
				this.HideDownloadStatus();
				this.HideInstallingStatus();
				DownloadManager.DownloadProgress -= new EventHandler<DownloadManager.DownloadProgressEventArgs>(this.OnDownloadProgress);
				DownloadManager.DownloadCompleted -= new EventHandler<DownloadManager.DownloadCompletedEventArgs>(this.OnDownloadCompleted);
				this.isDownloadInProgress = false;
				this.isInstalling = false;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("[HideDownloadProgress] Error: {0}", new object[]
				{
					ex.Message
				}));
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00008E18 File Offset: 0x00007018
		[DebuggerStepThrough]
		private Task<bool> IsAppInstalled(string appName)
		{
			AppDetailsPage.<IsAppInstalled>d__51 <IsAppInstalled>d__ = new AppDetailsPage.<IsAppInstalled>d__51();
			<IsAppInstalled>d__.<>4__this = this;
			<IsAppInstalled>d__.appName = appName;
			<IsAppInstalled>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<IsAppInstalled>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<bool> <>t__builder = <IsAppInstalled>d__.<>t__builder;
			<>t__builder.Start<AppDetailsPage.<IsAppInstalled>d__51>(ref <IsAppInstalled>d__);
			return <IsAppInstalled>d__.<>t__builder.Task;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00008E68 File Offset: 0x00007068
		[DebuggerStepThrough]
		private Task UpdateDownloadButtonState()
		{
			AppDetailsPage.<UpdateDownloadButtonState>d__52 <UpdateDownloadButtonState>d__ = new AppDetailsPage.<UpdateDownloadButtonState>d__52();
			<UpdateDownloadButtonState>d__.<>4__this = this;
			<UpdateDownloadButtonState>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateDownloadButtonState>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <UpdateDownloadButtonState>d__.<>t__builder;
			<>t__builder.Start<AppDetailsPage.<UpdateDownloadButtonState>d__52>(ref <UpdateDownloadButtonState>d__);
			return <UpdateDownloadButtonState>d__.<>t__builder.Task;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00008EB0 File Offset: 0x000070B0
		[DebuggerStepThrough]
		private void ReportButton_Click(object sender, RoutedEventArgs e)
		{
			AppDetailsPage.<ReportButton_Click>d__53 <ReportButton_Click>d__ = new AppDetailsPage.<ReportButton_Click>d__53();
			<ReportButton_Click>d__.<>4__this = this;
			<ReportButton_Click>d__.sender = sender;
			<ReportButton_Click>d__.e = e;
			<ReportButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<ReportButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <ReportButton_Click>d__.<>t__builder;
			<>t__builder.Start<AppDetailsPage.<ReportButton_Click>d__53>(ref <ReportButton_Click>d__);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00008EFC File Offset: 0x000070FC
		private void RelatedAppsListView_ItemClick(object sender, ItemClickEventArgs e)
		{
			StoreApp storeApp = e.ClickedItem as StoreApp;
			bool flag = storeApp != null;
			if (flag)
			{
				base.Frame.Navigate(typeof(AppDetailsPage), storeApp);
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00008F38 File Offset: 0x00007138
		private void ScreenshotList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Image image = base.FindName("MainScreenshotImage") as Image;
			Image image2 = base.FindName("FallbackIconImage") as Image;
			Grid grid = base.FindName("ScreenshotLO") as Grid;
			ProgressRing progressRing = base.FindName("ScreenshoRingi") as ProgressRing;
			bool flag = image != null;
			if (flag)
			{
				image.put_Visibility(1);
				bool flag2 = grid != null;
				if (flag2)
				{
					grid.put_Visibility(0);
					bool flag3 = progressRing != null;
					if (flag3)
					{
						progressRing.put_IsActive(true);
					}
				}
			}
			bool flag4 = image2 != null;
			if (flag4)
			{
				image2.put_Visibility(1);
			}
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00008FD8 File Offset: 0x000071D8
		private void SeeAllReviews_Click(object sender, RoutedEventArgs e)
		{
			bool flag = this.currentApp == null;
			if (!flag)
			{
				base.Frame.Navigate(typeof(AppReviewsPage), this.currentApp);
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000595D File Offset: 0x00003B5D
		private void FullscreenButton_Click(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00009014 File Offset: 0x00007214
		private void MainScreenshot_ImageOpened(object sender, RoutedEventArgs e)
		{
			Image image = sender as Image;
			bool flag = image != null;
			if (flag)
			{
				image.put_Visibility(0);
				Grid grid = base.FindName("ScreenshotLO") as Grid;
				bool flag2 = grid != null;
				if (flag2)
				{
					grid.put_Visibility(1);
					ProgressRing progressRing = base.FindName("ScreenshoRingi") as ProgressRing;
					bool flag3 = progressRing != null;
					if (flag3)
					{
						progressRing.put_IsActive(false);
					}
				}
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00009084 File Offset: 0x00007284
		private void MainScreenshot_ImageFailed(object sender, ExceptionRoutedEventArgs e)
		{
			Debug.WriteLine("Main screenshot failed to load: " + e.ErrorMessage);
			Image image = sender as Image;
			Image image2 = base.FindName("FallbackIconImage") as Image;
			bool flag = image != null;
			if (flag)
			{
				image.put_Visibility(1);
			}
			bool flag2 = image2 != null;
			if (flag2)
			{
				image2.put_Visibility(0);
				Grid grid = base.FindName("ScreenshotLO") as Grid;
				bool flag3 = grid != null;
				if (flag3)
				{
					grid.put_Visibility(1);
					ProgressRing progressRing = base.FindName("ScreenshoRingi") as ProgressRing;
					bool flag4 = progressRing != null;
					if (flag4)
					{
						progressRing.put_IsActive(false);
					}
				}
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00005867 File Offset: 0x00003A67
		private void Border_Tapped_1(object sender, TappedRoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(AllAppsPage));
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00009134 File Offset: 0x00007334
		[DebuggerStepThrough]
		private Task ShowMessageDialog(string title, string message)
		{
			AppDetailsPage.<ShowMessageDialog>d__61 <ShowMessageDialog>d__ = new AppDetailsPage.<ShowMessageDialog>d__61();
			<ShowMessageDialog>d__.<>4__this = this;
			<ShowMessageDialog>d__.title = title;
			<ShowMessageDialog>d__.message = message;
			<ShowMessageDialog>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ShowMessageDialog>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <ShowMessageDialog>d__.<>t__builder;
			<>t__builder.Start<AppDetailsPage.<ShowMessageDialog>d__61>(ref <ShowMessageDialog>d__);
			return <ShowMessageDialog>d__.<>t__builder.Task;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000918C File Offset: 0x0000738C
		[DebuggerStepThrough]
		private Task EnsureFirstScreenshotSelected()
		{
			AppDetailsPage.<EnsureFirstScreenshotSelected>d__62 <EnsureFirstScreenshotSelected>d__ = new AppDetailsPage.<EnsureFirstScreenshotSelected>d__62();
			<EnsureFirstScreenshotSelected>d__.<>4__this = this;
			<EnsureFirstScreenshotSelected>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<EnsureFirstScreenshotSelected>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <EnsureFirstScreenshotSelected>d__.<>t__builder;
			<>t__builder.Start<AppDetailsPage.<EnsureFirstScreenshotSelected>d__62>(ref <EnsureFirstScreenshotSelected>d__);
			return <EnsureFirstScreenshotSelected>d__.<>t__builder.Task;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000091D4 File Offset: 0x000073D4
		[DebuggerStepThrough]
		private Task LoadReviewStatsForCurrentApp()
		{
			AppDetailsPage.<LoadReviewStatsForCurrentApp>d__63 <LoadReviewStatsForCurrentApp>d__ = new AppDetailsPage.<LoadReviewStatsForCurrentApp>d__63();
			<LoadReviewStatsForCurrentApp>d__.<>4__this = this;
			<LoadReviewStatsForCurrentApp>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadReviewStatsForCurrentApp>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadReviewStatsForCurrentApp>d__.<>t__builder;
			<>t__builder.Start<AppDetailsPage.<LoadReviewStatsForCurrentApp>d__63>(ref <LoadReviewStatsForCurrentApp>d__);
			return <LoadReviewStatsForCurrentApp>d__.<>t__builder.Task;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000921C File Offset: 0x0000741C
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			bool contentLoaded = this._contentLoaded;
			if (!contentLoaded)
			{
				this._contentLoaded = true;
				Application.LoadComponent(this, new Uri("ms-appx:///AppDetailsPage.xaml"), 0);
				this.pageRoot = (Page)base.FindName("pageRoot");
				this.TopProgressBar = (ProgressBar)base.FindName("TopProgressBar");
				this.MainHub = (Hub)base.FindName("MainHub");
				this.backButton = (Button)base.FindName("backButton");
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000092A8 File Offset: 0x000074A8
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.BackButton_Click));
				break;
			}
			case 2:
			{
				ListViewBase listViewBase = (ListViewBase)target;
				WindowsRuntimeMarshal.AddEventHandler<ItemClickEventHandler>(new Func<ItemClickEventHandler, EventRegistrationToken>(listViewBase.add_ItemClick), new Action<EventRegistrationToken>(listViewBase.remove_ItemClick), new ItemClickEventHandler(this.RelatedAppsListView_ItemClick));
				break;
			}
			case 3:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.SeeAllReviews_Click));
				break;
			}
			case 4:
			{
				Selector selector = (Selector)target;
				WindowsRuntimeMarshal.AddEventHandler<SelectionChangedEventHandler>(new Func<SelectionChangedEventHandler, EventRegistrationToken>(selector.add_SelectionChanged), new Action<EventRegistrationToken>(selector.remove_SelectionChanged), new SelectionChangedEventHandler(this.ScreenshotList_SelectionChanged));
				break;
			}
			case 5:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.FullscreenButton_Click));
				break;
			}
			case 6:
			{
				Image image = (Image)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(image.add_ImageOpened), new Action<EventRegistrationToken>(image.remove_ImageOpened), new RoutedEventHandler(this.MainScreenshot_ImageOpened));
				image = (Image)target;
				WindowsRuntimeMarshal.AddEventHandler<ExceptionRoutedEventHandler>(new Func<ExceptionRoutedEventHandler, EventRegistrationToken>(image.add_ImageFailed), new Action<EventRegistrationToken>(image.remove_ImageFailed), new ExceptionRoutedEventHandler(this.MainScreenshot_ImageFailed));
				break;
			}
			case 7:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.DownloadButton_Click));
				break;
			}
			case 8:
			{
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<PointerEventHandler>(new Func<PointerEventHandler, EventRegistrationToken>(uielement.add_PointerPressed), new Action<EventRegistrationToken>(uielement.remove_PointerPressed), new PointerEventHandler(this.PublisherText_PointerPressed));
				uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<PointerEventHandler>(new Func<PointerEventHandler, EventRegistrationToken>(uielement.add_PointerEntered), new Action<EventRegistrationToken>(uielement.remove_PointerEntered), new PointerEventHandler(this.PublisherText_PointerEntered));
				uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<PointerEventHandler>(new Func<PointerEventHandler, EventRegistrationToken>(uielement.add_PointerExited), new Action<EventRegistrationToken>(uielement.remove_PointerExited), new PointerEventHandler(this.PublisherText_PointerExited));
				break;
			}
			case 9:
			{
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uielement.add_Tapped), new Action<EventRegistrationToken>(uielement.remove_Tapped), new TappedEventHandler(this.Border_Tapped_1));
				break;
			}
			}
			this._contentLoaded = true;
		}

		// Token: 0x040000A3 RID: 163
		private StoreApp currentApp;

		// Token: 0x040000A4 RID: 164
		private HttpClient httpClient;

		// Token: 0x040000A5 RID: 165
		private bool isDownloading = false;

		// Token: 0x040000A6 RID: 166
		private bool isLoadingFromDeepLink = false;

		// Token: 0x040000A7 RID: 167
		private List<StoreApp> allApps;

		// Token: 0x040000A8 RID: 168
		private List<StoreApp> suggestedApps = new List<StoreApp>();

		// Token: 0x040000A9 RID: 169
		private List<Review> appReviews = new List<Review>();

		// Token: 0x040000AA RID: 170
		private UserAccount currentUser;

		// Token: 0x040000AB RID: 171
		private bool showReviews = true;

		// Token: 0x040000AC RID: 172
		private NavigationHelper navigationHelper;

		// Token: 0x040000AD RID: 173
		private ObservableDictionary defaultViewModel = new ObservableDictionary();

		// Token: 0x040000AE RID: 174
		private DownloadItem currentDownloadItem;

		// Token: 0x040000AF RID: 175
		private bool isDownloadInProgress = false;

		// Token: 0x040000B0 RID: 176
		private bool isInstalling = false;

		// Token: 0x040000B1 RID: 177
		private TextBlock statusTextBlock;

		// Token: 0x040000B2 RID: 178
		private Border downloadingBorder;

		// Token: 0x040000B3 RID: 179
		private Border installingBorder;

		// Token: 0x040000B4 RID: 180
		private Grid downloadingGrid;

		// Token: 0x040000B5 RID: 181
		private Grid installingGrid;

		// Token: 0x040000B6 RID: 182
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Page pageRoot;

		// Token: 0x040000B7 RID: 183
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ProgressBar TopProgressBar;

		// Token: 0x040000B8 RID: 184
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Hub MainHub;

		// Token: 0x040000B9 RID: 185
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button backButton;

		// Token: 0x040000BA RID: 186
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
