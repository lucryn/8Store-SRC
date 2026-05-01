using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
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
using Windows.UI.Xaml.Navigation;

namespace Win81StoreRevival
{
	// Token: 0x02000038 RID: 56
	public sealed class PopularPage : Page, IComponentConnector
	{
		// Token: 0x0600037C RID: 892 RVA: 0x00015064 File Offset: 0x00013264
		public PopularPage()
		{
			this.InitializeComponent();
			Debug.WriteLine("PopularPage constructor called");
			base.put_DataContext(this.topRatedApps);
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(this.add_Loaded), new Action<EventRegistrationToken>(this.remove_Loaded), new RoutedEventHandler(this.PopularPage_Loaded));
		}

		// Token: 0x0600037D RID: 893 RVA: 0x000150D0 File Offset: 0x000132D0
		[DebuggerStepThrough]
		private void PopularPage_Loaded(object sender, RoutedEventArgs e)
		{
			PopularPage.<PopularPage_Loaded>d__4 <PopularPage_Loaded>d__ = new PopularPage.<PopularPage_Loaded>d__4();
			<PopularPage_Loaded>d__.<>4__this = this;
			<PopularPage_Loaded>d__.sender = sender;
			<PopularPage_Loaded>d__.e = e;
			<PopularPage_Loaded>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<PopularPage_Loaded>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <PopularPage_Loaded>d__.<>t__builder;
			<>t__builder.Start<PopularPage.<PopularPage_Loaded>d__4>(ref <PopularPage_Loaded>d__);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0001511C File Offset: 0x0001331C
		[DebuggerStepThrough]
		private Task LoadTopRatedAppsAsync()
		{
			PopularPage.<LoadTopRatedAppsAsync>d__5 <LoadTopRatedAppsAsync>d__ = new PopularPage.<LoadTopRatedAppsAsync>d__5();
			<LoadTopRatedAppsAsync>d__.<>4__this = this;
			<LoadTopRatedAppsAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadTopRatedAppsAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadTopRatedAppsAsync>d__.<>t__builder;
			<>t__builder.Start<PopularPage.<LoadTopRatedAppsAsync>d__5>(ref <LoadTopRatedAppsAsync>d__);
			return <LoadTopRatedAppsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00015164 File Offset: 0x00013364
		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			bool canGoBack = base.Frame.CanGoBack;
			if (canGoBack)
			{
				base.Frame.GoBack();
			}
			else
			{
				base.Frame.Navigate(typeof(MainPage));
			}
		}

		// Token: 0x06000380 RID: 896 RVA: 0x000151AC File Offset: 0x000133AC
		private void AppItem_Tapped(object sender, TappedRoutedEventArgs e)
		{
			StackPanel stackPanel = sender as StackPanel;
			bool flag = stackPanel != null;
			if (flag)
			{
				StoreApp storeApp = stackPanel.DataContext as StoreApp;
				bool flag2 = storeApp != null;
				if (flag2)
				{
					Debug.WriteLine(string.Format("App tapped: {0}", new object[]
					{
						storeApp.Name
					}));
					bool flag3 = !storeApp.Id.StartsWith("settings");
					if (flag3)
					{
						base.Frame.Navigate(typeof(AppDetailsPage), storeApp);
					}
				}
			}
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00015234 File Offset: 0x00013434
		[DebuggerStepThrough]
		private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
		{
			PopularPage.<Image_ImageFailed>d__8 <Image_ImageFailed>d__ = new PopularPage.<Image_ImageFailed>d__8();
			<Image_ImageFailed>d__.<>4__this = this;
			<Image_ImageFailed>d__.sender = sender;
			<Image_ImageFailed>d__.e = e;
			<Image_ImageFailed>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<Image_ImageFailed>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <Image_ImageFailed>d__.<>t__builder;
			<>t__builder.Start<PopularPage.<Image_ImageFailed>d__8>(ref <Image_ImageFailed>d__);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0001527E File Offset: 0x0001347E
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);
			Debug.WriteLine("PopularPage OnNavigatedTo called");
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00015294 File Offset: 0x00013494
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			bool contentLoaded = this._contentLoaded;
			if (!contentLoaded)
			{
				this._contentLoaded = true;
				Application.LoadComponent(this, new Uri("ms-appx:///PopularPage.xaml"), 0);
				this.LoadingPanel = (StackPanel)base.FindName("LoadingPanel");
				this.NoResultsPanel = (StackPanel)base.FindName("NoResultsPanel");
				this.MainScrollViewer = (ScrollViewer)base.FindName("MainScrollViewer");
				this.TopRatedGridView = (GridView)base.FindName("TopRatedGridView");
				this.LoadingRing = (ProgressRing)base.FindName("LoadingRing");
				this.BackButton = (Button)base.FindName("BackButton");
			}
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00015350 File Offset: 0x00013550
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

		// Token: 0x0400019B RID: 411
		private ObservableCollection<StoreApp> topRatedApps = new ObservableCollection<StoreApp>();

		// Token: 0x0400019C RID: 412
		private const string AppsJsonUrl = "https://8store.modyleprojects.ru/data/apps.json";

		// Token: 0x0400019D RID: 413
		private static List<StoreApp> cachedApps = null;

		// Token: 0x0400019E RID: 414
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel LoadingPanel;

		// Token: 0x0400019F RID: 415
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel NoResultsPanel;

		// Token: 0x040001A0 RID: 416
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ScrollViewer MainScrollViewer;

		// Token: 0x040001A1 RID: 417
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private GridView TopRatedGridView;

		// Token: 0x040001A2 RID: 418
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ProgressRing LoadingRing;

		// Token: 0x040001A3 RID: 419
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button BackButton;

		// Token: 0x040001A4 RID: 420
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
