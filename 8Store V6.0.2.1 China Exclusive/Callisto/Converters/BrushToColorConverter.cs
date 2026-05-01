using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Callisto.Converters
{
	// Token: 0x02000025 RID: 37
	public class BrushToColorConverter : IValueConverter
	{
		// Token: 0x060001C6 RID: 454 RVA: 0x00009A54 File Offset: 0x00007C54
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			SolidColorBrush solidColorBrush = parameter as SolidColorBrush;
			if (solidColorBrush != null)
			{
				return solidColorBrush.Color;
			}
			return value;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00009A78 File Offset: 0x00007C78
		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			return value;
		}
	}
}
