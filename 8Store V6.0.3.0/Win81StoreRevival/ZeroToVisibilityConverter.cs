using System;
using Windows.UI.Xaml.Data;

namespace Win81StoreRevival
{
	// Token: 0x02000043 RID: 67
	public class ZeroToVisibilityConverter : IValueConverter
	{
		// Token: 0x060003D9 RID: 985 RVA: 0x000176E4 File Offset: 0x000158E4
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			bool flag = value is int;
			object result;
			if (flag)
			{
				int num = (int)value;
				result = ((num == 0) ? 0 : 1);
			}
			else
			{
				bool flag2 = value is double;
				if (flag2)
				{
					double num2 = (double)value;
					result = ((num2 == 0.0) ? 0 : 1);
				}
				else
				{
					result = 1;
				}
			}
			return result;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00017750 File Offset: 0x00015950
		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			return 0;
		}
	}
}
