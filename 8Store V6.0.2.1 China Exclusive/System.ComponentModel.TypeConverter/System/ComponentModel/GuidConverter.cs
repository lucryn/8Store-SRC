using System;
using System.Globalization;

namespace System.ComponentModel
{
	// Token: 0x02000012 RID: 18
	public class GuidConverter : TypeConverter
	{
		// Token: 0x06000076 RID: 118 RVA: 0x00002BF8 File Offset: 0x00000DF8
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002F64 File Offset: 0x00001164
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003704 File Offset: 0x00001904
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text != null)
			{
				text = text.Trim();
				return new Guid(text);
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003737 File Offset: 0x00001937
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
