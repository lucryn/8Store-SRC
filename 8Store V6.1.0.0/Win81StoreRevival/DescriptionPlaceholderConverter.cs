using System;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Win81StoreRevival
{
	// Token: 0x02000023 RID: 35
	public sealed class DescriptionPlaceholderConverter : IValueConverter
	{
		// Token: 0x060001F9 RID: 505 RVA: 0x00009F70 File Offset: 0x00008170
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			string text = value as string;
			if (string.IsNullOrWhiteSpace(text))
			{
				return ResourceLoader.GetForCurrentView().GetString("DescriptionMissing");
			}
			return text;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00009F9D File Offset: 0x0000819D
		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			return DependencyProperty.UnsetValue;
		}
	}
}
