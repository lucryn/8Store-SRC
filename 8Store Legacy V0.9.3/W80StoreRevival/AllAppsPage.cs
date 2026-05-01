using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace W80StoreRevival
{
	// Token: 0x02000003 RID: 3
	public sealed class AllAppsPage : Page, IComponentConnector
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002198 File Offset: 0x00000398
		public AllAppsPage()
		{
			try
			{
				Debug.WriteLine("AllAppsPage constructor");
				this.InitializeComponent();
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Constructor error: " + ex.Message);
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000230C File Offset: 0x0000050C
		[DebuggerStepThrough]
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			AllAppsPage.<OnNavigatedTo>d__0 <OnNavigatedTo>d__;
			<OnNavigatedTo>d__.<>4__this = this;
			<OnNavigatedTo>d__.e = e;
			<OnNavigatedTo>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<OnNavigatedTo>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <OnNavigatedTo>d__.<>t__builder;
			<>t__builder.Start<AllAppsPage.<OnNavigatedTo>d__0>(ref <OnNavigatedTo>d__);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000027E0 File Offset: 0x000009E0
		[DebuggerStepThrough]
		private Task LoadApps()
		{
			AllAppsPage.<LoadApps>d__4 <LoadApps>d__;
			<LoadApps>d__.<>4__this = this;
			<LoadApps>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadApps>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadApps>d__.<>t__builder;
			<>t__builder.Start<AllAppsPage.<LoadApps>d__4>(ref <LoadApps>d__);
			return <LoadApps>d__.<>t__builder.Task;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002C5C File Offset: 0x00000E5C
		[DebuggerStepThrough]
		private Task LoadAppsFromServer()
		{
			AllAppsPage.<LoadAppsFromServer>d__b <LoadAppsFromServer>d__b;
			<LoadAppsFromServer>d__b.<>4__this = this;
			<LoadAppsFromServer>d__b.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadAppsFromServer>d__b.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadAppsFromServer>d__b.<>t__builder;
			<>t__builder.Start<AllAppsPage.<LoadAppsFromServer>d__b>(ref <LoadAppsFromServer>d__b);
			return <LoadAppsFromServer>d__b.<>t__builder.Task;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000304C File Offset: 0x0000124C
		[DebuggerStepThrough]
		private Task<string> LoadCachedJson()
		{
			AllAppsPage.<LoadCachedJson>d__13 <LoadCachedJson>d__;
			<LoadCachedJson>d__.<>4__this = this;
			<LoadCachedJson>d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<LoadCachedJson>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<string> <>t__builder = <LoadCachedJson>d__.<>t__builder;
			<>t__builder.Start<AllAppsPage.<LoadCachedJson>d__13>(ref <LoadCachedJson>d__);
			return <LoadCachedJson>d__.<>t__builder.Task;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000329C File Offset: 0x0000149C
		[DebuggerStepThrough]
		private Task SaveJsonToCache(string json)
		{
			AllAppsPage.<SaveJsonToCache>d__1d <SaveJsonToCache>d__1d;
			<SaveJsonToCache>d__1d.<>4__this = this;
			<SaveJsonToCache>d__1d.json = json;
			<SaveJsonToCache>d__1d.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SaveJsonToCache>d__1d.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <SaveJsonToCache>d__1d.<>t__builder;
			<>t__builder.Start<AllAppsPage.<SaveJsonToCache>d__1d>(ref <SaveJsonToCache>d__1d);
			return <SaveJsonToCache>d__1d.<>t__builder.Task;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000035C0 File Offset: 0x000017C0
		[DebuggerStepThrough]
		private Task<List<StoreApp>> ParseJsonAsync(string json)
		{
			AllAppsPage.<ParseJsonAsync>d__23 <ParseJsonAsync>d__;
			<ParseJsonAsync>d__.<>4__this = this;
			<ParseJsonAsync>d__.json = json;
			<ParseJsonAsync>d__.<>t__builder = AsyncTaskMethodBuilder<List<StoreApp>>.Create();
			<ParseJsonAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<List<StoreApp>> <>t__builder = <ParseJsonAsync>d__.<>t__builder;
			<>t__builder.Start<AllAppsPage.<ParseJsonAsync>d__23>(ref <ParseJsonAsync>d__);
			return <ParseJsonAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00003614 File Offset: 0x00001814
		private string GetJsonString(JsonObject obj, string key)
		{
			string result;
			if (obj.ContainsKey(key) && obj[key].ValueType == 3)
			{
				result = obj.GetNamedString(key);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000039C8 File Offset: 0x00001BC8
		[DebuggerStepThrough]
		private Task AddAppsToUIAsync(List<StoreApp> apps)
		{
			AllAppsPage.<AddAppsToUIAsync>d__28 <AddAppsToUIAsync>d__;
			<AddAppsToUIAsync>d__.<>4__this = this;
			<AddAppsToUIAsync>d__.apps = apps;
			<AddAppsToUIAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<AddAppsToUIAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <AddAppsToUIAsync>d__.<>t__builder;
			<>t__builder.Start<AllAppsPage.<AddAppsToUIAsync>d__28>(ref <AddAppsToUIAsync>d__);
			return <AddAppsToUIAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00003D7C File Offset: 0x00001F7C
		[DebuggerStepThrough]
		private void ClearCacheButton_Click(object sender, RoutedEventArgs e)
		{
			AllAppsPage.<ClearCacheButton_Click>d__2b <ClearCacheButton_Click>d__2b;
			<ClearCacheButton_Click>d__2b.<>4__this = this;
			<ClearCacheButton_Click>d__2b.sender = sender;
			<ClearCacheButton_Click>d__2b.e = e;
			<ClearCacheButton_Click>d__2b.<>t__builder = AsyncVoidMethodBuilder.Create();
			<ClearCacheButton_Click>d__2b.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <ClearCacheButton_Click>d__2b.<>t__builder;
			<>t__builder.Start<AllAppsPage.<ClearCacheButton_Click>d__2b>(ref <ClearCacheButton_Click>d__2b);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00003DC8 File Offset: 0x00001FC8
		private StackPanel CreateAppItem(StoreApp app)
		{
			StackPanel stackPanel = new StackPanel();
			stackPanel.put_Width(300.0);
			stackPanel.put_Height(380.0);
			stackPanel.put_Margin(new Thickness(10.0, 8.0, 10.0, 8.0));
			stackPanel.put_Background(null);
			stackPanel.put_Tag(app);
			Border border = new Border();
			border.put_Width(300.0);
			border.put_Height(200.0);
			border.put_Background(new SolidColorBrush(Color.FromArgb(byte.MaxValue, 0, 120, 215)));
			border.put_CornerRadius(new CornerRadius(0.0));
			Image image = new Image();
			image.put_Stretch(3);
			image.put_HorizontalAlignment(1);
			image.put_VerticalAlignment(1);
			if (!string.IsNullOrEmpty(app.IconUrl))
			{
				try
				{
					image.put_Source(new BitmapImage(new Uri(app.IconUrl)));
				}
				catch (Exception ex)
				{
					Debug.WriteLine("Failed to load image: " + app.IconUrl);
				}
			}
			WindowsRuntimeMarshal.AddEventHandler<ExceptionRoutedEventHandler>(new Func<ExceptionRoutedEventHandler, EventRegistrationToken>(image.add_ImageFailed), new Action<EventRegistrationToken>(image.remove_ImageFailed), new ExceptionRoutedEventHandler(this.Image_ImageFailed));
			border.put_Child(image);
			stackPanel.Children.Add(border);
			StackPanel stackPanel2 = new StackPanel();
			stackPanel2.put_Margin(new Thickness(0.0, 12.0, 0.0, 0.0));
			stackPanel2.put_HorizontalAlignment(0);
			stackPanel2.put_VerticalAlignment(0);
			stackPanel2.put_Width(300.0);
			TextBlock textBlock = new TextBlock();
			textBlock.put_Text(app.Name);
			textBlock.put_FontFamily(new FontFamily("Segoe UI"));
			textBlock.put_FontSize(18.0);
			textBlock.put_Foreground(new SolidColorBrush(Color.FromArgb(byte.MaxValue, 0, 0, 0)));
			textBlock.put_TextWrapping(2);
			textBlock.put_TextTrimming(2);
			textBlock.put_MaxHeight(44.0);
			textBlock.put_HorizontalAlignment(0);
			stackPanel2.Children.Add(textBlock);
			TextBlock textBlock2 = new TextBlock();
			textBlock2.put_Text(app.Publisher);
			textBlock2.put_FontFamily(new FontFamily("Segoe UI"));
			textBlock2.put_FontSize(14.0);
			textBlock2.put_Foreground(new SolidColorBrush(Color.FromArgb(byte.MaxValue, 102, 102, 102)));
			textBlock2.put_TextWrapping(1);
			textBlock2.put_TextTrimming(2);
			textBlock2.put_Margin(new Thickness(0.0, 6.0, 0.0, 0.0));
			textBlock2.put_HorizontalAlignment(0);
			stackPanel2.Children.Add(textBlock2);
			TextBlock textBlock3 = new TextBlock();
			textBlock3.put_Text("Version: " + app.Version);
			textBlock3.put_FontFamily(new FontFamily("Segoe UI"));
			textBlock3.put_FontSize(12.0);
			textBlock3.put_Foreground(new SolidColorBrush(Color.FromArgb(byte.MaxValue, 0, 120, 215)));
			textBlock3.put_TextWrapping(1);
			textBlock3.put_TextTrimming(2);
			textBlock3.put_Margin(new Thickness(0.0, 4.0, 0.0, 0.0));
			textBlock3.put_HorizontalAlignment(0);
			stackPanel2.Children.Add(textBlock3);
			StackPanel stackPanel3 = new StackPanel();
			stackPanel3.put_Orientation(1);
			stackPanel3.put_Margin(new Thickness(0.0, 8.0, 0.0, 0.0));
			stackPanel3.put_HorizontalAlignment(0);
			TextBlock textBlock4 = new TextBlock();
			textBlock4.put_Text(" • ");
			textBlock4.put_Foreground(new SolidColorBrush(Color.FromArgb(byte.MaxValue, 102, 102, 102)));
			textBlock4.put_FontSize(12.0);
			textBlock4.put_Margin(new Thickness(4.0, 0.0, 4.0, 0.0));
			stackPanel3.Children.Add(textBlock4);
			stackPanel2.Children.Add(stackPanel3);
			stackPanel.Children.Add(stackPanel2);
			WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(stackPanel.add_Tapped), new Action<EventRegistrationToken>(stackPanel.remove_Tapped), new TappedEventHandler(this.AppItem_Tapped));
			return stackPanel;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000042BC File Offset: 0x000024BC
		private void AppItem_Tapped(object sender, TappedRoutedEventArgs e)
		{
			try
			{
				StackPanel stackPanel = sender as StackPanel;
				if (stackPanel != null && stackPanel.Tag != null)
				{
					StoreApp storeApp = stackPanel.Tag as StoreApp;
					if (storeApp != null)
					{
						base.Frame.Navigate(typeof(AppDetailsPage), storeApp);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("AppItem_Tapped error: " + ex.Message);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00004344 File Offset: 0x00002544
		private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
		{
			try
			{
				Image image = sender as Image;
				Debug.WriteLine("Image failed to load: " + e.ErrorMessage);
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Image_ImageFailed error: " + ex.Message);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00004564 File Offset: 0x00002764
		[DebuggerStepThrough]
		private void ShowError(string message)
		{
			AllAppsPage.<ShowError>d__34 <ShowError>d__;
			<ShowError>d__.<>4__this = this;
			<ShowError>d__.message = message;
			<ShowError>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<ShowError>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <ShowError>d__.<>t__builder;
			<>t__builder.Start<AllAppsPage.<ShowError>d__34>(ref <ShowError>d__);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000045A8 File Offset: 0x000027A8
		private void SearchTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
		{
			if (e.Key == 13)
			{
				this.PerformSearch();
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000045D0 File Offset: 0x000027D0
		private void PerformSearch()
		{
			try
			{
				string text = "";
				if (this.SearchTextBox != null)
				{
					text = this.SearchTextBox.Text;
					if (text != null)
					{
						text = text.Trim();
					}
				}
				if (this.ClearSearchButton != null)
				{
					this.ClearSearchButton.put_Visibility(string.IsNullOrEmpty(text) ? 1 : 0);
				}
				if (string.IsNullOrEmpty(text))
				{
					this.ShowAllApps();
				}
				else
				{
					string text2 = text.ToLower();
					List<StoreApp> list = new List<StoreApp>();
					foreach (StoreApp storeApp in this.allApps)
					{
						bool flag = false;
						if (!string.IsNullOrEmpty(storeApp.Name) && storeApp.Name.ToLower().Contains(text2))
						{
							flag = true;
						}
						else if (!string.IsNullOrEmpty(storeApp.Publisher) && storeApp.Publisher.ToLower().Contains(text2))
						{
							flag = true;
						}
						else if (!string.IsNullOrEmpty(storeApp.Category) && storeApp.Category.ToLower().Contains(text2))
						{
							flag = true;
						}
						if (flag)
						{
							list.Add(storeApp);
						}
					}
					if (list.Count == 0)
					{
						this.NoResultsText.put_Text("No apps found for \"" + text + "\"");
						this.NoResultsPanel.put_Visibility(0);
						this.MainScrollViewer.put_Visibility(1);
					}
					else
					{
						this.UpdateAppsDisplay(list);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("PerformSearch error: " + ex.Message);
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000047FC File Offset: 0x000029FC
		private void ShowAllApps()
		{
			try
			{
				this.Row1.Children.Clear();
				this.Row2.Children.Clear();
				if (this.allApps.Count == 0)
				{
					this.NoResultsText.put_Text("No apps found");
					this.NoResultsPanel.put_Visibility(0);
					this.MainScrollViewer.put_Visibility(1);
				}
				else
				{
					int num = (int)Math.Ceiling((double)this.allApps.Count / 2.0);
					for (int i = 0; i < this.allApps.Count; i++)
					{
						StoreApp app = this.allApps[i];
						StackPanel stackPanel = this.CreateAppItem(app);
						if (i < num)
						{
							this.Row1.Children.Add(stackPanel);
						}
						else
						{
							this.Row2.Children.Add(stackPanel);
						}
					}
					this.MainScrollViewer.put_Visibility(0);
					this.NoResultsPanel.put_Visibility(1);
					this.LoadingPanel.put_Visibility(1);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("ShowAllApps error: " + ex.Message);
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00004964 File Offset: 0x00002B64
		private void UpdateAppsDisplay(List<StoreApp> appsToShow)
		{
			try
			{
				this.Row1.Children.Clear();
				this.Row2.Children.Clear();
				int num = (int)Math.Ceiling((double)appsToShow.Count / 2.0);
				for (int i = 0; i < appsToShow.Count; i++)
				{
					StoreApp app = appsToShow[i];
					StackPanel stackPanel = this.CreateAppItem(app);
					if (i < num)
					{
						this.Row1.Children.Add(stackPanel);
					}
					else
					{
						this.Row2.Children.Add(stackPanel);
					}
				}
				this.MainScrollViewer.put_Visibility(0);
				this.NoResultsPanel.put_Visibility(1);
				this.LoadingPanel.put_Visibility(1);
			}
			catch (Exception ex)
			{
				Debug.WriteLine("UpdateAppsDisplay error: " + ex.Message);
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00004A68 File Offset: 0x00002C68
		private void ClearSearchButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (this.SearchTextBox != null)
				{
					this.SearchTextBox.put_Text("");
				}
				if (this.ClearSearchButton != null)
				{
					this.ClearSearchButton.put_Visibility(1);
				}
				this.ShowAllApps();
			}
			catch (Exception ex)
			{
				Debug.WriteLine("ClearSearchButton_Click error: " + ex.Message);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00004AE8 File Offset: 0x00002CE8
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

		// Token: 0x0600001A RID: 26 RVA: 0x00004B6C File Offset: 0x00002D6C
		[DebuggerNonUserCode]
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this._contentLoaded)
			{
				this._contentLoaded = true;
				Application.LoadComponent(this, new Uri("ms-appx:///AllAppsPage.xaml"), 0);
				this.BackButton = (Button)base.FindName("BackButton");
				this.SearchTextBox = (TextBox)base.FindName("SearchTextBox");
				this.ClearSearchButton = (Button)base.FindName("ClearSearchButton");
				this.LoadingPanel = (StackPanel)base.FindName("LoadingPanel");
				this.NoResultsPanel = (StackPanel)base.FindName("NoResultsPanel");
				this.MainScrollViewer = (ScrollViewer)base.FindName("MainScrollViewer");
				this.TwoRowContainer = (StackPanel)base.FindName("TwoRowContainer");
				this.Row1 = (StackPanel)base.FindName("Row1");
				this.Row2 = (StackPanel)base.FindName("Row2");
				this.NoResultsText = (TextBlock)base.FindName("NoResultsText");
				this.LoadingRing = (ProgressRing)base.FindName("LoadingRing");
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00004C98 File Offset: 0x00002E98
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
			{
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uielement.add_Tapped), new Action<EventRegistrationToken>(uielement.remove_Tapped), new TappedEventHandler(this.AppItem_Tapped));
				break;
			}
			case 2:
			{
				Image image = (Image)target;
				WindowsRuntimeMarshal.AddEventHandler<ExceptionRoutedEventHandler>(new Func<ExceptionRoutedEventHandler, EventRegistrationToken>(image.add_ImageFailed), new Action<EventRegistrationToken>(image.remove_ImageFailed), new ExceptionRoutedEventHandler(this.Image_ImageFailed));
				break;
			}
			case 3:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.BackButton_Click));
				break;
			}
			case 4:
			{
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<KeyEventHandler>(new Func<KeyEventHandler, EventRegistrationToken>(uielement.add_KeyDown), new Action<EventRegistrationToken>(uielement.remove_KeyDown), new KeyEventHandler(this.SearchTextBox_KeyDown));
				break;
			}
			case 5:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.ClearSearchButton_Click));
				break;
			}
			}
			this._contentLoaded = true;
		}

		// Token: 0x04000003 RID: 3
		private const string AppsJsonUrl = "https://8store.dankassassin368.com/data/apps.json";

		// Token: 0x04000004 RID: 4
		private const string CACHE_FILE_NAME = "apps_cache.json";

		// Token: 0x04000005 RID: 5
		private ObservableCollection<StoreApp> allApps = new ObservableCollection<StoreApp>();

		// Token: 0x04000006 RID: 6
		private ObservableCollection<StoreApp> filteredApps = new ObservableCollection<StoreApp>();

		// Token: 0x04000007 RID: 7
		private static List<StoreApp> cachedApps = null;

		// Token: 0x04000008 RID: 8
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button BackButton;

		// Token: 0x04000009 RID: 9
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBox SearchTextBox;

		// Token: 0x0400000A RID: 10
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button ClearSearchButton;

		// Token: 0x0400000B RID: 11
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel LoadingPanel;

		// Token: 0x0400000C RID: 12
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel NoResultsPanel;

		// Token: 0x0400000D RID: 13
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ScrollViewer MainScrollViewer;

		// Token: 0x0400000E RID: 14
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel TwoRowContainer;

		// Token: 0x0400000F RID: 15
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel Row1;

		// Token: 0x04000010 RID: 16
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel Row2;

		// Token: 0x04000011 RID: 17
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock NoResultsText;

		// Token: 0x04000012 RID: 18
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ProgressRing LoadingRing;

		// Token: 0x04000013 RID: 19
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
