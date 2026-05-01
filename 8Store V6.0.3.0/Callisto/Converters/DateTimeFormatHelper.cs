using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Callisto.Converters
{
	// Token: 0x02000028 RID: 40
	internal static class DateTimeFormatHelper
	{
		// Token: 0x060001CF RID: 463 RVA: 0x00009BF5 File Offset: 0x00007DF5
		public static int GetRelativeDayOfWeek(DateTime dt)
		{
			return (dt.DayOfWeek - CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek + 7) % 7;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00009C12 File Offset: 0x00007E12
		public static bool IsFutureDateTime(DateTime relative, DateTime given)
		{
			return relative < given;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00009C1B File Offset: 0x00007E1B
		public static bool IsAnOlderYear(DateTime relative, DateTime given)
		{
			return relative.Year > given.Year;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00009C2D File Offset: 0x00007E2D
		public static bool IsAnOlderWeek(DateTime relative, DateTime given)
		{
			return DateTimeFormatHelper.IsAtLeastOneWeekOld(relative, given) || DateTimeFormatHelper.GetRelativeDayOfWeek(given) > DateTimeFormatHelper.GetRelativeDayOfWeek(relative);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00009C48 File Offset: 0x00007E48
		public static bool IsAtLeastOneWeekOld(DateTime relative, DateTime given)
		{
			return (double)((int)(relative - given).TotalMinutes) >= 10080.0;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00009C74 File Offset: 0x00007E74
		public static bool IsPastDayOfWeek(DateTime relative, DateTime given)
		{
			return DateTimeFormatHelper.GetRelativeDayOfWeek(relative) > DateTimeFormatHelper.GetRelativeDayOfWeek(given);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00009C84 File Offset: 0x00007E84
		public static bool IsPastDayOfWeekWithWindow(DateTime relative, DateTime given)
		{
			return DateTimeFormatHelper.IsPastDayOfWeek(relative, given) && (double)((int)(relative - given).TotalMinutes) > 180.0;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00009CB8 File Offset: 0x00007EB8
		public static bool IsCurrentCultureJapanese()
		{
			return CultureInfo.CurrentCulture.Name.StartsWith("ja", 5);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00009CCF File Offset: 0x00007ECF
		public static bool IsCurrentCultureKorean()
		{
			return CultureInfo.CurrentCulture.Name.StartsWith("ko", 5);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00009CE6 File Offset: 0x00007EE6
		public static bool IsCurrentCultureTurkish()
		{
			return CultureInfo.CurrentCulture.Name.StartsWith("tr", 5);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00009CFD File Offset: 0x00007EFD
		public static bool IsCurrentCultureHungarian()
		{
			return CultureInfo.CurrentCulture.Name.StartsWith("hu", 5);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00009D14 File Offset: 0x00007F14
		public static bool IsCurrentUICultureFrench()
		{
			return CultureInfo.CurrentUICulture.Name.Equals("fr-FR", 4);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00009D2C File Offset: 0x00007F2C
		public static string GetAbbreviatedDay(DateTime dt)
		{
			if (DateTimeFormatHelper.IsCurrentCultureJapanese() || DateTimeFormatHelper.IsCurrentCultureKorean())
			{
				return "(" + dt.ToString("ddd", CultureInfo.CurrentCulture) + ")";
			}
			return dt.ToString("ddd", CultureInfo.CurrentCulture);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00009D7C File Offset: 0x00007F7C
		public static string GetSuperShortTime(DateTime dt)
		{
			if (DateTimeFormatHelper.formatInfo_GetSuperShortTime == null)
			{
				lock (DateTimeFormatHelper.lock_GetSuperShortTime)
				{
					StringBuilder stringBuilder = new StringBuilder(string.Empty);
					DateTimeFormatHelper.formatInfo_GetSuperShortTime = (DateTimeFormatInfo)CultureInfo.CurrentCulture.DateTimeFormat.Clone();
					stringBuilder.Append(DateTimeFormatHelper.formatInfo_GetSuperShortTime.LongTimePattern);
					string value = DateTimeFormatHelper.rxSeconds.Match(stringBuilder.ToString()).Value;
					stringBuilder.Replace(" ", string.Empty);
					stringBuilder.Replace(value, string.Empty);
					if (!DateTimeFormatHelper.IsCurrentCultureJapanese() && !DateTimeFormatHelper.IsCurrentCultureKorean() && !DateTimeFormatHelper.IsCurrentCultureHungarian())
					{
						stringBuilder.Replace("tt", "t");
					}
					DateTimeFormatHelper.formatInfo_GetSuperShortTime.ShortTimePattern = stringBuilder.ToString();
				}
			}
			return dt.ToString("t", DateTimeFormatHelper.formatInfo_GetSuperShortTime).ToLowerInvariant();
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00009E74 File Offset: 0x00008074
		public static string GetMonthAndDay(DateTime dt)
		{
			if (DateTimeFormatHelper.formatInfo_GetMonthAndDay == null)
			{
				lock (DateTimeFormatHelper.lock_GetMonthAndDay)
				{
					StringBuilder stringBuilder = new StringBuilder(string.Empty);
					DateTimeFormatHelper.formatInfo_GetMonthAndDay = (DateTimeFormatInfo)CultureInfo.CurrentCulture.DateTimeFormat.Clone();
					stringBuilder.Append(DateTimeFormatHelper.rxMonthAndDay.Match(DateTimeFormatHelper.formatInfo_GetMonthAndDay.ShortDatePattern).Value);
					if (stringBuilder.ToString().Contains("."))
					{
						stringBuilder.Append(".");
					}
					DateTimeFormatHelper.formatInfo_GetMonthAndDay.ShortDatePattern = stringBuilder.ToString();
				}
			}
			return dt.ToString("d", DateTimeFormatHelper.formatInfo_GetMonthAndDay);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00009F3C File Offset: 0x0000813C
		public static string GetShortDate(DateTime dt)
		{
			return dt.ToString("d", CultureInfo.CurrentCulture);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00009F50 File Offset: 0x00008150
		public static string GetShortTime(DateTime dt)
		{
			if (DateTimeFormatHelper.formatInfo_GetShortTime == null)
			{
				lock (DateTimeFormatHelper.lock_GetShortTime)
				{
					StringBuilder stringBuilder = new StringBuilder(string.Empty);
					DateTimeFormatHelper.formatInfo_GetShortTime = (DateTimeFormatInfo)CultureInfo.CurrentCulture.DateTimeFormat.Clone();
					stringBuilder.Append(DateTimeFormatHelper.formatInfo_GetSuperShortTime.LongTimePattern);
					string value = DateTimeFormatHelper.rxSeconds.Match(stringBuilder.ToString()).Value;
					stringBuilder.Replace(value, string.Empty);
					DateTimeFormatHelper.formatInfo_GetShortTime.ShortTimePattern = stringBuilder.ToString();
				}
			}
			return dt.ToString("t", DateTimeFormatHelper.formatInfo_GetShortTime);
		}

		// Token: 0x040000E1 RID: 225
		private const double Hour = 60.0;

		// Token: 0x040000E2 RID: 226
		private const double Day = 1440.0;

		// Token: 0x040000E3 RID: 227
		private const string SingleMeridiemDesignator = "t";

		// Token: 0x040000E4 RID: 228
		private const string DoubleMeridiemDesignator = "tt";

		// Token: 0x040000E5 RID: 229
		private static DateTimeFormatInfo formatInfo_GetSuperShortTime = null;

		// Token: 0x040000E6 RID: 230
		private static DateTimeFormatInfo formatInfo_GetMonthAndDay = null;

		// Token: 0x040000E7 RID: 231
		private static DateTimeFormatInfo formatInfo_GetShortTime = null;

		// Token: 0x040000E8 RID: 232
		private static object lock_GetSuperShortTime = new object();

		// Token: 0x040000E9 RID: 233
		private static object lock_GetMonthAndDay = new object();

		// Token: 0x040000EA RID: 234
		private static object lock_GetShortTime = new object();

		// Token: 0x040000EB RID: 235
		private static readonly Regex rxMonthAndDay = new Regex("(d{1,2}[^A-Za-z]M{1,3})|(M{1,3}[^A-Za-z]d{1,2})");

		// Token: 0x040000EC RID: 236
		private static readonly Regex rxSeconds = new Regex("([^A-Za-z]s{1,2})");
	}
}
