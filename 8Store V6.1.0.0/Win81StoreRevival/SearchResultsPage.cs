using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.Core;
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
	// Token: 0x02000040 RID: 64
	public sealed class SearchResultsPage : Page, IComponentConnector
	{
		// Token: 0x0600045A RID: 1114 RVA: 0x00015CC0 File Offset: 0x00013EC0
		public SearchResultsPage()
		{
			this.InitializeComponent();
			this.UpdateTitleNB();
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(this.add_Loaded), new Action<EventRegistrationToken>(this.remove_Loaded), new RoutedEventHandler(this.SearchResultsPage_Loaded));
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(this.add_Unloaded), new Action<EventRegistrationToken>(this.remove_Unloaded), new RoutedEventHandler(this.SearchResultsPage_Unloaded));
			Window window = Window.Current;
			WindowsRuntimeMarshal.AddEventHandler<WindowSizeChangedEventHandler>(new Func<WindowSizeChangedEventHandler, EventRegistrationToken>(window.add_SizeChanged), new Action<EventRegistrationToken>(window.remove_SizeChanged), new WindowSizeChangedEventHandler(this.OnPageSizeChanged));
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00015D88 File Offset: 0x00013F88
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			SearchResultsPage.<OnNavigatedTo>d__7 <OnNavigatedTo>d__;
			<OnNavigatedTo>d__.<>4__this = this;
			<OnNavigatedTo>d__.e = e;
			<OnNavigatedTo>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<OnNavigatedTo>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <OnNavigatedTo>d__.<>t__builder;
			<>t__builder.Start<SearchResultsPage.<OnNavigatedTo>d__7>(ref <OnNavigatedTo>d__);
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00015DCC File Offset: 0x00013FCC
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

		// Token: 0x0600045D RID: 1117 RVA: 0x00015E30 File Offset: 0x00014030
		private void nbSearch_QuerySubmitted(object sender, SearchBoxQuerySubmittedEventArgs e)
		{
			string text = (e != null && e.QueryText != null) ? e.QueryText.Trim() : null;
			if (!string.IsNullOrEmpty(text))
			{
				base.Frame.Navigate(typeof(SearchResultsPage), text);
			}
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00015E78 File Offset: 0x00014078
		private Task SearchAppsAsync(string query)
		{
			SearchResultsPage.<SearchAppsAsync>d__10 <SearchAppsAsync>d__;
			<SearchAppsAsync>d__.<>4__this = this;
			<SearchAppsAsync>d__.query = query;
			<SearchAppsAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SearchAppsAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <SearchAppsAsync>d__.<>t__builder;
			<>t__builder.Start<SearchResultsPage.<SearchAppsAsync>d__10>(ref <SearchAppsAsync>d__);
			return <SearchAppsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00015EC8 File Offset: 0x000140C8
		private StoreApp MapSearchItemToStoreApp(SearchResultsPage.SearchAppItem item)
		{
			if (item == null)
			{
				return null;
			}
			string text = item.icon_path;
			if (!string.IsNullOrEmpty(text) && !text.StartsWith("http", 5))
			{
				if (!string.IsNullOrWhiteSpace(item.id))
				{
					text = "https://8store.modyleprojects.ru/api/files/" + item.id + "/" + text;
				}
				else
				{
					text = "https://8store.modyleprojects.ru/data/icons/" + text;
				}
			}
			string text2 = item.file_path;
			if (!string.IsNullOrEmpty(text2) && !text2.StartsWith("http", 5))
			{
				text2 = "https://8store.modyleprojects.ru/data/downloads/" + text2;
			}
			return new StoreApp
			{
				Id = (item.id ?? ""),
				Name = (item.name ?? ""),
				Publisher = (item.publisher ?? ""),
				Category = (item.category ?? ""),
				Description = (item.description ?? ""),
				IconUrl = (text ?? ""),
				DownloadUrl = (text2 ?? ""),
				Version = (item.version ?? "1.0.0"),
				Type = (item.type ?? "App"),
				Featured = (item.featured == 1),
				ReviewStats = new ReviewStats
				{
					AverageRating = 0.0,
					ReviewCount = 0
				}
			};
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0001603C File Offset: 0x0001423C
		private T FindChild<T>(DependencyObject parent, string name) where T : DependencyObject
		{
			if (parent == null)
			{
				return default(T);
			}
			int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
			for (int i = 0; i < childrenCount; i++)
			{
				DependencyObject child = VisualTreeHelper.GetChild(parent, i);
				if (child is T && (child as FrameworkElement).Name == name)
				{
					return (T)((object)child);
				}
				T t = this.FindChild<T>(child, name);
				if (t != null)
				{
					return t;
				}
			}
			return default(T);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x000160B4 File Offset: 0x000142B4
		private void FillCategoryCombo()
		{
			ComboBox comboBox = this.FindChild<ComboBox>(this.ResultsSection, "CategoryComboBox");
			if (comboBox == null)
			{
				return;
			}
			WindowsRuntimeMarshal.RemoveEventHandler<SelectionChangedEventHandler>(new Action<EventRegistrationToken>(comboBox.remove_SelectionChanged), new SelectionChangedEventHandler(this.CategoryComboBox_SelectionChanged));
			comboBox.Items.Clear();
			foreach (string text in SearchResultsPage.Categories)
			{
				ICollection<object> items = comboBox.Items;
				ComboBoxItem comboBoxItem = new ComboBoxItem();
				comboBoxItem.put_Content(text);
				items.Add(comboBoxItem);
			}
			comboBox.put_SelectedIndex(0);
			ComboBox comboBox2 = comboBox;
			WindowsRuntimeMarshal.AddEventHandler<SelectionChangedEventHandler>(new Func<SelectionChangedEventHandler, EventRegistrationToken>(comboBox2.add_SelectionChanged), new Action<EventRegistrationToken>(comboBox2.remove_SelectionChanged), new SelectionChangedEventHandler(this.CategoryComboBox_SelectionChanged));
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00007789 File Offset: 0x00005989
		private string GetCombo(ComboBox box, string def)
		{
			ComboBoxItem comboBoxItem = ((box != null) ? box.SelectedItem : null) as ComboBoxItem;
			string text;
			if (comboBoxItem == null)
			{
				text = null;
			}
			else
			{
				object content = comboBoxItem.Content;
				text = ((content != null) ? content.ToString() : null);
			}
			return text ?? def;
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00016166 File Offset: 0x00014366
		private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			this.ApplyFiltersAndSort();
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00016166 File Offset: 0x00014366
		private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			this.ApplyFiltersAndSort();
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00016170 File Offset: 0x00014370
		private void ApplyFiltersAndSort()
		{
			ComboBox box = this.FindChild<ComboBox>(this.ResultsSection, "CategoryComboBox");
			string cat = this.GetCombo(box, "All");
			IEnumerable<StoreApp> enumerable = Enumerable.AsEnumerable<StoreApp>(this._allResults);
			if (cat != "All")
			{
				enumerable = Enumerable.Where<StoreApp>(enumerable, (StoreApp a) => string.Equals(a.Category, cat, 5));
			}
			this._filteredResults = Enumerable.ToList<StoreApp>(Enumerable.OrderBy<StoreApp, string>(enumerable, (StoreApp a) => a.Name));
			this.UpdateResultsList(this._filteredResults);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00016214 File Offset: 0x00014414
		private void UpdateResultsList(List<StoreApp> apps)
		{
			ResourceLoader forCurrentView = ResourceLoader.GetForCurrentView();
			string pluralKey = this.GetPluralKey(apps.Count, "AppCounter", "AppsCounter1", "AppsCounter");
			this.countText.put_Text(string.Format(forCurrentView.GetString(pluralKey), new object[]
			{
				apps.Count
			}));
			this._filteredResults = (apps ?? new List<StoreApp>());
			this.RefreshVisibleResults();
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00016284 File Offset: 0x00014484
		private void ResultItem_Tapped(object sender, TappedRoutedEventArgs e)
		{
			FrameworkElement frameworkElement = sender as FrameworkElement;
			if (frameworkElement == null)
			{
				return;
			}
			StoreApp storeApp = frameworkElement.DataContext as StoreApp;
			if (storeApp != null)
			{
				base.Frame.Navigate(typeof(AppDetailsPage), new AppDetailsNavigationData
				{
					App = storeApp,
					ShowReviews = false
				});
			}
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0000E2C1 File Offset: 0x0000C4C1
		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			if (base.Frame.CanGoBack)
			{
				base.Frame.GoBack();
				return;
			}
			base.Frame.Navigate(typeof(MainPage));
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00004859 File Offset: 0x00002A59
		private void StoreLogo_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(MainPage));
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x000162D4 File Offset: 0x000144D4
		private void SearchResultsPage_Loaded(object sender, RoutedEventArgs e)
		{
			this.RefreshVisibleResults();
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x000162DC File Offset: 0x000144DC
		private void ResultsGridView_Loaded(object sender, RoutedEventArgs e)
		{
			this._resultsGridView = (sender as GridView);
			this.RefreshVisibleResults();
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x000162F0 File Offset: 0x000144F0
		private void SearchResultsPage_Unloaded(object sender, RoutedEventArgs e)
		{
			WindowsRuntimeMarshal.RemoveEventHandler<WindowSizeChangedEventHandler>(new Action<EventRegistrationToken>(Window.Current.remove_SizeChanged), new WindowSizeChangedEventHandler(this.OnPageSizeChanged));
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x000162D4 File Offset: 0x000144D4
		private void OnPageSizeChanged(object sender, WindowSizeChangedEventArgs e)
		{
			this.RefreshVisibleResults();
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00016314 File Offset: 0x00014514
		private void RefreshVisibleResults()
		{
			if (this._resultsGridView == null)
			{
				return;
			}
			this.SyncVisibleCollection(this._resultsGridView, this._filteredResults, this._visibleResults);
			this._resultsGridView.put_ItemsSource(this._visibleResults);
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00016348 File Offset: 0x00014548
		private void SyncVisibleCollection(GridView gridView, List<StoreApp> source, ObservableCollection<StoreApp> target)
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

		// Token: 0x06000470 RID: 1136 RVA: 0x00006827 File Offset: 0x00004A27
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

		// Token: 0x06000471 RID: 1137 RVA: 0x000163AC File Offset: 0x000145AC
		private void SearchBoxBorder_Tapped(object sender, TappedRoutedEventArgs e)
		{
			this.TopSearchBox.Focus(3);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x000163BC File Offset: 0x000145BC
		private void TopSearchBox_QuerySubmitted(object sender, SearchBoxQuerySubmittedEventArgs e)
		{
			string text = (e != null && e.QueryText != null) ? e.QueryText.Trim() : null;
			if (!string.IsNullOrEmpty(text))
			{
				this._currentQuery = text;
				this.SearchAppsAsync(text);
			}
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x000163FC File Offset: 0x000145FC
		private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
		{
			Image image = sender as Image;
			if (image == null)
			{
				return;
			}
			try
			{
				image.put_Source(new BitmapImage(new Uri("ms-appx:///Assets/icon-error.png")));
			}
			catch
			{
			}
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00016440 File Offset: 0x00014640
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Application.LoadComponent(this, new Uri("ms-appx:///SearchResultsPage.xaml"), 0);
			this.pageRoot = (Page)base.FindName("pageRoot");
			this.MainHub = (Hub)base.FindName("MainHub");
			this.backButton = (Button)base.FindName("backButton");
			this.pageTitle = (TextBlock)base.FindName("pageTitle");
			this.countText = (TextBlock)base.FindName("countText");
			this.ResultsSection = (HubSection)base.FindName("ResultsSection");
			this.TopSearchBox = (SearchBox)base.FindName("TopSearchBox");
			this.nbLogoText = (TextBlock)base.FindName("nbLogoText");
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00016520 File Offset: 0x00014720
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
			{
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uielement.add_Tapped), new Action<EventRegistrationToken>(uielement.remove_Tapped), new TappedEventHandler(this.ResultItem_Tapped));
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
			case 4:
			{
				FrameworkElement frameworkElement = (FrameworkElement)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(frameworkElement.add_Loaded), new Action<EventRegistrationToken>(frameworkElement.remove_Loaded), new RoutedEventHandler(this.ResultsGridView_Loaded));
				break;
			}
			case 5:
			{
				Selector selector = (Selector)target;
				WindowsRuntimeMarshal.AddEventHandler<SelectionChangedEventHandler>(new Func<SelectionChangedEventHandler, EventRegistrationToken>(selector.add_SelectionChanged), new Action<EventRegistrationToken>(selector.remove_SelectionChanged), new SelectionChangedEventHandler(this.CategoryComboBox_SelectionChanged));
				break;
			}
			case 6:
			{
				Selector selector = (Selector)target;
				WindowsRuntimeMarshal.AddEventHandler<SelectionChangedEventHandler>(new Func<SelectionChangedEventHandler, EventRegistrationToken>(selector.add_SelectionChanged), new Action<EventRegistrationToken>(selector.remove_SelectionChanged), new SelectionChangedEventHandler(this.SortComboBox_SelectionChanged));
				break;
			}
			case 7:
			{
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uielement.add_Tapped), new Action<EventRegistrationToken>(uielement.remove_Tapped), new TappedEventHandler(this.StoreLogo_Click));
				break;
			}
			case 8:
			{
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uielement.add_Tapped), new Action<EventRegistrationToken>(uielement.remove_Tapped), new TappedEventHandler(this.SearchBoxBorder_Tapped));
				break;
			}
			case 9:
			{
				SearchBox searchBox = (SearchBox)target;
				WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<SearchBox, SearchBoxQuerySubmittedEventArgs>>(new Func<TypedEventHandler<SearchBox, SearchBoxQuerySubmittedEventArgs>, EventRegistrationToken>(searchBox.add_QuerySubmitted), new Action<EventRegistrationToken>(searchBox.remove_QuerySubmitted), new TypedEventHandler<SearchBox, SearchBoxQuerySubmittedEventArgs>(this.TopSearchBox_QuerySubmitted));
				break;
			}
			}
			this._contentLoaded = true;
		}

		// Token: 0x040001E1 RID: 481
		private static readonly string[] Categories = new string[]
		{
			"All",
			"Books + Reference",
			"Dependency",
			"Education",
			"Finance",
			"Food + Cooking",
			"Government",
			"Health + Fitness",
			"Music + Audio",
			"News + Weather",
			"Other",
			"Photo + Design",
			"Security",
			"Shopping",
			"Social",
			"Sports",
			"Travel + Navigation",
			"Utilities",
			"Video + Entertainment",
			"Action",
			"Adventure",
			"Arcade",
			"Card + Casino",
			"Classics",
			"Family + Kids",
			"Games",
			"Music + Rhythm",
			"Puzzle",
			"Racing + Flying",
			"Strategy",
			"Simulation"
		};

		// Token: 0x040001E2 RID: 482
		private string _currentQuery;

		// Token: 0x040001E3 RID: 483
		private List<StoreApp> _allResults = new List<StoreApp>();

		// Token: 0x040001E4 RID: 484
		private List<StoreApp> _filteredResults = new List<StoreApp>();

		// Token: 0x040001E5 RID: 485
		private ObservableCollection<StoreApp> _visibleResults = new ObservableCollection<StoreApp>();

		// Token: 0x040001E6 RID: 486
		private GridView _resultsGridView;

		// Token: 0x040001E7 RID: 487
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Page pageRoot;

		// Token: 0x040001E8 RID: 488
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Hub MainHub;

		// Token: 0x040001E9 RID: 489
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button backButton;

		// Token: 0x040001EA RID: 490
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock pageTitle;

		// Token: 0x040001EB RID: 491
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock countText;

		// Token: 0x040001EC RID: 492
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private HubSection ResultsSection;

		// Token: 0x040001ED RID: 493
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private SearchBox TopSearchBox;

		// Token: 0x040001EE RID: 494
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock nbLogoText;

		// Token: 0x040001EF RID: 495
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;

		// Token: 0x0200014E RID: 334
		private sealed class SearchApiResponse
		{
			// Token: 0x17000145 RID: 325
			// (get) Token: 0x060008CD RID: 2253 RVA: 0x0003746E File Offset: 0x0003566E
			// (set) Token: 0x060008CE RID: 2254 RVA: 0x00037476 File Offset: 0x00035676
			[JsonProperty("applications")]
			public List<SearchResultsPage.SearchAppItem> applications { get; set; }
		}

		// Token: 0x0200014F RID: 335
		private sealed class SearchAppItem
		{
			// Token: 0x17000146 RID: 326
			// (get) Token: 0x060008D0 RID: 2256 RVA: 0x0003747F File Offset: 0x0003567F
			// (set) Token: 0x060008D1 RID: 2257 RVA: 0x00037487 File Offset: 0x00035687
			[JsonProperty("id")]
			public string id { get; set; }

			// Token: 0x17000147 RID: 327
			// (get) Token: 0x060008D2 RID: 2258 RVA: 0x00037490 File Offset: 0x00035690
			// (set) Token: 0x060008D3 RID: 2259 RVA: 0x00037498 File Offset: 0x00035698
			[JsonProperty("name")]
			public string name { get; set; }

			// Token: 0x17000148 RID: 328
			// (get) Token: 0x060008D4 RID: 2260 RVA: 0x000374A1 File Offset: 0x000356A1
			// (set) Token: 0x060008D5 RID: 2261 RVA: 0x000374A9 File Offset: 0x000356A9
			[JsonProperty("publisher")]
			public string publisher { get; set; }

			// Token: 0x17000149 RID: 329
			// (get) Token: 0x060008D6 RID: 2262 RVA: 0x000374B2 File Offset: 0x000356B2
			// (set) Token: 0x060008D7 RID: 2263 RVA: 0x000374BA File Offset: 0x000356BA
			[JsonProperty("category")]
			public string category { get; set; }

			// Token: 0x1700014A RID: 330
			// (get) Token: 0x060008D8 RID: 2264 RVA: 0x000374C3 File Offset: 0x000356C3
			// (set) Token: 0x060008D9 RID: 2265 RVA: 0x000374CB File Offset: 0x000356CB
			[JsonProperty("description")]
			public string description { get; set; }

			// Token: 0x1700014B RID: 331
			// (get) Token: 0x060008DA RID: 2266 RVA: 0x000374D4 File Offset: 0x000356D4
			// (set) Token: 0x060008DB RID: 2267 RVA: 0x000374DC File Offset: 0x000356DC
			[JsonProperty("icon_path")]
			public string icon_path { get; set; }

			// Token: 0x1700014C RID: 332
			// (get) Token: 0x060008DC RID: 2268 RVA: 0x000374E5 File Offset: 0x000356E5
			// (set) Token: 0x060008DD RID: 2269 RVA: 0x000374ED File Offset: 0x000356ED
			[JsonProperty("file_path")]
			public string file_path { get; set; }

			// Token: 0x1700014D RID: 333
			// (get) Token: 0x060008DE RID: 2270 RVA: 0x000374F6 File Offset: 0x000356F6
			// (set) Token: 0x060008DF RID: 2271 RVA: 0x000374FE File Offset: 0x000356FE
			[JsonProperty("version")]
			public string version { get; set; }

			// Token: 0x1700014E RID: 334
			// (get) Token: 0x060008E0 RID: 2272 RVA: 0x00037507 File Offset: 0x00035707
			// (set) Token: 0x060008E1 RID: 2273 RVA: 0x0003750F File Offset: 0x0003570F
			[JsonProperty("type")]
			public string type { get; set; }

			// Token: 0x1700014F RID: 335
			// (get) Token: 0x060008E2 RID: 2274 RVA: 0x00037518 File Offset: 0x00035718
			// (set) Token: 0x060008E3 RID: 2275 RVA: 0x00037520 File Offset: 0x00035720
			[JsonProperty("featured")]
			public int featured { get; set; }
		}

		// Token: 0x02000150 RID: 336
		public class SearchResultsViewModel
		{
			// Token: 0x17000150 RID: 336
			// (get) Token: 0x060008E5 RID: 2277 RVA: 0x00037529 File Offset: 0x00035729
			// (set) Token: 0x060008E6 RID: 2278 RVA: 0x00037531 File Offset: 0x00035731
			public List<StoreApp> Apps { get; set; }
		}
	}
}
