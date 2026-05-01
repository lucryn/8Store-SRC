using System;
using Newtonsoft.Json;

namespace Win81StoreRevival
{
	// Token: 0x0200000D RID: 13
	public class Review
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00003543 File Offset: 0x00001743
		// (set) Token: 0x0600004C RID: 76 RVA: 0x0000354B File Offset: 0x0000174B
		[JsonProperty("id")]
		public int Id { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00003554 File Offset: 0x00001754
		// (set) Token: 0x0600004E RID: 78 RVA: 0x0000355C File Offset: 0x0000175C
		[JsonProperty("app_id")]
		public string AppId { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00003565 File Offset: 0x00001765
		// (set) Token: 0x06000050 RID: 80 RVA: 0x0000356D File Offset: 0x0000176D
		[JsonProperty("user_id")]
		public string UserId { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00003576 File Offset: 0x00001776
		// (set) Token: 0x06000052 RID: 82 RVA: 0x0000357E File Offset: 0x0000177E
		[JsonProperty("username")]
		public string Username { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00003587 File Offset: 0x00001787
		// (set) Token: 0x06000054 RID: 84 RVA: 0x0000358F File Offset: 0x0000178F
		[JsonProperty("rating")]
		public int Rating { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00003598 File Offset: 0x00001798
		// (set) Token: 0x06000056 RID: 86 RVA: 0x000035A0 File Offset: 0x000017A0
		[JsonProperty("comment")]
		public string Comment { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000057 RID: 87 RVA: 0x000035A9 File Offset: 0x000017A9
		// (set) Token: 0x06000058 RID: 88 RVA: 0x000035B1 File Offset: 0x000017B1
		[JsonProperty("created_at")]
		public DateTime CreatedAt { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000059 RID: 89 RVA: 0x000035BA File Offset: 0x000017BA
		// (set) Token: 0x0600005A RID: 90 RVA: 0x000035C2 File Offset: 0x000017C2
		[JsonIgnore]
		public bool IsOwnReview { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000035CC File Offset: 0x000017CC
		[JsonIgnore]
		public string RatingDisplay
		{
			get
			{
				return "★" + this.Rating.ToString();
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600005C RID: 92 RVA: 0x000035F8 File Offset: 0x000017F8
		[JsonIgnore]
		public string RatingStars
		{
			get
			{
				return new string('★', Math.Max(0, Math.Min(5, this.Rating)));
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00003628 File Offset: 0x00001828
		[JsonIgnore]
		public string DateDisplay
		{
			get
			{
				return this.CreatedAt.ToString("MMM dd, yyyy");
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003650 File Offset: 0x00001850
		[JsonIgnore]
		public bool ShowShortComment
		{
			get
			{
				return !this.ShowReadMore;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600005F RID: 95 RVA: 0x0000366C File Offset: 0x0000186C
		[JsonIgnore]
		public bool ShowReadMore
		{
			get
			{
				return !string.IsNullOrEmpty(this.Comment) && this.Comment.Length > 140;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000036A0 File Offset: 0x000018A0
		[JsonIgnore]
		public string CommentPreview
		{
			get
			{
				bool flag = string.IsNullOrEmpty(this.Comment);
				string result;
				if (flag)
				{
					result = string.Empty;
				}
				else
				{
					result = ((this.Comment.Length > 140) ? (this.Comment.Substring(0, 140) + "...") : this.Comment);
				}
				return result;
			}
		}
	}
}
