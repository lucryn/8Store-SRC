using System;
using System.Globalization;

namespace System.ComponentModel
{
	// Token: 0x0200001A RID: 26
	public class SByteConverter : BaseNumberConverter
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00003BAA File Offset: 0x00001DAA
		internal override Type TargetType
		{
			get
			{
				return typeof(sbyte);
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003BB6 File Offset: 0x00001DB6
		internal override object FromString(string value, int radix)
		{
			return Convert.ToSByte(value, radix);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003BC4 File Offset: 0x00001DC4
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return sbyte.Parse(value, 7, formatInfo);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003BD3 File Offset: 0x00001DD3
		internal override object FromString(string value, CultureInfo culture)
		{
			return sbyte.Parse(value, culture);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003BE4 File Offset: 0x00001DE4
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((sbyte)value).ToString("G", formatInfo);
		}
	}
}
