using System;
using System.Globalization;

namespace System.ComponentModel
{
	// Token: 0x0200001C RID: 28
	public class StringConverter : TypeConverter
	{
		// Token: 0x060000B2 RID: 178 RVA: 0x00002BF8 File Offset: 0x00000DF8
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003C65 File Offset: 0x00001E65
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				return (string)value;
			}
			if (value == null)
			{
				return string.Empty;
			}
			return base.ConvertFrom(context, culture, value);
		}
	}
}
