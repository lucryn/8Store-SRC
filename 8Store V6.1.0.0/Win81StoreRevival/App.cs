using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Win81StoreRevival.Config;
using Win81StoreRevival.Themes;
using Win81StoreRevival.Win81StoreRevival_XamlTypeInfo;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Search;
using Windows.Foundation;
using Windows.Globalization;
using Windows.Storage;
using Windows.UI.ApplicationSettings;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Navigation;

namespace Win81StoreRevival
{
	// Token: 0x0200001D RID: 29
	public sealed class App : Application, IComponentConnector, IXamlMetadataProvider
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00007B0C File Offset: 0x00005D0C
		// (set) Token: 0x0600017A RID: 378 RVA: 0x00007B13 File Offset: 0x00005D13
		public static ObservableCollection<StoreApp> LoadedApps { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00007B1B File Offset: 0x00005D1B
		// (set) Token: 0x0600017C RID: 380 RVA: 0x00007B22 File Offset: 0x00005D22
		public static List<StoreApp> AllAppsForStats { get; set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00007B2A File Offset: 0x00005D2A
		// (set) Token: 0x0600017E RID: 382 RVA: 0x00007B31 File Offset: 0x00005D31
		public static bool IsFirstLoadComplete { get; set; } = false;

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00007B39 File Offset: 0x00005D39
		// (set) Token: 0x06000180 RID: 384 RVA: 0x00007B40 File Offset: 0x00005D40
		public static DateTime DataLoadTime { get; set; } = DateTime.MinValue;

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00007B48 File Offset: 0x00005D48
		// (set) Token: 0x06000182 RID: 386 RVA: 0x00007B4F File Offset: 0x00005D4F
		public static bool IsDataLoaded { get; set; } = false;

		// Token: 0x06000183 RID: 387 RVA: 0x00007B58 File Offset: 0x00005D58
		public App()
		{
			this.InitializeComponent();
			WindowsRuntimeMarshal.AddEventHandler<UnhandledExceptionEventHandler>(new Func<UnhandledExceptionEventHandler, EventRegistrationToken>(this.add_UnhandledException), new Action<EventRegistrationToken>(this.remove_UnhandledException), new UnhandledExceptionEventHandler(this.App_UnhandledException));
			WindowsRuntimeMarshal.AddEventHandler<SuspendingEventHandler>(new Func<SuspendingEventHandler, EventRegistrationToken>(this.add_Suspending), new Action<EventRegistrationToken>(this.remove_Suspending), new SuspendingEventHandler(this.OnSuspending));
			WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(this.add_Resuming), new Action<EventRegistrationToken>(this.remove_Resuming), new EventHandler<object>(this.OnResuming));
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00007BF4 File Offset: 0x00005DF4
		private void App_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			try
			{
			}
			catch
			{
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00007C18 File Offset: 0x00005E18
		protected override void OnLaunched(LaunchActivatedEventArgs e)
		{
			App._lastActivationTime = DateTime.Now;
			Frame frame = Window.Current.Content as Frame;
			if (frame == null)
			{
				frame = new Frame();
				frame.put_Language(ApplicationLanguages.Languages[0]);
				Frame frame2 = frame;
				WindowsRuntimeMarshal.AddEventHandler<NavigationFailedEventHandler>(new Func<NavigationFailedEventHandler, EventRegistrationToken>(frame2.add_NavigationFailed), new Action<EventRegistrationToken>(frame2.remove_NavigationFailed), new NavigationFailedEventHandler(this.OnNavigationFailed));
				Control.Current.ApplyTheme();
				this.EnsureSettingsCommands();
				if (e.PreviousExecutionState == 3)
				{
					this.LoadAppState();
				}
				else if (e.PreviousExecutionState != 4)
				{
					ApplicationExecutionState previousExecutionState = e.PreviousExecutionState;
				}
				Window.Current.put_Content(frame);
			}
			if (frame.Content == null)
			{
				this.LoadData();
				frame.Navigate(typeof(MainPage), e.Arguments);
			}
			if (!string.IsNullOrEmpty(e.Arguments))
			{
				DownloadManager.HandleLaunch(e.Arguments);
			}
			string text = ApplicationLanguages.Languages[0];
			if (frame != null)
			{
				frame.put_FlowDirection((text.StartsWith("ar") || text.StartsWith("he")) ? 1 : 0);
			}
			Window.Current.Activate();
			this.AutoLoginAsync();
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00007D48 File Offset: 0x00005F48
		protected override void OnActivated(IActivatedEventArgs args)
		{
			App.<OnActivated>d__29 <OnActivated>d__;
			<OnActivated>d__.<>4__this = this;
			<OnActivated>d__.args = args;
			<OnActivated>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<OnActivated>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <OnActivated>d__.<>t__builder;
			<>t__builder.Start<App.<OnActivated>d__29>(ref <OnActivated>d__);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00007D8C File Offset: 0x00005F8C
		private void AutoLoginAsync()
		{
			App.<AutoLoginAsync>d__30 <AutoLoginAsync>d__;
			<AutoLoginAsync>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<AutoLoginAsync>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <AutoLoginAsync>d__.<>t__builder;
			<>t__builder.Start<App.<AutoLoginAsync>d__30>(ref <AutoLoginAsync>d__);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00007DC0 File Offset: 0x00005FC0
		private Task HandleProtocolActivationAsync(Uri uri)
		{
			App.<HandleProtocolActivationAsync>d__31 <HandleProtocolActivationAsync>d__;
			<HandleProtocolActivationAsync>d__.uri = uri;
			<HandleProtocolActivationAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<HandleProtocolActivationAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <HandleProtocolActivationAsync>d__.<>t__builder;
			<>t__builder.Start<App.<HandleProtocolActivationAsync>d__31>(ref <HandleProtocolActivationAsync>d__);
			return <HandleProtocolActivationAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00007E08 File Offset: 0x00006008
		private static string GetQueryValue(Uri uri, string key)
		{
			if (uri == null || string.IsNullOrWhiteSpace(key))
			{
				return string.Empty;
			}
			string text = uri.Query;
			if (string.IsNullOrWhiteSpace(text))
			{
				return string.Empty;
			}
			if (text.StartsWith("?"))
			{
				text = text.Substring(1);
			}
			foreach (string text2 in text.Split(new char[]
			{
				'&'
			}))
			{
				if (!string.IsNullOrWhiteSpace(text2))
				{
					int num = text2.IndexOf('=');
					if (num > 0 && string.Equals(Uri.UnescapeDataString(text2.Substring(0, num)), key, 5))
					{
						return Uri.UnescapeDataString(text2.Substring(num + 1).Replace("+", " "));
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00007ECC File Offset: 0x000060CC
		private void OnCommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
		{
			args.Request.ApplicationCommands.Clear();
			args.Request.ApplicationCommands.Add(new SettingsCommand("general", MainPage.Localized("GeneralCharm", new object[0]), delegate(IUICommand cmd)
			{
				this.SafeOpenSettingsFlyout(typeof(GeneralSettingsFlyout));
			}));
			args.Request.ApplicationCommands.Add(new SettingsCommand("personalization", MainPage.Localized("PersonalizationCharm", new object[0]), delegate(IUICommand cmd)
			{
				this.SafeOpenSettingsFlyout(typeof(CosmeticFlyout));
			}));
			args.Request.ApplicationCommands.Add(new SettingsCommand("downloads", MainPage.Localized("MyDownloads", new object[0]), delegate(IUICommand cmd)
			{
				Frame frame = Window.Current.Content as Frame;
				if (frame != null)
				{
					frame.Navigate(typeof(DownloadsHub));
				}
			}));
			args.Request.ApplicationCommands.Add(new SettingsCommand("language", MainPage.Localized("LanguageCharm", new object[0]), delegate(IUICommand cmd)
			{
				this.SafeOpenSettingsFlyout(typeof(LanguageSettingsFlyout));
			}));
			args.Request.ApplicationCommands.Add(new SettingsCommand("8Store Recovery", MainPage.Localized("RecoveryCharm", new object[0]), delegate(IUICommand cmd)
			{
				this.SafeOpenSettingsFlyout(typeof(FactoryResetFlyout));
			}));
			args.Request.ApplicationCommands.Add(new SettingsCommand("about", MainPage.Localized("AboutCharm", new object[0]), delegate(IUICommand cmd)
			{
				this.SafeOpenSettingsFlyout(typeof(AboutSettingsFlyout));
			}));
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00008040 File Offset: 0x00006240
		private void EnsureSettingsCommands()
		{
			if (this._settingsCommandsRegistered)
			{
				return;
			}
			SettingsPane forCurrentView = SettingsPane.GetForCurrentView();
			WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<SettingsPane, SettingsPaneCommandsRequestedEventArgs>>(new Func<TypedEventHandler<SettingsPane, SettingsPaneCommandsRequestedEventArgs>, EventRegistrationToken>(forCurrentView.add_CommandsRequested), new Action<EventRegistrationToken>(forCurrentView.remove_CommandsRequested), new TypedEventHandler<SettingsPane, SettingsPaneCommandsRequestedEventArgs>(this.OnCommandsRequested));
			this._settingsCommandsRegistered = true;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00008090 File Offset: 0x00006290
		private void SafeOpenSettingsFlyout(Type flyoutType)
		{
			try
			{
				this.OpenSettingsFlyout(flyoutType);
			}
			catch (Exception)
			{
				try
				{
					new MessageDialog(string.Format("This feature is not supported on this device. ({0})", new object[]
					{
						flyoutType.Name
					}), "Error").ShowAsync();
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000080F4 File Offset: 0x000062F4
		private void OpenSettingsFlyout(Type flyoutType)
		{
			((SettingsFlyout)Activator.CreateInstance(flyoutType)).Show();
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00008108 File Offset: 0x00006308
		private void LoadAppState()
		{
			try
			{
				ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
				if (localSettings.Values.ContainsKey("LastSuspensionTime"))
				{
					string text = (string)localSettings.Values["LastSuspensionTime"];
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00008160 File Offset: 0x00006360
		private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
		{
			throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000817C File Offset: 0x0000637C
		public void LoadData()
		{
			App.<LoadData>d__39 <LoadData>d__;
			<LoadData>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<LoadData>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <LoadData>d__.<>t__builder;
			<>t__builder.Start<App.<LoadData>d__39>(ref <LoadData>d__);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000081B0 File Offset: 0x000063B0
		private void OnSuspending(object sender, SuspendingEventArgs e)
		{
			App._isSuspending = true;
			SuspendingDeferral deferral = e.SuspendingOperation.GetDeferral();
			try
			{
				Frame frame = Window.Current.Content as Frame;
				if (frame != null && frame.Content != null)
				{
					ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
					localSettings.Values["SuspendedPage"] = frame.Content.GetType().FullName;
					localSettings.Values["LastSuspensionTime"] = DateTime.Now.ToString();
					this.SaveAppState();
				}
				GC.Collect();
				GC.WaitForPendingFinalizers();
			}
			catch (Exception)
			{
			}
			finally
			{
				deferral.Complete();
				App._isSuspending = false;
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00008270 File Offset: 0x00006470
		private void SaveAppState()
		{
			try
			{
				ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
				localSettings.Values["AppVersion"] = Config.version;
				localSettings.Values["LastSaveTime"] = DateTime.Now.ToString();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000082D0 File Offset: 0x000064D0
		private void OnResuming(object sender, object e)
		{
			try
			{
				App._isSuspending = false;
				Frame frame = Window.Current.Content as Frame;
				MainPage mainPage = ((frame != null) ? frame.Content : null) as MainPage;
				if (mainPage != null && App.IsDataLoaded)
				{
					Window.Current.Dispatcher.RunAsync(0, delegate()
					{
						try
						{
							mainPage.RefreshData();
						}
						catch (Exception)
						{
						}
					});
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00008354 File Offset: 0x00006554
		public static bool IsSuspending
		{
			get
			{
				return App._isSuspending;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000195 RID: 405 RVA: 0x0000835B File Offset: 0x0000655B
		public static DateTime LastActivationTime
		{
			get
			{
				return App._lastActivationTime;
			}
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00008364 File Offset: 0x00006564
		public static Task EnsureCacheEndpointAsync()
		{
			App.<EnsureCacheEndpointAsync>d__47 <EnsureCacheEndpointAsync>d__;
			<EnsureCacheEndpointAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<EnsureCacheEndpointAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <EnsureCacheEndpointAsync>d__.<>t__builder;
			<>t__builder.Start<App.<EnsureCacheEndpointAsync>d__47>(ref <EnsureCacheEndpointAsync>d__);
			return <EnsureCacheEndpointAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x000083A4 File Offset: 0x000065A4
		private void OnSearchQuerySubmitted(SearchPane sender, SearchPaneQuerySubmittedEventArgs args)
		{
			Frame frame = Window.Current.Content as Frame;
			if (frame == null)
			{
				frame = new Frame();
				frame.put_Language(ApplicationLanguages.Languages[0]);
				Frame frame2 = frame;
				WindowsRuntimeMarshal.AddEventHandler<NavigationFailedEventHandler>(new Func<NavigationFailedEventHandler, EventRegistrationToken>(frame2.add_NavigationFailed), new Action<EventRegistrationToken>(frame2.remove_NavigationFailed), new NavigationFailedEventHandler(this.OnNavigationFailed));
				Window.Current.put_Content(frame);
			}
			frame.Navigate(typeof(SearchResultsPage), args.QueryText);
			Window.Current.Activate();
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00008434 File Offset: 0x00006634
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00008446 File Offset: 0x00006646
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void Connect(int connectionId, object target)
		{
			this._contentLoaded = true;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000844F File Offset: 0x0000664F
		public IXamlType GetXamlType(Type type)
		{
			if (this._provider == null)
			{
				this._provider = new XamlTypeInfoProvider();
			}
			return this._provider.GetXamlTypeByType(type);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00008470 File Offset: 0x00006670
		public IXamlType GetXamlType(string fullName)
		{
			if (this._provider == null)
			{
				this._provider = new XamlTypeInfoProvider();
			}
			return this._provider.GetXamlTypeByName(fullName);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00008491 File Offset: 0x00006691
		public XmlnsDefinition[] GetXmlnsDefinitions()
		{
			return new XmlnsDefinition[0];
		}

		// Token: 0x0400009A RID: 154
		private static bool _isSuspending = false;

		// Token: 0x0400009B RID: 155
		private static DateTime _lastActivationTime;

		// Token: 0x0400009C RID: 156
		private bool _settingsCommandsRegistered;

		// Token: 0x040000A2 RID: 162
		private const string CacheEndpointKey = "CacheEndpoint";

		// Token: 0x040000A3 RID: 163
		private const string CacheFolderName = "AppDataCache";

		// Token: 0x040000A4 RID: 164
		private const string AppsCacheFile = "apps_cache.json";

		// Token: 0x040000A5 RID: 165
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;

		// Token: 0x040000A6 RID: 166
		private XamlTypeInfoProvider _provider;
	}
}
