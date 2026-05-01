using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Callisto.Controls.Common;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace Callisto.Controls
{
	// Token: 0x02000019 RID: 25
	[TemplateVisualState(Name = "PointerOver", GroupName = "CommonStates")]
	[StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(RatingItem))]
	[TemplateVisualState(Name = "ReadOnly", GroupName = "CommonStates")]
	[TemplateVisualState(Name = "Normal", GroupName = "CommonStates")]
	[TemplateVisualState(Name = "Disabled", GroupName = "CommonStates")]
	[TemplateVisualState(Name = "PointerPressed", GroupName = "CommonStates")]
	[TemplateVisualState(Name = "Focused", GroupName = "FocusStates")]
	[TemplateVisualState(Name = "Unfocused", GroupName = "FocusStates")]
	public class Rating : ItemsControl, IUpdateVisualState
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000112 RID: 274 RVA: 0x000070F7 File Offset: 0x000052F7
		// (set) Token: 0x06000113 RID: 275 RVA: 0x00007109 File Offset: 0x00005309
		protected double DisplayValue
		{
			get
			{
				return (double)base.GetValue(Rating.DisplayValueProperty);
			}
			set
			{
				base.SetValue(Rating.DisplayValueProperty, value);
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000711C File Offset: 0x0000531C
		private static void OnDisplayValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
		{
			Rating rating = (Rating)dependencyObject;
			rating.OnDisplayValueChanged();
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00007136 File Offset: 0x00005336
		private void OnDisplayValueChanged()
		{
			this.UpdateDisplayValues();
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000116 RID: 278 RVA: 0x0000713E File Offset: 0x0000533E
		// (set) Token: 0x06000117 RID: 279 RVA: 0x00007146 File Offset: 0x00005346
		private RatingItem HoveredRatingItem { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000118 RID: 280 RVA: 0x0000714F File Offset: 0x0000534F
		// (set) Token: 0x06000119 RID: 281 RVA: 0x00007157 File Offset: 0x00005357
		internal InteractionHelper Interaction { get; private set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00007160 File Offset: 0x00005360
		// (set) Token: 0x0600011B RID: 283 RVA: 0x00007168 File Offset: 0x00005368
		private ItemsControlHelper ItemsControlHelper { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00007171 File Offset: 0x00005371
		// (set) Token: 0x0600011D RID: 285 RVA: 0x00007183 File Offset: 0x00005383
		public int ItemCount
		{
			get
			{
				return (int)base.GetValue(Rating.ItemCountProperty);
			}
			set
			{
				base.SetValue(Rating.ItemCountProperty, value);
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00007198 File Offset: 0x00005398
		private static void OnItemCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Rating rating = d as Rating;
			int newValue = (int)e.NewValue;
			rating.OnItemCountChanged(newValue);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000071C0 File Offset: 0x000053C0
		private void OnItemCountChanged(int newValue)
		{
			if (newValue < 0)
			{
				throw new ArgumentException("Value must be larger than or equal to 0.");
			}
			int num = newValue - base.Items.Count;
			if (num > 0)
			{
				for (int i = 0; i < num; i++)
				{
					base.Items.Add(new RatingItem());
				}
				return;
			}
			if (num < 0)
			{
				for (int j = 0; j < Math.Abs(num); j++)
				{
					base.Items.RemoveAt(base.Items.Count - 1);
				}
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00007238 File Offset: 0x00005438
		private static void OnItemContainerStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Rating rating = (Rating)d;
			Style newValue = (Style)e.NewValue;
			rating.OnItemContainerStyleChanged(newValue);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000725F File Offset: 0x0000545F
		protected virtual void OnItemContainerStyleChanged(Style newValue)
		{
			this.ItemsControlHelper.UpdateItemContainerStyle(newValue);
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000122 RID: 290 RVA: 0x0000726D File Offset: 0x0000546D
		// (set) Token: 0x06000123 RID: 291 RVA: 0x0000727F File Offset: 0x0000547F
		public RatingSelectionMode SelectionMode
		{
			get
			{
				return (RatingSelectionMode)base.GetValue(Rating.SelectionModeProperty);
			}
			set
			{
				base.SetValue(Rating.SelectionModeProperty, value);
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00007294 File Offset: 0x00005494
		private static void OnSelectionModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Rating rating = (Rating)d;
			RatingSelectionMode oldValue = (RatingSelectionMode)e.OldValue;
			RatingSelectionMode newValue = (RatingSelectionMode)e.NewValue;
			rating.OnSelectionModeChanged(oldValue, newValue);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000072C8 File Offset: 0x000054C8
		protected virtual void OnSelectionModeChanged(RatingSelectionMode oldValue, RatingSelectionMode newValue)
		{
			this.UpdateDisplayValues();
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000126 RID: 294 RVA: 0x000072D0 File Offset: 0x000054D0
		// (set) Token: 0x06000127 RID: 295 RVA: 0x000072E2 File Offset: 0x000054E2
		public double Value
		{
			get
			{
				return (double)base.GetValue(Rating.ValueProperty);
			}
			set
			{
				base.SetValue(Rating.ValueProperty, value);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000128 RID: 296 RVA: 0x000072F5 File Offset: 0x000054F5
		// (set) Token: 0x06000129 RID: 297 RVA: 0x000072FD File Offset: 0x000054FD
		public double WeightedValue { get; private set; }

		// Token: 0x0600012A RID: 298 RVA: 0x00007308 File Offset: 0x00005508
		private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Rating rating = (Rating)d;
			double oldValue = (double)e.OldValue;
			double newValue = (double)e.NewValue;
			rating.OnValueChanged(oldValue, newValue);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000733C File Offset: 0x0000553C
		protected virtual void OnValueChanged(double oldValue, double newValue)
		{
			this.WeightedValue = newValue / (double)this.ItemCount;
			this.UpdateValues();
			ValueChangedEventHandler<double> valueChanged = this.ValueChanged;
			if (valueChanged != null)
			{
				valueChanged(this, new ValueChangedEventArgs<double>(oldValue, newValue));
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00007376 File Offset: 0x00005576
		protected override void OnItemsChanged(object e)
		{
			this.ItemCount = base.Items.Count;
			base.OnItemsChanged(e);
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600012D RID: 301 RVA: 0x00007390 File Offset: 0x00005590
		// (remove) Token: 0x0600012E RID: 302 RVA: 0x000073C8 File Offset: 0x000055C8
		public event ValueChangedEventHandler<double> ValueChanged;

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600012F RID: 303 RVA: 0x000073FD File Offset: 0x000055FD
		// (set) Token: 0x06000130 RID: 304 RVA: 0x0000740F File Offset: 0x0000560F
		public SolidColorBrush PointerPressedFill
		{
			get
			{
				return (SolidColorBrush)base.GetValue(Rating.PointerPressedFillProperty);
			}
			set
			{
				base.SetValue(Rating.PointerPressedFillProperty, value);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000131 RID: 305 RVA: 0x0000741D File Offset: 0x0000561D
		// (set) Token: 0x06000132 RID: 306 RVA: 0x0000742F File Offset: 0x0000562F
		public SolidColorBrush PointerOverFill
		{
			get
			{
				return (SolidColorBrush)base.GetValue(Rating.PointerOverFillProperty);
			}
			set
			{
				base.SetValue(Rating.PointerOverFillProperty, value);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000133 RID: 307 RVA: 0x0000743D File Offset: 0x0000563D
		// (set) Token: 0x06000134 RID: 308 RVA: 0x0000744F File Offset: 0x0000564F
		public SolidColorBrush ReadOnlyFill
		{
			get
			{
				return (SolidColorBrush)base.GetValue(Rating.ReadOnlyFillProperty);
			}
			set
			{
				base.SetValue(Rating.ReadOnlyFillProperty, value);
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000746C File Offset: 0x0000566C
		public Rating()
		{
			base.put_DefaultStyleKey(typeof(Rating));
			this.Interaction = new InteractionHelper(this);
			this.ItemsControlHelper = new ItemsControlHelper(this);
			WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(base.add_LayoutUpdated), new Action<EventRegistrationToken>(base.remove_LayoutUpdated), delegate(object snd, object arg)
			{
				this.UpdateValues();
				this.UpdateDisplayValues();
			});
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000074D9 File Offset: 0x000056D9
		protected override void OnApplyTemplate()
		{
			this.ItemsControlHelper.OnApplyTemplate();
			base.OnApplyTemplate();
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000074EC File Offset: 0x000056EC
		protected override void OnPointerEntered(PointerRoutedEventArgs e)
		{
			if (this.Interaction.AllowPointerEnter(e))
			{
				this.Interaction.UpdateVisualStateBase(true);
			}
			base.OnPointerEntered(e);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000750F File Offset: 0x0000570F
		protected override void OnPointerExited(PointerRoutedEventArgs e)
		{
			if (this.Interaction.AllowPointerExit(e))
			{
				this.Interaction.UpdateVisualStateBase(true);
			}
			base.OnPointerExited(e);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00007532 File Offset: 0x00005732
		protected override void OnPointerPressed(PointerRoutedEventArgs e)
		{
			if (this.Interaction.AllowPointerPressed(e))
			{
				this.Interaction.OnPointerPressedBase();
			}
			base.OnPointerPressed(e);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00007554 File Offset: 0x00005754
		protected override void OnPointerReleased(PointerRoutedEventArgs e)
		{
			if (this.Interaction.AllowPointerPressed(e))
			{
				this.Interaction.OnPointerReleasedBase();
			}
			base.OnPointerReleased(e);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000758C File Offset: 0x0000578C
		private void UpdateValues()
		{
			IList<RatingItem> list = Enumerable.ToList<RatingItem>(this.GetRatingItems());
			this.GetSelectedRatingItem();
			IEnumerable<Tuple<RatingItem, double>> enumerable = EnumerableFunctions.Zip<RatingItem, double, Tuple<RatingItem, double>>(list, Enumerable.Select<RatingItem, double>(list, (RatingItem ratingItem) => 1.0).GetWeightedValues(this.WeightedValue), (RatingItem item, double percent) => Tuple.Create<RatingItem, double>(item, percent));
			foreach (Tuple<RatingItem, double> tuple in enumerable)
			{
				tuple.Item1.Value = tuple.Item2;
			}
			this.GetSelectedRatingItem();
			if (this.HoveredRatingItem == null)
			{
				this.DisplayValue = this.WeightedValue;
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00007688 File Offset: 0x00005888
		private void UpdateDisplayValues()
		{
			IList<RatingItem> list = Enumerable.ToList<RatingItem>(this.GetRatingItems());
			IEnumerable<Tuple<RatingItem, double>> enumerable = EnumerableFunctions.Zip<RatingItem, double, Tuple<RatingItem, double>>(list, Enumerable.Select<RatingItem, double>(list, (RatingItem ratingItem) => 1.0).GetWeightedValues(this.WeightedValue), (RatingItem item, double percent) => Tuple.Create<RatingItem, double>(item, percent));
			Tuple<RatingItem, double> tuple = Enumerable.LastOrDefault<Tuple<RatingItem, double>>(enumerable, (Tuple<RatingItem, double> i) => i.Item2 > 0.0);
			RatingItem ratingItem2;
			if (tuple != null)
			{
				ratingItem2 = tuple.Item1;
			}
			else
			{
				ratingItem2 = this.GetSelectedRatingItem();
			}
			foreach (Tuple<RatingItem, double> tuple2 in enumerable)
			{
				if (this.SelectionMode == RatingSelectionMode.Individual && tuple2.Item1 != ratingItem2)
				{
					tuple2.Item1.DisplayValue = 0.0;
				}
				else
				{
					tuple2.Item1.DisplayValue = tuple2.Item2;
				}
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000077A4 File Offset: 0x000059A4
		private void UpdateHoverStates()
		{
			if (this.HoveredRatingItem != null && base.IsEnabled)
			{
				IList<RatingItem> list = Enumerable.ToList<RatingItem>(this.GetRatingItems());
				int num = list.IndexOf(this.HoveredRatingItem);
				double num2 = (double)Enumerable.Count<RatingItem>(list);
				double num3 = (double)(num + 1);
				this.DisplayValue = num3 / num2;
				for (int i = 0; i < list.Count; i++)
				{
					RatingItem ratingItem = list[i];
					if (i <= num && this.SelectionMode == RatingSelectionMode.Continuous)
					{
						VisualStates.GoToState(ratingItem, true, new string[]
						{
							"PointerOver"
						});
					}
					else
					{
						IUpdateVisualState updateVisualState = ratingItem;
						updateVisualState.UpdateVisualState(true);
					}
				}
				return;
			}
			this.DisplayValue = this.Value;
			foreach (IUpdateVisualState updateVisualState2 in Enumerable.OfType<IUpdateVisualState>(this.GetRatingItems()))
			{
				updateVisualState2.UpdateVisualState(true);
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000078A4 File Offset: 0x00005AA4
		protected override DependencyObject GetContainerForItemOverride()
		{
			return new RatingItem();
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000078AB File Offset: 0x00005AAB
		protected override bool IsItemItsOwnContainerOverride(object item)
		{
			return item is RatingItem;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000078B8 File Offset: 0x00005AB8
		protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
		{
			RatingItem ratingItem = (RatingItem)element;
			int num = base.ItemContainerGenerator.IndexFromContainer(element);
			if (num > -1)
			{
				ToolTipService.SetToolTip(ratingItem, (num + 1).ToString());
			}
			FrameworkElement frameworkElement = ratingItem;
			DependencyProperty foregroundProperty = Control.ForegroundProperty;
			Binding binding = new Binding();
			binding.put_Path(new PropertyPath("Foreground"));
			binding.put_Source(this);
			frameworkElement.SetBinding(foregroundProperty, binding);
			FrameworkElement frameworkElement2 = ratingItem;
			DependencyProperty pointerOverFillProperty = RatingItem.PointerOverFillProperty;
			Binding binding2 = new Binding();
			binding2.put_Path(new PropertyPath("PointerOverFill"));
			binding2.put_Source(this);
			frameworkElement2.SetBinding(pointerOverFillProperty, binding2);
			FrameworkElement frameworkElement3 = ratingItem;
			DependencyProperty pointerPressedFillProperty = RatingItem.PointerPressedFillProperty;
			Binding binding3 = new Binding();
			binding3.put_Path(new PropertyPath("PointerPressedFill"));
			binding3.put_Source(this);
			frameworkElement3.SetBinding(pointerPressedFillProperty, binding3);
			FrameworkElement frameworkElement4 = ratingItem;
			DependencyProperty fontSizeProperty = Control.FontSizeProperty;
			Binding binding4 = new Binding();
			binding4.put_Path(new PropertyPath("FontSize"));
			binding4.put_Source(this);
			frameworkElement4.SetBinding(fontSizeProperty, binding4);
			FrameworkElement frameworkElement5 = ratingItem;
			DependencyProperty tagProperty = FrameworkElement.TagProperty;
			Binding binding5 = new Binding();
			binding5.put_Path(new PropertyPath("Tag"));
			binding5.put_Source(this);
			frameworkElement5.SetBinding(tagProperty, binding5);
			FrameworkElement frameworkElement6 = ratingItem;
			DependencyProperty backgroundProperty = Control.BackgroundProperty;
			Binding binding6 = new Binding();
			binding6.put_Path(new PropertyPath("Background"));
			binding6.put_Source(this);
			frameworkElement6.SetBinding(backgroundProperty, binding6);
			FrameworkElement frameworkElement7 = ratingItem;
			DependencyProperty readOnlyFillProperty = RatingItem.ReadOnlyFillProperty;
			Binding binding7 = new Binding();
			binding7.put_Path(new PropertyPath("ReadOnlyFill"));
			binding7.put_Source(this);
			frameworkElement7.SetBinding(readOnlyFillProperty, binding7);
			ratingItem.put_IsEnabled(base.IsEnabled);
			if (ratingItem.Style == null)
			{
				ratingItem.put_Style(base.ItemContainerStyle);
			}
			WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(ratingItem.add_Click), new Action<EventRegistrationToken>(ratingItem.remove_Click), new RoutedEventHandler(this.RatingItemTapped));
			WindowsRuntimeMarshal.AddEventHandler<PointerEventHandler>(new Func<PointerEventHandler, EventRegistrationToken>(ratingItem.add_PointerEntered), new Action<EventRegistrationToken>(ratingItem.remove_PointerEntered), new PointerEventHandler(this.RatingItemPointerEnter));
			WindowsRuntimeMarshal.AddEventHandler<PointerEventHandler>(new Func<PointerEventHandler, EventRegistrationToken>(ratingItem.add_PointerExited), new Action<EventRegistrationToken>(ratingItem.remove_PointerExited), new PointerEventHandler(this.RatingItemPointerExited));
			ratingItem.ParentRating = this;
			base.PrepareContainerForItemOverride(element, item);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00007ACC File Offset: 0x00005CCC
		protected override void ClearContainerForItemOverride(DependencyObject element, object item)
		{
			RatingItem ratingItem = (RatingItem)element;
			WindowsRuntimeMarshal.RemoveEventHandler<RoutedEventHandler>(new Action<EventRegistrationToken>(ratingItem.remove_Click), new RoutedEventHandler(this.RatingItemTapped));
			WindowsRuntimeMarshal.RemoveEventHandler<PointerEventHandler>(new Action<EventRegistrationToken>(ratingItem.remove_PointerEntered), new PointerEventHandler(this.RatingItemPointerEnter));
			WindowsRuntimeMarshal.RemoveEventHandler<PointerEventHandler>(new Action<EventRegistrationToken>(ratingItem.remove_PointerExited), new PointerEventHandler(this.RatingItemPointerExited));
			ratingItem.ParentRating = null;
			if (ratingItem == this.HoveredRatingItem)
			{
				this.HoveredRatingItem = null;
				this.UpdateDisplayValues();
				this.UpdateHoverStates();
			}
			base.ClearContainerForItemOverride(element, item);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00007B62 File Offset: 0x00005D62
		private void RatingItemPointerEnter(object sender, PointerRoutedEventArgs e)
		{
			this.HoveredRatingItem = (RatingItem)sender;
			this.UpdateHoverStates();
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00007B76 File Offset: 0x00005D76
		private void RatingItemPointerExited(object sender, PointerRoutedEventArgs e)
		{
			this.HoveredRatingItem = null;
			this.UpdateDisplayValues();
			this.UpdateHoverStates();
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00007BA8 File Offset: 0x00005DA8
		internal IEnumerable<RatingItem> GetRatingItems()
		{
			return Enumerable.Where<RatingItem>(Enumerable.Select<int, RatingItem>(Enumerable.Range(0, base.Items.Count), (int index) => (RatingItem)base.ItemContainerGenerator.ContainerFromIndex(index)), (RatingItem ratingItem) => ratingItem != null);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00007C04 File Offset: 0x00005E04
		internal void SelectRatingItem(RatingItem selectedRatingItem)
		{
			if (base.IsEnabled)
			{
				IList<RatingItem> list = Enumerable.ToList<RatingItem>(this.GetRatingItems());
				IEnumerable<double> enumerable = Enumerable.Select<RatingItem, double>(list, (RatingItem ratingItem) => 1.0);
				double num = (double)Enumerable.Count<RatingItem>(list);
				if (num != 0.0)
				{
					double value = Enumerable.Sum(Enumerable.Take<double>(enumerable, list.IndexOf(selectedRatingItem) + 1)) / num;
					this.Value = value;
				}
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00007C7C File Offset: 0x00005E7C
		private void RatingItemTapped(object sender, RoutedEventArgs e)
		{
			if (base.IsEnabled)
			{
				RatingItem ratingItem = (RatingItem)sender;
				this.OnRatingItemValueSelected(ratingItem, 1.0);
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00007CBB File Offset: 0x00005EBB
		private RatingItem GetSelectedRatingItem()
		{
			return Enumerable.LastOrDefault<RatingItem>(this.GetRatingItems(), (RatingItem ratingItem) => ratingItem.Value > 0.0);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00007CE8 File Offset: 0x00005EE8
		protected virtual void OnRatingItemValueSelected(RatingItem ratingItem, double newValue)
		{
			List<RatingItem> list = Enumerable.ToList<RatingItem>(this.GetRatingItems());
			Enumerable.Count<RatingItem>(list);
			this.Value = (double)(list.IndexOf(ratingItem) + 1);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00007D18 File Offset: 0x00005F18
		protected override void OnKeyDown(KeyRoutedEventArgs e)
		{
			if (!this.Interaction.AllowKeyDown(e))
			{
				return;
			}
			base.OnKeyDown(e);
			if (e.Handled)
			{
				return;
			}
			VirtualKey logicalKey = InteractionHelper.GetLogicalKey(base.FlowDirection, e.Key);
			VirtualKey virtualKey = logicalKey;
			switch (virtualKey)
			{
			case 37:
			{
				RatingItem ratingItem = FocusManager.GetFocusedElement() as RatingItem;
				if (ratingItem != null)
				{
					ratingItem = this.GetRatingItemAtOffsetFrom(ratingItem, -1);
				}
				else
				{
					ratingItem = Enumerable.FirstOrDefault<RatingItem>(this.GetRatingItems());
				}
				if (ratingItem != null && ratingItem.Focus(2))
				{
					e.put_Handled(true);
					return;
				}
				break;
			}
			case 38:
				break;
			case 39:
			{
				RatingItem ratingItem2 = FocusManager.GetFocusedElement() as RatingItem;
				if (ratingItem2 != null)
				{
					ratingItem2 = this.GetRatingItemAtOffsetFrom(ratingItem2, 1);
				}
				else
				{
					ratingItem2 = Enumerable.FirstOrDefault<RatingItem>(this.GetRatingItems());
				}
				if (ratingItem2 != null && ratingItem2.Focus(2))
				{
					e.put_Handled(true);
					return;
				}
				break;
			}
			default:
				switch (virtualKey)
				{
				case 107:
					if (base.IsEnabled)
					{
						RatingItem ratingItem3 = this.GetSelectedRatingItem();
						if (ratingItem3 != null)
						{
							ratingItem3 = this.GetRatingItemAtOffsetFrom(ratingItem3, 1);
						}
						else
						{
							ratingItem3 = Enumerable.FirstOrDefault<RatingItem>(this.GetRatingItems());
						}
						if (ratingItem3 != null)
						{
							ratingItem3.SelectValue();
							e.put_Handled(true);
							return;
						}
					}
					break;
				case 108:
					break;
				case 109:
					if (base.IsEnabled)
					{
						RatingItem ratingItem4 = this.GetSelectedRatingItem();
						if (ratingItem4 != null)
						{
							ratingItem4 = this.GetRatingItemAtOffsetFrom(ratingItem4, -1);
						}
						if (ratingItem4 != null)
						{
							ratingItem4.SelectValue();
							e.put_Handled(true);
						}
					}
					break;
				default:
					return;
				}
				break;
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00007E6C File Offset: 0x0000606C
		private RatingItem GetRatingItemAtOffsetFrom(RatingItem ratingItem, int offset)
		{
			IList<RatingItem> list = Enumerable.ToList<RatingItem>(this.GetRatingItems());
			int num = list.IndexOf(ratingItem);
			if (num == -1)
			{
				return null;
			}
			num += offset;
			if (num >= 0 && num < list.Count)
			{
				ratingItem = list[num];
			}
			else
			{
				ratingItem = null;
			}
			return ratingItem;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00007EB3 File Offset: 0x000060B3
		void IUpdateVisualState.UpdateVisualState(bool useTransitions)
		{
			this.Interaction.UpdateVisualStateBase(useTransitions);
		}

		// Token: 0x04000093 RID: 147
		protected static readonly DependencyProperty DisplayValueProperty = DependencyProperty.Register("DisplayValue", typeof(double), typeof(Rating), new PropertyMetadata(0.0, new PropertyChangedCallback(Rating.OnDisplayValueChanged)));

		// Token: 0x04000094 RID: 148
		public static readonly DependencyProperty ItemCountProperty = DependencyProperty.Register("ItemCount", typeof(int), typeof(Rating), new PropertyMetadata(0, new PropertyChangedCallback(Rating.OnItemCountChanged)));

		// Token: 0x04000095 RID: 149
		public static readonly DependencyProperty SelectionModeProperty = DependencyProperty.Register("SelectionMode", typeof(RatingSelectionMode), typeof(Rating), new PropertyMetadata(RatingSelectionMode.Continuous, new PropertyChangedCallback(Rating.OnSelectionModeChanged)));

		// Token: 0x04000096 RID: 150
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double), typeof(Rating), new PropertyMetadata(0.0, new PropertyChangedCallback(Rating.OnValueChanged)));

		// Token: 0x04000098 RID: 152
		public static readonly DependencyProperty PointerPressedFillProperty = DependencyProperty.Register("PointerPressedFill", typeof(SolidColorBrush), typeof(Rating), null);

		// Token: 0x04000099 RID: 153
		public static readonly DependencyProperty PointerOverFillProperty = DependencyProperty.Register("PointerOverFill", typeof(SolidColorBrush), typeof(Rating), null);

		// Token: 0x0400009A RID: 154
		public static readonly DependencyProperty ReadOnlyFillProperty = DependencyProperty.Register("ReadOnlyFill", typeof(SolidColorBrush), typeof(Rating), null);
	}
}
