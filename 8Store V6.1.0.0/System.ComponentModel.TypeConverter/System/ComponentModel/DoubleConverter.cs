using System;
using System.Globalization;

namespace System.ComponentModel
{
	// Token: 0x02000010 RID: 16
	public class DoubleConverter : BaseNumberConverter
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002172 File Offset: 0x00000372
		internal override bool AllowHex
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000068 RID: 104 RVA: 0x0000333D File Offset: 0x0000153D
		internal override Type TargetType
		{
			get
			{
				return typeof(double);
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003349 File Offset: 0x00001549
		internal override object FromString(string value, int radix)
		{
			return Convert.ToDouble(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000335B File Offset: 0x0000155B
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return double.Parse(value, 167, formatInfo);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000336E File Offset: 0x0000156E
		internal override object FromString(string value, CultureInfo culture)
		{
			return double.Parse(value, culture);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000337C File Offset: 0x0000157C
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((double)value).ToString("R", formatInfo);
		}
	}
}
