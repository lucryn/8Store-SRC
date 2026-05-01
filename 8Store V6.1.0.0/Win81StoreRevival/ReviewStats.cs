using System;
using Windows.ApplicationModel.Resources;

namespace Win81StoreRevival
{
	// Token: 0x02000015 RID: 21
	public class ReviewStats
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x0000361A File Offset: 0x0000181A
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00003622 File Offset: 0x00001822
		public double AverageRating { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x0000362B File Offset: 0x0000182B
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x00003633 File Offset: 0x00001833
		public int ReviewCount { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x0000363C File Offset: 0x0000183C
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00003644 File Offset: 0x00001844
		public int Star1 { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x0000364D File Offset: 0x0000184D
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00003655 File Offset: 0x00001855
		public int Star2 { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000BB RID: 187 RVA: 0x0000365E File Offset: 0x0000185E
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00003666 File Offset: 0x00001866
		public int Star3 { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000BD RID: 189 RVA: 0x0000366F File Offset: 0x0000186F
		// (set) Token: 0x060000BE RID: 190 RVA: 0x00003677 File Offset: 0x00001877
		public int Star4 { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00003680 File Offset: 0x00001880
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x00003688 File Offset: 0x00001888
		public int Star5 { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00003691 File Offset: 0x00001891
		public int Star1Count
		{
			get
			{
				return this.Star1;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00003699 File Offset: 0x00001899
		public int Star2Count
		{
			get
			{
				return this.Star2;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x000036A1 File Offset: 0x000018A1
		public int Star3Count
		{
			get
			{
				return this.Star3;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x000036A9 File Offset: 0x000018A9
		public int Star4Count
		{
			get
			{
				return this.Star4;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000036B1 File Offset: 0x000018B1
		public int Star5Count
		{
			get
			{
				return this.Star5;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000036B9 File Offset: 0x000018B9
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

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x000036E7 File Offset: 0x000018E7
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

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00003715 File Offset: 0x00001915
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

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00003743 File Offset: 0x00001943
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

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00003771 File Offset: 0x00001971
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

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000CB RID: 203 RVA: 0x000037A0 File Offset: 0x000019A0
		public string StarRatingDisplay
		{
			get
			{
				if (this.ReviewCount == 0)
				{
					return ResourceLoader.GetForCurrentView().GetString("NoRatings");
				}
				return this.AverageRating.ToString("F1") + " ★ (" + this.ReviewCount.ToString() + ")";
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000CC RID: 204 RVA: 0x000037F8 File Offset: 0x000019F8
		public string FormattedAverageRating
		{
			get
			{
				return this.AverageRating.ToString("N1");
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00003818 File Offset: 0x00001A18
		public string StarRatingDisplayFull
		{
			get
			{
				int num = (int)Math.Floor(this.AverageRating);
				bool flag = this.AverageRating % 1.0 >= 0.5;
				int num2 = 5 - num - (flag ? 1 : 0);
				string text = new string('★', num);
				if (flag)
				{
					text += "☆";
				}
				return text + new string('☆', num2);
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000CE RID: 206 RVA: 0x0000388B File Offset: 0x00001A8B
		public double FilledStarWidth
		{
			get
			{
				return Math.Min(this.AverageRating * 38.0, 190.0);
			}
		}
	}
}
