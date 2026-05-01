using System;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace Callisto.Controls
{
	// Token: 0x02000010 RID: 16
	[TemplateVisualState(Name = "Pressed", GroupName = "Common")]
	[TemplateVisualState(Name = "Disabled", GroupName = "Common")]
	[TemplateVisualState(Name = "Base", GroupName = "Common")]
	[TemplateVisualState(Name = "Hover", GroupName = "Common")]
	[Obsolete("Windows 8.1 now provides this functionality in the XAML framework itself as MenuFlyoutItem.")]
	public class MenuItem : MenuItemBase
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x00005422 File Offset: 0x00003622
		public MenuItem()
		{
			base.put_DefaultStyleKey(typeof(MenuItem));
			this._isActive = false;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00005441 File Offset: 0x00003641
		protected override void OnPointerEntered(PointerRoutedEventArgs e)
		{
			this._isActive = true;
			this.UpdateState(true);
			base.OnPointerEntered(e);
			base.Focus(3);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00005460 File Offset: 0x00003660
		protected override void OnPointerExited(PointerRoutedEventArgs e)
		{
			base.OnPointerExited(e);
			this._isActive = false;
			this.UpdateState(true);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00005477 File Offset: 0x00003677
		protected override void OnPointerMoved(PointerRoutedEventArgs e)
		{
			this._isActive = true;
			this.UpdateState(true);
			base.OnPointerMoved(e);
			base.Focus(3);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00005496 File Offset: 0x00003696
		protected override void OnGotFocus(RoutedEventArgs e)
		{
			base.OnGotFocus(e);
			this._isActive = true;
			this.UpdateState(true);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000054AD File Offset: 0x000036AD
		protected override void OnLostFocus(RoutedEventArgs e)
		{
			base.OnLostFocus(e);
			this._isActive = false;
			this.UpdateState(true);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000054C4 File Offset: 0x000036C4
		protected override void OnKeyDown(KeyRoutedEventArgs e)
		{
			base.OnKeyDown(e);
			if (e.Key == 13 || e.Key == 32)
			{
				this.OnTapped(new TappedRoutedEventArgs());
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000054EC File Offset: 0x000036EC
		protected override void OnTapped(TappedRoutedEventArgs e)
		{
			base.OnTapped(e);
			VisualStateManager.GoToState(this, "Base", true);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00005502 File Offset: 0x00003702
		protected override void OnPointerReleased(PointerRoutedEventArgs e)
		{
			base.OnPointerReleased(e);
			VisualStateManager.GoToState(this, "Base", true);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00005518 File Offset: 0x00003718
		protected override void OnPointerPressed(PointerRoutedEventArgs e)
		{
			base.OnPointerPressed(e);
			VisualStateManager.GoToState(this, "Pressed", true);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000552E File Offset: 0x0000372E
		private void UpdateState(bool useTransitions)
		{
			if (!base.IsEnabled)
			{
				VisualStateManager.GoToState(this, "Disabled", useTransitions);
				return;
			}
			if (this._isActive)
			{
				VisualStateManager.GoToState(this, "Hover", useTransitions);
				return;
			}
			VisualStateManager.GoToState(this, "Base", useTransitions);
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00005569 File Offset: 0x00003769
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x0000557B File Offset: 0x0000377B
		public string Text
		{
			get
			{
				return (string)base.GetValue(MenuItem.TextProperty);
			}
			set
			{
				base.SetValue(MenuItem.TextProperty, value);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00005589 File Offset: 0x00003789
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x0000559B File Offset: 0x0000379B
		public ICommand Command
		{
			get
			{
				return (ICommand)base.GetValue(MenuItem.CommandProperty);
			}
			set
			{
				base.SetValue(MenuItem.CommandProperty, value);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x000055A9 File Offset: 0x000037A9
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x000055B6 File Offset: 0x000037B6
		public object CommandParameter
		{
			get
			{
				return base.GetValue(MenuItem.CommandParameterProperty);
			}
			set
			{
				base.SetValue(MenuItem.CommandParameterProperty, value);
			}
		}

		// Token: 0x0400005C RID: 92
		private const string GroupCommon = "Common";

		// Token: 0x0400005D RID: 93
		private const string StateBase = "Base";

		// Token: 0x0400005E RID: 94
		private const string StateHover = "Hover";

		// Token: 0x0400005F RID: 95
		private const string StatePressed = "Pressed";

		// Token: 0x04000060 RID: 96
		private const string StateDisabled = "Disabled";

		// Token: 0x04000061 RID: 97
		public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(MenuItem), null);

		// Token: 0x04000062 RID: 98
		public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(MenuItem), null);

		// Token: 0x04000063 RID: 99
		public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(MenuItem), null);

		// Token: 0x04000064 RID: 100
		private bool _isActive;
	}
}
