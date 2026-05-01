using System;
using System.Globalization;

namespace System.ComponentModel
{
	// Token: 0x02000007 RID: 7
	public class ArrayConverter : CollectionConverter
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00002B7C File Offset: 0x00000D7C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string) && value is Array)
			{
				return SR.Format(SR.Array, value.GetType().Name);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002BCF File Offset: 0x00000DCF
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
