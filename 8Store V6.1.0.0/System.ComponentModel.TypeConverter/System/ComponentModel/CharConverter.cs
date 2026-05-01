using System;
using System.Globalization;

namespace System.ComponentModel
{
	// Token: 0x0200000B RID: 11
	public class CharConverter : TypeConverter
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00002BF8 File Offset: 0x00000DF8
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002E8D File Offset: 0x0000108D
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string) && value is char && (char)value == '\0')
			{
				return "";
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002EC0 File Offset: 0x000010C0
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text == null)
			{
				return base.ConvertFrom(context, culture, value);
			}
			if (text.Length > 1)
			{
				text = text.Trim();
			}
			if (text.Length <= 0)
			{
				return '\0';
			}
			if (text.Length != 1)
			{
				throw new FormatException(SR.Format(SR.ConvertInvalidPrimitive, text, "Char"));
			}
			return text.get_Chars(0);
		}
	}
}
