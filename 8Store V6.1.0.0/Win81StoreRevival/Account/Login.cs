using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace Win81StoreRevival.Account
{
	// Token: 0x02000066 RID: 102
	public sealed class Login : UserControl, IComponentConnector
	{
		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000668 RID: 1640 RVA: 0x0001DF90 File Offset: 0x0001C190
		// (remove) Token: 0x06000669 RID: 1641 RVA: 0x0001DFC8 File Offset: 0x0001C1C8
		public event EventHandler CloseRequested;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x0600066A RID: 1642 RVA: 0x0001E000 File Offset: 0x0001C200
		// (remove) Token: 0x0600066B RID: 1643 RVA: 0x0001E038 File Offset: 0x0001C238
		public event EventHandler RegisterRequested;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x0600066C RID: 1644 RVA: 0x0001E070 File Offset: 0x0001C270
		// (remove) Token: 0x0600066D RID: 1645 RVA: 0x0001E0A8 File Offset: 0x0001C2A8
		public event EventHandler DiscordRequested;

		// Token: 0x0600066E RID: 1646 RVA: 0x0001E0E0 File Offset: 0x0001C2E0
		public Login()
		{
			this.InitializeComponent();
			this.CenterStrip.put_Opacity(0.0);
			this.LoadingPanel.put_Visibility(1);
			this.ErrorPanel.put_Visibility(1);
			this.SuccessPanel.put_Visibility(1);
			this.MainContent.put_Visibility(0);
			Storyboard fadeIn = this.FadeIn;
			WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(fadeIn.add_Completed), new Action<EventRegistrationToken>(fadeIn.remove_Completed), delegate(object s, object e)
			{
				this.CenterStrip.put_CacheMode(null);
			});
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(this.add_Loaded), new Action<EventRegistrationToken>(this.remove_Loaded), delegate(object s, RoutedEventArgs e)
			{
				this.FadeIn.Begin();
			});
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0001E1A8 File Offset: 0x0001C3A8
		public void CloseOverlay()
		{
			base.put_IsHitTestVisible(false);
			this.CenterStrip.put_CacheMode(new BitmapCache());
			this.FadeOut.Begin();
			Storyboard fadeOut = this.FadeOut;
			WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(fadeOut.add_Completed), new Action<EventRegistrationToken>(fadeOut.remove_Completed), delegate(object s, object e)
			{
				this.CloseRequested.Invoke(this, EventArgs.Empty);
			});
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0001E20C File Offset: 0x0001C40C
		private void ShowRegisterWithLoading(object sender, RoutedEventArgs e)
		{
			Login.<ShowRegisterWithLoading>d__12 <ShowRegisterWithLoading>d__;
			<ShowRegisterWithLoading>d__.<>4__this = this;
			<ShowRegisterWithLoading>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<ShowRegisterWithLoading>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <ShowRegisterWithLoading>d__.<>t__builder;
			<>t__builder.Start<Login.<ShowRegisterWithLoading>d__12>(ref <ShowRegisterWithLoading>d__);
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0001E248 File Offset: 0x0001C448
		private void ShowDiscordWithLoading(object sender, RoutedEventArgs e)
		{
			Login.<ShowDiscordWithLoading>d__13 <ShowDiscordWithLoading>d__;
			<ShowDiscordWithLoading>d__.<>4__this = this;
			<ShowDiscordWithLoading>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<ShowDiscordWithLoading>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <ShowDiscordWithLoading>d__.<>t__builder;
			<>t__builder.Start<Login.<ShowDiscordWithLoading>d__13>(ref <ShowDiscordWithLoading>d__);
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0001E284 File Offset: 0x0001C484
		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			Login.<SaveButton_Click>d__14 <SaveButton_Click>d__;
			<SaveButton_Click>d__.<>4__this = this;
			<SaveButton_Click>d__.sender = sender;
			<SaveButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<SaveButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <SaveButton_Click>d__.<>t__builder;
			<>t__builder.Start<Login.<SaveButton_Click>d__14>(ref <SaveButton_Click>d__);
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0001E2C8 File Offset: 0x0001C4C8
		private void ShowSuccess(string displayName)
		{
			this.MainContent.put_Opacity(0.0);
			this.MainContent.put_Visibility(1);
			this.LoadingPanel.put_Opacity(0.0);
			this.LoadingPanel.put_Visibility(1);
			this.ErrorPanel.put_Opacity(0.0);
			this.ErrorPanel.put_Visibility(1);
			this.SuccessPanel.put_Visibility(0);
			this.SuccessPanel.put_Opacity(1.0);
			if (string.IsNullOrWhiteSpace(this.SuccessTitle.Text))
			{
				this.SuccessTitle.put_Text("Successfully signed in");
			}
			string text = this.SuccessMessage.Text;
			if (string.IsNullOrWhiteSpace(text))
			{
				text = "Welcome, {0}! You have successfully signed in.";
			}
			this.SuccessMessage.put_Text(string.Format(text, new object[]
			{
				displayName
			}));
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0001E3AC File Offset: 0x0001C5AC
		private void SuccessContinueButton_Click(object sender, RoutedEventArgs e)
		{
			this.CloseOverlay();
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0001E3B4 File Offset: 0x0001C5B4
		private void ShowError(string title, string message)
		{
			this.MainContent.put_Opacity(0.0);
			this.MainContent.put_Visibility(1);
			this.LoadingPanel.put_Opacity(0.0);
			this.LoadingPanel.put_Visibility(1);
			this.ErrorPanel.put_Opacity(1.0);
			this.ErrorPanel.put_Visibility(0);
			this.SuccessPanel.put_Opacity(0.0);
			this.SuccessPanel.put_Visibility(1);
			this.ErrorTitle.put_Text(title);
			this.ErrorMessage.put_Text(message);
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0001E45C File Offset: 0x0001C65C
		private void ErrorBackButton_Click(object sender, RoutedEventArgs e)
		{
			this.MainContent.put_Opacity(1.0);
			this.MainContent.put_Visibility(0);
			this.LoadingPanel.put_Opacity(0.0);
			this.LoadingPanel.put_Visibility(1);
			this.ErrorPanel.put_Opacity(0.0);
			this.ErrorPanel.put_Visibility(1);
			this.SuccessPanel.put_Opacity(0.0);
			this.SuccessPanel.put_Visibility(1);
			if (!string.IsNullOrEmpty(this.EmailBox.Text))
			{
				this.EmailBox.Focus(3);
				return;
			}
			this.PassBox.Focus(3);
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0001E3AC File Offset: 0x0001C5AC
		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			this.CloseOverlay();
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0001E518 File Offset: 0x0001C718
		private Task ShowMessage(string message, string title)
		{
			Login.<ShowMessage>d__20 <ShowMessage>d__;
			<ShowMessage>d__.message = message;
			<ShowMessage>d__.title = title;
			<ShowMessage>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ShowMessage>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <ShowMessage>d__.<>t__builder;
			<>t__builder.Start<Login.<ShowMessage>d__20>(ref <ShowMessage>d__);
			return <ShowMessage>d__.<>t__builder.Task;
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0001E568 File Offset: 0x0001C768
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Application.LoadComponent(this, new Uri("ms-appx:///Account/Login.xaml"), 0);
			this.FadeIn = (Storyboard)base.FindName("FadeIn");
			this.FadeOut = (Storyboard)base.FindName("FadeOut");
			this.ShowLoading = (Storyboard)base.FindName("ShowLoading");
			this.RootGrid = (Grid)base.FindName("RootGrid");
			this.BackgroundBorder = (Border)base.FindName("BackgroundBorder");
			this.CenterStrip = (Grid)base.FindName("CenterStrip");
			this.LoadingPanel = (StackPanel)base.FindName("LoadingPanel");
			this.ErrorPanel = (Grid)base.FindName("ErrorPanel");
			this.SuccessPanel = (Grid)base.FindName("SuccessPanel");
			this.MainContent = (Grid)base.FindName("MainContent");
			this.EmailBox = (TextBox)base.FindName("EmailBox");
			this.PassBox = (PasswordBox)base.FindName("PassBox");
			this.SuccessTitle = (TextBlock)base.FindName("SuccessTitle");
			this.SuccessMessage = (TextBlock)base.FindName("SuccessMessage");
			this.ErrorTitle = (TextBlock)base.FindName("ErrorTitle");
			this.ErrorMessage = (TextBlock)base.FindName("ErrorMessage");
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0001E6F8 File Offset: 0x0001C8F8
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.SaveButton_Click));
				break;
			}
			case 2:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.CancelButton_Click));
				break;
			}
			case 3:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.ShowRegisterWithLoading));
				break;
			}
			case 4:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.ShowDiscordWithLoading));
				break;
			}
			case 5:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.SuccessContinueButton_Click));
				break;
			}
			case 6:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.ErrorBackButton_Click));
				break;
			}
			case 7:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.CancelButton_Click));
				break;
			}
			}
			this._contentLoaded = true;
		}

		// Token: 0x0400027A RID: 634
		private readonly ResourceLoader _loader = ResourceLoader.GetForCurrentView();

		// Token: 0x0400027B RID: 635
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Storyboard FadeIn;

		// Token: 0x0400027C RID: 636
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Storyboard FadeOut;

		// Token: 0x0400027D RID: 637
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Storyboard ShowLoading;

		// Token: 0x0400027E RID: 638
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Grid RootGrid;

		// Token: 0x0400027F RID: 639
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Border BackgroundBorder;

		// Token: 0x04000280 RID: 640
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Grid CenterStrip;

		// Token: 0x04000281 RID: 641
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel LoadingPanel;

		// Token: 0x04000282 RID: 642
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Grid ErrorPanel;

		// Token: 0x04000283 RID: 643
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Grid SuccessPanel;

		// Token: 0x04000284 RID: 644
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Grid MainContent;

		// Token: 0x04000285 RID: 645
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBox EmailBox;

		// Token: 0x04000286 RID: 646
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private PasswordBox PassBox;

		// Token: 0x04000287 RID: 647
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock SuccessTitle;

		// Token: 0x04000288 RID: 648
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock SuccessMessage;

		// Token: 0x04000289 RID: 649
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock ErrorTitle;

		// Token: 0x0400028A RID: 650
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock ErrorMessage;

		// Token: 0x0400028B RID: 651
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
