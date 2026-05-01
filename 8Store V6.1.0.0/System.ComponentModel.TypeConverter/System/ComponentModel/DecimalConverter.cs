using System;
using System.Globalization;

namespace System.ComponentModel
{
	// Token: 0x0200000F RID: 15
	public class DecimalConverter : BaseNumberConverter
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002172 File Offset: 0x00000372
		internal override bool AllowHex
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000032B4 File Offset: 0x000014B4
		internal override Type TargetType
		{
			get
			{
				return typeof(decimal);
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000032C0 File Offset: 0x000014C0
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000032CA File Offset: 0x000014CA
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000032E6 File Offset: 0x000014E6
		internal override object FromString(string value, int radix)
		{
			return Convert.ToDecimal(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000032F8 File Offset: 0x000014F8
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return decimal.Parse(value, 167, formatInfo);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000330B File Offset: 0x0000150B
		internal override object FromString(string value, CultureInfo culture)
		{
			return decimal.Parse(value, culture);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000331C File Offset: 0x0000151C
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((decimal)value).ToString("G", formatInfo);
		}
	}
}
