using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Win81StoreRevival
{
	// Token: 0x02000027 RID: 39
	public sealed class DownloadsHub : Page, IComponentConnector
	{
		// Token: 0x06000227 RID: 551 RVA: 0x0000CBB0 File Offset: 0x0000ADB0
		public DownloadsHub()
		{
			this.InitializeComponent();
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(this.add_Loaded), new Action<EventRegistrationToken>(this.remove_Loaded), new RoutedEventHandler(this.DownloadsHub_Loaded));
			this.PlayUpdateSound();
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000CC00 File Offset: 0x0000AE00
		private void PlayUpdateSound()
		{
			try
			{
				Debug.WriteLine("Playing update notification sound...");
				MediaElement soundPlayer = new MediaElement();
				soundPlayer.put_AutoPlay(true);
				soundPlayer.put_Volume(0.5);
				soundPlayer.put_Visibility(1);
				Grid grid = base.Content as Grid;
				bool flag = grid != null;
				if (flag)
				{
					grid.Children.Add(soundPlayer);
				}
				soundPlayer.put_Source(new Uri("ms-appx:///Assets/notify1.wav"));
				MediaElement soundPlayer2 = soundPlayer;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(soundPlayer2.add_MediaEnded), new Action<EventRegistrationToken>(soundPlayer2.remove_MediaEnded), delegate(object sender, RoutedEventArgs e)
				{
					Debug.WriteLine("Sound playback completed");
					Grid grid2 = this.Content as Grid;
					bool flag2 = grid2 != null;
					if (flag2)
					{
						grid2.Children.Remove(soundPlayer);
					}
				});
				soundPlayer2 = soundPlayer;
				WindowsRuntimeMarshal.AddEventHandler<ExceptionRoutedEventHandler>(new Func<ExceptionRoutedEventHandler, EventRegistrationToken>(soundPlayer2.add_MediaFailed), new Action<EventRegistrationToken>(soundPlayer2.remove_MediaFailed), delegate(object sender, ExceptionRoutedEventArgs e)
				{
					Debug.WriteLine("Sound playback failed: " + e.ErrorMessage);
					Grid grid2 = this.Content as Grid;
					bool flag2 = grid2 != null;
					if (flag2)
					{
						grid2.Children.Remove(soundPlayer);
					}
				});
				Debug.WriteLine("Sound playback started");
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error playing sound: " + ex.Message);
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000CD4C File Offset: 0x0000AF4C
		private void DownloadsHub_Loaded(object sender, RoutedEventArgs e)
		{
			this.RefreshDownloadsList();
			DownloadManager.DownloadProgress += new EventHandler<DownloadManager.DownloadProgressEventArgs>(this.DownloadManager_DownloadProgress);
			DownloadManager.DownloadCompleted += new EventHandler<DownloadManager.DownloadCompletedEventArgs>(this.DownloadManager_DownloadCompleted);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000CD7A File Offset: 0x0000AF7A
		private void DownloadManager_DownloadProgress(object sender, DownloadManager.DownloadProgressEventArgs e)
		{
			this.RefreshDownloadsList();
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000CD7A File Offset: 0x0000AF7A
		private void DownloadManager_DownloadCompleted(object sender, DownloadManager.DownloadCompletedEventArgs e)
		{
			this.RefreshDownloadsList();
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000CD84 File Offset: 0x0000AF84
		private void RefreshDownloadsList()
		{
			try
			{
				List<DownloadItem> activeDownloads = DownloadManager.GetActiveDownloads();
				List<DownloadItem> completedDownloads = DownloadManager.GetCompletedDownloads();
				ListView listView = DownloadsHub.FindVisualChild<ListView>(this, "ActiveDownloadsListView");
				Border border = DownloadsHub.FindVisualChild<Border>(this, "EmptyStatePanel");
				ListView listView2 = DownloadsHub.FindVisualChild<ListView>(this, "CompletedDownloadsListView");
				Border border2 = DownloadsHub.FindVisualChild<Border>(this, "EmptyCompletedStatePanel");
				TextBlock textBlock = DownloadsHub.FindVisualChild<TextBlock>(this, "ActiveDownloadsHeader");
				TextBlock textBlock2 = DownloadsHub.FindVisualChild<TextBlock>(this, "CompletedDownloadsHeader");
				HubSection completedHubSection = this.CompletedHubSection;
				bool flag = listView != null;
				if (flag)
				{
					listView.put_ItemsSource(activeDownloads);
				}
				bool flag2 = border != null;
				if (flag2)
				{
					bool flag3 = activeDownloads.Count == 0;
					if (flag3)
					{
						border.put_Visibility(0);
						bool flag4 = listView != null;
						if (flag4)
						{
							listView.put_Visibility(1);
						}
					}
					else
					{
						border.put_Visibility(1);
						bool flag5 = listView != null;
						if (flag5)
						{
							listView.put_Visibility(0);
						}
					}
				}
				bool flag6 = listView2 != null;
				if (flag6)
				{
					listView2.put_ItemsSource(completedDownloads);
				}
				bool flag7 = border2 != null;
				if (flag7)
				{
					bool flag8 = completedDownloads.Count == 0;
					if (flag8)
					{
						border2.put_Visibility(0);
						bool flag9 = listView2 != null;
						if (flag9)
						{
							listView2.put_Visibility(1);
						}
					}
					else
					{
						border2.put_Visibility(1);
						bool flag10 = listView2 != null;
						if (flag10)
						{
							listView2.put_Visibility(0);
						}
					}
				}
				bool flag11 = activeDownloads.Count > 0;
				bool flag12 = completedDownloads.Count > 0;
				bool flag13 = textBlock != null;
				if (flag13)
				{
					textBlock.put_Visibility((flag11 || flag12) ? 0 : 1);
				}
				bool flag14 = textBlock2 != null;
				if (flag14)
				{
					textBlock2.put_Visibility(flag12 ? 0 : 1);
				}
				bool flag15 = listView2 != null;
				if (flag15)
				{
					listView2.put_Visibility(flag12 ? 0 : 1);
				}
				bool flag16 = completedHubSection != null;
				if (flag16)
				{
					completedHubSection.put_Visibility(flag12 ? 0 : 1);
				}
				Debug.WriteLine("[DOWNLOADS HUB] Refreshed: " + activeDownloads.Count + " downloads");
			}
			catch (Exception ex)
			{
				Debug.WriteLine("[DOWNLOADS HUB ERROR] " + ex.Message);
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000CFC0 File Offset: 0x0000B1C0
		private void CancelDownload_Click(object sender, RoutedEventArgs e)
		{
			Button button = sender as Button;
			bool flag = button != null && button.Tag != null;
			if (flag)
			{
				string text = button.Tag.ToString();
				bool flag2 = !string.IsNullOrEmpty(text);
				if (flag2)
				{
					DownloadManager.CancelDownload(text);
					this.RefreshDownloadsList();
				}
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000D014 File Offset: 0x0000B214
		[DebuggerStepThrough]
		private void OpenFile_Click(object sender, RoutedEventArgs e)
		{
			DownloadsHub.<OpenFile_Click>d__7 <OpenFile_Click>d__ = new DownloadsHub.<OpenFile_Click>d__7();
			<OpenFile_Click>d__.<>4__this = this;
			<OpenFile_Click>d__.sender = sender;
			<OpenFile_Click>d__.e = e;
			<OpenFile_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<OpenFile_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <OpenFile_Click>d__.<>t__builder;
			<>t__builder.Start<DownloadsHub.<OpenFile_Click>d__7>(ref <OpenFile_Click>d__);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000D060 File Offset: 0x0000B260
		[DebuggerStepThrough]
		private void OpenFolder_Click(object sender, RoutedEventArgs e)
		{
			DownloadsHub.<OpenFolder_Click>d__8 <OpenFolder_Click>d__ = new DownloadsHub.<OpenFolder_Click>d__8();
			<OpenFolder_Click>d__.<>4__this = this;
			<OpenFolder_Click>d__.sender = sender;
			<OpenFolder_Click>d__.e = e;
			<OpenFolder_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<OpenFolder_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <OpenFolder_Click>d__.<>t__builder;
			<>t__builder.Start<DownloadsHub.<OpenFolder_Click>d__8>(ref <OpenFolder_Click>d__);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000D0AC File Offset: 0x0000B2AC
		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			bool canGoBack = base.Frame.CanGoBack;
			if (canGoBack)
			{
				base.Frame.GoBack();
			}
			else
			{
				base.Frame.Navigate(typeof(MainPage));
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000D0F1 File Offset: 0x0000B2F1
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);
			this.RefreshDownloadsList();
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000CD7A File Offset: 0x0000AF7A
		private void DownloadsSection_Loaded(object sender, RoutedEventArgs e)
		{
			this.RefreshDownloadsList();
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00005880 File Offset: 0x00003A80
		private void TopAppsButton_Click_1(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(PopularPage));
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00009F07 File Offset: 0x00008107
		private void TopAppsButton_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(Dependencies));
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00005899 File Offset: 0x00003A99
		private void DownloadHubButton_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(DownloadsHub));
		}

		// Token: 0x06000236 RID: 566 RVA: 0x000058B2 File Offset: 0x00003AB2
		private void ExclusiveHubButton_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(AccountPage));
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00005867 File Offset: 0x00003A67
		private void Border_Tapped_1(object sender, TappedRoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(AllAppsPage));
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000D104 File Offset: 0x0000B304
		private static T FindVisualChild<T>(DependencyObject obj, string name) where T : FrameworkElement
		{
			bool flag = obj == null;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
				{
					DependencyObject child = VisualTreeHelper.GetChild(obj, i);
					bool flag2 = child is T && (child as FrameworkElement).Name == name;
					if (flag2)
					{
						return child as T;
					}
					T t = DownloadsHub.FindVisualChild<T>(child, name);
					bool flag3 = t != null;
					if (flag3)
					{
						return t;
					}
				}
				result = default(T);
			}
			return result;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000D1AC File Offset: 0x0000B3AC
		private DownloadItem GetItemFromSender(object sender)
		{
			FrameworkElement frameworkElement = sender as FrameworkElement;
			string text = ((frameworkElement != null) ? frameworkElement.Tag : null) as string;
			bool flag = string.IsNullOrEmpty(text);
			DownloadItem result;
			if (flag)
			{
				result = null;
			}
			else
			{
				foreach (DownloadItem downloadItem in DownloadManager.GetActiveDownloads())
				{
					bool flag2 = string.Equals(downloadItem.FileName, text, 5);
					if (flag2)
					{
						return downloadItem;
					}
				}
				foreach (DownloadItem downloadItem2 in DownloadManager.GetCompletedDownloads())
				{
					bool flag3 = string.Equals(downloadItem2.FileName, text, 5);
					if (flag3)
					{
						return downloadItem2;
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000D2A0 File Offset: 0x0000B4A0
		[DebuggerStepThrough]
		private Task ShowMessageDialog(string title, string message)
		{
			DownloadsHub.<ShowMessageDialog>d__19 <ShowMessageDialog>d__ = new DownloadsHub.<ShowMessageDialog>d__19();
			<ShowMessageDialog>d__.<>4__this = this;
			<ShowMessageDialog>d__.title = title;
			<ShowMessageDialog>d__.message = message;
			<ShowMessageDialog>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ShowMessageDialog>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <ShowMessageDialog>d__.<>t__builder;
			<>t__builder.Start<DownloadsHub.<ShowMessageDialog>d__19>(ref <ShowMessageDialog>d__);
			return <ShowMessageDialog>d__.<>t__builder.Task;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000D2F8 File Offset: 0x0000B4F8
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			bool contentLoaded = this._contentLoaded;
			if (!contentLoaded)
			{
				this._contentLoaded = true;
				Application.LoadComponent(this, new Uri("ms-appx:///DownloadsHub.xaml"), 0);
				this.pageRoot = (Page)base.FindName("pageRoot");
				this.DownloadsHubControl = (Hub)base.FindName("DownloadsHubControl");
				this.BackButton = (Button)base.FindName("BackButton");
				this.CompletedHubSection = (HubSection)base.FindName("CompletedHubSection");
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000D384 File Offset: 0x0000B584
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
				FrameworkElement frameworkElement = (FrameworkElement)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(frameworkElement.add_Loaded), new Action<EventRegistrationToken>(frameworkElement.remove_Loaded), new RoutedEventHandler(this.DownloadsSection_Loaded));
				break;
			}
			case 3:
			{
				MenuFlyoutItem menuFlyoutItem = (MenuFlyoutItem)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(menuFlyoutItem.add_Click), new Action<EventRegistrationToken>(menuFlyoutItem.remove_Click), new RoutedEventHandler(this.OpenFile_Click));
				break;
			}
			case 4:
			{
				MenuFlyoutItem menuFlyoutItem = (MenuFlyoutItem)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(menuFlyoutItem.add_Click), new Action<EventRegistrationToken>(menuFlyoutItem.remove_Click), new RoutedEventHandler(this.OpenFolder_Click));
				break;
			}
			case 5:
			{
				FrameworkElement frameworkElement = (FrameworkElement)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(frameworkElement.add_Loaded), new Action<EventRegistrationToken>(frameworkElement.remove_Loaded), new RoutedEventHandler(this.DownloadsSection_Loaded));
				break;
			}
			case 6:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.CancelDownload_Click));
				break;
			}
			case 7:
			{
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uielement.add_Tapped), new Action<EventRegistrationToken>(uielement.remove_Tapped), new TappedEventHandler(this.Border_Tapped_1));
				break;
			}
			}
			this._contentLoaded = true;
		}

		// Token: 0x04000111 RID: 273
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Page pageRoot;

		// Token: 0x04000112 RID: 274
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Hub DownloadsHubControl;

		// Token: 0x04000113 RID: 275
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button BackButton;

		// Token: 0x04000114 RID: 276
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private HubSection CompletedHubSection;

		// Token: 0x04000115 RID: 277
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
