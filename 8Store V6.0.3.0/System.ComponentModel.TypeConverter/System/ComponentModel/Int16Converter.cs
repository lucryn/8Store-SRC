using System;
using System.Globalization;

namespace System.ComponentModel
{
	// Token: 0x02000013 RID: 19
	public class Int16Converter : BaseNumberConverter
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00003753 File Offset: 0x00001953
		internal override Type TargetType
		{
			get
			{
				return typeof(short);
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000375F File Offset: 0x0000195F
		internal override object FromString(string value, int radix)
		{
			return Convert.ToInt16(value, radix);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000376D File Offset: 0x0000196D
		internal override object FromString(string value, CultureInfo culture)
		{
			return short.Parse(value, culture);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000377B File Offset: 0x0000197B
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return short.Parse(value, 7, formatInfo);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000378C File Offset: 0x0000198C
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((short)value).ToString("G", formatInfo);
		}
	}
}
