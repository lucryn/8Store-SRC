using System;
using System.Globalization;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Win81StoreRevival.Common
{
	// Token: 0x0200005B RID: 91
	public class ColorConverter : IValueConverter
	{
		// Token: 0x06000603 RID: 1539 RVA: 0x0001CE4C File Offset: 0x0001B04C
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			Color color;
			if (value is SolidColorBrush)
			{
				color = ((SolidColorBrush)value).Color;
			}
			else
			{
				if (!(value is Color))
				{
					return value;
				}
				color = (Color)value;
			}
			double num = 1.0;
			if (parameter != null)
			{
				double.TryParse(parameter.ToString(), 511, CultureInfo.InvariantCulture, ref num);
			}
			byte b = (byte)Math.Max(0.0, Math.Min(255.0, (double)color.R * num));
			byte b2 = (byte)Math.Max(0.0, Math.Min(255.0, (double)color.G * num));
			byte b3 = (byte)Math.Max(0.0, Math.Min(255.0, (double)color.B * num));
			return new SolidColorBrush(Color.FromArgb(color.A, b, b2, b3));
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00009F9D File Offset: 0x0000819D
		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			return DependencyProperty.UnsetValue;
		}
	}
}
