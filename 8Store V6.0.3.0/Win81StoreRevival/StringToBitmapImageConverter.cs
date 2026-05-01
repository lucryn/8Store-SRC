using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace Win81StoreRevival
{
	// Token: 0x0200003F RID: 63
	public sealed class StringToBitmapImageConverter : IValueConverter
	{
		// Token: 0x060003B7 RID: 951 RVA: 0x000165A8 File Offset: 0x000147A8
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			bool flag = value == null;
			object result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string text = value.ToString();
				bool flag2 = string.IsNullOrWhiteSpace(text);
				if (flag2)
				{
					result = null;
				}
				else
				{
					Uri uri;
					bool flag3 = !Uri.TryCreate(text, 1, ref uri);
					if (flag3)
					{
						result = null;
					}
					else
					{
						result = new BitmapImage(uri);
					}
				}
			}
			return result;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x000165FC File Offset: 0x000147FC
		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			return null;
		}
	}
}
