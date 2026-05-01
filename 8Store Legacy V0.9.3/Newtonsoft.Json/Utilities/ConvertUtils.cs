using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.Reflection;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000B6 RID: 182
	internal static class ConvertUtils
	{
		// Token: 0x0600090E RID: 2318 RVA: 0x00022330 File Offset: 0x00020530
		public static PrimitiveTypeCode GetTypeCode(Type t)
		{
			PrimitiveTypeCode result;
			if (ConvertUtils.TypeCodeMap.TryGetValue(t, ref result))
			{
				return result;
			}
			if (t.IsEnum())
			{
				return ConvertUtils.GetTypeCode(Enum.GetUnderlyingType(t));
			}
			if (ReflectionUtils.IsNullableType(t))
			{
				Type underlyingType = Nullable.GetUnderlyingType(t);
				if (underlyingType.IsEnum())
				{
					Type t2 = typeof(Nullable).MakeGenericType(new Type[]
					{
						Enum.GetUnderlyingType(underlyingType)
					});
					return ConvertUtils.GetTypeCode(t2);
				}
			}
			return PrimitiveTypeCode.Object;
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x000223A2 File Offset: 0x000205A2
		public static PrimitiveTypeCode GetTypeCode(object o)
		{
			return ConvertUtils.GetTypeCode(o.GetType());
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x000223B0 File Offset: 0x000205B0
		public static bool IsConvertible(Type t)
		{
			return t == typeof(bool) || t == typeof(byte) || t == typeof(char) || t == typeof(DateTime) || t == typeof(decimal) || t == typeof(double) || t == typeof(short) || t == typeof(int) || t == typeof(long) || t == typeof(sbyte) || t == typeof(float) || t == typeof(string) || t == typeof(ushort) || t == typeof(uint) || t == typeof(ulong) || t.IsEnum();
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x00022497 File Offset: 0x00020697
		public static TimeSpan ParseTimeSpan(string input)
		{
			return TimeSpan.Parse(input, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x000224D4 File Offset: 0x000206D4
		private static Func<object, object> CreateCastConverter(ConvertUtils.TypeConvertKey t)
		{
			MethodInfo method = t.TargetType.GetMethod("op_Implicit", new Type[]
			{
				t.InitialType
			});
			if (method == null)
			{
				method = t.TargetType.GetMethod("op_Explicit", new Type[]
				{
					t.InitialType
				});
			}
			if (method == null)
			{
				return null;
			}
			MethodCall<object, object> call = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(method);
			return (object o) => call(null, new object[]
			{
				o
			});
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00022554 File Offset: 0x00020754
		internal static BigInteger ToBigInteger(object value)
		{
			if (value is BigInteger)
			{
				return (BigInteger)value;
			}
			if (value is string)
			{
				return BigInteger.Parse((string)value, CultureInfo.InvariantCulture);
			}
			if (value is float)
			{
				return new BigInteger((float)value);
			}
			if (value is double)
			{
				return new BigInteger((double)value);
			}
			if (value is decimal)
			{
				return new BigInteger((decimal)value);
			}
			if (value is int)
			{
				return new BigInteger((int)value);
			}
			if (value is long)
			{
				return new BigInteger((long)value);
			}
			if (value is uint)
			{
				return new BigInteger((uint)value);
			}
			if (value is ulong)
			{
				return new BigInteger((ulong)value);
			}
			if (value is byte[])
			{
				return new BigInteger((byte[])value);
			}
			throw new InvalidCastException("Cannot convert {0} to BigInteger.".FormatWith(CultureInfo.InvariantCulture, value.GetType()));
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x00022644 File Offset: 0x00020844
		public static object FromBigInteger(BigInteger i, Type targetType)
		{
			if (targetType == typeof(decimal))
			{
				return (decimal)i;
			}
			if (targetType == typeof(double))
			{
				return (double)i;
			}
			if (targetType == typeof(float))
			{
				return (float)i;
			}
			if (targetType == typeof(ulong))
			{
				return (ulong)i;
			}
			object result;
			try
			{
				result = System.Convert.ChangeType((long)i, targetType, CultureInfo.InvariantCulture);
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Can not convert from BigInteger to {0}.".FormatWith(CultureInfo.InvariantCulture, targetType), ex);
			}
			return result;
		}

		/// <summary>
		/// Converts the value to the specified type.
		/// </summary>
		/// <param name="initialValue">The value to convert.</param>
		/// <param name="culture">The culture to use when converting.</param>
		/// <param name="targetType">The type to convert the value to.</param>
		/// <returns>The converted type.</returns>
		// Token: 0x06000915 RID: 2325 RVA: 0x000226FC File Offset: 0x000208FC
		public static object Convert(object initialValue, CultureInfo culture, Type targetType)
		{
			if (initialValue == null)
			{
				throw new ArgumentNullException("initialValue");
			}
			if (ReflectionUtils.IsNullableType(targetType))
			{
				targetType = Nullable.GetUnderlyingType(targetType);
			}
			Type type = initialValue.GetType();
			if (targetType == type)
			{
				return initialValue;
			}
			if (ConvertUtils.IsConvertible(initialValue.GetType()) && ConvertUtils.IsConvertible(targetType))
			{
				if (targetType.IsEnum())
				{
					if (initialValue is string)
					{
						return Enum.Parse(targetType, initialValue.ToString(), true);
					}
					if (ConvertUtils.IsInteger(initialValue))
					{
						return Enum.ToObject(targetType, initialValue);
					}
				}
				return System.Convert.ChangeType(initialValue, targetType, culture);
			}
			if (initialValue is DateTime && targetType == typeof(DateTimeOffset))
			{
				return new DateTimeOffset((DateTime)initialValue);
			}
			if (initialValue is byte[] && targetType == typeof(Guid))
			{
				return new Guid((byte[])initialValue);
			}
			if (initialValue is string)
			{
				if (targetType == typeof(Guid))
				{
					return new Guid((string)initialValue);
				}
				if (targetType == typeof(Uri))
				{
					return new Uri((string)initialValue, 0);
				}
				if (targetType == typeof(TimeSpan))
				{
					return ConvertUtils.ParseTimeSpan((string)initialValue);
				}
				if (typeof(Type).IsAssignableFrom(targetType))
				{
					return Type.GetType((string)initialValue, true);
				}
			}
			if (targetType == typeof(BigInteger))
			{
				return ConvertUtils.ToBigInteger(initialValue);
			}
			if (initialValue is BigInteger)
			{
				return ConvertUtils.FromBigInteger((BigInteger)initialValue, targetType);
			}
			if (targetType.IsInterface() || targetType.IsGenericTypeDefinition() || targetType.IsAbstract())
			{
				throw new ArgumentException("Target type {0} is not a value type or a non-abstract class.".FormatWith(CultureInfo.InvariantCulture, targetType), "targetType");
			}
			throw new InvalidOperationException("Can not convert from {0} to {1}.".FormatWith(CultureInfo.InvariantCulture, type, targetType));
		}

		/// <summary>
		/// Converts the value to the specified type.
		/// </summary>
		/// <param name="initialValue">The value to convert.</param>
		/// <param name="culture">The culture to use when converting.</param>
		/// <param name="targetType">The type to convert the value to.</param>
		/// <param name="convertedValue">The converted value if the conversion was successful or the default value of <c>T</c> if it failed.</param>
		/// <returns>
		/// 	<c>true</c> if <c>initialValue</c> was converted successfully; otherwise, <c>false</c>.
		/// </returns>
		// Token: 0x06000916 RID: 2326 RVA: 0x000228C0 File Offset: 0x00020AC0
		public static bool TryConvert(object initialValue, CultureInfo culture, Type targetType, out object convertedValue)
		{
			bool result;
			try
			{
				convertedValue = ConvertUtils.Convert(initialValue, culture, targetType);
				result = true;
			}
			catch
			{
				convertedValue = null;
				result = false;
			}
			return result;
		}

		/// <summary>
		/// Converts the value to the specified type. If the value is unable to be converted, the
		/// value is checked whether it assignable to the specified type.
		/// </summary>
		/// <param name="initialValue">The value to convert.</param>
		/// <param name="culture">The culture to use when converting.</param>
		/// <param name="targetType">The type to convert or cast the value to.</param>
		/// <returns>
		/// The converted type. If conversion was unsuccessful, the initial value
		/// is returned if assignable to the target type.
		/// </returns>
		// Token: 0x06000917 RID: 2327 RVA: 0x000228F4 File Offset: 0x00020AF4
		public static object ConvertOrCast(object initialValue, CultureInfo culture, Type targetType)
		{
			if (targetType == typeof(object))
			{
				return initialValue;
			}
			if (initialValue == null && ReflectionUtils.IsNullable(targetType))
			{
				return null;
			}
			object result;
			if (ConvertUtils.TryConvert(initialValue, culture, targetType, out result))
			{
				return result;
			}
			return ConvertUtils.EnsureTypeAssignable(initialValue, ReflectionUtils.GetObjectType(initialValue), targetType);
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x00022938 File Offset: 0x00020B38
		private static object EnsureTypeAssignable(object value, Type initialType, Type targetType)
		{
			Type type = (value != null) ? value.GetType() : null;
			if (value != null)
			{
				if (targetType.IsAssignableFrom(type))
				{
					return value;
				}
				Func<object, object> func = ConvertUtils.CastConverters.Get(new ConvertUtils.TypeConvertKey(type, targetType));
				if (func != null)
				{
					return func.Invoke(value);
				}
			}
			else if (ReflectionUtils.IsNullable(targetType))
			{
				return null;
			}
			throw new ArgumentException("Could not cast or convert from {0} to {1}.".FormatWith(CultureInfo.InvariantCulture, (initialType != null) ? initialType.ToString() : "{null}", targetType));
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x000229AC File Offset: 0x00020BAC
		public static bool IsInteger(object value)
		{
			switch (ConvertUtils.GetTypeCode(value))
			{
			case PrimitiveTypeCode.SByte:
			case PrimitiveTypeCode.Int16:
			case PrimitiveTypeCode.UInt16:
			case PrimitiveTypeCode.Int32:
			case PrimitiveTypeCode.Byte:
			case PrimitiveTypeCode.UInt32:
			case PrimitiveTypeCode.Int64:
			case PrimitiveTypeCode.UInt64:
				return true;
			}
			return false;
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x00022A0C File Offset: 0x00020C0C
		public static int Int32Parse(char[] chars, int start, int length)
		{
			if (length == 0)
			{
				throw new FormatException("Input string was not in a correct format.");
			}
			bool flag = chars[start] == '-';
			if (flag)
			{
				if (length == 1)
				{
					throw new FormatException("Input string was not in a correct format.");
				}
				start++;
				length--;
			}
			int num = 0;
			int num2 = start + length;
			for (int i = start; i < num2; i++)
			{
				int num3 = (int)(chars[i] - '0');
				if (num3 < 0 || num3 > 9)
				{
					throw new FormatException("Input string was not in a correct format.");
				}
				int num4 = 10 * num - num3;
				if (num4 > num)
				{
					throw new OverflowException();
				}
				num = num4;
			}
			if (!flag)
			{
				if (num == -2147483648)
				{
					throw new OverflowException();
				}
				num = -num;
			}
			return num;
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x00022AA8 File Offset: 0x00020CA8
		public static ParseResult Int64TryParse(char[] chars, int start, int length, out long value)
		{
			value = 0L;
			if (length == 0)
			{
				return ParseResult.Invalid;
			}
			bool flag = chars[start] == '-';
			if (flag)
			{
				if (length == 1)
				{
					return ParseResult.Invalid;
				}
				start++;
				length--;
			}
			int num = start + length;
			for (int i = start; i < num; i++)
			{
				int num2 = (int)(chars[i] - '0');
				if (num2 < 0 || num2 > 9)
				{
					return ParseResult.Invalid;
				}
				long num3 = 10L * value - (long)num2;
				if (num3 > value)
				{
					return ParseResult.Overflow;
				}
				value = num3;
			}
			if (!flag)
			{
				if (value == -9223372036854775808L)
				{
					return ParseResult.Overflow;
				}
				value = -value;
			}
			return ParseResult.Success;
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00022B2C File Offset: 0x00020D2C
		// Note: this type is marked as 'beforefieldinit'.
		static ConvertUtils()
		{
			Dictionary<Type, PrimitiveTypeCode> dictionary = new Dictionary<Type, PrimitiveTypeCode>();
			dictionary.Add(typeof(char), PrimitiveTypeCode.Char);
			dictionary.Add(typeof(char?), PrimitiveTypeCode.CharNullable);
			dictionary.Add(typeof(bool), PrimitiveTypeCode.Boolean);
			dictionary.Add(typeof(bool?), PrimitiveTypeCode.BooleanNullable);
			dictionary.Add(typeof(sbyte), PrimitiveTypeCode.SByte);
			dictionary.Add(typeof(sbyte?), PrimitiveTypeCode.SByteNullable);
			dictionary.Add(typeof(short), PrimitiveTypeCode.Int16);
			dictionary.Add(typeof(short?), PrimitiveTypeCode.Int16Nullable);
			dictionary.Add(typeof(ushort), PrimitiveTypeCode.UInt16);
			dictionary.Add(typeof(ushort?), PrimitiveTypeCode.UInt16Nullable);
			dictionary.Add(typeof(int), PrimitiveTypeCode.Int32);
			dictionary.Add(typeof(int?), PrimitiveTypeCode.Int32Nullable);
			dictionary.Add(typeof(byte), PrimitiveTypeCode.Byte);
			dictionary.Add(typeof(byte?), PrimitiveTypeCode.ByteNullable);
			dictionary.Add(typeof(uint), PrimitiveTypeCode.UInt32);
			dictionary.Add(typeof(uint?), PrimitiveTypeCode.UInt32Nullable);
			dictionary.Add(typeof(long), PrimitiveTypeCode.Int64);
			dictionary.Add(typeof(long?), PrimitiveTypeCode.Int64Nullable);
			dictionary.Add(typeof(ulong), PrimitiveTypeCode.UInt64);
			dictionary.Add(typeof(ulong?), PrimitiveTypeCode.UInt64Nullable);
			dictionary.Add(typeof(float), PrimitiveTypeCode.Single);
			dictionary.Add(typeof(float?), PrimitiveTypeCode.SingleNullable);
			dictionary.Add(typeof(double), PrimitiveTypeCode.Double);
			dictionary.Add(typeof(double?), PrimitiveTypeCode.DoubleNullable);
			dictionary.Add(typeof(DateTime), PrimitiveTypeCode.DateTime);
			dictionary.Add(typeof(DateTime?), PrimitiveTypeCode.DateTimeNullable);
			dictionary.Add(typeof(DateTimeOffset), PrimitiveTypeCode.DateTimeOffset);
			dictionary.Add(typeof(DateTimeOffset?), PrimitiveTypeCode.DateTimeOffsetNullable);
			dictionary.Add(typeof(decimal), PrimitiveTypeCode.Decimal);
			dictionary.Add(typeof(decimal?), PrimitiveTypeCode.DecimalNullable);
			dictionary.Add(typeof(Guid), PrimitiveTypeCode.Guid);
			dictionary.Add(typeof(Guid?), PrimitiveTypeCode.GuidNullable);
			dictionary.Add(typeof(TimeSpan), PrimitiveTypeCode.TimeSpan);
			dictionary.Add(typeof(TimeSpan?), PrimitiveTypeCode.TimeSpanNullable);
			dictionary.Add(typeof(BigInteger), PrimitiveTypeCode.BigInteger);
			dictionary.Add(typeof(BigInteger?), PrimitiveTypeCode.BigIntegerNullable);
			dictionary.Add(typeof(Uri), PrimitiveTypeCode.Uri);
			dictionary.Add(typeof(string), PrimitiveTypeCode.String);
			dictionary.Add(typeof(byte[]), PrimitiveTypeCode.Bytes);
			ConvertUtils.TypeCodeMap = dictionary;
			List<TypeInformation> list = new List<TypeInformation>();
			list.Add(new TypeInformation
			{
				Type = typeof(object),
				TypeCode = PrimitiveTypeCode.Empty
			});
			list.Add(new TypeInformation
			{
				Type = typeof(object),
				TypeCode = PrimitiveTypeCode.Object
			});
			list.Add(new TypeInformation
			{
				Type = typeof(object),
				TypeCode = PrimitiveTypeCode.DBNull
			});
			list.Add(new TypeInformation
			{
				Type = typeof(bool),
				TypeCode = PrimitiveTypeCode.Boolean
			});
			list.Add(new TypeInformation
			{
				Type = typeof(char),
				TypeCode = PrimitiveTypeCode.Char
			});
			list.Add(new TypeInformation
			{
				Type = typeof(sbyte),
				TypeCode = PrimitiveTypeCode.SByte
			});
			list.Add(new TypeInformation
			{
				Type = typeof(byte),
				TypeCode = PrimitiveTypeCode.Byte
			});
			list.Add(new TypeInformation
			{
				Type = typeof(short),
				TypeCode = PrimitiveTypeCode.Int16
			});
			list.Add(new TypeInformation
			{
				Type = typeof(ushort),
				TypeCode = PrimitiveTypeCode.UInt16
			});
			list.Add(new TypeInformation
			{
				Type = typeof(int),
				TypeCode = PrimitiveTypeCode.Int32
			});
			list.Add(new TypeInformation
			{
				Type = typeof(uint),
				TypeCode = PrimitiveTypeCode.UInt32
			});
			list.Add(new TypeInformation
			{
				Type = typeof(long),
				TypeCode = PrimitiveTypeCode.Int64
			});
			list.Add(new TypeInformation
			{
				Type = typeof(ulong),
				TypeCode = PrimitiveTypeCode.UInt64
			});
			list.Add(new TypeInformation
			{
				Type = typeof(float),
				TypeCode = PrimitiveTypeCode.Single
			});
			list.Add(new TypeInformation
			{
				Type = typeof(double),
				TypeCode = PrimitiveTypeCode.Double
			});
			list.Add(new TypeInformation
			{
				Type = typeof(decimal),
				TypeCode = PrimitiveTypeCode.Decimal
			});
			list.Add(new TypeInformation
			{
				Type = typeof(DateTime),
				TypeCode = PrimitiveTypeCode.DateTime
			});
			list.Add(new TypeInformation
			{
				Type = typeof(object),
				TypeCode = PrimitiveTypeCode.Empty
			});
			list.Add(new TypeInformation
			{
				Type = typeof(string),
				TypeCode = PrimitiveTypeCode.String
			});
			ConvertUtils.PrimitiveTypeCodes = list;
			ConvertUtils.CastConverters = new ThreadSafeStore<ConvertUtils.TypeConvertKey, Func<object, object>>(new Func<ConvertUtils.TypeConvertKey, Func<object, object>>(ConvertUtils.CreateCastConverter));
		}

		// Token: 0x04000366 RID: 870
		private static readonly Dictionary<Type, PrimitiveTypeCode> TypeCodeMap;

		// Token: 0x04000367 RID: 871
		private static readonly List<TypeInformation> PrimitiveTypeCodes;

		// Token: 0x04000368 RID: 872
		private static readonly ThreadSafeStore<ConvertUtils.TypeConvertKey, Func<object, object>> CastConverters;

		// Token: 0x020000B7 RID: 183
		internal struct TypeConvertKey : IEquatable<ConvertUtils.TypeConvertKey>
		{
			// Token: 0x170001EE RID: 494
			// (get) Token: 0x0600091D RID: 2333 RVA: 0x0002311A File Offset: 0x0002131A
			public Type InitialType
			{
				get
				{
					return this._initialType;
				}
			}

			// Token: 0x170001EF RID: 495
			// (get) Token: 0x0600091E RID: 2334 RVA: 0x00023122 File Offset: 0x00021322
			public Type TargetType
			{
				get
				{
					return this._targetType;
				}
			}

			// Token: 0x0600091F RID: 2335 RVA: 0x0002312A File Offset: 0x0002132A
			public TypeConvertKey(Type initialType, Type targetType)
			{
				this._initialType = initialType;
				this._targetType = targetType;
			}

			// Token: 0x06000920 RID: 2336 RVA: 0x0002313A File Offset: 0x0002133A
			public override int GetHashCode()
			{
				return this._initialType.GetHashCode() ^ this._targetType.GetHashCode();
			}

			// Token: 0x06000921 RID: 2337 RVA: 0x00023153 File Offset: 0x00021353
			public override bool Equals(object obj)
			{
				return obj is ConvertUtils.TypeConvertKey && this.Equals((ConvertUtils.TypeConvertKey)obj);
			}

			// Token: 0x06000922 RID: 2338 RVA: 0x0002316B File Offset: 0x0002136B
			public bool Equals(ConvertUtils.TypeConvertKey other)
			{
				return this._initialType == other._initialType && this._targetType == other._targetType;
			}

			// Token: 0x04000369 RID: 873
			private readonly Type _initialType;

			// Token: 0x0400036A RID: 874
			private readonly Type _targetType;
		}
	}
}
