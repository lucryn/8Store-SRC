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
	// Token: 0x0200002B RID: 43
	public sealed class DownloadsHub : Page, IComponentConnector
	{
		// Token: 0x060002B5 RID: 693 RVA: 0x0000DF19 File Offset: 0x0000C119
		public DownloadsHub()
		{
			this.InitializeComponent();
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(this.add_Loaded), new Action<EventRegistrationToken>(this.remove_Loaded), new RoutedEventHandler(this.DownloadsHub_Loaded));
			this.PlayUpdateSound();
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000DF58 File Offset: 0x0000C158
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

		// Token: 0x060002B7 RID: 695 RVA: 0x0000E058 File Offset: 0x0000C258
		private void DownloadsHub_Loaded(object sender, RoutedEventArgs e)
		{
			this.RefreshDownloadsList();
			DownloadManager.DownloadProgress += new EventHandler<DownloadManager.DownloadProgressEventArgs>(this.DownloadManager_DownloadProgress);
			DownloadManager.DownloadCompleted += new EventHandler<DownloadManager.DownloadCompletedEventArgs>(this.DownloadManager_DownloadCompleted);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00004859 File Offset: 0x00002A59
		private void StoreLogo_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(MainPage));
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000E082 File Offset: 0x0000C282
		private void DownloadManager_DownloadProgress(object sender, DownloadManager.DownloadProgressEventArgs e)
		{
			this.RefreshDownloadsList();
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000E082 File Offset: 0x0000C282
		private void DownloadManager_DownloadCompleted(object sender, DownloadManager.DownloadCompletedEventArgs e)
		{
			this.RefreshDownloadsList();
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000E08C File Offset: 0x0000C28C
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
				if (listView != null)
				{
					listView.put_ItemsSource(activeDownloads);
				}
				if (border != null)
				{
					if (activeDownloads.Count == 0)
					{
						border.put_Visibility(0);
						if (listView != null)
						{
							listView.put_Visibility(1);
						}
					}
					else
					{
						border.put_Visibility(1);
						if (listView != null)
						{
							listView.put_Visibility(0);
						}
					}
				}
				if (listView2 != null)
				{
					listView2.put_ItemsSource(completedDownloads);
				}
				if (border2 != null)
				{
					if (completedDownloads.Count == 0)
					{
						border2.put_Visibility(0);
						if (listView2 != null)
						{
							listView2.put_Visibility(1);
						}
					}
					else
					{
						border2.put_Visibility(1);
						if (listView2 != null)
						{
							listView2.put_Visibility(0);
						}
					}
				}
				bool flag = activeDownloads.Count > 0;
				bool flag2 = completedDownloads.Count > 0;
				if (textBlock != null)
				{
					textBlock.put_Visibility((flag || flag2) ? 0 : 1);
				}
				if (textBlock2 != null)
				{
					textBlock2.put_Visibility(flag2 ? 0 : 1);
				}
				if (listView2 != null)
				{
					listView2.put_Visibility(flag2 ? 0 : 1);
				}
				if (completedHubSection != null)
				{
					completedHubSection.put_Visibility(flag2 ? 0 : 1);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000E1FC File Offset: 0x0000C3FC
		private void CancelDownload_Click(object sender, RoutedEventArgs e)
		{
			Button button = sender as Button;
			if (button != null && button.Tag != null)
			{
				string text = button.Tag.ToString();
				if (!string.IsNullOrEmpty(text))
				{
					DownloadManager.CancelDownload(text);
					this.RefreshDownloadsList();
				}
			}
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000E23C File Offset: 0x0000C43C
		private void OpenFile_Click(object sender, RoutedEventArgs e)
		{
			DownloadsHub.<OpenFile_Click>d__8 <OpenFile_Click>d__;
			<OpenFile_Click>d__.<>4__this = this;
			<OpenFile_Click>d__.sender = sender;
			<OpenFile_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<OpenFile_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <OpenFile_Click>d__.<>t__builder;
			<>t__builder.Start<DownloadsHub.<OpenFile_Click>d__8>(ref <OpenFile_Click>d__);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000E280 File Offset: 0x0000C480
		private void OpenFolder_Click(object sender, RoutedEventArgs e)
		{
			DownloadsHub.<OpenFolder_Click>d__9 <OpenFolder_Click>d__;
			<OpenFolder_Click>d__.<>4__this = this;
			<OpenFolder_Click>d__.sender = sender;
			<OpenFolder_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<OpenFolder_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <OpenFolder_Click>d__.<>t__builder;
			<>t__builder.Start<DownloadsHub.<OpenFolder_Click>d__9>(ref <OpenFolder_Click>d__);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000E2C1 File Offset: 0x0000C4C1
		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			if (base.Frame.CanGoBack)
			{
				base.Frame.GoBack();
				return;
			}
			base.Frame.Navigate(typeof(MainPage));
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000E2F2 File Offset: 0x0000C4F2
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);
			this.RefreshDownloadsList();
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000E082 File Offset: 0x0000C282
		private void DownloadsSection_Loaded(object sender, RoutedEventArgs e)
		{
			this.RefreshDownloadsList();
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000E304 File Offset: 0x0000C504
		private static T FindVisualChild<T>(DependencyObject obj, string name) where T : FrameworkElement
		{
			if (obj == null)
			{
				return default(T);
			}
			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
			{
				DependencyObject child = VisualTreeHelper.GetChild(obj, i);
				if (child is T && (child as FrameworkElement).Name == name)
				{
					return child as T;
				}
				T t = DownloadsHub.FindVisualChild<T>(child, name);
				if (t != null)
				{
					return t;
				}
			}
			return default(T);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000E37C File Offset: 0x0000C57C
		private DownloadItem GetItemFromSender(object sender)
		{
			FrameworkElement frameworkElement = sender as FrameworkElement;
			string text = ((frameworkElement != null) ? frameworkElement.Tag : null) as string;
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			foreach (DownloadItem downloadItem in DownloadManager.GetActiveDownloads())
			{
				if (string.Equals(downloadItem.FileName, text, 5))
				{
					return downloadItem;
				}
			}
			foreach (DownloadItem downloadItem2 in DownloadManager.GetCompletedDownloads())
			{
				if (string.Equals(downloadItem2.FileName, text, 5))
				{
					return downloadItem2;
				}
			}
			return null;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000E450 File Offset: 0x0000C650
		private Task ShowMessageDialog(string title, string message)
		{
			DownloadsHub.<ShowMessageDialog>d__15 <ShowMessageDialog>d__;
			<ShowMessageDialog>d__.title = title;
			<ShowMessageDialog>d__.message = message;
			<ShowMessageDialog>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ShowMessageDialog>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <ShowMessageDialog>d__.<>t__builder;
			<>t__builder.Start<DownloadsHub.<ShowMessageDialog>d__15>(ref <ShowMessageDialog>d__);
			return <ShowMessageDialog>d__.<>t__builder.Task;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000E4A0 File Offset: 0x0000C6A0
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Application.LoadComponent(this, new Uri("ms-appx:///DownloadsHub.xaml"), 0);
			this.pageRoot = (Page)base.FindName("pageRoot");
			this.DownloadsHubControl = (Hub)base.FindName("DownloadsHubControl");
			this.BackButton = (Button)base.FindName("BackButton");
			this.CompletedHubSection = (HubSection)base.FindName("CompletedHubSection");
			this.nbLogoText = (TextBlock)base.FindName("nbLogoText");
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000E53C File Offset: 0x0000C73C
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
				WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uielement.add_Tapped), new Action<EventRegistrationToken>(uielement.remove_Tapped), new TappedEventHandler(this.StoreLogo_Click));
				break;
			}
			}
			this._contentLoaded = true;
		}

		// Token: 0x0400011E RID: 286
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Page pageRoot;

		// Token: 0x0400011F RID: 287
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Hub DownloadsHubControl;

		// Token: 0x04000120 RID: 288
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button BackButton;

		// Token: 0x04000121 RID: 289
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private HubSection CompletedHubSection;

		// Token: 0x04000122 RID: 290
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock nbLogoText;

		// Token: 0x04000123 RID: 291
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
