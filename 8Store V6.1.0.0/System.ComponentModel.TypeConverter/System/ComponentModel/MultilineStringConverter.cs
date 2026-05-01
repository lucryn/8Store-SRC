using System;
using System.Globalization;

namespace System.ComponentModel
{
	// Token: 0x02000017 RID: 23
	public class MultilineStringConverter : TypeConverter
	{
		// Token: 0x06000092 RID: 146 RVA: 0x0000385D File Offset: 0x00001A5D
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string) && value is string)
			{
				return SR.Text;
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00002172 File Offset: 0x00000372
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return false;
		}
	}
}
