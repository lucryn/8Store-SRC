using System;
using Callisto.Controls.Common;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace Callisto.Controls
{
	// Token: 0x0200001A RID: 26
	[TemplateVisualState(Name = "PointerExited", GroupName = "CommonStates")]
	[TemplateVisualState(Name = "Focused", GroupName = "FocusStates")]
	[TemplateVisualState(Name = "Unfocused", GroupName = "FocusStates")]
	[TemplateVisualState(Name = "PointerPressed", GroupName = "CommonStates")]
	[TemplateVisualState(Name = "Disabled", GroupName = "CommonStates")]
	[TemplateVisualState(Name = "Partial", GroupName = "FillStates")]
	[TemplateVisualState(Name = "Normal", GroupName = "CommonStates")]
	[TemplateVisualState(Name = "PointerOver", GroupName = "CommonStates")]
	[TemplateVisualState(Name = "Filled", GroupName = "FillStates")]
	[TemplateVisualState(Name = "Empty", GroupName = "FillStates")]
	public class RatingItem : ButtonBase, IUpdateVisualState
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00008035 File Offset: 0x00006235
		// (set) Token: 0x06000158 RID: 344 RVA: 0x00008048 File Offset: 0x00006248
		public double DisplayValue
		{
			get
			{
				return (double)base.GetValue(RatingItem.DisplayValueProperty);
			}
			internal set
			{
				this._settingDisplayValue = true;
				try
				{
					base.SetValue(RatingItem.DisplayValueProperty, value);
				}
				finally
				{
					this._settingDisplayValue = false;
				}
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00008088 File Offset: 0x00006288
		private static void OnDisplayValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			RatingItem ratingItem = (RatingItem)d;
			ratingItem.OnDisplayValueChanged((double)e.OldValue, (double)e.NewValue);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000080B8 File Offset: 0x000062B8
		private void OnDisplayValueChanged(double oldValue, double newValue)
		{
			if (!this._settingDisplayValue)
			{
				this._settingDisplayValue = true;
				this.DisplayValue = oldValue;
				throw new InvalidOperationException(string.Format("Invalid attempt to change read-only property '{0}.'", new object[]
				{
					"DisplayValue"
				}));
			}
			if (newValue <= 0.0)
			{
				VisualStates.GoToState(this, true, new string[]
				{
					"Empty"
				});
				return;
			}
			if (newValue >= 1.0)
			{
				VisualStates.GoToState(this, true, new string[]
				{
					"Filled"
				});
				return;
			}
			VisualStates.GoToState(this, true, new string[]
			{
				"Partial"
			});
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600015B RID: 347 RVA: 0x0000815A File Offset: 0x0000635A
		// (set) Token: 0x0600015C RID: 348 RVA: 0x0000816C File Offset: 0x0000636C
		public SolidColorBrush PointerOverFill
		{
			get
			{
				return (SolidColorBrush)base.GetValue(RatingItem.PointerOverFillProperty);
			}
			set
			{
				base.SetValue(RatingItem.PointerOverFillProperty, value);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600015D RID: 349 RVA: 0x0000817A File Offset: 0x0000637A
		// (set) Token: 0x0600015E RID: 350 RVA: 0x0000818C File Offset: 0x0000638C
		public SolidColorBrush PointerPressedFill
		{
			get
			{
				return (SolidColorBrush)base.GetValue(RatingItem.PointerPressedFillProperty);
			}
			set
			{
				base.SetValue(RatingItem.PointerPressedFillProperty, value);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600015F RID: 351 RVA: 0x0000819A File Offset: 0x0000639A
		// (set) Token: 0x06000160 RID: 352 RVA: 0x000081AC File Offset: 0x000063AC
		public SolidColorBrush ReadOnlyFill
		{
			get
			{
				return (SolidColorBrush)base.GetValue(RatingItem.ReadOnlyFillProperty);
			}
			set
			{
				base.SetValue(RatingItem.ReadOnlyFillProperty, value);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000161 RID: 353 RVA: 0x000081BA File Offset: 0x000063BA
		// (set) Token: 0x06000162 RID: 354 RVA: 0x000081C2 File Offset: 0x000063C2
		internal Rating ParentRating { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000163 RID: 355 RVA: 0x000081CB File Offset: 0x000063CB
		// (set) Token: 0x06000164 RID: 356 RVA: 0x000081DD File Offset: 0x000063DD
		internal double Value
		{
			get
			{
				return (double)base.GetValue(RatingItem.ValueProperty);
			}
			set
			{
				base.SetValue(RatingItem.ValueProperty, value);
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000081F0 File Offset: 0x000063F0
		internal void SelectValue()
		{
			if (!base.IsEnabled)
			{
				this.Value = 1.0;
				this.OnTapped(null);
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00008210 File Offset: 0x00006410
		public RatingItem()
		{
			base.put_DefaultStyleKey(typeof(RatingItem));
			this._interactionHelper = new InteractionHelper(this);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00008234 File Offset: 0x00006434
		protected override void OnPointerPressed(PointerRoutedEventArgs e)
		{
			if (this._interactionHelper.AllowPointerPressed(e))
			{
				this._interactionHelper.OnPointerPressedBase();
			}
			base.OnPointerPressed(e);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00008256 File Offset: 0x00006456
		protected override void OnPointerReleased(PointerRoutedEventArgs e)
		{
			if (this._interactionHelper.AllowPointerPressed(e))
			{
				this._interactionHelper.OnPointerReleasedBase();
			}
			base.OnPointerReleased(e);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00008278 File Offset: 0x00006478
		protected override void OnPointerEntered(PointerRoutedEventArgs e)
		{
			if (this._interactionHelper.AllowPointerEnter(e))
			{
				this._interactionHelper.UpdateVisualStateBase(true);
			}
			base.OnPointerEntered(e);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000829B File Offset: 0x0000649B
		protected override void OnPointerExited(PointerRoutedEventArgs e)
		{
			if (this._interactionHelper.AllowPointerExit(e))
			{
				this._interactionHelper.UpdateVisualStateBase(true);
			}
			base.OnPointerExited(e);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000082BE File Offset: 0x000064BE
		protected override void OnTapped(TappedRoutedEventArgs e)
		{
			base.OnTapped(e);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000082C7 File Offset: 0x000064C7
		void IUpdateVisualState.UpdateVisualState(bool useTransitions)
		{
			this._interactionHelper.UpdateVisualStateBase(useTransitions);
		}

		// Token: 0x040000A7 RID: 167
		private const string StateFilled = "Filled";

		// Token: 0x040000A8 RID: 168
		private const string StateEmpty = "Empty";

		// Token: 0x040000A9 RID: 169
		private const string GroupFill = "FillStates";

		// Token: 0x040000AA RID: 170
		private const string StatePartial = "Partial";

		// Token: 0x040000AB RID: 171
		private InteractionHelper _interactionHelper;

		// Token: 0x040000AC RID: 172
		private bool _settingDisplayValue;

		// Token: 0x040000AD RID: 173
		public static readonly DependencyProperty DisplayValueProperty = DependencyProperty.Register("DisplayValue", typeof(double), typeof(RatingItem), new PropertyMetadata(0.0, new PropertyChangedCallback(RatingItem.OnDisplayValueChanged)));

		// Token: 0x040000AE RID: 174
		public static readonly DependencyProperty PointerOverFillProperty = DependencyProperty.Register("PointerOverFill", typeof(SolidColorBrush), typeof(RatingItem), null);

		// Token: 0x040000AF RID: 175
		public static readonly DependencyProperty PointerPressedFillProperty = DependencyProperty.Register("PointerPressedFill", typeof(SolidColorBrush), typeof(RatingItem), null);

		// Token: 0x040000B0 RID: 176
		public static readonly DependencyProperty ReadOnlyFillProperty = DependencyProperty.Register("ReadOnlyFill", typeof(SolidColorBrush), typeof(RatingItem), null);

		// Token: 0x040000B1 RID: 177
		internal static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double), typeof(RatingItem), new PropertyMetadata(0.0));
	}
}
