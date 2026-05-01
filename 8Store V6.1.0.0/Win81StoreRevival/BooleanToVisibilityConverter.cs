using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Win81StoreRevival
{
	// Token: 0x02000025 RID: 37
	public class BooleanToVisibilityConverter : IValueConverter
	{
		// Token: 0x06000225 RID: 549 RVA: 0x0000B105 File Offset: 0x00009305
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			return (value is bool && (bool)value) ? 0 : 1;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000B120 File Offset: 0x00009320
		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			return value is Visibility && (Visibility)value == 0;
		}
	}
}
