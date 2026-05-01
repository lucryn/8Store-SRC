using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace W80StoreRevival
{
	// Token: 0x02000013 RID: 19
	public sealed class UpdatePage : Page, IComponentConnector
	{
		// Token: 0x060000BE RID: 190 RVA: 0x0000C9C0 File Offset: 0x0000ABC0
		public UpdatePage()
		{
			this.InitializeComponent();
			this._httpClient = new HttpClient();
			this.LoadUpdateInfo();
			this.StartAnimations();
			this.StartDotsAnimation();
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000CA14 File Offset: 0x0000AC14
		private void LoadUpdateInfo()
		{
			try
			{
				ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
				if (localSettings.Values.ContainsKey("UpdateLatestVersion"))
				{
					string text = localSettings.Values["UpdateLatestVersion"] as string;
					string text2 = localSettings.Values["UpdateReleaseNotes"] as string;
					this.UpdateTitle.put_Text("Update Available!");
					this.UpdateDescription.put_Text("Version " + text + " contains these changes:");
					this.ReleaseNotesText.put_Text(text2);
					if (localSettings.Values.ContainsKey("UpdateForceUpdate"))
					{
						this._isForceUpdate = (bool)localSettings.Values["UpdateForceUpdate"];
						if (this._isForceUpdate)
						{
							this.RemindLaterButton.put_Visibility(1);
							this.UpdatePrompt.put_Text("This update is required. Please update now.");
						}
					}
				}
				else
				{
					this.UpdateTitle.put_Text("Update Check");
					this.UpdateDescription.put_Text("No update information available.");
					this.ReleaseNotesText.put_Text("Please check your internet connection and try again.");
					this.UpdateNowButton.put_IsEnabled(false);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("[UPDATEPAGE] Error loading update info: " + ex.Message);
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000CB98 File Offset: 0x0000AD98
		private void StartAnimations()
		{
			this.TitleEntranceAnimation.Begin();
			this.DescriptionEntranceAnimation.Begin();
			this.ReleaseNotesEntranceAnimation.Begin();
			this.ButtonsEntranceAnimation.Begin();
			this.SoundPlayer.Play();
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000CBD8 File Offset: 0x0000ADD8
		private void StartDotsAnimation()
		{
			this._dotsTimer = new DispatcherTimer();
			this._dotsTimer.put_Interval(TimeSpan.FromMilliseconds(300.0));
			DispatcherTimer dotsTimer = this._dotsTimer;
			WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(dotsTimer.add_Tick), new Action<EventRegistrationToken>(dotsTimer.remove_Tick), new EventHandler<object>(this.DotsTimer_Tick));
			this._dotsTimer.Start();
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000CC48 File Offset: 0x0000AE48
		private void DotsTimer_Tick(object sender, object e)
		{
			this.Dot1.put_Opacity(0.3);
			this.Dot2.put_Opacity(0.3);
			this.Dot3.put_Opacity(0.3);
			switch (this._currentDot)
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
			this._currentDot = (this._currentDot + 1) % 3;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000CF10 File Offset: 0x0000B110
		[DebuggerStepThrough]
		private void UpdateNowButton_Click(object sender, RoutedEventArgs e)
		{
			UpdatePage.<UpdateNowButton_Click>d__0 <UpdateNowButton_Click>d__;
			<UpdateNowButton_Click>d__.<>4__this = this;
			<UpdateNowButton_Click>d__.sender = sender;
			<UpdateNowButton_Click>d__.e = e;
			<UpdateNowButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<UpdateNowButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <UpdateNowButton_Click>d__.<>t__builder;
			<>t__builder.Start<UpdatePage.<UpdateNowButton_Click>d__0>(ref <UpdateNowButton_Click>d__);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000DC50 File Offset: 0x0000BE50
		[DebuggerStepThrough]
		private Task DownloadUpdateAsync(string url)
		{
			UpdatePage.<DownloadUpdateAsync>d__c <DownloadUpdateAsync>d__c;
			<DownloadUpdateAsync>d__c.<>4__this = this;
			<DownloadUpdateAsync>d__c.url = url;
			<DownloadUpdateAsync>d__c.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DownloadUpdateAsync>d__c.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <DownloadUpdateAsync>d__c.<>t__builder;
			<>t__builder.Start<UpdatePage.<DownloadUpdateAsync>d__c>(ref <DownloadUpdateAsync>d__c);
			return <DownloadUpdateAsync>d__c.<>t__builder.Task;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000E03C File Offset: 0x0000C23C
		[DebuggerStepThrough]
		private Task OpenDownloadedFile()
		{
			UpdatePage.<OpenDownloadedFile>d__23 <OpenDownloadedFile>d__;
			<OpenDownloadedFile>d__.<>4__this = this;
			<OpenDownloadedFile>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<OpenDownloadedFile>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <OpenDownloadedFile>d__.<>t__builder;
			<>t__builder.Start<UpdatePage.<OpenDownloadedFile>d__23>(ref <OpenDownloadedFile>d__);
			return <OpenDownloadedFile>d__.<>t__builder.Task;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000E088 File Offset: 0x0000C288
		private string FormatBytes(long bytes)
		{
			string[] array = new string[]
			{
				"B",
				"KB",
				"MB",
				"GB"
			};
			int num = 0;
			decimal num2 = bytes;
			while (Math.Round(num2 / 1024m) >= 1m)
			{
				num2 /= 1024m;
				num++;
			}
			return string.Format("{0:n1} {1}", new object[]
			{
				num2,
				array[num]
			});
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000E138 File Offset: 0x0000C338
		private void RemindLaterButton_Click(object sender, RoutedEventArgs e)
		{
			if (this._isForceUpdate)
			{
				MessageDialog messageDialog = new MessageDialog("This update is required. You must update to continue using 8Store.", "Force Update Required");
				messageDialog.ShowAsync();
			}
			else
			{
				Debug.WriteLine("[UPDATEPAGE] Remind later clicked");
				Frame frame = Window.Current.Content as Frame;
				if (frame != null && frame.CanGoBack)
				{
					frame.GoBack();
				}
				else
				{
					frame.Navigate(typeof(MainPage));
				}
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000E388 File Offset: 0x0000C588
		[DebuggerStepThrough]
		private void CancelDownloadButton_Click(object sender, RoutedEventArgs e)
		{
			UpdatePage.<CancelDownloadButton_Click>d__28 <CancelDownloadButton_Click>d__;
			<CancelDownloadButton_Click>d__.<>4__this = this;
			<CancelDownloadButton_Click>d__.sender = sender;
			<CancelDownloadButton_Click>d__.e = e;
			<CancelDownloadButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<CancelDownloadButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <CancelDownloadButton_Click>d__.<>t__builder;
			<>t__builder.Start<UpdatePage.<CancelDownloadButton_Click>d__28>(ref <CancelDownloadButton_Click>d__);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000E3D4 File Offset: 0x0000C5D4
		private void ShowError(string message)
		{
			this.ErrorRestoreAnimation.Begin();
			MessageDialog messageDialog = new MessageDialog(message, "Update Error");
			messageDialog.ShowAsync();
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000E404 File Offset: 0x0000C604
		protected override void OnNavigatedFrom(NavigationEventArgs e)
		{
			base.OnNavigatedFrom(e);
			if (this._dotsTimer != null)
			{
				this._dotsTimer.Stop();
				this._dotsTimer = null;
			}
			if (this._httpClient != null)
			{
				this._httpClient.Dispose();
				this._httpClient = null;
			}
			this._isDownloading = false;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000E468 File Offset: 0x0000C668
		[DebuggerNonUserCode]
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this._contentLoaded)
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
				this.DotsAnimationPanel = (StackPanel)base.FindName("DotsAnimationPanel");
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

		// Token: 0x060000CC RID: 204 RVA: 0x0000E6C8 File Offset: 0x0000C8C8
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

		// Token: 0x04000078 RID: 120
		private DispatcherTimer _dotsTimer;

		// Token: 0x04000079 RID: 121
		private int _currentDot = 0;

		// Token: 0x0400007A RID: 122
		private HttpClient _httpClient;

		// Token: 0x0400007B RID: 123
		private bool _isDownloading = false;

		// Token: 0x0400007C RID: 124
		private bool _isForceUpdate = false;

		// Token: 0x0400007D RID: 125
		private StorageFile _downloadedFile;

		// Token: 0x0400007E RID: 126
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Storyboard TitleEntranceAnimation;

		// Token: 0x0400007F RID: 127
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Storyboard DescriptionEntranceAnimation;

		// Token: 0x04000080 RID: 128
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Storyboard ReleaseNotesEntranceAnimation;

		// Token: 0x04000081 RID: 129
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Storyboard ButtonsEntranceAnimation;

		// Token: 0x04000082 RID: 130
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Storyboard DownloadStartAnimation;

		// Token: 0x04000083 RID: 131
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Storyboard ErrorRestoreAnimation;

		// Token: 0x04000084 RID: 132
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock UpdateTitle;

		// Token: 0x04000085 RID: 133
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock UpdateDescription;

		// Token: 0x04000086 RID: 134
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ScrollViewer ReleaseNotesScrollViewer;

		// Token: 0x04000087 RID: 135
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel ButtonsPanel;

		// Token: 0x04000088 RID: 136
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel DownloadProgressPanel;

		// Token: 0x04000089 RID: 137
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private MediaElement SoundPlayer;

		// Token: 0x0400008A RID: 138
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock DownloadStatusText;

		// Token: 0x0400008B RID: 139
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel DotsAnimationPanel;

		// Token: 0x0400008C RID: 140
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock DownloadDetailsText;

		// Token: 0x0400008D RID: 141
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button CancelDownloadButton;

		// Token: 0x0400008E RID: 142
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ProgressBar DownloadProgressBar;

		// Token: 0x0400008F RID: 143
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock ProgressText;

		// Token: 0x04000090 RID: 144
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock Dot1;

		// Token: 0x04000091 RID: 145
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock Dot2;

		// Token: 0x04000092 RID: 146
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock Dot3;

		// Token: 0x04000093 RID: 147
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock UpdatePrompt;

		// Token: 0x04000094 RID: 148
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button UpdateNowButton;

		// Token: 0x04000095 RID: 149
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button RemindLaterButton;

		// Token: 0x04000096 RID: 150
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock ReleaseNotesText;

		// Token: 0x04000097 RID: 151
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
