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
using Windows.System;
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
	// Token: 0x0200001C RID: 28
	public sealed class AllAppsPage1 : Page, IComponentConnector
	{
		// Token: 0x06000158 RID: 344 RVA: 0x00006C50 File Offset: 0x00004E50
		public AllAppsPage1()
		{
			List<string> list = new List<string>();
			list.Add("All");
			list.Add("Books + Reference");
			list.Add("Dependency");
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
			list.Add("Action");
			list.Add("Adventure");
			list.Add("Arcade");
			list.Add("Card + Casino");
			list.Add("Classics");
			list.Add("Family + Kids");
			list.Add("Games");
			list.Add("Music + Rhythm");
			list.Add("Puzzle");
			list.Add("Racing + Flying");
			list.Add("Strategy");
			list.Add("Simulation");
			this.allCategories = list;
			List<string> list2 = new List<string>();
			list2.Add("All");
			list2.Add("Books + Reference");
			list2.Add("Dependency");
			list2.Add("Education");
			list2.Add("Finance");
			list2.Add("Food + Cooking");
			list2.Add("Government");
			list2.Add("Health + Fitness");
			list2.Add("Music + Audio");
			list2.Add("News + Weather");
			list2.Add("Other");
			list2.Add("Photo + Design");
			list2.Add("Security");
			list2.Add("Shopping");
			list2.Add("Social");
			list2.Add("Sports");
			list2.Add("Travel + Navigation");
			list2.Add("Utilities");
			list2.Add("Video + Entertainment");
			this.appCategories = list2;
			List<string> list3 = new List<string>();
			list3.Add("Action");
			list3.Add("Adventure");
			list3.Add("Arcade");
			list3.Add("Card + Casino");
			list3.Add("Classics");
			list3.Add("Family + Kids");
			list3.Add("Games");
			list3.Add("Music + Rhythm");
			list3.Add("Puzzle");
			list3.Add("Racing + Flying");
			list3.Add("Strategy");
			list3.Add("Simulation");
			this.gamesCategories = list3;
			base..ctor();
			this.InitializeComponent();
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(this.add_Loaded), new Action<EventRegistrationToken>(this.remove_Loaded), new RoutedEventHandler(this.AllAppsPage_LoadedAsync));
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(this.add_Unloaded), new Action<EventRegistrationToken>(this.remove_Unloaded), new RoutedEventHandler(this.AllAppsPage_Unloaded));
			Window window = Window.Current;
			WindowsRuntimeMarshal.AddEventHandler<WindowSizeChangedEventHandler>(new Func<WindowSizeChangedEventHandler, EventRegistrationToken>(window.add_SizeChanged), new Action<EventRegistrationToken>(window.remove_SizeChanged), new WindowSizeChangedEventHandler(this.OnPageSizeChanged));
			this.UpdateResultText(0);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00006FE4 File Offset: 0x000051E4
		private void AllAppsPage_LoadedAsync(object sender, RoutedEventArgs e)
		{
			AllAppsPage1.<AllAppsPage_LoadedAsync>d__9 <AllAppsPage_LoadedAsync>d__;
			<AllAppsPage_LoadedAsync>d__.<>4__this = this;
			<AllAppsPage_LoadedAsync>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<AllAppsPage_LoadedAsync>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <AllAppsPage_LoadedAsync>d__.<>t__builder;
			<>t__builder.Start<AllAppsPage1.<AllAppsPage_LoadedAsync>d__9>(ref <AllAppsPage_LoadedAsync>d__);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000701D File Offset: 0x0000521D
		private void PlaySound()
		{
			SoundManager.PlayNotificationSound();
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00007024 File Offset: 0x00005224
		private Task LoadAllAppsFromJsonAsync()
		{
			AllAppsPage1.<LoadAllAppsFromJsonAsync>d__11 <LoadAllAppsFromJsonAsync>d__;
			<LoadAllAppsFromJsonAsync>d__.<>4__this = this;
			<LoadAllAppsFromJsonAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadAllAppsFromJsonAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadAllAppsFromJsonAsync>d__.<>t__builder;
			<>t__builder.Start<AllAppsPage1.<LoadAllAppsFromJsonAsync>d__11>(ref <LoadAllAppsFromJsonAsync>d__);
			return <LoadAllAppsFromJsonAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00004859 File Offset: 0x00002A59
		private void StoreLogo_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(MainPage));
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000706C File Offset: 0x0000526C
		private void nbSearch_QuerySubmitted(object sender, SearchBoxQuerySubmittedEventArgs e)
		{
			string text = (e != null && e.QueryText != null) ? e.QueryText.Trim() : null;
			if (!string.IsNullOrEmpty(text))
			{
				base.Frame.Navigate(typeof(SearchResultsPage), text);
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000070B4 File Offset: 0x000052B4
		private Task LoadReviewStatsAsync(List<StoreApp> apps)
		{
			AllAppsPage1.<LoadReviewStatsAsync>d__14 <LoadReviewStatsAsync>d__;
			<LoadReviewStatsAsync>d__.apps = apps;
			<LoadReviewStatsAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadReviewStatsAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadReviewStatsAsync>d__.<>t__builder;
			<>t__builder.Start<AllAppsPage1.<LoadReviewStatsAsync>d__14>(ref <LoadReviewStatsAsync>d__);
			return <LoadReviewStatsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00005FEF File Offset: 0x000041EF
		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			if (base.Frame.CanGoBack)
			{
				base.Frame.GoBack();
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000070FC File Offset: 0x000052FC
		private void AppItem_Tapped(object sender, TappedRoutedEventArgs e)
		{
			Grid grid = sender as Grid;
			if (grid != null)
			{
				StoreApp storeApp = grid.DataContext as StoreApp;
				if (storeApp != null && !storeApp.Id.StartsWith("settings"))
				{
					base.Frame.Navigate(typeof(AppDetailsPage), storeApp);
				}
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000714C File Offset: 0x0000534C
		private void AllAppsGridView_ItemClick(object sender, ItemClickEventArgs e)
		{
			StoreApp storeApp = e.ClickedItem as StoreApp;
			if (storeApp != null && !storeApp.Id.StartsWith("settings"))
			{
				base.Frame.Navigate(typeof(AppDetailsPage), storeApp);
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00007194 File Offset: 0x00005394
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

		// Token: 0x06000163 RID: 355 RVA: 0x00004901 File Offset: 0x00002B01
		private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
		}

		// Token: 0x06000164 RID: 356 RVA: 0x000071D8 File Offset: 0x000053D8
		private void SearchTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
		{
			if (e.Key == 13)
			{
				this.ApplyFilters();
				return;
			}
			VirtualKey key = e.Key;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000071F8 File Offset: 0x000053F8
		private void FilterApps(string searchText)
		{
			this.filteredApps.Clear();
			foreach (StoreApp storeApp in this.allApps)
			{
				this.filteredApps.Add(storeApp);
			}
			this.UpdateResultText(this.filteredApps.Count);
			this.RefreshVisibleApps();
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000726C File Offset: 0x0000546C
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);
			this.TypeComboBox.put_SelectedIndex(0);
			this.BuildInitialCategoryBox();
			this.ApplyFilters();
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00007290 File Offset: 0x00005490
		private void BuildInitialCategoryBox()
		{
			if (this.CategoryComboBox == null)
			{
				return;
			}
			this.CategoryComboBox.Items.Clear();
			foreach (string text in this.allCategories)
			{
				ICollection<object> items = this.CategoryComboBox.Items;
				ComboBoxItem comboBoxItem = new ComboBoxItem();
				comboBoxItem.put_Content(text);
				items.Add(comboBoxItem);
			}
			this.CategoryComboBox.put_SelectedIndex(0);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00007320 File Offset: 0x00005520
		private void TypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			this.RebuildCategoryBox();
			this.ApplyFilters();
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00007330 File Offset: 0x00005530
		private void RebuildCategoryBox()
		{
			if (this.CategoryComboBox == null || this.TypeComboBox == null)
			{
				return;
			}
			this.CategoryComboBox.Items.Clear();
			ICollection<object> items = this.CategoryComboBox.Items;
			ComboBoxItem comboBoxItem = new ComboBoxItem();
			comboBoxItem.put_Content("All");
			items.Add(comboBoxItem);
			string comboValue = this.GetComboValue(this.TypeComboBox, "All");
			IEnumerable<string> enumerable2;
			if (comboValue.Equals("All", 5))
			{
				IEnumerable<string> enumerable = this.allCategories;
				enumerable2 = (enumerable ?? Enumerable.Empty<string>());
			}
			else
			{
				IEnumerable<StoreApp> enumerable3 = this.allApps;
				IEnumerable<StoreApp> enumerable4 = enumerable3 ?? Enumerable.Empty<StoreApp>();
				if (comboValue.Equals("App", 5))
				{
					enumerable4 = Enumerable.Where<StoreApp>(enumerable4, delegate(StoreApp a)
					{
						string type = a.Type;
						return type != null && type.Equals("App", 5);
					});
				}
				else if (comboValue.Equals("Game", 5))
				{
					enumerable4 = Enumerable.Where<StoreApp>(enumerable4, delegate(StoreApp a)
					{
						string type = a.Type;
						return type != null && type.Equals("Game", 5);
					});
				}
				enumerable2 = Enumerable.OrderBy<string, string>(Enumerable.Distinct<string>(Enumerable.Select<StoreApp, string>(Enumerable.Where<StoreApp>(enumerable4, (StoreApp a) => !string.IsNullOrEmpty(a.Category)), (StoreApp a) => a.Category)), (string a) => a);
			}
			foreach (string text in enumerable2)
			{
				ICollection<object> items2 = this.CategoryComboBox.Items;
				ComboBoxItem comboBoxItem2 = new ComboBoxItem();
				comboBoxItem2.put_Content(text);
				items2.Add(comboBoxItem2);
			}
			this.CategoryComboBox.put_SelectedIndex(0);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000750C File Offset: 0x0000570C
		private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			this.ApplyFilters();
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00007514 File Offset: 0x00005714
		private void ApplyFilters()
		{
			if (this.allApps == null)
			{
				return;
			}
			string comboValue = this.GetComboValue(this.TypeComboBox, "All");
			string category = this.GetComboValue(this.CategoryComboBox, "All");
			IEnumerable<StoreApp> enumerable = this.allApps;
			if (comboValue == "App")
			{
				enumerable = Enumerable.Where<StoreApp>(enumerable, delegate(StoreApp a)
				{
					string type = a.Type;
					return type != null && type.Equals("App", 5);
				});
			}
			else if (comboValue == "Game")
			{
				enumerable = Enumerable.Where<StoreApp>(enumerable, delegate(StoreApp a)
				{
					string type = a.Type;
					return type != null && type.Equals("Game", 5);
				});
			}
			if (category != "All")
			{
				enumerable = Enumerable.Where<StoreApp>(enumerable, delegate(StoreApp a)
				{
					string category = a.Category;
					return category != null && category.Equals(category, 5);
				});
			}
			List<StoreApp> list = Enumerable.ToList<StoreApp>(enumerable);
			this.filteredApps.Clear();
			foreach (StoreApp storeApp in list)
			{
				this.filteredApps.Add(storeApp);
			}
			this.UpdateResultText(this.filteredApps.Count);
			this.RefreshVisibleApps();
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000765C File Offset: 0x0000585C
		private void AllAppsGridView_Loaded(object sender, RoutedEventArgs e)
		{
			this.appsGridView = (sender as GridView);
			this.RefreshVisibleApps();
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00007670 File Offset: 0x00005870
		private void AllAppsPage_Unloaded(object sender, RoutedEventArgs e)
		{
			WindowsRuntimeMarshal.RemoveEventHandler<WindowSizeChangedEventHandler>(new Action<EventRegistrationToken>(Window.Current.remove_SizeChanged), new WindowSizeChangedEventHandler(this.OnPageSizeChanged));
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00007694 File Offset: 0x00005894
		private void OnPageSizeChanged(object sender, WindowSizeChangedEventArgs e)
		{
			this.RefreshVisibleApps();
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000769C File Offset: 0x0000589C
		private void RefreshVisibleApps()
		{
			if (this.appsGridView == null)
			{
				return;
			}
			this.SyncVisibleCollection(this.appsGridView, this.filteredApps, this.visibleApps);
			this.appsGridView.put_ItemsSource(this.visibleApps);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000076D0 File Offset: 0x000058D0
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

		// Token: 0x06000171 RID: 369 RVA: 0x00007730 File Offset: 0x00005930
		private void UpdateResultText(int count)
		{
			if (this.resultText == null)
			{
				return;
			}
			ResourceLoader forCurrentView = ResourceLoader.GetForCurrentView();
			string pluralKey = this.GetPluralKey(count, "AppCounter", "AppsCounter1", "AppsCounter");
			this.resultText.put_Text(string.Format(forCurrentView.GetString(pluralKey), new object[]
			{
				count
			}));
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00006827 File Offset: 0x00004A27
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

		// Token: 0x06000173 RID: 371 RVA: 0x00007789 File Offset: 0x00005989
		private string GetComboValue(ComboBox box, string fallback)
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
			return text ?? fallback;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000077BC File Offset: 0x000059BC
		private T GetVisualChild<T>(DependencyObject parent) where T : DependencyObject
		{
			int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
			for (int i = 0; i < childrenCount; i++)
			{
				DependencyObject child = VisualTreeHelper.GetChild(parent, i);
				if (child != null && child is T)
				{
					return (T)((object)child);
				}
				T visualChild = this.GetVisualChild<T>(child);
				if (visualChild != null)
				{
					return visualChild;
				}
			}
			return default(T);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00007814 File Offset: 0x00005A14
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Application.LoadComponent(this, new Uri("ms-appx:///AllAppsPage1.xaml"), 0);
			this.TypeComboBox = (ComboBox)base.FindName("TypeComboBox");
			this.CategoryComboBox = (ComboBox)base.FindName("CategoryComboBox");
			this.TopProgressBar = (ProgressBar)base.FindName("TopProgressBar");
			this.MainHub = (Hub)base.FindName("MainHub");
			this.backButton = (Button)base.FindName("backButton");
			this.resultText = (TextBlock)base.FindName("resultText");
			this.AppsSection = (HubSection)base.FindName("AppsSection");
			this.nbSearch = (SearchBox)base.FindName("nbSearch");
			this.nbLogoText = (TextBlock)base.FindName("nbLogoText");
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00007908 File Offset: 0x00005B08
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
			{
				Selector selector = (Selector)target;
				WindowsRuntimeMarshal.AddEventHandler<SelectionChangedEventHandler>(new Func<SelectionChangedEventHandler, EventRegistrationToken>(selector.add_SelectionChanged), new Action<EventRegistrationToken>(selector.remove_SelectionChanged), new SelectionChangedEventHandler(this.TypeComboBox_SelectionChanged));
				break;
			}
			case 2:
			{
				Selector selector = (Selector)target;
				WindowsRuntimeMarshal.AddEventHandler<SelectionChangedEventHandler>(new Func<SelectionChangedEventHandler, EventRegistrationToken>(selector.add_SelectionChanged), new Action<EventRegistrationToken>(selector.remove_SelectionChanged), new SelectionChangedEventHandler(this.CategoryComboBox_SelectionChanged));
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
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.BackButton_Click));
				break;
			}
			case 5:
			{
				ListViewBase listViewBase = (ListViewBase)target;
				WindowsRuntimeMarshal.AddEventHandler<ItemClickEventHandler>(new Func<ItemClickEventHandler, EventRegistrationToken>(listViewBase.add_ItemClick), new Action<EventRegistrationToken>(listViewBase.remove_ItemClick), new ItemClickEventHandler(this.AllAppsGridView_ItemClick));
				FrameworkElement frameworkElement = (FrameworkElement)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(frameworkElement.add_Loaded), new Action<EventRegistrationToken>(frameworkElement.remove_Loaded), new RoutedEventHandler(this.AllAppsGridView_Loaded));
				break;
			}
			case 6:
			{
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uielement.add_Tapped), new Action<EventRegistrationToken>(uielement.remove_Tapped), new TappedEventHandler(this.StoreLogo_Click));
				break;
			}
			case 7:
			{
				SearchBox searchBox = (SearchBox)target;
				WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<SearchBox, SearchBoxQuerySubmittedEventArgs>>(new Func<TypedEventHandler<SearchBox, SearchBoxQuerySubmittedEventArgs>, EventRegistrationToken>(searchBox.add_QuerySubmitted), new Action<EventRegistrationToken>(searchBox.remove_QuerySubmitted), new TypedEventHandler<SearchBox, SearchBoxQuerySubmittedEventArgs>(this.nbSearch_QuerySubmitted));
				break;
			}
			}
			this._contentLoaded = true;
		}

		// Token: 0x04000085 RID: 133
		private ObservableCollection<StoreApp> allApps = new ObservableCollection<StoreApp>();

		// Token: 0x04000086 RID: 134
		private ObservableCollection<StoreApp> filteredApps = new ObservableCollection<StoreApp>();

		// Token: 0x04000087 RID: 135
		private ObservableCollection<StoreApp> visibleApps = new ObservableCollection<StoreApp>();

		// Token: 0x04000088 RID: 136
		private const string AppsJsonUrl = "https://8store.modyleprojects.ru/data/apps.json";

		// Token: 0x04000089 RID: 137
		private bool isSearchMode;

		// Token: 0x0400008A RID: 138
		private static List<StoreApp> cachedApps;

		// Token: 0x0400008B RID: 139
		private GridView appsGridView;

		// Token: 0x0400008C RID: 140
		private bool _sectionAnimationsPrimed;

		// Token: 0x0400008D RID: 141
		private List<string> allCategories;

		// Token: 0x0400008E RID: 142
		private List<string> appCategories;

		// Token: 0x0400008F RID: 143
		private List<string> gamesCategories;

		// Token: 0x04000090 RID: 144
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ComboBox TypeComboBox;

		// Token: 0x04000091 RID: 145
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ComboBox CategoryComboBox;

		// Token: 0x04000092 RID: 146
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ProgressBar TopProgressBar;

		// Token: 0x04000093 RID: 147
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Hub MainHub;

		// Token: 0x04000094 RID: 148
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button backButton;

		// Token: 0x04000095 RID: 149
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock resultText;

		// Token: 0x04000096 RID: 150
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private HubSection AppsSection;

		// Token: 0x04000097 RID: 151
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private SearchBox nbSearch;

		// Token: 0x04000098 RID: 152
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock nbLogoText;

		// Token: 0x04000099 RID: 153
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
