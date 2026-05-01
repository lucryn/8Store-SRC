using System;
using System.Collections;
using System.Globalization;
using System.Reflection;

namespace System.ComponentModel
{
	// Token: 0x02000018 RID: 24
	public class NullableConverter : TypeConverter
	{
		// Token: 0x06000095 RID: 149 RVA: 0x00003898 File Offset: 0x00001A98
		public NullableConverter(Type type)
		{
			this._nullableType = type;
			this._simpleType = Nullable.GetUnderlyingType(type);
			if (this._simpleType == null)
			{
				throw new ArgumentException(SR.NullableConverterBadCtorArg, "type");
			}
			this._simpleTypeConverter = TypeDescriptor.GetConverter(this._simpleType);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000038E7 File Offset: 0x00001AE7
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == this._simpleType)
			{
				return true;
			}
			if (this._simpleTypeConverter != null)
			{
				return this._simpleTypeConverter.CanConvertFrom(context, sourceType);
			}
			return base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003914 File Offset: 0x00001B14
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null || value.GetType() == this._simpleType)
			{
				return value;
			}
			if (value is string && string.IsNullOrEmpty(value as string))
			{
				return null;
			}
			if (this._simpleTypeConverter != null)
			{
				return this._simpleTypeConverter.ConvertFrom(context, culture, value);
			}
			return base.ConvertFrom(context, culture, value);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000396B File Offset: 0x00001B6B
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == this._simpleType)
			{
				return true;
			}
			if (this._simpleTypeConverter != null)
			{
				return this._simpleTypeConverter.CanConvertTo(context, destinationType);
			}
			return base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003998 File Offset: 0x00001B98
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == this._simpleType && value != null && IntrospectionExtensions.GetTypeInfo(this._nullableType).IsAssignableFrom(IntrospectionExtensions.GetTypeInfo(value.GetType())))
			{
				return value;
			}
			if (value == null)
			{
				if (destinationType == typeof(string))
				{
					return string.Empty;
				}
			}
			else if (this._simpleTypeConverter != null)
			{
				return this._simpleTypeConverter.ConvertTo(context, culture, value, destinationType);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003A1C File Offset: 0x00001C1C
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			if (this._simpleTypeConverter != null)
			{
				return this._simpleTypeConverter.CreateInstance(context, propertyValues);
			}
			return base.CreateInstance(context, propertyValues);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003A49 File Offset: 0x00001C49
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			if (this._simpleTypeConverter != null)
			{
				return this._simpleTypeConverter.GetCreateInstanceSupported(context);
			}
			return base.GetCreateInstanceSupported(context);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003A67 File Offset: 0x00001C67
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			if (this._simpleTypeConverter != null)
			{
				return this._simpleTypeConverter.GetPropertiesSupported(context);
			}
			return base.GetPropertiesSupported(context);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003A88 File Offset: 0x00001C88
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (this._simpleTypeConverter != null)
			{
				TypeConverter.StandardValuesCollection standardValues = this._simpleTypeConverter.GetStandardValues(context);
				if (this.GetStandardValuesSupported(context) && standardValues != null)
				{
					object[] array = new object[standardValues.Count + 1];
					int num = 0;
					array[num++] = null;
					foreach (object obj in standardValues)
					{
						array[num++] = obj;
					}
					return new TypeConverter.StandardValuesCollection(array);
				}
			}
			return base.GetStandardValues(context);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003B24 File Offset: 0x00001D24
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			if (this._simpleTypeConverter != null)
			{
				return this._simpleTypeConverter.GetStandardValuesExclusive(context);
			}
			return base.GetStandardValuesExclusive(context);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003B42 File Offset: 0x00001D42
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			if (this._simpleTypeConverter != null)
			{
				return this._simpleTypeConverter.GetStandardValuesSupported(context);
			}
			return base.GetStandardValuesSupported(context);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003B60 File Offset: 0x00001D60
		public override bool IsValid(ITypeDescriptorContext context, object value)
		{
			if (this._simpleTypeConverter != null)
			{
				return value == null || this._simpleTypeConverter.IsValid(context, value);
			}
			return base.IsValid(context, value);
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003B92 File Offset: 0x00001D92
		public Type NullableType
		{
			get
			{
				return this._nullableType;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003B9A File Offset: 0x00001D9A
		public Type UnderlyingType
		{
			get
			{
				return this._simpleType;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003BA2 File Offset: 0x00001DA2
		public TypeConverter UnderlyingTypeConverter
		{
			get
			{
				return this._simpleTypeConverter;
			}
		}

		// Token: 0x04000009 RID: 9
		private readonly Type _nullableType;

		// Token: 0x0400000A RID: 10
		private readonly Type _simpleType;

		// Token: 0x0400000B RID: 11
		private readonly TypeConverter _simpleTypeConverter;
	}
}
