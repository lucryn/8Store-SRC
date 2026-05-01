using System;
using System.Globalization;

namespace System.ComponentModel
{
	// Token: 0x02000020 RID: 32
	public abstract class TypeListConverter : TypeConverter
	{
		// Token: 0x060000E4 RID: 228 RVA: 0x00003FEB File Offset: 0x000021EB
		protected TypeListConverter(Type[] types)
		{
			this._types = types;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00003FFA File Offset: 0x000021FA
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00002F64 File Offset: 0x00001164
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return base.CanConvertTo(context, destinationType);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004014 File Offset: 0x00002214
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				foreach (Type type in this._types)
				{
					if (value.Equals(type.FullName))
					{
						return type;
					}
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000405B File Offset: 0x0000225B
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType != typeof(string))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			if (value == null)
			{
				return SR.none;
			}
			return ((Type)value).FullName;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000409C File Offset: 0x0000229C
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (this._values == null)
			{
				object[] array;
				if (this._types != null)
				{
					array = new object[this._types.Length];
					Array.Copy(this._types, array, this._types.Length);
				}
				else
				{
					array = null;
				}
				this._values = new TypeConverter.StandardValuesCollection(array);
			}
			return this._values;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00002BCF File Offset: 0x00000DCF
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00002BCF File Offset: 0x00000DCF
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x0400000E RID: 14
		private readonly Type[] _types;

		// Token: 0x0400000F RID: 15
		private TypeConverter.StandardValuesCollection _values;
	}
}
