using System;
using System.Globalization;
using System.IO;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000BA RID: 186
	internal static class DateTimeUtils
	{
		// Token: 0x0600092D RID: 2349 RVA: 0x00023772 File Offset: 0x00021972
		public static TimeSpan GetUtcOffset(this DateTime d)
		{
			return TimeZoneInfo.Local.GetUtcOffset(d);
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x00023780 File Offset: 0x00021980
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

		// Token: 0x0600092F RID: 2351 RVA: 0x000237D8 File Offset: 0x000219D8
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

		// Token: 0x06000930 RID: 2352 RVA: 0x0002381C File Offset: 0x00021A1C
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

		// Token: 0x06000931 RID: 2353 RVA: 0x0002385E File Offset: 0x00021A5E
		private static long ToUniversalTicks(DateTime dateTime)
		{
			if (dateTime.Kind == 1)
			{
				return dateTime.Ticks;
			}
			return DateTimeUtils.ToUniversalTicks(dateTime, dateTime.GetUtcOffset());
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x00023880 File Offset: 0x00021A80
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

		// Token: 0x06000933 RID: 2355 RVA: 0x000238E8 File Offset: 0x00021AE8
		internal static long ConvertDateTimeToJavaScriptTicks(DateTime dateTime, TimeSpan offset)
		{
			long universialTicks = DateTimeUtils.ToUniversalTicks(dateTime, offset);
			return DateTimeUtils.UniversialTicksToJavaScriptTicks(universialTicks);
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x00023903 File Offset: 0x00021B03
		internal static long ConvertDateTimeToJavaScriptTicks(DateTime dateTime)
		{
			return DateTimeUtils.ConvertDateTimeToJavaScriptTicks(dateTime, true);
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0002390C File Offset: 0x00021B0C
		internal static long ConvertDateTimeToJavaScriptTicks(DateTime dateTime, bool convertToUtc)
		{
			long universialTicks = convertToUtc ? DateTimeUtils.ToUniversalTicks(dateTime) : dateTime.Ticks;
			return DateTimeUtils.UniversialTicksToJavaScriptTicks(universialTicks);
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x00023934 File Offset: 0x00021B34
		private static long UniversialTicksToJavaScriptTicks(long universialTicks)
		{
			return (universialTicks - DateTimeUtils.InitialJavaScriptDateTicks) / 10000L;
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x00023954 File Offset: 0x00021B54
		internal static DateTime ConvertJavaScriptTicksToDateTime(long javaScriptTicks)
		{
			DateTime result;
			result..ctor(javaScriptTicks * 10000L + DateTimeUtils.InitialJavaScriptDateTicks, 1);
			return result;
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x00023978 File Offset: 0x00021B78
		internal static bool TryParseDateIso(string text, DateParseHandling dateParseHandling, DateTimeZoneHandling dateTimeZoneHandling, out object dt)
		{
			DateTimeParser dateTimeParser = default(DateTimeParser);
			if (!dateTimeParser.Parse(text))
			{
				dt = null;
				return false;
			}
			DateTime dateTime;
			dateTime..ctor(dateTimeParser.Year, dateTimeParser.Month, dateTimeParser.Day, dateTimeParser.Hour, dateTimeParser.Minute, dateTimeParser.Second);
			dateTime = dateTime.AddTicks((long)dateTimeParser.Fraction);
			if (dateParseHandling != DateParseHandling.DateTimeOffset)
			{
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
					DateTime maxValue = DateTime.MaxValue;
					if (num2 <= maxValue.Ticks)
					{
						dateTime = new DateTime(num, 1).ToLocalTime();
					}
					else
					{
						num += dateTime.GetUtcOffset().Ticks;
						long num3 = num;
						DateTime maxValue2 = DateTime.MaxValue;
						if (num3 > maxValue2.Ticks)
						{
							DateTime maxValue3 = DateTime.MaxValue;
							num = maxValue3.Ticks;
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
					DateTime minValue = DateTime.MinValue;
					if (num4 >= minValue.Ticks)
					{
						dateTime = new DateTime(num, 1).ToLocalTime();
					}
					else
					{
						num += dateTime.GetUtcOffset().Ticks;
						long num5 = num;
						DateTime minValue2 = DateTime.MinValue;
						if (num5 < minValue2.Ticks)
						{
							DateTime minValue3 = DateTime.MinValue;
							num = minValue3.Ticks;
						}
						dateTime..ctor(num, 2);
					}
					break;
				}
				}
				dt = DateTimeUtils.EnsureDateTime(dateTime, dateTimeZoneHandling);
				return true;
			}
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
			long num6 = dateTime.Ticks - utcOffset.Ticks;
			if (num6 < 0L || num6 > 3155378975999999999L)
			{
				dt = null;
				return false;
			}
			dt = new DateTimeOffset(dateTime, utcOffset);
			return true;
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x00023BEC File Offset: 0x00021DEC
		internal static bool TryParseDateTime(string s, DateParseHandling dateParseHandling, DateTimeZoneHandling dateTimeZoneHandling, out object dt)
		{
			if (s.Length > 0)
			{
				if (s.get_Chars(0) == '/')
				{
					if (s.StartsWith("/Date(", 4) && s.EndsWith(")/", 4))
					{
						return DateTimeUtils.TryParseDateMicrosoft(s, dateParseHandling, dateTimeZoneHandling, out dt);
					}
				}
				else if (s.Length >= 19 && s.Length <= 40 && char.IsDigit(s.get_Chars(0)) && s.get_Chars(10) == 'T')
				{
					return DateTimeUtils.TryParseDateIso(s, dateParseHandling, dateTimeZoneHandling, out dt);
				}
			}
			dt = null;
			return false;
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x00023C70 File Offset: 0x00021E70
		private static bool TryParseDateMicrosoft(string text, DateParseHandling dateParseHandling, DateTimeZoneHandling dateTimeZoneHandling, out object dt)
		{
			string text2 = text.Substring(6, text.Length - 8);
			DateTimeKind dateTimeKind = 1;
			int num = text2.IndexOf('+', 1);
			if (num == -1)
			{
				num = text2.IndexOf('-', 1);
			}
			TimeSpan timeSpan = TimeSpan.Zero;
			if (num != -1)
			{
				dateTimeKind = 2;
				timeSpan = DateTimeUtils.ReadOffset(text2.Substring(num));
				text2 = text2.Substring(0, num);
			}
			long javaScriptTicks = long.Parse(text2, 7, CultureInfo.InvariantCulture);
			DateTime dateTime = DateTimeUtils.ConvertJavaScriptTicksToDateTime(javaScriptTicks);
			if (dateParseHandling == DateParseHandling.DateTimeOffset)
			{
				dt = new DateTimeOffset(dateTime.Add(timeSpan).Ticks, timeSpan);
				return true;
			}
			DateTime value;
			switch (dateTimeKind)
			{
			case 0:
				value = DateTime.SpecifyKind(dateTime.ToLocalTime(), 0);
				goto IL_BF;
			case 2:
				value = dateTime.ToLocalTime();
				goto IL_BF;
			}
			value = dateTime;
			IL_BF:
			dt = DateTimeUtils.EnsureDateTime(value, dateTimeZoneHandling);
			return true;
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x00023D4C File Offset: 0x00021F4C
		private static TimeSpan ReadOffset(string offsetText)
		{
			bool flag = offsetText.get_Chars(0) == '-';
			int num = int.Parse(offsetText.Substring(1, 2), 7, CultureInfo.InvariantCulture);
			int num2 = 0;
			if (offsetText.Length >= 5)
			{
				num2 = int.Parse(offsetText.Substring(3, 2), 7, CultureInfo.InvariantCulture);
			}
			TimeSpan result = TimeSpan.FromHours((double)num) + TimeSpan.FromMinutes((double)num2);
			if (flag)
			{
				result = result.Negate();
			}
			return result;
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x00023DB8 File Offset: 0x00021FB8
		internal static void WriteDateTimeString(TextWriter writer, DateTime value, DateFormatHandling format, string formatString, CultureInfo culture)
		{
			if (string.IsNullOrEmpty(formatString))
			{
				char[] array = new char[64];
				int num = DateTimeUtils.WriteDateTimeString(array, 0, value, default(TimeSpan?), value.Kind, format);
				writer.Write(array, 0, num);
				return;
			}
			writer.Write(value.ToString(formatString, culture));
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x00023E0C File Offset: 0x0002200C
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
				switch (kind)
				{
				case 0:
					if (value != DateTime.MaxValue && value != DateTime.MinValue)
					{
						num2 = DateTimeUtils.WriteDateTimeOffset(chars, num2, offset2, format);
					}
					break;
				case 2:
					num2 = DateTimeUtils.WriteDateTimeOffset(chars, num2, offset2, format);
					break;
				}
				")\\/".CopyTo(0, chars, num2, 3);
				num2 += 3;
			}
			else
			{
				num2 = DateTimeUtils.WriteDefaultIsoDate(chars, start, value);
				switch (kind)
				{
				case 1:
					chars[num2++] = 'Z';
					break;
				case 2:
					num2 = DateTimeUtils.WriteDateTimeOffset(chars, num2, offset ?? value.GetUtcOffset(), format);
					break;
				}
			}
			return num2;
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x00023F30 File Offset: 0x00022130
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

		// Token: 0x0600093F RID: 2367 RVA: 0x00024015 File Offset: 0x00022215
		private static void CopyIntToCharArray(char[] chars, int start, int value, int digits)
		{
			while (digits-- != 0)
			{
				chars[start + digits] = (char)(value % 10 + 48);
				value /= 10;
			}
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x00024034 File Offset: 0x00022234
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

		// Token: 0x06000941 RID: 2369 RVA: 0x000240A0 File Offset: 0x000222A0
		internal static void WriteDateTimeOffsetString(TextWriter writer, DateTimeOffset value, DateFormatHandling format, string formatString, CultureInfo culture)
		{
			if (string.IsNullOrEmpty(formatString))
			{
				char[] array = new char[64];
				int num = DateTimeUtils.WriteDateTimeString(array, 0, (format == DateFormatHandling.IsoDateFormat) ? value.DateTime : value.UtcDateTime, new TimeSpan?(value.Offset), 2, format);
				writer.Write(array, 0, num);
				return;
			}
			writer.Write(value.ToString(formatString, culture));
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x00024100 File Offset: 0x00022300
		private static void GetDateValues(DateTime td, out int year, out int month, out int day)
		{
			long ticks = td.Ticks;
			int i = (int)(ticks / 864000000000L);
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

		// Token: 0x0400038D RID: 909
		private const int DaysPer100Years = 36524;

		// Token: 0x0400038E RID: 910
		private const int DaysPer400Years = 146097;

		// Token: 0x0400038F RID: 911
		private const int DaysPer4Years = 1461;

		// Token: 0x04000390 RID: 912
		private const int DaysPerYear = 365;

		// Token: 0x04000391 RID: 913
		private const long TicksPerDay = 864000000000L;

		// Token: 0x04000392 RID: 914
		internal static readonly long InitialJavaScriptDateTicks = 621355968000000000L;

		// Token: 0x04000393 RID: 915
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

		// Token: 0x04000394 RID: 916
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
