using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Win81StoreRevival
{
	// Token: 0x02000014 RID: 20
	public class RatingSummary
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00003412 File Offset: 0x00001612
		// (set) Token: 0x06000098 RID: 152 RVA: 0x0000341A File Offset: 0x0000161A
		[JsonProperty("average_rating")]
		public double AverageRating { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003423 File Offset: 0x00001623
		// (set) Token: 0x0600009A RID: 154 RVA: 0x0000342B File Offset: 0x0000162B
		[JsonProperty("review_count")]
		public int ReviewCount { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00003434 File Offset: 0x00001634
		// (set) Token: 0x0600009C RID: 156 RVA: 0x0000343C File Offset: 0x0000163C
		[JsonProperty("star1")]
		public int Star1 { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00003445 File Offset: 0x00001645
		// (set) Token: 0x0600009E RID: 158 RVA: 0x0000344D File Offset: 0x0000164D
		[JsonProperty("star2")]
		public int Star2 { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00003456 File Offset: 0x00001656
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x0000345E File Offset: 0x0000165E
		[JsonProperty("star3")]
		public int Star3 { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003467 File Offset: 0x00001667
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x0000346F File Offset: 0x0000166F
		[JsonProperty("star4")]
		public int Star4 { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003478 File Offset: 0x00001678
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00003480 File Offset: 0x00001680
		[JsonProperty("star5")]
		public int Star5 { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00003489 File Offset: 0x00001689
		[JsonIgnore]
		public int Star1Count
		{
			get
			{
				return this.Star1;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00003491 File Offset: 0x00001691
		[JsonIgnore]
		public int Star2Count
		{
			get
			{
				return this.Star2;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00003499 File Offset: 0x00001699
		[JsonIgnore]
		public int Star3Count
		{
			get
			{
				return this.Star3;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x000034A1 File Offset: 0x000016A1
		[JsonIgnore]
		public int Star4Count
		{
			get
			{
				return this.Star4;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x000034A9 File Offset: 0x000016A9
		[JsonIgnore]
		public int Star5Count
		{
			get
			{
				return this.Star5;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000AA RID: 170 RVA: 0x000034B1 File Offset: 0x000016B1
		[JsonIgnore]
		public double Star1Percentage
		{
			get
			{
				if (this.ReviewCount <= 0)
				{
					return 0.0;
				}
				return (double)this.Star1 * 240.0 / (double)this.ReviewCount;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000AB RID: 171 RVA: 0x000034DF File Offset: 0x000016DF
		[JsonIgnore]
		public double Star2Percentage
		{
			get
			{
				if (this.ReviewCount <= 0)
				{
					return 0.0;
				}
				return (double)this.Star2 * 240.0 / (double)this.ReviewCount;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000AC RID: 172 RVA: 0x0000350D File Offset: 0x0000170D
		[JsonIgnore]
		public double Star3Percentage
		{
			get
			{
				if (this.ReviewCount <= 0)
				{
					return 0.0;
				}
				return (double)this.Star3 * 240.0 / (double)this.ReviewCount;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000AD RID: 173 RVA: 0x0000353B File Offset: 0x0000173B
		[JsonIgnore]
		public double Star4Percentage
		{
			get
			{
				if (this.ReviewCount <= 0)
				{
					return 0.0;
				}
				return (double)this.Star4 * 240.0 / (double)this.ReviewCount;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00003569 File Offset: 0x00001769
		[JsonIgnore]
		public double Star5Percentage
		{
			get
			{
				if (this.ReviewCount <= 0)
				{
					return 0.0;
				}
				return (double)this.Star5 * 240.0 / (double)this.ReviewCount;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00003598 File Offset: 0x00001798
		[JsonIgnore]
		public string FormattedAverageRating
		{
			get
			{
				return this.AverageRating.ToString("N1");
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000031B2 File Offset: 0x000013B2
		[JsonIgnore]
		public List<int> StarBg
		{
			get
			{
				List<int> list = new List<int>();
				list.Add(1);
				list.Add(1);
				list.Add(1);
				list.Add(1);
				list.Add(1);
				return list;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x000035B8 File Offset: 0x000017B8
		[JsonIgnore]
		public List<double> StarList
		{
			get
			{
				List<double> list = new List<double>();
				double num = this.AverageRating;
				for (int i = 1; i <= 5; i++)
				{
					if (num >= (double)i)
					{
						list.Add(1.0);
					}
					else if (num > (double)(i - 1))
					{
						list.Add(num - (double)(i - 1));
					}
					else
					{
						list.Add(0.0);
					}
				}
				return list;
			}
		}
	}
}
