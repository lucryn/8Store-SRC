using System;
using Windows.UI.Xaml.Data;

namespace Win81StoreRevival.Common
{
	// Token: 0x02000059 RID: 89
	public sealed class BooleanNegationConverter : IValueConverter
	{
		// Token: 0x060005FD RID: 1533 RVA: 0x0001CE30 File Offset: 0x0001B030
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			return !(value is bool) || !(bool)value;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0001CE30 File Offset: 0x0001B030
		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			return !(value is bool) || !(bool)value;
		}
	}
}
