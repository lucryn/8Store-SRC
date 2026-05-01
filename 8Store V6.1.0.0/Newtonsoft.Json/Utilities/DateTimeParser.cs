using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000056 RID: 86
	[NullableContext(1)]
	[Nullable(0)]
	internal struct DateTimeParser
	{
		// Token: 0x0600048E RID: 1166 RVA: 0x0001207E File Offset: 0x0001027E
		public bool Parse(char[] text, int startIndex, int length)
		{
			this._text = text;
			this._end = startIndex + length;
			return this.ParseDate(startIndex) && this.ParseChar(DateTimeParser.Lzyyyy_MM_dd + startIndex, 'T') && this.ParseTimeAndZoneAndWhitespace(DateTimeParser.Lzyyyy_MM_ddT + startIndex);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x000120BC File Offset: 0x000102BC
		private bool ParseDate(int start)
		{
			return this.Parse4Digit(start, out this.Year) && 1 <= this.Year && this.ParseChar(start + DateTimeParser.Lzyyyy, '-') && this.Parse2Digit(start + DateTimeParser.Lzyyyy_, out this.Month) && 1 <= this.Month && this.Month <= 12 && this.ParseChar(start + DateTimeParser.Lzyyyy_MM, '-') && this.Parse2Digit(start + DateTimeParser.Lzyyyy_MM_, out this.Day) && 1 <= this.Day && this.Day <= DateTime.DaysInMonth(this.Year, this.Month);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0001216D File Offset: 0x0001036D
		private bool ParseTimeAndZoneAndWhitespace(int start)
		{
			return this.ParseTime(ref start) && this.ParseZone(start);
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00012184 File Offset: 0x00010384
		private bool ParseTime(ref int start)
		{
			if (!this.Parse2Digit(start, out this.Hour) || this.Hour > 24 || !this.ParseChar(start + DateTimeParser.LzHH, ':') || !this.Parse2Digit(start + DateTimeParser.LzHH_, out this.Minute) || this.Minute >= 60 || !this.ParseChar(start + DateTimeParser.LzHH_mm, ':') || !this.Parse2Digit(start + DateTimeParser.LzHH_mm_, out this.Second) || this.Second >= 60 || (this.Hour == 24 && (this.Minute != 0 || this.Second != 0)))
			{
				return false;
			}
			start += DateTimeParser.LzHH_mm_ss;
			if (this.ParseChar(start, '.'))
			{
				this.Fraction = 0;
				int num = 0;
				for (;;)
				{
					int num2 = start + 1;
					start = num2;
					if (num2 >= this._end || num >= 7)
					{
						break;
					}
					int num3 = (int)(this._text[start] - '0');
					if (num3 < 0 || num3 > 9)
					{
						break;
					}
					this.Fraction = this.Fraction * 10 + num3;
					num++;
				}
				if (num < 7)
				{
					if (num == 0)
					{
						return false;
					}
					this.Fraction *= DateTimeParser.Power10[7 - num];
				}
				if (this.Hour == 24 && this.Fraction != 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x000122C4 File Offset: 0x000104C4
		private bool ParseZone(int start)
		{
			if (start < this._end)
			{
				char c = this._text[start];
				if (c == 'Z' || c == 'z')
				{
					this.Zone = ParserTimeZone.Utc;
					start++;
				}
				else
				{
					if (start + 2 < this._end && this.Parse2Digit(start + DateTimeParser.Lz_, out this.ZoneHour) && this.ZoneHour <= 99)
					{
						if (c != '+')
						{
							if (c == '-')
							{
								this.Zone = ParserTimeZone.LocalWestOfUtc;
								start += DateTimeParser.Lz_zz;
							}
						}
						else
						{
							this.Zone = ParserTimeZone.LocalEastOfUtc;
							start += DateTimeParser.Lz_zz;
						}
					}
					if (start < this._end)
					{
						if (this.ParseChar(start, ':'))
						{
							start++;
							if (start + 1 < this._end && this.Parse2Digit(start, out this.ZoneMinute) && this.ZoneMinute <= 99)
							{
								start += 2;
							}
						}
						else if (start + 1 < this._end && this.Parse2Digit(start, out this.ZoneMinute) && this.ZoneMinute <= 99)
						{
							start += 2;
						}
					}
				}
			}
			return start == this._end;
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x000123D0 File Offset: 0x000105D0
		private bool Parse4Digit(int start, out int num)
		{
			if (start + 3 < this._end)
			{
				int num2 = (int)(this._text[start] - '0');
				int num3 = (int)(this._text[start + 1] - '0');
				int num4 = (int)(this._text[start + 2] - '0');
				int num5 = (int)(this._text[start + 3] - '0');
				if (0 <= num2 && num2 < 10 && 0 <= num3 && num3 < 10 && 0 <= num4 && num4 < 10 && 0 <= num5 && num5 < 10)
				{
					num = ((num2 * 10 + num3) * 10 + num4) * 10 + num5;
					return true;
				}
			}
			num = 0;
			return false;
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0001245C File Offset: 0x0001065C
		private bool Parse2Digit(int start, out int num)
		{
			if (start + 1 < this._end)
			{
				int num2 = (int)(this._text[start] - '0');
				int num3 = (int)(this._text[start + 1] - '0');
				if (0 <= num2 && num2 < 10 && 0 <= num3 && num3 < 10)
				{
					num = num2 * 10 + num3;
					return true;
				}
			}
			num = 0;
			return false;
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x000124AE File Offset: 0x000106AE
		private bool ParseChar(int start, char ch)
		{
			return start < this._end && this._text[start] == ch;
		}

		// Token: 0x040001D4 RID: 468
		public int Year;

		// Token: 0x040001D5 RID: 469
		public int Month;

		// Token: 0x040001D6 RID: 470
		public int Day;

		// Token: 0x040001D7 RID: 471
		public int Hour;

		// Token: 0x040001D8 RID: 472
		public int Minute;

		// Token: 0x040001D9 RID: 473
		public int Second;

		// Token: 0x040001DA RID: 474
		public int Fraction;

		// Token: 0x040001DB RID: 475
		public int ZoneHour;

		// Token: 0x040001DC RID: 476
		public int ZoneMinute;

		// Token: 0x040001DD RID: 477
		public ParserTimeZone Zone;

		// Token: 0x040001DE RID: 478
		private char[] _text;

		// Token: 0x040001DF RID: 479
		private int _end;

		// Token: 0x040001E0 RID: 480
		private static readonly int[] Power10 = new int[]
		{
			-1,
			10,
			100,
			1000,
			10000,
			100000,
			1000000
		};

		// Token: 0x040001E1 RID: 481
		private static readonly int Lzyyyy = "yyyy".Length;

		// Token: 0x040001E2 RID: 482
		private static readonly int Lzyyyy_ = "yyyy-".Length;

		// Token: 0x040001E3 RID: 483
		private static readonly int Lzyyyy_MM = "yyyy-MM".Length;

		// Token: 0x040001E4 RID: 484
		private static readonly int Lzyyyy_MM_ = "yyyy-MM-".Length;

		// Token: 0x040001E5 RID: 485
		private static readonly int Lzyyyy_MM_dd = "yyyy-MM-dd".Length;

		// Token: 0x040001E6 RID: 486
		private static readonly int Lzyyyy_MM_ddT = "yyyy-MM-ddT".Length;

		// Token: 0x040001E7 RID: 487
		private static readonly int LzHH = "HH".Length;

		// Token: 0x040001E8 RID: 488
		private static readonly int LzHH_ = "HH:".Length;

		// Token: 0x040001E9 RID: 489
		private static readonly int LzHH_mm = "HH:mm".Length;

		// Token: 0x040001EA RID: 490
		private static readonly int LzHH_mm_ = "HH:mm:".Length;

		// Token: 0x040001EB RID: 491
		private static readonly int LzHH_mm_ss = "HH:mm:ss".Length;

		// Token: 0x040001EC RID: 492
		private static readonly int Lz_ = "-".Length;

		// Token: 0x040001ED RID: 493
		private static readonly int Lz_zz = "-zz".Length;

		// Token: 0x040001EE RID: 494
		private const short MaxFractionDigits = 7;
	}
}
