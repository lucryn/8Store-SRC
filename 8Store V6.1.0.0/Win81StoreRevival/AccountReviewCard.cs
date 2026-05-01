using System;
using System.Collections.Generic;

namespace Win81StoreRevival
{
	// Token: 0x02000017 RID: 23
	public sealed class AccountReviewCard
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000EE RID: 238 RVA: 0x000042D9 File Offset: 0x000024D9
		// (set) Token: 0x060000EF RID: 239 RVA: 0x000042E1 File Offset: 0x000024E1
		public StoreApp App { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x000042EA File Offset: 0x000024EA
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x000042F2 File Offset: 0x000024F2
		public Review Review { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x000042FB File Offset: 0x000024FB
		public string AppTitle
		{
			get
			{
				if (this.App != null)
				{
					return this.App.Name;
				}
				if (this.Review == null)
				{
					return "";
				}
				return this.Review.AppId;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x0000432A File Offset: 0x0000252A
		public string AppImageUrl
		{
			get
			{
				if (this.App == null)
				{
					return "";
				}
				return this.App.IconUrl;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00004345 File Offset: 0x00002545
		public string ReviewUsername
		{
			get
			{
				if (this.Review == null)
				{
					return "";
				}
				return this.Review.Username;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00004360 File Offset: 0x00002560
		public string ReviewDate
		{
			get
			{
				if (this.Review == null)
				{
					return "";
				}
				return this.Review.DateDisplay;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x0000437B File Offset: 0x0000257B
		public string ReviewPreview
		{
			get
			{
				if (this.Review == null)
				{
					return "";
				}
				return this.Review.CommentPreview;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00004396 File Offset: 0x00002596
		public string ReviewFullText
		{
			get
			{
				if (this.Review == null)
				{
					return "";
				}
				return this.Review.Comment;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x000043B1 File Offset: 0x000025B1
		public bool ReviewHasMore
		{
			get
			{
				return this.Review != null && !string.IsNullOrEmpty(this.Review.Comment) && this.Review.Comment.Length > 150;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x000043E6 File Offset: 0x000025E6
		public List<int> ReviewStarBg
		{
			get
			{
				if (this.Review == null)
				{
					List<int> list = new List<int>();
					list.Add(1);
					list.Add(1);
					list.Add(1);
					list.Add(1);
					list.Add(1);
					return list;
				}
				return this.Review.StarBg;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00004424 File Offset: 0x00002624
		public List<double> ReviewStarList
		{
			get
			{
				if (this.Review == null)
				{
					List<double> list = new List<double>();
					list.Add(0.0);
					list.Add(0.0);
					list.Add(0.0);
					list.Add(0.0);
					list.Add(0.0);
					return list;
				}
				return this.Review.StarList;
			}
		}
	}
}
