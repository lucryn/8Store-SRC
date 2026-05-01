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
using Win81StoreRevival.Config;
using Windows.ApplicationModel.Resources;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using Windows.UI;
using Windows.UI.ApplicationSettings;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace Win81StoreRevival
{
	// Token: 0x02000035 RID: 53
	public sealed class MainPage : Page, IComponentConnector
	{
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600031A RID: 794 RVA: 0x00010064 File Offset: 0x0000E264
		// (set) Token: 0x0600031B RID: 795 RVA: 0x0001006C File Offset: 0x0000E26C
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

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600031C RID: 796 RVA: 0x00010075 File Offset: 0x0000E275
		// (set) Token: 0x0600031D RID: 797 RVA: 0x0001007D File Offset: 0x0000E27D
		public string TotalAppsCount { get; set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600031E RID: 798 RVA: 0x00010086 File Offset: 0x0000E286
		// (set) Token: 0x0600031F RID: 799 RVA: 0x0001008E File Offset: 0x0000E28E
		public string UniquePublishersCount { get; set; }

		// Token: 0x06000320 RID: 800 RVA: 0x00005879 File Offset: 0x00003A79
		public static string Localized(string key, params object[] args)
		{
			return string.Format(new ResourceLoader().GetString(key), args);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00010098 File Offset: 0x0000E298
		private void OnWindowVisibilityChanged(object sender, VisibilityChangedEventArgs e)
		{
			try
			{
				if (e.Visible)
				{
					if (this._lastCacheUpdate.AddMinutes(5.0) < DateTime.Now)
					{
						Task.Run(delegate()
						{
							MainPage.<<OnWindowVisibilityChanged>b__45_0>d <<OnWindowVisibilityChanged>b__45_0>d;
							<<OnWindowVisibilityChanged>b__45_0>d.<>4__this = this;
							<<OnWindowVisibilityChanged>b__45_0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
							<<OnWindowVisibilityChanged>b__45_0>d.<>1__state = -1;
							AsyncTaskMethodBuilder <>t__builder = <<OnWindowVisibilityChanged>b__45_0>d.<>t__builder;
							<>t__builder.Start<MainPage.<<OnWindowVisibilityChanged>b__45_0>d>(ref <<OnWindowVisibilityChanged>b__45_0>d);
							return <<OnWindowVisibilityChanged>b__45_0>d.<>t__builder.Task;
						});
					}
				}
				else
				{
					try
					{
						if (this._isCacheDirty && !this._isSavingCache && this._isCacheValid && this.allApps != null && this.allApps.Count > 0)
						{
							MainPage.<>c__DisplayClass45_0 CS$<>8__locals1 = new MainPage.<>c__DisplayClass45_0();
							CS$<>8__locals1.<>4__this = this;
							this._isSavingCache = true;
							CS$<>8__locals1.appsList = new List<StoreApp>(this.allApps);
							Task.Run(delegate()
							{
								MainPage.<>c__DisplayClass45_0.<<OnWindowVisibilityChanged>b__1>d <<OnWindowVisibilityChanged>b__1>d;
								<<OnWindowVisibilityChanged>b__1>d.<>4__this = CS$<>8__locals1;
								<<OnWindowVisibilityChanged>b__1>d.<>t__builder = AsyncTaskMethodBuilder.Create();
								<<OnWindowVisibilityChanged>b__1>d.<>1__state = -1;
								AsyncTaskMethodBuilder <>t__builder = <<OnWindowVisibilityChanged>b__1>d.<>t__builder;
								<>t__builder.Start<MainPage.<>c__DisplayClass45_0.<<OnWindowVisibilityChanged>b__1>d>(ref <<OnWindowVisibilityChanged>b__1>d);
								return <<OnWindowVisibilityChanged>b__1>d.<>t__builder.Task;
							});
						}
					}
					catch (Exception)
					{
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0001016C File Offset: 0x0000E36C
		public MainPage()
		{
			this.InitializeComponent();
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(this.add_Loaded), new Action<EventRegistrationToken>(this.remove_Loaded), new RoutedEventHandler(this.MainPage_Loaded));
			Window window = Window.Current;
			WindowsRuntimeMarshal.AddEventHandler<WindowVisibilityChangedEventHandler>(new Func<WindowVisibilityChangedEventHandler, EventRegistrationToken>(window.add_VisibilityChanged), new Action<EventRegistrationToken>(window.remove_VisibilityChanged), new WindowVisibilityChangedEventHandler(this.OnWindowVisibilityChanged));
			window = Window.Current;
			WindowsRuntimeMarshal.AddEventHandler<WindowSizeChangedEventHandler>(new Func<WindowSizeChangedEventHandler, EventRegistrationToken>(window.add_SizeChanged), new Action<EventRegistrationToken>(window.remove_SizeChanged), new WindowSizeChangedEventHandler(this.OnMainPageSizeChanged));
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(this.add_Unloaded), new Action<EventRegistrationToken>(this.remove_Unloaded), new RoutedEventHandler(this.MainPage_Unloaded));
			Hub mainHub = this.MainHub;
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(mainHub.add_Loaded), new Action<EventRegistrationToken>(mainHub.remove_Loaded), new RoutedEventHandler(this.MainHub_Loaded));
			this._jsonLoadingCancellationTokenSource = new CancellationTokenSource();
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(this.add_Loaded), new Action<EventRegistrationToken>(this.remove_Loaded), delegate(object s, RoutedEventArgs e)
			{
				if (this.indevText != null)
				{
					bool isDebugBuild = Config.IsDebugBuild;
					bool isPreviewBuild = Config.IsPreviewBuild;
					this.indevText.put_Visibility((isDebugBuild || isPreviewBuild) ? 0 : 1);
					if (this.indevText.Visibility == null)
					{
						ResourceLoader forCurrentView = ResourceLoader.GetForCurrentView();
						if (isDebugBuild && isPreviewBuild)
						{
							string @string = forCurrentView.GetString("DevelopmentTag/Text");
							string string2 = forCurrentView.GetString("PreviewTag/Text");
							this.indevText.put_Text(string.Format("{0}+{1}", new object[]
							{
								@string,
								string2
							}));
							return;
						}
						string text = isPreviewBuild ? "PreviewTag/Text" : "DevelopmentTag/Text";
						this.indevText.put_Text(forCurrentView.GetString(text));
					}
				}
			});
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00010314 File Offset: 0x0000E514
		public void UpdateTitle()
		{
			object obj = ApplicationData.Current.LocalSettings.Values["UseClassicTitle"];
			if (obj != null && (bool)obj)
			{
				ResourceLoader forCurrentView = ResourceLoader.GetForCurrentView();
				this.AppTitle.put_Text(forCurrentView.GetString("StoreWord"));
				return;
			}
			this.AppTitle.put_Text("8Store");
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00010378 File Offset: 0x0000E578
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

		// Token: 0x06000325 RID: 805 RVA: 0x000103DC File Offset: 0x0000E5DC
		private void MainHub_Loaded(object sender, RoutedEventArgs e)
		{
			try
			{
				ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
				bool enabled = localSettings.Values.ContainsKey("LowPerformanceMode") && (bool)localSettings.Values["LowPerformanceMode"];
				this.ApplyLowPerformanceMode(enabled);
				if (this.allApps.Count > 0 && this.allAppsForStats != null)
				{
					this.UpdateUIWithData();
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00010458 File Offset: 0x0000E658
		private void AccountSection_Loaded(object sender, RoutedEventArgs e)
		{
			try
			{
				MainPage.<>c__DisplayClass50_0 CS$<>8__locals1 = new MainPage.<>c__DisplayClass50_0();
				CS$<>8__locals1.<>4__this = this;
				CS$<>8__locals1.timer = new DispatcherTimer();
				CS$<>8__locals1.timer.put_Interval(TimeSpan.FromMilliseconds(100.0));
				DispatcherTimer timer = CS$<>8__locals1.timer;
				WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(timer.add_Tick), new Action<EventRegistrationToken>(timer.remove_Tick), delegate(object s, object args)
				{
					MainPage.<>c__DisplayClass50_0.<<AccountSection_Loaded>b__0>d <<AccountSection_Loaded>b__0>d;
					<<AccountSection_Loaded>b__0>d.<>4__this = CS$<>8__locals1;
					<<AccountSection_Loaded>b__0>d.<>t__builder = AsyncVoidMethodBuilder.Create();
					<<AccountSection_Loaded>b__0>d.<>1__state = -1;
					AsyncVoidMethodBuilder <>t__builder = <<AccountSection_Loaded>b__0>d.<>t__builder;
					<>t__builder.Start<MainPage.<>c__DisplayClass50_0.<<AccountSection_Loaded>b__0>d>(ref <<AccountSection_Loaded>b__0>d);
				});
				CS$<>8__locals1.timer.Start();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x000104E8 File Offset: 0x0000E6E8
		private void ApplyAppSettings()
		{
			try
			{
				ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
				bool enabled = false;
				if (localSettings.Values.ContainsKey("LowPerformanceMode"))
				{
					enabled = (bool)localSettings.Values["LowPerformanceMode"];
				}
				this.UpdateTitle();
				this.UpdateTitleNB();
				this.ApplyLowPerformanceMode(enabled);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00010554 File Offset: 0x0000E754
		private Task InitializeCacheAsync()
		{
			MainPage.<InitializeCacheAsync>d__52 <InitializeCacheAsync>d__;
			<InitializeCacheAsync>d__.<>4__this = this;
			<InitializeCacheAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<InitializeCacheAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <InitializeCacheAsync>d__.<>t__builder;
			<>t__builder.Start<MainPage.<InitializeCacheAsync>d__52>(ref <InitializeCacheAsync>d__);
			return <InitializeCacheAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0001059C File Offset: 0x0000E79C
		private Task<StorageFile> GetCacheFileAsync()
		{
			MainPage.<GetCacheFileAsync>d__53 <GetCacheFileAsync>d__;
			<GetCacheFileAsync>d__.<>4__this = this;
			<GetCacheFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<StorageFile>.Create();
			<GetCacheFileAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<StorageFile> <>t__builder = <GetCacheFileAsync>d__.<>t__builder;
			<>t__builder.Start<MainPage.<GetCacheFileAsync>d__53>(ref <GetCacheFileAsync>d__);
			return <GetCacheFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x000105E4 File Offset: 0x0000E7E4
		private Task UpdateCacheAsync(List<StoreApp> apps)
		{
			MainPage.<UpdateCacheAsync>d__54 <UpdateCacheAsync>d__;
			<UpdateCacheAsync>d__.<>4__this = this;
			<UpdateCacheAsync>d__.apps = apps;
			<UpdateCacheAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateCacheAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <UpdateCacheAsync>d__.<>t__builder;
			<>t__builder.Start<MainPage.<UpdateCacheAsync>d__54>(ref <UpdateCacheAsync>d__);
			return <UpdateCacheAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00010634 File Offset: 0x0000E834
		private Task<bool> TryLoadAppsFromCache()
		{
			MainPage.<TryLoadAppsFromCache>d__55 <TryLoadAppsFromCache>d__;
			<TryLoadAppsFromCache>d__.<>4__this = this;
			<TryLoadAppsFromCache>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<TryLoadAppsFromCache>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<bool> <>t__builder = <TryLoadAppsFromCache>d__.<>t__builder;
			<>t__builder.Start<MainPage.<TryLoadAppsFromCache>d__55>(ref <TryLoadAppsFromCache>d__);
			return <TryLoadAppsFromCache>d__.<>t__builder.Task;
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0001067C File Offset: 0x0000E87C
		private void ApplyDarkModeTemplates()
		{
			try
			{
				HubSection hubSection = this.MainHub.Sections[1];
				if (hubSection != null)
				{
					GridView gridView = this.FindChild<GridView>(hubSection, "SpecialPicksGridView");
					if (gridView != null)
					{
						gridView.put_ItemTemplate(base.Resources["AppItemTemplate"] as DataTemplate);
					}
				}
				HubSection hubSection2 = this.MainHub.Sections[2];
				if (hubSection2 != null)
				{
					GridView gridView2 = this.FindChild<GridView>(hubSection2, "FeaturedGridView");
					if (gridView2 != null)
					{
						gridView2.put_ItemTemplate(base.Resources["AppItemTemplate"] as DataTemplate);
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00010720 File Offset: 0x0000E920
		private void ApplyLightModeTemplates()
		{
			try
			{
				HubSection hubSection = this.MainHub.Sections[1];
				if (hubSection != null)
				{
					GridView gridView = this.FindChild<GridView>(hubSection, "SpecialPicksGridView");
					if (gridView != null)
					{
						gridView.put_ItemTemplate(base.Resources["AppItemTemplate"] as DataTemplate);
					}
				}
				HubSection hubSection2 = this.MainHub.Sections[2];
				if (hubSection2 != null)
				{
					GridView gridView2 = this.FindChild<GridView>(hubSection2, "FeaturedGridView");
					if (gridView2 != null)
					{
						gridView2.put_ItemTemplate(base.Resources["AppItemTemplate"] as DataTemplate);
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600032E RID: 814 RVA: 0x000107C4 File Offset: 0x0000E9C4
		private void UpdateSpotlightTheme(bool darkMode)
		{
			try
			{
				if (!darkMode)
				{
					Color black = Colors.Black;
				}
				else
				{
					Color white = Colors.White;
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x000107F8 File Offset: 0x0000E9F8
		private void ChangeAllTextColors(Color color)
		{
			SolidColorBrush solidColorBrush = new SolidColorBrush(color);
			TextBlock textBlock = this.MainHub.Header as TextBlock;
			if (textBlock != null)
			{
				textBlock.put_Foreground(solidColorBrush);
			}
			foreach (HubSection hubSection in this.MainHub.Sections)
			{
				TextBlock textBlock2 = hubSection.Header as TextBlock;
				if (textBlock2 != null)
				{
					textBlock2.put_Foreground(solidColorBrush);
				}
			}
			TextBlock textBlock3 = this.FindChild<TextBlock>(this, "SearchTextBox");
			if (textBlock3 != null)
			{
				textBlock3.put_Foreground(solidColorBrush);
			}
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00010894 File Offset: 0x0000EA94
		private List<T> FindAllChildren<T>(DependencyObject parent) where T : DependencyObject
		{
			List<T> list = new List<T>();
			if (parent == null)
			{
				return list;
			}
			int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
			for (int i = 0; i < childrenCount; i++)
			{
				DependencyObject child = VisualTreeHelper.GetChild(parent, i);
				if (child is T)
				{
					list.Add(child as T);
				}
				list.AddRange(this.FindAllChildren<T>(child));
			}
			return list;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00005FE8 File Offset: 0x000041E8
		private void PlayUpdateSound()
		{
			SoundManager.PlayStartupSound();
		}

		// Token: 0x06000332 RID: 818 RVA: 0x000108F0 File Offset: 0x0000EAF0
		private void MainPage_Loaded(object sender, RoutedEventArgs e)
		{
			MainPage.<MainPage_Loaded>d__62 <MainPage_Loaded>d__;
			<MainPage_Loaded>d__.<>4__this = this;
			<MainPage_Loaded>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<MainPage_Loaded>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <MainPage_Loaded>d__.<>t__builder;
			<>t__builder.Start<MainPage.<MainPage_Loaded>d__62>(ref <MainPage_Loaded>d__);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0001092C File Offset: 0x0000EB2C
		public Task<bool> RefreshCacheFromNetwork()
		{
			MainPage.<RefreshCacheFromNetwork>d__63 <RefreshCacheFromNetwork>d__;
			<RefreshCacheFromNetwork>d__.<>4__this = this;
			<RefreshCacheFromNetwork>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<RefreshCacheFromNetwork>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<bool> <>t__builder = <RefreshCacheFromNetwork>d__.<>t__builder;
			<>t__builder.Start<MainPage.<RefreshCacheFromNetwork>d__63>(ref <RefreshCacheFromNetwork>d__);
			return <RefreshCacheFromNetwork>d__.<>t__builder.Task;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00010974 File Offset: 0x0000EB74
		private void UpdateUIWithData()
		{
			try
			{
				if ((this.allAppsForStats == null || this.allAppsForStats.Count == 0) && this.allApps != null && this.allApps.Count > 0)
				{
					this.allAppsForStats = new List<StoreApp>(this.allApps);
				}
				if (this.MainHub != null && this.MainHub.Sections.Count > 0)
				{
					HubSection hubSection = this.MainHub.Sections[0];
					if (hubSection != null)
					{
						ListView listView = this.FindChild<ListView>(hubSection, "SpotlightLeftList");
						FlipView flipView = this.FindChild<FlipView>(hubSection, "SpotlightRightList");
						if (listView != null)
						{
							listView.put_ItemsSource(null);
							listView.put_ItemsSource(this.randomApps);
							listView.put_SelectedIndex(0);
						}
						if (flipView != null)
						{
							flipView.put_ItemsSource(null);
							flipView.put_ItemsSource(this.randomApps);
							flipView.put_SelectedIndex(0);
						}
					}
					if (this.MainHub.Sections.Count > 1)
					{
						HubSection hubSection2 = this.MainHub.Sections[1];
						if (hubSection2 != null)
						{
							GridView gridView = this.FindChild<GridView>(hubSection2, "SpecialPicksGridView");
							if (gridView != null)
							{
								gridView.put_ItemsSource(null);
								gridView.put_ItemsSource(this.specialPicksVisibleApps);
							}
						}
					}
					if (this.MainHub.Sections.Count > 2)
					{
						HubSection hubSection3 = this.MainHub.Sections[2];
						if (hubSection3 != null)
						{
							GridView gridView2 = this.FindChild<GridView>(hubSection3, "FeaturedGridView");
							if (gridView2 != null)
							{
								gridView2.put_ItemsSource(null);
								gridView2.put_ItemsSource(this.featuredVisibleApps);
							}
						}
					}
					if (this.MainHub.Sections.Count > 3 && this.MainHub.Sections[3] != null)
					{
						TextBlock textBlock = this.FindChild<TextBlock>(this, "TotalAppsText");
						TextBlock textBlock2 = this.FindChild<TextBlock>(this, "PublishersText");
						if (textBlock != null && this.allAppsForStats != null && this.allAppsForStats.Count > 0)
						{
							string text = this.allAppsForStats.Count.ToString();
							textBlock.put_Text(text);
						}
						else if (textBlock != null)
						{
							textBlock.put_Text("0");
						}
						if (textBlock2 != null && this.allAppsForStats != null && this.allAppsForStats.Count > 0)
						{
							string text2 = Enumerable.Count<string>(Enumerable.Distinct<string>(Enumerable.Select<StoreApp, string>(Enumerable.Where<StoreApp>(this.allAppsForStats, (StoreApp a) => !string.IsNullOrEmpty(a.Publisher)), (StoreApp a) => a.Publisher))).ToString();
							textBlock2.put_Text(text2);
						}
						else if (textBlock2 != null)
						{
							textBlock2.put_Text("0");
						}
					}
					this.UpdateVisibleAppCounts();
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00010C40 File Offset: 0x0000EE40
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

		// Token: 0x06000336 RID: 822 RVA: 0x00004901 File Offset: 0x00002B01
		private void CategoriesButton_Click(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00010C84 File Offset: 0x0000EE84
		private void SafeApplyLowPerformanceMode(bool enabled)
		{
			try
			{
				if (this.MainHub != null)
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
			catch (Exception)
			{
			}
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00010D34 File Offset: 0x0000EF34
		private void ApplyLowPerformanceMode(bool enabled)
		{
			try
			{
				Border border = this.FindChild<Border>(this, "SearchBarBorder");
				if (border != null)
				{
					border.put_Visibility(enabled ? 1 : 0);
				}
				if (this.MainHub != null && this.MainHub.Sections.Count != 0)
				{
					if (enabled)
					{
						this.MainHub.Sections[0].put_Visibility(1);
					}
					else
					{
						this.MainHub.Sections[0].put_Visibility(0);
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00010DC4 File Offset: 0x0000EFC4
		private Task UpdateTileQueueFromJson(string jsonUrl)
		{
			MainPage.<UpdateTileQueueFromJson>d__69 <UpdateTileQueueFromJson>d__;
			<UpdateTileQueueFromJson>d__.<>4__this = this;
			<UpdateTileQueueFromJson>d__.jsonUrl = jsonUrl;
			<UpdateTileQueueFromJson>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateTileQueueFromJson>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <UpdateTileQueueFromJson>d__.<>t__builder;
			<>t__builder.Start<MainPage.<UpdateTileQueueFromJson>d__69>(ref <UpdateTileQueueFromJson>d__);
			return <UpdateTileQueueFromJson>d__.<>t__builder.Task;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00010E14 File Offset: 0x0000F014
		private Task UpdateTileFromJson(string jsonUrl, int index)
		{
			MainPage.<UpdateTileFromJson>d__70 <UpdateTileFromJson>d__;
			<UpdateTileFromJson>d__.<>4__this = this;
			<UpdateTileFromJson>d__.jsonUrl = jsonUrl;
			<UpdateTileFromJson>d__.index = index;
			<UpdateTileFromJson>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateTileFromJson>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <UpdateTileFromJson>d__.<>t__builder;
			<>t__builder.Start<MainPage.<UpdateTileFromJson>d__70>(ref <UpdateTileFromJson>d__);
			return <UpdateTileFromJson>d__.<>t__builder.Task;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00010E6C File Offset: 0x0000F06C
		private TileApp GetByIndex(Dictionary<string, TileApp> dict, int index)
		{
			if (dict == null)
			{
				return null;
			}
			string text = "app" + index;
			if (dict.ContainsKey(text))
			{
				return dict[text];
			}
			return null;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00010EA4 File Offset: 0x0000F0A4
		private void FillSquare(XmlDocument doc, TileApp app)
		{
			if (app == null)
			{
				return;
			}
			XmlNodeList elementsByTagName = doc.GetElementsByTagName("image");
			if (elementsByTagName.Count > 0 && !string.IsNullOrEmpty(app.imgUrl))
			{
				elementsByTagName[0].Attributes.GetNamedItem("src").put_NodeValue(app.imgUrl);
			}
			XmlNodeList elementsByTagName2 = doc.GetElementsByTagName("text");
			if (elementsByTagName2.Count > 0)
			{
				elementsByTagName2[0].put_InnerText(app.title ?? "");
			}
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00010F28 File Offset: 0x0000F128
		private void FillWide(XmlDocument doc, TileApp app)
		{
			if (app == null)
			{
				return;
			}
			XmlNodeList elementsByTagName = doc.GetElementsByTagName("image");
			if (elementsByTagName.Count > 0 && !string.IsNullOrEmpty(app.imgUrl))
			{
				elementsByTagName[0].Attributes.GetNamedItem("src").put_NodeValue(app.imgUrl);
			}
			XmlNodeList elementsByTagName2 = doc.GetElementsByTagName("text");
			if (elementsByTagName2.Count > 0)
			{
				elementsByTagName2[0].put_InnerText(app.title ?? "");
			}
			if (elementsByTagName2.Count > 1)
			{
				elementsByTagName2[1].put_InnerText(app.description ?? "");
			}
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00010FD0 File Offset: 0x0000F1D0
		private void FillLarge(XmlDocument doc, TileApp app)
		{
			if (app == null)
			{
				return;
			}
			XmlNodeList elementsByTagName = doc.GetElementsByTagName("image");
			if (elementsByTagName.Count > 0 && !string.IsNullOrEmpty(app.imgUrl))
			{
				elementsByTagName[0].Attributes.GetNamedItem("src").put_NodeValue(app.imgUrl);
			}
			XmlNodeList elementsByTagName2 = doc.GetElementsByTagName("text");
			if (elementsByTagName2.Count > 0)
			{
				elementsByTagName2[0].put_InnerText(app.title ?? "");
			}
			if (elementsByTagName2.Count > 1)
			{
				elementsByTagName2[1].put_InnerText(app.description ?? "");
			}
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00011078 File Offset: 0x0000F278
		private void ImportBinding(XmlDocument to, XmlDocument from)
		{
			IXmlNode xmlNode = from.GetElementsByTagName("binding").Item(0U);
			IXmlNode xmlNode2 = to.GetElementsByTagName("visual").Item(0U);
			IXmlNode xmlNode3 = to.ImportNode(xmlNode, true);
			xmlNode2.AppendChild(xmlNode3);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x000110B8 File Offset: 0x0000F2B8
		private Task ProcessAutoLoginWithSignOut()
		{
			MainPage.<ProcessAutoLoginWithSignOut>d__76 <ProcessAutoLoginWithSignOut>d__;
			<ProcessAutoLoginWithSignOut>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ProcessAutoLoginWithSignOut>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <ProcessAutoLoginWithSignOut>d__.<>t__builder;
			<>t__builder.Start<MainPage.<ProcessAutoLoginWithSignOut>d__76>(ref <ProcessAutoLoginWithSignOut>d__);
			return <ProcessAutoLoginWithSignOut>d__.<>t__builder.Task;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x000110F8 File Offset: 0x0000F2F8
		private Task LoadAppsFromJsonAndAccount()
		{
			MainPage.<LoadAppsFromJsonAndAccount>d__77 <LoadAppsFromJsonAndAccount>d__;
			<LoadAppsFromJsonAndAccount>d__.<>4__this = this;
			<LoadAppsFromJsonAndAccount>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadAppsFromJsonAndAccount>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadAppsFromJsonAndAccount>d__.<>t__builder;
			<>t__builder.Start<MainPage.<LoadAppsFromJsonAndAccount>d__77>(ref <LoadAppsFromJsonAndAccount>d__);
			return <LoadAppsFromJsonAndAccount>d__.<>t__builder.Task;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00011140 File Offset: 0x0000F340
		private Task LoadAppsFromJsonAndAccountWithAutoLogin()
		{
			MainPage.<LoadAppsFromJsonAndAccountWithAutoLogin>d__78 <LoadAppsFromJsonAndAccountWithAutoLogin>d__;
			<LoadAppsFromJsonAndAccountWithAutoLogin>d__.<>4__this = this;
			<LoadAppsFromJsonAndAccountWithAutoLogin>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadAppsFromJsonAndAccountWithAutoLogin>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadAppsFromJsonAndAccountWithAutoLogin>d__.<>t__builder;
			<>t__builder.Start<MainPage.<LoadAppsFromJsonAndAccountWithAutoLogin>d__78>(ref <LoadAppsFromJsonAndAccountWithAutoLogin>d__);
			return <LoadAppsFromJsonAndAccountWithAutoLogin>d__.<>t__builder.Task;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00011188 File Offset: 0x0000F388
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
					if (int.TryParse(array[i], ref num) && int.TryParse(array2[i], ref num2))
					{
						if (num2 > num)
						{
							return true;
						}
						if (num2 < num)
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

		// Token: 0x06000344 RID: 836 RVA: 0x00011220 File Offset: 0x0000F420
		private Task<bool> CheckForUpdates(bool autoRedirect = true)
		{
			MainPage.<CheckForUpdates>d__80 <CheckForUpdates>d__;
			<CheckForUpdates>d__.<>4__this = this;
			<CheckForUpdates>d__.autoRedirect = autoRedirect;
			<CheckForUpdates>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<CheckForUpdates>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<bool> <>t__builder = <CheckForUpdates>d__.<>t__builder;
			<>t__builder.Start<MainPage.<CheckForUpdates>d__80>(ref <CheckForUpdates>d__);
			return <CheckForUpdates>d__.<>t__builder.Task;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00011270 File Offset: 0x0000F470
		private Task DownloadAndInstallUpdate(AppVersionInfo versionInfo)
		{
			MainPage.<DownloadAndInstallUpdate>d__81 <DownloadAndInstallUpdate>d__;
			<DownloadAndInstallUpdate>d__.<>4__this = this;
			<DownloadAndInstallUpdate>d__.versionInfo = versionInfo;
			<DownloadAndInstallUpdate>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DownloadAndInstallUpdate>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <DownloadAndInstallUpdate>d__.<>t__builder;
			<>t__builder.Start<MainPage.<DownloadAndInstallUpdate>d__81>(ref <DownloadAndInstallUpdate>d__);
			return <DownloadAndInstallUpdate>d__.<>t__builder.Task;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x000112C0 File Offset: 0x0000F4C0
		private Task InstallUpdate()
		{
			MainPage.<InstallUpdate>d__82 <InstallUpdate>d__;
			<InstallUpdate>d__.<>4__this = this;
			<InstallUpdate>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<InstallUpdate>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <InstallUpdate>d__.<>t__builder;
			<>t__builder.Start<MainPage.<InstallUpdate>d__82>(ref <InstallUpdate>d__);
			return <InstallUpdate>d__.<>t__builder.Task;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00011308 File Offset: 0x0000F508
		private void ShowForceUpdateWarning(AppVersionInfo versionInfo)
		{
			MainPage.<ShowForceUpdateWarning>d__83 <ShowForceUpdateWarning>d__;
			<ShowForceUpdateWarning>d__.<>4__this = this;
			<ShowForceUpdateWarning>d__.versionInfo = versionInfo;
			<ShowForceUpdateWarning>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<ShowForceUpdateWarning>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <ShowForceUpdateWarning>d__.<>t__builder;
			<>t__builder.Start<MainPage.<ShowForceUpdateWarning>d__83>(ref <ShowForceUpdateWarning>d__);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0001134C File Offset: 0x0000F54C
		private Task ShowMessage(string title, string message)
		{
			MainPage.<ShowMessage>d__84 <ShowMessage>d__;
			<ShowMessage>d__.title = title;
			<ShowMessage>d__.message = message;
			<ShowMessage>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ShowMessage>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <ShowMessage>d__.<>t__builder;
			<>t__builder.Start<MainPage.<ShowMessage>d__84>(ref <ShowMessage>d__);
			return <ShowMessage>d__.<>t__builder.Task;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0001139C File Offset: 0x0000F59C
		private Task LoadAppsFromNetworkAndUpdateCache()
		{
			MainPage.<LoadAppsFromNetworkAndUpdateCache>d__85 <LoadAppsFromNetworkAndUpdateCache>d__;
			<LoadAppsFromNetworkAndUpdateCache>d__.<>4__this = this;
			<LoadAppsFromNetworkAndUpdateCache>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadAppsFromNetworkAndUpdateCache>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadAppsFromNetworkAndUpdateCache>d__.<>t__builder;
			<>t__builder.Start<MainPage.<LoadAppsFromNetworkAndUpdateCache>d__85>(ref <LoadAppsFromNetworkAndUpdateCache>d__);
			return <LoadAppsFromNetworkAndUpdateCache>d__.<>t__builder.Task;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x000113E4 File Offset: 0x0000F5E4
		private Task RefreshCacheInBackground()
		{
			MainPage.<RefreshCacheInBackground>d__86 <RefreshCacheInBackground>d__;
			<RefreshCacheInBackground>d__.<>4__this = this;
			<RefreshCacheInBackground>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<RefreshCacheInBackground>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <RefreshCacheInBackground>d__.<>t__builder;
			<>t__builder.Start<MainPage.<RefreshCacheInBackground>d__86>(ref <RefreshCacheInBackground>d__);
			return <RefreshCacheInBackground>d__.<>t__builder.Task;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0001142C File Offset: 0x0000F62C
		private Task UpdateReviewStatsForCachedApps()
		{
			MainPage.<UpdateReviewStatsForCachedApps>d__87 <UpdateReviewStatsForCachedApps>d__;
			<UpdateReviewStatsForCachedApps>d__.<>4__this = this;
			<UpdateReviewStatsForCachedApps>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateReviewStatsForCachedApps>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <UpdateReviewStatsForCachedApps>d__.<>t__builder;
			<>t__builder.Start<MainPage.<UpdateReviewStatsForCachedApps>d__87>(ref <UpdateReviewStatsForCachedApps>d__);
			return <UpdateReviewStatsForCachedApps>d__.<>t__builder.Task;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00011474 File Offset: 0x0000F674
		private Task SelectRandomApps()
		{
			MainPage.<SelectRandomApps>d__88 <SelectRandomApps>d__;
			<SelectRandomApps>d__.<>4__this = this;
			<SelectRandomApps>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SelectRandomApps>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <SelectRandomApps>d__.<>t__builder;
			<>t__builder.Start<MainPage.<SelectRandomApps>d__88>(ref <SelectRandomApps>d__);
			return <SelectRandomApps>d__.<>t__builder.Task;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x000114BC File Offset: 0x0000F6BC
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

		// Token: 0x0600034E RID: 846 RVA: 0x00011508 File Offset: 0x0000F708
		private void SpotlightLeft_ItemClick(object sender, ItemClickEventArgs e)
		{
			StoreApp storeApp = e.ClickedItem as StoreApp;
			if (storeApp == null)
			{
				return;
			}
			Hub mainHub = this.MainHub;
			bool flag;
			if (mainHub == null)
			{
				flag = false;
			}
			else
			{
				IList<HubSection> sections = mainHub.Sections;
				flag = (((sections != null) ? new int?(sections.Count) : default(int?)) > 0);
			}
			HubSection hubSection = flag ? this.MainHub.Sections[0] : null;
			if (hubSection == null)
			{
				return;
			}
			ListView listView = this.FindChild<ListView>(hubSection, "SpotlightLeftList");
			FlipView flipView = this.FindChild<FlipView>(hubSection, "SpotlightRightList");
			int num = this.randomApps.IndexOf(storeApp);
			if (num >= 0 && flipView != null)
			{
				int num2 = num;
				ItemCollection items = flipView.Items;
				if (!(num2 >= ((items != null) ? new int?(items.Count) : default(int?))))
				{
					this._syncingSpotlight = true;
					try
					{
						flipView.put_SelectedIndex(num);
						if (listView != null && num >= 0)
						{
							int num3 = num;
							ItemCollection items2 = listView.Items;
							if (num3 < ((items2 != null) ? new int?(items2.Count) : default(int?)))
							{
								listView.put_SelectedIndex(num);
							}
						}
					}
					finally
					{
						this._syncingSpotlight = false;
					}
					return;
				}
			}
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00011664 File Offset: 0x0000F864
		private void SpotlightLeft_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this._syncingSpotlight)
			{
				return;
			}
			ListView listView = sender as ListView;
			if (listView != null && listView.SelectedIndex < 0)
			{
				return;
			}
			int selectedIndex = listView.SelectedIndex;
			Hub mainHub = this.MainHub;
			bool flag;
			if (mainHub == null)
			{
				flag = false;
			}
			else
			{
				IList<HubSection> sections = mainHub.Sections;
				flag = (((sections != null) ? new int?(sections.Count) : default(int?)) > 0);
			}
			HubSection parent = flag ? this.MainHub.Sections[0] : null;
			FlipView flipView = this.FindChild<FlipView>(parent, "SpotlightRightList");
			if (flipView != null && selectedIndex >= 0)
			{
				int num = selectedIndex;
				ItemCollection items = flipView.Items;
				if (num < ((items != null) ? new int?(items.Count) : default(int?)))
				{
					this._syncingSpotlight = true;
					flipView.put_SelectedIndex(selectedIndex);
					this._syncingSpotlight = false;
				}
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00011754 File Offset: 0x0000F954
		private void SpotlightRight_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this._syncingSpotlight)
			{
				return;
			}
			FlipView flipView = sender as FlipView;
			if (flipView != null && flipView.SelectedIndex < 0)
			{
				return;
			}
			int selectedIndex = flipView.SelectedIndex;
			Hub mainHub = this.MainHub;
			bool flag;
			if (mainHub == null)
			{
				flag = false;
			}
			else
			{
				IList<HubSection> sections = mainHub.Sections;
				flag = (((sections != null) ? new int?(sections.Count) : default(int?)) > 0);
			}
			HubSection parent = flag ? this.MainHub.Sections[0] : null;
			ListView listView = this.FindChild<ListView>(parent, "SpotlightLeftList");
			if (listView != null && selectedIndex >= 0)
			{
				int num = selectedIndex;
				ItemCollection items = listView.Items;
				if (num < ((items != null) ? new int?(items.Count) : default(int?)))
				{
					this._syncingSpotlight = true;
					listView.put_SelectedIndex(selectedIndex);
					this._syncingSpotlight = false;
				}
			}
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00011844 File Offset: 0x0000FA44
		private void SpotlightCard_Tapped(object sender, TappedRoutedEventArgs e)
		{
			Border border = sender as Border;
			StoreApp storeApp = ((border != null) ? border.DataContext : null) as StoreApp;
			if (storeApp == null || storeApp.Id.StartsWith("settings"))
			{
				return;
			}
			base.Frame.Navigate(typeof(AppDetailsPage), new AppDetailsNavigationData
			{
				App = storeApp,
				ShowReviews = true
			});
		}

		// Token: 0x06000352 RID: 850 RVA: 0x000118A8 File Offset: 0x0000FAA8
		private void SpotlightRight_Loaded(object sender, RoutedEventArgs e)
		{
			if (!(sender is FlipView) || this._spotlightTimer != null)
			{
				return;
			}
			DispatcherTimer dispatcherTimer = new DispatcherTimer();
			dispatcherTimer.put_Interval(TimeSpan.FromSeconds(4.0));
			this._spotlightTimer = dispatcherTimer;
			DispatcherTimer spotlightTimer = this._spotlightTimer;
			WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(spotlightTimer.add_Tick), new Action<EventRegistrationToken>(spotlightTimer.remove_Tick), delegate(object s, object args)
			{
				Hub mainHub = this.MainHub;
				bool flag;
				if (mainHub == null)
				{
					flag = false;
				}
				else
				{
					IList<HubSection> sections = mainHub.Sections;
					flag = (((sections != null) ? new int?(sections.Count) : default(int?)) > 0);
				}
				HubSection parent = flag ? this.MainHub.Sections[0] : null;
				FlipView flipView = this.FindChild<FlipView>(parent, "SpotlightRightList");
				if (flipView != null)
				{
					ItemCollection items = flipView.Items;
					if (((items != null) ? new int?(items.Count) : default(int?)) > 1)
					{
						int num = (flipView.SelectedIndex + 1) % Math.Max(1, flipView.Items.Count);
						this._syncingSpotlight = true;
						flipView.put_SelectedIndex(num);
						ListView listView = this.FindChild<ListView>(parent, "SpotlightLeftList");
						if (listView != null)
						{
							listView.put_SelectedIndex(num);
						}
						this._syncingSpotlight = false;
						return;
					}
				}
			});
			this._spotlightTimer.Start();
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00011922 File Offset: 0x0000FB22
		private void SpotlightRight_Unloaded(object sender, RoutedEventArgs e)
		{
			if (this._spotlightTimer != null)
			{
				this._spotlightTimer.Stop();
				this._spotlightTimer = null;
			}
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0001193E File Offset: 0x0000FB3E
		private void SpotlightRight_PointerEntered(object sender, PointerRoutedEventArgs e)
		{
			if (this._spotlightTimer != null)
			{
				this._spotlightTimer.Stop();
			}
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00011953 File Offset: 0x0000FB53
		private void SpotlightRight_PointerExited(object sender, PointerRoutedEventArgs e)
		{
			if (this._spotlightTimer != null)
			{
				this._spotlightTimer.Start();
			}
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00011968 File Offset: 0x0000FB68
		private void SpotlightNextButton_Click(object sender, RoutedEventArgs e)
		{
			Hub mainHub = this.MainHub;
			bool flag;
			if (mainHub == null)
			{
				flag = false;
			}
			else
			{
				IList<HubSection> sections = mainHub.Sections;
				flag = (((sections != null) ? new int?(sections.Count) : default(int?)) > 0);
			}
			HubSection hubSection = flag ? this.MainHub.Sections[0] : null;
			if (hubSection == null)
			{
				return;
			}
			FlipView flipView = this.FindChild<FlipView>(hubSection, "SpotlightRightList");
			if (flipView != null)
			{
				ItemCollection items = flipView.Items;
				if (((items != null) ? new int?(items.Count) : default(int?)) == 0)
				{
					return;
				}
			}
			int count = flipView.Items.Count;
			int num = (flipView.SelectedIndex + 1) % count;
			this._syncingSpotlight = true;
			flipView.put_SelectedIndex(num);
			ListView listView = this.FindChild<ListView>(hubSection, "SpotlightLeftList");
			if (listView != null)
			{
				listView.put_SelectedIndex(num);
			}
			this._syncingSpotlight = false;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00011A6C File Offset: 0x0000FC6C
		private T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
		{
			if (parent == null)
			{
				return default(T);
			}
			int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
			for (int i = 0; i < childrenCount; i++)
			{
				DependencyObject child = VisualTreeHelper.GetChild(parent, i);
				if (child is T && ((FrameworkElement)child).Name == childName)
				{
					return (T)((object)child);
				}
				T t = this.FindChild<T>(child, childName);
				if (t != null)
				{
					return t;
				}
			}
			return default(T);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00011AE4 File Offset: 0x0000FCE4
		private void AppItem_Tapped(object sender, TappedRoutedEventArgs e)
		{
			Border border = sender as Border;
			if (border != null)
			{
				StoreApp storeApp = border.DataContext as StoreApp;
				if (storeApp != null && !storeApp.Id.StartsWith("settings"))
				{
					base.Frame.Navigate(typeof(AppDetailsPage), new AppDetailsNavigationData
					{
						App = storeApp,
						ShowReviews = true
					});
				}
			}
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00011B48 File Offset: 0x0000FD48
		private void ShowAboutDialog()
		{
			MainPage.<ShowAboutDialog>d__101 <ShowAboutDialog>d__;
			<ShowAboutDialog>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<ShowAboutDialog>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <ShowAboutDialog>d__.<>t__builder;
			<>t__builder.Start<MainPage.<ShowAboutDialog>d__101>(ref <ShowAboutDialog>d__);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00011B7C File Offset: 0x0000FD7C
		private void SubmitAppsButton_Click(object sender, RoutedEventArgs e)
		{
			MainPage.<SubmitAppsButton_Click>d__102 <SubmitAppsButton_Click>d__;
			<SubmitAppsButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<SubmitAppsButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <SubmitAppsButton_Click>d__.<>t__builder;
			<>t__builder.Start<MainPage.<SubmitAppsButton_Click>d__102>(ref <SubmitAppsButton_Click>d__);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00011BAD File Offset: 0x0000FDAD
		private void AccountButton_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(AccountPage));
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00011BC8 File Offset: 0x0000FDC8
		private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
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
				}
			}
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00004901 File Offset: 0x00002B01
		private void SpecialPicksContainer_Loaded(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00004901 File Offset: 0x00002B01
		private void FeaturedContainer_Loaded(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00011C0C File Offset: 0x0000FE0C
		private void SpecialPicksGridView_Loaded(object sender, RoutedEventArgs e)
		{
			this._specialPicksGridView = (sender as GridView);
			this.UpdateVisibleAppCounts();
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00011C20 File Offset: 0x0000FE20
		private void FeaturedGridView_Loaded(object sender, RoutedEventArgs e)
		{
			this._featuredGridView = (sender as GridView);
			this.UpdateVisibleAppCounts();
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00011C34 File Offset: 0x0000FE34
		private void MainPage_Unloaded(object sender, RoutedEventArgs e)
		{
			WindowsRuntimeMarshal.RemoveEventHandler<WindowSizeChangedEventHandler>(new Action<EventRegistrationToken>(Window.Current.remove_SizeChanged), new WindowSizeChangedEventHandler(this.OnMainPageSizeChanged));
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00011C58 File Offset: 0x0000FE58
		private void OnMainPageSizeChanged(object sender, WindowSizeChangedEventArgs e)
		{
			this.UpdateVisibleAppCounts();
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00011C60 File Offset: 0x0000FE60
		private void UpdateVisibleAppCounts()
		{
			if (this._sectionAnimationsPrimed)
			{
				this.DisableSectionAnimations();
			}
			this.UpdateVisibleCollectionByHeight(this._specialPicksGridView, this.specialPicksApps, this.specialPicksVisibleApps);
			this.UpdateVisibleCollectionByHeight(this._featuredGridView, this.featuredApps, this.featuredVisibleApps);
			this._sectionAnimationsPrimed = true;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00011CB2 File Offset: 0x0000FEB2
		private void DisableSectionAnimations()
		{
			if (this._specialPicksGridView != null)
			{
				this._specialPicksGridView.put_ItemContainerTransitions(new TransitionCollection());
			}
			if (this._featuredGridView != null)
			{
				this._featuredGridView.put_ItemContainerTransitions(new TransitionCollection());
			}
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00011CE4 File Offset: 0x0000FEE4
		private void UpdateVisibleCollectionByHeight(GridView gridView, ObservableCollection<StoreApp> source, ObservableCollection<StoreApp> target)
		{
			target.Clear();
			if (gridView == null || source == null || source.Count == 0)
			{
				return;
			}
			Point point = gridView.TransformToVisual(this).TransformPoint(new Point(0.0, 0.0));
			double num = Window.Current.Bounds.Height - point.Y - 24.0;
			int num2 = Math.Max(1, (int)Math.Floor(num / 368.0));
			int num3 = Math.Min(source.Count, num2 * 3);
			for (int i = 0; i < num3; i++)
			{
				target.Add(source[i]);
			}
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00011D98 File Offset: 0x0000FF98
		private void OrganizeAppsIntoSections()
		{
			this.specialPicksApps.Clear();
			this.featuredApps.Clear();
			this.topFreeApps.Clear();
			if (this.allApps == null || this.allApps.Count == 0)
			{
				return;
			}
			List<StoreApp> list = Enumerable.ToList<StoreApp>(Enumerable.Where<StoreApp>(this.allApps, (StoreApp app) => !app.Id.StartsWith("settings")));
			if (list.Count == 0)
			{
				return;
			}
			int num = 0;
			foreach (StoreApp storeApp in list)
			{
				if (num < 9)
				{
					this.specialPicksApps.Add(storeApp);
					num++;
				}
			}
			int num2 = 0;
			foreach (StoreApp storeApp2 in list)
			{
				if (storeApp2.Featured && num2 < 9)
				{
					this.featuredApps.Add(storeApp2);
					num2++;
				}
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00006009 File Offset: 0x00004209
		private void TopAppsButton_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(AllAppsPage));
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00011EC0 File Offset: 0x000100C0
		public void RefreshData()
		{
			MainPage.<RefreshData>d__116 <RefreshData>d__;
			<RefreshData>d__.<>4__this = this;
			<RefreshData>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<RefreshData>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <RefreshData>d__.<>t__builder;
			<>t__builder.Start<MainPage.<RefreshData>d__116>(ref <RefreshData>d__);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x000048A9 File Offset: 0x00002AA9
		private void StaffPicksButton_Click(object sender, TappedRoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(CollectionsViewPage), "Staff picks");
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00011EFC File Offset: 0x000100FC
		private void SpotlightCollectionTile_Click(object sender, ItemClickEventArgs e)
		{
			try
			{
				AppCollection appCollection = e.ClickedItem as AppCollection;
				if (appCollection != null)
				{
					base.Frame.Navigate(typeof(CollectionsViewPage), appCollection.Name);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00006009 File Offset: 0x00004209
		private void SeeAllCollectionsButton_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(AllAppsPage));
		}

		// Token: 0x0600036C RID: 876 RVA: 0x000048A9 File Offset: 0x00002AA9
		private void StaffPicksSeeAllButton_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(CollectionsViewPage), "Staff picks");
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00011F4C File Offset: 0x0001014C
		private void FeaturedSeeAllButton_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(CollectionsViewPage), "Featured");
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00011F69 File Offset: 0x00010169
		private void NewRisingButton_Click(object sender, TappedRoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(CollectionsViewPage), "New & Rising");
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00011F86 File Offset: 0x00010186
		private void GamesButton_Click(object sender, TappedRoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(CollectionsViewPage), "Games");
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00011FA4 File Offset: 0x000101A4
		private void Border_Tapped(object sender, TappedRoutedEventArgs e)
		{
			MainPage.<Border_Tapped>d__124 <Border_Tapped>d__;
			<Border_Tapped>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<Border_Tapped>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <Border_Tapped>d__.<>t__builder;
			<>t__builder.Start<MainPage.<Border_Tapped>d__124>(ref <Border_Tapped>d__);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00011FD8 File Offset: 0x000101D8
		private void MSNNews_Tapped(object sender, TappedRoutedEventArgs e)
		{
			StoreApp app = new StoreApp
			{
				Id = "5",
				Name = "CnWeather Desktop (Beta)",
				Publisher = "ChockingNetDude",
				Version = "0.1.0.4",
				DownloadUrl = "https://dl.dankassassin368.com/CnWeather_M1.appx",
				IconUrl = "https://dankassassin368.com/random/icons/37af59a1f4af189398117b3d69047faa.png",
				Description = "CnWeather Desktop is a fully functional weather app with Auto-Location, 3 days forecast, hourly forecast, live weather alerts and many more features weather app, application is based on CND Apps series. Currently still in beta.",
				Featured = false,
				ReviewStats = new ReviewStats
				{
					AverageRating = 0.0,
					ReviewCount = 0
				},
				Type = "App",
				Category = "News + Weather"
			};
			base.Frame.Navigate(typeof(AppDetailsPage), new AppDetailsNavigationData
			{
				App = app,
				ShowReviews = true
			});
		}

		// Token: 0x06000372 RID: 882 RVA: 0x000120A0 File Offset: 0x000102A0
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

		// Token: 0x06000373 RID: 883 RVA: 0x00012168 File Offset: 0x00010368
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

		// Token: 0x06000374 RID: 884 RVA: 0x00012230 File Offset: 0x00010430
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

		// Token: 0x06000375 RID: 885 RVA: 0x00004891 File Offset: 0x00002A91
		private void Border_Tapped_1(object sender, TappedRoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(AllAppsPage1));
		}

		// Token: 0x06000376 RID: 886 RVA: 0x000122F8 File Offset: 0x000104F8
		private void nbSearch_QuerySubmitted(object sender, SearchBoxQuerySubmittedEventArgs e)
		{
			string text = (e != null && e.QueryText != null) ? e.QueryText.Trim() : null;
			if (!string.IsNullOrEmpty(text))
			{
				base.Frame.Navigate(typeof(SearchResultsPage), text);
			}
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00012340 File Offset: 0x00010540
		protected override void OnNavigatedFrom(NavigationEventArgs e)
		{
			MainPage.<OnNavigatedFrom>d__131 <OnNavigatedFrom>d__;
			<OnNavigatedFrom>d__.<>4__this = this;
			<OnNavigatedFrom>d__.e = e;
			<OnNavigatedFrom>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<OnNavigatedFrom>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <OnNavigatedFrom>d__.<>t__builder;
			<>t__builder.Start<MainPage.<OnNavigatedFrom>d__131>(ref <OnNavigatedFrom>d__);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00012384 File Offset: 0x00010584
		private Task SaveCacheState()
		{
			MainPage.<SaveCacheState>d__132 <SaveCacheState>d__;
			<SaveCacheState>d__.<>4__this = this;
			<SaveCacheState>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SaveCacheState>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <SaveCacheState>d__.<>t__builder;
			<>t__builder.Start<MainPage.<SaveCacheState>d__132>(ref <SaveCacheState>d__);
			return <SaveCacheState>d__.<>t__builder.Task;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x000123CC File Offset: 0x000105CC
		public void CancelJsonLoading()
		{
			try
			{
				if (this._jsonLoadingCancellationTokenSource != null && !this._jsonLoadingCancellationTokenSource.IsCancellationRequested)
				{
					this._jsonLoadingCancellationTokenSource.Cancel();
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00012410 File Offset: 0x00010610
		private string LocalizedCategory(string category)
		{
			if (string.IsNullOrEmpty(category))
			{
				return category;
			}
			string text = category.Replace(" ", "").Replace("&", "");
			string text2 = "CollectionCategory_" + text;
			try
			{
				string @string = ResourceLoader.GetForCurrentView().GetString(text2);
				if (!string.IsNullOrEmpty(@string))
				{
					return @string;
				}
			}
			catch
			{
			}
			return category;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00012484 File Offset: 0x00010684
		private Task CleanupResourcesAsync()
		{
			MainPage.<CleanupResourcesAsync>d__136 <CleanupResourcesAsync>d__;
			<CleanupResourcesAsync>d__.<>4__this = this;
			<CleanupResourcesAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<CleanupResourcesAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <CleanupResourcesAsync>d__.<>t__builder;
			<>t__builder.Start<MainPage.<CleanupResourcesAsync>d__136>(ref <CleanupResourcesAsync>d__);
			return <CleanupResourcesAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600037C RID: 892 RVA: 0x000124C9 File Offset: 0x000106C9
		private void DisposeHttpClients()
		{
			GC.Collect();
			GC.WaitForPendingFinalizers();
		}

		// Token: 0x0600037D RID: 893 RVA: 0x000124D8 File Offset: 0x000106D8
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			MainPage.<OnNavigatedTo>d__138 <OnNavigatedTo>d__;
			<OnNavigatedTo>d__.<>4__this = this;
			<OnNavigatedTo>d__.e = e;
			<OnNavigatedTo>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<OnNavigatedTo>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <OnNavigatedTo>d__.<>t__builder;
			<>t__builder.Start<MainPage.<OnNavigatedTo>d__138>(ref <OnNavigatedTo>d__);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0001251C File Offset: 0x0001071C
		private Task LoadAppsFromJson()
		{
			MainPage.<LoadAppsFromJson>d__139 <LoadAppsFromJson>d__;
			<LoadAppsFromJson>d__.<>4__this = this;
			<LoadAppsFromJson>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadAppsFromJson>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadAppsFromJson>d__.<>t__builder;
			<>t__builder.Start<MainPage.<LoadAppsFromJson>d__139>(ref <LoadAppsFromJson>d__);
			return <LoadAppsFromJson>d__.<>t__builder.Task;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00012564 File Offset: 0x00010764
		private Task UpdateUIWithPreloadedData()
		{
			MainPage.<UpdateUIWithPreloadedData>d__140 <UpdateUIWithPreloadedData>d__;
			<UpdateUIWithPreloadedData>d__.<>4__this = this;
			<UpdateUIWithPreloadedData>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateUIWithPreloadedData>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <UpdateUIWithPreloadedData>d__.<>t__builder;
			<>t__builder.Start<MainPage.<UpdateUIWithPreloadedData>d__140>(ref <UpdateUIWithPreloadedData>d__);
			return <UpdateUIWithPreloadedData>d__.<>t__builder.Task;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x000125AC File Offset: 0x000107AC
		private Task RestoreCacheState()
		{
			MainPage.<RestoreCacheState>d__141 <RestoreCacheState>d__;
			<RestoreCacheState>d__.<>4__this = this;
			<RestoreCacheState>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<RestoreCacheState>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <RestoreCacheState>d__.<>t__builder;
			<>t__builder.Start<MainPage.<RestoreCacheState>d__141>(ref <RestoreCacheState>d__);
			return <RestoreCacheState>d__.<>t__builder.Task;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x000125F4 File Offset: 0x000107F4
		private Task LoadSpotlightCollections()
		{
			MainPage.<LoadSpotlightCollections>d__142 <LoadSpotlightCollections>d__;
			<LoadSpotlightCollections>d__.<>4__this = this;
			<LoadSpotlightCollections>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadSpotlightCollections>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadSpotlightCollections>d__.<>t__builder;
			<>t__builder.Start<MainPage.<LoadSpotlightCollections>d__142>(ref <LoadSpotlightCollections>d__);
			return <LoadSpotlightCollections>d__.<>t__builder.Task;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0001263C File Offset: 0x0001083C
		private List<string> GetRandomIcons(int count, string category = null)
		{
			List<string> list = new List<string>();
			Random random = new Random();
			if (this.allApps == null || this.allApps.Count <= 0)
			{
				goto IL_105;
			}
			IEnumerable<StoreApp> enumerable = this.allApps;
			if (!string.IsNullOrEmpty(category))
			{
				enumerable = Enumerable.Where<StoreApp>(this.allApps, (StoreApp a) => !string.IsNullOrEmpty(a.Category) && a.Category.Equals(category, 5));
			}
			List<StoreApp> list2 = Enumerable.ToList<StoreApp>(Enumerable.Where<StoreApp>(enumerable, (StoreApp a) => !string.IsNullOrEmpty(a.IconUrl)));
			if (list2.Count <= 0)
			{
				goto IL_105;
			}
			using (List<StoreApp>.Enumerator enumerator = Enumerable.ToList<StoreApp>(Enumerable.Take<StoreApp>(Enumerable.OrderBy<StoreApp, int>(list2, (StoreApp x) => random.Next()), Math.Min(count, list2.Count))).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					StoreApp storeApp = enumerator.Current;
					list.Add(storeApp.IconUrl);
				}
				goto IL_105;
			}
			IL_FA:
			list.Add("ms-appx:///Assets/icon-error.png");
			IL_105:
			if (list.Count >= count)
			{
				return list;
			}
			goto IL_FA;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00012768 File Offset: 0x00010968
		private string GetCollectionDisplayName(ResourceLoader tns, string category)
		{
			string collectionDisplayKey = this.GetCollectionDisplayKey(category);
			if (!string.IsNullOrWhiteSpace(collectionDisplayKey))
			{
				string @string = tns.GetString(collectionDisplayKey);
				if (!string.IsNullOrWhiteSpace(@string))
				{
					return @string;
				}
			}
			return category;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00012798 File Offset: 0x00010998
		private string GetCollectionDisplayKey(string category)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(category);
			if (num <= 2447142016U)
			{
				if (num <= 1315724598U)
				{
					if (num <= 417729427U)
					{
						if (num <= 320514149U)
						{
							if (num != 175614239U)
							{
								if (num == 320514149U)
								{
									if (category == "Books + Reference")
									{
										return "CollectionCategory_BooksReference";
									}
								}
							}
							else if (category == "Action")
							{
								return "CollectionCategory_Action";
							}
						}
						else if (num != 379048501U)
						{
							if (num == 417729427U)
							{
								if (category == "New & Rising")
								{
									return "CollectionCategory_NewRising";
								}
							}
						}
						else if (category == "Education")
						{
							return "CollectionCategory_Education";
						}
					}
					else if (num <= 1066741867U)
					{
						if (num != 496028322U)
						{
							if (num == 1066741867U)
							{
								if (category == "Adventure")
								{
									return "CollectionCategory_Adventure";
								}
							}
						}
						else if (category == "Health + Fitness")
						{
							return "CollectionCategory_HealthFitness";
						}
					}
					else if (num != 1071417850U)
					{
						if (num != 1213003503U)
						{
							if (num == 1315724598U)
							{
								if (category == "Simulation")
								{
									return "CollectionCategory_Simulation";
								}
							}
						}
						else if (category == "Keep connected")
						{
							return "CollectionCategory_KeepConnected";
						}
					}
					else if (category == "Surface picks")
					{
						return "CollectionCategory_SurfacePicks";
					}
				}
				else if (num <= 1849229205U)
				{
					if (num <= 1545232503U)
					{
						if (num != 1456528693U)
						{
							if (num == 1545232503U)
							{
								if (category == "Music + Rhythm")
								{
									return "CollectionCategory_MusicRhythm";
								}
							}
						}
						else if (category == "Family + Kids")
						{
							return "CollectionCategory_FamilyKids";
						}
					}
					else if (num != 1584519634U)
					{
						if (num != 1612786629U)
						{
							if (num == 1849229205U)
							{
								if (category == "Other")
								{
									return "CollectionCategory_Other";
								}
							}
						}
						else if (category == "Card + Casino")
						{
							return "CollectionCategory_CardCasino";
						}
					}
					else if (category == "Travel + Navigation")
					{
						return "CollectionCategory_TravelNavigation";
					}
				}
				else if (num <= 1945738556U)
				{
					if (num != 1904316691U)
					{
						if (num == 1945738556U)
						{
							if (category == "Games")
							{
								return "CollectionCategory_Games";
							}
						}
					}
					else if (category == "Great on 8")
					{
						return "CollectionCategory_GreatOn8";
					}
				}
				else if (num != 2039478274U)
				{
					if (num != 2136908150U)
					{
						if (num == 2447142016U)
						{
							if (category == "Classics")
							{
								return "CollectionCategory_Classics";
							}
						}
					}
					else if (category == "Government")
					{
						return "CollectionCategory_Government";
					}
				}
				else if (category == "Kitchen helpers")
				{
					return "CollectionCategory_KitchenHelpers";
				}
			}
			else if (num <= 3271434715U)
			{
				if (num <= 2704337693U)
				{
					if (num <= 2548511764U)
					{
						if (num != 2465108186U)
						{
							if (num == 2548511764U)
							{
								if (category == "Photo + Design")
								{
									return "CollectionCategory_PhotoDesign";
								}
							}
						}
						else if (category == "Sports")
						{
							return "CollectionCategory_Sports";
						}
					}
					else if (num != 2639503373U)
					{
						if (num == 2704337693U)
						{
							if (category == "Shopping")
							{
								return "CollectionCategory_Shopping";
							}
						}
					}
					else if (category == "Microsoft apps")
					{
						return "CollectionCategory_MicrosoftApps";
					}
				}
				else if (num <= 3120275647U)
				{
					if (num != 3066062199U)
					{
						if (num == 3120275647U)
						{
							if (category == "Staff picks")
							{
								return "CollectionCategory_StaffPicks";
							}
						}
					}
					else if (category == "Utilities")
					{
						return "CollectionCategory_Utilities";
					}
				}
				else if (num != 3205650756U)
				{
					if (num != 3213326657U)
					{
						if (num == 3271434715U)
						{
							if (category == "Security")
							{
								return "CollectionCategory_Security";
							}
						}
					}
					else if (category == "Arcade")
					{
						return "CollectionCategory_Arcade";
					}
				}
				else if (category == "Strategy")
				{
					return "CollectionCategory_Strategy";
				}
			}
			else if (num <= 3398496685U)
			{
				if (num <= 3319603597U)
				{
					if (num != 3315627437U)
					{
						if (num == 3319603597U)
						{
							if (category == "Finance")
							{
								return "CollectionCategory_Finance";
							}
						}
					}
					else if (category == "Puzzle")
					{
						return "CollectionCategory_Puzzle";
					}
				}
				else if (num != 3333035499U)
				{
					if (num != 3364160439U)
					{
						if (num == 3398496685U)
						{
							if (category == "Racing + Flying")
							{
								return "CollectionCategory_RacingFlying";
							}
						}
					}
					else if (category == "News + Weather")
					{
						return "CollectionCategory_NewsWeather";
					}
				}
				else if (category == "Music + Audio")
				{
					return "CollectionCategory_MusicAudio";
				}
			}
			else if (num <= 4112790225U)
			{
				if (num != 3909915428U)
				{
					if (num == 4112790225U)
					{
						if (category == "Microsoft Office")
						{
							return "CollectionCategory_MicrosoftOffice";
						}
					}
				}
				else if (category == "Food + Cooking")
				{
					return "CollectionCategory_FoodCooking";
				}
			}
			else if (num != 4161635175U)
			{
				if (num != 4177547506U)
				{
					if (num == 4259187209U)
					{
						if (category == "Essential apps")
						{
							return "CollectionCategory_EssentialApps";
						}
					}
				}
				else if (category == "Social")
				{
					return "CollectionCategory_Social";
				}
			}
			else if (category == "Video + Entertainment")
			{
				return "CollectionCategory_VideoEntertainment";
			}
			return null;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00012E20 File Offset: 0x00011020
		private void FindAndSetCollectionsGridView()
		{
			try
			{
				GridView gridView = this.FindVisualChild<GridView>(this, "SpotlightCollectionsGridView");
				if (gridView != null)
				{
					gridView.put_ItemsSource(this.spotlightCollections);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00012E60 File Offset: 0x00011060
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

		// Token: 0x06000387 RID: 903 RVA: 0x00012ED8 File Offset: 0x000110D8
		private void OnCommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
		{
			args.Request.ApplicationCommands.Clear();
			args.Request.ApplicationCommands.Add(new SettingsCommand("general", MainPage.Localized("GeneralCharm", new object[0]), delegate(IUICommand cmd)
			{
				this.SafeOpenSettingsFlyout(typeof(GeneralSettingsFlyout));
			}));
			args.Request.ApplicationCommands.Add(new SettingsCommand("personalization", MainPage.Localized("PersonalizationCharm", new object[0]), delegate(IUICommand cmd)
			{
				this.SafeOpenSettingsFlyout(typeof(CosmeticFlyout));
			}));
			args.Request.ApplicationCommands.Add(new SettingsCommand("downloads", MainPage.Localized("MyDownloads", new object[0]), delegate(IUICommand cmd)
			{
				Frame frame = Window.Current.Content as Frame;
				if (frame != null)
				{
					frame.Navigate(typeof(DownloadsHub));
				}
			}));
			args.Request.ApplicationCommands.Add(new SettingsCommand("language", MainPage.Localized("LanguageCharm", new object[0]), delegate(IUICommand cmd)
			{
				this.SafeOpenSettingsFlyout(typeof(LanguageSettingsFlyout));
			}));
			args.Request.ApplicationCommands.Add(new SettingsCommand("8Store Recovery", MainPage.Localized("RecoveryCharm", new object[0]), delegate(IUICommand cmd)
			{
				this.SafeOpenSettingsFlyout(typeof(FactoryResetFlyout));
			}));
			args.Request.ApplicationCommands.Add(new SettingsCommand("about", MainPage.Localized("AboutCharm", new object[0]), delegate(IUICommand cmd)
			{
				this.SafeOpenSettingsFlyout(typeof(AboutSettingsFlyout));
			}));
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0001304C File Offset: 0x0001124C
		private void SafeOpenSettingsFlyout(Type flyoutType)
		{
			try
			{
				this.OpenSettingsFlyout(flyoutType);
			}
			catch (Exception)
			{
				try
				{
					new MessageDialog(string.Format("This feature is not supported on this device. ({0})", new object[]
					{
						flyoutType.Name
					}), "Error").ShowAsync();
				}
				catch
				{
				}
			}
		}

		// Token: 0x06000389 RID: 905 RVA: 0x000080F4 File Offset: 0x000062F4
		private void OpenSettingsFlyout(Type flyoutType)
		{
			((SettingsFlyout)Activator.CreateInstance(flyoutType)).Show();
		}

		// Token: 0x0600038A RID: 906 RVA: 0x000130B0 File Offset: 0x000112B0
		private Task LoadAccountDataAsync()
		{
			MainPage.<LoadAccountDataAsync>d__151 <LoadAccountDataAsync>d__;
			<LoadAccountDataAsync>d__.<>4__this = this;
			<LoadAccountDataAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadAccountDataAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadAccountDataAsync>d__.<>t__builder;
			<>t__builder.Start<MainPage.<LoadAccountDataAsync>d__151>(ref <LoadAccountDataAsync>d__);
			return <LoadAccountDataAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x000130F8 File Offset: 0x000112F8
		public Task UpdateAccountUIAsync()
		{
			MainPage.<UpdateAccountUIAsync>d__152 <UpdateAccountUIAsync>d__;
			<UpdateAccountUIAsync>d__.<>4__this = this;
			<UpdateAccountUIAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateAccountUIAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <UpdateAccountUIAsync>d__.<>t__builder;
			<>t__builder.Start<MainPage.<UpdateAccountUIAsync>d__152>(ref <UpdateAccountUIAsync>d__);
			return <UpdateAccountUIAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00013140 File Offset: 0x00011340
		private void AccountLogoutButton_Click(object sender, RoutedEventArgs e)
		{
			MainPage.<AccountLogoutButton_Click>d__153 <AccountLogoutButton_Click>d__;
			<AccountLogoutButton_Click>d__.<>4__this = this;
			<AccountLogoutButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<AccountLogoutButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <AccountLogoutButton_Click>d__.<>t__builder;
			<>t__builder.Start<MainPage.<AccountLogoutButton_Click>d__153>(ref <AccountLogoutButton_Click>d__);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0001317C File Offset: 0x0001137C
		private void ShowAccountLoginPopup()
		{
			MainPage.<>c__DisplayClass154_0 CS$<>8__locals1 = new MainPage.<>c__DisplayClass154_0();
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
				MainPage.<>c__DisplayClass154_0.<<ShowAccountLoginPopup>b__0>d <<ShowAccountLoginPopup>b__0>d;
				<<ShowAccountLoginPopup>b__0>d.<>4__this = CS$<>8__locals1;
				<<ShowAccountLoginPopup>b__0>d.<>t__builder = AsyncVoidMethodBuilder.Create();
				<<ShowAccountLoginPopup>b__0>d.<>1__state = -1;
				AsyncVoidMethodBuilder <>t__builder = <<ShowAccountLoginPopup>b__0>d.<>t__builder;
				<>t__builder.Start<MainPage.<>c__DisplayClass154_0.<<ShowAccountLoginPopup>b__0>d>(ref <<ShowAccountLoginPopup>b__0>d);
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

		// Token: 0x0600038E RID: 910 RVA: 0x0001356C File Offset: 0x0001176C
		private Task ProcessAccountLogin(string email, string password)
		{
			MainPage.<ProcessAccountLogin>d__155 <ProcessAccountLogin>d__;
			<ProcessAccountLogin>d__.<>4__this = this;
			<ProcessAccountLogin>d__.email = email;
			<ProcessAccountLogin>d__.password = password;
			<ProcessAccountLogin>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ProcessAccountLogin>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <ProcessAccountLogin>d__.<>t__builder;
			<>t__builder.Start<MainPage.<ProcessAccountLogin>d__155>(ref <ProcessAccountLogin>d__);
			return <ProcessAccountLogin>d__.<>t__builder.Task;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x000135C4 File Offset: 0x000117C4
		private Task ProcessAccountRegister(string email, string username, string password, string confirmPassword)
		{
			MainPage.<ProcessAccountRegister>d__156 <ProcessAccountRegister>d__;
			<ProcessAccountRegister>d__.<>4__this = this;
			<ProcessAccountRegister>d__.email = email;
			<ProcessAccountRegister>d__.username = username;
			<ProcessAccountRegister>d__.password = password;
			<ProcessAccountRegister>d__.confirmPassword = confirmPassword;
			<ProcessAccountRegister>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ProcessAccountRegister>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <ProcessAccountRegister>d__.<>t__builder;
			<>t__builder.Start<MainPage.<ProcessAccountRegister>d__156>(ref <ProcessAccountRegister>d__);
			return <ProcessAccountRegister>d__.<>t__builder.Task;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0001362C File Offset: 0x0001182C
		private void ShowAccountRegisterPopup()
		{
			MainPage.<>c__DisplayClass157_0 CS$<>8__locals1 = new MainPage.<>c__DisplayClass157_0();
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
				MainPage.<>c__DisplayClass157_0.<<ShowAccountRegisterPopup>b__0>d <<ShowAccountRegisterPopup>b__0>d;
				<<ShowAccountRegisterPopup>b__0>d.<>4__this = CS$<>8__locals1;
				<<ShowAccountRegisterPopup>b__0>d.<>t__builder = AsyncVoidMethodBuilder.Create();
				<<ShowAccountRegisterPopup>b__0>d.<>1__state = -1;
				AsyncVoidMethodBuilder <>t__builder = <<ShowAccountRegisterPopup>b__0>d.<>t__builder;
				<>t__builder.Start<MainPage.<>c__DisplayClass157_0.<<ShowAccountRegisterPopup>b__0>d>(ref <<ShowAccountRegisterPopup>b__0>d);
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

		// Token: 0x06000391 RID: 913 RVA: 0x00013B04 File Offset: 0x00011D04
		private Task ShowMessageAsync(string message, string title)
		{
			MainPage.<ShowMessageAsync>d__158 <ShowMessageAsync>d__;
			<ShowMessageAsync>d__.message = message;
			<ShowMessageAsync>d__.title = title;
			<ShowMessageAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ShowMessageAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <ShowMessageAsync>d__.<>t__builder;
			<>t__builder.Start<MainPage.<ShowMessageAsync>d__158>(ref <ShowMessageAsync>d__);
			return <ShowMessageAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00004891 File Offset: 0x00002A91
		private void DownloadHubButton_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(AllAppsPage1));
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00013B54 File Offset: 0x00011D54
		private void FlipView_PointerEntered(object sender, PointerRoutedEventArgs e)
		{
			FlipView flipView = sender as FlipView;
			if (flipView == null)
			{
				return;
			}
			DispatcherTimer dispatcherTimer;
			if (this._timers.TryGetValue(flipView, ref dispatcherTimer))
			{
				dispatcherTimer.Stop();
			}
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00013B84 File Offset: 0x00011D84
		private void FlipView_PointerExited(object sender, PointerRoutedEventArgs e)
		{
			FlipView flipView = sender as FlipView;
			if (flipView == null)
			{
				return;
			}
			DispatcherTimer dispatcherTimer;
			if (this._timers.TryGetValue(flipView, ref dispatcherTimer))
			{
				dispatcherTimer.Start();
			}
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00013BB4 File Offset: 0x00011DB4
		private void FlipView_Loaded(object sender, RoutedEventArgs e)
		{
			FlipView flip = sender as FlipView;
			if (flip == null)
			{
				return;
			}
			if (this._timers.ContainsKey(flip))
			{
				return;
			}
			DispatcherTimer dispatcherTimer = new DispatcherTimer();
			dispatcherTimer.put_Interval(TimeSpan.FromSeconds(4.0));
			DispatcherTimer dispatcherTimer2 = dispatcherTimer;
			WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(dispatcherTimer2.add_Tick), new Action<EventRegistrationToken>(dispatcherTimer2.remove_Tick), delegate(object s, object args)
			{
				if (flip.Items.Count <= 1)
				{
					return;
				}
				int num = flip.SelectedIndex + 1;
				if (num >= flip.Items.Count)
				{
					num = 0;
				}
				flip.put_SelectedIndex(num);
			});
			this._timers.Add(flip, dispatcherTimer);
			dispatcherTimer.Start();
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00013C50 File Offset: 0x00011E50
		private void FlipView_Unloaded(object sender, RoutedEventArgs e)
		{
			FlipView flipView = sender as FlipView;
			if (flipView == null)
			{
				return;
			}
			DispatcherTimer dispatcherTimer;
			if (this._timers.TryGetValue(flipView, ref dispatcherTimer))
			{
				dispatcherTimer.Stop();
				this._timers.Remove(flipView);
			}
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00006021 File Offset: 0x00004221
		private void TopAppsButton_Click_1(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(PopularPage));
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00013C8C File Offset: 0x00011E8C
		private void StaffPickApp_Tapped(object sender, TappedRoutedEventArgs e)
		{
			Border border = sender as Border;
			if (border != null)
			{
				StoreApp storeApp = border.DataContext as StoreApp;
				if (storeApp != null && !storeApp.Id.StartsWith("settings"))
				{
					base.Frame.Navigate(typeof(AppDetailsPage), new AppDetailsNavigationData
					{
						App = storeApp,
						ShowReviews = true
					});
				}
			}
		}

		// Token: 0x06000399 RID: 921 RVA: 0x000048A9 File Offset: 0x00002AA9
		private void ViewAllStaffPicks_Tapped(object sender, TappedRoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(CollectionsViewPage), "Staff picks");
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00013CF0 File Offset: 0x00011EF0
		public Task RefreshCacheAsync()
		{
			MainPage.<RefreshCacheAsync>d__167 <RefreshCacheAsync>d__;
			<RefreshCacheAsync>d__.<>4__this = this;
			<RefreshCacheAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<RefreshCacheAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <RefreshCacheAsync>d__.<>t__builder;
			<>t__builder.Start<MainPage.<RefreshCacheAsync>d__167>(ref <RefreshCacheAsync>d__);
			return <RefreshCacheAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00013D38 File Offset: 0x00011F38
		public Task ClearCacheAsync()
		{
			MainPage.<ClearCacheAsync>d__168 <ClearCacheAsync>d__;
			<ClearCacheAsync>d__.<>4__this = this;
			<ClearCacheAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ClearCacheAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <ClearCacheAsync>d__.<>t__builder;
			<>t__builder.Start<MainPage.<ClearCacheAsync>d__168>(ref <ClearCacheAsync>d__);
			return <ClearCacheAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00013D7D File Offset: 0x00011F7D
		private void Image_Tapped(object sender, TappedRoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(InstalledAppsPage));
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00013D98 File Offset: 0x00011F98
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Application.LoadComponent(this, new Uri("ms-appx:///MainPage.xaml"), 0);
			this.MainStorePage = (Page)base.FindName("MainStorePage");
			this.MainHub = (Hub)base.FindName("MainHub");
			this.backButton = (Button)base.FindName("backButton");
			this.AppTitle = (TextBlock)base.FindName("AppTitle");
			this.indevText = (TextBlock)base.FindName("indevText");
			this.SpotlightSection = (HubSection)base.FindName("SpotlightSection");
			this.nbSearch = (SearchBox)base.FindName("nbSearch");
			this.DownloadHubButton = (Button)base.FindName("DownloadHubButton");
			this.CollectionsButton = (Button)base.FindName("CollectionsButton");
			this.AccountButton = (Button)base.FindName("AccountButton");
			this.nbLogoText = (TextBlock)base.FindName("nbLogoText");
			this.PrimaryView = (VisualState)base.FindName("PrimaryView");
			this.SnappedView = (VisualState)base.FindName("SnappedView");
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00013EE4 File Offset: 0x000120E4
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
				Image image = (Image)target;
				WindowsRuntimeMarshal.AddEventHandler<ExceptionRoutedEventHandler>(new Func<ExceptionRoutedEventHandler, EventRegistrationToken>(image.add_ImageFailed), new Action<EventRegistrationToken>(image.remove_ImageFailed), new ExceptionRoutedEventHandler(this.Image_ImageFailed));
				break;
			}
			case 4:
			{
				Image image = (Image)target;
				WindowsRuntimeMarshal.AddEventHandler<ExceptionRoutedEventHandler>(new Func<ExceptionRoutedEventHandler, EventRegistrationToken>(image.add_ImageFailed), new Action<EventRegistrationToken>(image.remove_ImageFailed), new ExceptionRoutedEventHandler(this.Image_ImageFailed));
				break;
			}
			case 5:
			{
				Image image = (Image)target;
				WindowsRuntimeMarshal.AddEventHandler<ExceptionRoutedEventHandler>(new Func<ExceptionRoutedEventHandler, EventRegistrationToken>(image.add_ImageFailed), new Action<EventRegistrationToken>(image.remove_ImageFailed), new ExceptionRoutedEventHandler(this.Image_ImageFailed));
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
				WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uielement.add_Tapped), new Action<EventRegistrationToken>(uielement.remove_Tapped), new TappedEventHandler(this.SpotlightCard_Tapped));
				break;
			}
			case 8:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.BackButton_Click));
				break;
			}
			case 9:
			{
				FrameworkElement frameworkElement = (FrameworkElement)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(frameworkElement.add_Loaded), new Action<EventRegistrationToken>(frameworkElement.remove_Loaded), new RoutedEventHandler(this.AccountSection_Loaded));
				break;
			}
			case 10:
			{
				FrameworkElement frameworkElement = (FrameworkElement)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(frameworkElement.add_Loaded), new Action<EventRegistrationToken>(frameworkElement.remove_Loaded), new RoutedEventHandler(this.FeaturedContainer_Loaded));
				break;
			}
			case 11:
			{
				FrameworkElement frameworkElement = (FrameworkElement)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(frameworkElement.add_Loaded), new Action<EventRegistrationToken>(frameworkElement.remove_Loaded), new RoutedEventHandler(this.FeaturedGridView_Loaded));
				break;
			}
			case 12:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.FeaturedSeeAllButton_Click));
				break;
			}
			case 13:
			{
				FrameworkElement frameworkElement = (FrameworkElement)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(frameworkElement.add_Loaded), new Action<EventRegistrationToken>(frameworkElement.remove_Loaded), new RoutedEventHandler(this.SpecialPicksContainer_Loaded));
				break;
			}
			case 14:
			{
				FrameworkElement frameworkElement = (FrameworkElement)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(frameworkElement.add_Loaded), new Action<EventRegistrationToken>(frameworkElement.remove_Loaded), new RoutedEventHandler(this.SpecialPicksGridView_Loaded));
				break;
			}
			case 15:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.StaffPicksSeeAllButton_Click));
				break;
			}
			case 16:
			{
				ListViewBase listViewBase = (ListViewBase)target;
				WindowsRuntimeMarshal.AddEventHandler<ItemClickEventHandler>(new Func<ItemClickEventHandler, EventRegistrationToken>(listViewBase.add_ItemClick), new Action<EventRegistrationToken>(listViewBase.remove_ItemClick), new ItemClickEventHandler(this.SpotlightCollectionTile_Click));
				break;
			}
			case 17:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.SeeAllCollectionsButton_Click));
				break;
			}
			case 18:
			{
				FrameworkElement frameworkElement = (FrameworkElement)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(frameworkElement.add_Loaded), new Action<EventRegistrationToken>(frameworkElement.remove_Loaded), new RoutedEventHandler(this.SpotlightRight_Loaded));
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<PointerEventHandler>(new Func<PointerEventHandler, EventRegistrationToken>(uielement.add_PointerEntered), new Action<EventRegistrationToken>(uielement.remove_PointerEntered), new PointerEventHandler(this.SpotlightRight_PointerEntered));
				uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<PointerEventHandler>(new Func<PointerEventHandler, EventRegistrationToken>(uielement.add_PointerExited), new Action<EventRegistrationToken>(uielement.remove_PointerExited), new PointerEventHandler(this.SpotlightRight_PointerExited));
				Selector selector = (Selector)target;
				WindowsRuntimeMarshal.AddEventHandler<SelectionChangedEventHandler>(new Func<SelectionChangedEventHandler, EventRegistrationToken>(selector.add_SelectionChanged), new Action<EventRegistrationToken>(selector.remove_SelectionChanged), new SelectionChangedEventHandler(this.SpotlightRight_SelectionChanged));
				frameworkElement = (FrameworkElement)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(frameworkElement.add_Unloaded), new Action<EventRegistrationToken>(frameworkElement.remove_Unloaded), new RoutedEventHandler(this.SpotlightRight_Unloaded));
				break;
			}
			case 19:
			{
				ListViewBase listViewBase = (ListViewBase)target;
				WindowsRuntimeMarshal.AddEventHandler<ItemClickEventHandler>(new Func<ItemClickEventHandler, EventRegistrationToken>(listViewBase.add_ItemClick), new Action<EventRegistrationToken>(listViewBase.remove_ItemClick), new ItemClickEventHandler(this.SpotlightLeft_ItemClick));
				Selector selector = (Selector)target;
				WindowsRuntimeMarshal.AddEventHandler<SelectionChangedEventHandler>(new Func<SelectionChangedEventHandler, EventRegistrationToken>(selector.add_SelectionChanged), new Action<EventRegistrationToken>(selector.remove_SelectionChanged), new SelectionChangedEventHandler(this.SpotlightLeft_SelectionChanged));
				break;
			}
			case 20:
			{
				Image image = (Image)target;
				WindowsRuntimeMarshal.AddEventHandler<ExceptionRoutedEventHandler>(new Func<ExceptionRoutedEventHandler, EventRegistrationToken>(image.add_ImageFailed), new Action<EventRegistrationToken>(image.remove_ImageFailed), new ExceptionRoutedEventHandler(this.Image_ImageFailed));
				break;
			}
			case 21:
			{
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uielement.add_Tapped), new Action<EventRegistrationToken>(uielement.remove_Tapped), new TappedEventHandler(this.Border_Tapped_1));
				break;
			}
			case 22:
			{
				SearchBox searchBox = (SearchBox)target;
				WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<SearchBox, SearchBoxQuerySubmittedEventArgs>>(new Func<TypedEventHandler<SearchBox, SearchBoxQuerySubmittedEventArgs>, EventRegistrationToken>(searchBox.add_QuerySubmitted), new Action<EventRegistrationToken>(searchBox.remove_QuerySubmitted), new TypedEventHandler<SearchBox, SearchBoxQuerySubmittedEventArgs>(this.nbSearch_QuerySubmitted));
				break;
			}
			case 23:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.DownloadHubButton_Click));
				break;
			}
			case 24:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.TopAppsButton_Click));
				break;
			}
			case 25:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.AccountButton_Click));
				break;
			}
			}
			this._contentLoaded = true;
		}

		// Token: 0x0400015D RID: 349
		private const string VERSION_CHECK_URL = "https://8store.modyleprojects.ru/updates/version.json";

		// Token: 0x0400015E RID: 350
		private const string UPDATE_URL = "https://8store.modyleprojects.ru/updates/update.appx";

		// Token: 0x0400015F RID: 351
		private const string AppsJsonUrl = "https://8store.modyleprojects.ru/data/apps.json";

		// Token: 0x04000160 RID: 352
		private bool _isDataLoaded;

		// Token: 0x04000161 RID: 353
		private const string CACHE_FOLDER_NAME = "AppDataCache";

		// Token: 0x04000162 RID: 354
		private const string APPS_CACHE_FILE = "apps_cache.json";

		// Token: 0x04000163 RID: 355
		private const int CACHE_EXPIRY_MINUTES = 5;

		// Token: 0x04000164 RID: 356
		private ObservableCollection<StoreApp> allApps = new ObservableCollection<StoreApp>();

		// Token: 0x04000165 RID: 357
		private ObservableCollection<StoreApp> specialPicksApps = new ObservableCollection<StoreApp>();

		// Token: 0x04000166 RID: 358
		private ObservableCollection<StoreApp> featuredApps = new ObservableCollection<StoreApp>();

		// Token: 0x04000167 RID: 359
		private ObservableCollection<StoreApp> specialPicksVisibleApps = new ObservableCollection<StoreApp>();

		// Token: 0x04000168 RID: 360
		private ObservableCollection<StoreApp> featuredVisibleApps = new ObservableCollection<StoreApp>();

		// Token: 0x04000169 RID: 361
		private ObservableCollection<StoreApp> topFreeApps = new ObservableCollection<StoreApp>();

		// Token: 0x0400016A RID: 362
		private ObservableCollection<AppCollection> spotlightCollections = new ObservableCollection<AppCollection>();

		// Token: 0x0400016B RID: 363
		private List<StoreApp> allAppsForStats;

		// Token: 0x0400016C RID: 364
		private ObservableCollection<StoreApp> randomApps = new ObservableCollection<StoreApp>();

		// Token: 0x0400016D RID: 365
		private GridView _specialPicksGridView;

		// Token: 0x0400016E RID: 366
		private GridView _featuredGridView;

		// Token: 0x0400016F RID: 367
		private bool _sectionAnimationsPrimed;

		// Token: 0x04000170 RID: 368
		private bool _hasCheckedForUpdates;

		// Token: 0x04000171 RID: 369
		private StorageFolder _cacheFolder;

		// Token: 0x04000172 RID: 370
		private StorageFile _appsCacheFile;

		// Token: 0x04000173 RID: 371
		private DateTime _lastCacheUpdate = DateTime.MinValue;

		// Token: 0x04000174 RID: 372
		private bool _isCacheValid;

		// Token: 0x04000175 RID: 373
		private bool _isLoadingFromCache;

		// Token: 0x04000176 RID: 374
		private bool _isCacheDirty;

		// Token: 0x04000177 RID: 375
		private bool _isSavingCache;

		// Token: 0x04000178 RID: 376
		private DownloadOperation downloadOperation;

		// Token: 0x04000179 RID: 377
		private BackgroundDownloader downloader;

		// Token: 0x0400017A RID: 378
		private StorageFile downloadedFile;

		// Token: 0x0400017B RID: 379
		private Dictionary<FlipView, DispatcherTimer> _timers = new Dictionary<FlipView, DispatcherTimer>();

		// Token: 0x0400017C RID: 380
		private DispatcherTimer _spotlightTimer;

		// Token: 0x0400017D RID: 381
		private bool _syncingSpotlight;

		// Token: 0x04000180 RID: 384
		private CancellationTokenSource _jsonLoadingCancellationTokenSource;

		// Token: 0x04000181 RID: 385
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Page MainStorePage;

		// Token: 0x04000182 RID: 386
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Hub MainHub;

		// Token: 0x04000183 RID: 387
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button backButton;

		// Token: 0x04000184 RID: 388
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock AppTitle;

		// Token: 0x04000185 RID: 389
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock indevText;

		// Token: 0x04000186 RID: 390
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private HubSection SpotlightSection;

		// Token: 0x04000187 RID: 391
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private SearchBox nbSearch;

		// Token: 0x04000188 RID: 392
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button DownloadHubButton;

		// Token: 0x04000189 RID: 393
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button CollectionsButton;

		// Token: 0x0400018A RID: 394
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button AccountButton;

		// Token: 0x0400018B RID: 395
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock nbLogoText;

		// Token: 0x0400018C RID: 396
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private VisualState PrimaryView;

		// Token: 0x0400018D RID: 397
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private VisualState SnappedView;

		// Token: 0x0400018E RID: 398
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
