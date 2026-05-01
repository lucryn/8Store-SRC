using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace Win81StoreRevival.Account
{
	// Token: 0x02000065 RID: 101
	public sealed class Discord : UserControl, IComponentConnector
	{
		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000658 RID: 1624 RVA: 0x0001DB9C File Offset: 0x0001BD9C
		// (remove) Token: 0x06000659 RID: 1625 RVA: 0x0001DBD4 File Offset: 0x0001BDD4
		public event EventHandler CloseRequested;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600065A RID: 1626 RVA: 0x0001DC0C File Offset: 0x0001BE0C
		// (remove) Token: 0x0600065B RID: 1627 RVA: 0x0001DC44 File Offset: 0x0001BE44
		public event EventHandler LoginRequested;

		// Token: 0x0600065C RID: 1628 RVA: 0x0001DC7C File Offset: 0x0001BE7C
		public Discord()
		{
			this.InitializeComponent();
			this.CenterStrip.put_Opacity(0.0);
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

		// Token: 0x0600065D RID: 1629 RVA: 0x0001DD14 File Offset: 0x0001BF14
		public void CloseOverlay()
		{
			base.put_IsHitTestVisible(false);
			this.CenterStrip.put_CacheMode(new BitmapCache());
			this.FadeOut.Begin();
			Storyboard fadeOut = this.FadeOut;
			WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(fadeOut.add_Completed), new Action<EventRegistrationToken>(fadeOut.remove_Completed), delegate(object s, object e)
			{
				EventHandler closeRequested = this.CloseRequested;
				if (closeRequested == null)
				{
					return;
				}
				closeRequested.Invoke(this, EventArgs.Empty);
			});
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0001DD75 File Offset: 0x0001BF75
		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			EventHandler loginRequested = this.LoginRequested;
			if (loginRequested == null)
			{
				return;
			}
			loginRequested.Invoke(this, EventArgs.Empty);
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0001DD8D File Offset: 0x0001BF8D
		private void ShowError(string title, string message)
		{
			this.MainContent.put_Opacity(0.0);
			this.MainContent.put_Visibility(1);
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0001DDAF File Offset: 0x0001BFAF
		private void ErrorBackButton_Click(object sender, RoutedEventArgs e)
		{
			this.MainContent.put_Opacity(1.0);
			this.MainContent.put_Visibility(0);
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0001DDD1 File Offset: 0x0001BFD1
		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			this.CloseOverlay();
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0001DDDC File Offset: 0x0001BFDC
		private void Hyperlink_Click(Hyperlink sender, HyperlinkClickEventArgs args)
		{
			Discord.<Hyperlink_Click>d__12 <Hyperlink_Click>d__;
			<Hyperlink_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<Hyperlink_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <Hyperlink_Click>d__.<>t__builder;
			<>t__builder.Start<Discord.<Hyperlink_Click>d__12>(ref <Hyperlink_Click>d__);
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0001DE10 File Offset: 0x0001C010
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Application.LoadComponent(this, new Uri("ms-appx:///Account/Discord.xaml"), 0);
			this.FadeIn = (Storyboard)base.FindName("FadeIn");
			this.FadeOut = (Storyboard)base.FindName("FadeOut");
			this.ShowLoading = (Storyboard)base.FindName("ShowLoading");
			this.RootGrid = (Grid)base.FindName("RootGrid");
			this.BackgroundBorder = (Border)base.FindName("BackgroundBorder");
			this.CenterStrip = (Grid)base.FindName("CenterStrip");
			this.MainContent = (Grid)base.FindName("MainContent");
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0001DED8 File Offset: 0x0001C0D8
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void Connect(int connectionId, object target)
		{
			if (connectionId != 1)
			{
				if (connectionId == 2)
				{
					Hyperlink hyperlink = (Hyperlink)target;
					WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<Hyperlink, HyperlinkClickEventArgs>>(new Func<TypedEventHandler<Hyperlink, HyperlinkClickEventArgs>, EventRegistrationToken>(hyperlink.add_Click), new Action<EventRegistrationToken>(hyperlink.remove_Click), new TypedEventHandler<Hyperlink, HyperlinkClickEventArgs>(this.Hyperlink_Click));
				}
			}
			else
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.BackButton_Click));
			}
			this._contentLoaded = true;
		}

		// Token: 0x0400026F RID: 623
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Storyboard FadeIn;

		// Token: 0x04000270 RID: 624
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Storyboard FadeOut;

		// Token: 0x04000271 RID: 625
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Storyboard ShowLoading;

		// Token: 0x04000272 RID: 626
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Grid RootGrid;

		// Token: 0x04000273 RID: 627
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Border BackgroundBorder;

		// Token: 0x04000274 RID: 628
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Grid CenterStrip;

		// Token: 0x04000275 RID: 629
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Grid MainContent;

		// Token: 0x04000276 RID: 630
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
