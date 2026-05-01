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
using Windows.UI.Xaml.Navigation;

namespace Win81StoreRevival
{
	// Token: 0x02000019 RID: 25
	public sealed class AllAppsPage1 : Page, IComponentConnector
	{
		// Token: 0x06000114 RID: 276 RVA: 0x00005D98 File Offset: 0x00003F98
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
			Debug.WriteLine("AllAppsPage constructor called");
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00006114 File Offset: 0x00004314
		[DebuggerStepThrough]
		private void AllAppsPage_LoadedAsync(object sender, RoutedEventArgs e)
		{
			AllAppsPage1.<AllAppsPage_LoadedAsync>d__6 <AllAppsPage_LoadedAsync>d__ = new AllAppsPage1.<AllAppsPage_LoadedAsync>d__6();
			<AllAppsPage_LoadedAsync>d__.<>4__this = this;
			<AllAppsPage_LoadedAsync>d__.sender = sender;
			<AllAppsPage_LoadedAsync>d__.e = e;
			<AllAppsPage_LoadedAsync>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<AllAppsPage_LoadedAsync>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <AllAppsPage_LoadedAsync>d__.<>t__builder;
			<>t__builder.Start<AllAppsPage1.<AllAppsPage_LoadedAsync>d__6>(ref <AllAppsPage_LoadedAsync>d__);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000615E File Offset: 0x0000435E
		private void PlaySound()
		{
			SoundManager.PlayNotificationSound();
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00006168 File Offset: 0x00004368
		[DebuggerStepThrough]
		private Task LoadAllAppsFromJsonAsync()
		{
			AllAppsPage1.<LoadAllAppsFromJsonAsync>d__8 <LoadAllAppsFromJsonAsync>d__ = new AllAppsPage1.<LoadAllAppsFromJsonAsync>d__8();
			<LoadAllAppsFromJsonAsync>d__.<>4__this = this;
			<LoadAllAppsFromJsonAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadAllAppsFromJsonAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadAllAppsFromJsonAsync>d__.<>t__builder;
			<>t__builder.Start<AllAppsPage1.<LoadAllAppsFromJsonAsync>d__8>(ref <LoadAllAppsFromJsonAsync>d__);
			return <LoadAllAppsFromJsonAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000061B0 File Offset: 0x000043B0
		[DebuggerStepThrough]
		private Task LoadReviewStatsAsync(List<StoreApp> apps)
		{
			AllAppsPage1.<LoadReviewStatsAsync>d__9 <LoadReviewStatsAsync>d__ = new AllAppsPage1.<LoadReviewStatsAsync>d__9();
			<LoadReviewStatsAsync>d__.<>4__this = this;
			<LoadReviewStatsAsync>d__.apps = apps;
			<LoadReviewStatsAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadReviewStatsAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadReviewStatsAsync>d__.<>t__builder;
			<>t__builder.Start<AllAppsPage1.<LoadReviewStatsAsync>d__9>(ref <LoadReviewStatsAsync>d__);
			return <LoadReviewStatsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00006200 File Offset: 0x00004400
		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			bool canGoBack = base.Frame.CanGoBack;
			if (canGoBack)
			{
				base.Frame.GoBack();
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000622C File Offset: 0x0000442C
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

		// Token: 0x0600011B RID: 283 RVA: 0x000062B4 File Offset: 0x000044B4
		[DebuggerStepThrough]
		private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
		{
			AllAppsPage1.<Image_ImageFailed>d__12 <Image_ImageFailed>d__ = new AllAppsPage1.<Image_ImageFailed>d__12();
			<Image_ImageFailed>d__.<>4__this = this;
			<Image_ImageFailed>d__.sender = sender;
			<Image_ImageFailed>d__.e = e;
			<Image_ImageFailed>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<Image_ImageFailed>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <Image_ImageFailed>d__.<>t__builder;
			<>t__builder.Start<AllAppsPage1.<Image_ImageFailed>d__12>(ref <Image_ImageFailed>d__);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000062FE File Offset: 0x000044FE
		private void SearchButton_Click(object sender, RoutedEventArgs e)
		{
			this.EnterSearchMode();
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00006308 File Offset: 0x00004508
		private void SearchBackButton_Click(object sender, RoutedEventArgs e)
		{
			this.ExitSearchMode();
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00006314 File Offset: 0x00004514
		private void EnterSearchMode()
		{
			this.isSearchMode = true;
			this.MainContentArea.put_Margin(new Thickness(0.0, 180.0, 0.0, 0.0));
			this.SearchTextBox.Focus(3);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000636C File Offset: 0x0000456C
		private void ExitSearchMode()
		{
			this.isSearchMode = false;
			this.SearchTextBox.put_Text("");
			this.MainContentArea.put_Margin(new Thickness(0.0, 100.0, 0.0, 0.0));
			this.SearchResultsText.put_Visibility(1);
			this.NoResultsPanel.put_Visibility(1);
			this.FilterApps("");
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000595D File Offset: 0x00003B5D
		private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000063F0 File Offset: 0x000045F0
		private void SearchTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
		{
			bool flag = e.Key == 13;
			if (flag)
			{
				this.ApplyFilters();
			}
			else
			{
				bool flag2 = e.Key == 27;
				if (flag2)
				{
				}
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00006427 File Offset: 0x00004627
		private void ClearSearchButton_Click(object sender, RoutedEventArgs e)
		{
			this.SearchTextBox.put_Text("");
			this.SearchTextBox.Focus(3);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00006448 File Offset: 0x00004648
		private void FilterApps(string searchText)
		{
			bool flag = string.IsNullOrWhiteSpace(searchText);
			if (flag)
			{
				this.filteredApps.Clear();
				foreach (StoreApp storeApp in this.allApps)
				{
					this.filteredApps.Add(storeApp);
				}
				this.SearchResultsText.put_Visibility(1);
				this.NoResultsPanel.put_Visibility(1);
				this.AllAppsGridView.put_Visibility(0);
			}
			else
			{
				string searchLower = searchText.ToLower();
				List<StoreApp> list = Enumerable.ToList<StoreApp>(Enumerable.Where<StoreApp>(this.allApps, (StoreApp app) => app.Name.ToLower().Contains(searchLower) || app.Publisher.ToLower().Contains(searchLower) || (app.Description != null && app.Description.ToLower().Contains(searchLower)) || (app.Version != null && app.Version.ToLower().Contains(searchLower)) || (app.Id != null && app.Id.ToLower().Contains(searchLower))));
				this.filteredApps.Clear();
				foreach (StoreApp storeApp2 in list)
				{
					this.filteredApps.Add(storeApp2);
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
					this.AllAppsGridView.put_Visibility(0);
				}
				else
				{
					this.SearchResultsText.put_Visibility(1);
					this.NoResultsPanel.put_Visibility(0);
					this.AllAppsGridView.put_Visibility(1);
				}
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00006624 File Offset: 0x00004824
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);
			bool flag = e.Parameter is string && (string)e.Parameter == "search";
			if (flag)
			{
				this.EnterSearchMode();
			}
			this.TypeComboBox.put_SelectedIndex(0);
			this.BuildInitialCategoryBox();
			this.ApplyFilters();
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00006688 File Offset: 0x00004888
		private void BuildInitialCategoryBox()
		{
			bool flag = this.CategoryComboBox == null;
			if (!flag)
			{
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
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00006724 File Offset: 0x00004924
		private void TypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			this.RebuildCategoryBox();
			this.ApplyFilters();
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00006738 File Offset: 0x00004938
		private void RebuildCategoryBox()
		{
			bool flag = this.CategoryComboBox == null || this.TypeComboBox == null;
			if (!flag)
			{
				this.CategoryComboBox.Items.Clear();
				ICollection<object> items = this.CategoryComboBox.Items;
				ComboBoxItem comboBoxItem = new ComboBoxItem();
				comboBoxItem.put_Content("All");
				items.Add(comboBoxItem);
				string comboValue = this.GetComboValue(this.TypeComboBox, "All");
				bool flag2 = comboValue.Equals("All", 5);
				IEnumerable<string> enumerable2;
				if (flag2)
				{
					IEnumerable<string> enumerable = this.allCategories;
					enumerable2 = (enumerable ?? Enumerable.Empty<string>());
				}
				else
				{
					IEnumerable<StoreApp> enumerable3 = this.allApps;
					IEnumerable<StoreApp> enumerable4 = enumerable3 ?? Enumerable.Empty<StoreApp>();
					bool flag3 = comboValue.Equals("App", 5);
					if (flag3)
					{
						enumerable4 = Enumerable.Where<StoreApp>(enumerable4, delegate(StoreApp a)
						{
							string type = a.Type;
							return type != null && type.Equals("App", 5);
						});
					}
					else
					{
						bool flag4 = comboValue.Equals("Game", 5);
						if (flag4)
						{
							enumerable4 = Enumerable.Where<StoreApp>(enumerable4, delegate(StoreApp a)
							{
								string type = a.Type;
								return type != null && type.Equals("Game", 5);
							});
						}
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
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000693C File Offset: 0x00004B3C
		private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			this.ApplyFilters();
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00006948 File Offset: 0x00004B48
		private void ApplyFilters()
		{
			AllAppsPage1.<>c__DisplayClass26_0 CS$<>8__locals1 = new AllAppsPage1.<>c__DisplayClass26_0();
			bool flag = this.allApps == null || this.allApps.Count == 0;
			if (!flag)
			{
				string comboValue = this.GetComboValue(this.TypeComboBox, "All");
				CS$<>8__locals1.category = this.GetComboValue(this.CategoryComboBox, "All");
				AllAppsPage1.<>c__DisplayClass26_0 CS$<>8__locals2 = CS$<>8__locals1;
				string text = this.SearchTextBox.Text;
				CS$<>8__locals2.search = ((text != null) ? text.Trim().ToLower() : null);
				IEnumerable<StoreApp> enumerable = this.allApps;
				bool flag2 = comboValue == "App";
				if (flag2)
				{
					enumerable = Enumerable.Where<StoreApp>(enumerable, delegate(StoreApp a)
					{
						string type = a.Type;
						return type != null && type.Equals("App", 5);
					});
				}
				else
				{
					bool flag3 = comboValue == "Game";
					if (flag3)
					{
						enumerable = Enumerable.Where<StoreApp>(enumerable, delegate(StoreApp a)
						{
							string type = a.Type;
							return type != null && type.Equals("Game", 5);
						});
					}
				}
				bool flag4 = CS$<>8__locals1.category != "All";
				if (flag4)
				{
					enumerable = Enumerable.Where<StoreApp>(enumerable, delegate(StoreApp a)
					{
						string category = a.Category;
						return category != null && category.Equals(CS$<>8__locals1.category, 5);
					});
				}
				bool flag5 = !string.IsNullOrWhiteSpace(CS$<>8__locals1.search);
				if (flag5)
				{
					enumerable = Enumerable.Where<StoreApp>(enumerable, delegate(StoreApp a)
					{
						string name = a.Name;
						if (name == null || !name.ToLower().Contains(CS$<>8__locals1.search))
						{
							string publisher = a.Publisher;
							if (publisher == null || !publisher.ToLower().Contains(CS$<>8__locals1.search))
							{
								string description = a.Description;
								return description != null && description.ToLower().Contains(CS$<>8__locals1.search);
							}
						}
						return true;
					});
				}
				List<StoreApp> list = Enumerable.ToList<StoreApp>(enumerable);
				this.filteredApps.Clear();
				foreach (StoreApp storeApp in list)
				{
					this.filteredApps.Add(storeApp);
				}
				bool flag6 = this.filteredApps.Count > 0;
				if (flag6)
				{
					this.SearchResultsText.put_Text(string.Format("{0} app{1} found", new object[]
					{
						this.filteredApps.Count,
						(this.filteredApps.Count == 1) ? "" : "s"
					}));
					this.SearchResultsText.put_Visibility(0);
					this.NoResultsPanel.put_Visibility(1);
					this.AllAppsGridView.put_Visibility(0);
				}
				else
				{
					this.SearchResultsText.put_Visibility(1);
					this.NoResultsPanel.put_Visibility(0);
					this.AllAppsGridView.put_Visibility(1);
				}
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00006BA8 File Offset: 0x00004DA8
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

		// Token: 0x0600012B RID: 299 RVA: 0x00006BEC File Offset: 0x00004DEC
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			bool contentLoaded = this._contentLoaded;
			if (!contentLoaded)
			{
				this._contentLoaded = true;
				Application.LoadComponent(this, new Uri("ms-appx:///AllAppsPage1.xaml"), 0);
				this.TypeComboBox = (ComboBox)base.FindName("TypeComboBox");
				this.CategoryComboBox = (ComboBox)base.FindName("CategoryComboBox");
				this.MainContentArea = (Grid)base.FindName("MainContentArea");
				this.LoadingPanel = (StackPanel)base.FindName("LoadingPanel");
				this.NoResultsPanel = (StackPanel)base.FindName("NoResultsPanel");
				this.AllAppsGridView = (GridView)base.FindName("AllAppsGridView");
				this.LoadingRing = (ProgressRing)base.FindName("LoadingRing");
				this.TopAppsButton = (Button)base.FindName("TopAppsButton");
				this.SearchTextBox = (TextBox)base.FindName("SearchTextBox");
				this.SearchResultsText = (TextBlock)base.FindName("SearchResultsText");
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00006D00 File Offset: 0x00004F00
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
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uielement.add_Tapped), new Action<EventRegistrationToken>(uielement.remove_Tapped), new TappedEventHandler(this.AppItem_Tapped));
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
				TextBox textBox = (TextBox)target;
				WindowsRuntimeMarshal.AddEventHandler<TextChangedEventHandler>(new Func<TextChangedEventHandler, EventRegistrationToken>(textBox.add_TextChanged), new Action<EventRegistrationToken>(textBox.remove_TextChanged), new TextChangedEventHandler(this.SearchTextBox_TextChanged));
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<KeyEventHandler>(new Func<KeyEventHandler, EventRegistrationToken>(uielement.add_KeyDown), new Action<EventRegistrationToken>(uielement.remove_KeyDown), new KeyEventHandler(this.SearchTextBox_KeyDown));
				break;
			}
			}
			this._contentLoaded = true;
		}

		// Token: 0x04000087 RID: 135
		private ObservableCollection<StoreApp> allApps = new ObservableCollection<StoreApp>();

		// Token: 0x04000088 RID: 136
		private ObservableCollection<StoreApp> filteredApps = new ObservableCollection<StoreApp>();

		// Token: 0x04000089 RID: 137
		private const string AppsJsonUrl = "https://8store.modyleprojects.ru/data/apps.json";

		// Token: 0x0400008A RID: 138
		private bool isSearchMode = false;

		// Token: 0x0400008B RID: 139
		private static List<StoreApp> cachedApps = null;

		// Token: 0x0400008C RID: 140
		private List<string> allCategories;

		// Token: 0x0400008D RID: 141
		private List<string> appCategories;

		// Token: 0x0400008E RID: 142
		private List<string> gamesCategories;

		// Token: 0x0400008F RID: 143
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ComboBox TypeComboBox;

		// Token: 0x04000090 RID: 144
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ComboBox CategoryComboBox;

		// Token: 0x04000091 RID: 145
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Grid MainContentArea;

		// Token: 0x04000092 RID: 146
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel LoadingPanel;

		// Token: 0x04000093 RID: 147
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel NoResultsPanel;

		// Token: 0x04000094 RID: 148
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private GridView AllAppsGridView;

		// Token: 0x04000095 RID: 149
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ProgressRing LoadingRing;

		// Token: 0x04000096 RID: 150
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button TopAppsButton;

		// Token: 0x04000097 RID: 151
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBox SearchTextBox;

		// Token: 0x04000098 RID: 152
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock SearchResultsText;

		// Token: 0x04000099 RID: 153
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
