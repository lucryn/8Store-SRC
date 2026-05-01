using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Data.Json;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

namespace W80StoreRevival
{
	// Token: 0x02000008 RID: 8
	public sealed class ExtendedSplash : Page, IComponentConnector
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00007A18 File Offset: 0x00005C18
		public ExtendedSplash(SplashScreen splashscreen, bool loadState)
		{
			try
			{
				Debug.WriteLine("ExtendedSplash constructor");
				this.InitializeComponent();
				this.splash = splashscreen;
				if (this.splash != null)
				{
					SplashScreen splashScreen = this.splash;
					WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<SplashScreen, object>>(new Func<TypedEventHandler<SplashScreen, object>, EventRegistrationToken>(splashScreen.add_Dismissed), new Action<EventRegistrationToken>(splashScreen.remove_Dismissed), new TypedEventHandler<SplashScreen, object>(this.DismissedEventHandler));
					this.splashImageRect = this.splash.ImageLocation;
					this.PositionImage();
				}
				this.rootFrame = new Frame();
				this.LoadDataAsync();
			}
			catch (Exception ex)
			{
				Debug.WriteLine("ExtendedSplash constructor error: " + ex.Message);
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00007AE8 File Offset: 0x00005CE8
		private void PositionImage()
		{
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00007C08 File Offset: 0x00005E08
		[DebuggerStepThrough]
		private void DismissedEventHandler(SplashScreen sender, object e)
		{
			ExtendedSplash.<DismissedEventHandler>d__2 <DismissedEventHandler>d__;
			<DismissedEventHandler>d__.<>4__this = this;
			<DismissedEventHandler>d__.sender = sender;
			<DismissedEventHandler>d__.e = e;
			<DismissedEventHandler>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<DismissedEventHandler>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <DismissedEventHandler>d__.<>t__builder;
			<>t__builder.Start<ExtendedSplash.<DismissedEventHandler>d__2>(ref <DismissedEventHandler>d__);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00008098 File Offset: 0x00006298
		[DebuggerStepThrough]
		private void LoadDataAsync()
		{
			ExtendedSplash.<LoadDataAsync>d__9 <LoadDataAsync>d__;
			<LoadDataAsync>d__.<>4__this = this;
			<LoadDataAsync>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<LoadDataAsync>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <LoadDataAsync>d__.<>t__builder;
			<>t__builder.Start<ExtendedSplash.<LoadDataAsync>d__9>(ref <LoadDataAsync>d__);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00008390 File Offset: 0x00006590
		[DebuggerStepThrough]
		private Task<bool> TryLoadFromCache()
		{
			ExtendedSplash.<TryLoadFromCache>d__e <TryLoadFromCache>d__e;
			<TryLoadFromCache>d__e.<>4__this = this;
			<TryLoadFromCache>d__e.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<TryLoadFromCache>d__e.<>1__state = -1;
			AsyncTaskMethodBuilder<bool> <>t__builder = <TryLoadFromCache>d__e.<>t__builder;
			<>t__builder.Start<ExtendedSplash.<TryLoadFromCache>d__e>(ref <TryLoadFromCache>d__e);
			return <TryLoadFromCache>d__e.<>t__builder.Task;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00008754 File Offset: 0x00006954
		[DebuggerStepThrough]
		private Task LoadFromServer()
		{
			ExtendedSplash.<LoadFromServer>d__14 <LoadFromServer>d__;
			<LoadFromServer>d__.<>4__this = this;
			<LoadFromServer>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadFromServer>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <LoadFromServer>d__.<>t__builder;
			<>t__builder.Start<ExtendedSplash.<LoadFromServer>d__14>(ref <LoadFromServer>d__);
			return <LoadFromServer>d__.<>t__builder.Task;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00008880 File Offset: 0x00006A80
		[DebuggerStepThrough]
		private Task ParseAndStoreApps()
		{
			ExtendedSplash.<ParseAndStoreApps>d__1c <ParseAndStoreApps>d__1c;
			<ParseAndStoreApps>d__1c.<>4__this = this;
			<ParseAndStoreApps>d__1c.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ParseAndStoreApps>d__1c.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <ParseAndStoreApps>d__1c.<>t__builder;
			<>t__builder.Start<ExtendedSplash.<ParseAndStoreApps>d__1c>(ref <ParseAndStoreApps>d__1c);
			return <ParseAndStoreApps>d__1c.<>t__builder.Task;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00008C70 File Offset: 0x00006E70
		[DebuggerStepThrough]
		private Task<string> LoadCachedJson()
		{
			ExtendedSplash.<LoadCachedJson>d__1f <LoadCachedJson>d__1f;
			<LoadCachedJson>d__1f.<>4__this = this;
			<LoadCachedJson>d__1f.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
			<LoadCachedJson>d__1f.<>1__state = -1;
			AsyncTaskMethodBuilder<string> <>t__builder = <LoadCachedJson>d__1f.<>t__builder;
			<>t__builder.Start<ExtendedSplash.<LoadCachedJson>d__1f>(ref <LoadCachedJson>d__1f);
			return <LoadCachedJson>d__1f.<>t__builder.Task;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00008EC0 File Offset: 0x000070C0
		[DebuggerStepThrough]
		private Task SaveJsonToCache(string json)
		{
			ExtendedSplash.<SaveJsonToCache>d__29 <SaveJsonToCache>d__;
			<SaveJsonToCache>d__.<>4__this = this;
			<SaveJsonToCache>d__.json = json;
			<SaveJsonToCache>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SaveJsonToCache>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <SaveJsonToCache>d__.<>t__builder;
			<>t__builder.Start<ExtendedSplash.<SaveJsonToCache>d__29>(ref <SaveJsonToCache>d__);
			return <SaveJsonToCache>d__.<>t__builder.Task;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00009264 File Offset: 0x00007464
		[DebuggerStepThrough]
		private Task<List<StoreApp>> ParseJsonAsync(string json)
		{
			ExtendedSplash.<ParseJsonAsync>d__32 <ParseJsonAsync>d__;
			<ParseJsonAsync>d__.<>4__this = this;
			<ParseJsonAsync>d__.json = json;
			<ParseJsonAsync>d__.<>t__builder = AsyncTaskMethodBuilder<List<StoreApp>>.Create();
			<ParseJsonAsync>d__.<>1__state = -1;
			AsyncTaskMethodBuilder<List<StoreApp>> <>t__builder = <ParseJsonAsync>d__.<>t__builder;
			<>t__builder.Start<ExtendedSplash.<ParseJsonAsync>d__32>(ref <ParseJsonAsync>d__);
			return <ParseJsonAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000092B8 File Offset: 0x000074B8
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

		// Token: 0x06000058 RID: 88 RVA: 0x000092FC File Offset: 0x000074FC
		[DebuggerNonUserCode]
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this._contentLoaded)
			{
				this._contentLoaded = true;
				Application.LoadComponent(this, new Uri("ms-appx:///ExtendedSplash.xaml"), 0);
				this.VersionText = (TextBlock)base.FindName("VersionText");
				this.LoadingProgressRing = (ProgressRing)base.FindName("LoadingProgressRing");
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000935E File Offset: 0x0000755E
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void Connect(int connectionId, object target)
		{
			this._contentLoaded = true;
		}

		// Token: 0x0400003B RID: 59
		private const string AppsJsonUrl = "http://8store.dankassassin368.com/data/apps.json";

		// Token: 0x0400003C RID: 60
		private const string CACHE_FILE_NAME = "apps_cache.json";

		// Token: 0x0400003D RID: 61
		internal Rect splashImageRect;

		// Token: 0x0400003E RID: 62
		private SplashScreen splash;

		// Token: 0x0400003F RID: 63
		internal bool dismissed = false;

		// Token: 0x04000040 RID: 64
		internal Frame rootFrame;

		// Token: 0x04000041 RID: 65
		private static List<StoreApp> cachedApps = null;

		// Token: 0x04000042 RID: 66
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock VersionText;

		// Token: 0x04000043 RID: 67
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ProgressRing LoadingProgressRing;

		// Token: 0x04000044 RID: 68
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
