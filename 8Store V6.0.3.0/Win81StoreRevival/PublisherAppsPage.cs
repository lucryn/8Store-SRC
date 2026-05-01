using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
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
	// Token: 0x02000039 RID: 57
	public sealed class PublisherAppsPage : Page, IComponentConnector
	{
		// Token: 0x06000387 RID: 903 RVA: 0x00015454 File Offset: 0x00013654
		public PublisherAppsPage()
		{
			try
			{
				this.InitializeComponent();
				this.httpClient = new HttpClient();
				this.httpClient.Timeout = TimeSpan.FromSeconds(30.0);
				Debug.WriteLine("PublisherAppsPage constructor called");
			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("Error in PublisherAppsPage constructor: {0}", new object[]
				{
					ex.Message
				}));
			}
		}

		// Token: 0x06000388 RID: 904 RVA: 0x000154D8 File Offset: 0x000136D8
		[DebuggerStepThrough]
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			PublisherAppsPage.<OnNavigatedTo>d__4 <OnNavigatedTo>d__ = new PublisherAppsPage.<OnNavigatedTo>d__4();
			<OnNavigatedTo>d__.<>4__this = this;
			<OnNavigatedTo>d__.e = e;
			<OnNavigatedTo>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<OnNavigatedTo>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <OnNavigatedTo>d__.<>t__builder;
			<>t__builder.Start<PublisherAppsPage.<OnNavigatedTo>d__4>(ref <OnNavigatedTo>d__);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0001551C File Offset: 0x0001371C
		[DebuggerStepThrough]
		private Task LoadAndFilterApps()
		{
			PublisherAppsPage.<LoadAndFilterApps>d__5 <LoadAndFilterApps>d__ = new PublisherAppsPage.<LoadAndFilterApps>d__5();
			<LoadAndFilterApps>d__.<>4__this = this;
			<LoadAndFilterApps>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadAndFilterApps>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadAndFilterApps>d__.<>t__builder;
			<>t__builder.Start<PublisherAppsPage.<LoadAndFilterApps>d__5>(ref <LoadAndFilterApps>d__);
			return <LoadAndFilterApps>d__.<>t__builder.Task;
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00015564 File Offset: 0x00013764
		[DebuggerStepThrough]
		private Task ShowNoAppsFound(string message)
		{
			PublisherAppsPage.<ShowNoAppsFound>d__6 <ShowNoAppsFound>d__ = new PublisherAppsPage.<ShowNoAppsFound>d__6();
			<ShowNoAppsFound>d__.<>4__this = this;
			<ShowNoAppsFound>d__.message = message;
			<ShowNoAppsFound>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ShowNoAppsFound>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <ShowNoAppsFound>d__.<>t__builder;
			<>t__builder.Start<PublisherAppsPage.<ShowNoAppsFound>d__6>(ref <ShowNoAppsFound>d__);
			return <ShowNoAppsFound>d__.<>t__builder.Task;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x000155B4 File Offset: 0x000137B4
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
					bool flag2 = base.Frame != null;
					if (flag2)
					{
						base.Frame.Navigate(typeof(MainPage));
					}
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

		// Token: 0x0600038C RID: 908 RVA: 0x0001564C File Offset: 0x0001384C
		private void AppItem_Tapped(object sender, TappedRoutedEventArgs e)
		{
			try
			{
				Grid grid = sender as Grid;
				bool flag = grid != null;
				if (flag)
				{
					StoreApp storeApp = grid.DataContext as StoreApp;
					bool flag2 = storeApp != null && base.Frame != null;
					if (flag2)
					{
						Debug.WriteLine(string.Format("Navigating to AppDetailsPage for: {0}", new object[]
						{
							storeApp.Name
						}));
						base.Frame.Navigate(typeof(AppDetailsPage), storeApp);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("Error in AppItem_Tapped: {0}", new object[]
				{
					ex.Message
				}));
			}
		}

		// Token: 0x0600038D RID: 909 RVA: 0x000156FC File Offset: 0x000138FC
		private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
		{
			try
			{
				Debug.WriteLine(string.Format("Image failed to load: {0}", new object[]
				{
					(e != null) ? e.ErrorMessage : null
				}));
				Image image = sender as Image;
				bool flag = image != null;
				if (flag)
				{
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
						image.put_Source(new BitmapImage(new Uri("ms-appx:///Assets/placeholder.png")));
					}
				}
			}
			catch (Exception ex2)
			{
				Debug.WriteLine(string.Format("Error in Image_ImageFailed: {0}", new object[]
				{
					ex2.Message
				}));
			}
		}

		// Token: 0x0600038E RID: 910 RVA: 0x000157E0 File Offset: 0x000139E0
		protected override void OnNavigatedFrom(NavigationEventArgs e)
		{
			base.OnNavigatedFrom(e);
			bool flag = this.httpClient != null;
			if (flag)
			{
				this.httpClient.Dispose();
				this.httpClient = null;
			}
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00015818 File Offset: 0x00013A18
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			bool contentLoaded = this._contentLoaded;
			if (!contentLoaded)
			{
				this._contentLoaded = true;
				Application.LoadComponent(this, new Uri("ms-appx:///PublisherAppsPage.xaml"), 0);
				this.MainContentArea = (Grid)base.FindName("MainContentArea");
				this.LoadingPanel = (StackPanel)base.FindName("LoadingPanel");
				this.NoAppsPanel = (StackPanel)base.FindName("NoAppsPanel");
				this.AppsGridView = (GridView)base.FindName("AppsGridView");
				this.NoAppsText = (TextBlock)base.FindName("NoAppsText");
				this.LoadingRing = (ProgressRing)base.FindName("LoadingRing");
				this.BackButton = (Button)base.FindName("BackButton");
				this.PublisherTitle = (TextBlock)base.FindName("PublisherTitle");
			}
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00015900 File Offset: 0x00013B00
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
			{
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uielement.add_Tapped), new Action<EventRegistrationToken>(uielement.remove_Tapped), new TappedEventHandler(this.AppItem_Tapped));
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
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.BackButton_Click));
				break;
			}
			}
			this._contentLoaded = true;
		}

		// Token: 0x040001A5 RID: 421
		private string publisherName;

		// Token: 0x040001A6 RID: 422
		private List<StoreApp> allApps;

		// Token: 0x040001A7 RID: 423
		private HttpClient httpClient;

		// Token: 0x040001A8 RID: 424
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Grid MainContentArea;

		// Token: 0x040001A9 RID: 425
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel LoadingPanel;

		// Token: 0x040001AA RID: 426
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel NoAppsPanel;

		// Token: 0x040001AB RID: 427
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private GridView AppsGridView;

		// Token: 0x040001AC RID: 428
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock NoAppsText;

		// Token: 0x040001AD RID: 429
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ProgressRing LoadingRing;

		// Token: 0x040001AE RID: 430
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button BackButton;

		// Token: 0x040001AF RID: 431
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock PublisherTitle;

		// Token: 0x040001B0 RID: 432
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
