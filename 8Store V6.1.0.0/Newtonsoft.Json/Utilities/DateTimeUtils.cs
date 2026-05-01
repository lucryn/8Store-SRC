using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000057 RID: 87
	[NullableContext(1)]
	[Nullable(0)]
	internal static class DateTimeUtils
	{
		// Token: 0x06000497 RID: 1175 RVA: 0x00012504 File Offset: 0x00010704
		public static TimeSpan GetUtcOffset(this DateTime d)
		{
			return TimeZoneInfo.Local.GetUtcOffset(d);
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00012511 File Offset: 0x00010711
		public static string ToDateTimeFormat(DateTimeKind kind)
		{
			switch (kind)
			{
			case 0:
				return "yyyy-MM-ddTHH:mm:ss.FFFFFFF";
			case 1:
				return "yyyy-MM-ddTHH:mm:ss.FFFFFFFZ";
			case 2:
				return "yyyy-MM-ddTHH:mm:ss.FFFFFFFK";
			default:
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException("kind", kind, "Unexpected DateTimeKind value.");
			}
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00012550 File Offset: 0x00010750
		internal static DateTime EnsureDateTime(DateTime value, DateTimeZoneHandling timeZone)
		{
			switch (timeZone)
			{
			case DateTimeZoneHandling.Local:
				value = DateTimeUtils.SwitchToLocalTime(value);
				break;
			case DateTimeZoneHandling.Utc:
				value = DateTimeUtils.SwitchToUtcTime(value);
				break;
			case DateTimeZoneHandling.Unspecified:
				value..ctor(value.Ticks, 0);
				break;
			case DateTimeZoneHandling.RoundtripKind:
				break;
			default:
				throw new ArgumentException("Invalid date time handling value.");
			}
			return value;
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x000125A8 File Offset: 0x000107A8
		private static DateTime SwitchToLocalTime(DateTime value)
		{
			switch (value.Kind)
			{
			case 0:
				return new DateTime(value.Ticks, 2);
			case 1:
				return value.ToLocalTime();
			case 2:
				return value;
			default:
				return value;
			}
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x000125EC File Offset: 0x000107EC
		private static DateTime SwitchToUtcTime(DateTime value)
		{
			switch (value.Kind)
			{
			case 0:
				return new DateTime(value.Ticks, 1);
			case 1:
				return value;
			case 2:
				return value.ToUniversalTime();
			default:
				return value;
			}
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0001262E File Offset: 0x0001082E
		private static long ToUniversalTicks(DateTime dateTime)
		{
			if (dateTime.Kind == 1)
			{
				return dateTime.Ticks;
			}
			return DateTimeUtils.ToUniversalTicks(dateTime, dateTime.GetUtcOffset());
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00012650 File Offset: 0x00010850
		private static long ToUniversalTicks(DateTime dateTime, TimeSpan offset)
		{
			if (dateTime.Kind == 1 || dateTime == DateTime.MaxValue || dateTime == DateTime.MinValue)
			{
				return dateTime.Ticks;
			}
			long num = dateTime.Ticks - offset.Ticks;
			if (num > 3155378975999999999L)
			{
				return 3155378975999999999L;
			}
			if (num < 0L)
			{
				return 0L;
			}
			return num;
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x000126B8 File Offset: 0x000108B8
		internal static long ConvertDateTimeToJavaScriptTicks(DateTime dateTime, TimeSpan offset)
		{
			return DateTimeUtils.UniversalTicksToJavaScriptTicks(DateTimeUtils.ToUniversalTicks(dateTime, offset));
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x000126C6 File Offset: 0x000108C6
		internal static long ConvertDateTimeToJavaScriptTicks(DateTime dateTime)
		{
			return DateTimeUtils.ConvertDateTimeToJavaScriptTicks(dateTime, true);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x000126CF File Offset: 0x000108CF
		internal static long ConvertDateTimeToJavaScriptTicks(DateTime dateTime, bool convertToUtc)
		{
			return DateTimeUtils.UniversalTicksToJavaScriptTicks(convertToUtc ? DateTimeUtils.ToUniversalTicks(dateTime) : dateTime.Ticks);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x000126E8 File Offset: 0x000108E8
		private static long UniversalTicksToJavaScriptTicks(long universalTicks)
		{
			return (universalTicks - DateTimeUtils.InitialJavaScriptDateTicks) / 10000L;
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x000126F8 File Offset: 0x000108F8
		internal static DateTime ConvertJavaScriptTicksToDateTime(long javaScriptTicks)
		{
			return new DateTime(javaScriptTicks * 10000L + DateTimeUtils.InitialJavaScriptDateTicks, 1);
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00012710 File Offset: 0x00010910
		internal static bool TryParseDateTimeIso(StringReference text, DateTimeZoneHandling dateTimeZoneHandling, out DateTime dt)
		{
			DateTimeParser dateTimeParser = default(DateTimeParser);
			if (!dateTimeParser.Parse(text.Chars, text.StartIndex, text.Length))
			{
				dt = default(DateTime);
				return false;
			}
			DateTime dateTime = DateTimeUtils.CreateDateTime(dateTimeParser);
			switch (dateTimeParser.Zone)
			{
			case ParserTimeZone.Utc:
				dateTime..ctor(dateTime.Ticks, 1);
				break;
			case ParserTimeZone.LocalWestOfUtc:
			{
				TimeSpan timeSpan;
				timeSpan..ctor(dateTimeParser.ZoneHour, dateTimeParser.ZoneMinute, 0);
				long num = dateTime.Ticks + timeSpan.Ticks;
				long num2 = num;
				DateTime dateTime2 = DateTime.MaxValue;
				if (num2 <= dateTime2.Ticks)
				{
					dateTime = new DateTime(num, 1).ToLocalTime();
				}
				else
				{
					num += dateTime.GetUtcOffset().Ticks;
					long num3 = num;
					dateTime2 = DateTime.MaxValue;
					if (num3 > dateTime2.Ticks)
					{
						dateTime2 = DateTime.MaxValue;
						num = dateTime2.Ticks;
					}
					dateTime..ctor(num, 2);
				}
				break;
			}
			case ParserTimeZone.LocalEastOfUtc:
			{
				TimeSpan timeSpan2;
				timeSpan2..ctor(dateTimeParser.ZoneHour, dateTimeParser.ZoneMinute, 0);
				long num = dateTime.Ticks - timeSpan2.Ticks;
				long num4 = num;
				DateTime dateTime2 = DateTime.MinValue;
				if (num4 >= dateTime2.Ticks)
				{
					dateTime = new DateTime(num, 1).ToLocalTime();
				}
				else
				{
					num += dateTime.GetUtcOffset().Ticks;
					long num5 = num;
					dateTime2 = DateTime.MinValue;
					if (num5 < dateTime2.Ticks)
					{
						dateTime2 = DateTime.MinValue;
						num = dateTime2.Ticks;
					}
					dateTime..ctor(num, 2);
				}
				break;
			}
			}
			dt = DateTimeUtils.EnsureDateTime(dateTime, dateTimeZoneHandling);
			return true;
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x000128A4 File Offset: 0x00010AA4
		internal static bool TryParseDateTimeOffsetIso(StringReference text, out DateTimeOffset dt)
		{
			DateTimeParser dateTimeParser = default(DateTimeParser);
			if (!dateTimeParser.Parse(text.Chars, text.StartIndex, text.Length))
			{
				dt = default(DateTimeOffset);
				return false;
			}
			DateTime dateTime = DateTimeUtils.CreateDateTime(dateTimeParser);
			TimeSpan utcOffset;
			switch (dateTimeParser.Zone)
			{
			case ParserTimeZone.Utc:
				utcOffset..ctor(0L);
				break;
			case ParserTimeZone.LocalWestOfUtc:
				utcOffset..ctor(-dateTimeParser.ZoneHour, -dateTimeParser.ZoneMinute, 0);
				break;
			case ParserTimeZone.LocalEastOfUtc:
				utcOffset..ctor(dateTimeParser.ZoneHour, dateTimeParser.ZoneMinute, 0);
				break;
			default:
				utcOffset = TimeZoneInfo.Local.GetUtcOffset(dateTime);
				break;
			}
			long num = dateTime.Ticks - utcOffset.Ticks;
			if (num < 0L || num > 3155378975999999999L)
			{
				dt = default(DateTimeOffset);
				return false;
			}
			dt = new DateTimeOffset(dateTime, utcOffset);
			return true;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00012984 File Offset: 0x00010B84
		private static DateTime CreateDateTime(DateTimeParser dateTimeParser)
		{
			bool flag;
			if (dateTimeParser.Hour == 24)
			{
				flag = true;
				dateTimeParser.Hour = 0;
			}
			else
			{
				flag = false;
			}
			DateTime result;
			result..ctor(dateTimeParser.Year, dateTimeParser.Month, dateTimeParser.Day, dateTimeParser.Hour, dateTimeParser.Minute, dateTimeParser.Second);
			result = result.AddTicks((long)dateTimeParser.Fraction);
			if (flag)
			{
				result = result.AddDays(1.0);
			}
			return result;
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x000129F8 File Offset: 0x00010BF8
		internal static bool TryParseDateTime(StringReference s, DateTimeZoneHandling dateTimeZoneHandling, [Nullable(2)] string dateFormatString, CultureInfo culture, out DateTime dt)
		{
			if (s.Length > 0)
			{
				int startIndex = s.StartIndex;
				if (s[startIndex] == '/')
				{
					if (s.Length >= 9 && s.StartsWith("/Date(") && s.EndsWith(")/") && DateTimeUtils.TryParseDateTimeMicrosoft(s, dateTimeZoneHandling, out dt))
					{
						return true;
					}
				}
				else if (s.Length >= 19 && s.Length <= 40 && char.IsDigit(s[startIndex]) && s[startIndex + 10] == 'T' && DateTimeUtils.TryParseDateTimeIso(s, dateTimeZoneHandling, out dt))
				{
					return true;
				}
				if (!StringUtils.IsNullOrEmpty(dateFormatString) && DateTimeUtils.TryParseDateTimeExact(s.ToString(), dateTimeZoneHandling, dateFormatString, culture, out dt))
				{
					return true;
				}
			}
			dt = default(DateTime);
			return false;
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00012AC8 File Offset: 0x00010CC8
		internal static bool TryParseDateTime(string s, DateTimeZoneHandling dateTimeZoneHandling, [Nullable(2)] string dateFormatString, CultureInfo culture, out DateTime dt)
		{
			if (s.Length > 0)
			{
				if (s.get_Chars(0) == '/')
				{
					if (s.Length >= 9 && s.StartsWith("/Date(", 4) && s.EndsWith(")/", 4) && DateTimeUtils.TryParseDateTimeMicrosoft(new StringReference(s.ToCharArray(), 0, s.Length), dateTimeZoneHandling, out dt))
					{
						return true;
					}
				}
				else if (s.Length >= 19 && s.Length <= 40 && char.IsDigit(s.get_Chars(0)) && s.get_Chars(10) == 'T' && DateTime.TryParseExact(s, "yyyy-MM-ddTHH:mm:ss.FFFFFFFK", CultureInfo.InvariantCulture, 128, ref dt))
				{
					dt = DateTimeUtils.EnsureDateTime(dt, dateTimeZoneHandling);
					return true;
				}
				if (!StringUtils.IsNullOrEmpty(dateFormatString) && DateTimeUtils.TryParseDateTimeExact(s, dateTimeZoneHandling, dateFormatString, culture, out dt))
				{
					return true;
				}
			}
			dt = default(DateTime);
			return false;
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00012BB4 File Offset: 0x00010DB4
		internal static bool TryParseDateTimeOffset(StringReference s, [Nullable(2)] string dateFormatString, CultureInfo culture, out DateTimeOffset dt)
		{
			if (s.Length > 0)
			{
				int startIndex = s.StartIndex;
				if (s[startIndex] == '/')
				{
					if (s.Length >= 9 && s.StartsWith("/Date(") && s.EndsWith(")/") && DateTimeUtils.TryParseDateTimeOffsetMicrosoft(s, out dt))
					{
						return true;
					}
				}
				else if (s.Length >= 19 && s.Length <= 40 && char.IsDigit(s[startIndex]) && s[startIndex + 10] == 'T' && DateTimeUtils.TryParseDateTimeOffsetIso(s, out dt))
				{
					return true;
				}
				if (!StringUtils.IsNullOrEmpty(dateFormatString) && DateTimeUtils.TryParseDateTimeOffsetExact(s.ToString(), dateFormatString, culture, out dt))
				{
					return true;
				}
			}
			dt = default(DateTimeOffset);
			return false;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00012C7C File Offset: 0x00010E7C
		internal static bool TryParseDateTimeOffset(string s, [Nullable(2)] string dateFormatString, CultureInfo culture, out DateTimeOffset dt)
		{
			if (s.Length > 0)
			{
				if (s.get_Chars(0) == '/')
				{
					if (s.Length >= 9 && s.StartsWith("/Date(", 4) && s.EndsWith(")/", 4) && DateTimeUtils.TryParseDateTimeOffsetMicrosoft(new StringReference(s.ToCharArray(), 0, s.Length), out dt))
					{
						return true;
					}
				}
				else if (s.Length >= 19 && s.Length <= 40 && char.IsDigit(s.get_Chars(0)) && s.get_Chars(10) == 'T' && DateTimeOffset.TryParseExact(s, "yyyy-MM-ddTHH:mm:ss.FFFFFFFK", CultureInfo.InvariantCulture, 128, ref dt) && DateTimeUtils.TryParseDateTimeOffsetIso(new StringReference(s.ToCharArray(), 0, s.Length), out dt))
				{
					return true;
				}
				if (!StringUtils.IsNullOrEmpty(dateFormatString) && DateTimeUtils.TryParseDateTimeOffsetExact(s, dateFormatString, culture, out dt))
				{
					return true;
				}
			}
			dt = default(DateTimeOffset);
			return false;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00012D68 File Offset: 0x00010F68
		private static bool TryParseMicrosoftDate(StringReference text, out long ticks, out TimeSpan offset, out DateTimeKind kind)
		{
			kind = 1;
			int num = text.IndexOf('+', 7, text.Length - 8);
			if (num == -1)
			{
				num = text.IndexOf('-', 7, text.Length - 8);
			}
			if (num != -1)
			{
				kind = 2;
				if (!DateTimeUtils.TryReadOffset(text, num + text.StartIndex, out offset))
				{
					ticks = 0L;
					return false;
				}
			}
			else
			{
				offset = TimeSpan.Zero;
				num = text.Length - 2;
			}
			return ConvertUtils.Int64TryParse(text.Chars, 6 + text.StartIndex, num - 6, out ticks) == ParseResult.Success;
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00012DF4 File Offset: 0x00010FF4
		private static bool TryParseDateTimeMicrosoft(StringReference text, DateTimeZoneHandling dateTimeZoneHandling, out DateTime dt)
		{
			long javaScriptTicks;
			TimeSpan timeSpan;
			DateTimeKind dateTimeKind;
			if (!DateTimeUtils.TryParseMicrosoftDate(text, out javaScriptTicks, out timeSpan, out dateTimeKind))
			{
				dt = default(DateTime);
				return false;
			}
			DateTime dateTime = DateTimeUtils.ConvertJavaScriptTicksToDateTime(javaScriptTicks);
			if (dateTimeKind != null)
			{
				if (dateTimeKind != 2)
				{
					dt = dateTime;
				}
				else
				{
					dt = dateTime.ToLocalTime();
				}
			}
			else
			{
				dt = DateTime.SpecifyKind(dateTime.ToLocalTime(), 0);
			}
			dt = DateTimeUtils.EnsureDateTime(dt, dateTimeZoneHandling);
			return true;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00012E68 File Offset: 0x00011068
		private static bool TryParseDateTimeExact(string text, DateTimeZoneHandling dateTimeZoneHandling, string dateFormatString, CultureInfo culture, out DateTime dt)
		{
			DateTime dateTime;
			if (DateTime.TryParseExact(text, dateFormatString, culture, 128, ref dateTime))
			{
				dateTime = DateTimeUtils.EnsureDateTime(dateTime, dateTimeZoneHandling);
				dt = dateTime;
				return true;
			}
			dt = default(DateTime);
			return false;
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00012EA4 File Offset: 0x000110A4
		private static bool TryParseDateTimeOffsetMicrosoft(StringReference text, out DateTimeOffset dt)
		{
			long javaScriptTicks;
			TimeSpan timeSpan;
			DateTimeKind dateTimeKind;
			if (!DateTimeUtils.TryParseMicrosoftDate(text, out javaScriptTicks, out timeSpan, out dateTimeKind))
			{
				dt = default(DateTime);
				return false;
			}
			dt = new DateTimeOffset(DateTimeUtils.ConvertJavaScriptTicksToDateTime(javaScriptTicks).Add(timeSpan).Ticks, timeSpan);
			return true;
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00012EFC File Offset: 0x000110FC
		private static bool TryParseDateTimeOffsetExact(string text, string dateFormatString, CultureInfo culture, out DateTimeOffset dt)
		{
			DateTimeOffset dateTimeOffset;
			if (DateTimeOffset.TryParseExact(text, dateFormatString, culture, 128, ref dateTimeOffset))
			{
				dt = dateTimeOffset;
				return true;
			}
			dt = default(DateTimeOffset);
			return false;
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00012F2C File Offset: 0x0001112C
		private static bool TryReadOffset(StringReference offsetText, int startIndex, out TimeSpan offset)
		{
			bool flag = offsetText[startIndex] == '-';
			int num;
			if (ConvertUtils.Int32TryParse(offsetText.Chars, startIndex + 1, 2, out num) != ParseResult.Success)
			{
				offset = default(TimeSpan);
				return false;
			}
			int num2 = 0;
			if (offsetText.Length - startIndex > 5 && ConvertUtils.Int32TryParse(offsetText.Chars, startIndex + 3, 2, out num2) != ParseResult.Success)
			{
				offset = default(TimeSpan);
				return false;
			}
			offset = TimeSpan.FromHours((double)num) + TimeSpan.FromMinutes((double)num2);
			if (flag)
			{
				offset = offset.Negate();
			}
			return true;
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00012FBC File Offset: 0x000111BC
		internal static void WriteDateTimeString(TextWriter writer, DateTime value, DateFormatHandling format, [Nullable(2)] string formatString, CultureInfo culture)
		{
			if (StringUtils.IsNullOrEmpty(formatString))
			{
				char[] array = new char[64];
				int num = DateTimeUtils.WriteDateTimeString(array, 0, value, default(TimeSpan?), value.Kind, format);
				writer.Write(array, 0, num);
				return;
			}
			writer.Write(value.ToString(formatString, culture));
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00013010 File Offset: 0x00011210
		internal static int WriteDateTimeString(char[] chars, int start, DateTime value, TimeSpan? offset, DateTimeKind kind, DateFormatHandling format)
		{
			int num2;
			if (format == DateFormatHandling.MicrosoftDateFormat)
			{
				TimeSpan offset2 = offset ?? value.GetUtcOffset();
				long num = DateTimeUtils.ConvertDateTimeToJavaScriptTicks(value, offset2);
				"\\/Date(".CopyTo(0, chars, start, 7);
				num2 = start + 7;
				string text = num.ToString(CultureInfo.InvariantCulture);
				text.CopyTo(0, chars, num2, text.Length);
				num2 += text.Length;
				if (kind != null)
				{
					if (kind == 2)
					{
						num2 = DateTimeUtils.WriteDateTimeOffset(chars, num2, offset2, format);
					}
				}
				else if (value != DateTime.MaxValue && value != DateTime.MinValue)
				{
					num2 = DateTimeUtils.WriteDateTimeOffset(chars, num2, offset2, format);
				}
				")\\/".CopyTo(0, chars, num2, 3);
				num2 += 3;
			}
			else
			{
				num2 = DateTimeUtils.WriteDefaultIsoDate(chars, start, value);
				if (kind != 1)
				{
					if (kind == 2)
					{
						num2 = DateTimeUtils.WriteDateTimeOffset(chars, num2, offset ?? value.GetUtcOffset(), format);
					}
				}
				else
				{
					chars[num2++] = 'Z';
				}
			}
			return num2;
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00013118 File Offset: 0x00011318
		internal static int WriteDefaultIsoDate(char[] chars, int start, DateTime dt)
		{
			int num = 19;
			int value;
			int value2;
			int value3;
			DateTimeUtils.GetDateValues(dt, out value, out value2, out value3);
			DateTimeUtils.CopyIntToCharArray(chars, start, value, 4);
			chars[start + 4] = '-';
			DateTimeUtils.CopyIntToCharArray(chars, start + 5, value2, 2);
			chars[start + 7] = '-';
			DateTimeUtils.CopyIntToCharArray(chars, start + 8, value3, 2);
			chars[start + 10] = 'T';
			DateTimeUtils.CopyIntToCharArray(chars, start + 11, dt.Hour, 2);
			chars[start + 13] = ':';
			DateTimeUtils.CopyIntToCharArray(chars, start + 14, dt.Minute, 2);
			chars[start + 16] = ':';
			DateTimeUtils.CopyIntToCharArray(chars, start + 17, dt.Second, 2);
			int num2 = (int)(dt.Ticks % 10000000L);
			if (num2 != 0)
			{
				int num3 = 7;
				while (num2 % 10 == 0)
				{
					num3--;
					num2 /= 10;
				}
				chars[start + 19] = '.';
				DateTimeUtils.CopyIntToCharArray(chars, start + 20, num2, num3);
				num += num3 + 1;
			}
			return start + num;
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x000131FD File Offset: 0x000113FD
		private static void CopyIntToCharArray(char[] chars, int start, int value, int digits)
		{
			while (digits-- != 0)
			{
				chars[start + digits] = (char)(value % 10 + 48);
				value /= 10;
			}
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0001321C File Offset: 0x0001141C
		internal static int WriteDateTimeOffset(char[] chars, int start, TimeSpan offset, DateFormatHandling format)
		{
			chars[start++] = ((offset.Ticks >= 0L) ? '+' : '-');
			int value = Math.Abs(offset.Hours);
			DateTimeUtils.CopyIntToCharArray(chars, start, value, 2);
			start += 2;
			if (format == DateFormatHandling.IsoDateFormat)
			{
				chars[start++] = ':';
			}
			int value2 = Math.Abs(offset.Minutes);
			DateTimeUtils.CopyIntToCharArray(chars, start, value2, 2);
			start += 2;
			return start;
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00013288 File Offset: 0x00011488
		internal static void WriteDateTimeOffsetString(TextWriter writer, DateTimeOffset value, DateFormatHandling format, [Nullable(2)] string formatString, CultureInfo culture)
		{
			if (StringUtils.IsNullOrEmpty(formatString))
			{
				char[] array = new char[64];
				int num = DateTimeUtils.WriteDateTimeString(array, 0, (format == DateFormatHandling.IsoDateFormat) ? value.DateTime : value.UtcDateTime, new TimeSpan?(value.Offset), 2, format);
				writer.Write(array, 0, num);
				return;
			}
			writer.Write(value.ToString(formatString, culture));
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x000132E8 File Offset: 0x000114E8
		private static void GetDateValues(DateTime td, out int year, out int month, out int day)
		{
			int i = (int)(td.Ticks / 864000000000L);
			int num = i / 146097;
			i -= num * 146097;
			int num2 = i / 36524;
			if (num2 == 4)
			{
				num2 = 3;
			}
			i -= num2 * 36524;
			int num3 = i / 1461;
			i -= num3 * 1461;
			int num4 = i / 365;
			if (num4 == 4)
			{
				num4 = 3;
			}
			year = num * 400 + num2 * 100 + num3 * 4 + num4 + 1;
			i -= num4 * 365;
			int[] array = (num4 == 3 && (num3 != 24 || num2 == 3)) ? DateTimeUtils.DaysToMonth366 : DateTimeUtils.DaysToMonth365;
			int num5 = i >> 6;
			while (i >= array[num5])
			{
				num5++;
			}
			month = num5;
			day = i - array[num5 - 1] + 1;
		}

		// Token: 0x040001EF RID: 495
		internal static readonly long InitialJavaScriptDateTicks = 621355968000000000L;

		// Token: 0x040001F0 RID: 496
		private const string IsoDateFormat = "yyyy-MM-ddTHH:mm:ss.FFFFFFFK";

		// Token: 0x040001F1 RID: 497
		private const int DaysPer100Years = 36524;

		// Token: 0x040001F2 RID: 498
		private const int DaysPer400Years = 146097;

		// Token: 0x040001F3 RID: 499
		private const int DaysPer4Years = 1461;

		// Token: 0x040001F4 RID: 500
		private const int DaysPerYear = 365;

		// Token: 0x040001F5 RID: 501
		private const long TicksPerDay = 864000000000L;

		// Token: 0x040001F6 RID: 502
		private static readonly int[] DaysToMonth365 = new int[]
		{
			0,
			31,
			59,
			90,
			120,
			151,
			181,
			212,
			243,
			273,
			304,
			334,
			365
		};

		// Token: 0x040001F7 RID: 503
		private static readonly int[] DaysToMonth366 = new int[]
		{
			0,
			31,
			60,
			91,
			121,
			152,
			182,
			213,
			244,
			274,
			305,
			335,
			366
		};
	}
}
