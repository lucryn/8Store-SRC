using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Navigation;

namespace Win81StoreRevival
{
	// Token: 0x0200000C RID: 12
	public sealed class AccountPage : Page, IComponentConnector
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00002CD5 File Offset: 0x00000ED5
		public AccountPage()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002CE8 File Offset: 0x00000EE8
		[DebuggerStepThrough]
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			AccountPage.<OnNavigatedTo>d__2 <OnNavigatedTo>d__ = new AccountPage.<OnNavigatedTo>d__2();
			<OnNavigatedTo>d__.<>4__this = this;
			<OnNavigatedTo>d__.e = e;
			<OnNavigatedTo>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<OnNavigatedTo>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <OnNavigatedTo>d__.<>t__builder;
			<>t__builder.Start<AccountPage.<OnNavigatedTo>d__2>(ref <OnNavigatedTo>d__);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002D2C File Offset: 0x00000F2C
		[DebuggerStepThrough]
		private Task LoadAppsForStats()
		{
			AccountPage.<LoadAppsForStats>d__3 <LoadAppsForStats>d__ = new AccountPage.<LoadAppsForStats>d__3();
			<LoadAppsForStats>d__.<>4__this = this;
			<LoadAppsForStats>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadAppsForStats>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadAppsForStats>d__.<>t__builder;
			<>t__builder.Start<AccountPage.<LoadAppsForStats>d__3>(ref <LoadAppsForStats>d__);
			return <LoadAppsForStats>d__.<>t__builder.Task;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002D74 File Offset: 0x00000F74
		private void UpdateStats()
		{
			bool flag = this.allAppsForStats != null;
			if (flag)
			{
				this.TotalAppsText.put_Text(this.allAppsForStats.Count.ToString());
				int num = Enumerable.Count<string>(Enumerable.Distinct<string>(Enumerable.Select<StoreApp, string>(this.allAppsForStats, (StoreApp a) => a.Publisher)));
				this.PublishersText.put_Text(num.ToString());
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002DF8 File Offset: 0x00000FF8
		private void UpdateUIBasedOnLoginState()
		{
			UserAccount currentUser = SupabaseService.GetCurrentUser();
			bool flag = currentUser == null;
			if (flag)
			{
				this.NotLoggedInPanel.put_Visibility(0);
				this.LoggedInPanel.put_Visibility(1);
				this.RegisterPanel.put_Visibility(1);
				this.PageTitle.put_Text("My Account");
			}
			else
			{
				this.NotLoggedInPanel.put_Visibility(1);
				this.LoggedInPanel.put_Visibility(0);
				this.RegisterPanel.put_Visibility(1);
				this.PageTitle.put_Text("My Account");
				this.WelcomeUsername.put_Text(currentUser.Username);
				this.UserEmail.put_Text(currentUser.Email);
				this.UserName.put_Text(currentUser.Username);
				this.UserId.put_Text(currentUser.Id.ToString());
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002EDA File Offset: 0x000010DA
		private void ShowLoading(bool show)
		{
			this.LoadingPanel.put_Visibility(show ? 0 : 1);
			this.LoadingRing.put_IsActive(show);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002F00 File Offset: 0x00001100
		[DebuggerStepThrough]
		private void SignInButton_Click(object sender, RoutedEventArgs e)
		{
			AccountPage.<SignInButton_Click>d__7 <SignInButton_Click>d__ = new AccountPage.<SignInButton_Click>d__7();
			<SignInButton_Click>d__.<>4__this = this;
			<SignInButton_Click>d__.sender = sender;
			<SignInButton_Click>d__.e = e;
			<SignInButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<SignInButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <SignInButton_Click>d__.<>t__builder;
			<>t__builder.Start<AccountPage.<SignInButton_Click>d__7>(ref <SignInButton_Click>d__);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002F4A File Offset: 0x0000114A
		private void CreateAccountButton_Click(object sender, RoutedEventArgs e)
		{
			this.NotLoggedInPanel.put_Visibility(1);
			this.RegisterPanel.put_Visibility(0);
			this.PageTitle.put_Text("Create Account");
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002F78 File Offset: 0x00001178
		private void BackToLoginButton_Click(object sender, RoutedEventArgs e)
		{
			this.RegisterPanel.put_Visibility(1);
			this.NotLoggedInPanel.put_Visibility(0);
			this.PageTitle.put_Text("My Account");
			this.RegisterEmailBox.put_Text("");
			this.RegisterUsernameBox.put_Text("");
			this.RegisterPasswordBox.put_Password("");
			this.RegisterConfirmPasswordBox.put_Password("");
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002FF8 File Offset: 0x000011F8
		[DebuggerStepThrough]
		private void SignUpButton_Click(object sender, RoutedEventArgs e)
		{
			AccountPage.<SignUpButton_Click>d__10 <SignUpButton_Click>d__ = new AccountPage.<SignUpButton_Click>d__10();
			<SignUpButton_Click>d__.<>4__this = this;
			<SignUpButton_Click>d__.sender = sender;
			<SignUpButton_Click>d__.e = e;
			<SignUpButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<SignUpButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <SignUpButton_Click>d__.<>t__builder;
			<>t__builder.Start<AccountPage.<SignUpButton_Click>d__10>(ref <SignUpButton_Click>d__);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003044 File Offset: 0x00001244
		[DebuggerStepThrough]
		private void SignOutButton_Click(object sender, RoutedEventArgs e)
		{
			AccountPage.<SignOutButton_Click>d__11 <SignOutButton_Click>d__ = new AccountPage.<SignOutButton_Click>d__11();
			<SignOutButton_Click>d__.<>4__this = this;
			<SignOutButton_Click>d__.sender = sender;
			<SignOutButton_Click>d__.e = e;
			<SignOutButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<SignOutButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <SignOutButton_Click>d__.<>t__builder;
			<>t__builder.Start<AccountPage.<SignOutButton_Click>d__11>(ref <SignOutButton_Click>d__);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003090 File Offset: 0x00001290
		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			bool canGoBack = base.Frame.CanGoBack;
			if (canGoBack)
			{
				base.Frame.GoBack();
			}
			else
			{
				base.Frame.Navigate(typeof(MainPage));
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000030D4 File Offset: 0x000012D4
		[DebuggerStepThrough]
		private Task ShowMessage(string message, string title)
		{
			AccountPage.<ShowMessage>d__13 <ShowMessage>d__ = new AccountPage.<ShowMessage>d__13();
			<ShowMessage>d__.<>4__this = this;
			<ShowMessage>d__.message = message;
			<ShowMessage>d__.title = title;
			<ShowMessage>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ShowMessage>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <ShowMessage>d__.<>t__builder;
			<>t__builder.Start<AccountPage.<ShowMessage>d__13>(ref <ShowMessage>d__);
			return <ShowMessage>d__.<>t__builder.Task;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000312C File Offset: 0x0000132C
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			bool contentLoaded = this._contentLoaded;
			if (!contentLoaded)
			{
				this._contentLoaded = true;
				Application.LoadComponent(this, new Uri("ms-appx:///AccountPage.xaml"), 0);
				this.LoadingPanel = (Grid)base.FindName("LoadingPanel");
				this.NotLoggedInPanel = (Grid)base.FindName("NotLoggedInPanel");
				this.LoggedInPanel = (Grid)base.FindName("LoggedInPanel");
				this.RegisterPanel = (Grid)base.FindName("RegisterPanel");
				this.BackToLoginButton = (Button)base.FindName("BackToLoginButton");
				this.RegisterEmailBox = (TextBox)base.FindName("RegisterEmailBox");
				this.RegisterUsernameBox = (TextBox)base.FindName("RegisterUsernameBox");
				this.RegisterPasswordBox = (PasswordBox)base.FindName("RegisterPasswordBox");
				this.RegisterConfirmPasswordBox = (PasswordBox)base.FindName("RegisterConfirmPasswordBox");
				this.SignUpButton = (Button)base.FindName("SignUpButton");
				this.SignOutButton = (Button)base.FindName("SignOutButton");
				this.PublishersText = (TextBlock)base.FindName("PublishersText");
				this.TotalAppsText = (TextBlock)base.FindName("TotalAppsText");
				this.UserId = (TextBlock)base.FindName("UserId");
				this.UserName = (TextBlock)base.FindName("UserName");
				this.UserEmail = (TextBlock)base.FindName("UserEmail");
				this.WelcomeUsername = (TextBlock)base.FindName("WelcomeUsername");
				this.PageTitle = (TextBlock)base.FindName("PageTitle");
				this.LoginEmailBox = (TextBox)base.FindName("LoginEmailBox");
				this.LoginPasswordBox = (PasswordBox)base.FindName("LoginPasswordBox");
				this.SignInButton = (Button)base.FindName("SignInButton");
				this.CreateAccountButton = (Button)base.FindName("CreateAccountButton");
				this.LoadingRing = (ProgressRing)base.FindName("LoadingRing");
				this.BackButton = (Button)base.FindName("BackButton");
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003374 File Offset: 0x00001574
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.BackToLoginButton_Click));
				break;
			}
			case 2:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.SignUpButton_Click));
				break;
			}
			case 3:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.SignOutButton_Click));
				break;
			}
			case 4:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.SignInButton_Click));
				break;
			}
			case 5:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.CreateAccountButton_Click));
				break;
			}
			case 6:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.BackButton_Click));
				break;
			}
			}
			this._contentLoaded = true;
		}

		// Token: 0x04000016 RID: 22
		private List<StoreApp> allAppsForStats;

		// Token: 0x04000017 RID: 23
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Grid LoadingPanel;

		// Token: 0x04000018 RID: 24
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Grid NotLoggedInPanel;

		// Token: 0x04000019 RID: 25
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Grid LoggedInPanel;

		// Token: 0x0400001A RID: 26
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Grid RegisterPanel;

		// Token: 0x0400001B RID: 27
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button BackToLoginButton;

		// Token: 0x0400001C RID: 28
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBox RegisterEmailBox;

		// Token: 0x0400001D RID: 29
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBox RegisterUsernameBox;

		// Token: 0x0400001E RID: 30
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private PasswordBox RegisterPasswordBox;

		// Token: 0x0400001F RID: 31
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private PasswordBox RegisterConfirmPasswordBox;

		// Token: 0x04000020 RID: 32
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button SignUpButton;

		// Token: 0x04000021 RID: 33
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button SignOutButton;

		// Token: 0x04000022 RID: 34
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock PublishersText;

		// Token: 0x04000023 RID: 35
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock TotalAppsText;

		// Token: 0x04000024 RID: 36
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock UserId;

		// Token: 0x04000025 RID: 37
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock UserName;

		// Token: 0x04000026 RID: 38
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock UserEmail;

		// Token: 0x04000027 RID: 39
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock WelcomeUsername;

		// Token: 0x04000028 RID: 40
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock PageTitle;

		// Token: 0x04000029 RID: 41
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBox LoginEmailBox;

		// Token: 0x0400002A RID: 42
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private PasswordBox LoginPasswordBox;

		// Token: 0x0400002B RID: 43
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button SignInButton;

		// Token: 0x0400002C RID: 44
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button CreateAccountButton;

		// Token: 0x0400002D RID: 45
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ProgressRing LoadingRing;

		// Token: 0x0400002E RID: 46
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button BackButton;

		// Token: 0x0400002F RID: 47
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
