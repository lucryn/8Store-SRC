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
	// Token: 0x0200003D RID: 61
	public sealed class PopularPage : Page, IComponentConnector
	{
		// Token: 0x06000440 RID: 1088 RVA: 0x000154B4 File Offset: 0x000136B4
		public PopularPage()
		{
			this.InitializeComponent();
			base.put_DataContext(this.topRatedApps);
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(this.add_Loaded), new Action<EventRegistrationToken>(this.remove_Loaded), new RoutedEventHandler(this.PopularPage_Loaded));
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00015510 File Offset: 0x00013710
		private void PopularPage_Loaded(object sender, RoutedEventArgs e)
		{
			PopularPage.<PopularPage_Loaded>d__4 <PopularPage_Loaded>d__;
			<PopularPage_Loaded>d__.<>4__this = this;
			<PopularPage_Loaded>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<PopularPage_Loaded>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <PopularPage_Loaded>d__.<>t__builder;
			<>t__builder.Start<PopularPage.<PopularPage_Loaded>d__4>(ref <PopularPage_Loaded>d__);
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0001554C File Offset: 0x0001374C
		private Task LoadTopRatedAppsAsync()
		{
			PopularPage.<LoadTopRatedAppsAsync>d__5 <LoadTopRatedAppsAsync>d__;
			<LoadTopRatedAppsAsync>d__.<>4__this = this;
			<LoadTopRatedAppsAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadTopRatedAppsAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadTopRatedAppsAsync>d__.<>t__builder;
			<>t__builder.Start<PopularPage.<LoadTopRatedAppsAsync>d__5>(ref <LoadTopRatedAppsAsync>d__);
			return <LoadTopRatedAppsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000E2C1 File Offset: 0x0000C4C1
		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			if (base.Frame.CanGoBack)
			{
				base.Frame.GoBack();
				return;
			}
			base.Frame.Navigate(typeof(MainPage));
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00015594 File Offset: 0x00013794
		private void AppItem_Tapped(object sender, TappedRoutedEventArgs e)
		{
			StackPanel stackPanel = sender as StackPanel;
			if (stackPanel != null)
			{
				StoreApp storeApp = stackPanel.DataContext as StoreApp;
				if (storeApp != null && !storeApp.Id.StartsWith("settings"))
				{
					base.Frame.Navigate(typeof(AppDetailsPage), storeApp);
				}
			}
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x000155E4 File Offset: 0x000137E4
		private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
		{
			PopularPage.<Image_ImageFailed>d__8 <Image_ImageFailed>d__;
			<Image_ImageFailed>d__.sender = sender;
			<Image_ImageFailed>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<Image_ImageFailed>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <Image_ImageFailed>d__.<>t__builder;
			<>t__builder.Start<PopularPage.<Image_ImageFailed>d__8>(ref <Image_ImageFailed>d__);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00004F3D File Offset: 0x0000313D
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00015620 File Offset: 0x00013820
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Application.LoadComponent(this, new Uri("ms-appx:///PopularPage.xaml"), 0);
			this.LoadingPanel = (StackPanel)base.FindName("LoadingPanel");
			this.NoResultsPanel = (StackPanel)base.FindName("NoResultsPanel");
			this.MainScrollViewer = (ScrollViewer)base.FindName("MainScrollViewer");
			this.TopRatedGridView = (GridView)base.FindName("TopRatedGridView");
			this.LoadingRing = (ProgressRing)base.FindName("LoadingRing");
			this.BackButton = (Button)base.FindName("BackButton");
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x000156D4 File Offset: 0x000138D4
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

		// Token: 0x040001CB RID: 459
		private ObservableCollection<StoreApp> topRatedApps = new ObservableCollection<StoreApp>();

		// Token: 0x040001CC RID: 460
		private const string AppsJsonUrl = "https://8store.modyleprojects.ru/data/apps.json";

		// Token: 0x040001CD RID: 461
		private static List<StoreApp> cachedApps;

		// Token: 0x040001CE RID: 462
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel LoadingPanel;

		// Token: 0x040001CF RID: 463
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel NoResultsPanel;

		// Token: 0x040001D0 RID: 464
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ScrollViewer MainScrollViewer;

		// Token: 0x040001D1 RID: 465
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private GridView TopRatedGridView;

		// Token: 0x040001D2 RID: 466
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ProgressRing LoadingRing;

		// Token: 0x040001D3 RID: 467
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button BackButton;

		// Token: 0x040001D4 RID: 468
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
