using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
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
	// Token: 0x02000021 RID: 33
	public sealed class CollectionsViewPage : Page, IComponentConnector
	{
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x0000A753 File Offset: 0x00008953
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x0000A75B File Offset: 0x0000895B
		public ObservableCollection<AppCollection> AllCollections { get; set; } = new ObservableCollection<AppCollection>();

		// Token: 0x060001B5 RID: 437 RVA: 0x0000A764 File Offset: 0x00008964
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
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000A948 File Offset: 0x00008B48
		[DebuggerStepThrough]
		private void AllAppsPage_LoadedAsync(object sender, RoutedEventArgs e)
		{
			CollectionsViewPage.<AllAppsPage_LoadedAsync>d__15 <AllAppsPage_LoadedAsync>d__ = new CollectionsViewPage.<AllAppsPage_LoadedAsync>d__15();
			<AllAppsPage_LoadedAsync>d__.<>4__this = this;
			<AllAppsPage_LoadedAsync>d__.sender = sender;
			<AllAppsPage_LoadedAsync>d__.e = e;
			<AllAppsPage_LoadedAsync>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<AllAppsPage_LoadedAsync>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <AllAppsPage_LoadedAsync>d__.<>t__builder;
			<>t__builder.Start<CollectionsViewPage.<AllAppsPage_LoadedAsync>d__15>(ref <AllAppsPage_LoadedAsync>d__);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000A994 File Offset: 0x00008B94
		[DebuggerStepThrough]
		private Task LoadAllAppsFromJsonAsync()
		{
			CollectionsViewPage.<LoadAllAppsFromJsonAsync>d__16 <LoadAllAppsFromJsonAsync>d__ = new CollectionsViewPage.<LoadAllAppsFromJsonAsync>d__16();
			<LoadAllAppsFromJsonAsync>d__.<>4__this = this;
			<LoadAllAppsFromJsonAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadAllAppsFromJsonAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadAllAppsFromJsonAsync>d__.<>t__builder;
			<>t__builder.Start<CollectionsViewPage.<LoadAllAppsFromJsonAsync>d__16>(ref <LoadAllAppsFromJsonAsync>d__);
			return <LoadAllAppsFromJsonAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000A9DC File Offset: 0x00008BDC
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

		// Token: 0x060001B9 RID: 441 RVA: 0x0000B024 File Offset: 0x00009224
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

		// Token: 0x060001BA RID: 442 RVA: 0x00005833 File Offset: 0x00003A33
		private void PlaySound()
		{
			SoundManager.PlayStartupSound();
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000B108 File Offset: 0x00009308
		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			bool canGoBack = base.Frame.CanGoBack;
			if (canGoBack)
			{
				base.Frame.GoBack();
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00005867 File Offset: 0x00003A67
		private void TopAppsButton_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(AllAppsPage));
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00005880 File Offset: 0x00003A80
		private void TopAppsButton_Click_1(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(PopularPage));
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00005899 File Offset: 0x00003A99
		private void DownloadHubButton_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(DownloadsHub));
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000058B2 File Offset: 0x00003AB2
		private void ExclusiveHubButton_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(AccountPage));
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000B134 File Offset: 0x00009334
		private void CollectionTile_Click(object sender, ItemClickEventArgs e)
		{
			AppCollection appCollection = e.ClickedItem as AppCollection;
			bool flag = appCollection != null;
			if (flag)
			{
				this.SwitchToAppsView(appCollection.Name);
			}
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000B168 File Offset: 0x00009368
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
					this.SwitchToAppsView(appCollection.Name);
				}
			}
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000B1AC File Offset: 0x000093AC
		private void AppItem_Click(object sender, ItemClickEventArgs e)
		{
			StoreApp storeApp = e.ClickedItem as StoreApp;
			bool flag = storeApp != null;
			if (flag)
			{
				base.Frame.Navigate(typeof(AppDetailsPage), storeApp);
			}
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000B1E8 File Offset: 0x000093E8
		private void AppItem_Tapped(object sender, TappedRoutedEventArgs e)
		{
			Grid grid = sender as Grid;
			bool flag = grid != null;
			if (flag)
			{
				StoreApp storeApp = grid.DataContext as StoreApp;
				bool flag2 = storeApp != null;
				if (flag2)
				{
					base.Frame.Navigate(typeof(AppDetailsPage), storeApp);
				}
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000B234 File Offset: 0x00009434
		private void BackToCollectionsButton_Click(object sender, RoutedEventArgs e)
		{
			this.BackToCollectionsView();
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000B240 File Offset: 0x00009440
		private void SwitchToAppsView(string categoryName)
		{
			this.isCollectionsView = false;
			this.currentCategory = categoryName;
			this.CollectionsSection.put_Visibility(1);
			this.AppsSection.put_Visibility(0);
			this.CategoryTitleText.put_Visibility(0);
			this.CategoryTitleText.put_Text(categoryName);
			this.FilterAppsByCategory(categoryName);
			this.LoadingText.put_Text("Loading apps...");
			this.SearchTextBox.put_Text("");
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000B2BC File Offset: 0x000094BC
		private void BackToCollectionsView()
		{
			this.isCollectionsView = true;
			this.currentCategory = "";
			this.CollectionsSection.put_Visibility(0);
			this.AppsSection.put_Visibility(1);
			this.CategoryTitleText.put_Visibility(1);
			this.SearchTextBox.put_Text("");
			this.SearchResultsText.put_Visibility(1);
			this.NoResultsPanel.put_Visibility(1);
			this.LoadingText.put_Text("Loading collections...");
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000B340 File Offset: 0x00009540
		private void FilterAppsByCategory(string category)
		{
			List<StoreApp> list = Enumerable.ToList<StoreApp>(Enumerable.Where<StoreApp>(this.allApps, (StoreApp app) => app.Category != null && app.Category.Equals(category, 5)));
			this.filteredApps.Clear();
			foreach (StoreApp storeApp in list)
			{
				this.filteredApps.Add(storeApp);
			}
			bool flag = this.allAppsGridView != null;
			if (flag)
			{
				this.allAppsGridView.put_ItemsSource(this.filteredApps);
			}
			bool flag2 = this.filteredApps.Count > 0;
			if (flag2)
			{
				this.SearchResultsText.put_Text(string.Format("{0} app{1}", new object[]
				{
					this.filteredApps.Count,
					(this.filteredApps.Count == 1) ? "" : "s"
				}));
				this.SearchResultsText.put_Visibility(0);
				this.NoResultsPanel.put_Visibility(1);
			}
			else
			{
				this.SearchResultsText.put_Visibility(1);
				this.NoResultsPanel.put_Visibility(0);
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000B48C File Offset: 0x0000968C
		private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			string text = this.SearchTextBox.Text;
			string searchText = (text != null) ? text.ToLower() : null;
			bool flag = !this.isCollectionsView;
			if (flag)
			{
				this.SearchApps(searchText);
			}
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000B4CC File Offset: 0x000096CC
		private void SearchApps(string searchText)
		{
			bool flag = string.IsNullOrWhiteSpace(searchText);
			if (flag)
			{
				this.FilterAppsByCategory(this.currentCategory);
			}
			else
			{
				IEnumerable<StoreApp> enumerable = Enumerable.Where<StoreApp>(this.allApps, (StoreApp app) => app.Category != null && app.Category.Equals(this.currentCategory, 5));
				List<StoreApp> list = Enumerable.ToList<StoreApp>(Enumerable.Where<StoreApp>(enumerable, (StoreApp app) => (app.Name != null && app.Name.ToLower().Contains(searchText)) || (app.Publisher != null && app.Publisher.ToLower().Contains(searchText)) || (app.Description != null && app.Description.ToLower().Contains(searchText))));
				this.filteredApps.Clear();
				foreach (StoreApp storeApp in list)
				{
					this.filteredApps.Add(storeApp);
				}
				bool flag2 = this.filteredApps.Count > 0;
				if (flag2)
				{
					this.SearchResultsText.put_Text(string.Format("{0} app{1} found", new object[]
					{
						this.filteredApps.Count,
						(this.filteredApps.Count == 1) ? "" : "s"
					}));
					this.SearchResultsText.put_Visibility(0);
					this.NoResultsPanel.put_Visibility(1);
				}
				else
				{
					this.SearchResultsText.put_Visibility(1);
					this.NoResultsPanel.put_Visibility(0);
				}
			}
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000B630 File Offset: 0x00009830
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

		// Token: 0x060001CB RID: 459 RVA: 0x0000B680 File Offset: 0x00009880
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);
			string text = e.Parameter as string;
			bool flag = !string.IsNullOrWhiteSpace(text);
			if (flag)
			{
				this.currentCategory = text;
				this.isCollectionsView = false;
				this.CategoryTitleText.put_Visibility(0);
				this.CategoryTitleText.put_Text(text);
				this.CollectionsSection.put_Visibility(1);
				this.AppsSection.put_Visibility(0);
			}
			else
			{
				bool flag2 = !this.isCollectionsView;
				if (flag2)
				{
					this.BackToCollectionsView();
				}
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000B70C File Offset: 0x0000990C
		private void CollectionsGridView_Loaded(object sender, RoutedEventArgs e)
		{
			this.collectionsGridView = (sender as GridView);
			bool flag = this.collectionsGridView != null;
			if (flag)
			{
				this.collectionsGridView.put_ItemsSource(this.AllCollections);
			}
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000B748 File Offset: 0x00009948
		private void AllAppsGridView_Loaded(object sender, RoutedEventArgs e)
		{
			this.allAppsGridView = (sender as GridView);
			bool flag = this.allAppsGridView != null;
			if (flag)
			{
				this.allAppsGridView.put_ItemsSource(this.filteredApps);
			}
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000B784 File Offset: 0x00009984
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			bool contentLoaded = this._contentLoaded;
			if (!contentLoaded)
			{
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
				this.SearchTextBox = (TextBox)base.FindName("SearchTextBox");
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000B8C4 File Offset: 0x00009AC4
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
				WindowsRuntimeMarshal.AddEventHandler<ItemClickEventHandler>(new Func<ItemClickEventHandler, EventRegistrationToken>(listViewBase.add_ItemClick), new Action<EventRegistrationToken>(listViewBase.remove_ItemClick), new ItemClickEventHandler(this.AppItem_Click));
				FrameworkElement frameworkElement = (FrameworkElement)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(frameworkElement.add_Loaded), new Action<EventRegistrationToken>(frameworkElement.remove_Loaded), new RoutedEventHandler(this.AllAppsGridView_Loaded));
				break;
			}
			case 8:
			{
				ListViewBase listViewBase = (ListViewBase)target;
				WindowsRuntimeMarshal.AddEventHandler<ItemClickEventHandler>(new Func<ItemClickEventHandler, EventRegistrationToken>(listViewBase.add_ItemClick), new Action<EventRegistrationToken>(listViewBase.remove_ItemClick), new ItemClickEventHandler(this.CollectionTile_Click));
				FrameworkElement frameworkElement = (FrameworkElement)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(frameworkElement.add_Loaded), new Action<EventRegistrationToken>(frameworkElement.remove_Loaded), new RoutedEventHandler(this.CollectionsGridView_Loaded));
				break;
			}
			case 9:
			{
				TextBox textBox = (TextBox)target;
				WindowsRuntimeMarshal.AddEventHandler<TextChangedEventHandler>(new Func<TextChangedEventHandler, EventRegistrationToken>(textBox.add_TextChanged), new Action<EventRegistrationToken>(textBox.remove_TextChanged), new TextChangedEventHandler(this.SearchTextBox_TextChanged));
				break;
			}
			}
			this._contentLoaded = true;
		}

		// Token: 0x040000D2 RID: 210
		private ObservableCollection<StoreApp> allApps = new ObservableCollection<StoreApp>();

		// Token: 0x040000D3 RID: 211
		private ObservableCollection<StoreApp> filteredApps = new ObservableCollection<StoreApp>();

		// Token: 0x040000D4 RID: 212
		private const string AppsJsonUrl = "https://8store.modyleprojects.ru/data/apps.json";

		// Token: 0x040000D5 RID: 213
		private static List<StoreApp> cachedApps = null;

		// Token: 0x040000D6 RID: 214
		private GridView collectionsGridView;

		// Token: 0x040000D7 RID: 215
		private GridView allAppsGridView;

		// Token: 0x040000D9 RID: 217
		private bool isCollectionsView = true;

		// Token: 0x040000DA RID: 218
		private string currentCategory = "";

		// Token: 0x040000DB RID: 219
		private List<string> appCategories;

		// Token: 0x040000DC RID: 220
		private List<string> gamesCategories;

		// Token: 0x040000DD RID: 221
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Grid MainContentGrid;

		// Token: 0x040000DE RID: 222
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Grid LoadingPanel;

		// Token: 0x040000DF RID: 223
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Hub AllAppsHub;

		// Token: 0x040000E0 RID: 224
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel NoResultsPanel;

		// Token: 0x040000E1 RID: 225
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button backButton;

		// Token: 0x040000E2 RID: 226
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock CategoryTitleText;

		// Token: 0x040000E3 RID: 227
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock SearchResultsText;

		// Token: 0x040000E4 RID: 228
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private HubSection CollectionsSection;

		// Token: 0x040000E5 RID: 229
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private HubSection AppsSection;

		// Token: 0x040000E6 RID: 230
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ProgressRing LoadingRing;

		// Token: 0x040000E7 RID: 231
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock LoadingText;

		// Token: 0x040000E8 RID: 232
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBox SearchTextBox;

		// Token: 0x040000E9 RID: 233
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
