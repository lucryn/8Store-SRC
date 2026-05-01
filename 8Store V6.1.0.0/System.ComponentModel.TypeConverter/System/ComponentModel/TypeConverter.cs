using System;
using System.Collections;
using System.Globalization;

namespace System.ComponentModel
{
	// Token: 0x0200001E RID: 30
	public class TypeConverter
	{
		// Token: 0x060000BA RID: 186 RVA: 0x00003CF0 File Offset: 0x00001EF0
		public bool CanConvertFrom(Type sourceType)
		{
			return this.CanConvertFrom(null, sourceType);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00002172 File Offset: 0x00000372
		public virtual bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return false;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003CFA File Offset: 0x00001EFA
		public bool CanConvertTo(Type destinationType)
		{
			return this.CanConvertTo(null, destinationType);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003D04 File Offset: 0x00001F04
		public virtual bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003D13 File Offset: 0x00001F13
		public object ConvertFrom(object value)
		{
			return this.ConvertFrom(null, CultureInfo.CurrentCulture, value);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003D22 File Offset: 0x00001F22
		public virtual object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			throw this.GetConvertFromException(value);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003D2B File Offset: 0x00001F2B
		public object ConvertFromInvariantString(string text)
		{
			return this.ConvertFromString(null, CultureInfo.InvariantCulture, text);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003D3A File Offset: 0x00001F3A
		public object ConvertFromInvariantString(ITypeDescriptorContext context, string text)
		{
			return this.ConvertFromString(context, CultureInfo.InvariantCulture, text);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003D49 File Offset: 0x00001F49
		public object ConvertFromString(string text)
		{
			return this.ConvertFrom(null, null, text);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003D54 File Offset: 0x00001F54
		public object ConvertFromString(ITypeDescriptorContext context, string text)
		{
			return this.ConvertFrom(context, CultureInfo.CurrentCulture, text);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003D63 File Offset: 0x00001F63
		public object ConvertFromString(ITypeDescriptorContext context, CultureInfo culture, string text)
		{
			return this.ConvertFrom(context, culture, text);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003D6E File Offset: 0x00001F6E
		public object ConvertTo(object value, Type destinationType)
		{
			return this.ConvertTo(null, null, value, destinationType);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003D7C File Offset: 0x00001F7C
		public virtual object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType != typeof(string))
			{
				throw this.GetConvertToException(value, destinationType);
			}
			if (value == null)
			{
				return string.Empty;
			}
			if (culture != null && culture != CultureInfo.CurrentCulture)
			{
				IFormattable formattable = value as IFormattable;
				if (formattable != null)
				{
					return formattable.ToString(null, culture);
				}
			}
			return value.ToString();
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003DDD File Offset: 0x00001FDD
		public string ConvertToInvariantString(object value)
		{
			return this.ConvertToString(null, CultureInfo.InvariantCulture, value);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003DEC File Offset: 0x00001FEC
		public string ConvertToInvariantString(ITypeDescriptorContext context, object value)
		{
			return this.ConvertToString(context, CultureInfo.InvariantCulture, value);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003DFB File Offset: 0x00001FFB
		public string ConvertToString(object value)
		{
			return (string)this.ConvertTo(null, CultureInfo.CurrentCulture, value, typeof(string));
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003E19 File Offset: 0x00002019
		public string ConvertToString(ITypeDescriptorContext context, object value)
		{
			return (string)this.ConvertTo(context, CultureInfo.CurrentCulture, value, typeof(string));
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003E37 File Offset: 0x00002037
		public string ConvertToString(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			return (string)this.ConvertTo(context, culture, value, typeof(string));
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003E51 File Offset: 0x00002051
		public object CreateInstance(IDictionary propertyValues)
		{
			return this.CreateInstance(null, propertyValues);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003E5B File Offset: 0x0000205B
		public virtual object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			return null;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003E60 File Offset: 0x00002060
		protected Exception GetConvertFromException(object value)
		{
			string p;
			if (value == null)
			{
				p = SR.Null;
			}
			else
			{
				p = value.GetType().FullName;
			}
			throw new NotSupportedException(SR.Format(SR.ConvertFromException, base.GetType().Name, p));
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003EA0 File Offset: 0x000020A0
		protected Exception GetConvertToException(object value, Type destinationType)
		{
			string p;
			if (value == null)
			{
				p = SR.Null;
			}
			else
			{
				p = value.GetType().FullName;
			}
			throw new NotSupportedException(SR.Format(SR.ConvertToException, base.GetType().Name, p, destinationType.FullName));
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003EE5 File Offset: 0x000020E5
		public bool GetCreateInstanceSupported()
		{
			return this.GetCreateInstanceSupported(null);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00002172 File Offset: 0x00000372
		public virtual bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003EEE File Offset: 0x000020EE
		public bool GetPropertiesSupported()
		{
			return this.GetPropertiesSupported(null);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00002172 File Offset: 0x00000372
		public virtual bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003EF7 File Offset: 0x000020F7
		public ICollection GetStandardValues()
		{
			return this.GetStandardValues(null);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003E5B File Offset: 0x0000205B
		public virtual TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return null;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003F00 File Offset: 0x00002100
		public bool GetStandardValuesExclusive()
		{
			return this.GetStandardValuesExclusive(null);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00002172 File Offset: 0x00000372
		public virtual bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00003F09 File Offset: 0x00002109
		public bool GetStandardValuesSupported()
		{
			return this.GetStandardValuesSupported(null);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00002172 File Offset: 0x00000372
		public virtual bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00003F12 File Offset: 0x00002112
		public bool IsValid(object value)
		{
			return this.IsValid(null, value);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00003F1C File Offset: 0x0000211C
		public virtual bool IsValid(ITypeDescriptorContext context, object value)
		{
			bool result = true;
			try
			{
				if (value == null || this.CanConvertFrom(context, value.GetType()))
				{
					this.ConvertFrom(context, CultureInfo.InvariantCulture, value);
				}
				else
				{
					result = false;
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x02000024 RID: 36
		public class StandardValuesCollection : ICollection, IEnumerable
		{
			// Token: 0x060000FE RID: 254 RVA: 0x000041FC File Offset: 0x000023FC
			public StandardValuesCollection(ICollection values)
			{
				if (values == null)
				{
					values = new object[0];
				}
				Array array = values as Array;
				if (array != null)
				{
					this._valueArray = array;
				}
				this._values = values;
			}

			// Token: 0x17000036 RID: 54
			// (get) Token: 0x060000FF RID: 255 RVA: 0x00004232 File Offset: 0x00002432
			public int Count
			{
				get
				{
					if (this._valueArray != null)
					{
						return this._valueArray.Length;
					}
					return this._values.Count;
				}
			}

			// Token: 0x17000037 RID: 55
			public object this[int index]
			{
				get
				{
					if (this._valueArray != null)
					{
						return this._valueArray.GetValue(new int[]
						{
							index
						});
					}
					IList list = this._values as IList;
					if (list != null)
					{
						return list[index];
					}
					this._valueArray = new object[this._values.Count];
					this._values.CopyTo(this._valueArray, 0);
					return this._valueArray.GetValue(new int[]
					{
						index
					});
				}
			}

			// Token: 0x06000101 RID: 257 RVA: 0x000042D3 File Offset: 0x000024D3
			public void CopyTo(Array array, int index)
			{
				this._values.CopyTo(array, index);
			}

			// Token: 0x06000102 RID: 258 RVA: 0x000042E2 File Offset: 0x000024E2
			public IEnumerator GetEnumerator()
			{
				return this._values.GetEnumerator();
			}

			// Token: 0x17000038 RID: 56
			// (get) Token: 0x06000103 RID: 259 RVA: 0x00002172 File Offset: 0x00000372
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000039 RID: 57
			// (get) Token: 0x06000104 RID: 260 RVA: 0x00003E5B File Offset: 0x0000205B
			object ICollection.SyncRoot
			{
				get
				{
					return null;
				}
			}

			// Token: 0x04000010 RID: 16
			private ICollection _values;

			// Token: 0x04000011 RID: 17
			private Array _valueArray;
		}
	}
}
