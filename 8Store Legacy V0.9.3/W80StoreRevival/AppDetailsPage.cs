using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace W80StoreRevival
{
	// Token: 0x02000006 RID: 6
	public sealed class AppDetailsPage : Page, IComponentConnector
	{
		// Token: 0x06000035 RID: 53 RVA: 0x00005BD4 File Offset: 0x00003DD4
		public AppDetailsPage()
		{
			try
			{
				this.InitializeComponent();
				this.httpClient = new HttpClient();
				this.httpClient.Timeout = TimeSpan.FromMinutes(5.0);
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error initializing AppDetailsPage: " + ex.Message);
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00005C50 File Offset: 0x00003E50
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);
			object parameter = e.Parameter;
			if (parameter is StoreApp)
			{
				this.currentApp = (StoreApp)parameter;
				this.LoadAppData();
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00005C94 File Offset: 0x00003E94
		private void LoadAppData()
		{
			if (this.currentApp != null)
			{
				if (this.AppTitle != null)
				{
					this.AppTitle.put_Text(this.currentApp.Name);
				}
				if (this.AppPublisher != null)
				{
					this.AppPublisher.put_Text(this.currentApp.Publisher);
				}
				if (this.DescriptionText != null)
				{
					this.DescriptionText.put_Text(this.currentApp.Description);
				}
				if (this.LatestVersion != null)
				{
					this.LatestVersion.put_Text(this.currentApp.Version);
				}
				if (this.AuthorName != null)
				{
					this.AuthorName.put_Text(this.currentApp.Publisher);
				}
				if (this.DownloadButton != null && !string.IsNullOrEmpty(this.currentApp.DownloadUrl))
				{
					try
					{
						Uri uri = new Uri(this.currentApp.DownloadUrl);
						string text = Path.GetFileName(uri.LocalPath);
						if (!string.IsNullOrEmpty(text))
						{
							text = WebUtility.UrlDecode(text);
							this.DownloadButton.put_Content("Download");
						}
						else
						{
							this.DownloadButton.put_Content("Download");
						}
					}
					catch
					{
						this.DownloadButton.put_Content("Download");
					}
				}
				if (this.AppIcon != null && !string.IsNullOrEmpty(this.currentApp.IconUrl))
				{
					try
					{
						this.AppIcon.put_Source(new BitmapImage(new Uri(this.currentApp.IconUrl)));
					}
					catch
					{
						this.AppIcon.put_Source(new BitmapImage(new Uri("ms-appx:///Assets/StoreLogo.png")));
					}
				}
				if (this.ScreenshotList != null)
				{
					this.LoadScreenshots();
				}
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00005E98 File Offset: 0x00004098
		private void LoadScreenshots()
		{
			this.ScreenshotList.Items.Clear();
			List<string> list = new List<string>();
			if (!string.IsNullOrEmpty(this.currentApp.Screenshot1))
			{
				list.Add(this.currentApp.Screenshot1);
			}
			if (!string.IsNullOrEmpty(this.currentApp.Screenshot2))
			{
				list.Add(this.currentApp.Screenshot2);
			}
			if (!string.IsNullOrEmpty(this.currentApp.Screenshot3))
			{
				list.Add(this.currentApp.Screenshot3);
			}
			if (!string.IsNullOrEmpty(this.currentApp.Screenshot4))
			{
				list.Add(this.currentApp.Screenshot4);
			}
			if (!string.IsNullOrEmpty(this.currentApp.Screenshot5))
			{
				list.Add(this.currentApp.Screenshot5);
			}
			if (list.Count == 0)
			{
				list.Add("ms-appx:///Assets/Screenshot1.png");
				list.Add("ms-appx:///Assets/Screenshot2.png");
				list.Add("ms-appx:///Assets/Screenshot3.png");
			}
			foreach (string text in list)
			{
				try
				{
					Image image = new Image();
					image.put_Source(new BitmapImage(new Uri(text)));
					image.put_Width(120.0);
					image.put_Height(70.0);
					image.put_Stretch(3);
					this.ScreenshotList.Items.Add(image);
				}
				catch
				{
				}
			}
			if (this.ScreenshotList.Items.Count > 0 && this.MainScreenshot != null)
			{
				this.ScreenshotList.put_SelectedIndex(0);
				this.UpdateMainScreenshot();
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00006098 File Offset: 0x00004298
		private void UpdateMainScreenshot()
		{
			if (this.ScreenshotList != null && this.ScreenshotList.SelectedItem is Image && this.MainScreenshot != null)
			{
				Image image = (Image)this.ScreenshotList.SelectedItem;
				this.MainScreenshot.put_Source(image.Source);
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000060F8 File Offset: 0x000042F8
		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			if (base.Frame.CanGoBack)
			{
				base.Frame.GoBack();
			}
			else
			{
				base.Frame.Navigate(typeof(MainPage));
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00006578 File Offset: 0x00004778
		[DebuggerStepThrough]
		private void DownloadButton_Click(object sender, RoutedEventArgs e)
		{
			AppDetailsPage.<DownloadButton_Click>d__0 <DownloadButton_Click>d__;
			<DownloadButton_Click>d__.<>4__this = this;
			<DownloadButton_Click>d__.sender = sender;
			<DownloadButton_Click>d__.e = e;
			<DownloadButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<DownloadButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <DownloadButton_Click>d__.<>t__builder;
			<>t__builder.Start<AppDetailsPage.<DownloadButton_Click>d__0>(ref <DownloadButton_Click>d__);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00006EBC File Offset: 0x000050BC
		[DebuggerStepThrough]
		private Task DownloadFileWithProgress(Uri uri, string fileName)
		{
			AppDetailsPage.<DownloadFileWithProgress>d__5 <DownloadFileWithProgress>d__;
			<DownloadFileWithProgress>d__.<>4__this = this;
			<DownloadFileWithProgress>d__.uri = uri;
			<DownloadFileWithProgress>d__.fileName = fileName;
			<DownloadFileWithProgress>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DownloadFileWithProgress>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <DownloadFileWithProgress>d__.<>t__builder;
			<>t__builder.Start<AppDetailsPage.<DownloadFileWithProgress>d__5>(ref <DownloadFileWithProgress>d__);
			return <DownloadFileWithProgress>d__.<>t__builder.Task;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000706C File Offset: 0x0000526C
		[DebuggerStepThrough]
		private Task UpdateProgressBar(int percent)
		{
			AppDetailsPage.<UpdateProgressBar>d__1d <UpdateProgressBar>d__1d;
			<UpdateProgressBar>d__1d.<>4__this = this;
			<UpdateProgressBar>d__1d.percent = percent;
			<UpdateProgressBar>d__1d.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateProgressBar>d__1d.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <UpdateProgressBar>d__1d.<>t__builder;
			<>t__builder.Start<AppDetailsPage.<UpdateProgressBar>d__1d>(ref <UpdateProgressBar>d__1d);
			return <UpdateProgressBar>d__1d.<>t__builder.Task;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000070C0 File Offset: 0x000052C0
		private string GetSafeFileName(string fileName)
		{
			char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
			foreach (char c in invalidFileNameChars)
			{
				fileName = fileName.Replace(c.ToString(), "_");
			}
			fileName = fileName.Replace(" ", "_");
			fileName = fileName.Replace("%", "_");
			fileName = fileName.Replace("&", "_");
			fileName = fileName.Replace("?", "_");
			fileName = fileName.Replace("#", "_");
			return fileName;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000716C File Offset: 0x0000536C
		private void ShowDownloadStartedNotification()
		{
			try
			{
				string text = "\r\n                <toast>\r\n                    <visual>\r\n                        <binding template='ToastText02'>\r\n                            <text id='1'>Download started!</text>\r\n                            <text id='2'>" + this.currentApp.Name + " is now downloading in 8Store, please be patient</text>\r\n                        </binding>\r\n                    </visual>\r\n                    <audio src='ms-winsoundevent:Notification.Default'/>\r\n                </toast>";
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(text);
				ToastNotification toastNotification = new ToastNotification(xmlDocument);
				ToastNotificationManager.CreateToastNotifier().Show(toastNotification);
				Debug.WriteLine("Download started notification shown");
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error showing download started notification: " + ex.Message);
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000071F4 File Offset: 0x000053F4
		private void ShowDownloadFinishedNotification()
		{
			try
			{
				string text = "\r\n                <toast>\r\n                    <visual>\r\n                        <binding template='ToastText02'>\r\n                            <text id='1'>Download Finished!</text>\r\n                            <text id='2'>Please follow the further procedure to install</text>\r\n                        </binding>\r\n                    </visual>\r\n                    <audio src='ms-winsoundevent:Notification.Default'/>\r\n                </toast>";
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(text);
				ToastNotification toastNotification = new ToastNotification(xmlDocument);
				ToastNotificationManager.CreateToastNotifier().Show(toastNotification);
				Debug.WriteLine("Download finished notification shown");
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error showing download finished notification: " + ex.Message);
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00007264 File Offset: 0x00005464
		private void ShowNotification(string title, string message)
		{
			try
			{
				string text = string.Concat(new string[]
				{
					"\r\n                <toast>\r\n                    <visual>\r\n                        <binding template='ToastText02'>\r\n                            <text id='1'>",
					title,
					"</text>\r\n                            <text id='2'>",
					message,
					"</text>\r\n                        </binding>\r\n                    </visual>\r\n                    <audio src='ms-winsoundevent:Notification.Default'/>\r\n                </toast>"
				});
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(text);
				ToastNotification toastNotification = new ToastNotification(xmlDocument);
				ToastNotificationManager.CreateToastNotifier().Show(toastNotification);
				Debug.WriteLine("Notification shown: " + title + " - " + message);
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error showing notification: " + ex.Message);
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00007310 File Offset: 0x00005510
		private void FullscreenButton_Click(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00007313 File Offset: 0x00005513
		private void ScreenshotList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			this.UpdateMainScreenshot();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00007320 File Offset: 0x00005520
		protected override void OnNavigatedFrom(NavigationEventArgs e)
		{
			base.OnNavigatedFrom(e);
			if (this.httpClient != null)
			{
				this.httpClient.Dispose();
				this.httpClient = null;
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00007358 File Offset: 0x00005558
		[DebuggerNonUserCode]
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this._contentLoaded)
			{
				this._contentLoaded = true;
				Application.LoadComponent(this, new Uri("ms-appx:///AppDetailsPage.xaml"), 0);
				this.pageRoot = (Page)base.FindName("pageRoot");
				this.BackButton = (Button)base.FindName("BackButton");
				this.TopProgressBar = (Grid)base.FindName("TopProgressBar");
				this.ScreenshotList = (ListBox)base.FindName("ScreenshotList");
				this.FullscreenButton = (Button)base.FindName("FullscreenButton");
				this.MainScreenshot = (Image)base.FindName("MainScreenshot");
				this.DownloadButton = (Button)base.FindName("DownloadButton");
				this.DescriptionText = (TextBlock)base.FindName("DescriptionText");
				this.PublisherText = (TextBlock)base.FindName("PublisherText");
				this.VersionText = (TextBlock)base.FindName("VersionText");
				this.LatestVersion = (Run)base.FindName("LatestVersion");
				this.AuthorName = (Run)base.FindName("AuthorName");
				this.InstallBlock1 = (TextBlock)base.FindName("InstallBlock1");
				this.InstallBlock2 = (TextBlock)base.FindName("InstallBlock2");
				this.PriceText = (TextBlock)base.FindName("PriceText");
				this.AppTitle = (TextBlock)base.FindName("AppTitle");
				this.AppPublisher = (TextBlock)base.FindName("AppPublisher");
				this.AppIcon = (Image)base.FindName("AppIcon");
				this.DownloadProgressBar = (ProgressBar)base.FindName("DownloadProgressBar");
				this.DownloadProgressText = (TextBlock)base.FindName("DownloadProgressText");
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000754C File Offset: 0x0000574C
		[DebuggerNonUserCode]
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		public void Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.BackButton_Click));
				break;
			}
			case 2:
			{
				Selector selector = (Selector)target;
				WindowsRuntimeMarshal.AddEventHandler<SelectionChangedEventHandler>(new Func<SelectionChangedEventHandler, EventRegistrationToken>(selector.add_SelectionChanged), new Action<EventRegistrationToken>(selector.remove_SelectionChanged), new SelectionChangedEventHandler(this.ScreenshotList_SelectionChanged));
				break;
			}
			case 3:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.FullscreenButton_Click));
				break;
			}
			case 4:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.DownloadButton_Click));
				break;
			}
			}
			this._contentLoaded = true;
		}

		// Token: 0x0400001D RID: 29
		private StoreApp currentApp;

		// Token: 0x0400001E RID: 30
		private HttpClient httpClient;

		// Token: 0x0400001F RID: 31
		private bool isDownloading = false;

		// Token: 0x04000020 RID: 32
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Page pageRoot;

		// Token: 0x04000021 RID: 33
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button BackButton;

		// Token: 0x04000022 RID: 34
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Grid TopProgressBar;

		// Token: 0x04000023 RID: 35
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ListBox ScreenshotList;

		// Token: 0x04000024 RID: 36
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button FullscreenButton;

		// Token: 0x04000025 RID: 37
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Image MainScreenshot;

		// Token: 0x04000026 RID: 38
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button DownloadButton;

		// Token: 0x04000027 RID: 39
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock DescriptionText;

		// Token: 0x04000028 RID: 40
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock PublisherText;

		// Token: 0x04000029 RID: 41
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock VersionText;

		// Token: 0x0400002A RID: 42
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Run LatestVersion;

		// Token: 0x0400002B RID: 43
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Run AuthorName;

		// Token: 0x0400002C RID: 44
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock InstallBlock1;

		// Token: 0x0400002D RID: 45
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock InstallBlock2;

		// Token: 0x0400002E RID: 46
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock PriceText;

		// Token: 0x0400002F RID: 47
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock AppTitle;

		// Token: 0x04000030 RID: 48
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock AppPublisher;

		// Token: 0x04000031 RID: 49
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Image AppIcon;

		// Token: 0x04000032 RID: 50
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ProgressBar DownloadProgressBar;

		// Token: 0x04000033 RID: 51
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock DownloadProgressText;

		// Token: 0x04000034 RID: 52
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
