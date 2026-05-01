using System;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000B9 RID: 185
	internal struct DateTimeParser
	{
		// Token: 0x06000924 RID: 2340 RVA: 0x000232B0 File Offset: 0x000214B0
		public bool Parse(string text)
		{
			this._text = text;
			this._length = text.Length;
			return this.ParseDate(0) && this.ParseChar(DateTimeParser.Lzyyyy_MM_dd, 'T') && this.ParseTimeAndZoneAndWhitespace(DateTimeParser.Lzyyyy_MM_ddT);
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x000232F0 File Offset: 0x000214F0
		private bool ParseDate(int start)
		{
			return this.Parse4Digit(start, out this.Year) && 1 <= this.Year && this.ParseChar(start + DateTimeParser.Lzyyyy, '-') && this.Parse2Digit(start + DateTimeParser.Lzyyyy_, out this.Month) && 1 <= this.Month && this.Month <= 12 && this.ParseChar(start + DateTimeParser.Lzyyyy_MM, '-') && this.Parse2Digit(start + DateTimeParser.Lzyyyy_MM_, out this.Day) && 1 <= this.Day && this.Day <= DateTime.DaysInMonth(this.Year, this.Month);
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x000233A1 File Offset: 0x000215A1
		private bool ParseTimeAndZoneAndWhitespace(int start)
		{
			return this.ParseTime(ref start) && this.ParseZone(start);
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x000233B8 File Offset: 0x000215B8
		private bool ParseTime(ref int start)
		{
			if (!this.Parse2Digit(start, out this.Hour) || this.Hour >= 24 || !this.ParseChar(start + DateTimeParser.LzHH, ':') || !this.Parse2Digit(start + DateTimeParser.LzHH_, out this.Minute) || this.Minute >= 60 || !this.ParseChar(start + DateTimeParser.LzHH_mm, ':') || !this.Parse2Digit(start + DateTimeParser.LzHH_mm_, out this.Second) || this.Second >= 60)
			{
				return false;
			}
			start += DateTimeParser.LzHH_mm_ss;
			if (this.ParseChar(start, '.'))
			{
				this.Fraction = 0;
				int num = 0;
				while (++start < this._length && num < 7)
				{
					int num2 = (int)(this._text.get_Chars(start) - '0');
					if (num2 < 0 || num2 > 9)
					{
						break;
					}
					this.Fraction = this.Fraction * 10 + num2;
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
			}
			return true;
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x000234CC File Offset: 0x000216CC
		private bool ParseZone(int start)
		{
			if (start < this._length)
			{
				char c = this._text.get_Chars(start);
				if (c == 'Z' || c == 'z')
				{
					this.Zone = ParserTimeZone.Utc;
					start++;
				}
				else if (start + 5 < this._length && this.Parse2Digit(start + DateTimeParser.Lz_, out this.ZoneHour) && this.ZoneHour <= 99 && this.ParseChar(start + DateTimeParser.Lz_zz, ':') && this.Parse2Digit(start + DateTimeParser.Lz_zz_, out this.ZoneMinute) && this.ZoneMinute <= 99)
				{
					switch (c)
					{
					case '+':
						this.Zone = ParserTimeZone.LocalEastOfUtc;
						start += DateTimeParser.Lz_zz_zz;
						break;
					case '-':
						this.Zone = ParserTimeZone.LocalWestOfUtc;
						start += DateTimeParser.Lz_zz_zz;
						break;
					}
				}
			}
			return start == this._length;
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x000235B0 File Offset: 0x000217B0
		private bool Parse4Digit(int start, out int num)
		{
			if (start + 3 < this._length)
			{
				int num2 = (int)(this._text.get_Chars(start) - '0');
				int num3 = (int)(this._text.get_Chars(start + 1) - '0');
				int num4 = (int)(this._text.get_Chars(start + 2) - '0');
				int num5 = (int)(this._text.get_Chars(start + 3) - '0');
				if (0 <= num2 && num2 < 10 && 0 <= num3 && num3 < 10 && 0 <= num4 && num4 < 10 && 0 <= num5 && num5 < 10)
				{
					num = ((num2 * 10 + num3) * 10 + num4) * 10 + num5;
					return true;
				}
			}
			num = 0;
			return false;
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x0002364C File Offset: 0x0002184C
		private bool Parse2Digit(int start, out int num)
		{
			if (start + 1 < this._length)
			{
				int num2 = (int)(this._text.get_Chars(start) - '0');
				int num3 = (int)(this._text.get_Chars(start + 1) - '0');
				if (0 <= num2 && num2 < 10 && 0 <= num3 && num3 < 10)
				{
					num = num2 * 10 + num3;
					return true;
				}
			}
			num = 0;
			return false;
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x000236A6 File Offset: 0x000218A6
		private bool ParseChar(int start, char ch)
		{
			return start < this._length && this._text.get_Chars(start) == ch;
		}

		// Token: 0x04000370 RID: 880
		private const short MaxFractionDigits = 7;

		// Token: 0x04000371 RID: 881
		public int Year;

		// Token: 0x04000372 RID: 882
		public int Month;

		// Token: 0x04000373 RID: 883
		public int Day;

		// Token: 0x04000374 RID: 884
		public int Hour;

		// Token: 0x04000375 RID: 885
		public int Minute;

		// Token: 0x04000376 RID: 886
		public int Second;

		// Token: 0x04000377 RID: 887
		public int Fraction;

		// Token: 0x04000378 RID: 888
		public int ZoneHour;

		// Token: 0x04000379 RID: 889
		public int ZoneMinute;

		// Token: 0x0400037A RID: 890
		public ParserTimeZone Zone;

		// Token: 0x0400037B RID: 891
		private string _text;

		// Token: 0x0400037C RID: 892
		private int _length;

		// Token: 0x0400037D RID: 893
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

		// Token: 0x0400037E RID: 894
		private static readonly int Lzyyyy = "yyyy".Length;

		// Token: 0x0400037F RID: 895
		private static readonly int Lzyyyy_ = "yyyy-".Length;

		// Token: 0x04000380 RID: 896
		private static readonly int Lzyyyy_MM = "yyyy-MM".Length;

		// Token: 0x04000381 RID: 897
		private static readonly int Lzyyyy_MM_ = "yyyy-MM-".Length;

		// Token: 0x04000382 RID: 898
		private static readonly int Lzyyyy_MM_dd = "yyyy-MM-dd".Length;

		// Token: 0x04000383 RID: 899
		private static readonly int Lzyyyy_MM_ddT = "yyyy-MM-ddT".Length;

		// Token: 0x04000384 RID: 900
		private static readonly int LzHH = "HH".Length;

		// Token: 0x04000385 RID: 901
		private static readonly int LzHH_ = "HH:".Length;

		// Token: 0x04000386 RID: 902
		private static readonly int LzHH_mm = "HH:mm".Length;

		// Token: 0x04000387 RID: 903
		private static readonly int LzHH_mm_ = "HH:mm:".Length;

		// Token: 0x04000388 RID: 904
		private static readonly int LzHH_mm_ss = "HH:mm:ss".Length;

		// Token: 0x04000389 RID: 905
		private static readonly int Lz_ = "-".Length;

		// Token: 0x0400038A RID: 906
		private static readonly int Lz_zz = "-zz".Length;

		// Token: 0x0400038B RID: 907
		private static readonly int Lz_zz_ = "-zz:".Length;

		// Token: 0x0400038C RID: 908
		private static readonly int Lz_zz_zz = "-zz:zz".Length;
	}
}
