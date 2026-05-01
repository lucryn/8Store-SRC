using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Win81StoreRevival.Account;
using Windows.ApplicationModel.Resources;
using Windows.Storage;
using Windows.UI;
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
	// Token: 0x02000018 RID: 24
	public sealed class AccountPage : Page, IComponentConnector
	{
		// Token: 0x060000FC RID: 252 RVA: 0x00004495 File Offset: 0x00002695
		public AccountPage()
		{
			this.InitializeComponent();
			this.UpdateTitleNB();
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000044AC File Offset: 0x000026AC
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			AccountPage.<OnNavigatedTo>d__1 <OnNavigatedTo>d__;
			<OnNavigatedTo>d__.<>4__this = this;
			<OnNavigatedTo>d__.e = e;
			<OnNavigatedTo>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<OnNavigatedTo>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <OnNavigatedTo>d__.<>t__builder;
			<>t__builder.Start<AccountPage.<OnNavigatedTo>d__1>(ref <OnNavigatedTo>d__);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000044F0 File Offset: 0x000026F0
		private void MainHub_Loaded(object sender, RoutedEventArgs e)
		{
			AccountPage.<MainHub_Loaded>d__2 <MainHub_Loaded>d__;
			<MainHub_Loaded>d__.<>4__this = this;
			<MainHub_Loaded>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<MainHub_Loaded>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <MainHub_Loaded>d__.<>t__builder;
			<>t__builder.Start<AccountPage.<MainHub_Loaded>d__2>(ref <MainHub_Loaded>d__);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000452C File Offset: 0x0000272C
		private void OpenLoginPopup()
		{
			try
			{
				AccountPage.<>c__DisplayClass3_0 CS$<>8__locals1 = new AccountPage.<>c__DisplayClass3_0();
				CS$<>8__locals1.<>4__this = this;
				Login login = new Login();
				CS$<>8__locals1.popup = new Popup();
				login.put_Width(Window.Current.Bounds.Width);
				login.put_Height(Window.Current.Bounds.Height);
				CS$<>8__locals1.popup.put_Child(login);
				login.RegisterRequested += delegate(object s, EventArgs args)
				{
					Register register = new Register();
					register.put_Width(Window.Current.Bounds.Width);
					register.put_Height(Window.Current.Bounds.Height);
					Register register2 = register;
					EventHandler value;
					if ((value = CS$<>8__locals1.<>9__1) == null)
					{
						value = (CS$<>8__locals1.<>9__1 = delegate(object rs, EventArgs rargs)
						{
							AccountPage.<>c__DisplayClass3_0.<<OpenLoginPopup>b__1>d <<OpenLoginPopup>b__1>d;
							<<OpenLoginPopup>b__1>d.<>4__this = CS$<>8__locals1;
							<<OpenLoginPopup>b__1>d.<>t__builder = AsyncVoidMethodBuilder.Create();
							<<OpenLoginPopup>b__1>d.<>1__state = -1;
							AsyncVoidMethodBuilder <>t__builder = <<OpenLoginPopup>b__1>d.<>t__builder;
							<>t__builder.Start<AccountPage.<>c__DisplayClass3_0.<<OpenLoginPopup>b__1>d>(ref <<OpenLoginPopup>b__1>d);
						});
					}
					register2.CloseRequested += value;
					Register register3 = register;
					EventHandler value2;
					if ((value2 = CS$<>8__locals1.<>9__2) == null)
					{
						value2 = (CS$<>8__locals1.<>9__2 = delegate(object rs, EventArgs rargs)
						{
							Login login2 = new Login();
							login2.put_Width(Window.Current.Bounds.Width);
							login2.put_Height(Window.Current.Bounds.Height);
							CS$<>8__locals1.<>4__this.AttachLoginPopupHandlers(CS$<>8__locals1.popup, login2);
							CS$<>8__locals1.popup.put_Child(login2);
						});
					}
					register3.LoginRequested += value2;
					CS$<>8__locals1.popup.put_Child(register);
				};
				login.DiscordRequested += delegate(object s, EventArgs args)
				{
					CS$<>8__locals1.<>4__this.ShowDiscordPopup(CS$<>8__locals1.popup);
				};
				login.CloseRequested += delegate(object s, EventArgs args)
				{
					AccountPage.<>c__DisplayClass3_0.<<OpenLoginPopup>b__4>d <<OpenLoginPopup>b__4>d;
					<<OpenLoginPopup>b__4>d.<>4__this = CS$<>8__locals1;
					<<OpenLoginPopup>b__4>d.<>t__builder = AsyncVoidMethodBuilder.Create();
					<<OpenLoginPopup>b__4>d.<>1__state = -1;
					AsyncVoidMethodBuilder <>t__builder = <<OpenLoginPopup>b__4>d.<>t__builder;
					<>t__builder.Start<AccountPage.<>c__DisplayClass3_0.<<OpenLoginPopup>b__4>d>(ref <<OpenLoginPopup>b__4>d);
				};
				CS$<>8__locals1.popup.put_IsOpen(true);
			}
			catch (Exception)
			{
				base.Frame.GoBack();
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000045F8 File Offset: 0x000027F8
		private void AttachLoginPopupHandlers(Popup popup, Login loginUI)
		{
			AccountPage.<>c__DisplayClass4_0 CS$<>8__locals1 = new AccountPage.<>c__DisplayClass4_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.popup = popup;
			loginUI.CloseRequested += delegate(object s, EventArgs args)
			{
				AccountPage.<>c__DisplayClass4_0.<<AttachLoginPopupHandlers>b__0>d <<AttachLoginPopupHandlers>b__0>d;
				<<AttachLoginPopupHandlers>b__0>d.<>4__this = CS$<>8__locals1;
				<<AttachLoginPopupHandlers>b__0>d.<>t__builder = AsyncVoidMethodBuilder.Create();
				<<AttachLoginPopupHandlers>b__0>d.<>1__state = -1;
				AsyncVoidMethodBuilder <>t__builder = <<AttachLoginPopupHandlers>b__0>d.<>t__builder;
				<>t__builder.Start<AccountPage.<>c__DisplayClass4_0.<<AttachLoginPopupHandlers>b__0>d>(ref <<AttachLoginPopupHandlers>b__0>d);
			};
			loginUI.RegisterRequested += delegate(object s, EventArgs args)
			{
				Register register = new Register();
				register.put_Width(Window.Current.Bounds.Width);
				register.put_Height(Window.Current.Bounds.Height);
				Register register2 = register;
				EventHandler value;
				if ((value = CS$<>8__locals1.<>9__2) == null)
				{
					value = (CS$<>8__locals1.<>9__2 = delegate(object rs, EventArgs rargs)
					{
						AccountPage.<>c__DisplayClass4_0.<<AttachLoginPopupHandlers>b__2>d <<AttachLoginPopupHandlers>b__2>d;
						<<AttachLoginPopupHandlers>b__2>d.<>4__this = CS$<>8__locals1;
						<<AttachLoginPopupHandlers>b__2>d.<>t__builder = AsyncVoidMethodBuilder.Create();
						<<AttachLoginPopupHandlers>b__2>d.<>1__state = -1;
						AsyncVoidMethodBuilder <>t__builder = <<AttachLoginPopupHandlers>b__2>d.<>t__builder;
						<>t__builder.Start<AccountPage.<>c__DisplayClass4_0.<<AttachLoginPopupHandlers>b__2>d>(ref <<AttachLoginPopupHandlers>b__2>d);
					});
				}
				register2.CloseRequested += value;
				Register register3 = register;
				EventHandler value2;
				if ((value2 = CS$<>8__locals1.<>9__3) == null)
				{
					value2 = (CS$<>8__locals1.<>9__3 = delegate(object rs, EventArgs rargs)
					{
						Login login = new Login();
						login.put_Width(Window.Current.Bounds.Width);
						login.put_Height(Window.Current.Bounds.Height);
						CS$<>8__locals1.<>4__this.AttachLoginPopupHandlers(CS$<>8__locals1.popup, login);
						CS$<>8__locals1.popup.put_Child(login);
					});
				}
				register3.LoginRequested += value2;
				CS$<>8__locals1.popup.put_Child(register);
			};
			loginUI.DiscordRequested += delegate(object s, EventArgs args)
			{
				CS$<>8__locals1.<>4__this.ShowDiscordPopup(CS$<>8__locals1.popup);
			};
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004650 File Offset: 0x00002850
		private void ShowDiscordPopup(Popup popup)
		{
			AccountPage.<>c__DisplayClass5_0 CS$<>8__locals1 = new AccountPage.<>c__DisplayClass5_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.popup = popup;
			Discord discord = new Discord();
			discord.put_Width(Window.Current.Bounds.Width);
			discord.put_Height(Window.Current.Bounds.Height);
			discord.CloseRequested += delegate(object s, EventArgs args)
			{
				AccountPage.<>c__DisplayClass5_0.<<ShowDiscordPopup>b__0>d <<ShowDiscordPopup>b__0>d;
				<<ShowDiscordPopup>b__0>d.<>4__this = CS$<>8__locals1;
				<<ShowDiscordPopup>b__0>d.<>t__builder = AsyncVoidMethodBuilder.Create();
				<<ShowDiscordPopup>b__0>d.<>1__state = -1;
				AsyncVoidMethodBuilder <>t__builder = <<ShowDiscordPopup>b__0>d.<>t__builder;
				<>t__builder.Start<AccountPage.<>c__DisplayClass5_0.<<ShowDiscordPopup>b__0>d>(ref <<ShowDiscordPopup>b__0>d);
			};
			discord.LoginRequested += delegate(object s, EventArgs args)
			{
				Login login = new Login();
				login.put_Width(Window.Current.Bounds.Width);
				login.put_Height(Window.Current.Bounds.Height);
				CS$<>8__locals1.<>4__this.AttachLoginPopupHandlers(CS$<>8__locals1.popup, login);
				CS$<>8__locals1.popup.put_Child(login);
			};
			CS$<>8__locals1.popup.put_Child(discord);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000046D8 File Offset: 0x000028D8
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

		// Token: 0x06000103 RID: 259 RVA: 0x0000473C File Offset: 0x0000293C
		public Task UpdateAccountUIAsync()
		{
			AccountPage.<UpdateAccountUIAsync>d__7 <UpdateAccountUIAsync>d__;
			<UpdateAccountUIAsync>d__.<>4__this = this;
			<UpdateAccountUIAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateAccountUIAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <UpdateAccountUIAsync>d__.<>t__builder;
			<>t__builder.Start<AccountPage.<UpdateAccountUIAsync>d__7>(ref <UpdateAccountUIAsync>d__);
			return <UpdateAccountUIAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004784 File Offset: 0x00002984
		private Task RefreshPersonalSectionsAsync()
		{
			AccountPage.<RefreshPersonalSectionsAsync>d__8 <RefreshPersonalSectionsAsync>d__;
			<RefreshPersonalSectionsAsync>d__.<>4__this = this;
			<RefreshPersonalSectionsAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<RefreshPersonalSectionsAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <RefreshPersonalSectionsAsync>d__.<>t__builder;
			<>t__builder.Start<AccountPage.<RefreshPersonalSectionsAsync>d__8>(ref <RefreshPersonalSectionsAsync>d__);
			return <RefreshPersonalSectionsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000047CC File Offset: 0x000029CC
		private Task LoadMyAppsAsync()
		{
			AccountPage.<LoadMyAppsAsync>d__9 <LoadMyAppsAsync>d__;
			<LoadMyAppsAsync>d__.<>4__this = this;
			<LoadMyAppsAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadMyAppsAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadMyAppsAsync>d__.<>t__builder;
			<>t__builder.Start<AccountPage.<LoadMyAppsAsync>d__9>(ref <LoadMyAppsAsync>d__);
			return <LoadMyAppsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004814 File Offset: 0x00002A14
		private Task LoadLatestReviewsAsync()
		{
			AccountPage.<LoadLatestReviewsAsync>d__10 <LoadLatestReviewsAsync>d__;
			<LoadLatestReviewsAsync>d__.<>4__this = this;
			<LoadLatestReviewsAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadLatestReviewsAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadLatestReviewsAsync>d__.<>t__builder;
			<>t__builder.Start<AccountPage.<LoadLatestReviewsAsync>d__10>(ref <LoadLatestReviewsAsync>d__);
			return <LoadLatestReviewsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004859 File Offset: 0x00002A59
		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(MainPage));
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004871 File Offset: 0x00002A71
		private void StoreLogo_Click(object sender, RoutedEventArgs e)
		{
			if (base.Frame != null)
			{
				base.Frame.Navigate(typeof(MainPage));
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004891 File Offset: 0x00002A91
		private void DownloadHubButton_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(AllAppsPage1));
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000048A9 File Offset: 0x00002AA9
		private void CollectionsButton_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(CollectionsViewPage), "Staff picks");
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004891 File Offset: 0x00002A91
		private void Border_Tapped_1(object sender, TappedRoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(AllAppsPage1));
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000048C8 File Offset: 0x00002AC8
		private void AccountLogoutButton_Click(object sender, RoutedEventArgs e)
		{
			AccountPage.<AccountLogoutButton_Click>d__16 <AccountLogoutButton_Click>d__;
			<AccountLogoutButton_Click>d__.<>4__this = this;
			<AccountLogoutButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<AccountLogoutButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <AccountLogoutButton_Click>d__.<>t__builder;
			<>t__builder.Start<AccountPage.<AccountLogoutButton_Click>d__16>(ref <AccountLogoutButton_Click>d__);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00004901 File Offset: 0x00002B01
		private void ViewAllMyAppsButton_Click(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004904 File Offset: 0x00002B04
		private void MyAppItemClick(object sender, ItemClickEventArgs e)
		{
			StoreApp storeApp = e.ClickedItem as StoreApp;
			if (storeApp != null)
			{
				base.Frame.Navigate(typeof(AppDetailsPage), storeApp);
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00004938 File Offset: 0x00002B38
		private void WebStoreButton_Click(object sender, RoutedEventArgs e)
		{
			AccountPage.<WebStoreButton_Click>d__19 <WebStoreButton_Click>d__;
			<WebStoreButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<WebStoreButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <WebStoreButton_Click>d__.<>t__builder;
			<>t__builder.Start<AccountPage.<WebStoreButton_Click>d__19>(ref <WebStoreButton_Click>d__);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000496C File Offset: 0x00002B6C
		private void SubmitPortalButton_Click(object sender, RoutedEventArgs e)
		{
			AccountPage.<SubmitPortalButton_Click>d__20 <SubmitPortalButton_Click>d__;
			<SubmitPortalButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<SubmitPortalButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <SubmitPortalButton_Click>d__.<>t__builder;
			<>t__builder.Start<AccountPage.<SubmitPortalButton_Click>d__20>(ref <SubmitPortalButton_Click>d__);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000049A0 File Offset: 0x00002BA0
		private void DiscordButton_Click(object sender, RoutedEventArgs e)
		{
			AccountPage.<DiscordButton_Click>d__21 <DiscordButton_Click>d__;
			<DiscordButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<DiscordButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <DiscordButton_Click>d__.<>t__builder;
			<>t__builder.Start<AccountPage.<DiscordButton_Click>d__21>(ref <DiscordButton_Click>d__);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000049D4 File Offset: 0x00002BD4
		private void TelegramButton_Click(object sender, RoutedEventArgs e)
		{
			AccountPage.<TelegramButton_Click>d__22 <TelegramButton_Click>d__;
			<TelegramButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<TelegramButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <TelegramButton_Click>d__.<>t__builder;
			<>t__builder.Start<AccountPage.<TelegramButton_Click>d__22>(ref <TelegramButton_Click>d__);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004A08 File Offset: 0x00002C08
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
			catch (Exception)
			{
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004A4C File Offset: 0x00002C4C
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

		// Token: 0x06000115 RID: 277 RVA: 0x00004AC4 File Offset: 0x00002CC4
		private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				FrameworkElement frameworkElement = sender as FrameworkElement;
				if (frameworkElement != null)
				{
					StoreApp storeApp = frameworkElement.DataContext as StoreApp;
					if (storeApp != null)
					{
						if (!string.IsNullOrEmpty(storeApp.Publisher))
						{
							TextBlock textBlock = sender as TextBlock;
							if (textBlock != null)
							{
								try
								{
									textBlock.put_Foreground(new SolidColorBrush(Color.FromArgb(byte.MaxValue, 0, 170, 79)));
								}
								catch (Exception)
								{
								}
							}
							if (base.Frame != null)
							{
								base.Frame.Navigate(typeof(PublisherAppsPage), storeApp.Publisher);
							}
						}
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004B6C File Offset: 0x00002D6C
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Application.LoadComponent(this, new Uri("ms-appx:///AccountPage.xaml"), 0);
			this.pageRoot = (Page)base.FindName("pageRoot");
			this.MainHub = (Hub)base.FindName("MainHub");
			this.backButton = (Button)base.FindName("backButton");
			this.LoadingProgressRing = (ProgressRing)base.FindName("LoadingProgressRing");
			this.ProfileSection = (HubSection)base.FindName("ProfileSection");
			this.MyAppsSection = (HubSection)base.FindName("MyAppsSection");
			this.LatestReviewsSection = (HubSection)base.FindName("LatestReviewsSection");
			this.QuickLinksSection = (HubSection)base.FindName("QuickLinksSection");
			this.nbLogoText = (TextBlock)base.FindName("nbLogoText");
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004C60 File Offset: 0x00002E60
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
				FrameworkElement frameworkElement = (FrameworkElement)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(frameworkElement.add_Loaded), new Action<EventRegistrationToken>(frameworkElement.remove_Loaded), new RoutedEventHandler(this.MainHub_Loaded));
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
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.WebStoreButton_Click));
				break;
			}
			case 6:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.SubmitPortalButton_Click));
				break;
			}
			case 7:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.DiscordButton_Click));
				break;
			}
			case 8:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.TelegramButton_Click));
				break;
			}
			case 9:
			{
				ListViewBase listViewBase = (ListViewBase)target;
				WindowsRuntimeMarshal.AddEventHandler<ItemClickEventHandler>(new Func<ItemClickEventHandler, EventRegistrationToken>(listViewBase.add_ItemClick), new Action<EventRegistrationToken>(listViewBase.remove_ItemClick), new ItemClickEventHandler(this.MyAppItemClick));
				break;
			}
			case 10:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.HyperlinkButton_Click));
				break;
			}
			case 11:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.AccountLogoutButton_Click));
				break;
			}
			case 12:
			{
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uielement.add_Tapped), new Action<EventRegistrationToken>(uielement.remove_Tapped), new TappedEventHandler(this.StoreLogo_Click));
				break;
			}
			}
			this._contentLoaded = true;
		}

		// Token: 0x04000052 RID: 82
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Page pageRoot;

		// Token: 0x04000053 RID: 83
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Hub MainHub;

		// Token: 0x04000054 RID: 84
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button backButton;

		// Token: 0x04000055 RID: 85
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ProgressRing LoadingProgressRing;

		// Token: 0x04000056 RID: 86
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private HubSection ProfileSection;

		// Token: 0x04000057 RID: 87
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private HubSection MyAppsSection;

		// Token: 0x04000058 RID: 88
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private HubSection LatestReviewsSection;

		// Token: 0x04000059 RID: 89
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private HubSection QuickLinksSection;

		// Token: 0x0400005A RID: 90
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock nbLogoText;

		// Token: 0x0400005B RID: 91
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
