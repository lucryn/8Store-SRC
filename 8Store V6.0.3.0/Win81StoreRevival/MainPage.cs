using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Win81StoreRevival.Flyouts;
using Windows.ApplicationModel.Resources;
using Windows.Data.Xml.Dom;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using Windows.UI;
using Windows.UI.ApplicationSettings;
using Windows.UI.Core;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.Text;
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
	// Token: 0x02000031 RID: 49
	public sealed class MainPage : Page, IComponentConnector
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0000FC38 File Offset: 0x0000DE38
		// (set) Token: 0x06000289 RID: 649 RVA: 0x0000FC50 File Offset: 0x0000DE50
		public ObservableCollection<StoreApp> MoreApps
		{
			get
			{
				return this.moreApps;
			}
			set
			{
				this.moreApps = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000FC5C File Offset: 0x0000DE5C
		// (set) Token: 0x0600028B RID: 651 RVA: 0x0000FC74 File Offset: 0x0000DE74
		public ObservableCollection<StoreApp> RandomApps
		{
			get
			{
				return this.randomApps;
			}
			set
			{
				this.randomApps = value;
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000FC80 File Offset: 0x0000DE80
		public static string Localized(string key, params object[] args)
		{
			ResourceLoader resourceLoader = new ResourceLoader();
			string @string = resourceLoader.GetString(key);
			return string.Format(@string, args);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000FCA8 File Offset: 0x0000DEA8
		private void OnWindowVisibilityChanged(object sender, VisibilityChangedEventArgs e)
		{
			try
			{
				Debug.WriteLine(string.Format("[MainPage] Window visibility changed: Visible = {0}", new object[]
				{
					e.Visible
				}));
				bool visible = e.Visible;
				if (visible)
				{
					Debug.WriteLine("[MainPage] App is now visible - refreshing if needed");
					bool flag = this._lastCacheUpdate.AddMinutes(1.0) < DateTime.Now;
					if (flag)
					{
						Debug.WriteLine("[MainPage] Cache is stale, refreshing...");
						Task task = Task.Run(delegate()
						{
							MainPage.<<OnWindowVisibilityChanged>b__34_0>d <<OnWindowVisibilityChanged>b__34_0>d = new MainPage.<<OnWindowVisibilityChanged>b__34_0>d();
							<<OnWindowVisibilityChanged>b__34_0>d.<>4__this = this;
							<<OnWindowVisibilityChanged>b__34_0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
							<<OnWindowVisibilityChanged>b__34_0>d.<>1__state = -1;
							AsyncTaskMethodBuilder <>t__builder = <<OnWindowVisibilityChanged>b__34_0>d.<>t__builder;
							<>t__builder.Start<MainPage.<<OnWindowVisibilityChanged>b__34_0>d>(ref <<OnWindowVisibilityChanged>b__34_0>d);
							return <<OnWindowVisibilityChanged>b__34_0>d.<>t__builder.Task;
						});
					}
				}
				else
				{
					Debug.WriteLine("[MainPage] App is being hidden - saving state");
					try
					{
						bool flag2 = this._isCacheValid && this.allApps != null && this.allApps.Count > 0;
						if (flag2)
						{
							MainPage.<>c__DisplayClass34_0 CS$<>8__locals1 = new MainPage.<>c__DisplayClass34_0();
							CS$<>8__locals1.<>4__this = this;
							CS$<>8__locals1.appsList = new List<StoreApp>(this.allApps);
							Task.Run(delegate()
							{
								MainPage.<>c__DisplayClass34_0.<<OnWindowVisibilityChanged>b__1>d <<OnWindowVisibilityChanged>b__1>d = new MainPage.<>c__DisplayClass34_0.<<OnWindowVisibilityChanged>b__1>d();
								<<OnWindowVisibilityChanged>b__1>d.<>4__this = CS$<>8__locals1;
								<<OnWindowVisibilityChanged>b__1>d.<>t__builder = AsyncTaskMethodBuilder.Create();
								<<OnWindowVisibilityChanged>b__1>d.<>1__state = -1;
								AsyncTaskMethodBuilder <>t__builder = <<OnWindowVisibilityChanged>b__1>d.<>t__builder;
								<>t__builder.Start<MainPage.<>c__DisplayClass34_0.<<OnWindowVisibilityChanged>b__1>d>(ref <<OnWindowVisibilityChanged>b__1>d);
								return <<OnWindowVisibilityChanged>b__1>d.<>t__builder.Task;
							});
						}
					}
					catch (Exception ex)
					{
						Debug.WriteLine(string.Format("[MainPage] Error saving cache on hide: {0}", new object[]
						{
							ex.Message
						}));
					}
				}
			}
			catch (Exception ex2)
			{
				Debug.WriteLine(string.Format("[MainPage] Error in visibility handler: {0}", new object[]
				{
					ex2.Message
				}));
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000FE28 File Offset: 0x0000E028
		public MainPage()
		{
			this.InitializeComponent();
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(this.add_Loaded), new Action<EventRegistrationToken>(this.remove_Loaded), new RoutedEventHandler(this.MainPage_Loaded));
			this.moreApps = new ObservableCollection<StoreApp>();
			Window window = Window.Current;
			WindowsRuntimeMarshal.AddEventHandler<WindowVisibilityChangedEventHandler>(new Func<WindowVisibilityChangedEventHandler, EventRegistrationToken>(window.add_VisibilityChanged), new Action<EventRegistrationToken>(window.remove_VisibilityChanged), new WindowVisibilityChangedEventHandler(this.OnWindowVisibilityChanged));
			Hub mainHub = this.MainHub;
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(mainHub.add_Loaded), new Action<EventRegistrationToken>(mainHub.remove_Loaded), new RoutedEventHandler(this.MainHub_Loaded));
			this._jsonLoadingCancellationTokenSource = new CancellationTokenSource();
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000FF78 File Offset: 0x0000E178
		[DebuggerStepThrough]
		private void MainHub_Loaded(object sender, RoutedEventArgs e)
		{
			MainPage.<MainHub_Loaded>d__36 <MainHub_Loaded>d__ = new MainPage.<MainHub_Loaded>d__36();
			<MainHub_Loaded>d__.<>4__this = this;
			<MainHub_Loaded>d__.sender = sender;
			<MainHub_Loaded>d__.e = e;
			<MainHub_Loaded>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<MainHub_Loaded>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <MainHub_Loaded>d__.<>t__builder;
			<>t__builder.Start<MainPage.<MainHub_Loaded>d__36>(ref <MainHub_Loaded>d__);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000FFC4 File Offset: 0x0000E1C4
		[DebuggerStepThrough]
		private Task ShowDiscontinueServiceDialog()
		{
			MainPage.<ShowDiscontinueServiceDialog>d__37 <ShowDiscontinueServiceDialog>d__ = new MainPage.<ShowDiscontinueServiceDialog>d__37();
			<ShowDiscontinueServiceDialog>d__.<>4__this = this;
			<ShowDiscontinueServiceDialog>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ShowDiscontinueServiceDialog>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <ShowDiscontinueServiceDialog>d__.<>t__builder;
			<>t__builder.Start<MainPage.<ShowDiscontinueServiceDialog>d__37>(ref <ShowDiscontinueServiceDialog>d__);
			return <ShowDiscontinueServiceDialog>d__.<>t__builder.Task;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0001000C File Offset: 0x0000E20C
		private void ApplyAppSettings()
		{
			try
			{
				ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
				bool enabled = false;
				bool flag = localSettings.Values.ContainsKey("LowPerformanceMode");
				if (flag)
				{
					enabled = (bool)localSettings.Values["LowPerformanceMode"];
				}
				bool enabled2 = false;
				bool flag2 = localSettings.Values.ContainsKey("DarkMode");
				if (flag2)
				{
					enabled2 = (bool)localSettings.Values["DarkMode"];
				}
				this.ApplyLowPerformanceMode(enabled);
				this.ApplyDarkMode(enabled2);
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error applying app settings: " + ex.Message);
			}
		}

		// Token: 0x06000292 RID: 658 RVA: 0x000100C4 File Offset: 0x0000E2C4
		private void ApplyDarkMode(bool enabled)
		{
			try
			{
				if (enabled)
				{
					Grid grid = base.Content as Grid;
					bool flag = grid != null;
					if (flag)
					{
						Panel panel = grid;
						ImageBrush imageBrush = new ImageBrush();
						imageBrush.put_ImageSource(new BitmapImage(new Uri("ms-appx:///Assets/Store1.png")));
						imageBrush.put_Stretch(3);
						panel.put_Background(imageBrush);
					}
					Control mainHub = this.MainHub;
					ImageBrush imageBrush2 = new ImageBrush();
					imageBrush2.put_ImageSource(new BitmapImage(new Uri("ms-appx:///Assets/Store1.png")));
					imageBrush2.put_Stretch(3);
					mainHub.put_Background(imageBrush2);
					this.SpotlightSection.put_Visibility(1);
					this.ChangeAllTextColors(Colors.White);
					this.ApplyDarkModeTemplates();
					this.UpdateSpotlightTheme(true);
				}
				else
				{
					Grid grid2 = base.Content as Grid;
					bool flag2 = grid2 != null;
					if (flag2)
					{
						grid2.put_Background(new SolidColorBrush(Colors.White));
					}
					Control mainHub2 = this.MainHub;
					ImageBrush imageBrush3 = new ImageBrush();
					imageBrush3.put_ImageSource(new BitmapImage(new Uri("ms-appx:///Assets/Store.png")));
					imageBrush3.put_Stretch(3);
					mainHub2.put_Background(imageBrush3);
					this.SpotlightSection.put_Visibility(0);
					this.ChangeAllTextColors(Colors.Black);
					this.ApplyLightModeTemplates();
					this.UpdateSpotlightTheme(false);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("[ApplyDarkMode] Error: {0}", new object[]
				{
					ex.Message
				}));
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00010240 File Offset: 0x0000E440
		[DebuggerStepThrough]
		private Task InitializeCacheAsync()
		{
			MainPage.<InitializeCacheAsync>d__40 <InitializeCacheAsync>d__ = new MainPage.<InitializeCacheAsync>d__40();
			<InitializeCacheAsync>d__.<>4__this = this;
			<InitializeCacheAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<InitializeCacheAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <InitializeCacheAsync>d__.<>t__builder;
			<>t__builder.Start<MainPage.<InitializeCacheAsync>d__40>(ref <InitializeCacheAsync>d__);
			return <InitializeCacheAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00010288 File Offset: 0x0000E488
		[DebuggerStepThrough]
		private Task<StorageFile> GetCacheFileAsync()
		{
			MainPage.<GetCacheFileAsync>d__41 <GetCacheFileAsync>d__ = new MainPage.<GetCacheFileAsync>d__41();
			<GetCacheFileAsync>d__.<>4__this = this;
			<GetCacheFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<StorageFile>.Create();
			<GetCacheFileAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<StorageFile> <>t__builder = <GetCacheFileAsync>d__.<>t__builder;
			<>t__builder.Start<MainPage.<GetCacheFileAsync>d__41>(ref <GetCacheFileAsync>d__);
			return <GetCacheFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x000102D0 File Offset: 0x0000E4D0
		[DebuggerStepThrough]
		private Task UpdateCacheAsync(List<StoreApp> apps)
		{
			MainPage.<UpdateCacheAsync>d__42 <UpdateCacheAsync>d__ = new MainPage.<UpdateCacheAsync>d__42();
			<UpdateCacheAsync>d__.<>4__this = this;
			<UpdateCacheAsync>d__.apps = apps;
			<UpdateCacheAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateCacheAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <UpdateCacheAsync>d__.<>t__builder;
			<>t__builder.Start<MainPage.<UpdateCacheAsync>d__42>(ref <UpdateCacheAsync>d__);
			return <UpdateCacheAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00010320 File Offset: 0x0000E520
		[DebuggerStepThrough]
		private Task<bool> TryLoadAppsFromCache()
		{
			MainPage.<TryLoadAppsFromCache>d__43 <TryLoadAppsFromCache>d__ = new MainPage.<TryLoadAppsFromCache>d__43();
			<TryLoadAppsFromCache>d__.<>4__this = this;
			<TryLoadAppsFromCache>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<TryLoadAppsFromCache>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<bool> <>t__builder = <TryLoadAppsFromCache>d__.<>t__builder;
			<>t__builder.Start<MainPage.<TryLoadAppsFromCache>d__43>(ref <TryLoadAppsFromCache>d__);
			return <TryLoadAppsFromCache>d__.<>t__builder.Task;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00010368 File Offset: 0x0000E568
		private void ApplyDarkModeTemplates()
		{
			try
			{
				HubSection hubSection = this.MainHub.Sections[1];
				bool flag = hubSection != null;
				if (flag)
				{
					ListView listView = this.FindChild<ListView>(hubSection, "SpecialPicksListView");
					bool flag2 = listView != null;
					if (flag2)
					{
						listView.put_ItemTemplate(base.Resources["AppItemTemplateDark"] as DataTemplate);
					}
				}
				HubSection hubSection2 = this.MainHub.Sections[2];
				bool flag3 = hubSection2 != null;
				if (flag3)
				{
					GridView gridView = this.FindChild<GridView>(hubSection2, "FeaturedGridView");
					bool flag4 = gridView != null;
					if (flag4)
					{
						gridView.put_ItemTemplate(base.Resources["FeaturedGridItemTemplateDark"] as DataTemplate);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error applying dark mode templates: " + ex.Message);
			}
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00010450 File Offset: 0x0000E650
		private void ApplyLightModeTemplates()
		{
			try
			{
				HubSection hubSection = this.MainHub.Sections[1];
				bool flag = hubSection != null;
				if (flag)
				{
					ListView listView = this.FindChild<ListView>(hubSection, "SpecialPicksListView");
					bool flag2 = listView != null;
					if (flag2)
					{
						listView.put_ItemTemplate(base.Resources["AppItemTemplateLight"] as DataTemplate);
					}
				}
				HubSection hubSection2 = this.MainHub.Sections[2];
				bool flag3 = hubSection2 != null;
				if (flag3)
				{
					GridView gridView = this.FindChild<GridView>(hubSection2, "FeaturedGridView");
					bool flag4 = gridView != null;
					if (flag4)
					{
						gridView.put_ItemTemplate(base.Resources["FeaturedGridItemTemplateLight"] as DataTemplate);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error applying light mode templates: " + ex.Message);
			}
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00010538 File Offset: 0x0000E738
		private void UpdateSpotlightTheme(bool darkMode)
		{
			try
			{
				Debug.WriteLine(string.Format("[UpdateSpotlightTheme] Updating spotlight, darkMode: {0}", new object[]
				{
					darkMode
				}));
				Color color = darkMode ? Colors.White : Colors.Black;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("[UpdateSpotlightTheme] Error: {0}", new object[]
				{
					ex.Message
				}));
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x000105B0 File Offset: 0x0000E7B0
		private void ChangeAllTextColors(Color color)
		{
			SolidColorBrush solidColorBrush = new SolidColorBrush(color);
			TextBlock textBlock = this.MainHub.Header as TextBlock;
			bool flag = textBlock != null;
			if (flag)
			{
				textBlock.put_Foreground(solidColorBrush);
			}
			foreach (HubSection hubSection in this.MainHub.Sections)
			{
				TextBlock textBlock2 = hubSection.Header as TextBlock;
				bool flag2 = textBlock2 != null;
				if (flag2)
				{
					textBlock2.put_Foreground(solidColorBrush);
				}
			}
			TextBlock textBlock3 = this.FindChild<TextBlock>(this, "SearchTextBox");
			bool flag3 = textBlock3 != null;
			if (flag3)
			{
				textBlock3.put_Foreground(solidColorBrush);
			}
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00010678 File Offset: 0x0000E878
		private List<T> FindAllChildren<T>(DependencyObject parent) where T : DependencyObject
		{
			List<T> list = new List<T>();
			bool flag = parent == null;
			List<T> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
				for (int i = 0; i < childrenCount; i++)
				{
					DependencyObject child = VisualTreeHelper.GetChild(parent, i);
					bool flag2 = child is T;
					if (flag2)
					{
						list.Add(child as T);
					}
					list.AddRange(this.FindAllChildren<T>(child));
				}
				result = list;
			}
			return result;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00005833 File Offset: 0x00003A33
		private void PlayUpdateSound()
		{
			SoundManager.PlayStartupSound();
		}

		// Token: 0x0600029D RID: 669 RVA: 0x000106FC File Offset: 0x0000E8FC
		[DebuggerStepThrough]
		private void MainPage_Loaded(object sender, RoutedEventArgs e)
		{
			MainPage.<MainPage_Loaded>d__50 <MainPage_Loaded>d__ = new MainPage.<MainPage_Loaded>d__50();
			<MainPage_Loaded>d__.<>4__this = this;
			<MainPage_Loaded>d__.sender = sender;
			<MainPage_Loaded>d__.e = e;
			<MainPage_Loaded>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<MainPage_Loaded>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <MainPage_Loaded>d__.<>t__builder;
			<>t__builder.Start<MainPage.<MainPage_Loaded>d__50>(ref <MainPage_Loaded>d__);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00010748 File Offset: 0x0000E948
		[DebuggerStepThrough]
		public Task<bool> RefreshCacheFromNetwork()
		{
			MainPage.<RefreshCacheFromNetwork>d__51 <RefreshCacheFromNetwork>d__ = new MainPage.<RefreshCacheFromNetwork>d__51();
			<RefreshCacheFromNetwork>d__.<>4__this = this;
			<RefreshCacheFromNetwork>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<RefreshCacheFromNetwork>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<bool> <>t__builder = <RefreshCacheFromNetwork>d__.<>t__builder;
			<>t__builder.Start<MainPage.<RefreshCacheFromNetwork>d__51>(ref <RefreshCacheFromNetwork>d__);
			return <RefreshCacheFromNetwork>d__.<>t__builder.Task;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00010790 File Offset: 0x0000E990
		private void UpdateUIWithData()
		{
			try
			{
				Debug.WriteLine("=== UPDATING UI WITH DATA ===");
				Debug.WriteLine(string.Format("Staff Picks: {0}, Featured: {1}, More Apps: {2}", new object[]
				{
					this.staffPicksApps.Count,
					this.featuredApps.Count,
					this.moreApps.Count
				}));
				bool flag = this.MainHub == null || this.MainHub.Sections.Count < 4;
				if (flag)
				{
					Debug.WriteLine("MainHub not ready yet");
				}
				else
				{
					bool flag2 = this.MainHub.Sections.Count > 0;
					if (flag2)
					{
						HubSection hubSection = this.MainHub.Sections[0];
						bool flag3 = hubSection != null;
						if (flag3)
						{
							ItemsControl itemsControl = this.FindVisualChild<ItemsControl>(hubSection, "RandomAppsVerticalList");
							bool flag4 = itemsControl != null;
							if (flag4)
							{
								itemsControl.put_ItemsSource(this.randomApps);
								Debug.WriteLine("Set RandomAppsVerticalList");
							}
						}
					}
					bool flag5 = this.MainHub.Sections.Count > 1;
					if (flag5)
					{
						HubSection hubSection2 = this.MainHub.Sections[1];
						bool flag6 = hubSection2 != null;
						if (flag6)
						{
							ListView listView = this.FindVisualChild<ListView>(hubSection2, "SpecialPicksListView");
							bool flag7 = listView != null;
							if (flag7)
							{
								listView.put_ItemsSource(this.staffPicksApps);
								Debug.WriteLine("Set SpecialPicksListView (Staff Picks) with " + this.staffPicksApps.Count + " items");
							}
							else
							{
								Debug.WriteLine("ERROR: Could not find SpecialPicksListView in Section 2");
							}
						}
					}
					bool flag8 = this.MainHub.Sections.Count > 2;
					if (flag8)
					{
						HubSection hubSection3 = this.MainHub.Sections[2];
						bool flag9 = hubSection3 != null;
						if (flag9)
						{
							GridView gridView = this.FindVisualChild<GridView>(hubSection3, "FeaturedGridView");
							bool flag10 = gridView != null;
							if (flag10)
							{
								gridView.put_ItemsSource(this.featuredApps);
								Debug.WriteLine("Set FeaturedGridView with " + this.featuredApps.Count + " items");
							}
							else
							{
								Debug.WriteLine("ERROR: Could not find FeaturedGridView in Section 3");
							}
						}
					}
					bool flag11 = this.MainHub.Sections.Count > 3;
					if (flag11)
					{
						HubSection hubSection4 = this.MainHub.Sections[3];
						bool flag12 = hubSection4 != null;
						if (flag12)
						{
							ListView listView2 = this.FindVisualChild<ListView>(hubSection4, "MoreAppsListView");
							bool flag13 = listView2 != null;
							if (flag13)
							{
								listView2.put_ItemsSource(this.moreApps);
								Debug.WriteLine("Set MoreAppsListView with " + this.moreApps.Count + " items");
							}
							else
							{
								Debug.WriteLine("ERROR: Could not find MoreAppsListView in Section 4");
								ListView listView3 = this.FindVisualChild<ListView>(this.MainHub, "MoreAppsListView");
								bool flag14 = listView3 != null;
								if (flag14)
								{
									listView3.put_ItemsSource(this.moreApps);
									Debug.WriteLine("Found MoreAppsListView via direct search");
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("ERROR updating UI: {0}", new object[]
				{
					ex.Message
				}));
			}
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00010AEC File Offset: 0x0000ECEC
		private void MoreAppsListView_ItemClick(object sender, ItemClickEventArgs e)
		{
			StoreApp storeApp = e.ClickedItem as StoreApp;
			bool flag = storeApp != null;
			if (flag)
			{
				base.Frame.Navigate(typeof(AppDetailsPage), storeApp);
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00010B28 File Offset: 0x0000ED28
		private void SafeApplyDarkMode(bool enabled)
		{
			try
			{
				bool flag = this.MainHub == null;
				if (flag)
				{
					Debug.WriteLine("[SafeApplyDarkMode] MainHub is null, skipping...");
				}
				else
				{
					DispatcherTimer timer = new DispatcherTimer();
					timer.put_Interval(TimeSpan.FromMilliseconds(500.0));
					DispatcherTimer timer2 = timer;
					WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(timer2.add_Tick), new Action<EventRegistrationToken>(timer2.remove_Tick), delegate(object s, object args)
					{
						timer.Stop();
						this.ApplyDarkMode(enabled);
					});
					timer.Start();
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("[SafeApplyDarkMode] Error: {0}", new object[]
				{
					ex.Message
				}));
			}
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00010C14 File Offset: 0x0000EE14
		private void SafeApplyLowPerformanceMode(bool enabled)
		{
			try
			{
				bool flag = this.MainHub == null;
				if (flag)
				{
					Debug.WriteLine("[SafeApplyLowPerformanceMode] MainHub is null, skipping...");
				}
				else
				{
					DispatcherTimer timer = new DispatcherTimer();
					timer.put_Interval(TimeSpan.FromMilliseconds(600.0));
					DispatcherTimer timer2 = timer;
					WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(timer2.add_Tick), new Action<EventRegistrationToken>(timer2.remove_Tick), delegate(object s, object args)
					{
						timer.Stop();
						this.ApplyLowPerformanceMode(enabled);
					});
					timer.Start();
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("[SafeApplyLowPerformanceMode] Error: {0}", new object[]
				{
					ex.Message
				}));
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00010D00 File Offset: 0x0000EF00
		private void ApplyLowPerformanceMode(bool enabled)
		{
			try
			{
				Debug.WriteLine(string.Format("[ApplyLowPerformanceMode] Starting, enabled={0}", new object[]
				{
					enabled
				}));
				Border border = this.FindChild<Border>(this, "SearchBarBorder");
				bool flag = border != null;
				if (flag)
				{
					border.put_Visibility(enabled ? 1 : 0);
					Debug.WriteLine(string.Format("[ApplyLowPerformanceMode] Search bar visibility: {0}", new object[]
					{
						border.Visibility
					}));
				}
				else
				{
					Debug.WriteLine("[ApplyLowPerformanceMode] SearchBarBorder not found!");
				}
				bool flag2 = this.MainHub == null || this.MainHub.Sections.Count == 0;
				if (flag2)
				{
					Debug.WriteLine("[ApplyLowPerformanceMode] MainHub or sections not ready");
				}
				else
				{
					if (enabled)
					{
						this.MainHub.Sections[0].put_Visibility(1);
						Debug.WriteLine("[ApplyLowPerformanceMode] Hiding spotlight section");
						bool flag3 = this.TopAppsButton != null;
						if (flag3)
						{
							this.TopAppsButton.put_Visibility(1);
							Debug.WriteLine("[ApplyLowPerformanceMode] Hiding TopAppsButton");
						}
						bool flag4 = this.ExclusiveHubButton != null;
						if (flag4)
						{
							this.ExclusiveHubButton.put_Visibility(1);
							Debug.WriteLine("[ApplyLowPerformanceMode] Hiding ExclusiveHubButton");
						}
					}
					else
					{
						this.MainHub.Sections[0].put_Visibility(0);
						Debug.WriteLine("[ApplyLowPerformanceMode] Showing spotlight section");
						bool flag5 = this.TopAppsButton != null;
						if (flag5)
						{
							this.TopAppsButton.put_Visibility(0);
							Debug.WriteLine("[ApplyLowPerformanceMode] Showing TopAppsButton");
						}
						bool flag6 = this.ExclusiveHubButton != null;
						if (flag6)
						{
							this.ExclusiveHubButton.put_Visibility(0);
							Debug.WriteLine("[ApplyLowPerformanceMode] Showing ExclusiveHubButton");
						}
					}
					Debug.WriteLine("[ApplyLowPerformanceMode] Completed successfully");
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("[ApplyLowPerformanceMode] Error: {0}", new object[]
				{
					ex.Message
				}));
				Debug.WriteLine(string.Format("[ApplyLowPerformanceMode] Stack trace: {0}", new object[]
				{
					ex.StackTrace
				}));
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00010F20 File Offset: 0x0000F120
		[DebuggerStepThrough]
		private Task UpdateTileQueueFromJson(string jsonUrl)
		{
			MainPage.<UpdateTileQueueFromJson>d__57 <UpdateTileQueueFromJson>d__ = new MainPage.<UpdateTileQueueFromJson>d__57();
			<UpdateTileQueueFromJson>d__.<>4__this = this;
			<UpdateTileQueueFromJson>d__.jsonUrl = jsonUrl;
			<UpdateTileQueueFromJson>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateTileQueueFromJson>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <UpdateTileQueueFromJson>d__.<>t__builder;
			<>t__builder.Start<MainPage.<UpdateTileQueueFromJson>d__57>(ref <UpdateTileQueueFromJson>d__);
			return <UpdateTileQueueFromJson>d__.<>t__builder.Task;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00010F70 File Offset: 0x0000F170
		[DebuggerStepThrough]
		private Task UpdateTileFromJson(string jsonUrl, int index)
		{
			MainPage.<UpdateTileFromJson>d__58 <UpdateTileFromJson>d__ = new MainPage.<UpdateTileFromJson>d__58();
			<UpdateTileFromJson>d__.<>4__this = this;
			<UpdateTileFromJson>d__.jsonUrl = jsonUrl;
			<UpdateTileFromJson>d__.index = index;
			<UpdateTileFromJson>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateTileFromJson>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <UpdateTileFromJson>d__.<>t__builder;
			<>t__builder.Start<MainPage.<UpdateTileFromJson>d__58>(ref <UpdateTileFromJson>d__);
			return <UpdateTileFromJson>d__.<>t__builder.Task;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00010FC8 File Offset: 0x0000F1C8
		private TileApp GetByIndex(Dictionary<string, TileApp> dict, int index)
		{
			bool flag = dict == null;
			TileApp result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string text = "app" + index;
				bool flag2 = dict.ContainsKey(text);
				if (flag2)
				{
					result = dict[text];
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00011010 File Offset: 0x0000F210
		private void FillSquare(XmlDocument doc, TileApp app)
		{
			bool flag = app == null;
			if (!flag)
			{
				XmlNodeList elementsByTagName = doc.GetElementsByTagName("image");
				bool flag2 = elementsByTagName.Count > 0 && !string.IsNullOrEmpty(app.imgUrl);
				if (flag2)
				{
					elementsByTagName[0].Attributes.GetNamedItem("src").put_NodeValue(app.imgUrl);
				}
				XmlNodeList elementsByTagName2 = doc.GetElementsByTagName("text");
				bool flag3 = elementsByTagName2.Count > 0;
				if (flag3)
				{
					elementsByTagName2[0].put_InnerText(app.title ?? "");
				}
			}
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x000110B0 File Offset: 0x0000F2B0
		private void FillWide(XmlDocument doc, TileApp app)
		{
			bool flag = app == null;
			if (!flag)
			{
				XmlNodeList elementsByTagName = doc.GetElementsByTagName("image");
				bool flag2 = elementsByTagName.Count > 0 && !string.IsNullOrEmpty(app.imgUrl);
				if (flag2)
				{
					elementsByTagName[0].Attributes.GetNamedItem("src").put_NodeValue(app.imgUrl);
				}
				XmlNodeList elementsByTagName2 = doc.GetElementsByTagName("text");
				bool flag3 = elementsByTagName2.Count > 0;
				if (flag3)
				{
					elementsByTagName2[0].put_InnerText(app.title ?? "");
				}
				bool flag4 = elementsByTagName2.Count > 1;
				if (flag4)
				{
					elementsByTagName2[1].put_InnerText(app.description ?? "");
				}
			}
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0001117C File Offset: 0x0000F37C
		private void FillLarge(XmlDocument doc, TileApp app)
		{
			bool flag = app == null;
			if (!flag)
			{
				XmlNodeList elementsByTagName = doc.GetElementsByTagName("image");
				bool flag2 = elementsByTagName.Count > 0 && !string.IsNullOrEmpty(app.imgUrl);
				if (flag2)
				{
					elementsByTagName[0].Attributes.GetNamedItem("src").put_NodeValue(app.imgUrl);
				}
				XmlNodeList elementsByTagName2 = doc.GetElementsByTagName("text");
				bool flag3 = elementsByTagName2.Count > 0;
				if (flag3)
				{
					elementsByTagName2[0].put_InnerText(app.title ?? "");
				}
				bool flag4 = elementsByTagName2.Count > 1;
				if (flag4)
				{
					elementsByTagName2[1].put_InnerText(app.description ?? "");
				}
			}
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00011248 File Offset: 0x0000F448
		private void ImportBinding(XmlDocument to, XmlDocument from)
		{
			IXmlNode xmlNode = from.GetElementsByTagName("binding").Item(0U);
			IXmlNode xmlNode2 = to.GetElementsByTagName("visual").Item(0U);
			IXmlNode xmlNode3 = to.ImportNode(xmlNode, true);
			xmlNode2.AppendChild(xmlNode3);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0001128C File Offset: 0x0000F48C
		[DebuggerStepThrough]
		private Task ProcessAutoLoginWithSignOut()
		{
			MainPage.<ProcessAutoLoginWithSignOut>d__64 <ProcessAutoLoginWithSignOut>d__ = new MainPage.<ProcessAutoLoginWithSignOut>d__64();
			<ProcessAutoLoginWithSignOut>d__.<>4__this = this;
			<ProcessAutoLoginWithSignOut>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ProcessAutoLoginWithSignOut>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <ProcessAutoLoginWithSignOut>d__.<>t__builder;
			<>t__builder.Start<MainPage.<ProcessAutoLoginWithSignOut>d__64>(ref <ProcessAutoLoginWithSignOut>d__);
			return <ProcessAutoLoginWithSignOut>d__.<>t__builder.Task;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x000112D4 File Offset: 0x0000F4D4
		[DebuggerStepThrough]
		private Task LoadAppsFromJsonAndAccount()
		{
			MainPage.<LoadAppsFromJsonAndAccount>d__65 <LoadAppsFromJsonAndAccount>d__ = new MainPage.<LoadAppsFromJsonAndAccount>d__65();
			<LoadAppsFromJsonAndAccount>d__.<>4__this = this;
			<LoadAppsFromJsonAndAccount>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadAppsFromJsonAndAccount>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadAppsFromJsonAndAccount>d__.<>t__builder;
			<>t__builder.Start<MainPage.<LoadAppsFromJsonAndAccount>d__65>(ref <LoadAppsFromJsonAndAccount>d__);
			return <LoadAppsFromJsonAndAccount>d__.<>t__builder.Task;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0001131C File Offset: 0x0000F51C
		[DebuggerStepThrough]
		private Task LoadAppsFromJsonAndAccountWithAutoLogin()
		{
			MainPage.<LoadAppsFromJsonAndAccountWithAutoLogin>d__66 <LoadAppsFromJsonAndAccountWithAutoLogin>d__ = new MainPage.<LoadAppsFromJsonAndAccountWithAutoLogin>d__66();
			<LoadAppsFromJsonAndAccountWithAutoLogin>d__.<>4__this = this;
			<LoadAppsFromJsonAndAccountWithAutoLogin>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadAppsFromJsonAndAccountWithAutoLogin>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadAppsFromJsonAndAccountWithAutoLogin>d__.<>t__builder;
			<>t__builder.Start<MainPage.<LoadAppsFromJsonAndAccountWithAutoLogin>d__66>(ref <LoadAppsFromJsonAndAccountWithAutoLogin>d__);
			return <LoadAppsFromJsonAndAccountWithAutoLogin>d__.<>t__builder.Task;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00011364 File Offset: 0x0000F564
		[DebuggerStepThrough]
		private Task<bool> CheckForUpdates(bool autoRedirect = true)
		{
			MainPage.<CheckForUpdates>d__67 <CheckForUpdates>d__ = new MainPage.<CheckForUpdates>d__67();
			<CheckForUpdates>d__.<>4__this = this;
			<CheckForUpdates>d__.autoRedirect = autoRedirect;
			<CheckForUpdates>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<CheckForUpdates>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<bool> <>t__builder = <CheckForUpdates>d__.<>t__builder;
			<>t__builder.Start<MainPage.<CheckForUpdates>d__67>(ref <CheckForUpdates>d__);
			return <CheckForUpdates>d__.<>t__builder.Task;
		}

		// Token: 0x060002AF RID: 687 RVA: 0x000113B4 File Offset: 0x0000F5B4
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

		// Token: 0x060002B0 RID: 688 RVA: 0x0001146C File Offset: 0x0000F66C
		[DebuggerStepThrough]
		private Task ShowUpdateDialog(AppVersionInfo versionInfo)
		{
			MainPage.<ShowUpdateDialog>d__69 <ShowUpdateDialog>d__ = new MainPage.<ShowUpdateDialog>d__69();
			<ShowUpdateDialog>d__.<>4__this = this;
			<ShowUpdateDialog>d__.versionInfo = versionInfo;
			<ShowUpdateDialog>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ShowUpdateDialog>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <ShowUpdateDialog>d__.<>t__builder;
			<>t__builder.Start<MainPage.<ShowUpdateDialog>d__69>(ref <ShowUpdateDialog>d__);
			return <ShowUpdateDialog>d__.<>t__builder.Task;
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x000114BC File Offset: 0x0000F6BC
		[DebuggerStepThrough]
		private Task DownloadAndInstallUpdate(AppVersionInfo versionInfo)
		{
			MainPage.<DownloadAndInstallUpdate>d__70 <DownloadAndInstallUpdate>d__ = new MainPage.<DownloadAndInstallUpdate>d__70();
			<DownloadAndInstallUpdate>d__.<>4__this = this;
			<DownloadAndInstallUpdate>d__.versionInfo = versionInfo;
			<DownloadAndInstallUpdate>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DownloadAndInstallUpdate>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <DownloadAndInstallUpdate>d__.<>t__builder;
			<>t__builder.Start<MainPage.<DownloadAndInstallUpdate>d__70>(ref <DownloadAndInstallUpdate>d__);
			return <DownloadAndInstallUpdate>d__.<>t__builder.Task;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0001150C File Offset: 0x0000F70C
		[DebuggerStepThrough]
		private Task InstallUpdate()
		{
			MainPage.<InstallUpdate>d__71 <InstallUpdate>d__ = new MainPage.<InstallUpdate>d__71();
			<InstallUpdate>d__.<>4__this = this;
			<InstallUpdate>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<InstallUpdate>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <InstallUpdate>d__.<>t__builder;
			<>t__builder.Start<MainPage.<InstallUpdate>d__71>(ref <InstallUpdate>d__);
			return <InstallUpdate>d__.<>t__builder.Task;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00011554 File Offset: 0x0000F754
		[DebuggerStepThrough]
		private void ShowForceUpdateWarning(AppVersionInfo versionInfo)
		{
			MainPage.<ShowForceUpdateWarning>d__72 <ShowForceUpdateWarning>d__ = new MainPage.<ShowForceUpdateWarning>d__72();
			<ShowForceUpdateWarning>d__.<>4__this = this;
			<ShowForceUpdateWarning>d__.versionInfo = versionInfo;
			<ShowForceUpdateWarning>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<ShowForceUpdateWarning>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <ShowForceUpdateWarning>d__.<>t__builder;
			<>t__builder.Start<MainPage.<ShowForceUpdateWarning>d__72>(ref <ShowForceUpdateWarning>d__);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00011598 File Offset: 0x0000F798
		[DebuggerStepThrough]
		private Task ShowMessage(string title, string message)
		{
			MainPage.<ShowMessage>d__73 <ShowMessage>d__ = new MainPage.<ShowMessage>d__73();
			<ShowMessage>d__.<>4__this = this;
			<ShowMessage>d__.title = title;
			<ShowMessage>d__.message = message;
			<ShowMessage>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ShowMessage>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <ShowMessage>d__.<>t__builder;
			<>t__builder.Start<MainPage.<ShowMessage>d__73>(ref <ShowMessage>d__);
			return <ShowMessage>d__.<>t__builder.Task;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x000115F0 File Offset: 0x0000F7F0
		[DebuggerStepThrough]
		private Task LoadAppsFromNetworkAndUpdateCache()
		{
			MainPage.<LoadAppsFromNetworkAndUpdateCache>d__74 <LoadAppsFromNetworkAndUpdateCache>d__ = new MainPage.<LoadAppsFromNetworkAndUpdateCache>d__74();
			<LoadAppsFromNetworkAndUpdateCache>d__.<>4__this = this;
			<LoadAppsFromNetworkAndUpdateCache>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadAppsFromNetworkAndUpdateCache>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadAppsFromNetworkAndUpdateCache>d__.<>t__builder;
			<>t__builder.Start<MainPage.<LoadAppsFromNetworkAndUpdateCache>d__74>(ref <LoadAppsFromNetworkAndUpdateCache>d__);
			return <LoadAppsFromNetworkAndUpdateCache>d__.<>t__builder.Task;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00011638 File Offset: 0x0000F838
		[DebuggerStepThrough]
		private Task RefreshCacheInBackground()
		{
			MainPage.<RefreshCacheInBackground>d__75 <RefreshCacheInBackground>d__ = new MainPage.<RefreshCacheInBackground>d__75();
			<RefreshCacheInBackground>d__.<>4__this = this;
			<RefreshCacheInBackground>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<RefreshCacheInBackground>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <RefreshCacheInBackground>d__.<>t__builder;
			<>t__builder.Start<MainPage.<RefreshCacheInBackground>d__75>(ref <RefreshCacheInBackground>d__);
			return <RefreshCacheInBackground>d__.<>t__builder.Task;
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00011680 File Offset: 0x0000F880
		[DebuggerStepThrough]
		private Task UpdateReviewStatsForCachedApps()
		{
			MainPage.<UpdateReviewStatsForCachedApps>d__76 <UpdateReviewStatsForCachedApps>d__ = new MainPage.<UpdateReviewStatsForCachedApps>d__76();
			<UpdateReviewStatsForCachedApps>d__.<>4__this = this;
			<UpdateReviewStatsForCachedApps>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateReviewStatsForCachedApps>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <UpdateReviewStatsForCachedApps>d__.<>t__builder;
			<>t__builder.Start<MainPage.<UpdateReviewStatsForCachedApps>d__76>(ref <UpdateReviewStatsForCachedApps>d__);
			return <UpdateReviewStatsForCachedApps>d__.<>t__builder.Task;
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x000116C8 File Offset: 0x0000F8C8
		[DebuggerStepThrough]
		private Task SelectRandomApps()
		{
			MainPage.<SelectRandomApps>d__77 <SelectRandomApps>d__ = new MainPage.<SelectRandomApps>d__77();
			<SelectRandomApps>d__.<>4__this = this;
			<SelectRandomApps>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SelectRandomApps>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <SelectRandomApps>d__.<>t__builder;
			<>t__builder.Start<MainPage.<SelectRandomApps>d__77>(ref <SelectRandomApps>d__);
			return <SelectRandomApps>d__.<>t__builder.Task;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00011710 File Offset: 0x0000F910
		private void Shuffle<T>(List<T> list)
		{
			Random random = new Random();
			int i = list.Count;
			while (i > 1)
			{
				i--;
				int num = random.Next(i + 1);
				T t = list[num];
				list[num] = list[i];
				list[i] = t;
			}
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00011768 File Offset: 0x0000F968
		private void RandomAppIcon_Tapped(object sender, TappedRoutedEventArgs e)
		{
			Border border = sender as Border;
			bool flag = border != null;
			if (flag)
			{
				StoreApp storeApp = border.DataContext as StoreApp;
				bool flag2 = storeApp != null;
				if (flag2)
				{
					Debug.WriteLine("Random app icon tapped: " + storeApp.Name);
					bool flag3 = !storeApp.Id.StartsWith("settings");
					if (flag3)
					{
						base.Frame.Navigate(typeof(AppDetailsPage), new AppDetailsNavigationData
						{
							App = storeApp,
							ShowReviews = true
						});
					}
				}
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x000117FC File Offset: 0x0000F9FC
		private T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
		{
			bool flag = parent == null;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
				for (int i = 0; i < childrenCount; i++)
				{
					DependencyObject child = VisualTreeHelper.GetChild(parent, i);
					bool flag2 = child is T && ((FrameworkElement)child).Name == childName;
					if (flag2)
					{
						return (T)((object)child);
					}
					T t = this.FindChild<T>(child, childName);
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

		// Token: 0x060002BC RID: 700 RVA: 0x000118A8 File Offset: 0x0000FAA8
		private void AppItem_Tapped(object sender, TappedRoutedEventArgs e)
		{
			Border border = sender as Border;
			bool flag = border != null;
			if (flag)
			{
				StoreApp storeApp = border.DataContext as StoreApp;
				bool flag2 = storeApp != null;
				if (flag2)
				{
					Debug.WriteLine("App tapped: " + storeApp.Name);
					bool flag3 = !storeApp.Id.StartsWith("settings");
					if (flag3)
					{
						base.Frame.Navigate(typeof(AppDetailsPage), new AppDetailsNavigationData
						{
							App = storeApp,
							ShowReviews = true
						});
					}
				}
			}
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0001193C File Offset: 0x0000FB3C
		[DebuggerStepThrough]
		private void ShowAboutDialog()
		{
			MainPage.<ShowAboutDialog>d__82 <ShowAboutDialog>d__ = new MainPage.<ShowAboutDialog>d__82();
			<ShowAboutDialog>d__.<>4__this = this;
			<ShowAboutDialog>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<ShowAboutDialog>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <ShowAboutDialog>d__.<>t__builder;
			<>t__builder.Start<MainPage.<ShowAboutDialog>d__82>(ref <ShowAboutDialog>d__);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00011978 File Offset: 0x0000FB78
		[DebuggerStepThrough]
		private void SubmitAppsButton_Click(object sender, RoutedEventArgs e)
		{
			MainPage.<SubmitAppsButton_Click>d__83 <SubmitAppsButton_Click>d__ = new MainPage.<SubmitAppsButton_Click>d__83();
			<SubmitAppsButton_Click>d__.<>4__this = this;
			<SubmitAppsButton_Click>d__.sender = sender;
			<SubmitAppsButton_Click>d__.e = e;
			<SubmitAppsButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<SubmitAppsButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <SubmitAppsButton_Click>d__.<>t__builder;
			<>t__builder.Start<MainPage.<SubmitAppsButton_Click>d__83>(ref <SubmitAppsButton_Click>d__);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00009F07 File Offset: 0x00008107
		private void CollectionsButton_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(Dependencies));
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x000119C2 File Offset: 0x0000FBC2
		private void CategoriesButton_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(SettingsPage));
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x000119DC File Offset: 0x0000FBDC
		[DebuggerStepThrough]
		private void AccountButton_Click(object sender, RoutedEventArgs e)
		{
			MainPage.<AccountButton_Click>d__86 <AccountButton_Click>d__ = new MainPage.<AccountButton_Click>d__86();
			<AccountButton_Click>d__.<>4__this = this;
			<AccountButton_Click>d__.sender = sender;
			<AccountButton_Click>d__.e = e;
			<AccountButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<AccountButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <AccountButton_Click>d__.<>t__builder;
			<>t__builder.Start<MainPage.<AccountButton_Click>d__86>(ref <AccountButton_Click>d__);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00011A28 File Offset: 0x0000FC28
		private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
		{
			Image image = sender as Image;
			bool flag = image != null;
			if (flag)
			{
				Debug.WriteLine("Image failed to load: " + e.ErrorMessage);
				try
				{
					Uri uri = new Uri("https://i.ibb.co/Tx7VKhhW/noapp.png");
					BitmapImage bitmapImage = new BitmapImage(uri);
					image.put_Source(bitmapImage);
				}
				catch (Exception ex)
				{
					Debug.WriteLine("Fallback image also failed: " + ex.Message);
				}
			}
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00011AA8 File Offset: 0x0000FCA8
		private void OrganizeAppsIntoSections()
		{
			this.staffPicksApps.Clear();
			this.featuredApps.Clear();
			this.moreApps.Clear();
			bool flag = this.allApps == null || this.allApps.Count == 0;
			if (flag)
			{
				Debug.WriteLine("No apps to organize");
			}
			else
			{
				Debug.WriteLine("Organizing " + this.allApps.Count + " apps into sections...");
				List<StoreApp> list = Enumerable.ToList<StoreApp>(Enumerable.Where<StoreApp>(this.allApps, (StoreApp app) => !app.Id.StartsWith("settings")));
				bool flag2 = list.Count == 0;
				if (flag2)
				{
					Debug.WriteLine("No valid apps found after filtering");
				}
				else
				{
					List<StoreApp> list2 = new List<StoreApp>(list);
					this.Shuffle<StoreApp>(list2);
					int num = Math.Min(10, list2.Count);
					for (int i = 0; i < num; i++)
					{
						this.staffPicksApps.Add(list2[i]);
						Debug.WriteLine(string.Format("Staff Picks ADDED: {0}", new object[]
						{
							list2[i].Name
						}));
					}
					List<StoreApp> list3 = Enumerable.ToList<StoreApp>(Enumerable.Where<StoreApp>(list, (StoreApp app) => !Enumerable.Any<StoreApp>(this.staffPicksApps, (StoreApp s) => s.Id == app.Id)));
					this.Shuffle<StoreApp>(list3);
					num = Math.Min(10, list3.Count);
					for (int j = 0; j < num; j++)
					{
						this.featuredApps.Add(list3[j]);
						Debug.WriteLine(string.Format("Featured ADDED: {0}", new object[]
						{
							list3[j].Name
						}));
					}
					List<StoreApp> list4 = Enumerable.ToList<StoreApp>(Enumerable.Where<StoreApp>(list, (StoreApp app) => !Enumerable.Any<StoreApp>(this.staffPicksApps, (StoreApp s) => s.Id == app.Id) && !Enumerable.Any<StoreApp>(this.featuredApps, (StoreApp f) => f.Id == app.Id)));
					this.Shuffle<StoreApp>(list4);
					num = Math.Min(10, list4.Count);
					for (int k = 0; k < num; k++)
					{
						this.moreApps.Add(list4[k]);
						Debug.WriteLine(string.Format("More Apps ADDED: {0}", new object[]
						{
							list4[k].Name
						}));
					}
					Debug.WriteLine(string.Format("Complete: Staff Picks={0}, Featured={1}, More Apps={2}", new object[]
					{
						this.staffPicksApps.Count,
						this.featuredApps.Count,
						this.moreApps.Count
					}));
				}
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00005867 File Offset: 0x00003A67
		private void TopAppsButton_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(AllAppsPage));
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00011D44 File Offset: 0x0000FF44
		[DebuggerStepThrough]
		public void RefreshData()
		{
			MainPage.<RefreshData>d__90 <RefreshData>d__ = new MainPage.<RefreshData>d__90();
			<RefreshData>d__.<>4__this = this;
			<RefreshData>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<RefreshData>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <RefreshData>d__.<>t__builder;
			<>t__builder.Start<MainPage.<RefreshData>d__90>(ref <RefreshData>d__);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00011D80 File Offset: 0x0000FF80
		private void StaffPicksButton_Click(object sender, TappedRoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(CollectionsViewPage), "Staff picks");
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00011DA0 File Offset: 0x0000FFA0
		private void SpotlightCollectionTile_Click(object sender, ItemClickEventArgs e)
		{
			try
			{
				AppCollection appCollection = e.ClickedItem as AppCollection;
				bool flag = appCollection != null;
				if (flag)
				{
					Debug.WriteLine(string.Format("Collection clicked: {0}", new object[]
					{
						appCollection.Name
					}));
					base.Frame.Navigate(typeof(CollectionsViewPage), appCollection.Name);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("Error in SpotlightCollectionTile_Click: {0}", new object[]
				{
					ex.Message
				}));
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00011E38 File Offset: 0x00010038
		private void NewRisingButton_Click(object sender, TappedRoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(CollectionsViewPage), "New & Rising");
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00011E56 File Offset: 0x00010056
		private void GamesButton_Click(object sender, TappedRoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(CollectionsViewPage), "Games");
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00011E74 File Offset: 0x00010074
		[DebuggerStepThrough]
		private void Border_Tapped(object sender, TappedRoutedEventArgs e)
		{
			MainPage.<Border_Tapped>d__95 <Border_Tapped>d__ = new MainPage.<Border_Tapped>d__95();
			<Border_Tapped>d__.<>4__this = this;
			<Border_Tapped>d__.sender = sender;
			<Border_Tapped>d__.e = e;
			<Border_Tapped>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<Border_Tapped>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <Border_Tapped>d__.<>t__builder;
			<>t__builder.Start<MainPage.<Border_Tapped>d__95>(ref <Border_Tapped>d__);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00011EC0 File Offset: 0x000100C0
		private void MSNNews_Tapped(object sender, TappedRoutedEventArgs e)
		{
			StoreApp app = new StoreApp
			{
				Id = "5",
				Name = "Opentracks Desktop Service",
				Publisher = "ChockingNetDude",
				Version = "0.3.0.0",
				DownloadUrl = "https://appxboxdl.dankassassin368.com/Windows%208.1%20APPX%20Files/Apps/OpenTracks%20Desktop/OpenTracksDesktop_B3.appx",
				IconUrl = "https://i.ibb.co/5W25dZmW/757de76ad56f84e8eb563b9629468167.png",
				Description = "Opentracks is a music service designed for multiple legacy old platform, and desktop version is here",
				Featured = false,
				ReviewStats = new ReviewStats
				{
					AverageRating = 0.0,
					ReviewCount = 0
				},
				Type = "App",
				Category = "Music"
			};
			base.Frame.Navigate(typeof(AppDetailsPage), new AppDetailsNavigationData
			{
				App = app,
				ShowReviews = true
			});
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00011F98 File Offset: 0x00010198
		private void OTS_Tapped(object sender, TappedRoutedEventArgs e)
		{
			StoreApp app = new StoreApp
			{
				Id = "525",
				Name = "Farming Simulator 14",
				Publisher = "GIANTS Software",
				Version = "1.3.0.1",
				DownloadUrl = "https://dl.dankassassin368.com/GIANTSSoftware.FarmingSimulator14_1.3.0.1_x86__fa8jxm5fj0esw.appx",
				IconUrl = "https://dankassassin368.com/random/icons/cec30eaebc9dec6547d57074767b5e7a.png",
				Description = "Start your agricultural career in Farming Simulator 14 on mobile and tablet! Take control of your farm and its fields to fulfill your harvesting dreams.    As well as a refined look and feel, Farming Simulator 14 gives you double the number of farm machines to control, all authentically modeled on equipment from real agricultural manufacturers, including Case IH, Deutz-Fahr, Lamborghini, Kuhn, Amazone and Krone.",
				Featured = true,
				ReviewStats = new ReviewStats
				{
					AverageRating = 0.0,
					ReviewCount = 0
				},
				Type = "Game",
				Category = "Simulation"
			};
			base.Frame.Navigate(typeof(AppDetailsPage), new AppDetailsNavigationData
			{
				App = app,
				ShowReviews = true
			});
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00012070 File Offset: 0x00010270
		private void Fresh_Tapped(object sender, TappedRoutedEventArgs e)
		{
			StoreApp app = new StoreApp
			{
				Id = "645",
				Name = "LivelyText (Beta)",
				Publisher = "ChockingNetDude, ExceptionError102",
				Version = "8.6.3.1",
				DownloadUrl = "https://dl.dankassassin368.com/LiveTileApp_9.0.0.5_AnyCPU_Debug.appx",
				IconUrl = "https://dankassassin368.com/random/icons/5b5c77f51a415ca6f5c4c6f5d2016398.png",
				Description = "Set any text you want on lively text! we support multiple types of text from emoji to textmojis!",
				Featured = true,
				ReviewStats = new ReviewStats
				{
					AverageRating = 0.0,
					ReviewCount = 0
				},
				Type = "App",
				Category = "Photo + Design"
			};
			base.Frame.Navigate(typeof(AppDetailsPage), new AppDetailsNavigationData
			{
				App = app,
				ShowReviews = true
			});
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00012148 File Offset: 0x00010348
		private void Accu_Tapped(object sender, TappedRoutedEventArgs e)
		{
			StoreApp app = new StoreApp
			{
				Id = "301",
				Name = "Nokia Music",
				Publisher = "Nokia Corporation",
				Version = "1.4.0.5133",
				DownloadUrl = "https://dl.dankassassin368.com/NokiaCorporation.NokiaMusic_1.4.0.5133_x86__6d0q6r3z979nw.appx",
				IconUrl = "https://i.ibb.co/JFKT4qsB/f805998960fd1cd932df67b345a573d1.png",
				Description = "Use Nokia Music app on your Windows 8 device to listen to the newest hits, find local gigs and even play your music stored in your Music folder!\r\nExcept all online things, app works perfectly fine for local music. App is x86.",
				Featured = false,
				ReviewStats = new ReviewStats
				{
					AverageRating = 0.0,
					ReviewCount = 0
				},
				Type = "App",
				Category = "Music + Audio"
			};
			base.Frame.Navigate(typeof(AppDetailsPage), new AppDetailsNavigationData
			{
				App = app,
				ShowReviews = true
			});
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0001221F File Offset: 0x0001041F
		private void Border_Tapped_1(object sender, TappedRoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(AllAppsPage1));
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00012238 File Offset: 0x00010438
		[DebuggerStepThrough]
		public void CheckForUpdatesManually()
		{
			MainPage.<CheckForUpdatesManually>d__101 <CheckForUpdatesManually>d__ = new MainPage.<CheckForUpdatesManually>d__101();
			<CheckForUpdatesManually>d__.<>4__this = this;
			<CheckForUpdatesManually>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<CheckForUpdatesManually>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <CheckForUpdatesManually>d__.<>t__builder;
			<>t__builder.Start<MainPage.<CheckForUpdatesManually>d__101>(ref <CheckForUpdatesManually>d__);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00012274 File Offset: 0x00010474
		[DebuggerStepThrough]
		protected override void OnNavigatedFrom(NavigationEventArgs e)
		{
			MainPage.<OnNavigatedFrom>d__102 <OnNavigatedFrom>d__ = new MainPage.<OnNavigatedFrom>d__102();
			<OnNavigatedFrom>d__.<>4__this = this;
			<OnNavigatedFrom>d__.e = e;
			<OnNavigatedFrom>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<OnNavigatedFrom>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <OnNavigatedFrom>d__.<>t__builder;
			<>t__builder.Start<MainPage.<OnNavigatedFrom>d__102>(ref <OnNavigatedFrom>d__);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x000122B8 File Offset: 0x000104B8
		[DebuggerStepThrough]
		private Task SaveCacheState()
		{
			MainPage.<SaveCacheState>d__103 <SaveCacheState>d__ = new MainPage.<SaveCacheState>d__103();
			<SaveCacheState>d__.<>4__this = this;
			<SaveCacheState>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SaveCacheState>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <SaveCacheState>d__.<>t__builder;
			<>t__builder.Start<MainPage.<SaveCacheState>d__103>(ref <SaveCacheState>d__);
			return <SaveCacheState>d__.<>t__builder.Task;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00012300 File Offset: 0x00010500
		public void CancelJsonLoading()
		{
			try
			{
				bool flag = this._jsonLoadingCancellationTokenSource != null && !this._jsonLoadingCancellationTokenSource.IsCancellationRequested;
				if (flag)
				{
					this._jsonLoadingCancellationTokenSource.Cancel();
					Debug.WriteLine("JSON loading cancelled");
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error cancelling JSON loading: " + ex.Message);
			}
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00012374 File Offset: 0x00010574
		[DebuggerStepThrough]
		private Task CleanupResourcesAsync()
		{
			MainPage.<CleanupResourcesAsync>d__106 <CleanupResourcesAsync>d__ = new MainPage.<CleanupResourcesAsync>d__106();
			<CleanupResourcesAsync>d__.<>4__this = this;
			<CleanupResourcesAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<CleanupResourcesAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <CleanupResourcesAsync>d__.<>t__builder;
			<>t__builder.Start<MainPage.<CleanupResourcesAsync>d__106>(ref <CleanupResourcesAsync>d__);
			return <CleanupResourcesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x000123BB File Offset: 0x000105BB
		private void DisposeHttpClients()
		{
			GC.Collect();
			GC.WaitForPendingFinalizers();
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x000123CC File Offset: 0x000105CC
		[DebuggerStepThrough]
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			MainPage.<OnNavigatedTo>d__108 <OnNavigatedTo>d__ = new MainPage.<OnNavigatedTo>d__108();
			<OnNavigatedTo>d__.<>4__this = this;
			<OnNavigatedTo>d__.e = e;
			<OnNavigatedTo>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<OnNavigatedTo>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <OnNavigatedTo>d__.<>t__builder;
			<>t__builder.Start<MainPage.<OnNavigatedTo>d__108>(ref <OnNavigatedTo>d__);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00012410 File Offset: 0x00010610
		[DebuggerStepThrough]
		private Task LoadAppsFromJson()
		{
			MainPage.<LoadAppsFromJson>d__109 <LoadAppsFromJson>d__ = new MainPage.<LoadAppsFromJson>d__109();
			<LoadAppsFromJson>d__.<>4__this = this;
			<LoadAppsFromJson>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadAppsFromJson>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadAppsFromJson>d__.<>t__builder;
			<>t__builder.Start<MainPage.<LoadAppsFromJson>d__109>(ref <LoadAppsFromJson>d__);
			return <LoadAppsFromJson>d__.<>t__builder.Task;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00012458 File Offset: 0x00010658
		[DebuggerStepThrough]
		private Task UpdateUIWithPreloadedData()
		{
			MainPage.<UpdateUIWithPreloadedData>d__110 <UpdateUIWithPreloadedData>d__ = new MainPage.<UpdateUIWithPreloadedData>d__110();
			<UpdateUIWithPreloadedData>d__.<>4__this = this;
			<UpdateUIWithPreloadedData>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateUIWithPreloadedData>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <UpdateUIWithPreloadedData>d__.<>t__builder;
			<>t__builder.Start<MainPage.<UpdateUIWithPreloadedData>d__110>(ref <UpdateUIWithPreloadedData>d__);
			return <UpdateUIWithPreloadedData>d__.<>t__builder.Task;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x000124A0 File Offset: 0x000106A0
		[DebuggerStepThrough]
		private Task RestoreCacheState()
		{
			MainPage.<RestoreCacheState>d__111 <RestoreCacheState>d__ = new MainPage.<RestoreCacheState>d__111();
			<RestoreCacheState>d__.<>4__this = this;
			<RestoreCacheState>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<RestoreCacheState>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <RestoreCacheState>d__.<>t__builder;
			<>t__builder.Start<MainPage.<RestoreCacheState>d__111>(ref <RestoreCacheState>d__);
			return <RestoreCacheState>d__.<>t__builder.Task;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x000124E8 File Offset: 0x000106E8
		[DebuggerStepThrough]
		private Task LoadSpotlightCollections()
		{
			MainPage.<LoadSpotlightCollections>d__112 <LoadSpotlightCollections>d__ = new MainPage.<LoadSpotlightCollections>d__112();
			<LoadSpotlightCollections>d__.<>4__this = this;
			<LoadSpotlightCollections>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadSpotlightCollections>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadSpotlightCollections>d__.<>t__builder;
			<>t__builder.Start<MainPage.<LoadSpotlightCollections>d__112>(ref <LoadSpotlightCollections>d__);
			return <LoadSpotlightCollections>d__.<>t__builder.Task;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00012530 File Offset: 0x00010730
		private List<string> GetRandomIcons(int count, string category = null)
		{
			List<string> list = new List<string>();
			Random random = new Random();
			bool flag = this.allApps != null && this.allApps.Count > 0;
			if (flag)
			{
				IEnumerable<StoreApp> enumerable = this.allApps;
				bool flag2 = !string.IsNullOrEmpty(category);
				if (flag2)
				{
					enumerable = Enumerable.Where<StoreApp>(this.allApps, (StoreApp a) => !string.IsNullOrEmpty(a.Category) && a.Category.Equals(category, 5));
					Debug.WriteLine(string.Format("Filtering by category '{0}': {1} apps found", new object[]
					{
						category,
						Enumerable.Count<StoreApp>(enumerable)
					}));
				}
				List<StoreApp> list2 = Enumerable.ToList<StoreApp>(Enumerable.Where<StoreApp>(enumerable, (StoreApp a) => !string.IsNullOrEmpty(a.IconUrl)));
				bool flag3 = list2.Count > 0;
				if (flag3)
				{
					List<StoreApp> list3 = Enumerable.ToList<StoreApp>(Enumerable.Take<StoreApp>(Enumerable.OrderBy<StoreApp, int>(list2, (StoreApp x) => random.Next()), Math.Min(count, list2.Count)));
					foreach (StoreApp storeApp in list3)
					{
						list.Add(storeApp.IconUrl);
					}
					Debug.WriteLine(string.Format("Got {0} icons for category '{1}'", new object[]
					{
						list.Count,
						category ?? "all"
					}));
				}
				else
				{
					Debug.WriteLine(string.Format("No apps with icons found for category '{0}'", new object[]
					{
						category ?? "all"
					}));
				}
			}
			while (list.Count < count)
			{
				list.Add("https://cdn-icons-png.flaticon.com/512/149/149452.png");
			}
			return list;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00012724 File Offset: 0x00010924
		private void FindAndSetCollectionsGridView()
		{
			try
			{
				GridView gridView = this.FindVisualChild<GridView>(this, "SpotlightCollectionsGridView");
				bool flag = gridView != null;
				if (flag)
				{
					gridView.put_ItemsSource(this.spotlightCollections);
					Debug.WriteLine("Found GridView via FindVisualChild and set items");
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("Error in FindAndSetCollectionsGridView: {0}", new object[]
				{
					ex.Message
				}));
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0001279C File Offset: 0x0001099C
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

		// Token: 0x060002DE RID: 734 RVA: 0x00012844 File Offset: 0x00010A44
		private void OnCommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
		{
			args.Request.ApplicationCommands.Clear();
			args.Request.ApplicationCommands.Add(new SettingsCommand("downloads", MainPage.Localized("MyDownloads", new object[0]), delegate(IUICommand cmd)
			{
				Frame frame = Window.Current.Content as Frame;
				bool flag = frame != null;
				if (flag)
				{
					frame.Navigate(typeof(DownloadsHub));
				}
			}));
			args.Request.ApplicationCommands.Add(new SettingsCommand("general", MainPage.Localized("GeneralCharm", new object[0]), delegate(IUICommand cmd)
			{
				this.OpenSettingsFlyout(typeof(GeneralSettingsFlyout));
			}));
			args.Request.ApplicationCommands.Add(new SettingsCommand("8Store Recovery", MainPage.Localized("RecoveryCharm", new object[0]), delegate(IUICommand cmd)
			{
				this.OpenSettingsFlyout(typeof(FactoryResetFlyout));
			}));
			args.Request.ApplicationCommands.Add(new SettingsCommand("language", MainPage.Localized("LanguageCharm", new object[0]), delegate(IUICommand cmd)
			{
				this.OpenSettingsFlyout(typeof(LanguageSettingsFlyout));
			}));
			args.Request.ApplicationCommands.Add(new SettingsCommand("about", MainPage.Localized("AboutCharm", new object[0]), delegate(IUICommand cmd)
			{
				this.OpenSettingsFlyout(typeof(AboutSettingsFlyout));
			}));
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0001298C File Offset: 0x00010B8C
		private void OpenSettingsFlyout(Type flyoutType)
		{
			SettingsFlyout settingsFlyout = (SettingsFlyout)Activator.CreateInstance(flyoutType);
			settingsFlyout.Show();
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x000129B0 File Offset: 0x00010BB0
		[DebuggerStepThrough]
		private Task LoadAccountDataAsync()
		{
			MainPage.<LoadAccountDataAsync>d__118 <LoadAccountDataAsync>d__ = new MainPage.<LoadAccountDataAsync>d__118();
			<LoadAccountDataAsync>d__.<>4__this = this;
			<LoadAccountDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadAccountDataAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadAccountDataAsync>d__.<>t__builder;
			<>t__builder.Start<MainPage.<LoadAccountDataAsync>d__118>(ref <LoadAccountDataAsync>d__);
			return <LoadAccountDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x000129F8 File Offset: 0x00010BF8
		[DebuggerStepThrough]
		public Task UpdateAccountUIAsync()
		{
			MainPage.<UpdateAccountUIAsync>d__119 <UpdateAccountUIAsync>d__ = new MainPage.<UpdateAccountUIAsync>d__119();
			<UpdateAccountUIAsync>d__.<>4__this = this;
			<UpdateAccountUIAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateAccountUIAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <UpdateAccountUIAsync>d__.<>t__builder;
			<>t__builder.Start<MainPage.<UpdateAccountUIAsync>d__119>(ref <UpdateAccountUIAsync>d__);
			return <UpdateAccountUIAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00012A40 File Offset: 0x00010C40
		private void AccountLoginButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				Debug.WriteLine("[MainPage] Opening Login Flyout");
				LoginFlyout loginFlyout = new LoginFlyout();
				loginFlyout.Show();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("[MainPage] Error opening login flyout: {0}", new object[]
				{
					ex.Message
				}));
			}
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00012AA0 File Offset: 0x00010CA0
		private void AccountRegisterButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				Debug.WriteLine("[MainPage] Opening Register Flyout");
				RegisterFlyout registerFlyout = new RegisterFlyout();
				registerFlyout.Show();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("[MainPage] Error opening register flyout: {0}", new object[]
				{
					ex.Message
				}));
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00012B00 File Offset: 0x00010D00
		[DebuggerStepThrough]
		private void AccountLogoutButton_Click(object sender, RoutedEventArgs e)
		{
			MainPage.<AccountLogoutButton_Click>d__122 <AccountLogoutButton_Click>d__ = new MainPage.<AccountLogoutButton_Click>d__122();
			<AccountLogoutButton_Click>d__.<>4__this = this;
			<AccountLogoutButton_Click>d__.sender = sender;
			<AccountLogoutButton_Click>d__.e = e;
			<AccountLogoutButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<AccountLogoutButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <AccountLogoutButton_Click>d__.<>t__builder;
			<>t__builder.Start<MainPage.<AccountLogoutButton_Click>d__122>(ref <AccountLogoutButton_Click>d__);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00012B4C File Offset: 0x00010D4C
		private void ShowAccountLoginPopup()
		{
			MainPage.<>c__DisplayClass123_0 CS$<>8__locals1 = new MainPage.<>c__DisplayClass123_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.popup = new Popup();
			CS$<>8__locals1.popup.put_VerticalOffset(100.0);
			CS$<>8__locals1.popup.put_HorizontalOffset(100.0);
			CS$<>8__locals1.popup.put_IsLightDismissEnabled(true);
			Grid grid = new Grid();
			grid.put_Background(new SolidColorBrush(Colors.White));
			grid.put_Width(400.0);
			grid.put_Margin(new Thickness(20.0));
			StackPanel stackPanel = new StackPanel();
			stackPanel.put_Margin(new Thickness(10.0));
			TextBlock textBlock = new TextBlock();
			textBlock.put_Text("Sign In");
			textBlock.put_FontSize(24.0);
			textBlock.put_FontWeight(FontWeights.Bold);
			textBlock.put_Margin(new Thickness(0.0, 0.0, 0.0, 20.0));
			CS$<>8__locals1.emailBox = new TextBox();
			CS$<>8__locals1.emailBox.put_PlaceholderText("Email");
			CS$<>8__locals1.emailBox.put_Width(300.0);
			CS$<>8__locals1.emailBox.put_Margin(new Thickness(0.0, 0.0, 0.0, 10.0));
			CS$<>8__locals1.passwordBox = new PasswordBox();
			CS$<>8__locals1.passwordBox.put_PlaceholderText("Password");
			CS$<>8__locals1.passwordBox.put_Width(300.0);
			CS$<>8__locals1.passwordBox.put_Margin(new Thickness(0.0, 0.0, 0.0, 20.0));
			StackPanel stackPanel2 = new StackPanel();
			stackPanel2.put_Orientation(1);
			stackPanel2.put_HorizontalAlignment(2);
			Button button = new Button();
			button.put_Content("Sign In");
			button.put_Background(new SolidColorBrush(Color.FromArgb(byte.MaxValue, 0, 170, 79)));
			button.put_Foreground(new SolidColorBrush(Colors.White));
			button.put_Padding(new Thickness(20.0, 10.0, 20.0, 10.0));
			button.put_Margin(new Thickness(5.0, 0.0, 0.0, 0.0));
			Button button2 = button;
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(button2.add_Click), new Action<EventRegistrationToken>(button2.remove_Click), delegate(object s, RoutedEventArgs args)
			{
				MainPage.<>c__DisplayClass123_0.<<ShowAccountLoginPopup>b__0>d <<ShowAccountLoginPopup>b__0>d = new MainPage.<>c__DisplayClass123_0.<<ShowAccountLoginPopup>b__0>d();
				<<ShowAccountLoginPopup>b__0>d.<>4__this = CS$<>8__locals1;
				<<ShowAccountLoginPopup>b__0>d.s = s;
				<<ShowAccountLoginPopup>b__0>d.args = args;
				<<ShowAccountLoginPopup>b__0>d.<>t__builder = AsyncVoidMethodBuilder.Create();
				<<ShowAccountLoginPopup>b__0>d.<>1__state = -1;
				AsyncVoidMethodBuilder <>t__builder = <<ShowAccountLoginPopup>b__0>d.<>t__builder;
				<>t__builder.Start<MainPage.<>c__DisplayClass123_0.<<ShowAccountLoginPopup>b__0>d>(ref <<ShowAccountLoginPopup>b__0>d);
			});
			Button button3 = new Button();
			button3.put_Content("Cancel");
			button3.put_Background(new SolidColorBrush(Colors.Transparent));
			button3.put_Foreground(new SolidColorBrush(Colors.Black));
			button3.put_BorderBrush(new SolidColorBrush(Colors.Gray));
			button3.put_BorderThickness(new Thickness(1.0));
			button3.put_Padding(new Thickness(20.0, 10.0, 20.0, 10.0));
			button2 = button3;
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(button2.add_Click), new Action<EventRegistrationToken>(button2.remove_Click), delegate(object s, RoutedEventArgs args)
			{
				CS$<>8__locals1.popup.put_IsOpen(false);
			});
			stackPanel2.Children.Add(button3);
			stackPanel2.Children.Add(button);
			stackPanel.Children.Add(textBlock);
			stackPanel.Children.Add(CS$<>8__locals1.emailBox);
			stackPanel.Children.Add(CS$<>8__locals1.passwordBox);
			stackPanel.Children.Add(stackPanel2);
			grid.Children.Add(stackPanel);
			CS$<>8__locals1.popup.put_Child(grid);
			CS$<>8__locals1.popup.put_IsOpen(true);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00012F64 File Offset: 0x00011164
		[DebuggerStepThrough]
		private Task ProcessAccountLogin(string email, string password)
		{
			MainPage.<ProcessAccountLogin>d__124 <ProcessAccountLogin>d__ = new MainPage.<ProcessAccountLogin>d__124();
			<ProcessAccountLogin>d__.<>4__this = this;
			<ProcessAccountLogin>d__.email = email;
			<ProcessAccountLogin>d__.password = password;
			<ProcessAccountLogin>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ProcessAccountLogin>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <ProcessAccountLogin>d__.<>t__builder;
			<>t__builder.Start<MainPage.<ProcessAccountLogin>d__124>(ref <ProcessAccountLogin>d__);
			return <ProcessAccountLogin>d__.<>t__builder.Task;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00012FBC File Offset: 0x000111BC
		[DebuggerStepThrough]
		private Task ProcessAccountRegister(string email, string username, string password, string confirmPassword)
		{
			MainPage.<ProcessAccountRegister>d__125 <ProcessAccountRegister>d__ = new MainPage.<ProcessAccountRegister>d__125();
			<ProcessAccountRegister>d__.<>4__this = this;
			<ProcessAccountRegister>d__.email = email;
			<ProcessAccountRegister>d__.username = username;
			<ProcessAccountRegister>d__.password = password;
			<ProcessAccountRegister>d__.confirmPassword = confirmPassword;
			<ProcessAccountRegister>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ProcessAccountRegister>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <ProcessAccountRegister>d__.<>t__builder;
			<>t__builder.Start<MainPage.<ProcessAccountRegister>d__125>(ref <ProcessAccountRegister>d__);
			return <ProcessAccountRegister>d__.<>t__builder.Task;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00013020 File Offset: 0x00011220
		private void StaffPicksItem_Click(object sender, ItemClickEventArgs e)
		{
			StoreApp storeApp = e.ClickedItem as StoreApp;
			bool flag = storeApp != null;
			if (flag)
			{
				base.Frame.Navigate(typeof(AppDetailsPage), storeApp);
			}
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00005867 File Offset: 0x00003A67
		private void ViewAllStaffPicksButton_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(AllAppsPage));
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0001305C File Offset: 0x0001125C
		private void ShowAccountRegisterPopup()
		{
			MainPage.<>c__DisplayClass128_0 CS$<>8__locals1 = new MainPage.<>c__DisplayClass128_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.popup = new Popup();
			CS$<>8__locals1.popup.put_VerticalOffset(100.0);
			CS$<>8__locals1.popup.put_HorizontalOffset(100.0);
			CS$<>8__locals1.popup.put_IsLightDismissEnabled(true);
			Grid grid = new Grid();
			grid.put_Background(new SolidColorBrush(Colors.White));
			grid.put_Width(400.0);
			grid.put_Margin(new Thickness(20.0));
			StackPanel stackPanel = new StackPanel();
			stackPanel.put_Margin(new Thickness(10.0));
			TextBlock textBlock = new TextBlock();
			textBlock.put_Text("Create Account");
			textBlock.put_FontSize(24.0);
			textBlock.put_FontWeight(FontWeights.Bold);
			textBlock.put_Margin(new Thickness(0.0, 0.0, 0.0, 20.0));
			CS$<>8__locals1.emailBox = new TextBox();
			CS$<>8__locals1.emailBox.put_PlaceholderText("Email");
			CS$<>8__locals1.emailBox.put_Width(300.0);
			CS$<>8__locals1.emailBox.put_Margin(new Thickness(0.0, 0.0, 0.0, 10.0));
			CS$<>8__locals1.usernameBox = new TextBox();
			CS$<>8__locals1.usernameBox.put_PlaceholderText("Username");
			CS$<>8__locals1.usernameBox.put_Width(300.0);
			CS$<>8__locals1.usernameBox.put_Margin(new Thickness(0.0, 0.0, 0.0, 10.0));
			CS$<>8__locals1.passwordBox = new PasswordBox();
			CS$<>8__locals1.passwordBox.put_PlaceholderText("Password");
			CS$<>8__locals1.passwordBox.put_Width(300.0);
			CS$<>8__locals1.passwordBox.put_Margin(new Thickness(0.0, 0.0, 0.0, 10.0));
			CS$<>8__locals1.confirmPasswordBox = new PasswordBox();
			CS$<>8__locals1.confirmPasswordBox.put_PlaceholderText("Confirm Password");
			CS$<>8__locals1.confirmPasswordBox.put_Width(300.0);
			CS$<>8__locals1.confirmPasswordBox.put_Margin(new Thickness(0.0, 0.0, 0.0, 20.0));
			StackPanel stackPanel2 = new StackPanel();
			stackPanel2.put_Orientation(1);
			stackPanel2.put_HorizontalAlignment(2);
			Button button = new Button();
			button.put_Content("Sign Up");
			button.put_Background(new SolidColorBrush(Color.FromArgb(byte.MaxValue, 0, 170, 79)));
			button.put_Foreground(new SolidColorBrush(Colors.White));
			button.put_Padding(new Thickness(20.0, 10.0, 20.0, 10.0));
			button.put_Margin(new Thickness(5.0, 0.0, 0.0, 0.0));
			Button button2 = button;
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(button2.add_Click), new Action<EventRegistrationToken>(button2.remove_Click), delegate(object s, RoutedEventArgs args)
			{
				MainPage.<>c__DisplayClass128_0.<<ShowAccountRegisterPopup>b__0>d <<ShowAccountRegisterPopup>b__0>d = new MainPage.<>c__DisplayClass128_0.<<ShowAccountRegisterPopup>b__0>d();
				<<ShowAccountRegisterPopup>b__0>d.<>4__this = CS$<>8__locals1;
				<<ShowAccountRegisterPopup>b__0>d.s = s;
				<<ShowAccountRegisterPopup>b__0>d.args = args;
				<<ShowAccountRegisterPopup>b__0>d.<>t__builder = AsyncVoidMethodBuilder.Create();
				<<ShowAccountRegisterPopup>b__0>d.<>1__state = -1;
				AsyncVoidMethodBuilder <>t__builder = <<ShowAccountRegisterPopup>b__0>d.<>t__builder;
				<>t__builder.Start<MainPage.<>c__DisplayClass128_0.<<ShowAccountRegisterPopup>b__0>d>(ref <<ShowAccountRegisterPopup>b__0>d);
			});
			Button button3 = new Button();
			button3.put_Content("Cancel");
			button3.put_Background(new SolidColorBrush(Colors.Transparent));
			button3.put_Foreground(new SolidColorBrush(Colors.Black));
			button3.put_BorderBrush(new SolidColorBrush(Colors.Gray));
			button3.put_BorderThickness(new Thickness(1.0));
			button3.put_Padding(new Thickness(20.0, 10.0, 20.0, 10.0));
			button2 = button3;
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(button2.add_Click), new Action<EventRegistrationToken>(button2.remove_Click), delegate(object s, RoutedEventArgs args)
			{
				CS$<>8__locals1.popup.put_IsOpen(false);
			});
			stackPanel2.Children.Add(button3);
			stackPanel2.Children.Add(button);
			stackPanel.Children.Add(textBlock);
			stackPanel.Children.Add(CS$<>8__locals1.emailBox);
			stackPanel.Children.Add(CS$<>8__locals1.usernameBox);
			stackPanel.Children.Add(CS$<>8__locals1.passwordBox);
			stackPanel.Children.Add(CS$<>8__locals1.confirmPasswordBox);
			stackPanel.Children.Add(stackPanel2);
			grid.Children.Add(stackPanel);
			CS$<>8__locals1.popup.put_Child(grid);
			CS$<>8__locals1.popup.put_IsOpen(true);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00013564 File Offset: 0x00011764
		[DebuggerStepThrough]
		private Task ShowMessageAsync(string message, string title)
		{
			MainPage.<ShowMessageAsync>d__129 <ShowMessageAsync>d__ = new MainPage.<ShowMessageAsync>d__129();
			<ShowMessageAsync>d__.<>4__this = this;
			<ShowMessageAsync>d__.message = message;
			<ShowMessageAsync>d__.title = title;
			<ShowMessageAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ShowMessageAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <ShowMessageAsync>d__.<>t__builder;
			<>t__builder.Start<MainPage.<ShowMessageAsync>d__129>(ref <ShowMessageAsync>d__);
			return <ShowMessageAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0001221F File Offset: 0x0001041F
		private void DownloadHubButton_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(AllAppsPage1));
		}

		// Token: 0x060002ED RID: 749 RVA: 0x000135BC File Offset: 0x000117BC
		private void UpdateTile()
		{
			string text = "<tile><visual lang='en' version='2'><binding template='TileSquare150x150PeekImageAndText04' branding='name'><image id='1' src='ms-appx:///Assets/Store.png'/><text id='1'>AccuWeather</text></binding><binding template='TileWide310x150SmallImageAndText02' branding='logo'><image id='1' src='ms-appx:///Assets/Store.png'/><text id='1'>AccuWeather</text><text id='2'>Free   4.3  42</text></binding><binding template='TileSquare310x310SmallImageAndText01' branding='logo'><image id='1' src='ms-appx:///Assets/Store.png'/><text id='1'>AccuWeather</text><text id='2'>View weather with accuracy</text><text id='3'>Free   4.3  42</text></binding></visual></tile>";
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(text);
			TileUpdater tileUpdater = TileUpdateManager.CreateTileUpdaterForApplication();
			tileUpdater.Clear();
			tileUpdater.EnableNotificationQueue(false);
			TileNotification tileNotification = new TileNotification(xmlDocument);
			tileUpdater.Update(tileNotification);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00013604 File Offset: 0x00011804
		private void FlipView_PointerEntered(object sender, PointerRoutedEventArgs e)
		{
			FlipView flipView = sender as FlipView;
			bool flag = flipView == null;
			if (!flag)
			{
				DispatcherTimer dispatcherTimer;
				bool flag2 = this._timers.TryGetValue(flipView, ref dispatcherTimer);
				if (flag2)
				{
					dispatcherTimer.Stop();
				}
			}
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0001363C File Offset: 0x0001183C
		private void FlipView_PointerExited(object sender, PointerRoutedEventArgs e)
		{
			FlipView flipView = sender as FlipView;
			bool flag = flipView == null;
			if (!flag)
			{
				DispatcherTimer dispatcherTimer;
				bool flag2 = this._timers.TryGetValue(flipView, ref dispatcherTimer);
				if (flag2)
				{
					dispatcherTimer.Start();
				}
			}
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00013674 File Offset: 0x00011874
		private void FlipView_Loaded(object sender, RoutedEventArgs e)
		{
			FlipView flip = sender as FlipView;
			bool flag = flip == null;
			if (!flag)
			{
				bool flag2 = this._timers.ContainsKey(flip);
				if (!flag2)
				{
					DispatcherTimer dispatcherTimer = new DispatcherTimer();
					dispatcherTimer.put_Interval(TimeSpan.FromSeconds(4.0));
					DispatcherTimer dispatcherTimer2 = dispatcherTimer;
					WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(dispatcherTimer2.add_Tick), new Action<EventRegistrationToken>(dispatcherTimer2.remove_Tick), delegate(object s, object args)
					{
						bool flag3 = flip.Items.Count <= 1;
						if (!flag3)
						{
							int num = flip.SelectedIndex + 1;
							bool flag4 = num >= flip.Items.Count;
							if (flag4)
							{
								num = 0;
							}
							flip.put_SelectedIndex(num);
						}
					});
					this._timers.Add(flip, dispatcherTimer);
					dispatcherTimer.Start();
				}
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00013720 File Offset: 0x00011920
		private void FlipView_Unloaded(object sender, RoutedEventArgs e)
		{
			FlipView flipView = sender as FlipView;
			bool flag = flipView == null;
			if (!flag)
			{
				DispatcherTimer dispatcherTimer;
				bool flag2 = this._timers.TryGetValue(flipView, ref dispatcherTimer);
				if (flag2)
				{
					dispatcherTimer.Stop();
					this._timers.Remove(flipView);
				}
			}
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00005880 File Offset: 0x00003A80
		private void TopAppsButton_Click_1(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(PopularPage));
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x000058B2 File Offset: 0x00003AB2
		private void ExclusiveHubButton_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(AccountPage));
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00013768 File Offset: 0x00011968
		private void StaffPickApp_Tapped(object sender, TappedRoutedEventArgs e)
		{
			Border border = sender as Border;
			bool flag = border != null;
			if (flag)
			{
				StoreApp storeApp = border.DataContext as StoreApp;
				bool flag2 = storeApp != null;
				if (flag2)
				{
					Debug.WriteLine("Staff pick app tapped: " + storeApp.Name);
					bool flag3 = !storeApp.Id.StartsWith("settings");
					if (flag3)
					{
						base.Frame.Navigate(typeof(AppDetailsPage), new AppDetailsNavigationData
						{
							App = storeApp,
							ShowReviews = true
						});
					}
				}
			}
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0001221F File Offset: 0x0001041F
		private void ViewAllStaffPicks_Tapped(object sender, TappedRoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(AllAppsPage1));
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x000137F9 File Offset: 0x000119F9
		private void ViewAllCollectionsButton_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(CollectionsViewPage));
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00005867 File Offset: 0x00003A67
		private void ViewAllFeaturedButton_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(AllAppsPage));
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00013814 File Offset: 0x00011A14
		private void FeaturedItem_Click(object sender, ItemClickEventArgs e)
		{
			StoreApp storeApp = e.ClickedItem as StoreApp;
			bool flag = storeApp != null;
			if (flag)
			{
				base.Frame.Navigate(typeof(AppDetailsPage), storeApp);
			}
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00013850 File Offset: 0x00011A50
		[DebuggerStepThrough]
		public Task RefreshCacheAsync()
		{
			MainPage.<RefreshCacheAsync>d__143 <RefreshCacheAsync>d__ = new MainPage.<RefreshCacheAsync>d__143();
			<RefreshCacheAsync>d__.<>4__this = this;
			<RefreshCacheAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<RefreshCacheAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <RefreshCacheAsync>d__.<>t__builder;
			<>t__builder.Start<MainPage.<RefreshCacheAsync>d__143>(ref <RefreshCacheAsync>d__);
			return <RefreshCacheAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00013898 File Offset: 0x00011A98
		[DebuggerStepThrough]
		public Task ClearCacheAsync()
		{
			MainPage.<ClearCacheAsync>d__144 <ClearCacheAsync>d__ = new MainPage.<ClearCacheAsync>d__144();
			<ClearCacheAsync>d__.<>4__this = this;
			<ClearCacheAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ClearCacheAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <ClearCacheAsync>d__.<>t__builder;
			<>t__builder.Start<MainPage.<ClearCacheAsync>d__144>(ref <ClearCacheAsync>d__);
			return <ClearCacheAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x000138DF File Offset: 0x00011ADF
		private void Image_Tapped(object sender, TappedRoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(InstalledAppsPage));
		}

		// Token: 0x060002FC RID: 764 RVA: 0x000138F8 File Offset: 0x00011AF8
		private void SelectRandomAppsForMoreSection(List<StoreApp> validApps)
		{
			try
			{
				HashSet<string> usedAppIds = new HashSet<string>();
				foreach (StoreApp storeApp in this.specialPicksApps)
				{
					usedAppIds.Add(storeApp.Id);
				}
				foreach (StoreApp storeApp2 in this.featuredApps)
				{
					usedAppIds.Add(storeApp2.Id);
				}
				List<StoreApp> list = Enumerable.ToList<StoreApp>(Enumerable.Where<StoreApp>(validApps, (StoreApp app) => !usedAppIds.Contains(app.Id)));
				bool flag = list.Count == 0;
				if (flag)
				{
					Debug.WriteLine("No available apps for More Apps section");
				}
				else
				{
					List<StoreApp> list2 = new List<StoreApp>(list);
					this.Shuffle<StoreApp>(list2);
					int num = Math.Min(10, list2.Count);
					for (int i = 0; i < num; i++)
					{
						this.moreApps.Add(list2[i]);
						Debug.WriteLine(string.Format("More Apps ADDED: {0}", new object[]
						{
							list2[i].Name
						}));
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("Error selecting random apps: {0}", new object[]
				{
					ex.Message
				}));
			}
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00005867 File Offset: 0x00003A67
		private void ViewAllMoreAppsButton_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(AllAppsPage));
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00013ABC File Offset: 0x00011CBC
		private void MoreAppsItem_Click(object sender, ItemClickEventArgs e)
		{
			StoreApp storeApp = e.ClickedItem as StoreApp;
			bool flag = storeApp != null;
			if (flag)
			{
				base.Frame.Navigate(typeof(AppDetailsPage), storeApp);
			}
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00013AF8 File Offset: 0x00011CF8
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			bool contentLoaded = this._contentLoaded;
			if (!contentLoaded)
			{
				this._contentLoaded = true;
				Application.LoadComponent(this, new Uri("ms-appx:///MainPage.xaml"), 0);
				this.MainStorePage = (Page)base.FindName("MainStorePage");
				this.RootGrid = (Grid)base.FindName("RootGrid");
				this.MainHub = (Hub)base.FindName("MainHub");
				this.SpotlightSection = (HubSection)base.FindName("SpotlightSection");
				this.TopAppsButton = (Button)base.FindName("TopAppsButton");
				this.DownloadHubButton = (Button)base.FindName("DownloadHubButton");
				this.ExclusiveHubButton = (Button)base.FindName("ExclusiveHubButton");
			}
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00013BC8 File Offset: 0x00011DC8
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
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uielement.add_Tapped), new Action<EventRegistrationToken>(uielement.remove_Tapped), new TappedEventHandler(this.AppItem_Tapped));
				break;
			}
			case 4:
			{
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uielement.add_Tapped), new Action<EventRegistrationToken>(uielement.remove_Tapped), new TappedEventHandler(this.AppItem_Tapped));
				break;
			}
			case 5:
			{
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uielement.add_Tapped), new Action<EventRegistrationToken>(uielement.remove_Tapped), new TappedEventHandler(this.AppItem_Tapped));
				break;
			}
			case 6:
			{
				Image image = (Image)target;
				WindowsRuntimeMarshal.AddEventHandler<ExceptionRoutedEventHandler>(new Func<ExceptionRoutedEventHandler, EventRegistrationToken>(image.add_ImageFailed), new Action<EventRegistrationToken>(image.remove_ImageFailed), new ExceptionRoutedEventHandler(this.Image_ImageFailed));
				break;
			}
			case 7:
			{
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uielement.add_Tapped), new Action<EventRegistrationToken>(uielement.remove_Tapped), new TappedEventHandler(this.AppItem_Tapped));
				break;
			}
			case 8:
			{
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uielement.add_Tapped), new Action<EventRegistrationToken>(uielement.remove_Tapped), new TappedEventHandler(this.AppItem_Tapped));
				break;
			}
			case 9:
			{
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uielement.add_Tapped), new Action<EventRegistrationToken>(uielement.remove_Tapped), new TappedEventHandler(this.AppItem_Tapped));
				break;
			}
			case 10:
			{
				Image image = (Image)target;
				WindowsRuntimeMarshal.AddEventHandler<ExceptionRoutedEventHandler>(new Func<ExceptionRoutedEventHandler, EventRegistrationToken>(image.add_ImageFailed), new Action<EventRegistrationToken>(image.remove_ImageFailed), new ExceptionRoutedEventHandler(this.Image_ImageFailed));
				break;
			}
			case 11:
			{
				Image image = (Image)target;
				WindowsRuntimeMarshal.AddEventHandler<ExceptionRoutedEventHandler>(new Func<ExceptionRoutedEventHandler, EventRegistrationToken>(image.add_ImageFailed), new Action<EventRegistrationToken>(image.remove_ImageFailed), new ExceptionRoutedEventHandler(this.Image_ImageFailed));
				break;
			}
			case 12:
			{
				Image image = (Image)target;
				WindowsRuntimeMarshal.AddEventHandler<ExceptionRoutedEventHandler>(new Func<ExceptionRoutedEventHandler, EventRegistrationToken>(image.add_ImageFailed), new Action<EventRegistrationToken>(image.remove_ImageFailed), new ExceptionRoutedEventHandler(this.Image_ImageFailed));
				break;
			}
			case 13:
			{
				Image image = (Image)target;
				WindowsRuntimeMarshal.AddEventHandler<ExceptionRoutedEventHandler>(new Func<ExceptionRoutedEventHandler, EventRegistrationToken>(image.add_ImageFailed), new Action<EventRegistrationToken>(image.remove_ImageFailed), new ExceptionRoutedEventHandler(this.Image_ImageFailed));
				break;
			}
			case 14:
			{
				Image image = (Image)target;
				WindowsRuntimeMarshal.AddEventHandler<ExceptionRoutedEventHandler>(new Func<ExceptionRoutedEventHandler, EventRegistrationToken>(image.add_ImageFailed), new Action<EventRegistrationToken>(image.remove_ImageFailed), new ExceptionRoutedEventHandler(this.Image_ImageFailed));
				break;
			}
			case 15:
			{
				ListViewBase listViewBase = (ListViewBase)target;
				WindowsRuntimeMarshal.AddEventHandler<ItemClickEventHandler>(new Func<ItemClickEventHandler, EventRegistrationToken>(listViewBase.add_ItemClick), new Action<EventRegistrationToken>(listViewBase.remove_ItemClick), new ItemClickEventHandler(this.MoreAppsItem_Click));
				break;
			}
			case 16:
			{
				ListViewBase listViewBase = (ListViewBase)target;
				WindowsRuntimeMarshal.AddEventHandler<ItemClickEventHandler>(new Func<ItemClickEventHandler, EventRegistrationToken>(listViewBase.add_ItemClick), new Action<EventRegistrationToken>(listViewBase.remove_ItemClick), new ItemClickEventHandler(this.FeaturedItem_Click));
				break;
			}
			case 17:
			{
				ListViewBase listViewBase = (ListViewBase)target;
				WindowsRuntimeMarshal.AddEventHandler<ItemClickEventHandler>(new Func<ItemClickEventHandler, EventRegistrationToken>(listViewBase.add_ItemClick), new Action<EventRegistrationToken>(listViewBase.remove_ItemClick), new ItemClickEventHandler(this.StaffPicksItem_Click));
				break;
			}
			case 18:
			{
				ListViewBase listViewBase = (ListViewBase)target;
				WindowsRuntimeMarshal.AddEventHandler<ItemClickEventHandler>(new Func<ItemClickEventHandler, EventRegistrationToken>(listViewBase.add_ItemClick), new Action<EventRegistrationToken>(listViewBase.remove_ItemClick), new ItemClickEventHandler(this.SpotlightCollectionTile_Click));
				break;
			}
			case 19:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.ViewAllCollectionsButton_Click));
				break;
			}
			case 20:
			{
				FrameworkElement frameworkElement = (FrameworkElement)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(frameworkElement.add_Loaded), new Action<EventRegistrationToken>(frameworkElement.remove_Loaded), new RoutedEventHandler(this.FlipView_Loaded));
				frameworkElement = (FrameworkElement)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(frameworkElement.add_Unloaded), new Action<EventRegistrationToken>(frameworkElement.remove_Unloaded), new RoutedEventHandler(this.FlipView_Unloaded));
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<PointerEventHandler>(new Func<PointerEventHandler, EventRegistrationToken>(uielement.add_PointerEntered), new Action<EventRegistrationToken>(uielement.remove_PointerEntered), new PointerEventHandler(this.FlipView_PointerEntered));
				uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<PointerEventHandler>(new Func<PointerEventHandler, EventRegistrationToken>(uielement.add_PointerExited), new Action<EventRegistrationToken>(uielement.remove_PointerExited), new PointerEventHandler(this.FlipView_PointerExited));
				break;
			}
			case 21:
			{
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uielement.add_Tapped), new Action<EventRegistrationToken>(uielement.remove_Tapped), new TappedEventHandler(this.MSNNews_Tapped));
				break;
			}
			case 22:
			{
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uielement.add_Tapped), new Action<EventRegistrationToken>(uielement.remove_Tapped), new TappedEventHandler(this.OTS_Tapped));
				break;
			}
			case 23:
			{
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uielement.add_Tapped), new Action<EventRegistrationToken>(uielement.remove_Tapped), new TappedEventHandler(this.Fresh_Tapped));
				break;
			}
			case 24:
			{
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uielement.add_Tapped), new Action<EventRegistrationToken>(uielement.remove_Tapped), new TappedEventHandler(this.RandomAppIcon_Tapped));
				break;
			}
			case 25:
			{
				Image image = (Image)target;
				WindowsRuntimeMarshal.AddEventHandler<ExceptionRoutedEventHandler>(new Func<ExceptionRoutedEventHandler, EventRegistrationToken>(image.add_ImageFailed), new Action<EventRegistrationToken>(image.remove_ImageFailed), new ExceptionRoutedEventHandler(this.Image_ImageFailed));
				break;
			}
			case 26:
			{
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uielement.add_Tapped), new Action<EventRegistrationToken>(uielement.remove_Tapped), new TappedEventHandler(this.Border_Tapped_1));
				break;
			}
			case 27:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.TopAppsButton_Click_1));
				break;
			}
			case 28:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.DownloadHubButton_Click));
				break;
			}
			case 29:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.ExclusiveHubButton_Click));
				break;
			}
			case 30:
			{
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uielement.add_Tapped), new Action<EventRegistrationToken>(uielement.remove_Tapped), new TappedEventHandler(this.Image_Tapped));
				break;
			}
			}
			this._contentLoaded = true;
		}

		// Token: 0x0400014B RID: 331
		private const string CURRENT_VERSION = "6.0.3.0";

		// Token: 0x0400014C RID: 332
		private const string VERSION_CHECK_URL = "https://8store.modyleprojects.ru/updates/version.json";

		// Token: 0x0400014D RID: 333
		private const string UPDATE_URL = "https://8store.modyleprojects.ru/updates/update.appx";

		// Token: 0x0400014E RID: 334
		private const string AppsJsonUrl = "https://8store.modyleprojects.ru/data/apps.json";

		// Token: 0x0400014F RID: 335
		private bool _isDataLoaded = false;

		// Token: 0x04000150 RID: 336
		private const string CACHE_FOLDER_NAME = "AppDataCache";

		// Token: 0x04000151 RID: 337
		private const string APPS_CACHE_FILE = "apps_cache.json";

		// Token: 0x04000152 RID: 338
		private const int CACHE_EXPIRY_MINUTES = 5;

		// Token: 0x04000153 RID: 339
		private ObservableCollection<StoreApp> allApps = new ObservableCollection<StoreApp>();

		// Token: 0x04000154 RID: 340
		private ObservableCollection<StoreApp> specialPicksApps = new ObservableCollection<StoreApp>();

		// Token: 0x04000155 RID: 341
		private ObservableCollection<StoreApp> featuredApps = new ObservableCollection<StoreApp>();

		// Token: 0x04000156 RID: 342
		private ObservableCollection<StoreApp> topFreeApps = new ObservableCollection<StoreApp>();

		// Token: 0x04000157 RID: 343
		private ObservableCollection<AppCollection> spotlightCollections = new ObservableCollection<AppCollection>();

		// Token: 0x04000158 RID: 344
		private ObservableCollection<StoreApp> staffPicksApps = new ObservableCollection<StoreApp>();

		// Token: 0x04000159 RID: 345
		private List<StoreApp> allAppsForStats;

		// Token: 0x0400015A RID: 346
		private ObservableCollection<StoreApp> randomApps = new ObservableCollection<StoreApp>();

		// Token: 0x0400015B RID: 347
		private ObservableCollection<StoreApp> moreApps = new ObservableCollection<StoreApp>();

		// Token: 0x0400015C RID: 348
		private bool _hasCheckedForUpdates = false;

		// Token: 0x0400015D RID: 349
		private StorageFolder _cacheFolder;

		// Token: 0x0400015E RID: 350
		private StorageFile _appsCacheFile;

		// Token: 0x0400015F RID: 351
		private DateTime _lastCacheUpdate = DateTime.MinValue;

		// Token: 0x04000160 RID: 352
		private bool _isCacheValid = false;

		// Token: 0x04000161 RID: 353
		private bool _isLoadingFromCache = false;

		// Token: 0x04000162 RID: 354
		private DownloadOperation downloadOperation;

		// Token: 0x04000163 RID: 355
		private BackgroundDownloader downloader;

		// Token: 0x04000164 RID: 356
		private StorageFile downloadedFile;

		// Token: 0x04000165 RID: 357
		private Dictionary<FlipView, DispatcherTimer> _timers = new Dictionary<FlipView, DispatcherTimer>();

		// Token: 0x04000166 RID: 358
		private CancellationTokenSource _jsonLoadingCancellationTokenSource;

		// Token: 0x04000167 RID: 359
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Page MainStorePage;

		// Token: 0x04000168 RID: 360
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Grid RootGrid;

		// Token: 0x04000169 RID: 361
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Hub MainHub;

		// Token: 0x0400016A RID: 362
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private HubSection SpotlightSection;

		// Token: 0x0400016B RID: 363
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button TopAppsButton;

		// Token: 0x0400016C RID: 364
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button DownloadHubButton;

		// Token: 0x0400016D RID: 365
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button ExclusiveHubButton;

		// Token: 0x0400016E RID: 366
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
