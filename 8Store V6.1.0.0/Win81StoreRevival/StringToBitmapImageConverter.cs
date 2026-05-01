using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace Win81StoreRevival
{
	// Token: 0x02000045 RID: 69
	public sealed class StringToBitmapImageConverter : IValueConverter
	{
		// Token: 0x0600049B RID: 1179 RVA: 0x0001712C File Offset: 0x0001532C
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			if (value == null)
			{
				return null;
			}
			string text = value.ToString();
			if (string.IsNullOrWhiteSpace(text))
			{
				return null;
			}
			Uri uri;
			if (!Uri.TryCreate(text, 1, ref uri))
			{
				return null;
			}
			return new BitmapImage(uri);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00017162 File Offset: 0x00015362
		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			return null;
		}
	}
}
