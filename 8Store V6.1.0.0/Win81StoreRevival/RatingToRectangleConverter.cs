using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Win81StoreRevival
{
	// Token: 0x0200003F RID: 63
	public sealed class RatingToRectangleConverter : IValueConverter
	{
		// Token: 0x06000457 RID: 1111 RVA: 0x00015C54 File Offset: 0x00013E54
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			double num = (value is double) ? ((double)value) : 0.0;
			double num2 = 42.0;
			if (parameter != null)
			{
				double.TryParse(parameter.ToString(), ref num2);
			}
			return new Rect(0.0, 0.0, num2 * num, 200.0);
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00009F9D File Offset: 0x0000819D
		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			return DependencyProperty.UnsetValue;
		}
	}
}
