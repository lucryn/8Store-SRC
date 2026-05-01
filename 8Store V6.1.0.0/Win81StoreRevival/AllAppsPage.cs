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
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media.Imaging;

namespace Win81StoreRevival
{
	// Token: 0x0200001A RID: 26
	public sealed class AllAppsPage : Page, IComponentConnector
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600012B RID: 299 RVA: 0x000054FE File Offset: 0x000036FE
		// (set) Token: 0x0600012C RID: 300 RVA: 0x00005506 File Offset: 0x00003706
		public ObservableCollection<AppCollection> AllCollections { get; set; } = new ObservableCollection<AppCollection>();

		// Token: 0x0600012D RID: 301 RVA: 0x00005510 File Offset: 0x00003710
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
			this.UpdateTitleNB();
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000056BC File Offset: 0x000038BC
		private void AllAppsPage_LoadedAsync(object sender, RoutedEventArgs e)
		{
			AllAppsPage.<AllAppsPage_LoadedAsync>d__14 <AllAppsPage_LoadedAsync>d__;
			<AllAppsPage_LoadedAsync>d__.<>4__this = this;
			<AllAppsPage_LoadedAsync>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<AllAppsPage_LoadedAsync>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <AllAppsPage_LoadedAsync>d__.<>t__builder;
			<>t__builder.Start<AllAppsPage.<AllAppsPage_LoadedAsync>d__14>(ref <AllAppsPage_LoadedAsync>d__);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000056F8 File Offset: 0x000038F8
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

		// Token: 0x06000130 RID: 304 RVA: 0x0000575C File Offset: 0x0000395C
		private Task InitializeCacheAsync()
		{
			AllAppsPage.<InitializeCacheAsync>d__16 <InitializeCacheAsync>d__;
			<InitializeCacheAsync>d__.<>4__this = this;
			<InitializeCacheAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<InitializeCacheAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <InitializeCacheAsync>d__.<>t__builder;
			<>t__builder.Start<AllAppsPage.<InitializeCacheAsync>d__16>(ref <InitializeCacheAsync>d__);
			return <InitializeCacheAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000057A4 File Offset: 0x000039A4
		private Task<StorageFile> GetCacheFileAsync()
		{
			AllAppsPage.<GetCacheFileAsync>d__17 <GetCacheFileAsync>d__;
			<GetCacheFileAsync>d__.<>4__this = this;
			<GetCacheFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<StorageFile>.Create();
			<GetCacheFileAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<StorageFile> <>t__builder = <GetCacheFileAsync>d__.<>t__builder;
			<>t__builder.Start<AllAppsPage.<GetCacheFileAsync>d__17>(ref <GetCacheFileAsync>d__);
			return <GetCacheFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00004859 File Offset: 0x00002A59
		private void StoreLogo_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(MainPage));
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000057EC File Offset: 0x000039EC
		private void nbSearch_QuerySubmitted(object sender, SearchBoxQuerySubmittedEventArgs e)
		{
			string text = (e != null && e.QueryText != null) ? e.QueryText.Trim() : null;
			if (!string.IsNullOrEmpty(text))
			{
				base.Frame.Navigate(typeof(SearchResultsPage), text);
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00005834 File Offset: 0x00003A34
		private Task<bool> TryLoadAppsFromDiskCache()
		{
			AllAppsPage.<TryLoadAppsFromDiskCache>d__20 <TryLoadAppsFromDiskCache>d__;
			<TryLoadAppsFromDiskCache>d__.<>4__this = this;
			<TryLoadAppsFromDiskCache>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<TryLoadAppsFromDiskCache>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<bool> <>t__builder = <TryLoadAppsFromDiskCache>d__.<>t__builder;
			<>t__builder.Start<AllAppsPage.<TryLoadAppsFromDiskCache>d__20>(ref <TryLoadAppsFromDiskCache>d__);
			return <TryLoadAppsFromDiskCache>d__.<>t__builder.Task;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00005879 File Offset: 0x00003A79
		public static string Localized(string key, params object[] args)
		{
			return string.Format(new ResourceLoader().GetString(key), args);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000588C File Offset: 0x00003A8C
		private string LocalizedCategory(string category)
		{
			if (string.IsNullOrEmpty(category))
			{
				return category;
			}
			string text = category.Replace(" ", "").Replace("&", "");
			string text2 = AllAppsPage.Localized("CollectionCategory_" + text, new object[0]);
			if (string.IsNullOrEmpty(text2))
			{
				return category;
			}
			return text2;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000058E8 File Offset: 0x00003AE8
		private Task SaveToDiskCacheAsync(List<StoreApp> apps)
		{
			AllAppsPage.<SaveToDiskCacheAsync>d__23 <SaveToDiskCacheAsync>d__;
			<SaveToDiskCacheAsync>d__.<>4__this = this;
			<SaveToDiskCacheAsync>d__.apps = apps;
			<SaveToDiskCacheAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SaveToDiskCacheAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <SaveToDiskCacheAsync>d__.<>t__builder;
			<>t__builder.Start<AllAppsPage.<SaveToDiskCacheAsync>d__23>(ref <SaveToDiskCacheAsync>d__);
			return <SaveToDiskCacheAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00005938 File Offset: 0x00003B38
		private Task LoadAllAppsFromJsonAsync()
		{
			AllAppsPage.<LoadAllAppsFromJsonAsync>d__24 <LoadAllAppsFromJsonAsync>d__;
			<LoadAllAppsFromJsonAsync>d__.<>4__this = this;
			<LoadAllAppsFromJsonAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadAllAppsFromJsonAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadAllAppsFromJsonAsync>d__.<>t__builder;
			<>t__builder.Start<AllAppsPage.<LoadAllAppsFromJsonAsync>d__24>(ref <LoadAllAppsFromJsonAsync>d__);
			return <LoadAllAppsFromJsonAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00005980 File Offset: 0x00003B80
		private Task LoadReviewStatsInBackground()
		{
			AllAppsPage.<LoadReviewStatsInBackground>d__25 <LoadReviewStatsInBackground>d__;
			<LoadReviewStatsInBackground>d__.<>4__this = this;
			<LoadReviewStatsInBackground>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadReviewStatsInBackground>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadReviewStatsInBackground>d__.<>t__builder;
			<>t__builder.Start<AllAppsPage.<LoadReviewStatsInBackground>d__25>(ref <LoadReviewStatsInBackground>d__);
			return <LoadReviewStatsInBackground>d__.<>t__builder.Task;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000059C8 File Offset: 0x00003BC8
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

		// Token: 0x0600013B RID: 315 RVA: 0x00005F3C File Offset: 0x0000413C
		private List<string> GetRandomIcons(List<StoreApp> sourceApps, Random random, int count)
		{
			List<string> list = new List<string>();
			if (sourceApps == null || sourceApps.Count == 0)
			{
				for (int i = 0; i < count; i++)
				{
					list.Add("ms-appx:///Assets/icon-error.png");
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

		// Token: 0x0600013C RID: 316 RVA: 0x00005FE8 File Offset: 0x000041E8
		private void PlaySound()
		{
			SoundManager.PlayStartupSound();
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00005FEF File Offset: 0x000041EF
		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			if (base.Frame.CanGoBack)
			{
				base.Frame.GoBack();
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00006009 File Offset: 0x00004209
		private void TopAppsButton_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(AllAppsPage));
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00006021 File Offset: 0x00004221
		private void TopAppsButton_Click_1(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(PopularPage));
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00006039 File Offset: 0x00004239
		private void DownloadHubButton_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(DownloadsHub));
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00006054 File Offset: 0x00004254
		private void CollectionTile_Click(object sender, ItemClickEventArgs e)
		{
			AppCollection appCollection = e.ClickedItem as AppCollection;
			if (appCollection != null)
			{
				base.Frame.Navigate(typeof(CollectionsViewPage), appCollection.Name);
			}
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000608C File Offset: 0x0000428C
		private void CollectionTile_Tapped(object sender, TappedRoutedEventArgs e)
		{
			Grid grid = sender as Grid;
			if (grid != null)
			{
				AppCollection appCollection = grid.DataContext as AppCollection;
				if (appCollection != null)
				{
					base.Frame.Navigate(typeof(CollectionsViewPage), appCollection.Name);
				}
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00004901 File Offset: 0x00002B01
		private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000060D0 File Offset: 0x000042D0
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

		// Token: 0x06000145 RID: 325 RVA: 0x00006114 File Offset: 0x00004314
		private void CollectionsGridView_Loaded(object sender, RoutedEventArgs e)
		{
			this.collectionsGridView = (sender as GridView);
			if (this.collectionsGridView != null)
			{
				this.collectionsGridView.put_ItemsSource(this.AllCollections);
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000613C File Offset: 0x0000433C
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

		// Token: 0x06000147 RID: 327 RVA: 0x0000616C File Offset: 0x0000436C
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

		// Token: 0x06000148 RID: 328 RVA: 0x000067A8 File Offset: 0x000049A8
		private string GetAppCountText(ResourceLoader tns, int count)
		{
			string pluralKey = this.GetPluralKey(count, "AppCounter", "AppsCounter1", "AppsCounter");
			return string.Format(tns.GetString(pluralKey), new object[]
			{
				count
			});
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000067E8 File Offset: 0x000049E8
		private string GetGameCountText(ResourceLoader tns, int count)
		{
			string pluralKey = this.GetPluralKey(count, "GameCounter", "GamesCounter1", "GamesCounter");
			return string.Format(tns.GetString(pluralKey), new object[]
			{
				count
			});
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00006827 File Offset: 0x00004A27
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

		// Token: 0x0600014B RID: 331 RVA: 0x0000685C File Offset: 0x00004A5C
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
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
			this.nbLogoText = (TextBlock)base.FindName("nbLogoText");
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000697C File Offset: 0x00004B7C
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
			case 8:
			{
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uielement.add_Tapped), new Action<EventRegistrationToken>(uielement.remove_Tapped), new TappedEventHandler(this.StoreLogo_Click));
				break;
			}
			}
			this._contentLoaded = true;
		}

		// Token: 0x0400006B RID: 107
		private ObservableCollection<StoreApp> allApps = new ObservableCollection<StoreApp>();

		// Token: 0x0400006C RID: 108
		private const string AppsJsonUrl = "https://8store.modyleprojects.ru/data/apps.json";

		// Token: 0x0400006D RID: 109
		private GridView collectionsGridView;

		// Token: 0x0400006E RID: 110
		private const string CACHE_FOLDER_NAME = "AppDataCache";

		// Token: 0x0400006F RID: 111
		private const string APPS_CACHE_FILE = "apps_cache.json";

		// Token: 0x04000070 RID: 112
		private StorageFolder _cacheFolder;

		// Token: 0x04000071 RID: 113
		private StorageFile _appsCacheFile;

		// Token: 0x04000073 RID: 115
		private List<string> appCategories;

		// Token: 0x04000074 RID: 116
		private List<string> gamesCategories;

		// Token: 0x04000075 RID: 117
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Grid MainContentGrid;

		// Token: 0x04000076 RID: 118
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Grid LoadingPanel;

		// Token: 0x04000077 RID: 119
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Hub AllAppsHub;

		// Token: 0x04000078 RID: 120
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel NoResultsPanel;

		// Token: 0x04000079 RID: 121
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button backButton;

		// Token: 0x0400007A RID: 122
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock resultText;

		// Token: 0x0400007B RID: 123
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private HubSection CollectionsSection;

		// Token: 0x0400007C RID: 124
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ProgressRing LoadingRing;

		// Token: 0x0400007D RID: 125
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock LoadingText;

		// Token: 0x0400007E RID: 126
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock SearchResultsText;

		// Token: 0x0400007F RID: 127
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock nbLogoText;

		// Token: 0x04000080 RID: 128
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
