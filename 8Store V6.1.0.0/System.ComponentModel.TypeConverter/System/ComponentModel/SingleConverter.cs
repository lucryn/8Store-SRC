using System;
using System.Globalization;

namespace System.ComponentModel
{
	// Token: 0x0200001B RID: 27
	public class SingleConverter : BaseNumberConverter
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00002172 File Offset: 0x00000372
		internal override bool AllowHex
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00003C05 File Offset: 0x00001E05
		internal override Type TargetType
		{
			get
			{
				return typeof(float);
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003C11 File Offset: 0x00001E11
		internal override object FromString(string value, int radix)
		{
			return Convert.ToSingle(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003C23 File Offset: 0x00001E23
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return float.Parse(value, 167, formatInfo);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003C36 File Offset: 0x00001E36
		internal override object FromString(string value, CultureInfo culture)
		{
			return float.Parse(value, culture);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003C44 File Offset: 0x00001E44
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((float)value).ToString("R", formatInfo);
		}
	}
}
