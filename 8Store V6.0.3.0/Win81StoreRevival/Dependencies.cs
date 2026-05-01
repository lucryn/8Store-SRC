using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;

namespace Win81StoreRevival
{
	// Token: 0x02000023 RID: 35
	public sealed class Dependencies : Page, IComponentConnector
	{
		// Token: 0x060001D9 RID: 473 RVA: 0x0000BEFE File Offset: 0x0000A0FE
		public Dependencies()
		{
			this.InitializeComponent();
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000BF0F File Offset: 0x0000A10F
		private void TopAppsButton_Click(object sender, RoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(MainPage));
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000BF28 File Offset: 0x0000A128
		[DebuggerStepThrough]
		private void Border_Tapped(object sender, TappedRoutedEventArgs e)
		{
			Dependencies.<Border_Tapped>d__2 <Border_Tapped>d__ = new Dependencies.<Border_Tapped>d__2();
			<Border_Tapped>d__.<>4__this = this;
			<Border_Tapped>d__.sender = sender;
			<Border_Tapped>d__.e = e;
			<Border_Tapped>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<Border_Tapped>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <Border_Tapped>d__.<>t__builder;
			<>t__builder.Start<Dependencies.<Border_Tapped>d__2>(ref <Border_Tapped>d__);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000BF74 File Offset: 0x0000A174
		private void MSNNews_Tapped(object sender, TappedRoutedEventArgs e)
		{
			StoreApp storeApp = new StoreApp
			{
				Id = "3",
				Name = "Cut the Rope!",
				Publisher = "Zepto Lab",
				Version = "1.2.0.23",
				DownloadUrl = "https://dankassassin368.com/Stuff/APPX/Retiled%20Project/Apptravaganza!/Cut%20The%20Rope/8.1/x86/ZeptoLabUKLimited.CutTheRope_1.2.0.43_x86__sq9zxnwrk84pj.appx",
				IconUrl = "https://is2-ssl.mzstatic.com/image/thumb/Purple2/v4/57/2e/c3/572ec341-c56a-00c6-7486-fe80c30d709c/pr_source.png/0x0ss-85.jpg",
				Description = "Literally a news app from Microsoft",
				Featured = false
			};
			base.Frame.Navigate(typeof(AppDetailsPage), storeApp);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000BFFC File Offset: 0x0000A1FC
		private void OTS_Tapped(object sender, TappedRoutedEventArgs e)
		{
			StoreApp storeApp = new StoreApp
			{
				Id = "1",
				Name = "OpenTracks Desktop",
				Publisher = "ChockingNetDude",
				Version = "0.3.0",
				DownloadUrl = "http://droidjacks4.helioho.st/OpenTracksDesktop_B3.appx",
				IconUrl = "https://icns-pl1.ls.olcdns.com//7ffea5b7ce28eff1a181946f2266535d.png",
				Description = "Your best Online Music App!",
				Featured = false
			};
			base.Frame.Navigate(typeof(AppDetailsPage), storeApp);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000C084 File Offset: 0x0000A284
		private void Fresh_Tapped(object sender, TappedRoutedEventArgs e)
		{
			StoreApp storeApp = new StoreApp
			{
				Id = "70",
				Name = "Windows Phone App",
				Publisher = "Microsoft",
				Version = "2014.0.8400",
				DownloadUrl = "https://dankassassin368.com//Stuff//APPX//Windows%208.1%20APPX%20Files//Apps//Windows%20Phone%20App//wpapp.appx",
				IconUrl = "https://encrypted-tbn0.gstatic.com//images?q=tbn:ANd9GcRruPlrHDjzclLCHHSo3zsAlGGfZviV8WOwIw&s",
				Description = "Except Marketplace, other all works!",
				Featured = false
			};
			base.Frame.Navigate(typeof(AppDetailsPage), storeApp);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000C10C File Offset: 0x0000A30C
		private void Accu_Tapped(object sender, TappedRoutedEventArgs e)
		{
			StoreApp storeApp = new StoreApp
			{
				Id = "7",
				Name = "AccuWeather",
				Publisher = "AccuWeather",
				Version = "2.1.7.2",
				DownloadUrl = "https://dankassassin368.com//Stuff//APPX//Windows%208.1%20APPX%20Files//Apps//AccuWeather//AccuWeather.AccuWeatherforWindows8_2.1.7.2_x64__8zz2pj9h1h1d8.appx",
				IconUrl = "https://www.accuweather.com//images//components//core//base-image-color.jpg",
				Description = "Weather forecast application",
				Featured = true
			};
			base.Frame.Navigate(typeof(AppDetailsPage), storeApp);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00005867 File Offset: 0x00003A67
		private void Border_Tapped_1(object sender, TappedRoutedEventArgs e)
		{
			base.Frame.Navigate(typeof(AllAppsPage));
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000C194 File Offset: 0x0000A394
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			bool contentLoaded = this._contentLoaded;
			if (!contentLoaded)
			{
				this._contentLoaded = true;
				Application.LoadComponent(this, new Uri("ms-appx:///Dependencies.xaml"), 0);
				this.MainStorePage = (Page)base.FindName("MainStorePage");
				this.MainHub = (Hub)base.FindName("MainHub");
				this.SpotlightSection = (HubSection)base.FindName("SpotlightSection");
				this.TopAppsButton = (Button)base.FindName("TopAppsButton");
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000C220 File Offset: 0x0000A420
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.TopAppsButton_Click));
			}
			this._contentLoaded = true;
		}

		// Token: 0x040000F2 RID: 242
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Page MainStorePage;

		// Token: 0x040000F3 RID: 243
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Hub MainHub;

		// Token: 0x040000F4 RID: 244
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private HubSection SpotlightSection;

		// Token: 0x040000F5 RID: 245
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button TopAppsButton;

		// Token: 0x040000F6 RID: 246
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
