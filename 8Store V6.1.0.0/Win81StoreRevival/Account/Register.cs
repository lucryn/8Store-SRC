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
	// Token: 0x02000067 RID: 103
	public sealed class Register : UserControl, IComponentConnector
	{
		// Token: 0x1400000F RID: 15
		// (add) Token: 0x0600067E RID: 1662 RVA: 0x0001E8DC File Offset: 0x0001CADC
		// (remove) Token: 0x0600067F RID: 1663 RVA: 0x0001E914 File Offset: 0x0001CB14
		public event EventHandler CloseRequested;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000680 RID: 1664 RVA: 0x0001E94C File Offset: 0x0001CB4C
		// (remove) Token: 0x06000681 RID: 1665 RVA: 0x0001E984 File Offset: 0x0001CB84
		public event EventHandler LoginRequested;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000682 RID: 1666 RVA: 0x0001E9BC File Offset: 0x0001CBBC
		// (remove) Token: 0x06000683 RID: 1667 RVA: 0x0001E9F4 File Offset: 0x0001CBF4
		public event EventHandler DiscordRequested;

		// Token: 0x06000684 RID: 1668 RVA: 0x0001EA2C File Offset: 0x0001CC2C
		public Register()
		{
			this.InitializeComponent();
			this.CenterStrip.put_Opacity(0.0);
			this.LoadingPanel.put_Visibility(1);
			this.ErrorPanel.put_Visibility(1);
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

		// Token: 0x06000685 RID: 1669 RVA: 0x0001EAE8 File Offset: 0x0001CCE8
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

		// Token: 0x06000686 RID: 1670 RVA: 0x0001EB4C File Offset: 0x0001CD4C
		private void ShowError(string title, string message)
		{
			this.MainContent.put_Opacity(0.0);
			this.MainContent.put_Visibility(1);
			this.LoadingPanel.put_Opacity(0.0);
			this.LoadingPanel.put_Visibility(1);
			this.ErrorPanel.put_Opacity(1.0);
			this.ErrorPanel.put_Visibility(0);
			this.ErrorTitle.put_Text(title);
			this.ErrorMessage.put_Text(message);
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0001EBD4 File Offset: 0x0001CDD4
		private void ErrorBackButton_Click(object sender, RoutedEventArgs e)
		{
			this.MainContent.put_Opacity(1.0);
			this.LoadingPanel.put_Opacity(0.0);
			this.ErrorPanel.put_Opacity(0.0);
			if (!string.IsNullOrEmpty(this.EmailBox.Text))
			{
				this.EmailBox.Focus(3);
				return;
			}
			if (!string.IsNullOrEmpty(this.UsernameBox.Text))
			{
				this.UsernameBox.Focus(3);
				return;
			}
			this.PassBox.Focus(3);
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0001EC6C File Offset: 0x0001CE6C
		private void ShowLoginWithLoading(object sender, RoutedEventArgs e)
		{
			Register.<ShowLoginWithLoading>d__14 <ShowLoginWithLoading>d__;
			<ShowLoginWithLoading>d__.<>4__this = this;
			<ShowLoginWithLoading>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<ShowLoginWithLoading>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <ShowLoginWithLoading>d__.<>t__builder;
			<>t__builder.Start<Register.<ShowLoginWithLoading>d__14>(ref <ShowLoginWithLoading>d__);
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0001ECA8 File Offset: 0x0001CEA8
		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			Register.<SaveButton_Click>d__15 <SaveButton_Click>d__;
			<SaveButton_Click>d__.<>4__this = this;
			<SaveButton_Click>d__.sender = sender;
			<SaveButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<SaveButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <SaveButton_Click>d__.<>t__builder;
			<>t__builder.Start<Register.<SaveButton_Click>d__15>(ref <SaveButton_Click>d__);
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0001ECE9 File Offset: 0x0001CEE9
		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			this.CloseOverlay();
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0001ECF4 File Offset: 0x0001CEF4
		private Task ShowMessage(string message, string title)
		{
			Register.<ShowMessage>d__17 <ShowMessage>d__;
			<ShowMessage>d__.message = message;
			<ShowMessage>d__.title = title;
			<ShowMessage>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ShowMessage>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <ShowMessage>d__.<>t__builder;
			<>t__builder.Start<Register.<ShowMessage>d__17>(ref <ShowMessage>d__);
			return <ShowMessage>d__.<>t__builder.Task;
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0001ED44 File Offset: 0x0001CF44
		private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
		{
			Register.<HyperlinkButton_Click>d__18 <HyperlinkButton_Click>d__;
			<HyperlinkButton_Click>d__.<>4__this = this;
			<HyperlinkButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<HyperlinkButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <HyperlinkButton_Click>d__.<>t__builder;
			<>t__builder.Start<Register.<HyperlinkButton_Click>d__18>(ref <HyperlinkButton_Click>d__);
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0001ED80 File Offset: 0x0001CF80
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Application.LoadComponent(this, new Uri("ms-appx:///Account/Register.xaml"), 0);
			this.FadeIn = (Storyboard)base.FindName("FadeIn");
			this.FadeOut = (Storyboard)base.FindName("FadeOut");
			this.ShowLoading = (Storyboard)base.FindName("ShowLoading");
			this.RootGrid = (Grid)base.FindName("RootGrid");
			this.BackgroundBorder = (Border)base.FindName("BackgroundBorder");
			this.CenterStrip = (Grid)base.FindName("CenterStrip");
			this.LoadingPanel = (StackPanel)base.FindName("LoadingPanel");
			this.ErrorPanel = (Grid)base.FindName("ErrorPanel");
			this.MainContent = (Grid)base.FindName("MainContent");
			this.UsernameBox = (TextBox)base.FindName("UsernameBox");
			this.EmailBox = (TextBox)base.FindName("EmailBox");
			this.PassBox = (PasswordBox)base.FindName("PassBox");
			this.PassBox1 = (PasswordBox)base.FindName("PassBox1");
			this.ErrorTitle = (TextBlock)base.FindName("ErrorTitle");
			this.ErrorMessage = (TextBlock)base.FindName("ErrorMessage");
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0001EEF8 File Offset: 0x0001D0F8
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
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.ShowLoginWithLoading));
				break;
			}
			case 4:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.HyperlinkButton_Click));
				break;
			}
			case 5:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.ErrorBackButton_Click));
				break;
			}
			case 6:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.CancelButton_Click));
				break;
			}
			}
			this._contentLoaded = true;
		}

		// Token: 0x0400028F RID: 655
		private readonly ResourceLoader _loader = ResourceLoader.GetForCurrentView();

		// Token: 0x04000290 RID: 656
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Storyboard FadeIn;

		// Token: 0x04000291 RID: 657
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Storyboard FadeOut;

		// Token: 0x04000292 RID: 658
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Storyboard ShowLoading;

		// Token: 0x04000293 RID: 659
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Grid RootGrid;

		// Token: 0x04000294 RID: 660
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Border BackgroundBorder;

		// Token: 0x04000295 RID: 661
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Grid CenterStrip;

		// Token: 0x04000296 RID: 662
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel LoadingPanel;

		// Token: 0x04000297 RID: 663
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Grid ErrorPanel;

		// Token: 0x04000298 RID: 664
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Grid MainContent;

		// Token: 0x04000299 RID: 665
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBox UsernameBox;

		// Token: 0x0400029A RID: 666
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBox EmailBox;

		// Token: 0x0400029B RID: 667
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private PasswordBox PassBox;

		// Token: 0x0400029C RID: 668
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private PasswordBox PassBox1;

		// Token: 0x0400029D RID: 669
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock ErrorTitle;

		// Token: 0x0400029E RID: 670
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock ErrorMessage;

		// Token: 0x0400029F RID: 671
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
