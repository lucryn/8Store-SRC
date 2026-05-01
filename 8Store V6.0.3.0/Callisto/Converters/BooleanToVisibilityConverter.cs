using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Callisto.Converters
{
	// Token: 0x02000024 RID: 36
	public class BooleanToVisibilityConverter : IValueConverter
	{
		// Token: 0x060001C3 RID: 451 RVA: 0x00009A13 File Offset: 0x00007C13
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			return (value is bool && (bool)value) ? 0 : 1;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00009A2E File Offset: 0x00007C2E
		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			return value is Visibility && (Visibility)value == 0;
		}
	}
}
