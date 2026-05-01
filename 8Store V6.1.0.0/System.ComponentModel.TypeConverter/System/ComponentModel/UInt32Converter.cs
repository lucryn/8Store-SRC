using System;
using System.Globalization;

namespace System.ComponentModel
{
	// Token: 0x02000022 RID: 34
	public class UInt32Converter : BaseNumberConverter
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00004149 File Offset: 0x00002349
		internal override Type TargetType
		{
			get
			{
				return typeof(uint);
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004155 File Offset: 0x00002355
		internal override object FromString(string value, int radix)
		{
			return Convert.ToUInt32(value, radix);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004163 File Offset: 0x00002363
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return uint.Parse(value, 7, formatInfo);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004172 File Offset: 0x00002372
		internal override object FromString(string value, CultureInfo culture)
		{
			return uint.Parse(value, culture);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004180 File Offset: 0x00002380
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((uint)value).ToString("G", formatInfo);
		}
	}
}
