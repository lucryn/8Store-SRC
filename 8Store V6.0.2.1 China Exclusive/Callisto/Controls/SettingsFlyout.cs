using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.UI.ApplicationSettings;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace Callisto.Controls
{
	// Token: 0x0200001E RID: 30
	[Obsolete("Windows 8.1 now provides this functionality in the XAML framework itself as SettingsFlyout.")]
	public sealed class SettingsFlyout : ContentControl
	{
		// Token: 0x06000177 RID: 375 RVA: 0x00008404 File Offset: 0x00006604
		protected override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			if (this._backButton != null)
			{
				WindowsRuntimeMarshal.RemoveEventHandler<RoutedEventHandler>(new Action<EventRegistrationToken>(this._backButton.remove_Click), new RoutedEventHandler(this.OnBackButtonClicked));
			}
			this._backButton = (base.GetTemplateChild("SettingsBackButton") as Button);
			if (this._backButton != null)
			{
				Button backButton = this._backButton;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(backButton.add_Click), new Action<EventRegistrationToken>(backButton.remove_Click), new RoutedEventHandler(this.OnBackButtonClicked));
			}
			if (this._contentGrid == null)
			{
				this._contentGrid = (base.GetTemplateChild("SettingsFlyoutContentGrid") as Grid);
			}
			if (this._contentGrid != null)
			{
				this._contentGrid.put_Transitions(new TransitionCollection());
				ICollection<Transition> transitions = this._contentGrid.Transitions;
				EntranceThemeTransition entranceThemeTransition = new EntranceThemeTransition();
				entranceThemeTransition.put_FromHorizontalOffset((double)((SettingsPane.Edge == null) ? 100 : -100));
				transitions.Add(entranceThemeTransition);
			}
			this._rootBorder = (base.GetTemplateChild("PART_RootBorder") as Border);
			this._contentScrollViewer = (base.GetTemplateChild("PART_ContentScrollViewer") as ScrollViewer);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000851C File Offset: 0x0000671C
		public SettingsFlyout()
		{
			base.put_DefaultStyleKey(typeof(SettingsFlyout));
			this._windowBounds = Window.Current.Bounds;
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(base.add_Loaded), new Action<EventRegistrationToken>(base.remove_Loaded), new RoutedEventHandler(this.OnLoaded));
			this._hostPopup = new Popup();
			this._hostPopup.put_ChildTransitions(new TransitionCollection());
			ICollection<Transition> childTransitions = this._hostPopup.ChildTransitions;
			PaneThemeTransition paneThemeTransition = new PaneThemeTransition();
			paneThemeTransition.put_Edge((SettingsPane.Edge == null) ? 2 : 0);
			childTransitions.Add(paneThemeTransition);
			Popup hostPopup = this._hostPopup;
			WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(hostPopup.add_Closed), new Action<EventRegistrationToken>(hostPopup.remove_Closed), new EventHandler<object>(this.OnHostPopupClosed));
			this._hostPopup.put_IsLightDismissEnabled(true);
			this._hostPopup.put_Height(this._windowBounds.Height);
			this._hostPopup.put_Child(this);
			this._hostPopup.SetValue(Canvas.TopProperty, 0);
			base.put_Height(this._windowBounds.Height);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00008644 File Offset: 0x00006844
		private void OnLoaded(object sender, RoutedEventArgs e)
		{
			Window window = Window.Current;
			WindowsRuntimeMarshal.AddEventHandler<WindowActivatedEventHandler>(new Func<WindowActivatedEventHandler, EventRegistrationToken>(window.add_Activated), new Action<EventRegistrationToken>(window.remove_Activated), new WindowActivatedEventHandler(this.OnCurrentWindowActivated));
			Window window2 = Window.Current;
			WindowsRuntimeMarshal.AddEventHandler<WindowSizeChangedEventHandler>(new Func<WindowSizeChangedEventHandler, EventRegistrationToken>(window2.add_SizeChanged), new Action<EventRegistrationToken>(window2.remove_SizeChanged), new WindowSizeChangedEventHandler(this.OnCurrentWindowSizeChanged));
			if (SettingsPane.Edge == 1 && this._rootBorder != null)
			{
				this._rootBorder.put_BorderThickness(new Thickness(0.0, 0.0, 1.0, 0.0));
			}
			this._settingsWidth = (double)this.FlyoutWidth;
			FrameworkElement hostPopup = this._hostPopup;
			double settingsWidth;
			this._contentScrollViewer.put_Width(settingsWidth = this._settingsWidth);
			double num;
			base.put_Width(num = settingsWidth);
			hostPopup.put_Width(num);
			this._hostPopup.SetValue(Canvas.LeftProperty, (SettingsPane.Edge == null) ? (this._windowBounds.Width - this._settingsWidth) : 0.0);
			if (this._hostPopup.Parent == null)
			{
				InputPane forCurrentView = InputPane.GetForCurrentView();
				WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<InputPane, InputPaneVisibilityEventArgs>>(new Func<TypedEventHandler<InputPane, InputPaneVisibilityEventArgs>, EventRegistrationToken>(forCurrentView.add_Showing), new Action<EventRegistrationToken>(forCurrentView.remove_Showing), new TypedEventHandler<InputPane, InputPaneVisibilityEventArgs>(this.OnInputPaneShowing));
				InputPane forCurrentView2 = InputPane.GetForCurrentView();
				WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<InputPane, InputPaneVisibilityEventArgs>>(new Func<TypedEventHandler<InputPane, InputPaneVisibilityEventArgs>, EventRegistrationToken>(forCurrentView2.add_Hiding), new Action<EventRegistrationToken>(forCurrentView2.remove_Hiding), new TypedEventHandler<InputPane, InputPaneVisibilityEventArgs>(this.OnInputPaneHiding));
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000087D2 File Offset: 0x000069D2
		private void OnCurrentWindowSizeChanged(object sender, WindowSizeChangedEventArgs e)
		{
			this.IsOpen = false;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000087DB File Offset: 0x000069DB
		private void OnInputPaneHiding(InputPane sender, InputPaneVisibilityEventArgs args)
		{
			if (this._ihmFocusMoved)
			{
				Popup hostPopup = this._hostPopup;
				hostPopup.put_VerticalOffset(hostPopup.VerticalOffset + this._ihmOccludeHeight);
				this._ihmFocusMoved = false;
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00008804 File Offset: 0x00006A04
		private void OnInputPaneShowing(InputPane sender, InputPaneVisibilityEventArgs args)
		{
			FrameworkElement frameworkElement = FocusManager.GetFocusedElement() as FrameworkElement;
			if (frameworkElement != null)
			{
				GeneralTransform generalTransform = frameworkElement.TransformToVisual(Window.Current.Content);
				Rect rect = generalTransform.TransformBounds(new Rect(0.0, 0.0, frameworkElement.ActualWidth, frameworkElement.ActualHeight));
				if (rect.Bottom > this._windowBounds.Height - args.OccludedRect.Top)
				{
					this._ihmFocusMoved = true;
					this._ihmOccludeHeight = ((rect.Top < (double)((int)args.OccludedRect.Top)) ? rect.Top : args.OccludedRect.Top);
					Popup hostPopup = this._hostPopup;
					hostPopup.put_VerticalOffset(hostPopup.VerticalOffset - this._ihmOccludeHeight);
				}
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600017D RID: 381 RVA: 0x000088D8 File Offset: 0x00006AD8
		// (remove) Token: 0x0600017E RID: 382 RVA: 0x00008910 File Offset: 0x00006B10
		public event EventHandler<BackClickedEventArgs> BackClicked;

		// Token: 0x0600017F RID: 383 RVA: 0x00008948 File Offset: 0x00006B48
		private void InvokeOnBackClick(BackClickedEventArgs args)
		{
			EventHandler<BackClickedEventArgs> backClicked = this.BackClicked;
			if (backClicked != null)
			{
				backClicked.Invoke(this, args);
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00008968 File Offset: 0x00006B68
		private void OnBackButtonClicked(object sender, object e)
		{
			BackClickedEventArgs backClickedEventArgs = new BackClickedEventArgs
			{
				Cancel = false
			};
			this.InvokeOnBackClick(backClickedEventArgs);
			if (backClickedEventArgs.Cancel)
			{
				return;
			}
			if (this._hostPopup != null)
			{
				this._hostPopup.put_IsOpen(false);
			}
			if (ApplicationView.Value != 2)
			{
				SettingsPane.Show();
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000089B8 File Offset: 0x00006BB8
		private void OnHostPopupClosed(object sender, object e)
		{
			this._hostPopup.put_Child(null);
			WindowsRuntimeMarshal.RemoveEventHandler<WindowActivatedEventHandler>(new Action<EventRegistrationToken>(Window.Current.remove_Activated), new WindowActivatedEventHandler(this.OnCurrentWindowActivated));
			WindowsRuntimeMarshal.RemoveEventHandler<WindowSizeChangedEventHandler>(new Action<EventRegistrationToken>(Window.Current.remove_SizeChanged), new WindowSizeChangedEventHandler(this.OnCurrentWindowSizeChanged));
			WindowsRuntimeMarshal.RemoveEventHandler<TypedEventHandler<InputPane, InputPaneVisibilityEventArgs>>(new Action<EventRegistrationToken>(InputPane.GetForCurrentView().remove_Showing), new TypedEventHandler<InputPane, InputPaneVisibilityEventArgs>(this.OnInputPaneShowing));
			WindowsRuntimeMarshal.RemoveEventHandler<TypedEventHandler<InputPane, InputPaneVisibilityEventArgs>>(new Action<EventRegistrationToken>(InputPane.GetForCurrentView().remove_Hiding), new TypedEventHandler<InputPane, InputPaneVisibilityEventArgs>(this.OnInputPaneHiding));
			base.put_Content(null);
			if (this.Closed != null)
			{
				this.Closed.Invoke(this, e);
			}
			this.IsOpen = false;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00008A78 File Offset: 0x00006C78
		private void OnCurrentWindowActivated(object sender, WindowActivatedEventArgs e)
		{
			if (e.WindowActivationState == 1)
			{
				this.IsOpen = false;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00008A8A File Offset: 0x00006C8A
		public Popup HostPopup
		{
			get
			{
				return this._hostPopup;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00008A92 File Offset: 0x00006C92
		// (set) Token: 0x06000185 RID: 389 RVA: 0x00008AA4 File Offset: 0x00006CA4
		public bool IsOpen
		{
			get
			{
				return (bool)base.GetValue(SettingsFlyout.IsOpenProperty);
			}
			set
			{
				base.SetValue(SettingsFlyout.IsOpenProperty, value);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00008AB7 File Offset: 0x00006CB7
		// (set) Token: 0x06000187 RID: 391 RVA: 0x00008AC9 File Offset: 0x00006CC9
		public SolidColorBrush HeaderBrush
		{
			get
			{
				return (SolidColorBrush)base.GetValue(SettingsFlyout.HeaderBrushProperty);
			}
			set
			{
				base.SetValue(SettingsFlyout.HeaderBrushProperty, value);
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00008AD8 File Offset: 0x00006CD8
		private static void OnHeaderBrushColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.OldValue != e.NewValue)
			{
				SolidColorBrush solidColorBrush = e.NewValue as SolidColorBrush;
				if (solidColorBrush != null)
				{
					int num = ((int)solidColorBrush.Color.R * 299 + (int)solidColorBrush.Color.G * 587 + (int)(solidColorBrush.Color.B * 114)) / 1000;
				}
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00008B44 File Offset: 0x00006D44
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00008B56 File Offset: 0x00006D56
		public SettingsFlyout.SettingsFlyoutWidth FlyoutWidth
		{
			get
			{
				return (SettingsFlyout.SettingsFlyoutWidth)base.GetValue(SettingsFlyout.FlyoutWidthProperty);
			}
			set
			{
				base.SetValue(SettingsFlyout.FlyoutWidthProperty, value);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00008B69 File Offset: 0x00006D69
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00008B7B File Offset: 0x00006D7B
		public string HeaderText
		{
			get
			{
				return (string)base.GetValue(SettingsFlyout.HeaderTextProperty);
			}
			set
			{
				base.SetValue(SettingsFlyout.HeaderTextProperty, value);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00008B89 File Offset: 0x00006D89
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00008B9B File Offset: 0x00006D9B
		public ImageSource SmallLogoImageSource
		{
			get
			{
				return (ImageSource)base.GetValue(SettingsFlyout.SmallLogoImageSourceProperty);
			}
			set
			{
				base.SetValue(SettingsFlyout.SmallLogoImageSourceProperty, value);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00008BA9 File Offset: 0x00006DA9
		// (set) Token: 0x06000190 RID: 400 RVA: 0x00008BBB File Offset: 0x00006DBB
		public SolidColorBrush ContentForegroundBrush
		{
			get
			{
				return (SolidColorBrush)base.GetValue(SettingsFlyout.ContentForegroundBrushProperty);
			}
			set
			{
				base.SetValue(SettingsFlyout.ContentForegroundBrushProperty, value);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00008BC9 File Offset: 0x00006DC9
		// (set) Token: 0x06000192 RID: 402 RVA: 0x00008BDB File Offset: 0x00006DDB
		public SolidColorBrush ContentBackgroundBrush
		{
			get
			{
				return (SolidColorBrush)base.GetValue(SettingsFlyout.ContentBackgroundBrushProperty);
			}
			set
			{
				base.SetValue(SettingsFlyout.ContentBackgroundBrushProperty, value);
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000193 RID: 403 RVA: 0x00008BEC File Offset: 0x00006DEC
		// (remove) Token: 0x06000194 RID: 404 RVA: 0x00008C24 File Offset: 0x00006E24
		public event EventHandler<object> Closed;

		// Token: 0x040000B8 RID: 184
		private const int CONTENT_HORIZONTAL_OFFSET = 100;

		// Token: 0x040000B9 RID: 185
		private const string PART_BACK_BUTTON = "SettingsBackButton";

		// Token: 0x040000BA RID: 186
		private const string PART_CONTENT_GRID = "SettingsFlyoutContentGrid";

		// Token: 0x040000BB RID: 187
		private const string PART_ROOT_BORDER = "PART_RootBorder";

		// Token: 0x040000BC RID: 188
		private const string PART_CONTENT_SCROLLVIEWER = "PART_ContentScrollViewer";

		// Token: 0x040000BD RID: 189
		private Popup _hostPopup;

		// Token: 0x040000BE RID: 190
		private Rect _windowBounds;

		// Token: 0x040000BF RID: 191
		private double _settingsWidth;

		// Token: 0x040000C0 RID: 192
		private Button _backButton;

		// Token: 0x040000C1 RID: 193
		private Grid _contentGrid;

		// Token: 0x040000C2 RID: 194
		private Border _rootBorder;

		// Token: 0x040000C3 RID: 195
		private ScrollViewer _contentScrollViewer;

		// Token: 0x040000C4 RID: 196
		private bool _ihmFocusMoved;

		// Token: 0x040000C5 RID: 197
		private double _ihmOccludeHeight;

		// Token: 0x040000C7 RID: 199
		public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register("IsOpen", typeof(bool), typeof(SettingsFlyout), new PropertyMetadata(false, delegate(DependencyObject obj, DependencyPropertyChangedEventArgs args)
		{
			if (args.NewValue != args.OldValue)
			{
				SettingsFlyout settingsFlyout = (SettingsFlyout)obj;
				settingsFlyout._hostPopup.put_IsOpen((bool)args.NewValue);
			}
		}));

		// Token: 0x040000C8 RID: 200
		public static readonly DependencyProperty HeaderBrushProperty = DependencyProperty.Register("HeaderBrush", typeof(SolidColorBrush), typeof(SettingsFlyout), new PropertyMetadata(null, new PropertyChangedCallback(SettingsFlyout.OnHeaderBrushColorChanged)));

		// Token: 0x040000C9 RID: 201
		public static readonly DependencyProperty FlyoutWidthProperty = DependencyProperty.Register("FlyoutWidth", typeof(SettingsFlyout.SettingsFlyoutWidth), typeof(SettingsFlyout), new PropertyMetadata(SettingsFlyout.SettingsFlyoutWidth.Narrow));

		// Token: 0x040000CA RID: 202
		public static readonly DependencyProperty HeaderTextProperty = DependencyProperty.Register("HeaderText", typeof(string), typeof(SettingsFlyout), new PropertyMetadata(null));

		// Token: 0x040000CB RID: 203
		public static readonly DependencyProperty SmallLogoImageSourceProperty = DependencyProperty.Register("SmallLogoImageSource", typeof(ImageSource), typeof(SettingsFlyout), null);

		// Token: 0x040000CC RID: 204
		public static readonly DependencyProperty ContentForegroundBrushProperty = DependencyProperty.Register("ContentForegroundBrush", typeof(SolidColorBrush), typeof(SettingsFlyout), null);

		// Token: 0x040000CD RID: 205
		public static readonly DependencyProperty ContentBackgroundBrushProperty = DependencyProperty.Register("ContentBackgroundBrush", typeof(SolidColorBrush), typeof(SettingsFlyout), null);

		// Token: 0x0200001F RID: 31
		public enum SettingsFlyoutWidth
		{
			// Token: 0x040000D1 RID: 209
			Narrow = 346,
			// Token: 0x040000D2 RID: 210
			Wide = 646
		}
	}
}
