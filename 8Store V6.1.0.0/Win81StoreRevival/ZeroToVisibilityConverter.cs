using System;
using Windows.UI.Xaml.Data;

namespace Win81StoreRevival
{
	// Token: 0x02000048 RID: 72
	public class ZeroToVisibilityConverter : IValueConverter
	{
		// Token: 0x060004B3 RID: 1203 RVA: 0x00017B94 File Offset: 0x00015D94
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			bool flag = false;
			if (value is int)
			{
				flag = ((int)value == 0);
			}
			else if (value is double)
			{
				flag = ((double)value == 0.0);
			}
			bool flag2 = flag;
			if (parameter != null && parameter.ToString() == "Inverse")
			{
				flag2 = !flag;
			}
			return flag2 ? 0 : 1;
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00017BF9 File Offset: 0x00015DF9
		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			return 0;
		}
	}
}
