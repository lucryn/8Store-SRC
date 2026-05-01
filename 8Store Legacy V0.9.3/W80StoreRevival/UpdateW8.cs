using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Navigation;

namespace W80StoreRevival
{
	// Token: 0x02000014 RID: 20
	public sealed class UpdateW8 : Page, IComponentConnector
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x0000E794 File Offset: 0x0000C994
		public UpdateW8()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("pro64", "https://ia601809.us.archive.org/27/items/windows-8.1-x64-fully-updated/en_windows_8.1_x64.iso");
			dictionary.Add("enterprise64", "http://care.dlservice.microsoft.com/dl/download/B/9/9/B999286E-0A47-406D-8B3D-5B5AD7373A4A/9600.17050.WINBLUE_REFRESH.140317-1640_X64FRE_ENTERPRISE_EVAL_EN-US-IR3_CENA_X64FREE_EN-US_DV9.ISO");
			dictionary.Add("pro32", "https://dn721606.ca.archive.org/0/items/windows-8.1-x-32-x-64/Win8.1_English_x32.iso");
			dictionary.Add("enterprise32", "http://care.dlservice.microsoft.com/dl/download/B/9/9/B999286E-0A47-406D-8B3D-5B5AD7373A4A/9600.17050.WINBLUE_REFRESH.140317-1640_X86FRE_ENTERPRISE_EVAL_EN-US-IR3_CENA_X86FREE_EN-US_DV9.ISO");
			dictionary.Add("core64", "https://dn721806.ca.archive.org/0/items/en-gb_windows_8_1_x64_dvd_2707421/en-gb_windows_8_1_x64_dvd_2707421.iso");
			dictionary.Add("core32", "https://dn721603.ca.archive.org/0/items/en_windows_8_1_x86_dvd_2707392/en_windows_8_1_x86_dvd_2707392.iso");
			this.isoUrls = dictionary;
			Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
			dictionary2.Add("pro64", "Windows8.1_Pro_x64.iso");
			dictionary2.Add("enterprise64", "Windows8.1_Enterprise_x64.iso");
			dictionary2.Add("core64", "Windows8.1_Core_x64.iso");
			dictionary2.Add("pro32", "Windows8.1_Pro_x86.iso");
			dictionary2.Add("enterprise32", "Windows8.1_Enterprise_x86.iso");
			dictionary2.Add("core32", "Windows8.1_Core_x86.iso");
			this.isoFileNames = dictionary2;
			Dictionary<string, string> dictionary3 = new Dictionary<string, string>();
			dictionary3.Add("pro64", "Windows 8.1 Pro x64 (Update 3)");
			dictionary3.Add("enterprise64", "Windows 8.1 Enterprise x64");
			dictionary3.Add("core64", "Windows 8.1 Core x64");
			dictionary3.Add("pro32", "Windows 8.1 Pro x86");
			dictionary3.Add("enterprise32", "Windows 8.1 Enterprise x86");
			dictionary3.Add("core32", "Windows 8.1 Core x86");
			this.displayNames = dictionary3;
			this.selectedEdition = "pro64";
			this.isDownloading = false;
			base..ctor();
			this.InitializeComponent();
			if (this.EditionComboBox != null)
			{
				this.EditionComboBox.put_SelectedIndex(0);
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000E938 File Offset: 0x0000CB38
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000E93C File Offset: 0x0000CB3C
		private void EditionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.EditionComboBox != null && this.EditionComboBox.SelectedItem is ComboBoxItem)
			{
				ComboBoxItem comboBoxItem = this.EditionComboBox.SelectedItem as ComboBoxItem;
				this.selectedEdition = comboBoxItem.Tag.ToString();
				this.UpdateDescriptionBasedOnEdition();
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000E99C File Offset: 0x0000CB9C
		private void UpdateDescriptionBasedOnEdition()
		{
			string text = this.selectedEdition.EndsWith("64") ? "64-bit" : "32-bit";
			if (this.selectedEdition.Contains("pro"))
			{
				string text2 = "Pro";
				this.DescriptionText.put_Text(string.Concat(new string[]
				{
					"Upgrading to Windows 8.1 ",
					text,
					" ",
					text2,
					" delivers smoother performance with a restored Start button and enhanced multitasking. Includes BitLocker encryption, Remote Desktop hosting, domain join capabilities, and improved business features for professional users."
				}));
			}
			else if (this.selectedEdition.Contains("enterprise"))
			{
				string text2 = "Enterprise";
				this.DescriptionText.put_Text(string.Concat(new string[]
				{
					"Windows 8.1 ",
					text,
					" ",
					text2,
					" includes all Pro features plus enterprise-grade security and management. Get AppLocker, DirectAccess, BranchCache, Windows To Go, VDI improvements, and comprehensive device management for large organizations."
				}));
			}
			else if (this.selectedEdition.Contains("core"))
			{
				string text2 = "Core";
				this.DescriptionText.put_Text(string.Concat(new string[]
				{
					"Windows 8.1 ",
					text,
					" ",
					text2,
					" is the standard edition offering the essential Windows 8.1 experience. Perfect for home users, it includes the Start screen, Windows Store apps, improved multitasking, and all core features for everyday computing."
				}));
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000F024 File Offset: 0x0000D224
		[DebuggerStepThrough]
		private void DownloadButton_Click(object sender, RoutedEventArgs e)
		{
			UpdateW8.<DownloadButton_Click>d__3 <DownloadButton_Click>d__;
			<DownloadButton_Click>d__.<>4__this = this;
			<DownloadButton_Click>d__.sender = sender;
			<DownloadButton_Click>d__.e = e;
			<DownloadButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<DownloadButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <DownloadButton_Click>d__.<>t__builder;
			<>t__builder.Start<UpdateW8.<DownloadButton_Click>d__3>(ref <DownloadButton_Click>d__);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000F738 File Offset: 0x0000D938
		[DebuggerStepThrough]
		private Task DownloadWithProgressAsync(string url, StorageFile file, string displayName)
		{
			UpdateW8.<DownloadWithProgressAsync>d__1d <DownloadWithProgressAsync>d__1d;
			<DownloadWithProgressAsync>d__1d.<>4__this = this;
			<DownloadWithProgressAsync>d__1d.url = url;
			<DownloadWithProgressAsync>d__1d.file = file;
			<DownloadWithProgressAsync>d__1d.displayName = displayName;
			<DownloadWithProgressAsync>d__1d.<>t__builder = AsyncTaskMethodBuilder.Create();
			<DownloadWithProgressAsync>d__1d.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <DownloadWithProgressAsync>d__1d.<>t__builder;
			<>t__builder.Start<UpdateW8.<DownloadWithProgressAsync>d__1d>(ref <DownloadWithProgressAsync>d__1d);
			return <DownloadWithProgressAsync>d__1d.<>t__builder.Task;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000F898 File Offset: 0x0000DA98
		[DebuggerStepThrough]
		private Task ShowMessageAsync(string message, string title)
		{
			UpdateW8.<ShowMessageAsync>d__22 <ShowMessageAsync>d__;
			<ShowMessageAsync>d__.<>4__this = this;
			<ShowMessageAsync>d__.message = message;
			<ShowMessageAsync>d__.title = title;
			<ShowMessageAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ShowMessageAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <ShowMessageAsync>d__.<>t__builder;
			<>t__builder.Start<UpdateW8.<ShowMessageAsync>d__22>(ref <ShowMessageAsync>d__);
			return <ShowMessageAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000F8F4 File Offset: 0x0000DAF4
		private void OpenISOFile(StorageFile isoFile, string displayName)
		{
			try
			{
				string text = this.selectedEdition.EndsWith("64") ? "64-bit" : "32-bit";
				string text2 = text + " requires a compatible " + (this.selectedEdition.EndsWith("64") ? "64-bit" : "32-bit") + " processor.";
				string message = string.Concat(new string[]
				{
					displayName,
					" ISO has been downloaded successfully!\n\nFile saved to: ",
					isoFile.Path,
					"\n\nSystem Requirement: ",
					text2,
					"\n\nTo install:\n1. Go to the file location above\n2. Right-click the ISO file\n3. Select 'Mount'\n4. Run setup.exe from the virtual drive"
				});
				this.ShowSuccessMessageAsync(message, "Download Complete", isoFile);
			}
			catch (Exception ex)
			{
				this.ShowErrorMessageAsync("ISO downloaded but could not be opened: " + ex.Message, "Warning");
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000FCB4 File Offset: 0x0000DEB4
		[DebuggerStepThrough]
		private void ShowSuccessMessageAsync(string message, string title, StorageFile isoFile)
		{
			UpdateW8.<ShowSuccessMessageAsync>d__2c <ShowSuccessMessageAsync>d__2c;
			<ShowSuccessMessageAsync>d__2c.<>4__this = this;
			<ShowSuccessMessageAsync>d__2c.message = message;
			<ShowSuccessMessageAsync>d__2c.title = title;
			<ShowSuccessMessageAsync>d__2c.isoFile = isoFile;
			<ShowSuccessMessageAsync>d__2c.<>t__builder = AsyncVoidMethodBuilder.Create();
			<ShowSuccessMessageAsync>d__2c.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <ShowSuccessMessageAsync>d__2c.<>t__builder;
			<>t__builder.Start<UpdateW8.<ShowSuccessMessageAsync>d__2c>(ref <ShowSuccessMessageAsync>d__2c);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000FDF8 File Offset: 0x0000DFF8
		[DebuggerStepThrough]
		private void ShowErrorMessageAsync(string message, string title)
		{
			UpdateW8.<ShowErrorMessageAsync>d__30 <ShowErrorMessageAsync>d__;
			<ShowErrorMessageAsync>d__.<>4__this = this;
			<ShowErrorMessageAsync>d__.message = message;
			<ShowErrorMessageAsync>d__.title = title;
			<ShowErrorMessageAsync>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<ShowErrorMessageAsync>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <ShowErrorMessageAsync>d__.<>t__builder;
			<>t__builder.Start<UpdateW8.<ShowErrorMessageAsync>d__30>(ref <ShowErrorMessageAsync>d__);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000FF64 File Offset: 0x0000E164
		[DebuggerStepThrough]
		private Task ShowFilePathMessageAsync(StorageFile isoFile)
		{
			UpdateW8.<ShowFilePathMessageAsync>d__33 <ShowFilePathMessageAsync>d__;
			<ShowFilePathMessageAsync>d__.<>4__this = this;
			<ShowFilePathMessageAsync>d__.isoFile = isoFile;
			<ShowFilePathMessageAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ShowFilePathMessageAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <ShowFilePathMessageAsync>d__.<>t__builder;
			<>t__builder.Start<UpdateW8.<ShowFilePathMessageAsync>d__33>(ref <ShowFilePathMessageAsync>d__);
			return <ShowFilePathMessageAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000FFB8 File Offset: 0x0000E1B8
		private string GetFileSizeString(StorageFile file)
		{
			string result2;
			try
			{
				BasicProperties result = WindowsRuntimeSystemExtensions.AsTask<BasicProperties>(file.GetBasicPropertiesAsync()).Result;
				double num = result.Size;
				if (num < 1024.0)
				{
					result2 = num + " bytes";
				}
				else if (num < 1048576.0)
				{
					result2 = (num / 1024.0).ToString("F1") + " KB";
				}
				else if (num < 1073741824.0)
				{
					result2 = (num / 1048576.0).ToString("F1") + " MB";
				}
				else
				{
					result2 = (num / 1073741824.0).ToString("F1") + " GB";
				}
			}
			catch
			{
				result2 = "Unknown size";
			}
			return result2;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000100C0 File Offset: 0x0000E2C0
		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (base.Frame != null)
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
			}
			catch (Exception ex)
			{
				Debug.WriteLine("BackButton error: " + ex.Message);
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00010144 File Offset: 0x0000E344
		private void ScreenshotList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			this.UpdateMainScreenshot();
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00010150 File Offset: 0x0000E350
		private void UpdateMainScreenshot()
		{
			if (this.ScreenshotList != null && this.ScreenshotList.SelectedItem is Image && this.MainScreenshot != null)
			{
				Image image = (Image)this.ScreenshotList.SelectedItem;
				this.MainScreenshot.put_Source(image.Source);
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000101B0 File Offset: 0x0000E3B0
		[DebuggerNonUserCode]
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this._contentLoaded)
			{
				this._contentLoaded = true;
				Application.LoadComponent(this, new Uri("ms-appx:///UpdateW8.xaml"), 0);
				this.BackButton = (Button)base.FindName("BackButton");
				this.TopProgressBar = (Grid)base.FindName("TopProgressBar");
				this.ScreenshotList = (ListBox)base.FindName("ScreenshotList");
				this.FullscreenButton = (Button)base.FindName("FullscreenButton");
				this.MainScreenshot = (Image)base.FindName("MainScreenshot");
				this.EditionComboBox = (ComboBox)base.FindName("EditionComboBox");
				this.DownloadButton = (Button)base.FindName("DownloadButton");
				this.DescriptionText = (TextBlock)base.FindName("DescriptionText");
				this.PublisherText = (TextBlock)base.FindName("PublisherText");
				this.VersionText = (TextBlock)base.FindName("VersionText");
				this.LatestVersion = (Run)base.FindName("LatestVersion");
				this.AuthorName = (Run)base.FindName("AuthorName");
				this.InstallBlock1 = (TextBlock)base.FindName("InstallBlock1");
				this.PriceText = (TextBlock)base.FindName("PriceText");
				this.AppTitle = (TextBlock)base.FindName("AppTitle");
				this.AppPublisher = (TextBlock)base.FindName("AppPublisher");
				this.AppIcon = (Image)base.FindName("AppIcon");
				this.DownloadProgressBar = (ProgressBar)base.FindName("DownloadProgressBar");
				this.DownloadProgressText = (TextBlock)base.FindName("DownloadProgressText");
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0001038C File Offset: 0x0000E58C
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
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
				Selector selector = (Selector)target;
				WindowsRuntimeMarshal.AddEventHandler<SelectionChangedEventHandler>(new Func<SelectionChangedEventHandler, EventRegistrationToken>(selector.add_SelectionChanged), new Action<EventRegistrationToken>(selector.remove_SelectionChanged), new SelectionChangedEventHandler(this.EditionComboBox_SelectionChanged));
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

		// Token: 0x04000098 RID: 152
		private Dictionary<string, string> isoUrls;

		// Token: 0x04000099 RID: 153
		private Dictionary<string, string> isoFileNames;

		// Token: 0x0400009A RID: 154
		private Dictionary<string, string> displayNames;

		// Token: 0x0400009B RID: 155
		private string selectedEdition;

		// Token: 0x0400009C RID: 156
		private BackgroundDownloader downloader;

		// Token: 0x0400009D RID: 157
		private DownloadOperation downloadOperation;

		// Token: 0x0400009E RID: 158
		private bool isDownloading;

		// Token: 0x0400009F RID: 159
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button BackButton;

		// Token: 0x040000A0 RID: 160
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Grid TopProgressBar;

		// Token: 0x040000A1 RID: 161
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ListBox ScreenshotList;

		// Token: 0x040000A2 RID: 162
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button FullscreenButton;

		// Token: 0x040000A3 RID: 163
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Image MainScreenshot;

		// Token: 0x040000A4 RID: 164
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ComboBox EditionComboBox;

		// Token: 0x040000A5 RID: 165
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button DownloadButton;

		// Token: 0x040000A6 RID: 166
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock DescriptionText;

		// Token: 0x040000A7 RID: 167
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock PublisherText;

		// Token: 0x040000A8 RID: 168
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock VersionText;

		// Token: 0x040000A9 RID: 169
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Run LatestVersion;

		// Token: 0x040000AA RID: 170
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Run AuthorName;

		// Token: 0x040000AB RID: 171
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock InstallBlock1;

		// Token: 0x040000AC RID: 172
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock PriceText;

		// Token: 0x040000AD RID: 173
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock AppTitle;

		// Token: 0x040000AE RID: 174
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock AppPublisher;

		// Token: 0x040000AF RID: 175
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Image AppIcon;

		// Token: 0x040000B0 RID: 176
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ProgressBar DownloadProgressBar;

		// Token: 0x040000B1 RID: 177
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock DownloadProgressText;

		// Token: 0x040000B2 RID: 178
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
