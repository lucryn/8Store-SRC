using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Networking.BackgroundTransfer;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace Win81StoreRevival
{
	// Token: 0x02000046 RID: 70
	public sealed class UpdatePage : Page, IComponentConnector
	{
		// Token: 0x0600049E RID: 1182 RVA: 0x00017165 File Offset: 0x00015365
		public UpdatePage()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00017174 File Offset: 0x00015374
		private void PlayUpdateSound()
		{
			try
			{
				MediaElement soundPlayer = new MediaElement();
				soundPlayer.put_AutoPlay(true);
				soundPlayer.put_Volume(0.5);
				soundPlayer.put_Visibility(1);
				Grid grid = base.Content as Grid;
				if (grid != null)
				{
					grid.Children.Add(soundPlayer);
				}
				soundPlayer.put_Source(new Uri("ms-appx:///Assets/notify1.wav"));
				MediaElement soundPlayer2 = soundPlayer;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(soundPlayer2.add_MediaEnded), new Action<EventRegistrationToken>(soundPlayer2.remove_MediaEnded), delegate(object sender, RoutedEventArgs e)
				{
					Grid grid2 = this.Content as Grid;
					if (grid2 != null)
					{
						grid2.Children.Remove(soundPlayer);
					}
				});
				soundPlayer2 = soundPlayer;
				WindowsRuntimeMarshal.AddEventHandler<ExceptionRoutedEventHandler>(new Func<ExceptionRoutedEventHandler, EventRegistrationToken>(soundPlayer2.add_MediaFailed), new Action<EventRegistrationToken>(soundPlayer2.remove_MediaFailed), delegate(object sender, ExceptionRoutedEventArgs e)
				{
					Grid grid2 = this.Content as Grid;
					if (grid2 != null)
					{
						grid2.Children.Remove(soundPlayer);
					}
				});
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00017274 File Offset: 0x00015474
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);
			if (e.Parameter is UpdateNavigationData)
			{
				UpdateNavigationData updateNavigationData = (UpdateNavigationData)e.Parameter;
				this._versionInfo = updateNavigationData.VersionInfo;
				this._currentVersion = updateNavigationData.CurrentVersion;
				this.PlayUpdateSound();
				this.DisplayUpdateInfo();
				this.StartEntranceAnimations();
			}
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x000172CC File Offset: 0x000154CC
		private void StartEntranceAnimations()
		{
			this.UpdateTitle.put_Opacity(0.0);
			this.UpdateDescription.put_Opacity(0.0);
			this.ReleaseNotesScrollViewer.put_Opacity(0.0);
			this.ButtonsPanel.put_Opacity(0.0);
			this.DownloadProgressPanel.put_Opacity(0.0);
			this.TitleEntranceAnimation.Begin();
			this._entranceTimer = new DispatcherTimer();
			this._entranceTimer.put_Interval(TimeSpan.FromMilliseconds(250.0));
			int animationStep = 0;
			DispatcherTimer entranceTimer = this._entranceTimer;
			WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(entranceTimer.add_Tick), new Action<EventRegistrationToken>(entranceTimer.remove_Tick), delegate(object s, object args)
			{
				int animationStep = animationStep;
				animationStep++;
				switch (animationStep)
				{
				case 1:
					this.DescriptionEntranceAnimation.Begin();
					return;
				case 2:
					this.ReleaseNotesEntranceAnimation.Begin();
					return;
				case 3:
					this.ButtonsEntranceAnimation.Begin();
					this._entranceTimer.Stop();
					return;
				default:
					return;
				}
			});
			this._entranceTimer.Start();
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x000173C0 File Offset: 0x000155C0
		private void StartDotsAnimation()
		{
			this._dotsAnimationTimer = new DispatcherTimer();
			this._dotsAnimationTimer.put_Interval(TimeSpan.FromMilliseconds(500.0));
			int dotStep = 0;
			DispatcherTimer dotsAnimationTimer = this._dotsAnimationTimer;
			WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(dotsAnimationTimer.add_Tick), new Action<EventRegistrationToken>(dotsAnimationTimer.remove_Tick), delegate(object s, object args)
			{
				this.Dot1.put_Opacity(0.3);
				this.Dot2.put_Opacity(0.3);
				this.Dot3.put_Opacity(0.3);
				switch (dotStep)
				{
				case 0:
					this.Dot1.put_Opacity(1.0);
					break;
				case 1:
					this.Dot2.put_Opacity(1.0);
					break;
				case 2:
					this.Dot3.put_Opacity(1.0);
					break;
				}
				dotStep = (dotStep + 1) % 3;
			});
			this._dotsAnimationTimer.Start();
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00017442 File Offset: 0x00015642
		private void StopDotsAnimation()
		{
			if (this._dotsAnimationTimer != null)
			{
				this._dotsAnimationTimer.Stop();
				this._dotsAnimationTimer = null;
			}
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00017460 File Offset: 0x00015660
		private void DisplayUpdateInfo()
		{
			this._originalTitle = this.UpdateTitle.Text;
			this._originalDescription = this.UpdateDescription.Text;
			if (!string.IsNullOrEmpty(this._versionInfo.ReleaseNotes))
			{
				this.ReleaseNotesText.put_Text(this._versionInfo.ReleaseNotes);
			}
			else
			{
				this.ReleaseNotesText.put_Text("• Bug fixes and performance improvements\n• Enhanced user experience\n• Updated app database");
			}
			if (this._versionInfo.ForceUpdate)
			{
				this.UpdatePrompt.put_Text("This update is REQUIRED to continue using 8Store!");
				this.UpdatePrompt.put_Foreground(new SolidColorBrush(Color.FromArgb(byte.MaxValue, 216, 0, 39)));
				this.UpdateTitle.put_Text("Update Required!");
				this._originalTitle = this.UpdateTitle.Text;
				this.RemindLaterButton.put_IsEnabled(false);
				this.RemindLaterButton.put_Content("Update Required");
				this.RemindLaterButton.put_Background(new SolidColorBrush(Color.FromArgb(byte.MaxValue, 150, 150, 150)));
				return;
			}
			this.UpdatePrompt.put_Text("Would you like to Update 8Store to latest version now?");
			this.UpdatePrompt.put_Foreground(new SolidColorBrush(Color.FromArgb(byte.MaxValue, 0, 0, 0)));
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x000175A0 File Offset: 0x000157A0
		private void UpdateNowButton_Click(object sender, RoutedEventArgs e)
		{
			UpdatePage.<UpdateNowButton_Click>d__16 <UpdateNowButton_Click>d__;
			<UpdateNowButton_Click>d__.<>4__this = this;
			<UpdateNowButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<UpdateNowButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <UpdateNowButton_Click>d__.<>t__builder;
			<>t__builder.Start<UpdatePage.<UpdateNowButton_Click>d__16>(ref <UpdateNowButton_Click>d__);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x000175DC File Offset: 0x000157DC
		private Task DownloadAndInstallUpdate(AppVersionInfo versionInfo)
		{
			UpdatePage.<DownloadAndInstallUpdate>d__17 <DownloadAndInstallUpdate>d__;
			<DownloadAndInstallUpdate>d__.<>4__this = this;
			<DownloadAndInstallUpdate>d__.versionInfo = versionInfo;
			<DownloadAndInstallUpdate>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DownloadAndInstallUpdate>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <DownloadAndInstallUpdate>d__.<>t__builder;
			<>t__builder.Start<UpdatePage.<DownloadAndInstallUpdate>d__17>(ref <DownloadAndInstallUpdate>d__);
			return <DownloadAndInstallUpdate>d__.<>t__builder.Task;
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0001762C File Offset: 0x0001582C
		private Task ReportDownloadProgressAsync()
		{
			UpdatePage.<ReportDownloadProgressAsync>d__18 <ReportDownloadProgressAsync>d__;
			<ReportDownloadProgressAsync>d__.<>4__this = this;
			<ReportDownloadProgressAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ReportDownloadProgressAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <ReportDownloadProgressAsync>d__.<>t__builder;
			<>t__builder.Start<UpdatePage.<ReportDownloadProgressAsync>d__18>(ref <ReportDownloadProgressAsync>d__);
			return <ReportDownloadProgressAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00017674 File Offset: 0x00015874
		private void CancelDownloadButton_Click(object sender, RoutedEventArgs e)
		{
			UpdatePage.<CancelDownloadButton_Click>d__19 <CancelDownloadButton_Click>d__;
			<CancelDownloadButton_Click>d__.<>4__this = this;
			<CancelDownloadButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<CancelDownloadButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <CancelDownloadButton_Click>d__.<>t__builder;
			<>t__builder.Start<UpdatePage.<CancelDownloadButton_Click>d__19>(ref <CancelDownloadButton_Click>d__);
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x000176B0 File Offset: 0x000158B0
		private void RemindLaterButton_Click(object sender, RoutedEventArgs e)
		{
			if (!this._versionInfo.ForceUpdate)
			{
				if (base.Frame.CanGoBack)
				{
					base.Frame.GoBack();
					return;
				}
				base.Frame.Navigate(typeof(MainPage), "fromUpdatePage");
			}
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x000176FE File Offset: 0x000158FE
		protected override void OnNavigatedFrom(NavigationEventArgs e)
		{
			base.OnNavigatedFrom(e);
			this.StopDotsAnimation();
			if (this._entranceTimer != null)
			{
				this._entranceTimer.Stop();
				this._entranceTimer = null;
			}
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00017728 File Offset: 0x00015928
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Application.LoadComponent(this, new Uri("ms-appx:///UpdatePage.xaml"), 0);
			this.TitleEntranceAnimation = (Storyboard)base.FindName("TitleEntranceAnimation");
			this.DescriptionEntranceAnimation = (Storyboard)base.FindName("DescriptionEntranceAnimation");
			this.ReleaseNotesEntranceAnimation = (Storyboard)base.FindName("ReleaseNotesEntranceAnimation");
			this.ButtonsEntranceAnimation = (Storyboard)base.FindName("ButtonsEntranceAnimation");
			this.DownloadStartAnimation = (Storyboard)base.FindName("DownloadStartAnimation");
			this.ErrorRestoreAnimation = (Storyboard)base.FindName("ErrorRestoreAnimation");
			this.UpdateTitle = (TextBlock)base.FindName("UpdateTitle");
			this.UpdateDescription = (TextBlock)base.FindName("UpdateDescription");
			this.ReleaseNotesScrollViewer = (ScrollViewer)base.FindName("ReleaseNotesScrollViewer");
			this.ButtonsPanel = (StackPanel)base.FindName("ButtonsPanel");
			this.DownloadProgressPanel = (StackPanel)base.FindName("DownloadProgressPanel");
			this.SoundPlayer = (MediaElement)base.FindName("SoundPlayer");
			this.DownloadStatusText = (TextBlock)base.FindName("DownloadStatusText");
			this.DownloadDetailsText = (TextBlock)base.FindName("DownloadDetailsText");
			this.CancelDownloadButton = (Button)base.FindName("CancelDownloadButton");
			this.DownloadProgressBar = (ProgressBar)base.FindName("DownloadProgressBar");
			this.ProgressText = (TextBlock)base.FindName("ProgressText");
			this.Dot1 = (TextBlock)base.FindName("Dot1");
			this.Dot2 = (TextBlock)base.FindName("Dot2");
			this.Dot3 = (TextBlock)base.FindName("Dot3");
			this.UpdatePrompt = (TextBlock)base.FindName("UpdatePrompt");
			this.UpdateNowButton = (Button)base.FindName("UpdateNowButton");
			this.RemindLaterButton = (Button)base.FindName("RemindLaterButton");
			this.ReleaseNotesText = (TextBlock)base.FindName("ReleaseNotesText");
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00017968 File Offset: 0x00015B68
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.CancelDownloadButton_Click));
				break;
			}
			case 2:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.UpdateNowButton_Click));
				break;
			}
			case 3:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.RemindLaterButton_Click));
				break;
			}
			}
			this._contentLoaded = true;
		}

		// Token: 0x04000200 RID: 512
		private AppVersionInfo _versionInfo;

		// Token: 0x04000201 RID: 513
		private string _currentVersion;

		// Token: 0x04000202 RID: 514
		private DownloadOperation _downloadOperation;

		// Token: 0x04000203 RID: 515
		private bool _isDownloading;

		// Token: 0x04000204 RID: 516
		private DispatcherTimer _entranceTimer;

		// Token: 0x04000205 RID: 517
		private DispatcherTimer _dotsAnimationTimer;

		// Token: 0x04000206 RID: 518
		private string _originalTitle;

		// Token: 0x04000207 RID: 519
		private string _originalDescription;

		// Token: 0x04000208 RID: 520
		private bool _hasCheckedForUpdates;

		// Token: 0x04000209 RID: 521
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Storyboard TitleEntranceAnimation;

		// Token: 0x0400020A RID: 522
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Storyboard DescriptionEntranceAnimation;

		// Token: 0x0400020B RID: 523
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Storyboard ReleaseNotesEntranceAnimation;

		// Token: 0x0400020C RID: 524
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Storyboard ButtonsEntranceAnimation;

		// Token: 0x0400020D RID: 525
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Storyboard DownloadStartAnimation;

		// Token: 0x0400020E RID: 526
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Storyboard ErrorRestoreAnimation;

		// Token: 0x0400020F RID: 527
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock UpdateTitle;

		// Token: 0x04000210 RID: 528
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock UpdateDescription;

		// Token: 0x04000211 RID: 529
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ScrollViewer ReleaseNotesScrollViewer;

		// Token: 0x04000212 RID: 530
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel ButtonsPanel;

		// Token: 0x04000213 RID: 531
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel DownloadProgressPanel;

		// Token: 0x04000214 RID: 532
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private MediaElement SoundPlayer;

		// Token: 0x04000215 RID: 533
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock DownloadStatusText;

		// Token: 0x04000216 RID: 534
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock DownloadDetailsText;

		// Token: 0x04000217 RID: 535
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button CancelDownloadButton;

		// Token: 0x04000218 RID: 536
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ProgressBar DownloadProgressBar;

		// Token: 0x04000219 RID: 537
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock ProgressText;

		// Token: 0x0400021A RID: 538
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock Dot1;

		// Token: 0x0400021B RID: 539
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock Dot2;

		// Token: 0x0400021C RID: 540
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock Dot3;

		// Token: 0x0400021D RID: 541
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock UpdatePrompt;

		// Token: 0x0400021E RID: 542
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button UpdateNowButton;

		// Token: 0x0400021F RID: 543
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button RemindLaterButton;

		// Token: 0x04000220 RID: 544
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock ReleaseNotesText;

		// Token: 0x04000221 RID: 545
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
