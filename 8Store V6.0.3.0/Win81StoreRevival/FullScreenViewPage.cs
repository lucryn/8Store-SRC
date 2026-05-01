using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace Win81StoreRevival
{
	// Token: 0x0200002B RID: 43
	public sealed class FullScreenViewPage : Page, IComponentConnector
	{
		// Token: 0x06000260 RID: 608 RVA: 0x0000E0F7 File Offset: 0x0000C2F7
		public FullScreenViewPage()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000E108 File Offset: 0x0000C308
		private void GoBack_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.GoBack();
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000E118 File Offset: 0x0000C318
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);
			string text = e.Parameter as string;
			bool flag = text != null;
			if (flag)
			{
				this.RootImage.put_Source(new BitmapImage(new Uri(text)));
			}
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000E15C File Offset: 0x0000C35C
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			bool contentLoaded = this._contentLoaded;
			if (!contentLoaded)
			{
				this._contentLoaded = true;
				Application.LoadComponent(this, new Uri("ms-appx:///FullScreenViewPage.xaml"), 0);
				this.GoBack = (Button)base.FindName("GoBack");
				this.RootImage = (Image)base.FindName("RootImage");
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000E1BC File Offset: 0x0000C3BC
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.GoBack_Click));
			}
			this._contentLoaded = true;
		}

		// Token: 0x0400012E RID: 302
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button GoBack;

		// Token: 0x0400012F RID: 303
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Image RootImage;

		// Token: 0x04000130 RID: 304
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
