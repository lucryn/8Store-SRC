using System;
using System.Globalization;

namespace System.ComponentModel
{
	// Token: 0x0200000A RID: 10
	public class ByteConverter : BaseNumberConverter
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002E2A File Offset: 0x0000102A
		internal override Type TargetType
		{
			get
			{
				return typeof(byte);
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002E36 File Offset: 0x00001036
		internal override object FromString(string value, int radix)
		{
			return Convert.ToByte(value, radix);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002E44 File Offset: 0x00001044
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return byte.Parse(value, 7, formatInfo);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002E53 File Offset: 0x00001053
		internal override object FromString(string value, CultureInfo culture)
		{
			return byte.Parse(value, culture);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002E64 File Offset: 0x00001064
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((byte)value).ToString("G", formatInfo);
		}
	}
}
