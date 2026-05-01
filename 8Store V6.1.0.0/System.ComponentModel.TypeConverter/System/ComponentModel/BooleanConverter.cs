using System;
using System.Globalization;

namespace System.ComponentModel
{
	// Token: 0x02000009 RID: 9
	public class BooleanConverter : TypeConverter
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00002BF8 File Offset: 0x00000DF8
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002D90 File Offset: 0x00000F90
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text != null)
			{
				text = text.Trim();
				try
				{
					return bool.Parse(text);
				}
				catch (FormatException ex)
				{
					throw new FormatException(SR.Format(SR.ConvertInvalidPrimitive, (string)value, "Boolean"), ex);
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002DF4 File Offset: 0x00000FF4
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (BooleanConverter.s_values == null)
			{
				BooleanConverter.s_values = new TypeConverter.StandardValuesCollection(new object[]
				{
					true,
					false
				});
			}
			return BooleanConverter.s_values;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002BCF File Offset: 0x00000DCF
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002BCF File Offset: 0x00000DCF
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x04000006 RID: 6
		private static volatile TypeConverter.StandardValuesCollection s_values;
	}
}
