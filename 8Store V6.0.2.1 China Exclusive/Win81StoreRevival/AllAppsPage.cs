using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media.Imaging;

namespace Win81StoreRevival
{
	// Token: 0x02000017 RID: 23
	public sealed class AllAppsPage : Page, IComponentConnector
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00004D2F File Offset: 0x00002F2F
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x00004D37 File Offset: 0x00002F37
		public ObservableCollection<AppCollection> AllCollections { get; set; } = new ObservableCollection<AppCollection>();

		// Token: 0x060000F4 RID: 244 RVA: 0x00004D40 File Offset: 0x00002F40
		public AllAppsPage()
		{
			List<string> list = new List<string>();
			list.Add("Books + Reference");
			list.Add("Education");
			list.Add("Finance");
			list.Add("Food + Cooking");
			list.Add("Government");
			list.Add("Health + Fitness");
			list.Add("Music + Audio");
			list.Add("News + Weather");
			list.Add("Other");
			list.Add("Photo + Design");
			list.Add("Security");
			list.Add("Shopping");
			list.Add("Social");
			list.Add("Sports");
			list.Add("Travel + Navigation");
			list.Add("Utilities");
			list.Add("Video + Entertainment");
			this.appCategories = list;
			List<string> list2 = new List<string>();
			list2.Add("Action");
			list2.Add("Adventure");
			list2.Add("Arcade");
			list2.Add("Card + Casino");
			list2.Add("Classics");
			list2.Add("Family + Kids");
			list2.Add("Music + Rhythm");
			list2.Add("Puzzle");
			list2.Add("Racing + Flying");
			list2.Add("Strategy");
			list2.Add("Simulation");
			this.gamesCategories = list2;
			base..ctor();
			this.InitializeComponent();
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(this.add_Loaded), new Action<EventRegistrationToken>(this.remove_Loaded), new RoutedEventHandler(this.AllAppsPage_LoadedAsync));
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004F04 File Offset: 0x00003104
		[DebuggerStepThrough]
		private void AllAppsPage_LoadedAsync(object sender, RoutedEventArgs e)
		{
			AllAppsPage.<AllAppsPage_LoadedAsync>d__14 <AllAppsPage_LoadedAsync>d__ = new AllAppsPage.<AllAppsPage_LoadedAsync>d__14();
			<AllAppsPage_LoadedAsync>d__.<>4__this = this;
			<AllAppsPage_LoadedAsync>d__.sender = sender;
			<AllAppsPage_LoadedAsync>d__.e = e;
			<AllAppsPage_LoadedAsync>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<AllAppsPage_LoadedAsync>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <AllAppsPage_LoadedAsync>d__.<>t__builder;
			<>t__builder.Start<AllAppsPage.<AllAppsPage_LoadedAsync>d__14>(ref <AllAppsPage_LoadedAsync>d__);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004F50 File Offset: 0x00003150
		[DebuggerStepThrough]
		private Task InitializeCacheAsync()
		{
			AllAppsPage.<InitializeCacheAsync>d__15 <InitializeCacheAsync>d__ = new AllAppsPage.<InitializeCacheAsync>d__15();
			<InitializeCacheAsync>d__.<>4__this = this;
			<InitializeCacheAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<InitializeCacheAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <InitializeCacheAsync>d__.<>t__builder;
			<>t__builder.Start<AllAppsPage.<InitializeCacheAsync>d__15>(ref <InitializeCacheAsync>d__);
			return <InitializeCacheAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004F98 File Offset: 0x00003198
		[DebuggerStepThrough]
		private Task<StorageFile> GetCacheFileAsync()
		{
			AllAppsPage.<GetCacheFileAsync>d__16 <GetCacheFileAsync>d__ = new AllAppsPage.<GetCacheFileAsync>d__16();
			<GetCacheFileAsync>d__.<>4__this = this;
			<GetCacheFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<StorageFile>.Create();
			<GetCacheFileAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<StorageFile> <>t__builder = <GetCacheFileAsync>d__.<>t__builder;
			<>t__builder.Start<AllAppsPage.<GetCacheFileAsync>d__16>(ref <GetCacheFileAsync>d__);
			return <GetCacheFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004FE0 File Offset: 0x000031E0
		[DebuggerStepThrough]
		private Task<bool> TryLoadAppsFromDiskCache()
		{
			AllAppsPage.<TryLoadAppsFromDiskCache>d__17 <TryLoadAppsFromDiskCache>d__ = new AllAppsPage.<TryLoadAppsFromDiskCache>d__17();
			<TryLoadAppsFromDiskCache>d__.<>4__this = this;
			<TryLoadAppsFromDiskCache>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<TryLoadAppsFromDiskCache>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<bool> <>t__builder = <TryLoadAppsFromDiskCache>d__.<>t__builder;
			<>t__builder.Start<AllAppsPage.<TryLoadAppsFromDiskCache>d__17>(ref <TryLoadAppsFromDiskCache>d__);
			return <TryLoadAppsFromDiskCache>d__.<>t__builder.Task;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00005028 File Offset: 0x00003228
		[DebuggerStepThrough]
		private Task SaveToDiskCacheAsync(List<StoreApp> apps)
		{
			AllAppsPage.<SaveToDiskCacheAsync>d__18 <SaveToDiskCacheAsync>d__ = new AllAppsPage.<SaveToDiskCacheAsync>d__18();
			<SaveToDiskCacheAsync>d__.<>4__this = this;
			<SaveToDiskCacheAsync>d__.apps = apps;
			<SaveToDiskCacheAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SaveToDiskCacheAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <SaveToDiskCacheAsync>d__.<>t__builder;
			<>t__builder.Start<AllAppsPage.<SaveToDiskCacheAsync>d__18>(ref <SaveToDiskCacheAsync>d__);
			return <SaveToDiskCacheAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005078 File Offset: 0x00003278
		[DebuggerStepThrough]
		private Task LoadAllAppsFromJsonAsync()
		{
			AllAppsPage.<LoadAllAppsFromJsonAsync>d__19 <LoadAllAppsFromJsonAsync>d__ = new AllAppsPage.<LoadAllAppsFromJsonAsync>d__19();
			<LoadAllAppsFromJsonAsync>d__.<>4__this = this;
			<LoadAllAppsFromJsonAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadAllAppsFromJsonAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadAllAppsFromJsonAsync>d__.<>t__builder;
			<>t__builder.Start<AllAppsPage.<LoadAllAppsFromJsonAsync>d__19>(ref <LoadAllAppsFromJsonAsync>d__);
			return <LoadAllAppsFromJsonAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000050C0 File Offset: 0x000032C0
		[DebuggerStepThrough]
		private Task LoadReviewStatsInBackground()
		{
			AllAppsPage.<LoadReviewStatsInBackground>d__20 <LoadReviewStatsInBackground>d__ = new AllAppsPage.<LoadReviewStatsInBackground>d__20();
			<LoadReviewStatsInBackground>d__.<>4__this = this;
			<LoadReviewStatsInBackground>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadReviewStatsInBackground>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadReviewStatsInBackground>d__.<>t__builder;
			<>t__builder.Start<AllAppsPage.<LoadReviewStatsInBackground>d__20>(ref <LoadReviewStatsInBackground>d__);
			return <LoadReviewStatsInBackground>d__.<>t__builder.Task;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00005108 File Offset: 0x00003308
		private void GenerateCollections(List<StoreApp> apps)
		{
			try
			{
				Random random = new Random();
				List<StoreApp> list = Enumerable.ToList<StoreApp>(apps);
				this.AllCollections.Clear();
				List<StoreApp> list2 = Enumerable.ToList<StoreApp>(Enumerable.Where<StoreApp>(list, (StoreApp a) => a.Type == "App"));
				using (List<string>.Enumerator enumerator = this.appCategories.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string cat = enumerator.Current;
						List<StoreApp> list3 = Enumerable.ToList<StoreApp>(Enumerable.Where<StoreApp>(list2, (StoreApp a) => a.Category == cat));
						bool flag = Enumerable.Any<StoreApp>(list3);
						if (flag)
						{
							this.AllCollections.Add(new AppCollection
							{
								Name = cat,
								AppCount = string.Format("{0} apps", new object[]
								{
									list3.Count
								}),
								AppIcons = this.GetRandomIcons(list3, random, 4)
							});
						}
					}
				}
				List<StoreApp> list4 = Enumerable.ToList<StoreApp>(Enumerable.Where<StoreApp>(list, (StoreApp a) => a.Type == "Game"));
				using (List<string>.Enumerator enumerator2 = this.gamesCategories.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						string cat = enumerator2.Current;
						List<StoreApp> list5 = Enumerable.ToList<StoreApp>(Enumerable.Where<StoreApp>(list4, (StoreApp a) => a.Category == cat));
						bool flag2 = Enumerable.Any<StoreApp>(list5);
						if (flag2)
						{
							this.AllCollections.Add(new AppCollection
							{
								Name = cat,
								AppCount = string.Format("{0} games", new object[]
								{
									list5.Count
								}),
								AppIcons = this.GetRandomIcons(list5, random, 4)
							});
						}
					}
				}
				List<StoreApp> list6 = Enumerable.ToList<StoreApp>(Enumerable.Where<StoreApp>(list, (StoreApp a) => a.Publisher != null && a.Publisher.Contains("Microsoft")));
				List<StoreApp> list7 = Enumerable.ToList<StoreApp>(Enumerable.Where<StoreApp>(list, (StoreApp a) => a.Featured));
				bool flag3 = Enumerable.Any<StoreApp>(list6);
				if (flag3)
				{
					this.AllCollections.Add(new AppCollection
					{
						Name = "Microsoft apps",
						AppCount = string.Format("{0} apps", new object[]
						{
							list6.Count
						}),
						AppIcons = this.GetRandomIcons(list6, random, 4)
					});
				}
				bool flag4 = Enumerable.Any<StoreApp>(list7);
				if (flag4)
				{
					this.AllCollections.Add(new AppCollection
					{
						Name = "Staff picks",
						AppCount = string.Format("{0} apps", new object[]
						{
							list7.Count
						}),
						AppIcons = this.GetRandomIcons(list7, random, 4)
					});
				}
				this.AllCollections.Add(new AppCollection
				{
					Name = "Essential apps",
					AppCount = string.Format("{0} apps", new object[]
					{
						list.Count
					}),
					AppIcons = this.GetRandomIcons(list, random, 4)
				});
				this.AllCollections.Add(new AppCollection
				{
					Name = "Surface picks",
					AppCount = string.Format("{0} apps", new object[]
					{
						list.Count
					}),
					AppIcons = this.GetRandomIcons(list, random, 4)
				});
				this.AllCollections.Add(new AppCollection
				{
					Name = "Keep connected",
					AppCount = string.Format("{0} apps", new object[]
					{
						list.Count
					}),
					AppIcons = this.GetRandomIcons(list, random, 4)
				});
				bool flag5 = Enumerable.Any<StoreApp>(list6);
				if (flag5)
				{
					this.AllCollections.Add(new AppCollection
					{
						Name = "Microsoft Office",
						AppCount = string.Format("{0} apps", new object[]
						{
							list6.Count
						}),
						AppIcons = this.GetRandomIcons(list6, random, 4)
					});
				}
				this.AllCollections.Add(new AppCollection
				{
					Name = "Great on 8",
					AppCount = string.Format("{0} apps", new object[]
					{
						list.Count
					}),
					AppIcons = this.GetRandomIcons(list, random, 4)
				});
				List<StoreApp> list8 = Enumerable.ToList<StoreApp>(Enumerable.Where<StoreApp>(list, (StoreApp a) => a.Category == "Food + Cooking"));
				bool flag6 = Enumerable.Any<StoreApp>(list8);
				if (flag6)
				{
					this.AllCollections.Add(new AppCollection
					{
						Name = "Kitchen helpers",
						AppCount = string.Format("{0} apps", new object[]
						{
							list8.Count
						}),
						AppIcons = this.GetRandomIcons(list8, random, 4)
					});
				}
				Debug.WriteLine(string.Format("Collections generated with YOUR categories: {0} total collections", new object[]
				{
					this.AllCollections.Count
				}));
			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("Error generating collections: {0}", new object[]
				{
					ex.Message
				}));
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005750 File Offset: 0x00003950
		private List<string> GetRandomIcons(List<StoreApp> sourceApps, Random random, int count)
		{
			List<string> list = new List<string>();
			bool flag = sourceApps == null || sourceApps.Count == 0;
			List<string> result;
			if (flag)
			{
				for (int i = 0; i < count; i++)
				{
					list.Add("https://i.ibb.co/Tx7VKhhW/noapp.png");
				}
				result = list;
			}
			else
			{
				List<StoreApp> list2 = Enumerable.ToList<StoreApp>(Enumerable.Take<StoreApp>(Enumerable.OrderBy<StoreApp, int>(sourceApps, (StoreApp x) => random.Next()), count));
				for (int j = 0; j < count; j++)
				{
					bool flag2 = j < list2.Count && list2[j].IconUrl != null;
					if (flag2)
					{
						list.Add(list2[j].IconUrl);
					}
					else
					{
						list.Add("https://i.ibb.co/Tx7VKhhW/noapp.png");
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005833 File Offset: 0x00003A33
		private void PlaySound()
		{
			SoundManager.PlayStartupSound();
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000583C File Offset: 0x00003A3C
		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			bool canGoBack = base.Frame.CanGoBack;
			if (canGoBack)
			{
				base.Frame.GoBack();
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005867 File Offset: 0x00003A67
		private void TopAppsButton_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(AllAppsPage));
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005880 File Offset: 0x00003A80
		private void TopAppsButton_Click_1(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(PopularPage));
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005899 File Offset: 0x00003A99
		private void DownloadHubButton_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(DownloadsHub));
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000058B2 File Offset: 0x00003AB2
		private void ExclusiveHubButton_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(AccountPage));
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000058CC File Offset: 0x00003ACC
		private void CollectionTile_Click(object sender, ItemClickEventArgs e)
		{
			AppCollection appCollection = e.ClickedItem as AppCollection;
			bool flag = appCollection != null;
			if (flag)
			{
				base.Frame.Navigate(typeof(CollectionsViewPage), appCollection.Name);
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0000590C File Offset: 0x00003B0C
		private void CollectionTile_Tapped(object sender, TappedRoutedEventArgs e)
		{
			Grid grid = sender as Grid;
			bool flag = grid != null;
			if (flag)
			{
				AppCollection appCollection = grid.DataContext as AppCollection;
				bool flag2 = appCollection != null;
				if (flag2)
				{
					base.Frame.Navigate(typeof(CollectionsViewPage), appCollection.Name);
				}
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000595D File Offset: 0x00003B5D
		private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005960 File Offset: 0x00003B60
		private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
		{
			Image image = sender as Image;
			bool flag = image != null;
			if (flag)
			{
				try
				{
					image.put_Source(new BitmapImage(new Uri("https://i.ibb.co/Tx7VKhhW/noapp.png")));
				}
				catch
				{
				}
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000059B0 File Offset: 0x00003BB0
		private void CollectionsGridView_Loaded(object sender, RoutedEventArgs e)
		{
			this.collectionsGridView = (sender as GridView);
			bool flag = this.collectionsGridView != null;
			if (flag)
			{
				this.collectionsGridView.put_ItemsSource(this.AllCollections);
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000059EC File Offset: 0x00003BEC
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			bool contentLoaded = this._contentLoaded;
			if (!contentLoaded)
			{
				this._contentLoaded = true;
				Application.LoadComponent(this, new Uri("ms-appx:///AllAppsPage.xaml"), 0);
				this.MainContentGrid = (Grid)base.FindName("MainContentGrid");
				this.LoadingPanel = (Grid)base.FindName("LoadingPanel");
				this.AllAppsHub = (Hub)base.FindName("AllAppsHub");
				this.NoResultsPanel = (StackPanel)base.FindName("NoResultsPanel");
				this.backButton = (Button)base.FindName("backButton");
				this.resultText = (TextBlock)base.FindName("resultText");
				this.CollectionsSection = (HubSection)base.FindName("CollectionsSection");
				this.LoadingRing = (ProgressRing)base.FindName("LoadingRing");
				this.LoadingText = (TextBlock)base.FindName("LoadingText");
				this.SearchResultsText = (TextBlock)base.FindName("SearchResultsText");
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00005B00 File Offset: 0x00003D00
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
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.BackButton_Click));
				break;
			}
			case 7:
			{
				ListViewBase listViewBase = (ListViewBase)target;
				WindowsRuntimeMarshal.AddEventHandler<ItemClickEventHandler>(new Func<ItemClickEventHandler, EventRegistrationToken>(listViewBase.add_ItemClick), new Action<EventRegistrationToken>(listViewBase.remove_ItemClick), new ItemClickEventHandler(this.CollectionTile_Click));
				FrameworkElement frameworkElement = (FrameworkElement)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(frameworkElement.add_Loaded), new Action<EventRegistrationToken>(frameworkElement.remove_Loaded), new RoutedEventHandler(this.CollectionsGridView_Loaded));
				break;
			}
			}
			this._contentLoaded = true;
		}

		// Token: 0x0400006F RID: 111
		private ObservableCollection<StoreApp> allApps = new ObservableCollection<StoreApp>();

		// Token: 0x04000070 RID: 112
		private const string AppsJsonUrl = "https://8store.modyleprojects.ru/data/apps.json";

		// Token: 0x04000071 RID: 113
		private GridView collectionsGridView;

		// Token: 0x04000072 RID: 114
		private const string CACHE_FOLDER_NAME = "AppDataCache";

		// Token: 0x04000073 RID: 115
		private const string APPS_CACHE_FILE = "apps_cache.json";

		// Token: 0x04000074 RID: 116
		private StorageFolder _cacheFolder;

		// Token: 0x04000075 RID: 117
		private StorageFile _appsCacheFile;

		// Token: 0x04000077 RID: 119
		private List<string> appCategories;

		// Token: 0x04000078 RID: 120
		private List<string> gamesCategories;

		// Token: 0x04000079 RID: 121
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Grid MainContentGrid;

		// Token: 0x0400007A RID: 122
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Grid LoadingPanel;

		// Token: 0x0400007B RID: 123
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Hub AllAppsHub;

		// Token: 0x0400007C RID: 124
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel NoResultsPanel;

		// Token: 0x0400007D RID: 125
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button backButton;

		// Token: 0x0400007E RID: 126
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock resultText;

		// Token: 0x0400007F RID: 127
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private HubSection CollectionsSection;

		// Token: 0x04000080 RID: 128
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ProgressRing LoadingRing;

		// Token: 0x04000081 RID: 129
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock LoadingText;

		// Token: 0x04000082 RID: 130
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock SearchResultsText;

		// Token: 0x04000083 RID: 131
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
