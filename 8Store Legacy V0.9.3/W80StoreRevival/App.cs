using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using W80StoreRevival.W80StoreRevival_XamlTypeInfo;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.ApplicationSettings;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

namespace W80StoreRevival
{
	// Token: 0x02000004 RID: 4
	public sealed class App : Application, IComponentConnector, IXamlMetadataProvider
	{
		// Token: 0x0600001E RID: 30 RVA: 0x00004DE8 File Offset: 0x00002FE8
		public App()
		{
			this.InitializeComponent();
			WindowsRuntimeMarshal.AddEventHandler<SuspendingEventHandler>(new Func<SuspendingEventHandler, EventRegistrationToken>(base.add_Suspending), new Action<EventRegistrationToken>(base.remove_Suspending), new SuspendingEventHandler(this.OnSuspending));
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000050B4 File Offset: 0x000032B4
		[DebuggerStepThrough]
		private void UpdateLiveTileOnStartup()
		{
			App.<UpdateLiveTileOnStartup>d__0 <UpdateLiveTileOnStartup>d__;
			<UpdateLiveTileOnStartup>d__.<>4__this = this;
			<UpdateLiveTileOnStartup>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<UpdateLiveTileOnStartup>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <UpdateLiveTileOnStartup>d__.<>t__builder;
			<>t__builder.Start<App.<UpdateLiveTileOnStartup>d__0>(ref <UpdateLiveTileOnStartup>d__);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00005560 File Offset: 0x00003760
		[DebuggerStepThrough]
		private void CheckAutoUpdateOnLaunch()
		{
			App.<CheckAutoUpdateOnLaunch>d__8 <CheckAutoUpdateOnLaunch>d__;
			<CheckAutoUpdateOnLaunch>d__.<>4__this = this;
			<CheckAutoUpdateOnLaunch>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<CheckAutoUpdateOnLaunch>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <CheckAutoUpdateOnLaunch>d__.<>t__builder;
			<>t__builder.Start<App.<CheckAutoUpdateOnLaunch>d__8>(ref <CheckAutoUpdateOnLaunch>d__);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000559C File Offset: 0x0000379C
		protected override void OnLaunched(LaunchActivatedEventArgs args)
		{
			try
			{
				Debug.WriteLine("App OnLaunched - First Launch Check");
				Frame frame = Window.Current.Content as Frame;
				if (frame == null)
				{
					frame = new Frame();
					bool flag = true;
					try
					{
						ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
						if (localSettings.Values.ContainsKey("HasLaunchedBefore"))
						{
							flag = false;
							Debug.WriteLine("App: Not first launch");
						}
						else
						{
							localSettings.Values["HasLaunchedBefore"] = true;
							Debug.WriteLine("App: First launch detected");
						}
					}
					catch (Exception ex)
					{
						Debug.WriteLine("App: Error checking first launch: " + ex.Message);
						flag = true;
					}
					if (flag)
					{
						Debug.WriteLine("App: Navigating to WelcomePage");
						frame.Navigate(typeof(WelcomePage));
						Window.Current.put_Content(frame);
					}
					else
					{
						Debug.WriteLine("App: Showing ExtendedSplash");
						ExtendedSplash extendedSplash = new ExtendedSplash(args.SplashScreen, false);
						Window.Current.put_Content(extendedSplash);
					}
				}
				Window.Current.Activate();
				if (frame != null && frame.Content != null)
				{
					if (!(frame.Content is WelcomePage))
					{
						this.RegisterCharmsSettings();
						this.CheckAutoUpdateOnLaunch();
						this.UpdateLiveTileOnStartup();
					}
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("App OnLaunched error: " + ex.Message);
				try
				{
					Frame frame2 = new Frame();
					frame2.Navigate(typeof(MainPage));
					Window.Current.put_Content(frame2);
					Window.Current.Activate();
				}
				catch
				{
				}
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000057B0 File Offset: 0x000039B0
		private void RegisterCharmsSettings()
		{
			try
			{
				Debug.WriteLine("[APP] Registering charms settings");
				SettingsPane forCurrentView = SettingsPane.GetForCurrentView();
				WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<SettingsPane, SettingsPaneCommandsRequestedEventArgs>>(new Func<TypedEventHandler<SettingsPane, SettingsPaneCommandsRequestedEventArgs>, EventRegistrationToken>(forCurrentView.add_CommandsRequested), new Action<EventRegistrationToken>(forCurrentView.remove_CommandsRequested), new TypedEventHandler<SettingsPane, SettingsPaneCommandsRequestedEventArgs>(this.OnCommandsRequested));
			}
			catch (Exception ex)
			{
				Debug.WriteLine("[APP] Error registering settings: " + ex.Message);
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00005840 File Offset: 0x00003A40
		private void OnCommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
		{
			try
			{
				Debug.WriteLine("[APP] Charms bar settings requested");
				IList<SettingsCommand> applicationCommands = args.Request.ApplicationCommands;
				SettingsCommand settingsCommand = new SettingsCommand("8store_settings", "8Store", delegate(IUICommand handler)
				{
					this.ShowSettingsFlyout();
				});
				applicationCommands.Add(settingsCommand);
				SettingsCommand settingsCommand2 = new SettingsCommand("about", "About", delegate(IUICommand handler)
				{
					this.ShowAboutFlyout();
				});
				applicationCommands.Add(settingsCommand2);
			}
			catch (Exception ex)
			{
				Debug.WriteLine("[APP] Error in commands requested: " + ex.Message);
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000058F8 File Offset: 0x00003AF8
		private void ShowAboutFlyout()
		{
			try
			{
				Debug.WriteLine("[APP] Showing about flyout");
				AboutFlyout aboutFlyout = new AboutFlyout();
				aboutFlyout.ShowFlyout();
			}
			catch (Exception ex)
			{
				Debug.WriteLine("[APP] Error showing about: " + ex.Message);
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00005950 File Offset: 0x00003B50
		private void ShowSettingsFlyout()
		{
			try
			{
				Debug.WriteLine("[APP] Showing settings flyout");
				SettingsPage settingsPage = new SettingsPage();
				settingsPage.Show();
			}
			catch (Exception ex)
			{
				Debug.WriteLine("[APP] Error showing settings: " + ex.Message);
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000059A8 File Offset: 0x00003BA8
		private void OnSuspending(object sender, SuspendingEventArgs e)
		{
			SuspendingDeferral deferral = e.SuspendingOperation.GetDeferral();
			try
			{
				Debug.WriteLine("[APP] App suspending, updating live tile...");
			}
			catch (Exception ex)
			{
				Debug.WriteLine("[APP] Error during suspend: " + ex.Message);
			}
			finally
			{
				deferral.Complete();
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00005A48 File Offset: 0x00003C48
		[DebuggerNonUserCode]
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this._contentLoaded)
			{
				this._contentLoaded = true;
				DebugSettings debugSettings = base.DebugSettings;
				WindowsRuntimeMarshal.AddEventHandler<BindingFailedEventHandler>(new Func<BindingFailedEventHandler, EventRegistrationToken>(debugSettings.add_BindingFailed), new Action<EventRegistrationToken>(debugSettings.remove_BindingFailed), delegate(object sender, BindingFailedEventArgs args)
				{
					Debug.WriteLine(args.Message);
				});
				WindowsRuntimeMarshal.AddEventHandler<UnhandledExceptionEventHandler>(new Func<UnhandledExceptionEventHandler, EventRegistrationToken>(base.add_UnhandledException), new Action<EventRegistrationToken>(base.remove_UnhandledException), delegate(object sender, UnhandledExceptionEventArgs e)
				{
					if (Debugger.IsAttached)
					{
						Debugger.Break();
					}
				});
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00005AF2 File Offset: 0x00003CF2
		[DebuggerNonUserCode]
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		public void Connect(int connectionId, object target)
		{
			this._contentLoaded = true;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00005AFC File Offset: 0x00003CFC
		public IXamlType GetXamlType(Type type)
		{
			if (this._provider == null)
			{
				this._provider = new XamlTypeInfoProvider();
			}
			return this._provider.GetXamlTypeByType(type);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00005B38 File Offset: 0x00003D38
		public IXamlType GetXamlType(string fullName)
		{
			if (this._provider == null)
			{
				this._provider = new XamlTypeInfoProvider();
			}
			return this._provider.GetXamlTypeByName(fullName);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00005B74 File Offset: 0x00003D74
		public XmlnsDefinition[] GetXmlnsDefinitions()
		{
			return new XmlnsDefinition[0];
		}

		// Token: 0x04000014 RID: 20
		private bool _isFirstLaunchChecked = false;

		// Token: 0x04000015 RID: 21
		private bool _isLiveTileUpdating = false;

		// Token: 0x04000016 RID: 22
		private bool _isAutoUpdateChecking = false;

		// Token: 0x04000017 RID: 23
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;

		// Token: 0x04000018 RID: 24
		private XamlTypeInfoProvider _provider;
	}
}
