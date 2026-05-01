using System;
using Windows.ApplicationModel.Resources;

namespace Win81StoreRevival
{
	// Token: 0x02000014 RID: 20
	public class ReviewStats
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00003A18 File Offset: 0x00001C18
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00003A20 File Offset: 0x00001C20
		public double AverageRating { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00003A29 File Offset: 0x00001C29
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00003A31 File Offset: 0x00001C31
		public int ReviewCount { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00003A3A File Offset: 0x00001C3A
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00003A42 File Offset: 0x00001C42
		public int Star1 { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00003A4B File Offset: 0x00001C4B
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x00003A53 File Offset: 0x00001C53
		public int Star2 { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00003A5C File Offset: 0x00001C5C
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00003A64 File Offset: 0x00001C64
		public int Star3 { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00003A6D File Offset: 0x00001C6D
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x00003A75 File Offset: 0x00001C75
		public int Star4 { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00003A7E File Offset: 0x00001C7E
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00003A86 File Offset: 0x00001C86
		public int Star5 { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00003A8F File Offset: 0x00001C8F
		public int Star1Count
		{
			get
			{
				return this.Star1;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00003A97 File Offset: 0x00001C97
		public int Star2Count
		{
			get
			{
				return this.Star2;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00003A9F File Offset: 0x00001C9F
		public int Star3Count
		{
			get
			{
				return this.Star3;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00003AA7 File Offset: 0x00001CA7
		public int Star4Count
		{
			get
			{
				return this.Star4;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00003AAF File Offset: 0x00001CAF
		public int Star5Count
		{
			get
			{
				return this.Star5;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00003AB7 File Offset: 0x00001CB7
		public double Star1Percentage
		{
			get
			{
				return (this.ReviewCount > 0) ? ((double)this.Star1 * 240.0 / (double)this.ReviewCount) : 0.0;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00003AE6 File Offset: 0x00001CE6
		public double Star2Percentage
		{
			get
			{
				return (this.ReviewCount > 0) ? ((double)this.Star2 * 240.0 / (double)this.ReviewCount) : 0.0;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00003B15 File Offset: 0x00001D15
		public double Star3Percentage
		{
			get
			{
				return (this.ReviewCount > 0) ? ((double)this.Star3 * 240.0 / (double)this.ReviewCount) : 0.0;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00003B44 File Offset: 0x00001D44
		public double Star4Percentage
		{
			get
			{
				return (this.ReviewCount > 0) ? ((double)this.Star4 * 240.0 / (double)this.ReviewCount) : 0.0;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00003B73 File Offset: 0x00001D73
		public double Star5Percentage
		{
			get
			{
				return (this.ReviewCount > 0) ? ((double)this.Star5 * 240.0 / (double)this.ReviewCount) : 0.0;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00003BA4 File Offset: 0x00001DA4
		public string StarRatingDisplay
		{
			get
			{
				bool flag = this.ReviewCount == 0;
				string result;
				if (flag)
				{
					ResourceLoader forCurrentView = ResourceLoader.GetForCurrentView();
					result = forCurrentView.GetString("NoRatings");
				}
				else
				{
					result = this.AverageRating.ToString("F1") + " ★ (" + this.ReviewCount.ToString() + ")";
				}
				return result;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00003C0C File Offset: 0x00001E0C
		public string FormattedAverageRating
		{
			get
			{
				return this.AverageRating.ToString("N1");
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00003C34 File Offset: 0x00001E34
		public string StarRatingDisplayFull
		{
			get
			{
				int num = (int)Math.Floor(this.AverageRating);
				bool flag = this.AverageRating % 1.0 >= 0.5;
				int num2 = 5 - num - (flag ? 1 : 0);
				string text = new string('★', num);
				bool flag2 = flag;
				if (flag2)
				{
					text += "☆";
				}
				return text + new string('☆', num2);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00003CB4 File Offset: 0x00001EB4
		public double FilledStarWidth
		{
			get
			{
				return Math.Min(this.AverageRating * 38.0, 190.0);
			}
		}
	}
}
