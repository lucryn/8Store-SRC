using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Win81StoreRevival.Win81StoreRevival_XamlTypeInfo;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
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
	// Token: 0x0200001A RID: 26
	public sealed class App : Application, IComponentConnector, IXamlMetadataProvider
	{
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00006E98 File Offset: 0x00005098
		// (set) Token: 0x06000130 RID: 304 RVA: 0x00006E9F File Offset: 0x0000509F
		public static ObservableCollection<StoreApp> LoadedApps { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00006EA7 File Offset: 0x000050A7
		// (set) Token: 0x06000132 RID: 306 RVA: 0x00006EAE File Offset: 0x000050AE
		public static List<StoreApp> AllAppsForStats { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00006EB6 File Offset: 0x000050B6
		// (set) Token: 0x06000134 RID: 308 RVA: 0x00006EBD File Offset: 0x000050BD
		public static bool IsFirstLoadComplete { get; set; } = false;

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00006EC5 File Offset: 0x000050C5
		// (set) Token: 0x06000136 RID: 310 RVA: 0x00006ECC File Offset: 0x000050CC
		public static DateTime DataLoadTime { get; set; } = DateTime.MinValue;

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00006ED4 File Offset: 0x000050D4
		// (set) Token: 0x06000138 RID: 312 RVA: 0x00006EDB File Offset: 0x000050DB
		public static bool IsDataLoaded { get; set; } = false;

		// Token: 0x06000139 RID: 313 RVA: 0x00006EE4 File Offset: 0x000050E4
		public App()
		{
			this.InitializeComponent();
			WindowsRuntimeMarshal.AddEventHandler<UnhandledExceptionEventHandler>(new Func<UnhandledExceptionEventHandler, EventRegistrationToken>(this.add_UnhandledException), new Action<EventRegistrationToken>(this.remove_UnhandledException), new UnhandledExceptionEventHandler(this.App_UnhandledException));
			WindowsRuntimeMarshal.AddEventHandler<SuspendingEventHandler>(new Func<SuspendingEventHandler, EventRegistrationToken>(this.add_Suspending), new Action<EventRegistrationToken>(this.remove_Suspending), new SuspendingEventHandler(this.OnSuspending));
			WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(this.add_Resuming), new Action<EventRegistrationToken>(this.remove_Resuming), new EventHandler<object>(this.OnResuming));
			Debug.WriteLine("[APP] Application constructed");
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00006F90 File Offset: 0x00005190
		private void App_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			try
			{
				Debug.WriteLine("[APP] Unhandled exception: " + e.Exception);
			}
			catch
			{
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00006FD0 File Offset: 0x000051D0
		protected override void OnLaunched(LaunchActivatedEventArgs e)
		{
			App._lastActivationTime = DateTime.Now;
			Debug.WriteLine(string.Format("[APP] OnLaunched called at {0}", new object[]
			{
				App._lastActivationTime
			}));
			Debug.WriteLine(string.Format("[APP] Previous execution state: {0}", new object[]
			{
				e.PreviousExecutionState
			}));
			Debug.WriteLine(string.Format("[APP] Arguments: {0}", new object[]
			{
				e.Arguments
			}));
			bool isAttached = Debugger.IsAttached;
			if (isAttached)
			{
				base.DebugSettings.put_EnableFrameRateCounter(false);
			}
			Frame frame = Window.Current.Content as Frame;
			bool flag = frame == null;
			if (flag)
			{
				Debug.WriteLine("[APP] Creating new root frame");
				frame = new Frame();
				frame.put_Language(ApplicationLanguages.Languages[0]);
				Frame frame2 = frame;
				WindowsRuntimeMarshal.AddEventHandler<NavigationFailedEventHandler>(new Func<NavigationFailedEventHandler, EventRegistrationToken>(frame2.add_NavigationFailed), new Action<EventRegistrationToken>(frame2.remove_NavigationFailed), new NavigationFailedEventHandler(this.OnNavigationFailed));
				bool flag2 = e.PreviousExecutionState == 3;
				if (flag2)
				{
					Debug.WriteLine("[APP] Resuming from terminated state - loading saved state");
					this.LoadAppState();
				}
				else
				{
					bool flag3 = e.PreviousExecutionState == 4;
					if (flag3)
					{
						Debug.WriteLine("[APP] Launched after being closed by user");
					}
					else
					{
						bool flag4 = e.PreviousExecutionState == 0;
						if (flag4)
						{
							Debug.WriteLine("[APP] Fresh launch");
						}
					}
				}
				Window.Current.put_Content(frame);
			}
			else
			{
				Debug.WriteLine("[APP] Root frame already exists");
			}
			bool flag5 = frame.Content == null;
			if (flag5)
			{
				Debug.WriteLine("[APP] Navigating to MainPage");
				frame.Navigate(typeof(MainPage), e.Arguments);
			}
			else
			{
				Debug.WriteLine("[APP] Root frame already has content");
			}
			bool flag6 = !string.IsNullOrEmpty(e.Arguments);
			if (flag6)
			{
				Debug.WriteLine(string.Format("[APP] Handling launch arguments: {0}", new object[]
				{
					e.Arguments
				}));
				DownloadManager.HandleLaunch(e.Arguments);
			}
			Window.Current.Activate();
			bool flag7 = e.PreviousExecutionState == 0;
			if (flag7)
			{
				Debug.WriteLine("[APP] Showing splash screen");
				ExtendedSplash extendedSplash = new ExtendedSplash(e.SplashScreen);
				Window.Current.put_Content(extendedSplash);
			}
			Debug.WriteLine("[APP] Starting auto-login");
			this.AutoLoginAsync();
			Debug.WriteLine("[APP] OnLaunched completed");
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00007230 File Offset: 0x00005430
		[DebuggerStepThrough]
		private void AutoLoginAsync()
		{
			App.<AutoLoginAsync>d__25 <AutoLoginAsync>d__ = new App.<AutoLoginAsync>d__25();
			<AutoLoginAsync>d__.<>4__this = this;
			<AutoLoginAsync>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<AutoLoginAsync>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <AutoLoginAsync>d__.<>t__builder;
			<>t__builder.Start<App.<AutoLoginAsync>d__25>(ref <AutoLoginAsync>d__);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000726C File Offset: 0x0000546C
		private void OnCommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
		{
			args.Request.ApplicationCommands.Clear();
			args.Request.ApplicationCommands.Add(new SettingsCommand("downloads", "Downloads", delegate(IUICommand cmd)
			{
				Frame frame = Window.Current.Content as Frame;
				bool flag = frame != null;
				if (flag)
				{
					frame.Navigate(typeof(DownloadsHub));
				}
			}));
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000072CC File Offset: 0x000054CC
		private void LoadAppState()
		{
			try
			{
				ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
				bool flag = localSettings.Values.ContainsKey("LastSuspensionTime");
				if (flag)
				{
					string text = (string)localSettings.Values["LastSuspensionTime"];
					Debug.WriteLine(string.Format("[APP] Last suspension was at: {0}", new object[]
					{
						text
					}));
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("[APP] Error loading app state: {0}", new object[]
				{
					ex.Message
				}));
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00007364 File Offset: 0x00005564
		private void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
		{
			Debug.WriteLine(string.Format("[APP] Navigation failed to {0}: {1}", new object[]
			{
				e.SourcePageType.FullName,
				e.Exception.Message
			}));
			throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000073C0 File Offset: 0x000055C0
		private void OnSuspending(object sender, SuspendingEventArgs e)
		{
			App._isSuspending = true;
			Debug.WriteLine(string.Format("[APP] OnSuspending called at {0}", new object[]
			{
				DateTime.Now
			}));
			SuspendingDeferral deferral = e.SuspendingOperation.GetDeferral();
			try
			{
				Debug.WriteLine("[APP] Suspending application...");
				Frame frame = Window.Current.Content as Frame;
				bool flag = frame != null && frame.Content != null;
				if (flag)
				{
					ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
					localSettings.Values["SuspendedPage"] = frame.Content.GetType().FullName;
					localSettings.Values["LastSuspensionTime"] = DateTime.Now.ToString();
					Debug.WriteLine(string.Format("[APP] Saved suspension state for page: {0}", new object[]
					{
						frame.Content.GetType().Name
					}));
					this.SaveAppState();
				}
				Debug.WriteLine("[APP] Running garbage collection before suspension");
				GC.Collect();
				GC.WaitForPendingFinalizers();
				Debug.WriteLine("[APP] Suspension preparation completed");
			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("[APP] Error during suspension: {0}", new object[]
				{
					ex.Message
				}));
			}
			finally
			{
				deferral.Complete();
				Debug.WriteLine("[APP] Suspension deferral completed");
				App._isSuspending = false;
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00007534 File Offset: 0x00005734
		private void SaveAppState()
		{
			try
			{
				ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
				localSettings.Values["AppVersion"] = "1.0.0";
				localSettings.Values["LastSaveTime"] = DateTime.Now.ToString();
				Debug.WriteLine("[APP] App state saved");
			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("[APP] Error saving app state: {0}", new object[]
				{
					ex.Message
				}));
			}
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000075C4 File Offset: 0x000057C4
		private void OnResuming(object sender, object e)
		{
			Debug.WriteLine(string.Format("[APP] OnResuming called at {0}", new object[]
			{
				DateTime.Now
			}));
			try
			{
				App._isSuspending = false;
				Frame frame = Window.Current.Content as Frame;
				MainPage mainPage = ((frame != null) ? frame.Content : null) as MainPage;
				bool flag = mainPage != null;
				if (flag)
				{
					Debug.WriteLine("[APP] MainPage found, refreshing data...");
					IAsyncAction asyncAction = Window.Current.Dispatcher.RunAsync(0, delegate()
					{
						try
						{
							mainPage.RefreshData();
							Debug.WriteLine("[APP] Data refresh initiated on resume");
						}
						catch (Exception ex2)
						{
							Debug.WriteLine(string.Format("[APP] Error refreshing data: {0}", new object[]
							{
								ex2.Message
							}));
						}
					});
				}
				else
				{
					Debug.WriteLine("[APP] MainPage not found or frame is null");
				}
				Debug.WriteLine("[APP] Resume completed");
			}
			catch (Exception ex)
			{
				Debug.WriteLine(string.Format("[APP] Error during resume: {0}", new object[]
				{
					ex.Message
				}));
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000143 RID: 323 RVA: 0x000076B4 File Offset: 0x000058B4
		public static bool IsSuspending
		{
			get
			{
				return App._isSuspending;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000144 RID: 324 RVA: 0x000076CC File Offset: 0x000058CC
		public static DateTime LastActivationTime
		{
			get
			{
				return App._lastActivationTime;
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000076E4 File Offset: 0x000058E4
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			bool contentLoaded = this._contentLoaded;
			if (!contentLoaded)
			{
				this._contentLoaded = true;
				DebugSettings debugSettings = base.DebugSettings;
				WindowsRuntimeMarshal.AddEventHandler<BindingFailedEventHandler>(new Func<BindingFailedEventHandler, EventRegistrationToken>(debugSettings.add_BindingFailed), new Action<EventRegistrationToken>(debugSettings.remove_BindingFailed), delegate(object sender, BindingFailedEventArgs args)
				{
					Debug.WriteLine(args.Message);
				});
				WindowsRuntimeMarshal.AddEventHandler<UnhandledExceptionEventHandler>(new Func<UnhandledExceptionEventHandler, EventRegistrationToken>(this.add_UnhandledException), new Action<EventRegistrationToken>(this.remove_UnhandledException), delegate(object sender, UnhandledExceptionEventArgs e)
				{
					bool isAttached = Debugger.IsAttached;
					if (isAttached)
					{
						Debugger.Break();
					}
				});
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000778D File Offset: 0x0000598D
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void Connect(int connectionId, object target)
		{
			this._contentLoaded = true;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00007798 File Offset: 0x00005998
		public IXamlType GetXamlType(Type type)
		{
			bool flag = this._provider == null;
			if (flag)
			{
				this._provider = new XamlTypeInfoProvider();
			}
			return this._provider.GetXamlTypeByType(type);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000077D0 File Offset: 0x000059D0
		public IXamlType GetXamlType(string fullName)
		{
			bool flag = this._provider == null;
			if (flag)
			{
				this._provider = new XamlTypeInfoProvider();
			}
			return this._provider.GetXamlTypeByName(fullName);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00007808 File Offset: 0x00005A08
		public XmlnsDefinition[] GetXmlnsDefinitions()
		{
			return new XmlnsDefinition[0];
		}

		// Token: 0x0400009A RID: 154
		private static bool _isSuspending = false;

		// Token: 0x0400009B RID: 155
		private static DateTime _lastActivationTime;

		// Token: 0x040000A1 RID: 161
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;

		// Token: 0x040000A2 RID: 162
		private XamlTypeInfoProvider _provider;
	}
}
