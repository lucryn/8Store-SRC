using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Globalization;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020000D2 RID: 210
	[NullableContext(1)]
	[Nullable(0)]
	public class JValue : JToken, IEquatable<JValue>, IFormattable, IComparable, IComparable<JValue>
	{
		// Token: 0x06000B76 RID: 2934 RVA: 0x0002C844 File Offset: 0x0002AA44
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public override Task WriteToAsync(JsonWriter writer, CancellationToken cancellationToken, params JsonConverter[] converters)
		{
			if (converters != null && converters.Length != 0 && this._value != null)
			{
				JsonConverter matchingConverter = JsonSerializer.GetMatchingConverter(converters, this._value.GetType());
				if (matchingConverter != null && matchingConverter.CanWrite)
				{
					matchingConverter.WriteJson(writer, this._value, JsonSerializer.CreateDefault());
					return AsyncUtils.CompletedTask;
				}
			}
			switch (this._valueType)
			{
			case JTokenType.Comment:
			{
				object value = this._value;
				return writer.WriteCommentAsync((value != null) ? value.ToString() : null, cancellationToken);
			}
			case JTokenType.Integer:
			{
				object value2 = this._value;
				if (value2 is int)
				{
					int value3 = (int)value2;
					return writer.WriteValueAsync(value3, cancellationToken);
				}
				value2 = this._value;
				if (value2 is long)
				{
					long value4 = (long)value2;
					return writer.WriteValueAsync(value4, cancellationToken);
				}
				value2 = this._value;
				if (value2 is ulong)
				{
					ulong value5 = (ulong)value2;
					return writer.WriteValueAsync(value5, cancellationToken);
				}
				return writer.WriteValueAsync(Convert.ToInt64(this._value, CultureInfo.InvariantCulture), cancellationToken);
			}
			case JTokenType.Float:
			{
				object value2 = this._value;
				if (value2 is decimal)
				{
					decimal value6 = (decimal)value2;
					return writer.WriteValueAsync(value6, cancellationToken);
				}
				value2 = this._value;
				if (value2 is double)
				{
					double value7 = (double)value2;
					return writer.WriteValueAsync(value7, cancellationToken);
				}
				value2 = this._value;
				if (value2 is float)
				{
					float value8 = (float)value2;
					return writer.WriteValueAsync(value8, cancellationToken);
				}
				return writer.WriteValueAsync(Convert.ToDouble(this._value, CultureInfo.InvariantCulture), cancellationToken);
			}
			case JTokenType.String:
			{
				object value9 = this._value;
				return writer.WriteValueAsync((value9 != null) ? value9.ToString() : null, cancellationToken);
			}
			case JTokenType.Boolean:
				return writer.WriteValueAsync(Convert.ToBoolean(this._value, CultureInfo.InvariantCulture), cancellationToken);
			case JTokenType.Null:
				return writer.WriteNullAsync(cancellationToken);
			case JTokenType.Undefined:
				return writer.WriteUndefinedAsync(cancellationToken);
			case JTokenType.Date:
			{
				object value2 = this._value;
				if (value2 is DateTimeOffset)
				{
					DateTimeOffset value10 = (DateTimeOffset)value2;
					return writer.WriteValueAsync(value10, cancellationToken);
				}
				return writer.WriteValueAsync(Convert.ToDateTime(this._value, CultureInfo.InvariantCulture), cancellationToken);
			}
			case JTokenType.Raw:
			{
				object value11 = this._value;
				return writer.WriteRawValueAsync((value11 != null) ? value11.ToString() : null, cancellationToken);
			}
			case JTokenType.Bytes:
				return writer.WriteValueAsync((byte[])this._value, cancellationToken);
			case JTokenType.Guid:
				return writer.WriteValueAsync((this._value != null) ? ((Guid?)this._value) : default(Guid?), cancellationToken);
			case JTokenType.Uri:
				return writer.WriteValueAsync((Uri)this._value, cancellationToken);
			case JTokenType.TimeSpan:
				return writer.WriteValueAsync((this._value != null) ? ((TimeSpan?)this._value) : default(TimeSpan?), cancellationToken);
			default:
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException("Type", this._valueType, "Unexpected token type.");
			}
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0002CB21 File Offset: 0x0002AD21
		[NullableContext(2)]
		internal JValue(object value, JTokenType type)
		{
			this._value = value;
			this._valueType = type;
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0002CB37 File Offset: 0x0002AD37
		internal JValue(JValue other, [Nullable(2)] JsonCloneSettings settings) : this(other.Value, other.Type)
		{
			if (settings == null || settings.CopyAnnotations)
			{
				base.CopyAnnotations(this, other);
			}
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0002CB61 File Offset: 0x0002AD61
		public JValue(JValue other) : this(other.Value, other.Type)
		{
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0002CB75 File Offset: 0x0002AD75
		public JValue(long value) : this(BoxedPrimitives.Get(value), JTokenType.Integer)
		{
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0002CB84 File Offset: 0x0002AD84
		public JValue(decimal value) : this(BoxedPrimitives.Get(value), JTokenType.Float)
		{
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0002CB93 File Offset: 0x0002AD93
		public JValue(char value) : this(value, JTokenType.String)
		{
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0002CBA2 File Offset: 0x0002ADA2
		[CLSCompliant(false)]
		public JValue(ulong value) : this(value, JTokenType.Integer)
		{
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0002CBB1 File Offset: 0x0002ADB1
		public JValue(double value) : this(BoxedPrimitives.Get(value), JTokenType.Float)
		{
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0002CBC0 File Offset: 0x0002ADC0
		public JValue(float value) : this(value, JTokenType.Float)
		{
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0002CBCF File Offset: 0x0002ADCF
		public JValue(DateTime value) : this(value, JTokenType.Date)
		{
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0002CBDF File Offset: 0x0002ADDF
		public JValue(DateTimeOffset value) : this(value, JTokenType.Date)
		{
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0002CBEF File Offset: 0x0002ADEF
		public JValue(bool value) : this(BoxedPrimitives.Get(value), JTokenType.Boolean)
		{
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x0002CBFF File Offset: 0x0002ADFF
		[NullableContext(2)]
		public JValue(string value) : this(value, JTokenType.String)
		{
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0002CC09 File Offset: 0x0002AE09
		public JValue(Guid value) : this(value, JTokenType.Guid)
		{
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0002CC19 File Offset: 0x0002AE19
		[NullableContext(2)]
		public JValue(Uri value) : this(value, (value != null) ? JTokenType.Uri : JTokenType.Null)
		{
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0002CC31 File Offset: 0x0002AE31
		public JValue(TimeSpan value) : this(value, JTokenType.TimeSpan)
		{
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x0002CC44 File Offset: 0x0002AE44
		[NullableContext(2)]
		public JValue(object value) : this(value, JValue.GetValueType(default(JTokenType?), value))
		{
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0002CC68 File Offset: 0x0002AE68
		internal override bool DeepEquals(JToken node)
		{
			JValue jvalue = node as JValue;
			return jvalue != null && (jvalue == this || JValue.ValuesEquals(this, jvalue));
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000B89 RID: 2953 RVA: 0x0002CC8E File Offset: 0x0002AE8E
		public override bool HasValues
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0002CC94 File Offset: 0x0002AE94
		[NullableContext(2)]
		internal static int Compare(JTokenType valueType, object objA, object objB)
		{
			if (objA == objB)
			{
				return 0;
			}
			if (objB == null)
			{
				return 1;
			}
			if (objA == null)
			{
				return -1;
			}
			switch (valueType)
			{
			case JTokenType.Comment:
			case JTokenType.String:
			case JTokenType.Raw:
			{
				string text = Convert.ToString(objA, CultureInfo.InvariantCulture);
				string text2 = Convert.ToString(objB, CultureInfo.InvariantCulture);
				return string.CompareOrdinal(text, text2);
			}
			case JTokenType.Integer:
				if (objA is ulong || objB is ulong || objA is decimal || objB is decimal)
				{
					return Convert.ToDecimal(objA, CultureInfo.InvariantCulture).CompareTo(Convert.ToDecimal(objB, CultureInfo.InvariantCulture));
				}
				if (objA is float || objB is float || objA is double || objB is double)
				{
					return JValue.CompareFloat(objA, objB);
				}
				return Convert.ToInt64(objA, CultureInfo.InvariantCulture).CompareTo(Convert.ToInt64(objB, CultureInfo.InvariantCulture));
			case JTokenType.Float:
				if (objA is ulong || objB is ulong || objA is decimal || objB is decimal)
				{
					return Convert.ToDecimal(objA, CultureInfo.InvariantCulture).CompareTo(Convert.ToDecimal(objB, CultureInfo.InvariantCulture));
				}
				return JValue.CompareFloat(objA, objB);
			case JTokenType.Boolean:
			{
				bool flag = Convert.ToBoolean(objA, CultureInfo.InvariantCulture);
				bool flag2 = Convert.ToBoolean(objB, CultureInfo.InvariantCulture);
				return flag.CompareTo(flag2);
			}
			case JTokenType.Date:
			{
				if (objA is DateTime)
				{
					DateTime dateTime = (DateTime)objA;
					DateTime dateTime2;
					if (objB is DateTimeOffset)
					{
						dateTime2 = ((DateTimeOffset)objB).DateTime;
					}
					else
					{
						dateTime2 = Convert.ToDateTime(objB, CultureInfo.InvariantCulture);
					}
					return dateTime.CompareTo(dateTime2);
				}
				DateTimeOffset dateTimeOffset = (DateTimeOffset)objA;
				DateTimeOffset dateTimeOffset2;
				if (objB is DateTimeOffset)
				{
					dateTimeOffset2 = (DateTimeOffset)objB;
				}
				else
				{
					dateTimeOffset2..ctor(Convert.ToDateTime(objB, CultureInfo.InvariantCulture));
				}
				return dateTimeOffset.CompareTo(dateTimeOffset2);
			}
			case JTokenType.Bytes:
			{
				byte[] array = objB as byte[];
				if (array == null)
				{
					throw new ArgumentException("Object must be of type byte[].");
				}
				return MiscellaneousUtils.ByteArrayCompare(objA as byte[], array);
			}
			case JTokenType.Guid:
			{
				if (!(objB is Guid))
				{
					throw new ArgumentException("Object must be of type Guid.");
				}
				Guid guid = (Guid)objA;
				Guid guid2 = (Guid)objB;
				return guid.CompareTo(guid2);
			}
			case JTokenType.Uri:
			{
				Uri uri = objB as Uri;
				if (uri == null)
				{
					throw new ArgumentException("Object must be of type Uri.");
				}
				Uri uri2 = (Uri)objA;
				return Comparer<string>.Default.Compare(uri2.ToString(), uri.ToString());
			}
			case JTokenType.TimeSpan:
			{
				if (!(objB is TimeSpan))
				{
					throw new ArgumentException("Object must be of type TimeSpan.");
				}
				TimeSpan timeSpan = (TimeSpan)objA;
				TimeSpan timeSpan2 = (TimeSpan)objB;
				return timeSpan.CompareTo(timeSpan2);
			}
			}
			throw MiscellaneousUtils.CreateArgumentOutOfRangeException("valueType", valueType, "Unexpected value type: {0}".FormatWith(CultureInfo.InvariantCulture, valueType));
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0002CF5C File Offset: 0x0002B15C
		private static int CompareFloat(object objA, object objB)
		{
			double d = Convert.ToDouble(objA, CultureInfo.InvariantCulture);
			double num = Convert.ToDouble(objB, CultureInfo.InvariantCulture);
			if (MathUtils.ApproxEquals(d, num))
			{
				return 0;
			}
			return d.CompareTo(num);
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0002CF94 File Offset: 0x0002B194
		[NullableContext(2)]
		private static bool Operation(ExpressionType operation, object objA, object objB, out object result)
		{
			if ((objA is string || objB is string) && (operation == null || operation == 63))
			{
				result = ((objA != null) ? objA.ToString() : null) + ((objB != null) ? objB.ToString() : null);
				return true;
			}
			if (objA is ulong || objB is ulong || objA is decimal || objB is decimal)
			{
				if (objA == null || objB == null)
				{
					result = null;
					return true;
				}
				decimal num = Convert.ToDecimal(objA, CultureInfo.InvariantCulture);
				decimal num2 = Convert.ToDecimal(objB, CultureInfo.InvariantCulture);
				if (operation <= 42)
				{
					if (operation <= 12)
					{
						if (operation != null)
						{
							if (operation != 12)
							{
								goto IL_2D4;
							}
							goto IL_F8;
						}
					}
					else
					{
						if (operation == 26)
						{
							goto IL_E8;
						}
						if (operation != 42)
						{
							goto IL_2D4;
						}
						goto IL_D8;
					}
				}
				else if (operation <= 65)
				{
					if (operation != 63)
					{
						if (operation != 65)
						{
							goto IL_2D4;
						}
						goto IL_F8;
					}
				}
				else
				{
					if (operation == 69)
					{
						goto IL_E8;
					}
					if (operation != 73)
					{
						goto IL_2D4;
					}
					goto IL_D8;
				}
				result = num + num2;
				return true;
				IL_D8:
				result = num - num2;
				return true;
				IL_E8:
				result = num * num2;
				return true;
				IL_F8:
				result = num / num2;
				return true;
			}
			else if (objA is float || objB is float || objA is double || objB is double)
			{
				if (objA == null || objB == null)
				{
					result = null;
					return true;
				}
				double num3 = Convert.ToDouble(objA, CultureInfo.InvariantCulture);
				double num4 = Convert.ToDouble(objB, CultureInfo.InvariantCulture);
				if (operation <= 42)
				{
					if (operation <= 12)
					{
						if (operation != null)
						{
							if (operation != 12)
							{
								goto IL_2D4;
							}
							goto IL_1BB;
						}
					}
					else
					{
						if (operation == 26)
						{
							goto IL_1AF;
						}
						if (operation != 42)
						{
							goto IL_2D4;
						}
						goto IL_1A3;
					}
				}
				else if (operation <= 65)
				{
					if (operation != 63)
					{
						if (operation != 65)
						{
							goto IL_2D4;
						}
						goto IL_1BB;
					}
				}
				else
				{
					if (operation == 69)
					{
						goto IL_1AF;
					}
					if (operation != 73)
					{
						goto IL_2D4;
					}
					goto IL_1A3;
				}
				result = num3 + num4;
				return true;
				IL_1A3:
				result = num3 - num4;
				return true;
				IL_1AF:
				result = num3 * num4;
				return true;
				IL_1BB:
				result = num3 / num4;
				return true;
			}
			else if (objA is int || objA is uint || objA is long || objA is short || objA is ushort || objA is sbyte || objA is byte || objB is int || objB is uint || objB is long || objB is short || objB is ushort || objB is sbyte || objB is byte)
			{
				if (objA == null || objB == null)
				{
					result = null;
					return true;
				}
				long num5 = Convert.ToInt64(objA, CultureInfo.InvariantCulture);
				long num6 = Convert.ToInt64(objB, CultureInfo.InvariantCulture);
				if (operation <= 42)
				{
					if (operation <= 12)
					{
						if (operation != null)
						{
							if (operation != 12)
							{
								goto IL_2D4;
							}
							goto IL_2C6;
						}
					}
					else
					{
						if (operation == 26)
						{
							goto IL_2B8;
						}
						if (operation != 42)
						{
							goto IL_2D4;
						}
						goto IL_2AA;
					}
				}
				else if (operation <= 65)
				{
					if (operation != 63)
					{
						if (operation != 65)
						{
							goto IL_2D4;
						}
						goto IL_2C6;
					}
				}
				else
				{
					if (operation == 69)
					{
						goto IL_2B8;
					}
					if (operation != 73)
					{
						goto IL_2D4;
					}
					goto IL_2AA;
				}
				result = num5 + num6;
				return true;
				IL_2AA:
				result = num5 - num6;
				return true;
				IL_2B8:
				result = num5 * num6;
				return true;
				IL_2C6:
				result = num5 / num6;
				return true;
			}
			IL_2D4:
			result = null;
			return false;
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0002D279 File Offset: 0x0002B479
		internal override JToken CloneToken([Nullable(2)] JsonCloneSettings settings)
		{
			return new JValue(this, settings);
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0002D282 File Offset: 0x0002B482
		public static JValue CreateComment([Nullable(2)] string value)
		{
			return new JValue(value, JTokenType.Comment);
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0002D28B File Offset: 0x0002B48B
		public static JValue CreateString([Nullable(2)] string value)
		{
			return new JValue(value, JTokenType.String);
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0002D294 File Offset: 0x0002B494
		public static JValue CreateNull()
		{
			return new JValue(null, JTokenType.Null);
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x0002D29E File Offset: 0x0002B49E
		public static JValue CreateUndefined()
		{
			return new JValue(null, JTokenType.Undefined);
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0002D2A8 File Offset: 0x0002B4A8
		[NullableContext(2)]
		private static JTokenType GetValueType(JTokenType? current, object value)
		{
			if (value == null)
			{
				return JTokenType.Null;
			}
			if (value is string)
			{
				return JValue.GetStringValueType(current);
			}
			if (value is long || value is int || value is short || value is sbyte || value is ulong || value is uint || value is ushort || value is byte)
			{
				return JTokenType.Integer;
			}
			if (value is Enum)
			{
				return JTokenType.Integer;
			}
			if (value is double || value is float || value is decimal)
			{
				return JTokenType.Float;
			}
			if (value is DateTime)
			{
				return JTokenType.Date;
			}
			if (value is DateTimeOffset)
			{
				return JTokenType.Date;
			}
			if (value is byte[])
			{
				return JTokenType.Bytes;
			}
			if (value is bool)
			{
				return JTokenType.Boolean;
			}
			if (value is Guid)
			{
				return JTokenType.Guid;
			}
			if (value is Uri)
			{
				return JTokenType.Uri;
			}
			if (value is TimeSpan)
			{
				return JTokenType.TimeSpan;
			}
			throw new ArgumentException("Could not determine JSON object type for type {0}.".FormatWith(CultureInfo.InvariantCulture, value.GetType()));
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0002D398 File Offset: 0x0002B598
		private static JTokenType GetStringValueType(JTokenType? current)
		{
			if (current == null)
			{
				return JTokenType.String;
			}
			JTokenType valueOrDefault = current.GetValueOrDefault();
			if (valueOrDefault == JTokenType.Comment || valueOrDefault == JTokenType.String || valueOrDefault == JTokenType.Raw)
			{
				return current.GetValueOrDefault();
			}
			return JTokenType.String;
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x0002D3CE File Offset: 0x0002B5CE
		public override JTokenType Type
		{
			get
			{
				return this._valueType;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000B95 RID: 2965 RVA: 0x0002D3D6 File Offset: 0x0002B5D6
		// (set) Token: 0x06000B96 RID: 2966 RVA: 0x0002D3E0 File Offset: 0x0002B5E0
		[Nullable(2)]
		public new object Value
		{
			[NullableContext(2)]
			get
			{
				return this._value;
			}
			[NullableContext(2)]
			set
			{
				object value2 = this._value;
				Type type = (value2 != null) ? value2.GetType() : null;
				Type type2 = (value != null) ? value.GetType() : null;
				if (type != type2)
				{
					this._valueType = JValue.GetValueType(new JTokenType?(this._valueType), value);
				}
				this._value = value;
			}
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x0002D430 File Offset: 0x0002B630
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			if (converters != null && converters.Length != 0 && this._value != null)
			{
				JsonConverter matchingConverter = JsonSerializer.GetMatchingConverter(converters, this._value.GetType());
				if (matchingConverter != null && matchingConverter.CanWrite)
				{
					matchingConverter.WriteJson(writer, this._value, JsonSerializer.CreateDefault());
					return;
				}
			}
			switch (this._valueType)
			{
			case JTokenType.Comment:
			{
				object value = this._value;
				writer.WriteComment((value != null) ? value.ToString() : null);
				return;
			}
			case JTokenType.Integer:
			{
				object value2 = this._value;
				if (value2 is int)
				{
					int value3 = (int)value2;
					writer.WriteValue(value3);
					return;
				}
				value2 = this._value;
				if (value2 is long)
				{
					long value4 = (long)value2;
					writer.WriteValue(value4);
					return;
				}
				value2 = this._value;
				if (value2 is ulong)
				{
					ulong value5 = (ulong)value2;
					writer.WriteValue(value5);
					return;
				}
				writer.WriteValue(Convert.ToInt64(this._value, CultureInfo.InvariantCulture));
				return;
			}
			case JTokenType.Float:
			{
				object value2 = this._value;
				if (value2 is decimal)
				{
					decimal value6 = (decimal)value2;
					writer.WriteValue(value6);
					return;
				}
				value2 = this._value;
				if (value2 is double)
				{
					double value7 = (double)value2;
					writer.WriteValue(value7);
					return;
				}
				value2 = this._value;
				if (value2 is float)
				{
					float value8 = (float)value2;
					writer.WriteValue(value8);
					return;
				}
				writer.WriteValue(Convert.ToDouble(this._value, CultureInfo.InvariantCulture));
				return;
			}
			case JTokenType.String:
			{
				object value9 = this._value;
				writer.WriteValue((value9 != null) ? value9.ToString() : null);
				return;
			}
			case JTokenType.Boolean:
				writer.WriteValue(Convert.ToBoolean(this._value, CultureInfo.InvariantCulture));
				return;
			case JTokenType.Null:
				writer.WriteNull();
				return;
			case JTokenType.Undefined:
				writer.WriteUndefined();
				return;
			case JTokenType.Date:
			{
				object value2 = this._value;
				if (value2 is DateTimeOffset)
				{
					DateTimeOffset value10 = (DateTimeOffset)value2;
					writer.WriteValue(value10);
					return;
				}
				writer.WriteValue(Convert.ToDateTime(this._value, CultureInfo.InvariantCulture));
				return;
			}
			case JTokenType.Raw:
			{
				object value11 = this._value;
				writer.WriteRawValue((value11 != null) ? value11.ToString() : null);
				return;
			}
			case JTokenType.Bytes:
				writer.WriteValue((byte[])this._value);
				return;
			case JTokenType.Guid:
				writer.WriteValue((this._value != null) ? ((Guid?)this._value) : default(Guid?));
				return;
			case JTokenType.Uri:
				writer.WriteValue((Uri)this._value);
				return;
			case JTokenType.TimeSpan:
				writer.WriteValue((this._value != null) ? ((TimeSpan?)this._value) : default(TimeSpan?));
				return;
			default:
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException("Type", this._valueType, "Unexpected token type.");
			}
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x0002D6F4 File Offset: 0x0002B8F4
		internal override int GetDeepHashCode()
		{
			int num = (this._value != null) ? this._value.GetHashCode() : 0;
			int valueType = (int)this._valueType;
			return valueType.GetHashCode() ^ num;
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0002D728 File Offset: 0x0002B928
		private static bool ValuesEquals(JValue v1, JValue v2)
		{
			return v1 == v2 || (v1._valueType == v2._valueType && JValue.Compare(v1._valueType, v1._value, v2._value) == 0);
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x0002D75A File Offset: 0x0002B95A
		[NullableContext(2)]
		public bool Equals(JValue other)
		{
			return other != null && JValue.ValuesEquals(this, other);
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x0002D768 File Offset: 0x0002B968
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			JValue jvalue = obj as JValue;
			return jvalue != null && this.Equals(jvalue);
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0002D788 File Offset: 0x0002B988
		public override int GetHashCode()
		{
			if (this._value == null)
			{
				return 0;
			}
			return this._value.GetHashCode();
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0002D79F File Offset: 0x0002B99F
		public override string ToString()
		{
			if (this._value == null)
			{
				return string.Empty;
			}
			return this._value.ToString();
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x0002D7BA File Offset: 0x0002B9BA
		public string ToString(string format)
		{
			return this.ToString(format, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x0002D7C8 File Offset: 0x0002B9C8
		public string ToString([Nullable(2)] IFormatProvider formatProvider)
		{
			return this.ToString(null, formatProvider);
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0002D7D4 File Offset: 0x0002B9D4
		[NullableContext(2)]
		[return: Nullable(1)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			if (this._value == null)
			{
				return string.Empty;
			}
			IFormattable formattable = this._value as IFormattable;
			if (formattable != null)
			{
				return formattable.ToString(format, formatProvider);
			}
			return this._value.ToString();
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0002D812 File Offset: 0x0002BA12
		protected override DynamicMetaObject GetMetaObject(Expression parameter)
		{
			return new DynamicProxyMetaObject<JValue>(parameter, this, new JValue.JValueDynamicProxy());
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0002D820 File Offset: 0x0002BA20
		[NullableContext(2)]
		int IComparable.CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			JValue jvalue = obj as JValue;
			object objB;
			JTokenType valueType;
			if (jvalue != null)
			{
				objB = jvalue.Value;
				valueType = ((this._valueType == JTokenType.String && this._valueType != jvalue._valueType) ? jvalue._valueType : this._valueType);
			}
			else
			{
				objB = obj;
				valueType = this._valueType;
			}
			return JValue.Compare(valueType, this._value, objB);
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0002D881 File Offset: 0x0002BA81
		[NullableContext(2)]
		public int CompareTo(JValue obj)
		{
			if (obj == null)
			{
				return 1;
			}
			return JValue.Compare((this._valueType == JTokenType.String && this._valueType != obj._valueType) ? obj._valueType : this._valueType, this._value, obj._value);
		}

		// Token: 0x040003F1 RID: 1009
		private JTokenType _valueType;

		// Token: 0x040003F2 RID: 1010
		[Nullable(2)]
		private object _value;

		// Token: 0x020001E1 RID: 481
		[Nullable(new byte[]
		{
			0,
			1
		})]
		[RequiresUnreferencedCode("Newtonsoft.Json relies on reflection over types that may be removed when trimming.")]
		[RequiresDynamicCode("Newtonsoft.Json relies on dynamically creating types that may not be available with Ahead of Time compilation.")]
		private class JValueDynamicProxy : DynamicProxy<JValue>
		{
			// Token: 0x06000F82 RID: 3970 RVA: 0x00043AE4 File Offset: 0x00041CE4
			public override bool TryConvert(JValue instance, ConvertBinder binder, [Nullable(2)] [NotNullWhen(true)] out object result)
			{
				if (binder.Type == typeof(JValue) || binder.Type == typeof(JToken))
				{
					result = instance;
					return true;
				}
				object value = instance.Value;
				if (value == null)
				{
					result = null;
					return ReflectionUtils.IsNullable(binder.Type);
				}
				result = ConvertUtils.Convert(value, CultureInfo.InvariantCulture, binder.Type);
				return true;
			}

			// Token: 0x06000F83 RID: 3971 RVA: 0x00043B48 File Offset: 0x00041D48
			public override bool TryBinaryOperation(JValue instance, BinaryOperationBinder binder, object arg, [Nullable(2)] [NotNullWhen(true)] out object result)
			{
				JValue jvalue = arg as JValue;
				object objB = (jvalue != null) ? jvalue.Value : arg;
				ExpressionType operation = binder.Operation;
				if (operation <= 35)
				{
					if (operation <= 21)
					{
						if (operation != null)
						{
							switch (operation)
							{
							case 12:
								break;
							case 13:
								result = (JValue.Compare(instance.Type, instance.Value, objB) == 0);
								return true;
							case 14:
							case 17:
							case 18:
							case 19:
								goto IL_18D;
							case 15:
								result = (JValue.Compare(instance.Type, instance.Value, objB) > 0);
								return true;
							case 16:
								result = (JValue.Compare(instance.Type, instance.Value, objB) >= 0);
								return true;
							case 20:
								result = (JValue.Compare(instance.Type, instance.Value, objB) < 0);
								return true;
							case 21:
								result = (JValue.Compare(instance.Type, instance.Value, objB) <= 0);
								return true;
							default:
								goto IL_18D;
							}
						}
					}
					else if (operation != 26)
					{
						if (operation != 35)
						{
							goto IL_18D;
						}
						result = (JValue.Compare(instance.Type, instance.Value, objB) != 0);
						return true;
					}
				}
				else if (operation <= 63)
				{
					if (operation != 42 && operation != 63)
					{
						goto IL_18D;
					}
				}
				else if (operation != 65 && operation != 69 && operation != 73)
				{
					goto IL_18D;
				}
				if (JValue.Operation(binder.Operation, instance.Value, objB, out result))
				{
					result = new JValue(result);
					return true;
				}
				IL_18D:
				result = null;
				return false;
			}
		}
	}
}
