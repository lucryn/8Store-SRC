using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;

namespace Win81StoreRevival
{
	// Token: 0x02000028 RID: 40
	public sealed class ExtendedSplash : Page, IComponentConnector
	{
		// Token: 0x0600023D RID: 573 RVA: 0x0000D548 File Offset: 0x0000B748
		public ExtendedSplash(SplashScreen splashscreen)
		{
			this.InitializeComponent();
			this.SetRandomSplashText();
			this.localSettings = ApplicationData.Current.LocalSettings;
			Debug.WriteLine("[ExtendedSplash] Constructor called");
			Window window = Window.Current;
			WindowsRuntimeMarshal.AddEventHandler<WindowSizeChangedEventHandler>(new Func<WindowSizeChangedEventHandler, EventRegistrationToken>(window.add_SizeChanged), new Action<EventRegistrationToken>(window.remove_SizeChanged), new WindowSizeChangedEventHandler(this.ExtendedSplash_OnResize));
			this.splash = splashscreen;
			bool flag = this.splash != null;
			if (flag)
			{
				SplashScreen splashScreen = this.splash;
				WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<SplashScreen, object>>(new Func<TypedEventHandler<SplashScreen, object>, EventRegistrationToken>(splashScreen.add_Dismissed), new Action<EventRegistrationToken>(splashScreen.remove_Dismissed), new TypedEventHandler<SplashScreen, object>(this.DismissedEventHandler));
				this.splashImageRect = this.splash.ImageLocation;
				this.PositionImage();
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(this.add_Loaded), new Action<EventRegistrationToken>(this.remove_Loaded), delegate(object s, RoutedEventArgs e)
				{
					this.PositionLoadingContainer();
				});
			}
			this.PlayUpdateSound();
			this.InitializeAndLoadData();
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000D668 File Offset: 0x0000B868
		[DebuggerStepThrough]
		private Task InitializeAndLoadData()
		{
			ExtendedSplash.<InitializeAndLoadData>d__11 <InitializeAndLoadData>d__ = new ExtendedSplash.<InitializeAndLoadData>d__11();
			<InitializeAndLoadData>d__.<>4__this = this;
			<InitializeAndLoadData>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<InitializeAndLoadData>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <InitializeAndLoadData>d__.<>t__builder;
			<>t__builder.Start<ExtendedSplash.<InitializeAndLoadData>d__11>(ref <InitializeAndLoadData>d__);
			return <InitializeAndLoadData>d__.<>t__builder.Task;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000D6B0 File Offset: 0x0000B8B0
		[DebuggerStepThrough]
		private Task InitializeCacheAsync()
		{
			ExtendedSplash.<InitializeCacheAsync>d__12 <InitializeCacheAsync>d__ = new ExtendedSplash.<InitializeCacheAsync>d__12();
			<InitializeCacheAsync>d__.<>4__this = this;
			<InitializeCacheAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<InitializeCacheAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <InitializeCacheAsync>d__.<>t__builder;
			<>t__builder.Start<ExtendedSplash.<InitializeCacheAsync>d__12>(ref <InitializeCacheAsync>d__);
			return <InitializeCacheAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000D6F8 File Offset: 0x0000B8F8
		[DebuggerStepThrough]
		private Task LoadAppData()
		{
			ExtendedSplash.<LoadAppData>d__13 <LoadAppData>d__ = new ExtendedSplash.<LoadAppData>d__13();
			<LoadAppData>d__.<>4__this = this;
			<LoadAppData>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadAppData>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadAppData>d__.<>t__builder;
			<>t__builder.Start<ExtendedSplash.<LoadAppData>d__13>(ref <LoadAppData>d__);
			return <LoadAppData>d__.<>t__builder.Task;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000D740 File Offset: 0x0000B940
		[DebuggerStepThrough]
		private Task LoadAppsFromNetworkAsync()
		{
			ExtendedSplash.<LoadAppsFromNetworkAsync>d__14 <LoadAppsFromNetworkAsync>d__ = new ExtendedSplash.<LoadAppsFromNetworkAsync>d__14();
			<LoadAppsFromNetworkAsync>d__.<>4__this = this;
			<LoadAppsFromNetworkAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadAppsFromNetworkAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadAppsFromNetworkAsync>d__.<>t__builder;
			<>t__builder.Start<ExtendedSplash.<LoadAppsFromNetworkAsync>d__14>(ref <LoadAppsFromNetworkAsync>d__);
			return <LoadAppsFromNetworkAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000D788 File Offset: 0x0000B988
		[DebuggerStepThrough]
		private Task<bool> TryLoadAppsFromCache()
		{
			ExtendedSplash.<TryLoadAppsFromCache>d__15 <TryLoadAppsFromCache>d__ = new ExtendedSplash.<TryLoadAppsFromCache>d__15();
			<TryLoadAppsFromCache>d__.<>4__this = this;
			<TryLoadAppsFromCache>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<TryLoadAppsFromCache>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<bool> <>t__builder = <TryLoadAppsFromCache>d__.<>t__builder;
			<>t__builder.Start<ExtendedSplash.<TryLoadAppsFromCache>d__15>(ref <TryLoadAppsFromCache>d__);
			return <TryLoadAppsFromCache>d__.<>t__builder.Task;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000D7D0 File Offset: 0x0000B9D0
		[DebuggerStepThrough]
		private Task LoadAppsFromNetworkAndUpdateCache()
		{
			ExtendedSplash.<LoadAppsFromNetworkAndUpdateCache>d__16 <LoadAppsFromNetworkAndUpdateCache>d__ = new ExtendedSplash.<LoadAppsFromNetworkAndUpdateCache>d__16();
			<LoadAppsFromNetworkAndUpdateCache>d__.<>4__this = this;
			<LoadAppsFromNetworkAndUpdateCache>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadAppsFromNetworkAndUpdateCache>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadAppsFromNetworkAndUpdateCache>d__.<>t__builder;
			<>t__builder.Start<ExtendedSplash.<LoadAppsFromNetworkAndUpdateCache>d__16>(ref <LoadAppsFromNetworkAndUpdateCache>d__);
			return <LoadAppsFromNetworkAndUpdateCache>d__.<>t__builder.Task;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000D818 File Offset: 0x0000BA18
		[DebuggerStepThrough]
		private Task RefreshCacheInBackground()
		{
			ExtendedSplash.<RefreshCacheInBackground>d__17 <RefreshCacheInBackground>d__ = new ExtendedSplash.<RefreshCacheInBackground>d__17();
			<RefreshCacheInBackground>d__.<>4__this = this;
			<RefreshCacheInBackground>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<RefreshCacheInBackground>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <RefreshCacheInBackground>d__.<>t__builder;
			<>t__builder.Start<ExtendedSplash.<RefreshCacheInBackground>d__17>(ref <RefreshCacheInBackground>d__);
			return <RefreshCacheInBackground>d__.<>t__builder.Task;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000D860 File Offset: 0x0000BA60
		[DebuggerStepThrough]
		private Task UpdateCacheAsync(List<StoreApp> apps)
		{
			ExtendedSplash.<UpdateCacheAsync>d__18 <UpdateCacheAsync>d__ = new ExtendedSplash.<UpdateCacheAsync>d__18();
			<UpdateCacheAsync>d__.<>4__this = this;
			<UpdateCacheAsync>d__.apps = apps;
			<UpdateCacheAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateCacheAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <UpdateCacheAsync>d__.<>t__builder;
			<>t__builder.Start<ExtendedSplash.<UpdateCacheAsync>d__18>(ref <UpdateCacheAsync>d__);
			return <UpdateCacheAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000D8B0 File Offset: 0x0000BAB0
		[DebuggerStepThrough]
		private Task CheckForUpdates()
		{
			ExtendedSplash.<CheckForUpdates>d__19 <CheckForUpdates>d__ = new ExtendedSplash.<CheckForUpdates>d__19();
			<CheckForUpdates>d__.<>4__this = this;
			<CheckForUpdates>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<CheckForUpdates>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <CheckForUpdates>d__.<>t__builder;
			<>t__builder.Start<ExtendedSplash.<CheckForUpdates>d__19>(ref <CheckForUpdates>d__);
			return <CheckForUpdates>d__.<>t__builder.Task;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000D8F8 File Offset: 0x0000BAF8
		[DebuggerStepThrough]
		private Task ProcessAutoLogin()
		{
			ExtendedSplash.<ProcessAutoLogin>d__20 <ProcessAutoLogin>d__ = new ExtendedSplash.<ProcessAutoLogin>d__20();
			<ProcessAutoLogin>d__.<>4__this = this;
			<ProcessAutoLogin>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ProcessAutoLogin>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <ProcessAutoLogin>d__.<>t__builder;
			<>t__builder.Start<ExtendedSplash.<ProcessAutoLogin>d__20>(ref <ProcessAutoLogin>d__);
			return <ProcessAutoLogin>d__.<>t__builder.Task;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000D940 File Offset: 0x0000BB40
		[DebuggerStepThrough]
		private void DismissExtendedSplash()
		{
			ExtendedSplash.<DismissExtendedSplash>d__21 <DismissExtendedSplash>d__ = new ExtendedSplash.<DismissExtendedSplash>d__21();
			<DismissExtendedSplash>d__.<>4__this = this;
			<DismissExtendedSplash>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<DismissExtendedSplash>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <DismissExtendedSplash>d__.<>t__builder;
			<>t__builder.Start<ExtendedSplash.<DismissExtendedSplash>d__21>(ref <DismissExtendedSplash>d__);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00005833 File Offset: 0x00003A33
		private void PlayUpdateSound()
		{
			SoundManager.PlayStartupSound();
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000D97C File Offset: 0x0000BB7C
		private void SetRandomSplashText()
		{
			try
			{
				DateTime now = DateTime.Now;
				string text = null;
				bool flag = now.Month == 1 && now.Day == 1;
				if (flag)
				{
					text = "Happy new year everyone!!1!!!";
				}
				else
				{
					bool flag2 = (now.Year == 2026 && now.Month == 2 && now.Day == 17) || (now.Year == 2027 && now.Month == 2 && now.Day == 6) || (now.Year == 2028 && now.Month == 1 && now.Day == 26);
					if (flag2)
					{
						text = "Happy Lunar New Year.... 新年快樂!";
					}
				}
				string[] array = new string[]
				{
					"Welcome back... Time to start your app exploring adventure",
					"8NetLive | Feel Connected, Simple, Smooth"
				};
				Random random = new Random();
				int num = random.Next(0, array.Length);
				bool flag3 = text != null;
				if (flag3)
				{
					this.splashTextBlock.put_Text(text);
				}
				else
				{
					this.splashTextBlock.put_Text(array[num]);
				}
				Debug.WriteLine("[ExtendedSplash] Set splash text: " + this.splashTextBlock.Text);
			}
			catch (Exception ex)
			{
				Debug.WriteLine("[ExtendedSplash] Error setting random text: " + ex.Message);
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000DAE4 File Offset: 0x0000BCE4
		private void PositionImage()
		{
			try
			{
				this.extendedSplashImage.SetValue(Canvas.LeftProperty, this.splashImageRect.X);
				this.extendedSplashImage.SetValue(Canvas.TopProperty, this.splashImageRect.Y);
				this.extendedSplashImage.put_Height(this.splashImageRect.Height);
				this.extendedSplashImage.put_Width(this.splashImageRect.Width);
			}
			catch (Exception ex)
			{
				Debug.WriteLine("[ExtendedSplash] Error positioning image: " + ex.Message);
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000DB94 File Offset: 0x0000BD94
		private void PositionLoadingContainer()
		{
			try
			{
				bool flag = this.loadingContainer == null;
				if (!flag)
				{
					bool flag2 = this.loadingContainer.ActualWidth == 0.0;
					if (flag2)
					{
						base.Dispatcher.RunAsync(0, delegate()
						{
							this.PositionLoadingContainer();
						});
					}
					else
					{
						double num = this.splashImageRect.Y + this.splashImageRect.Height + 32.0;
						double num2 = this.splashImageRect.X + this.splashImageRect.Width * 0.5;
						this.loadingContainer.SetValue(Canvas.LeftProperty, num2);
						this.loadingContainer.SetValue(Canvas.TopProperty, num);
						TranslateTransform translateTransform = new TranslateTransform();
						translateTransform.put_X(-this.loadingContainer.ActualWidth * 0.5);
						this.loadingContainer.put_RenderTransform(translateTransform);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("[ExtendedSplash] Error positioning container: " + ex.Message);
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000DCC4 File Offset: 0x0000BEC4
		private void ExtendedSplash_OnResize(object sender, WindowSizeChangedEventArgs e)
		{
			bool flag = this.splash != null;
			if (flag)
			{
				this.splashImageRect = this.splash.ImageLocation;
				this.PositionImage();
				this.PositionLoadingContainer();
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000DD00 File Offset: 0x0000BF00
		private void DismissedEventHandler(SplashScreen sender, object e)
		{
			this.dismissed = true;
			Debug.WriteLine("[ExtendedSplash] System splash screen dismissed");
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000DD18 File Offset: 0x0000BF18
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			bool contentLoaded = this._contentLoaded;
			if (!contentLoaded)
			{
				this._contentLoaded = true;
				Application.LoadComponent(this, new Uri("ms-appx:///ExtendedSplash.xaml"), 0);
				this.SoundPlayer = (MediaElement)base.FindName("SoundPlayer");
				this.extendedSplashImage = (Image)base.FindName("extendedSplashImage");
				this.loadingContainer = (StackPanel)base.FindName("loadingContainer");
				this.splashProgressRing = (ProgressRing)base.FindName("splashProgressRing");
				this.splashTextBlock = (TextBlock)base.FindName("splashTextBlock");
			}
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000DDBC File Offset: 0x0000BFBC
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void Connect(int connectionId, object target)
		{
			this._contentLoaded = true;
		}

		// Token: 0x04000116 RID: 278
		internal Rect splashImageRect;

		// Token: 0x04000117 RID: 279
		private SplashScreen splash;

		// Token: 0x04000118 RID: 280
		internal bool dismissed = false;

		// Token: 0x04000119 RID: 281
		private ObservableCollection<StoreApp> loadedApps = new ObservableCollection<StoreApp>();

		// Token: 0x0400011A RID: 282
		private List<StoreApp> allAppsForStats;

		// Token: 0x0400011B RID: 283
		private ApplicationDataContainer localSettings;

		// Token: 0x0400011C RID: 284
		private const string CACHE_FOLDER_NAME = "AppDataCache";

		// Token: 0x0400011D RID: 285
		private const string APPS_CACHE_FILE = "apps_cache.json";

		// Token: 0x0400011E RID: 286
		private StorageFolder _cacheFolder;

		// Token: 0x0400011F RID: 287
		private StorageFile _appsCacheFile;

		// Token: 0x04000120 RID: 288
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private MediaElement SoundPlayer;

		// Token: 0x04000121 RID: 289
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Image extendedSplashImage;

		// Token: 0x04000122 RID: 290
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel loadingContainer;

		// Token: 0x04000123 RID: 291
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ProgressRing splashProgressRing;

		// Token: 0x04000124 RID: 292
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock splashTextBlock;

		// Token: 0x04000125 RID: 293
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
