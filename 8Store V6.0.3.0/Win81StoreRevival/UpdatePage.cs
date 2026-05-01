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
	// Token: 0x02000040 RID: 64
	public sealed class UpdatePage : Page, IComponentConnector
	{
		// Token: 0x060003BA RID: 954 RVA: 0x0001660F File Offset: 0x0001480F
		public UpdatePage()
		{
			this.InitializeComponent();
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000615E File Offset: 0x0000435E
		private void PlayUpdateSound()
		{
			SoundManager.PlayNotificationSound();
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00016630 File Offset: 0x00014830
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);
			bool flag = e.Parameter is UpdateNavigationData;
			if (flag)
			{
				UpdateNavigationData updateNavigationData = (UpdateNavigationData)e.Parameter;
				this._versionInfo = updateNavigationData.VersionInfo;
				this._currentVersion = updateNavigationData.CurrentVersion;
				this.PlayUpdateSound();
				this.DisplayUpdateInfo();
				this.StartEntranceAnimations();
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00016694 File Offset: 0x00014894
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
					break;
				case 2:
					this.ReleaseNotesEntranceAnimation.Begin();
					break;
				case 3:
					this.ButtonsEntranceAnimation.Begin();
					this._entranceTimer.Stop();
					break;
				}
			});
			this._entranceTimer.Start();
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00016790 File Offset: 0x00014990
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

		// Token: 0x060003BF RID: 959 RVA: 0x00016818 File Offset: 0x00014A18
		private void StopDotsAnimation()
		{
			bool flag = this._dotsAnimationTimer != null;
			if (flag)
			{
				this._dotsAnimationTimer.Stop();
				this._dotsAnimationTimer = null;
			}
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00016848 File Offset: 0x00014A48
		private void DisplayUpdateInfo()
		{
			this._originalTitle = this.UpdateTitle.Text;
			this._originalDescription = this.UpdateDescription.Text;
			bool flag = !string.IsNullOrEmpty(this._versionInfo.ReleaseNotes);
			if (flag)
			{
				this.ReleaseNotesText.put_Text(this._versionInfo.ReleaseNotes);
			}
			else
			{
				this.ReleaseNotesText.put_Text("• Bug fixes and performance improvements\n• Enhanced user experience\n• Updated app database");
			}
			bool forceUpdate = this._versionInfo.ForceUpdate;
			if (forceUpdate)
			{
				this.UpdatePrompt.put_Text("This update is REQUIRED to continue using 8Store!");
				this.UpdatePrompt.put_Foreground(new SolidColorBrush(Color.FromArgb(byte.MaxValue, 216, 0, 39)));
				this.UpdateTitle.put_Text("Update Required!");
				this._originalTitle = this.UpdateTitle.Text;
				this.RemindLaterButton.put_IsEnabled(false);
				this.RemindLaterButton.put_Content("Update Required");
				this.RemindLaterButton.put_Background(new SolidColorBrush(Color.FromArgb(byte.MaxValue, 150, 150, 150)));
			}
			else
			{
				this.UpdatePrompt.put_Text("Would you like to Update 8Store to latest version now?");
				this.UpdatePrompt.put_Foreground(new SolidColorBrush(Color.FromArgb(byte.MaxValue, 0, 0, 0)));
			}
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x000169A4 File Offset: 0x00014BA4
		[DebuggerStepThrough]
		private void UpdateNowButton_Click(object sender, RoutedEventArgs e)
		{
			UpdatePage.<UpdateNowButton_Click>d__16 <UpdateNowButton_Click>d__ = new UpdatePage.<UpdateNowButton_Click>d__16();
			<UpdateNowButton_Click>d__.<>4__this = this;
			<UpdateNowButton_Click>d__.sender = sender;
			<UpdateNowButton_Click>d__.e = e;
			<UpdateNowButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<UpdateNowButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <UpdateNowButton_Click>d__.<>t__builder;
			<>t__builder.Start<UpdatePage.<UpdateNowButton_Click>d__16>(ref <UpdateNowButton_Click>d__);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x000169F0 File Offset: 0x00014BF0
		[DebuggerStepThrough]
		private Task DownloadAndInstallUpdate(AppVersionInfo versionInfo)
		{
			UpdatePage.<DownloadAndInstallUpdate>d__17 <DownloadAndInstallUpdate>d__ = new UpdatePage.<DownloadAndInstallUpdate>d__17();
			<DownloadAndInstallUpdate>d__.<>4__this = this;
			<DownloadAndInstallUpdate>d__.versionInfo = versionInfo;
			<DownloadAndInstallUpdate>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DownloadAndInstallUpdate>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <DownloadAndInstallUpdate>d__.<>t__builder;
			<>t__builder.Start<UpdatePage.<DownloadAndInstallUpdate>d__17>(ref <DownloadAndInstallUpdate>d__);
			return <DownloadAndInstallUpdate>d__.<>t__builder.Task;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00016A40 File Offset: 0x00014C40
		[DebuggerStepThrough]
		private Task ReportDownloadProgressAsync()
		{
			UpdatePage.<ReportDownloadProgressAsync>d__18 <ReportDownloadProgressAsync>d__ = new UpdatePage.<ReportDownloadProgressAsync>d__18();
			<ReportDownloadProgressAsync>d__.<>4__this = this;
			<ReportDownloadProgressAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ReportDownloadProgressAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <ReportDownloadProgressAsync>d__.<>t__builder;
			<>t__builder.Start<UpdatePage.<ReportDownloadProgressAsync>d__18>(ref <ReportDownloadProgressAsync>d__);
			return <ReportDownloadProgressAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00016A88 File Offset: 0x00014C88
		[DebuggerStepThrough]
		private void CancelDownloadButton_Click(object sender, RoutedEventArgs e)
		{
			UpdatePage.<CancelDownloadButton_Click>d__19 <CancelDownloadButton_Click>d__ = new UpdatePage.<CancelDownloadButton_Click>d__19();
			<CancelDownloadButton_Click>d__.<>4__this = this;
			<CancelDownloadButton_Click>d__.sender = sender;
			<CancelDownloadButton_Click>d__.e = e;
			<CancelDownloadButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<CancelDownloadButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <CancelDownloadButton_Click>d__.<>t__builder;
			<>t__builder.Start<UpdatePage.<CancelDownloadButton_Click>d__19>(ref <CancelDownloadButton_Click>d__);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00016AD4 File Offset: 0x00014CD4
		private void RemindLaterButton_Click(object sender, RoutedEventArgs e)
		{
			bool flag = !this._versionInfo.ForceUpdate;
			if (flag)
			{
				bool canGoBack = base.Frame.CanGoBack;
				if (canGoBack)
				{
					base.Frame.GoBack();
				}
				else
				{
					base.Frame.Navigate(typeof(MainPage), "fromUpdatePage");
				}
			}
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00016B30 File Offset: 0x00014D30
		protected override void OnNavigatedFrom(NavigationEventArgs e)
		{
			base.OnNavigatedFrom(e);
			this.StopDotsAnimation();
			bool flag = this._entranceTimer != null;
			if (flag)
			{
				this._entranceTimer.Stop();
				this._entranceTimer = null;
			}
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00016B70 File Offset: 0x00014D70
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			bool contentLoaded = this._contentLoaded;
			if (!contentLoaded)
			{
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
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00016DB8 File Offset: 0x00014FB8
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

		// Token: 0x040001C1 RID: 449
		private AppVersionInfo _versionInfo;

		// Token: 0x040001C2 RID: 450
		private string _currentVersion;

		// Token: 0x040001C3 RID: 451
		private DownloadOperation _downloadOperation;

		// Token: 0x040001C4 RID: 452
		private bool _isDownloading = false;

		// Token: 0x040001C5 RID: 453
		private DispatcherTimer _entranceTimer;

		// Token: 0x040001C6 RID: 454
		private DispatcherTimer _dotsAnimationTimer;

		// Token: 0x040001C7 RID: 455
		private string _originalTitle;

		// Token: 0x040001C8 RID: 456
		private string _originalDescription;

		// Token: 0x040001C9 RID: 457
		private bool _hasCheckedForUpdates = false;

		// Token: 0x040001CA RID: 458
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Storyboard TitleEntranceAnimation;

		// Token: 0x040001CB RID: 459
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Storyboard DescriptionEntranceAnimation;

		// Token: 0x040001CC RID: 460
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Storyboard ReleaseNotesEntranceAnimation;

		// Token: 0x040001CD RID: 461
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Storyboard ButtonsEntranceAnimation;

		// Token: 0x040001CE RID: 462
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Storyboard DownloadStartAnimation;

		// Token: 0x040001CF RID: 463
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Storyboard ErrorRestoreAnimation;

		// Token: 0x040001D0 RID: 464
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock UpdateTitle;

		// Token: 0x040001D1 RID: 465
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock UpdateDescription;

		// Token: 0x040001D2 RID: 466
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ScrollViewer ReleaseNotesScrollViewer;

		// Token: 0x040001D3 RID: 467
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel ButtonsPanel;

		// Token: 0x040001D4 RID: 468
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel DownloadProgressPanel;

		// Token: 0x040001D5 RID: 469
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private MediaElement SoundPlayer;

		// Token: 0x040001D6 RID: 470
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock DownloadStatusText;

		// Token: 0x040001D7 RID: 471
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock DownloadDetailsText;

		// Token: 0x040001D8 RID: 472
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button CancelDownloadButton;

		// Token: 0x040001D9 RID: 473
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ProgressBar DownloadProgressBar;

		// Token: 0x040001DA RID: 474
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock ProgressText;

		// Token: 0x040001DB RID: 475
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock Dot1;

		// Token: 0x040001DC RID: 476
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock Dot2;

		// Token: 0x040001DD RID: 477
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock Dot3;

		// Token: 0x040001DE RID: 478
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock UpdatePrompt;

		// Token: 0x040001DF RID: 479
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button UpdateNowButton;

		// Token: 0x040001E0 RID: 480
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button RemindLaterButton;

		// Token: 0x040001E1 RID: 481
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock ReleaseNotesText;

		// Token: 0x040001E2 RID: 482
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
