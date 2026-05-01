using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;

namespace Win81StoreRevival
{
	// Token: 0x0200002D RID: 45
	public sealed class FactoryResetFlyout : SettingsFlyout, IComponentConnector
	{
		// Token: 0x060002D6 RID: 726 RVA: 0x0000EC69 File Offset: 0x0000CE69
		public FactoryResetFlyout()
		{
			this.InitializeComponent();
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000EC78 File Offset: 0x0000CE78
		private void FactoryResetButton_Click(object sender, RoutedEventArgs e)
		{
			FactoryResetFlyout.<FactoryResetButton_Click>d__1 <FactoryResetButton_Click>d__;
			<FactoryResetButton_Click>d__.<>4__this = this;
			<FactoryResetButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<FactoryResetButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <FactoryResetButton_Click>d__.<>t__builder;
			<>t__builder.Start<FactoryResetFlyout.<FactoryResetButton_Click>d__1>(ref <FactoryResetButton_Click>d__);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000ECB4 File Offset: 0x0000CEB4
		private Task PerformFactoryReset()
		{
			FactoryResetFlyout.<PerformFactoryReset>d__2 <PerformFactoryReset>d__;
			<PerformFactoryReset>d__.<>4__this = this;
			<PerformFactoryReset>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<PerformFactoryReset>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <PerformFactoryReset>d__.<>t__builder;
			<>t__builder.Start<FactoryResetFlyout.<PerformFactoryReset>d__2>(ref <PerformFactoryReset>d__);
			return <PerformFactoryReset>d__.<>t__builder.Task;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00005282 File Offset: 0x00003482
		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			base.Hide();
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000ECFC File Offset: 0x0000CEFC
		private void PlayResetSound()
		{
			try
			{
				MediaElement mediaElement = new MediaElement();
				mediaElement.put_Source(new Uri("ms-appx:///Assets/Click.wav"));
				mediaElement.put_AutoPlay(true);
				mediaElement.put_Visibility(1);
				Grid grid = base.Content as Grid;
				if (grid != null)
				{
					grid.Children.Add(mediaElement);
					MediaElement mediaElement2 = mediaElement;
					WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(mediaElement2.add_MediaEnded), new Action<EventRegistrationToken>(mediaElement2.remove_MediaEnded), delegate(object sender, RoutedEventArgs e)
					{
						grid.Children.Remove(mediaElement);
					});
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000EDC0 File Offset: 0x0000CFC0
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Application.LoadComponent(this, new Uri("ms-appx:///FactoryResetFlyout.xaml"), 0);
			this.FactoryResetButton = (Button)base.FindName("FactoryResetButton");
			this.CancelButton = (Button)base.FindName("CancelButton");
			this.StatusMessage = (TextBlock)base.FindName("StatusMessage");
			this.LoadingPanel = (StackPanel)base.FindName("LoadingPanel");
			this.LoadingRing = (ProgressRing)base.FindName("LoadingRing");
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000EE5C File Offset: 0x0000D05C
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void Connect(int connectionId, object target)
		{
			if (connectionId != 1)
			{
				if (connectionId == 2)
				{
					ButtonBase buttonBase = (ButtonBase)target;
					WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.CancelButton_Click));
				}
			}
			else
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.FactoryResetButton_Click));
			}
			this._contentLoaded = true;
		}

		// Token: 0x04000132 RID: 306
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button FactoryResetButton;

		// Token: 0x04000133 RID: 307
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button CancelButton;

		// Token: 0x04000134 RID: 308
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock StatusMessage;

		// Token: 0x04000135 RID: 309
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel LoadingPanel;

		// Token: 0x04000136 RID: 310
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ProgressRing LoadingRing;

		// Token: 0x04000137 RID: 311
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
