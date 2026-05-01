using System;
using System.Globalization;
using Windows.ApplicationModel.Resources;
using Windows.ApplicationModel.Resources.Core;
using Windows.UI.Xaml.Data;

namespace Callisto.Converters
{
	// Token: 0x0200002A RID: 42
	public class RelativeTimeConverter : IValueConverter
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x0000A08B File Offset: 0x0000828B
		private static ResourceLoader TimeResources
		{
			get
			{
				return new ResourceLoader("Callisto/Resources");
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x0000A097 File Offset: 0x00008297
		private static CultureInfo PreferredCulture
		{
			get
			{
				return new CultureInfo(RelativeTimeConverter.CurrentLanguageTag);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x0000A0A3 File Offset: 0x000082A3
		public static string CurrentLanguageTag
		{
			get
			{
				return ResourceManager.Current.DefaultContext.Languages[0].ToString();
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000A0C0 File Offset: 0x000082C0
		private void SetLocalizationCulture(CultureInfo culture)
		{
			this.PluralHourStrings = new string[]
			{
				RelativeTimeConverter.TimeResources.GetString("XHoursAgo_2To4"),
				RelativeTimeConverter.TimeResources.GetString("XHoursAgo_EndsIn1Not11"),
				RelativeTimeConverter.TimeResources.GetString("XHoursAgo_EndsIn2To4Not12To14"),
				RelativeTimeConverter.TimeResources.GetString("XHoursAgo_Other")
			};
			this.PluralMinuteStrings = new string[]
			{
				RelativeTimeConverter.TimeResources.GetString("XMinutesAgo_2To4"),
				RelativeTimeConverter.TimeResources.GetString("XMinutesAgo_EndsIn1Not11"),
				RelativeTimeConverter.TimeResources.GetString("XMinutesAgo_EndsIn2To4Not12To14"),
				RelativeTimeConverter.TimeResources.GetString("XMinutesAgo_Other")
			};
			this.PluralSecondStrings = new string[]
			{
				RelativeTimeConverter.TimeResources.GetString("XSecondsAgo_2To4"),
				RelativeTimeConverter.TimeResources.GetString("XSecondsAgo_EndsIn1Not11"),
				RelativeTimeConverter.TimeResources.GetString("XSecondsAgo_EndsIn2To4Not12To14"),
				RelativeTimeConverter.TimeResources.GetString("XSecondsAgo_Other")
			};
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000A1D0 File Offset: 0x000083D0
		private static string GetPluralMonth(int month)
		{
			if (month >= 2 && month <= 4)
			{
				return string.Format(RelativeTimeConverter.PreferredCulture, RelativeTimeConverter.TimeResources.GetString("XMonthsAgo_2To4"), new object[]
				{
					month.ToString(RelativeTimeConverter.PreferredCulture)
				});
			}
			if (month >= 5 && month <= 12)
			{
				return string.Format(RelativeTimeConverter.PreferredCulture, RelativeTimeConverter.TimeResources.GetString("XMonthsAgo_5To12"), new object[]
				{
					month.ToString(RelativeTimeConverter.PreferredCulture)
				});
			}
			throw new ArgumentException(RelativeTimeConverter.TimeResources.GetString("InvalidNumberOfMonths"));
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000A264 File Offset: 0x00008464
		private static string GetPluralTimeUnits(int units, string[] resources)
		{
			int num = units % 10;
			int num2 = units % 100;
			if (units <= 1)
			{
				throw new ArgumentException(RelativeTimeConverter.TimeResources.GetString("InvalidNumberOfTimeUnits"));
			}
			if (units >= 2 && units <= 4)
			{
				return string.Format(RelativeTimeConverter.PreferredCulture, resources[0], new object[]
				{
					units.ToString(RelativeTimeConverter.PreferredCulture)
				});
			}
			if (num == 1 && num2 != 11)
			{
				return string.Format(RelativeTimeConverter.PreferredCulture, resources[1], new object[]
				{
					units.ToString(RelativeTimeConverter.PreferredCulture)
				});
			}
			if (num >= 2 && num <= 4 && (num2 < 12 || num2 > 14))
			{
				return string.Format(RelativeTimeConverter.PreferredCulture, resources[2], new object[]
				{
					units.ToString(RelativeTimeConverter.PreferredCulture)
				});
			}
			return string.Format(RelativeTimeConverter.PreferredCulture, resources[3], new object[]
			{
				units.ToString(RelativeTimeConverter.PreferredCulture)
			});
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000A350 File Offset: 0x00008550
		private static string GetDayOfWeek(DayOfWeek dow)
		{
			string @string;
			switch (dow)
			{
			case 0:
				@string = RelativeTimeConverter.TimeResources.GetString("Sunday");
				break;
			case 1:
				@string = RelativeTimeConverter.TimeResources.GetString("Monday");
				break;
			case 2:
				@string = RelativeTimeConverter.TimeResources.GetString("Tuesday");
				break;
			case 3:
				@string = RelativeTimeConverter.TimeResources.GetString("Wednesday");
				break;
			case 4:
				@string = RelativeTimeConverter.TimeResources.GetString("Thursday");
				break;
			case 5:
				@string = RelativeTimeConverter.TimeResources.GetString("Friday");
				break;
			case 6:
				@string = RelativeTimeConverter.TimeResources.GetString("Saturday");
				break;
			default:
				@string = RelativeTimeConverter.TimeResources.GetString("Sunday");
				break;
			}
			return @string;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000A414 File Offset: 0x00008614
		private static string GetOnDayOfWeek(DayOfWeek dow)
		{
			if (dow == 2)
			{
				return string.Format(RelativeTimeConverter.PreferredCulture, RelativeTimeConverter.TimeResources.GetString("OnDayOfWeek_Tuesday"), new object[]
				{
					RelativeTimeConverter.GetDayOfWeek(dow)
				});
			}
			return string.Format(RelativeTimeConverter.PreferredCulture, RelativeTimeConverter.TimeResources.GetString("OnDayOfWeek_Other"), new object[]
			{
				RelativeTimeConverter.GetDayOfWeek(dow)
			});
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000A47C File Offset: 0x0000867C
		public object Convert(object value, Type targetType, object parameter, string culture)
		{
			if (!(value is DateTime))
			{
				throw new ArgumentException(RelativeTimeConverter.TimeResources.GetString("InvalidDateTimeArgument"));
			}
			DateTime dateTime = ((DateTime)value).ToLocalTime();
			DateTime now = DateTime.Now;
			TimeSpan timeSpan = now - dateTime;
			this.SetLocalizationCulture(RelativeTimeConverter.PreferredCulture);
			string result;
			if (DateTimeFormatHelper.IsFutureDateTime(now, dateTime))
			{
				result = RelativeTimeConverter.GetPluralTimeUnits(2, this.PluralSecondStrings);
			}
			if (timeSpan.TotalSeconds > 31536000.0)
			{
				result = RelativeTimeConverter.TimeResources.GetString("OverAYearAgo");
			}
			else if (timeSpan.TotalSeconds > 3952800.0)
			{
				int month = (int)((timeSpan.TotalSeconds + 1317600.0) / 2635200.0);
				result = RelativeTimeConverter.GetPluralMonth(month);
			}
			else if (timeSpan.TotalSeconds >= 2116800.0)
			{
				result = RelativeTimeConverter.TimeResources.GetString("AboutAMonthAgo");
			}
			else if (timeSpan.TotalSeconds >= 604800.0)
			{
				int num = (int)(timeSpan.TotalSeconds / 604800.0);
				if (num > 1)
				{
					result = string.Format(RelativeTimeConverter.PreferredCulture, RelativeTimeConverter.TimeResources.GetString("XWeeksAgo_2To4"), new object[]
					{
						num.ToString(RelativeTimeConverter.PreferredCulture)
					});
				}
				else
				{
					result = RelativeTimeConverter.TimeResources.GetString("AboutAWeekAgo");
				}
			}
			else if (timeSpan.TotalSeconds >= 432000.0)
			{
				result = string.Format(RelativeTimeConverter.PreferredCulture, RelativeTimeConverter.TimeResources.GetString("LastDayOfWeek"), new object[]
				{
					RelativeTimeConverter.GetDayOfWeek(dateTime.DayOfWeek)
				});
			}
			else if (timeSpan.TotalSeconds >= 86400.0)
			{
				result = RelativeTimeConverter.GetOnDayOfWeek(dateTime.DayOfWeek);
			}
			else if (timeSpan.TotalSeconds >= 7200.0)
			{
				int units = (int)(timeSpan.TotalSeconds / 3600.0);
				result = RelativeTimeConverter.GetPluralTimeUnits(units, this.PluralHourStrings);
			}
			else if (timeSpan.TotalSeconds >= 3600.0)
			{
				result = RelativeTimeConverter.TimeResources.GetString("AboutAnHourAgo");
			}
			else if (timeSpan.TotalSeconds >= 120.0)
			{
				int units2 = (int)(timeSpan.TotalSeconds / 60.0);
				result = RelativeTimeConverter.GetPluralTimeUnits(units2, this.PluralMinuteStrings);
			}
			else if (timeSpan.TotalSeconds >= 60.0)
			{
				result = RelativeTimeConverter.TimeResources.GetString("AboutAMinuteAgo");
			}
			else
			{
				int units3 = ((double)((int)timeSpan.TotalSeconds) > 1.0) ? ((int)timeSpan.TotalSeconds) : 2;
				result = RelativeTimeConverter.GetPluralTimeUnits(units3, this.PluralSecondStrings);
			}
			return result;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000A742 File Offset: 0x00008942
		public object ConvertBack(object value, Type targetType, object parameter, string culture)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040000ED RID: 237
		private const double Minute = 60.0;

		// Token: 0x040000EE RID: 238
		private const double Hour = 3600.0;

		// Token: 0x040000EF RID: 239
		private const double Day = 86400.0;

		// Token: 0x040000F0 RID: 240
		private const double Week = 604800.0;

		// Token: 0x040000F1 RID: 241
		private const double Month = 2635200.0;

		// Token: 0x040000F2 RID: 242
		private const double Year = 31536000.0;

		// Token: 0x040000F3 RID: 243
		private const string DefaultCulture = "en-US";

		// Token: 0x040000F4 RID: 244
		private string[] PluralHourStrings;

		// Token: 0x040000F5 RID: 245
		private string[] PluralMinuteStrings;

		// Token: 0x040000F6 RID: 246
		private string[] PluralSecondStrings;
	}
}
