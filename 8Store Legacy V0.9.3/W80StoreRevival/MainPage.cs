using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media.Imaging;

namespace W80StoreRevival
{
	// Token: 0x0200000A RID: 10
	public sealed class MainPage : Page, IComponentConnector
	{
		// Token: 0x06000066 RID: 102 RVA: 0x00009CAC File Offset: 0x00007EAC
		public MainPage()
		{
			this.InitializeComponent();
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(base.add_Loaded), new Action<EventRegistrationToken>(base.remove_Loaded), new RoutedEventHandler(this.MainPage_Loaded));
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00009D00 File Offset: 0x00007F00
		private void MainPage_Loaded(object sender, RoutedEventArgs e)
		{
			this.LoadStaffPicksApps();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00009D0C File Offset: 0x00007F0C
		private void LoadStaffPicksApps()
		{
			try
			{
				List<StoreApp> list = new List<StoreApp>();
				list.Add(new StoreApp
				{
					Id = "1",
					Name = "MSN News",
					Publisher = "Microsoft Corp",
					Version = "1.0.0",
					DownloadUrl = "https://appxboxdl.dankassassin368.com/APPX/Retiled%20Project/newRetiled/8.0/9200/News/x86/Microsoft.BingNews_2.0.0.320_x86__8wekyb3d8bbwe.appx",
					IconUrl = "https://i.ibb.co//dwf9xVQn//windows-8-news.png",
					Description = "Stay informed with the latest news from around the world.",
					Category = "News"
				});
				list.Add(new StoreApp
				{
					Id = "2",
					Name = "Windows Phone App",
					Publisher = "Microsoft",
					Version = "1.2.0",
					DownloadUrl = "https://appxboxdl.dankassassin368.com/APPX/Windows%208.1%20APPX%20Files/Apps/Windows%20Phone%20App/wpapp.appx",
					IconUrl = "https://encrypted-tbn0.gstatic.com//images?q=tbn:ANd9GcRruPlrHDjzclLCHHSo3zsAlGGfZviV8WOwIw&amp;s",
					Description = "A comprehensive Windows Phone companion app.",
					Category = "Productivity"
				});
				list.Add(new StoreApp
				{
					Id = "3",
					Name = "OpenTracks",
					Publisher = "ChockingNetDude",
					Version = "0.4.0",
					DownloadUrl = "https://dl.dankassassin368.com/opentracksb4_-_copia.appx",
					IconUrl = "https://i.ibb.co/Rkbr6LgC/757de76ad56f84e8eb563b9629468167.png",
					Description = "Discover music like never before with this Windows 8.1 music app.",
					Category = "Music"
				});
				list.Add(new StoreApp
				{
					Id = "4",
					Name = "AccuWeather",
					Publisher = "AccuWeather Inc.",
					Version = "3.5.1",
					DownloadUrl = "https://appxboxdl.dankassassin368.com/APPX/Retiled%20Project/Apptravaganza%21/AccuWeather/AccuWeather%28x86%29.appx",
					IconUrl = "https://www.accuweather.com//images//components//core//base-image-color.jpg",
					Description = "Get the most accurate weather forecasts with AccuWeather.",
					Category = "Weather"
				});
				list.Add(new StoreApp
				{
					Id = "5",
					Name = "Coming Soon",
					Publisher = "Microsoft Corp",
					Version = "8.0",
					DownloadUrl = "https://example.com/office.appx",
					IconUrl = "https://store-images.s-microsoft.com/image/apps.12523.9007199266244428.3e1f3e7a-5c1c-4b5e-8b0a-5b5b5b5b5b5b",
					Description = "Microsoft Office for Windows 8.",
					Category = "Office"
				});
				list.Add(new StoreApp
				{
					Id = "6",
					Name = "Coming Soon",
					Publisher = "Adobe Inc.",
					Version = "8.0",
					DownloadUrl = "https://example.com/photoshop.appx",
					IconUrl = "https://store-images.s-microsoft.com/image/apps.34567.9007199266244428.87654321-1234-1234-1234-123456789012",
					Description = "Edit photos on your Windows 8 device.",
					Category = "Photo"
				});
				list.Add(new StoreApp
				{
					Id = "7",
					Name = "Coming Soon",
					Publisher = "Netflix, Inc.",
					Version = "8.0",
					DownloadUrl = "https://example.com/netflix.appx",
					IconUrl = "https://store-images.s-microsoft.com/image/apps.56789.9007199266244428.12345678-5432-5432-5432-321098765432",
					Description = "Watch TV shows and movies on Netflix.",
					Category = "Entertainment"
				});
				list.Add(new StoreApp
				{
					Id = "8",
					Name = "Coming Soon",
					Publisher = "Spotify AB",
					Version = "8.0",
					DownloadUrl = "https://example.com/spotify.appx",
					IconUrl = "https://store-images.s-microsoft.com/image/apps.67890.9007199266244428.23456789-1357-1357-1357-135724681357",
					Description = "Listen to music on Spotify.",
					Category = "Music"
				});
				List<StoreApp> list2 = list;
				this.staffPicksApps.Clear();
				this.StaffPicksRow1.Children.Clear();
				this.StaffPicksRow2.Children.Clear();
				foreach (StoreApp storeApp in list2)
				{
					StoreApp storeApp;
					this.staffPicksApps.Add(storeApp);
				}
				int num = (int)Math.Ceiling((double)list2.Count / 2.0);
				for (int i = 0; i < list2.Count; i++)
				{
					StoreApp storeApp = list2[i];
					ContentControl contentControl = new ContentControl();
					contentControl.put_ContentTemplate((DataTemplate)base.Resources["SquareAppItemTemplate"]);
					contentControl.put_Content(storeApp);
					contentControl.put_Tag(storeApp.Id);
					if (i < num)
					{
						this.StaffPicksRow1.Children.Add(contentControl);
					}
					else
					{
						this.StaffPicksRow2.Children.Add(contentControl);
					}
				}
				Debug.WriteLine("Loaded {staffPicks.Count} staff picks apps in {appsPerRow} + {staffPicks.Count - appsPerRow} row layout");
			}
			catch (Exception ex)
			{
				Debug.WriteLine("LoadStaffPicksApps error: " + ex.Message);
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000A270 File Offset: 0x00008470
		private void Border1_Tapped(object sender, TappedRoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(UpdateW8));
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000A28C File Offset: 0x0000848C
		private void Border_Tapped(object sender, TappedRoutedEventArgs e)
		{
			StoreApp storeApp = new StoreApp
			{
				Id = "0",
				Name = "Submit Your App",
				Publisher = "8Store Team",
				Version = "1.0.0",
				DownloadUrl = "",
				IconUrl = "Assets/spotlight_test.png",
				Description = "Help 8Store grow by submitting your own Windows 8 apps! We welcome developers to share their creations with our community. Your app could be featured here and help make the Windows 8 ecosystem vibrant again.",
				Featured = true,
				Type = "Information",
				Category = "Development",
				Screenshot1 = "https://i.ibb.co/4Zddzw6c/Screenshot-2026-02-04-225149.png"
			};
			base.Frame.Navigate(typeof(AppDetailsPage), storeApp);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000A33C File Offset: 0x0000853C
		private void StaffPickAppItem_Tapped(object sender, TappedRoutedEventArgs e)
		{
			try
			{
				ContentControl contentControl = sender as ContentControl;
				if (contentControl != null && contentControl.Content != null)
				{
					StoreApp storeApp = contentControl.Content as StoreApp;
					if (storeApp != null)
					{
						StoreApp fullAppDetails = this.GetFullAppDetails(storeApp.Id);
						if (fullAppDetails != null)
						{
							base.Frame.Navigate(typeof(AppDetailsPage), fullAppDetails);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("StaffPickAppItem_Tapped error: " + ex.Message);
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000A3E4 File Offset: 0x000085E4
		private StoreApp GetFullAppDetails(string id)
		{
			if (id != null)
			{
				if (<PrivateImplementationDetails>{A3A91DA2-38F6-4321-9787-FB35B90B83DF}.$$method0x6000062-1 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(8);
					dictionary.Add("1", 0);
					dictionary.Add("2", 1);
					dictionary.Add("3", 2);
					dictionary.Add("4", 3);
					dictionary.Add("5", 4);
					dictionary.Add("6", 5);
					dictionary.Add("7", 6);
					dictionary.Add("8", 7);
					<PrivateImplementationDetails>{A3A91DA2-38F6-4321-9787-FB35B90B83DF}.$$method0x6000062-1 = dictionary;
				}
				int num;
				if (<PrivateImplementationDetails>{A3A91DA2-38F6-4321-9787-FB35B90B83DF}.$$method0x6000062-1.TryGetValue(id, ref num))
				{
					switch (num)
					{
					case 0:
						return new StoreApp
						{
							Id = "1",
							Name = "MSN News (RETILED)",
							Publisher = "Microsoft Corporation",
							Version = "1.0.0",
							DownloadUrl = "https://appxboxdl.dankassassin368.com/APPX/Retiled%20Project/newRetiled/8.0/9200/News/x86/Microsoft.BingNews_2.0.0.320_x86__8wekyb3d8bbwe.appx",
							IconUrl = "https://i.ibb.co//dwf9xVQn//windows-8-news.png",
							Description = "Stay informed with the latest news from around the world. MSN News delivers breaking news, trending stories, and personalized content to keep you updated on topics that matter to you.",
							Featured = true,
							Type = "App",
							Category = "News",
							Screenshot1 = "https://i.ibb.co/prJbvZh8/Screenshot-2026-02-09-120503.png",
							Screenshot2 = "https://i.ibb.co/VcdnXLZb/Screenshot-2026-02-09-120628.png",
							Screenshot3 = "https://i.ibb.co/vx3MjBsQ/Screenshot-2026-02-09-120621.png"
						};
					case 1:
						return new StoreApp
						{
							Id = "3",
							Name = "Windows Phone App",
							Publisher = "Microsoft",
							Version = "1.2.0",
							DownloadUrl = "https://appxboxdl.dankassassin368.com/APPX/Windows%208.1%20APPX%20Files/Apps/Windows%20Phone%20App/wpapp.appx",
							IconUrl = "https://encrypted-tbn0.gstatic.com//images?q=tbn:ANd9GcRruPlrHDjzclLCHHSo3zsAlGGfZviV8WOwIw&amp;s",
							Description = "A comprehensive Windows Phone companion app that helps you manage your device, sync content, and access exclusive features. Connect your Windows Phone to your PC seamlessly.",
							Featured = true,
							Type = "App",
							Category = "Productivity"
						};
					case 2:
						return new StoreApp
						{
							Id = "2",
							Name = "OpenTracks Desktop Service",
							Publisher = "ChockingNetDude",
							Version = "0.4.0",
							DownloadUrl = "https://dl.dankassassin368.com/opentracksb4_-_copia.appx",
							IconUrl = "https://i.ibb.co/Rkbr6LgC/757de76ad56f84e8eb563b9629468167.png",
							Description = "OpenTracks Desktop is here!! Discover music like never before with my Windows 8.1 music app, designed for fans of the latest hits. Featuring a library of over 100 tracks, the app brings together top charts and trending songs in one place.",
							Featured = true,
							Type = "App",
							Category = "Music + Audio",
							Screenshot1 = "https://i.ibb.co/QvV3xj7C/Screenshot-2026-02-05-000912.png",
							Screenshot2 = "https://i.ibb.co/KxZTPgND/Screenshot-2026-02-06-111141.png",
							Screenshot3 = "https://i.ibb.co/whQnt661/Screenshot-2026-02-06-111211.png"
						};
					case 3:
						return new StoreApp
						{
							Id = "4",
							Name = "AccuWeather",
							Publisher = "AccuWeather Inc.",
							Version = "3.5.1",
							DownloadUrl = "https://appxboxdl.dankassassin368.com/APPX/Retiled%20Project/Apptravaganza%21/AccuWeather/AccuWeather%28x86%29.appx",
							IconUrl = "https://www.accuweather.com//images//components//core//base-image-color.jpg",
							Description = "Get the most accurate weather forecasts with AccuWeather. Real-time updates, severe weather alerts, and detailed forecasts for any location worldwide.",
							Featured = true,
							Type = "App",
							Category = "Weather"
						};
					case 4:
						return new StoreApp
						{
							Id = "5",
							Name = "Microsoft Office",
							Publisher = "Microsoft Corporation",
							Version = "8.0",
							DownloadUrl = "https://example.com/office.appx",
							IconUrl = "https://store-images.s-microsoft.com/image/apps.12523.9007199266244428.3e1f3e7a-5c1c-4b5e-8b0a-5b5b5b5b5b5b",
							Description = "Microsoft Office for Windows 8. Experience Word, Excel, PowerPoint and more with a touch-optimized interface designed for Windows 8.",
							Featured = true,
							Type = "App",
							Category = "Productivity"
						};
					case 5:
						return new StoreApp
						{
							Id = "6",
							Name = "Adobe Photoshop Express",
							Publisher = "Adobe Inc.",
							Version = "8.0",
							DownloadUrl = "https://example.com/photoshop.appx",
							IconUrl = "https://store-images.s-microsoft.com/image/apps.34567.9007199266244428.87654321-1234-1234-1234-123456789012",
							Description = "Edit photos on your Windows 8 device with Adobe Photoshop Express. Crop, rotate, adjust colors and apply filters to your images.",
							Featured = true,
							Type = "App",
							Category = "Photo & Video"
						};
					case 6:
						return new StoreApp
						{
							Id = "7",
							Name = "Netflix",
							Publisher = "Netflix, Inc.",
							Version = "8.0",
							DownloadUrl = "https://example.com/netflix.appx",
							IconUrl = "https://store-images.s-microsoft.com/image/apps.56789.9007199266244428.12345678-5432-5432-5432-321098765432",
							Description = "Watch TV shows and movies on Netflix. Stream thousands of titles directly to your Windows 8 device.",
							Featured = true,
							Type = "App",
							Category = "Entertainment"
						};
					case 7:
						return new StoreApp
						{
							Id = "8",
							Name = "Spotify Music",
							Publisher = "Spotify AB",
							Version = "8.0",
							DownloadUrl = "https://example.com/spotify.appx",
							IconUrl = "https://store-images.s-microsoft.com/image/apps.67890.9007199266244428.23456789-1357-1357-1357-135724681357",
							Description = "Listen to music on Spotify. Stream millions of songs and create playlists for every mood.",
							Featured = true,
							Type = "App",
							Category = "Music"
						};
					}
				}
			}
			return null;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000A940 File Offset: 0x00008B40
		private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
		{
			try
			{
				Image image = sender as Image;
				if (image != null)
				{
					image.put_Source(new BitmapImage(new Uri("ms-appx:///Assets/StoreLogo.png")));
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000A990 File Offset: 0x00008B90
		private void AllAppsButton_Tapped(object sender, TappedRoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(AllAppsPage));
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000A9A9 File Offset: 0x00008BA9
		private void CollectionsButton_Tapped(object sender, TappedRoutedEventArgs e)
		{
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000A9AC File Offset: 0x00008BAC
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (!this._contentLoaded)
			{
				this._contentLoaded = true;
				Application.LoadComponent(this, new Uri("ms-appx:///MainPage.xaml"), 0);
				this.StaffPicksScrollViewer = (ScrollViewer)base.FindName("StaffPicksScrollViewer");
				this.StaffPicksTwoRowContainer = (StackPanel)base.FindName("StaffPicksTwoRowContainer");
				this.StaffPicksRow1 = (StackPanel)base.FindName("StaffPicksRow1");
				this.StaffPicksRow2 = (StackPanel)base.FindName("StaffPicksRow2");
				this.SpotlightImage = (Image)base.FindName("SpotlightImage");
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000AA54 File Offset: 0x00008C54
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
			{
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uielement.add_Tapped), new Action<EventRegistrationToken>(uielement.remove_Tapped), new TappedEventHandler(this.StaffPickAppItem_Tapped));
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
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uielement.add_Tapped), new Action<EventRegistrationToken>(uielement.remove_Tapped), new TappedEventHandler(this.AllAppsButton_Tapped));
				break;
			}
			case 4:
			{
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uielement.add_Tapped), new Action<EventRegistrationToken>(uielement.remove_Tapped), new TappedEventHandler(this.CollectionsButton_Tapped));
				break;
			}
			case 5:
			{
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uielement.add_Tapped), new Action<EventRegistrationToken>(uielement.remove_Tapped), new TappedEventHandler(this.Border_Tapped));
				break;
			}
			case 6:
			{
				UIElement uielement = (UIElement)target;
				WindowsRuntimeMarshal.AddEventHandler<TappedEventHandler>(new Func<TappedEventHandler, EventRegistrationToken>(uielement.add_Tapped), new Action<EventRegistrationToken>(uielement.remove_Tapped), new TappedEventHandler(this.Border1_Tapped));
				break;
			}
			}
			this._contentLoaded = true;
		}

		// Token: 0x04000047 RID: 71
		private ObservableCollection<StoreApp> staffPicksApps = new ObservableCollection<StoreApp>();

		// Token: 0x04000048 RID: 72
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ScrollViewer StaffPicksScrollViewer;

		// Token: 0x04000049 RID: 73
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel StaffPicksTwoRowContainer;

		// Token: 0x0400004A RID: 74
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel StaffPicksRow1;

		// Token: 0x0400004B RID: 75
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel StaffPicksRow2;

		// Token: 0x0400004C RID: 76
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Image SpotlightImage;

		// Token: 0x0400004D RID: 77
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
