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
	// Token: 0x0200002A RID: 42
	public sealed class FactoryResetFlyout : SettingsFlyout, IComponentConnector
	{
		// Token: 0x06000258 RID: 600 RVA: 0x0000DDF2 File Offset: 0x0000BFF2
		public FactoryResetFlyout()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000DE04 File Offset: 0x0000C004
		[DebuggerStepThrough]
		private void FactoryResetButton_Click(object sender, RoutedEventArgs e)
		{
			FactoryResetFlyout.<FactoryResetButton_Click>d__1 <FactoryResetButton_Click>d__ = new FactoryResetFlyout.<FactoryResetButton_Click>d__1();
			<FactoryResetButton_Click>d__.<>4__this = this;
			<FactoryResetButton_Click>d__.sender = sender;
			<FactoryResetButton_Click>d__.e = e;
			<FactoryResetButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<FactoryResetButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <FactoryResetButton_Click>d__.<>t__builder;
			<>t__builder.Start<FactoryResetFlyout.<FactoryResetButton_Click>d__1>(ref <FactoryResetButton_Click>d__);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000DE50 File Offset: 0x0000C050
		[DebuggerStepThrough]
		private Task PerformFactoryReset()
		{
			FactoryResetFlyout.<PerformFactoryReset>d__2 <PerformFactoryReset>d__ = new FactoryResetFlyout.<PerformFactoryReset>d__2();
			<PerformFactoryReset>d__.<>4__this = this;
			<PerformFactoryReset>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<PerformFactoryReset>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <PerformFactoryReset>d__.<>t__builder;
			<>t__builder.Start<FactoryResetFlyout.<PerformFactoryReset>d__2>(ref <PerformFactoryReset>d__);
			return <PerformFactoryReset>d__.<>t__builder.Task;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00004A93 File Offset: 0x00002C93
		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			base.Hide();
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000DE98 File Offset: 0x0000C098
		private void PlayResetSound()
		{
			try
			{
				MediaElement mediaElement = new MediaElement();
				mediaElement.put_Source(new Uri("ms-appx:///Assets/Click.wav"));
				mediaElement.put_AutoPlay(true);
				mediaElement.put_Visibility(1);
				Grid grid = base.Content as Grid;
				bool flag = grid != null;
				if (flag)
				{
					grid.Children.Add(mediaElement);
					MediaElement mediaElement2 = mediaElement;
					WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(mediaElement2.add_MediaEnded), new Action<EventRegistrationToken>(mediaElement2.remove_MediaEnded), delegate(object sender, RoutedEventArgs e)
					{
						grid.Children.Remove(mediaElement);
					});
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error playing reset sound: " + ex.Message);
			}
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000DF80 File Offset: 0x0000C180
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			bool contentLoaded = this._contentLoaded;
			if (!contentLoaded)
			{
				this._contentLoaded = true;
				Application.LoadComponent(this, new Uri("ms-appx:///FactoryResetFlyout.xaml"), 0);
				this.FactoryResetButton = (Button)base.FindName("FactoryResetButton");
				this.CancelButton = (Button)base.FindName("CancelButton");
				this.StatusMessage = (TextBlock)base.FindName("StatusMessage");
				this.LoadingPanel = (StackPanel)base.FindName("LoadingPanel");
				this.LoadingRing = (ProgressRing)base.FindName("LoadingRing");
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000E024 File Offset: 0x0000C224
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

		// Token: 0x04000128 RID: 296
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button FactoryResetButton;

		// Token: 0x04000129 RID: 297
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button CancelButton;

		// Token: 0x0400012A RID: 298
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock StatusMessage;

		// Token: 0x0400012B RID: 299
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel LoadingPanel;

		// Token: 0x0400012C RID: 300
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ProgressRing LoadingRing;

		// Token: 0x0400012D RID: 301
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
