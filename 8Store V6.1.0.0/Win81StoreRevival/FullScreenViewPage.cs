using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Navigation;

namespace Win81StoreRevival
{
	// Token: 0x0200002E RID: 46
	public sealed class FullScreenViewPage : Page, IComponentConnector
	{
		// Token: 0x060002DE RID: 734 RVA: 0x0000EF19 File Offset: 0x0000D119
		public FullScreenViewPage()
		{
			this.InitializeComponent();
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000EF27 File Offset: 0x0000D127
		private void GoBack_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.GoBack();
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000EF34 File Offset: 0x0000D134
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);
			FullScreenViewPage.FullscreenViewPayload fullscreenViewPayload = e.Parameter as FullScreenViewPage.FullscreenViewPayload;
			if (fullscreenViewPayload != null && fullscreenViewPayload.Screenshots != null && fullscreenViewPayload.Screenshots.Count > 0)
			{
				this.RootFlipView.put_ItemsSource(fullscreenViewPayload.Screenshots);
				int num = fullscreenViewPayload.SelectedIndex;
				if (num < 0 || num >= fullscreenViewPayload.Screenshots.Count)
				{
					num = 0;
				}
				this.RootFlipView.put_SelectedIndex(num);
				return;
			}
			string text = e.Parameter as string;
			if (!string.IsNullOrWhiteSpace(text))
			{
				ItemsControl rootFlipView = this.RootFlipView;
				List<string> list = new List<string>();
				list.Add(text);
				rootFlipView.put_ItemsSource(list);
				this.RootFlipView.put_SelectedIndex(0);
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000EFE0 File Offset: 0x0000D1E0
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Application.LoadComponent(this, new Uri("ms-appx:///FullScreenViewPage.xaml"), 0);
			this.GoBack = (Button)base.FindName("GoBack");
			this.RootFlipView = (FlipView)base.FindName("RootFlipView");
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000F03C File Offset: 0x0000D23C
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

		// Token: 0x04000138 RID: 312
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button GoBack;

		// Token: 0x04000139 RID: 313
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private FlipView RootFlipView;

		// Token: 0x0400013A RID: 314
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;

		// Token: 0x020000FF RID: 255
		public class FullscreenViewPayload
		{
			// Token: 0x17000143 RID: 323
			// (get) Token: 0x0600081D RID: 2077 RVA: 0x0002E176 File Offset: 0x0002C376
			// (set) Token: 0x0600081E RID: 2078 RVA: 0x0002E17E File Offset: 0x0002C37E
			public List<string> Screenshots { get; set; }

			// Token: 0x17000144 RID: 324
			// (get) Token: 0x0600081F RID: 2079 RVA: 0x0002E187 File Offset: 0x0002C387
			// (set) Token: 0x06000820 RID: 2080 RVA: 0x0002E18F File Offset: 0x0002C38F
			public int SelectedIndex { get; set; }
		}
	}
}
