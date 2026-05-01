using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000054 RID: 84
	[NullableContext(1)]
	[Nullable(0)]
	internal static class ConvertUtils
	{
		// Token: 0x0600047B RID: 1147 RVA: 0x00011004 File Offset: 0x0000F204
		public static PrimitiveTypeCode GetTypeCode(Type t)
		{
			bool flag;
			return ConvertUtils.GetTypeCode(t, out flag);
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0001101C File Offset: 0x0000F21C
		[UnconditionalSuppressMessage("AotAnalysis", "IL3050", Justification = "Nullable<T> instantiated over primitive types are kept by TypeCodeMap")]
		public static PrimitiveTypeCode GetTypeCode(Type t, out bool isEnum)
		{
			PrimitiveTypeCode result;
			if (ConvertUtils.TypeCodeMap.TryGetValue(t, ref result))
			{
				isEnum = false;
				return result;
			}
			if (t.IsEnum())
			{
				isEnum = true;
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
					isEnum = true;
					return ConvertUtils.GetTypeCode(t2);
				}
			}
			isEnum = false;
			return PrimitiveTypeCode.Object;
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00011098 File Offset: 0x0000F298
		public static bool IsConvertible(Type t)
		{
			return t == typeof(bool) || t == typeof(byte) || t == typeof(char) || t == typeof(DateTime) || t == typeof(decimal) || t == typeof(double) || t == typeof(short) || t == typeof(int) || t == typeof(long) || t == typeof(sbyte) || t == typeof(float) || t == typeof(string) || t == typeof(ushort) || t == typeof(uint) || t == typeof(ulong) || t.IsEnum();
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0001117F File Offset: 0x0000F37F
		public static TimeSpan ParseTimeSpan(string input)
		{
			return TimeSpan.Parse(input, CultureInfo.InvariantCulture);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0001118C File Offset: 0x0000F38C
		[NullableContext(2)]
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		private static Func<object, object> CreateCastConverter([Nullable(new byte[]
		{
			0,
			1,
			1
		})] StructMultiKey<Type, Type> t)
		{
			Type value = t.Value1;
			Type value2 = t.Value2;
			MethodInfo method;
			if ((method = value2.GetMethod("op_Implicit", new Type[]
			{
				value
			})) == null)
			{
				method = value2.GetMethod("op_Explicit", new Type[]
				{
					value
				});
			}
			MethodInfo methodInfo = method;
			if (methodInfo == null)
			{
				return null;
			}
			MethodCall<object, object> call = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(methodInfo);
			return (object o) => call(null, new object[]
			{
				o
			});
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00011200 File Offset: 0x0000F400
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public static object Convert(object initialValue, CultureInfo culture, Type targetType)
		{
			object result;
			switch (ConvertUtils.TryConvertInternal(initialValue, culture, targetType, out result))
			{
			case ConvertUtils.ConvertResult.Success:
				return result;
			case ConvertUtils.ConvertResult.CannotConvertNull:
				throw new Exception("Can not convert null {0} into non-nullable {1}.".FormatWith(CultureInfo.InvariantCulture, initialValue.GetType(), targetType));
			case ConvertUtils.ConvertResult.NotInstantiableType:
				throw new ArgumentException("Target type {0} is not a value type or a non-abstract class.".FormatWith(CultureInfo.InvariantCulture, targetType), "targetType");
			case ConvertUtils.ConvertResult.NoValidConversion:
				throw new InvalidOperationException("Can not convert from {0} to {1}.".FormatWith(CultureInfo.InvariantCulture, initialValue.GetType(), targetType));
			default:
				throw new InvalidOperationException("Unexpected conversion result.");
			}
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00011290 File Offset: 0x0000F490
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		private static bool TryConvert([Nullable(2)] object initialValue, CultureInfo culture, Type targetType, [Nullable(2)] out object value)
		{
			bool result;
			try
			{
				if (ConvertUtils.TryConvertInternal(initialValue, culture, targetType, out value) == ConvertUtils.ConvertResult.Success)
				{
					result = true;
				}
				else
				{
					value = null;
					result = false;
				}
			}
			catch
			{
				value = null;
				result = false;
			}
			return result;
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x000112CC File Offset: 0x0000F4CC
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		private static ConvertUtils.ConvertResult TryConvertInternal([Nullable(2)] object initialValue, CultureInfo culture, Type targetType, [Nullable(2)] out object value)
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
				value = initialValue;
				return ConvertUtils.ConvertResult.Success;
			}
			if (ConvertUtils.IsConvertible(initialValue.GetType()) && ConvertUtils.IsConvertible(targetType))
			{
				if (targetType.IsEnum())
				{
					if (initialValue is string)
					{
						value = Enum.Parse(targetType, initialValue.ToString(), true);
						return ConvertUtils.ConvertResult.Success;
					}
					if (ConvertUtils.IsInteger(initialValue))
					{
						value = Enum.ToObject(targetType, initialValue);
						return ConvertUtils.ConvertResult.Success;
					}
				}
				value = System.Convert.ChangeType(initialValue, targetType, culture);
				return ConvertUtils.ConvertResult.Success;
			}
			if (initialValue is DateTime)
			{
				DateTime dateTime = (DateTime)initialValue;
				if (targetType == typeof(DateTimeOffset))
				{
					value = new DateTimeOffset(dateTime);
					return ConvertUtils.ConvertResult.Success;
				}
			}
			byte[] array = initialValue as byte[];
			if (array != null && targetType == typeof(Guid))
			{
				value = new Guid(array);
				return ConvertUtils.ConvertResult.Success;
			}
			if (initialValue is Guid)
			{
				Guid guid = (Guid)initialValue;
				if (targetType == typeof(byte[]))
				{
					value = guid.ToByteArray();
					return ConvertUtils.ConvertResult.Success;
				}
			}
			string text = initialValue as string;
			if (text != null)
			{
				if (targetType == typeof(Guid))
				{
					value = new Guid(text);
					return ConvertUtils.ConvertResult.Success;
				}
				if (targetType == typeof(Uri))
				{
					value = new Uri(text, 0);
					return ConvertUtils.ConvertResult.Success;
				}
				if (targetType == typeof(TimeSpan))
				{
					value = ConvertUtils.ParseTimeSpan(text);
					return ConvertUtils.ConvertResult.Success;
				}
				if (targetType == typeof(byte[]))
				{
					value = System.Convert.FromBase64String(text);
					return ConvertUtils.ConvertResult.Success;
				}
				if (targetType == typeof(Version))
				{
					Version version;
					if (ConvertUtils.VersionTryParse(text, out version))
					{
						value = version;
						return ConvertUtils.ConvertResult.Success;
					}
					value = null;
					return ConvertUtils.ConvertResult.NoValidConversion;
				}
				else if (typeof(Type).IsAssignableFrom(targetType))
				{
					value = Type.GetType(text, true);
					return ConvertUtils.ConvertResult.Success;
				}
			}
			TypeConverter converter = TypeDescriptor.GetConverter(type);
			if (converter != null && converter.CanConvertTo(targetType))
			{
				value = converter.ConvertTo(null, culture, initialValue, targetType);
				return ConvertUtils.ConvertResult.Success;
			}
			TypeConverter converter2 = TypeDescriptor.GetConverter(targetType);
			if (converter2 != null && converter2.CanConvertFrom(type))
			{
				value = converter2.ConvertFrom(null, culture, initialValue);
				return ConvertUtils.ConvertResult.Success;
			}
			if (targetType.IsInterface() || targetType.IsGenericTypeDefinition() || targetType.IsAbstract())
			{
				value = null;
				return ConvertUtils.ConvertResult.NotInstantiableType;
			}
			value = null;
			return ConvertUtils.ConvertResult.NoValidConversion;
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x000114FC File Offset: 0x0000F6FC
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		[return: Nullable(2)]
		public static object ConvertOrCast([Nullable(2)] object initialValue, CultureInfo culture, Type targetType)
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

		// Token: 0x06000484 RID: 1156 RVA: 0x00011540 File Offset: 0x0000F740
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		[return: Nullable(2)]
		private static object EnsureTypeAssignable([Nullable(2)] object value, Type initialType, Type targetType)
		{
			if (value != null)
			{
				Type type = value.GetType();
				if (targetType.IsAssignableFrom(type))
				{
					return value;
				}
				Func<object, object> func = ConvertUtils.CastConverters.Instance.Get(new StructMultiKey<Type, Type>(type, targetType));
				if (func != null)
				{
					return func.Invoke(value);
				}
			}
			else if (ReflectionUtils.IsNullable(targetType))
			{
				return null;
			}
			throw new ArgumentException("Could not cast or convert from {0} to {1}.".FormatWith(CultureInfo.InvariantCulture, ((initialType != null) ? initialType.ToString() : null) ?? "{null}", targetType));
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x000115B3 File Offset: 0x0000F7B3
		public static bool VersionTryParse(string input, [Nullable(2)] [NotNullWhen(true)] out Version result)
		{
			return Version.TryParse(input, ref result);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x000115BC File Offset: 0x0000F7BC
		public static bool IsInteger(object value)
		{
			switch (ConvertUtils.GetTypeCode(value.GetType()))
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

		// Token: 0x06000487 RID: 1159 RVA: 0x00011620 File Offset: 0x0000F820
		public static ParseResult Int32TryParse(char[] chars, int start, int length, out int value)
		{
			value = 0;
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
			if (length > 10 || (length == 10 && chars[start] - '0' > '\u0002'))
			{
				for (int i = start; i < num; i++)
				{
					int num2 = (int)(chars[i] - '0');
					if (num2 < 0 || num2 > 9)
					{
						return ParseResult.Invalid;
					}
				}
				return ParseResult.Overflow;
			}
			for (int j = start; j < num; j++)
			{
				int num3 = (int)(chars[j] - '0');
				if (num3 < 0 || num3 > 9)
				{
					return ParseResult.Invalid;
				}
				int num4 = 10 * value - num3;
				if (num4 > value)
				{
					for (j++; j < num; j++)
					{
						num3 = (int)(chars[j] - '0');
						if (num3 < 0 || num3 > 9)
						{
							return ParseResult.Invalid;
						}
					}
					return ParseResult.Overflow;
				}
				value = num4;
			}
			if (!flag)
			{
				if (value == -2147483648)
				{
					return ParseResult.Overflow;
				}
				value = -value;
			}
			return ParseResult.Success;
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00011700 File Offset: 0x0000F900
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
			if (length > 19)
			{
				for (int i = start; i < num; i++)
				{
					int num2 = (int)(chars[i] - '0');
					if (num2 < 0 || num2 > 9)
					{
						return ParseResult.Invalid;
					}
				}
				return ParseResult.Overflow;
			}
			for (int j = start; j < num; j++)
			{
				int num3 = (int)(chars[j] - '0');
				if (num3 < 0 || num3 > 9)
				{
					return ParseResult.Invalid;
				}
				long num4 = 10L * value - (long)num3;
				if (num4 > value)
				{
					for (j++; j < num; j++)
					{
						num3 = (int)(chars[j] - '0');
						if (num3 < 0 || num3 > 9)
						{
							return ParseResult.Invalid;
						}
					}
					return ParseResult.Overflow;
				}
				value = num4;
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

		// Token: 0x06000489 RID: 1161 RVA: 0x000117DC File Offset: 0x0000F9DC
		public static ParseResult DecimalTryParse(char[] chars, int start, int length, out decimal value)
		{
			value = default(decimal);
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
			int i = start;
			int num = start + length;
			int num2 = num;
			int num3 = num;
			int num4 = 0;
			ulong num5 = 0UL;
			ulong num6 = 0UL;
			int num7 = 0;
			int num8 = 0;
			char? c = default(char?);
			bool? flag2 = default(bool?);
			while (i < num)
			{
				char c2 = chars[i];
				if (c2 == '.')
				{
					goto IL_74;
				}
				if (c2 != 'E' && c2 != 'e')
				{
					if (c2 < '0' || c2 > '9')
					{
						return ParseResult.Invalid;
					}
					if (i == start && c2 == '0')
					{
						i++;
						if (i != num)
						{
							c2 = chars[i];
							if (c2 == '.')
							{
								goto IL_74;
							}
							if (c2 != 'e' && c2 != 'E')
							{
								return ParseResult.Invalid;
							}
							goto IL_91;
						}
					}
					if (num7 < 29)
					{
						if (num7 == 28)
						{
							bool? flag3 = flag2;
							bool valueOrDefault;
							if (flag3 == null)
							{
								flag2 = new bool?(num5 > 7922816251426433759UL || (num5 == 7922816251426433759UL && (num6 > 354395033UL || (num6 == 354395033UL && c2 > '5'))));
								bool? flag4 = flag2;
								valueOrDefault = flag4.GetValueOrDefault();
							}
							else
							{
								valueOrDefault = flag3.GetValueOrDefault();
							}
							if (valueOrDefault)
							{
								goto IL_1FF;
							}
						}
						if (num7 < 19)
						{
							num5 = num5 * 10UL + (ulong)((long)(c2 - '0'));
						}
						else
						{
							num6 = num6 * 10UL + (ulong)((long)(c2 - '0'));
						}
						num7++;
						goto IL_217;
					}
					IL_1FF:
					if (c == null)
					{
						c = new char?(c2);
					}
					num8++;
					goto IL_217;
				}
				IL_91:
				if (i == start)
				{
					return ParseResult.Invalid;
				}
				if (i == num2)
				{
					return ParseResult.Invalid;
				}
				i++;
				if (i == num)
				{
					return ParseResult.Invalid;
				}
				if (num2 < num)
				{
					num3 = i - 1;
				}
				c2 = chars[i];
				bool flag5 = false;
				if (c2 != '+')
				{
					if (c2 == '-')
					{
						flag5 = true;
						i++;
					}
				}
				else
				{
					i++;
				}
				while (i < num)
				{
					c2 = chars[i];
					if (c2 < '0' || c2 > '9')
					{
						return ParseResult.Invalid;
					}
					int num9 = 10 * num4 + (int)(c2 - '0');
					if (num4 < num9)
					{
						num4 = num9;
					}
					i++;
				}
				if (flag5)
				{
					num4 = -num4;
				}
				IL_217:
				i++;
				continue;
				IL_74:
				if (i == start)
				{
					return ParseResult.Invalid;
				}
				if (i + 1 == num)
				{
					return ParseResult.Invalid;
				}
				if (num2 != num)
				{
					return ParseResult.Invalid;
				}
				num2 = i + 1;
				goto IL_217;
			}
			num4 += num8;
			num4 -= num3 - num2;
			if (num7 <= 19)
			{
				value = num5;
			}
			else
			{
				value = num5 / new decimal(1, 0, 0, false, (byte)(num7 - 19)) + num6;
			}
			if (num4 > 0)
			{
				num7 += num4;
				if (num7 > 29)
				{
					return ParseResult.Overflow;
				}
				if (num7 == 29)
				{
					if (num4 > 1)
					{
						value /= new decimal(1, 0, 0, false, (byte)(num4 - 1));
						if (value > 7922816251426433759354395033m)
						{
							return ParseResult.Overflow;
						}
					}
					else if (value == 7922816251426433759354395033m)
					{
						char? c3 = c;
						int? num10 = (c3 != null) ? new int?((int)c3.GetValueOrDefault()) : default(int?);
						int num11 = 53;
						if (num10.GetValueOrDefault() > num11 & num10 != null)
						{
							return ParseResult.Overflow;
						}
					}
					value *= 10m;
				}
				else
				{
					value /= new decimal(1, 0, 0, false, (byte)num4);
				}
			}
			else
			{
				char? c3 = c;
				int? num10 = (c3 != null) ? new int?((int)c3.GetValueOrDefault()) : default(int?);
				int num11 = 53;
				if ((num10.GetValueOrDefault() >= num11 & num10 != null) && num4 >= -28)
				{
					value = ++value;
				}
				if (num4 < 0)
				{
					if (num7 + num4 + 28 <= 0)
					{
						value = (flag ? 0m : 0m);
						return ParseResult.Success;
					}
					if (num4 >= -28)
					{
						value *= new decimal(1, 0, 0, false, (byte)(-(byte)num4));
					}
					else
					{
						value /= 10000000000000000000000000000m;
						value *= new decimal(1, 0, 0, false, (byte)(-num4 - 28));
					}
				}
			}
			if (flag)
			{
				value = -value;
			}
			return ParseResult.Success;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00011C71 File Offset: 0x0000FE71
		public static bool TryConvertGuid(string s, out Guid g)
		{
			return Guid.TryParseExact(s, "D", ref g);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00011C80 File Offset: 0x0000FE80
		public static bool TryHexTextToInt(char[] text, int start, int end, out int value)
		{
			value = 0;
			for (int i = start; i < end; i++)
			{
				char c = text[i];
				int num;
				if (c <= '9' && c >= '0')
				{
					num = (int)(c - '0');
				}
				else if (c <= 'F' && c >= 'A')
				{
					num = (int)(c - '7');
				}
				else
				{
					if (c > 'f' || c < 'a')
					{
						value = 0;
						return false;
					}
					num = (int)(c - 'W');
				}
				value += num << (end - 1 - i) * 4;
			}
			return true;
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00011CEC File Offset: 0x0000FEEC
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
			dictionary.Add(typeof(Uri), PrimitiveTypeCode.Uri);
			dictionary.Add(typeof(string), PrimitiveTypeCode.String);
			dictionary.Add(typeof(byte[]), PrimitiveTypeCode.Bytes);
			ConvertUtils.TypeCodeMap = dictionary;
		}

		// Token: 0x040001CE RID: 462
		private static readonly Dictionary<Type, PrimitiveTypeCode> TypeCodeMap;

		// Token: 0x02000166 RID: 358
		[NullableContext(0)]
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		private static class CastConverters
		{
			// Token: 0x040006BF RID: 1727
			[Nullable(new byte[]
			{
				1,
				0,
				1,
				1,
				2,
				2,
				2
			})]
			public static readonly ThreadSafeStore<StructMultiKey<Type, Type>, Func<object, object>> Instance = new ThreadSafeStore<StructMultiKey<Type, Type>, Func<object, object>>(new Func<StructMultiKey<Type, Type>, Func<object, object>>(ConvertUtils.CreateCastConverter));
		}

		// Token: 0x02000167 RID: 359
		[NullableContext(0)]
		internal enum ConvertResult
		{
			// Token: 0x040006C1 RID: 1729
			Success,
			// Token: 0x040006C2 RID: 1730
			CannotConvertNull,
			// Token: 0x040006C3 RID: 1731
			NotInstantiableType,
			// Token: 0x040006C4 RID: 1732
			NoValidConversion
		}
	}
}
