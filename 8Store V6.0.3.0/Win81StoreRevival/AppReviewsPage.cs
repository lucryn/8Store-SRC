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
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Win81StoreRevival
{
	// Token: 0x0200001F RID: 31
	public sealed class AppReviewsPage : Page, IComponentConnector
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600018A RID: 394 RVA: 0x0000967C File Offset: 0x0000787C
		// (set) Token: 0x0600018B RID: 395 RVA: 0x00009684 File Offset: 0x00007884
		public bool IsUserLoggedIn { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00009690 File Offset: 0x00007890
		public NavigationHelper NavigationHelper
		{
			get
			{
				return this.navigationHelper;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600018D RID: 397 RVA: 0x000096A8 File Offset: 0x000078A8
		public ObservableDictionary DefaultViewModel
		{
			get
			{
				return this.defaultViewModel;
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000096C0 File Offset: 0x000078C0
		public AppReviewsPage()
		{
			this.InitializeComponent();
			this.navigationHelper = new NavigationHelper(this);
			this.navigationHelper.LoadState += this.navigationHelper_LoadState;
			this.httpClient = new HttpClient();
			this.httpClient.Timeout = TimeSpan.FromSeconds(30.0);
			this.IsUserLoggedIn = SupabaseService.IsUserLoggedIn();
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00009768 File Offset: 0x00007968
		private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
		{
			bool flag = e.PageState != null && e.PageState.ContainsKey("SelectedItem");
			if (flag)
			{
				object obj = e.PageState["SelectedItem"];
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000097A8 File Offset: 0x000079A8
		[DebuggerStepThrough]
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			AppReviewsPage.<OnNavigatedTo>d__22 <OnNavigatedTo>d__ = new AppReviewsPage.<OnNavigatedTo>d__22();
			<OnNavigatedTo>d__.<>4__this = this;
			<OnNavigatedTo>d__.e = e;
			<OnNavigatedTo>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<OnNavigatedTo>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <OnNavigatedTo>d__.<>t__builder;
			<>t__builder.Start<AppReviewsPage.<OnNavigatedTo>d__22>(ref <OnNavigatedTo>d__);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000097EC File Offset: 0x000079EC
		[DebuggerStepThrough]
		private Task LoadReviewsForApp(string appId, bool forceReload)
		{
			AppReviewsPage.<LoadReviewsForApp>d__23 <LoadReviewsForApp>d__ = new AppReviewsPage.<LoadReviewsForApp>d__23();
			<LoadReviewsForApp>d__.<>4__this = this;
			<LoadReviewsForApp>d__.appId = appId;
			<LoadReviewsForApp>d__.forceReload = forceReload;
			<LoadReviewsForApp>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadReviewsForApp>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadReviewsForApp>d__.<>t__builder;
			<>t__builder.Start<AppReviewsPage.<LoadReviewsForApp>d__23>(ref <LoadReviewsForApp>d__);
			return <LoadReviewsForApp>d__.<>t__builder.Task;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00009844 File Offset: 0x00007A44
		private List<Review> ApplySortFilter(List<Review> reviews)
		{
			bool flag = reviews == null || reviews.Count == 0;
			List<Review> result;
			if (flag)
			{
				result = (reviews ?? new List<Review>());
			}
			else
			{
				IOrderedEnumerable<Review> orderedEnumerable = null;
				bool flag2 = this.currentFilter == "positive_first";
				if (flag2)
				{
					orderedEnumerable = Enumerable.OrderByDescending<Review, int>(reviews, (Review r) => r.Rating);
				}
				else
				{
					bool flag3 = this.currentFilter == "negative_first";
					if (flag3)
					{
						orderedEnumerable = Enumerable.OrderBy<Review, int>(reviews, (Review r) => r.Rating);
					}
				}
				bool flag4 = orderedEnumerable != null;
				if (flag4)
				{
					bool flag5 = this.currentSort == "oldest";
					if (flag5)
					{
						orderedEnumerable = Enumerable.ThenBy<Review, DateTime>(orderedEnumerable, (Review r) => r.CreatedAt);
					}
					else
					{
						orderedEnumerable = Enumerable.ThenByDescending<Review, DateTime>(orderedEnumerable, (Review r) => r.CreatedAt);
					}
					result = Enumerable.ToList<Review>(orderedEnumerable);
				}
				else
				{
					string text = this.currentSort;
					if (!(text == "oldest"))
					{
						if (!(text == "highest"))
						{
							result = Enumerable.ToList<Review>(Enumerable.OrderByDescending<Review, DateTime>(reviews, (Review r) => r.CreatedAt));
						}
						else
						{
							result = Enumerable.ToList<Review>(Enumerable.ThenByDescending<Review, DateTime>(Enumerable.OrderByDescending<Review, int>(reviews, (Review r) => r.Rating), (Review r) => r.CreatedAt));
						}
					}
					else
					{
						result = Enumerable.ToList<Review>(Enumerable.OrderBy<Review, DateTime>(reviews, (Review r) => r.CreatedAt));
					}
				}
			}
			return result;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00009A40 File Offset: 0x00007C40
		private void UpdateReviewSummary()
		{
			string text = null;
			UserAccount userAccount = SupabaseService.GetCurrentUser();
			bool flag = userAccount != null;
			if (flag)
			{
				text = userAccount.Id;
			}
			bool flag2 = this.currentReviews != null && Enumerable.Any<Review>(this.currentReviews);
			if (flag2)
			{
				double averageRating = Enumerable.Average<Review>(this.currentReviews, (Review r) => r.Rating);
				int count = this.currentReviews.Count;
				int star = Enumerable.Count<Review>(Enumerable.Where<Review>(this.currentReviews, (Review r) => r.Rating == 1));
				int star2 = Enumerable.Count<Review>(Enumerable.Where<Review>(this.currentReviews, (Review r) => r.Rating == 2));
				int star3 = Enumerable.Count<Review>(Enumerable.Where<Review>(this.currentReviews, (Review r) => r.Rating == 3));
				int star4 = Enumerable.Count<Review>(Enumerable.Where<Review>(this.currentReviews, (Review r) => r.Rating == 4));
				int star5 = Enumerable.Count<Review>(Enumerable.Where<Review>(this.currentReviews, (Review r) => r.Rating == 5));
				this.currentSummary = new RatingSummary
				{
					AverageRating = averageRating,
					ReviewCount = count,
					Star1 = star,
					Star2 = star2,
					Star3 = star3,
					Star4 = star4,
					Star5 = star5
				};
				foreach (Review review in this.currentReviews)
				{
					review.IsOwnReview = (!string.IsNullOrEmpty(text) && review.UserId == text);
				}
				bool flag3 = count == 1;
				if (flag3)
				{
					this.resultText.put_Text("1 review");
				}
				else
				{
					this.resultText.put_Text(string.Format("{0} reviews", new object[]
					{
						count
					}));
				}
				this.DefaultViewModel["Reviews"] = this.currentReviews;
				this.DefaultViewModel["Summary"] = this.currentSummary;
				this.UpdateStarClipping(averageRating);
			}
			else
			{
				this.resultText.put_Text("0 reviews");
				this.DefaultViewModel["Reviews"] = new List<Review>();
				this.DefaultViewModel["Summary"] = new RatingSummary
				{
					AverageRating = 0.0,
					ReviewCount = 0
				};
				this.UpdateStarClipping(0.0);
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00009D4C File Offset: 0x00007F4C
		private void UpdateStarClipping(double averageRating)
		{
			try
			{
				TextBlock textBlock = AppReviewsPage.FindVisualChild<TextBlock>(this, "GreenStarsLayer");
				bool flag = textBlock != null && textBlock.Clip != null;
				if (flag)
				{
					RectangleGeometry clip = textBlock.Clip;
					double num = textBlock.ActualWidth;
					bool flag2 = num <= 0.0;
					if (flag2)
					{
						num = 274.0;
					}
					double num2 = averageRating / 5.0 * num;
					clip.put_Rect(new Rect(0.0, 0.0, num2, 100.0));
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("[UPDATE STAR CLIPPING ERROR] " + ex.Message);
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00009E14 File Offset: 0x00008014
		private static T FindVisualChild<T>(DependencyObject obj, string name) where T : FrameworkElement
		{
			bool flag = obj == null;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
				{
					DependencyObject child = VisualTreeHelper.GetChild(obj, i);
					bool flag2 = child is T && (child as FrameworkElement).Name == name;
					if (flag2)
					{
						return child as T;
					}
					T t = AppReviewsPage.FindVisualChild<T>(child, name);
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

		// Token: 0x06000196 RID: 406 RVA: 0x00009EBA File Offset: 0x000080BA
		protected override void OnNavigatedFrom(NavigationEventArgs e)
		{
			this.navigationHelper.OnNavigatedFrom(e);
			HttpClient httpClient = this.httpClient;
			if (httpClient != null)
			{
				httpClient.Dispose();
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00009EDC File Offset: 0x000080DC
		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			bool canGoBack = base.Frame.CanGoBack;
			if (canGoBack)
			{
				base.Frame.GoBack();
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00009F07 File Offset: 0x00008107
		private void CollectionsButton_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(Dependencies));
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00005899 File Offset: 0x00003A99
		private void DownloadHubButton_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(DownloadsHub));
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000058B2 File Offset: 0x00003AB2
		private void ExclusiveHubButton_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(AccountPage));
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00005880 File Offset: 0x00003A80
		private void TopAppsButton_Click_1(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(PopularPage));
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00005867 File Offset: 0x00003A67
		private void Border_Tapped_1(object sender, TappedRoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(AllAppsPage));
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00009F20 File Offset: 0x00008120
		[DebuggerStepThrough]
		private void SubmitReviewButton_Click(object sender, RoutedEventArgs e)
		{
			AppReviewsPage.<SubmitReviewButton_Click>d__35 <SubmitReviewButton_Click>d__ = new AppReviewsPage.<SubmitReviewButton_Click>d__35();
			<SubmitReviewButton_Click>d__.<>4__this = this;
			<SubmitReviewButton_Click>d__.sender = sender;
			<SubmitReviewButton_Click>d__.e = e;
			<SubmitReviewButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<SubmitReviewButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <SubmitReviewButton_Click>d__.<>t__builder;
			<>t__builder.Start<AppReviewsPage.<SubmitReviewButton_Click>d__35>(ref <SubmitReviewButton_Click>d__);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00009F6C File Offset: 0x0000816C
		[DebuggerStepThrough]
		private void DeleteReview_Click(object sender, RoutedEventArgs e)
		{
			AppReviewsPage.<DeleteReview_Click>d__36 <DeleteReview_Click>d__ = new AppReviewsPage.<DeleteReview_Click>d__36();
			<DeleteReview_Click>d__.<>4__this = this;
			<DeleteReview_Click>d__.sender = sender;
			<DeleteReview_Click>d__.e = e;
			<DeleteReview_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<DeleteReview_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <DeleteReview_Click>d__.<>t__builder;
			<>t__builder.Start<AppReviewsPage.<DeleteReview_Click>d__36>(ref <DeleteReview_Click>d__);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00009FB8 File Offset: 0x000081B8
		[DebuggerStepThrough]
		private Task ShowMessageDialog(string title, string message)
		{
			AppReviewsPage.<ShowMessageDialog>d__37 <ShowMessageDialog>d__ = new AppReviewsPage.<ShowMessageDialog>d__37();
			<ShowMessageDialog>d__.<>4__this = this;
			<ShowMessageDialog>d__.title = title;
			<ShowMessageDialog>d__.message = message;
			<ShowMessageDialog>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ShowMessageDialog>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <ShowMessageDialog>d__.<>t__builder;
			<>t__builder.Start<AppReviewsPage.<ShowMessageDialog>d__37>(ref <ShowMessageDialog>d__);
			return <ShowMessageDialog>d__.<>t__builder.Task;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000A010 File Offset: 0x00008210
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			AppReviewsPage.<>c__DisplayClass38_0 CS$<>8__locals1 = new AppReviewsPage.<>c__DisplayClass38_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.flyout = new AddRating();
			CS$<>8__locals1.flyout.AppId = this.currentApp.Id;
			CS$<>8__locals1.flyout.put_DataContext(this.currentApp);
			CS$<>8__locals1.flyout.ReviewSubmitted += delegate(object s, EventArgs ev)
			{
				AppReviewsPage.<>c__DisplayClass38_0.<<Button_Click>b__0>d <<Button_Click>b__0>d = new AppReviewsPage.<>c__DisplayClass38_0.<<Button_Click>b__0>d();
				<<Button_Click>b__0>d.<>4__this = CS$<>8__locals1;
				<<Button_Click>b__0>d.s = s;
				<<Button_Click>b__0>d.ev = ev;
				<<Button_Click>b__0>d.<>t__builder = AsyncVoidMethodBuilder.Create();
				<<Button_Click>b__0>d.<>1__state = -1;
				AsyncVoidMethodBuilder <>t__builder = <<Button_Click>b__0>d.<>t__builder;
				<>t__builder.Start<AppReviewsPage.<>c__DisplayClass38_0.<<Button_Click>b__0>d>(ref <<Button_Click>b__0>d);
			};
			CS$<>8__locals1.flyout.Show();
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000A084 File Offset: 0x00008284
		private void UpdateRatingsSummary()
		{
			bool flag = this.currentSummary != null;
			if (flag)
			{
				this.DefaultViewModel["Summary"] = this.currentSummary;
			}
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000A0B8 File Offset: 0x000082B8
		[DebuggerStepThrough]
		private void LikeReview_Click(object sender, RoutedEventArgs e)
		{
			AppReviewsPage.<LikeReview_Click>d__40 <LikeReview_Click>d__ = new AppReviewsPage.<LikeReview_Click>d__40();
			<LikeReview_Click>d__.<>4__this = this;
			<LikeReview_Click>d__.sender = sender;
			<LikeReview_Click>d__.e = e;
			<LikeReview_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<LikeReview_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <LikeReview_Click>d__.<>t__builder;
			<>t__builder.Start<AppReviewsPage.<LikeReview_Click>d__40>(ref <LikeReview_Click>d__);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000A104 File Offset: 0x00008304
		[DebuggerStepThrough]
		private void DislikeReview_Click(object sender, RoutedEventArgs e)
		{
			AppReviewsPage.<DislikeReview_Click>d__41 <DislikeReview_Click>d__ = new AppReviewsPage.<DislikeReview_Click>d__41();
			<DislikeReview_Click>d__.<>4__this = this;
			<DislikeReview_Click>d__.sender = sender;
			<DislikeReview_Click>d__.e = e;
			<DislikeReview_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<DislikeReview_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <DislikeReview_Click>d__.<>t__builder;
			<>t__builder.Start<AppReviewsPage.<DislikeReview_Click>d__41>(ref <DislikeReview_Click>d__);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000A150 File Offset: 0x00008350
		[DebuggerStepThrough]
		private void ReportButton_Click(object sender, RoutedEventArgs e)
		{
			AppReviewsPage.<ReportButton_Click>d__42 <ReportButton_Click>d__ = new AppReviewsPage.<ReportButton_Click>d__42();
			<ReportButton_Click>d__.<>4__this = this;
			<ReportButton_Click>d__.sender = sender;
			<ReportButton_Click>d__.e = e;
			<ReportButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<ReportButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <ReportButton_Click>d__.<>t__builder;
			<>t__builder.Start<AppReviewsPage.<ReportButton_Click>d__42>(ref <ReportButton_Click>d__);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000A19C File Offset: 0x0000839C
		[DebuggerStepThrough]
		private Task VoteReview(int reviewId, string action)
		{
			AppReviewsPage.<VoteReview>d__43 <VoteReview>d__ = new AppReviewsPage.<VoteReview>d__43();
			<VoteReview>d__.<>4__this = this;
			<VoteReview>d__.reviewId = reviewId;
			<VoteReview>d__.action = action;
			<VoteReview>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<VoteReview>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <VoteReview>d__.<>t__builder;
			<>t__builder.Start<AppReviewsPage.<VoteReview>d__43>(ref <VoteReview>d__);
			return <VoteReview>d__.<>t__builder.Task;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000A1F4 File Offset: 0x000083F4
		private void ToggleReviewExpand_Click(object sender, RoutedEventArgs e)
		{
			FrameworkElement frameworkElement = sender as FrameworkElement;
			bool flag = frameworkElement == null;
			if (!flag)
			{
				Review review = frameworkElement.Tag as Review;
				bool flag2 = review == null;
				if (!flag2)
				{
					ScrollViewer scrollViewer = new ScrollViewer();
					scrollViewer.put_VerticalScrollBarVisibility(1);
					scrollViewer.put_HorizontalScrollBarVisibility(0);
					scrollViewer.put_MaxHeight(300.0);
					scrollViewer.put_Width(300.0);
					ScrollViewer scrollViewer2 = scrollViewer;
					ContentControl contentControl = scrollViewer2;
					TextBlock textBlock = new TextBlock();
					textBlock.put_Text(review.Comment ?? "");
					textBlock.put_TextWrapping(2);
					textBlock.put_FontSize(14.0);
					contentControl.put_Content(textBlock);
					Flyout flyout = new Flyout();
					flyout.put_Content(scrollViewer2);
					flyout.ShowAt(frameworkElement);
				}
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000A2BC File Offset: 0x000084BC
		private void ReviewsFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			bool flag = this.currentApp == null;
			if (!flag)
			{
				ComboBox comboBox = sender as ComboBox;
				bool flag2 = comboBox == null;
				if (!flag2)
				{
					string text = (comboBox.Tag as string) ?? "";
					bool flag3 = text == "sort";
					if (flag3)
					{
						this.currentSort = AppReviewsPage.MapSort(comboBox.SelectedIndex);
					}
					else
					{
						bool flag4 = text == "filter";
						if (flag4)
						{
							this.currentFilter = AppReviewsPage.MapFilter(comboBox.SelectedIndex);
						}
						else
						{
							bool flag5 = text == "version";
							if (flag5)
							{
							}
						}
					}
					this.currentReviews = this.ApplySortFilter(this.baseReviews);
					this.UpdateReviewSummary();
				}
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000A380 File Offset: 0x00008580
		private static string MapSort(int index)
		{
			string result;
			if (index != 1)
			{
				if (index != 2)
				{
					result = "newest";
				}
				else
				{
					result = "highest";
				}
			}
			else
			{
				result = "oldest";
			}
			return result;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000A3B8 File Offset: 0x000085B8
		private static string MapFilter(int index)
		{
			string result;
			if (index != 1)
			{
				if (index != 2)
				{
					result = "all";
				}
				else
				{
					result = "negative_first";
				}
			}
			else
			{
				result = "positive_first";
			}
			return result;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000A3F0 File Offset: 0x000085F0
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			bool contentLoaded = this._contentLoaded;
			if (!contentLoaded)
			{
				this._contentLoaded = true;
				Application.LoadComponent(this, new Uri("ms-appx:///AppReviewsPage.xaml"), 0);
				this.pageRoot = (Page)base.FindName("pageRoot");
				this.MainHub = (Hub)base.FindName("MainHub");
				this.backButton = (Button)base.FindName("backButton");
				this.resultText = (TextBlock)base.FindName("resultText");
			}
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000A47C File Offset: 0x0000867C
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
				WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uielement.add_Tapped), new Action<EventRegistrationToken>(uielement.remove_Tapped), new TappedEventHandler(this.Border_Tapped_1));
				break;
			}
			}
			this._contentLoaded = true;
		}

		// Token: 0x040000C1 RID: 193
		private NavigationHelper navigationHelper;

		// Token: 0x040000C2 RID: 194
		private ObservableDictionary defaultViewModel = new ObservableDictionary();

		// Token: 0x040000C3 RID: 195
		private StoreApp currentApp;

		// Token: 0x040000C4 RID: 196
		private RatingSummary currentSummary;

		// Token: 0x040000C5 RID: 197
		private List<Review> currentReviews = new List<Review>();

		// Token: 0x040000C6 RID: 198
		private List<Review> baseReviews = new List<Review>();

		// Token: 0x040000C7 RID: 199
		private UserAccount currentUser;

		// Token: 0x040000C8 RID: 200
		private HttpClient httpClient;

		// Token: 0x040000C9 RID: 201
		private string currentSort = "newest";

		// Token: 0x040000CA RID: 202
		private string currentFilter = "all";

		// Token: 0x040000CB RID: 203
		private string lastAppId;

		// Token: 0x040000CC RID: 204
		private bool isLoading;

		// Token: 0x040000CD RID: 205
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Page pageRoot;

		// Token: 0x040000CE RID: 206
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Hub MainHub;

		// Token: 0x040000CF RID: 207
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button backButton;

		// Token: 0x040000D0 RID: 208
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock resultText;

		// Token: 0x040000D1 RID: 209
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
