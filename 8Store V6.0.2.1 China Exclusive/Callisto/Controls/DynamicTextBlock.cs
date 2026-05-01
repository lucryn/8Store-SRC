using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Callisto.Controls
{
	// Token: 0x02000009 RID: 9
	[Obsolete("Windows 8.1 now provides this functionality in the XAML framework itself as CharacterEllipsis.")]
	public class DynamicTextBlock : ContentControl
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002DA0 File Offset: 0x00000FA0
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002DB2 File Offset: 0x00000FB2
		public string Text
		{
			get
			{
				return (string)base.GetValue(DynamicTextBlock.TextProperty);
			}
			set
			{
				base.SetValue(DynamicTextBlock.TextProperty, value);
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002DC0 File Offset: 0x00000FC0
		private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((DynamicTextBlock)d).OnTextChanged(e);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002DCE File Offset: 0x00000FCE
		protected virtual void OnTextChanged(DependencyPropertyChangedEventArgs e)
		{
			base.InvalidateMeasure();
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002DD6 File Offset: 0x00000FD6
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00002DE8 File Offset: 0x00000FE8
		public TextWrapping TextWrapping
		{
			get
			{
				return (TextWrapping)base.GetValue(DynamicTextBlock.TextWrappingProperty);
			}
			set
			{
				base.SetValue(DynamicTextBlock.TextWrappingProperty, value);
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002DFB File Offset: 0x00000FFB
		private static void OnTextWrappingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((DynamicTextBlock)d).OnTextWrappingChanged(e);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002E09 File Offset: 0x00001009
		protected virtual void OnTextWrappingChanged(DependencyPropertyChangedEventArgs e)
		{
			this.textBlock.put_TextWrapping((TextWrapping)e.NewValue);
			base.InvalidateMeasure();
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002E27 File Offset: 0x00001027
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002E39 File Offset: 0x00001039
		public double LineHeight
		{
			get
			{
				return (double)base.GetValue(DynamicTextBlock.LineHeightProperty);
			}
			set
			{
				base.SetValue(DynamicTextBlock.LineHeightProperty, value);
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002E4C File Offset: 0x0000104C
		private static void OnLineHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((DynamicTextBlock)d).OnLineHeightChanged(e);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002E5A File Offset: 0x0000105A
		protected virtual void OnLineHeightChanged(DependencyPropertyChangedEventArgs e)
		{
			this.textBlock.put_LineHeight(this.LineHeight);
			base.InvalidateMeasure();
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002E73 File Offset: 0x00001073
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00002E85 File Offset: 0x00001085
		public LineStackingStrategy LineStackingStrategy
		{
			get
			{
				return (LineStackingStrategy)base.GetValue(DynamicTextBlock.LineStackingStrategyProperty);
			}
			set
			{
				base.SetValue(DynamicTextBlock.LineStackingStrategyProperty, value);
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002E98 File Offset: 0x00001098
		private static void OnLineStackingStrategyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((DynamicTextBlock)d).OnLineStackingStrategyChanged(e);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002EA6 File Offset: 0x000010A6
		protected virtual void OnLineStackingStrategyChanged(DependencyPropertyChangedEventArgs e)
		{
			this.textBlock.put_LineStackingStrategy((LineStackingStrategy)e.NewValue);
			base.InvalidateMeasure();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002EC4 File Offset: 0x000010C4
		public DynamicTextBlock()
		{
			this.textBlock = new TextBlock();
			base.put_Content(this.textBlock);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002EE4 File Offset: 0x000010E4
		protected override Size MeasureOverride(Size availableSize)
		{
			bool flag = this.TextWrapping == 2;
			Size size = flag ? new Size(availableSize.Width, double.PositiveInfinity) : new Size(double.PositiveInfinity, availableSize.Height);
			string text = this.Text;
			if (string.IsNullOrEmpty(text))
			{
				text = string.Empty;
			}
			this.textBlock.put_Text(text);
			Size size2 = base.MeasureOverride(size);
			while (flag ? (size2.Height > availableSize.Height) : (size2.Width > availableSize.Width))
			{
				int length = text.Length;
				if (text.Length > 0)
				{
					text = this.ReduceText(text);
				}
				if (text.Length == length)
				{
					break;
				}
				this.textBlock.put_Text(text + "...");
				size2 = base.MeasureOverride(size);
			}
			return base.MeasureOverride(availableSize);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002FC5 File Offset: 0x000011C5
		protected virtual string ReduceText(string text)
		{
			return text.Substring(0, text.Length - 1);
		}

		// Token: 0x0400002F RID: 47
		public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(DynamicTextBlock), new PropertyMetadata(string.Empty, new PropertyChangedCallback(DynamicTextBlock.OnTextChanged)));

		// Token: 0x04000030 RID: 48
		public static readonly DependencyProperty TextWrappingProperty = DependencyProperty.Register("TextWrapping", typeof(TextWrapping), typeof(DynamicTextBlock), new PropertyMetadata(1, new PropertyChangedCallback(DynamicTextBlock.OnTextWrappingChanged)));

		// Token: 0x04000031 RID: 49
		public static readonly DependencyProperty LineHeightProperty = DependencyProperty.Register("LineHeight", typeof(double), typeof(DynamicTextBlock), new PropertyMetadata(0.0, new PropertyChangedCallback(DynamicTextBlock.OnLineHeightChanged)));

		// Token: 0x04000032 RID: 50
		public static readonly DependencyProperty LineStackingStrategyProperty = DependencyProperty.Register("LineStackingStrategy", typeof(LineStackingStrategy), typeof(DynamicTextBlock), new PropertyMetadata(1, new PropertyChangedCallback(DynamicTextBlock.OnLineStackingStrategyChanged)));

		// Token: 0x04000033 RID: 51
		private TextBlock textBlock;
	}
}
