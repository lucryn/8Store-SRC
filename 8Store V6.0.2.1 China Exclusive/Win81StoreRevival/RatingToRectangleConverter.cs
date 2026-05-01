using System;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace Win81StoreRevival
{
	// Token: 0x0200003A RID: 58
	public sealed class RatingToRectangleConverter : IValueConverter
	{
		// Token: 0x06000393 RID: 915 RVA: 0x00015A50 File Offset: 0x00013C50
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			double num = 0.0;
			bool flag = value is double;
			if (flag)
			{
				num = (double)value;
			}
			else
			{
				bool flag2 = value != null;
				if (flag2)
				{
					double num2;
					bool flag3 = double.TryParse(value.ToString(), ref num2);
					if (flag3)
					{
						num = num2;
					}
				}
			}
			bool flag4 = double.IsNaN(num) || double.IsInfinity(num);
			if (flag4)
			{
				num = 0.0;
			}
			double num3 = 190.0;
			bool flag5 = parameter != null;
			if (flag5)
			{
				double num4;
				bool flag6 = double.TryParse(parameter.ToString(), ref num4) && num4 > 0.0;
				if (flag6)
				{
					num3 = num4;
				}
			}
			double num5 = Math.Min(num / 5.0 * num3, num3);
			bool flag7 = double.IsNaN(num5) || double.IsInfinity(num5) || num5 < 0.0;
			if (flag7)
			{
				num5 = 0.0;
			}
			return new Rect(0.0, 0.0, num5, 100.0);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00015B76 File Offset: 0x00013D76
		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new NotImplementedException();
		}
	}
}
