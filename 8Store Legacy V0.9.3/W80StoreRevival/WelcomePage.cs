using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

namespace W80StoreRevival
{
	// Token: 0x02000015 RID: 21
	public sealed class WelcomePage : Page, IComponentConnector
	{
		// Token: 0x060000E1 RID: 225 RVA: 0x0001048F File Offset: 0x0000E68F
		public WelcomePage()
		{
			this.InitializeComponent();
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000104A1 File Offset: 0x0000E6A1
		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			this.StartEntranceAnimations();
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000104AC File Offset: 0x0000E6AC
		private void StartEntranceAnimations()
		{
			this.TitleEntranceAnimation.Begin();
			this.animationTimer = new DispatcherTimer();
			this.animationTimer.put_Interval(TimeSpan.FromSeconds(0.5));
			DispatcherTimer dispatcherTimer = this.animationTimer;
			WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(dispatcherTimer.add_Tick), new Action<EventRegistrationToken>(dispatcherTimer.remove_Tick), new EventHandler<object>(this.AnimationTimer_Tick));
			this.animationTimer.Start();
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00010528 File Offset: 0x0000E728
		private void AnimationTimer_Tick(object sender, object e)
		{
			this.animationTimer.Stop();
			this.DescriptionEntranceAnimation.Begin();
			this.animationTimer.put_Interval(TimeSpan.FromSeconds(0.3));
			WindowsRuntimeMarshal.RemoveEventHandler<EventHandler<object>>(new Action<EventRegistrationToken>(this.animationTimer.remove_Tick), new EventHandler<object>(this.AnimationTimer_Tick));
			DispatcherTimer dispatcherTimer = this.animationTimer;
			WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(dispatcherTimer.add_Tick), new Action<EventRegistrationToken>(dispatcherTimer.remove_Tick), new EventHandler<object>(this.AnimationTimer_Tick2));
			this.animationTimer.Start();
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000105C8 File Offset: 0x0000E7C8
		private void AnimationTimer_Tick2(object sender, object e)
		{
			this.animationTimer.Stop();
			this.SectionHeaderEntranceAnimation.Begin();
			this.animationTimer.put_Interval(TimeSpan.FromSeconds(0.3));
			WindowsRuntimeMarshal.RemoveEventHandler<EventHandler<object>>(new Action<EventRegistrationToken>(this.animationTimer.remove_Tick), new EventHandler<object>(this.AnimationTimer_Tick2));
			DispatcherTimer dispatcherTimer = this.animationTimer;
			WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(dispatcherTimer.add_Tick), new Action<EventRegistrationToken>(dispatcherTimer.remove_Tick), new EventHandler<object>(this.AnimationTimer_Tick3));
			this.animationTimer.Start();
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00010668 File Offset: 0x0000E868
		private void AnimationTimer_Tick3(object sender, object e)
		{
			this.animationTimer.Stop();
			this.SettingsListEntranceAnimation.Begin();
			this.animationTimer.put_Interval(TimeSpan.FromSeconds(0.3));
			WindowsRuntimeMarshal.RemoveEventHandler<EventHandler<object>>(new Action<EventRegistrationToken>(this.animationTimer.remove_Tick), new EventHandler<object>(this.AnimationTimer_Tick3));
			DispatcherTimer dispatcherTimer = this.animationTimer;
			WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(dispatcherTimer.add_Tick), new Action<EventRegistrationToken>(dispatcherTimer.remove_Tick), new EventHandler<object>(this.AnimationTimer_Tick4));
			this.animationTimer.Start();
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00010708 File Offset: 0x0000E908
		private void AnimationTimer_Tick4(object sender, object e)
		{
			this.animationTimer.Stop();
			this.SeparatorEntranceAnimation.Begin();
			this.animationTimer.put_Interval(TimeSpan.FromSeconds(0.3));
			WindowsRuntimeMarshal.RemoveEventHandler<EventHandler<object>>(new Action<EventRegistrationToken>(this.animationTimer.remove_Tick), new EventHandler<object>(this.AnimationTimer_Tick4));
			DispatcherTimer dispatcherTimer = this.animationTimer;
			WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(dispatcherTimer.add_Tick), new Action<EventRegistrationToken>(dispatcherTimer.remove_Tick), new EventHandler<object>(this.AnimationTimer_Tick5));
			this.animationTimer.Start();
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000107A8 File Offset: 0x0000E9A8
		private void AnimationTimer_Tick5(object sender, object e)
		{
			this.animationTimer.Stop();
			this.animationTimer = null;
			this.ButtonsEntranceAnimation.Begin();
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000109FC File Offset: 0x0000EBFC
		[DebuggerStepThrough]
		private void ExpressButton_Click(object sender, RoutedEventArgs e)
		{
			WelcomePage.<ExpressButton_Click>d__0 <ExpressButton_Click>d__;
			<ExpressButton_Click>d__.<>4__this = this;
			<ExpressButton_Click>d__.sender = sender;
			<ExpressButton_Click>d__.e = e;
			<ExpressButton_Click>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<ExpressButton_Click>d__.<>1__state = -1;
			AsyncVoidMethodBuilder <>t__builder = <ExpressButton_Click>d__.<>t__builder;
			<>t__builder.Start<WelcomePage.<ExpressButton_Click>d__0>(ref <ExpressButton_Click>d__);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00010A48 File Offset: 0x0000EC48
		protected override void OnNavigatedFrom(NavigationEventArgs e)
		{
			base.OnNavigatedFrom(e);
			if (this.animationTimer != null)
			{
				this.animationTimer.Stop();
				this.animationTimer = null;
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00010A80 File Offset: 0x0000EC80
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (!this._contentLoaded)
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
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00010C30 File Offset: 0x0000EE30
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
			}
			this._contentLoaded = true;
		}

		// Token: 0x040000B3 RID: 179
		private DispatcherTimer animationTimer;

		// Token: 0x040000B4 RID: 180
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Storyboard TitleEntranceAnimation;

		// Token: 0x040000B5 RID: 181
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Storyboard DescriptionEntranceAnimation;

		// Token: 0x040000B6 RID: 182
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Storyboard SectionHeaderEntranceAnimation;

		// Token: 0x040000B7 RID: 183
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Storyboard SettingsListEntranceAnimation;

		// Token: 0x040000B8 RID: 184
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Storyboard SeparatorEntranceAnimation;

		// Token: 0x040000B9 RID: 185
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Storyboard ButtonsEntranceAnimation;

		// Token: 0x040000BA RID: 186
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Storyboard PageExitAnimation;

		// Token: 0x040000BB RID: 187
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Grid MainGrid;

		// Token: 0x040000BC RID: 188
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private ScrollViewer MainContent;

		// Token: 0x040000BD RID: 189
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private MediaElement SoundPlayer;

		// Token: 0x040000BE RID: 190
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock WelcomeTitle;

		// Token: 0x040000BF RID: 191
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock DescriptionText;

		// Token: 0x040000C0 RID: 192
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private TextBlock ExpressSettingsHeader;

		// Token: 0x040000C1 RID: 193
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel SettingsListPanel;

		// Token: 0x040000C2 RID: 194
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Rectangle SeparatorLine;

		// Token: 0x040000C3 RID: 195
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private StackPanel ButtonsPanel;

		// Token: 0x040000C4 RID: 196
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private Button ExpressButton;

		// Token: 0x040000C5 RID: 197
		[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
		private bool _contentLoaded;
	}
}
