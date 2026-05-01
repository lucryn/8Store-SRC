using System;
using System.Globalization;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Callisto.Converters
{
	// Token: 0x02000026 RID: 38
	public class ColorBrightnessConverter : IValueConverter
	{
		// Token: 0x060001C9 RID: 457 RVA: 0x00009A84 File Offset: 0x00007C84
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			double num = System.Convert.ToDouble(parameter, CultureInfo.InvariantCulture);
			SolidColorBrush solidColorBrush = (SolidColorBrush)value;
			Color color = default(Color);
			color.A = solidColorBrush.Color.A;
			color.B = System.Convert.ToByte((double)solidColorBrush.Color.B * num, CultureInfo.InvariantCulture);
			color.G = System.Convert.ToByte((double)solidColorBrush.Color.G * num, CultureInfo.InvariantCulture);
			color.R = System.Convert.ToByte((double)solidColorBrush.Color.R * num, CultureInfo.InvariantCulture);
			Color color2 = color;
			return new SolidColorBrush(color2);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00009B42 File Offset: 0x00007D42
		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			return value;
		}
	}
}
