using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Win81StoreRevival
{
	// Token: 0x02000020 RID: 32
	public class BooleanToVisibilityConverter : IValueConverter
	{
		// Token: 0x060001B0 RID: 432 RVA: 0x0000A6FC File Offset: 0x000088FC
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			return (value is bool && (bool)value) ? 0 : 1;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000A728 File Offset: 0x00008928
		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			return value is Visibility && (Visibility)value == 0;
		}
	}
}
