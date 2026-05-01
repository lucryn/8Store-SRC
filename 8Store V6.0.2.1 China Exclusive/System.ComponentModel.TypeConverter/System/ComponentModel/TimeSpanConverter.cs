using System;
using System.Globalization;

namespace System.ComponentModel
{
	// Token: 0x0200001D RID: 29
	public class TimeSpanConverter : TypeConverter
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x00002BF8 File Offset: 0x00000DF8
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00002F64 File Offset: 0x00001164
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return base.CanConvertTo(context, destinationType);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003C88 File Offset: 0x00001E88
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text != null)
			{
				text = text.Trim();
				try
				{
					return TimeSpan.Parse(text, culture);
				}
				catch (FormatException ex)
				{
					throw new FormatException(SR.Format(SR.ConvertInvalidPrimitive, (string)value, "TimeSpan"), ex);
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003737 File Offset: 0x00001937
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
