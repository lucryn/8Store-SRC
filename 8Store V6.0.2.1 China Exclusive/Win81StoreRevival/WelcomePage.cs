using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Shapes;

namespace Win81StoreRevival
{
	// Token: 0x02000042 RID: 66
	public sealed class WelcomePage : Page, IComponentConnector
	{
		// Token: 0x060003CF RID: 975 RVA: 0x00017008 File Offset: 0x00015208
		public WelcomePage()
		{
			this.InitializeComponent();
			this.localSettings = ApplicationData.Current.LocalSettings;
			this.PlaySound();
			this.SetDefaultSettings();
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00017038 File Offset: 0x00015238
		[DebuggerStepThrough]
		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			WelcomePage.<Page_Loaded>d__2 <Page_Loaded>d__ = new WelcomePage.<Page_Loaded>d__2();
			<Page_Loaded>d__.<>4__this = this;
			<Page_Loaded>d__.sender = sender;
			<Page_Loaded>d__.e = e;
			<Page_Loaded>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<Page_Loaded>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <Page_Loaded>d__.<>t__builder;
			<>t__builder.Start<WelcomePage.<Page_Loaded>d__2>(ref <Page_Loaded>d__);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00017084 File Offset: 0x00015284
		[DebuggerStepThrough]
		private Task PlayEntranceAnimations()
		{
			WelcomePage.<PlayEntranceAnimations>d__3 <PlayEntranceAnimations>d__ = new WelcomePage.<PlayEntranceAnimations>d__3();
			<PlayEntranceAnimations>d__.<>4__this = this;
			<PlayEntranceAnimations>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<PlayEntranceAnimations>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <PlayEntranceAnimations>d__.<>t__builder;
			<>t__builder.Start<WelcomePage.<PlayEntranceAnimations>d__3>(ref <PlayEntranceAnimations>d__);
			return <PlayEntranceAnimations>d__.<>t__builder.Task;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x000170CC File Offset: 0x000152CC
		private void SetDefaultSettings()
		{
			bool flag = !this.localSettings.Values.ContainsKey("AutoOpen");
			if (flag)
			{
				this.localSettings.Values["AutoOpen"] = true;
			}
			bool flag2 = !this.localSettings.Values.ContainsKey("AutoUpdateNotify");
			if (flag2)
			{
				this.localSettings.Values["AutoUpdateNotify"] = true;
			}
			bool flag3 = !this.localSettings.Values.ContainsKey("LiveTile");
			if (flag3)
			{
				this.localSettings.Values["LiveTile"] = true;
			}
			bool flag4 = !this.localSettings.Values.ContainsKey("PerformanceMode");
			if (flag4)
			{
				this.localSettings.Values["PerformanceMode"] = "Normal";
			}
			bool flag5 = !this.localSettings.Values.ContainsKey("DarkMode");
			if (flag5)
			{
				this.localSettings.Values["DarkMode"] = false;
			}
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x000171F4 File Offset: 0x000153F4
		[DebuggerStepThrough]
		private void ExpressButton_Click(object sender, RoutedEventArgs e)
		{
			WelcomePage.<ExpressButton_Click>d__5 <ExpressButton_Click>d__ = new WelcomePage.<ExpressButton_Click>d__5();
			<ExpressButton_Click>d__.<>4__this = this;
			<ExpressButton_Click>d__.sender = sender;
			<ExpressButton_Click>d__.e = e;
			<ExpressButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<ExpressButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <ExpressButton_Click>d__.<>t__builder;
			<>t__builder.Start<WelcomePage.<ExpressButton_Click>d__5>(ref <ExpressButton_Click>d__);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00017240 File Offset: 0x00015440
		[DebuggerStepThrough]
		private void CustomButton_Click(object sender, RoutedEventArgs e)
		{
			WelcomePage.<CustomButton_Click>d__6 <CustomButton_Click>d__ = new WelcomePage.<CustomButton_Click>d__6();
			<CustomButton_Click>d__.<>4__this = this;
			<CustomButton_Click>d__.sender = sender;
			<CustomButton_Click>d__.e = e;
			<CustomButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<CustomButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <CustomButton_Click>d__.<>t__builder;
			<>t__builder.Start<WelcomePage.<CustomButton_Click>d__6>(ref <CustomButton_Click>d__);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0001728C File Offset: 0x0001548C
		[DebuggerStepThrough]
		private Task AnimateButtonPress(Button button)
		{
			WelcomePage.<AnimateButtonPress>d__7 <AnimateButtonPress>d__ = new WelcomePage.<AnimateButtonPress>d__7();
			<AnimateButtonPress>d__.<>4__this = this;
			<AnimateButtonPress>d__.button = button;
			<AnimateButtonPress>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<AnimateButtonPress>d__.<>1__state = -1;
			AsyncTaskMethodBuilder <>t__builder = <AnimateButtonPress>d__.<>t__builder;
			<>t__builder.Start<WelcomePage.<AnimateButtonPress>d__7>(ref <AnimateButtonPress>d__);
			return <AnimateButtonPress>d__.<>t__builder.Task;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x000172DC File Offset: 0x000154DC
		private void PlaySound()
		{
			try
			{
				WelcomePage.<>c__DisplayClass8_0 CS$<>8__locals1 = new WelcomePage.<>c__DisplayClass8_0();
				CS$<>8__locals1.<>4__this = this;
				Debug.WriteLine("Playing update notification sound...");
				WelcomePage.<>c__DisplayClass8_0 CS$<>8__locals2 = CS$<>8__locals1;
				MediaElement mediaElement = new MediaElement();
				mediaElement.put_AutoPlay(true);
				mediaElement.put_Volume(1.0);
				mediaElement.put_Visibility(1);
				mediaElement.put_IsLooping(true);
				CS$<>8__locals2.soundPlayer = mediaElement;
				CS$<>8__locals1.soundPlayer.put_AutoPlay(true);
				CS$<>8__locals1.soundPlayer.put_Volume(1.0);
				CS$<>8__locals1.soundPlayer.put_Visibility(1);
				Grid grid = base.Content as Grid;
				bool flag = grid != null;
				if (flag)
				{
					grid.Children.Add(CS$<>8__locals1.soundPlayer);
				}
				CS$<>8__locals1.soundPlayer.put_Source(new Uri("ms-appx:///Assets/welcome.mp3"));
				MediaElement soundPlayer = CS$<>8__locals1.soundPlayer;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(soundPlayer.add_MediaEnded), new Action<EventRegistrationToken>(soundPlayer.remove_MediaEnded), delegate(object sender, RoutedEventArgs e)
				{
					Debug.WriteLine("Sound playback completed");
					Grid grid2 = CS$<>8__locals1.<>4__this.Content as Grid;
					bool flag2 = grid2 != null;
					if (flag2)
					{
						grid2.Children.Remove(CS$<>8__locals1.soundPlayer);
					}
				});
				soundPlayer = CS$<>8__locals1.soundPlayer;
				WindowsRuntimeMarshal.AddEventHandler<ExceptionRoutedEventHandler>(new Func<ExceptionRoutedEventHandler, EventRegistrationToken>(soundPlayer.add_MediaFailed), new Action<EventRegistrationToken>(soundPlayer.remove_MediaFailed), delegate(object sender, ExceptionRoutedEventArgs e)
				{
					Debug.WriteLine("Sound playback failed: " + e.ErrorMessage);
					Grid grid2 = CS$<>8__locals1.<>4__this.Content as Grid;
					bool flag2 = grid2 != null;
					if (flag2)
					{
						grid2.Children.Remove(CS$<>8__locals1.soundPlayer);
					}
				});
				Debug.WriteLine("Sound playback started");
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Error playing sound: " + ex.Message);
			}
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00017450 File Offset: 0x00015650
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			bool contentLoaded = this._contentLoaded;
			if (!contentLoaded)
			{
				this._contentLoaded = true;
				Application.LoadComponent(this, new Uri("ms-appx:///WelcomePage.xaml"), 0);
				this.TitleEntranceAnimation = (Storyboard)base.FindName("TitleEntranceAnimation");
				this.DescriptionEntranceAnimation = (Storyboard)base.FindName("DescriptionEntranceAnimation");
				this.SectionHeaderEntranceAnimation = (Storyboard)base.FindName("SectionHeaderEntranceAnimation");
				this.SettingsListEntranceAnimation = (Storyboard)base.FindName("SettingsListEntranceAnimation");
				this.SeparatorEntranceAnimation = (Storyboard)base.FindName("SeparatorEntranceAnimation");
				this.ButtonsEntranceAnimation = (Storyboard)base.FindName("ButtonsEntranceAnimation");
				this.PageExitAnimation = (Storyboard)base.FindName("PageExitAnimation");
				this.MainGrid = (Grid)base.FindName("MainGrid");
				this.MainContent = (ScrollViewer)base.FindName("MainContent");
				this.SoundPlayer = (MediaElement)base.FindName("SoundPlayer");
				this.WelcomeTitle = (TextBlock)base.FindName("WelcomeTitle");
				this.DescriptionText = (TextBlock)base.FindName("DescriptionText");
				this.ExpressSettingsHeader = (TextBlock)base.FindName("ExpressSettingsHeader");
				this.SettingsListPanel = (StackPanel)base.FindName("SettingsListPanel");
				this.SeparatorLine = (Rectangle)base.FindName("SeparatorLine");
				this.ButtonsPanel = (StackPanel)base.FindName("ButtonsPanel");
				this.ExpressButton = (Button)base.FindName("ExpressButton");
				this.CustomButton = (Button)base.FindName("CustomButton");
			}
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00017614 File Offset: 0x00015814
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
			{
				FrameworkElement frameworkElement = (FrameworkElement)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(frameworkElement.add_Loaded), new Action<EventRegistrationToken>(frameworkElement.remove_Loaded), new RoutedEventHandler(this.Page_Loaded));
				break;
			}
			case 2:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.ExpressButton_Click));
				break;
			}
			case 3:
			{
				ButtonBase buttonBase = (ButtonBase)target;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase.add_Click), new Action<EventRegistrationToken>(buttonBase.remove_Click), new RoutedEventHandler(this.CustomButton_Click));
				break;
			}
			}
			this._contentLoaded = true;
		}

		// Token: 0x040001E5 RID: 485
		private ApplicationDataContainer localSettings;

		// Token: 0x040001E6 RID: 486
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Storyboard TitleEntranceAnimation;

		// Token: 0x040001E7 RID: 487
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Storyboard DescriptionEntranceAnimation;

		// Token: 0x040001E8 RID: 488
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Storyboard SectionHeaderEntranceAnimation;

		// Token: 0x040001E9 RID: 489
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Storyboard SettingsListEntranceAnimation;

		// Token: 0x040001EA RID: 490
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Storyboard SeparatorEntranceAnimation;

		// Token: 0x040001EB RID: 491
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Storyboard ButtonsEntranceAnimation;

		// Token: 0x040001EC RID: 492
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Storyboard PageExitAnimation;

		// Token: 0x040001ED RID: 493
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Grid MainGrid;

		// Token: 0x040001EE RID: 494
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ScrollViewer MainContent;

		// Token: 0x040001EF RID: 495
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private MediaElement SoundPlayer;

		// Token: 0x040001F0 RID: 496
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock WelcomeTitle;

		// Token: 0x040001F1 RID: 497
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock DescriptionText;

		// Token: 0x040001F2 RID: 498
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock ExpressSettingsHeader;

		// Token: 0x040001F3 RID: 499
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel SettingsListPanel;

		// Token: 0x040001F4 RID: 500
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Rectangle SeparatorLine;

		// Token: 0x040001F5 RID: 501
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel ButtonsPanel;

		// Token: 0x040001F6 RID: 502
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button ExpressButton;

		// Token: 0x040001F7 RID: 503
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button CustomButton;

		// Token: 0x040001F8 RID: 504
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
