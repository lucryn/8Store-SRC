using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace System.ComponentModel
{
	// Token: 0x02000011 RID: 17
	public class EnumConverter : TypeConverter
	{
		// Token: 0x0600006E RID: 110 RVA: 0x0000339D File Offset: 0x0000159D
		public EnumConverter(Type type)
		{
			this._type = type;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000033AC File Offset: 0x000015AC
		protected Type EnumType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000033B4 File Offset: 0x000015B4
		// (set) Token: 0x06000071 RID: 113 RVA: 0x000033BC File Offset: 0x000015BC
		protected TypeConverter.StandardValuesCollection Values
		{
			get
			{
				return this._values;
			}
			set
			{
				this._values = value;
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000033C5 File Offset: 0x000015C5
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || sourceType == typeof(Enum[]) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000033EB File Offset: 0x000015EB
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(Enum[]) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003404 File Offset: 0x00001604
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text != null)
			{
				try
				{
					if (text.IndexOf(',') != -1)
					{
						long num = 0L;
						string[] array = text.Split(new char[]
						{
							','
						});
						foreach (string text2 in array)
						{
							num |= Convert.ToInt64((Enum)Enum.Parse(this._type, text2, true), culture);
						}
						return Enum.ToObject(this._type, num);
					}
					return Enum.Parse(this._type, text, true);
				}
				catch (Exception ex)
				{
					throw new FormatException(SR.Format(SR.ConvertInvalidPrimitive, (string)value, this._type.Name), ex);
				}
			}
			if (value is Enum[])
			{
				long num2 = 0L;
				foreach (Enum @enum in (Enum[])value)
				{
					num2 |= Convert.ToInt64(@enum, culture);
				}
				return Enum.ToObject(this._type, num2);
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000352C File Offset: 0x0000172C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string) && value != null)
			{
				if (!CustomAttributeExtensions.IsDefined(IntrospectionExtensions.GetTypeInfo(this._type), typeof(FlagsAttribute), false) && !Enum.IsDefined(this._type, value))
				{
					throw new ArgumentException(SR.Format(SR.EnumConverterInvalidValue, value.ToString(), this._type.Name));
				}
				return Enum.Format(this._type, value, "G");
			}
			else
			{
				if (destinationType != typeof(Enum[]) || value == null)
				{
					return base.ConvertTo(context, culture, value, destinationType);
				}
				if (CustomAttributeExtensions.IsDefined(IntrospectionExtensions.GetTypeInfo(this._type), typeof(FlagsAttribute), false))
				{
					List<Enum> list = new List<Enum>();
					Array values = Enum.GetValues(this._type);
					long[] array = new long[values.Length];
					for (int i = 0; i < values.Length; i++)
					{
						array[i] = Convert.ToInt64((Enum)values.GetValue(new int[]
						{
							i
						}), culture);
					}
					long num = Convert.ToInt64((Enum)value, culture);
					bool flag = true;
					while (flag)
					{
						flag = false;
						foreach (long num2 in array)
						{
							if ((num2 != 0L && (num2 & num) == num2) || num2 == num)
							{
								list.Add((Enum)Enum.ToObject(this._type, num2));
								flag = true;
								num &= ~num2;
								break;
							}
						}
						if (num == 0L)
						{
							break;
						}
					}
					if (!flag && num != 0L)
					{
						list.Add((Enum)Enum.ToObject(this._type, num));
					}
					return list.ToArray();
				}
				return new Enum[]
				{
					(Enum)Enum.ToObject(this._type, value)
				};
			}
		}

		// Token: 0x04000007 RID: 7
		private TypeConverter.StandardValuesCollection _values;

		// Token: 0x04000008 RID: 8
		private readonly Type _type;
	}
}
