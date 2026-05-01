using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Callisto.Converters
{
	// Token: 0x02000027 RID: 39
	public class ColorContrastConverter : IValueConverter
	{
		// Token: 0x060001CC RID: 460 RVA: 0x00009B50 File Offset: 0x00007D50
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			SolidColorBrush solidColorBrush = (SolidColorBrush)value;
			int num = ((int)solidColorBrush.Color.R * 299 + (int)solidColorBrush.Color.G * 587 + (int)(solidColorBrush.Color.B * 114)) / 1000;
			Color color = (parameter != null && System.Convert.ToBoolean(parameter)) ? ((num >= 128) ? Colors.White : Colors.Black) : ((num >= 128) ? Colors.Black : Colors.White);
			return new SolidColorBrush(color);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00009BEA File Offset: 0x00007DEA
		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			return value;
		}
	}
}
