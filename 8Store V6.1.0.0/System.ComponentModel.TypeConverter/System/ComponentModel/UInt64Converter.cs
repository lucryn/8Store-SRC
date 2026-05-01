using System;
using System.Globalization;

namespace System.ComponentModel
{
	// Token: 0x02000023 RID: 35
	public class UInt64Converter : BaseNumberConverter
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x000041A1 File Offset: 0x000023A1
		internal override Type TargetType
		{
			get
			{
				return typeof(ulong);
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000041AD File Offset: 0x000023AD
		internal override object FromString(string value, int radix)
		{
			return Convert.ToUInt64(value, radix);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000041BB File Offset: 0x000023BB
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return ulong.Parse(value, 7, formatInfo);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000041CA File Offset: 0x000023CA
		internal override object FromString(string value, CultureInfo culture)
		{
			return ulong.Parse(value, culture);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000041D8 File Offset: 0x000023D8
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((ulong)value).ToString("G", formatInfo);
		}
	}
}
