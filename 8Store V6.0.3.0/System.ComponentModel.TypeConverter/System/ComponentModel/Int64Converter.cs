using System;
using System.Globalization;

namespace System.ComponentModel
{
	// Token: 0x02000015 RID: 21
	public class Int64Converter : BaseNumberConverter
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003805 File Offset: 0x00001A05
		internal override Type TargetType
		{
			get
			{
				return typeof(long);
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003811 File Offset: 0x00001A11
		internal override object FromString(string value, int radix)
		{
			return Convert.ToInt64(value, radix);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000381F File Offset: 0x00001A1F
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return long.Parse(value, 7, formatInfo);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000382E File Offset: 0x00001A2E
		internal override object FromString(string value, CultureInfo culture)
		{
			return long.Parse(value, culture);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000383C File Offset: 0x00001A3C
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((long)value).ToString("G", formatInfo);
		}
	}
}
