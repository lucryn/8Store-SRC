using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Win81StoreRevival.Common;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Win81StoreRevival
{
	// Token: 0x02000024 RID: 36
	public sealed class AppReviewsPage : Page, IComponentConnector
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00009FA4 File Offset: 0x000081A4
		// (set) Token: 0x060001FD RID: 509 RVA: 0x00009FAC File Offset: 0x000081AC
		public bool IsUserLoggedIn { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00009FB5 File Offset: 0x000081B5
		public NavigationHelper NavigationHelper
		{
			get
			{
				return this.navigationHelper;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00009FBD File Offset: 0x000081BD
		public ObservableDictionary DefaultViewModel
		{
			get
			{
				return this.defaultViewModel;
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00009FC8 File Offset: 0x000081C8
		public AppReviewsPage()
		{
			this.InitializeComponent();
			this.navigationHelper = new NavigationHelper(this);
			this.navigationHelper.LoadState += this.navigationHelper_LoadState;
			this.httpClient = new HttpClient();
			this.httpClient.Timeout = TimeSpan.FromSeconds(30.0);
			this.UpdateTitleNB();
			this.IsUserLoggedIn = SupabaseService.IsUserLoggedIn();
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000A070 File Offset: 0x00008270
		private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
		{
			if (e.PageState != null && e.PageState.ContainsKey("SelectedItem"))
			{
				object obj = e.PageState["SelectedItem"];
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000A0A0 File Offset: 0x000082A0
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

		// Token: 0x06000203 RID: 515 RVA: 0x0000A104 File Offset: 0x00008304
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			AppReviewsPage.<OnNavigatedTo>d__23 <OnNavigatedTo>d__;
			<OnNavigatedTo>d__.<>4__this = this;
			<OnNavigatedTo>d__.e = e;
			<OnNavigatedTo>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<OnNavigatedTo>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <OnNavigatedTo>d__.<>t__builder;
			<>t__builder.Start<AppReviewsPage.<OnNavigatedTo>d__23>(ref <OnNavigatedTo>d__);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000A148 File Offset: 0x00008348
		private Task LoadReviewsForApp(string appId, bool forceReload)
		{
			AppReviewsPage.<LoadReviewsForApp>d__24 <LoadReviewsForApp>d__;
			<LoadReviewsForApp>d__.<>4__this = this;
			<LoadReviewsForApp>d__.appId = appId;
			<LoadReviewsForApp>d__.forceReload = forceReload;
			<LoadReviewsForApp>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadReviewsForApp>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadReviewsForApp>d__.<>t__builder;
			<>t__builder.Start<AppReviewsPage.<LoadReviewsForApp>d__24>(ref <LoadReviewsForApp>d__);
			return <LoadReviewsForApp>d__.<>t__builder.Task;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000A1A0 File Offset: 0x000083A0
		private List<Review> ApplySortFilter(List<Review> reviews)
		{
			if (reviews == null || reviews.Count == 0)
			{
				return reviews ?? new List<Review>();
			}
			IOrderedEnumerable<Review> orderedEnumerable = null;
			if (this.currentFilter == "positive_first")
			{
				orderedEnumerable = Enumerable.OrderByDescending<Review, int>(reviews, (Review r) => r.Rating);
			}
			else if (this.currentFilter == "negative_first")
			{
				orderedEnumerable = Enumerable.OrderBy<Review, int>(reviews, (Review r) => r.Rating);
			}
			if (orderedEnumerable != null)
			{
				if (this.currentSort == "oldest")
				{
					orderedEnumerable = Enumerable.ThenBy<Review, DateTime>(orderedEnumerable, (Review r) => r.CreatedAt);
				}
				else
				{
					orderedEnumerable = Enumerable.ThenByDescending<Review, DateTime>(orderedEnumerable, (Review r) => r.CreatedAt);
				}
				return Enumerable.ToList<Review>(orderedEnumerable);
			}
			string text = this.currentSort;
			if (text == "oldest")
			{
				return Enumerable.ToList<Review>(Enumerable.OrderBy<Review, DateTime>(reviews, (Review r) => r.CreatedAt));
			}
			if (!(text == "highest"))
			{
				return Enumerable.ToList<Review>(Enumerable.OrderByDescending<Review, DateTime>(reviews, (Review r) => r.CreatedAt));
			}
			return Enumerable.ToList<Review>(Enumerable.ThenByDescending<Review, DateTime>(Enumerable.OrderByDescending<Review, int>(reviews, (Review r) => r.Rating), (Review r) => r.CreatedAt));
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000A368 File Offset: 0x00008568
		private void UpdateReviewSummary()
		{
			string text = null;
			UserAccount userAccount = SupabaseService.GetCurrentUser();
			if (userAccount != null)
			{
				text = userAccount.Id;
			}
			if (this.currentReviews != null && Enumerable.Any<Review>(this.currentReviews))
			{
				double num = Enumerable.Average<Review>(this.currentReviews, (Review r) => r.Rating);
				int count = this.currentReviews.Count;
				int star = Enumerable.Count<Review>(Enumerable.Where<Review>(this.currentReviews, (Review r) => r.Rating == 1));
				int star2 = Enumerable.Count<Review>(Enumerable.Where<Review>(this.currentReviews, (Review r) => r.Rating == 2));
				int star3 = Enumerable.Count<Review>(Enumerable.Where<Review>(this.currentReviews, (Review r) => r.Rating == 3));
				int star4 = Enumerable.Count<Review>(Enumerable.Where<Review>(this.currentReviews, (Review r) => r.Rating == 4));
				int star5 = Enumerable.Count<Review>(Enumerable.Where<Review>(this.currentReviews, (Review r) => r.Rating == 5));
				this.currentSummary = new ReviewStats
				{
					AverageRating = num,
					ReviewCount = count,
					Star1 = star,
					Star2 = star2,
					Star3 = star3,
					Star4 = star4,
					Star5 = star5
				};
				ResourceLoader forCurrentView = ResourceLoader.GetForCurrentView();
				foreach (Review review in this.currentReviews)
				{
					review.IsOwnReview = (!string.IsNullOrEmpty(text) && review.UserId == text);
				}
				if (count == 1)
				{
					this.resultText.put_Text(string.Format("1 {0}", new object[]
					{
						forCurrentView.GetString("ReviewText")
					}));
				}
				else
				{
					this.resultText.put_Text(string.Format("{0} {1}", new object[]
					{
						count,
						forCurrentView.GetString("ReviewsText")
					}));
				}
				this.DefaultViewModel["Reviews"] = this.currentReviews;
				this.DefaultViewModel["Summary"] = this.currentSummary;
				int num2 = (int)Math.Floor(num);
				double num3 = num - (double)num2;
				List<double> list = new List<double>();
				for (int i = 0; i < num2; i++)
				{
					list.Add(1.0);
				}
				if (num3 > 0.0)
				{
					list.Add(num3);
				}
				while (list.Count < 5)
				{
					list.Add(0.0);
				}
				ObservableDictionary observableDictionary = this.DefaultViewModel;
				string key = "StarBg";
				List<double> list2 = new List<double>();
				list2.Add(1.0);
				list2.Add(1.0);
				list2.Add(1.0);
				list2.Add(1.0);
				list2.Add(1.0);
				observableDictionary[key] = list2;
				this.DefaultViewModel["StarList"] = list;
				this.UpdateStarClipping(num);
				return;
			}
			ResourceLoader forCurrentView2 = ResourceLoader.GetForCurrentView();
			this.resultText.put_Text(string.Format("0 {0}", new object[]
			{
				forCurrentView2.GetString("ReviewsText")
			}));
			this.DefaultViewModel["Reviews"] = new List<Review>();
			this.DefaultViewModel["Summary"] = new ReviewStats
			{
				AverageRating = 0.0,
				ReviewCount = 0
			};
			ObservableDictionary observableDictionary2 = this.DefaultViewModel;
			string key2 = "StarBg";
			List<double> list3 = new List<double>();
			list3.Add(1.0);
			list3.Add(1.0);
			list3.Add(1.0);
			list3.Add(1.0);
			list3.Add(1.0);
			observableDictionary2[key2] = list3;
			ObservableDictionary observableDictionary3 = this.DefaultViewModel;
			string key3 = "StarList";
			List<double> list4 = new List<double>();
			list4.Add(0.0);
			list4.Add(0.0);
			list4.Add(0.0);
			list4.Add(0.0);
			list4.Add(0.0);
			observableDictionary3[key3] = list4;
			this.UpdateStarClipping(0.0);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000A82C File Offset: 0x00008A2C
		private void UpdateStarClipping(double averageRating)
		{
			try
			{
				TextBlock textBlock = AppReviewsPage.FindVisualChild<TextBlock>(this, "GreenStarsLayer");
				if (textBlock != null && textBlock.Clip != null)
				{
					RectangleGeometry clip = textBlock.Clip;
					double num = textBlock.ActualWidth;
					if (num <= 0.0)
					{
						num = 274.0;
					}
					double num2 = averageRating / 5.0 * num;
					clip.put_Rect(new Rect(0.0, 0.0, num2, 100.0));
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000A8BC File Offset: 0x00008ABC
		private static T FindVisualChild<T>(DependencyObject obj, string name) where T : FrameworkElement
		{
			if (obj == null)
			{
				return default(T);
			}
			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
			{
				DependencyObject child = VisualTreeHelper.GetChild(obj, i);
				if (child is T && (child as FrameworkElement).Name == name)
				{
					return child as T;
				}
				T t = AppReviewsPage.FindVisualChild<T>(child, name);
				if (t != null)
				{
					return t;
				}
			}
			return default(T);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000A931 File Offset: 0x00008B31
		protected override void OnNavigatedFrom(NavigationEventArgs e)
		{
			this.navigationHelper.OnNavigatedFrom(e);
			HttpClient httpClient = this.httpClient;
			if (httpClient == null)
			{
				return;
			}
			httpClient.Dispose();
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00005FEF File Offset: 0x000041EF
		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			if (base.Frame.CanGoBack)
			{
				base.Frame.GoBack();
			}
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00004859 File Offset: 0x00002A59
		private void StoreLogo_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(MainPage));
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000A950 File Offset: 0x00008B50
		private void nbSearch_QuerySubmitted(object sender, SearchBoxQuerySubmittedEventArgs e)
		{
			string text = (e != null && e.QueryText != null) ? e.QueryText.Trim() : null;
			if (!string.IsNullOrEmpty(text))
			{
				base.Frame.Navigate(typeof(SearchResultsPage), text);
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00006039 File Offset: 0x00004239
		private void DownloadHubButton_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(DownloadsHub));
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00006021 File Offset: 0x00004221
		private void TopAppsButton_Click_1(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(PopularPage));
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00006009 File Offset: 0x00004209
		private void Border_Tapped_1(object sender, TappedRoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(AllAppsPage));
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000A998 File Offset: 0x00008B98
		private void SubmitReviewButton_Click(object sender, RoutedEventArgs e)
		{
			AppReviewsPage.<SubmitReviewButton_Click>d__36 <SubmitReviewButton_Click>d__;
			<SubmitReviewButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<SubmitReviewButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <SubmitReviewButton_Click>d__.<>t__builder;
			<>t__builder.Start<AppReviewsPage.<SubmitReviewButton_Click>d__36>(ref <SubmitReviewButton_Click>d__);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000A9CC File Offset: 0x00008BCC
		private void DeleteReview_Click(object sender, RoutedEventArgs e)
		{
			AppReviewsPage.<DeleteReview_Click>d__37 <DeleteReview_Click>d__;
			<DeleteReview_Click>d__.<>4__this = this;
			<DeleteReview_Click>d__.sender = sender;
			<DeleteReview_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<DeleteReview_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <DeleteReview_Click>d__.<>t__builder;
			<>t__builder.Start<AppReviewsPage.<DeleteReview_Click>d__37>(ref <DeleteReview_Click>d__);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000AA10 File Offset: 0x00008C10
		private Task ShowMessageDialog(string title, string message)
		{
			AppReviewsPage.<ShowMessageDialog>d__38 <ShowMessageDialog>d__;
			<ShowMessageDialog>d__.title = title;
			<ShowMessageDialog>d__.message = message;
			<ShowMessageDialog>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ShowMessageDialog>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <ShowMessageDialog>d__.<>t__builder;
			<>t__builder.Start<AppReviewsPage.<ShowMessageDialog>d__38>(ref <ShowMessageDialog>d__);
			return <ShowMessageDialog>d__.<>t__builder.Task;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000AA5D File Offset: 0x00008C5D
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			AddRating addRating = new AddRating();
			addRating.AppId = this.currentApp.Id;
			addRating.put_DataContext(this.currentApp);
			addRating.ReviewSubmitted += delegate(object s, EventArgs ev)
			{
				AppReviewsPage.<<Button_Click>b__39_0>d <<Button_Click>b__39_0>d;
				<<Button_Click>b__39_0>d.<>4__this = this;
				<<Button_Click>b__39_0>d.<>t__builder = AsyncVoidMethodBuilder.Create();
				<<Button_Click>b__39_0>d.<>1__state = -1;
				AsyncVoidMethodBuilder <>t__builder = <<Button_Click>b__39_0>d.<>t__builder;
				<>t__builder.Start<AppReviewsPage.<<Button_Click>b__39_0>d>(ref <<Button_Click>b__39_0>d);
			};
			addRating.Show();
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000AA98 File Offset: 0x00008C98
		private void UpdateRatingsSummary()
		{
			if (this.currentSummary != null)
			{
				this.DefaultViewModel["Summary"] = this.currentSummary;
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000AAB8 File Offset: 0x00008CB8
		private void LikeReview_Click(object sender, RoutedEventArgs e)
		{
			AppReviewsPage.<LikeReview_Click>d__41 <LikeReview_Click>d__;
			<LikeReview_Click>d__.<>4__this = this;
			<LikeReview_Click>d__.sender = sender;
			<LikeReview_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<LikeReview_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <LikeReview_Click>d__.<>t__builder;
			<>t__builder.Start<AppReviewsPage.<LikeReview_Click>d__41>(ref <LikeReview_Click>d__);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000AAFC File Offset: 0x00008CFC
		private void DislikeReview_Click(object sender, RoutedEventArgs e)
		{
			AppReviewsPage.<DislikeReview_Click>d__42 <DislikeReview_Click>d__;
			<DislikeReview_Click>d__.<>4__this = this;
			<DislikeReview_Click>d__.sender = sender;
			<DislikeReview_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<DislikeReview_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <DislikeReview_Click>d__.<>t__builder;
			<>t__builder.Start<AppReviewsPage.<DislikeReview_Click>d__42>(ref <DislikeReview_Click>d__);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000AB40 File Offset: 0x00008D40
		private void ReportButton_Click(object sender, RoutedEventArgs e)
		{
			AppReviewsPage.<ReportButton_Click>d__43 <ReportButton_Click>d__;
			<ReportButton_Click>d__.<>4__this = this;
			<ReportButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<ReportButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <ReportButton_Click>d__.<>t__builder;
			<>t__builder.Start<AppReviewsPage.<ReportButton_Click>d__43>(ref <ReportButton_Click>d__);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000AB7C File Offset: 0x00008D7C
		private void ToggleReviewExpand_Click(object sender, RoutedEventArgs e)
		{
			HyperlinkButton hyperlinkButton = sender as HyperlinkButton;
			Review review = ((hyperlinkButton != null) ? hyperlinkButton.Tag : null) as Review;
			if (review != null)
			{
				review.IsExpanded = !review.IsExpanded;
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000ABB4 File Offset: 0x00008DB4
		private Task VoteReview(int reviewId, string action)
		{
			AppReviewsPage.<VoteReview>d__45 <VoteReview>d__;
			<VoteReview>d__.<>4__this = this;
			<VoteReview>d__.reviewId = reviewId;
			<VoteReview>d__.action = action;
			<VoteReview>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<VoteReview>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <VoteReview>d__.<>t__builder;
			<>t__builder.Start<AppReviewsPage.<VoteReview>d__45>(ref <VoteReview>d__);
			return <VoteReview>d__.<>t__builder.Task;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000AC0C File Offset: 0x00008E0C
		private void ReviewsFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.currentApp == null)
			{
				return;
			}
			ComboBox comboBox = sender as ComboBox;
			if (comboBox == null)
			{
				return;
			}
			string text = (comboBox.Tag as string) ?? "";
			if (text == "sort")
			{
				this.currentSort = AppReviewsPage.MapSort(comboBox.SelectedIndex);
			}
			else if (text == "filter")
			{
				this.currentFilter = AppReviewsPage.MapFilter(comboBox.SelectedIndex);
			}
			else
			{
				text == "version";
			}
			this.currentReviews = this.ApplySortFilter(this.baseReviews);
			this.UpdateReviewSummary();
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000ACA6 File Offset: 0x00008EA6
		private static string MapSort(int index)
		{
			if (index == 1)
			{
				return "oldest";
			}
			if (index != 2)
			{
				return "newest";
			}
			return "highest";
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000ACC3 File Offset: 0x00008EC3
		private static string MapFilter(int index)
		{
			if (index == 1)
			{
				return "positive_first";
			}
			if (index != 2)
			{
				return "all";
			}
			return "negative_first";
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000ACE0 File Offset: 0x00008EE0
		private void ReviewsListView_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			ListView listView = sender as ListView;
			if (listView == null)
			{
				return;
			}
			ItemsWrapGrid itemsWrapGrid = listView.ItemsPanelRoot as ItemsWrapGrid;
			if (itemsWrapGrid != null)
			{
				double num = (e.NewSize.Width - 50.0) / 2.0;
				itemsWrapGrid.put_ItemWidth(num);
			}
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000AD34 File Offset: 0x00008F34
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Application.LoadComponent(this, new Uri("ms-appx:///AppReviewsPage.xaml"), 0);
			this.pageRoot = (Page)base.FindName("pageRoot");
			this.MainHub = (Hub)base.FindName("MainHub");
			this.backButton = (Button)base.FindName("backButton");
			this.resultText = (TextBlock)base.FindName("resultText");
			this.nbSearch = (SearchBox)base.FindName("nbSearch");
			this.nbLogoText = (TextBlock)base.FindName("nbLogoText");
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000ADE8 File Offset: 0x00008FE8
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.DeleteReview_Click));
				break;
			}
			case 2:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.ReportButton_Click));
				break;
			}
			case 3:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.ToggleReviewExpand_Click));
				break;
			}
			case 4:
			{
				Selector selector = (Selector)target;
				WindowsRuntimeMarshal.AddEventHandler<SelectionChangedEventHandler>(new Func<SelectionChangedEventHandler, EventRegistrationToken>(selector.add_SelectionChanged), new Action<EventRegistrationToken>(selector.remove_SelectionChanged), new SelectionChangedEventHandler(this.ReviewsFilter_SelectionChanged));
				break;
			}
			case 5:
			{
				Selector selector = (Selector)target;
				WindowsRuntimeMarshal.AddEventHandler<SelectionChangedEventHandler>(new Func<SelectionChangedEventHandler, EventRegistrationToken>(selector.add_SelectionChanged), new Action<EventRegistrationToken>(selector.remove_SelectionChanged), new SelectionChangedEventHandler(this.ReviewsFilter_SelectionChanged));
				break;
			}
			case 6:
			{
				Selector selector = (Selector)target;
				WindowsRuntimeMarshal.AddEventHandler<SelectionChangedEventHandler>(new Func<SelectionChangedEventHandler, EventRegistrationToken>(selector.add_SelectionChanged), new Action<EventRegistrationToken>(selector.remove_SelectionChanged), new SelectionChangedEventHandler(this.ReviewsFilter_SelectionChanged));
				break;
			}
			case 7:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.Button_Click));
				break;
			}
			case 8:
			{
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uielement.add_Tapped), new Action<EventRegistrationToken>(uielement.remove_Tapped), new TappedEventHandler(this.StoreLogo_Click));
				break;
			}
			case 9:
			{
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uielement.add_Tapped), new Action<EventRegistrationToken>(uielement.remove_Tapped), new TappedEventHandler(this.Border_Tapped_1));
				break;
			}
			case 10:
			{
				SearchBox searchBox = (SearchBox)target;
				WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<SearchBox, SearchBoxQuerySubmittedEventArgs>>(new Func<TypedEventHandler<SearchBox, SearchBoxQuerySubmittedEventArgs>, EventRegistrationToken>(searchBox.add_QuerySubmitted), new Action<EventRegistrationToken>(searchBox.remove_QuerySubmitted), new TypedEventHandler<SearchBox, SearchBoxQuerySubmittedEventArgs>(this.nbSearch_QuerySubmitted));
				break;
			}
			}
			this._contentLoaded = true;
		}

		// Token: 0x040000C8 RID: 200
		private NavigationHelper navigationHelper;

		// Token: 0x040000C9 RID: 201
		private ObservableDictionary defaultViewModel = new ObservableDictionary();

		// Token: 0x040000CA RID: 202
		private StoreApp currentApp;

		// Token: 0x040000CB RID: 203
		private ReviewStats currentSummary;

		// Token: 0x040000CC RID: 204
		private List<Review> currentReviews = new List<Review>();

		// Token: 0x040000CD RID: 205
		private List<Review> baseReviews = new List<Review>();

		// Token: 0x040000CE RID: 206
		private UserAccount currentUser;

		// Token: 0x040000CF RID: 207
		private HttpClient httpClient;

		// Token: 0x040000D0 RID: 208
		private string currentSort = "newest";

		// Token: 0x040000D1 RID: 209
		private string currentFilter = "all";

		// Token: 0x040000D2 RID: 210
		private string lastAppId;

		// Token: 0x040000D3 RID: 211
		private bool isLoading;

		// Token: 0x040000D4 RID: 212
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Page pageRoot;

		// Token: 0x040000D5 RID: 213
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Hub MainHub;

		// Token: 0x040000D6 RID: 214
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button backButton;

		// Token: 0x040000D7 RID: 215
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock resultText;

		// Token: 0x040000D8 RID: 216
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private SearchBox nbSearch;

		// Token: 0x040000D9 RID: 217
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock nbLogoText;

		// Token: 0x040000DA RID: 218
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
