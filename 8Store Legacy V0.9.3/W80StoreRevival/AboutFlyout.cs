using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;

namespace W80StoreRevival
{
	// Token: 0x02000002 RID: 2
	public sealed class AboutFlyout : UserControl, IComponentConnector
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public AboutFlyout()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002064 File Offset: 0x00000264
		public void ShowFlyout()
		{
			try
			{
				this._aboutPopup = new Popup();
				this._aboutPopup.put_Child(this);
				this._aboutPopup.put_IsLightDismissEnabled(true);
				Popup aboutPopup = this._aboutPopup;
				WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(aboutPopup.add_Closed), new Action<EventRegistrationToken>(aboutPopup.remove_Closed), new EventHandler<object>(this.OnPopupClosed));
				Rect bounds = Window.Current.Bounds;
				this._aboutPopup.put_HorizontalOffset(bounds.Width - base.Width);
				this._aboutPopup.put_VerticalOffset(0.0);
				this._aboutPopup.put_IsOpen(true);
				Debug.WriteLine("[ABOUT] About flyout opened");
			}
			catch (Exception ex)
			{
				Debug.WriteLine("[ABOUT] Error opening about flyout: " + ex.Message);
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000214C File Offset: 0x0000034C
		private void OnPopupClosed(object sender, object e)
		{
			this._aboutPopup = null;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002158 File Offset: 0x00000358
		[DebuggerNonUserCode]
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this._contentLoaded)
			{
				this._contentLoaded = true;
				Application.LoadComponent(this, new Uri("ms-appx:///AboutFlyout.xaml"), 0);
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000218E File Offset: 0x0000038E
		[DebuggerNonUserCode]
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		public void Connect(int connectionId, object target)
		{
			this._contentLoaded = true;
		}

		// Token: 0x04000001 RID: 1
		private Popup _aboutPopup;

		// Token: 0x04000002 RID: 2
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
