using System;
using System.Collections;
using System.Globalization;

namespace System.ComponentModel
{
	// Token: 0x0200000C RID: 12
	public class CollectionConverter : TypeConverter
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00002F2C File Offset: 0x0000112C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string) && value is ICollection)
			{
				return SR.Collection;
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002172 File Offset: 0x00000372
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return false;
		}
	}
}
