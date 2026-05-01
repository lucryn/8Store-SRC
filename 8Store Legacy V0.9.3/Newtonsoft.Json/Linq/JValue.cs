using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq.Expressions;
using System.Numerics;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	/// <summary>
	/// Represents a value in JSON (string, integer, date, etc).
	/// </summary>
	// Token: 0x02000061 RID: 97
	public class JValue : JToken, IEquatable<JValue>, IFormattable, IComparable, IComparable<JValue>
	{
		// Token: 0x06000566 RID: 1382 RVA: 0x00014514 File Offset: 0x00012714
		internal JValue(object value, JTokenType type)
		{
			this._value = value;
			this._valueType = type;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Linq.JValue" /> class from another <see cref="T:Newtonsoft.Json.Linq.JValue" /> object.
		/// </summary>
		/// <param name="other">A <see cref="T:Newtonsoft.Json.Linq.JValue" /> object to copy from.</param>
		// Token: 0x06000567 RID: 1383 RVA: 0x0001452A File Offset: 0x0001272A
		public JValue(JValue other) : this(other.Value, other.Type)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Linq.JValue" /> class with the given value.
		/// </summary>
		/// <param name="value">The value.</param>
		// Token: 0x06000568 RID: 1384 RVA: 0x0001453E File Offset: 0x0001273E
		public JValue(long value) : this(value, JTokenType.Integer)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Linq.JValue" /> class with the given value.
		/// </summary>
		/// <param name="value">The value.</param>
		// Token: 0x06000569 RID: 1385 RVA: 0x0001454D File Offset: 0x0001274D
		public JValue(char value) : this(value, JTokenType.String)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Linq.JValue" /> class with the given value.
		/// </summary>
		/// <param name="value">The value.</param>
		// Token: 0x0600056A RID: 1386 RVA: 0x0001455C File Offset: 0x0001275C
		[CLSCompliant(false)]
		public JValue(ulong value) : this(value, JTokenType.Integer)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Linq.JValue" /> class with the given value.
		/// </summary>
		/// <param name="value">The value.</param>
		// Token: 0x0600056B RID: 1387 RVA: 0x0001456B File Offset: 0x0001276B
		public JValue(double value) : this(value, JTokenType.Float)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Linq.JValue" /> class with the given value.
		/// </summary>
		/// <param name="value">The value.</param>
		// Token: 0x0600056C RID: 1388 RVA: 0x0001457A File Offset: 0x0001277A
		public JValue(float value) : this(value, JTokenType.Float)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Linq.JValue" /> class with the given value.
		/// </summary>
		/// <param name="value">The value.</param>
		// Token: 0x0600056D RID: 1389 RVA: 0x00014589 File Offset: 0x00012789
		public JValue(DateTime value) : this(value, JTokenType.Date)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Linq.JValue" /> class with the given value.
		/// </summary>
		/// <param name="value">The value.</param>
		// Token: 0x0600056E RID: 1390 RVA: 0x00014599 File Offset: 0x00012799
		public JValue(bool value) : this(value, JTokenType.Boolean)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Linq.JValue" /> class with the given value.
		/// </summary>
		/// <param name="value">The value.</param>
		// Token: 0x0600056F RID: 1391 RVA: 0x000145A9 File Offset: 0x000127A9
		public JValue(string value) : this(value, JTokenType.String)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Linq.JValue" /> class with the given value.
		/// </summary>
		/// <param name="value">The value.</param>
		// Token: 0x06000570 RID: 1392 RVA: 0x000145B3 File Offset: 0x000127B3
		public JValue(Guid value) : this(value, JTokenType.Guid)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Linq.JValue" /> class with the given value.
		/// </summary>
		/// <param name="value">The value.</param>
		// Token: 0x06000571 RID: 1393 RVA: 0x000145C3 File Offset: 0x000127C3
		public JValue(Uri value) : this(value, (value != null) ? JTokenType.Uri : JTokenType.Null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Linq.JValue" /> class with the given value.
		/// </summary>
		/// <param name="value">The value.</param>
		// Token: 0x06000572 RID: 1394 RVA: 0x000145DB File Offset: 0x000127DB
		public JValue(TimeSpan value) : this(value, JTokenType.TimeSpan)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Newtonsoft.Json.Linq.JValue" /> class with the given value.
		/// </summary>
		/// <param name="value">The value.</param>
		// Token: 0x06000573 RID: 1395 RVA: 0x000145EC File Offset: 0x000127EC
		public JValue(object value) : this(value, JValue.GetValueType(default(JTokenType?), value))
		{
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00014610 File Offset: 0x00012810
		internal override bool DeepEquals(JToken node)
		{
			JValue jvalue = node as JValue;
			return jvalue != null && (jvalue == this || JValue.ValuesEquals(this, jvalue));
		}

		/// <summary>
		/// Gets a value indicating whether this token has childen tokens.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this token has child values; otherwise, <c>false</c>.
		/// </value>
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x00014636 File Offset: 0x00012836
		public override bool HasValues
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0001463C File Offset: 0x0001283C
		private static int CompareBigInteger(BigInteger i1, object i2)
		{
			int num = i1.CompareTo(ConvertUtils.ToBigInteger(i2));
			if (num != 0)
			{
				return num;
			}
			if (i2 is decimal)
			{
				decimal num2 = (decimal)i2;
				return 0m.CompareTo(Math.Abs(num2 - Math.Truncate(num2)));
			}
			if (i2 is double || i2 is float)
			{
				double num3 = Convert.ToDouble(i2, CultureInfo.InvariantCulture);
				return 0.0.CompareTo(Math.Abs(num3 - Math.Truncate(num3)));
			}
			return num;
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x000146C8 File Offset: 0x000128C8
		internal static int Compare(JTokenType valueType, object objA, object objB)
		{
			if (objA == null && objB == null)
			{
				return 0;
			}
			if (objA != null && objB == null)
			{
				return 1;
			}
			if (objA == null && objB != null)
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
				if (objA is BigInteger)
				{
					return JValue.CompareBigInteger((BigInteger)objA, objB);
				}
				if (objB is BigInteger)
				{
					return -JValue.CompareBigInteger((BigInteger)objB, objA);
				}
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
				if (objA is BigInteger)
				{
					return JValue.CompareBigInteger((BigInteger)objA, objB);
				}
				if (objB is BigInteger)
				{
					return -JValue.CompareBigInteger((BigInteger)objB, objA);
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
				if (!(objB is byte[]))
				{
					throw new ArgumentException("Object must be of type byte[].");
				}
				byte[] array = objA as byte[];
				byte[] array2 = objB as byte[];
				if (array == null)
				{
					return -1;
				}
				if (array2 == null)
				{
					return 1;
				}
				return MiscellaneousUtils.ByteArrayCompare(array, array2);
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
				if (!(objB is Uri))
				{
					throw new ArgumentException("Object must be of type Uri.");
				}
				Uri uri = (Uri)objA;
				Uri uri2 = (Uri)objB;
				return Comparer<string>.Default.Compare(uri.ToString(), uri2.ToString());
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

		// Token: 0x06000578 RID: 1400 RVA: 0x000149C8 File Offset: 0x00012BC8
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

		// Token: 0x06000579 RID: 1401 RVA: 0x00014A00 File Offset: 0x00012C00
		private static bool Operation(ExpressionType operation, object objA, object objB, out object result)
		{
			if ((objA is string || objB is string) && (operation == null || operation == 63))
			{
				result = ((objA != null) ? objA.ToString() : null) + ((objB != null) ? objB.ToString() : null);
				return true;
			}
			if (objA is BigInteger || objB is BigInteger)
			{
				if (objA == null || objB == null)
				{
					result = null;
					return true;
				}
				BigInteger bigInteger = ConvertUtils.ToBigInteger(objA);
				BigInteger bigInteger2 = ConvertUtils.ToBigInteger(objB);
				if (operation > 26)
				{
					if (operation <= 65)
					{
						if (operation != 42)
						{
							switch (operation)
							{
							case 63:
								goto IL_BC;
							case 64:
								goto IL_3CE;
							case 65:
								goto IL_EC;
							default:
								goto IL_3CE;
							}
						}
					}
					else
					{
						if (operation == 69)
						{
							goto IL_DC;
						}
						if (operation != 73)
						{
							goto IL_3CE;
						}
					}
					result = bigInteger - bigInteger2;
					return true;
				}
				if (operation != 0)
				{
					if (operation == 12)
					{
						goto IL_EC;
					}
					if (operation != 26)
					{
						goto IL_3CE;
					}
					goto IL_DC;
				}
				IL_BC:
				result = bigInteger + bigInteger2;
				return true;
				IL_DC:
				result = bigInteger * bigInteger2;
				return true;
				IL_EC:
				result = bigInteger / bigInteger2;
				return true;
			}
			else if (objA is ulong || objB is ulong || objA is decimal || objB is decimal)
			{
				if (objA == null || objB == null)
				{
					result = null;
					return true;
				}
				decimal num = Convert.ToDecimal(objA, CultureInfo.InvariantCulture);
				decimal num2 = Convert.ToDecimal(objB, CultureInfo.InvariantCulture);
				if (operation > 26)
				{
					if (operation <= 65)
					{
						if (operation != 42)
						{
							switch (operation)
							{
							case 63:
								goto IL_199;
							case 64:
								goto IL_3CE;
							case 65:
								goto IL_1C9;
							default:
								goto IL_3CE;
							}
						}
					}
					else
					{
						if (operation == 69)
						{
							goto IL_1B9;
						}
						if (operation != 73)
						{
							goto IL_3CE;
						}
					}
					result = num - num2;
					return true;
				}
				if (operation != 0)
				{
					if (operation == 12)
					{
						goto IL_1C9;
					}
					if (operation != 26)
					{
						goto IL_3CE;
					}
					goto IL_1B9;
				}
				IL_199:
				result = num + num2;
				return true;
				IL_1B9:
				result = num * num2;
				return true;
				IL_1C9:
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
				if (operation > 26)
				{
					if (operation <= 65)
					{
						if (operation != 42)
						{
							switch (operation)
							{
							case 63:
								goto IL_278;
							case 64:
								goto IL_3CE;
							case 65:
								goto IL_2A2;
							default:
								goto IL_3CE;
							}
						}
					}
					else
					{
						if (operation == 69)
						{
							goto IL_294;
						}
						if (operation != 73)
						{
							goto IL_3CE;
						}
					}
					result = num3 - num4;
					return true;
				}
				if (operation != 0)
				{
					if (operation == 12)
					{
						goto IL_2A2;
					}
					if (operation != 26)
					{
						goto IL_3CE;
					}
					goto IL_294;
				}
				IL_278:
				result = num3 + num4;
				return true;
				IL_294:
				result = num3 * num4;
				return true;
				IL_2A2:
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
				if (operation > 26)
				{
					if (operation <= 65)
					{
						if (operation != 42)
						{
							switch (operation)
							{
							case 63:
								goto IL_396;
							case 64:
								goto IL_3CE;
							case 65:
								goto IL_3C0;
							default:
								goto IL_3CE;
							}
						}
					}
					else
					{
						if (operation == 69)
						{
							goto IL_3B2;
						}
						if (operation != 73)
						{
							goto IL_3CE;
						}
					}
					result = num5 - num6;
					return true;
				}
				if (operation != 0)
				{
					if (operation == 12)
					{
						goto IL_3C0;
					}
					if (operation != 26)
					{
						goto IL_3CE;
					}
					goto IL_3B2;
				}
				IL_396:
				result = num5 + num6;
				return true;
				IL_3B2:
				result = num5 * num6;
				return true;
				IL_3C0:
				result = num5 / num6;
				return true;
			}
			IL_3CE:
			result = null;
			return false;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00014DDF File Offset: 0x00012FDF
		internal override JToken CloneToken()
		{
			return new JValue(this);
		}

		/// <summary>
		/// Creates a <see cref="T:Newtonsoft.Json.Linq.JValue" /> comment with the given value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>A <see cref="T:Newtonsoft.Json.Linq.JValue" /> comment with the given value.</returns>
		// Token: 0x0600057B RID: 1403 RVA: 0x00014DE7 File Offset: 0x00012FE7
		public static JValue CreateComment(string value)
		{
			return new JValue(value, JTokenType.Comment);
		}

		/// <summary>
		/// Creates a <see cref="T:Newtonsoft.Json.Linq.JValue" /> string with the given value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>A <see cref="T:Newtonsoft.Json.Linq.JValue" /> string with the given value.</returns>
		// Token: 0x0600057C RID: 1404 RVA: 0x00014DF0 File Offset: 0x00012FF0
		public static JValue CreateString(string value)
		{
			return new JValue(value, JTokenType.String);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00014DFC File Offset: 0x00012FFC
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
			if (value is BigInteger)
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

		// Token: 0x0600057E RID: 1406 RVA: 0x00014EF8 File Offset: 0x000130F8
		private static JTokenType GetStringValueType(JTokenType? current)
		{
			if (current == null)
			{
				return JTokenType.String;
			}
			JTokenType value = current.Value;
			if (value == JTokenType.Comment || value == JTokenType.String || value == JTokenType.Raw)
			{
				return current.Value;
			}
			return JTokenType.String;
		}

		/// <summary>
		/// Gets the node type for this <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// </summary>
		/// <value>The type.</value>
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x00014F2E File Offset: 0x0001312E
		public override JTokenType Type
		{
			get
			{
				return this._valueType;
			}
		}

		/// <summary>
		/// Gets or sets the underlying token value.
		/// </summary>
		/// <value>The underlying token value.</value>
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x00014F36 File Offset: 0x00013136
		// (set) Token: 0x06000581 RID: 1409 RVA: 0x00014F40 File Offset: 0x00013140
		public new object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				Type type = (this._value != null) ? this._value.GetType() : null;
				Type type2 = (value != null) ? value.GetType() : null;
				if (type != type2)
				{
					this._valueType = JValue.GetValueType(new JTokenType?(this._valueType), value);
				}
				this._value = value;
			}
		}

		/// <summary>
		/// Writes this token to a <see cref="T:Newtonsoft.Json.JsonWriter" />.
		/// </summary>
		/// <param name="writer">A <see cref="T:Newtonsoft.Json.JsonWriter" /> into which this method will write.</param>
		/// <param name="converters">A collection of <see cref="T:Newtonsoft.Json.JsonConverter" /> which will be used when writing the token.</param>
		// Token: 0x06000582 RID: 1410 RVA: 0x00014F94 File Offset: 0x00013194
		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			if (converters != null && converters.Length > 0 && this._value != null)
			{
				JsonConverter matchingConverter = JsonSerializer.GetMatchingConverter(converters, this._value.GetType());
				if (matchingConverter != null)
				{
					matchingConverter.WriteJson(writer, this._value, JsonSerializer.CreateDefault());
					return;
				}
			}
			switch (this._valueType)
			{
			case JTokenType.Comment:
				writer.WriteComment((this._value != null) ? this._value.ToString() : null);
				return;
			case JTokenType.Integer:
				if (this._value is BigInteger)
				{
					writer.WriteValue((BigInteger)this._value);
					return;
				}
				writer.WriteValue(Convert.ToInt64(this._value, CultureInfo.InvariantCulture));
				return;
			case JTokenType.Float:
				if (this._value is decimal)
				{
					writer.WriteValue((decimal)this._value);
					return;
				}
				if (this._value is double)
				{
					writer.WriteValue((double)this._value);
					return;
				}
				if (this._value is float)
				{
					writer.WriteValue((float)this._value);
					return;
				}
				writer.WriteValue(Convert.ToDouble(this._value, CultureInfo.InvariantCulture));
				return;
			case JTokenType.String:
				writer.WriteValue((this._value != null) ? this._value.ToString() : null);
				return;
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
				if (this._value is DateTimeOffset)
				{
					writer.WriteValue((DateTimeOffset)this._value);
					return;
				}
				writer.WriteValue(Convert.ToDateTime(this._value, CultureInfo.InvariantCulture));
				return;
			case JTokenType.Raw:
				writer.WriteRawValue((this._value != null) ? this._value.ToString() : null);
				return;
			case JTokenType.Bytes:
				writer.WriteValue((byte[])this._value);
				return;
			case JTokenType.Guid:
			case JTokenType.Uri:
			case JTokenType.TimeSpan:
				writer.WriteValue((this._value != null) ? this._value.ToString() : null);
				return;
			default:
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException("TokenType", this._valueType, "Unexpected token type.");
			}
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x000151CC File Offset: 0x000133CC
		internal override int GetDeepHashCode()
		{
			int num = (this._value != null) ? this._value.GetHashCode() : 0;
			return this._valueType.GetHashCode() ^ num;
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00015202 File Offset: 0x00013402
		private static bool ValuesEquals(JValue v1, JValue v2)
		{
			return v1 == v2 || (v1._valueType == v2._valueType && JValue.Compare(v1._valueType, v1._value, v2._value) == 0);
		}

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		/// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		// Token: 0x06000585 RID: 1413 RVA: 0x00015234 File Offset: 0x00013434
		public bool Equals(JValue other)
		{
			return other != null && JValue.ValuesEquals(this, other);
		}

		/// <summary>
		/// Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.
		/// </summary>
		/// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />.</param>
		/// <returns>
		/// true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.
		/// </returns>
		/// <exception cref="T:System.NullReferenceException">
		/// The <paramref name="obj" /> parameter is null.
		/// </exception>
		// Token: 0x06000586 RID: 1414 RVA: 0x00015244 File Offset: 0x00013444
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			JValue jvalue = obj as JValue;
			if (jvalue != null)
			{
				return this.Equals(jvalue);
			}
			return base.Equals(obj);
		}

		/// <summary>
		/// Serves as a hash function for a particular type.
		/// </summary>
		/// <returns>
		/// A hash code for the current <see cref="T:System.Object" />.
		/// </returns>
		// Token: 0x06000587 RID: 1415 RVA: 0x0001526F File Offset: 0x0001346F
		public override int GetHashCode()
		{
			if (this._value == null)
			{
				return 0;
			}
			return this._value.GetHashCode();
		}

		/// <summary>
		/// Returns a <see cref="T:System.String" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String" /> that represents this instance.
		/// </returns>
		// Token: 0x06000588 RID: 1416 RVA: 0x00015286 File Offset: 0x00013486
		public override string ToString()
		{
			if (this._value == null)
			{
				return string.Empty;
			}
			return this._value.ToString();
		}

		/// <summary>
		/// Returns a <see cref="T:System.String" /> that represents this instance.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <returns>
		/// A <see cref="T:System.String" /> that represents this instance.
		/// </returns>
		// Token: 0x06000589 RID: 1417 RVA: 0x000152A1 File Offset: 0x000134A1
		public string ToString(string format)
		{
			return this.ToString(format, CultureInfo.CurrentCulture);
		}

		/// <summary>
		/// Returns a <see cref="T:System.String" /> that represents this instance.
		/// </summary>
		/// <param name="formatProvider">The format provider.</param>
		/// <returns>
		/// A <see cref="T:System.String" /> that represents this instance.
		/// </returns>
		// Token: 0x0600058A RID: 1418 RVA: 0x000152AF File Offset: 0x000134AF
		public string ToString(IFormatProvider formatProvider)
		{
			return this.ToString(null, formatProvider);
		}

		/// <summary>
		/// Returns a <see cref="T:System.String" /> that represents this instance.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="formatProvider">The format provider.</param>
		/// <returns>
		/// A <see cref="T:System.String" /> that represents this instance.
		/// </returns>
		// Token: 0x0600058B RID: 1419 RVA: 0x000152BC File Offset: 0x000134BC
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

		/// <summary>
		/// Returns the <see cref="T:System.Dynamic.DynamicMetaObject" /> responsible for binding operations performed on this object.
		/// </summary>
		/// <param name="parameter">The expression tree representation of the runtime value.</param>
		/// <returns>
		/// The <see cref="T:System.Dynamic.DynamicMetaObject" /> to bind this object.
		/// </returns>
		// Token: 0x0600058C RID: 1420 RVA: 0x000152FA File Offset: 0x000134FA
		protected override DynamicMetaObject GetMetaObject(Expression parameter)
		{
			return new DynamicProxyMetaObject<JValue>(parameter, this, new JValue.JValueDynamicProxy(), true);
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0001530C File Offset: 0x0001350C
		int IComparable.CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			object objB = (obj is JValue) ? ((JValue)obj).Value : obj;
			return JValue.Compare(this._valueType, this._value, objB);
		}

		/// <summary>
		/// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
		/// </summary>
		/// <param name="obj">An object to compare with this instance.</param>
		/// <returns>
		/// A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has these meanings:
		/// Value
		/// Meaning
		/// Less than zero
		/// This instance is less than <paramref name="obj" />.
		/// Zero
		/// This instance is equal to <paramref name="obj" />.
		/// Greater than zero
		/// This instance is greater than <paramref name="obj" />.
		/// </returns>
		/// <exception cref="T:System.ArgumentException">
		/// 	<paramref name="obj" /> is not the same type as this instance.
		/// </exception>
		// Token: 0x0600058E RID: 1422 RVA: 0x00015347 File Offset: 0x00013547
		public int CompareTo(JValue obj)
		{
			if (obj == null)
			{
				return 1;
			}
			return JValue.Compare(this._valueType, this._value, obj._value);
		}

		// Token: 0x040001A9 RID: 425
		private JTokenType _valueType;

		// Token: 0x040001AA RID: 426
		private object _value;

		// Token: 0x02000062 RID: 98
		private class JValueDynamicProxy : DynamicProxy<JValue>
		{
			// Token: 0x0600058F RID: 1423 RVA: 0x00015368 File Offset: 0x00013568
			public override bool TryConvert(JValue instance, ConvertBinder binder, out object result)
			{
				if (binder.Type == typeof(JValue))
				{
					result = instance;
					return true;
				}
				if (instance.Value == null)
				{
					result = null;
					return ReflectionUtils.IsNullable(binder.Type);
				}
				result = ConvertUtils.Convert(instance.Value, CultureInfo.InvariantCulture, binder.Type);
				return true;
			}

			// Token: 0x06000590 RID: 1424 RVA: 0x000153C0 File Offset: 0x000135C0
			public override bool TryBinaryOperation(JValue instance, BinaryOperationBinder binder, object arg, out object result)
			{
				object objB = (arg is JValue) ? ((JValue)arg).Value : arg;
				ExpressionType operation = binder.Operation;
				if (operation <= 35)
				{
					if (operation <= 21)
					{
						if (operation != 0)
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
								goto IL_199;
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
								goto IL_199;
							}
						}
					}
					else if (operation != 26)
					{
						if (operation != 35)
						{
							goto IL_199;
						}
						result = (JValue.Compare(instance.Type, instance.Value, objB) != 0);
						return true;
					}
				}
				else if (operation <= 65)
				{
					if (operation != 42)
					{
						switch (operation)
						{
						case 63:
						case 65:
							break;
						case 64:
							goto IL_199;
						default:
							goto IL_199;
						}
					}
				}
				else if (operation != 69 && operation != 73)
				{
					goto IL_199;
				}
				if (JValue.Operation(binder.Operation, instance.Value, objB, out result))
				{
					result = new JValue(result);
					return true;
				}
				IL_199:
				result = null;
				return false;
			}
		}
	}
}
