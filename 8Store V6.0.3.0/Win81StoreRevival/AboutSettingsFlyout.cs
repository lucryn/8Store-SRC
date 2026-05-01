using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

namespace Win81StoreRevival
{
	// Token: 0x0200000B RID: 11
	public sealed class AboutSettingsFlyout : SettingsFlyout, IComponentConnector
	{
		// Token: 0x06000037 RID: 55 RVA: 0x00002C84 File Offset: 0x00000E84
		public AboutSettingsFlyout()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002C98 File Offset: 0x00000E98
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			bool contentLoaded = this._contentLoaded;
			if (!contentLoaded)
			{
				this._contentLoaded = true;
				Application.LoadComponent(this, new Uri("ms-appx:///AboutSettingsFlyout.xaml"), 0);
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002CCB File Offset: 0x00000ECB
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void Connect(int connectionId, object target)
		{
			this._contentLoaded = true;
		}

		// Token: 0x04000015 RID: 21
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
