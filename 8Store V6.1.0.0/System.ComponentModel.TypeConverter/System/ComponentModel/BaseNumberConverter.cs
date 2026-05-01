using System;
using System.Globalization;
using System.Reflection;

namespace System.ComponentModel
{
	// Token: 0x02000008 RID: 8
	public abstract class BaseNumberConverter : TypeConverter
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002BCF File Offset: 0x00000DCF
		internal virtual bool AllowHex
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000036 RID: 54
		internal abstract Type TargetType { get; }

		// Token: 0x06000037 RID: 55
		internal abstract object FromString(string value, int radix);

		// Token: 0x06000038 RID: 56
		internal abstract object FromString(string value, NumberFormatInfo formatInfo);

		// Token: 0x06000039 RID: 57
		internal abstract object FromString(string value, CultureInfo culture);

		// Token: 0x0600003A RID: 58 RVA: 0x00002BDA File Offset: 0x00000DDA
		internal virtual Exception FromStringError(string failedText, Exception innerException)
		{
			return new Exception(SR.Format(SR.ConvertInvalidPrimitive, failedText, this.TargetType.Name), innerException);
		}

		// Token: 0x0600003B RID: 59
		internal abstract string ToString(object value, NumberFormatInfo formatInfo);

		// Token: 0x0600003C RID: 60 RVA: 0x00002BF8 File Offset: 0x00000DF8
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002C14 File Offset: 0x00000E14
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text != null)
			{
				text = text.Trim();
				try
				{
					if (this.AllowHex && text.get_Chars(0) == '#')
					{
						return this.FromString(text.Substring(1), 16);
					}
					if ((this.AllowHex && text.StartsWith("0x", 5)) || text.StartsWith("&h", 5))
					{
						return this.FromString(text.Substring(2), 16);
					}
					if (culture == null)
					{
						culture = CultureInfo.CurrentCulture;
					}
					NumberFormatInfo formatInfo = (NumberFormatInfo)culture.GetFormat(typeof(NumberFormatInfo));
					return this.FromString(text, formatInfo);
				}
				catch (Exception innerException)
				{
					throw this.FromStringError(text, innerException);
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002CE0 File Offset: 0x00000EE0
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string) && value != null && IntrospectionExtensions.GetTypeInfo(this.TargetType).IsAssignableFrom(IntrospectionExtensions.GetTypeInfo(value.GetType())))
			{
				if (culture == null)
				{
					culture = CultureInfo.CurrentCulture;
				}
				NumberFormatInfo formatInfo = (NumberFormatInfo)culture.GetFormat(typeof(NumberFormatInfo));
				return this.ToString(value, formatInfo);
			}
			if (IntrospectionExtensions.GetTypeInfo(destinationType).IsPrimitive)
			{
				return Convert.ChangeType(value, destinationType, culture);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002D76 File Offset: 0x00000F76
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return base.CanConvertTo(context, destinationType) || IntrospectionExtensions.GetTypeInfo(destinationType).IsPrimitive;
		}
	}
}
