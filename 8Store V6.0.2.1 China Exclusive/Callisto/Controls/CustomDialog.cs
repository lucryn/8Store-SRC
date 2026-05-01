using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Callisto.Controls
{
	// Token: 0x02000017 RID: 23
	[TemplatePart(Name = "PART_RootGrid", Type = typeof(Grid))]
	[TemplatePart(Name = "PART_Content", Type = typeof(ContentPresenter))]
	[TemplatePart(Name = "PART_BackButton", Type = typeof(Button))]
	[TemplatePart(Name = "PART_RootBorder", Type = typeof(Border))]
	public class CustomDialog : ContentControl
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x00005F07 File Offset: 0x00004107
		public CustomDialog()
		{
			base.put_DefaultStyleKey(typeof(CustomDialog));
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00005F40 File Offset: 0x00004140
		protected override void OnApplyTemplate()
		{
			this._rootGrid = (Grid)base.GetTemplateChild("PART_RootGrid");
			this._rootBorder = (Border)base.GetTemplateChild("PART_RootBorder");
			this._backButton = (Button)base.GetTemplateChild("PART_BackButton");
			this.ResizeContainers();
			if (this._backButton != null)
			{
				Button backButton = this._backButton;
				WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(backButton.add_Click), new Action<EventRegistrationToken>(backButton.remove_Click), delegate(object bbs, RoutedEventArgs bba)
				{
					if (this.BackButtonClicked != null)
					{
						this.BackButtonClicked.Invoke(bbs, bba);
						return;
					}
					this.IsOpen = false;
				});
			}
			Window window = Window.Current;
			WindowsRuntimeMarshal.AddEventHandler<WindowSizeChangedEventHandler>(new Func<WindowSizeChangedEventHandler, EventRegistrationToken>(window.add_SizeChanged), new Action<EventRegistrationToken>(window.remove_SizeChanged), new WindowSizeChangedEventHandler(this.OnWindowSizeChanged));
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(base.add_Unloaded), new Action<EventRegistrationToken>(base.remove_Unloaded), new RoutedEventHandler(this.OnUnloaded));
			base.OnApplyTemplate();
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00006034 File Offset: 0x00004234
		private void ResizeContainers()
		{
			if (this._rootGrid != null)
			{
				this._rootGrid.put_Width(Window.Current.Bounds.Width);
				this._rootGrid.put_Height(Window.Current.Bounds.Height);
			}
			if (this._rootBorder != null)
			{
				this._rootBorder.put_Width(Window.Current.Bounds.Width);
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000060A8 File Offset: 0x000042A8
		private void OnWindowSizeChanged(object sender, WindowSizeChangedEventArgs e)
		{
			this.ResizeContainers();
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000060B0 File Offset: 0x000042B0
		private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
			WindowsRuntimeMarshal.RemoveEventHandler<RoutedEventHandler>(new Action<EventRegistrationToken>(base.remove_Unloaded), new RoutedEventHandler(this.OnUnloaded));
			WindowsRuntimeMarshal.RemoveEventHandler<WindowSizeChangedEventHandler>(new Action<EventRegistrationToken>(Window.Current.remove_SizeChanged), new WindowSizeChangedEventHandler(this.OnWindowSizeChanged));
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060000D6 RID: 214 RVA: 0x000060F0 File Offset: 0x000042F0
		// (remove) Token: 0x060000D7 RID: 215 RVA: 0x00006128 File Offset: 0x00004328
		public event RoutedEventHandler BackButtonClicked;

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x0000615D File Offset: 0x0000435D
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x0000616F File Offset: 0x0000436F
		public Visibility BackButtonVisibility
		{
			get
			{
				return (Visibility)base.GetValue(CustomDialog.BackButtonVisibilityProperty);
			}
			set
			{
				base.SetValue(CustomDialog.BackButtonVisibilityProperty, value);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00006182 File Offset: 0x00004382
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00006194 File Offset: 0x00004394
		public bool IsOpen
		{
			get
			{
				return (bool)base.GetValue(CustomDialog.IsOpenProperty);
			}
			set
			{
				base.SetValue(CustomDialog.IsOpenProperty, value);
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000061A8 File Offset: 0x000043A8
		private static void OnIsOpenPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if ((bool)e.NewValue)
			{
				CustomDialog customDialog = d as CustomDialog;
				if (customDialog != null)
				{
					customDialog.ApplyTemplate();
				}
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000DD RID: 221 RVA: 0x000061D3 File Offset: 0x000043D3
		// (set) Token: 0x060000DE RID: 222 RVA: 0x000061E5 File Offset: 0x000043E5
		public string Title
		{
			get
			{
				return (string)base.GetValue(CustomDialog.TitleProperty);
			}
			set
			{
				base.SetValue(CustomDialog.TitleProperty, value);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000DF RID: 223 RVA: 0x000061F3 File Offset: 0x000043F3
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x00006205 File Offset: 0x00004405
		public ICommand BackButtonCommand
		{
			get
			{
				return (ICommand)base.GetValue(CustomDialog.BackButtonCommandProperty);
			}
			set
			{
				base.SetValue(CustomDialog.BackButtonCommandProperty, value);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00006213 File Offset: 0x00004413
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00006220 File Offset: 0x00004420
		public object BackButtonCommandParameter
		{
			get
			{
				return base.GetValue(CustomDialog.BackButtonCommandParameterProperty);
			}
			set
			{
				base.SetValue(CustomDialog.BackButtonCommandParameterProperty, value);
			}
		}

		// Token: 0x04000070 RID: 112
		private const string PART_ROOT_BORDER = "PART_RootBorder";

		// Token: 0x04000071 RID: 113
		private const string PART_ROOT_GRID = "PART_RootGrid";

		// Token: 0x04000072 RID: 114
		private const string PART_BACK_BUTTON = "PART_BackButton";

		// Token: 0x04000073 RID: 115
		private const string PART_CONTENT = "PART_Content";

		// Token: 0x04000075 RID: 117
		private Grid _rootGrid;

		// Token: 0x04000076 RID: 118
		private Border _rootBorder;

		// Token: 0x04000077 RID: 119
		private Button _backButton;

		// Token: 0x04000078 RID: 120
		public static readonly DependencyProperty BackButtonVisibilityProperty = DependencyProperty.Register("BackButtonVisibility", typeof(Visibility), typeof(CustomDialog), new PropertyMetadata(1));

		// Token: 0x04000079 RID: 121
		public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register("IsOpen", typeof(bool), typeof(CustomDialog), new PropertyMetadata(false, new PropertyChangedCallback(CustomDialog.OnIsOpenPropertyChanged)));

		// Token: 0x0400007A RID: 122
		public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(CustomDialog), null);

		// Token: 0x0400007B RID: 123
		public static readonly DependencyProperty BackButtonCommandProperty = DependencyProperty.Register("BackButtonCommand", typeof(ICommand), typeof(CustomDialog), new PropertyMetadata(DependencyProperty.UnsetValue));

		// Token: 0x0400007C RID: 124
		public static readonly DependencyProperty BackButtonCommandParameterProperty = DependencyProperty.Register("BackButtonCommandParameter", typeof(object), typeof(CustomDialog), new PropertyMetadata(DependencyProperty.UnsetValue));
	}
}
