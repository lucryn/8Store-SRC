using System;
using Newtonsoft.Json;

namespace Win81StoreRevival
{
	// Token: 0x02000013 RID: 19
	public class RatingSummary
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000091 RID: 145 RVA: 0x0000386C File Offset: 0x00001A6C
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00003874 File Offset: 0x00001A74
		[JsonProperty("average_rating")]
		public double AverageRating { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000093 RID: 147 RVA: 0x0000387D File Offset: 0x00001A7D
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00003885 File Offset: 0x00001A85
		[JsonProperty("review_count")]
		public int ReviewCount { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000095 RID: 149 RVA: 0x0000388E File Offset: 0x00001A8E
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00003896 File Offset: 0x00001A96
		[JsonProperty("star1")]
		public int Star1 { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000097 RID: 151 RVA: 0x0000389F File Offset: 0x00001A9F
		// (set) Token: 0x06000098 RID: 152 RVA: 0x000038A7 File Offset: 0x00001AA7
		[JsonProperty("star2")]
		public int Star2 { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000099 RID: 153 RVA: 0x000038B0 File Offset: 0x00001AB0
		// (set) Token: 0x0600009A RID: 154 RVA: 0x000038B8 File Offset: 0x00001AB8
		[JsonProperty("star3")]
		public int Star3 { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000038C1 File Offset: 0x00001AC1
		// (set) Token: 0x0600009C RID: 156 RVA: 0x000038C9 File Offset: 0x00001AC9
		[JsonProperty("star4")]
		public int Star4 { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600009D RID: 157 RVA: 0x000038D2 File Offset: 0x00001AD2
		// (set) Token: 0x0600009E RID: 158 RVA: 0x000038DA File Offset: 0x00001ADA
		[JsonProperty("star5")]
		public int Star5 { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000038E3 File Offset: 0x00001AE3
		[JsonIgnore]
		public int Star1Count
		{
			get
			{
				return this.Star1;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000038EB File Offset: 0x00001AEB
		[JsonIgnore]
		public int Star2Count
		{
			get
			{
				return this.Star2;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x000038F3 File Offset: 0x00001AF3
		[JsonIgnore]
		public int Star3Count
		{
			get
			{
				return this.Star3;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000038FB File Offset: 0x00001AFB
		[JsonIgnore]
		public int Star4Count
		{
			get
			{
				return this.Star4;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003903 File Offset: 0x00001B03
		[JsonIgnore]
		public int Star5Count
		{
			get
			{
				return this.Star5;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x0000390B File Offset: 0x00001B0B
		[JsonIgnore]
		public double Star1Percentage
		{
			get
			{
				return (this.ReviewCount > 0) ? ((double)this.Star1 * 240.0 / (double)this.ReviewCount) : 0.0;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x0000393A File Offset: 0x00001B3A
		[JsonIgnore]
		public double Star2Percentage
		{
			get
			{
				return (this.ReviewCount > 0) ? ((double)this.Star2 * 240.0 / (double)this.ReviewCount) : 0.0;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00003969 File Offset: 0x00001B69
		[JsonIgnore]
		public double Star3Percentage
		{
			get
			{
				return (this.ReviewCount > 0) ? ((double)this.Star3 * 240.0 / (double)this.ReviewCount) : 0.0;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00003998 File Offset: 0x00001B98
		[JsonIgnore]
		public double Star4Percentage
		{
			get
			{
				return (this.ReviewCount > 0) ? ((double)this.Star4 * 240.0 / (double)this.ReviewCount) : 0.0;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x000039C7 File Offset: 0x00001BC7
		[JsonIgnore]
		public double Star5Percentage
		{
			get
			{
				return (this.ReviewCount > 0) ? ((double)this.Star5 * 240.0 / (double)this.ReviewCount) : 0.0;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x000039F8 File Offset: 0x00001BF8
		[JsonIgnore]
		public string FormattedAverageRating
		{
			get
			{
				return this.AverageRating.ToString("N1");
			}
		}
	}
}
