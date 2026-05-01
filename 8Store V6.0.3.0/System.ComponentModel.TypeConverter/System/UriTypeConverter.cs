using System;
using System.ComponentModel;
using System.Globalization;

namespace System
{
	// Token: 0x02000003 RID: 3
	public class UriTypeConverter : TypeConverter
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == null)
			{
				throw new ArgumentNullException("sourceType");
			}
			return sourceType == typeof(string) || sourceType == typeof(Uri);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000207C File Offset: 0x0000027C
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || destinationType == typeof(Uri);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000209C File Offset: 0x0000029C
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text != null)
			{
				if (string.IsNullOrEmpty(text))
				{
					return null;
				}
				return new Uri(text, 0);
			}
			else
			{
				Uri uri = value as Uri;
				if (uri != null)
				{
					return new Uri(uri.OriginalString);
				}
				throw base.GetConvertFromException(value);
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020E8 File Offset: 0x000002E8
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			Uri uri = value as Uri;
			if (uri != null)
			{
				if (destinationType == typeof(string))
				{
					return uri.OriginalString;
				}
				if (destinationType == typeof(Uri))
				{
					return new Uri(uri.OriginalString, 0);
				}
			}
			throw base.GetConvertToException(value, destinationType);
		}
	}
}
