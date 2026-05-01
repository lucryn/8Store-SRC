using System;
using System.Globalization;

namespace System.ComponentModel
{
	// Token: 0x02000014 RID: 20
	public class Int32Converter : BaseNumberConverter
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000081 RID: 129 RVA: 0x000037AD File Offset: 0x000019AD
		internal override Type TargetType
		{
			get
			{
				return typeof(int);
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000037B9 File Offset: 0x000019B9
		internal override object FromString(string value, int radix)
		{
			return Convert.ToInt32(value, radix);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000037C7 File Offset: 0x000019C7
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return int.Parse(value, 7, formatInfo);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000037D6 File Offset: 0x000019D6
		internal override object FromString(string value, CultureInfo culture)
		{
			return int.Parse(value, culture);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000037E4 File Offset: 0x000019E4
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((int)value).ToString("G", formatInfo);
		}
	}
}
