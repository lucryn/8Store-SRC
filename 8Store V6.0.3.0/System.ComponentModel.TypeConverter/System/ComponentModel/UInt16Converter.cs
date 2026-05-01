using System;
using System.Globalization;

namespace System.ComponentModel
{
	// Token: 0x02000021 RID: 33
	public class UInt16Converter : BaseNumberConverter
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000EC RID: 236 RVA: 0x000040F1 File Offset: 0x000022F1
		internal override Type TargetType
		{
			get
			{
				return typeof(ushort);
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000040FD File Offset: 0x000022FD
		internal override object FromString(string value, int radix)
		{
			return Convert.ToUInt16(value, radix);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000410B File Offset: 0x0000230B
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return ushort.Parse(value, 7, formatInfo);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000411A File Offset: 0x0000231A
		internal override object FromString(string value, CultureInfo culture)
		{
			return ushort.Parse(value, culture);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004128 File Offset: 0x00002328
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((ushort)value).ToString("G", formatInfo);
		}
	}
}
