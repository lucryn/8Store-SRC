using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Callisto.Controls.Common;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Callisto.Controls
{
	// Token: 0x02000021 RID: 33
	[TemplatePart(Name = "PART_Watermark", Type = typeof(ContentControl))]
	[Obsolete("Windows 8.1 now provides this functionality in the XAML framework itself as PlaceholderText on TextBox.")]
	public sealed class WatermarkTextBox : TextBox
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00008E01 File Offset: 0x00007001
		// (set) Token: 0x0600019B RID: 411 RVA: 0x00008E09 File Offset: 0x00007009
		internal TextBlock ElementContent { get; set; }

		// Token: 0x0600019C RID: 412 RVA: 0x00008E14 File Offset: 0x00007014
		public WatermarkTextBox()
		{
			base.put_DefaultStyleKey(typeof(WatermarkTextBox));
			this._resources = new ResourceLoader("Callisto/Resources");
			this.Watermark = this._resources.GetString("WatermarkTextBoxDefault");
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(base.add_GotFocus), new Action<EventRegistrationToken>(base.remove_GotFocus), new RoutedEventHandler(this.OnGotFocus));
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(base.add_LostFocus), new Action<EventRegistrationToken>(base.remove_LostFocus), new RoutedEventHandler(this.OnLostFocus));
			WindowsRuntimeMarshal.AddEventHandler<PointerEventHandler>(new Func<PointerEventHandler, EventRegistrationToken>(base.add_PointerEntered), new Action<EventRegistrationToken>(base.remove_PointerEntered), new PointerEventHandler(this.OnPointerEntered));
			WindowsRuntimeMarshal.AddEventHandler<PointerEventHandler>(new Func<PointerEventHandler, EventRegistrationToken>(base.add_PointerExited), new Action<EventRegistrationToken>(base.remove_PointerExited), new PointerEventHandler(this.OnPointerExited));
			WindowsRuntimeMarshal.AddEventHandler<TextChangedEventHandler>(new Func<TextChangedEventHandler, EventRegistrationToken>(base.add_TextChanged), new Action<EventRegistrationToken>(base.remove_TextChanged), new TextChangedEventHandler(this.OnTextChanged));
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(base.add_Loaded), new Action<EventRegistrationToken>(base.remove_Loaded), new RoutedEventHandler(this.OnLoaded));
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00008F65 File Offset: 0x00007165
		private void OnLoaded(object sender, RoutedEventArgs e)
		{
			base.ApplyTemplate();
			this.ChangeVisualState(false);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00008F75 File Offset: 0x00007175
		private void OnTextChanged(object sender, TextChangedEventArgs e)
		{
			this.ChangeVisualState();
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00008F7D File Offset: 0x0000717D
		private void OnPointerExited(object sender, PointerRoutedEventArgs e)
		{
			this.IsHovered = false;
			if (!this.HasFocusInternal)
			{
				this.ChangeVisualState();
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00008F94 File Offset: 0x00007194
		private void OnPointerEntered(object sender, PointerRoutedEventArgs e)
		{
			this.IsHovered = true;
			if (!this.HasFocusInternal)
			{
				this.ChangeVisualState();
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00008FAB File Offset: 0x000071AB
		internal void ChangeVisualState()
		{
			this.ChangeVisualState(true);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00008FB4 File Offset: 0x000071B4
		internal void ChangeVisualState(bool useTransitions)
		{
			if (!base.IsEnabled)
			{
				VisualStates.GoToState(this, useTransitions, new string[]
				{
					"Disabled",
					"Normal"
				});
			}
			else if (this.IsHovered)
			{
				VisualStates.GoToState(this, useTransitions, new string[]
				{
					"PointerOver",
					"Normal"
				});
			}
			else
			{
				VisualStates.GoToState(this, useTransitions, new string[]
				{
					"Normal"
				});
			}
			if (this.HasFocusInternal && base.IsEnabled)
			{
				VisualStates.GoToState(this, useTransitions, new string[]
				{
					"Focused",
					"Unfocused"
				});
			}
			else
			{
				VisualStates.GoToState(this, useTransitions, new string[]
				{
					"Unfocused"
				});
			}
			if (this.Watermark != null && string.IsNullOrEmpty(base.Text) && !this.HasFocusInternal)
			{
				VisualStates.GoToState(this, useTransitions, new string[]
				{
					"Watermarked",
					"Unwatermarked"
				});
				return;
			}
			VisualStates.GoToState(this, useTransitions, new string[]
			{
				"Unwatermarked"
			});
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000090D0 File Offset: 0x000072D0
		private void OnLostFocus(object sender, RoutedEventArgs e)
		{
			this.HasFocusInternal = false;
			this.ChangeVisualState();
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000090DF File Offset: 0x000072DF
		private void OnGotFocus(object sender, RoutedEventArgs e)
		{
			if (base.IsEnabled)
			{
				this.HasFocusInternal = true;
				this.ChangeVisualState();
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000090F8 File Offset: 0x000072F8
		private void OnWatermarkChanged()
		{
			if (this.ElementContent != null)
			{
				Control control = this.Watermark as Control;
				if (control != null)
				{
					control.put_IsTabStop(false);
					control.put_IsHitTestVisible(false);
				}
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000912C File Offset: 0x0000732C
		protected override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			this.ElementContent = (TextBlock)base.GetTemplateChild("PART_Watermark");
			if (!string.IsNullOrEmpty(base.PlaceholderText))
			{
				this.Watermark = base.PlaceholderText;
			}
			this.OnWatermarkChanged();
			this.ChangeVisualState(false);
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x0000917B File Offset: 0x0000737B
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x00009188 File Offset: 0x00007388
		public object Watermark
		{
			get
			{
				return base.GetValue(WatermarkTextBox.WatermarkProperty);
			}
			set
			{
				base.SetValue(WatermarkTextBox.WatermarkProperty, value);
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00009198 File Offset: 0x00007398
		private static void OnWatermarkPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			WatermarkTextBox watermarkTextBox = d as WatermarkTextBox;
			if (watermarkTextBox != null)
			{
				Control control = watermarkTextBox.Watermark as Control;
				if (control != null)
				{
					control.put_IsTabStop(true);
					control.put_IsHitTestVisible(true);
				}
			}
		}

		// Token: 0x040000D4 RID: 212
		private const string PART_ELEMENT_CONTENT_NAME = "PART_Watermark";

		// Token: 0x040000D5 RID: 213
		internal bool IsHovered;

		// Token: 0x040000D6 RID: 214
		internal bool HasFocusInternal;

		// Token: 0x040000D7 RID: 215
		private ResourceLoader _resources;

		// Token: 0x040000D8 RID: 216
		public static readonly DependencyProperty WatermarkProperty = DependencyProperty.Register("Watermark", typeof(object), typeof(WatermarkTextBox), new PropertyMetadata(null, new PropertyChangedCallback(WatermarkTextBox.OnWatermarkPropertyChanged)));
	}
}
