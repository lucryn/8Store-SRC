using System;
using Windows.UI.Xaml.Data;

namespace Callisto.Converters
{
	// Token: 0x02000029 RID: 41
	public class StringLengthToBooleanConverter : IValueConverter
	{
		// Token: 0x060001E1 RID: 481 RVA: 0x0000A067 File Offset: 0x00008267
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			return ((string)value).Length > 0;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000A07C File Offset: 0x0000827C
		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new NotImplementedException();
		}
	}
}
