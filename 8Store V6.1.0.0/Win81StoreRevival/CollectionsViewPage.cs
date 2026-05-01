using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace Win81StoreRevival
{
	// Token: 0x02000026 RID: 38
	public sealed class CollectionsViewPage : Page, IComponentConnector
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000B13B File Offset: 0x0000933B
		// (set) Token: 0x06000229 RID: 553 RVA: 0x0000B143 File Offset: 0x00009343
		public ObservableCollection<AppCollection> AllCollections { get; set; } = new ObservableCollection<AppCollection>();

		// Token: 0x0600022A RID: 554 RVA: 0x0000B14C File Offset: 0x0000934C
		public CollectionsViewPage()
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
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(this.add_Unloaded), new Action<EventRegistrationToken>(this.remove_Unloaded), new RoutedEventHandler(this.CollectionsViewPage_Unloaded));
			Window window = Window.Current;
			WindowsRuntimeMarshal.AddEventHandler<WindowSizeChangedEventHandler>(new Func<WindowSizeChangedEventHandler, EventRegistrationToken>(window.add_SizeChanged), new Action<EventRegistrationToken>(window.remove_SizeChanged), new WindowSizeChangedEventHandler(this.OnPageSizeChanged));
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000B374 File Offset: 0x00009574
		private void AllAppsPage_LoadedAsync(object sender, RoutedEventArgs e)
		{
			CollectionsViewPage.<AllAppsPage_LoadedAsync>d__18 <AllAppsPage_LoadedAsync>d__;
			<AllAppsPage_LoadedAsync>d__.<>4__this = this;
			<AllAppsPage_LoadedAsync>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<AllAppsPage_LoadedAsync>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <AllAppsPage_LoadedAsync>d__.<>t__builder;
			<>t__builder.Start<CollectionsViewPage.<AllAppsPage_LoadedAsync>d__18>(ref <AllAppsPage_LoadedAsync>d__);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00004859 File Offset: 0x00002A59
		private void StoreLogo_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(MainPage));
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00006009 File Offset: 0x00004209
		private void Border_Tapped_1(object sender, TappedRoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(AllAppsPage));
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000B3B0 File Offset: 0x000095B0
		private Task LoadAllAppsFromJsonAsync()
		{
			CollectionsViewPage.<LoadAllAppsFromJsonAsync>d__21 <LoadAllAppsFromJsonAsync>d__;
			<LoadAllAppsFromJsonAsync>d__.<>4__this = this;
			<LoadAllAppsFromJsonAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadAllAppsFromJsonAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadAllAppsFromJsonAsync>d__.<>t__builder;
			<>t__builder.Start<CollectionsViewPage.<LoadAllAppsFromJsonAsync>d__21>(ref <LoadAllAppsFromJsonAsync>d__);
			return <LoadAllAppsFromJsonAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000B3F8 File Offset: 0x000095F8
		private void GenerateCollections(List<StoreApp> apps)
		{
			try
			{
				Random random = new Random();
				List<StoreApp> list = Enumerable.ToList<StoreApp>(apps);
				ResourceLoader forCurrentView = ResourceLoader.GetForCurrentView();
				this.AllCollections.Clear();
				List<StoreApp> list2 = Enumerable.ToList<StoreApp>(Enumerable.Where<StoreApp>(list, (StoreApp a) => a.Type == "App"));
				using (List<string>.Enumerator enumerator = this.appCategories.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string cat = enumerator.Current;
						List<StoreApp> list3 = Enumerable.ToList<StoreApp>(Enumerable.Where<StoreApp>(list2, (StoreApp a) => a.Category == cat));
						if (Enumerable.Any<StoreApp>(list3))
						{
							this.AllCollections.Add(new AppCollection
							{
								Name = cat,
								DisplayName = this.GetCollectionDisplayName(forCurrentView, cat),
								AppCount = this.GetAppCountText(forCurrentView, list3.Count),
								AppIcons = this.GetRandomIcons(list3, random, 4)
							});
						}
					}
				}
				List<StoreApp> list4 = Enumerable.ToList<StoreApp>(Enumerable.Where<StoreApp>(list, (StoreApp a) => a.Type == "Game"));
				using (List<string>.Enumerator enumerator = this.gamesCategories.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string cat = enumerator.Current;
						List<StoreApp> list5 = Enumerable.ToList<StoreApp>(Enumerable.Where<StoreApp>(list4, (StoreApp a) => a.Category == cat));
						if (Enumerable.Any<StoreApp>(list5))
						{
							this.AllCollections.Add(new AppCollection
							{
								Name = cat,
								DisplayName = this.GetCollectionDisplayName(forCurrentView, cat),
								AppCount = this.GetGameCountText(forCurrentView, list5.Count),
								AppIcons = this.GetRandomIcons(list5, random, 4)
							});
						}
					}
				}
				List<StoreApp> list6 = Enumerable.ToList<StoreApp>(Enumerable.Where<StoreApp>(list, (StoreApp a) => a.Publisher != null && a.Publisher.Contains("Microsoft")));
				List<StoreApp> list7 = Enumerable.ToList<StoreApp>(Enumerable.Where<StoreApp>(list, (StoreApp a) => a.Featured));
				if (Enumerable.Any<StoreApp>(list6))
				{
					this.AllCollections.Add(new AppCollection
					{
						Name = "Microsoft apps",
						DisplayName = this.GetCollectionDisplayName(forCurrentView, "Microsoft apps"),
						AppCount = this.GetAppCountText(forCurrentView, list6.Count),
						AppIcons = this.GetRandomIcons(list6, random, 4)
					});
				}
				if (Enumerable.Any<StoreApp>(list7))
				{
					this.AllCollections.Add(new AppCollection
					{
						Name = "Staff picks",
						DisplayName = this.GetCollectionDisplayName(forCurrentView, "Staff picks"),
						AppCount = this.GetAppCountText(forCurrentView, list7.Count),
						AppIcons = this.GetRandomIcons(list7, random, 4)
					});
				}
				this.AllCollections.Add(new AppCollection
				{
					Name = "Essential apps",
					DisplayName = this.GetCollectionDisplayName(forCurrentView, "Essential apps"),
					AppCount = this.GetAppCountText(forCurrentView, list.Count),
					AppIcons = this.GetRandomIcons(list, random, 4)
				});
				this.AllCollections.Add(new AppCollection
				{
					Name = "Surface picks",
					DisplayName = this.GetCollectionDisplayName(forCurrentView, "Surface picks"),
					AppCount = this.GetAppCountText(forCurrentView, list.Count),
					AppIcons = this.GetRandomIcons(list, random, 4)
				});
				this.AllCollections.Add(new AppCollection
				{
					Name = "Keep connected",
					DisplayName = this.GetCollectionDisplayName(forCurrentView, "Keep connected"),
					AppCount = this.GetAppCountText(forCurrentView, list.Count),
					AppIcons = this.GetRandomIcons(list, random, 4)
				});
				if (Enumerable.Any<StoreApp>(list6))
				{
					this.AllCollections.Add(new AppCollection
					{
						Name = "Microsoft Office",
						DisplayName = this.GetCollectionDisplayName(forCurrentView, "Microsoft Office"),
						AppCount = this.GetAppCountText(forCurrentView, list6.Count),
						AppIcons = this.GetRandomIcons(list6, random, 4)
					});
				}
				this.AllCollections.Add(new AppCollection
				{
					Name = "Great on 8",
					DisplayName = this.GetCollectionDisplayName(forCurrentView, "Great on 8"),
					AppCount = this.GetAppCountText(forCurrentView, list.Count),
					AppIcons = this.GetRandomIcons(list, random, 4)
				});
				List<StoreApp> list8 = Enumerable.ToList<StoreApp>(Enumerable.Where<StoreApp>(list, (StoreApp a) => a.Category == "Food + Cooking"));
				if (Enumerable.Any<StoreApp>(list8))
				{
					this.AllCollections.Add(new AppCollection
					{
						Name = "Kitchen helpers",
						DisplayName = this.GetCollectionDisplayName(forCurrentView, "Kitchen helpers"),
						AppCount = this.GetAppCountText(forCurrentView, list8.Count),
						AppIcons = this.GetRandomIcons(list8, random, 4)
					});
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000B96C File Offset: 0x00009B6C
		private List<string> GetRandomIcons(List<StoreApp> sourceApps, Random random, int count)
		{
			List<string> list = new List<string>();
			if (sourceApps == null || sourceApps.Count == 0)
			{
				for (int i = 0; i < count; i++)
				{
					list.Add("ms-appx:///Assets");
				}
				return list;
			}
			List<StoreApp> list2 = Enumerable.ToList<StoreApp>(Enumerable.Take<StoreApp>(Enumerable.OrderBy<StoreApp, int>(sourceApps, (StoreApp x) => random.Next()), count));
			for (int j = 0; j < count; j++)
			{
				if (j < list2.Count && list2[j].IconUrl != null)
				{
					list.Add(list2[j].IconUrl);
				}
				else
				{
					list.Add("ms-appx:///Assets/icon-error.png");
				}
			}
			return list;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00005FE8 File Offset: 0x000041E8
		private void PlaySound()
		{
			SoundManager.PlayStartupSound();
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00005FEF File Offset: 0x000041EF
		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			if (base.Frame.CanGoBack)
			{
				base.Frame.GoBack();
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00006009 File Offset: 0x00004209
		private void TopAppsButton_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(AllAppsPage));
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00006021 File Offset: 0x00004221
		private void TopAppsButton_Click_1(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(PopularPage));
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00006039 File Offset: 0x00004239
		private void DownloadHubButton_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(DownloadsHub));
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000BA18 File Offset: 0x00009C18
		private void CollectionTile_Click(object sender, ItemClickEventArgs e)
		{
			AppCollection appCollection = e.ClickedItem as AppCollection;
			if (appCollection != null)
			{
				this.SwitchToAppsView(appCollection.Name);
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000BA40 File Offset: 0x00009C40
		private void CollectionTile_Tapped(object sender, TappedRoutedEventArgs e)
		{
			Grid grid = sender as Grid;
			if (grid != null)
			{
				AppCollection appCollection = grid.DataContext as AppCollection;
				if (appCollection != null)
				{
					this.SwitchToAppsView(appCollection.Name);
				}
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000BA74 File Offset: 0x00009C74
		private void AppItem_Click(object sender, ItemClickEventArgs e)
		{
			StoreApp storeApp = e.ClickedItem as StoreApp;
			if (storeApp != null)
			{
				base.Frame.Navigate(typeof(AppDetailsPage), storeApp);
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000BAA8 File Offset: 0x00009CA8
		private void AppItem_Tapped(object sender, TappedRoutedEventArgs e)
		{
			Grid grid = sender as Grid;
			if (grid != null)
			{
				StoreApp storeApp = grid.DataContext as StoreApp;
				if (storeApp != null)
				{
					base.Frame.Navigate(typeof(AppDetailsPage), storeApp);
				}
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000BAE5 File Offset: 0x00009CE5
		private void BackToCollectionsButton_Click(object sender, RoutedEventArgs e)
		{
			this.BackToCollectionsView();
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000BAF0 File Offset: 0x00009CF0
		private void SwitchToAppsView(string categoryName)
		{
			this.isCollectionsView = false;
			this.currentCategory = categoryName;
			this.CollectionsSection.put_Visibility(1);
			this.AppsSection.put_Visibility(0);
			this.CategoryTitleText.put_Visibility(0);
			this.CategoryTitleText.put_Text(this.GetCollectionDisplayName(ResourceLoader.GetForCurrentView(), categoryName));
			this.FilterAppsByCategory(categoryName);
			this.LoadingText.put_Text(ResourceLoader.GetForCurrentView().GetString("LoadingAppsText"));
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000BB68 File Offset: 0x00009D68
		private void BackToCollectionsView()
		{
			this.isCollectionsView = true;
			this.currentCategory = "";
			this.CollectionsSection.put_Visibility(0);
			this.AppsSection.put_Visibility(1);
			this.CategoryTitleText.put_Visibility(1);
			this.SearchResultsText.put_Visibility(1);
			this.NoResultsPanel.put_Visibility(1);
			this.LoadingText.put_Text(ResourceLoader.GetForCurrentView().GetString("LoadingCollectionsText"));
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000BBE0 File Offset: 0x00009DE0
		private void FilterAppsByCategory(string category)
		{
			List<StoreApp> appsForView = this.GetAppsForView(category);
			ResourceLoader forCurrentView = ResourceLoader.GetForCurrentView();
			this.filteredApps.Clear();
			foreach (StoreApp storeApp in appsForView)
			{
				this.filteredApps.Add(storeApp);
			}
			if (this.allAppsGridView != null)
			{
				this.RefreshVisibleApps();
			}
			if (this.filteredApps.Count > 0)
			{
				int count = this.filteredApps.Count;
				string text;
				if (count % 10 == 1 && count % 100 != 11)
				{
					text = "AppCounter";
				}
				else if (count % 10 >= 2 && count % 10 <= 4 && (count % 100 < 10 || count % 100 >= 20))
				{
					text = "AppsCounter1";
				}
				else
				{
					text = "AppsCounter";
				}
				this.SearchResultsText.put_Text(string.Format(forCurrentView.GetString(text), new object[]
				{
					count
				}));
				this.SearchResultsText.put_Visibility(0);
				this.NoResultsPanel.put_Visibility(1);
				return;
			}
			this.SearchResultsText.put_Visibility(1);
			this.NoResultsPanel.put_Visibility(0);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00004901 File Offset: 0x00002B01
		private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000BD14 File Offset: 0x00009F14
		private void nbSearch_QuerySubmitted(object sender, SearchBoxQuerySubmittedEventArgs e)
		{
			string text = (e != null && e.QueryText != null) ? e.QueryText.Trim() : null;
			if (!string.IsNullOrEmpty(text))
			{
				base.Frame.Navigate(typeof(SearchResultsPage), text);
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000BD5C File Offset: 0x00009F5C
		private void SearchApps(string searchText)
		{
			if (string.IsNullOrWhiteSpace(searchText))
			{
				this.FilterAppsByCategory(this.currentCategory);
				return;
			}
			List<StoreApp> list = Enumerable.ToList<StoreApp>(Enumerable.Where<StoreApp>(this.GetAppsForView(this.currentCategory), (StoreApp app) => (app.Name != null && app.Name.ToLower().Contains(searchText)) || (app.Publisher != null && app.Publisher.ToLower().Contains(searchText)) || (app.Description != null && app.Description.ToLower().Contains(searchText))));
			ResourceLoader.GetForCurrentView();
			this.filteredApps.Clear();
			foreach (StoreApp storeApp in list)
			{
				this.filteredApps.Add(storeApp);
			}
			this.UpdateSearchResultsCount(this.filteredApps.Count, true);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000BE1C File Offset: 0x0000A01C
		private void UpdateSearchResultsCount(int count, bool isSearch)
		{
			ResourceLoader forCurrentView = ResourceLoader.GetForCurrentView();
			if (count <= 0)
			{
				this.SearchResultsText.put_Visibility(1);
				this.NoResultsPanel.put_Visibility(0);
				return;
			}
			string text;
			if (isSearch)
			{
				if (count % 10 == 1 && count % 100 != 11)
				{
					text = "AppFoundCounter";
				}
				else if (count % 10 >= 2 && count % 10 <= 4 && (count % 100 < 10 || count % 100 >= 20))
				{
					text = "AppsFoundCounter1";
				}
				else
				{
					text = "AppsFoundCounter";
				}
			}
			else if (count % 10 == 1 && count % 100 != 11)
			{
				text = "AppCounter";
			}
			else if (count % 10 >= 2 && count % 10 <= 4 && (count % 100 < 10 || count % 100 >= 20))
			{
				text = "AppsCounter1";
			}
			else
			{
				text = "AppsCounter";
			}
			this.SearchResultsText.put_Text(string.Format(forCurrentView.GetString(text), new object[]
			{
				count
			}));
			this.SearchResultsText.put_Visibility(0);
			this.NoResultsPanel.put_Visibility(1);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000BF18 File Offset: 0x0000A118
		private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
		{
			Image image = sender as Image;
			if (image != null)
			{
				try
				{
					image.put_Source(new BitmapImage(new Uri("ms-appx:///Assets/icon-error.png")));
				}
				catch
				{
				}
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000BF5C File Offset: 0x0000A15C
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);
			string text = e.Parameter as string;
			if (!string.IsNullOrWhiteSpace(text))
			{
				this.currentCategory = text;
				this.isCollectionsView = false;
				this.CategoryTitleText.put_Visibility(0);
				this.CategoryTitleText.put_Text(this.GetCollectionDisplayName(ResourceLoader.GetForCurrentView(), text));
				this.CollectionsSection.put_Visibility(1);
				this.AppsSection.put_Visibility(0);
				return;
			}
			if (!this.isCollectionsView)
			{
				this.BackToCollectionsView();
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000BFDC File Offset: 0x0000A1DC
		private List<StoreApp> GetAppsForView(string category)
		{
			if (string.IsNullOrWhiteSpace(category))
			{
				return new List<StoreApp>();
			}
			if (category.Equals("Staff picks", 5))
			{
				return Enumerable.ToList<StoreApp>(Enumerable.Take<StoreApp>(Enumerable.Where<StoreApp>(this.allApps, (StoreApp app) => app != null && !string.IsNullOrWhiteSpace(app.Id) && !app.Id.StartsWith("settings")), 10));
			}
			if (category.Equals("Featured", 5))
			{
				return Enumerable.ToList<StoreApp>(Enumerable.Take<StoreApp>(Enumerable.Where<StoreApp>(this.allApps, (StoreApp app) => app != null && app.Featured), 12));
			}
			return Enumerable.ToList<StoreApp>(Enumerable.Where<StoreApp>(this.allApps, (StoreApp app) => app.Category != null && app.Category.Equals(category, 5)));
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000C0B9 File Offset: 0x0000A2B9
		private void CollectionsGridView_Loaded(object sender, RoutedEventArgs e)
		{
			this.collectionsGridView = (sender as GridView);
			if (this.collectionsGridView != null)
			{
				this.collectionsGridView.put_ItemsSource(this.AllCollections);
			}
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000C0E0 File Offset: 0x0000A2E0
		private void AllAppsGridView_Loaded(object sender, RoutedEventArgs e)
		{
			this.allAppsGridView = (sender as GridView);
			if (this.allAppsGridView != null)
			{
				this.RefreshVisibleApps();
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000C0FC File Offset: 0x0000A2FC
		private void CollectionsViewPage_Unloaded(object sender, RoutedEventArgs e)
		{
			WindowsRuntimeMarshal.RemoveEventHandler<WindowSizeChangedEventHandler>(new Action<EventRegistrationToken>(Window.Current.remove_SizeChanged), new WindowSizeChangedEventHandler(this.OnPageSizeChanged));
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000C120 File Offset: 0x0000A320
		private void OnPageSizeChanged(object sender, WindowSizeChangedEventArgs e)
		{
			this.RefreshVisibleApps();
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000C128 File Offset: 0x0000A328
		private void RefreshVisibleApps()
		{
			if (this.allAppsGridView == null)
			{
				return;
			}
			this.SyncVisibleCollection(this.allAppsGridView, this.filteredApps, this.visibleApps);
			this.allAppsGridView.put_ItemsSource(this.visibleApps);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000C15C File Offset: 0x0000A35C
		private void SyncVisibleCollection(GridView gridView, ObservableCollection<StoreApp> source, ObservableCollection<StoreApp> target)
		{
			target.Clear();
			if (gridView == null || source == null || source.Count == 0)
			{
				return;
			}
			foreach (StoreApp storeApp in source)
			{
				target.Add(storeApp);
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000C1BC File Offset: 0x0000A3BC
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

		// Token: 0x0600024C RID: 588 RVA: 0x0000C1EC File Offset: 0x0000A3EC
		private string GetCollectionDisplayKey(string category)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(category);
			if (num <= 2465108186U)
			{
				if (num <= 1456528693U)
				{
					if (num <= 496028322U)
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
							if (num == 496028322U)
							{
								if (category == "Health + Fitness")
								{
									return "CollectionCategory_HealthFitness";
								}
							}
						}
						else if (category == "Education")
						{
							return "CollectionCategory_Education";
						}
					}
					else if (num <= 1071417850U)
					{
						if (num != 1066741867U)
						{
							if (num == 1071417850U)
							{
								if (category == "Surface picks")
								{
									return "CollectionCategory_SurfacePicks";
								}
							}
						}
						else if (category == "Adventure")
						{
							return "CollectionCategory_Adventure";
						}
					}
					else if (num != 1213003503U)
					{
						if (num != 1315724598U)
						{
							if (num == 1456528693U)
							{
								if (category == "Family + Kids")
								{
									return "CollectionCategory_FamilyKids";
								}
							}
						}
						else if (category == "Simulation")
						{
							return "CollectionCategory_Simulation";
						}
					}
					else if (category == "Keep connected")
					{
						return "CollectionCategory_KeepConnected";
					}
				}
				else if (num <= 1849229205U)
				{
					if (num <= 1584519634U)
					{
						if (num != 1545232503U)
						{
							if (num == 1584519634U)
							{
								if (category == "Travel + Navigation")
								{
									return "CollectionCategory_TravelNavigation";
								}
							}
						}
						else if (category == "Music + Rhythm")
						{
							return "CollectionCategory_MusicRhythm";
						}
					}
					else if (num != 1612786629U)
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
				else if (num <= 2039478274U)
				{
					if (num != 1904316691U)
					{
						if (num == 2039478274U)
						{
							if (category == "Kitchen helpers")
							{
								return "CollectionCategory_KitchenHelpers";
							}
						}
					}
					else if (category == "Great on 8")
					{
						return "CollectionCategory_GreatOn8";
					}
				}
				else if (num != 2136908150U)
				{
					if (num != 2447142016U)
					{
						if (num == 2465108186U)
						{
							if (category == "Sports")
							{
								return "CollectionCategory_Sports";
							}
						}
					}
					else if (category == "Classics")
					{
						return "CollectionCategory_Classics";
					}
				}
				else if (category == "Government")
				{
					return "CollectionCategory_Government";
				}
			}
			else if (num <= 3315627437U)
			{
				if (num <= 3066062199U)
				{
					if (num <= 2639503373U)
					{
						if (num != 2548511764U)
						{
							if (num == 2639503373U)
							{
								if (category == "Microsoft apps")
								{
									return "CollectionCategory_MicrosoftApps";
								}
							}
						}
						else if (category == "Photo + Design")
						{
							return "CollectionCategory_PhotoDesign";
						}
					}
					else if (num != 2704337693U)
					{
						if (num == 3066062199U)
						{
							if (category == "Utilities")
							{
								return "CollectionCategory_Utilities";
							}
						}
					}
					else if (category == "Shopping")
					{
						return "CollectionCategory_Shopping";
					}
				}
				else if (num <= 3205650756U)
				{
					if (num != 3120275647U)
					{
						if (num == 3205650756U)
						{
							if (category == "Strategy")
							{
								return "CollectionCategory_Strategy";
							}
						}
					}
					else if (category == "Staff picks")
					{
						return "CollectionCategory_StaffPicks";
					}
				}
				else if (num != 3213326657U)
				{
					if (num != 3271434715U)
					{
						if (num == 3315627437U)
						{
							if (category == "Puzzle")
							{
								return "CollectionCategory_Puzzle";
							}
						}
					}
					else if (category == "Security")
					{
						return "CollectionCategory_Security";
					}
				}
				else if (category == "Arcade")
				{
					return "CollectionCategory_Arcade";
				}
			}
			else if (num <= 3398496685U)
			{
				if (num <= 3333035499U)
				{
					if (num != 3319603597U)
					{
						if (num == 3333035499U)
						{
							if (category == "Music + Audio")
							{
								return "CollectionCategory_MusicAudio";
							}
						}
					}
					else if (category == "Finance")
					{
						return "CollectionCategory_Finance";
					}
				}
				else if (num != 3364160439U)
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

		// Token: 0x0600024D RID: 589 RVA: 0x0000C828 File Offset: 0x0000AA28
		private string GetAppCountText(ResourceLoader tns, int count)
		{
			string pluralKey = this.GetPluralKey(count, "AppCounter", "AppsCounter1", "AppsCounter");
			return string.Format(tns.GetString(pluralKey), new object[]
			{
				count
			});
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000C868 File Offset: 0x0000AA68
		private string GetGameCountText(ResourceLoader tns, int count)
		{
			string pluralKey = this.GetPluralKey(count, "GameCounter", "GamesCounter1", "GamesCounter");
			return string.Format(tns.GetString(pluralKey), new object[]
			{
				count
			});
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00006827 File Offset: 0x00004A27
		private string GetPluralKey(int count, string singularKey, string fewKey, string manyKey)
		{
			if (count % 10 == 1 && count % 100 != 11)
			{
				return singularKey;
			}
			if (count % 10 >= 2 && count % 10 <= 4 && (count % 100 < 10 || count % 100 >= 20))
			{
				return fewKey;
			}
			return manyKey;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000C8A8 File Offset: 0x0000AAA8
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Application.LoadComponent(this, new Uri("ms-appx:///CollectionsViewPage.xaml"), 0);
			this.MainContentGrid = (Grid)base.FindName("MainContentGrid");
			this.LoadingPanel = (Grid)base.FindName("LoadingPanel");
			this.AllAppsHub = (Hub)base.FindName("AllAppsHub");
			this.NoResultsPanel = (StackPanel)base.FindName("NoResultsPanel");
			this.backButton = (Button)base.FindName("backButton");
			this.CategoryTitleText = (TextBlock)base.FindName("CategoryTitleText");
			this.SearchResultsText = (TextBlock)base.FindName("SearchResultsText");
			this.CollectionsSection = (HubSection)base.FindName("CollectionsSection");
			this.AppsSection = (HubSection)base.FindName("AppsSection");
			this.LoadingRing = (ProgressRing)base.FindName("LoadingRing");
			this.LoadingText = (TextBlock)base.FindName("LoadingText");
			this.nbSearch = (SearchBox)base.FindName("nbSearch");
			this.nbLogoText = (TextBlock)base.FindName("nbLogoText");
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000C9F4 File Offset: 0x0000ABF4
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
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.BackButton_Click));
				break;
			}
			case 8:
			{
				ListViewBase listViewBase = (ListViewBase)target;
				WindowsRuntimeMarshal.AddEventHandler<ItemClickEventHandler>(new Func<ItemClickEventHandler, EventRegistrationToken>(listViewBase.add_ItemClick), new Action<EventRegistrationToken>(listViewBase.remove_ItemClick), new ItemClickEventHandler(this.AppItem_Click));
				FrameworkElement frameworkElement = (FrameworkElement)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(frameworkElement.add_Loaded), new Action<EventRegistrationToken>(frameworkElement.remove_Loaded), new RoutedEventHandler(this.AllAppsGridView_Loaded));
				break;
			}
			case 9:
			{
				ListViewBase listViewBase = (ListViewBase)target;
				WindowsRuntimeMarshal.AddEventHandler<ItemClickEventHandler>(new Func<ItemClickEventHandler, EventRegistrationToken>(listViewBase.add_ItemClick), new Action<EventRegistrationToken>(listViewBase.remove_ItemClick), new ItemClickEventHandler(this.CollectionTile_Click));
				FrameworkElement frameworkElement = (FrameworkElement)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(frameworkElement.add_Loaded), new Action<EventRegistrationToken>(frameworkElement.remove_Loaded), new RoutedEventHandler(this.CollectionsGridView_Loaded));
				break;
			}
			case 10:
			{
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uielement.add_Tapped), new Action<EventRegistrationToken>(uielement.remove_Tapped), new TappedEventHandler(this.StoreLogo_Click));
				break;
			}
			case 11:
			{
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uielement.add_Tapped), new Action<EventRegistrationToken>(uielement.remove_Tapped), new TappedEventHandler(this.Border_Tapped_1));
				break;
			}
			case 12:
			{
				SearchBox searchBox = (SearchBox)target;
				WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<SearchBox, SearchBoxQuerySubmittedEventArgs>>(new Func<TypedEventHandler<SearchBox, SearchBoxQuerySubmittedEventArgs>, EventRegistrationToken>(searchBox.add_QuerySubmitted), new Action<EventRegistrationToken>(searchBox.remove_QuerySubmitted), new TypedEventHandler<SearchBox, SearchBoxQuerySubmittedEventArgs>(this.nbSearch_QuerySubmitted));
				break;
			}
			}
			this._contentLoaded = true;
		}

		// Token: 0x040000DB RID: 219
		private ObservableCollection<StoreApp> allApps = new ObservableCollection<StoreApp>();

		// Token: 0x040000DC RID: 220
		private ObservableCollection<StoreApp> filteredApps = new ObservableCollection<StoreApp>();

		// Token: 0x040000DD RID: 221
		private ObservableCollection<StoreApp> visibleApps = new ObservableCollection<StoreApp>();

		// Token: 0x040000DE RID: 222
		private const string AppsJsonUrl = "https://8store.modyleprojects.ru/data/apps.json";

		// Token: 0x040000DF RID: 223
		private static List<StoreApp> cachedApps;

		// Token: 0x040000E0 RID: 224
		private GridView collectionsGridView;

		// Token: 0x040000E1 RID: 225
		private GridView allAppsGridView;

		// Token: 0x040000E3 RID: 227
		private bool isCollectionsView = true;

		// Token: 0x040000E4 RID: 228
		private string currentCategory = "";

		// Token: 0x040000E5 RID: 229
		private const int StaffPicksLimit = 10;

		// Token: 0x040000E6 RID: 230
		private const int FeaturedLimit = 12;

		// Token: 0x040000E7 RID: 231
		private List<string> appCategories;

		// Token: 0x040000E8 RID: 232
		private List<string> gamesCategories;

		// Token: 0x040000E9 RID: 233
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Grid MainContentGrid;

		// Token: 0x040000EA RID: 234
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Grid LoadingPanel;

		// Token: 0x040000EB RID: 235
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Hub AllAppsHub;

		// Token: 0x040000EC RID: 236
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel NoResultsPanel;

		// Token: 0x040000ED RID: 237
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button backButton;

		// Token: 0x040000EE RID: 238
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock CategoryTitleText;

		// Token: 0x040000EF RID: 239
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock SearchResultsText;

		// Token: 0x040000F0 RID: 240
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private HubSection CollectionsSection;

		// Token: 0x040000F1 RID: 241
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private HubSection AppsSection;

		// Token: 0x040000F2 RID: 242
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ProgressRing LoadingRing;

		// Token: 0x040000F3 RID: 243
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock LoadingText;

		// Token: 0x040000F4 RID: 244
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private SearchBox nbSearch;

		// Token: 0x040000F5 RID: 245
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock nbLogoText;

		// Token: 0x040000F6 RID: 246
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
