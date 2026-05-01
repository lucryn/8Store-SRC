using System;
using System.Globalization;

namespace System.ComponentModel
{
	// Token: 0x0200000E RID: 14
	public class DateTimeOffsetConverter : TypeConverter
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00002BF8 File Offset: 0x00000DF8
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002F64 File Offset: 0x00001164
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return base.CanConvertTo(context, destinationType);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000310C File Offset: 0x0000130C
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text != null)
			{
				text = text.Trim();
				if (text.Length == 0)
				{
					return DateTimeOffset.MinValue;
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
						return DateTimeOffset.Parse(text, dateTimeFormatInfo);
					}
					return DateTimeOffset.Parse(text, culture);
				}
				catch (FormatException ex)
				{
					throw new FormatException(SR.Format(SR.ConvertInvalidPrimitive, (string)value, "DateTimeOffset"), ex);
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000031B4 File Offset: 0x000013B4
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType != typeof(string) || !(value is DateTimeOffset))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			DateTimeOffset dateTimeOffset = (DateTimeOffset)value;
			if (dateTimeOffset == DateTimeOffset.MinValue)
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
				if (dateTimeOffset.TimeOfDay.TotalSeconds == 0.0)
				{
					text = dateTimeFormatInfo.ShortDatePattern + " zzz";
				}
				else
				{
					text = dateTimeFormatInfo.ShortDatePattern + " " + dateTimeFormatInfo.ShortTimePattern + " zzz";
				}
				return dateTimeOffset.ToString(text, CultureInfo.CurrentCulture);
			}
			if (dateTimeOffset.TimeOfDay.TotalSeconds == 0.0)
			{
				return dateTimeOffset.ToString("yyyy-MM-dd zzz", culture);
			}
			return dateTimeOffset.ToString(culture);
		}
	}
}
