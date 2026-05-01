using System;
using System.Globalization;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Callisto.Controls
{
	// Token: 0x02000023 RID: 35
	public class WrapPanel : Panel
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x000092E1 File Offset: 0x000074E1
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x000092F3 File Offset: 0x000074F3
		public double ItemHeight
		{
			get
			{
				return (double)base.GetValue(WrapPanel.ItemHeightProperty);
			}
			set
			{
				base.SetValue(WrapPanel.ItemHeightProperty, value);
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00009306 File Offset: 0x00007506
		// (set) Token: 0x060001B9 RID: 441 RVA: 0x00009318 File Offset: 0x00007518
		public double ItemWidth
		{
			get
			{
				return (double)base.GetValue(WrapPanel.ItemWidthProperty);
			}
			set
			{
				base.SetValue(WrapPanel.ItemWidthProperty, value);
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001BA RID: 442 RVA: 0x0000932B File Offset: 0x0000752B
		// (set) Token: 0x060001BB RID: 443 RVA: 0x0000933D File Offset: 0x0000753D
		public Orientation Orientation
		{
			get
			{
				return (Orientation)base.GetValue(WrapPanel.OrientationProperty);
			}
			set
			{
				base.SetValue(WrapPanel.OrientationProperty, value);
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00009350 File Offset: 0x00007550
		private static void OnOrientationPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			WrapPanel wrapPanel = (WrapPanel)d;
			Orientation orientation = (Orientation)e.NewValue;
			if (wrapPanel._ignorePropertyChange)
			{
				wrapPanel._ignorePropertyChange = false;
				return;
			}
			if (orientation != 1 && orientation != null)
			{
				wrapPanel._ignorePropertyChange = true;
				wrapPanel.SetValue(WrapPanel.OrientationProperty, (Orientation)e.OldValue);
				string text = string.Format(CultureInfo.InvariantCulture, "Invalid Orientation value '{0}'.", new object[]
				{
					orientation
				});
				throw new ArgumentException(text, "value");
			}
			wrapPanel.InvalidateMeasure();
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000093DC File Offset: 0x000075DC
		private static void OnItemHeightOrWidthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			WrapPanel wrapPanel = (WrapPanel)d;
			double num = (double)e.NewValue;
			if (wrapPanel._ignorePropertyChange)
			{
				wrapPanel._ignorePropertyChange = false;
				return;
			}
			if (!num.IsNaN() && (num <= 0.0 || double.IsPositiveInfinity(num)))
			{
				wrapPanel._ignorePropertyChange = true;
				wrapPanel.SetValue(e.Property, (double)e.OldValue);
				string text = string.Format(CultureInfo.InvariantCulture, "Invalid length value '{0}'.", new object[]
				{
					num
				});
				throw new ArgumentException(text, "value");
			}
			wrapPanel.InvalidateMeasure();
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00009480 File Offset: 0x00007680
		protected override Size MeasureOverride(Size constraint)
		{
			Orientation orientation = this.Orientation;
			OrientedSize orientedSize = new OrientedSize(orientation);
			OrientedSize orientedSize2 = new OrientedSize(orientation);
			OrientedSize orientedSize3 = new OrientedSize(orientation, constraint.Width, constraint.Height);
			double itemWidth = this.ItemWidth;
			double itemHeight = this.ItemHeight;
			bool flag = !itemWidth.IsNaN();
			bool flag2 = !itemHeight.IsNaN();
			Size size;
			size..ctor(flag ? itemWidth : constraint.Width, flag2 ? itemHeight : constraint.Height);
			foreach (UIElement uielement in base.Children)
			{
				uielement.Measure(size);
				OrientedSize orientedSize4 = new OrientedSize(orientation, flag ? itemWidth : uielement.DesiredSize.Width, flag2 ? itemHeight : uielement.DesiredSize.Height);
				if (NumericExtensions.IsGreaterThan(orientedSize.Direct + orientedSize4.Direct, orientedSize3.Direct))
				{
					orientedSize2.Direct = Math.Max(orientedSize.Direct, orientedSize2.Direct);
					orientedSize2.Indirect += orientedSize.Indirect;
					orientedSize = orientedSize4;
					if (NumericExtensions.IsGreaterThan(orientedSize4.Direct, orientedSize3.Direct))
					{
						orientedSize2.Direct = Math.Max(orientedSize4.Direct, orientedSize2.Direct);
						orientedSize2.Indirect += orientedSize4.Indirect;
						orientedSize = new OrientedSize(orientation);
					}
				}
				else
				{
					orientedSize.Direct += orientedSize4.Direct;
					orientedSize.Indirect = Math.Max(orientedSize.Indirect, orientedSize4.Indirect);
				}
			}
			orientedSize2.Direct = Math.Max(orientedSize.Direct, orientedSize2.Direct);
			orientedSize2.Indirect += orientedSize.Indirect;
			return new Size(orientedSize2.Width, orientedSize2.Height);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000096A8 File Offset: 0x000078A8
		protected override Size ArrangeOverride(Size finalSize)
		{
			Orientation orientation = this.Orientation;
			OrientedSize orientedSize = new OrientedSize(orientation);
			OrientedSize orientedSize2 = new OrientedSize(orientation, finalSize.Width, finalSize.Height);
			double itemWidth = this.ItemWidth;
			double itemHeight = this.ItemHeight;
			bool flag = !itemWidth.IsNaN();
			bool flag2 = !itemHeight.IsNaN();
			double num = 0.0;
			double? directDelta = (orientation == 1) ? (flag ? new double?(itemWidth) : default(double?)) : (flag2 ? new double?(itemHeight) : default(double?));
			UIElementCollection children = base.Children;
			int count = children.Count;
			int num2 = 0;
			for (int i = 0; i < count; i++)
			{
				UIElement uielement = children[i];
				OrientedSize orientedSize3 = new OrientedSize(orientation, flag ? itemWidth : uielement.DesiredSize.Width, flag2 ? itemHeight : uielement.DesiredSize.Height);
				if (NumericExtensions.IsGreaterThan(orientedSize.Direct + orientedSize3.Direct, orientedSize2.Direct))
				{
					this.ArrangeLine(num2, i, directDelta, num, orientedSize.Indirect);
					num += orientedSize.Indirect;
					orientedSize = orientedSize3;
					if (NumericExtensions.IsGreaterThan(orientedSize3.Direct, orientedSize2.Direct))
					{
						this.ArrangeLine(i, ++i, directDelta, num, orientedSize3.Indirect);
						num += orientedSize.Indirect;
						orientedSize = new OrientedSize(orientation);
					}
					num2 = i;
				}
				else
				{
					orientedSize.Direct += orientedSize3.Direct;
					orientedSize.Indirect = Math.Max(orientedSize.Indirect, orientedSize3.Indirect);
				}
			}
			if (num2 < count)
			{
				this.ArrangeLine(num2, count, directDelta, num, orientedSize.Indirect);
			}
			return finalSize;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00009884 File Offset: 0x00007A84
		private void ArrangeLine(int lineStart, int lineEnd, double? directDelta, double indirectOffset, double indirectGrowth)
		{
			double num = 0.0;
			Orientation orientation = this.Orientation;
			bool flag = orientation == 1;
			UIElementCollection children = base.Children;
			for (int i = lineStart; i < lineEnd; i++)
			{
				UIElement uielement = children[i];
				OrientedSize orientedSize = new OrientedSize(orientation, uielement.DesiredSize.Width, uielement.DesiredSize.Height);
				double num2 = (directDelta != null) ? directDelta.Value : orientedSize.Direct;
				Rect rect = flag ? new Rect(num, indirectOffset, num2, indirectGrowth) : new Rect(indirectOffset, num, indirectGrowth, num2);
				uielement.Arrange(rect);
				num += num2;
			}
		}

		// Token: 0x040000DD RID: 221
		private bool _ignorePropertyChange;

		// Token: 0x040000DE RID: 222
		public static readonly DependencyProperty ItemHeightProperty = DependencyProperty.Register("ItemHeight", typeof(double), typeof(WrapPanel), new PropertyMetadata(double.NaN, new PropertyChangedCallback(WrapPanel.OnItemHeightOrWidthPropertyChanged)));

		// Token: 0x040000DF RID: 223
		public static readonly DependencyProperty ItemWidthProperty = DependencyProperty.Register("ItemWidth", typeof(double), typeof(WrapPanel), new PropertyMetadata(double.NaN, new PropertyChangedCallback(WrapPanel.OnItemHeightOrWidthPropertyChanged)));

		// Token: 0x040000E0 RID: 224
		public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(Orientation), typeof(WrapPanel), new PropertyMetadata(1, new PropertyChangedCallback(WrapPanel.OnOrientationPropertyChanged)));
	}
}
