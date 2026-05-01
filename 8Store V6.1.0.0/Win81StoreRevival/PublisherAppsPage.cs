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
	// Token: 0x0200003E RID: 62
	public sealed class PublisherAppsPage : Page, IComponentConnector
	{
		// Token: 0x0600044B RID: 1099 RVA: 0x000157C4 File Offset: 0x000139C4
		public PublisherAppsPage()
		{
			try
			{
				this.InitializeComponent();
				this.httpClient = new HttpClient();
				this.httpClient.Timeout = TimeSpan.FromSeconds(30.0);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00015818 File Offset: 0x00013A18
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			PublisherAppsPage.<OnNavigatedTo>d__4 <OnNavigatedTo>d__;
			<OnNavigatedTo>d__.<>4__this = this;
			<OnNavigatedTo>d__.e = e;
			<OnNavigatedTo>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<OnNavigatedTo>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <OnNavigatedTo>d__.<>t__builder;
			<>t__builder.Start<PublisherAppsPage.<OnNavigatedTo>d__4>(ref <OnNavigatedTo>d__);
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0001585C File Offset: 0x00013A5C
		private Task LoadAndFilterApps()
		{
			PublisherAppsPage.<LoadAndFilterApps>d__5 <LoadAndFilterApps>d__;
			<LoadAndFilterApps>d__.<>4__this = this;
			<LoadAndFilterApps>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadAndFilterApps>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadAndFilterApps>d__.<>t__builder;
			<>t__builder.Start<PublisherAppsPage.<LoadAndFilterApps>d__5>(ref <LoadAndFilterApps>d__);
			return <LoadAndFilterApps>d__.<>t__builder.Task;
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x000158A4 File Offset: 0x00013AA4
		private Task ShowNoAppsFound(string message)
		{
			PublisherAppsPage.<ShowNoAppsFound>d__6 <ShowNoAppsFound>d__;
			<ShowNoAppsFound>d__.<>4__this = this;
			<ShowNoAppsFound>d__.message = message;
			<ShowNoAppsFound>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ShowNoAppsFound>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <ShowNoAppsFound>d__.<>t__builder;
			<>t__builder.Start<PublisherAppsPage.<ShowNoAppsFound>d__6>(ref <ShowNoAppsFound>d__);
			return <ShowNoAppsFound>d__.<>t__builder.Task;
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x000158F4 File Offset: 0x00013AF4
		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (base.Frame != null && base.Frame.CanGoBack)
				{
					base.Frame.GoBack();
				}
				else if (base.Frame != null)
				{
					base.Frame.Navigate(typeof(MainPage));
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00015958 File Offset: 0x00013B58
		private void AppItem_Tapped(object sender, TappedRoutedEventArgs e)
		{
			try
			{
				Grid grid = sender as Grid;
				if (grid != null)
				{
					StoreApp storeApp = grid.DataContext as StoreApp;
					if (storeApp != null && base.Frame != null)
					{
						base.Frame.Navigate(typeof(AppDetailsPage), storeApp);
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x000159B4 File Offset: 0x00013BB4
		private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
		{
			try
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
						image.put_Source(new BitmapImage(new Uri("ms-appx:///Assets/placeholder.png")));
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00015A20 File Offset: 0x00013C20
		protected override void OnNavigatedFrom(NavigationEventArgs e)
		{
			base.OnNavigatedFrom(e);
			if (this.httpClient != null)
			{
				this.httpClient.Dispose();
				this.httpClient = null;
			}
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00015A44 File Offset: 0x00013C44
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
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

		// Token: 0x06000454 RID: 1108 RVA: 0x00015B24 File Offset: 0x00013D24
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

		// Token: 0x040001D5 RID: 469
		private string publisherName;

		// Token: 0x040001D6 RID: 470
		private List<StoreApp> allApps;

		// Token: 0x040001D7 RID: 471
		private HttpClient httpClient;

		// Token: 0x040001D8 RID: 472
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Grid MainContentArea;

		// Token: 0x040001D9 RID: 473
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel LoadingPanel;

		// Token: 0x040001DA RID: 474
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel NoAppsPanel;

		// Token: 0x040001DB RID: 475
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private GridView AppsGridView;

		// Token: 0x040001DC RID: 476
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock NoAppsText;

		// Token: 0x040001DD RID: 477
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ProgressRing LoadingRing;

		// Token: 0x040001DE RID: 478
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button BackButton;

		// Token: 0x040001DF RID: 479
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock PublisherTitle;

		// Token: 0x040001E0 RID: 480
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
