using System;
using System.Globalization;
using System.IO;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	/// <summary>
	/// Provides methods for converting between common language runtime types and JSON types.
	/// </summary>
	/// <example>
	///   <code lang="cs" source="..\Src\Newtonsoft.Json.Tests\Documentation\SerializationTests.cs" region="SerializeObject" title="Serializing and Deserializing JSON with JsonConvert" />
	/// </example>
	// Token: 0x0200003D RID: 61
	public static class JsonConvert
	{
		/// <summary>
		/// Gets or sets a function that creates default <see cref="T:Newtonsoft.Json.JsonSerializerSettings" />.
		/// Default settings are automatically used by serialization methods on <see cref="T:Newtonsoft.Json.JsonConvert" />,
		/// and <see cref="M:Newtonsoft.Json.Linq.JToken.ToObject``1" /> and <see cref="M:Newtonsoft.Json.Linq.JToken.FromObject(System.Object)" /> on <see cref="T:Newtonsoft.Json.Linq.JToken" />.
		/// To serialize without using any default settings create a <see cref="T:Newtonsoft.Json.JsonSerializer" /> with
		/// <see cref="M:Newtonsoft.Json.JsonSerializer.Create" />.
		/// </summary>
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00008F08 File Offset: 0x00007108
		// (set) Token: 0x06000219 RID: 537 RVA: 0x00008F0F File Offset: 0x0000710F
		public static Func<JsonSerializerSettings> DefaultSettings { get; set; }

		/// <summary>
		/// Converts the <see cref="T:System.DateTime" /> to its JSON string representation.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>A JSON string representation of the <see cref="T:System.DateTime" />.</returns>
		// Token: 0x0600021A RID: 538 RVA: 0x00008F17 File Offset: 0x00007117
		public static string ToString(DateTime value)
		{
			return JsonConvert.ToString(value, DateFormatHandling.IsoDateFormat, DateTimeZoneHandling.RoundtripKind);
		}

		/// <summary>
		/// Converts the <see cref="T:System.DateTime" /> to its JSON string representation using the <see cref="T:Newtonsoft.Json.DateFormatHandling" /> specified.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <param name="format">The format the date will be converted to.</param>
		/// <param name="timeZoneHandling">The time zone handling when the date is converted to a string.</param>
		/// <returns>A JSON string representation of the <see cref="T:System.DateTime" />.</returns>
		// Token: 0x0600021B RID: 539 RVA: 0x00008F24 File Offset: 0x00007124
		public static string ToString(DateTime value, DateFormatHandling format, DateTimeZoneHandling timeZoneHandling)
		{
			DateTime value2 = DateTimeUtils.EnsureDateTime(value, timeZoneHandling);
			string result;
			using (StringWriter stringWriter = StringUtils.CreateStringWriter(64))
			{
				stringWriter.Write('"');
				DateTimeUtils.WriteDateTimeString(stringWriter, value2, format, null, CultureInfo.InvariantCulture);
				stringWriter.Write('"');
				result = stringWriter.ToString();
			}
			return result;
		}

		/// <summary>
		/// Converts the <see cref="T:System.DateTimeOffset" /> to its JSON string representation.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>A JSON string representation of the <see cref="T:System.DateTimeOffset" />.</returns>
		// Token: 0x0600021C RID: 540 RVA: 0x00008F84 File Offset: 0x00007184
		public static string ToString(DateTimeOffset value)
		{
			return JsonConvert.ToString(value, DateFormatHandling.IsoDateFormat);
		}

		/// <summary>
		/// Converts the <see cref="T:System.DateTimeOffset" /> to its JSON string representation using the <see cref="T:Newtonsoft.Json.DateFormatHandling" /> specified.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <param name="format">The format the date will be converted to.</param>
		/// <returns>A JSON string representation of the <see cref="T:System.DateTimeOffset" />.</returns>
		// Token: 0x0600021D RID: 541 RVA: 0x00008F90 File Offset: 0x00007190
		public static string ToString(DateTimeOffset value, DateFormatHandling format)
		{
			string result;
			using (StringWriter stringWriter = StringUtils.CreateStringWriter(64))
			{
				stringWriter.Write('"');
				DateTimeUtils.WriteDateTimeOffsetString(stringWriter, value, format, null, CultureInfo.InvariantCulture);
				stringWriter.Write('"');
				result = stringWriter.ToString();
			}
			return result;
		}

		/// <summary>
		/// Converts the <see cref="T:System.Boolean" /> to its JSON string representation.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>A JSON string representation of the <see cref="T:System.Boolean" />.</returns>
		// Token: 0x0600021E RID: 542 RVA: 0x00008FE8 File Offset: 0x000071E8
		public static string ToString(bool value)
		{
			if (!value)
			{
				return JsonConvert.False;
			}
			return JsonConvert.True;
		}

		/// <summary>
		/// Converts the <see cref="T:System.Char" /> to its JSON string representation.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>A JSON string representation of the <see cref="T:System.Char" />.</returns>
		// Token: 0x0600021F RID: 543 RVA: 0x00008FF8 File Offset: 0x000071F8
		public static string ToString(char value)
		{
			return JsonConvert.ToString(char.ToString(value));
		}

		/// <summary>
		/// Converts the <see cref="T:System.Enum" /> to its JSON string representation.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>A JSON string representation of the <see cref="T:System.Enum" />.</returns>
		// Token: 0x06000220 RID: 544 RVA: 0x00009005 File Offset: 0x00007205
		public static string ToString(Enum value)
		{
			return value.ToString("D");
		}

		/// <summary>
		/// Converts the <see cref="T:System.Int32" /> to its JSON string representation.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>A JSON string representation of the <see cref="T:System.Int32" />.</returns>
		// Token: 0x06000221 RID: 545 RVA: 0x00009012 File Offset: 0x00007212
		public static string ToString(int value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Converts the <see cref="T:System.Int16" /> to its JSON string representation.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>A JSON string representation of the <see cref="T:System.Int16" />.</returns>
		// Token: 0x06000222 RID: 546 RVA: 0x00009021 File Offset: 0x00007221
		public static string ToString(short value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Converts the <see cref="T:System.UInt16" /> to its JSON string representation.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>A JSON string representation of the <see cref="T:System.UInt16" />.</returns>
		// Token: 0x06000223 RID: 547 RVA: 0x00009030 File Offset: 0x00007230
		[CLSCompliant(false)]
		public static string ToString(ushort value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Converts the <see cref="T:System.UInt32" /> to its JSON string representation.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>A JSON string representation of the <see cref="T:System.UInt32" />.</returns>
		// Token: 0x06000224 RID: 548 RVA: 0x0000903F File Offset: 0x0000723F
		[CLSCompliant(false)]
		public static string ToString(uint value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Converts the <see cref="T:System.Int64" />  to its JSON string representation.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>A JSON string representation of the <see cref="T:System.Int64" />.</returns>
		// Token: 0x06000225 RID: 549 RVA: 0x0000904E File Offset: 0x0000724E
		public static string ToString(long value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000905D File Offset: 0x0000725D
		private static string ToStringInternal(BigInteger value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Converts the <see cref="T:System.UInt64" /> to its JSON string representation.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>A JSON string representation of the <see cref="T:System.UInt64" />.</returns>
		// Token: 0x06000227 RID: 551 RVA: 0x0000906C File Offset: 0x0000726C
		[CLSCompliant(false)]
		public static string ToString(ulong value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Converts the <see cref="T:System.Single" /> to its JSON string representation.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>A JSON string representation of the <see cref="T:System.Single" />.</returns>
		// Token: 0x06000228 RID: 552 RVA: 0x0000907B File Offset: 0x0000727B
		public static string ToString(float value)
		{
			return JsonConvert.EnsureDecimalPlace((double)value, value.ToString("R", CultureInfo.InvariantCulture));
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00009095 File Offset: 0x00007295
		internal static string ToString(float value, FloatFormatHandling floatFormatHandling, char quoteChar, bool nullable)
		{
			return JsonConvert.EnsureFloatFormat((double)value, JsonConvert.EnsureDecimalPlace((double)value, value.ToString("R", CultureInfo.InvariantCulture)), floatFormatHandling, quoteChar, nullable);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x000090B9 File Offset: 0x000072B9
		private static string EnsureFloatFormat(double value, string text, FloatFormatHandling floatFormatHandling, char quoteChar, bool nullable)
		{
			if (floatFormatHandling == FloatFormatHandling.Symbol || (!double.IsInfinity(value) && !double.IsNaN(value)))
			{
				return text;
			}
			if (floatFormatHandling != FloatFormatHandling.DefaultValue)
			{
				return quoteChar + text + quoteChar;
			}
			if (nullable)
			{
				return JsonConvert.Null;
			}
			return "0.0";
		}

		/// <summary>
		/// Converts the <see cref="T:System.Double" /> to its JSON string representation.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>A JSON string representation of the <see cref="T:System.Double" />.</returns>
		// Token: 0x0600022B RID: 555 RVA: 0x000090F7 File Offset: 0x000072F7
		public static string ToString(double value)
		{
			return JsonConvert.EnsureDecimalPlace(value, value.ToString("R", CultureInfo.InvariantCulture));
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00009110 File Offset: 0x00007310
		internal static string ToString(double value, FloatFormatHandling floatFormatHandling, char quoteChar, bool nullable)
		{
			return JsonConvert.EnsureFloatFormat(value, JsonConvert.EnsureDecimalPlace(value, value.ToString("R", CultureInfo.InvariantCulture)), floatFormatHandling, quoteChar, nullable);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00009132 File Offset: 0x00007332
		private static string EnsureDecimalPlace(double value, string text)
		{
			if (double.IsNaN(value) || double.IsInfinity(value) || text.IndexOf('.') != -1 || text.IndexOf('E') != -1 || text.IndexOf('e') != -1)
			{
				return text;
			}
			return text + ".0";
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00009172 File Offset: 0x00007372
		private static string EnsureDecimalPlace(string text)
		{
			if (text.IndexOf('.') != -1)
			{
				return text;
			}
			return text + ".0";
		}

		/// <summary>
		/// Converts the <see cref="T:System.Byte" /> to its JSON string representation.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>A JSON string representation of the <see cref="T:System.Byte" />.</returns>
		// Token: 0x0600022F RID: 559 RVA: 0x0000918C File Offset: 0x0000738C
		public static string ToString(byte value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Converts the <see cref="T:System.SByte" /> to its JSON string representation.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>A JSON string representation of the <see cref="T:System.SByte" />.</returns>
		// Token: 0x06000230 RID: 560 RVA: 0x0000919B File Offset: 0x0000739B
		[CLSCompliant(false)]
		public static string ToString(sbyte value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Converts the <see cref="T:System.Decimal" /> to its JSON string representation.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>A JSON string representation of the <see cref="T:System.SByte" />.</returns>
		// Token: 0x06000231 RID: 561 RVA: 0x000091AA File Offset: 0x000073AA
		public static string ToString(decimal value)
		{
			return JsonConvert.EnsureDecimalPlace(value.ToString(null, CultureInfo.InvariantCulture));
		}

		/// <summary>
		/// Converts the <see cref="T:System.Guid" /> to its JSON string representation.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>A JSON string representation of the <see cref="T:System.Guid" />.</returns>
		// Token: 0x06000232 RID: 562 RVA: 0x000091BE File Offset: 0x000073BE
		public static string ToString(Guid value)
		{
			return JsonConvert.ToString(value, '"');
		}

		// Token: 0x06000233 RID: 563 RVA: 0x000091C8 File Offset: 0x000073C8
		internal static string ToString(Guid value, char quoteChar)
		{
			string text = value.ToString("D");
			return quoteChar + text + quoteChar;
		}

		/// <summary>
		/// Converts the <see cref="T:System.TimeSpan" /> to its JSON string representation.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>A JSON string representation of the <see cref="T:System.TimeSpan" />.</returns>
		// Token: 0x06000234 RID: 564 RVA: 0x000091F6 File Offset: 0x000073F6
		public static string ToString(TimeSpan value)
		{
			return JsonConvert.ToString(value, '"');
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00009200 File Offset: 0x00007400
		internal static string ToString(TimeSpan value, char quoteChar)
		{
			return JsonConvert.ToString(value.ToString(), quoteChar);
		}

		/// <summary>
		/// Converts the <see cref="T:System.Uri" /> to its JSON string representation.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>A JSON string representation of the <see cref="T:System.Uri" />.</returns>
		// Token: 0x06000236 RID: 566 RVA: 0x00009215 File Offset: 0x00007415
		public static string ToString(Uri value)
		{
			if (value == null)
			{
				return JsonConvert.Null;
			}
			return JsonConvert.ToString(value, '"');
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000922E File Offset: 0x0000742E
		internal static string ToString(Uri value, char quoteChar)
		{
			return JsonConvert.ToString(value.ToString(), quoteChar);
		}

		/// <summary>
		/// Converts the <see cref="T:System.String" /> to its JSON string representation.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>A JSON string representation of the <see cref="T:System.String" />.</returns>
		// Token: 0x06000238 RID: 568 RVA: 0x0000923C File Offset: 0x0000743C
		public static string ToString(string value)
		{
			return JsonConvert.ToString(value, '"');
		}

		/// <summary>
		/// Converts the <see cref="T:System.String" /> to its JSON string representation.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <param name="delimiter">The string delimiter character.</param>
		/// <returns>A JSON string representation of the <see cref="T:System.String" />.</returns>
		// Token: 0x06000239 RID: 569 RVA: 0x00009246 File Offset: 0x00007446
		public static string ToString(string value, char delimiter)
		{
			if (delimiter != '"' && delimiter != '\'')
			{
				throw new ArgumentException("Delimiter must be a single or double quote.", "delimiter");
			}
			return JavaScriptUtils.ToEscapedJavaScriptString(value, delimiter, true);
		}

		/// <summary>
		/// Converts the <see cref="T:System.Object" /> to its JSON string representation.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>A JSON string representation of the <see cref="T:System.Object" />.</returns>
		// Token: 0x0600023A RID: 570 RVA: 0x0000926C File Offset: 0x0000746C
		public static string ToString(object value)
		{
			if (value == null)
			{
				return JsonConvert.Null;
			}
			switch (ConvertUtils.GetTypeCode(value))
			{
			case PrimitiveTypeCode.Char:
				return JsonConvert.ToString((char)value);
			case PrimitiveTypeCode.Boolean:
				return JsonConvert.ToString((bool)value);
			case PrimitiveTypeCode.SByte:
				return JsonConvert.ToString((sbyte)value);
			case PrimitiveTypeCode.Int16:
				return JsonConvert.ToString((short)value);
			case PrimitiveTypeCode.UInt16:
				return JsonConvert.ToString((ushort)value);
			case PrimitiveTypeCode.Int32:
				return JsonConvert.ToString((int)value);
			case PrimitiveTypeCode.Byte:
				return JsonConvert.ToString((byte)value);
			case PrimitiveTypeCode.UInt32:
				return JsonConvert.ToString((uint)value);
			case PrimitiveTypeCode.Int64:
				return JsonConvert.ToString((long)value);
			case PrimitiveTypeCode.UInt64:
				return JsonConvert.ToString((ulong)value);
			case PrimitiveTypeCode.Single:
				return JsonConvert.ToString((float)value);
			case PrimitiveTypeCode.Double:
				return JsonConvert.ToString((double)value);
			case PrimitiveTypeCode.DateTime:
				return JsonConvert.ToString((DateTime)value);
			case PrimitiveTypeCode.DateTimeOffset:
				return JsonConvert.ToString((DateTimeOffset)value);
			case PrimitiveTypeCode.Decimal:
				return JsonConvert.ToString((decimal)value);
			case PrimitiveTypeCode.Guid:
				return JsonConvert.ToString((Guid)value);
			case PrimitiveTypeCode.TimeSpan:
				return JsonConvert.ToString((TimeSpan)value);
			case PrimitiveTypeCode.BigInteger:
				return JsonConvert.ToStringInternal((BigInteger)value);
			case PrimitiveTypeCode.Uri:
				return JsonConvert.ToString((Uri)value);
			case PrimitiveTypeCode.String:
				return JsonConvert.ToString((string)value);
			}
			throw new ArgumentException("Unsupported type: {0}. Use the JsonSerializer class to get the object's JSON representation.".FormatWith(CultureInfo.InvariantCulture, value.GetType()));
		}

		/// <summary>
		/// Serializes the specified object to a JSON string.
		/// </summary>
		/// <param name="value">The object to serialize.</param>
		/// <returns>A JSON string representation of the object.</returns>
		// Token: 0x0600023B RID: 571 RVA: 0x0000943A File Offset: 0x0000763A
		public static string SerializeObject(object value)
		{
			return JsonConvert.SerializeObject(value, Formatting.None, null);
		}

		/// <summary>
		/// Serializes the specified object to a JSON string using formatting.
		/// </summary>
		/// <param name="value">The object to serialize.</param>
		/// <param name="formatting">Indicates how the output is formatted.</param>
		/// <returns>
		/// A JSON string representation of the object.
		/// </returns>
		// Token: 0x0600023C RID: 572 RVA: 0x00009444 File Offset: 0x00007644
		public static string SerializeObject(object value, Formatting formatting)
		{
			return JsonConvert.SerializeObject(value, formatting, null);
		}

		/// <summary>
		/// Serializes the specified object to a JSON string using a collection of <see cref="T:Newtonsoft.Json.JsonConverter" />.
		/// </summary>
		/// <param name="value">The object to serialize.</param>
		/// <param name="converters">A collection converters used while serializing.</param>
		/// <returns>A JSON string representation of the object.</returns>
		// Token: 0x0600023D RID: 573 RVA: 0x0000944E File Offset: 0x0000764E
		public static string SerializeObject(object value, params JsonConverter[] converters)
		{
			return JsonConvert.SerializeObject(value, Formatting.None, converters);
		}

		/// <summary>
		/// Serializes the specified object to a JSON string using formatting and a collection of <see cref="T:Newtonsoft.Json.JsonConverter" />.
		/// </summary>
		/// <param name="value">The object to serialize.</param>
		/// <param name="formatting">Indicates how the output is formatted.</param>
		/// <param name="converters">A collection converters used while serializing.</param>
		/// <returns>A JSON string representation of the object.</returns>
		// Token: 0x0600023E RID: 574 RVA: 0x00009458 File Offset: 0x00007658
		public static string SerializeObject(object value, Formatting formatting, params JsonConverter[] converters)
		{
			JsonSerializerSettings settings = (converters != null && converters.Length > 0) ? new JsonSerializerSettings
			{
				Converters = converters
			} : null;
			return JsonConvert.SerializeObject(value, formatting, settings);
		}

		/// <summary>
		/// Serializes the specified object to a JSON string using <see cref="T:Newtonsoft.Json.JsonSerializerSettings" />.
		/// </summary>
		/// <param name="value">The object to serialize.</param>
		/// <param name="settings">The <see cref="T:Newtonsoft.Json.JsonSerializerSettings" /> used to serialize the object.
		/// If this is null, default serialization settings will be is used.</param>
		/// <returns>
		/// A JSON string representation of the object.
		/// </returns>
		// Token: 0x0600023F RID: 575 RVA: 0x00009488 File Offset: 0x00007688
		public static string SerializeObject(object value, JsonSerializerSettings settings)
		{
			return JsonConvert.SerializeObject(value, Formatting.None, settings);
		}

		/// <summary>
		/// Serializes the specified object to a JSON string using formatting and <see cref="T:Newtonsoft.Json.JsonSerializerSettings" />.
		/// </summary>
		/// <param name="value">The object to serialize.</param>
		/// <param name="formatting">Indicates how the output is formatted.</param>
		/// <param name="settings">The <see cref="T:Newtonsoft.Json.JsonSerializerSettings" /> used to serialize the object.
		/// If this is null, default serialization settings will be is used.</param>
		/// <returns>
		/// A JSON string representation of the object.
		/// </returns>
		// Token: 0x06000240 RID: 576 RVA: 0x00009492 File Offset: 0x00007692
		public static string SerializeObject(object value, Formatting formatting, JsonSerializerSettings settings)
		{
			return JsonConvert.SerializeObject(value, null, formatting, settings);
		}

		/// <summary>
		/// Serializes the specified object to a JSON string using a type, formatting and <see cref="T:Newtonsoft.Json.JsonSerializerSettings" />.
		/// </summary>
		/// <param name="value">The object to serialize.</param>
		/// <param name="formatting">Indicates how the output is formatted.</param>
		/// <param name="settings">The <see cref="T:Newtonsoft.Json.JsonSerializerSettings" /> used to serialize the object.
		/// If this is null, default serialization settings will be is used.</param>
		/// <param name="type">
		/// The type of the value being serialized.
		/// This parameter is used when <see cref="T:Newtonsoft.Json.TypeNameHandling" /> is Auto to write out the type name if the type of the value does not match.
		/// Specifing the type is optional.
		/// </param>
		/// <returns>
		/// A JSON string representation of the object.
		/// </returns>
		// Token: 0x06000241 RID: 577 RVA: 0x000094A0 File Offset: 0x000076A0
		public static string SerializeObject(object value, Type type, Formatting formatting, JsonSerializerSettings settings)
		{
			JsonSerializer jsonSerializer = JsonSerializer.CreateDefault(settings);
			StringBuilder stringBuilder = new StringBuilder(256);
			StringWriter stringWriter = new StringWriter(stringBuilder, CultureInfo.InvariantCulture);
			using (JsonTextWriter jsonTextWriter = new JsonTextWriter(stringWriter))
			{
				jsonTextWriter.Formatting = formatting;
				jsonSerializer.Serialize(jsonTextWriter, value, type);
			}
			return stringWriter.ToString();
		}

		/// <summary>
		/// Asynchronously serializes the specified object to a JSON string.
		/// Serialization will happen on a new thread.
		/// </summary>
		/// <param name="value">The object to serialize.</param>
		/// <returns>
		/// A task that represents the asynchronous serialize operation. The value of the <c>TResult</c> parameter contains a JSON string representation of the object.
		/// </returns>
		// Token: 0x06000242 RID: 578 RVA: 0x00009504 File Offset: 0x00007704
		public static Task<string> SerializeObjectAsync(object value)
		{
			return JsonConvert.SerializeObjectAsync(value, Formatting.None, null);
		}

		/// <summary>
		/// Asynchronously serializes the specified object to a JSON string using formatting.
		/// Serialization will happen on a new thread.
		/// </summary>
		/// <param name="value">The object to serialize.</param>
		/// <param name="formatting">Indicates how the output is formatted.</param>
		/// <returns>
		/// A task that represents the asynchronous serialize operation. The value of the <c>TResult</c> parameter contains a JSON string representation of the object.
		/// </returns>
		// Token: 0x06000243 RID: 579 RVA: 0x0000950E File Offset: 0x0000770E
		public static Task<string> SerializeObjectAsync(object value, Formatting formatting)
		{
			return JsonConvert.SerializeObjectAsync(value, formatting, null);
		}

		/// <summary>
		/// Asynchronously serializes the specified object to a JSON string using formatting and a collection of <see cref="T:Newtonsoft.Json.JsonConverter" />.
		/// Serialization will happen on a new thread.
		/// </summary>
		/// <param name="value">The object to serialize.</param>
		/// <param name="formatting">Indicates how the output is formatted.</param>
		/// <param name="settings">The <see cref="T:Newtonsoft.Json.JsonSerializerSettings" /> used to serialize the object.
		/// If this is null, default serialization settings will be is used.</param>
		/// <returns>
		/// A task that represents the asynchronous serialize operation. The value of the <c>TResult</c> parameter contains a JSON string representation of the object.
		/// </returns>
		// Token: 0x06000244 RID: 580 RVA: 0x0000953C File Offset: 0x0000773C
		public static Task<string> SerializeObjectAsync(object value, Formatting formatting, JsonSerializerSettings settings)
		{
			return Task.Factory.StartNew<string>(() => JsonConvert.SerializeObject(value, formatting, settings));
		}

		/// <summary>
		/// Deserializes the JSON to a .NET object.
		/// </summary>
		/// <param name="value">The JSON to deserialize.</param>
		/// <returns>The deserialized object from the Json string.</returns>
		// Token: 0x06000245 RID: 581 RVA: 0x0000957A File Offset: 0x0000777A
		public static object DeserializeObject(string value)
		{
			return JsonConvert.DeserializeObject(value, null, null);
		}

		/// <summary>
		/// Deserializes the JSON to a .NET object using <see cref="T:Newtonsoft.Json.JsonSerializerSettings" />.
		/// </summary>
		/// <param name="value">The JSON to deserialize.</param>
		/// <param name="settings">
		/// The <see cref="T:Newtonsoft.Json.JsonSerializerSettings" /> used to deserialize the object.
		/// If this is null, default serialization settings will be is used.
		/// </param>
		/// <returns>The deserialized object from the JSON string.</returns>
		// Token: 0x06000246 RID: 582 RVA: 0x00009584 File Offset: 0x00007784
		public static object DeserializeObject(string value, JsonSerializerSettings settings)
		{
			return JsonConvert.DeserializeObject(value, null, settings);
		}

		/// <summary>
		/// Deserializes the JSON to the specified .NET type.
		/// </summary>
		/// <param name="value">The JSON to deserialize.</param>
		/// <param name="type">The <see cref="T:System.Type" /> of object being deserialized.</param>
		/// <returns>The deserialized object from the Json string.</returns>
		// Token: 0x06000247 RID: 583 RVA: 0x0000958E File Offset: 0x0000778E
		public static object DeserializeObject(string value, Type type)
		{
			return JsonConvert.DeserializeObject(value, type, null);
		}

		/// <summary>
		/// Deserializes the JSON to the specified .NET type.
		/// </summary>
		/// <typeparam name="T">The type of the object to deserialize to.</typeparam>
		/// <param name="value">The JSON to deserialize.</param>
		/// <returns>The deserialized object from the Json string.</returns>
		// Token: 0x06000248 RID: 584 RVA: 0x00009598 File Offset: 0x00007798
		public static T DeserializeObject<T>(string value)
		{
			return JsonConvert.DeserializeObject<T>(value, null);
		}

		/// <summary>
		/// Deserializes the JSON to the given anonymous type.
		/// </summary>
		/// <typeparam name="T">
		/// The anonymous type to deserialize to. This can't be specified
		/// traditionally and must be infered from the anonymous type passed
		/// as a parameter.
		/// </typeparam>
		/// <param name="value">The JSON to deserialize.</param>
		/// <param name="anonymousTypeObject">The anonymous type object.</param>
		/// <returns>The deserialized anonymous type from the JSON string.</returns>
		// Token: 0x06000249 RID: 585 RVA: 0x000095A1 File Offset: 0x000077A1
		public static T DeserializeAnonymousType<T>(string value, T anonymousTypeObject)
		{
			return JsonConvert.DeserializeObject<T>(value);
		}

		/// <summary>
		/// Deserializes the JSON to the given anonymous type using <see cref="T:Newtonsoft.Json.JsonSerializerSettings" />.
		/// </summary>
		/// <typeparam name="T">
		/// The anonymous type to deserialize to. This can't be specified
		/// traditionally and must be infered from the anonymous type passed
		/// as a parameter.
		/// </typeparam>
		/// <param name="value">The JSON to deserialize.</param>
		/// <param name="anonymousTypeObject">The anonymous type object.</param>
		/// <param name="settings">
		/// The <see cref="T:Newtonsoft.Json.JsonSerializerSettings" /> used to deserialize the object.
		/// If this is null, default serialization settings will be is used.
		/// </param>
		/// <returns>The deserialized anonymous type from the JSON string.</returns>
		// Token: 0x0600024A RID: 586 RVA: 0x000095A9 File Offset: 0x000077A9
		public static T DeserializeAnonymousType<T>(string value, T anonymousTypeObject, JsonSerializerSettings settings)
		{
			return JsonConvert.DeserializeObject<T>(value, settings);
		}

		/// <summary>
		/// Deserializes the JSON to the specified .NET type using a collection of <see cref="T:Newtonsoft.Json.JsonConverter" />.
		/// </summary>
		/// <typeparam name="T">The type of the object to deserialize to.</typeparam>
		/// <param name="value">The JSON to deserialize.</param>
		/// <param name="converters">Converters to use while deserializing.</param>
		/// <returns>The deserialized object from the JSON string.</returns>
		// Token: 0x0600024B RID: 587 RVA: 0x000095B2 File Offset: 0x000077B2
		public static T DeserializeObject<T>(string value, params JsonConverter[] converters)
		{
			return (T)((object)JsonConvert.DeserializeObject(value, typeof(T), converters));
		}

		/// <summary>
		/// Deserializes the JSON to the specified .NET type using <see cref="T:Newtonsoft.Json.JsonSerializerSettings" />.
		/// </summary>
		/// <typeparam name="T">The type of the object to deserialize to.</typeparam>
		/// <param name="value">The object to deserialize.</param>
		/// <param name="settings">
		/// The <see cref="T:Newtonsoft.Json.JsonSerializerSettings" /> used to deserialize the object.
		/// If this is null, default serialization settings will be is used.
		/// </param>
		/// <returns>The deserialized object from the JSON string.</returns>
		// Token: 0x0600024C RID: 588 RVA: 0x000095CA File Offset: 0x000077CA
		public static T DeserializeObject<T>(string value, JsonSerializerSettings settings)
		{
			return (T)((object)JsonConvert.DeserializeObject(value, typeof(T), settings));
		}

		/// <summary>
		/// Deserializes the JSON to the specified .NET type using a collection of <see cref="T:Newtonsoft.Json.JsonConverter" />.
		/// </summary>
		/// <param name="value">The JSON to deserialize.</param>
		/// <param name="type">The type of the object to deserialize.</param>
		/// <param name="converters">Converters to use while deserializing.</param>
		/// <returns>The deserialized object from the JSON string.</returns>
		// Token: 0x0600024D RID: 589 RVA: 0x000095E4 File Offset: 0x000077E4
		public static object DeserializeObject(string value, Type type, params JsonConverter[] converters)
		{
			JsonSerializerSettings settings = (converters != null && converters.Length > 0) ? new JsonSerializerSettings
			{
				Converters = converters
			} : null;
			return JsonConvert.DeserializeObject(value, type, settings);
		}

		/// <summary>
		/// Deserializes the JSON to the specified .NET type using <see cref="T:Newtonsoft.Json.JsonSerializerSettings" />.
		/// </summary>
		/// <param name="value">The JSON to deserialize.</param>
		/// <param name="type">The type of the object to deserialize to.</param>
		/// <param name="settings">
		/// The <see cref="T:Newtonsoft.Json.JsonSerializerSettings" /> used to deserialize the object.
		/// If this is null, default serialization settings will be is used.
		/// </param>
		/// <returns>The deserialized object from the JSON string.</returns>
		// Token: 0x0600024E RID: 590 RVA: 0x00009614 File Offset: 0x00007814
		public static object DeserializeObject(string value, Type type, JsonSerializerSettings settings)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			StringReader reader = new StringReader(value);
			JsonSerializer jsonSerializer = JsonSerializer.CreateDefault(settings);
			if (!jsonSerializer.IsCheckAdditionalContentSet())
			{
				jsonSerializer.CheckAdditionalContent = true;
			}
			return jsonSerializer.Deserialize(new JsonTextReader(reader), type);
		}

		/// <summary>
		/// Asynchronously deserializes the JSON to the specified .NET type.
		/// Deserialization will happen on a new thread.
		/// </summary>
		/// <typeparam name="T">The type of the object to deserialize to.</typeparam>
		/// <param name="value">The JSON to deserialize.</param>
		/// <returns>
		/// A task that represents the asynchronous deserialize operation. The value of the <c>TResult</c> parameter contains the deserialized object from the JSON string.
		/// </returns>
		// Token: 0x0600024F RID: 591 RVA: 0x00009656 File Offset: 0x00007856
		public static Task<T> DeserializeObjectAsync<T>(string value)
		{
			return JsonConvert.DeserializeObjectAsync<T>(value, null);
		}

		/// <summary>
		/// Asynchronously deserializes the JSON to the specified .NET type using <see cref="T:Newtonsoft.Json.JsonSerializerSettings" />.
		/// Deserialization will happen on a new thread.
		/// </summary>
		/// <typeparam name="T">The type of the object to deserialize to.</typeparam>
		/// <param name="value">The JSON to deserialize.</param>
		/// <param name="settings">
		/// The <see cref="T:Newtonsoft.Json.JsonSerializerSettings" /> used to deserialize the object.
		/// If this is null, default serialization settings will be is used.
		/// </param>
		/// <returns>
		/// A task that represents the asynchronous deserialize operation. The value of the <c>TResult</c> parameter contains the deserialized object from the JSON string.
		/// </returns>
		// Token: 0x06000250 RID: 592 RVA: 0x0000967C File Offset: 0x0000787C
		public static Task<T> DeserializeObjectAsync<T>(string value, JsonSerializerSettings settings)
		{
			return Task.Factory.StartNew<T>(() => JsonConvert.DeserializeObject<T>(value, settings));
		}

		/// <summary>
		/// Asynchronously deserializes the JSON to the specified .NET type.
		/// Deserialization will happen on a new thread.
		/// </summary>
		/// <param name="value">The JSON to deserialize.</param>
		/// <returns>
		/// A task that represents the asynchronous deserialize operation. The value of the <c>TResult</c> parameter contains the deserialized object from the JSON string.
		/// </returns>
		// Token: 0x06000251 RID: 593 RVA: 0x000096B3 File Offset: 0x000078B3
		public static Task<object> DeserializeObjectAsync(string value)
		{
			return JsonConvert.DeserializeObjectAsync(value, null, null);
		}

		/// <summary>
		/// Asynchronously deserializes the JSON to the specified .NET type using <see cref="T:Newtonsoft.Json.JsonSerializerSettings" />.
		/// Deserialization will happen on a new thread.
		/// </summary>
		/// <param name="value">The JSON to deserialize.</param>
		/// <param name="type">The type of the object to deserialize to.</param>
		/// <param name="settings">
		/// The <see cref="T:Newtonsoft.Json.JsonSerializerSettings" /> used to deserialize the object.
		/// If this is null, default serialization settings will be is used.
		/// </param>
		/// <returns>
		/// A task that represents the asynchronous deserialize operation. The value of the <c>TResult</c> parameter contains the deserialized object from the JSON string.
		/// </returns>
		// Token: 0x06000252 RID: 594 RVA: 0x000096E0 File Offset: 0x000078E0
		public static Task<object> DeserializeObjectAsync(string value, Type type, JsonSerializerSettings settings)
		{
			return Task.Factory.StartNew<object>(() => JsonConvert.DeserializeObject(value, type, settings));
		}

		/// <summary>
		/// Populates the object with values from the JSON string.
		/// </summary>
		/// <param name="value">The JSON to populate values from.</param>
		/// <param name="target">The target object to populate values onto.</param>
		// Token: 0x06000253 RID: 595 RVA: 0x0000971E File Offset: 0x0000791E
		public static void PopulateObject(string value, object target)
		{
			JsonConvert.PopulateObject(value, target, null);
		}

		/// <summary>
		/// Populates the object with values from the JSON string using <see cref="T:Newtonsoft.Json.JsonSerializerSettings" />.
		/// </summary>
		/// <param name="value">The JSON to populate values from.</param>
		/// <param name="target">The target object to populate values onto.</param>
		/// <param name="settings">
		/// The <see cref="T:Newtonsoft.Json.JsonSerializerSettings" /> used to deserialize the object.
		/// If this is null, default serialization settings will be is used.
		/// </param>
		// Token: 0x06000254 RID: 596 RVA: 0x00009728 File Offset: 0x00007928
		public static void PopulateObject(string value, object target, JsonSerializerSettings settings)
		{
			StringReader reader = new StringReader(value);
			JsonSerializer jsonSerializer = JsonSerializer.CreateDefault(settings);
			using (JsonReader jsonReader = new JsonTextReader(reader))
			{
				jsonSerializer.Populate(jsonReader, target);
				if (jsonReader.Read() && jsonReader.TokenType != JsonToken.Comment)
				{
					throw new JsonSerializationException("Additional text found in JSON string after finishing deserializing object.");
				}
			}
		}

		/// <summary>
		/// Asynchronously populates the object with values from the JSON string using <see cref="T:Newtonsoft.Json.JsonSerializerSettings" />.
		/// </summary>
		/// <param name="value">The JSON to populate values from.</param>
		/// <param name="target">The target object to populate values onto.</param>
		/// <param name="settings">
		/// The <see cref="T:Newtonsoft.Json.JsonSerializerSettings" /> used to deserialize the object.
		/// If this is null, default serialization settings will be is used.
		/// </param>
		/// <returns>
		/// A task that represents the asynchronous populate operation.
		/// </returns>
		// Token: 0x06000255 RID: 597 RVA: 0x000097B0 File Offset: 0x000079B0
		public static Task PopulateObjectAsync(string value, object target, JsonSerializerSettings settings)
		{
			return Task.Factory.StartNew(delegate()
			{
				JsonConvert.PopulateObject(value, target, settings);
			});
		}

		/// <summary>
		/// Serializes the <see cref="T:System.Xml.Linq.XNode" /> to a JSON string.
		/// </summary>
		/// <param name="node">The node to convert to JSON.</param>
		/// <returns>A JSON string of the XNode.</returns>
		// Token: 0x06000256 RID: 598 RVA: 0x000097EE File Offset: 0x000079EE
		public static string SerializeXNode(XObject node)
		{
			return JsonConvert.SerializeXNode(node, Formatting.None);
		}

		/// <summary>
		/// Serializes the <see cref="T:System.Xml.Linq.XNode" /> to a JSON string using formatting.
		/// </summary>
		/// <param name="node">The node to convert to JSON.</param>
		/// <param name="formatting">Indicates how the output is formatted.</param>
		/// <returns>A JSON string of the XNode.</returns>
		// Token: 0x06000257 RID: 599 RVA: 0x000097F7 File Offset: 0x000079F7
		public static string SerializeXNode(XObject node, Formatting formatting)
		{
			return JsonConvert.SerializeXNode(node, formatting, false);
		}

		/// <summary>
		/// Serializes the <see cref="T:System.Xml.Linq.XNode" /> to a JSON string using formatting and omits the root object if <see cref="!:omitRootObject" /> is <c>true</c>.
		/// </summary>
		/// <param name="node">The node to serialize.</param>
		/// <param name="formatting">Indicates how the output is formatted.</param>
		/// <param name="omitRootObject">Omits writing the root object.</param>
		/// <returns>A JSON string of the XNode.</returns>
		// Token: 0x06000258 RID: 600 RVA: 0x00009804 File Offset: 0x00007A04
		public static string SerializeXNode(XObject node, Formatting formatting, bool omitRootObject)
		{
			XmlNodeConverter xmlNodeConverter = new XmlNodeConverter
			{
				OmitRootObject = omitRootObject
			};
			return JsonConvert.SerializeObject(node, formatting, new JsonConverter[]
			{
				xmlNodeConverter
			});
		}

		/// <summary>
		/// Deserializes the <see cref="T:System.Xml.Linq.XNode" /> from a JSON string.
		/// </summary>
		/// <param name="value">The JSON string.</param>
		/// <returns>The deserialized XNode</returns>
		// Token: 0x06000259 RID: 601 RVA: 0x00009833 File Offset: 0x00007A33
		public static XDocument DeserializeXNode(string value)
		{
			return JsonConvert.DeserializeXNode(value, null);
		}

		/// <summary>
		/// Deserializes the <see cref="T:System.Xml.Linq.XNode" /> from a JSON string nested in a root elment specified by <see cref="!:deserializeRootElementName" />.
		/// </summary>
		/// <param name="value">The JSON string.</param>
		/// <param name="deserializeRootElementName">The name of the root element to append when deserializing.</param>
		/// <returns>The deserialized XNode</returns>
		// Token: 0x0600025A RID: 602 RVA: 0x0000983C File Offset: 0x00007A3C
		public static XDocument DeserializeXNode(string value, string deserializeRootElementName)
		{
			return JsonConvert.DeserializeXNode(value, deserializeRootElementName, false);
		}

		/// <summary>
		/// Deserializes the <see cref="T:System.Xml.Linq.XNode" /> from a JSON string nested in a root elment specified by <see cref="!:deserializeRootElementName" />
		/// and writes a .NET array attribute for collections.
		/// </summary>
		/// <param name="value">The JSON string.</param>
		/// <param name="deserializeRootElementName">The name of the root element to append when deserializing.</param>
		/// <param name="writeArrayAttribute">
		/// A flag to indicate whether to write the Json.NET array attribute.
		/// This attribute helps preserve arrays when converting the written XML back to JSON.
		/// </param>
		/// <returns>The deserialized XNode</returns>
		// Token: 0x0600025B RID: 603 RVA: 0x00009848 File Offset: 0x00007A48
		public static XDocument DeserializeXNode(string value, string deserializeRootElementName, bool writeArrayAttribute)
		{
			XmlNodeConverter xmlNodeConverter = new XmlNodeConverter();
			xmlNodeConverter.DeserializeRootElementName = deserializeRootElementName;
			xmlNodeConverter.WriteArrayAttribute = writeArrayAttribute;
			return (XDocument)JsonConvert.DeserializeObject(value, typeof(XDocument), new JsonConverter[]
			{
				xmlNodeConverter
			});
		}

		/// <summary>
		/// Represents JavaScript's boolean value true as a string. This field is read-only.
		/// </summary>
		// Token: 0x040000C9 RID: 201
		public static readonly string True = "true";

		/// <summary>
		/// Represents JavaScript's boolean value false as a string. This field is read-only.
		/// </summary>
		// Token: 0x040000CA RID: 202
		public static readonly string False = "false";

		/// <summary>
		/// Represents JavaScript's null as a string. This field is read-only.
		/// </summary>
		// Token: 0x040000CB RID: 203
		public static readonly string Null = "null";

		/// <summary>
		/// Represents JavaScript's undefined as a string. This field is read-only.
		/// </summary>
		// Token: 0x040000CC RID: 204
		public static readonly string Undefined = "undefined";

		/// <summary>
		/// Represents JavaScript's positive infinity as a string. This field is read-only.
		/// </summary>
		// Token: 0x040000CD RID: 205
		public static readonly string PositiveInfinity = "Infinity";

		/// <summary>
		/// Represents JavaScript's negative infinity as a string. This field is read-only.
		/// </summary>
		// Token: 0x040000CE RID: 206
		public static readonly string NegativeInfinity = "-Infinity";

		/// <summary>
		/// Represents JavaScript's NaN as a string. This field is read-only.
		/// </summary>
		// Token: 0x040000CF RID: 207
		public static readonly string NaN = "NaN";
	}
}
