using System;
using System.Globalization;

namespace System.ComponentModel
{
	// Token: 0x0200000D RID: 13
	public class DateTimeConverter : TypeConverter
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00002BF8 File Offset: 0x00000DF8
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002F64 File Offset: 0x00001164
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002F70 File Offset: 0x00001170
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text != null)
			{
				text = text.Trim();
				if (text.Length == 0)
				{
					return DateTime.MinValue;
				}
				try
				{
					DateTimeFormatInfo dateTimeFormatInfo = null;
					if (culture != null)
					{
						dateTimeFormatInfo = (DateTimeFormatInfo)culture.GetFormat(typeof(DateTimeFormatInfo));
					}
					if (dateTimeFormatInfo != null)
					{
						return DateTime.Parse(text, dateTimeFormatInfo);
					}
					return DateTime.Parse(text, culture);
				}
				catch (FormatException ex)
				{
					throw new FormatException(SR.Format(SR.ConvertInvalidPrimitive, (string)value, "DateTime"), ex);
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003018 File Offset: 0x00001218
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType != typeof(string) || !(value is DateTime))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			DateTime dateTime = (DateTime)value;
			if (dateTime == DateTime.MinValue)
			{
				return string.Empty;
			}
			if (culture == null)
			{
				culture = CultureInfo.CurrentCulture;
			}
			DateTimeFormatInfo dateTimeFormatInfo = (DateTimeFormatInfo)culture.GetFormat(typeof(DateTimeFormatInfo));
			if (culture != CultureInfo.InvariantCulture)
			{
				string text;
				if (dateTime.TimeOfDay.TotalSeconds == 0.0)
				{
					text = dateTimeFormatInfo.ShortDatePattern;
				}
				else
				{
					text = dateTimeFormatInfo.ShortDatePattern + " " + dateTimeFormatInfo.ShortTimePattern;
				}
				return dateTime.ToString(text, CultureInfo.CurrentCulture);
			}
			if (dateTime.TimeOfDay.TotalSeconds == 0.0)
			{
				return dateTime.ToString("yyyy-MM-dd", culture);
			}
			return dateTime.ToString(culture);
		}
	}
}
